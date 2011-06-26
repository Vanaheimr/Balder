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
    /// A HyperEdgeId is the unique identificator of any sensor.
    /// </summary>    
    public class HyperEdgeId : IEquatable<HyperEdgeId>, IComparable<HyperEdgeId>, IComparable
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

        #region HyperEdgeId()

        /// <summary>
        /// Generates a new HyperEdgeId based on a GUID.
        /// </summary>
        public HyperEdgeId()
        {
            _Id = Guid.NewGuid().ToString();
        }

        #endregion

        #region HyperEdgeId(Int32)

        /// <summary>
        /// Generates a HyperEdgeId based on the content of an Int32.
        /// </summary>
        public HyperEdgeId(Int32 Int32)
            : this(Math.Abs(Int32).ToString())
        { }

        #endregion

        #region HyperEdgeId(UInt32)

        /// <summary>
        /// Generates a HyperEdgeId based on the content of an UInt32.
        /// </summary>
        public HyperEdgeId(UInt32 UInt32)
            : this(UInt32.ToString())
        { }

        #endregion

        #region HyperEdgeId(Int64)

        /// <summary>
        /// Generates a HyperEdgeId based on the content of an Int64.
        /// </summary>
        public HyperEdgeId(Int64 Int64)
            : this(Int64.ToString())
        { }

        #endregion

        #region HyperEdgeId(UInt64)

        /// <summary>
        /// Generates a HyperEdgeId based on the content of an UInt64.
        /// </summary>
        public HyperEdgeId(UInt64 UInt64)
            : this(UInt64.ToString())
        { }

        #endregion

        #region HyperEdgeId(String)

        /// <summary>
        /// Generates a HyperEdgeId based on the content of String.
        /// </summary>
        public HyperEdgeId(String String)
        {
            _Id = String.Trim();
        }

        #endregion

        #region HyperEdgeId(Uri)

        /// <summary>
        /// Generates a HyperEdgeId based on the content of Uri.
        /// </summary>
        public HyperEdgeId(Uri Uri)
        {
            _Id = Uri.ToString();
        }

        #endregion

        #region HyperEdgeId(HyperEdgeId)

        /// <summary>
        /// Generates a HyperEdgeId based on the content of HyperEdgeId.
        /// </summary>
        /// <param name="HyperEdgeId">A HyperEdgeId</param>
        public HyperEdgeId(HyperEdgeId HyperEdgeId)
        {
            _Id = HyperEdgeId.ToString();
        }

        #endregion

        #endregion


        #region NewHyperEdgeId

        /// <summary>
        /// Generate a new HyperEdgeId.
        /// </summary>
        public static HyperEdgeId NewHyperEdgeId
        {
            get
            {
                return new HyperEdgeId(Guid.NewGuid().ToString());
            }
        }

        #endregion


        #region Operator overloading

        #region Operator == (HyperEdgeId1, HyperEdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HyperEdgeId1">A HyperEdgeId.</param>
        /// <param name="HyperEdgeId2">Another HyperEdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (HyperEdgeId HyperEdgeId1, HyperEdgeId HyperEdgeId2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(HyperEdgeId1, HyperEdgeId2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) HyperEdgeId1 == null) || ((Object) HyperEdgeId2 == null))
                return false;

            return HyperEdgeId1.Equals(HyperEdgeId2);

        }

        #endregion

        #region Operator != (HyperEdgeId1, HyperEdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HyperEdgeId1">A HyperEdgeId.</param>
        /// <param name="HyperEdgeId2">Another HyperEdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (HyperEdgeId HyperEdgeId1, HyperEdgeId HyperEdgeId2)
        {
            return !(HyperEdgeId1 == HyperEdgeId2);
        }

        #endregion

        #region Operator <  (HyperEdgeId1, HyperEdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HyperEdgeId1">A HyperEdgeId.</param>
        /// <param name="HyperEdgeId2">Another HyperEdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (HyperEdgeId HyperEdgeId1, HyperEdgeId HyperEdgeId2)
        {

            if ((Object) HyperEdgeId1 == null)
                throw new ArgumentNullException("The given HyperEdgeId1 must not be null!");

            return HyperEdgeId1.CompareTo(HyperEdgeId2) < 0;

        }

        #endregion

        #region Operator >  (HyperEdgeId1, HyperEdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HyperEdgeId1">A HyperEdgeId.</param>
        /// <param name="HyperEdgeId2">Another HyperEdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (HyperEdgeId HyperEdgeId1, HyperEdgeId HyperEdgeId2)
        {

            if ((Object) HyperEdgeId1 == null)
                throw new ArgumentNullException("The given HyperEdgeId1 must not be null!");

            return HyperEdgeId1.CompareTo(HyperEdgeId2) > 0;

        }

        #endregion

        #region Operator <= (HyperEdgeId1, HyperEdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HyperEdgeId1">A HyperEdgeId.</param>
        /// <param name="HyperEdgeId2">Another HyperEdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (HyperEdgeId HyperEdgeId1, HyperEdgeId HyperEdgeId2)
        {
            return !(HyperEdgeId1 > HyperEdgeId2);
        }

        #endregion

        #region Operator >= (HyperEdgeId1, HyperEdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HyperEdgeId1">A HyperEdgeId.</param>
        /// <param name="HyperEdgeId2">Another HyperEdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (HyperEdgeId HyperEdgeId1, HyperEdgeId HyperEdgeId2)
        {
            return !(HyperEdgeId1 < HyperEdgeId2);
        }

        #endregion

        #endregion

        #region IComparable<HyperEdgeId> Members

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

            // Check if the given object is an HyperEdgeId.
            var HyperEdgeId = Object as HyperEdgeId;
            if ((Object) HyperEdgeId == null)
                throw new ArgumentException("The given object is not a HyperEdgeId!");

            return CompareTo(HyperEdgeId);

        }

        #endregion

        #region CompareTo(HyperEdgeId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HyperEdgeId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(HyperEdgeId HyperEdgeId)
        {

            if ((Object) HyperEdgeId == null)
                throw new ArgumentNullException("The given HyperEdgeId must not be null!");

            // Compare the length of the HyperEdgeIds
            var _Result = this.Length.CompareTo(HyperEdgeId.Length);

            // If equal: Compare Ids
            if (_Result == 0)
                _Result = _Id.CompareTo(HyperEdgeId._Id);

            return _Result;

        }

        #endregion

        #endregion

        #region IEquatable<HyperEdgeId> Members

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

            // Check if the given object is an HyperEdgeId.
            var HyperEdgeId = Object as HyperEdgeId;
            if ((Object) HyperEdgeId == null)
                throw new ArgumentException("The given object is not a HyperEdgeId!");

            return this.Equals(HyperEdgeId);

        }

        #endregion

        #region Equals(HyperEdgeId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HyperEdgeId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(HyperEdgeId HyperEdgeId)
        {

            if ((Object) HyperEdgeId == null)
                throw new ArgumentNullException("The given HyperEdgeId must not be null!");

            return _Id.Equals(HyperEdgeId._Id);

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
