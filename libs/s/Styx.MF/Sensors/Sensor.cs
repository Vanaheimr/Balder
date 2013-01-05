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

#endregion

namespace de.ahzf.Styx.Sensors.Active
{

    #region Sensor<TId, TValue>

    /// <summary>
    /// A generic sensor.
    /// </summary>
    /// <typeparam name="TId">The type of the unique identification.</typeparam>
    /// <typeparam name="TValue">The type of the returned sensor data.</typeparam>
    public class Sensor<TId, TValue> : AbstractSensor<TId, TValue>
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    {

        #region Data

        /// <summary>
        /// The internal TValue enumerator.
        /// </summary>
        private readonly IEnumerator<TValue> IEnumerator;

        #endregion

        #region Constructor(s)

        #region Sensor(ISensor<TId, TValue>)

        /// <summary>
        /// A sensor based on another ISensor&lt;TId, TValue&gt;.
        /// All needed information will be taken from the wrapped sensor.
        /// </summary>
        /// <param name="ISensor">An ISensor&lt;TId, TValue&gt;.</param>
        public Sensor(ISensor<TId, TValue> ISensor)
            : base(ISensor.Id, ISensor.Name, ISensor.ValueUnit)
        {

            #region Initial Checks

            if (ISensor == null)
                throw new ArgumentNullException("The given ISensor must not be null!");

            #endregion

            this.IEnumerator = ISensor;

        }

        #endregion

        #region Sensor(IEnumerable, SensorId, SensorName, ValueUnit)

        /// <summary>
        /// An  sensor.
        /// </summary>
        /// <param name="IEnumerable">An enumeration of TValues.</param>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        /// <param name="ValueUnit">The unit of the value (m, m², °C, °F, MBytes, ...).</param>
        public Sensor(IEnumerable<TValue> IEnumerable,
                      TId                 SensorId,
                      String              SensorName,
                      String              ValueUnit)

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

        }

        #endregion

        #region Sensor(IEnumerator, SensorId, SensorName, ValueUnit)

        /// <summary>
        /// An  sensor.
        /// </summary>
        /// <param name="IEnumerator">An enumerator of TValues.</param>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        /// <param name="ValueUnit">The unit of the value (m, m², °C, °F, MBytes, ...).</param>
        public Sensor(IEnumerator<TValue> IEnumerator,
                      TId                 SensorId,
                      String              SensorName,
                      String              ValueUnit)

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
