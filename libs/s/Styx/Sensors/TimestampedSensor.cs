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
using System.Linq;
using System.Collections.Generic;

using de.ahzf.Illias.Commons;

#endregion

namespace de.ahzf.Vanaheimr.Styx.Sensors.Active
{

    #region TimestampedSensor<TId, TValue>

    /// <summary>
    /// A generic sensor.
    /// </summary>
    /// <typeparam name="TId">The type of the unique identification.</typeparam>
    /// <typeparam name="TValue">The type of the returned sensor data.</typeparam>
    public class TimestampedSensor<TId, TValue> : ISensor<TId, Measurement<TValue>>
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    {

        #region Data

        /// <summary>
        /// The internal sensor.
        /// </summary>
        private readonly ISensor<TId, TValue> InternalSensor;

        #endregion

        #region Properties

        #region Id

        /// <summary>
        /// A unique identification of the sensor.
        /// </summary>
        public TId Id
        {
            get
            {
                return InternalSensor.Id;
            }
        }

        #endregion

        #region Name

        /// <summary>
        /// The user-friendly name of the sensor.
        /// </summary>
        public String Name
        {
            get
            {
                return InternalSensor.Name;
            }
        }

        #endregion

        #region Description

        /// <summary>
        /// An description of this sensor.
        /// </summary>
        public String Description
        {

            get
            {
                return InternalSensor.Description;
            }

            set
            {
                InternalSensor.Description = value;
            }

        }

        #endregion

        #region ValueUnit

        /// <summary>
        /// The unit of the value (m, m², °C, °F, MBytes, ...).
        /// </summary>
        public String ValueUnit
        {
            get { return InternalSensor.ValueUnit; }
        }

        #endregion

        #region IsActive

        /// <summary>
        /// Whether this sensor is active or passive.
        /// </summary>
        public Boolean IsActive
        {
            get
            {
                return InternalSensor.IsActive;
            }
        }

        #endregion


        #region Intervall

        /// <summary>
        /// The intervall will throttle the measurements of passive
        /// sensors and the event notifications of active sensors.
        /// </summary>
        public TimeSpan MeasurementIntervall
        {

            get
            {
                return InternalSensor.MeasurementIntervall;
            }

            set
            {
                InternalSensor.MeasurementIntervall = value;
            }

        }

        #endregion

        #region ThrottlingSleepDuration

        /// <summary>
        /// The amount of time in milliseconds a passive sensor
        /// will sleep if it is in throttling mode.
        /// </summary>
        public TimeSpan ThrottlingSleepDuration
        {
            get { return InternalSensor.ThrottlingSleepDuration; }
        }

        #endregion

        #region InitializationTimestamp

        /// <summary>
        /// The timestamp when this sensor was initialized.
        /// </summary>
        public DateTime InitializationTimestamp
        {
            get
            {
                return InternalSensor.InitializationTimestamp;
            }
        }

        #endregion

        #region Now

        /// <summary>
        /// The current time.
        /// </summary>
        public DateTime Now
        {
            get
            {
                return InternalSensor.Now;
            }
        }

        #endregion

        #region LastMeasurementAt

        /// <summary>
        /// The timestamp of the last time this sensor invoked a measurement.
        /// </summary>
        public DateTime LastMeasurementAt
        {
            get
            {
                return InternalSensor.LastMeasurementAt;
            }
        }

        #endregion

        #region NextMeasurementAt

        /// <summary>
        /// The timestamp of the next time this sensor will invoked a measurement.
        /// </summary>
        public DateTime NextMeasurementAt
        {
            get
            {
                return InternalSensor.NextMeasurementAt;
            }
        }

        #endregion

        #region Current

        /// <summary>
        /// The current value of the sensor.
        /// </summary>
        public Measurement<TValue> Current
        {
            get
            {
                return new Measurement<TValue>(Now, InternalSensor.Current);
            }
        }

        /// <summary>
        /// The current value of the sensor.
        /// </summary>
        Object System.Collections.IEnumerator.Current
        {
            get
            {
                return new Measurement<TValue>(Now, InternalSensor.Current);
            }
        }

        #endregion

        #region CurrentValue

        /// <summary>
        /// Invokes a new measurement and return the current value of the sensor.
        /// </summary>
        public Measurement<TValue> CurrentValue
        {
            get
            {
                var Value = InternalSensor.CurrentValue;
                return new Measurement<TValue>(LastMeasurementAt, Value);
            }
        }

        #endregion

        #region TimestampedValue

        /// <summary>
        /// The current value of this sensor and its measurement timestamp.
        /// </summary>
        public Measurement<Measurement<TValue>> TimestampedValue
        {
            get
            {
                return new Measurement<Measurement<TValue>>(InternalSensor.LastMeasurementAt, new Measurement<TValue>(InternalSensor.LastMeasurementAt, InternalSensor.Current));
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region Sensor(ISensor<TId, TValue>)

        /// <summary>
        /// A sensor based on another ISensor&lt;TId, TValue&gt;.
        /// All needed information will be taken from the wrapped sensor.
        /// </summary>
        /// <param name="ISensor">An ISensor&lt;TId, TValue&gt;.</param>
        public TimestampedSensor(ISensor<TId, TValue> ISensor)
        {

            #region Initial Checks

            if (ISensor == null)
                throw new ArgumentNullException("The given ISensor must not be null!");

            #endregion

            this.InternalSensor = ISensor;

        }

        #endregion

        #endregion


        #region GetEnumerator()

        /// <summary>
        /// Return an IEnumerator.
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return InternalSensor.GetEnumerator();
        }

        /// <summary>
        /// Return an IEnumerator&lt;Measurement&lt;TValue&gt&gt;.
        /// </summary>        
        public IEnumerator<Measurement<TValue>> GetEnumerator()
        {
            return (from   Value
                    in     InternalSensor
                    select new Measurement<TValue>(LastMeasurementAt, Value)).GetEnumerator();
        }

        #endregion

        #region MoveNext()

        /// <summary>
        /// The current value of this performance counter.
        /// </summary>
        public Boolean MoveNext()
        {
            return InternalSensor.MoveNext();
        }

        #endregion

        #region Reset()

        /// <summary>
        /// Reset the sensor.
        /// </summary>
        public void Reset()
        {
            InternalSensor.Reset();
        }

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
            return InternalSensor.Equals(OtherId);
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
            return InternalSensor.Id.Equals(OtherSensor.Id);
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
            if ((Object)ISensor == null)
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
            return InternalSensor.Id.CompareTo(OtherId);
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
            return InternalSensor.Id.CompareTo(OtherSensor.Id);
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
            return InternalSensor.Id.GetHashCode();
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Dispose this pbject.
        /// </summary>
        public virtual void Dispose()
        {
            InternalSensor.Dispose();
        }

        #endregion

    }

    #endregion

}
