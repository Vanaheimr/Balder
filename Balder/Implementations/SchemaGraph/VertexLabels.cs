/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
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
using System.Linq;

#endregion

namespace eu.Vanaheimr.Blueprints.Schema
{

    /// <summary>
    /// A vertex label.
    /// </summary>
    public class VertexLabel : IEquatable<VertexLabel>, IComparable<VertexLabel>, IComparable
    {

        #region Properties

        /// <summary>
        /// The internal name of the vertex label.
        /// </summary>
        public String Name { get; private set; }

        #endregion

        #region VertexLabel(Name)

        /// <summary>
        /// Create a new vertex label.
        /// </summary>
        /// <param name="Name"></param>
        public VertexLabel(String Name)
        {
            this.Name = Name;
        }

        #endregion


        public static VertexLabel DEFAULT           = new VertexLabel("DEFAULT");
        public static VertexLabel Vertex            = new VertexLabel("Vertex");
        public static VertexLabel Edge              = new VertexLabel("Edge");
        public static VertexLabel MultiEdge         = new VertexLabel("MultiEdge");
        public static VertexLabel HyperEdge         = new VertexLabel("HyperEdge");


        #region ParseString(VertexLabelAsString)

        /// <summary>
        /// Tries to find the appropriate VertexLabel for the given string representation.
        /// </summary>
        /// <param name="VertexLabelAsString">A string representation of a VertexLabel.</param>
        /// <returns>A VertexLabel</returns>
        public static VertexLabel ParseString(String VertexLabelAsString)
        {

            return (from   _FieldInfo in typeof(VertexLabel).GetFields()
                    let    __VertexLabel = _FieldInfo.GetValue(null) as VertexLabel
                    where  __VertexLabel != null
                    where  __VertexLabel.Name == VertexLabelAsString
                    select __VertexLabel).FirstOrDefault();

        }

        #endregion

        #region TryParseString(VertexLabelAsString, out VertexLabel)

        /// <summary>
        /// Tries to find the appropriate VertexLabel for the given string representation.
        /// </summary>
        /// <param name="VertexLabelAsString">A string representation of a VertexLabel.</param>
        /// <param name="VertexLabel">The parsed VertexLabel.</param>
        /// <returns>true or false</returns>
        public static Boolean TryParseString(String VertexLabelAsString, out VertexLabel VertexLabel)
        {

            VertexLabel = (from   _FieldInfo in typeof(VertexLabel).GetFields()
                            let    __VertexLabel = _FieldInfo.GetValue(null) as VertexLabel
                            where  __VertexLabel != null
                            where  __VertexLabel.Name == VertexLabelAsString
                            select __VertexLabel).FirstOrDefault();

            return (VertexLabel != null) ? true : false;

        }

        #endregion


        #region IComparable<VertexLabel> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given Object must not be null!");

            // Check if myObject can be casted to a VertexLabel object
            var _VertexLabel = Object as VertexLabel;
            if ((Object) _VertexLabel == null)
                throw new ArgumentException("The given object is a VertexLabel object!");

            return CompareTo(_VertexLabel);

        }

        #endregion

        #region CompareTo(VertexLabel)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VertexLabel">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(VertexLabel VertexLabel)
        {

            if ((Object) VertexLabel == null)
                throw new ArgumentNullException("The given VertexLabel must not be null!");

            return Name.CompareTo(VertexLabel.Name);

        }

        #endregion

        #endregion

        #region IEquatable<VertexLabel> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>true|false</returns>
        public override Boolean Equals(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given Object must not be null!");

            // Check if myObject can be cast to VertexLabel
            var _VertexLabel = Object as VertexLabel;
            if ((Object) _VertexLabel == null)
                throw new ArgumentException("The given object is not a VertexLabel object!");

            return this.Equals(_VertexLabel);

        }

        #endregion

        #region Equals(VertexLabel)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VertexLabel">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(VertexLabel VertexLabel)
        {

            if ((Object) VertexLabel == null)
                throw new ArgumentNullException("The given VertexLabel must not be null!");

            return Name.Equals(VertexLabel.Name);

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
            return Name.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        public override String ToString()
        {
            return Name;
        }

        #endregion

    }

}
