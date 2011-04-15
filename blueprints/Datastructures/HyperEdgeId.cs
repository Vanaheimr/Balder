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

#endregion

namespace de.ahzf.blueprints
{

    /// <summary>
    /// A HyperEdgeId is unique identificator for a hyperedge.
    /// </summary>
    public class HyperEdgeId : ElementId, IEquatable<HyperEdgeId>, IComparable<HyperEdgeId>, IComparable
    {

        #region Constructor(s)

        #region HyperEdgeId()

        /// <summary>
        /// Generates a new HyperEdgeId
        /// </summary>
        public HyperEdgeId()
            : base()
        {
        }

        #endregion

        #region HyperEdgeId(myInt32)

        /// <summary>
        /// Generates a HyperEdgeId based on the content of an Int32
        /// </summary>
        public HyperEdgeId(Int32 myInt32)
            : base(myInt32)
        {
        }

        #endregion

        #region HyperEdgeId(myUInt32)

        /// <summary>
        /// Generates a HyperEdgeId based on the content of an UInt32
        /// </summary>
        public HyperEdgeId(UInt32 myUInt32)
            : base(myUInt32)
        {
        }

        #endregion

        #region HyperEdgeId(myInt64)

        /// <summary>
        /// Generates a HyperEdgeId based on the content of an Int64
        /// </summary>
        public HyperEdgeId(Int64 myInt64)
            : base(myInt64)
        {
        }

        #endregion

        #region HyperEdgeId(myUInt64)

        /// <summary>
        /// Generates a HyperEdgeId based on the content of an UInt64
        /// </summary>
        public HyperEdgeId(UInt64 myUInt64)
            : base(myUInt64)
        {
        }

        #endregion

        #region HyperEdgeId(myString)

        /// <summary>
        /// Generates a HyperEdgeId based on the content of myString.
        /// </summary>
        public HyperEdgeId(String myString)
            : base(myString)
        {
        }

        #endregion

        #region HyperEdgeId(myUri)

        /// <summary>
        /// Generates a HyperEdgeId based on the content of myUri.
        /// </summary>
        public HyperEdgeId(Uri myUri)
            : base(myUri)
        {
        }

        #endregion

        #region HyperEdgeId(myHyperEdgeId)

        /// <summary>
        /// Generates a HyperEdgeId based on the content of myHyperEdgeId
        /// </summary>
        /// <param name="myHyperEdgeId">A HyperEdgeId</param>
        public HyperEdgeId(HyperEdgeId myHyperEdgeId)
            : base(myHyperEdgeId)
        {
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

        #region Operator == (myHyperEdgeId1, myHyperEdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myHyperEdgeId1">A HyperEdgeId.</param>
        /// <param name="myHyperEdgeId2">Another HyperEdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (HyperEdgeId myHyperEdgeId1, HyperEdgeId myHyperEdgeId2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(myHyperEdgeId1, myHyperEdgeId2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) myHyperEdgeId1 == null) || ((Object) myHyperEdgeId2 == null))
                return false;

            return myHyperEdgeId1.Equals(myHyperEdgeId2);

        }

        #endregion

        #region Operator != (myHyperEdgeId1, myHyperEdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myHyperEdgeId1">A HyperEdgeId.</param>
        /// <param name="myHyperEdgeId2">Another HyperEdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (HyperEdgeId myHyperEdgeId1, HyperEdgeId myHyperEdgeId2)
        {
            return !(myHyperEdgeId1 == myHyperEdgeId2);
        }

        #endregion

        #region Operator <  (myHyperEdgeId1, myHyperEdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myHyperEdgeId1">A HyperEdgeId.</param>
        /// <param name="myHyperEdgeId2">Another HyperEdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (HyperEdgeId myHyperEdgeId1, HyperEdgeId myHyperEdgeId2)
        {

            // Check if myHyperEdgeId1 is null
            if ((Object) myHyperEdgeId1 == null)
                throw new ArgumentNullException("Parameter myHyperEdgeId1 must not be null!");

            // Check if myHyperEdgeId2 is null
            if ((Object) myHyperEdgeId2 == null)
                throw new ArgumentNullException("Parameter myHyperEdgeId2 must not be null!");


            // Check the length of the HyperEdgeIds
            if (myHyperEdgeId1.Length < myHyperEdgeId2.Length)
                return true;

            if (myHyperEdgeId1.Length > myHyperEdgeId2.Length)
                return false;

            return myHyperEdgeId1.CompareTo(myHyperEdgeId2) < 0;

        }

        #endregion

        #region Operator >  (myHyperEdgeId1, myHyperEdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myHyperEdgeId1">A HyperEdgeId.</param>
        /// <param name="myHyperEdgeId2">Another HyperEdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (HyperEdgeId myHyperEdgeId1, HyperEdgeId myHyperEdgeId2)
        {

            // Check if myHyperEdgeId1 is null
            if ((Object) myHyperEdgeId1 == null)
                throw new ArgumentNullException("Parameter myHyperEdgeId1 must not be null!");

            // Check if myHyperEdgeId2 is null
            if ((Object) myHyperEdgeId2 == null)
                throw new ArgumentNullException("Parameter myHyperEdgeId2 must not be null!");


            // Check the length of the HyperEdgeIds
            if (myHyperEdgeId1.Length > myHyperEdgeId2.Length)
                return true;

            if (myHyperEdgeId1.Length < myHyperEdgeId2.Length)
                return false;

            return myHyperEdgeId1.CompareTo(myHyperEdgeId2) > 0;

        }

        #endregion

        #region Operator <= (myHyperEdgeId1, myHyperEdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myHyperEdgeId1">A HyperEdgeId.</param>
        /// <param name="myHyperEdgeId2">Another HyperEdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (HyperEdgeId myHyperEdgeId1, HyperEdgeId myHyperEdgeId2)
        {
            return !(myHyperEdgeId1 > myHyperEdgeId2);
        }

        #endregion

        #region Operator >= (myHyperEdgeId1, myHyperEdgeId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myHyperEdgeId1">A HyperEdgeId.</param>
        /// <param name="myHyperEdgeId2">Another HyperEdgeId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (HyperEdgeId myHyperEdgeId1, HyperEdgeId myHyperEdgeId2)
        {
            return !(myHyperEdgeId1 < myHyperEdgeId2);
        }

        #endregion

        #endregion

        #region IComparable<HyperEdgeId> Members

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

            // Check if myObject can be casted to an HyperEdgeId object
            var myHyperEdgeId = myObject as HyperEdgeId;
            if ((Object) myHyperEdgeId == null)
                throw new ArgumentException("myObject is not of type HyperEdgeId!");

            return CompareTo(myHyperEdgeId);

        }

        #endregion

        #region CompareTo(myHyperEdgeId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myHyperEdgeId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(HyperEdgeId myHyperEdgeId)
        {

            // Check if myHyperEdgeId is null
            if (myHyperEdgeId == null)
                throw new ArgumentNullException("myHyperEdgeId must not be null!");

            return _ElementId.CompareTo(myHyperEdgeId._ElementId);

        }

        #endregion
        
        #endregion

        #region IEquatable<HyperEdgeId> Members

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

            // Check if myObject can be cast to HyperEdgeId
            var myHyperEdgeId = myObject as HyperEdgeId;
            if ((Object) myHyperEdgeId == null)
                throw new ArgumentException("Parameter myObject could not be casted to type HyperEdgeId!");

            return this.Equals(myHyperEdgeId);

        }

        #endregion

        #region Equals(myHyperEdgeId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myHyperEdgeId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(HyperEdgeId myHyperEdgeId)
        {

            // Check if myHyperEdgeId is null
            if (myHyperEdgeId == null)
                throw new ArgumentNullException("Parameter myHyperEdgeId must not be null!");

            return _ElementId.Equals(myHyperEdgeId._ElementId);

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
