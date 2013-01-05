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

#endregion

namespace de.ahzf.Vanaheimr.Styx.Sensors.Simple
{

    #region RandomSensor

    /// <summary>
    /// A sensor returning random numbers.
    /// </summary>
    public class RandomSensor : RandomSensor<SensorId>
    {

        #region RandomSensor(SensorName)

        /// <summary>
        /// A sensor returning random numbers.
        /// </summary>
        /// <param name="SensorName">The name of the sensor.</param>
        public RandomSensor(String SensorName)
            : base(SensorId.NewSensorId, SensorName)
        { }

        #endregion

        #region RandomSensor(SensorId, SensorName)

        /// <summary>
        /// A sensor returning random numbers.
        /// </summary>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        public RandomSensor(SensorId SensorId, String SensorName)
            : base(SensorId, SensorName)
        { }

        #endregion

        #region RandomSensor(SensorId_UInt64, SensorName)

        /// <summary>
        /// A sensor returning random numbers.
        /// </summary>
        /// <param name="SensorId_UInt64">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        public RandomSensor(UInt64 SensorId_UInt64, String SensorName)
            : base(new SensorId(SensorId_UInt64), SensorName)
        { }

        #endregion

    }

    #endregion

    #region RandomSensor<TId>

    /// <summary>
    /// A sensor returning random numbers.
    /// </summary>
    /// <typeparam name="TId">The type of the unique identification.</typeparam>
    public class RandomSensor<TId> : AbstractSensor<TId, Double>
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    {

        #region Data

        /// <summary>
        /// The source of randomness.
        /// </summary>
        private readonly Random _Random;

        #endregion

        #region RandomSensor(SensorId, SensorName)

        /// <summary>
        /// A sensor returning random numbers.
        /// </summary>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        public RandomSensor(TId SensorId, String SensorName)
            : base(SensorId, SensorName, "Double")
        {
            _Random = new Random();
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

            _Current = _Random.NextDouble();
            return true;

        }

        #endregion

    }

    #endregion

}
