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

using de.ahzf.Styx;
using de.ahzf.Illias.Commons;

#endregion

namespace de.ahzf.Styx.Sensors.Active
{

    /// <summary>
    /// Extension methods for the ISensor interfaces.
    /// </summary>
    public static class ISensorExtensions
    {

        #region ToSensor(this ISensor<TId, TValue>)

        /// <summary>
        /// Creates a new sensor based on the given ISensor&lt;TId, TValue&gt;.
        /// </summary>
        /// <typeparam name="TId">The type of the unique identification.</typeparam>
        /// <typeparam name="TValue">The type of the measured value.</typeparam>
        /// <param name="ISensor">An ISensor&lt;TId, TValue&gt;.</param>
        /// <returns>A new ISensor&lt;TId, TValue&gt;.</returns>
        public static ISensor<TId, TValue> ToSensor<TId, TValue>(this ISensor<TId, TValue> ISensor)
            where TId : IEquatable<TId>, IComparable<TId>, IComparable
        {
            return new Sensor<TId, TValue>(ISensor);
        }

        #endregion


        #region ToActiveSensor(this ISensor<TId, TValue>, Autostart, StartAsTask, InitialDelay)

        /// <summary>
        /// Creates a new ActiveSensor based on the given ISensor&lt;TId, TValue&gt;.
        /// </summary>
        /// <typeparam name="TId">The type of the unique identification.</typeparam>
        /// <typeparam name="TValue">The type of the measured value.</typeparam>
        /// <param name="ISensor">An ISensor&lt;TId, TValue&gt;.</param>
        /// <param name="Autostart">Start the sniper automatically.</param>
        /// <param name="StartAsTask">Start the ActiveSensor within its own task.</param>
        /// <param name="InitialDelay">Set the initial delay of the sniper.</param>
        /// <returns>A new IActiveSensor&lt;TId, TValue&gt;.</returns>
        public static IActiveSensor<TId, TValue> ToActiveSensor<TId, TValue>(this ISensor<TId, TValue> ISensor,
                                                                             Boolean                   Autostart    = false,
                                                                             Boolean                   StartAsTask  = false,
                                                                             Nullable<TimeSpan>        InitialDelay = null)

            where TId : IEquatable<TId>, IComparable<TId>, IComparable

        {
            return new ActiveSensor<TId, TValue>(ISensor, Autostart, StartAsTask, InitialDelay);
        }

        #endregion

        #region ToActiveSensor(this ISensor<TId, TValue>, MessageRecipient.Recipient, Autostart, StartAsTask, InitialDelay)

        /// <summary>
        /// Creates a new ActiveSensor based on the given ISensor&lt;TId, TValue&gt;.
        /// </summary>
        /// <typeparam name="TId">The type of the unique identification.</typeparam>
        /// <typeparam name="TValue">The type of the measured value.</typeparam>
        /// <param name="ISensor">An ISensor&lt;TId, TValue&gt;.</param>
        /// <param name="Recipient">A recipient of the processed measurements.</param>
        /// <param name="Autostart">Start the sniper automatically.</param>
        /// <param name="StartAsTask">Start the ActiveSensor within its own task.</param>
        /// <param name="InitialDelay">Set the initial delay of the sniper.</param>
        /// <returns>A new IActiveSensor&lt;TId, TValue&gt;.</returns>
        public static IActiveSensor<TId, TValue> ToActiveSensor<TId, TValue>(this ISensor<TId, TValue> ISensor,
                                                                             MessageRecipient<TValue>  Recipient,
                                                                             Boolean                   Autostart    = false,
                                                                             Boolean                   StartAsTask  = false,
                                                                             Nullable<TimeSpan>        InitialDelay = null)

            where TId : IEquatable<TId>, IComparable<TId>, IComparable

        {
            return new ActiveSensor<TId, TValue>(ISensor, Recipient, Autostart, StartAsTask, InitialDelay);
        }

        #endregion

        #region ToActiveSensor(this ISensor<TId, TValue>, IArrowReceiver.Recipient, Autostart, StartAsTask, InitialDelay)

        /// <summary>
        /// Creates a new ActiveSensor based on the given ISensor&lt;TId, TValue&gt;.
        /// </summary>
        /// <typeparam name="TId">The type of the unique identification.</typeparam>
        /// <typeparam name="TValue">The type of the measured value.</typeparam>
        /// <param name="ISensor">An ISensor&lt;TId, TValue&gt;.</param>
        /// <param name="Recipient">A recipient of the processed measurements.</param>
        /// <param name="Autostart">Start the sniper automatically.</param>
        /// <param name="StartAsTask">Start the ActiveSensor within its own task.</param>
        /// <param name="InitialDelay">Set the initial delay of the sniper.</param>
        /// <returns>A new IActiveSensor&lt;TId, TValue&gt;.</returns>
        public static IActiveSensor<TId, TValue> ToActiveSensor<TId, TValue>(this ISensor<TId, TValue> ISensor,
                                                                             IArrowReceiver<TValue>    Recipient,
                                                                             Boolean                   Autostart    = false,
                                                                             Boolean                   StartAsTask  = false,
                                                                             Nullable<TimeSpan>        InitialDelay = null)

            where TId : IEquatable<TId>, IComparable<TId>, IComparable

        {
            return new ActiveSensor<TId, TValue>(ISensor, Recipient, Autostart, StartAsTask, InitialDelay);
        }

        #endregion


        #region WithTimestamp(this ISensor<TId, TValue>)

        /// <summary>
        /// Creates a new TimestampedSensor based on the given ISensor&lt;TId, TValue&gt;.
        /// </summary>
        /// <typeparam name="TId">The type of the unique identification.</typeparam>
        /// <typeparam name="TValue">The type of the measured value.</typeparam>
        /// <param name="ISensor">An ISensor&lt;TId, TValue&gt;.</param>
        /// <returns>A new TimestampedSensor&lt;TId, TValue&gt;.</returns>
        public static ISensor<TId, Measurement<TValue>> WithTimestamp<TId, TValue>(this ISensor<TId, TValue> ISensor)

            where TId : IEquatable<TId>, IComparable<TId>, IComparable
        {
            return new TimestampedSensor<TId, TValue>(ISensor);
        }

        #endregion

    }

}
