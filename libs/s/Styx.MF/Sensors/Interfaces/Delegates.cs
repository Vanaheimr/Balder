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

#endregion

namespace de.ahzf.Styx.Sensors
{

    /// <summary>
    /// An event handler used whenever an active sensor is
    /// starting its measurements.
    /// </summary>
    /// <param name="Sensor">The sensor sending this event.</param>
    public delegate void SensorIsStartingEventHandler(Object Sensor);

    /// <summary>
    /// An event handler used whenever an active sensor
    /// started its measurements.
    /// </summary>
    /// <param name="Sensor">The sensor sending this event.</param>
    public delegate void SensorStartedEventHandler(Object Sensor);


    /// <summary>
    /// An event handler used whenever an active sensor is
    /// stopping its measurements.
    /// </summary>
    /// <param name="Sensor">The sensor sending this event.</param>
    public delegate void SensorIsStoppingEventHandler(Object Sensor);

    /// <summary>
    /// An event handler used whenever an active sensor has
    /// stopped its measurements.
    /// </summary>
    /// <param name="Sensor">The sensor sending this event.</param>
    public delegate void SensorStoppedEventHandler(Object Sensor);

}
