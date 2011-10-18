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

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// A simple implementation of a immutable map.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys.</typeparam>
    /// <typeparam name="TValue">The type of the values.</typeparam>
    public class ImmutableMap<TKey, TValue> : IImmutableMap<TKey, TValue>
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
    {

        #region Data

        private readonly IDictionary<TKey, TValue> InternalIDictionary;

        #endregion

        #region Constructor(s)

        #region ImmutableMap()

        /// <summary>
        /// Creates a simple implementation of a immutable map.
        /// </summary>
        public ImmutableMap()
        {
            this.InternalIDictionary = new Dictionary<TKey, TValue>();
        }

        #endregion

        #region ImmutableMap(InternalIDictionary)

        /// <summary>
        /// Creates a simple implementation of a immutable map.
        /// </summary>
        /// <param name="InternalIDictionary">An user-defined internal map.</param>
        public ImmutableMap(IDictionary<TKey, TValue> InternalIDictionary)
        {
            this.InternalIDictionary = InternalIDictionary;
        }

        #endregion

        #endregion


        #region Keys(KeyFilter = null)

        public IEnumerable<TKey> Keys(ItemFilter<TKey> KeyFilter = null)
        {

            if (KeyFilter == null)
                return this.InternalIDictionary.Keys;

            else
                return from   Item
                       in     InternalIDictionary.Keys
                       where  KeyFilter(Item)
                       select Item;

        }

        #endregion

        #region KeyCount(KeyFilter = null)

        public UInt64 KeyCount(ItemFilter<TKey> KeyFilter = null)
        {
            
            if (KeyFilter == null)
                return (UInt64) this.InternalIDictionary.Count;

            else
                return (UInt64) (from   Item
                                 in     InternalIDictionary.Keys
                                 where  KeyFilter(Item)
                                 select Item).Count();

        }

        #endregion


        #region Values(ValueFilter = null)

        public IEnumerable<TValue> Values(ItemFilter<TValue> ValueFilter = null)
        {

            if (ValueFilter == null)
                return from   KeyValuePair
                       in     InternalIDictionary
                       select KeyValuePair.Value;

            else
                return from   KeyValuePair
                       in     InternalIDictionary
                       where  ValueFilter(KeyValuePair.Value)
                       select KeyValuePair.Value;

        }

        #endregion

        #region ValueCount(ValueFilter = null)

        public UInt64 ValueCount(ItemFilter<TValue> ValueFilter = null)
        {

            if (ValueFilter == null)
                return (UInt64) InternalIDictionary.Values.Count();

            else
                return (UInt64) (from   Item
                                 in     InternalIDictionary.Values
                                 where  ValueFilter(Item)
                                 select Item).Count();

        }

        #endregion


        #region ContainsKey(Key)

        public Boolean ContainsKey(TKey Key)
        {
            return this.InternalIDictionary.ContainsKey(Key);
        }

        #endregion

        #region ContainsValue(Value)

        public Boolean ContainsValue(TValue Value)
        {

            if (Value == null)
                return false;

            foreach (var Item in this.InternalIDictionary.Values)
                if (Item.Equals(Value))
                    return true;

            return false;

        }

        #endregion

        #region Contains(KeyValuePair)

        public Boolean Contains(KeyValuePair<TKey, TValue> KeyValuePair)
        {
            return this.InternalIDictionary.Contains(KeyValuePair);
        }

        #endregion


        #region TryGetValue(Key, out Value)

        public Boolean TryGetValue(TKey Key, out TValue Value)
        {
            return this.InternalIDictionary.TryGetValue(Key, out Value);
        }

        #endregion


        #region GetEnumerator()

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.InternalIDictionary.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.InternalIDictionary.GetEnumerator();
        }

        #endregion

    }

}
