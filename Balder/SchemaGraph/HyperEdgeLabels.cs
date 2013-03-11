/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Balder <http://www.github.com/Vanaheimr/Balder>
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

namespace eu.Vanaheimr.Balder.Schema
{

    /// <summary>
    /// HyperEdge semantics.
    /// </summary>
    public class HyperEdgeLabel : IEquatable<HyperEdgeLabel>, IComparable<HyperEdgeLabel>, IComparable
    {

        #region Properties

        /// <summary>
        /// The internal name of the HyperEdgeLabel.
        /// </summary>
        public String Name { get; private set; }

        #endregion

        #region HyperEdgeLabel(Name)

        /// <summary>
        /// Create a new HyperEdgeLabel object.
        /// </summary>
        /// <param name="Name"></param>
        public HyperEdgeLabel(String Name)
        {
            this.Name = Name;
        }

        #endregion


        public static HyperEdgeLabel DEFAULT        = new HyperEdgeLabel("DEFAULT");


        #region ParseString(HyperEdgeLabelAsString)

        /// <summary>
        /// Tries to find the appropriate HyperEdgeLabel for the given string representation.
        /// </summary>
        /// <param name="HyperEdgeLabelAsString">A string representation of a HyperEdgeLabel.</param>
        /// <returns>A HyperEdgeLabel</returns>
        public static HyperEdgeLabel ParseString(String HyperEdgeLabelAsString)
        {

            return (from   _FieldInfo in typeof(HyperEdgeLabel).GetFields()
                    let    __HyperEdgeLabel = _FieldInfo.GetValue(null) as HyperEdgeLabel
                    where  __HyperEdgeLabel != null
                    where  __HyperEdgeLabel.Name == HyperEdgeLabelAsString
                    select __HyperEdgeLabel).FirstOrDefault();

        }

        #endregion

        #region TryParseString(HyperEdgeLabelAsString, out HyperEdgeLabel)

        /// <summary>
        /// Tries to find the appropriate HyperEdgeLabel for the given string representation.
        /// </summary>
        /// <param name="HyperEdgeLabelAsString">A string representation of a HyperEdgeLabel.</param>
        /// <param name="HyperEdgeLabel">The parsed HyperEdgeLabel.</param>
        /// <returns>true or false</returns>
        public static Boolean TryParseString(String HyperEdgeLabelAsString, out HyperEdgeLabel HyperEdgeLabel)
        {

            HyperEdgeLabel = (from   _FieldInfo in typeof(HyperEdgeLabel).GetFields()
                              let    __HyperEdgeLabel = _FieldInfo.GetValue(null) as HyperEdgeLabel
                              where  __HyperEdgeLabel != null
                              where  __HyperEdgeLabel.Name == HyperEdgeLabelAsString
                              select __HyperEdgeLabel).FirstOrDefault();

            return (HyperEdgeLabel != null) ? true : false;

        }

        #endregion


        #region IComparable<HyperEdgeLabel> Members

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

            // Check if myObject can be casted to a HyperEdgeLabel object
            var _HyperEdgeLabel = Object as HyperEdgeLabel;
            if ((Object) _HyperEdgeLabel == null)
                throw new ArgumentException("The given object is a HyperEdgeLabel object!");

            return CompareTo(_HyperEdgeLabel);

        }

        #endregion

        #region CompareTo(HyperEdgeLabel)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HyperEdgeLabel">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(HyperEdgeLabel HyperEdgeLabel)
        {

            if ((Object) HyperEdgeLabel == null)
                throw new ArgumentNullException("The given HyperEdgeLabel must not be null!");

            return Name.CompareTo(HyperEdgeLabel.Name);

        }

        #endregion

        #endregion

        #region IEquatable<HyperEdgeLabel> Members

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

            // Check if myObject can be cast to HyperEdgeLabel
            var _HyperEdgeLabel = Object as HyperEdgeLabel;
            if ((Object) _HyperEdgeLabel == null)
                throw new ArgumentException("The given object is not a HyperEdgeLabel object!");

            return this.Equals(_HyperEdgeLabel);

        }

        #endregion

        #region Equals(HyperEdgeLabel)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="HyperEdgeLabel">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(HyperEdgeLabel HyperEdgeLabel)
        {

            if ((Object) HyperEdgeLabel == null)
                throw new ArgumentNullException("The given HyperEdgeLabel must not be null!");

            return Name.Equals(HyperEdgeLabel.Name);

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
