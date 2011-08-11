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

namespace de.ahzf.Blueprints.PropertyGraph
{

    #region IPropertyVertex

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    public interface IPropertyVertex : IComparable
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
    public interface IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                                     : IGraphElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex>,
                                       IEquatable <IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>,
                                       IComparable<IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>,
                                       IPropertyVertex

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

        where TIdVertex               : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge                 : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdHyperEdge            : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TEdgeLabel              : IEquatable<TEdgeLabel>,           IComparable<TEdgeLabel>,           IComparable
        where THyperEdgeLabel         : IEquatable<THyperEdgeLabel>,      IComparable<THyperEdgeLabel>,      IComparable

        where TRevisionIdVertex       : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge         : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdHyperEdge    : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

    {

        #region Graph

        /// <summary>
        /// The associated property graph.
        /// </summary>
        IPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph { get; }

        #endregion

        #region OutEdge methods...

        #region AddOutEdge(Edge)

        /// <summary>
        /// Add an outgoing edge.
        /// </summary>
        /// <param name="Edge">The edge to add.</param>
        void AddOutEdge(IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge);

        #endregion


        #region OutEdges(params EdgeLabels)      // OutEdges()!

        /// <summary>
        /// The edges emanating from, or leaving, this vertex
        /// filtered by their label. If no label was given,
        /// all edges will be returned.
        /// </summary>
        IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            OutEdges(params TEdgeLabel[] EdgeLabels);

        #endregion

        #region OutEdges(EdgeFilter)

        /// <summary>
        /// The edges emanating from, or leaving, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
            OutEdges(EdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
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
        UInt64 OutDegree(EdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter);

        #endregion


        #region RemoveOutEdges(params Edges)    // RemoveOutEdges()!

        /// <summary>
        /// Remove outgoing edges.
        /// </summary>
        /// <param name="Edges">An array of outgoing edges to be removed.</param>
        void RemoveOutEdges(params IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Edges);

        #endregion

        #region RemoveOutEdges(EdgeFilter = null)

        /// <summary>
        /// Remove any outgoing edge matching the
        /// given edge filter delegate.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        void RemoveOutEdges(EdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter = null);

        #endregion

        #endregion

        #region InEdge methods...

        #region AddInEdge(Edge)

        /// <summary>
        /// Add an incoming edge.
        /// </summary>
        /// <param name="Edge">The edge to add.</param>
        void AddInEdge(IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge);

        #endregion


        #region InEdges(params EdgeLabels)     // InEdges()!

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex
        /// filtered by their label. If no label was given,
        /// all edges will be returned.
        /// </summary>
        IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            InEdges(params TEdgeLabel[] EdgeLabels);

        #endregion

        #region InEdges(EdgeFilter)

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
            InEdges(EdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
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
        UInt64 InDegree(EdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter);

        #endregion


        #region RemoveInEdges(params Edges)    // RemoveInEdges()!

        /// <summary>
        /// Remove incoming edges.
        /// </summary>
        /// <param name="Edges">An array of incoming edges to be removed.</param>
        void RemoveInEdges(params IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Edges);

        #endregion

        #region RemoveInEdges(EdgeFilter = null)

        /// <summary>
        /// Remove any incoming edge matching the
        /// given edge filter delegate.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        void RemoveInEdges(EdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
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
        void AddHyperEdge(IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdge);

        #endregion


        #region HyperEdges(params HyperEdgeLabels)      // HyperEdges()!

        /// <summary>
        /// The hyperedges emanating from, or leaving, this vertex
        /// filtered by their label. If no label was given,
        /// all hyperedges will be returned.
        /// </summary>
        IEnumerable<IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            HyperEdges(params THyperEdgeLabel[] HyperEdgeLabels);

        #endregion

        #region HyperEdges(HyperEdgeFilter)

        /// <summary>
        /// The hyperedges emanating from, or leaving, this vertex
        /// filtered by the given hyperedge filter delegate.
        /// </summary>
        /// <param name="HyperEdgeFilter">A delegate for hyperedge filtering.</param>
        IEnumerable<IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
            HyperEdges(HyperEdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter);

        #endregion


        #region RemoveHyperEdges(params HyperEdges)    // RemoveHyperEdges()!

        /// <summary>
        /// Remove hyperedges.
        /// </summary>
        /// <param name="HyperEdges">An array of outgoing edges to be removed.</param>
        void RemoveHyperEdges(params IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] HyperEdges);

        #endregion

        #region RemoveHyperEdges(HyperEdgeFilter = null)

        /// <summary>
        /// Remove any outgoing hyperedge matching
        /// the given hyperedge filter delegate.
        /// </summary>
        /// <param name="HyperEdgeFilter">A delegate for hyperedge filtering.</param>
        void RemoveHyperEdges(HyperEdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
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
    /// <typeparam name="TKeyVertex">The type of the vertex property keys.</typeparam>
    /// <typeparam name="TValueVertex">The type of the vertex property values.</typeparam>
    /// <typeparam name="TDatastructureVertex"></typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
    /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
    /// <typeparam name="TDatastructureEdge"></typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
    /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
    /// <typeparam name="TDatastructureHyperEdge"></typeparam>
    public interface IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>

                                     : IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,
        
                                       IGraphElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex, TDatastructureVertex>

        where TDatastructureVertex    : IDictionary<TKeyVertex,    TValueVertex>
        where TDatastructureEdge      : IDictionary<TKeyEdge,      TValueEdge>
        where TDatastructureHyperEdge : IDictionary<TKeyHyperEdge, TValueHyperEdge>

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

        where TIdVertex               : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge                 : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdHyperEdge            : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TEdgeLabel              : IEquatable<TEdgeLabel>,           IComparable<TEdgeLabel>,           IComparable
        where THyperEdgeLabel         : IEquatable<THyperEdgeLabel>,      IComparable<THyperEdgeLabel>,      IComparable

        where TRevisionIdVertex       : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge         : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdHyperEdge    : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

    { }

    #endregion

}
