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

namespace de.ahzf.Vanaheimr.Styx.Sensors.PerformanceCounters.ProcessorCounters
{

    #region ProcessorTotalTimeSensor

    /// <summary>
    /// A sensor measuring the total usage time of the processors.
    /// </summary>
    public class ProcessorTotalTimeSensor : ProcessorTotalTimeSensor<SensorId>
    {

        #region ProcessorTotalTimeSensor(SensorName)

        /// <summary>
        /// A sensor measuring the total usage time of the processors.
        /// </summary>
        /// <param name="SensorName">The name of the sensor.</param>
        public ProcessorTotalTimeSensor(String SensorName)
            : base(SensorId.NewSensorId, SensorName)
        { }

        #endregion

        #region ProcessorTotalTimeSensor(SensorId, SensorName)

        /// <summary>
        /// A sensor measuring the total usage time of the processors.
        /// </summary>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        public ProcessorTotalTimeSensor(SensorId SensorId, String SensorName)
            : base(SensorId, SensorName)
        { }

        #endregion

        #region ProcessorTotalTimeSensor(SensorId_UInt64, SensorName)

        /// <summary>
        /// A sensor measuring the total usage time of the processors.
        /// </summary>
        /// <param name="SensorId_UInt64">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        public ProcessorTotalTimeSensor(UInt64 SensorId_UInt64, String SensorName)
            : base(new SensorId(SensorId_UInt64), SensorName)
        { }

        #endregion

    }

    #endregion

    #region ProcessorTotalTimeSensor<TId>

    /// <summary>
    /// A sensor measuring the total usage time of the processors.
    /// </summary>
    /// <typeparam name="TId">The type of the unique identification.</typeparam>
    public class ProcessorTotalTimeSensor<TId> : AbstractPerformanceCounterSensor<TId, UInt64>
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    {

        #region ProcessorTotalTimeSensor(SensorId, SensorName)

        /// <summary>
        /// A sensor measuring the total usage time of the processors.
        /// </summary>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        public ProcessorTotalTimeSensor(TId SensorId, String SensorName)
            : base(SensorId, SensorName, "ms", "Processor", "% Processor Time", "_Total", Value => (UInt64) Value)
        { }

        #endregion

    }

    #endregion

}
