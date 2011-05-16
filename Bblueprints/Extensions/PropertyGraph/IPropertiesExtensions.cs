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
using System.Linq;
using System.Collections.Generic;

#endregion

namespace de.ahzf.blueprints.PropertyGraph
{

    /// <summary>
    /// Extensions to the IElement interface
    /// </summary>
    public static class IPropertiesExtensions
    {

        #region AsDynamic(this myIProperties)

        /// <summary>
        /// Converts the given IElement into a dynamic object
        /// </summary>
        /// <param name="myIProperties">An object implementing IElement.</param>
        /// <returns>A dynamic object</returns>
        public static dynamic AsDynamic<TKey, TValue, TDatastructure>(this IProperties<TKey, TValue, TDatastructure> myIProperties)
            where TKey           : IEquatable<TKey>, IComparable<TKey>, IComparable
            where TDatastructure : IDictionary<TKey, TValue>
        {
            return myIProperties as dynamic;
        }

        #endregion
        

        #region SetProperty(this myIProperties, myKeyValuePair)

        /// <summary>
        /// Assign a KeyValuePair to the element.
        /// If a value already exists for this key, then the previous key/value is overwritten.
        /// </summary>
        /// <param name="myIProperties">An object implementing IElement.</param>
        /// <param name="myKeyValuePair">A KeyValuePair of type string and object</param>
        public static IProperties<TKey, TValue> SetProperty<TKey, TValue>(this IProperties<TKey, TValue> myIProperties, KeyValuePair<TKey, TValue> myKeyValuePair)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {
            return myIProperties.SetProperty(myKeyValuePair.Key, myKeyValuePair.Value);
        }

        #endregion

        #region SetProperties(this myIProperties, myKeyValuePairs)

        /// <summary>
        /// Assign the given enumeration of KeyValuePairs to the element.
        /// If a value already exists for a key, then the previous key/value is overwritten.
        /// </summary>
        /// <param name="myIProperties">An object implementing IElement.</param>
        /// <param name="myKeyValuePairs">A enumeration of KeyValuePairs of type string and object</param>
        public static void SetProperties<TKey, TValue>(this IProperties<TKey, TValue> myIProperties, IEnumerable<KeyValuePair<TKey, TValue>> myKeyValuePairs)
            where TKey           : IEquatable<TKey>, IComparable<TKey>, IComparable
        {
            foreach (var _KeyValuePair in myKeyValuePairs)
                myIProperties.SetProperty(_KeyValuePair.Key, _KeyValuePair.Value);
        }

        #endregion

        #region SetProperties(this myIProperties, myIDictionary)

        /// <summary>
        /// Assign the given IDictionary to the element.
        /// If a value already exists for a key, then the previous key/value is overwritten.
        /// </summary>
        /// <param name="myIProperties">An object implementing IElement.</param>
        /// <param name="myIDictionary">A IDictionary of type string and object</param>
        public static void SetProperties<TKey, TValue>(this IProperties<TKey, TValue> myIProperties, IDictionary<TKey, TValue> myIDictionary)
            where TKey           : IEquatable<TKey>, IComparable<TKey>, IComparable
        {
            foreach (var _KeyValuePair in myIDictionary)
                myIProperties.SetProperty(_KeyValuePair.Key, _KeyValuePair.Value);
        }

        #endregion


        #region HasProperty(this myIProperties, myKey)

        /// <summary>
        /// Checks if a property having the given property key exists within this element.
        /// </summary>
        /// <param name="myIProperties">An object implementing IElement.</param>
        /// <param name="myKey">The property key.</param>
        /// <returns>true|false</returns>
        public static Boolean HasProperty<TKey, TValue>(this IProperties<TKey, TValue> myIProperties, TKey myKey)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            var _Value = myIProperties.GetProperty(myKey);

            if (_Value == null)
                return false;

            return true;

        }

        #endregion

        #region HasProperty(this myIProperties, myKey, myValue)

        /// <summary>
        /// Checks if a property having the given property key and value
        /// exists within this element.
        /// NOTE: Will not work as expected if the values do not implement
        /// the ".Equals(...)"-methods correctly!
        /// </summary>
        /// <param name="myIProperties">An object implementing IElement.</param>
        /// <param name="myKey">The property key.</param>
        /// <param name="myValue">The property value.</param>
        /// <returns>true|false</returns>
        public static Boolean HasProperty<TKey, TValue>(this IProperties<TKey, TValue> myIProperties, TKey myKey, Object myValue)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {
            return myValue.Equals(myIProperties.GetProperty(myKey));
        }

        #endregion

        #region HasProperty<TValue>(this myIProperties, myKey, myValue)

        /// <summary>
        /// Checks if a property having the given property key and value
        /// exists within this element.
        /// NOTE: Will not work as expected if the values do not implement
        /// the ".Equals(...)"-methods correctly!
        /// </summary>
        /// <typeparam name="TValue">The type the property.</typeparam>
        /// <param name="myIProperties">An object implementing IElement.</param>
        /// <param name="myKey">The property key.</param>
        /// <param name="myValue">The property value.</param>
        /// <returns>true|false</returns>
        public static Boolean HasProperty<TKey, TValue>(this IProperties<TKey, TValue> myIProperties, TKey myKey, TValue myValue)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {
            return myValue.Equals((TValue) myIProperties.GetProperty(myKey));
        }

        #endregion

        #region HasProperty(this myIProperties, myPropertyFilter = null)

        /// <summary>
        /// Checks if a property having the given property key and value
        /// exists within this element.
        /// NOTE: Will not work as expected if the values do not implement
        /// the ".Equals(...)"-methods correctly!
        /// </summary>
        /// <param name="myIProperties">An object implementing IElement.</param>
        /// <param name="myPropertyFilter">A delegate for property filtering.</param>
        /// <returns>true|false</returns>
        public static Boolean HasProperty<TKey, TValue>(this IProperties<TKey, TValue> myIProperties, Func<TKey, TValue, Boolean> myPropertyFilter = null)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {
            return myIProperties.GetProperties(myPropertyFilter).Any();
        }

        #endregion

        //#region HasProperty(this myIProperties, myPropertyFilter = null)

        ///// <summary>
        ///// Checks if any properties matching the given property filter
        ///// exist within this element.
        ///// </summary>
        ///// <param name="myIProperties">An object implementing IElement.</param>
        ///// <param name="myPropertyFilter">A delegate for property filtering.</param>
        ///// <returns>true|false</returns>
        //public static Boolean HasProperty<TKey, TValue, TDatastructure>(this IProperties<TKey, TValue, TDatastructure> myIProperties, Func<TKey, TValue, Boolean> myPropertyFilter = null)
        //    where TKey           : IEquatable<TKey>, IComparable<TKey>, IComparable
        //    where TDatastructure : IDictionary<TKey, TValue>
        //{
        //    return myIProperties.GetProperties<TKey, TValue>(myPropertyFilter).Any();
        //}

        //#endregion


        #region GetProperty<TValue>(this myIProperties, myKey)

        /// <summary>
        /// Return the object value of type TValue associated with the provided string key.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type the property.</typeparam>
        /// <param name="myIProperties">An object implementing IElement.</param>
        /// <param name="myKey">the key of the key/value property</param>
        /// <returns>the object value related to the string key</returns>
        public static TValue GetProperty<TKey, TValue>(this IProperties<TKey, TValue> myIProperties, TKey myKey)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            try
            {
                return (TValue) myIProperties.GetProperty(myKey);
            }

            catch (Exception)
            {
                throw new Exception("Graph property '" + myKey + "' could not be casted to '" + typeof(TValue) + "'!");
            }

        }

        #endregion

        #region GetProperties<TKey, TValue, TDatastructure>(this myIProperties, myPropertyFilter = null)

        /// <summary>
        /// Get an enumeration of all properties as KeyValuePairs.
        /// An additional property filter may be applied for filtering.
        /// </summary>
        /// <typeparam name="TValue">The type the properties.</typeparam>
        /// <param name="myIProperties">An object implementing IElement.</param>
        /// <param name="myPropertyFilter">A delegate for property filtering.</param>
        /// <returns>An enumeration of all selected properties.</returns>
        public static IEnumerable<KeyValuePair<TKey, TValue>> GetProperties<TKey, TValue>(this IProperties<TKey, TValue> myIProperties, Func<TKey, TValue, Boolean> myPropertyFilter = null)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            Boolean _ExceptionOccured = false;
            TValue  _TValue           = default(TValue);

            foreach (var _KeyValuePair in myIProperties.GetProperties(myPropertyFilter))
            {

                _ExceptionOccured = false;

                try
                {
                    _TValue = (TValue) _KeyValuePair.Value;
                }
                catch
                {
                    _ExceptionOccured = true;
                }

                if (_ExceptionOccured == false && _TValue != null)
                    yield return new KeyValuePair<TKey, TValue>(_KeyValuePair.Key, _TValue);

            }

        }

        #endregion

        #region GetPropertyValues<TKey, TValue, TDatastructure>(this myIProperties, myPropertyFilter = null)

        /// <summary>
        /// Get an enumeration of all property values.
        /// An additional property filter may be applied for filtering.
        /// </summary>
        /// <param name="myIProperties">An object implementing IElement.</param>
        /// <param name="myPropertyFilter">A delegate for property filtering.</param>
        /// <returns>An enumeration of all selected property values.</returns>
        public static IEnumerable<TValue> GetPropertyValues<TKey, TValue>(this IProperties<TKey, TValue> myIProperties, Func<TKey, TValue, Boolean> myPropertyFilter = null)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {
            return from _KeyValuePair in myIProperties.GetProperties(myPropertyFilter) select _KeyValuePair.Value;
        }

        #endregion

        #region GetPropertyValues<TKey, TValue, TDatastructure, TCast>(this myIProperties, myPropertyFilter = null)

        /// <summary>
        /// Get an enumeration of all property values.
        /// An additional property filter may be applied for filtering.
        /// </summary>
        /// <typeparam name="TValue">The type the properties.</typeparam>
        /// <param name="myIProperties">An object implementing IElement.</param>
        /// <param name="myPropertyFilter">A delegate for property filtering.</param>
        /// <returns>An enumeration of all selected property values.</returns>
        public static IEnumerable<TCast> GetPropertyValues<TKey, TValue, TCast>(this IProperties<TKey, TValue> myIProperties, Func<TKey, TValue, Boolean> myPropertyFilter = null)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
            //where TCast          : TValue
        {

            Boolean _ExceptionOccured = false;
            TCast   _TCastedValue     = default(TCast);

            foreach (var _Value in myIProperties.GetPropertyValues(myPropertyFilter))
            {

                _ExceptionOccured = false;

                try
                {
                    _TCastedValue = (TCast) (Object) _Value;
                }
                catch
                {
                    _ExceptionOccured = true;
                }

                if (_ExceptionOccured == false && _TCastedValue != null)
                    yield return _TCastedValue;

            }

        }

        #endregion


        #region CompareProperties(this myIProperties1, myIProperties2)

        /// <summary>
        /// Compares the properties of two different IElement objects (vertices or edges).
        /// </summary>
        /// <param name="myIProperties1">A vertex or edge</param>
        /// <param name="myIProperties2">Another vertex or edge</param>
        /// <returns>true if both IElement objects carry the same properties</returns>
        public static Boolean CompareProperties<TKey, TValue>(this IProperties<TKey, TValue> myIProperties1, IProperties<TKey, TValue> myIProperties2)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            if (Object.ReferenceEquals(myIProperties1, myIProperties2))
                return true;

            // Get a ordered list of all properties from both
            var _Properties1 = (from _KeyValuePair in myIProperties1 orderby _KeyValuePair.Key select _KeyValuePair).ToList();
            var _Properties2 = (from _KeyValuePair in myIProperties2 orderby _KeyValuePair.Key select _KeyValuePair).ToList();

            // Check the total number of entries
            if (_Properties1.Count != _Properties2.Count)
                return false;

            // Check the entries in detail
            for (var i=0; i<_Properties1.Count; i++)
            {

                if (!_Properties1[i].Key.Equals(_Properties2[i].Key))
                    return false;

                if (Object.ReferenceEquals(_Properties1[i].Key, _Properties2[i].Key))
                    continue;

                // Handle with care as just objects are compared!
                if (!_Properties1[i].Value.Equals(_Properties2[i].Value))
                    return false;

            }

            return true;

        }

        #endregion


        #region AsList<TValue>(this myIProperties)

        /// <summary>
        /// Return the given object as an IEnumerable of its type.
        /// </summary>
        /// <typeparam name="TValue">The type the object.</typeparam>
        /// <param name="myIProperties">An object implementing IElement.</param>
        /// <returns>The given object as an IEnumerable of its type.</returns>
        public static IEnumerable<TValue> AsList<TKey, TValue>(this TValue myIProperties)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            return new List<TValue>() { myIProperties };

        }

        #endregion

    }

}
