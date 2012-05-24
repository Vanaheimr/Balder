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
    /// A GraphElementId is unique identificator for any graph element.
    /// It's the base for the VertexId, EdgeId, MultiEdgeId and HyperEdgeId.
    /// </summary>    
    public abstract class AGraphElementId : IEquatable<AGraphElementId>,
                                            IComparable<AGraphElementId>,
                                            IComparable

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

        #region GraphElementId()

        /// <summary>
        /// Generates a new GraphElementId based on a GUID.
        /// </summary>
        public AGraphElementId()
        {
            _Id = Guid.NewGuid().ToString();
        }

        #endregion

        #region GraphElementId(Int32)

        /// <summary>
        /// Generates a GraphElementId based on the content of an Int32.
        /// </summary>
        public AGraphElementId(Int32 Int32)
            : this(Math.Abs(Int32).ToString())
        { }

        #endregion

        #region GraphElementId(UInt32)

        /// <summary>
        /// Generates a GraphElementId based on the content of an UInt32.
        /// </summary>
        public AGraphElementId(UInt32 UInt32)
            : this(UInt32.ToString())
        { }

        #endregion

        #region GraphElementId(Int64)

        /// <summary>
        /// Generates a GraphElementId based on the content of an Int64.
        /// </summary>
        public AGraphElementId(Int64 Int64)
            : this(Int64.ToString())
        { }

        #endregion

        #region GraphElementId(UInt64)

        /// <summary>
        /// Generates a GraphElementId based on the content of an UInt64.
        /// </summary>
        public AGraphElementId(UInt64 UInt64)
            : this(UInt64.ToString())
        { }

        #endregion

        #region GraphElementId(String)

        /// <summary>
        /// Generates a GraphElementId based on the content of String.
        /// </summary>
        public AGraphElementId(String String)
        {
            _Id = String.Trim();
        }

        #endregion

        #region GraphElementId(Uri)

        /// <summary>
        /// Generates a GraphElementId based on the content of Uri.
        /// </summary>
        public AGraphElementId(Uri Uri)
        {
            _Id = Uri.ToString();
        }

        #endregion

        #region GraphElementId(GraphElementId)

        /// <summary>
        /// Generates a GraphElementId based on the content of GraphElementId.
        /// </summary>
        /// <param name="GraphElementId">A GraphElementId</param>
        public AGraphElementId(AGraphElementId GraphElementId)
        {
            _Id = GraphElementId.ToString();
        }

        #endregion

        #endregion


        #region IComparable<GraphElementId> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public virtual Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given object must not be null!");

            // Check if the given object is an GraphElementId.
            var GraphElementId = Object as AGraphElementId;
            if ((Object) GraphElementId == null)
                throw new ArgumentException("The given object is not a AGraphElementId!");

            return CompareTo(GraphElementId);

        }

        #endregion

        #region CompareTo(AGraphElementId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="GraphElementId">An object to compare with.</param>
        public Int32 CompareTo(AGraphElementId GraphElementId)
        {

            if ((Object) GraphElementId == null)
                throw new ArgumentNullException("The given AGraphElementId must not be null!");

            // Compare the length of the ElementIds
            var _Result = this.Length.CompareTo(GraphElementId.Length);

            // If equal: Compare Ids
            if (_Result == 0)
                _Result = _Id.CompareTo(GraphElementId._Id);

            return _Result;

        }

        #endregion

        #endregion

        #region IEquatable<GraphElementId> Members

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

            // Check if the given object is an AGraphElementId.
            var GraphElementId = Object as AGraphElementId;
            if ((Object) GraphElementId == null)
                return false;

            return this.Equals(GraphElementId);

        }

        #endregion

        #region Equals(GraphElementId)

        /// <summary>
        /// Compares two GraphElementIds for equality.
        /// </summary>
        /// <param name="GraphElementId">A GraphElementId to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(AGraphElementId GraphElementId)
        {

            if ((Object) GraphElementId == null)
                return false;

            return _Id.Equals(GraphElementId._Id);

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
