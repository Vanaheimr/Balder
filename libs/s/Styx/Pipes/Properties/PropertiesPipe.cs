/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Styx <http://www.github.com/Vanaheimr/Styx>
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
using de.ahzf.Illias.Commons.Collections;

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    #region PropertiesPipeExtentions

    /// <summary>
    /// Extention methods for the PropertiesPipe.
    /// </summary>
    public static class PropertiesPipeExtentions
    {

        #region Ps<TKey, TValue>(this IReadOnlyProperties<...>, Keys)

        /// <summary>
        /// Emits the property values of the given property keys (OR-logic).
        /// </summary>
        /// <typeparam name="TKey">The type of the keys.</typeparam>
        /// <typeparam name="TValue">The type of the values.</typeparam>
        /// <param name="Properties">An object implementing IReadOnlyProperties&lt;TKey, TValue&gt;.</param>
        /// <param name="Keys">An array of property keys.</param>
        /// <returns>The property values of the given property keys.</returns>
        public static PropertiesPipe<TKey, TValue> Ps<TKey, TValue>(this IReadOnlyProperties<TKey, TValue> Properties,
                                                                    params TKey[] Keys)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {
            return new PropertiesPipe<TKey, TValue>(new IReadOnlyProperties<TKey, TValue>[1] { Properties }, Keys: Keys);
        }

        #endregion

        #region Ps<TKey, TValue>(this IEnumerable<IReadOnlyProperties<...>>, Keys)

        /// <summary>
        /// Emits the property values of the given property keys (OR-logic).
        /// </summary>
        /// <typeparam name="TKey">The type of the keys.</typeparam>
        /// <typeparam name="TValue">The type of the values.</typeparam>
        /// <param name="IEnumerable">An enumeration of IReadOnlyProperties&lt;TKey, TValue&gt;.</param>
        /// <param name="Keys">An array of property keys.</param>
        /// <returns>The property values of the given property keys.</returns>
        public static PropertiesPipe<TKey, TValue> Ps<TKey, TValue>(this IEnumerable<IReadOnlyProperties<TKey, TValue>> IEnumerable,
                                                                    params TKey[] Keys)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {
            return new PropertiesPipe<TKey, TValue>(IEnumerable, Keys: Keys);
        }

        #endregion


        #region Ps<TKey, TValue>(this IReadOnlyProperties<...>, KeyValueFilter)

        /// <summary>
        /// Emits the property values filtered by the given keyvalue filter.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys.</typeparam>
        /// <typeparam name="TValue">The type of the values.</typeparam>
        /// <param name="Properties">An object implementing IReadOnlyProperties&lt;TKey, TValue&gt;.</param>
        /// <param name="KeyValueFilter">An optional delegate for keyvalue filtering.</param>
        /// <returns>The property values of the given property keys.</returns>
        public static PropertiesPipe<TKey, TValue> Ps<TKey, TValue>(this IReadOnlyProperties<TKey, TValue> Properties,
                                                                    KeyValueFilter<TKey, TValue> KeyValueFilter)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {
            return new PropertiesPipe<TKey, TValue>(KeyValueFilter, new IReadOnlyProperties<TKey, TValue>[1] { Properties });
        }

        #endregion

        #region Ps<TKey, TValue>(this IEnumerable<IReadOnlyProperties<...>>, KeyValueFilter)

        /// <summary>
        /// Emits the property values filtered by the given keyvalue filter.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys.</typeparam>
        /// <typeparam name="TValue">The type of the values.</typeparam>
        /// <param name="IEnumerable">An enumeration of IReadOnlyProperties&lt;TKey, TValue&gt;.</param>
        /// <param name="KeyValueFilter">An optional delegate for keyvalue filtering.</param>
        /// <returns>The property values of the given property keys.</returns>
        public static PropertiesPipe<TKey, TValue> Ps<TKey, TValue>(this IEnumerable<IReadOnlyProperties<TKey, TValue>> IEnumerable,
                                                                    KeyValueFilter<TKey, TValue> KeyValueFilter)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {
            return new PropertiesPipe<TKey, TValue>(KeyValueFilter, IEnumerable);
        }

        #endregion

    }

    #endregion

    #region PropertiesPipe<TKey, TValue>

    /// <summary>
    /// Emits the property values.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys.</typeparam>
    /// <typeparam name="TValue">The type of the values.</typeparam>
    public class PropertiesPipe<TKey, TValue> : AbstractPipe<IReadOnlyProperties<TKey, TValue>, IEnumerable<TValue>>
        
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

    {

        #region Data

        private readonly TKey[] Keys;

        private readonly KeyValueFilter<TKey, TValue> KeyValueFilter;

        private IEnumerator<TKey> KeysInterator;

        private IEnumerator<KeyValuePair<TKey, TValue>> KeyValueInterator;

        #endregion

        #region Constructor(s)

        #region PropertiesPipe(IEnumerable = null, IEnumerator = null, Keys)

        /// <summary>
        /// Emits the property values of the given property keys (OR-logic).
        /// </summary>
        /// <param name="IEnumerable">An optional IEnumerable&lt;IIdentifier&lt;TId&gt;&gt; as element source.</param>
        /// <param name="IEnumerator">An optional IEnumerator&lt;IIdentifier&lt;TId&gt;&gt; as element source.</param>
        /// <param name="Keys">The property keys.</param>
        /// <returns>The property values of the given property keys.</returns>
        public PropertiesPipe(IEnumerable<IReadOnlyProperties<TKey, TValue>> IEnumerable = null,
                              IEnumerator<IReadOnlyProperties<TKey, TValue>> IEnumerator = null,
                              params TKey[] Keys)

            : base(IEnumerable, IEnumerator)

        {
            this.Keys = Keys;
        }

        #endregion

        #region PropertiesPipe(KeyValueFilter, IEnumerable = null, IEnumerator = null)

        /// <summary>
        /// Emits the property values filtered by the given keyvalue filter.
        /// </summary>
        /// <param name="KeyValueFilter">An optional delegate for keyvalue filtering.</param>
        /// <param name="IEnumerable">An optional IEnumerable&lt;...&gt; as element source.</param>
        /// <param name="IEnumerator">An optional IEnumerator&lt;...&gt; as element source.</param>
        public PropertiesPipe(KeyValueFilter<TKey, TValue>                   KeyValueFilter,
                              IEnumerable<IReadOnlyProperties<TKey, TValue>> IEnumerable = null,
                              IEnumerator<IReadOnlyProperties<TKey, TValue>> IEnumerator = null)

            : base(IEnumerable, IEnumerator)

        {
            this.KeyValueFilter = (KeyValueFilter != null) ? KeyValueFilter : (k, v) => true;
        }

        #endregion

        #endregion


        #region MoveNext()

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// True if the enumerator was successfully advanced to the next
        /// element; false if the enumerator has passed the end of the
        /// collection.
        /// </returns>
        public override Boolean MoveNext()
        {

            if (_InputEnumerator == null)
                return false;

            #region Process the key array

            if (Keys != null)
            {

                if (_InputEnumerator.MoveNext())
                {

                    _CurrentElement = (from   Key
                                       in     Keys
                                       select _InputEnumerator.Current.GetProperty(Key)).ToList();

                    return true;

                }

                return false;

            }

            #endregion

            #region Process the KeyValue filter

            else
            {

                if (_InputEnumerator.MoveNext())
                {

                    _CurrentElement = from   KeyValuePair
                                      in     _InputEnumerator.Current
                                      where  KeyValueFilter(KeyValuePair.Key, KeyValuePair.Value)
                                      select KeyValuePair.Value;

                    return true;

                }

                return false;

            }

            #endregion

        }

        #endregion


        #region ToString()

        /// <summary>
        /// A string representation of this pipe.
        /// </summary>
        public override String ToString()
        {

            if (Keys != null)
                return base.ToString() + "<" + Keys.Aggregate("", (a, b) => a.ToString() + " " + b.ToString()) + ">";

            return base.ToString();

        }

        #endregion

    }

    #endregion

}
