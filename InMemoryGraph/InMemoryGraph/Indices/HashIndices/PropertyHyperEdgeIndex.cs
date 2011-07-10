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
using System.Collections.Generic;

using de.ahzf.Blueprints.Indices;
using de.ahzf.Blueprints.PropertyGraph.Indices;

#endregion

namespace de.ahzf.Blueprints.PropertyGraph.InMemory
{

    /// <summary>
    /// The PropertyHyperEdgeIndex is a data structure that supports
    /// the indexing of PropertyHyperEdges in property graphs.
    /// </summary>
    public class PropertyHyperEdgeIndex<TIndexKey,
                                        TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                                        : PropertyIndex<TIndexKey,
                                                        IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

        where TIdVertex               : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge                 : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdHyperEdge            : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TEdgeLabel              : IEquatable<TEdgeLabel>,           IComparable<TEdgeLabel>,           IComparable
        where THyperEdgeLabel         : IEquatable<THyperEdgeLabel>,      IComparable<THyperEdgeLabel>,      IComparable

        where TRevisionIdVertex       : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge         : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdHyperEdge    : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

        where TIndexKey               : IEquatable<TIndexKey>,            IComparable<TIndexKey>,            IComparable

    {

        #region Constructor(s)

        #region PropertyHyperEdgeIndex(Name, IndexDatastructure, Transformation, Selector, AutomaticIndex = false)

        /// <summary>
        /// The PropertyHyperEdgeIndex is a data structure that supports
        /// the indexing of PropertyHyperEdges in property graphs.
        /// </summary>
        /// <param name="Name">A human-friendly name for this index.</param>
        /// <param name="IndexDatastructure">A datastructure for maintaining the index.</param>
        /// <param name="Transformation">A delegate for transforming a hyperedge into an index key.</param>
        /// <param name="Selector">An optional delegate for deciding if a hyperedge should be indexed or not.</param>
        /// <param name="AutomaticIndex">Should this index be maintained by the database or by the user?</param>
        public PropertyHyperEdgeIndex(String Name,
                                      IIndex<TIndexKey,  IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> IndexDatastructure,
                                      IndexTransformation<TIndexKey,
                                                          IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Transformation,
                                      IndexSelector      <TIndexKey,
                                                          IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Selector = null,
                                      Boolean AutomaticIndex = false)

            : base(Name, IndexDatastructure, Transformation, Selector, AutomaticIndex)

        { }

        #endregion

        #endregion

    }

}
