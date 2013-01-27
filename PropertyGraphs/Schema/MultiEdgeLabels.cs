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

namespace de.ahzf.Vanaheimr.Blueprints.Schema
{

    /// <summary>
    /// MultiEdge semantics.
    /// </summary>
    public class MultiEdgeLabel : IEquatable<MultiEdgeLabel>, IComparable<MultiEdgeLabel>, IComparable
    {

        #region Properties

        /// <summary>
        /// The internal name of the MultiEdgeLabel.
        /// </summary>
        public String Name { get; private set; }

        #endregion

        #region MultiEdgeLabel(Name)

        /// <summary>
        /// Create a new MultiEdgeLabel object.
        /// </summary>
        /// <param name="Name"></param>
        public MultiEdgeLabel(String Name)
        {
            this.Name = Name;
        }

        #endregion


        public static MultiEdgeLabel DEFAULT        = new MultiEdgeLabel("DEFAULT");


        #region ParseString(MultiEdgeLabelAsString)

        /// <summary>
        /// Tries to find the appropriate MultiEdgeLabel for the given string representation.
        /// </summary>
        /// <param name="MultiEdgeLabelAsString">A string representation of a MultiEdgeLabel.</param>
        /// <returns>A MultiEdgeLabel</returns>
        public static MultiEdgeLabel ParseString(String MultiEdgeLabelAsString)
        {

            return (from   _FieldInfo in typeof(MultiEdgeLabel).GetFields()
                    let    __MultiEdgeLabel = _FieldInfo.GetValue(null) as MultiEdgeLabel
                    where  __MultiEdgeLabel != null
                    where  __MultiEdgeLabel.Name == MultiEdgeLabelAsString
                    select __MultiEdgeLabel).FirstOrDefault();

        }

        #endregion

        #region TryParseString(MultiEdgeLabelAsString, out MultiEdgeLabel)

        /// <summary>
        /// Tries to find the appropriate MultiEdgeLabel for the given string representation.
        /// </summary>
        /// <param name="MultiEdgeLabelAsString">A string representation of a MultiEdgeLabel.</param>
        /// <param name="MultiEdgeLabel">The parsed MultiEdgeLabel.</param>
        /// <returns>true or false</returns>
        public static Boolean TryParseString(String MultiEdgeLabelAsString, out MultiEdgeLabel MultiEdgeLabel)
        {

            MultiEdgeLabel = (from   _FieldInfo in typeof(MultiEdgeLabel).GetFields()
                              let    __MultiEdgeLabel = _FieldInfo.GetValue(null) as MultiEdgeLabel
                              where  __MultiEdgeLabel != null
                              where  __MultiEdgeLabel.Name == MultiEdgeLabelAsString
                              select __MultiEdgeLabel).FirstOrDefault();

            return (MultiEdgeLabel != null) ? true : false;

        }

        #endregion


        #region IComparable<MultiEdgeLabel> Members

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

            // Check if myObject can be casted to a MultiEdgeLabel object
            var _MultiEdgeLabel = Object as MultiEdgeLabel;
            if ((Object) _MultiEdgeLabel == null)
                throw new ArgumentException("The given object is a MultiEdgeLabel object!");

            return CompareTo(_MultiEdgeLabel);

        }

        #endregion

        #region CompareTo(MultiEdgeLabel)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="MultiEdgeLabel">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(MultiEdgeLabel MultiEdgeLabel)
        {

            if ((Object) MultiEdgeLabel == null)
                throw new ArgumentNullException("The given MultiEdgeLabel must not be null!");

            return Name.CompareTo(MultiEdgeLabel.Name);

        }

        #endregion

        #endregion

        #region IEquatable<MultiEdgeLabel> Members

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

            // Check if myObject can be cast to MultiEdgeLabel
            var _MultiEdgeLabel = Object as MultiEdgeLabel;
            if ((Object) _MultiEdgeLabel == null)
                throw new ArgumentException("The given object is not a MultiEdgeLabel object!");

            return this.Equals(_MultiEdgeLabel);

        }

        #endregion

        #region Equals(MultiEdgeLabel)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="MultiEdgeLabel">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(MultiEdgeLabel MultiEdgeLabel)
        {

            if ((Object) MultiEdgeLabel == null)
                throw new ArgumentNullException("The given MultiEdgeLabel must not be null!");

            return Name.Equals(MultiEdgeLabel.Name);

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
