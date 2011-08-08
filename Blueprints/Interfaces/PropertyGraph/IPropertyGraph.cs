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

using de.ahzf.Blueprints.PropertyGraph.Indices;

#endregion

namespace de.ahzf.Blueprints.PropertyGraph
{

    #region IPropertyGraph

    /// <summary>
    /// A standardized property graph.
    /// </summary>
    public interface IPropertyGraph : IPropertyGraph<VertexId,    RevisionId,         String, Object, IDictionary<String, Object>,
                                                     EdgeId,      RevisionId, String, String, Object, IDictionary<String, Object>,
                                                     HyperEdgeId, RevisionId, String, String, Object, IDictionary<String, Object>>

    { }

    #endregion

    #region IPropertyGraph<...>

    /// <summary>
    /// A generic property graph.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TKeyVertex">The type of the vertex property keys.</typeparam>
    /// <typeparam name="TValueVertex">The type of the vertex property values.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
    /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
    /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
    public interface IPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                                    : IReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,
        
                                      IGraphElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex>,

                                      IGraphIndexing<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                      IPropertyGraphNotifications<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                      IEquatable<IPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>,

                                      IComparable<IPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>,

                                      IComparable

        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

        where TEdgeLabel           : IEquatable<TEdgeLabel>,           IComparable<TEdgeLabel>,           IComparable
        where THyperEdgeLabel      : IEquatable<THyperEdgeLabel>,      IComparable<THyperEdgeLabel>,      IComparable

        where TKeyVertex           : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge             : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyHyperEdge        : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

    {

        #region Vertex methods

        #region AddVertex(VertexInitializer = null)

        /// <summary>
        /// Create a new vertex, add it to the graph, and return the newly created vertex.
        /// If the object identifier is already being used by the graph to reference a vertex,
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="VertexId">The vertex identifier.</param>
        /// <param name="VertexInitializer">A delegate to initialize the new vertex.</param>
        /// <returns>The newly created vertex as IPropertyVertex&lt;...&gt;.</returns>
        IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                        AddVertex(VertexInitializer<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexInitializer = null);

        #endregion

        #region AddVertex(VertexId, VertexInitializer = null)

        /// <summary>
        /// Create a new vertex, add it to the graph, and return the newly created vertex.
        /// If the object identifier is already being used by the graph to reference a vertex,
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="VertexId">The vertex identifier.</param>
        /// <param name="VertexInitializer">A delegate to initialize the new vertex.</param>
        /// <returns>The newly created vertex as IPropertyVertex&lt;...&gt;.</returns>
        IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                        AddVertex(TIdVertex VertexId,
                                  VertexInitializer<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexInitializer = null);

        #endregion

        #region AddVertex(Vertex)

        /// <summary>
        /// Add the given vertex to the graph, and return the newly created vertex.
        /// If the object identifier is already being used by the graph to reference a vertex,
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="Vertex">An IPropertyVertex.</param>
        /// <returns>The given IPropertyVertex.</returns>
        IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                        AddVertex(IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex);

        #endregion


        #region RemoveVerticesById(params VertexIds)

        /// <summary>
        /// Remove the vertices identified by their VertexIds.
        /// </summary>
        /// <param name="VertexIds">An array of VertexIds of the vertices to remove.</param>
        void RemoveVerticesById(params TIdVertex[] VertexIds);

        #endregion

        #region RemoveVertices(Vertices)

        /// <summary>
        /// Remove the given array of vertices from the graph.
        /// Upon removing a vertex, all the edges by which the vertex
        /// is connected will be removed as well.
        /// </summary>
        /// <param name="Vertices">An array of vertices to be removed from the graph.</param>
        void RemoveVertices(params IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices);

        #endregion

        #region RemoveVertices(VertexFilter = null)

        /// <summary>
        /// Remove any vertex matching the given vertex filter.
        /// </summary>
        /// <param name="VertexFilter">A delegate for vertex filtering.</param>
        void RemoveVertices(VertexFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexFilter = null);
        
        #endregion

        #endregion

        #region Edge methods

        #region AddEdge(OutVertex, InVertex, Label  = default(TEdgeLabel), EdgeInitializer = null)

        /// <summary>
        /// Add an edge to the graph. The added edge requires a tail vertex,
        /// a head vertex, an identifier, a label and initializes the edge
        /// by invoking the given EdgeInitializer.
        /// </summary>
        /// <param name="OutVertex">The vertex on the tail of the edge.</param>
        /// <param name="InVertex">The vertex on the head of the edge.</param>
        /// <param name="Label">The label associated with the edge.</param>
        /// <param name="EdgeInitializer">A delegate to initialize the new edge.</param>
        /// <returns>The newly created edge</returns>
        IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                      AddEdge(IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

                              IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

                              TEdgeLabel Label  = default(TEdgeLabel),

                              EdgeInitializer<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer = null);

        #endregion

        #region AddEdge(OutVertex, InVertex, EdgeId, Label  = default(TEdgeLabel), EdgeInitializer = null)

        /// <summary>
        /// Add an edge to the graph. The added edge requires a tail vertex,
        /// a head vertex, an identifier, a label and initializes the edge
        /// by invoking the given EdgeInitializer.
        /// </summary>
        /// <param name="OutVertex">The vertex on the tail of the edge.</param>
        /// <param name="InVertex">The vertex on the head of the edge.</param>
        /// <param name="EdgeId">The recommended object identifier.</param>
        /// <param name="Label">The label associated with the edge.</param>
        /// <param name="EdgeInitializer">A delegate to initialize the new edge.</param>
        /// <returns>The newly created edge</returns>
        IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                      AddEdge(IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

                              IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

                              TIdEdge    EdgeId,
                              TEdgeLabel Label  = default(TEdgeLabel),

                              EdgeInitializer<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer = null);

        #endregion


        #region RemoveEdgesById(params EdgeIds)

        /// <summary>
        /// Remove the given array of edges identified by their EdgeIds.
        /// </summary>
        /// <param name="EdgeIds">An array of EdgeIds of the edges to remove.</param>
        void RemoveEdgesById(params TIdEdge[] EdgeIds);

        #endregion

        #region RemoveEdges(params Edges)

        /// <summary>
        /// Remove the given array of edges from the graph.
        /// </summary>
        /// <param name="Edges">An array of edges to be removed from the graph.</param>
        void RemoveEdges(params IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Edges);

        #endregion

        #region RemoveEdges(EdgeFilter = null)

        /// <summary>
        /// Remove any edge matching the given edge filter.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        void RemoveEdges(EdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter = null);

        #endregion

        #endregion

        #region HyperEdge methods

        #endregion

        #region Graph Maintenance

        /// <summary>
        /// Remove all the vertices, edges and hyperedges from the graph.
        /// </summary>
        void Clear();

        /// <summary>
        /// A shutdown function is required to properly close the graph.
        /// This is important for implementations that utilize disk-based serializations.
        /// </summary>
        /// <param name="Message">An optional message, e.g. a reason for the shutdown.</param>
        void Shutdown(String Message = "");

        #endregion

    }

    #endregion

    #region IPropertyGraph<..., TDatastructureXXX>

    /// <summary>
    /// Generic property graph helper interface.
    /// </summary>
    public interface IPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>

                                    : IPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

        where TIdVertex               : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge                 : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdHyperEdge            : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TRevisionIdVertex       : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge         : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdHyperEdge    : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

        where TEdgeLabel              : IEquatable<TEdgeLabel>,           IComparable<TEdgeLabel>,           IComparable
        where THyperEdgeLabel         : IEquatable<THyperEdgeLabel>,      IComparable<THyperEdgeLabel>,      IComparable

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

        where TDatastructureVertex    : IDictionary<TKeyVertex,    TValueVertex>
        where TDatastructureEdge      : IDictionary<TKeyEdge,      TValueEdge>
        where TDatastructureHyperEdge : IDictionary<TKeyHyperEdge, TValueHyperEdge>

    { }

    #endregion

}
