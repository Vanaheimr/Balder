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

    public interface IGenericVertex
    { }

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    /// <typeparam name="TVertexId">The type of the vertex identifier.</typeparam>
    public interface IGenericVertex<TVertexId,    TVertexRevisionId,    TVertexData,
                                    TEdgeId,      TEdgeRevisionId,      TEdgeData,
                                    THyperEdgeId, THyperEdgeRevisionId, THyperEdgeData>

                                    : IGenericElement<TVertexId, TVertexRevisionId>,
                                      IGenericVertex

        where TVertexId            : IEquatable<TVertexId>,            IComparable<TVertexId>,            IComparable
        where TEdgeId              : IEquatable<TEdgeId>,              IComparable<TEdgeId>,              IComparable
        where THyperEdgeId         : IEquatable<THyperEdgeId>,         IComparable<THyperEdgeId>,         IComparable

        where TVertexRevisionId    : IEquatable<TVertexRevisionId>,    IComparable<TVertexRevisionId>,    IComparable
        where TEdgeRevisionId      : IEquatable<TEdgeRevisionId>,      IComparable<TEdgeRevisionId>,      IComparable
        where THyperEdgeRevisionId : IEquatable<THyperEdgeRevisionId>, IComparable<THyperEdgeRevisionId>, IComparable

    {

        #region OutEdges

        /// <summary>
        /// Add an outgoing edge.
        /// </summary>
        /// <param name="myIEdge">The edge to add.</param>
        void AddOutEdge(IGenericEdge<TVertexId,    TVertexRevisionId,    TVertexData,
                                     TEdgeId,      TEdgeRevisionId,      TEdgeData,
                                     THyperEdgeId, THyperEdgeRevisionId, THyperEdgeData> myIEdge);

        /// <summary>
        /// The edges emanating from, or leaving, this vertex.
        /// </summary>
        IEnumerable<IGenericEdge<TVertexId,    TVertexRevisionId,    TVertexData,
                                 TEdgeId,      TEdgeRevisionId,      TEdgeData,
                                 THyperEdgeId, THyperEdgeRevisionId, THyperEdgeData>> OutEdges { get; }

        /// <summary>
        /// The edges emanating from, or leaving, this vertex
        /// filtered by their label.
        /// </summary>
        IEnumerable<IGenericEdge<TVertexId,    TVertexRevisionId,    TVertexData,
                                 TEdgeId,      TEdgeRevisionId,      TEdgeData,
                                 THyperEdgeId, THyperEdgeRevisionId, THyperEdgeData>> GetOutEdges(String myLabel);

        /// <summary>
        /// Remove an outgoing edge.
        /// </summary>
        /// <param name="myIEdge">The edge to remove.</param>
        void RemoveOutEdge(IGenericEdge<TVertexId,    TVertexRevisionId,    TVertexData,
                                        TEdgeId,      TEdgeRevisionId,      TEdgeData,
                                        THyperEdgeId, THyperEdgeRevisionId, THyperEdgeData> myIEdge);

        #endregion

        #region InEdges

        /// <summary>
        /// Add an incoming edge.
        /// </summary>
        /// <param name="myIEdge">The edge to add.</param>
        void AddInEdge(IGenericEdge<TVertexId,    TVertexRevisionId,    TVertexData,
                                    TEdgeId,      TEdgeRevisionId,      TEdgeData,
                                    THyperEdgeId, THyperEdgeRevisionId, THyperEdgeData> myIEdge);

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex.
        /// </summary>
        IEnumerable<IGenericEdge<TVertexId,    TVertexRevisionId,    TVertexData,
                                 TEdgeId,      TEdgeRevisionId,      TEdgeData,
                                 THyperEdgeId, THyperEdgeRevisionId, THyperEdgeData>> InEdges { get; }

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex
        /// filtered by their label.
        /// </summary>
        IEnumerable<IGenericEdge<TVertexId,    TVertexRevisionId,    TVertexData,
                                 TEdgeId,      TEdgeRevisionId,      TEdgeData,
                                 THyperEdgeId, THyperEdgeRevisionId, THyperEdgeData>> GetInEdges(String myLabel);

        /// <summary>
        /// Remove an incoming edge.
        /// </summary>
        /// <param name="myIEdge">The edge to remove.</param>
        void RemoveInEdge(IGenericEdge<TVertexId,    TVertexRevisionId,    TVertexData,
                                       TEdgeId,      TEdgeRevisionId,      TEdgeData,
                                       THyperEdgeId, THyperEdgeRevisionId, THyperEdgeData> myIEdge);

        #endregion

        /// <summary>
        /// Return the vertex data.
        /// </summary>
        TVertexData Data { get; }

    }

}
