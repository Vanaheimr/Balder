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

    public class NetworkInterfaceCounter : IComparable, IComparable<NetworkInterfaceCounter>, IEquatable<NetworkInterfaceCounter>
    {

        #region Properties

        /// <summary>
        /// The identification string of the network interface counter.
        /// </summary>
        protected readonly String _Id;

        /// <summary>
        /// The name of the network interface counter.
        /// </summary>        
        protected readonly String _CounterName;

        #endregion

        #region Constructor(s)

        #region NetworkInterfaceCounter(myCounter)

        /// <summary>
        /// Creates a new network interface counter type based on the given counter name.
        /// </summary>
        /// <param name="myCounterName">The name of the network interface counter.</param>
        public NetworkInterfaceCounter(String myCounterName)
        {
            _Id          = this.GetType().FullName + "." + myCounterName;
            _CounterName = myCounterName;
        }

        #endregion

        #endregion


        #region /etc/services

        public static readonly NetworkInterfaceCounter PacketsReceived_Sec  = new NetworkInterfaceCounter("Packets Received/sec");
        public static readonly NetworkInterfaceCounter PacketsSent_Sec      = new NetworkInterfaceCounter("Packets Sent/sec");
        public static readonly NetworkInterfaceCounter Packets_Sec          = new NetworkInterfaceCounter("Packets/sec");

        public static readonly NetworkInterfaceCounter BytesReceived_Sec    = new NetworkInterfaceCounter("Bytes Received/sec");
        public static readonly NetworkInterfaceCounter BytesSent_Sec        = new NetworkInterfaceCounter("Bytes Sent/sec");
        public static readonly NetworkInterfaceCounter BytesTotal_Sec       = new NetworkInterfaceCounter("Bytes Total/sec");

        #endregion


        #region Operator overloading

        #region Operator == (myNetworkInterfaceCounter1, myNetworkInterfaceCounter2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myNetworkInterfaceCounter1">A NetworkInterfaceCounter.</param>
        /// <param name="myNetworkInterfaceCounter2">Another NetworkInterfaceCounter.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (NetworkInterfaceCounter myNetworkInterfaceCounter1, NetworkInterfaceCounter myNetworkInterfaceCounter2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(myNetworkInterfaceCounter1, myNetworkInterfaceCounter2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) myNetworkInterfaceCounter1 == null) || ((Object) myNetworkInterfaceCounter2 == null))
                return false;

            return myNetworkInterfaceCounter1.Equals(myNetworkInterfaceCounter2);

        }

        #endregion

        #region Operator != (myNetworkInterfaceCounter1, myNetworkInterfaceCounter2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myNetworkInterfaceCounter1">A NetworkInterfaceCounter.</param>
        /// <param name="myNetworkInterfaceCounter2">Another NetworkInterfaceCounter.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (NetworkInterfaceCounter myNetworkInterfaceCounter1, NetworkInterfaceCounter myNetworkInterfaceCounter2)
        {
            return !(myNetworkInterfaceCounter1 == myNetworkInterfaceCounter2);
        }

        #endregion

        #endregion


        #region IComparable<NetworkInterfaceCounter> Members

        #region CompareTo(myObject)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myObject">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(Object myObject)
        {

            // Check if myObject is null
            if (myObject == null)
                throw new ArgumentNullException("myObject must not be null!");

            // Check if myObject can be casted to an NetworkInterfaceCounter object
            var myNetworkInterfaceCounter = myObject as NetworkInterfaceCounter;
            if ((Object) myNetworkInterfaceCounter == null)
                throw new ArgumentException("myObject is not of type NetworkInterfaceCounter!");

            return CompareTo(myNetworkInterfaceCounter);

        }

        #endregion

        #region CompareTo(myNetworkInterfaceCounter)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myNetworkInterfaceCounter">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(NetworkInterfaceCounter myNetworkInterfaceCounter)
        {

            // Check if myNetworkInterfaceCounter is null
            if (myNetworkInterfaceCounter == null)
                throw new ArgumentNullException("myElementId must not be null!");

            return _Id.CompareTo(myNetworkInterfaceCounter._Id);

        }

        #endregion

        #endregion

        #region IEquatable<NetworkInterfaceCounter> Members

        #region Equals(myObject)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myObject">An object to compare with.</param>
        /// <returns>true|false</returns>
        public override Boolean Equals(Object myObject)
        {

            // Check if myObject is null
            if (myObject == null)
                throw new ArgumentNullException("Parameter myObject must not be null!");

            // Check if myObject can be cast to NetworkInterfaceCounter
            var myNetworkInterfaceCounter = myObject as NetworkInterfaceCounter;
            if ((Object) myNetworkInterfaceCounter == null)
                throw new ArgumentException("Parameter myObject could not be casted to type NetworkInterfaceCounter!");

            return this.Equals(myNetworkInterfaceCounter);

        }

        #endregion

        #region Equals(myNetworkInterfaceCounter)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myNetworkInterfaceCounter">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(NetworkInterfaceCounter myNetworkInterfaceCounter)
        {

            // Check if myNetworkInterfaceCounter is null
            if (myNetworkInterfaceCounter == null)
                throw new ArgumentNullException("Parameter myNetworkInterfaceCounter must not be null!");

            return _Id.Equals(myNetworkInterfaceCounter._Id);

        }

        #endregion

        #endregion

        #region GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override Int32 GetHashCode()
        {
            return _Id.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        public override String ToString()
        {
            return _CounterName;
        }

        #endregion

    }
}
