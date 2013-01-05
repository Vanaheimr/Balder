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

namespace de.ahzf.Styx.Sensors.PerformanceCounters
{

    /// <summary>
    /// An abstract sensor node for accessing performance counters.
    /// </summary>
    /// <typeparam name="TId">The type of the unique identification.</typeparam>
    /// <typeparam name="TValue">The type of the measured value.</typeparam>
    public abstract class AbstractPerformanceCounterSensor<TId, TValue> : AbstractSensor<TId, TValue>
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    {

        #region Data

        /// <summary>
        /// The internal performance counter.
        /// </summary>
        protected readonly PerformanceCounter _PerformanceCounter;

        /// <summary>
        /// A delegate converting performance counter
        /// values into another datastructure.
        /// </summary>
        protected readonly Func<Single, TValue> _ValueConverter;

        #endregion

        #region Constructor(s)

        #region AbstractPerformanceCounterSensor(SensorId, SensorName, ValueUnit, CategoryName, CounterName, ValueConverter = null)

        /// <summary>
        /// Creates a new AbstractPerformanceCounterSensor_UInt64.
        /// </summary>
        /// <param name="SensorId">The identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        /// <param name="ValueUnit">The unit of the value (m, m², °C, °F, MBytes, ...).</param>
        /// <param name="CategoryName">The performance counter category.</param>
        /// <param name="CounterName">The name of the performance counter.</param>
        /// <param name="ValueConverter">A delegate converting performance counter values into another datastructure.</param>
        public AbstractPerformanceCounterSensor(TId SensorId, String SensorName, String ValueUnit, String CategoryName, String CounterName, Func<Single, TValue> ValueConverter = null)
            : base(SensorId, SensorName, ValueUnit)
        {
            _PerformanceCounter = new PerformanceCounter(CategoryName, CounterName);
            _ValueConverter     = ValueConverter;
        }

        #endregion

        #region AbstractPerformanceCounterSensor(SensorId, SensorName, ValueUnit, CategoryName, CounterName, InstanceName)

        /// <summary>
        /// Creates a new AbstractPerformanceCounterSensor_UInt64.
        /// </summary>
        /// <param name="SensorId">The identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        /// <param name="ValueUnit">The unit of the value (m, m², °C, °F, MBytes, ...).</param>
        /// <param name="CategoryName">The performance counter category.</param>
        /// <param name="CounterName">The name of the performance counter.</param>
        /// <param name="InstanceName">The name of the performance counter instance.</param>
        /// <param name="ValueConverter">A delegate converting performance counter values into another datastructure.</param>
        public AbstractPerformanceCounterSensor(TId SensorId, String SensorName, String ValueUnit, String CategoryName, String CounterName, String InstanceName, Func<Single, TValue> ValueConverter = null)
            : base(SensorId, SensorName, ValueUnit)
        {
            _PerformanceCounter = new PerformanceCounter(CategoryName, CounterName, InstanceName);
            _ValueConverter     = ValueConverter;
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

            // Convert the performance counter value into the return value
            if (_ValueConverter != null)
                _Current = _ValueConverter(_PerformanceCounter.NextValue());

            else
            {
                try
                {
                    // This might or might not throw an Exception... ;)
                    _Current = (TValue) (Object) _PerformanceCounter.NextValue();
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;

        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Dispose this object.
        /// </summary>
        public override void Dispose()
        {
            _PerformanceCounter.Dispose();
        }

        #endregion

    }

}
