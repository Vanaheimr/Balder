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
using System.Collections.Generic;

using eu.Vanaheimr.Styx;
using eu.Vanaheimr.Balder;

#endregion

namespace eu.Vanaheimr.Balder
{

    #region GraphElementFilterPipeExtensions

    /// <summary>
    /// Extension methods for the GraphElementFilterPipe.
    /// </summary>
    public static class GraphElementFilterPipeExtensions
    {

        #region GraphElementFilter(this IReadOnlyGraphElement, KeySelector, ComparisonFilter)

        /// <summary>
        /// Filters graph elements based on their properties.
        /// </summary>
        /// <typeparam name="TId">The type of the identifiers.</typeparam>
        /// <typeparam name="TRevId">The type of the revision identifiers.</typeparam>
        /// <typeparam name="TLabel">The taype of the labels.</typeparam>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <typeparam name="T">The type of the property to filter on.</typeparam>
        /// <typeparam name="S">A type derived from IReadOnlyGraphElement&lt;...&gt;.</typeparam>
        /// <param name="SourceElement">An object derived from IReadOnlyGraphElement&lt;...&gt; as element source.</param>
        /// <param name="KeySelector">A delegate for key selection.</param>
        /// <param name="ComparisonFilter">The comparison filter to use.</param>
        /// <returns>A filtered enumeration of graph elements.</returns>
        public static GraphElementFilterPipe<TId, TRevId, TLabel, TKey, TValue, T, S>

                              GraphElementFilter<TId, TRevId, TLabel, TKey, TValue, T, S>(this S              SourceElement,
                                                                                          Func<S, T>          KeySelector,
                                                                                          ComparisonFilter<T> ComparisonFilter)

            where TId     : IEquatable<TId>,    IComparable<TId>,    IComparable, TValue
            where TRevId  : IEquatable<TRevId>, IComparable<TRevId>, IComparable, TValue
            where TLabel  : IEquatable<TLabel>, IComparable<TLabel>, IComparable
            where TKey    : IEquatable<TKey>,   IComparable<TKey>,   IComparable
            where S       : IReadOnlyGraphElement<TId, TRevId, TLabel, TKey, TValue>

        {

            return new GraphElementFilterPipe<TId, TRevId, TLabel, TKey, TValue, T, S>(SourceElement, KeySelector, ComparisonFilter);

        }

        #endregion

        #region GraphElementFilter(this IEnumerable<S>, KeySelector, ComparisonFilter)

        /// <summary>
        /// Filters graph elements based on their properties.
        /// </summary>
        /// <typeparam name="TId">The type of the identifiers.</typeparam>
        /// <typeparam name="TRevId">The type of the revision identifiers.</typeparam>
        /// <typeparam name="TLabel">The taype of the labels.</typeparam>
        /// <typeparam name="TKey">The type of the property keys.</typeparam>
        /// <typeparam name="TValue">The type of the property values.</typeparam>
        /// <typeparam name="T">The type of the property to filter on.</typeparam>
        /// <typeparam name="S">A type derived from IReadOnlyGraphElement&lt;...&gt;.</typeparam>
        /// <param name="IEnumerable">An IEnumerable&lt;S&gt; as element source.</param>
        /// <param name="KeySelector">A delegate for key selection.</param>
        /// <param name="ComparisonFilter">The comparison filter to use.</param>
        /// <returns>A filtered enumeration of graph elements.</returns>
        public static GraphElementFilterPipe<TId, TRevId, TLabel, TKey, TValue, T, S>

                          GraphElementFilter<TId, TRevId, TLabel, TKey, TValue, T, S>(this IEndPipe<S>     SourcePipe,
                                                                                      Func<S, T>           KeySelector,
                                                                                      ComparisonFilter<T>  ComparisonFilter)


            where TId     : IEquatable<TId>,    IComparable<TId>,    IComparable, TValue
            where TRevId  : IEquatable<TRevId>, IComparable<TRevId>, IComparable, TValue
            where TLabel  : IEquatable<TLabel>, IComparable<TLabel>, IComparable
            where TKey    : IEquatable<TKey>,   IComparable<TKey>,   IComparable
            where S       : IReadOnlyGraphElement<TId, TRevId, TLabel, TKey, TValue>

        {

            return new GraphElementFilterPipe<TId, TRevId, TLabel, TKey, TValue, T, S>(SourcePipe, KeySelector, ComparisonFilter);

        }

        #endregion

    }

    #endregion

    #region GraphElementFilterPipe<...>

    /// <summary>
    /// Filters graph elements based on their properties.
    /// </summary>
    /// <typeparam name="TId">The type of the identifiers.</typeparam>
    /// <typeparam name="TRevId">The type of the revision identifiers.</typeparam>
    /// <typeparam name="TLabel">The taype of the labels.</typeparam>
    /// <typeparam name="TKey">The type of the property keys.</typeparam>
    /// <typeparam name="TValue">The type of the property values.</typeparam>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="S"></typeparam>
    public class GraphElementFilterPipe<TId, TRevId, TLabel, TKey, TValue, T, S>
                     : AbstractFilterPipe<S>

        where TId     : IEquatable<TId>,    IComparable<TId>,    IComparable, TValue
        where TRevId  : IEquatable<TRevId>, IComparable<TRevId>, IComparable, TValue
        where TLabel  : IEquatable<TLabel>, IComparable<TLabel>, IComparable
        where TKey    : IEquatable<TKey>,   IComparable<TKey>,   IComparable
        where S       : IReadOnlyGraphElement<TId, TRevId, TLabel, TKey, TValue>

    {

        #region Data

        private readonly Func<S, T>           KeySelector;
        private readonly ComparisonFilter<T>  ComparisonFilter;
        //private          T                    ActualValue;

        #endregion

        #region Constructor(s)

        #region GraphElementFilterPipe(SourceElement, KeySelector, ComparisonFilter)

        /// <summary>
        /// Filters graph elements based on their properties.
        /// </summary>
        /// <param name="SourceElement">A single value as element source.</param>
        /// <param name="KeySelector">A delegate for key selection.</param>
        /// <param name="ComparisonFilter">The comparison filter to use.</param>
        public GraphElementFilterPipe(S                   SourceElement,
                                      Func<S, T>          KeySelector,
                                      ComparisonFilter<T> ComparisonFilter)

            : base(SourceElement)

        {

            #region Initial checks

            if (KeySelector == null)
                throw new ArgumentNullException("KeySelector", "The given KeySelector delegate must not be null!");

            if (ComparisonFilter == null)
                throw new ArgumentNullException("ComparisonFilter", "The given ComparisonFilter delegate must not be null!");

            #endregion

            this.KeySelector      = KeySelector;
            this.ComparisonFilter = ComparisonFilter;

        }

        #endregion

        #region GraphElementFilterPipe(SourcePipe, KeySelector, ComparisonFilter)

        /// <summary>
        /// Filters graph elements based on their properties.
        /// </summary>
        /// <param name="SourcePipe">A pipe as element source.</param>
        /// <param name="KeySelector">A delegate for key selection.</param>
        /// <param name="ComparisonFilter">The comparison filter to use.</param>
        public GraphElementFilterPipe(IEndPipe<S>         SourcePipe,
                                      Func<S, T>          KeySelector,
                                      ComparisonFilter<T> ComparisonFilter)

            : base(SourcePipe)

        {

            #region Initial checks

            if (KeySelector == null)
                throw new ArgumentNullException("KeySelector", "The given KeySelector delegate must not be null!");

            if (ComparisonFilter == null)
                throw new ArgumentNullException("ComparisonFilter", "The given ComparisonFilter delegate must not be null!");

            #endregion

            this.KeySelector      = KeySelector;
            this.ComparisonFilter = ComparisonFilter;

        }

        #endregion

        #region GraphElementFilterPipe(SourceEnumerator, KeySelector, ComparisonFilter)

        /// <summary>
        /// Filters graph elements based on their properties.
        /// </summary>
        /// <param name="SourceEnumerator">An enumerator as element source.</param>
        /// <param name="KeySelector">A delegate for key selection.</param>
        /// <param name="ComparisonFilter">The comparison filter to use.</param>
        public GraphElementFilterPipe(IEnumerator<S>      SourceEnumerator,
                                      Func<S, T>          KeySelector,
                                      ComparisonFilter<T> ComparisonFilter)

            : base(SourceEnumerator)

        {

            #region Initial checks

            if (KeySelector == null)
                throw new ArgumentNullException("KeySelector", "The given KeySelector delegate must not be null!");

            if (ComparisonFilter == null)
                throw new ArgumentNullException("ComparisonFilter", "The given ComparisonFilter delegate must not be null!");

            #endregion

            this.KeySelector      = KeySelector;
            this.ComparisonFilter = ComparisonFilter;

        }

        #endregion

        #region GraphElementFilterPipe(SourceEnumerable, KeySelector, ComparisonFilter)

        /// <summary>
        /// Filters graph elements based on their properties.
        /// </summary>
        /// <param name="SourceEnumerable">An enumerable as element source.</param>
        /// <param name="KeySelector">A delegate for key selection.</param>
        /// <param name="ComparisonFilter">The comparison filter to use.</param>
        public GraphElementFilterPipe(IEnumerable<S>      SourceEnumerable,
                                      Func<S, T>          KeySelector,
                                      ComparisonFilter<T> ComparisonFilter)

            : base(SourceEnumerable)

        {

            #region Initial checks

            if (KeySelector == null)
                throw new ArgumentNullException("KeySelector", "The given KeySelector delegate must not be null!");

            if (ComparisonFilter == null)
                throw new ArgumentNullException("ComparisonFilter", "The given ComparisonFilter delegate must not be null!");

            #endregion

            this.KeySelector      = KeySelector;
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

            if (SourcePipe == null)
                return false;

            while (SourcePipe.MoveNext())
            {

                if (ComparisonFilter(KeySelector(SourcePipe.Current)))
                {
                    _CurrentElement = SourcePipe.Current;
                    return true;
                }

            }

            return false;

        }

        #endregion

    }

    #endregion

}
