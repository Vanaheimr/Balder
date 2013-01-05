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

namespace de.ahzf.Styx.Sensors.PerformanceCounters.NetworkInterfaceCounters
{

    /// <summary>
    /// A struct collecting received and sent packets and bytes.
    /// </summary>
    public struct TransmissionsStruct
    {

        #region Data

        /// <summary>
        /// The packets received.
        /// </summary>
        public readonly UInt64 PacketsReceived;

        /// <summary>
        /// The packets sent.
        /// </summary>
        public readonly UInt64 PacketsSent;

        /// <summary>
        /// The bytes received.
        /// </summary>
        public readonly UInt64 BytesReceived;

        /// <summary>
        /// The bytes sent.
        /// </summary>
        public readonly UInt64 BytesSent;

        #endregion

        #region Constructor(s)

        #region TransmissionsStruct(Single ...)

        /// <summary>
        /// Generates a new TransmissionsStruct based on the given Single values.
        /// </summary>
        /// <param name="myPacketsReceived">The packets received.</param>
        /// <param name="myPacketsSent">The packets sent.</param>
        /// <param name="myBytesReceived">The bytes received.</param>
        /// <param name="myBytesSent">The bytes sent.</param>
        public TransmissionsStruct(Single myPacketsReceived, Single myPacketsSent, Single myBytesReceived, Single myBytesSent)
        {
            PacketsReceived = (UInt64) myBytesReceived;
            PacketsSent     = (UInt64) myPacketsSent;
            BytesReceived   = (UInt64) myBytesReceived;
            BytesSent       = (UInt64) myBytesSent;
        }

        #endregion

        #region TransmissionsStruct(UInt64 ...)

        /// <summary>
        /// Generates a new TransmissionsStruct based on the given UInt64 values.
        /// </summary>
        /// <param name="myPacketsReceived">The packets received.</param>
        /// <param name="myPacketsSent">The packets sent.</param>
        /// <param name="myBytesReceived">The bytes received.</param>
        /// <param name="myBytesSent">The bytes sent.</param>
        public TransmissionsStruct(UInt64 myPacketsReceived, UInt64 myPacketsSent, UInt64 myBytesReceived, UInt64 myBytesSent)
        {
            PacketsReceived = myBytesReceived;
            PacketsSent = myPacketsSent;
            BytesReceived = myBytesReceived;
            BytesSent = myBytesSent;
        }

        #endregion

        #endregion

        #region ToString()

        /// <summary>
        /// Converts this TransmissionsStruct to its string representation.
        /// </summary>
        public override String ToString()
        {
            return String.Format("{0}, {1}, {2}, {3}", PacketsReceived, PacketsSent, BytesReceived, BytesSent);
        }

        #endregion

    }

}
