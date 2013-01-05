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

using de.ahzf.Styx;

#endregion

namespace de.ahzf.Styx.Sensors.Active
{

    #region IActiveSensor

    /// <summary>
    /// The common IActiveSensor interface.
    /// </summary>
    public interface IActiveSensor : ISensor,
                                     IArrowSender
    {

        /// <summary>
        /// Start sensor measurements.
        /// </summary>
        /// <returns>True if succeeded; false otherwise.</returns>
        Boolean StartMeasurements();

        event SensorIsStartingEventHandler OnSensorStart;
        event SensorStartedEventHandler OnSensorStarted;


        /// <summary>
        /// Stop sensor measurements.
        /// </summary>
        /// <returns>True if succeeded; false otherwise.</returns>
        Boolean StopMeasurements();

        event SensorIsStoppingEventHandler OnSensorStop;
        event SensorStoppedEventHandler OnSensorStopped;

    }

    #endregion

    #region IActiveSensor<TId>

    /// <summary>
    /// The common generic IActiveSensor interface.
    /// </summary>
    public interface IActiveSensor<TId> : ISensor<TId>

        where TId : IEquatable<TId>, IComparable<TId>, IComparable

    { }

    #endregion

    #region IActiveSensor<TId, TValue>

    /// <summary>
    /// The generic IActiveSensor interface with the type of the measureds values.
    /// </summary>
    /// <typeparam name="TId">The type of the unique identification.</typeparam>
    /// <typeparam name="TValue">The type of the measured values.</typeparam>
    public interface IActiveSensor<TId, TValue> : ISensor<TId, TValue>,
                                                  IActiveSensor<TId>,
                                                  IArrowSender<TValue>

        where TId : IEquatable<TId>, IComparable<TId>, IComparable

    {

        ///// <summary>
        ///// An event that clients can use to be notified whenever
        ///// new sensor data is available.
        ///// </summary>
        //event MessageRecipient<TValue> OnMessageAvailable;

    }

    #endregion

}
