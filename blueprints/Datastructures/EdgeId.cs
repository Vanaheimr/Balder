/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

#endregion

namespace de.ahzf.blueprints.Datastructures
{

    /// <summary>
    /// A EdgeId is unique identificator for edge.
    /// </summary>
    public class EdgeId : ElementId, IComparable, IComparable<EdgeId>, IEquatable<EdgeId>
    {


        #region Constructor(s)

        #region EdgeId()

        /// <summary>
        /// Generates a new EdgeId
        /// </summary>
        public EdgeId()
            : base()
        {
        }

        #endregion

        #region EdgeId(myInt32)

        /// <summary>
        /// Generates a EdgeId based on the content of an Int32
        /// </summary>
        public EdgeId(Int32 myInt32)
            : base(myInt32)
        {
        }

        #endregion

        #region EdgeId(myUInt32)

        /// <summary>
        /// Generates a EdgeId based on the content of an UInt32
        /// </summary>
        public EdgeId(UInt32 myUInt32)
            : base(myUInt32)
        {
        }

        #endregion

        #region EdgeId(myInt64)

        /// <summary>
        /// Generates a EdgeId based on the content of an Int64
        /// </summary>
        public EdgeId(Int64 myInt64)
            : base(myInt64)
        {
        }

        #endregion

        #region EdgeId(myUInt64)

        /// <summary>
        /// Generates a EdgeId based on the content of an UInt64
        /// </summary>
        public EdgeId(UInt64 myUInt64)
            : base(myUInt64)
        {
        }

        #endregion

        #region EdgeId(myString)

        /// <summary>
        /// Generates a EdgeId based on the content of myString.
        /// </summary>
        public EdgeId(String myString)
            : base(myString)
        {
        }

        #endregion

        #region EdgeId(myUri)

        /// <summary>
        /// Generates a EdgeId based on the content of myUri.
        /// </summary>
        public EdgeId(Uri myUri)
            : base(myUri)
        {
        }

        #endregion

        #region EdgeId(myEdgeId)

        /// <summary>
        /// Generates a EdgeId based on the content of myEdgeId
        /// </summary>
        /// <param name="myEdgeId">A EdgeId</param>
        public EdgeId(EdgeId myEdgeId)
            : base(myEdgeId)
        {
        }

        #endregion

        #endregion


        #region NewEdgeId

        public static EdgeId NewEdgeId
        {
            get
            {
                return new EdgeId(Guid.NewGuid().ToString());
            }
        }

        #endregion


        #region Operator overloading

        #region Operator == (myEdgeId1, myEdgeId2)

        public static Boolean operator == (EdgeId myEdgeId1, EdgeId myEdgeId2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(myEdgeId1, myEdgeId2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) myEdgeId1 == null) || ((Object) myEdgeId2 == null))
                return false;

            return myEdgeId1.Equals(myEdgeId2);

        }

        #endregion

        #region Operator != (myEdgeId1, myEdgeId2)

        public static Boolean operator != (EdgeId myEdgeId1, EdgeId myEdgeId2)
        {
            return !(myEdgeId1 == myEdgeId2);
        }

        #endregion

        #region Operator <  (myEdgeId1, myEdgeId2)

        public static Boolean operator < (EdgeId myEdgeId1, EdgeId myEdgeId2)
        {

            // Check if myEdgeId1 is null
            if ((Object) myEdgeId1 == null)
                throw new ArgumentNullException("Parameter myEdgeId1 must not be null!");

            // Check if myEdgeId2 is null
            if ((Object) myEdgeId2 == null)
                throw new ArgumentNullException("Parameter myEdgeId2 must not be null!");


            // Check the length of the EdgeIds
            if (myEdgeId1.Length < myEdgeId2.Length)
                return true;

            if (myEdgeId1.Length > myEdgeId2.Length)
                return false;

            return myEdgeId1.CompareTo(myEdgeId2) < 0;

        }

        #endregion

        #region Operator >  (myEdgeId1, myEdgeId2)

        public static Boolean operator > (EdgeId myEdgeId1, EdgeId myEdgeId2)
        {

            // Check if myEdgeId1 is null
            if ((Object) myEdgeId1 == null)
                throw new ArgumentNullException("Parameter myEdgeId1 must not be null!");

            // Check if myEdgeId2 is null
            if ((Object) myEdgeId2 == null)
                throw new ArgumentNullException("Parameter myEdgeId2 must not be null!");


            // Check the length of the EdgeIds
            if (myEdgeId1.Length > myEdgeId2.Length)
                return true;

            if (myEdgeId1.Length < myEdgeId2.Length)
                return false;

            return myEdgeId1.CompareTo(myEdgeId2) > 0;

        }

        #endregion

        #region Operator <= (myEdgeId1, myEdgeId2)

        public static Boolean operator <= (EdgeId myEdgeId1, EdgeId myEdgeId2)
        {
            return !(myEdgeId1 > myEdgeId2);
        }

        #endregion

        #region Operator >= (myEdgeId1, myEdgeId2)

        public static Boolean operator >= (EdgeId myEdgeId1, EdgeId myEdgeId2)
        {
            return !(myEdgeId1 < myEdgeId2);
        }

        #endregion

        #endregion


        #region IComparable Members

        public Int32 CompareTo(Object myObject)
        {

            // Check if myObject is null
            if (myObject == null)
                throw new ArgumentNullException("myObject must not be null!");

            // Check if myObject can be casted to an EdgeId object
            var myEdgeId = myObject as EdgeId;
            if ((Object) myEdgeId == null)
                throw new ArgumentException("myObject is not of type EdgeId!");

            return CompareTo(myEdgeId);

        }

        #endregion

        #region IComparable<EdgeId> Members

        public Int32 CompareTo(EdgeId myEdgeId)
        {

            // Check if myEdgeId is null
            if (myEdgeId == null)
                throw new ArgumentNullException("myEdgeId must not be null!");

            return _ElementId.CompareTo(myEdgeId._ElementId);

        }

        #endregion

        #region IEquatable<EdgeId> Members

        #region Equals(myObject)

        public override Boolean Equals(Object myObject)
        {

            // Check if myObject is null
            if (myObject == null)
                throw new ArgumentNullException("Parameter myObject must not be null!");

            // Check if myObject can be cast to EdgeId
            var myEdgeId = myObject as EdgeId;
            if ((Object) myEdgeId == null)
                throw new ArgumentException("Parameter myObject could not be casted to type EdgeId!");

            return this.Equals(myEdgeId);

        }

        #endregion

        #region Equals(myEdgeId)

        public Boolean Equals(EdgeId myEdgeId)
        {

            // Check if myEdgeId is null
            if (myEdgeId == null)
                throw new ArgumentNullException("Parameter myEdgeId must not be null!");

            return _ElementId.Equals(myEdgeId._ElementId);

        }

        #endregion

        #endregion

        #region GetHashCode()

        public override Int32 GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

    }

}
