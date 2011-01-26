/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET
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

namespace de.ahzf.blueprints
{

    /// <summary>
    /// Extensions to the IElement interface
    /// </summary>
    public static class IElementExtensions
    {

        #region AsDynamic(this myIElement)

        /// <summary>
        /// Converts the given IElement into a dynamic object
        /// </summary>
        /// <param name="myIElement">An object implementing IElement.</param>
        /// <returns>A dynamic object</returns>
        public static dynamic AsDynamic(this IElement myIElement)
        {
            return myIElement as dynamic;
        }

        #endregion
        

        #region SetProperty(this myIElement, myKeyValuePair)

        /// <summary>
        /// Assign a KeyValuePair to the element.
        /// If a value already exists for this key, then the previous key/value is overwritten.
        /// </summary>
        /// <param name="myIElement">An object implementing IElement.</param>
        /// <param name="myKeyValuePair">A KeyValuePair of type string and object</param>
        public static void SetProperty(this IElement myIElement, KeyValuePair<String, Object> myKeyValuePair)
        {
            myIElement.SetProperty(myKeyValuePair.Key, myKeyValuePair.Value);
        }

        #endregion

        #region SetProperties(this myIElement, myKeyValuePairs)

        /// <summary>
        /// Assign the given enumeration of KeyValuePairs to the element.
        /// If a value already exists for a key, then the previous key/value is overwritten.
        /// </summary>
        /// <param name="myIElement">An object implementing IElement.</param>
        /// <param name="myKeyValuePairs">A enumeration of KeyValuePairs of type string and object</param>
        public static void SetProperties(this IElement myIElement, IEnumerable<KeyValuePair<String, Object>> myKeyValuePairs)
        {

            foreach (var _KeyValuePair in myKeyValuePairs)
                myIElement.SetProperty(_KeyValuePair.Key, _KeyValuePair.Value);

        }

        #endregion

        #region SetProperties(this myIElement, myIDictionary)

        /// <summary>
        /// Assign the given IDictionary to the element.
        /// If a value already exists for a key, then the previous key/value is overwritten.
        /// </summary>
        /// <param name="myIElement">An object implementing IElement.</param>
        /// <param name="myIDictionary">A IDictionary of type string and object</param>
        public static void SetProperties(this IElement myIElement, IDictionary<String, Object> myIDictionary)
        {

            foreach (var _KeyValuePair in myIDictionary)
                myIElement.SetProperty(_KeyValuePair.Key, _KeyValuePair.Value);

        }

        #endregion


        #region HasProperty(this myIElement, myKey)

        /// <summary>
        /// Checks if a property having the given property key exists within this element.
        /// </summary>
        /// <param name="myIElement">An object implementing IElement.</param>
        /// <param name="myKey">The property key.</param>
        /// <returns>true|false</returns>
        public static Boolean HasProperty(this IElement myIElement, String myKey)
        {

            var _Value = myIElement.GetProperty(myKey);

            if (_Value == null)
                return false;

            return true;

        }

        #endregion

        #region HasProperty(this myIElement, myKey, myValue)

        /// <summary>
        /// Checks if a property having the given property key and value
        /// exists within this element.
        /// NOTE: Will not work as expected if the values do not implement
        /// the ".Equals(...)"-methods correctly!
        /// </summary>
        /// <param name="myIElement">An object implementing IElement.</param>
        /// <param name="myKey">The property key.</param>
        /// <param name="myValue">The property value.</param>
        /// <returns>true|false</returns>
        public static Boolean HasProperty(this IElement myIElement, String myKey, Object myValue)
        {
            return myValue.Equals(myIElement.GetProperty(myKey));
        }

        #endregion

        #region HasProperty<TValue>(this myIElement, myKey, myValue)

        /// <summary>
        /// Checks if a property having the given property key and value
        /// exists within this element.
        /// NOTE: Will not work as expected if the values do not implement
        /// the ".Equals(...)"-methods correctly!
        /// </summary>
        /// <typeparam name="TValue">The type the property.</typeparam>
        /// <param name="myIElement">An object implementing IElement.</param>
        /// <param name="myKey">The property key.</param>
        /// <param name="myValue">The property value.</param>
        /// <returns>true|false</returns>
        public static Boolean HasProperty<TValue>(this IElement myIElement, String myKey, TValue myValue)
        {
            return myValue.Equals((TValue) myIElement.GetProperty(myKey));
        }

        #endregion

        #region HasProperty(this myIElement, myPropertyFilter = null)

        /// <summary>
        /// Checks if a property having the given property key and value
        /// exists within this element.
        /// NOTE: Will not work as expected if the values do not implement
        /// the ".Equals(...)"-methods correctly!
        /// </summary>
        /// <param name="myIElement">An object implementing IElement.</param>
        /// <param name="myPropertyFilter">A delegate for property filtering.</param>
        /// <returns>true|false</returns>
        public static Boolean HasProperty(this IElement myIElement, Func<String, Object, Boolean> myPropertyFilter = null)
        {
            return myIElement.GetProperties(myPropertyFilter).Any();
        }

        #endregion

        #region HasProperty(this myIElement, myPropertyFilter = null)

        /// <summary>
        /// Checks if any properties matching the given property filter
        /// exist within this element.
        /// </summary>
        /// <param name="myIElement">An object implementing IElement.</param>
        /// <param name="myPropertyFilter">A delegate for property filtering.</param>
        /// <returns>true|false</returns>
        public static Boolean HasProperty<TValue>(this IElement myIElement, Func<String, TValue, Boolean> myPropertyFilter = null)
        {
            return myIElement.GetProperties<TValue>(myPropertyFilter).Any();
        }

        #endregion


        #region GetProperty<TValue>(this myIElement, myKey)

        /// <summary>
        /// Return the object value of type TValue associated with the provided string key.
        /// </summary>
        /// <typeparam name="TValue">The type the property.</typeparam>
        /// <param name="myIElement">An object implementing IElement.</param>
        /// <param name="myKey">the key of the key/value property</param>
        /// <returns>the object value related to the string key</returns>
        public static TValue GetProperty<TValue>(this IElement myIElement, String myKey)
        {

            try
            {
                return (TValue) myIElement.GetProperty(myKey);
            }
            catch
            { }

            return default(TValue);

        }

        #endregion

        #region GetProperties<TValue>(this myIElement, myPropertyFilter = null)

        /// <summary>
        /// Get an enumeration of all properties as KeyValuePairs.
        /// An additional property filter may be applied for filtering.
        /// </summary>
        /// <typeparam name="TValue">The type the properties.</typeparam>
        /// <param name="myIElement">An object implementing IElement.</param>
        /// <param name="myPropertyFilter">A delegate for property filtering.</param>
        /// <returns>An enumeration of all selected properties.</returns>
        public static IEnumerable<KeyValuePair<String, TValue>> GetProperties<TValue>(this IElement myIElement, Func<String, TValue, Boolean> myPropertyFilter = null)
        {

            Boolean _ExceptionOccured = false;
            TValue  _TValue           = default(TValue);

            foreach (var _KeyValuePair in myIElement.GetProperties(myPropertyFilter))
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
                    yield return new KeyValuePair<String, TValue>(_KeyValuePair.Key, _TValue);

            }

        }

        #endregion

        #region GetPropertyValues(this myIElement, myPropertyFilter = null)

        /// <summary>
        /// Get an enumeration of all property values.
        /// An additional property filter may be applied for filtering.
        /// </summary>
        /// <param name="myIElement">An object implementing IElement.</param>
        /// <param name="myPropertyFilter">A delegate for property filtering.</param>
        /// <returns>An enumeration of all selected property values.</returns>
        public static IEnumerable<Object> GetPropertyValues(this IElement myIElement, Func<String, Object, Boolean> myPropertyFilter = null)
        {
            return from _KeyValuePair in myIElement.GetProperties(myPropertyFilter) select _KeyValuePair.Value;
        }

        #endregion

        #region GetPropertyValues<TValue>(this myIElement, myPropertyFilter = null)

        /// <summary>
        /// Get an enumeration of all property values.
        /// An additional property filter may be applied for filtering.
        /// </summary>
        /// <typeparam name="TValue">The type the properties.</typeparam>
        /// <param name="myIElement">An object implementing IElement.</param>
        /// <param name="myPropertyFilter">A delegate for property filtering.</param>
        /// <returns>An enumeration of all selected property values.</returns>
        public static IEnumerable<TValue> GetPropertyValues<TValue>(this IElement myIElement, Func<String, Object, Boolean> myPropertyFilter = null)
        {

            Boolean _ExceptionOccured = false;
            TValue  _TValue           = default(TValue);

            foreach (var _Value in myIElement.GetPropertyValues(myPropertyFilter))
            {

                _ExceptionOccured = false;

                try
                {
                    _TValue = (TValue) _Value;
                }
                catch
                {
                    _ExceptionOccured = true;
                }

                if (_ExceptionOccured == false && _TValue != null)
                    yield return _TValue;

            }

        }

        #endregion


        #region CompareProperties(this myIElement1, myIElement2)

        /// <summary>
        /// Compares the properties of two different IElement objects (vertices or edges).
        /// </summary>
        /// <param name="myIElement1">A vertex or edge</param>
        /// <param name="myIElement2">Another vertex or edge</param>
        /// <returns>true if both IElement objects carry the same properties</returns>
        public static Boolean CompareProperties(this IElement myIElement1, IElement myIElement2)
        {

            if (Object.ReferenceEquals(myIElement1, myIElement2))
                return true;

            // Get a ordered list of all properties from both
            var _Properties1 = (from _KeyValuePair in myIElement1 orderby _KeyValuePair.Key select _KeyValuePair).ToList();
            var _Properties2 = (from _KeyValuePair in myIElement2 orderby _KeyValuePair.Key select _KeyValuePair).ToList();

            // Check the total number of entries
            if (_Properties1.Count != _Properties2.Count)
                return false;

            // Check the entries in detail
            for (var i=0; i<_Properties1.Count; i++)
            {

                if (_Properties1[i].Key != _Properties2[i].Key)
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

    }

}
