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

using de.ahzf.Vanaheimr.Styx;

#endregion

namespace de.ahzf.Vanaheimr.Styx.Sensors.Active
{

    // Delegates

    #region SensorIsStartingEventHandler

    /// <summary>
    /// An event handler used whenever an active sensor is
    /// starting its measurements.
    /// </summary>
    /// <typeparam name="TId">The type of the unique identification.</typeparam>
    /// <param name="Sensor">The sensor sending this event.</param>
    public delegate void SensorIsStartingEventHandler<TId>(IActiveSensor<TId> Sensor)

        where TId : IEquatable<TId>, IComparable<TId>, IComparable;

    #endregion

    #region SensorStartedEventHandler

    /// <summary>
    /// An event handler used whenever an active sensor
    /// started its measurements.
    /// </summary>
    /// <typeparam name="TId">The type of the unique identification.</typeparam>
    /// <param name="Sensor">The sensor sending this event.</param>
    public delegate void SensorStartedEventHandler<TId>(IActiveSensor<TId> Sensor)

        where TId : IEquatable<TId>, IComparable<TId>, IComparable;

    #endregion

    #region SensorIsStoppingEventHandler

    /// <summary>
    /// An event handler used whenever an active sensor is
    /// stopping its measurements.
    /// </summary>
    /// <typeparam name="TId">The type of the unique identification.</typeparam>
    /// <param name="Sensor">The sensor sending this event.</param>
    public delegate void SensorIsStoppingEventHandler<TId>(IActiveSensor<TId> Sensor)

        where TId : IEquatable<TId>, IComparable<TId>, IComparable;

    #endregion

    #region SensorStoppedEventHandler

    /// <summary>
    /// An event handler used whenever an active sensor has
    /// stopped its measurements.
    /// </summary>
    /// <typeparam name="TId">The type of the unique identification.</typeparam>
    /// <param name="Sensor">The sensor sending this event.</param>
    public delegate void SensorStoppedEventHandler<TId>(IActiveSensor<TId> Sensor)

        where TId : IEquatable<TId>, IComparable<TId>, IComparable;

    #endregion


    // Interfaces

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

        /// <summary>
        /// Stop sensor measurements.
        /// </summary>
        /// <returns>True if succeeded; false otherwise.</returns>
        Boolean StopMeasurements();

    }

    #endregion

    #region IActiveSensor<TId>

    /// <summary>
    /// The common generic IActiveSensor interface.
    /// </summary>
    /// <typeparam name="TId">The type of the unique identification.</typeparam>
    public interface IActiveSensor<TId> : ISensor<TId>,
                                          IActiveSensor

        where TId : IEquatable<TId>, IComparable<TId>, IComparable

    {


        event SensorIsStartingEventHandler<TId> OnSensorStarting;
        event SensorStartedEventHandler<TId> OnSensorStarted;


        event SensorIsStoppingEventHandler<TId> OnSensorStopping;
        event SensorStoppedEventHandler<TId> OnSensorStopped;
    
    }

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

    { }

    #endregion

}
