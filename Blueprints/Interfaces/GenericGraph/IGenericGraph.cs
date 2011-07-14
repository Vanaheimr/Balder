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

#endregion

namespace de.ahzf.Blueprints.GenericGraph
{

    #region IGenericGraph

    /// <summary>
    /// A graph is a container object for a collection of vertices, edges and hyperedges.
    /// </summary>
    public interface IGenericGraph
    {

        /// <summary>
        /// Remove all the vertices, edges and hyperedges from the graph.
        /// </summary>
        void Clear();


        /// <summary>
        /// A shutdown function is required to properly close the graph.
        /// This is important for implementations that utilize disk-based serializations.
        /// </summary>
        void Shutdown();

    }

    #endregion

    #region IGenericGraph<...>

    /// <summary>
    /// A generic graph is a container object for a collection of vertices, edges and hyperedges.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TDataVertex">The type of the embedded vertex data.</typeparam>
    /// <typeparam name="TVertex">The type of the vertices.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TDataEdge">The type of the embedded edge data.</typeparam>
    /// <typeparam name="TEdge">The type of the edges.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TDataHyperEdge">The type of the embedded hyperedge data.</typeparam>
    /// <typeparam name="THyperEdge">The type of the hyperedges.</typeparam>
    public interface IGenericGraph<TIdVertex,    TRevisionIdVertex,                     TDataVertex,    TVertex,    
                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,      TEdge,
                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge, THyperEdge>

                                   : IGenericElement<TIdVertex, TRevisionIdVertex, TDataVertex>,
                                     IGenericGraph

        where TVertex              : IGenericVertex   <TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>

        where TEdge                : IGenericEdge     <TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>
        
        where THyperEdge           : IGenericHyperEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>

        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable
        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable
        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable

        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable
        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable
        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable

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
        TVertex AddVertex(TIdVertex VertexId = default(TIdVertex), Action<TDataVertex> VertexInitializer = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="VertexId"></param>
        /// <returns></returns>
        TVertex AddVertex(TVertex VertexId);


        /// <summary>
        /// Return the vertex referenced by the given vertex identifier.
        /// If no vertex is referenced by that identifier, then return null.
        /// </summary>
        /// <param name="myVertexId">The identifier of the vertex.</param>
        /// <returns>The vertex referenced by the provided identifier or null when no such edge exists.</returns>
        TVertex GetVertex(TIdVertex myVertexId);


        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// </summary>
        IEnumerable<TVertex> Vertices { get; }


        /// <summary>
        /// Return a collection of vertices referenced by the given array of vertex identifiers.
        /// </summary>
        /// <param name="myVertexIds">An array of vertex identifiers.</param>
        IEnumerable<TVertex> GetVertices(params TIdVertex[] myVertexIds);


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
        TEdge AddEdge(TVertex myOutVertex, TVertex myInVertex, TIdEdge EdgeId = default(TIdEdge), String Label = null, Action<TDataEdge> EdgeInitializer = null);


        /// <summary>
        /// Return the edge referenced by the given edge identifier.
        /// If no edge is referenced by that identifier, then return null.
        /// </summary>
        /// <param name="myEdgeId">The identifier of the edge.</param>
        /// <returns>The edge referenced by the provided identifier or null when no such edge exists.</returns>
        TEdge GetEdge(TIdEdge myEdgeId);


        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// </summary>
        IEnumerable<TEdge> Edges { get; }


        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// An additional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="myEdgeIds">An array of edge identifiers.</param>
        IEnumerable<TEdge> GetEdges(params TIdEdge[] myEdgeIds);


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

        #endregion

        #region HyperEdge methods

        // yet to come!

        #endregion

    }

    #endregion

}
