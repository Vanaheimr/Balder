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

    public interface IGenericGraph
    {
    }

    public interface IGenericGraph<TGraphDatastructure> : IGenericGraph
    {
    }

    /// <summary>
    /// A graph is a container object for a collection of vertices, edges and hyperedges.
    /// </summary>
    /// <typeparam name="TVertex">The type of the vertices.</typeparam>
    /// <typeparam name="TVertexId">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TVertexData">The type of the additional vertex data.</typeparam>
    /// <typeparam name="TEdge">The type of the edges.</typeparam>
    /// <typeparam name="TEdgeId">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TEdgeData">The type of the additional edge data.</typeparam>
    /// <typeparam name="THyperEdge">The type of the hyperedges.</typeparam>
    /// <typeparam name="THyperEdgeId">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="THyperEdgeData">The type of the additional hyperedge data.</typeparam>
    /// <typeparam name="TRevisionId">The type of the revision identifiers.</typeparam>
    /// <typeparam name="TGraphDatastructure">The datastructure to holding all vertices, edges and hyperedges.</typeparam>
    public interface IGenericGraph<TVertex,    TVertexId,    TVertexRevisionId,    TVertexData,
                                   TEdge,      TEdgeId,      TEdgeRevisionId,      TEdgeData,
                                   THyperEdge, THyperEdgeId, THyperEdgeRevisionId, THyperEdgeData,
                                   TGraphDatastructure> : IGenericGraph<TGraphDatastructure>

        where TVertex              : IGenericVertex   <TVertexId, TVertexRevisionId, TVertexData, TEdgeId, TEdgeRevisionId, TEdgeData, THyperEdgeId, THyperEdgeRevisionId, THyperEdgeData>
        where TEdge                : IGenericEdge     <TVertexId, TVertexRevisionId, TVertexData, TEdgeId, TEdgeRevisionId, TEdgeData, THyperEdgeId, THyperEdgeRevisionId, THyperEdgeData>
        where THyperEdge           : IGenericHyperEdge<TVertexId, TVertexRevisionId, TVertexData, TEdgeId, TEdgeRevisionId, TEdgeData, THyperEdgeId, THyperEdgeRevisionId, THyperEdgeData>

        where TVertexId            : IEquatable<TVertexId>,            IComparable<TVertexId>,            IComparable
        where TEdgeId              : IEquatable<TEdgeId>,              IComparable<TEdgeId>,              IComparable
        where THyperEdgeId         : IEquatable<THyperEdgeId>,         IComparable<THyperEdgeId>,         IComparable

        where TVertexRevisionId    : IEquatable<TVertexRevisionId>,    IComparable<TVertexRevisionId>,    IComparable
        where TEdgeRevisionId      : IEquatable<TEdgeRevisionId>,      IComparable<TEdgeRevisionId>,      IComparable
        where THyperEdgeRevisionId : IEquatable<THyperEdgeRevisionId>, IComparable<THyperEdgeRevisionId>, IComparable

    {


        /// <summary>
        /// Create a new vertex, add it to the graph, and return the newly created vertex.
        /// The provided object identifier is a recommendation for the identifier to use.
        /// It is not required that the implementation use this identifier.
        /// If the object identifier is already being used by the graph to reference a vertex,
        /// then that reference vertex is returned and no vertex is created.
        /// If the identifier is a vertex (perhaps from another graph),
        /// then the vertex is duplicated for this graph. Thus, a vertex can not be an identifier.
        /// </summary>
        /// <param name="VertexId">The recommended object identifier.</param>
        /// <param name="VertexInitializer">A delegate to initialize the new vertex.</param>
        /// <returns>The newly created vertex or the vertex already referenced by the provided identifier.</returns>
        TVertex AddVertex(TVertexId VertexId = default(TVertexId), Action<TVertexData> VertexInitializer = null);

        TVertex AddVertex(TVertex myVertexId);


        /// <summary>
        /// Return the vertex referenced by the given vertex identifier.
        /// If no vertex is referenced by that identifier, then return null.
        /// </summary>
        /// <param name="myVertexId">The identifier of the vertex.</param>
        /// <returns>The vertex referenced by the provided identifier or null when no such edge exists.</returns>
        TVertex GetVertex(TVertexId myVertexId);


        /// <summary>
        /// Return a collection of vertices referenced by the given array of vertex identifiers.
        /// </summary>
        /// <param name="myVertexIds">An array of vertex identifiers.</param>
        IEnumerable<TVertex> GetVertices(params TVertexId[] myVertexIds);


        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// An additional vertex filter may be applied for filtering.
        /// </summary>
        /// <param name="myVertexFilter">A delegate for vertex filtering.</param>
        IEnumerable<TVertex> GetVertices(Func<TVertex, Boolean> myVertexFilter = null);


        /// <summary>
        /// Remove the provided vertex from the graph.
        /// Upon removing the vertex, all the edges by which the vertex is connected will be removed as well.
        /// </summary>
        /// <param name="myIVertex">The vertex to be removed from the graph</param>
        void RemoveVertex(TVertex myIVertex);





        /// <summary>
        /// Add an edge to the graph. The added edges requires a recommended identifier, a tail vertex, an head vertex, and a label.
        /// Like adding a vertex, the provided object identifier is can be ignored by the implementation.
        /// </summary>
        /// <param name="myOutVertex">The vertex on the tail of the edge.</param>
        /// <param name="myInVertex">The vertex on the head of the edge.</param>
        /// <param name="EdgeId">The recommended object identifier.</param>
        /// <param name="Label">The label associated with the edge.</param>
        /// <param name="EdgeInitializer">A delegate to initialize the new edge.</param>
        /// <returns>The newly created edge</returns>
        TEdge AddEdge(TVertex myOutVertex, TVertex myInVertex, TEdgeId EdgeId = default(TEdgeId), String Label = null, Action<TEdgeData> EdgeInitializer = null);


        /// <summary>
        /// Return the edge referenced by the given edge identifier.
        /// If no edge is referenced by that identifier, then return null.
        /// </summary>
        /// <param name="myEdgeId">The identifier of the edge.</param>
        /// <returns>The edge referenced by the provided identifier or null when no such edge exists.</returns>
        TEdge GetEdge(TEdgeId myEdgeId);

        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// An additional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="myEdgeIds">An array of edge identifiers.</param>
        IEnumerable<TEdge> GetEdges(params TEdgeId[] myEdgeIds);

        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// An additional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="myEdgeFilter">A delegate for edge filtering.</param>
        IEnumerable<TEdge> GetEdges(Func<TEdge, Boolean> myEdgeFilter = null);


        /// <summary>
        /// Remove the provided edge from the graph.
        /// </summary>
        /// <param name="myIEdge">The edge to be removed from the graph</param>
        void RemoveEdge(TEdge myIEdge);





        /// <summary>
        /// Remove all the edges and vertices from the graph.
        /// </summary>
        void Clear();


        /// <summary>
        /// A shutdown function is required to properly close the graph.
        /// This is important for implementations that utilize disk-based serializations.
        /// </summary>
        void Shutdown();


    }

}
