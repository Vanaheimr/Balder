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
    /// A semantic property key combines a property key with semantic type information.
    /// </summary>    
    public class SemanticProperty : IEquatable<SemanticProperty>, IComparable<SemanticProperty>, IComparable
    {

        #region Properties

        /// <summary>
        /// The RDF prefix, e.g. "http://xmlns.com/foaf/0.1"
        /// </summary>
        public Uri     Prefix      { get; private set; }

        /// <summary>
        /// The RDF suffix, e.g. "knows"
        /// </summary>
        public String  Suffix      { get; private set; }
        
        /// <summary>
        /// The key for the given RDF statement within your domain.
        /// </summary>
        public String  Key         { get; private set; }
        
        /// <summary>
        /// An optional description for this RDF mapping.
        /// </summary>
        public String  Description { get; private set; }

        #endregion

        #region Constructor(s)

        #region SemanticPropertyKey(Key, Description)

        /// <summary>
        /// Creates a new semantic property key.
        /// </summary>
        /// <param name="Key">The key for the given RDF statement within your domain.</param>
        /// <param name="Description">An optional description for this RDF mapping.</param>
        public SemanticProperty(String Key, String Description = null)
        {
            this.Prefix      = new Uri("http://graph-database.org/gdb/0.1");
            this.Suffix      = Key;
            this.Key         = Key;
            this.Description = Description;
        }

        #endregion

        #region SemanticPropertyKey(Prefix, Type, Key, Description)

        /// <summary>
        /// Creates a new semantic property key.
        /// </summary>
        /// <param name="Prefix">The RDF prefix, e.g. "http://xmlns.com/foaf/0.1"</param>
        /// <param name="Suffix">The RDF suffix, e.g. "knows"</param>
        /// <param name="Key">The key for the given RDF statement within your domain, as the suffix may not be unique.</param>
        /// <param name="Description">An optional description for this RDF mapping.</param>
        public SemanticProperty(Uri Prefix, String Suffix, String Key, String Description = null)
        {
            this.Prefix      = Prefix;
            this.Suffix      = Suffix;
            this.Key         = Key;
            this.Description = Description;
        }

        #endregion

        #endregion


        #region Operator overloading

        #region Operator == (mySemanticPropertyKey1, mySemanticPropertyKey2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="mySemanticPropertyKey1">A SemanticPropertyKey.</param>
        /// <param name="mySemanticPropertyKey2">Another SemanticPropertyKey.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (SemanticProperty mySemanticPropertyKey1, SemanticProperty mySemanticPropertyKey2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(mySemanticPropertyKey1, mySemanticPropertyKey2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) mySemanticPropertyKey1 == null) || ((Object) mySemanticPropertyKey2 == null))
                return false;

            return mySemanticPropertyKey1.Key.Equals(mySemanticPropertyKey2.Key);

        }

        #endregion

        #region Operator != (mySemanticPropertyKey1, mySemanticPropertyKey2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="mySemanticPropertyKey1">A SemanticPropertyKey.</param>
        /// <param name="mySemanticPropertyKey2">Another SemanticPropertyKey.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (SemanticProperty mySemanticPropertyKey1, SemanticProperty mySemanticPropertyKey2)
        {
            return !(mySemanticPropertyKey1 == mySemanticPropertyKey2);
        }

        #endregion

        #region Operator <  (mySemanticPropertyKey1, mySemanticPropertyKey2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="mySemanticPropertyKey1">A SemanticPropertyKey.</param>
        /// <param name="mySemanticPropertyKey2">Another SemanticPropertyKey.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (SemanticProperty mySemanticPropertyKey1, SemanticProperty mySemanticPropertyKey2)
        {

            // Check if mySemanticPropertyKey1 is null
            if ((Object) mySemanticPropertyKey1 == null)
                throw new ArgumentNullException("Parameter mySemanticPropertyKey1 must not be null!");

            // Check if mySemanticPropertyKey2 is null
            if ((Object) mySemanticPropertyKey2 == null)
                throw new ArgumentNullException("Parameter mySemanticPropertyKey2 must not be null!");


            // Check the length of the SemanticPropertyKeys
            if (mySemanticPropertyKey1.Key.Length < mySemanticPropertyKey2.Key.Length)
                return true;

            if (mySemanticPropertyKey1.Key.Length > mySemanticPropertyKey2.Key.Length)
                return false;

            return mySemanticPropertyKey1.Key.CompareTo(mySemanticPropertyKey2.Key) < 0;

        }

        #endregion

        #region Operator >  (mySemanticPropertyKey1, mySemanticPropertyKey2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="mySemanticPropertyKey1">A SemanticPropertyKey.</param>
        /// <param name="mySemanticPropertyKey2">Another SemanticPropertyKey.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (SemanticProperty mySemanticPropertyKey1, SemanticProperty mySemanticPropertyKey2)
        {

            // Check if mySemanticPropertyKey1 is null
            if ((Object) mySemanticPropertyKey1 == null)
                throw new ArgumentNullException("Parameter mySemanticPropertyKey1 must not be null!");

            // Check if mySemanticPropertyKey2 is null
            if ((Object) mySemanticPropertyKey2 == null)
                throw new ArgumentNullException("Parameter mySemanticPropertyKey2 must not be null!");


            // Check the length of the SemanticPropertyKeys
            if (mySemanticPropertyKey1.Key.Length > mySemanticPropertyKey2.Key.Length)
                return true;

            if (mySemanticPropertyKey1.Key.Length < mySemanticPropertyKey2.Key.Length)
                return false;

            return mySemanticPropertyKey1.Key.CompareTo(mySemanticPropertyKey2.Key) > 0;

        }

        #endregion

        #region Operator <= (mySemanticPropertyKey1, mySemanticPropertyKey2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="mySemanticPropertyKey1">A SemanticPropertyKey.</param>
        /// <param name="mySemanticPropertyKey2">Another SemanticPropertyKey.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (SemanticProperty mySemanticPropertyKey1, SemanticProperty mySemanticPropertyKey2)
        {
            return !(mySemanticPropertyKey1 > mySemanticPropertyKey2);
        }

        #endregion

        #region Operator >= (mySemanticPropertyKey1, mySemanticPropertyKey2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="mySemanticPropertyKey1">A SemanticPropertyKey.</param>
        /// <param name="mySemanticPropertyKey2">Another SemanticPropertyKey.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (SemanticProperty mySemanticPropertyKey1, SemanticProperty mySemanticPropertyKey2)
        {
            return !(mySemanticPropertyKey1 < mySemanticPropertyKey2);
        }

        #endregion

        #endregion

        #region IComparable<SemanticPropertyKey> Members

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

            // Check if myObject can be casted to an SemanticPropertyKey object
            var mySemanticPropertyKey = myObject as SemanticProperty;
            if ((Object) mySemanticPropertyKey == null)
                throw new ArgumentException("myObject is not of type SemanticPropertyKey!");

            return CompareTo(mySemanticPropertyKey);

        }

        #endregion

        #region CompareTo(mySemanticPropertyKey)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="mySemanticPropertyKey">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(SemanticProperty mySemanticPropertyKey)
        {

            // Check if mySemanticPropertyKey is null
            if (mySemanticPropertyKey == null)
                throw new ArgumentNullException("mySemanticPropertyKey must not be null!");

            return Key.CompareTo(mySemanticPropertyKey.Key);

        }

        #endregion

        #endregion

        #region IEquatable<SemanticPropertyKey> Members

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

            // Check if myObject can be cast to SemanticPropertyKey
            var mySemanticPropertyKey = myObject as SemanticProperty;
            if ((Object) mySemanticPropertyKey == null)
                throw new ArgumentException("Parameter myObject could not be casted to type SemanticPropertyKey!");

            return this.Equals(mySemanticPropertyKey);

        }

        #endregion

        #region Equals(mySemanticPropertyKey)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="mySemanticPropertyKey">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(SemanticProperty mySemanticPropertyKey)
        {

            // Check if mySemanticPropertyKey is null
            if (mySemanticPropertyKey == null)
                throw new ArgumentNullException("Parameter mySemanticPropertyKey must not be null!");

            return Key.Equals(mySemanticPropertyKey.Key);

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
            return Key.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// A string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return String.Format("{0}#{1} [{2}]", Prefix, Suffix, Key);
        }

        #endregion

    }

}
