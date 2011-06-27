/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET <http://www.github.com/ahzf/Blueprints.NET>
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

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// A SystemId is unique identificator for a single system within
    /// a larger distributed system.
    /// </summary>    
    public class SystemId : IEquatable<SystemId>, IComparable<SystemId>, IComparable
    {

        #region Data

        /// <summary>
        /// The internal identification.
        /// </summary>
        protected readonly String _Id;

        #endregion

        #region Properties

        #region Length

        /// <summary>
        /// Returns the length of the identificator.
        /// </summary>
        public UInt64 Length
        {
            get
            {
                return (UInt64) _Id.Length;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region SystemId()

        /// <summary>
        /// Generates a new SystemId based on a GUID.
        /// </summary>
        public SystemId()
        {
            _Id = Guid.NewGuid().ToString();
        }

        #endregion

        #region SystemId(Int32)

        /// <summary>
        /// Generates a SystemId based on the content of an Int32.
        /// </summary>
        public SystemId(Int32 Int32)
            : this(Math.Abs(Int32).ToString())
        { }

        #endregion

        #region SystemId(UInt32)

        /// <summary>
        /// Generates a SystemId based on the content of an UInt32.
        /// </summary>
        public SystemId(UInt32 UInt32)
            : this(UInt32.ToString())
        { }

        #endregion

        #region SystemId(Int64)

        /// <summary>
        /// Generates a SystemId based on the content of an Int64.
        /// </summary>
        public SystemId(Int64 Int64)
            : this(Int64.ToString())
        { }

        #endregion

        #region SystemId(UInt64)

        /// <summary>
        /// Generates a SystemId based on the content of an UInt64.
        /// </summary>
        public SystemId(UInt64 UInt64)
            : this(UInt64.ToString())
        { }

        #endregion

        #region SystemId(String)

        /// <summary>
        /// Generates a SystemId based on the content of String.
        /// </summary>
        public SystemId(String String)
        {
            _Id = String.Trim();
        }

        #endregion

        #region SystemId(Uri)

        /// <summary>
        /// Generates a SystemId based on the content of Uri.
        /// </summary>
        public SystemId(Uri Uri)
        {
            _Id = Uri.ToString();
        }

        #endregion

        #region SystemId(SystemId)

        /// <summary>
        /// Generates a SystemId based on the content of SystemId.
        /// </summary>
        /// <param name="SystemId">A SystemId</param>
        public SystemId(SystemId SystemId)
        {
            _Id = SystemId.ToString();
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

        #region Operator == (SystemId1, SystemId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SystemId1">A SystemId.</param>
        /// <param name="SystemId2">Another SystemId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (SystemId SystemId1, SystemId SystemId2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(SystemId1, SystemId2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) SystemId1 == null) || ((Object) SystemId2 == null))
                return false;

            return SystemId1.Equals(SystemId2);

        }

        #endregion

        #region Operator != (SystemId1, SystemId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SystemId1">A SystemId.</param>
        /// <param name="SystemId2">Another SystemId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (SystemId SystemId1, SystemId SystemId2)
        {
            return !(SystemId1 == SystemId2);
        }

        #endregion

        #region Operator <  (SystemId1, SystemId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SystemId1">A SystemId.</param>
        /// <param name="SystemId2">Another SystemId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (SystemId SystemId1, SystemId SystemId2)
        {

            if ((Object) SystemId1 == null)
                throw new ArgumentNullException("The given SystemId1 must not be null!");

            return SystemId1.CompareTo(SystemId2) < 0;

        }

        #endregion

        #region Operator >  (SystemId1, SystemId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SystemId1">A SystemId.</param>
        /// <param name="SystemId2">Another SystemId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (SystemId SystemId1, SystemId SystemId2)
        {

            if ((Object) SystemId1 == null)
                throw new ArgumentNullException("The given SystemId1 must not be null!");

            return SystemId1.CompareTo(SystemId2) > 0;

        }

        #endregion

        #region Operator <= (SystemId1, SystemId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SystemId1">A SystemId.</param>
        /// <param name="SystemId2">Another SystemId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (SystemId SystemId1, SystemId SystemId2)
        {
            return !(SystemId1 > SystemId2);
        }

        #endregion

        #region Operator >= (SystemId1, SystemId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SystemId1">A SystemId.</param>
        /// <param name="SystemId2">Another SystemId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (SystemId SystemId1, SystemId SystemId2)
        {
            return !(SystemId1 < SystemId2);
        }

        #endregion

        #endregion

        #region IComparable<SystemId> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given object must not be null!");

            // Check if the given object is an SystemId.
            var SystemId = Object as SystemId;
            if ((Object) SystemId == null)
                throw new ArgumentException("The given object is not a SystemId!");

            return CompareTo(SystemId);

        }

        #endregion

        #region CompareTo(SystemId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SystemId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(SystemId SystemId)
        {

            if ((Object) SystemId == null)
                throw new ArgumentNullException("The given SystemId must not be null!");

            // Compare the length of the SystemIds
            var _Result = this.Length.CompareTo(SystemId.Length);

            // If equal: Compare Ids
            if (_Result == 0)
                _Result = _Id.CompareTo(SystemId._Id);

            return _Result;

        }

        #endregion

        #endregion

        #region IEquatable<SystemId> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>true|false</returns>
        public override Boolean Equals(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given object must not be null!");

            // Check if the given object is an SystemId.
            var SystemId = Object as SystemId;
            if ((Object) SystemId == null)
                throw new ArgumentException("The given object is not a SystemId!");

            return this.Equals(SystemId);

        }

        #endregion

        #region Equals(SystemId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SystemId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(SystemId SystemId)
        {

            if ((Object) SystemId == null)
                return false;

            return _Id.Equals(SystemId._Id);

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
        /// Return a string represtentation of this object.
        /// </summary>
        public override String ToString()
        {
            return _Id.ToString();
        }

        #endregion

    }

}
