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
using System.Diagnostics;

#endregion

namespace de.ahzf.Vanaheimr.Styx.Sensors.PerformanceCounters.ProcessCounters
{

    #region ProcessProcessorTimeSensor

    /// <summary>
    /// A sensor measuring the processor usage time of this process.
    /// </summary>
    public class ProcessProcessorTimeSensor : ProcessProcessorTimeSensor<SensorId>
    {

        #region ProcessProcessorTimeSensor(SensorName)

        /// <summary>
        /// A sensor measuring the processor usage time of this process.
        /// </summary>
        /// <param name="SensorName">The name of the sensor.</param>
        public ProcessProcessorTimeSensor(String SensorName)
            : base(SensorId.NewSensorId, SensorName)
        { }

        #endregion

        #region ProcessProcessorTimeSensor(SensorId, SensorName)

        /// <summary>
        /// A sensor measuring the processor usage time of this process.
        /// </summary>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        public ProcessProcessorTimeSensor(SensorId SensorId, String SensorName)
            : base(SensorId, SensorName)
        { }

        #endregion

        #region ProcessProcessorTimeSensor(SensorId_UInt64, SensorName)

        /// <summary>
        /// A sensor measuring the processor usage time of this process.
        /// </summary>
        /// <param name="SensorId_UInt64">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        public ProcessProcessorTimeSensor(UInt64 SensorId_UInt64, String SensorName)
            : base(new SensorId(SensorId_UInt64), SensorName)
        { }

        #endregion

    }

    #endregion

    #region ProcessProcessorTimeSensor<TId>

    /// <summary>
    /// A sensor measuring the processor usage time of this process.
    /// </summary>
    /// <typeparam name="TId">The type of the unique identification.</typeparam>
    public class ProcessProcessorTimeSensor<TId> : AbstractPerformanceCounterSensor<TId, Single>
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    {

        #region ProcessProcessorTimeSensor(SensorId, SensorName)

        /// <summary>
        /// A sensor measuring the processor usage time of this process.
        /// </summary>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        public ProcessProcessorTimeSensor(TId SensorId, String SensorName)
#if __MonoCS__
            : base(SensorId, SensorName, "ms", "Process", "% Processor Time", Process.GetCurrentProcess().Id.ToString(), Value => Value)
#else
            : base(SensorId, SensorName, "ms", "Process", "% Processor Time", Process.GetCurrentProcess().ProcessName, Value => Value)
#endif
        { }

        #endregion

    }

    #endregion

}
