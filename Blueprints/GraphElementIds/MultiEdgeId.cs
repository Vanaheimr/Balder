/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Blueprints.NET <http://www.github.com/Vanaheimr/Blueprints.NET>
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

namespace de.ahzf.Vanaheimr.Blueprints
{

    /// <summary>
    /// A MultiEdgeId is unique identificator of a multiedge.
    /// </summary>    
    public class MultiEdgeId : AGraphElementId,
                               IEquatable<MultiEdgeId>,
                               IComparable<MultiEdgeId>,
                               IComparable

    {

        #region Constructor(s)

        #region MultiEdgeId()

        /// <summary>
        /// Generates a new MultiEdgeId based on a GUID.
        /// </summary>
        public MultiEdgeId()
            : base()
        { }

        #endregion

        #region MultiEdgeId(Int32)

        /// <summary>
        /// Generates a MultiEdgeId based on the content of an Int32.
        /// </summary>
        public MultiEdgeId(Int32 Int32)
            : base(Int32)
        { }

        #endregion

        #region MultiEdgeId(UInt32)

        /// <summary>
        /// Generates a MultiEdgeId based on the content of an UInt32.
        /// </summary>
        public MultiEdgeId(UInt32 UInt32)
            : base(UInt32)
        { }

        #endregion

        #region MultiEdgeId(Int64)

        /// <summary>
        /// Generates a MultiEdgeId based on the content of an Int64.
        /// </summary>
        public MultiEdgeId(Int64 Int64)
            : base(Int64)
        { }

        #endregion

        #region MultiEdgeId(UInt64)

        /// <summary>
        /// Generates a MultiEdgeId based on the content of an UInt64.
        /// </summary>
        public MultiEdgeId(UInt64 UInt64)
            : base(UInt64)
        { }

        #endregion

        #region MultiEdgeId(String)

        /// <summary>
        /// Generates a MultiEdgeId based on the content of String.
        /// </summary>
        public MultiEdgeId(String String)
            : base(String)
        { }

        #endregion

        #region MultiEdgeId(Uri)

        /// <summary>
        /// Generates a MultiEdgeId based on the content of Uri.
        /// </summary>
        public MultiEdgeId(Uri Uri)
            : base(Uri)
        { }

        #endregion

        #region MultiEdgeId(MultiEdgeId)

        /// <summary>
        /// Generates a MultiEdgeId based on the content of MultiEdgeId.
        /// </summary>
        /// <param name="MultiEdgeId">A MultiEdgeId</param>
        public MultiEdgeId(MultiEdgeId MultiEdgeId)
            : base(MultiEdgeId)
        { }

        #endregion

        #endregion


        #region NewMultiEdgeId

        /// <summary>
        /// Generate a new MultiEdgeId.
        /// </summary>
        public static MultiEdgeId NewMultiEdgeId
        {
            get
            {
                return new MultiEdgeId(Guid.NewGuid().ToString());
            }
        }

        #endregion


        #region Operator overloading

        #region Operator == (MultiEdgeId1, MultiEdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="MultiEdgeId1">A MultiEdgeId.</param>
        /// <param name="MultiEdgeId2">Another MultiEdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (MultiEdgeId MultiEdgeId1, MultiEdgeId MultiEdgeId2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(MultiEdgeId1, MultiEdgeId2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) MultiEdgeId1 == null) || ((Object) MultiEdgeId2 == null))
                return false;

            return MultiEdgeId1.Equals(MultiEdgeId2);

        }

        #endregion

        #region Operator != (MultiEdgeId1, MultiEdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="MultiEdgeId1">A MultiEdgeId.</param>
        /// <param name="MultiEdgeId2">Another MultiEdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (MultiEdgeId MultiEdgeId1, MultiEdgeId MultiEdgeId2)
        {
            return !(MultiEdgeId1 == MultiEdgeId2);
        }

        #endregion

        #region Operator <  (MultiEdgeId1, MultiEdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="MultiEdgeId1">A MultiEdgeId.</param>
        /// <param name="MultiEdgeId2">Another MultiEdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (MultiEdgeId MultiEdgeId1, MultiEdgeId MultiEdgeId2)
        {

            if ((Object) MultiEdgeId1 == null)
                throw new ArgumentNullException("The given MultiEdgeId1 must not be null!");

            return MultiEdgeId1.CompareTo(MultiEdgeId2) < 0;

        }

        #endregion

        #region Operator <= (MultiEdgeId1, MultiEdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="MultiEdgeId1">A MultiEdgeId.</param>
        /// <param name="MultiEdgeId2">Another MultiEdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (MultiEdgeId MultiEdgeId1, MultiEdgeId MultiEdgeId2)
        {
            return !(MultiEdgeId1 > MultiEdgeId2);
        }

        #endregion

        #region Operator >  (MultiEdgeId1, MultiEdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="MultiEdgeId1">A MultiEdgeId.</param>
        /// <param name="MultiEdgeId2">Another MultiEdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (MultiEdgeId MultiEdgeId1, MultiEdgeId MultiEdgeId2)
        {

            if ((Object) MultiEdgeId1 == null)
                throw new ArgumentNullException("The given MultiEdgeId1 must not be null!");

            return MultiEdgeId1.CompareTo(MultiEdgeId2) > 0;

        }

        #endregion

        #region Operator >= (MultiEdgeId1, MultiEdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="MultiEdgeId1">A MultiEdgeId.</param>
        /// <param name="MultiEdgeId2">Another MultiEdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (MultiEdgeId MultiEdgeId1, MultiEdgeId MultiEdgeId2)
        {
            return !(MultiEdgeId1 < MultiEdgeId2);
        }

        #endregion

        #endregion

        #region IComparable<MultiEdgeId> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public override Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given object must not be null!");

            // Check if the given object is an MultiEdgeId.
            var MultiEdgeId = Object as MultiEdgeId;
            if ((Object) MultiEdgeId == null)
                throw new ArgumentException("The given object is not a MultiEdgeId!");

            return CompareTo(MultiEdgeId);

        }

        #endregion

        #region CompareTo(MultiEdgeId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="MultiEdgeId">An object to compare with.</param>
        public Int32 CompareTo(MultiEdgeId MultiEdgeId)
        {

            if ((Object) MultiEdgeId == null)
                throw new ArgumentNullException("The given MultiEdgeId must not be null!");

            // Compare the length of the MultiEdgeIds
            var _Result = this.Length.CompareTo(MultiEdgeId.Length);

            // If equal: Compare Ids
            if (_Result == 0)
                _Result = _Id.CompareTo(MultiEdgeId._Id);

            return _Result;

        }

        #endregion

        #endregion

        #region IEquatable<MultiEdgeId> Members

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

            // Check if the given object is an MultiEdgeId.
            var MultiEdgeId = Object as MultiEdgeId;
            if ((Object) MultiEdgeId == null)
                return false;

            return this.Equals(MultiEdgeId);

        }

        #endregion

        #region Equals(MultiEdgeId)

        /// <summary>
        /// Compares two MultiEdgeIds for equality.
        /// </summary>
        /// <param name="MultiEdgeId">A MultiEdgeId to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(MultiEdgeId MultiEdgeId)
        {

            if ((Object) MultiEdgeId == null)
                return false;

            return _Id.Equals(MultiEdgeId._Id);

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
