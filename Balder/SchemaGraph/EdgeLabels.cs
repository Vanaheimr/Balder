/*
 * Copyright (c) 2010-2014, Achim 'ahzf' Friedland <achim@graphdefined.org>
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
    /// Edge semantics.
    /// </summary>
    public class EdgeLabel : IEquatable<EdgeLabel>, IComparable<EdgeLabel>, IComparable
    {

        #region Properties

        /// <summary>
        /// The internal name of the EdgeLabel.
        /// </summary>
        public String Name { get; private set; }

        #endregion

        #region EdgeLabel(Name)

        /// <summary>
        /// Create a new EdgeLabel object.
        /// </summary>
        /// <param name="Name"></param>
        public EdgeLabel(String Name)
        {
            this.Name = Name;
        }

        #endregion


        public static EdgeLabel DEFAULT          = new EdgeLabel("DEFAULT");
        public static EdgeLabel IsConnectedWith  = new EdgeLabel("IsConnectedWith");


        #region ParseString(EdgeLabelAsString)

        /// <summary>
        /// Tries to find the appropriate EdgeLabel for the given string representation.
        /// </summary>
        /// <param name="EdgeLabelAsString">A string representation of an EdgeLabel.</param>
        /// <returns>An EdgeLabel</returns>
        public static EdgeLabel ParseString(String EdgeLabelAsString)
        {

            return (from   _FieldInfo in typeof(EdgeLabel).GetFields()
                    let    __EdgeLabel = _FieldInfo.GetValue(null) as EdgeLabel
                    where  __EdgeLabel != null
                    where  __EdgeLabel.Name == EdgeLabelAsString
                    select __EdgeLabel).FirstOrDefault();

        }

        #endregion

        #region TryParseString(EdgeLabelAsString, out EdgeLabel)

        /// <summary>
        /// Tries to find the appropriate EdgeLabel for the given string representation.
        /// </summary>
        /// <param name="EdgeLabelAsString">A string representation of an EdgeLabel.</param>
        /// <param name="EdgeLabel">The parsed EdgeLabel.</param>
        /// <returns>true or false</returns>
        public static Boolean TryParseString(String EdgeLabelAsString, out EdgeLabel EdgeLabel)
        {

            EdgeLabel = (from   _FieldInfo in typeof(EdgeLabel).GetFields()
                         let    __EdgeLabel = _FieldInfo.GetValue(null) as EdgeLabel
                         where  __EdgeLabel != null
                         where  __EdgeLabel.Name == EdgeLabelAsString
                         select __EdgeLabel).FirstOrDefault();

            return (EdgeLabel != null) ? true : false;

        }

        #endregion


        #region IComparable<EdgeLabel> Members

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

            // Check if myObject can be casted to a EdgeLabel object
            var _EdgeLabel = Object as EdgeLabel;
            if ((Object) _EdgeLabel == null)
                throw new ArgumentException("The given object is a EdgeLabel object!");

            return CompareTo(_EdgeLabel);

        }

        #endregion

        #region CompareTo(EdgeLabel)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EdgeLabel">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(EdgeLabel EdgeLabel)
        {

            if ((Object) EdgeLabel == null)
                throw new ArgumentNullException("The given EdgeLabel must not be null!");

            return Name.CompareTo(EdgeLabel.Name);

        }

        #endregion

        #endregion

        #region IEquatable<EdgeLabel> Members

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

            // Check if myObject can be cast to EdgeLabel
            var _EdgeLabel = Object as EdgeLabel;
            if ((Object) _EdgeLabel == null)
                throw new ArgumentException("The given object is not a EdgeLabel object!");

            return this.Equals(_EdgeLabel);

        }

        #endregion

        #region Equals(EdgeLabel)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EdgeLabel">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(EdgeLabel EdgeLabel)
        {

            if ((Object) EdgeLabel == null)
                throw new ArgumentNullException("The given EdgeLabel must not be null!");

            return Name.Equals(EdgeLabel.Name);

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
