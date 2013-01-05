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

namespace de.ahzf.Vanaheimr.Styx.Sensors.PerformanceCounters.NetworkInterfaceCounters
{

    /// <summary>
    /// A generic network interface sensor.
    /// </summary>
    public class NetworkInterfaceSensor<TId> : AbstractPerformanceCounterSensor<TId, UInt64>
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    {

        #region Constructor(s)

        #region NetworkInterfaceSensor(SensorName, NetworkInterfaceCounter)

        /// <summary>
        /// Creates a new generic network interface sensor.
        /// </summary>
        /// <param name="SensorName">The well-known name of the sensor.</param>
        /// <param name="NetworkInterfaceCounter">A network interface counter.</param>
        public NetworkInterfaceSensor(TId Id, String SensorName, NetworkInterfaceCounter NetworkInterfaceCounter)
            : base(Id, SensorName, "...", "Network Interface", NetworkInterfaceCounter.ToString(), Value => (UInt64) Value)
        { }

        #endregion

        #region NetworkInterfaceSensor(SensorName, NetworkInterface, NetworkInterfaceCounter)

        /// <summary>
        /// Creates a new generic network interface sensor.
        /// </summary>
        /// <param name="SensorName">The well-known name of the sensor.</param>
        /// <param name="NetworkInterface">The name of network interface.</param>
        /// <param name="NetworkInterfaceCounter">A network interface counter.</param>
        public NetworkInterfaceSensor(TId Id, String SensorName, String NetworkInterface, NetworkInterfaceCounter NetworkInterfaceCounter)
            : base(Id, SensorName, "...", "Network Interface", NetworkInterfaceCounter.ToString(), NetworkInterface, Value => (UInt64) Value)
        { }

        #endregion

        #endregion

    }

}
