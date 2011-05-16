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
    /// A Id is unique identificator.
    /// </summary>    
    public class ElementId : IEquatable<ElementId>, IComparable<ElementId>, IComparable
    {

        #region Data

        /// <summary>
        /// Holding the identification of this element.
        /// </summary>
        protected readonly String _ElementId;

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
            _ElementId = myString.Trim();
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


        #region IComparable<ElementId> Members

        #region CompareTo(myObject)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myObject">An object to compare with.</param>
        /// <returns>true|false</returns>
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

        #region CompareTo(myElementId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myElementId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(ElementId myElementId)
        {

            // Check if myElementId is null
            if (myElementId == null)
                throw new ArgumentNullException("myElementId must not be null!");

            return _ElementId.CompareTo(myElementId._ElementId);

        }

        #endregion

        #endregion

        #region IEquatable<ElementId> Members

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

            // Check if myObject can be cast to ElementId
            var myElementId = myObject as ElementId;
            if ((Object) myElementId == null)
                throw new ArgumentException("Parameter myObject could not be casted to type ElementId!");

            return this.Equals(myElementId);

        }

        #endregion

        #region Equals(myElementId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myElementId">An object to compare with.</param>
        /// <returns>true|false</returns>
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

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override Int32 GetHashCode()
        {
            return _ElementId.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        public override String ToString()
        {
            return _ElementId.ToString();
        }

        #endregion

    }

}
