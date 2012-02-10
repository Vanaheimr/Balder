/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <code@ahzf.de>
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

namespace de.ahzf.Blueprints.PropertyGraphs
{

    /// <summary>
    /// The interface of a generic property graph.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TVertexLabel">The type of the vertex type.</typeparam>
    /// <typeparam name="TKeyVertex">The type of the vertex property keys.</typeparam>
    /// <typeparam name="TValueVertex">The type of the vertex property values.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
    /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
    /// 
    /// <typeparam name="TIdMultiEdge">The type of the multiedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdMultiEdge">The type of the multiedge revision identifiers.</typeparam>
    /// <typeparam name="TMultiEdgeLabel">The type of the multiedge label.</typeparam>
    /// <typeparam name="TKeyMultiEdge">The type of the multiedge property keys.</typeparam>
    /// <typeparam name="TValueMultiEdge">The type of the multiedge property values.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
    /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
    public interface IGenericPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                                           : IGraphElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex>,
                                             IComparable

        where TIdVertex               : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge                 : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdMultiEdge            : IEquatable<TIdMultiEdge>,         IComparable<TIdMultiEdge>,         IComparable, TValueMultiEdge
        where TIdHyperEdge            : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TRevisionIdVertex       : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge         : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdMultiEdge    : IEquatable<TRevisionIdMultiEdge>, IComparable<TRevisionIdMultiEdge>, IComparable, TValueMultiEdge
        where TRevisionIdHyperEdge    : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

        where TVertexLabel            : IEquatable<TVertexLabel>,         IComparable<TVertexLabel>,         IComparable
        where TEdgeLabel              : IEquatable<TEdgeLabel>,           IComparable<TEdgeLabel>,           IComparable
        where TMultiEdgeLabel         : IEquatable<TMultiEdgeLabel>,      IComparable<TMultiEdgeLabel>,      IComparable
        where THyperEdgeLabel         : IEquatable<THyperEdgeLabel>,      IComparable<THyperEdgeLabel>,      IComparable

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyMultiEdge           : IEquatable<TKeyMultiEdge>,        IComparable<TKeyMultiEdge>,        IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

    {

        #region Vertex methods

        #region OnVertexAdding

        /// <summary>
        /// Called whenever a vertex will be added to the property graph.
        /// </summary>
        event VertexAddingEventHandler<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnVertexAdding;

        #endregion

        #region AddVertex(VertexInitializer = null)

        /// <summary>
        /// Create a new vertex, add it to the graph, and return the newly created vertex.
        /// If the object identifier is already being used by the graph to reference a vertex,
        /// then an exception will be thrown.
        /// </summary>
        /// <param name="VertexInitializer">A delegate to initialize the new vertex.</param>
        /// <returns>The newly created vertex as IPropertyVertex&lt;...&gt;.</returns>
        IGenericPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                               AddVertex(VertexInitializer<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
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
        IGenericPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                               AddVertex(TIdVertex VertexId,
                                         VertexInitializer<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
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
        IGenericPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                               AddVertex(IGenericPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex);

        #endregion

        #region OnVertexAdded

        /// <summary>
        /// Called whenever a vertex was added to the property graph.
        /// </summary>
        event VertexAddedEventHandler<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnVertexAdded;

        #endregion


        #region VertexById(VertexId)

        /// <summary>
        /// Return the vertex referenced by the given vertex identifier.
        /// If no vertex is referenced by the identifier return null.
        /// </summary>
        /// <param name="VertexId">A vertex identifier.</param>
        IGenericPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                               VertexById(TIdVertex VertexId);

        #endregion

        #region VerticesById(params VertexIds)

        /// <summary>
        /// Return the vertices referenced by the given array of vertex identifiers.
        /// If no vertex is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="VertexIds">An array of vertex identifiers.</param>
        IEnumerable<IGenericPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                                           VerticesById(params TIdVertex[] VertexIds);

        #endregion

        #region VerticesByLabel(params VertexLabels)

        /// <summary>
        /// Return an enumeration of all vertices having one of the
        /// given vertex labels.
        /// </summary>
        /// <param name="VertexLabels">An array of vertex labels.</param>
        IEnumerable<IGenericPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                                           VerticesByLabel(params TVertexLabel[] VertexLabels);

        #endregion

        #region Vertices(VertexFilter = null)

        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// An optional vertex filter may be applied for filtering.
        /// </summary>
        /// <param name="VertexFilter">A delegate for vertex filtering.</param>
        IEnumerable<IGenericPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                                           Vertices(VertexFilter<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexFilter = null);

        #endregion

        #region NumberOfVertices(VertexFilter = null)

        /// <summary>
        /// Return the current number of vertices which match the given optional filter.
        /// When the filter is null, this method should implement an optimized
        /// way to get the currenty number of vertices.
        /// </summary>
        /// <param name="VertexFilter">A delegate for vertex filtering.</param>
        UInt64 NumberOfVertices(VertexFilter<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexFilter = null);

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
        void RemoveVertices(params IGenericPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices);

        #endregion

        #region RemoveVertices(VertexFilter = null)

        /// <summary>
        /// Remove any vertex matching the given vertex filter.
        /// </summary>
        /// <param name="VertexFilter">A delegate for vertex filtering.</param>
        void RemoveVertices(VertexFilter<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexFilter = null);
        
        #endregion

        #endregion

        #region Edge methods

        #region OnEdgeAdding

        /// <summary>
        /// Called whenever an edge will be added to the property graph.
        /// </summary>
        event EdgeAddingEventHandler<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnEdgeAdding;

        #endregion

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
        IGenericPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                             AddEdge(IGenericPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

                                     TEdgeLabel Label,

                                     IGenericPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

                                     EdgeInitializer       <TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer = null);

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
        IGenericPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                             AddEdge(TIdEdge    EdgeId,

                                     IGenericPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

                                     TEdgeLabel Label,

                                     IGenericPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

                                     EdgeInitializer       <TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer = null);

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
        IGenericPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                             AddEdge(IGenericPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

                                     IGenericPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

                                     TEdgeLabel Label  = default(TEdgeLabel),

                                     EdgeInitializer       <TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
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
        /// <param name="EdgeId">A EdgeId. If none was given a new one will be generated.</param>
        /// <param name="Label">The label associated with the edge.</param>
        /// <param name="EdgeInitializer">A delegate to initialize the new edge.</param>
        /// <returns>The new edge.</returns>
        IGenericPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                             AddEdge(IGenericPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

                                     IGenericPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

                                     TIdEdge    EdgeId,
                                     TEdgeLabel Label  = default(TEdgeLabel),

                                     EdgeInitializer       <TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer = null);

        #endregion

        #region OnEdgeAdded

        /// <summary>
        /// Called whenever an edge was added to the property graph.
        /// </summary>
        event EdgeAddedEventHandler<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnEdgeAdded;

        #endregion


        #region EdgeById(EdgeId)

        /// <summary>
        /// Return the edge referenced by the given edge identifier.
        /// If no edge is referenced by a given identifier return null.
        /// </summary>
        /// <param name="EdgeId">An edge identifier.</param>
        IGenericPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                             EdgeById(TIdEdge EdgeId);

        #endregion

        #region EdgesById(params EdgeIds)

        /// <summary>
        /// Return the edges referenced by the given array of edge identifiers.
        /// If no edge is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="EdgeIds">An array of edge identifiers.</param>
        IEnumerable<IGenericPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                                         EdgesById(params TIdEdge[] EdgeIds);

        #endregion

        #region EdgesByLabel(params EdgeLabels)

        /// <summary>
        /// Return an enumeration of all edges having one of the
        /// given edge labels.
        /// </summary>
        /// <param name="EdgeLabels">An array of edge labels.</param>
        IEnumerable<IGenericPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> EdgesByLabel(params TEdgeLabel[] EdgeLabels);

        #endregion

        #region Edges(EdgeFilter = null)

        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// An optional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        IEnumerable<IGenericPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
                                         Edges(EdgeFilter<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter = null);

        #endregion

        #region NumberOfEdges(EdgeFilter = null)

        /// <summary>
        /// Return the current number of edges matching the given optional edge filter.
        /// When the filter is null, this method should implement an optimized
        /// way to get the currenty number of edges.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        UInt64 NumberOfEdges(EdgeFilter<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                        TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter = null);

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
        void RemoveEdges(params IGenericPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Edges);

        #endregion

        #region RemoveEdges(EdgeFilter = null)

        /// <summary>
        /// Remove any edge matching the given edge filter.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        void RemoveEdges(EdgeFilter<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter = null);

        #endregion

        #endregion

        #region MultiEdge methods

        #region OnMultiEdgeAdding

        /// <summary>
        /// Called whenever a multiedge will be added to the property graph.
        /// </summary>
        event MultiEdgeAddingEventHandler<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnMultiEdgeAdding;

        #endregion

        #region OnMultiEdgeAdded

        /// <summary>
        /// Called whenever a multiedge was added to the property graph.
        /// </summary>
        event MultiEdgeAddedEventHandler<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnMultiEdgeAdded;

        #endregion


        #region MultiEdgeById(MultiEdgeId)

        /// <summary>
        /// Return the MultiEdge referenced by the given MultiEdge identifier.
        /// If no MultiEdge is referenced by a given identifier return null.
        /// </summary>
        /// <param name="MultiEdgeId">A MultiEdge identifier.</param>
        IGenericPropertyMultiEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                                  MultiEdgeById(TIdMultiEdge MultiEdgeId);

        #endregion

        #region MultiEdgesById(params MultiEdgeIds)

        /// <summary>
        /// Return the MultiEdges referenced by the given array of MultiEdge identifiers.
        /// If no MultiEdge is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="MultiEdgeIds">An array of MultiEdge identifiers.</param>
        IEnumerable<IGenericPropertyMultiEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                                              MultiEdgesById(params TIdMultiEdge[] MultiEdgeIds);

        #endregion

        #region MultiEdgesByLabel(params EdgeLabels)

        /// <summary>
        /// The enumeration of all multiedges having one of the given label.
        /// </summary>
        IEnumerable<IGenericPropertyMultiEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> MultiEdgesByLabel(params TMultiEdgeLabel[] MultiEdgeLabels);

        #endregion

        #region MultiEdges(MultiEdgeFilter = null)

        /// <summary>
        /// Get an enumeration of all MultiEdges in the graph.
        /// An optional MultiEdge filter may be applied for filtering.
        /// </summary>
        /// <param name="MultiEdgeFilter">A delegate for MultiEdge filtering.</param>
        IEnumerable<IGenericPropertyMultiEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                                              MultiEdges(MultiEdgeFilter<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeFilter = null);

        #endregion

        #region NumberOfMultiEdges(MultiEdgeFilter = null)

        /// <summary>
        /// Return the current number of MultiEdges matching the given optional MultiEdge filter.
        /// When the filter is null, this method should implement an optimized
        /// way to get the currenty number of edges.
        /// </summary>
        /// <param name="MultiEdgeFilter">A delegate for MultiEdge filtering.</param>
        UInt64 NumberOfMultiEdges(MultiEdgeFilter<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeFilter = null);

        #endregion

        #endregion

        #region HyperEdge methods
        
        #region OnHyperEdgeAdding

        /// <summary>
        /// Called whenever a hyperedge will be added to the property graph.
        /// </summary>
        event HyperEdgeAddingEventHandler<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnHyperEdgeAdding;

        #endregion

        #region AddHyperEdge(params Vertices)

        /// <summary>
        /// Add a multiedge based on the given enumeration
        /// of vertices to the graph.
        /// </summary>
        /// <param name="Vertices">An enumeration of vertices.</param>
        /// <returns>The new multiedge</returns>
        IGenericPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        
                                  AddHyperEdge(params IGenericPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                             TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices);

        #endregion

        #region AddHyperEdge(Label, params Vertices)

        /// <summary>
        /// Add a multiedge based on the given multiedge label and
        /// an enumeration of vertices to the graph.
        /// </summary>
        /// <param name="Label">The multiedge label.</param>
        /// <param name="Vertices">An enumeration of vertices.</param>
        /// <returns>The new multiedge</returns>
        IGenericPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        
                                  AddHyperEdge(THyperEdgeLabel Label,            
                                               params IGenericPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                             TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices);

        #endregion

        #region AddHyperEdge(Label, HyperEdgeInitializer, params Vertices)

        /// <summary>
        /// Add a multiedge based on the given multiedge label and
        /// an enumeration of vertices to the graph and initialize
        /// it by invoking the given HyperEdgeInitializer.
        /// </summary>
        /// <param name="Label">The multiedge label.</param>
        /// <param name="HyperEdgeInitializer">A delegate to initialize the newly generated multiedge.</param>
        /// <param name="Vertices">An enumeration of vertices.</param>
        /// <returns>The new multiedge</returns>
        IGenericPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        
                                  AddHyperEdge(THyperEdgeLabel Label,
                                               HyperEdgeInitializer<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeInitializer,
        
                                               params IGenericPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                             TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices);

        #endregion

        #region AddHyperEdge(HyperEdgeId, Label, HyperEdgeInitializer, params Vertices)

        /// <summary>
        /// Add a multiedge based on the given multiedge label and
        /// an enumeration of vertices to the graph and initialize
        /// it by invoking the given HyperEdgeInitializer.
        /// </summary>
        /// <param name="HyperEdgeId">A HyperEdgeId. If none was given a new one will be generated.</param>
        /// <param name="Label">The multiedge label.</param>
        /// <param name="HyperEdgeInitializer">A delegate to initialize the newly generated multiedge.</param>
        /// <param name="Vertices">An enumeration of vertices.</param>
        /// <returns>The new multiedge</returns>
        IGenericPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        
                                  AddHyperEdge(TIdHyperEdge    HyperEdgeId,
                                               THyperEdgeLabel Label,

                                               HyperEdgeInitializer<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeInitializer,
        
                                               params IGenericPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                             TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices);

        #endregion

        #region OnHyperEdgeAdded

        /// <summary>
        /// Called whenever a hyperedge was added to the property graph.
        /// </summary>
        event HyperEdgeAddedEventHandler<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnHyperEdgeAdded;

        #endregion


        #region HyperEdgeById(HyperEdgeId)

        /// <summary>
        /// Return the HyperEdge referenced by the given HyperEdge identifier.
        /// If no HyperEdge is referenced by a given identifier return null.
        /// </summary>
        /// <param name="HyperEdgeId">A HyperEdge identifier.</param>
        IGenericPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                                  HyperEdgeById(TIdHyperEdge HyperEdgeId);

        #endregion

        #region HyperEdgesById(params HyperEdgeIds)

        /// <summary>
        /// Return the HyperEdges referenced by the given array of HyperEdge identifiers.
        /// If no HyperEdge is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="HyperEdgeIds">An array of HyperEdge identifiers.</param>
        IEnumerable<IGenericPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                                              HyperEdgesById(params TIdHyperEdge[] HyperEdgeIds);

        #endregion

        #region HyperEdgesByLabel(params EdgeLabels)

        /// <summary>
        /// The enumeration of all hyperedges having one of the given label.
        /// </summary>
        IEnumerable<IGenericPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
                                              HyperEdgesByLabel(params THyperEdgeLabel[] HyperEdgeLabels);

        #endregion

        #region HyperEdges(HyperEdgeFilter = null)

        /// <summary>
        /// Get an enumeration of all HyperEdges in the graph.
        /// An optional HyperEdge filter may be applied for filtering.
        /// </summary>
        /// <param name="HyperEdgeFilter">A delegate for HyperEdge filtering.</param>
        IEnumerable<IGenericPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                                              HyperEdges(HyperEdgeFilter<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter = null);

        #endregion

        #region NumberOfHyperEdges(HyperEdgeFilter = null)

        /// <summary>
        /// Return the current number of HyperEdges matching the given optional HyperEdge filter.
        /// When the filter is null, this method should implement an optimized
        /// way to get the currenty number of edges.
        /// </summary>
        /// <param name="HyperEdgeFilter">A delegate for HyperEdge filtering.</param>
        UInt64 NumberOfHyperEdges(HyperEdgeFilter<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter = null);

        #endregion

        #endregion

        #region Graph Maintenance

        #region Clear()

        /// <summary>
        /// Remove all the vertices, edges and hyperedges from the graph.
        /// </summary>
        void Clear();

        #endregion


        #region OnGraphShuttingdown

        /// <summary>
        /// Called whenever a property graph will be shutting down.
        /// </summary>
        event GraphShuttingdownEventHandler<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnGraphShuttingdown;

        #endregion

        #region Shutdown(Message = "")

        /// <summary>
        /// A shutdown function is required to properly close the graph.
        /// This is important for implementations that utilize disk-based serializations.
        /// </summary>
        /// <param name="Message">An optional message, e.g. a reason for the shutdown.</param>
        void Shutdown(String Message = "");

        #endregion

        #region OnGraphShutteddown

        /// <summary>
        /// Called whenever a property graphy was shutted down.
        /// </summary>
        event GraphShutteddownEventHandler <TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnGraphShutteddown;

        #endregion

        #endregion

    }

}
