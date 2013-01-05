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

#endregion

namespace de.ahzf.Vanaheimr.Styx.Sensors.Simple
{

    #region CounterSensor

    /// <summary>
    /// A sensor returning the actual value of a counter.
    /// </summary>
    public class CounterSensor : CounterSensor<SensorId>
    {

        #region CounterSensor(SensorName, StartValue = 0)

        /// <summary>
        /// A sensor returning the actual value of a counter.
        /// </summary>
        /// <param name="SensorName">The name of the sensor.</param>
        /// <param name="StartValue">An optional start value for the counter.</param>
        public CounterSensor(String SensorName, Int64 StartValue = 0)
            : base(SensorId.NewSensorId, SensorName, StartValue)
        { }

        #endregion

        #region CounterSensor(SensorId, SensorName, StartValue = 0)

        /// <summary>
        /// A sensor returning the actual value of a counter.
        /// </summary>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        /// <param name="StartValue">An optional start value for the counter.</param>
        public CounterSensor(SensorId SensorId, String SensorName, Int64 StartValue = 0)
            : base(SensorId, SensorName, StartValue)
        { }

        #endregion

        #region CounterSensor(SensorId_UInt64, SensorName, StartValue = 0)

        /// <summary>
        /// A sensor returning the actual value of a counter.
        /// </summary>
        /// <param name="SensorId_UInt64">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        /// <param name="StartValue">An optional start value for the counter.</param>
        public CounterSensor(UInt64 SensorId_UInt64, String SensorName, Int64 StartValue = 0)
            : base(new SensorId(SensorId_UInt64), SensorName, StartValue)
        { }

        #endregion

    }

    #endregion

    #region CounterSensor<TId>

    /// <summary>
    /// A sensor returning the actual value of a counter.
    /// </summary>
    /// <typeparam name="TId">The type of the unique identification.</typeparam>
    public class CounterSensor<TId> : AbstractSensor<TId, Int64>
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    {

        #region CounterSensor(SensorId, SensorName, StartValue = 0)

        /// <summary>
        /// A sensor returning the actual value of a counter.
        /// </summary>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        /// <param name="StartValue">An optional start value for the counter.</param>
        public CounterSensor(TId SensorId, String SensorName, Int64 StartValue = 0)
            : base(SensorId, SensorName, "Int64")
        {
            _Current = StartValue - 1;
        }

        #endregion

        #region MoveNext()

        /// <summary>
        /// The current value of this performance counter.
        /// </summary>
        public override Boolean MoveNext()
        {

            // Respect sensor throttling in the AbstractSensor class.
            base.MoveNext();

            Interlocked.Increment(ref _Current);

            return true;

        }

        #endregion

    }

    #endregion

}
