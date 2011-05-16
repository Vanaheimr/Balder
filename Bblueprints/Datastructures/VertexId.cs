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
    /// A VertexId is unique identificator for a vertex.
    /// </summary>    
    public class VertexId : ElementId, IEquatable<VertexId>, IComparable<VertexId>, IComparable
    {

        #region Constructor(s)

        #region VertexId()

        /// <summary>
        /// Generates a new VertexId
        /// </summary>
        public VertexId()
            : base()
        {
        }

        #endregion

        #region VertexId(myInt32)

        /// <summary>
        /// Generates a VertexId based on the content of an Int32
        /// </summary>
        public VertexId(Int32 myInt32)
            : base(myInt32)
        {
        }

        #endregion

        #region VertexId(myUInt32)

        /// <summary>
        /// Generates a VertexId based on the content of an UInt32
        /// </summary>
        public VertexId(UInt32 myUInt32)
            : base(myUInt32)
        {
        }

        #endregion

        #region VertexId(myInt64)

        /// <summary>
        /// Generates a VertexId based on the content of an Int64
        /// </summary>
        public VertexId(Int64 myInt64)
            : base(myInt64)
        {
        }

        #endregion

        #region VertexId(myUInt64)

        /// <summary>
        /// Generates a VertexId based on the content of an UInt64
        /// </summary>
        public VertexId(UInt64 myUInt64)
            : base(myUInt64)
        {
        }

        #endregion

        #region VertexId(myString)

        /// <summary>
        /// Generates a VertexId based on the content of myString.
        /// </summary>
        public VertexId(String myString)
            : base(myString)
        {
        }

        #endregion

        #region VertexId(myUri)

        /// <summary>
        /// Generates a VertexId based on the content of myUri.
        /// </summary>
        public VertexId(Uri myUri)
            : base(myUri)
        {
        }

        #endregion

        #region VertexId(myVertexId)

        /// <summary>
        /// Generates a VertexId based on the content of myVertexId
        /// </summary>
        /// <param name="myVertexId">A VertexId</param>
        public VertexId(VertexId myVertexId)
            : base(myVertexId)
        {
        }

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

        #region Operator == (myVertexId1, myVertexId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myVertexId1">A VertexId.</param>
        /// <param name="myVertexId2">Another VertexId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (VertexId myVertexId1, VertexId myVertexId2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(myVertexId1, myVertexId2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) myVertexId1 == null) || ((Object) myVertexId2 == null))
                return false;

            return myVertexId1.Equals(myVertexId2);

        }

        #endregion

        #region Operator != (myVertexId1, myVertexId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myVertexId1">A VertexId.</param>
        /// <param name="myVertexId2">Another VertexId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (VertexId myVertexId1, VertexId myVertexId2)
        {
            return !(myVertexId1 == myVertexId2);
        }

        #endregion

        #region Operator <  (myVertexId1, myVertexId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myVertexId1">A VertexId.</param>
        /// <param name="myVertexId2">Another VertexId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (VertexId myVertexId1, VertexId myVertexId2)
        {

            // Check if myVertexId1 is null
            if ((Object) myVertexId1 == null)
                throw new ArgumentNullException("Parameter myVertexId1 must not be null!");

            // Check if myVertexId2 is null
            if ((Object) myVertexId2 == null)
                throw new ArgumentNullException("Parameter myVertexId2 must not be null!");


            // Check the length of the VertexIds
            if (myVertexId1.Length < myVertexId2.Length)
                return true;

            if (myVertexId1.Length > myVertexId2.Length)
                return false;

            return myVertexId1.CompareTo(myVertexId2) < 0;

        }

        #endregion

        #region Operator >  (myVertexId1, myVertexId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myVertexId1">A VertexId.</param>
        /// <param name="myVertexId2">Another VertexId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (VertexId myVertexId1, VertexId myVertexId2)
        {

            // Check if myVertexId1 is null
            if ((Object) myVertexId1 == null)
                throw new ArgumentNullException("Parameter myVertexId1 must not be null!");

            // Check if myVertexId2 is null
            if ((Object) myVertexId2 == null)
                throw new ArgumentNullException("Parameter myVertexId2 must not be null!");


            // Check the length of the VertexIds
            if (myVertexId1.Length > myVertexId2.Length)
                return true;

            if (myVertexId1.Length < myVertexId2.Length)
                return false;

            return myVertexId1.CompareTo(myVertexId2) > 0;

        }

        #endregion

        #region Operator <= (myVertexId1, myVertexId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myVertexId1">A VertexId.</param>
        /// <param name="myVertexId2">Another VertexId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (VertexId myVertexId1, VertexId myVertexId2)
        {
            return !(myVertexId1 > myVertexId2);
        }

        #endregion

        #region Operator >= (myVertexId1, myVertexId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myVertexId1">A VertexId.</param>
        /// <param name="myVertexId2">Another VertexId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (VertexId myVertexId1, VertexId myVertexId2)
        {
            return !(myVertexId1 < myVertexId2);
        }

        #endregion

        #endregion

        #region IComparable<VertexId> Members

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

            // Check if myObject can be casted to an VertexId object
            var myVertexId = myObject as VertexId;
            if ((Object) myVertexId == null)
                throw new ArgumentException("myObject is not of type VertexId!");

            return CompareTo(myVertexId);

        }

        #endregion

        #region CompareTo(myVertexId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myVertexId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(VertexId myVertexId)
        {

            // Check if myVertexId is null
            if (myVertexId == null)
                throw new ArgumentNullException("myVertexId must not be null!");

            return _ElementId.CompareTo(myVertexId._ElementId);

        }

        #endregion

        #endregion

        #region IEquatable<VertexId> Members

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

            // Check if myObject can be cast to VertexId
            var myVertexId = myObject as VertexId;
            if ((Object) myVertexId == null)
                throw new ArgumentException("Parameter myObject could not be casted to type VertexId!");

            return this.Equals(myVertexId);

        }

        #endregion

        #region Equals(myVertexId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myVertexId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(VertexId myVertexId)
        {

            // Check if myVertexId is null
            if (myVertexId == null)
                throw new ArgumentNullException("Parameter myVertexId must not be null!");

            return _ElementId.Equals(myVertexId._ElementId);

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
