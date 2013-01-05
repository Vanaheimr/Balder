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

namespace de.ahzf.Styx.Sensors.PerformanceCounters.NetworkInterfaceCounters
{

    /// <summary>
    /// A sensor collecting the received and sent packets and bytes
    /// of a network interface.
    /// </summary>
    public class TransmissionsSensor : AbstractSensor<String, TransmissionsStruct>
    {

        #region Data

        private readonly PerformanceCounter _PacketsReceived_Counter;
        private readonly PerformanceCounter _PacketsSent_Counter;
        private readonly PerformanceCounter _BytesReceived_Counter;
        private readonly PerformanceCounter _BytesSent_Counter;

        #endregion

        #region Properties

        #region CurrentValue

        /// <summary>
        /// The current value of this performance counter.
        /// </summary>
        public override Boolean MoveNext()
        {

            _Current = new TransmissionsStruct(_PacketsReceived_Counter.NextValue(),
                                               _PacketsSent_Counter.NextValue(),
                                               _BytesReceived_Counter.NextValue(),
                                               _BytesSent_Counter.NextValue());

            return true;

        }

        #endregion

        #endregion

        #region Constructor(s)

        #region TransmissionsSensor(SensorId, mySensorName, myInstance)

        /// <summary>
        /// A sensor collecting the received and sent packets and bytes
        /// of a network interface.
        /// </summary>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="mySensorName">The well-known name of this sensor.</param>
        /// <param name="myInstance">The network instance to gather the data from.</param>
        public TransmissionsSensor(String SensorId, String mySensorName, String myInstance)
            : base(SensorId, mySensorName, "...")
        {
            _PacketsReceived_Counter = new PerformanceCounter("Network Interface", NetworkInterfaceCounter.PacketsReceived_Sec.ToString(), myInstance);
            _PacketsSent_Counter     = new PerformanceCounter("Network Interface", NetworkInterfaceCounter.PacketsSent_Sec.ToString(),     myInstance);
            _BytesReceived_Counter   = new PerformanceCounter("Network Interface", NetworkInterfaceCounter.BytesReceived_Sec.ToString(),   myInstance);
            _BytesSent_Counter       = new PerformanceCounter("Network Interface", NetworkInterfaceCounter.BytesSent_Sec.ToString(),       myInstance);
        }

        #endregion

        #endregion

    }

}
