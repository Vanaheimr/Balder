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

    public interface IPropertyVertex : IPropertyVertex<VertexId, RevisionId, String, Object, IDictionary<String, Object>,
                                                       EdgeId,      RevisionId, String, Object, IDictionary<String, Object>,
                                                       HyperEdgeId, RevisionId, String, Object, IDictionary<String, Object>>
    { }

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
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
    public interface IPropertyVertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                     TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                     THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>

                                     : IPropertyElement<TVertexId, TVertexRevisionId, TKeyVertex, TValueVertex, TDatastructureVertex>,

                                       IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                                      TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                                      THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>

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
