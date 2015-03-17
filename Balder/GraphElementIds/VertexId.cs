/*
 * Copyright (c) 2010-2015, Achim 'ahzf' Friedland <achim@graphdefined.org>
 * This file is part of Balder <http://www.github.com/Vanaheimr/Balder>
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

namespace org.GraphDefined.Vanaheimr.Balder
{

    /// <summary>
    /// A VertexId is unique identificator of a vertex.
    /// </summary>    
    public class VertexId : AGraphElementId,
                            IEquatable<VertexId>,
                            IComparable<VertexId>,
                            IComparable

    {

        #region Constructor(s)

        #region VertexId()

        /// <summary>
        /// Generates a new VertexId based on a GUID.
        /// </summary>
        public VertexId()
            : base()
        { }

        #endregion

        #region VertexId(Int32)

        /// <summary>
        /// Generates a VertexId based on the content of an Int32.
        /// </summary>
        public VertexId(Int32 Int32)
            : base(Int32)
        { }

        #endregion

        #region VertexId(UInt32)

        /// <summary>
        /// Generates a VertexId based on the content of an UInt32.
        /// </summary>
        public VertexId(UInt32 UInt32)
            : base(UInt32)
        { }

        #endregion

        #region VertexId(Int64)

        /// <summary>
        /// Generates a VertexId based on the content of an Int64.
        /// </summary>
        public VertexId(Int64 Int64)
            : base(Int64)
        { }

        #endregion

        #region VertexId(UInt64)

        /// <summary>
        /// Generates a VertexId based on the content of an UInt64.
        /// </summary>
        public VertexId(UInt64 UInt64)
            : base(UInt64)
        { }

        #endregion

        #region VertexId(String)

        /// <summary>
        /// Generates a VertexId based on the content of String.
        /// </summary>
        public VertexId(String String)
            : base(String)
        { }

        #endregion

        #region VertexId(Uri)

        /// <summary>
        /// Generates a VertexId based on the content of Uri.
        /// </summary>
        public VertexId(Uri Uri)
            : base(Uri)
        { }

        #endregion

        #region VertexId(VertexId)

        /// <summary>
        /// Generates a VertexId based on the content of VertexId.
        /// </summary>
        /// <param name="VertexId">A VertexId</param>
        public VertexId(VertexId VertexId)
            : base(VertexId)
        { }

        #endregion

        #endregion


        #region NewVertexId

        /// <summary>
        /// Generate a new VertexId.
        /// </summary>
        public static VertexId NewVertexId
        {
            get
            {
                return new VertexId(Guid.NewGuid().ToString());
            }
        }

        #endregion


        #region Operator overloading

        #region Operator == (VertexId1, VertexId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VertexId1">A VertexId.</param>
        /// <param name="VertexId2">Another VertexId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (VertexId VertexId1, VertexId VertexId2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(VertexId1, VertexId2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) VertexId1 == null) || ((Object) VertexId2 == null))
                return false;

            return VertexId1.Equals(VertexId2);

        }

        #endregion

        #region Operator != (VertexId1, VertexId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VertexId1">A VertexId.</param>
        /// <param name="VertexId2">Another VertexId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (VertexId VertexId1, VertexId VertexId2)
        {
            return !(VertexId1 == VertexId2);
        }

        #endregion

        #region Operator <  (VertexId1, VertexId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VertexId1">A VertexId.</param>
        /// <param name="VertexId2">Another VertexId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (VertexId VertexId1, VertexId VertexId2)
        {

            if ((Object) VertexId1 == null)
                throw new ArgumentNullException("The given VertexId1 must not be null!");

            return VertexId1.CompareTo(VertexId2) < 0;

        }

        #endregion

        #region Operator <= (VertexId1, VertexId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VertexId1">A VertexId.</param>
        /// <param name="VertexId2">Another VertexId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (VertexId VertexId1, VertexId VertexId2)
        {
            return !(VertexId1 > VertexId2);
        }

        #endregion

        #region Operator >  (VertexId1, VertexId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VertexId1">A VertexId.</param>
        /// <param name="VertexId2">Another VertexId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (VertexId VertexId1, VertexId VertexId2)
        {

            if ((Object) VertexId1 == null)
                throw new ArgumentNullException("The given VertexId1 must not be null!");

            return VertexId1.CompareTo(VertexId2) > 0;

        }

        #endregion

        #region Operator >= (VertexId1, VertexId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VertexId1">A VertexId.</param>
        /// <param name="VertexId2">Another VertexId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (VertexId VertexId1, VertexId VertexId2)
        {
            return !(VertexId1 < VertexId2);
        }

        #endregion

        #endregion

        #region IComparable<VertexId> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public override Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given object must not be null!");

            // Check if the given object is an VertexId.
            var VertexId = Object as VertexId;
            if ((Object) VertexId == null)
                throw new ArgumentException("The given object is not a VertexId!");

            return CompareTo(VertexId);

        }

        #endregion

        #region CompareTo(VertexId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VertexId">An object to compare with.</param>
        public Int32 CompareTo(VertexId VertexId)
        {

            if ((Object) VertexId == null)
                throw new ArgumentNullException("The given VertexId must not be null!");

            // Compare the length of the VertexIds
            var _Result = this.Length.CompareTo(VertexId.Length);

            // If equal: Compare Ids
            if (_Result == 0)
                _Result = _Id.CompareTo(VertexId._Id);

            return _Result;

        }

        #endregion

        #endregion

        #region IEquatable<VertexId> Members

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

            // Check if the given object is an VertexId.
            var VertexId = Object as VertexId;
            if ((Object) VertexId == null)
                return false;

            return this.Equals(VertexId);

        }

        #endregion

        #region Equals(VertexId)

        /// <summary>
        /// Compares two VertexIds for equality.
        /// </summary>
        /// <param name="VertexId">A VertexId to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(VertexId VertexId)
        {

            if ((Object) VertexId == null)
                return false;

            return _Id.Equals(VertexId._Id);

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
