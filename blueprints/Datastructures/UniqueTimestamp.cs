/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using System;
using System.Threading;

#endregion

namespace de.ahzf.blueprints.Datastructures
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

                // Return _InitialValue, as _LastTimestamp might
                // already be changed by yet another thread!
                return (UInt64) _NewValue;

            }

        }

        #endregion

    }

}
