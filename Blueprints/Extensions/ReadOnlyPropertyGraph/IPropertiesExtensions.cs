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
using System.Linq;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Blueprints.PropertyGraphs.ReadOnly
{

    /// <summary>
    /// Extensions to the IProperties interface.
    /// </summary>
    public static class IPropertiesExtensions
    {

        #region GetProperty<TValue>(this IProperties, Key)
        // Just an alternative syntax!

        /// <summary>
        /// Return the object value of type TValue associated with the provided string key.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="Key">A key.</param>
        public static TValue GetProperty<TKey, TValue>(this IReadOnlyProperties<TKey, TValue> IProperties, TKey Key)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {
            return IProperties[Key];
        }

        #endregion

        #region GetPropertyCasted<TKey, TValue, TCast>(this IProperties, Key)
        // Just an alternative syntax!

        /// <summary>
        /// Return the object value of type TValue associated with the provided string key.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <typeparam name="TCast">The casted type of the properety values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="Key">A key.</param>
        public static TCast GetPropertyCasted<TKey, TValue, TCast>(this IReadOnlyProperties<TKey, TValue> IProperties, TKey Key)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {
            return (TCast) (Object) IProperties[Key];
        }

        #endregion

        #region GetFilteredKeys<TKey, TValue>(this IProperties, KeyValueFilter)

        /// <summary>
        /// Get a filtered enumeration of all property keys.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="KeyValueFilter">A delegate to filter KeyValuePairs based on their keys and values.</param>
        /// <returns>An enumeration of all selected property values.</returns>
        public static IEnumerable<TKey> GetFilteredKeys<TKey, TValue>(this IReadOnlyProperties<TKey, TValue> IProperties, KeyValueFilter<TKey, TValue> KeyValueFilter)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            if (KeyValueFilter == null)
                throw new ArgumentNullException("The given KeyValueFilter must not be null!");

            return from _KeyValuePair in IProperties.GetProperties(KeyValueFilter) select _KeyValuePair.Key;

        }

        #endregion

        #region GetFilteredValues<TKey, TValue>(this IProperties, KeyValueFilter)

        /// <summary>
        /// Get a filtered enumeration of all property values.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="KeyValueFilter">A delegate to filter KeyValuePairs based on their keys and values.</param>
        /// <returns>An enumeration of all selected property values.</returns>
        public static IEnumerable<TValue> GetFilteredValues<TKey, TValue>(this IReadOnlyProperties<TKey, TValue> IProperties, KeyValueFilter<TKey, TValue> KeyValueFilter)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            if (KeyValueFilter == null)
                throw new ArgumentNullException("The given KeyValueFilter must not be null!");

            return from _KeyValuePair in IProperties.GetProperties(KeyValueFilter) select _KeyValuePair.Value;

        }

        #endregion

        #region GetFilteredValues<TKey, TValue, TCast>(this IProperties, KeyValueFilter = null)

        /// <summary>
        /// Get an enumeration of all property values.
        /// An additional property filter may be applied for filtering.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <typeparam name="TCast">The type of the cast.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="KeyValueFilter">An optional delegate to filter KeyValuePairs based on their keys and values.</param>
        /// <returns>An enumeration of all selected property values casted to TCast.</returns>
        public static IEnumerable<TCast> GetFilteredValues<TKey, TValue, TCast>(this IReadOnlyProperties<TKey, TValue> IProperties, KeyValueFilter<TKey, TValue> KeyValueFilter = null)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
            //where TCast          : TValue
        {

            Boolean _ExceptionOccured = false;
            TCast   _TCastedValue     = default(TCast);

            if (KeyValueFilter == null)
                KeyValueFilter = new KeyValueFilter<TKey, TValue>((key, value) => { return true; });

            foreach (var _Value in IProperties.GetFilteredValues(KeyValueFilter))
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
        public static Boolean CompareProperties<TKey, TValue>(this IReadOnlyProperties<TKey, TValue> myIProperties1, IReadOnlyProperties<TKey, TValue> myIProperties2)
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

    }

}
