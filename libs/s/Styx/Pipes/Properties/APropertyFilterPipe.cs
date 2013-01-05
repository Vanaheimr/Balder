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
using System.Collections.Generic;

using de.ahzf.Illias.Commons.Collections;

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    #region ComparisonFilter delegate

    /// <summary>
    /// A delegate for comparisions.
    /// </summary>
    /// <typeparam name="TValue">The type of the item to compare.</typeparam>
    /// <param name="Expected">The expected value of the item.</param>
    /// <returns>True if matches; False otherwise.</returns>
    public delegate Boolean ComparisonFilter<in TValue>(TValue Expected);

    #endregion

    #region APropertyFilterPipe<...>

    /// <summary>
    /// An abstract class for filtering objects by their properties.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys.</typeparam>
    /// <typeparam name="TValue">The type of the values.</typeparam>
    /// <typeparam name="S">The type of the objects to filter.</typeparam>
    public abstract class APropertyFilterPipe<TKey, TValue, S> : AbstractFilterPipe<S>

        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        where S    : IReadOnlyProperties<TKey, TValue>

    {

        #region Data

        private readonly TKey                     Key;
        private readonly ComparisonFilter<TValue> ComparisonFilter;
        private          TValue                   ActualValue;

        #endregion

        #region Constructor(s)

        #region APropertyFilterPipe(Key, ComparisonFilter, IEnumerable, IEnumerator)

        /// <summary>
        /// Creates a new PropertyFilterPipe.
        /// </summary>
        /// <param name="Key">The property key.</param>
        /// <param name="ComparisonFilter">The comparison filter to use.</param>
        /// <param name="IEnumerable">An IEnumerable&lt;...&gt; as element source.</param>
        /// <param name="IEnumerator">An IEnumerator&lt;...&gt; as element source.</param>
        public APropertyFilterPipe(TKey                     Key,
                                   ComparisonFilter<TValue> ComparisonFilter,
                                   IEnumerable<S>           IEnumerable,
                                   IEnumerator<S>           IEnumerator)

            : base(IEnumerable, IEnumerator)

        {

            #region Initial checks

            if (ComparisonFilter == null)
                throw new ArgumentNullException("ComparisonFilter", "The given ComparisonFilter delegate must not be null!");

            #endregion

            this.Key              = Key;
            this.ComparisonFilter = ComparisonFilter;

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

            while (_InputEnumerator.MoveNext())
            {

                if (_InputEnumerator.Current.TryGetProperty(Key, out ActualValue))
                {

                    if (_InputEnumerator.Current.TryGetProperty(Key, out ActualValue))
                    {
                        if (ComparisonFilter(ActualValue))
                        {
                            _CurrentElement = _InputEnumerator.Current;
                            return true;
                        }
                    }

                }

            }

            return false;

        }

        #endregion

    }

    #endregion

    #region APropertyFilterPipe<...TCast>

    /// <summary>
    /// An abstract class for filtering objects by their properties.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys.</typeparam>
    /// <typeparam name="TValue">The type of the values.</typeparam>
    /// <typeparam name="S">The type of the objects to filter.</typeparam>
    public abstract class APropertyFilterPipe<TKey, TValue, TCast, S> : AbstractFilterPipe<S>

        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        where S    : IReadOnlyProperties<TKey, TValue>
    {

        #region Data

        private readonly TKey Key;
        private readonly ComparisonFilter<TCast> ComparisonFilter;
        private TValue ActualValue;

        #endregion

        #region Constructor(s)

        #region APropertyFilterPipe(Key, ComparisonFilter, IEnumerable, IEnumerator)

        /// <summary>
        /// Creates a new PropertyFilterPipe.
        /// </summary>
        /// <param name="Key">The property key.</param>
        /// <param name="ComparisonFilter">The comparison filter to use.</param>
        /// <param name="IEnumerable">An IEnumerable&lt;...&gt; as element source.</param>
        /// <param name="IEnumerator">An IEnumerator&lt;...&gt; as element source.</param>
        public APropertyFilterPipe(TKey Key,
                                   ComparisonFilter<TCast> ComparisonFilter,
                                   IEnumerable<S> IEnumerable,
                                   IEnumerator<S> IEnumerator)

            : base(IEnumerable, IEnumerator)
        {

            #region Initial checks

            if (ComparisonFilter == null)
                throw new ArgumentNullException("ComparisonFilter", "The given ComparisonFilter delegate must not be null!");

            #endregion

            this.Key              = Key;
            this.ComparisonFilter = ComparisonFilter;

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

            while (_InputEnumerator.MoveNext())
            {

                if (_InputEnumerator.Current.TryGetProperty(Key, out ActualValue))
                {

                    try
                    {

                        if (ComparisonFilter((TCast) (Object) ActualValue))
                        {
                            _CurrentElement = _InputEnumerator.Current;
                            return true;
                        }

                    }

                    catch (InvalidCastException)
                    {
                        continue;
                    }

                }

            }

            return false;

        }

        #endregion

    }

    #endregion

}
