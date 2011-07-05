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
    /// <typeparam name="T">The type of the elements to be indexed.</typeparam>
    /// <typeparam name="TIndexKey">The type of the index keys.</typeparam>
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

        #region Name

        /// <summary>
        /// A human-friendly name of this index.
        /// </summary>
        public String Name { get; protected set; }

        #endregion

        #region AutomaticIndex

        /// <summary>
        /// Is this index maintained by the database or by the user?
        /// </summary>
        public Boolean AutomaticIndex { get; protected set; }

        #endregion

        #region KeyType

        /// <summary>
        /// The type of the index keys.
        /// </summary>
        public Type KeyType { get; protected set; }

        #endregion

        #endregion

        #region Constructor(s)

        #region PropertyElementIndex(Name, Selector, Transformation, Datastructure = null)

        /// <summary>
        /// Creates a new index for indexing PropertyElements like vertices, edges or hyperedges.
        /// </summary>
        /// <param name="Name">A human-friendly name for this index.</param>
        /// <param name="Transformation">A delegate for transforming a PropertyElement into an index key.</param>
        /// <param name="Selector">An optional delegate for deciding if a PropertyElement should be indexed or not.</param>
        /// <param name="Datastructure">An optional datastructure for maintaining the index.</param>
        /// <param name="AutomaticIndex">Should this index be maintained by the database or by the user?</param>
        public PropertyElementIndex(String                            Name,
                                    IndexTransformation<T, TIndexKey> Transformation,
                                    IndexSelector      <T, TIndexKey> Selector       = null,
                                    IDictionary        <TIndexKey, T> Datastructure  = null,
                                    Boolean                           AutomaticIndex = false)
        {
            
            this.Name      = Name;
            this.Transformation = Transformation;
            this.Selector       = Selector;
            this.AutomaticIndex = AutomaticIndex;
            this.KeyType        = typeof(TIndexKey);

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

        #region Get<TIdxKey>(IdxKey)

        /// <summary>
        /// Get the indexed elements for the given typed index key.
        /// </summary>
        /// <typeparam name="TIdxKey">The type of the index key which must be at least ICompare/IEquatable.</typeparam>
        /// <param name="IdxKey">An index key.</param>
        public IEnumerable<T> Get<TIdxKey>(TIdxKey IdxKey)
            where TIdxKey : IEquatable<TIdxKey>, IComparable<TIdxKey>, IComparable
        {

            try
            {
                return Get((TIndexKey) (Object) IdxKey);
            }
            catch (Exception)
            {
                return new List<T>();
            }

        }

        #endregion

        #region Get(IdxKey)

        /// <summary>
        /// Get the indexed elements for the given typed index key.
        /// </summary>
        /// <param name="IdxKey">An index key.</param>
        public IEnumerable<T> Get(TIndexKey IdxKey)
        {
            
            HashSet<T> _HashSet = null;

            if (Index.TryGetValue(IdxKey, out _HashSet))
                return _HashSet;

            return new List<T>();

        }

        #endregion

        #region Remove(Element)

        /// <summary>
        /// Remove an element from the index.
        /// </summary>
        /// <param name="Element">An element for indexing.</param>
        public void Remove(T Element)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region CastTo<TKey>()

        /// <summary>
        /// Downcast this index to an IPropertyElementIndex&lt;T, TIndexKey&gt; index.
        /// </summary>
        /// <typeparam name="TIdxKey">The type of the index keys.</typeparam>
        public IPropertyElementIndex<T, TIdxKey> CastTo<TIdxKey>()
            where TIdxKey : IEquatable<TIdxKey>, IComparable<TIdxKey>, IComparable
        {
            return (IPropertyElementIndex<T, TIdxKey>) this;
        }

        #endregion

    }

}
