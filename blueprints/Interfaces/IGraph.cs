/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using System;
using System.Collections.Generic;
using de.ahzf.blueprints.Datastructures;

#endregion

namespace de.ahzf.blueprints
{

    /// <summary>
    /// A property graph is a container object for a collection of vertices and edges.
    /// </summary>
    public interface IGraph
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
        /// <param name="myVertexId">the recommended object identifier.</param>
        /// <param name="myVertexInitializer">A delegate to initialize the new vertex.</param>
        /// <returns>the newly created vertex or the vertex already referenced by the provided identifier.</returns>
        IVertex AddVertex(VertexId myVertexId = null, Action<IVertex> myVertexInitializer = null);


        /// <summary>
        /// Return the vertex referenced by the given vertex identifier.
        /// If no vertex is referenced by that identifier, then return null.
        /// </summary>
        /// <param name="myVertexId">The identifier of the vertex.</param>
        /// <returns>The vertex referenced by the provided identifier or null when no such edge exists.</returns>
        IVertex GetVertex(VertexId myVertexId);


        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// An additional vertex filter may be applied for filtering.
        /// </summary>
        /// <param name="myVertexFilter">A delegate for vertex filtering.</param>
        IEnumerable<IVertex> GetVertices(Func<IVertex, Boolean> myVertexFilter = null);


        /// <summary>
        /// Remove the provided vertex from the graph.
        /// Upon removing the vertex, all the edges by which the vertex is connected will be removed as well.
        /// </summary>
        /// <param name="myIVertex">vertex the vertex to remove from the graph</param>
        void RemoveVertex(IVertex myIVertex);





        /// <summary>
        /// Add an edge to the graph. The added edges requires a recommended identifier, a tail vertex, an head vertex, and a label.
        /// Like adding a vertex, the provided object identifier is can be ignored by the implementation.
        /// </summary>
        /// <param name="myEdgeId">the recommended object identifier.</param>
        /// <param name="myOutVertex">the vertex on the tail of the edge.</param>
        /// <param name="myInVertex">the vertex on the head of the edge.</param>
        /// <param name="myLabel">the label associated with the edge.</param>
        /// <param name="myEdgeInitializer">A delegate to initialize the new edge.</param>
        /// <returns>the newly created edge</returns>
        IEdge AddEdge(IVertex myOutVertex, IVertex myInVertex, EdgeId myEdgeId = null, String myLabel = null, Action<IEdge> myEdgeInitializer = null);


        /// <summary>
        /// Return the edge referenced by the given edge identifier.
        /// If no edge is referenced by that identifier, then return null.
        /// </summary>
        /// <param name="myEdgeId">The identifier of the edge.</param>
        /// <returns>The edge referenced by the provided identifier or null when no such edge exists.</returns>
        IEdge GetEdge(EdgeId myEdgeId);


        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// An additional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="myEdgeFilter">A delegate for edge filtering.</param>
        IEnumerable<IEdge> GetEdges(Func<IEdge, Boolean> myEdgeFilter = null);


        /// <summary>
        /// Remove the provided edge from the graph.
        /// </summary>
        /// <param name="myIEdge">the edge to be removed from the graph</param>
        void RemoveEdge(IEdge myIEdge);





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
