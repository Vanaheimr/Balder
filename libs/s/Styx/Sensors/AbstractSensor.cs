/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Styx <http://www.github.com/Vanaheimr/Styx>
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region Usings

using System;
using System.Threading;
using de.ahzf.Illias.Commons;

#endregion

namespace de.ahzf.Vanaheimr.Styx.Sensors
{

    /// <summary>
    /// The abstract sensor.
    /// </summary>
    /// <typeparam name="TId">The type of the unique identification.</typeparam>
    /// <typeparam name="TValue">The type of the returned sensor data.</typeparam>
    public abstract class AbstractSensor<TId, TValue> : ISensor<TId, TValue>
        where TId : IComparable, IComparable<TId>, IEquatable<TId>
    {

        #region Data

        /// <summary>
        /// Shadow copy of the LastMeasurementAt property
        /// to save the value for the next measurement.
        /// </summary>
        private DateTime _LastMeasurementAt;

        #endregion

        #region Properties

        #region Id

        /// <summary>
        /// A unique identification of the sensor.
        /// </summary>
        public TId    Id   { get; private set; }

        #endregion

        #region Name

        /// <summary>
        /// The user-friendly name of the sensor.
        /// </summary>
        public String Name { get; private set; }

        #endregion

        #region Description

        /// <summary>
        /// An description of this sensor.
        /// </summary>
        public String Description { get; set; }

        #endregion

        #region ValueUnit

        /// <summary>
        /// The unit of the value (m, m², °C, °F, MBytes, ...).
        /// </summary>
        public String ValueUnit { get; protected set; }

        #endregion

        #region IsActive

        /// <summary>
        /// Whether this sensor is active or passive.
        /// </summary>
        public virtual Boolean IsActive { get; protected set; }

        #endregion


        #region MeasurementIntervall

        /// <summary>
        /// The intervall will throttle the measurements of passive
        /// sensors and the event notifications of active sensors.
        /// </summary>
        public TimeSpan MeasurementIntervall { get; set; }

        #endregion

        #region ThrottlingSleepDuration

        /// <summary>
        /// The amount of time in milliseconds a passive sensor
        /// will sleep if it is in throttling mode.
        /// </summary>
        public TimeSpan ThrottlingSleepDuration { get; set; }

        #endregion


        #region InitializationTimestamp

        /// <summary>
        /// The timestamp when this sensor was initialized.
        /// </summary>
        public DateTime InitializationTimestamp { get; private set; }

        #endregion

        #region Now

        /// <summary>
        /// The current time.
        /// </summary>
        public DateTime Now { get; protected set; }

        #endregion

        #region LastMeasurementAt

        /// <summary>
        /// The timestamp of the last time this sensor invoked a measurement.
        /// </summary>
        public DateTime LastMeasurementAt { get; protected set; }

        #endregion

        #region NextMeasurementAt

        /// <summary>
        /// The timestamp of the next time this sensor will invoked a measurement.
        /// </summary>
        public DateTime NextMeasurementAt { get; protected set; }

        #endregion

        #region Current

        /// <summary>
        /// The current value of the sensor.
        /// </summary>
        protected TValue _Current;

        /// <summary>
        /// The current value of the sensor.
        /// </summary>
        public TValue Current
        {
            get
            {
                return _Current;
            }
        }

        /// <summary>
        /// The current value of the sensor.
        /// </summary>
        object System.Collections.IEnumerator.Current
        {
            get
            {
                return _Current;
            }
        }

        #endregion

        #region CurrentValue

        /// <summary>
        /// Invokes a new measurement and return the current value of the sensor.
        /// </summary>
        public TValue CurrentValue
        {
            get
            {

                // Take a new measurement and return the result.
                if (MoveNext())
                    return _Current;

                return default(TValue);

            }
        }

        #endregion

        #region TimestampedValue

        /// <summary>
        /// The current value of this sensor and its measurement timestamp.
        /// </summary>
        public Measurement<TValue> TimestampedValue
        {
            get
            {
                return new Measurement<TValue>(Now, _Current);
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region AbstractSensor(SensorId, SensorName, ValueUnit)

        /// <summary>
        /// Creates a new sensor having the given Id and name.
        /// </summary>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        /// <param name="ValueUnit">The unit of the value (m, m², °C, °F, MBytes, ...).</param>
        public AbstractSensor(TId SensorId, String SensorName, String ValueUnit)
        {

            #region Initial Checks

            if (SensorId == null)
                throw new ArgumentNullException("The SensorId must not be null!");

            if (SensorName == null)
                throw new ArgumentNullException("The SensorName must not be null!");

            if (ValueUnit == null)
                throw new ArgumentNullException("The ValueUnit must not be null!");

            #endregion

            this.Id                      = SensorId;
            this.Name                    = SensorName;
            this.ValueUnit               = ValueUnit;
            this.InitializationTimestamp = DateTime.Now;
            this._LastMeasurementAt      = DateTime.MinValue;
            this.LastMeasurementAt       = DateTime.MinValue;
            this.NextMeasurementAt       = InitializationTimestamp;
            this.ThrottlingSleepDuration = TimeSpan.FromMilliseconds(50);
            this.IsActive                = false;

        }

        #endregion

        #endregion


        #region GetEnumerator()

        /// <summary>
        /// Return an IEnumerator.
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this;
        }

        /// <summary>
        /// Return an IEnumerator&lt;TValue&gt;.
        /// </summary>
        public System.Collections.Generic.IEnumerator<TValue> GetEnumerator()
        {
            return this;
        }

        #endregion

        #region MoveNext()

        /// <summary>
        /// Take a new sensor measurement.
        /// </summary>
        /// <returns>True if succeeded, false otherwise.</returns>
        public virtual Boolean MoveNext()
        {

            // Take the value from its shadow copy!
            LastMeasurementAt = _LastMeasurementAt;

            // Sleep if we are in throttling mode
            while (NextMeasurementAt - ThrottlingSleepDuration > DateTime.Now)
                Thread.Sleep(ThrottlingSleepDuration);

            // Sleep for the remaining waiting time - 5ms
            var MiniSleep = NextMeasurementAt - DateTime.Now - TimeSpan.FromMilliseconds(5);
            if (MiniSleep.TotalMilliseconds > 0)
                Thread.Sleep(MiniSleep);

            while (DateTime.Now < NextMeasurementAt)
                Thread.Sleep(1);

            // Set the current timestamp
            Now                = DateTime.Now;

            // Avoid to stack delays!
            NextMeasurementAt += MeasurementIntervall;

            // This might be a little bit later than expected!
            _LastMeasurementAt = Now;

            return true;

        }

        #endregion

        #region Reset()

        /// <summary>
        /// Reset the sensor.
        /// </summary>
        public void Reset()
        {
            var Now = DateTime.Now;
            LastMeasurementAt = Now;
            NextMeasurementAt = Now;
        }

        #endregion


        #region Operator overloading

        #region Operator == (AbstractSensor1, AbstractSensor2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AbstractSensor1">A AbstractSensor<TId, TValue>.</param>
        /// <param name="AbstractSensor2">Another AbstractSensor<TId, TValue>.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (AbstractSensor<TId, TValue> AbstractSensor1, AbstractSensor<TId, TValue> AbstractSensor2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(AbstractSensor1, AbstractSensor2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) AbstractSensor1 == null) || ((Object) AbstractSensor2 == null))
                return false;

            return AbstractSensor1.Equals(AbstractSensor2);

        }

        #endregion

        #region Operator != (AbstractSensor1, AbstractSensor2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AbstractSensor1">A AbstractSensor<TId, TValue>.</param>
        /// <param name="AbstractSensor2">Another AbstractSensor<TId, TValue>.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (AbstractSensor<TId, TValue> AbstractSensor1, AbstractSensor<TId, TValue> AbstractSensor2)
        {
            return !(AbstractSensor1 == AbstractSensor2);
        }

        #endregion

        #region Operator <  (AbstractSensor1, AbstractSensor2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AbstractSensor1">A AbstractSensor<TId, TValue>.</param>
        /// <param name="AbstractSensor2">Another AbstractSensor<TId, TValue>.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <  (AbstractSensor<TId, TValue> AbstractSensor1, AbstractSensor<TId, TValue> AbstractSensor2)
        {

            if ((Object)AbstractSensor1 == null)
                throw new ArgumentNullException("The given AbstractSensor1 must not be null!");

            return AbstractSensor1.CompareTo(AbstractSensor2) < 0;

        }

        #endregion

        #region Operator <= (AbstractSensor1, AbstractSensor2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AbstractSensor1">A AbstractSensor<TId, TValue>.</param>
        /// <param name="AbstractSensor2">Another AbstractSensor<TId, TValue>.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (AbstractSensor<TId, TValue> AbstractSensor1, AbstractSensor<TId, TValue> AbstractSensor2)
        {
            return !(AbstractSensor1 > AbstractSensor2);
        }

        #endregion

        #region Operator >  (AbstractSensor1, AbstractSensor2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AbstractSensor1">A AbstractSensor<TId, TValue>.</param>
        /// <param name="AbstractSensor2">Another AbstractSensor<TId, TValue>.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >  (AbstractSensor<TId, TValue> AbstractSensor1, AbstractSensor<TId, TValue> AbstractSensor2)
        {

            if ((Object)AbstractSensor1 == null)
                throw new ArgumentNullException("The given AbstractSensor1 must not be null!");

            return AbstractSensor1.CompareTo(AbstractSensor2) > 0;

        }

        #endregion

        #region Operator >= (AbstractSensor1, AbstractSensor2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AbstractSensor1">A AbstractSensor<TId, TValue>.</param>
        /// <param name="AbstractSensor2">Another AbstractSensor<TId, TValue>.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (AbstractSensor<TId, TValue> AbstractSensor1, AbstractSensor<TId, TValue> AbstractSensor2)
        {
            return !(AbstractSensor1 < AbstractSensor2);
        }

        #endregion

        #endregion

        #region IEquatable<TId> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>True if equal, false otherwise.</returns>
        public override Boolean Equals(Object Object)
        {

            if (Object == null)
                return false;

            // Check if the given object is an ISensor<TId, TValue>.
            var ISensor = Object as ISensor<TId, TValue>;
            if ((Object) ISensor == null)
                return false;

            return this.Equals(ISensor);

        }

        #endregion

        #region Equals(OtherId)

        /// <summary>
        /// Compares the Ids of two sensors for equality.
        /// </summary>
        /// <param name="OtherId">Another sensor Id.</param>
        /// <returns>True if equal, false otherwise.</returns>
        public Boolean Equals(TId OtherId)
        {
            return this.Id.Equals(OtherId);
        }

        #endregion

        #region Equals(OtherSensor)

        /// <summary>
        /// Compares two sensors for equality.
        /// </summary>
        /// <param name="OtherChargePoint">Another sensor.</param>
        /// <returns>True if equal, false otherwise.</returns>
        public Boolean Equals(ISensor<TId, TValue> OtherSensor)
        {
            return this.Id.Equals(OtherSensor.Id);
        }

        #endregion

        #endregion

        #region IComparable<TId> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares the Ids of two sensors.
        /// </summary>
        /// <param name="Object">Another sensor Id.</param>
        /// <returns>0 if equal, -1, 1 otherwise.</returns>
        public Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given object must not be null!");

            // Check if the given object is an ISensor<TId, TValue>.
            var ISensor = Object as ISensor<TId, TValue>;
            if ((Object) ISensor == null)
                throw new ArgumentException("The given object is not an ISensor<TId, TValue>!");

            return CompareTo(ISensor);

        }

        #endregion

        #region CompareTo(OtherId)

        /// <summary>
        /// Compares the Ids of two sensors.
        /// </summary>
        /// <param name="OtherId">Another sensor Id.</param>
        /// <returns>0 if equal, -1, 1 otherwise.</returns>
        public Int32 CompareTo(TId OtherId)
        {
            return this.Id.CompareTo(OtherId);
        }

        #endregion

        #region CompareTo(OtherSensor)

        /// <summary>
        /// Compares two sensors.
        /// </summary>
        /// <param name="OtherSensor">Another sensor.</param>
        /// <returns>0 if equal, -1, 1 otherwise.</returns>
        public Int32 CompareTo(ISensor<TId, TValue> OtherSensor)
        {
            return this.Id.CompareTo(OtherSensor.Id);
        }

        #endregion

        #endregion

        #region GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override Int32 GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Return a string represtentation of this object.
        /// </summary>
        public override String ToString()
        {
            return base.ToString();
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Dispose this pbject.
        /// </summary>
        public virtual void Dispose()
        { }

        #endregion

    }

}
