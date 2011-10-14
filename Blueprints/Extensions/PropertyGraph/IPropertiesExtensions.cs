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

namespace de.ahzf.Blueprints.PropertyGraphs
{

    /// <summary>
    /// Extensions to the IProperties interface.
    /// </summary>
    public static class IPropertiesExtensions
    {

        #region SetProperty(this IProperties, KeyValuePair)

        /// <summary>
        /// Assign a KeyValuePair to the given IProperties object.
        /// If a value already exists for this key, then the previous key/value is overwritten.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="KeyValuePair">A KeyValuePair of type string and object</param>
        public static IProperties<TKey, TValue> SetProperty<TKey, TValue>(this IProperties<TKey, TValue> IProperties, KeyValuePair<TKey, TValue> KeyValuePair)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            #endregion

            return IProperties.SetProperty(KeyValuePair.Key, KeyValuePair.Value);

        }

        #endregion

        #region SetProperties(this IProperties, KeyValuePairs)

        /// <summary>
        /// Assign the given enumeration of KeyValuePairs to the IProperties object.
        /// If a value already exists for a key, then the previous key/value is overwritten.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="KeyValuePairs">A enumeration of KeyValuePairs of type string and object</param>
        public static void SetProperties<TKey, TValue>(this IProperties<TKey, TValue> IProperties, IEnumerable<KeyValuePair<TKey, TValue>> KeyValuePairs)
            where TKey           : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (KeyValuePairs == null)
                throw new ArgumentNullException("The given KeyValuePair enumeration must not be null!");

            #endregion

            foreach (var _KeyValuePair in KeyValuePairs)
                IProperties.SetProperty(_KeyValuePair.Key, _KeyValuePair.Value);

        }

        #endregion

        #region SetProperties(this IProperties, IDictionary)

        /// <summary>
        /// Assign the given IDictionary to the IProperties object.
        /// If a value already exists for a key, then the previous key/value is overwritten.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="IDictionary">A IDictionary of type TKey and TValue</param>
        public static void SetProperties<TKey, TValue>(this IProperties<TKey, TValue> IProperties, IDictionary<TKey, TValue> IDictionary)
            where TKey           : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (IDictionary == null)
                throw new ArgumentNullException("The given dictionary must not be null!");

            #endregion

            foreach (var _KeyValuePair in IDictionary)
                IProperties.SetProperty(_KeyValuePair.Key, _KeyValuePair.Value);

        }

        #endregion



        #region GetProperty<TValue>(this IProperties, PropertyKey)
        // Just an alternative syntax!

        /// <summary>
        /// Return the object value of type TValue associated with the provided property key.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        public static TValue GetProperty<TKey, TValue>(this IProperties<TKey, TValue> IProperties, TKey PropertyKey)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            #endregion

            TValue PropertyValue = default(TValue);

            if (IProperties.GetProperty(PropertyKey, out PropertyValue))
                return PropertyValue;

            else
                return default(TValue);

        }

        #endregion

        #region GetProperty<TValue>(this IProperties, PropertyKey, PropertyType)
        // Just an alternative syntax!

        /// <summary>
        /// Return the object value of type TValue associated with the provided property key.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="PropertyType">The expected type of the property.</param>
        /// <param name="IProperties">An object implementing IProperties.</param>
        public static TValue GetProperty<TKey, TValue>(this IProperties<TKey, TValue> IProperties, TKey PropertyKey, Type PropertyType)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            #endregion

            TValue Value;

            if (IProperties.GetProperty(PropertyKey, out Value))
                if (Value.GetType().Equals(PropertyType))
                    return IProperties[PropertyKey];

            return default(TValue);

        }

        #endregion


        #region GetProperty<TKey, TValue>(IProperties, PropertyKey, OnSuccessAction [Action<TValue>] )

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="OnSuccessAction">A delegate to call for the associated value of the given property key.</param>
        public static void GetProperty<TKey, TValue>(this IProperties<TKey, TValue> IProperties,
                                                     TKey                           PropertyKey,
                                                     Action<TValue>                 OnSuccessAction)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccessAction == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;
            if (IProperties.GetProperty(PropertyKey, out Value))
                OnSuccessAction(Value);

        }

        #endregion

        #region GetProperty<TKey, TValue>(IProperties, PropertyKey, OnSuccessAction [Action<TKey, TValue>] )

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="OnSuccessAction">A delegate to call for the key and associated value of the given property key.</param>
        public static void GetProperty<TKey, TValue>(this IProperties<TKey, TValue> IProperties,
                                                     TKey                           PropertyKey,
                                                     Action<TKey, TValue>           OnSuccessAction)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccessAction == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;
            if (IProperties.GetProperty(PropertyKey, out Value))
                OnSuccessAction(PropertyKey, Value);

        }

        #endregion

        #region GetKeyValuePair<TKey, TValue>(IProperties, PropertyKey, OnSuccessAction [Action<KeyValuePair<TKey, TValue>>] )
        // Note: Renamed for disambiguity with GetProperty(..., Action<TValue>)

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="OnSuccessAction">A delegate to call for a matching KeyValuePair.</param>
        public static void GetKeyValuePair<TKey, TValue>(this IProperties<TKey, TValue>     IProperties,
                                                         TKey                               PropertyKey,
                                                         Action<KeyValuePair<TKey, TValue>> OnSuccessAction)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccessAction == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;
            if (IProperties.GetProperty(PropertyKey, out Value))
                OnSuccessAction(new KeyValuePair<TKey, TValue>(PropertyKey, Value));

        }

        #endregion


        #region GetProperty<TKey, TValue>(IProperties, PropertyKey, PropertyType, OnSuccessAction [Action<TValue>] )

        /// <summary>
        /// Call the given delegate if the given property key is assigned
        /// and the type of the value matches.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="PropertyType">The expected type of the property value.</param>
        /// <param name="OnSuccessAction">A delegate to call for the associated value of the given property key.</param>
        public static void GetProperty<TKey, TValue>(this IProperties<TKey, TValue> IProperties,
                                                     TKey                           PropertyKey,
                                                     Type                           PropertyType,
                                                     Action<TValue>                 OnSuccessAction)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccessAction == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;
            if (IProperties.GetProperty(PropertyKey, out Value))
                if (Value.GetType().Equals(PropertyType))
                    OnSuccessAction(Value);

        }

        #endregion

        #region GetProperty<TKey, TValue>(IProperties, PropertyKey, PropertyType, OnSuccessAction [Action<TKey, TValue>] )

        /// <summary>
        /// Call the given delegate if the given property key is assigned
        /// and the type of the value matches.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="PropertyType">The expected type of the property value.</param>
        /// <param name="OnSuccessAction">A delegate to call for the key and associated value of the given property key.</param>
        public static void GetProperty<TKey, TValue>(this IProperties<TKey, TValue> IProperties,
                                                     TKey                           PropertyKey,
                                                     Type                           PropertyType,
                                                     Action<TKey, TValue>           OnSuccessAction)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccessAction == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;
            if (IProperties.GetProperty(PropertyKey, out Value))
                if (Value.GetType().Equals(PropertyType))
                    OnSuccessAction(PropertyKey, Value);

        }

        #endregion

        #region GetKeyValuePair<TKey, TValue>(IProperties, PropertyKey, PropertyType, OnSuccessAction [Action<KeyValuePair<TKey, TValue>>] )
        // Note: Renamed for disambiguity with GetProperty(..., Action<TValue>)

        /// <summary>
        /// Call the given delegate if the given property key is assigned
        /// and the type of the value matches.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="PropertyType">The expected type of the property value.</param>
        /// <param name="OnSuccessAction">A delegate to call for a matching KeyValuePair.</param>
        public static void GetKeyValuePair<TKey, TValue>(this IProperties<TKey, TValue>     IProperties,
                                                         TKey                               PropertyKey,
                                                         Type                               PropertyType,
                                                         Action<KeyValuePair<TKey, TValue>> OnSuccessAction)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccessAction == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;
            if (IProperties.GetProperty(PropertyKey, out Value))
                if (Value.GetType().Equals(PropertyType))
                    OnSuccessAction(new KeyValuePair<TKey, TValue>(PropertyKey, Value));

        }

        #endregion


        #region GetProperty<TKey, TValue>(IProperties, PropertyKey, OnSuccessFunc [Func<TValue, Object>] )

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="OnSuccessFunc">A delegate to call for the associated value of the given property key.</param>
        public static Object GetProperty<TKey, TValue>(this IProperties<TKey, TValue> IProperties,
                                                       TKey                           PropertyKey,
                                                       Func<TValue, Object>           OnSuccessFunc)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccessFunc == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;
            if (IProperties.GetProperty(PropertyKey, out Value))
                return OnSuccessFunc(Value);

            return default(TValue);

        }

        #endregion

        #region GetProperty<TKey, TValue>(IProperties, PropertyKey, OnSuccessFunc [Func<TKey, TValue, Object>] )

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="OnSuccessFunc">A delegate to call for the key and associated value of the given property key.</param>
        public static Object GetProperty<TKey, TValue>(this IProperties<TKey, TValue> IProperties,
                                                       TKey                           PropertyKey,
                                                       Func<TKey, TValue, Object>     OnSuccessFunc)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccessFunc == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;
            if (IProperties.GetProperty(PropertyKey, out Value))
                return OnSuccessFunc(PropertyKey, Value);

            return default(TValue);

        }

        #endregion

        #region GetKeyValuePair<TKey, TValue>(IProperties, PropertyKey, OnSuccessFunc [Func<KeyValuePair<TKey, TValue>, Object>] )
        // Note: Renamed for disambiguity with GetProperty(..., Func<TValue, Object>)

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="OnSuccessFunc">A delegate to call for a matching KeyValuePair.</param>
        public static Object GetKeyValuePair<TKey, TValue>(this IProperties<TKey, TValue>           IProperties,
                                                           TKey                                     PropertyKey,
                                                           Func<KeyValuePair<TKey, TValue>, Object> OnSuccessFunc)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccessFunc == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;
            if (IProperties.GetProperty(PropertyKey, out Value))
                return OnSuccessFunc(new KeyValuePair<TKey, TValue>(PropertyKey, Value));

            return default(TValue);

        }

        #endregion


        #region GetProperty<TKey, TValue>(IProperties, PropertyKey, PropertyType, OnSuccessFunc [Func<TValue, Object>] )

        /// <summary>
        /// Call the given delegate if the given property key is assigned
        /// and the type of the value matches.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="PropertyType">The expected type of the property value.</param>
        /// <param name="OnSuccessFunc">A delegate to call for the associated value of the given property key.</param>
        public static Object GetProperty<TKey, TValue>(this IProperties<TKey, TValue> IProperties,
                                                       TKey                           PropertyKey,
                                                       Type                           PropertyType,
                                                       Func<TValue, Object>           OnSuccessFunc)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccessFunc == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;
            if (IProperties.GetProperty(PropertyKey, out Value))
                if (Value.GetType().Equals(PropertyType))
                    return OnSuccessFunc(Value);

            return default(TValue);

        }

        #endregion

        #region GetProperty<TKey, TValue>(IProperties, PropertyKey, PropertyType, OnSuccessFunc [Func<TKey, TValue, Object>] )

        /// <summary>
        /// Call the given delegate if the given property key is assigned
        /// and the type of the value matches.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="PropertyType">The expected type of the property value.</param>
        /// <param name="OnSuccessFunc">A delegate to call for the key and associated value of the given property key.</param>
        public static Object GetProperty<TKey, TValue>(this IProperties<TKey, TValue> IProperties,
                                                     TKey                             PropertyKey,
                                                     Type                             PropertyType,
                                                     Func<TKey, TValue, Object>       OnSuccessFunc)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccessFunc == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;
            if (IProperties.GetProperty(PropertyKey, out Value))
                if (Value.GetType().Equals(PropertyType))
                    return OnSuccessFunc(PropertyKey, Value);

            return default(TValue);

        }

        #endregion

        #region GetKeyValuePair<TKey, TValue>(IProperties, PropertyKey, PropertyType, OnSuccessFunc [Func<KeyValuePair<TKey, TValue>, Object>] )
        // Note: Renamed for disambiguity with GetProperty(..., Func<TValue, Object>)

        /// <summary>
        /// Call the given delegate if the given property key is assigned
        /// and the type of the value matches.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="PropertyType">The expected type of the property value.</param>
        /// <param name="OnSuccessFunc">A delegate to call for a matching KeyValuePair.</param>
        public static Object GetKeyValuePair<TKey, TValue>(this IProperties<TKey, TValue>           IProperties,
                                                           TKey                                     PropertyKey,
                                                           Type                                     PropertyType,
                                                           Func<KeyValuePair<TKey, TValue>, Object> OnSuccessFunc)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccessFunc == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;
            if (IProperties.GetProperty(PropertyKey, out Value))
                if (Value.GetType().Equals(PropertyType))
                    return OnSuccessFunc(new KeyValuePair<TKey, TValue>(PropertyKey, Value));

            return default(TValue);

        }

        #endregion



        #region GetPropertyCasted<TKey, TValue, TCast>(this IProperties, PropertyKey)
        // Just an alternative syntax!

        /// <summary>
        /// Return the object value of type TValue associated with the provided property key.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <typeparam name="TCast">The casted type of the properety values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        public static TCast GetPropertyCasted<TKey, TValue, TCast>(this IProperties<TKey, TValue> IProperties, TKey PropertyKey)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            #endregion

            return (TCast) (Object) IProperties[PropertyKey];

        }

        #endregion



        #region GetProperties<TKey, TValue>(IProperties, KeyValueFilter, OnSuccessAction [Action<TValue>] )

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="KeyValueFilter">A delegate to filter properties based on their keys and values.</param>
        /// <param name="OnSuccessAction">A delegate called for the associated value of each matching KeyValuePair.</param>
        public static void GetProperties<TKey, TValue>
                           (this IProperties<TKey, TValue> IProperties,
                            KeyValueFilter<TKey, TValue>   KeyValueFilter,
                            Action<TValue>                 OnSuccessAction)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (KeyValueFilter == null)
                throw new ArgumentNullException("The given KeyValueFilter must not be null!");

            if (OnSuccessAction == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            foreach (var Property in IProperties.GetProperties(KeyValueFilter))
                OnSuccessAction(Property.Value);

        }

        #endregion

        #region GetProperties<TKey, TValue>(IProperties, KeyValueFilter, OnSuccessAction [Action<TKey, TValue>] )

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="KeyValueFilter">A delegate to filter properties based on their keys and values.</param>
        /// <param name="OnSuccessAction">A delegate to call for each matching KeyValuePair.</param>
        public static void GetProperties<TKey, TValue>
                           (this IProperties<TKey, TValue> IProperties,
                            KeyValueFilter<TKey, TValue>   KeyValueFilter,
                            Action<TKey, TValue>           OnSuccessAction)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (KeyValueFilter == null)
                throw new ArgumentNullException("The given KeyValueFilter must not be null!");

            if (OnSuccessAction == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            foreach (var Property in IProperties.GetProperties(KeyValueFilter))
                OnSuccessAction(Property.Key, Property.Value);

        }

        #endregion

        #region GetProperties<TKey, TValue>(IProperties, KeyValueFilter, OnSuccessAction [Action<KeyValuePair<TKey, TValue>>] )

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="KeyValueFilter">A delegate to filter properties based on their keys and values.</param>
        /// <param name="OnSuccessAction">A delegate to call for each matching KeyValuePair.</param>
        public static void GetProperties<TKey, TValue>
                           (this IProperties<TKey, TValue>     IProperties,
                            KeyValueFilter<TKey, TValue>       KeyValueFilter,
                            Action<KeyValuePair<TKey, TValue>> OnSuccessAction)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (KeyValueFilter == null)
                throw new ArgumentNullException("The given KeyValueFilter must not be null!");

            if (OnSuccessAction == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            IProperties.GetProperties(KeyValueFilter).ForEach(OnSuccessAction);

        }

        #endregion


        #region GetProperties<TKey, TValue>(IProperties, KeyValueFilter, OnSuccessFunc [Func<TValue, Object>] )

        /// <summary>
        /// Call the given func delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="KeyValueFilter">A delegate to filter properties based on their keys and values.</param>
        /// <param name="OnSuccessFunc">A delegate returning an object for the associated value of each matching KeyValuePair.</param>
        public static IEnumerable<Object> GetProperties<TKey, TValue>
                                          (this IProperties<TKey, TValue> IProperties,
                                           KeyValueFilter<TKey, TValue>   KeyValueFilter,
                                           Func<TValue, Object>           OnSuccessFunc)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (KeyValueFilter == null)
                throw new ArgumentNullException("The given KeyValueFilter must not be null!");

            if (OnSuccessFunc == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            foreach (var Property in IProperties.GetProperties(KeyValueFilter))
                yield return OnSuccessFunc(Property.Value);

        }

        #endregion

        #region GetProperties<TKey, TValue>(IProperties, KeyValueFilter, OnSuccessFunc [Func<TKey, TValue, Object>] )

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="KeyValueFilter">A delegate to filter properties based on their keys and values.</param>
        /// <param name="OnSuccessFunc">A delegate returning an object for each matching KeyValuePair.</param>
        public static IEnumerable<Object> GetProperties<TKey, TValue>
                                          (this IProperties<TKey, TValue> IProperties,
                                           KeyValueFilter<TKey, TValue>   KeyValueFilter,
                                           Func<TKey, TValue, Object>     OnSuccessFunc)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (KeyValueFilter == null)
                throw new ArgumentNullException("The given KeyValueFilter must not be null!");

            if (OnSuccessFunc == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            //return IProperties.GetProperties(KeyValueFilter).Map(OnSuccessFunc);

            foreach (var Property in IProperties.GetProperties(KeyValueFilter))
                yield return OnSuccessFunc(Property.Key, Property.Value);

        }

        #endregion

        #region GetProperties<TKey, TValue>(IProperties, KeyValueFilter, OnSuccessFunc [Func<KeyValuePair<TKey, TValue>, Object>] )

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="KeyValueFilter">A delegate to filter properties based on their keys and values.</param>
        /// <param name="OnSuccessFunc">A delegate returning an object for each matching KeyValuePair.</param>
        public static IEnumerable<Object> GetProperties<TKey, TValue>
                                          (this IProperties<TKey, TValue>           IProperties,
                                           KeyValueFilter<TKey, TValue>             KeyValueFilter,
                                           Func<KeyValuePair<TKey, TValue>, Object> OnSuccessFunc)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (KeyValueFilter == null)
                throw new ArgumentNullException("The given KeyValueFilter must not be null!");

            if (OnSuccessFunc == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            return IProperties.GetProperties(KeyValueFilter).Select(OnSuccessFunc);

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
        public static IEnumerable<TKey> GetFilteredKeys<TKey, TValue>(this IProperties<TKey, TValue> IProperties, KeyValueFilter<TKey, TValue> KeyValueFilter)
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
        public static IEnumerable<TValue> GetFilteredValues<TKey, TValue>(this IProperties<TKey, TValue> IProperties, KeyValueFilter<TKey, TValue> KeyValueFilter)
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
        public static IEnumerable<TCast> GetFilteredValues<TKey, TValue, TCast>(this IProperties<TKey, TValue> IProperties, KeyValueFilter<TKey, TValue> KeyValueFilter = null)
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

    }

}
