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

namespace de.ahzf.Blueprints.PropertyGraphs
{

    #region IPropertyVertex

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    public interface IPropertyVertex : IPropertyVertex<UInt64, Int64, String, String, Object, IDictionary<String, Object>,
                                                       UInt64, Int64, String, String, Object, IDictionary<String, Object>,
                                                       UInt64, Int64, String, String, Object, IDictionary<String, Object>,
                                                       UInt64, Int64, String, String, Object, IDictionary<String, Object>>
    { }

    #endregion

    #region IPropertyVertex<...>

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TVertexType">The type of the vertex type.</typeparam>
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
    public interface IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                                     : IGraphElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex>,

                                       IEquatable <IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                   TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>,

                                       IComparable<IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                   TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>,

                                       IComparable

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyMultiEdge           : IEquatable<TKeyMultiEdge>,        IComparable<TKeyMultiEdge>,        IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

        where TIdVertex               : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge                 : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdMultiEdge            : IEquatable<TIdMultiEdge>,         IComparable<TIdMultiEdge>,         IComparable, TValueMultiEdge
        where TIdHyperEdge            : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TVertexType             : IEquatable<TVertexType>,          IComparable<TVertexType>,          IComparable
        where TEdgeLabel              : IEquatable<TEdgeLabel>,           IComparable<TEdgeLabel>,           IComparable
        where TMultiEdgeLabel         : IEquatable<TMultiEdgeLabel>,      IComparable<TMultiEdgeLabel>,      IComparable
        where THyperEdgeLabel         : IEquatable<THyperEdgeLabel>,      IComparable<THyperEdgeLabel>,      IComparable

        where TRevisionIdVertex       : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge         : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdMultiEdge    : IEquatable<TRevisionIdMultiEdge>, IComparable<TRevisionIdMultiEdge>, IComparable, TValueMultiEdge
        where TRevisionIdHyperEdge    : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

    {

        #region Graph

        /// <summary>
        /// The associated property graph.
        /// </summary>
        IPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                       TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph { get; }

        #endregion

        #region Type

        /// <summary>
        /// The type associated with this vertex.
        /// </summary>
        TVertexType Type { get; }

        #endregion

        #region OutEdge methods...

        #region AddOutEdge(Edge)

        /// <summary>
        /// Add an outgoing edge.
        /// </summary>
        /// <param name="Edge">The edge to add.</param>
        void AddOutEdge(IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge);

        #endregion


        #region OutEdges(params EdgeLabels)      // OutEdges()!

        /// <summary>
        /// The edges emanating from, or leaving, this vertex
        /// filtered by their label. If no label was given,
        /// all edges will be returned.
        /// </summary>
        IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            OutEdges(params TEdgeLabel[] EdgeLabels);

        #endregion

        #region OutEdges(EdgeFilter)

        /// <summary>
        /// The edges emanating from, or leaving, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
            OutEdges(EdgeFilter<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter);

        #endregion

        #region OutDegree(params EdgeLabels)     // OutDegree()!

        /// <summary>
        /// The number of edges emanating from, or leaving, this vertex
        /// filtered by their label. If no label was given,
        /// all edges will be returned.
        /// </summary>
        UInt64 OutDegree(params TEdgeLabel[] EdgeLabels);

        #endregion

        #region OutDegree(EdgeFilter)

        /// <summary>
        /// The number of edges emanating from, or leaving, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        UInt64 OutDegree(EdgeFilter<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter);

        #endregion


        #region RemoveOutEdges(params Edges)    // RemoveOutEdges()!

        /// <summary>
        /// Remove outgoing edges.
        /// </summary>
        /// <param name="Edges">An array of outgoing edges to be removed.</param>
        void RemoveOutEdges(params IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Edges);

        #endregion

        #region RemoveOutEdges(EdgeFilter = null)

        /// <summary>
        /// Remove any outgoing edge matching the
        /// given edge filter delegate.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        void RemoveOutEdges(EdgeFilter<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter = null);

        #endregion

        #endregion

        #region InEdge methods...

        #region AddInEdge(Edge)

        /// <summary>
        /// Add an incoming edge.
        /// </summary>
        /// <param name="Edge">The edge to add.</param>
        void AddInEdge(IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge);

        #endregion


        #region InEdges(params EdgeLabels)     // InEdges()!

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex
        /// filtered by their label. If no label was given,
        /// all edges will be returned.
        /// </summary>
        IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            InEdges(params TEdgeLabel[] EdgeLabels);

        #endregion

        #region InEdges(EdgeFilter)

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
            InEdges(EdgeFilter<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter);

        #endregion

        #region InDegree(params EdgeLabels)     // InDegree()!

        /// <summary>
        /// The number of edges incoming to, or arriving at, this vertex
        /// filtered by their label. If no label was given,
        /// all edges will be returned.
        /// </summary>
        UInt64 InDegree(params TEdgeLabel[] EdgeLabels);

        #endregion

        #region InDegree(EdgeFilter)

        /// <summary>
        /// The number of edges incoming to, or arriving at, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        UInt64 InDegree(EdgeFilter<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter);

        #endregion


        #region RemoveInEdges(params Edges)    // RemoveInEdges()!

        /// <summary>
        /// Remove incoming edges.
        /// </summary>
        /// <param name="Edges">An array of incoming edges to be removed.</param>
        void RemoveInEdges(params IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Edges);

        #endregion

        #region RemoveInEdges(EdgeFilter = null)

        /// <summary>
        /// Remove any incoming edge matching the
        /// given edge filter delegate.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        void RemoveInEdges(EdgeFilter<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter = null);

        #endregion

        #endregion

        #region MultiEdge methods...

        #endregion

        #region HyperEdge methods...

        #region AddHyperEdge(HyperEdge)

        /// <summary>
        /// Add a hyperedge.
        /// </summary>
        /// <param name="HyperEdge">The hyperedge to add.</param>
        void AddHyperEdge(IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdge);

        #endregion


        #region HyperEdges(params HyperEdgeLabels)      // HyperEdges()!

        /// <summary>
        /// The hyperedges emanating from, or leaving, this vertex
        /// filtered by their label. If no label was given,
        /// all hyperedges will be returned.
        /// </summary>
        IEnumerable<IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            HyperEdges(params THyperEdgeLabel[] HyperEdgeLabels);

        #endregion

        #region HyperEdges(HyperEdgeFilter)

        /// <summary>
        /// The hyperedges emanating from, or leaving, this vertex
        /// filtered by the given hyperedge filter delegate.
        /// </summary>
        /// <param name="HyperEdgeFilter">A delegate for hyperedge filtering.</param>
        IEnumerable<IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
            HyperEdges(HyperEdgeFilter<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter);

        #endregion


        #region RemoveHyperEdges(params HyperEdges)    // RemoveHyperEdges()!

        /// <summary>
        /// Remove hyperedges.
        /// </summary>
        /// <param name="HyperEdges">An array of outgoing edges to be removed.</param>
        void RemoveHyperEdges(params IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] HyperEdges);

        #endregion

        #region RemoveHyperEdges(HyperEdgeFilter = null)

        /// <summary>
        /// Remove any outgoing hyperedge matching
        /// the given hyperedge filter delegate.
        /// </summary>
        /// <param name="HyperEdgeFilter">A delegate for hyperedge filtering.</param>
        void RemoveHyperEdges(HyperEdgeFilter<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter = null);

        #endregion

        #endregion

    }

    #endregion

    #region IPropertyVertex<..., TDatastructureXXX>

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TVertexType">The type of the vertex type.</typeparam>
    /// <typeparam name="TKeyVertex">The type of the vertex property keys.</typeparam>
    /// <typeparam name="TValueVertex">The type of the vertex property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionVertex">A data structure to store the properties of the vertices.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
    /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionEdge">A data structure to store the properties of the edges.</typeparam>
    /// 
    /// <typeparam name="TIdMultiEdge">The type of the multiedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdMultiEdge">The type of the multiedge revision identifiers.</typeparam>
    /// <typeparam name="TMultiEdgeLabel">The type of the multiedge label.</typeparam>
    /// <typeparam name="TKeyMultiEdge">The type of the multiedge property keys.</typeparam>
    /// <typeparam name="TValueMultiEdge">The type of the multiedge property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionMultiEdge">A data structure to store the properties of the multiedges.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
    /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionHyperEdge">A data structure to store the properties of the hyperedges.</typeparam>
    public interface IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
                                     TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>

                                     : IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,
        
                                       IGraphElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex, TPropertiesCollectionVertex>

        where TPropertiesCollectionVertex    : IDictionary<TKeyVertex,    TValueVertex>
        where TPropertiesCollectionEdge      : IDictionary<TKeyEdge,      TValueEdge>
        where TPropertiesCollectionMultiEdge : IDictionary<TKeyMultiEdge, TValueMultiEdge>
        where TPropertiesCollectionHyperEdge : IDictionary<TKeyHyperEdge, TValueHyperEdge>

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyMultiEdge           : IEquatable<TKeyMultiEdge>,        IComparable<TKeyMultiEdge>,        IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

        where TIdVertex               : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge                 : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdMultiEdge            : IEquatable<TIdMultiEdge>,         IComparable<TIdMultiEdge>,         IComparable, TValueMultiEdge
        where TIdHyperEdge            : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TVertexType             : IEquatable<TVertexType>,          IComparable<TVertexType>,          IComparable
        where TEdgeLabel              : IEquatable<TEdgeLabel>,           IComparable<TEdgeLabel>,           IComparable
        where TMultiEdgeLabel         : IEquatable<TMultiEdgeLabel>,      IComparable<TMultiEdgeLabel>,      IComparable
        where THyperEdgeLabel         : IEquatable<THyperEdgeLabel>,      IComparable<THyperEdgeLabel>,      IComparable

        where TRevisionIdVertex       : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge         : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdMultiEdge    : IEquatable<TRevisionIdMultiEdge>, IComparable<TRevisionIdMultiEdge>, IComparable, TValueMultiEdge
        where TRevisionIdHyperEdge    : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

    { }

    #endregion

}
