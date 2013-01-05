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
using System.Diagnostics;

#endregion

namespace de.ahzf.Styx.Sensors.PerformanceCounters.ProcessCounters
{

    #region ProcessWorkingSetSensor

    /// <summary>
    /// A sensor measuring the size of the current process working set in bytes.
    /// The Working Set is the set of memory pages touched recently by the threads in the
    /// process. If free memory in the computer is above a threshold, pages are left in the
    /// Working Set of a process even if they are not in use.  When free memory falls below
    /// a threshold, pages are trimmed from Working Sets. If they are needed they will then
    /// be soft-faulted back into the Working Set before leaving main memory.
    /// </summary>
    public class ProcessWorkingSetSensor : ProcessWorkingSetSensor<SensorId>
    {

        #region ProcessWorkingSetSensor(SensorName)

        /// <summary>
        /// A sensor measuring the size of the current process working set in bytes.
        /// The Working Set is the set of memory pages touched recently by the threads in the
        /// process. If free memory in the computer is above a threshold, pages are left in the
        /// Working Set of a process even if they are not in use.  When free memory falls below
        /// a threshold, pages are trimmed from Working Sets. If they are needed they will then
        /// be soft-faulted back into the Working Set before leaving main memory.
        /// </summary>
        /// <param name="SensorName">The name of the sensor.</param>
        public ProcessWorkingSetSensor(String SensorName)
            : base(SensorId.NewSensorId, SensorName)
        { }

        #endregion

        #region ProcessWorkingSetSensor(SensorId, SensorName)

        /// <summary>
        /// A sensor measuring the size of the current process working set in bytes.
        /// The Working Set is the set of memory pages touched recently by the threads in the
        /// process. If free memory in the computer is above a threshold, pages are left in the
        /// Working Set of a process even if they are not in use.  When free memory falls below
        /// a threshold, pages are trimmed from Working Sets. If they are needed they will then
        /// be soft-faulted back into the Working Set before leaving main memory.
        /// </summary>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        public ProcessWorkingSetSensor(SensorId SensorId, String SensorName)
            : base(SensorId, SensorName)
        { }

        #endregion

        #region ProcessWorkingSetSensor(SensorId_UInt64, SensorName)

        /// <summary>
        /// A sensor measuring the size of the current process working set in bytes.
        /// The Working Set is the set of memory pages touched recently by the threads in the
        /// process. If free memory in the computer is above a threshold, pages are left in the
        /// Working Set of a process even if they are not in use.  When free memory falls below
        /// a threshold, pages are trimmed from Working Sets. If they are needed they will then
        /// be soft-faulted back into the Working Set before leaving main memory.
        /// </summary>
        /// <param name="SensorId_UInt64">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        public ProcessWorkingSetSensor(UInt64 SensorId_UInt64, String SensorName)
            : base(new SensorId(SensorId_UInt64), SensorName)
        { }

        #endregion

    }

    #endregion

    #region ProcessWorkingSetSensor<TId>

    /// <summary>
    /// A sensor measuring the size of the current process working set in bytes.
    /// The Working Set is the set of memory pages touched recently by the threads in the
    /// process. If free memory in the computer is above a threshold, pages are left in the
    /// Working Set of a process even if they are not in use.  When free memory falls below
    /// a threshold, pages are trimmed from Working Sets. If they are needed they will then
    /// be soft-faulted back into the Working Set before leaving main memory.
    /// </summary>
    /// <typeparam name="TId">The type of the unique identification.</typeparam>
    public class ProcessWorkingSetSensor<TId> : AbstractPerformanceCounterSensor<TId, UInt64>
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    {

        #region ProcessWorkingSetSensor(SensorId, SensorName)

        /// <summary>
        /// A sensor measuring the size of the current process working set in bytes.
        /// The Working Set is the set of memory pages touched recently by the threads in the
        /// process. If free memory in the computer is above a threshold, pages are left in the
        /// Working Set of a process even if they are not in use.  When free memory falls below
        /// a threshold, pages are trimmed from Working Sets. If they are needed they will then
        /// be soft-faulted back into the Working Set before leaving main memory.
        /// </summary>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        public ProcessWorkingSetSensor(TId SensorId, String SensorName)
#if __MonoCS__
            : base(SensorId, SensorName, "Bytes", "Process", "Working Set", Process.GetCurrentProcess().Id.ToString(), Value => (UInt64) Value)
#else
            : base(SensorId, SensorName, "Bytes", "Process", "Working Set", Process.GetCurrentProcess().ProcessName, Value => (UInt64) Value)
#endif
        { }

        #endregion

    }

    #endregion

}
