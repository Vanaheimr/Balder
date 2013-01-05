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

#endregion

namespace de.ahzf.Styx.Sensors.Active
{

    /// <summary>
    /// Extension methods for the IEnumerator interface.
    /// </summary>
    public static class IEnumeratorExtensions
    {

        #region ToSensor(this IEnumerator, SensorId, SensorName, ValueUnit)

        /// <summary>
        /// Creates a new sensor fireing the content of the given IEnumerator.
        /// </summary>
        /// <typeparam name="TId">The type of the unique identification.</typeparam>
        /// <typeparam name="TValue">The type of the measured value.</typeparam>
        /// <param name="IEnumerator">An enumerator of messages/objects to send.</param>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        /// <param name="ValueUnit">The unit of the value (m, m², °C, °F, MBytes, ...).</param>
        /// <returns>A new ISensor&lt;TId, TValue&gt;.</returns>
        public static ISensor<TId, TValue> ToSensor<TId, TValue>(this IEnumerator<TValue> IEnumerator,
                                                                 TId                      SensorId,
                                                                 String                   SensorName,
                                                                 String                   ValueUnit)

            where TId : IEquatable<TId>, IComparable<TId>, IComparable

        {
            return new Sensor<TId, TValue>(IEnumerator, SensorId, SensorName, ValueUnit);
        }

        #endregion


        #region ToActiveSensor(this IEnumerator, SensorId, SensorName, ValueUnit, Autostart, StartAsTask, InitialDelay)

        /// <summary>
        /// Creates a new ActiveSensor fireing the content of the given IEnumerator.
        /// </summary>
        /// <typeparam name="TId">The type of the unique identification.</typeparam>
        /// <typeparam name="TValue">The type of the measured value.</typeparam>
        /// <param name="IEnumerator">An enumerator of messages/objects to send.</param>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        /// <param name="ValueUnit">The unit of the value (m, m², °C, °F, MBytes, ...).</param>
        /// <param name="Autostart">Start the sniper automatically.</param>
        /// <param name="StartAsTask">Start the ActiveSensor within its own task.</param>
        /// <param name="InitialDelay">Set the initial delay of the sniper.</param>
        /// <returns>A new IActiveSensor&lt;TId, TValue&gt;.</returns>
        public static IActiveSensor<TId, TValue> ToActiveSensor<TId, TValue>(this IEnumerator<TValue> IEnumerator,
                                                                             TId                      SensorId,
                                                                             String                   SensorName,
                                                                             String                   ValueUnit,
                                                                             Boolean                  Autostart    = false,
                                                                             Boolean                  StartAsTask  = false,
                                                                             Nullable<TimeSpan>       InitialDelay = null)

            where TId : IEquatable<TId>, IComparable<TId>, IComparable

        {
            return new ActiveSensor<TId, TValue>(IEnumerator, SensorId, SensorName, ValueUnit, Autostart, StartAsTask, InitialDelay);
        }

        #endregion

        #region ToActiveSensor(this IEnumerator, SensorId, SensorName, ValueUnit, MessageRecipient.Recipient, Autostart, StartAsTask, InitialDelay)

        /// <summary>
        /// Creates a new ActiveSensor fireing the content of the given IEnumerator.
        /// </summary>
        /// <typeparam name="TId">The type of the unique identification.</typeparam>
        /// <typeparam name="TValue">The type of the measured value.</typeparam>
        /// <param name="IEnumerator">An enumerator of messages/objects to send.</param>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        /// <param name="ValueUnit">The unit of the value (m, m², °C, °F, MBytes, ...).</param>
        /// <param name="Recipient">A recipient of the processed measurements.</param>
        /// <param name="Autostart">Start the sniper automatically.</param>
        /// <param name="StartAsTask">Start the ActiveSensor within its own task.</param>
        /// <param name="InitialDelay">Set the initial delay of the sniper.</param>
        /// <returns>A new IActiveSensor&lt;TId, TValue&gt;.</returns>
        public static IActiveSensor<TId, TValue> ToActiveSensor<TId, TValue>(this IEnumerator<TValue> IEnumerator,
                                                                             TId                      SensorId,
                                                                             String                   SensorName,
                                                                             String                   ValueUnit,
                                                                             MessageRecipient<TValue> Recipient,
                                                                             Boolean                  Autostart    = false,
                                                                             Boolean                  StartAsTask  = false,
                                                                             Nullable<TimeSpan>       InitialDelay = null)

            where TId : IEquatable<TId>, IComparable<TId>, IComparable

        {
            return new ActiveSensor<TId, TValue>(IEnumerator, SensorId, SensorName, ValueUnit, Recipient, Autostart, StartAsTask, InitialDelay);
        }

        #endregion

        #region ToActiveSensor(this IEnumerator, SensorId, SensorName, ValueUnit, IArrowReceiver.Recipient, Autostart, StartAsTask, InitialDelay)

        /// <summary>
        /// Creates a new ActiveSensor fireing the content of the given IEnumerator.
        /// </summary>
        /// <typeparam name="TId">The type of the unique identification.</typeparam>
        /// <typeparam name="TValue">The type of the measured value.</typeparam>
        /// <param name="IEnumerator">An enumerator of messages/objects to send.</param>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        /// <param name="ValueUnit">The unit of the value (m, m², °C, °F, MBytes, ...).</param>
        /// <param name="Recipient">A recipient of the processed measurements.</param>
        /// <param name="Autostart">Start the sniper automatically.</param>
        /// <param name="StartAsTask">Start the ActiveSensor within its own task.</param>
        /// <param name="InitialDelay">Set the initial delay of the sniper.</param>
        /// <returns>A new IActiveSensor&lt;TId, TValue&gt;.</returns>
        public static IActiveSensor<TId, TValue> ToActiveSensor<TId, TValue>(this IEnumerator<TValue> IEnumerator,
                                                                             TId                      SensorId,
                                                                             String                   SensorName,
                                                                             String                   ValueUnit,
                                                                             IArrowReceiver<TValue>   Recipient,
                                                                             Boolean                  Autostart    = false,
                                                                             Boolean                  StartAsTask  = false,
                                                                             Nullable<TimeSpan>       InitialDelay = null)

            where TId : IEquatable<TId>, IComparable<TId>, IComparable

        {
            return new ActiveSensor<TId, TValue>(IEnumerator, SensorId, SensorName, ValueUnit, Recipient, Autostart, StartAsTask, InitialDelay);
        }

        #endregion

    }

}
