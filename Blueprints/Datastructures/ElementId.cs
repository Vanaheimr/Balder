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
    /// A ElementId is unique identificator for any graph element.
    /// It's the base for the VertexId, EdgeId and HyperEdgeId.
    /// </summary>    
    public abstract class ElementId : IEquatable<ElementId>, IComparable<ElementId>, IComparable
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

        #region ElementId()

        /// <summary>
        /// Generates a new ElementId based on a GUID.
        /// </summary>
        public ElementId()
        {
            _Id = Guid.NewGuid().ToString();
        }

        #endregion

        #region ElementId(Int32)

        /// <summary>
        /// Generates a ElementId based on the content of an Int32.
        /// </summary>
        public ElementId(Int32 Int32)
            : this(Math.Abs(Int32).ToString())
        { }

        #endregion

        #region ElementId(UInt32)

        /// <summary>
        /// Generates a ElementId based on the content of an UInt32.
        /// </summary>
        public ElementId(UInt32 UInt32)
            : this(UInt32.ToString())
        { }

        #endregion

        #region ElementId(Int64)

        /// <summary>
        /// Generates a ElementId based on the content of an Int64.
        /// </summary>
        public ElementId(Int64 Int64)
            : this(Int64.ToString())
        { }

        #endregion

        #region ElementId(UInt64)

        /// <summary>
        /// Generates a ElementId based on the content of an UInt64.
        /// </summary>
        public ElementId(UInt64 UInt64)
            : this(UInt64.ToString())
        { }

        #endregion

        #region ElementId(String)

        /// <summary>
        /// Generates a ElementId based on the content of String.
        /// </summary>
        public ElementId(String String)
        {
            _Id = String.Trim();
        }

        #endregion

        #region ElementId(Uri)

        /// <summary>
        /// Generates a ElementId based on the content of Uri.
        /// </summary>
        public ElementId(Uri Uri)
        {
            _Id = Uri.ToString();
        }

        #endregion

        #region ElementId(ElementId)

        /// <summary>
        /// Generates a ElementId based on the content of ElementId.
        /// </summary>
        /// <param name="ElementId">A ElementId</param>
        public ElementId(ElementId ElementId)
        {
            _Id = ElementId.ToString();
        }

        #endregion

        #endregion


        #region IComparable<ElementId> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public virtual Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given object must not be null!");

            // Check if the given object is an ElementId.
            var ElementId = Object as ElementId;
            if ((Object) ElementId == null)
                throw new ArgumentException("The given object is not a ElementId!");

            return CompareTo(ElementId);

        }

        #endregion

        #region CompareTo(ElementId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ElementId">An object to compare with.</param>
        public Int32 CompareTo(ElementId ElementId)
        {

            if ((Object) ElementId == null)
                throw new ArgumentNullException("The given ElementId must not be null!");

            // Compare the length of the ElementIds
            var _Result = this.Length.CompareTo(ElementId.Length);

            // If equal: Compare Ids
            if (_Result == 0)
                _Result = _Id.CompareTo(ElementId._Id);

            return _Result;

        }

        #endregion

        #endregion

        #region IEquatable<ElementId> Members

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

            // Check if the given object is an ElementId.
            var ElementId = Object as ElementId;
            if ((Object) ElementId == null)
                return false;

            return this.Equals(ElementId);

        }

        #endregion

        #region Equals(ElementId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ElementId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(ElementId ElementId)
        {

            if ((Object) ElementId == null)
                return false;

            return _Id.Equals(ElementId._Id);

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
