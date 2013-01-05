/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
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
using System.Collections.Generic;
using de.ahzf.Styx;

#endregion

namespace de.ahzf.Styx.Sensors.Active
{

    #region ActiveSensor<TId, TValue>

    /// <summary>
    /// A generic active sensor.
    /// </summary>
    /// <typeparam name="TId">The type of the unique identification.</typeparam>
    /// <typeparam name="TValue">The type of the returned sensor data.</typeparam>
    public class ActiveSensor<TId, TValue> : AbstractActiveSensor<TId, TValue>
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    {

        #region Data

        /// <summary>
        /// The internal TValue enumerator.
        /// </summary>
        private readonly IEnumerator<TValue> IEnumerator;

        #endregion

        #region Properties

        #region InitialDelay

        /// <summary>
        /// The initial delay before starting to fire asynchronously.
        /// </summary>
        public Nullable<TimeSpan> InitialDelay { get; private set; }

        #endregion

        #endregion

        #region Constructor(s)

        #region ActiveSensor(ISensor<TId, TValue>, Autostart = false, StartAsTask = false, InitialDelay = 0)

        /// <summary>
        /// An active sensor based on a passive ISensor&lt;TId, TValue&gt;.
        /// All needed information will be taken from the wrapped sensor.
        /// </summary>
        /// <param name="ISensor">An ISensor&lt;TId, TValue&gt;.</param>
        /// <param name="Autostart">Start the sniper automatically.</param>
        /// <param name="StartAsTask">Start the ActiveSensor within its own task.</param>
        /// <param name="InitialDelay">Set the initial delay of the sniper.</param>
        public ActiveSensor(ISensor<TId, TValue>     ISensor,
                            Boolean                  Autostart    = false,
                            Boolean                  StartAsTask  = false,
                            Nullable<TimeSpan>       InitialDelay = null)

            : base(ISensor.Id, ISensor.Name, ISensor.ValueUnit)

        {

            #region Initial Checks

            if (ISensor == null)
                throw new ArgumentNullException("The given ISensor must not be null!");

            #endregion

            this.IEnumerator  = ISensor;
            this.InitialDelay = InitialDelay;

            if (Autostart)
                base.StartMeasurements();

        }

        #endregion

        #region ActiveSensor(ISensor<TId, TValue>, MessageRecipient.Recipient, Autostart = false, StartAsTask = false, InitialDelay = 0)

        /// <summary>
        /// An active sensor based on a passive ISensor&lt;TId, TValue&gt;.
        /// All needed information will be taken from the wrapped sensor.
        /// </summary>
        /// <param name="ISensor">An ISensor&lt;TId, TValue&gt;.</param>
        /// <param name="Recipient">A recipient of the processed measurements.</param>
        /// <param name="Autostart">Start the sniper automatically.</param>
        /// <param name="StartAsTask">Start the ActiveSensor within its own task.</param>
        /// <param name="InitialDelay">Set the initial delay of the sniper.</param>
        public ActiveSensor(ISensor<TId, TValue>     ISensor,
                            MessageRecipient<TValue> Recipient,
                            Boolean                  Autostart    = false,
                            Boolean                  StartAsTask  = false,
                            Nullable<TimeSpan>       InitialDelay = null)

            : this(ISensor, Autostart, StartAsTask, InitialDelay)

        {

            #region Initial Checks

            if (Recipient == null)
                throw new ArgumentNullException("The given Recipient must not be null!");

            #endregion

            lock (this)
            {
                this.OnMessageAvailable += Recipient;
            }

        }

        #endregion

        #region ActiveSensor(ISensor<TId, TValue>, IArrowReceiver.Recipient, Autostart = false, StartAsTask = false, InitialDelay = 0)

        /// <summary>
        /// An active sensor based on a passive ISensor&lt;TId, TValue&gt;.
        /// All needed information will be taken from the wrapped sensor.
        /// </summary>
        /// <param name="ISensor">An ISensor&lt;TId, TValue&gt;.</param>
        /// <param name="Recipient">A recipient of the processed measurements.</param>
        /// <param name="Autostart">Start the sniper automatically.</param>
        /// <param name="StartAsTask">Start the ActiveSensor within its own task.</param>
        /// <param name="InitialDelay">Set the initial delay of the sniper.</param>
        public ActiveSensor(ISensor<TId, TValue>   ISensor,
                            IArrowReceiver<TValue> Recipient,
                            Boolean                Autostart    = false,
                            Boolean                StartAsTask  = false,
                            Nullable<TimeSpan>     InitialDelay = null)

            : this(ISensor, Autostart, StartAsTask, InitialDelay)

        {

            #region Initial Checks

            if (Recipient == null)
                throw new ArgumentNullException("The given Recipient must not be null!");

            #endregion

            lock (this)
            {
                this.OnMessageAvailable += Recipient.ReceiveMessage;
            }

        }

        #endregion


        #region ActiveSensor(IEnumerable, SensorId, SensorName, ValueUnit, Autostart = false, StartAsTask = false, InitialDelay = 0)

        /// <summary>
        /// An active sensor.
        /// </summary>
        /// <param name="IEnumerable">An enumeration of TValues.</param>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        /// <param name="ValueUnit">The unit of the value (m, m², °C, °F, MBytes, ...).</param>
        /// <param name="Autostart">Start the sniper automatically.</param>
        /// <param name="StartAsTask">Start the ActiveSensor within its own task.</param>
        /// <param name="InitialDelay">Set the initial delay of the sniper.</param>
        public ActiveSensor(IEnumerable<TValue> IEnumerable,
                            TId                 SensorId,
                            String              SensorName,
                            String              ValueUnit,
                            Boolean             Autostart    = false,
                            Boolean             StartAsTask  = false,
                            Nullable<TimeSpan>  InitialDelay = null)

            : base(SensorId, SensorName, ValueUnit)

        {

            #region Initial Checks

            if (IEnumerable == null)
                throw new ArgumentNullException("The given IEnumerable must not be null!");

            if (SensorId    == null)
                throw new ArgumentNullException("The given SensorId must not be null!");

            if (SensorName  == null)
                throw new ArgumentNullException("The given SensorName must not be null!");

            if (ValueUnit   == null)
                throw new ArgumentNullException("The given ValueUnit must not be null!");

            #endregion

            this.IEnumerator = IEnumerable.GetEnumerator();

            if (this.IEnumerator == null)
                throw new ArgumentNullException("IEnumerable.GetEnumerator() must not be null!");

            if (Autostart)
                base.StartMeasurements();

        }

        #endregion

        #region ActiveSensor(IEnumerable, SensorId, SensorName, ValueUnit, MessageRecipient.Recipient, Autostart = false, StartAsTask = false, InitialDelay = 0)

        /// <summary>
        /// An active sensor.
        /// </summary>
        /// <param name="IEnumerable">An enumeration of TValues.</param>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        /// <param name="ValueUnit">The unit of the value (m, m², °C, °F, MBytes, ...).</param>
        /// <param name="Recipient">A recipient of the processed measurements.</param>
        /// <param name="Autostart">Start the sniper automatically.</param>
        /// <param name="StartAsTask">Start the ActiveSensor within its own task.</param>
        /// <param name="InitialDelay">Set the initial delay of the sniper.</param>
        public ActiveSensor(IEnumerable<TValue>      IEnumerable,                            
                            TId                      SensorId,
                            String                   SensorName,
                            String                   ValueUnit,
                            MessageRecipient<TValue> Recipient,
                            Boolean                  Autostart    = false,
                            Boolean                  StartAsTask  = false,
                            Nullable<TimeSpan>       InitialDelay = null)

            : this(IEnumerable, SensorId, SensorName, ValueUnit, Autostart, StartAsTask, InitialDelay)

        {

            #region Initial Checks

            if (Recipient == null)
                throw new ArgumentNullException("The given Recipient must not be null!");

            #endregion

            lock (this)
            {
                this.OnMessageAvailable += Recipient;
            }

        }

        #endregion

        #region ActiveSensor(IEnumerable, SensorId, SensorName, ValueUnit, IArrowReceiver.Recipient, Autostart = false, StartAsTask = false, InitialDelay = 0)

        /// <summary>
        /// An active sensor.
        /// </summary>
        /// <param name="IEnumerable">An enumeration of TValues.</param>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        /// <param name="ValueUnit">The unit of the value (m, m², °C, °F, MBytes, ...).</param>
        /// <param name="Recipient">A recipient of the processed measurements.</param>
        /// <param name="Autostart">Start the sniper automatically.</param>
        /// <param name="StartAsTask">Start the ActiveSensor within its own task.</param>
        /// <param name="InitialDelay">Set the initial delay of the sniper.</param>
        public ActiveSensor(IEnumerable<TValue>    IEnumerable,
                            TId                    SensorId,
                            String                 SensorName,
                            String                 ValueUnit,
                            IArrowReceiver<TValue> Recipient,
                            Boolean                Autostart    = false,
                            Boolean                StartAsTask  = false,
                            Nullable<TimeSpan>     InitialDelay = null)

            : this(IEnumerable, SensorId, SensorName, ValueUnit, Autostart, StartAsTask, InitialDelay)

        {

            #region Initial Checks

            if (Recipient == null)
                throw new ArgumentNullException("The given Recipient must not be null!");

            #endregion

            lock (this)
            {
                this.OnMessageAvailable += Recipient.ReceiveMessage;
            }

        }

        #endregion


        #region ActiveSensor(IEnumerator, SensorId, SensorName, ValueUnit, Autostart = false, StartAsTask = false, InitialDelay = 0)

        /// <summary>
        /// An active sensor.
        /// </summary>
        /// <param name="IEnumerator">An enumerator of TValues.</param>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        /// <param name="ValueUnit">The unit of the value (m, m², °C, °F, MBytes, ...).</param>
        /// <param name="Autostart">Start the sniper automatically.</param>
        /// <param name="StartAsTask">Start the ActiveSensor within its own task.</param>
        /// <param name="InitialDelay">Set the initial delay of the sniper.</param>
        public ActiveSensor(IEnumerator<TValue> IEnumerator,
                            TId                 SensorId,
                            String              SensorName,
                            String              ValueUnit,
                            Boolean             Autostart    = false,
                            Boolean             StartAsTask  = false,
                            Nullable<TimeSpan>  InitialDelay = null)

            : base(SensorId, SensorName, ValueUnit)

        {

            #region Initial Checks

            if (IEnumerator == null)
                throw new ArgumentNullException("The given IEnumerator must not be null!");

            if (SensorId    == null)
                throw new ArgumentNullException("The given SensorId must not be null!");

            if (SensorName  == null)
                throw new ArgumentNullException("The given SensorName must not be null!");

            if (ValueUnit   == null)
                throw new ArgumentNullException("The given ValueUnit must not be null!");

            #endregion

            this.IEnumerator = IEnumerator;

            if (Autostart)
                base.StartMeasurements();

        }

        #endregion

        #region ActiveSensor(IEnumerable, SensorId, SensorName, ValueUnit, MessageRecipient.Recipient, Autostart = false, StartAsTask = false, InitialDelay = 0)

        /// <summary>
        /// An active sensor.
        /// </summary>
        /// <param name="IEnumerator">An enumerator of TValues.</param>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        /// <param name="ValueUnit">The unit of the value (m, m², °C, °F, MBytes, ...).</param>
        /// <param name="Recipient">A recipient of the processed measurements.</param>
        /// <param name="Autostart">Start the sniper automatically.</param>
        /// <param name="StartAsTask">Start the ActiveSensor within its own task.</param>
        /// <param name="InitialDelay">Set the initial delay of the sniper.</param>
        public ActiveSensor(IEnumerator<TValue>      IEnumerator,                            
                            TId                      SensorId,
                            String                   SensorName,
                            String                   ValueUnit,
                            MessageRecipient<TValue> Recipient,
                            Boolean                  Autostart    = false,
                            Boolean                  StartAsTask  = false,
                            Nullable<TimeSpan>       InitialDelay = null)

            : this(IEnumerator, SensorId, SensorName, ValueUnit, Autostart, StartAsTask, InitialDelay)

        {

            #region Initial Checks

            if (Recipient == null)
                throw new ArgumentNullException("The given Recipient must not be null!");

            #endregion

            lock (this)
            {
                this.OnMessageAvailable += Recipient;
            }

        }

        #endregion

        #region ActiveSensor(IEnumerable, SensorId, SensorName, ValueUnit, IArrowReceiver.Recipient, Autostart = false, StartAsTask = false, InitialDelay = 0)

        /// <summary>
        /// An active sensor.
        /// </summary>
        /// <param name="IEnumerator">An enumerator of TValues.</param>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        /// <param name="ValueUnit">The unit of the value (m, m², °C, °F, MBytes, ...).</param>
        /// <param name="Recipient">A recipient of the processed measurements.</param>
        /// <param name="Autostart">Start the sniper automatically.</param>
        /// <param name="StartAsTask">Start the ActiveSensor within its own task.</param>
        /// <param name="InitialDelay">Set the initial delay of the sniper.</param>
        public ActiveSensor(IEnumerator<TValue>    IEnumerator,                            
                            TId                    SensorId,
                            String                 SensorName,
                            String                 ValueUnit,
                            IArrowReceiver<TValue> Recipient,
                            Boolean                Autostart    = false,
                            Boolean                StartAsTask  = false,
                            Nullable<TimeSpan>     InitialDelay = null)

            : this(IEnumerator, SensorId, SensorName, ValueUnit, Autostart, StartAsTask, InitialDelay)

        {

            #region Initial Checks

            if (Recipient == null)
                throw new ArgumentNullException("The given Recipient must not be null!");

            #endregion

            lock (this)
            {
                this.OnMessageAvailable += Recipient.ReceiveMessage;
            }

        }

        #endregion

        #endregion

        #region MoveNext()

        /// <summary>
        /// The current value of this performance counter.
        /// </summary>
        public override Boolean MoveNext()
        {

            // Respect sensor throttling in the AbstractSensor class.
            base.MoveNext();

            if (IEnumerator.MoveNext())
            {
                _Current = IEnumerator.Current;
                return true;
            }

            return false;

        }

        #endregion

    }

    #endregion

}
