/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET <http://www.github.com/ahzf/blueprints.NET>
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

namespace de.ahzf.blueprints
{

    /// <summary>
    /// This class will return the current timestamp, but will ensure its
    /// uniqueness which means it will never return the same value twice.
    /// </summary>
    public static class UniqueTimestamp
    {

        #region Data

        private static Int64 _LastTimestamp;

        #endregion

        #region Properties

        #region Now

        /// <summary>
        /// Returns an unique timestamp as a DateTime object
        /// </summary>
        public static DateTime Now
        {
            get
            {
                return new DateTime((Int64) GetUniqueTimestamp);
            }
        }

        #endregion

        #region Ticks

        /// <summary>
        /// Returns an unique timestamp as an UInt64
        /// </summary>
        public static UInt64 Ticks
        {
            get
            {
                return GetUniqueTimestamp;
            }
        }

        #endregion

        #endregion


        #region (private) GetUniqueTimestamp

        /// <summary>
        /// Return a unique timestamp
        /// </summary>
        private static UInt64 GetUniqueTimestamp
        {

            get
            {

                Int64 _InitialValue, _NewValue;

                do
                {

                    // Save the last known timestamp in a local variable.
                    _InitialValue = _LastTimestamp;

                    _NewValue     = Math.Max(DateTime.Now.Ticks, _InitialValue + 1);

                }
                // Use CompareExchange to avoid locks!
                while (_InitialValue != Interlocked.CompareExchange(ref _LastTimestamp, _NewValue, _InitialValue));

                // Return _NewValue, as _LastTimestamp might
                // already be changed by yet another thread!
                return (UInt64) _NewValue;

            }

        }

        #endregion

    }

}
