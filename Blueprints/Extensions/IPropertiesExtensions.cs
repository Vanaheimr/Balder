/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <code@ahzf.de>
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

using de.ahzf.Illias.Commons;

#endregion

namespace de.ahzf.Blueprints.PropertyGraphs
{

    /// <summary>
    /// Extensions to the IProperties interface.
    /// </summary>
    public static class IPropertiesExtensions
    {

        // SetProperty(...)

        #region SetProperty(this IProperties, KeyValuePair)

        /// <summary>
        /// Assign a KeyValuePair to the given IProperties object.
        /// If a value already exists for this key, then the previous key/value is overwritten.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
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
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="KeyValuePairs">A enumeration of KeyValuePairs of type string and object</param>
        public static IProperties<TKey, TValue> SetProperties<TKey, TValue>(this IProperties<TKey, TValue> IProperties, IEnumerable<KeyValuePair<TKey, TValue>> KeyValuePairs)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (KeyValuePairs == null)
                throw new ArgumentNullException("The given KeyValuePair enumeration must not be null!");

            #endregion

            foreach (var _KeyValuePair in KeyValuePairs)
                IProperties.SetProperty(_KeyValuePair.Key, _KeyValuePair.Value);

            return IProperties;

        }

        #endregion

        #region SetProperties(this IProperties, IDictionary)

        /// <summary>
        /// Assign the given IDictionary to the IProperties object.
        /// If a value already exists for a key, then the previous key/value is overwritten.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="IDictionary">A IDictionary of type TKey and TValue</param>
        public static IProperties<TKey, TValue> SetProperties<TKey, TValue>(this IProperties<TKey, TValue> IProperties, IDictionary<TKey, TValue> IDictionary)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (IDictionary == null)
                throw new ArgumentNullException("The given dictionary must not be null!");

            #endregion

            foreach (var _KeyValuePair in IDictionary)
                IProperties.SetProperty(_KeyValuePair.Key, _KeyValuePair.Value);

            return IProperties;

        }

        #endregion



        // GetProperty(PropertyKey, ...)

        #region GetProperty<TKey, TValue>(this IProperties, PropertyKey)
        // Just an alternative syntax!

        /// <summary>
        /// Return the object value of type TValue associated with the provided property key.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
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

            if (IProperties.TryGetProperty(PropertyKey, out PropertyValue))
                return PropertyValue;

            else
                return default(TValue);

        }

        #endregion

        #region UseProperty<TKey, TValue>(this IProperties, PropertyKey, OnSuccess [Action<TValue>], OnError = null)

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="OnSuccess">A delegate to call for the associated value of the given property key and its value.</param>
        /// <param name="OnError">A delegate to call for the associated value of the given property key when an error occurs.</param>
        public static void UseProperty<TKey, TValue>(this IProperties<TKey, TValue> IProperties,
                                                     TKey                           PropertyKey,
                                                     Action<TValue>                 OnSuccess,
                                                     Action<TKey>                   OnError = null)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccess == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;
            if (IProperties.TryGetProperty(PropertyKey, out Value))
                OnSuccess(Value);

            else if (OnError != null)
                OnError(PropertyKey);

        }

        #endregion

        #region UseProperty<TKey, TValue>(this IProperties, PropertyKey, OnSuccess [Action<TKey, TValue>], OnError = null)

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="OnSuccess">A delegate to call for the associated value of the given property key and its value.</param>
        /// <param name="OnError">A delegate to call for the associated value of the given property key when an error occurs.</param>
        public static void UseProperty<TKey, TValue>(this IProperties<TKey, TValue> IProperties,
                                                     TKey                           PropertyKey,
                                                     Action<TKey, TValue>           OnSuccess,
                                                     Action<TKey>                   OnError = null)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccess == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;
            if (IProperties.TryGetProperty(PropertyKey, out Value))
                OnSuccess(PropertyKey, Value);

            else if (OnError != null)
                OnError(PropertyKey);

        }

        #endregion

        #region PropertyFunc<TKey, TValue, TResult>(this IProperties, PropertyKey, OnSuccessFunc [Func<TValue, TResult>], OnErrorFunc = null)

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="OnSuccessFunc">A delegate to call for the associated property value of the given property key.</param>
        /// <param name="OnErrorFunc">A delegate to call for the associated property key when the key was not found.</param>
        public static TResult PropertyFunc<TKey, TValue, TResult>(this IProperties<TKey, TValue> IProperties,
                                                                  TKey                           PropertyKey,
                                                                  Func<TValue, TResult>          OnSuccessFunc,
                                                                  Func<TKey, TResult>            OnErrorFunc = null)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccessFunc == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;
            if (IProperties.TryGetProperty(PropertyKey, out Value))
                return OnSuccessFunc(Value);

            if (OnErrorFunc != null)
                return OnErrorFunc(PropertyKey);

            return default(TResult);

        }

        #endregion

        #region PropertyFunc<TKey, TValue, TResult>(this IProperties, PropertyKey, OnSuccessFunc [Func<TKey, TValue, TResult>], OnErrorFunc = null)

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="OnSuccessFunc">A delegate to call for the key and associated value of the given property key.</param>
        public static TResult PropertyFunc<TKey, TValue, TResult>(this IProperties<TKey, TValue> IProperties,
                                                                  TKey                           PropertyKey,
                                                                  Func<TKey, TValue, TResult>    OnSuccessFunc,
                                                                  Func<TKey, TResult>            OnErrorFunc = null)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccessFunc == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;
            if (IProperties.TryGetProperty(PropertyKey, out Value))
                return OnSuccessFunc(PropertyKey, Value);

            if (OnErrorFunc != null)
                return OnErrorFunc(PropertyKey);

            return default(TResult);

        }

        #endregion


        // GetProperty(PropertyKey, PropertyType...)

        #region GetProperty<TKey, TValue>(this IProperties, PropertyKey, PropertyType)
        // Just an alternative syntax!

        /// <summary>
        /// Return the object value of type TValue associated with the provided property key.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="PropertyType">The expected type of the property.</param>
        public static TValue GetProperty<TKey, TValue>(this IProperties<TKey, TValue> IProperties, TKey PropertyKey, Type PropertyType)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            #endregion

            TValue Value;

            if (IProperties.TryGetProperty(PropertyKey, out Value))
                if (Value.GetType().Equals(PropertyType))
                    return Value;

            return default(TValue);

        }

        #endregion

        #region UseProperty<TKey, TValue>(this IProperties, PropertyKey, PropertyType, OnSuccess [Action<TValue>], OnError = null)

        /// <summary>
        /// Call the given delegate if the given property key is assigned
        /// and the type of the value matches.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="PropertyType">The expected type of the property value.</param>
        /// <param name="OnSuccess">A delegate to call for the associated value of the given property key.</param>
        public static void UseProperty<TKey, TValue>(this IProperties<TKey, TValue> IProperties,
                                                     TKey                           PropertyKey,
                                                     Type                           PropertyType,
                                                     Action<TValue>                 OnSuccess,
                                                     Action<TKey>                   OnError = null)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccess == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;

            if (IProperties.TryGetProperty(PropertyKey, out Value))
            {
                if (Value.GetType().Equals(PropertyType))
                    OnSuccess(Value);
            }
            else if (OnError != null)
                OnError(PropertyKey);

        }

        #endregion

        #region UseProperty<TKey, TValue>(this IProperties, PropertyKey, PropertyType, OnSuccess [Action<TKey, TValue>], OnError = null)

        /// <summary>
        /// Call the given delegate if the given property key is assigned
        /// and the type of the value matches.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="PropertyType">The expected type of the property value.</param>
        /// <param name="OnSuccess">A delegate to call for the key and associated value of the given property key.</param>
        public static void UseProperty<TKey, TValue>(this IProperties<TKey, TValue> IProperties,
                                                     TKey                           PropertyKey,
                                                     Type                           PropertyType,
                                                     Action<TKey, TValue>           OnSuccess,
                                                     Action<TKey>                   OnError = null)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccess == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;

            if (IProperties.TryGetProperty(PropertyKey, out Value))
            {
                if (Value.GetType().Equals(PropertyType))
                    OnSuccess(PropertyKey, Value);
            }
            else if (OnError != null)
                OnError(PropertyKey);

        }

        #endregion

        #region PropertyFunc<TKey, TValue>(this IProperties, PropertyKey, PropertyType, OnSuccessFunc [Func<TValue, Object>] )

        /// <summary>
        /// Call the given delegate if the given property key is assigned
        /// and the type of the value matches.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="PropertyType">The expected type of the property value.</param>
        /// <param name="OnSuccessFunc">A delegate to call for the associated value of the given property key.</param>
        public static Object PropertyFunc<TKey, TValue>(this IProperties<TKey, TValue> IProperties,
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
            if (IProperties.TryGetProperty(PropertyKey, out Value))
                if (Value.GetType().Equals(PropertyType))
                    return OnSuccessFunc(Value);

            return default(TValue);

        }

        #endregion

        #region PropertyFunc<TKey, TValue>(this IProperties, PropertyKey, PropertyType, OnSuccessFunc [Func<TKey, TValue, Object>] )

        /// <summary>
        /// Call the given delegate if the given property key is assigned
        /// and the type of the value matches.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="PropertyType">The expected type of the property value.</param>
        /// <param name="OnSuccessFunc">A delegate to call for the key and associated value of the given property key.</param>
        public static Object PropertyFunc<TKey, TValue>(this IProperties<TKey, TValue> IProperties,
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

            if (IProperties.TryGetProperty(PropertyKey, out Value))
                if (Value.GetType().Equals(PropertyType))
                    return OnSuccessFunc(PropertyKey, Value);

            return default(TValue);

        }

        #endregion


        // Get(Casted/Dynamic)Property(PropertyKey, ...)

        #region GetCastedProperty<TKey, TValue, TCast>(this IProperties, PropertyKey)
        // Just an alternative syntax!

        /// <summary>
        /// Return the object value of type TValue associated with the provided property key.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <typeparam name="TCast">The casted type of the properety values.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        public static TCast GetCastedProperty<TKey, TValue, TCast>(this IProperties<TKey, TValue> IProperties, TKey PropertyKey)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            #endregion

            return (TCast)(Object)IProperties[PropertyKey];

        }

        #endregion

        #region GetDynamicProperty<TKey, TValue>(this IProperties, PropertyKey)
        // Just an alternative syntax!

        /// <summary>
        /// Return the object value associated with the provided property key as dynamic.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        public static dynamic GetDynamicProperty<TKey, TValue>(this IProperties<TKey, TValue> IProperties, TKey PropertyKey)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            #endregion

            TValue Value;

            if (IProperties.TryGetProperty(PropertyKey, out Value))
                return (dynamic) Value;

            return default(TValue);

        }

        #endregion

        #region GetDynamicProperty<TKey, TValue>(this IProperties, PropertyKey, PropertyType)
        // Just an alternative syntax!

        /// <summary>
        /// Return the object value of type TValue associated with the provided property key as dynamic.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="PropertyType">The expected type of the property.</param>
        public static dynamic GetDynamicProperty<TKey, TValue>(this IProperties<TKey, TValue> IProperties, TKey PropertyKey, Type PropertyType)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            #endregion

            TValue Value;

            if (IProperties.TryGetProperty(PropertyKey, out Value))
                if (Value.GetType().Equals(PropertyType))
                    return (dynamic) Value;

            return default(TValue);

        }

        #endregion

        // InvokeProperty???










        // GetKeyValuePair

        #region GetKeyValuePair<TKey, TValue>(this IProperties, PropertyKey, OnSuccess [Action<KeyValuePair<TKey, TValue>>] )
        // Note: Renamed for disambiguity with GetProperty(..., Action<TValue>)

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="OnSuccess">A delegate to call for a matching KeyValuePair.</param>
        public static void GetKeyValuePair<TKey, TValue>(this IProperties<TKey, TValue>     IProperties,
                                                         TKey                               PropertyKey,
                                                         Action<KeyValuePair<TKey, TValue>> OnSuccess)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccess == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;
            if (IProperties.TryGetProperty(PropertyKey, out Value))
                OnSuccess(new KeyValuePair<TKey, TValue>(PropertyKey, Value));

        }

        #endregion

        #region GetKeyValuePair<TKey, TValue>(this IProperties, PropertyKey, PropertyType, OnSuccess [Action<KeyValuePair<TKey, TValue>>] )
        // Note: Renamed for disambiguity with GetProperty(..., Action<TValue>)

        /// <summary>
        /// Call the given delegate if the given property key is assigned
        /// and the type of the value matches.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="PropertyType">The expected type of the property value.</param>
        /// <param name="OnSuccess">A delegate to call for a matching KeyValuePair.</param>
        public static void GetKeyValuePair<TKey, TValue>(this IProperties<TKey, TValue>     IProperties,
                                                         TKey                               PropertyKey,
                                                         Type                               PropertyType,
                                                         Action<KeyValuePair<TKey, TValue>> OnSuccess)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccess == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;
            if (IProperties.TryGetProperty(PropertyKey, out Value))
                if (Value.GetType().Equals(PropertyType))
                    OnSuccess(new KeyValuePair<TKey, TValue>(PropertyKey, Value));

        }

        #endregion

        #region KeyValuePairFunc<TKey, TValue, TResult>(this IProperties, PropertyKey, OnSuccessFunc [Func<KeyValuePair<TKey, TValue>, TResult>] )
        // Note: Renamed for disambiguity with GetProperty(..., Func<TValue, Object>)

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="OnSuccessFunc">A delegate to call for a matching KeyValuePair.</param>
        public static TResult KeyValuePairFunc<TKey, TValue, TResult>(this IProperties<TKey, TValue>            IProperties,
                                                                      TKey                                      PropertyKey,
                                                                      Func<KeyValuePair<TKey, TValue>, TResult> OnSuccessFunc)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccessFunc == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;
            if (IProperties.TryGetProperty(PropertyKey, out Value))
                return OnSuccessFunc(new KeyValuePair<TKey, TValue>(PropertyKey, Value));

            return default(TResult);

        }

        #endregion

        #region KeyValuePairFunc<TKey, TValue, TResult>(this IProperties, PropertyKey, PropertyType, OnSuccessFunc [Func<KeyValuePair<TKey, TValue>, TResult>] )
        // Note: Renamed for disambiguity with GetProperty(..., Func<TValue, Object>)

        /// <summary>
        /// Call the given delegate if the given property key is assigned
        /// and the type of the value matches.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="PropertyType">The expected type of the property value.</param>
        /// <param name="OnSuccessFunc">A delegate to call for a matching KeyValuePair.</param>
        public static TResult KeyValuePairFunc<TKey, TValue, TResult>(this IProperties<TKey, TValue>            IProperties,
                                                                      TKey                                      PropertyKey,
                                                                      Type                                      PropertyType,
                                                                      Func<KeyValuePair<TKey, TValue>, TResult> OnSuccessFunc)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (OnSuccessFunc == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            TValue Value;
            if (IProperties.TryGetProperty(PropertyKey, out Value))
                if (Value.GetType().Equals(PropertyType))
                    return OnSuccessFunc(new KeyValuePair<TKey, TValue>(PropertyKey, Value));

            return default(TResult);

        }

        #endregion



        // UseProperties

        #region UseProperties<TKey, TValue>(IProperties, KeyValueFilter, OnSuccess [Action<TValue>] )

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="KeyValueFilter">A delegate to filter properties based on their keys and values.</param>
        /// <param name="OnSuccess">A delegate called for the associated value of each matching KeyValuePair.</param>
        public static void UseProperties<TKey, TValue>(this IProperties<TKey, TValue> IProperties,
                                                       KeyValueFilter<TKey, TValue>   KeyValueFilter,
                                                       Action<TValue>                 OnSuccess)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (KeyValueFilter == null)
                throw new ArgumentNullException("The given KeyValueFilter must not be null!");

            if (OnSuccess == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            foreach (var Property in IProperties.GetProperties(KeyValueFilter))
                OnSuccess(Property.Value);

        }

        #endregion

        #region UseProperties<TKey, TValue>(IProperties, KeyValueFilter, OnSuccess [Action<TKey, TValue>] )

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="KeyValueFilter">A delegate to filter properties based on their keys and values.</param>
        /// <param name="OnSuccess">A delegate to call for each matching KeyValuePair.</param>
        public static void UseProperties<TKey, TValue>(this IProperties<TKey, TValue> IProperties,
                                                       KeyValueFilter<TKey, TValue>   KeyValueFilter,
                                                       Action<TKey, TValue>           OnSuccess)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (KeyValueFilter == null)
                throw new ArgumentNullException("The given KeyValueFilter must not be null!");

            if (OnSuccess == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            foreach (var Property in IProperties.GetProperties(KeyValueFilter))
                OnSuccess(Property.Key, Property.Value);

        }

        #endregion

        #region UseProperties<TKey, TValue>(IProperties, KeyValueFilter, OnSuccess [Action<KeyValuePair<TKey, TValue>>] )

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="KeyValueFilter">A delegate to filter properties based on their keys and values.</param>
        /// <param name="OnSuccess">A delegate to call for each matching KeyValuePair.</param>
        public static void UseProperties<TKey, TValue>(this IProperties<TKey, TValue>     IProperties,
                                                       KeyValueFilter<TKey, TValue>       KeyValueFilter,
                                                       Action<KeyValuePair<TKey, TValue>> OnSuccess)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException("The given IProperties must not be null!");

            if (KeyValueFilter == null)
                throw new ArgumentNullException("The given KeyValueFilter must not be null!");

            if (OnSuccess == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            #endregion

            IProperties.GetProperties(KeyValueFilter).ForEach(OnSuccess);

        }

        #endregion

        #region PropertiesFunc<TKey, TValue, TResult>(IProperties, KeyValueFilter, OnSuccessFunc [Func<TValue, TResult>] )

        /// <summary>
        /// Call the given func delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="KeyValueFilter">A delegate to filter properties based on their keys and values.</param>
        /// <param name="OnSuccessFunc">A delegate returning an object for the associated value of each matching KeyValuePair.</param>
        public static IEnumerable<TResult> PropertiesFunc<TKey, TValue, TResult>(this IProperties<TKey, TValue> IProperties,
                                                                                 KeyValueFilter<TKey, TValue>   KeyValueFilter,
                                                                                 Func<TValue, TResult>          OnSuccessFunc)

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

        #region PropertiesFunc<TKey, TValue, TResult>(IProperties, KeyValueFilter, OnSuccessFunc [Func<TKey, TValue, TResult>] )

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="KeyValueFilter">A delegate to filter properties based on their keys and values.</param>
        /// <param name="OnSuccessFunc">A delegate returning an object for each matching KeyValuePair.</param>
        public static IEnumerable<TResult> PropertiesFunc<TKey, TValue, TResult>(this IProperties<TKey, TValue> IProperties,
                                                                                 KeyValueFilter<TKey, TValue>   KeyValueFilter,
                                                                                 Func<TKey, TValue, TResult>    OnSuccessFunc)

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

        #region PropertiesFunc<TKey, TValue, TResult>(IProperties, KeyValueFilter, OnSuccessFunc [Func<KeyValuePair<TKey, TValue>, TResult>] )

        /// <summary>
        /// Call the given delegate if the given property key is assigned.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <typeparam name="TResult">The type of the return value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="KeyValueFilter">A delegate to filter properties based on their keys and values.</param>
        /// <param name="OnSuccessFunc">A delegate returning an object for each matching KeyValuePair.</param>
        public static IEnumerable<TResult> PropertiesFunc<TKey, TValue, TResult>(this IProperties<TKey, TValue>            IProperties,
                                                                                 KeyValueFilter<TKey, TValue>              KeyValueFilter,
                                                                                 Func<KeyValuePair<TKey, TValue>, TResult> OnSuccessFunc)

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



        // FilteredKeys/Values

        #region FilteredKeys<TKey, TValue>(this IProperties, KeyValueFilter)

        /// <summary>
        /// Get a filtered enumeration of all property keys.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="KeyValueFilter">A delegate to filter KeyValuePairs based on their keys and values.</param>
        /// <returns>An enumeration of all selected property values.</returns>
        public static IEnumerable<TKey> FilteredKeys<TKey, TValue>(this IProperties<TKey, TValue> IProperties, KeyValueFilter<TKey, TValue> KeyValueFilter)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            #region Initial checks

            if (KeyValueFilter == null)
                throw new ArgumentNullException("The given KeyValueFilter must not be null!");

            #endregion

            return from _KeyValuePair in IProperties.GetProperties(KeyValueFilter) select _KeyValuePair.Key;

        }

        #endregion

        #region FilteredValues<TKey, TValue>(this IProperties, KeyValueFilter)

        /// <summary>
        /// Get a filtered enumeration of all property values.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <param name="IProperties">An object implementing IProperties.</param>
        /// <param name="KeyValueFilter">A delegate to filter KeyValuePairs based on their keys and values.</param>
        /// <returns>An enumeration of all selected property values.</returns>
        public static IEnumerable<TValue> FilteredValues<TKey, TValue>(this IProperties<TKey, TValue> IProperties, KeyValueFilter<TKey, TValue> KeyValueFilter)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            #region Initial checks

            if (KeyValueFilter == null)
                throw new ArgumentNullException("The given KeyValueFilter must not be null!");

            #endregion

            return from _KeyValuePair in IProperties.GetProperties(KeyValueFilter) select _KeyValuePair.Value;

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




        #region Remove(KeyValuePair)

        /// <summary>
        /// Remove the given KeyValuePair.
        /// </summary>
        /// <param name="KeyValuePair">A KeyValuePair.</param>
        /// <returns>The value associated with that key prior to the removal.</returns>
        public static TValue Remove<TKey, TValue>(this IProperties<TKey, TValue> IProperties, KeyValuePair<TKey, TValue> KeyValuePair)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {
            return IProperties.Remove(KeyValuePair.Key, KeyValuePair.Value);
        }

        #endregion

    }

}
