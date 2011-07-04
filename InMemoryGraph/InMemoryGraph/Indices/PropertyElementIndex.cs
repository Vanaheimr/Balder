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
using System.ComponentModel;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.Specialized;

#endregion

namespace de.ahzf.Blueprints.PropertyGraph.InMemory
{

    /// <summary>
    /// A property element index is a data structure that supports the indexing of properties in property graphs.
    /// An index is typically some sort of tree structure that allows for the fast lookup of elements by key/value pairs.
    /// </summary>
    public class PropertyElementIndex<T, TIndexKey>
                     : IPropertyElementIndex<T, TIndexKey>

        where T         : IEquatable<T>,         IComparable<T>,         IComparable
        where TIndexKey : IEquatable<TIndexKey>, IComparable<TIndexKey>, IComparable

    {

        #region Data

        /// <summary>
        /// A delegate for deciding if an element should be indexed or not.
        /// </summary>
        protected readonly IndexSelector<T, TIndexKey> Selector;

        /// <summary>
        /// A transformation from an incoming element to an index key.
        /// </summary>
        protected readonly IndexTransformation<T, TIndexKey> Transformation;

        /// <summary>
        /// The internal index datastructure.
        /// </summary>
        protected readonly IDictionary<TIndexKey, HashSet<T>> Index;

        #endregion

        #region Properties

        #region IndexName

        /// <summary>
        /// A human-friendly name of this index.
        /// </summary>
        public String IndexName { get; protected set; }

        #endregion

        #endregion

        #region Constructor(s)

        #region PropertyElementIndex(Name, Selector, Transformation, Datastructure = null)

        /// <summary>
        /// Creates a new index for indexing PropertyElements like vertices, edges or hyperedges.
        /// </summary>
        /// <param name="Name">A human-friendly name for this index.</param>
        /// <param name="Selector">A delegate for deciding if an PropertyElement should be indexed or not.</param>
        /// <param name="Transformation">A delegate for transforming a PropertyElement into an index key.</param>
        /// <param name="Datastructure">An optional datastructure for maintaining the index.</param>
        public PropertyElementIndex(String                            Name,
                                    IndexSelector      <T, TIndexKey> Selector,
                                    IndexTransformation<T, TIndexKey> Transformation,
                                    IDictionary<TIndexKey, T>         Datastructure = null)
        {
            
            this.IndexName      = Name;
            this.Selector       = Selector;
            this.Transformation = Transformation;

            if (Datastructure == null)
                this.Index      = new Dictionary<TIndexKey, HashSet<T>>();

        }

        #endregion

        #endregion


        #region Insert(Element)

        /// <summary>
        /// Inserts an element into the index.
        /// </summary>
        /// <param name="Element">An element to index.</param>
        public void Insert(T Element)
        {
            if (this.Selector(Element))
            {
                
                HashSet<T> _HashSet = null;
                var _IndexKey = Transformation(Element);

                if (Index.TryGetValue(_IndexKey, out _HashSet))
                    _HashSet.Add(Element);
                else
                    Index.Add(_IndexKey, new HashSet<T>() { Element });

            }
        }

        #endregion

        public IEnumerable<T> Get(TIndexKey key)
        {
            
            HashSet<T> _HashSet = null;

            if (Index.TryGetValue(key, out _HashSet))
                return _HashSet;

            return new List<T>();

        }

        public void Remove(T Element)
        {
            throw new NotImplementedException();
        }

    }

}
