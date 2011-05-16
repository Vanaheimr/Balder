/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET <http://www.github.com/ahzf/blueprints.NET>
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
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace de.ahzf.blueprints
{

    /// <summary>
    /// A SystemId is unique identificator for a single system within
    /// a larger distributed system.
    /// </summary>    
    public class SystemId : ElementId, IComparable, IComparable<SystemId>, IEquatable<SystemId>
    {

        #region Constructor(s)

        #region SystemId()

        /// <summary>
        /// Generates a new SystemId
        /// </summary>
        public SystemId()
            : base()
        {
        }

        #endregion

        #region SystemId(myInt32)

        /// <summary>
        /// Generates a SystemId based on the content of an Int32
        /// </summary>
        public SystemId(Int32 myInt32)
            : base(myInt32)
        {
        }

        #endregion

        #region SystemId(myUInt32)

        /// <summary>
        /// Generates a SystemId based on the content of an UInt32
        /// </summary>
        public SystemId(UInt32 myUInt32)
            : base(myUInt32)
        {
        }

        #endregion

        #region SystemId(myInt64)

        /// <summary>
        /// Generates a SystemId based on the content of an Int64
        /// </summary>
        public SystemId(Int64 myInt64)
            : base(myInt64)
        {
        }

        #endregion

        #region SystemId(myUInt64)

        /// <summary>
        /// Generates a SystemId based on the content of an UInt64
        /// </summary>
        public SystemId(UInt64 myUInt64)
            : base(myUInt64)
        {
        }

        #endregion

        #region SystemId(myString)

        /// <summary>
        /// Generates a SystemId based on the content of myString.
        /// </summary>
        public SystemId(String myString)
            : base(myString)
        {
        }

        #endregion

        #region SystemId(myUri)

        /// <summary>
        /// Generates a SystemId based on the content of myUri.
        /// </summary>
        public SystemId(Uri myUri)
            : base(myUri)
        {
        }

        #endregion

        #region SystemId(mySystemId)

        /// <summary>
        /// Generates a SystemId based on the content of mySystemId
        /// </summary>
        /// <param name="mySystemId">A SystemId</param>
        public SystemId(SystemId mySystemId)
            : base(mySystemId)
        {
        }

        #endregion

        #endregion

        #region NewSystemId

        /// <summary>
        /// Generate a new SystemId.
        /// </summary>
        public static SystemId NewSystemId
        {
            get
            {
                return new SystemId(Guid.NewGuid().ToString());
            }
        }

        #endregion


        #region Operator overloading

        #region Operator == (mySystemId1, mySystemId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="mySystemId1">A SystemId.</param>
        /// <param name="mySystemId2">Another SystemId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (SystemId mySystemId1, SystemId mySystemId2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(mySystemId1, mySystemId2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) mySystemId1 == null) || ((Object) mySystemId2 == null))
                return false;

            return mySystemId1.Equals(mySystemId2);

        }

        #endregion

        #region Operator != (mySystemId1, mySystemId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="mySystemId1">A SystemId.</param>
        /// <param name="mySystemId2">Another SystemId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (SystemId mySystemId1, SystemId mySystemId2)
        {
            return !(mySystemId1 == mySystemId2);
        }

        #endregion

        #region Operator <  (mySystemId1, mySystemId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="mySystemId1">A SystemId.</param>
        /// <param name="mySystemId2">Another SystemId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (SystemId mySystemId1, SystemId mySystemId2)
        {

            // Check if mySystemId1 is null
            if ((Object) mySystemId1 == null)
                throw new ArgumentNullException("Parameter mySystemId1 must not be null!");

            // Check if mySystemId2 is null
            if ((Object) mySystemId2 == null)
                throw new ArgumentNullException("Parameter mySystemId2 must not be null!");


            // Check the length of the SystemIds
            if (mySystemId1.Length < mySystemId2.Length)
                return true;

            if (mySystemId1.Length > mySystemId2.Length)
                return false;

            return mySystemId1.CompareTo(mySystemId2) < 0;

        }

        #endregion

        #region Operator >  (mySystemId1, mySystemId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="mySystemId1">A SystemId.</param>
        /// <param name="mySystemId2">Another SystemId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (SystemId mySystemId1, SystemId mySystemId2)
        {

            // Check if mySystemId1 is null
            if ((Object) mySystemId1 == null)
                throw new ArgumentNullException("Parameter mySystemId1 must not be null!");

            // Check if mySystemId2 is null
            if ((Object) mySystemId2 == null)
                throw new ArgumentNullException("Parameter mySystemId2 must not be null!");


            // Check the length of the SystemIds
            if (mySystemId1.Length > mySystemId2.Length)
                return true;

            if (mySystemId1.Length < mySystemId2.Length)
                return false;

            return mySystemId1.CompareTo(mySystemId2) > 0;

        }

        #endregion

        #region Operator <= (mySystemId1, mySystemId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="mySystemId1">A SystemId.</param>
        /// <param name="mySystemId2">Another SystemId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (SystemId mySystemId1, SystemId mySystemId2)
        {
            return !(mySystemId1 > mySystemId2);
        }

        #endregion

        #region Operator >= (mySystemId1, mySystemId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="mySystemId1">A SystemId.</param>
        /// <param name="mySystemId2">Another SystemId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (SystemId mySystemId1, SystemId mySystemId2)
        {
            return !(mySystemId1 < mySystemId2);
        }

        #endregion

        #endregion

        #region IComparable<SystemId> Members

        #region CompareTo(myObject)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myObject">An object to compare with.</param>
        /// <returns>true|false</returns>
        public new Int32 CompareTo(Object myObject)
        {

            // Check if myObject is null
            if (myObject == null)
                throw new ArgumentNullException("myObject must not be null!");

            // Check if myObject can be casted to an SystemId object
            var mySystemId = myObject as SystemId;
            if ((Object) mySystemId == null)
                throw new ArgumentException("myObject is not of type SystemId!");

            return CompareTo(mySystemId);

        }

        #endregion

        #region CompareTo(mySystemId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="mySystemId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(SystemId mySystemId)
        {

            // Check if mySystemId is null
            if (mySystemId == null)
                throw new ArgumentNullException("mySystemId must not be null!");

            return _ElementId.CompareTo(mySystemId._ElementId);

        }

        #endregion

        #endregion

        #region IEquatable<SystemId> Members

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

            // Check if myObject can be cast to SystemId
            var mySystemId = myObject as SystemId;
            if ((Object) mySystemId == null)
                throw new ArgumentException("Parameter myObject could not be casted to type SystemId!");

            return this.Equals(mySystemId);

        }

        #endregion

        #region Equals(mySystemId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="mySystemId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(SystemId mySystemId)
        {

            // Check if mySystemId is null
            if (mySystemId == null)
                throw new ArgumentNullException("Parameter mySystemId must not be null!");

            return _ElementId.Equals(mySystemId._ElementId);

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
            return base.GetHashCode();
        }

        #endregion

    }

}
