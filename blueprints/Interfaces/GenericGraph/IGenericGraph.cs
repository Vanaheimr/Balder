/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET <http://www.github.com/ahzf/blueprints.NET>
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
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TVertexData">The type of the additional vertex data.</typeparam>
    /// <typeparam name="TEdge">The type of the edges.</typeparam>
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TEdgeData">The type of the additional edge data.</typeparam>
    /// <typeparam name="THyperEdge">The type of the hyperedges.</typeparam>
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="THyperEdgeData">The type of the additional hyperedge data.</typeparam>
    /// <typeparam name="TRevisionId">The type of the revision identifiers.</typeparam>
    /// <typeparam name="TGraphDatastructure">The datastructure to holding all vertices, edges and hyperedges.</typeparam>
    public interface IGenericGraph<TVertex,    TIdVertex,    TRevisionIdVertex,    TVertexData,    TVertexExchange,
                                   TEdge,      TIdEdge,      TRevisionIdEdge,      TEdgeData,      TEdgeExchange,
                                   THyperEdge, TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData, THyperEdgeExchange,
                                   TGraphDatastructure>

                         : IGenericGraph<TGraphDatastructure>

        where TVertex              : IGenericVertex   <TIdVertex,    TRevisionIdVertex,    TVertexData,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeData, 
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData>

        where TEdge                : IGenericEdge     <TIdVertex,    TRevisionIdVertex,    TVertexData,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeData, 
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData>
        
        where THyperEdge           : IGenericHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexData,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeData, 
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData>

        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable
        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable
        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable

        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable
        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable
        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable

        //where TVertexExchange      : IGenericVertex   <TIdVertex, TRevisionIdVertex, TVertexData,  TIdEdge, TRevisionIdEdge, TEdgeData,  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData>
        //where TEdgeExchange        : IGenericEdge     <TIdVertex, TRevisionIdVertex, TVertexData,  TIdEdge, TRevisionIdEdge, TEdgeData,  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData>
        //where THyperEdgeExchange   : IGenericHyperEdge<TIdVertex, TRevisionIdVertex, TVertexData,  TIdEdge, TRevisionIdEdge, TEdgeData,  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData>

    {

        #region Vertex methods

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
        TVertexExchange AddVertex(TIdVertex VertexId = default(TIdVertex), Action<TVertexData> VertexInitializer = null);

        TVertexExchange AddVertex(TVertexExchange myVertexId);


        /// <summary>
        /// Return the vertex referenced by the given vertex identifier.
        /// If no vertex is referenced by that identifier, then return null.
        /// </summary>
        /// <param name="myVertexId">The identifier of the vertex.</param>
        /// <returns>The vertex referenced by the provided identifier or null when no such edge exists.</returns>
        TVertexExchange GetVertex(TIdVertex myVertexId);


        /// <summary>
        /// Return a collection of vertices referenced by the given array of vertex identifiers.
        /// </summary>
        /// <param name="myVertexIds">An array of vertex identifiers.</param>
        IEnumerable<TVertexExchange> GetVertices(params TIdVertex[] myVertexIds);


        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// An additional vertex filter may be applied for filtering.
        /// </summary>
        /// <param name="myVertexFilter">A delegate for vertex filtering.</param>
        IEnumerable<TVertexExchange> GetVertices(Func<TVertexExchange, Boolean> myVertexFilter = null);


        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// </summary>
        IEnumerable<TVertexExchange> Vertices { get; }


        /// <summary>
        /// Remove the provided vertex from the graph.
        /// Upon removing the vertex, all the edges by which the vertex is connected will be removed as well.
        /// </summary>
        /// <param name="myIVertex">The vertex to be removed from the graph</param>
        void RemoveVertex(TVertexExchange myIVertex);

        #endregion

        #region Edge methods

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
        TEdgeExchange AddEdge(TVertexExchange myOutVertex, TVertexExchange myInVertex, TIdEdge EdgeId = default(TIdEdge), String Label = null, Action<TEdgeData> EdgeInitializer = null);


        /// <summary>
        /// Return the edge referenced by the given edge identifier.
        /// If no edge is referenced by that identifier, then return null.
        /// </summary>
        /// <param name="myEdgeId">The identifier of the edge.</param>
        /// <returns>The edge referenced by the provided identifier or null when no such edge exists.</returns>
        TEdgeExchange GetEdge(TIdEdge myEdgeId);


        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// </summary>
        IEnumerable<TEdgeExchange> Edges { get; }


        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// An additional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="myEdgeIds">An array of edge identifiers.</param>
        IEnumerable<TEdgeExchange> GetEdges(params TIdEdge[] myEdgeIds);


        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// An additional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="myEdgeFilter">A delegate for edge filtering.</param>
        IEnumerable<TEdgeExchange> GetEdges(Func<TEdgeExchange, Boolean> myEdgeFilter = null);


        /// <summary>
        /// Remove the provided edge from the graph.
        /// </summary>
        /// <param name="myIEdge">The edge to be removed from the graph</param>
        void RemoveEdge(TEdgeExchange myIEdge);

        #endregion

        #region Utils

        /// <summary>
        /// Remove all the edges and vertices from the graph.
        /// </summary>
        void Clear();


        /// <summary>
        /// A shutdown function is required to properly close the graph.
        /// This is important for implementations that utilize disk-based serializations.
        /// </summary>
        void Shutdown();

        #endregion

    }

}
