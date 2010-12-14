/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using System;
using System.Collections.Generic;

#endregion

namespace de.ahzf.blueprints
{

    /// <summary>
    /// A graph is a container object for a collection of vertices and edges.
    /// </summary>
    public interface IGraph
    {

     //   **
     //* Create a new vertex, add it to the graph, and return the newly created vertex.
     //* The provided object identifier is a recommendation for the identifier to use.
     //* It is not required that the implementation use this identifier.
     //* If the object identifier is already being used by the graph to reference a vertex,
     //* then that reference vertex is returned and no vertex is created.
     //* If the identifier is a vertex (perhaps from another graph),
     //* then the vertex is duplicated for this graph. Thus, a vertex can not be an identifier.
     //*
     //* @param id the recommended object identifier
     //* @return the newly created vertex or the vertex already referenced by the provided identifier.
     //*/
        IVertex addVertex(Object id);

    /**
     * Return the vertex referenced by the provided object identifier.
     * If no vertex is referenced by that identifier, then return null.
     *
     * @param id the identifier of the vertex to retrieved from the graph
     * @return the vertex referenced by the provided identifier or null when no such vertex exists
     */
        IVertex GetVertex(Object id);


        /// <summary>
        /// Remove the provided vertex from the graph.
        /// Upon removing the vertex, all the edges by which the vertex is connected will be removed as well.
        /// </summary>
        /// <param name="myIVertex">vertex the vertex to remove from the graph</param>
        void RemoveVertex(IVertex myIVertex);


        /// <summary>
        /// Return an iterable reference to all the vertices in the graph. Thus, what is returned can be subjected to the foreach construct.
        /// If this is not possible for the implementation, then an UnsupportedOperationException can be thrown.
        /// </summary>
        IEnumerable<IVertex> Vertices { get; }


        /// <summary>
        /// Add an edge to the graph. The added edges requires a recommended identifier, a tail vertex, an head vertex, and a label.
        /// Like adding a vertex, the provided object identifier is can be ignored by the implementation.
        /// </summary>
        /// <param name="id">the recommended object identifier</param>
        /// <param name="outVertex">the vertex on the tail of the edge</param>
        /// <param name="inVertex">the vertex on the head of the edge</param>
        /// <param name="label">the label associated with the edge</param>
        /// <returns>the newly created edge</returns>
        IEdge AddEdge(Object id, IVertex outVertex, IVertex inVertex, String label);


        /// <summary>
        /// Return the edge referenced by the provided object identifier.
        /// If no edge is referenced by that identifier, then return null.
        /// </summary>
        /// <param name="id">the identifier of the edge to retrieved from the graph</param>
        /// <returns>the edge referenced by the provided identifier or null when no such edge exists</returns>
        IEdge GetEdge(Object myId);


        /// <summary>
        /// Remove the provided edge from the graph.
        /// </summary>
        /// <param name="myIEdge">the edge to be removed from the graph</param>
        void RemoveEdge(IEdge myIEdge);


        /// <summary>
        /// Return an iterable reference to all the edges in the graph. Thus, what is returned can be subjected to the foreach construct.
        /// If this is not possible for the implementation, then an UnsupportedOperationException can be thrown.
        /// </summary>
        IEnumerable<IEdge> Edges { get; }


        /// <summary>
        /// Remove all the edges and vertices from the graph.
        /// </summary>
        void Clear();


        /// <summary>
        /// A shtudown function is required to properly close the graph.
        /// This is important for implementations that utilize disk-based serializations.
        /// </summary>
        void Shutdown();


    }

}
