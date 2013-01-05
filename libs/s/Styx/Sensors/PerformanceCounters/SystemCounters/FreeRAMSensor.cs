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

namespace de.ahzf.Vanaheimr.Styx.Sensors.PerformanceCounters.SystemCounters
{

    #region FreeRAMSensor

    /// <summary>
    /// A sensor measuring the available amount of physical memory in MBytes.
    /// </summary>
    public class FreeRAMSensor : FreeRAMSensor<SensorId>
    {

        #region FreeRAMSensor(SensorName)

        /// <summary>
        /// A sensor measuring the available amount of physical memory in MBytes.
        /// </summary>
        /// <param name="SensorName">The name of the sensor.</param>
        public FreeRAMSensor(String SensorName)
            : base(SensorId.NewSensorId, SensorName)
        { }

        #endregion

        #region FreeRAMSensor(SensorId, SensorName)

        /// <summary>
        /// A sensor measuring the available amount of physical memory in MBytes.
        /// </summary>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        public FreeRAMSensor(SensorId SensorId, String SensorName)
            : base(SensorId, SensorName)
        { }

        #endregion

        #region FreeRAMSensor(SensorId_UInt64, SensorName)

        /// <summary>
        /// A sensor measuring the available amount of physical memory in MBytes.
        /// </summary>
        /// <param name="SensorId_UInt64">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        public FreeRAMSensor(UInt64 SensorId_UInt64, String SensorName)
            : base(new SensorId(SensorId_UInt64), SensorName)
        { }

        #endregion

    }

    #endregion

    #region FreeRAMSensor<TId>

    /// <summary>
    /// A sensor measuring the available amount of physical memory in MBytes.
    /// </summary>
    /// <typeparam name="TId">The type of the unique identification.</typeparam>
    public class FreeRAMSensor<TId> : AbstractPerformanceCounterSensor<TId, UInt64>
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    {

        #region FreeRAMSensor(SensorId, SensorName)

        /// <summary>
        /// A sensor measuring the available amount of physical memory in MBytes.
        /// </summary>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        public FreeRAMSensor(TId SensorId, String SensorName)
            : base(SensorId, SensorName, "MBytes", "Memory", "Available MBytes", Value => (UInt64) Value)
        { }

        #endregion

    }

    #endregion

}
