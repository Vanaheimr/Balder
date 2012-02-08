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
    /// An EdgeId is unique identificator for an edge.
    /// </summary>    
    public class EdgeId : AGraphElementId, IEquatable<EdgeId>, IComparable<EdgeId>, IComparable
    {

        #region Constructor(s)

        #region EdgeId()

        /// <summary>
        /// Generates a new EdgeId based on a GUID.
        /// </summary>
        public EdgeId()
            : base()
        { }

        #endregion

        #region EdgeId(Int32)

        /// <summary>
        /// Generates a EdgeId based on the content of an Int32.
        /// </summary>
        public EdgeId(Int32 Int32)
            : base(Int32)
        { }

        #endregion

        #region EdgeId(UInt32)

        /// <summary>
        /// Generates a EdgeId based on the content of an UInt32.
        /// </summary>
        public EdgeId(UInt32 UInt32)
            : base(UInt32)
        { }

        #endregion

        #region EdgeId(Int64)

        /// <summary>
        /// Generates a EdgeId based on the content of an Int64.
        /// </summary>
        public EdgeId(Int64 Int64)
            : base(Int64)
        { }

        #endregion

        #region EdgeId(UInt64)

        /// <summary>
        /// Generates a EdgeId based on the content of an UInt64.
        /// </summary>
        public EdgeId(UInt64 UInt64)
            : base(UInt64)
        { }

        #endregion

        #region EdgeId(String)

        /// <summary>
        /// Generates a EdgeId based on the content of String.
        /// </summary>
        public EdgeId(String String)
            : base(String)
        { }

        #endregion

        #region EdgeId(Uri)

        /// <summary>
        /// Generates a EdgeId based on the content of Uri.
        /// </summary>
        public EdgeId(Uri Uri)
            : base(Uri)
        { }

        #endregion

        #region EdgeId(EdgeId)

        /// <summary>
        /// Generates a EdgeId based on the content of EdgeId.
        /// </summary>
        /// <param name="EdgeId">A EdgeId</param>
        public EdgeId(EdgeId EdgeId)
            : base(EdgeId)
        { }

        #endregion

        #endregion


        #region NewEdgeId

        /// <summary>
        /// Generate a new EdgeId.
        /// </summary>
        public static EdgeId NewEdgeId
        {
            get
            {
                return new EdgeId(Guid.NewGuid().ToString());
            }
        }

        #endregion


        #region Operator overloading

        #region Operator == (EdgeId1, EdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EdgeId1">A EdgeId.</param>
        /// <param name="EdgeId2">Another EdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (EdgeId EdgeId1, EdgeId EdgeId2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(EdgeId1, EdgeId2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) EdgeId1 == null) || ((Object) EdgeId2 == null))
                return false;

            return EdgeId1.Equals(EdgeId2);

        }

        #endregion

        #region Operator != (EdgeId1, EdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EdgeId1">A EdgeId.</param>
        /// <param name="EdgeId2">Another EdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (EdgeId EdgeId1, EdgeId EdgeId2)
        {
            return !(EdgeId1 == EdgeId2);
        }

        #endregion

        #region Operator <  (EdgeId1, EdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EdgeId1">A EdgeId.</param>
        /// <param name="EdgeId2">Another EdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (EdgeId EdgeId1, EdgeId EdgeId2)
        {

            if ((Object) EdgeId1 == null)
                throw new ArgumentNullException("The given EdgeId1 must not be null!");

            return EdgeId1.CompareTo(EdgeId2) < 0;

        }

        #endregion

        #region Operator <= (EdgeId1, EdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EdgeId1">A EdgeId.</param>
        /// <param name="EdgeId2">Another EdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (EdgeId EdgeId1, EdgeId EdgeId2)
        {
            return !(EdgeId1 > EdgeId2);
        }

        #endregion

        #region Operator >  (EdgeId1, EdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EdgeId1">A EdgeId.</param>
        /// <param name="EdgeId2">Another EdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (EdgeId EdgeId1, EdgeId EdgeId2)
        {

            if ((Object) EdgeId1 == null)
                throw new ArgumentNullException("The given EdgeId1 must not be null!");

            return EdgeId1.CompareTo(EdgeId2) > 0;

        }

        #endregion

        #region Operator >= (EdgeId1, EdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EdgeId1">A EdgeId.</param>
        /// <param name="EdgeId2">Another EdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (EdgeId EdgeId1, EdgeId EdgeId2)
        {
            return !(EdgeId1 < EdgeId2);
        }

        #endregion

        #endregion

        #region IComparable<EdgeId> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public override Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given object must not be null!");

            // Check if the given object is an EdgeId.
            var EdgeId = Object as EdgeId;
            if ((Object) EdgeId == null)
                throw new ArgumentException("The given object is not a EdgeId!");

            return CompareTo(EdgeId);

        }

        #endregion

        #region CompareTo(EdgeId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EdgeId">An object to compare with.</param>
        public Int32 CompareTo(EdgeId EdgeId)
        {

            if ((Object) EdgeId == null)
                throw new ArgumentNullException("The given EdgeId must not be null!");

            // Compare the length of the EdgeIds
            var _Result = this.Length.CompareTo(EdgeId.Length);

            // If equal: Compare Ids
            if (_Result == 0)
                _Result = _Id.CompareTo(EdgeId._Id);

            return _Result;

        }

        #endregion

        #endregion

        #region IEquatable<EdgeId> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>true|false</returns>
        public override Boolean Equals(Object Object)
        {

            if (Object == null)
                return false;

            // Check if the given object is an EdgeId.
            var EdgeId = Object as EdgeId;
            if ((Object) EdgeId == null)
                return false;

            return this.Equals(EdgeId);

        }

        #endregion

        #region Equals(EdgeId)

        /// <summary>
        /// Compares two EdgeIds for equality.
        /// </summary>
        /// <param name="EdgeId">An EdgeId to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(EdgeId EdgeId)
        {

            if ((Object) EdgeId == null)
                return false;

            return _Id.Equals(EdgeId._Id);

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
