/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Blueprints.NET <http://www.github.com/Vanaheimr/Blueprints.NET>
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

namespace de.ahzf.Vanaheimr.Blueprints
{

    /// <summary>
    /// A standardized property graph.
    /// </summary>
    public interface IPropertyGraph : IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                            UInt64, Int64, String, String, Object,
                                                            UInt64, Int64, String, String, Object,
                                                            UInt64, Int64, String, String, Object>
    {

        #region Vertex methods

        #region AddVertex(VertexInitializer = null)

        /// <summary>
        /// Create a new vertex, add it to the graph, and return the newly created vertex.
        /// If the object identifier is already being used by the graph to reference a vertex,
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="VertexInitializer">A delegate to initialize the new vertex.</param>
        /// <returns>The newly created vertex as IPropertyVertex&lt;...&gt;.</returns>
        new IPropertyVertex AddVertex(VertexInitializer<UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object> VertexInitializer = null);

        #endregion

        #region AddVertex(VertexLabel, VertexInitializer = null)

        /// <summary>
        /// Create a new vertex, add it to the graph, and return the newly created vertex.
        /// If the object identifier is already being used by the graph to reference a vertex,
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="VertexLabel">The label (or type) of a vertex.</param>
        /// <param name="VertexInitializer">A delegate to initialize the new vertex.</param>
        /// <returns>The newly created vertex as IPropertyVertex&lt;...&gt;.</returns>
        new IPropertyVertex AddVertex(String VertexLabel,
                                      VertexInitializer<UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object> VertexInitializer = null);

        #endregion

        #region AddVertex(VertexId, VertexLabel, VertexInitializer = null)

        /// <summary>
        /// Create a new vertex, add it to the graph, and return the newly created vertex.
        /// If the object identifier is already being used by the graph to reference a vertex,
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="VertexId">The vertex identifier.</param>
        /// <param name="VertexLabel">The label (or type) of the vertex.</param>
        /// <param name="VertexInitializer">A delegate to initialize the new vertex.</param>
        /// <returns>The newly created vertex as IPropertyVertex&lt;...&gt;.</returns>
        new IPropertyVertex AddVertex(UInt64 VertexId,
                                      String VertexLabel,
                                      VertexInitializer<UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object> VertexInitializer = null);

        #endregion

        #region AddVertex(Vertex)

        /// <summary>
        /// Adds the given vertex to the graph, and returns it again.
        /// An exception will be thrown if the vertex identifier is already being
        /// used by the graph to reference another vertex.
        /// </summary>
        /// <param name="Vertex">A vertex.</param>
        /// <returns>The given vertex.</returns>
        new IPropertyVertex AddVertex(IPropertyVertex Vertex);

        #endregion


        #region VertexById(VertexId)

        /// <summary>
        /// Return the vertex referenced by the given vertex identifier.
        /// If no vertex is referenced by the identifier return null.
        /// </summary>
        /// <param name="VertexId">A vertex identifier.</param>
        new IPropertyVertex VertexById(UInt64 VertexId);

        #endregion

        #region VerticesById(params VertexIds)

        /// <summary>
        /// Return the vertices referenced by the given array of vertex identifiers.
        /// If no vertex is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="VertexIds">An array of vertex identifiers.</param>
        new IEnumerable<IPropertyVertex> VerticesById(params UInt64[] VertexIds);

        #endregion

        #region VerticesByLabel(params VertexLabels)

        /// <summary>
        /// Return an enumeration of all vertices having one of the
        /// given vertex labels.
        /// </summary>
        /// <param name="VertexLabels">An array of vertex labels.</param>
        new IEnumerable<IPropertyVertex> VerticesByLabel(params String[] VertexLabels);

        #endregion

        #region Vertices(VertexFilter = null)

        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// An optional vertex filter may be applied for filtering.
        /// </summary>
        /// <param name="VertexFilter">A delegate for vertex filtering.</param>
        new IEnumerable<IPropertyVertex> Vertices(VertexFilter<UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object> VertexFilter = null);

        #endregion

        #region NumberOfVertices(VertexFilter = null)

        /// <summary>
        /// Return the current number of vertices which match the given optional filter.
        /// When the filter is null, this method should implement an optimized
        /// way to get the currenty number of vertices.
        /// </summary>
        /// <param name="VertexFilter">A delegate for vertex filtering.</param>
        new UInt64 NumberOfVertices(VertexFilter<UInt64, Int64, String, String, Object,
                                                 UInt64, Int64, String, String, Object,
                                                 UInt64, Int64, String, String, Object,
                                                 UInt64, Int64, String, String, Object> VertexFilter = null);

        #endregion


        #region RemoveVerticesById(params VertexIds)

        /// <summary>
        /// Remove the vertices identified by their VertexIds.
        /// </summary>
        /// <param name="VertexIds">An array of VertexIds of the vertices to remove.</param>
        new void RemoveVerticesById(params UInt64[] VertexIds);

        #endregion

        #region RemoveVertices(Vertices)

        /// <summary>
        /// Remove the given array of vertices from the graph.
        /// Upon removing a vertex, all the edges by which the vertex
        /// is connected will be removed as well.
        /// </summary>
        /// <param name="Vertices">An array of vertices to be removed from the graph.</param>
        new void RemoveVertices(params IPropertyVertex[] Vertices);

        #endregion

        #region RemoveVertices(VertexFilter = null)

        /// <summary>
        /// Remove any vertex matching the given vertex filter.
        /// </summary>
        /// <param name="VertexFilter">A delegate for vertex filtering.</param>
        new void RemoveVertices(VertexFilter<UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object> VertexFilter = null);
        
        #endregion

        #endregion

        #region Edge methods

        #region AddEdge(OutVertex, Label, InVertex, EdgeInitializer = null)

        /// <summary>
        /// Add an edge to the graph. The added edge requires a tail vertex,
        /// a head vertex, a label and initializes the edge
        /// by invoking the given EdgeInitializer.
        /// OutVertex --Label-> InVertex is the "Semantic Web Notation" ;)
        /// </summary>
        /// <param name="OutVertex">The vertex on the tail of the edge.</param>
        /// <param name="Label">The label associated with the edge.</param>
        /// <param name="InVertex">The vertex on the head of the edge.</param>
        /// <param name="EdgeInitializer">A delegate to initialize the new edge.</param>
        /// <returns>The new edge.</returns>
        new IPropertyEdge AddEdge(IPropertyVertex OutVertex,
                                  String          Label,
                                  IPropertyVertex InVertex,
                                  EdgeInitializer<UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object> EdgeInitializer = null);

        #endregion

        #region AddEdge(EdgeId, OutVertex, Label, InVertex, EdgeInitializer = null)

        /// <summary>
        /// Add an edge to the graph. The added edge requires a tail vertex,
        /// a head vertex, an identifier, a label and initializes the edge
        /// by invoking the given EdgeInitializer.
        /// OutVertex --Label-> InVertex is the "Semantic Web Notation" ;)
        /// </summary>
        /// <param name="EdgeId">A EdgeId. If none was given a new one will be generated.</param>
        /// <param name="OutVertex">The vertex on the tail of the edge.</param>
        /// <param name="Label">The label associated with the edge.</param>
        /// <param name="InVertex">The vertex on the head of the edge.</param>
        /// <param name="EdgeInitializer">A delegate to initialize the new edge.</param>
        /// <returns>The new edge.</returns>
        new IPropertyEdge AddEdge(UInt64          EdgeId,
                                  IPropertyVertex OutVertex,
                                  String          Label,
                                  IPropertyVertex InVertex,
                                  EdgeInitializer<UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object> EdgeInitializer = null);

        #endregion

        #region AddEdge(OutVertex, InVertex, Label = default(TEdgeLabel), EdgeInitializer = null)

        /// <summary>
        /// Add an edge to the graph. The added edge requires a tail vertex,
        /// a head vertex, an identifier, a label and initializes the edge
        /// by invoking the given EdgeInitializer.
        /// </summary>
        /// <param name="OutVertex">The vertex on the tail of the edge.</param>
        /// <param name="InVertex">The vertex on the head of the edge.</param>
        /// <param name="Label">The label associated with the edge.</param>
        /// <param name="EdgeInitializer">A delegate to initialize the new edge.</param>
        /// <returns>The new edge.</returns>
        new IPropertyEdge AddEdge(IPropertyVertex OutVertex,
                                  IPropertyVertex InVertex,
                                  String          Label  = default(String),
                                  EdgeInitializer<UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object> EdgeInitializer = null);

        #endregion

        #region AddEdge(OutVertex, InVertex, EdgeId, Label  = default(TEdgeLabel), EdgeInitializer = null)

        /// <summary>
        /// Add an edge to the graph. The added edge requires a tail vertex,
        /// a head vertex, an identifier, a label and initializes the edge
        /// by invoking the given EdgeInitializer.
        /// </summary>
        /// <param name="OutVertex">The vertex on the tail of the edge.</param>
        /// <param name="InVertex">The vertex on the head of the edge.</param>
        /// <param name="EdgeId">A EdgeId. If none was given a new one will be generated.</param>
        /// <param name="Label">The label associated with the edge.</param>
        /// <param name="EdgeInitializer">A delegate to initialize the new edge.</param>
        /// <returns>The new edge.</returns>
        new IPropertyEdge AddEdge(IPropertyVertex OutVertex,
                                  IPropertyVertex InVertex,
                                  UInt64          EdgeId,
                                  String          Label = default(String),
                                  EdgeInitializer<UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object> EdgeInitializer = null);

        #endregion

        #region AddEdge(IPropertyEdge)

        /// <summary>
        /// Add the given edge to the graph, and returns it again.
        /// An exception will be thrown if the edge identifier is already being
        /// used by the graph to reference another edge.
        /// </summary>
        /// <param name="IPropertyEdge">An IPropertyEdge.</param>
        /// <returns>The given IPropertyEdge.</returns>
        new IPropertyEdge AddEdge(IPropertyEdge IPropertyEdge);

        #endregion


        #region EdgeById(EdgeId)

        /// <summary>
        /// Return the edge referenced by the given edge identifier.
        /// If no edge is referenced by a given identifier return null.
        /// </summary>
        /// <param name="EdgeId">An edge identifier.</param>
        new IPropertyEdge EdgeById(UInt64 EdgeId);

        #endregion

        #region EdgesById(params EdgeIds)

        /// <summary>
        /// Return the edges referenced by the given array of edge identifiers.
        /// If no edge is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="EdgeIds">An array of edge identifiers.</param>
        new IEnumerable<IPropertyEdge> EdgesById(params UInt64[] EdgeIds);

        #endregion

        #region EdgesByLabel(params EdgeLabels)

        /// <summary>
        /// Return an enumeration of all edges having one of the
        /// given edge labels.
        /// </summary>
        /// <param name="EdgeLabels">An array of edge labels.</param>
        new IEnumerable<IPropertyEdge> EdgesByLabel(params String[] EdgeLabels);

        #endregion

        #region Edges(EdgeFilter = null)

        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// An optional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        new IEnumerable<IPropertyEdge> Edges(EdgeFilter<UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object> EdgeFilter = null);

        #endregion

        #region NumberOfEdges(EdgeFilter = null)

        /// <summary>
        /// Return the current number of edges matching the given optional edge filter.
        /// When the filter is null, this method should implement an optimized
        /// way to get the currenty number of edges.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        new UInt64 NumberOfEdges(EdgeFilter<UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object> EdgeFilter = null);

        #endregion


        #region RemoveEdgesById(params EdgeIds)

        /// <summary>
        /// Remove the given array of edges identified by their EdgeIds.
        /// </summary>
        /// <param name="EdgeIds">An array of EdgeIds of the edges to remove.</param>
        new void RemoveEdgesById(params UInt64[] EdgeIds);

        #endregion

        #region RemoveEdges(params Edges)

        /// <summary>
        /// Remove the given array of edges from the graph.
        /// </summary>
        /// <param name="Edges">An array of edges to be removed from the graph.</param>
        new void RemoveEdges(params IPropertyEdge[] Edges);

        #endregion

        #region RemoveEdges(EdgeFilter = null)

        /// <summary>
        /// Remove any edge matching the given edge filter.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        new void RemoveEdges(EdgeFilter<UInt64, Int64, String, String, Object,
                                        UInt64, Int64, String, String, Object,
                                        UInt64, Int64, String, String, Object,
                                        UInt64, Int64, String, String, Object> EdgeFilter = null);

        #endregion

        #endregion

    }

}
