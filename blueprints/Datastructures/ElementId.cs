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
    /// A Id is unique identificator.
    /// </summary>    
    public class ElementId
    {

        #region Data

        protected readonly String _ElementId;

        #endregion

        #region Properties

        #region Length

        public UInt64 Length
        {
            get
            {
                return (UInt64) _ElementId.Length;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region ElementId()

        /// <summary>
        /// Generates a new ElementId based on a GUID
        /// </summary>
        public ElementId()
        {
            _ElementId = Guid.NewGuid().ToString();
        }

        #endregion

        #region ElementId(myInt32)

        /// <summary>
        /// Generates a ElementId based on the content of an Int32
        /// </summary>
        public ElementId(Int32 myInt32)
            : this(Math.Abs(myInt32).ToString())
        {
        }

        #endregion

        #region ElementId(myUInt32)

        /// <summary>
        /// Generates a ElementId based on the content of an UInt32
        /// </summary>
        public ElementId(UInt32 myUInt32)
            : this(myUInt32.ToString())
        {
        }

        #endregion

        #region ElementId(myInt64)

        /// <summary>
        /// Generates a ElementId based on the content of an Int64
        /// </summary>
        public ElementId(Int64 myInt64)
            : this(myInt64.ToString())
        {
        }

        #endregion

        #region ElementId(myUInt64)

        /// <summary>
        /// Generates a ElementId based on the content of an UInt64
        /// </summary>
        public ElementId(UInt64 myUInt64)
            : this(myUInt64.ToString())
        {
        }

        #endregion

        #region ElementId(myString)

        /// <summary>
        /// Generates a ElementId based on the content of myString.
        /// </summary>
        public ElementId(String myString)
        {
            _ElementId = myString;
        }

        #endregion

        #region ElementId(myUri)

        /// <summary>
        /// Generates a ElementId based on the content of myUri.
        /// </summary>
        public ElementId(Uri myUri)
        {
            _ElementId = myUri.ToString();
        }

        #endregion

        #region ElementId(myElementId)

        /// <summary>
        /// Generates a ElementId based on the content of myElementId
        /// </summary>
        /// <param name="myElementId">A ElementId</param>
        public ElementId(ElementId myElementId)
        {
            _ElementId = myElementId.ToString();
        }

        #endregion

        #endregion


        #region IComparable Members

        public Int32 CompareTo(Object myObject)
        {

            // Check if myObject is null
            if (myObject == null)
                throw new ArgumentNullException("myObject must not be null!");

            // Check if myObject can be casted to an ElementId object
            var myElementId = myObject as ElementId;
            if ((Object) myElementId == null)
                throw new ArgumentException("myObject is not of type ElementId!");

            return CompareTo(myElementId);

        }

        #endregion

        #region IComparable<ElementId> Members

        public Int32 CompareTo(ElementId myElementId)
        {

            // Check if myElementId is null
            if (myElementId == null)
                throw new ArgumentNullException("myElementId must not be null!");

            return _ElementId.CompareTo(myElementId._ElementId);

        }

        #endregion

        #region IEquatable<ElementId> Members

        #region Equals(myObject)

        public override Boolean Equals(Object myObject)
        {

            // Check if myObject is null
            if (myObject == null)
                throw new ArgumentNullException("Parameter myObject must not be null!");

            // Check if myObject can be cast to ElementId
            var myElementId = myObject as ElementId;
            if ((Object) myElementId == null)
                throw new ArgumentException("Parameter myObject could not be casted to type ElementId!");

            return this.Equals(myElementId);

        }

        #endregion

        #region Equals(myElementId)

        public Boolean Equals(ElementId myElementId)
        {

            // Check if myElementId is null
            if (myElementId == null)
                throw new ArgumentNullException("Parameter myElementId must not be null!");

            return _ElementId.Equals(myElementId._ElementId);

        }

        #endregion

        #endregion

        #region GetHashCode()

        public override Int32 GetHashCode()
        {
            return _ElementId.GetHashCode();
        }

        #endregion

        #region ToString()

        public override String ToString()
        {
            return _ElementId.ToString();
        }

        #endregion

    }

}
