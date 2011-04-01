/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET
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

using de.ahzf.blueprints.Datastructures;

#endregion

namespace de.ahzf.blueprints
{

    public interface IPropertyHyperEdge : IPropertyHyperEdge<VertexId,    RevisionId, String, Object,
                                                             EdgeId,      RevisionId, String, Object,
                                                             HyperEdgeId, RevisionId, String, Object>
    { }

    /// <summary>
    /// A hyperedge links multiple vertices. Along with its key/value properties,
    /// a hyperedge has both a directionality and a label.
    /// The directionality determines which vertex is the tail vertex
    /// (out vertex) and which vertices are the head vertices (in vertices).
    /// The hyperedge label determines the type of relationship that exists
    /// between these vertices.
    /// Diagrammatically, outVertex ---label---> inVertex1.
    ///                                      \--> inVertex2.
    /// </summary>
    /// <typeparam name="TKey">The type of the property keys.</typeparam>
    /// <typeparam name="TValue">The type of the property values.</typeparam>
    /// <typeparam name="TDatastructure">The type of the datastructure to maintain the key/value pairs.</typeparam>
    /// <typeparam name="TVertexId">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TVertexData">The type of the additional vertex data.</typeparam>
    /// <typeparam name="TEdgeId">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TEdgeData">The type of the additional edge data.</typeparam>
    /// <typeparam name="THyperEdgeId">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="THyperEdgeData">The type of the additional hyperedge data.</typeparam>
    /// <typeparam name="TRevisionId">The type of the revision identifiers.</typeparam>
    public interface IPropertyHyperEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,
                                        TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,
                                        THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge>

                                        : IPropertyElement<THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge>,

                                          IGenericHyperEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex>,
                                                            TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge>,
                                                            THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge>>

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

        where TVertexId               : IEquatable<TVertexId>,            IComparable<TVertexId>,            IComparable, TValueVertex
        where TEdgeId                 : IEquatable<TEdgeId>,              IComparable<TEdgeId>,              IComparable, TValueEdge
        where THyperEdgeId            : IEquatable<THyperEdgeId>,         IComparable<THyperEdgeId>,         IComparable, TValueHyperEdge

        where TVertexRevisionId       : IEquatable<TVertexRevisionId>,    IComparable<TVertexRevisionId>,    IComparable, TValueVertex
        where TEdgeRevisionId         : IEquatable<TEdgeRevisionId>,      IComparable<TEdgeRevisionId>,      IComparable, TValueEdge
        where THyperEdgeRevisionId    : IEquatable<THyperEdgeRevisionId>, IComparable<THyperEdgeRevisionId>, IComparable, TValueHyperEdge

    { }


    /// <summary>
    /// A hyperedge links multiple vertices. Along with its key/value properties,
    /// a hyperedge has both a directionality and a label.
    /// The directionality determines which vertex is the tail vertex
    /// (out vertex) and which vertices are the head vertices (in vertices).
    /// The hyperedge label determines the type of relationship that exists
    /// between these vertices.
    /// Diagrammatically, outVertex ---label---> inVertex1.
    ///                                      \--> inVertex2.
    /// </summary>
    /// <typeparam name="TKey">The type of the property keys.</typeparam>
    /// <typeparam name="TValue">The type of the property values.</typeparam>
    /// <typeparam name="TDatastructure">The type of the datastructure to maintain the key/value pairs.</typeparam>
    /// <typeparam name="TVertexId">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TVertexData">The type of the additional vertex data.</typeparam>
    /// <typeparam name="TEdgeId">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TEdgeData">The type of the additional edge data.</typeparam>
    /// <typeparam name="THyperEdgeId">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="THyperEdgeData">The type of the additional hyperedge data.</typeparam>
    /// <typeparam name="TRevisionId">The type of the revision identifiers.</typeparam>
    public interface IPropertyHyperEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                        TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                        THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>

                                        : IPropertyHyperEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,
                                                             TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,
                                                             THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge>,
        
                                          IPropertyElement<THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>

        where TDatastructureVertex    : IDictionary<TKeyVertex,    TValueVertex>
        where TDatastructureEdge      : IDictionary<TKeyEdge,      TValueEdge>
        where TDatastructureHyperEdge : IDictionary<TKeyHyperEdge, TValueHyperEdge>

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

        where TVertexId               : IEquatable<TVertexId>,            IComparable<TVertexId>,            IComparable, TValueVertex
        where TEdgeId                 : IEquatable<TEdgeId>,              IComparable<TEdgeId>,              IComparable, TValueEdge
        where THyperEdgeId            : IEquatable<THyperEdgeId>,         IComparable<THyperEdgeId>,         IComparable, TValueHyperEdge

        where TVertexRevisionId       : IEquatable<TVertexRevisionId>,    IComparable<TVertexRevisionId>,    IComparable, TValueVertex
        where TEdgeRevisionId         : IEquatable<TEdgeRevisionId>,      IComparable<TEdgeRevisionId>,      IComparable, TValueEdge
        where THyperEdgeRevisionId    : IEquatable<THyperEdgeRevisionId>, IComparable<THyperEdgeRevisionId>, IComparable, TValueHyperEdge

    { }


}
