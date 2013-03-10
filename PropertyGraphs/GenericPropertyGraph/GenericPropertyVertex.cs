/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
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
using System.Linq;
using System.Dynamic;
using System.Threading;
using System.Linq.Expressions;
using System.Collections.Generic;

using de.ahzf.Illias.Commons;
using de.ahzf.Illias.Commons.Votes;
using de.ahzf.Illias.Commons.Transactions;
using de.ahzf.Vanaheimr.Styx;

#endregion

namespace de.ahzf.Vanaheimr.Blueprints.InMemory
{

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---InEdges---> Vertex ---OutEdges/HyperEdges--->.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TVertexLabel">The type of the vertex type.</typeparam>
    /// <typeparam name="TKeyVertex">The type of the vertex property keys.</typeparam>
    /// <typeparam name="TValueVertex">The type of the vertex property values.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
    /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
    /// 
    /// <typeparam name="TIdMultiEdge">The type of the multiedge identifiers.</typeparam>
    /// <typeparam name="TRevIdMultiEdge">The type of the multiedge revision identifiers.</typeparam>
    /// <typeparam name="TMultiEdgeLabel">The type of the multiedge label.</typeparam>
    /// <typeparam name="TKeyMultiEdge">The type of the multiedge property keys.</typeparam>
    /// <typeparam name="TValueMultiEdge">The type of the multiedge property values.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
    /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
    public class GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                                       : AGraphElement<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex>,

                                         IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                         IGenericPropertyGraph <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                         IDynamicGraphElement<GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

        where TIdVertex        : IEquatable<TIdVertex>,       IComparable<TIdVertex>,       IComparable, TValueVertex
        where TIdEdge          : IEquatable<TIdEdge>,         IComparable<TIdEdge>,         IComparable, TValueEdge
        where TIdMultiEdge     : IEquatable<TIdMultiEdge>,    IComparable<TIdMultiEdge>,    IComparable, TValueMultiEdge
        where TIdHyperEdge     : IEquatable<TIdHyperEdge>,    IComparable<TIdHyperEdge>,    IComparable, TValueHyperEdge

        where TRevIdVertex     : IEquatable<TRevIdVertex>,    IComparable<TRevIdVertex>,    IComparable, TValueVertex
        where TRevIdEdge       : IEquatable<TRevIdEdge>,      IComparable<TRevIdEdge>,      IComparable, TValueEdge
        where TRevIdMultiEdge  : IEquatable<TRevIdMultiEdge>, IComparable<TRevIdMultiEdge>, IComparable, TValueMultiEdge
        where TRevIdHyperEdge  : IEquatable<TRevIdHyperEdge>, IComparable<TRevIdHyperEdge>, IComparable, TValueHyperEdge

        where TVertexLabel     : IEquatable<TVertexLabel>,    IComparable<TVertexLabel>,    IComparable, TValueVertex
        where TEdgeLabel       : IEquatable<TEdgeLabel>,      IComparable<TEdgeLabel>,      IComparable, TValueEdge
        where TMultiEdgeLabel  : IEquatable<TMultiEdgeLabel>, IComparable<TMultiEdgeLabel>, IComparable, TValueMultiEdge
        where THyperEdgeLabel  : IEquatable<THyperEdgeLabel>, IComparable<THyperEdgeLabel>, IComparable, TValueHyperEdge

        where TKeyVertex       : IEquatable<TKeyVertex>,      IComparable<TKeyVertex>,      IComparable
        where TKeyEdge         : IEquatable<TKeyEdge>,        IComparable<TKeyEdge>,        IComparable
        where TKeyMultiEdge    : IEquatable<TKeyMultiEdge>,   IComparable<TKeyMultiEdge>,   IComparable
        where TKeyHyperEdge    : IEquatable<TKeyHyperEdge>,   IComparable<TKeyHyperEdge>,   IComparable

    {

        #region Data

        #region SuperVertex <- this

        /// <summary>
        /// This object as a SuperVertex implementing IGenericPropertyVertex;
        /// </summary>
        protected readonly IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> SuperVertex;

        #endregion

        #region IGenericPropertyVertex -> IGroupedCollections

        /// <summary>
        /// The edges emanating from, or leaving, this vertex.
        /// </summary>
        protected readonly IGroupedCollection<TIdEdge,      IReadOnlyGenericPropertyEdge     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TEdgeLabel> _OutEdgesWhenVertex;

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex.
        /// </summary>
        protected readonly IGroupedCollection<TIdEdge,      IReadOnlyGenericPropertyEdge     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TEdgeLabel> _InEdgesWhenVertex;


        /// <summary>
        /// The hyperedges of this vertex.
        /// </summary>
        protected readonly IGroupedCollection<TIdHyperEdge, IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, THyperEdgeLabel> _HyperEdgesWhenVertex;

        #endregion

        #region Subgraph <- this

        /// <summary>
        /// This object as an IGenericPropertyGraph;
        /// </summary>
        protected readonly IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Subgraph;

        #endregion

        #region IGenericPropertyGraph -> IGroupedCollections

        /// <summary>
        /// The collection of vertices.
        /// </summary>
        protected readonly IGroupedCollection<TIdVertex,    IReadOnlyGenericPropertyVertex   <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TVertexLabel> _VerticesWhenGraph;

        /// <summary>
        /// The edges not conntected to this vertex but to other
        /// vertices when this vertex acts as a graph.
        /// </summary>
        protected readonly IGroupedCollection<TIdEdge,      IReadOnlyGenericPropertyEdge     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TEdgeLabel> _EdgesWhenGraph;

        /// <summary>
        /// The multiedges of this vertex.
        /// </summary>
        protected readonly IGroupedCollection<TIdMultiEdge, IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TMultiEdgeLabel> _MultiEdgesWhenGraph;

        /// <summary>
        /// The hyperedges of this vertex.
        /// </summary>
        protected readonly IGroupedCollection<TIdHyperEdge, IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, THyperEdgeLabel> _HyperEdgesWhenGraph;

        #endregion


        #region Vertex delegates...

        /// <summary>
        /// A delegate to create a new vertex identification.
        /// </summary>
        protected readonly VertexIdCreatorDelegate   <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _VertexIdCreatorDelegate;

        /// <summary>
        /// The default vertex label.
        /// </summary>
        protected readonly TVertexLabel _DefaultVertexLabel;

        /// <summary>
        /// A delegate to create a new vertex.
        /// </summary>
        protected readonly VertexCreatorDelegate     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _VertexCreatorDelegate;

        #endregion

        #region Edge delegates...

        /// <summary>
        /// A delegate to create a new edge identification.
        /// </summary>
        protected readonly EdgeIdCreatorDelegate     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _EdgeIdCreatorDelegate;

        /// <summary>
        /// The default edge label.
        /// </summary>
        protected readonly TEdgeLabel _DefaultEdgeLabel;

        /// <summary>
        /// A delegate to create a new edge.
        /// </summary>
        protected readonly EdgeCreatorDelegate       <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _EdgeCreatorDelegate;

        #endregion

        #region Multiedge delegates...

        /// <summary>
        /// A delegate to create a new multiedge identification.
        /// </summary>
        protected readonly MultiEdgeIdCreatorDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _MultiEdgeIdCreatorDelegate;

        /// <summary>
        /// The default multiedge label.
        /// </summary>
        protected readonly TMultiEdgeLabel _DefaultMultiEdgeLabel;

        /// <summary>
        /// A delegate to create a new multiedge.
        /// </summary>
        protected readonly MultiEdgeCreatorDelegate  <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _MultiEdgeCreatorDelegate;

        #endregion

        #region Hyperedge delegates...

        /// <summary>
        /// A delegate to create a new hyperedge identification.
        /// </summary>
        protected readonly HyperEdgeIdCreatorDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _HyperEdgeIdCreatorDelegate;

        /// <summary>
        /// The default hyperedge label.
        /// </summary>
        protected readonly THyperEdgeLabel _DefaultHyperEdgeLabel;

        /// <summary>
        /// A delegate to create a new hyperedge.
        /// </summary>
        protected readonly HyperEdgeCreatorDelegate  <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _HyperEdgeCreatorDelegate;

        #endregion

        #endregion

        #region Properties

        // this == IReadOnlyGenericPropertyGraph

        #region Graph

        /// <summary>
        /// The associated property graph.
        /// </summary>
        protected readonly IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph;

        /// <summary>
        /// The associated property graph.
        /// </summary>
        IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            Graph
            {
                get
                {
                    return Graph;
                }
            }

        #endregion

        #region VoteCreator

        private readonly Func<IVote<Boolean>> _VoteCreator;

        public Func<IVote<Boolean>> VoteCreator
        {
            get
            {
                return _VoteCreator;
            }
        }

        #endregion


        #region EdgeIdKey

        private readonly TKeyEdge _EdgeIdKey;

        TKeyEdge IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

        EdgeIdKey

        {
            get
            {
                return _EdgeIdKey;
            }
        }

        #endregion

        #region EdgeRevIdKey

        private readonly TKeyEdge _EdgeRevIdKey;

        TKeyEdge IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

        EdgeRevIdKey

        {
            get
            {
                return _EdgeRevIdKey;
            }
        }

        #endregion

        #region EdgeLabelKey

        private readonly TKeyEdge _EdgeLabelKey;

        TKeyEdge IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

        EdgeLabelKey

        {
            get
            {
                return _EdgeLabelKey;
            }
        }

        #endregion


        #region MultiEdgeIdKey

        private readonly TKeyMultiEdge _MultiEdgeIdKey;

        TKeyMultiEdge IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

        MultiEdgeIdKey

        {
            get
            {
                return _MultiEdgeIdKey;
            }
        }

        #endregion

        #region MultiEdgeRevIdKey

        private readonly TKeyMultiEdge _MultiEdgeRevIdKey;

        TKeyMultiEdge IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

        MultiEdgeRevIdKey

        {
            get
            {
                return _MultiEdgeRevIdKey;
            }
        }

        #endregion

        #region MultiEdgeLabelKey

        private readonly TKeyMultiEdge _MultiEdgeLabelKey;

        TKeyMultiEdge IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

        MultiEdgeLabelKey

        {
            get
            {
                return _MultiEdgeLabelKey;
            }
        }

        #endregion


        #region HyperEdgeIdKey

        private readonly TKeyHyperEdge _HyperEdgeIdKey;

        TKeyHyperEdge IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

        HyperEdgeIdKey

        {
            get
            {
                return _HyperEdgeIdKey;
            }
        }

        #endregion

        #region HyperEdgeRevIdKey

        private readonly TKeyHyperEdge _HyperEdgeRevIdKey;

        TKeyHyperEdge IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

        HyperEdgeRevIdKey

        {
            get
            {
                return _HyperEdgeRevIdKey;
            }
        }

        #endregion

        #region HyperEdgeLabelKey

        private readonly TKeyHyperEdge _HyperEdgeLabelKey;

        TKeyHyperEdge IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

        HyperEdgeLabelKey

        {
            get
            {
                return _HyperEdgeLabelKey;
            }
        }

        #endregion


        // this == IReadOnlyGenericPropertyGraph

        #region AsSubgraph

        /// <summary>
        /// Access this property vertex as a subgraph of the hosting property graph.
        /// </summary>
        IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            AsSubgraph

            {
                get
                {
                    return this.Subgraph;
                }
            }

        #endregion

        #endregion

        #region Constructor(s)

        #region (private) GenericPropertyVertex(VertexId, VertexLabel, IdKey, RevIdKey, LabelKey, PropertiesInitializer, VoteCreator, ...)

        /// <summary>
        /// Creates a new class-based in-memory implementation of a generic property vertex.
        /// </summary>
        /// <param name="VertexId">The identification of this vertex.</param>
        /// <param name="VertexLabel">A label stored within this vertex.</param>
        /// <param name="IdKey">The property key to access the Ids of the vertices.</param>
        /// <param name="RevIdKey">The property key to access the RevisionIds of the vertices.</param>
        /// <param name="LabelKey">The key to access the Label of this graph element.</param>
        /// <param name="PropertiesInitializer">A delegate to initialize the properties datastructure.</param>
        /// <param name="VoteCreator">A delegate to create a new vote.</param>
        /// 
        /// <param name="VertexIdCreatorDelegate">A delegate to create a new vertex identification.</param>
        /// <param name="DefaultVertexLabel">The default vertex label.</param>
        /// <param name="VertexCreatorDelegate">A delegate to create a new vertex.</param>
        /// <param name="VertexCollectionInitializer">A delegate to initialize the vertex collection datastructure.</param>
        /// 
        /// <param name="EdgeIdCreatorDelegate">A delegate to create a new edge identification.</param>
        /// <param name="DefaultEdgeLabel">The default edge label.</param>
        /// <param name="EdgeCreatorDelegate">A delegate to create a new edge.</param>
        /// <param name="EdgeCollectionInitializer">A delegate to initialize the edge collection datastructure.</param>
        /// 
        /// <param name="MultiEdgeIdCreatorDelegate">A delegate to create a new multiedge identification.</param>
        /// <param name="DefaultMultiEdgeLabel">The default multiedge label.</param>
        /// <param name="MultiEdgeCreatorDelegate">A delegate to create a new multiedge.</param>
        /// <param name="MultiEdgeCollectionInitializer">A delegate to initialize the multiedge collection datastructure.</param>
        /// 
        /// <param name="HyperEdgeIdCreatorDelegate">A delegate to create a new hyperedge identification.</param>
        /// <param name="DefaultHyperEdgeLabel">The default hyperedge label.</param>
        /// <param name="HyperEdgeCreatorDelegate">A delegate to create a new hyperedge.</param>
        /// <param name="HyperEdgeCollectionInitializer">A delegate to initialize the hyperedge collection datastructure.</param>
        private GenericPropertyVertex(TIdVertex     VertexId,
                                      TVertexLabel  VertexLabel, 
                                      TKeyVertex    IdKey,
                                      TKeyVertex    RevIdKey,
                                      TKeyVertex    LabelKey,

                                      IDictionaryInitializer<TKeyVertex, TValueVertex> PropertiesInitializer,
                                      Func<IVote<Boolean>> VoteCreator,

                                      #region Create a new vertex

                                      VertexIdCreatorDelegate       <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexIdCreatorDelegate,

                                      TVertexLabel DefaultVertexLabel,

                                      VertexCreatorDelegate         <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexCreatorDelegate,

                                      VertexCollectionInitializer   <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexCollectionInitializer,

                                      #endregion

                                      #region Create a new edge

                                      EdgeIdCreatorDelegate         <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeIdCreatorDelegate,

                                      TEdgeLabel DefaultEdgeLabel,

                                      EdgeCreatorDelegate           <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeCreatorDelegate,

                                      EdgeCollectionInitializer     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeCollectionInitializer,

                                      #endregion

                                      #region Create a new multiedge

                                      MultiEdgeIdCreatorDelegate    <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeIdCreatorDelegate,

                                      TMultiEdgeLabel DefaultMultiEdgeLabel,

                                      MultiEdgeCreatorDelegate      <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeCreatorDelegate,

                                      MultiEdgeCollectionInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeCollectionInitializer,

                                      #endregion

                                      #region Create a new hyperedge

                                      HyperEdgeIdCreatorDelegate    <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeIdCreatorDelegate,

                                      THyperEdgeLabel DefaultHyperEdgeLabel,

                                      HyperEdgeCreatorDelegate      <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeCreatorDelegate,

                                      HyperEdgeCollectionInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeCollectionInitializer)

                                      #endregion

            : base(VertexId, VertexLabel, IdKey, RevIdKey, LabelKey, PropertiesInitializer, VoteCreator: VoteCreator)

        {

            this._VoteCreator                 = (VoteCreator != null) ? VoteCreator : () => new VetoVote();


            this.SuperVertex                 = this as IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;

            this.Subgraph                    = this as IGenericPropertyGraph <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;

            this._VertexIdCreatorDelegate    = VertexIdCreatorDelegate;
            this._DefaultVertexLabel         = DefaultVertexLabel;
            this._VertexCreatorDelegate      = VertexCreatorDelegate;
            this._VerticesWhenGraph          = VertexCollectionInitializer();

            this._EdgeIdCreatorDelegate      = EdgeIdCreatorDelegate;
            this._DefaultEdgeLabel           = DefaultEdgeLabel;
            this._EdgeCreatorDelegate        = EdgeCreatorDelegate;
            this._OutEdgesWhenVertex         = EdgeCollectionInitializer();
            this._InEdgesWhenVertex          = EdgeCollectionInitializer();
            this._EdgesWhenGraph             = EdgeCollectionInitializer();

            this._MultiEdgeIdCreatorDelegate = MultiEdgeIdCreatorDelegate;
            this._DefaultMultiEdgeLabel      = DefaultMultiEdgeLabel;
            this._MultiEdgeCreatorDelegate   = MultiEdgeCreatorDelegate;
            this._MultiEdgesWhenGraph        = MultiEdgeCollectionInitializer();

            this._HyperEdgeIdCreatorDelegate = HyperEdgeIdCreatorDelegate;
            this._DefaultHyperEdgeLabel      = DefaultHyperEdgeLabel;
            this._HyperEdgeCreatorDelegate   = HyperEdgeCreatorDelegate;
            this._HyperEdgesWhenVertex       = HyperEdgeCollectionInitializer();
            this._HyperEdgesWhenGraph        = HyperEdgeCollectionInitializer();


            // IGenericPropertyGraph

            #region VertexAddition/-Removal

            this.VertexAddition   = new VotingNotificator<IReadOnlyGenericPropertyGraph <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          Boolean>(VoteCreator, true);


            this.VertexRemoval   = new VotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          Boolean>(VoteCreator, true);

            #endregion

            #region EdgeAddition/-Removal

            this.EdgeAddition     = new VotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          Boolean>(VoteCreator, true);


            this.EdgeRemoval      = new VotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          Boolean>(VoteCreator, true);

            #endregion

            #region MultiEdgeAddition/-Removal

            this.MultiEdgeAddition_WhenGraph = new VotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          Boolean>(VoteCreator, true);

            this.MultiEdgeRemoval_WhenGraph = new VotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          Boolean>(VoteCreator, true);

            #endregion

            #region HyperEdgeAddition/-Removal

            this.HyperEdgeAddition_WhenGraph = new VotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                        IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                        Boolean>(VoteCreator, true);

            this.HyperEdgeRemoval_WhenGraph = new VotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                        IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                        Boolean>(VoteCreator, true);

            #endregion

        }

        #endregion

        #region (internal) GenericPropertyVertex(...IdCreatorDelegates...)

        /// <summary>
        /// Creates a new class-based in-memory implementation of a generic property vertex.
        /// </summary>
        /// <param name="GraphId">The identification of this generic property graph.</param>
        /// 
        /// <param name="VertexIdKey">The property key to access the identifications of the vertices.</param>
        /// <param name="VertexRevIdKey">The property key to access the revision identifications of the vertices.</param>
        /// <param name="VertexLabelKey">The property key to access the labels of the vertices.</param>
        /// <param name="VertexIdCreatorDelegate">A delegate to create a new vertex identification.</param>
        /// <param name="DefaultVertexLabel">The default label of the vertices.</param>
        /// 
        /// <param name="EdgeIdKey">The property key to access the identifications of the edges.</param>
        /// <param name="EdgeRevIdKey">The property key to access the revision identifications of the edges.</param>
        /// <param name="EdgeLabelKey">The property key to access the labels of the edges.</param>
        /// <param name="EdgeIdCreatorDelegate">A delegate to create a new edge identification.</param>
        /// <param name="DefaultEdgeLabel">The default label of the edges.</param>
        /// 
        /// <param name="MultiEdgeIdKey">The property key to access the identifications of the multiedges.</param>
        /// <param name="MultiEdgeRevIdKey">The property key to access the revision identifications of the multiedges.</param>
        /// <param name="MultiEdgeLabelKey">The property key to access the labels of the multiedges.</param>
        /// <param name="MultiEdgeIdCreatorDelegate">A delegate to create a new multiedge identification.</param>
        /// <param name="DefaultMultiEdgeLabel">The default label of the multiedges.</param>
        /// 
        /// <param name="HyperEdgeIdKey">The property key to access the identifications of the hyperedges.</param>
        /// <param name="HyperEdgeRevIdKey">The property key to access the revision identifications of the hyperedges.</param>
        /// <param name="HyperEdgeLabelKey">The property key to access the labels of the hyperedges.</param>
        /// <param name="HyperEdgeIdCreatorDelegate">A delegate to create a new hyperedge identification.</param>
        /// <param name="DefaultHyperEdgeLabel">The default label of the hyperedges.</param>
        /// 
        /// <param name="VoteCreator">A delegate to create a new vote.</param>
        /// <param name="GraphInitializer">A delegate to initialize the new property graph.</param>
        internal GenericPropertyVertex(TIdVertex  GraphId,

                                       #region Vertices

                                       TKeyVertex VertexIdKey,
                                       TKeyVertex VertexRevIdKey,
                                       TKeyVertex VertexLabelKey,
                                       VertexIdCreatorDelegate   <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexIdCreatorDelegate,
                                       TVertexLabel DefaultVertexLabel,

                                       #endregion

                                       #region Edges

                                       TKeyEdge   EdgeIdKey,
                                       TKeyEdge   EdgeRevIdKey,
                                       TKeyEdge   EdgeLabelKey,
                                       EdgeIdCreatorDelegate     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeIdCreatorDelegate,
                                       TEdgeLabel DefaultEdgeLabel,

                                       #endregion

                                       #region MultiEdges

                                       TKeyMultiEdge MultiEdgeIdKey,
                                       TKeyMultiEdge MultiEdgeRevIdKey,
                                       TKeyMultiEdge MultiEdgeLabelKey,
                                       MultiEdgeIdCreatorDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeIdCreatorDelegate,
                                       TMultiEdgeLabel DefaultMultiEdgeLabel,

                                       #endregion

                                       #region HyperEdges

                                       TKeyHyperEdge HyperEdgeIdKey,
                                       TKeyHyperEdge HyperEdgeRevIdKey,
                                       TKeyHyperEdge HyperEdgeLabelKey,
                                       HyperEdgeIdCreatorDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeIdCreatorDelegate,
                                       THyperEdgeLabel DefaultHyperEdgeLabel,

                                       #endregion

                                       Func<IVote<Boolean>> VoteCreator = null,

                                       GraphInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GraphInitializer = null)

            // this == 01
            : this (GraphId,
                    DefaultVertexLabel,
                    VertexIdKey,
                    VertexRevIdKey,
                    VertexLabelKey,

                    () => new Dictionary<TKeyVertex, TValueVertex>(),
                    VoteCreator,

                    #region Vertices

                    // Create a new vertex identification
                    VertexIdCreatorDelegate,
                    DefaultVertexLabel,

                    // Create a new vertex
                    (Graph, _VertexId, _VertexLabel, VertexInitializer) =>

                        new GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                            (Graph,
                             _VertexId,
                             _VertexLabel,
                             VertexIdKey,
                             VertexRevIdKey,
                             VertexLabelKey,
                             () => new Dictionary<TKeyVertex, TValueVertex>(),

                             () => new GroupedCollection<TIdVertex,    IReadOnlyGenericPropertyVertex   <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TVertexLabel>(),

                             () => new GroupedCollection<TIdEdge,      IReadOnlyGenericPropertyEdge     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TEdgeLabel>(),

                             () => new GroupedCollection<TIdMultiEdge, IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TMultiEdgeLabel>(),

                             () => new GroupedCollection<TIdHyperEdge, IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, THyperEdgeLabel>(),

                             VoteCreator,
                             VertexInitializer),

                    // The vertices collection
                    () => new GroupedCollection<TIdVertex, IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TVertexLabel>(),

                    #endregion

                    #region Edges

                    // Create a new edge identification
                    EdgeIdCreatorDelegate,
                    DefaultEdgeLabel,

                    // Create a new edge
                    (Graph, OutVertex, InVertex, EdgeId, EdgeLabel, EdgeInitializer) =>
                        new GenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                            (Graph,
                             OutVertex,
                             InVertex,
                             EdgeId,
                             EdgeLabel,
                             EdgeIdKey,
                             EdgeRevIdKey,
                             EdgeLabelKey,
                             () => new Dictionary<TKeyEdge, TValueEdge>(),
                             VoteCreator,
                             EdgeInitializer),

                    // The edges collection
                    () => new GroupedCollection<TIdEdge, IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TEdgeLabel>(),

                    #endregion

                    #region MultiEdges

                    // Create a new multiedge identification
                    MultiEdgeIdCreatorDelegate,
                    DefaultMultiEdgeLabel,

                    // Create a new multiedge
                    (Graph, MultiEdgeId, MultiEdgeLabel, MultiEdgeInitializer, EdgeSelector, Edges) =>

                       new GenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                            (Graph,
                             MultiEdgeId,
                             MultiEdgeLabel,
                             MultiEdgeIdKey,
                             MultiEdgeRevIdKey,
                             MultiEdgeLabelKey,

                             () => new Dictionary<TKeyMultiEdge, TValueMultiEdge>(),
                             () => new GroupedCollection<TIdEdge, IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TEdgeLabel>(),

                             VoteCreator,
                             MultiEdgeInitializer,
                             EdgeSelector,
                             Edges),

                    // The multiedges collection
                    () => new GroupedCollection<TIdMultiEdge, IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TMultiEdgeLabel>(),
                    #endregion

                    #region HyperEdges

                    // Create a new hyperedge identification
                    HyperEdgeIdCreatorDelegate,
                    DefaultHyperEdgeLabel,

                    // Create a new hyperedge
                    (Graph, HyperEdgeId, HyperEdgeLabel, HyperEdgeInitializer, VertexSelector, Vertices) =>

                       new GenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                            (Graph,
                             HyperEdgeId,
                             HyperEdgeLabel,
                             HyperEdgeIdKey,
                             HyperEdgeRevIdKey,
                             HyperEdgeLabelKey,

                             () => new Dictionary<TKeyHyperEdge, TValueHyperEdge>(),
                             () => new GroupedCollection<TIdVertex, IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TVertexLabel>(),

                             VoteCreator,
                             HyperEdgeInitializer,
                             VertexSelector,
                             Vertices),

                    // The hyperedges collection
                    () => new GroupedCollection<TIdHyperEdge, IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, THyperEdgeLabel>()

                    #endregion

                   )

        {

            this._EdgeIdKey                = EdgeIdKey;
            this._EdgeRevIdKey             = EdgeRevIdKey;
            this._EdgeLabelKey             = EdgeLabelKey;

            this._MultiEdgeIdKey           = MultiEdgeIdKey;
            this._MultiEdgeRevIdKey        = MultiEdgeRevIdKey;
            this._MultiEdgeLabelKey        = MultiEdgeLabelKey;

            this._HyperEdgeIdKey           = HyperEdgeIdKey;
            this._HyperEdgeRevIdKey        = HyperEdgeRevIdKey;
            this._HyperEdgeLabelKey        = HyperEdgeLabelKey;

            if (GraphInitializer != null)
                GraphInitializer(this);

        }

        #endregion

        #region GenericPropertyVertex(...IIdCreators...)

        /// <summary>
        /// Creates a new class-based in-memory implementation of a generic property vertex.
        /// </summary>
        /// <param name="GraphId">The identification of this generic property graph.</param>
        /// 
        /// <param name="VertexIdKey">The property key to access the identifications of the vertices.</param>
        /// <param name="VertexRevIdKey">The property key to access the revision identifications of the vertices.</param>
        /// <param name="VertexLabelKey">The property key to access the labels of the vertices.</param>
        /// <param name="VertexIdCreator">A vertex identification creator.</param>
        /// <param name="DefaultVertexLabel">The default label of the vertices.</param>
        /// 
        /// <param name="EdgeIdKey">The property key to access the identifications of the edges.</param>
        /// <param name="EdgeRevIdKey">The property key to access the revision identifications of the edges.</param>
        /// <param name="EdgeLabelKey">The property key to access the labels of the edges.</param>
        /// <param name="EdgeIdCreator">A edge identification creator.</param>
        /// <param name="DefaultEdgeLabel">The default label of the edges.</param>
        /// 
        /// <param name="MultiEdgeIdKey">The property key to access the identifications of the multiedges.</param>
        /// <param name="MultiEdgeRevIdKey">The property key to access the revision identifications of the multiedges.</param>
        /// <param name="MultiEdgeLabelKey">The property key to access the labels of the multiedges.</param>
        /// <param name="MultiEdgeIdCreator">A multiedge identification creator.</param>
        /// <param name="DefaultMultiEdgeLabel">The default label of the multiedges.</param>
        /// 
        /// <param name="HyperEdgeIdKey">The property key to access the identifications of the hyperedges.</param>
        /// <param name="HyperEdgeRevIdKey">The property key to access the revision identifications of the hyperedges.</param>
        /// <param name="HyperEdgeLabelKey">The property key to access the labels of the hyperedges.</param>
        /// <param name="HyperEdgeIdCreator">A hyperedge identification creator.</param>
        /// <param name="DefaultHyperEdgeLabel">The default label of the hyperedges.</param>
        /// 
        /// <param name="VoteCreator">A delegate to create a new vote.</param>
        /// <param name="GraphInitializer">A delegate to initialize the new property graph.</param>
        public GenericPropertyVertex(TIdVertex  GraphId,

                                     // Vertices
                                     TKeyVertex VertexIdKey,
                                     TKeyVertex VertexRevIdKey,
                                     TKeyVertex VertexLabelKey,
                                     IIdGenerator<TIdVertex> VertexIdCreator,
                                     TVertexLabel DefaultVertexLabel,

                                     // Edges
                                     TKeyEdge   EdgeIdKey,
                                     TKeyEdge   EdgeRevIdKey,
                                     TKeyEdge   EdgeLabelKey,
                                     IIdGenerator<TIdEdge> EdgeIdCreator,
                                     TEdgeLabel DefaultEdgeLabel,

                                     // MultiEdges
                                     TKeyMultiEdge MultiEdgeIdKey,
                                     TKeyMultiEdge MultiEdgeRevIdKey,
                                     TKeyMultiEdge MultiEdgeLabelKey,
                                     IIdGenerator<TIdMultiEdge> MultiEdgeIdCreator,
                                     TMultiEdgeLabel DefaultMultiEdgeLabel,

                                     // HyperEdges
                                     TKeyHyperEdge HyperEdgeIdKey,
                                     TKeyHyperEdge HyperEdgeRevIdKey,
                                     TKeyHyperEdge HyperEdgeLabelKey,
                                     IIdGenerator<TIdHyperEdge> HyperEdgeIdCreator,
                                     THyperEdgeLabel DefaultHyperEdgeLabel,

                                     Func<IVote<Boolean>> VoteCreator = null,

                                     GraphInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GraphInitializer = null)

            : this (

                    GraphId,
                    DefaultVertexLabel,
                    VertexIdKey,
                    VertexRevIdKey,
                    VertexLabelKey,

                    () => new Dictionary<TKeyVertex, TValueVertex>(),
                    VoteCreator,

                    #region Vertices
                    
                    // Create a new vertex identification
                    graph => VertexIdCreator.NewId(NewId => !graph.HasVertexId(NewId)),
                    DefaultVertexLabel,

                    // Create a new vertex
                    (Graph, _VertexId, _VertexLabel, VertexInitializer) =>

                        new GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                            (Graph,
                             _VertexId,
                             _VertexLabel,
                             VertexIdKey,
                             VertexRevIdKey,
                             VertexLabelKey,
                             () => new Dictionary<TKeyVertex, TValueVertex>(),

                             () => new GroupedCollection<TIdVertex,    IReadOnlyGenericPropertyVertex   <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TVertexLabel>(),

                             () => new GroupedCollection<TIdEdge,      IReadOnlyGenericPropertyEdge     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TEdgeLabel>(),

                             () => new GroupedCollection<TIdMultiEdge, IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TMultiEdgeLabel>(),

                             () => new GroupedCollection<TIdHyperEdge, IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, THyperEdgeLabel>(),

                             VoteCreator,
                             VertexInitializer),

                    // The vertices collection
                    () => new GroupedCollection<TIdVertex, IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TVertexLabel>(),

                    #endregion

                    #region Edges

                    // Create a new edge identification
                    graph => EdgeIdCreator.NewId(NewId => !graph.HasEdgeId(NewId)),
                    DefaultEdgeLabel,

                    // Create a new edge
                    (Graph, OutVertex, InVertex, EdgeId, EdgeLabel, EdgeInitializer) =>
                        new GenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                            (Graph,
                             OutVertex,
                             InVertex,
                             EdgeId,
                             EdgeLabel,
                             EdgeIdKey,
                             EdgeRevIdKey,
                             EdgeLabelKey,
                             () => new Dictionary<TKeyEdge, TValueEdge>(),
                             VoteCreator,
                             EdgeInitializer),

                    // The edges collection
                    () => new GroupedCollection<TIdEdge, IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TEdgeLabel>(),

                    #endregion

                    #region MultiEdges

                    // Create a new multiedge identification
                    graph => MultiEdgeIdCreator.NewId(NewId => !graph.HasMultiEdgeId(NewId)),
                    DefaultMultiEdgeLabel,

                    // Create a new multiedge
                    (Graph, MultiEdgeId, MultiEdgeLabel, MultiEdgeInitializer, EdgeSelector, Edges) =>

                       new GenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                            (Graph,
                             MultiEdgeId,
                             MultiEdgeLabel,
                             MultiEdgeIdKey,
                             MultiEdgeRevIdKey,
                             MultiEdgeLabelKey,

                             () => new Dictionary<TKeyMultiEdge, TValueMultiEdge>(),
                             () => new GroupedCollection<TIdEdge, IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TEdgeLabel>(),

                             VoteCreator,
                             MultiEdgeInitializer,
                             EdgeSelector,
                             Edges),

                    // The multiedges collection
                    () => new GroupedCollection<TIdMultiEdge, IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TMultiEdgeLabel>(),
                    #endregion

                    #region HyperEdges

                    // Create a new hyperedge identification
                    graph => HyperEdgeIdCreator.NewId(NewId => !graph.HasHyperEdgeId(NewId)),
                    DefaultHyperEdgeLabel,

                    // Create a new hyperedge
                    (Graph, HyperEdgeId, HyperEdgeLabel, HyperEdgeInitializer, VertexSelector, Vertices) =>

                       new GenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                            (Graph,
                             HyperEdgeId,
                             HyperEdgeLabel,
                             HyperEdgeIdKey,
                             HyperEdgeRevIdKey,
                             HyperEdgeLabelKey,

                             () => new Dictionary<TKeyHyperEdge, TValueHyperEdge>(),
                             () => new GroupedCollection<TIdVertex, IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TVertexLabel>(),

                             VoteCreator,
                             HyperEdgeInitializer,
                             VertexSelector,
                             Vertices),

                    // The hyperedges collection
                    () => new GroupedCollection<TIdHyperEdge, IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, THyperEdgeLabel>()

                    #endregion

                   )

        {

            this._EdgeIdKey                = EdgeIdKey;
            this._EdgeRevIdKey             = EdgeRevIdKey;
            this._EdgeLabelKey             = EdgeLabelKey;

            this._MultiEdgeIdKey           = MultiEdgeIdKey;
            this._MultiEdgeRevIdKey        = MultiEdgeRevIdKey;
            this._MultiEdgeLabelKey        = MultiEdgeLabelKey;

            this._HyperEdgeIdKey           = HyperEdgeIdKey;
            this._HyperEdgeRevIdKey        = HyperEdgeRevIdKey;
            this._HyperEdgeLabelKey        = HyperEdgeLabelKey;

            if (GraphInitializer != null)
                GraphInitializer(this);

        }

        #endregion

        #region (private) GenericPropertyVertex(Graph, VertexId, VertexLabel, IdKey, RevIdKey, LabelKey, PropertiesInitializer, EdgeCollectionInitializer, HyperEdgeCollectionInitializer, VertexInitializer = null)

        /// <summary>
        /// Creates a new class-based in-memory implementation of a generic property vertex.
        /// </summary>
        /// <param name="Graph">The associated property graph.</param>
        /// <param name="VertexId">The identification of this vertex.</param>
        /// <param name="VertexLabel">The label of this vertex.</param>
        /// <param name="IdKey">The key to access the identifier of this vertex.</param>
        /// <param name="RevIdKey">The key to access the revision identifier of this vertex.</param>
        /// <param name="LabelKey">The key to access the label of this graph element.</param>
        /// <param name="DatastructureInitializer">A delegate to initialize the properties datastructure.</param>
        /// <param name="VerticesCollectionInitializer"></param>
        /// <param name="EdgesCollectionInitializer">A delegate to initialize the datastructure for storing all edges.</param>
        /// <param name="MultiEdgesCollectionInitializer"></param>
        /// <param name="HyperEdgesCollectionInitializer">A delegate to initialize the datastructure for storing all hyperedges.</param>
        /// <param name="VoteCreator">A delegate to create a new vote.</param>
        /// <param name="VertexInitializer">A delegate to initialize the newly created vertex.</param>
        private GenericPropertyVertex(IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,  
                                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,    
                                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph,

                                     TIdVertex     VertexId,
                                     TVertexLabel  VertexLabel,
                                     TKeyVertex    IdKey,
                                     TKeyVertex    RevIdKey,
                                     TKeyVertex    LabelKey,

                                     IDictionaryInitializer<TKeyVertex, TValueVertex> DatastructureInitializer,

                                     Func<IGroupedCollection<TIdVertex,    IReadOnlyGenericPropertyVertex   <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TVertexLabel>> VerticesCollectionInitializer,

                                     Func<IGroupedCollection<TIdEdge,      IReadOnlyGenericPropertyEdge     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TEdgeLabel>> EdgesCollectionInitializer,

                                     Func<IGroupedCollection<TIdMultiEdge, IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TMultiEdgeLabel>> MultiEdgesCollectionInitializer,

                                     Func<IGroupedCollection<TIdHyperEdge, IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, THyperEdgeLabel>> HyperEdgesCollectionInitializer,

                                     Func<IVote<Boolean>> VoteCreator = null,

                                     VertexInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexInitializer = null)

            : base(VertexId, VertexLabel, IdKey, RevIdKey, LabelKey, DatastructureInitializer, VoteCreator: VoteCreator)

        {

            #region Initial Checks

            if (Graph == null)
                throw new ArgumentNullException("The given graph must not be null!");

            if (VertexId == null)
                throw new ArgumentNullException("The given VertexId must not be null!");

            if (DatastructureInitializer == null)
                throw new ArgumentNullException("The given DatastructureInitializer must not be null!");

            if (VerticesCollectionInitializer == null)
                throw new ArgumentNullException("The given VerticesCollectionInitializer must not be null!");

            if (EdgesCollectionInitializer == null)
                throw new ArgumentNullException("The given EdgesCollectionInitializer must not be null!");

            if (MultiEdgesCollectionInitializer == null)
                throw new ArgumentNullException("The given MultiEdgesCollectionInitializer must not be null!");

            if (HyperEdgesCollectionInitializer == null)
                throw new ArgumentNullException("The given HyperEdgesCollectionInitializer must not be null!");

            #endregion

            this.SuperVertex            = this as IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;

            this.Subgraph               = this as IGenericPropertyGraph <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;

            this.Graph                  = Graph;
            this._VerticesWhenGraph     = VerticesCollectionInitializer();
            this._OutEdgesWhenVertex    = EdgesCollectionInitializer();
            this._InEdgesWhenVertex     = EdgesCollectionInitializer();
            this._EdgesWhenGraph        = EdgesCollectionInitializer();
            this._HyperEdgesWhenVertex  = HyperEdgesCollectionInitializer();

            // IGenericPropertyVertex

            #region OutEdgeAddition/-Removal

            this.OutEdgeAddition  = new VotingNotificator<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          Boolean>(VoteCreator, true);


            this.OutEdgeRemoval   = new VotingNotificator<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          Boolean>(VoteCreator, true);

            #endregion

            #region InEdgeAddition/-Removal

            this.InEdgeAddition   = new VotingNotificator<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          Boolean>(VoteCreator, true);


            this.InEdgeRemoval    = new VotingNotificator<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          Boolean>(VoteCreator, true);

            #endregion

            #region HyperEdgeAddition/-Removal

            this.HyperEdgeRemoval_WhenVertex = new VotingNotificator<IReadOnlyGenericPropertyVertex   <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                                     IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                                     Boolean>(VoteCreator, true);

            #endregion

            if (VertexInitializer != null)
                VertexInitializer(this);

        }

        #endregion

        #endregion


        // IGenericPropertyVertex

        #region OutEdge methods [IGenericPropertyVertex]

        #region OnOutEdgeAddition
        
        private readonly IVotingNotificator<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> OutEdgeAddition;

        /// <summary>
        /// Called whenever an outgoing edge will be or was added to the vertex.
        /// </summary>
        IVotingNotification<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            Boolean>
            
            IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.OnOutEdgeAddition

        {
            get
            {
                return OutEdgeAddition;
            }
        }

        #endregion

        #region AddOutEdge(Edge)

        /// <summary>
        /// Add an outgoing edge.
        /// </summary>
        /// <param name="Edge">The edge to add.</param>
        void IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                 AddOutEdge(IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)
        {

            if (OutEdgeAddition.SendVoting(this.SuperVertex.AsReadOnly(), Edge))
            {
                if (_OutEdgesWhenVertex.TryAddValue(Edge.Id, Edge, Edge.Label))    // Is supposed to be thread-safe!
                {
                    OutEdgeAddition.SendNotification(this.SuperVertex.AsReadOnly(), Edge);
                }
            }
            
        }

        #endregion


        #region OutEdges(params EdgeLabels)      // OutEdges()!

        /// <summary>
        /// The edges emanating from, or leaving, this vertex
        /// filtered by their label. If no label was given,
        /// all edges will be returned.
        /// </summary>
        IEnumerable<IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            OutEdges(params TEdgeLabel[] EdgeLabels)

        {

            if (EdgeLabels.Any())
                return from   Edge
                       in     _OutEdgesWhenVertex
                       where  EdgeLabels.Any(EdgeLabel => EdgeLabel.Equals(Edge.Label))
                       select Edge;

            else
                return from   Edge
                       in     _OutEdgesWhenVertex
                       select Edge;

        }

        #endregion

        #region OutEdges(EdgeFilter)

        /// <summary>
        /// The edges emanating from, or leaving, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        IEnumerable<IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
        
            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            OutEdges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)
        
        {

            if (EdgeFilter != null)
                return from   Edge
                       in     _OutEdgesWhenVertex
                       where  EdgeFilter(Edge)
                       select Edge;

            else
                return from   Edge
                       in     _OutEdgesWhenVertex
                       select Edge;

        }

        #endregion

        #region OutDegree(params EdgeLabels)     // OutDegree()!

        /// <summary>
        /// The number of edges emanating from, or leaving, this vertex
        /// filtered by their label. If no label was given,
        /// all edges will be returned.
        /// </summary>
        UInt64 IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.            

               OutDegree(params TEdgeLabel[] EdgeLabels)

        {

            if (EdgeLabels.Length == 0)
                return _OutEdgesWhenVertex.Count();

            var Counter = 0UL;

            if (EdgeLabels != null && EdgeLabels.Any())
            {
                foreach (var _Edge in _OutEdgesWhenVertex)
                    foreach (var _Label in EdgeLabels)
                        if (_Edge.Label.Equals(_Label))
                            Counter++;
            }

            else
                foreach (var _Edge in _OutEdgesWhenVertex)
                    Counter++;

            return Counter;

        }

        #endregion

        #region OutDegree(EdgeFilter)

        /// <summary>
        /// The number of edges emanating from, or leaving, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        UInt64 IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

               OutDegree(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)

        {

            if (EdgeFilter == null)
                return _OutEdgesWhenVertex.Count();

            return (UInt64) (from   Edge
                             in     _OutEdgesWhenVertex
                             where  EdgeFilter(Edge)
                             select Edge).LongCount();

        }

        #endregion


        #region OnOutEdgeRemoval

        private readonly IVotingNotificator<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyEdge <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> OutEdgeRemoval;

        /// <summary>
        /// Called whenever an outgoing edge will be or was removed to the vertex.
        /// </summary>
        IVotingNotification<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            Boolean>
            
            IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.OnOutEdgeRemoval

        {
            get
            {
                return OutEdgeRemoval;
            }
        }

        #endregion

        #region RemoveOutEdges(params Edges)    // RemoveOutEdges()!

        /// <summary>
        /// Remove outgoing edges.
        /// </summary>
        /// <param name="Edges">An array of outgoing edges to be removed.</param>
        void IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.            

             RemoveOutEdges(params IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[]
                                                        Edges)

        {

            lock (this)
            {

                foreach (var _Edge in _OutEdgesWhenVertex.ToArray())
                    _OutEdgesWhenVertex.TryRemoveValue(_Edge.Id, _Edge, _Edge.Label);    // Is supposed to be thread-safe!

            }

        }

        #endregion

        #region RemoveOutEdges(EdgeFilter)

        /// <summary>
        /// Remove any outgoing edge matching the
        /// given edge filter delegate.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        void IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.            

             RemoveOutEdges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)

        {

            if (EdgeFilter == null)
                throw new ArgumentNullException("The given edge filter delegate must not be null!");

            lock (this)
            {

                var _tmp = new List<IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>();

                if (EdgeFilter == null)
                    foreach (var _IEdge in _OutEdgesWhenVertex)
                        _tmp.Add(_IEdge);

                else foreach (var _IEdge in _OutEdgesWhenVertex)
                    if (EdgeFilter(_IEdge))
                        _tmp.Add(_IEdge);

                foreach (var _Edge in _tmp)
                    _OutEdgesWhenVertex.TryRemoveValue(_Edge.Id, _Edge, _Edge.Label);

            }

        }

        #endregion

        #endregion

        #region InEdge methods [IGenericPropertyVertex]

        #region OnInEdgeAddition
        
        private readonly IVotingNotificator<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> InEdgeAddition;

        /// <summary>
        /// Called whenever an incoming edge will be or was added to the vertex.
        /// </summary>
        IVotingNotification<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            Boolean>
            
            IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.OnInEdgeAddition

        {
            get
            {
                return InEdgeAddition;
            }
        }

        #endregion

        #region AddInEdge(Edge)

        /// <summary>
        /// Add an incoming edge.
        /// </summary>
        /// <param name="Edge">The edge to add.</param>
        void IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                 AddInEdge(IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)
        {

            if (InEdgeAddition.SendVoting(this.SuperVertex.AsReadOnly(), Edge))
            {
                if (_InEdgesWhenVertex.TryAddValue(Edge.Id, Edge, Edge.Label))    // Is supposed to be thread-safe!
                {
                    InEdgeAddition.SendNotification(this.SuperVertex.AsReadOnly(), Edge);
                }
            }
            
        }

        #endregion


        #region InEdges(params EdgeLabels)      // InEdges()!

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex
        /// filtered by their label. If no label was given,
        /// all edges will be returned.
        /// </summary>
        IEnumerable<IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            InEdges(params TEdgeLabel[] EdgeLabels)

        {

            if (EdgeLabels.Any())
                return from   Edge
                       in     _InEdgesWhenVertex
                       where  EdgeLabels.Any(EdgeLabel => EdgeLabel.Equals(Edge.Label))
                       select Edge;

            else
                return from   Edge
                       in     _InEdgesWhenVertex
                       select Edge;

        }

        #endregion

        #region InEdges(EdgeFilter)

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        IEnumerable<IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
        
            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            InEdges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)
        
        {

            if (EdgeFilter != null)
                return from   Edge
                       in     _InEdgesWhenVertex
                       where  EdgeFilter(Edge)
                       select Edge;

            else
                return from   Edge
                       in     _InEdgesWhenVertex
                       select Edge;

        }

        #endregion

        #region InDegree(params EdgeLabels)     // InDegree()!

        /// <summary>
        /// The number of edges incoming to, or arriving at, this vertex
        /// filtered by their label. If no label was given,
        /// all edges will be returned.
        /// </summary>
        UInt64 IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            InDegree(params TEdgeLabel[] EdgeLabels)

        {

            if (EdgeLabels.Length == 0)
                return _InEdgesWhenVertex.Count();

            var Counter = 0UL;

            if (EdgeLabels != null && EdgeLabels.Any())
            {
                foreach (var _Edge in _InEdgesWhenVertex)
                    foreach (var _Label in EdgeLabels)
                        if (_Edge.Label.Equals(_Label))
                            Counter++;
            }

            else
                foreach (var _Edge in _InEdgesWhenVertex)
                    Counter++;

            return Counter;

        }

        #endregion

        #region InDegree(EdgeFilter)

        /// <summary>
        /// The number of edges incoming to, or arriving at, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        UInt64 IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            
            InDegree(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)

        {

            if (EdgeFilter == null)
                return _InEdgesWhenVertex.Count();

            return (UInt64) (from   Edge
                             in     _InEdgesWhenVertex
                             where  EdgeFilter(Edge)
                             select Edge).LongCount();

        }

        #endregion


        #region OnInEdgeRemoval

        private readonly IVotingNotificator<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> InEdgeRemoval;

        /// <summary>
        /// Called whenever an incoming edge will be or was removed to the vertex.
        /// </summary>
        IVotingNotification<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            Boolean>
            
            IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.OnInEdgeRemoval

        {
            get
            {
                return InEdgeRemoval;
            }
        }

        #endregion

        #region RemoveInEdges(params Edges)    // RemoveInEdges()!

        /// <summary>
        /// Remove incoming edges.
        /// </summary>
        /// <param name="Edges">An array of incoming edges to be removed.</param>
        void IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            RemoveInEdges(params IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Edges)

        {

            lock (this)
            {

                foreach (var _Edge in _InEdgesWhenVertex.ToArray())
                    _InEdgesWhenVertex.TryRemoveValue(_Edge.Id, _Edge, _Edge.Label);     // Is supposed to be thread-safe!

            }

        }

        #endregion

        #region RemoveInEdges(EdgeFilter)

        /// <summary>
        /// Remove any incoming edge matching the
        /// given edge filter delegate.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        void IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            RemoveInEdges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)

        {

            if (EdgeFilter == null)
                throw new ArgumentNullException("The given edge filter delegate must not be null!");

            lock (this)
            {

                var _tmp = new List<IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>();

                if (EdgeFilter == null)
                    foreach (var _IEdge in _InEdgesWhenVertex)
                        _tmp.Add(_IEdge);

                else foreach (var _IEdge in _InEdgesWhenVertex)
                    if (EdgeFilter(_IEdge))
                        _tmp.Add(_IEdge);

                foreach (var _Edge in _tmp)
                    _InEdgesWhenVertex.TryRemoveValue(_Edge.Id, _Edge, _Edge.Label);    // Is supposed to be thread-safe!

            }

        }

        #endregion

        #endregion

        #region HyperEdge methods [IGenericPropertyVertex]

        #region OnHyperEdgeAddition

        private readonly IVotingNotificator<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> HyperEdgeAddition_WhenVertex;

        /// <summary>
        /// Called whenever a hyperedge will be or was added to the vertex.
        /// </summary>
        IVotingNotification<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            Boolean>
            
            IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.OnHyperEdgeAddition

        {
            get
            {
                return HyperEdgeAddition_WhenVertex;
            }
        }

        #endregion

        #region AddHyperEdge(HyperEdge)

        /// <summary>
        /// Add a hyperedge.
        /// </summary>
        /// <param name="HyperEdge">The hyperedge to add.</param>
        void IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            AddHyperEdge(IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdge)

        {
            _HyperEdgesWhenVertex.TryAddValue(HyperEdge.Id, HyperEdge, HyperEdge.Label);    // Is supposed to be thread-safe!
        }

        #endregion


        #region HyperEdges(params HyperEdgeLabels)      // HyperEdges()!

        IEnumerable<IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                HyperEdges(params THyperEdgeLabel[] HyperEdgeLabels)

        {

            if (HyperEdgeLabels != null && HyperEdgeLabels.Any())
            {
                foreach (var HyperEdge in _HyperEdgesWhenVertex)
                    foreach (var Label in HyperEdgeLabels)
                        if (HyperEdge.Label.Equals(Label))
                            yield return HyperEdge;
            }

            else
                foreach (var _HyperEdge in _HyperEdgesWhenVertex)
                    yield return _HyperEdge;

        }

        #endregion

        #region HyperEdges(HyperEdgeFilter = null)

        /// <summary>
        /// Get an enumeration of all HyperEdges in the graph.
        /// An optional HyperEdge filter may be applied for filtering.
        /// </summary>
        /// <param name="HyperEdgeFilter">A delegate for HyperEdge filtering.</param>
        IEnumerable<IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                HyperEdges(HyperEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter)

        {

            if (HyperEdgeFilter == null)
                return from   HyperEdge
                       in     _HyperEdgesWhenVertex
                       select HyperEdge;

            else
                return from   HyperEdge
                       in     _HyperEdgesWhenVertex
                       where  HyperEdgeFilter(HyperEdge)
                       select HyperEdge;

        }

        #endregion


        #region OnHyperEdgeRemoval

        private readonly IVotingNotificator<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> HyperEdgeRemoval_WhenVertex;

        /// <summary>
        /// Called whenever a hyperedge will be or was removed to the vertex.
        /// </summary>
        IVotingNotification<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            Boolean>

            IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.OnHyperEdgeRemoval

        {
            get
            {
                return HyperEdgeRemoval_WhenVertex;
            }
        }

        #endregion

        #region RemoveHyperEdges(params HyperEdges)

        void IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            RemoveHyperEdges(params IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] HyperEdges)

        {

            if (HyperEdges != null)
                HyperEdges.
                    ForEach(HyperEdge => this._HyperEdgesWhenVertex.TryRemoveValue(HyperEdge.Id, HyperEdge, HyperEdge.Label));

        }

        #endregion

        #region RemoveHyperEdges(HyperEdgeFilter = null)

        void IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            RemoveHyperEdges(HyperEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter)

        {

            if (HyperEdgeFilter != null)
                _HyperEdgesWhenVertex.
                    Where(HyperEdge => HyperEdgeFilter(HyperEdge)).ToArray().
                    ForEach(HyperEdge => this._HyperEdgesWhenVertex.TryRemoveValue(HyperEdge.Id, HyperEdge, HyperEdge.Label));

        }

        #endregion

        #endregion


        // IGenericPropertyGraph

        #region Vertex methods [IGenericPropertyGraph]

        #region OnVertexAddition

        private readonly IVotingNotificator<IReadOnlyGenericPropertyGraph <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> VertexAddition;

        /// <summary>
        /// Called whenever a vertex will be or was added to the graph.
        /// </summary>
        IVotingNotification<IReadOnlyGenericPropertyGraph <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            Boolean>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.OnVertexAddition

        {
            get
            {
                return VertexAddition;
            }
        }

        #endregion

        #region (private) AddVertexToGraph(Vertex)

        /// <summary>
        /// Add the given vertex to the graph.
        /// </summary>
        /// <param name="Vertex">A vertex.</param>
        /// <returns>The given vertex</returns>
        IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

               AddVertexToGraph(IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex)

        {

            if (VertexAddition.SendVoting(this, Vertex))
            {
                if (_VerticesWhenGraph.TryAddValue(Vertex.Id, Vertex, Vertex.Label))
                {
                    VertexAddition.SendNotification(this, Vertex);
                    return Vertex;
                }
            }

            return null;

        }

        #endregion

        #region AddVertex(VertexInitializer = null)

        /// <summary>
        /// Adds a vertex to the graph using the given VertexId and initializes
        /// the vertex by invoking the given vertex initializer.
        /// </summary>
        /// <param name="VertexInitializer">A delegate to initialize the new vertex.</param>
        /// <returns>The new vertex</returns>
        IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

               AddVertex(VertexInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexInitializer)

        {

            #region Initial checks

            var Id = _VertexIdCreatorDelegate(this);

            if (_VerticesWhenGraph.ContainsKey(Id))
                throw new DuplicateVertexIdException<TIdVertex>(Id);

            #endregion

            return AddVertexToGraph(_VertexCreatorDelegate(this, Id, _DefaultVertexLabel, VertexInitializer));

        }

        #endregion

        #region AddVertex(Label, VertexInitializer = null)

        /// <summary>
        /// Adds a vertex to the graph using the given VertexId and initializes
        /// the vertex by invoking the given vertex initializer.
        /// </summary>
        /// <param name="Label">The label (or type) of the vertex.</param>
        /// <param name="VertexInitializer">A delegate to initialize the new vertex.</param>
        /// <returns>The new vertex</returns>
        IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

               AddVertex(TVertexLabel Label,
                         VertexInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexInitializer)

        {

            #region Initial checks

            var VertexId = _VertexIdCreatorDelegate(this);

            if (_VerticesWhenGraph.ContainsKey(VertexId))
                throw new DuplicateVertexIdException<TIdVertex>(VertexId);

            if (Label == null)
                Label = _DefaultVertexLabel;

            #endregion

            return AddVertexToGraph(_VertexCreatorDelegate(this, VertexId, Label, VertexInitializer));

        }

        #endregion

        #region AddVertex(Id, Label, VertexInitializer = null)

        /// <summary>
        /// Adds a vertex to the graph using the given VertexId and initializes
        /// the vertex by invoking the given vertex initializer.
        /// </summary>
        /// <param name="Id">A VertexId. If none was given a new one will be generated.</param>
        /// <param name="Label">The label (or type) of the vertex.</param>
        /// <param name="VertexInitializer">A delegate to initialize the new vertex.</param>
        /// <returns>The new vertex</returns>
        IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

               AddVertex(TIdVertex    Id,
                         TVertexLabel Label,
                         VertexInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexInitializer)

        {

            #region Initial checks

            if (Id == null)
                Id = _VertexIdCreatorDelegate(this);

            if (_VerticesWhenGraph.ContainsKey(Id))
                throw new DuplicateVertexIdException<TIdVertex>(Id);

            if (Label == null)
                Label = _DefaultVertexLabel;

            #endregion

            return AddVertexToGraph(_VertexCreatorDelegate(this, Id, Label, VertexInitializer));

        }

        #endregion

        #region AddVertexIfNotExists(Id, Label, VertexInitializer = null, OnDuplicateVertexId = null, ElseDo = null, AnywayDo = null)

        /// <summary>
        /// Create a new vertex using the given vertex identifier, label and optional
        /// vertex initializer, if the given vertex identifier is not already being
        /// used by the graph to reference another vertex. An exception will be thrown
        /// if the given vertex identifier is already being to reference an existing
        /// vertex but having a different vertex label.
        /// </summary>
        /// <param name="Id">The vertex identifier.</param>
        /// <param name="Label">The label (or type) of the vertex.</param>
        /// <param name="VertexInitializer">A delegate to initialize the new vertex.</param>
        /// <param name="OnDuplicateVertexId">Call this delegate for a duplicate vertex identification, or throw an exception if undefined.</param>
        /// <param name="AnywayDo">A delegate to do something with the vertex, no matter if is was newly created or already existing.</param>
        /// <returns>The newly created or already existing vertex.</returns>
        IReadOnlyGenericPropertyVertex<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                AddVertexIfNotExists(TIdVertex    Id,
                                     TVertexLabel Label,
                                     VertexInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexInitializer,

                                     VertexAction     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnDuplicateVertexId,

                                     VertexInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> ElseDo,

                                     VertexInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> AnywayDo)

        {

            #region Initial checks

            if (Id == null)
                Id = _VertexIdCreatorDelegate(this);

            #endregion

            #region If the vertex already exists...

            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _Vertex = null;

            if (_VerticesWhenGraph.TryGetByKey(Id, out _Vertex))
            {

                if (Label.Equals(_Vertex.Label))
                {

                    if (ElseDo != null)
                        ElseDo(_Vertex.AsMutable());

                    if (AnywayDo != null)
                        AnywayDo(_Vertex.AsMutable());

                    return _Vertex;

                }
                else
                {

                    if (OnDuplicateVertexId != null)
                        OnDuplicateVertexId(_Vertex);
                    else
                        throw new DuplicateVertexIdException<TIdVertex>(_Vertex.Id);

                }

            }

            #endregion

            #region ...or, if not...

            if (Label == null)
                Label = _DefaultVertexLabel;

            #endregion

            var __Vertex = AddVertexToGraph(_VertexCreatorDelegate(this, Id, Label, VertexInitializer));

            if (AnywayDo != null)
                AnywayDo(__Vertex.AsMutable());

            return __Vertex;

        }

        #endregion

        #region AddVertex(Vertex)

        /// <summary>
        /// Adds the given vertex to the graph, and returns it again.
        /// An exception will be thrown if the vertex identifier is already being
        /// used by the graph to reference another vertex.
        /// </summary>
        /// <param name="Vertex">A vertex.</param>
        /// <returns>The given IGenericPropertyVertex.</returns>
        IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IVertexMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                AddVertex(IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex)

        {

            #region Initial checks

            if (Vertex == null)
                throw new ArgumentNullException("The given vertex must not be null!");

            if (Vertex.Id == null)
                throw new ArgumentNullException("The unique identification of vertex must not be null!");

            if (_VerticesWhenGraph.ContainsKey(Vertex.Id))
                throw new DuplicateVertexIdException<TIdVertex>(Vertex.Id);

            #endregion

            return AddVertexToGraph(Vertex);

        }

        #endregion

        #region AddVertexIfNotExists(Vertex, CheckExistanceDelegate = null)

        /// <summary>
        /// Adds the given vertex to the graph if the given check existance
        /// delegate returns true and the vertex identifier is not already
        /// being used by the graph to reference another vertex.
        /// </summary>
        /// <param name="Vertex">A Vertex.</param>
        /// <param name="CheckExistanceDelegate">A delegate the check the existance of the given vertex within the given graph.</param>
        /// <returns>The given vertex.</returns>
        IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            AddVertexIfNotExists(IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex,

                                 CheckVertexExistanceInGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> CheckExistanceDelegate)

        {

            #region Initial checks

            if (Vertex == null)
                throw new ArgumentNullException("The given vertex must not be null!");

            if (Vertex.Id == null)
                throw new ArgumentNullException("The unique identification of vertex must not be null!");

            if (_VerticesWhenGraph.ContainsKey(Vertex.Id))
                throw new DuplicateVertexIdException<TIdVertex>(Vertex.Id);

            #endregion

            if (CheckExistanceDelegate != null && !CheckExistanceDelegate(this.Graph, Vertex))
                throw new Exception("CheckExistanceDelegate constraint is violated!");

            return AddVertexToGraph(Vertex);

        }

        #endregion


        #region VertexById(Id)

        /// <summary>
        /// Return the vertex referenced by the given vertex identifier.
        /// If no vertex is referenced by the identifier return null.
        /// </summary>
        /// <param name="Id">A vertex identifier.</param>
        IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IReadOnlyVertexMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                VertexById(TIdVertex Id)

        {

            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex = null;

            if (Id != null && _VerticesWhenGraph.TryGetByKey(Id, out Vertex))
                return Vertex;

            return null;

        }

        #endregion

        #region TryGetVertexById(Id, out Vertex)

        /// <summary>
        /// Try to return the vertex referenced by the given vertex identifier.
        /// </summary>
        /// <param name="Id">A vertex identifier.</param>
        /// <param name="Vertex">A vertex.</param>
        /// <returns>True when success; false otherwise.</returns>
        Boolean IReadOnlyVertexMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                TryGetVertexById(TIdVertex Id,
                                 out IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex)
        {

            if (Id != null && _VerticesWhenGraph.TryGetByKey(Id, out Vertex))
                return true;

            Vertex = null;
            return false;

        }

        #endregion

        #region HasVertexId(Id)

        /// <summary>
        /// Check if the given vertex identifier already exists within the graph.
        /// </summary>
        /// <param name="Id">A vertex identifier.</param>
        /// <returns>True when yes; false otherwise.</returns>
        Boolean IReadOnlyVertexMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            HasVertexId(TIdVertex Id)

        {

            if (Id == null)
                return false;

            return _VerticesWhenGraph.ContainsKey(Id);

        }

        #endregion

        #region VerticesById(params Ids)

        /// <summary>
        /// Return the vertices referenced by the given array of vertex identifiers.
        /// If no vertex is referenced by a given identifier this value will be null.
        /// </summary>
        /// <param name="Ids">An array of vertex identifiers.</param>
        IEnumerable<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IReadOnlyVertexMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                VerticesById(params TIdVertex[] Ids)

        {

            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex;

            if (Ids != null)
                foreach (var Id in Ids)
                {

                    if (Id != null && _VerticesWhenGraph.TryGetByKey(Id, out Vertex))
                        yield return Vertex;

                    else
                        yield return null;

                }

        }

        #endregion

        #region VerticesByLabel(params Labels)

        /// <summary>
        /// Return all vertices having one of the given vertex labels.
        /// </summary>
        /// <param name="Labels">An array of vertex labels.</param>
        IEnumerable<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IReadOnlyVertexMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                VerticesByLabel(params TVertexLabel[] Labels)

        {

            IEnumerable<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Vertices;

            if (Labels != null)
                foreach (var Label in Labels)
                    if (Label != null && _VerticesWhenGraph.TryGetByGroup(Label, out Vertices))
                        foreach (var Vertex in Vertices)
                            yield return Vertex;

        }

        #endregion

        #region Vertices(Include = null)

        /// <summary>
        /// Return all vertices or only the ones matching the given filter delegate.
        /// </summary>
        /// <param name="Include">An optional delegate to select the vertices to be returned.</param>
        IEnumerable<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IReadOnlyVertexMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                Vertices(VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Include)

        {

            return (Include == null) ? _VerticesWhenGraph
                                     : _VerticesWhenGraph.Where(v => Include(v));

        }

        #endregion

        #region NumberOfVertices(Include = null)

        /// <summary>
        /// Return the total number of vertices or only the number of vertices matching the given filter delegate.
        /// When the filter is null, this method should implement an optimized
        /// way to get the currenty number of vertices.
        /// </summary>
        /// <param name="Include">An optional delegate to select the vertices to be counted.</param>
        UInt64 IReadOnlyVertexMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            NumberOfVertices(VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Include)

        {

            return (Include == null) ? _VerticesWhenGraph.Count()
                                     : Convert.ToUInt64(_VerticesWhenGraph.LongCount(v => Include(v)));

        }

        #endregion


        #region OnVertexRemoval

        private readonly IVotingNotificator<IReadOnlyGenericPropertyGraph <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> VertexRemoval;

        /// <summary>
        /// Called whenever a vertex will be or was removed from the graph.
        /// </summary>
        IVotingNotification<IReadOnlyGenericPropertyGraph <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            Boolean>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.OnVertexRemoval

        {
            get
            {
                return VertexRemoval;
            }
        }

        #endregion

        #region RemoveVerticesById(params VertexIds)

        /// <summary>
        /// Remove the vertex identified by the given VertexId from the graph
        /// </summary>
        /// <param name="VertexIds">An array of VertexIds of the vertices to remove.</param>
        void IVertexMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            RemoveVerticesById(params TIdVertex[] VertexIds)

        {

            #region Initial checks

            if (VertexIds == null)
                return;

            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex;

            #endregion

            foreach (var VertexId in VertexIds)
            {
                if (_VerticesWhenGraph.TryGetByKey(VertexId, out Vertex))
                    this.Graph.AsMutable().RemoveVertices(Vertex);
                else
                    throw new ArgumentException("The given vertex identifier '" + VertexId.ToString() + "' is unknown!");
            }

        }

        #endregion

        #region RemoveVertices(params Vertices)

        /// <summary>
        /// Remove the given array of vertices from the graph.
        /// Upon removing a vertex, all the edges by which the vertex
        /// is connected will be removed as well.
        /// </summary>
        /// <param name="Vertices">An array of vertices to be removed from the graph.</param>
        void IVertexMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

             RemoveVertices(params IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)

        {

            #region Initial checks

            if (Vertices == null)
                return;

            #endregion

            lock (this)
            {

                foreach (var Vertex in Vertices)
                    if (_VerticesWhenGraph.ContainsKey(Vertex.Id))
                    {

                        if (Vertex.OutDegree() > 0 || Vertex.InDegree() > 0)
                            throw new Exception("Vertex '" + Vertex.Id + "' could not be removed, as there are still edges attached!");

                        if (VertexRemoval.SendVoting(this, Vertex))
                        {

                            // Remove the vertex from any 'indexing' hyperedge...
                            Vertex.HyperEdges().ForEach(HyperEdge => HyperEdge.AsMutable().RemoveVertices(Vertex));

                            _VerticesWhenGraph.TryRemoveValue(Vertex.Id, Vertex.AsMutable(), Vertex.Label);
                            VertexRemoval.SendNotification(this, Vertex);

                        }

                    }

            }

        }

        #endregion

        #region RemoveVertices(VertexFilter = null)

        /// <summary>
        /// Remove each vertex matching the given filter.
        /// </summary>
        /// <param name="VertexFilter">A delegate for vertex filtering.</param>
        void IVertexMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

             RemoveVertices(VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexFilter)
        {

            lock (this)
            {

                if (VertexFilter == null)
                    _VerticesWhenGraph.ToArray().
                        ForEach(Vertex => this.Graph.AsMutable().
                            RemoveVertices(Vertex));

                else
                    _VerticesWhenGraph.
                        Where(Vertex => VertexFilter(Vertex)).ToArray().
                        ForEach(Vertex => this.Graph.AsMutable().
                            RemoveVertices(Vertex));

            }

        }

        #endregion

        #endregion

        #region Edge methods [IGenericPropertyGraph]

        #region OnEdgeAddition
        
        private readonly IVotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyEdge <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> EdgeAddition;

        /// <summary>
        /// Called whenever an edge will be or was added to the graph.
        /// </summary>
        IVotingNotification<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            IReadOnlyGenericPropertyEdge <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            Boolean>
            
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.OnEdgeAddition

        {
            get
            {
                return EdgeAddition;
            }
        }

        #endregion

        #region (private) AddEdgeToGraph(Edge)

        /// <summary>
        /// Add the given edge to the graph.
        /// </summary>
        /// <param name="Edge">An edge.</param>
        /// <returns>The given edge</returns>
        IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

               AddEdgeToGraph(IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)

        {

            if (EdgeAddition.SendVoting(this, Edge))
            {
                if (_EdgesWhenGraph.TryAddValue(Edge.Id, Edge.AsMutable(), Edge.Label))
                {
                    Edge.OutVertex.AsMutable().AddOutEdge(Edge);
                    Edge.InVertex.AsMutable().AddInEdge(Edge);
                    EdgeAddition.SendNotification(this, Edge);
                    return Edge;
                }
            }

            return null;

        }

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
        IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                AddEdge(IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

                        TEdgeLabel Label,

                        IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

                        EdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer)

        {
            return Subgraph.AddEdge(OutVertex, InVertex, _EdgeIdCreatorDelegate(this), Label, EdgeInitializer);
        }

        #endregion

        #region AddEdgeIfNotExists(OutVertex, Label, InVertex, EdgeInitializer = null, ElseDo = null, AnywayDo = null)

        /// <summary>
        /// Add an edge to the graph, if it dows not exist already.
        /// The added edge requires a tail vertex, a head vertex, an identifier,
        /// a label and initializes the edge by invoking the given EdgeInitializer.
        /// OutVertex --Label-> InVertex is the "Semantic Web Notation" ;)
        /// </summary>
        /// <param name="OutVertex">The vertex on the tail of the edge.</param>
        /// <param name="Label">The label associated with the edge.</param>
        /// <param name="InVertex">The vertex on the head of the edge.</param>
        /// <param name="EdgeInitializer">A delegate to initialize the newly created edge.</param>
        /// <param name="ElseDo">A delegate to modify the existing edge.</param>
        /// <param name="AnywayDo">A delegate to do something with the edge, no matter if is was newly created or already existing.</param>
        /// <returns>The new or old edge.</returns>
        IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                AddEdgeIfNotExists(IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

                                   TEdgeLabel Label,

                                   IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

                                   EdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer,

                                   EdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> ElseDo,

                                   EdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> AnywayDo)

        {

            #region Initial checks

            if (OutVertex == null)
                throw new ArgumentException("The OutVertex must not be null!");

            if (InVertex == null)
                throw new ArgumentException("The InVertex must not be null!");

            if (Label == null)
                Label = _DefaultEdgeLabel;

            #endregion

            #region If the edge already exists...

            var ExistingEdge = OutVertex.Edges(e => e.Label.Equals(Label) &&
                                                    e.InVertex.Equals(InVertex)).FirstOrDefault();

            if (ExistingEdge != null)
            {

                if (ElseDo != null)
                    ElseDo(ExistingEdge.AsMutable());

                if (AnywayDo != null)
                    AnywayDo(ExistingEdge.AsMutable());

                return ExistingEdge;

            }

            #endregion

            #region ...or, if not...

            var EdgeId = _EdgeIdCreatorDelegate(this);

            return AddEdgeToGraph(_EdgeCreatorDelegate(this, OutVertex, InVertex, EdgeId, Label, e => { EdgeInitializer(e); AnywayDo(e); }));

            #endregion

        }

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
        IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                AddEdge(TIdEdge     EdgeId,

                        IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

                        TEdgeLabel  Label,

                        IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

                        EdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer)


        {

            return Subgraph.AddEdge(OutVertex, InVertex, EdgeId, Label, EdgeInitializer);

        }

        #endregion

        #region AddEdgeIfNotExists(Id, OutVertex, Label, InVertex, EdgeInitializer = null, ElseDo = null, AnywayDo = null)

        /// <summary>
        /// Add an edge to the graph, if it dows not exist already.
        /// The added edge requires a tail vertex, a head vertex, an identifier,
        /// a label and initializes the edge by invoking the given EdgeInitializer.
        /// OutVertex --Label-> InVertex is the "Semantic Web Notation" ;)
        /// </summary>
        /// <param name="Id">The unique identification of the edge. If none was given a new one will be generated.</param>
        /// <param name="OutVertex">The vertex on the tail of the edge.</param>
        /// <param name="Label">The label associated with the edge.</param>
        /// <param name="InVertex">The vertex on the head of the edge.</param>
        /// <param name="EdgeInitializer">A delegate to initialize the newly created edge.</param>
        /// <param name="ElseDo">A delegate to modify the existing edge.</param>
        /// <param name="AnywayDo">A delegate to do something with the edge, no matter if is was newly created or already existing.</param>
        /// <returns>The new or old edge.</returns>
        IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                AddEdgeIfNotExists(TIdEdge Id,

                                   IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

                                   TEdgeLabel Label,

                                   IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

                                   EdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer,

                                   EdgeAction     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnDuplicateEdge,

                                   EdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> ElseDo,

                                   EdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> AnywayDo)

        {

            #region Initial checks

            if (Id == null)
                Id = _EdgeIdCreatorDelegate(this);

            IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _Edge = null;

            if (OutVertex == null)
                throw new ArgumentException("The OutVertex must not be null!");

            if (InVertex == null)
                throw new ArgumentException("The InVertex must not be null!");

            if (Label == null)
                Label = _DefaultEdgeLabel;

            #endregion

            #region If the edge already exists...

            if (_EdgesWhenGraph.TryGetByKey(Id, out _Edge))
            {

                if (!OutVertex.Equals(_Edge.OutVertex) ||
                        !Label.Equals(_Edge.Label) ||
                     !InVertex.Equals(_Edge.InVertex))
                {
                    if (OnDuplicateEdge != null)
                        OnDuplicateEdge(_Edge);
                    else
                        throw new DuplicateEdgeIdException<TIdEdge>(_Edge.Id);
                }

                if (ElseDo != null)
                    ElseDo(_Edge.AsMutable());

                if (AnywayDo != null)
                    AnywayDo(_Edge.AsMutable());

                return _Edge;

            }


            #endregion

            #region ...or, if not...

            var __Edge = AddEdgeToGraph(_EdgeCreatorDelegate(this, OutVertex, InVertex, Id, Label, EdgeInitializer));

            if (AnywayDo != null)
                AnywayDo(__Edge.AsMutable());

            return __Edge;

            #endregion

        }

        #endregion

        #region AddEdge(OutVertex, InVertex, Label = default, EdgeInitializer = null)

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
        IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                AddEdge(IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

                        IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

                        TEdgeLabel  Label,

                        EdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer)


        {

            return Subgraph.AddEdge(OutVertex, InVertex, _EdgeIdCreatorDelegate(this), Label, EdgeInitializer);

        }

        #endregion

        #region AddEdge(OutVertex, InVertex, EdgeId, Label = default, EdgeInitializer = null)

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
        IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                AddEdge(IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

                        IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

                        TIdEdge     EdgeId,
                        TEdgeLabel  Label,

                        EdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer)

        {

            #region Initial checks

            if (EdgeId == null)
                EdgeId = _EdgeIdCreatorDelegate(this);

            if (_EdgesWhenGraph.ContainsKey(EdgeId))
                throw new ArgumentException("Another edge with id " + EdgeId + " already exists!");

            if (OutVertex == null)
                throw new ArgumentException("The OutVertex must not be null!");

            if (InVertex == null)
                throw new ArgumentException("The InVertex must not be null!");

            if (Label == null)
                Label = _DefaultEdgeLabel;

            #endregion

            return AddEdgeToGraph(_EdgeCreatorDelegate(this, OutVertex, InVertex, EdgeId, Label, EdgeInitializer));

        }

        #endregion

        #region AddEdge(Edge)

        /// <summary>
        /// Adds the given edge to the graph, and returns it again.
        /// An exception will be thrown if the edge identifier is already being
        /// used by the graph to reference another edge.
        /// </summary>
        /// <param name="Edge">An edge.</param>
        /// <returns>The given edge.</returns>
        IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                AddEdge(IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)

        {

            #region Initial checks

            if (Edge == null)
                throw new ArgumentNullException("The given edge must not be null!");

            if (Edge.Id == null)
                throw new ArgumentNullException("The unique identification of edge must not be null!");

            if (_EdgesWhenGraph.ContainsKey(Edge.Id))
                throw new DuplicateEdgeIdException<TIdEdge>(Edge.Id);

            #endregion

            return AddEdgeToGraph(Edge);

        }

        #endregion

        #region AddEdgeIfNotExists(Edge)

        /// <summary>
        /// Adds the given edge to the graph, and returns it again.
        /// An exception will be thrown if the edge identifier is already being
        /// used by the graph to reference another edge.
        /// </summary>
        /// <param name="Edge">An edge.</param>
        /// <param name="CheckExistanceDelegate">A delegate the check the existance of the given vertex within the given graph.</param>
        /// <returns>The given edge.</returns>
        IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                AddEdgeIfNotExists(IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge,

                                   CheckEdgeExistanceInGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> CheckExistanceDelegate)

        {

            #region Initial checks

            if (Edge == null)
                throw new ArgumentNullException("The given edge must not be null!");

            if (Edge.Id == null)
                throw new ArgumentNullException("The unique identification of edge must not be null!");

            if (_EdgesWhenGraph.ContainsKey(Edge.Id))
                throw new DuplicateEdgeIdException<TIdEdge>(Edge.Id);

            #endregion

            if (CheckExistanceDelegate != null && !CheckExistanceDelegate(this.Graph, Edge))
                throw new Exception("CheckExistanceDelegate constraint is violated!");

            return AddEdgeToGraph(Edge);

        }

        #endregion


        #region EdgeById(Id)

        /// <summary>
        /// Return the edge referenced by the given edge identifier.
        /// If no edge is referenced by a given identifier return null.
        /// </summary>
        /// <param name="Id">An edge identifier.</param>
        IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IReadOnlyEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                EdgeById(TIdEdge Id)

        {

            IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge = null;

            if (Id != null && _EdgesWhenGraph.TryGetByKey(Id, out Edge))
                return Edge;

            return null;

        }

        #endregion

        #region TryGetEdgeById(Id)

        /// <summary>
        /// Try to return the edge referenced by the given edge identifier.
        /// </summary>
        /// <param name="Id">An edge identifier.</param>
        /// <param name="Edge">An edge.</param>
        /// <returns>True when success; false otherwise.</returns>
        Boolean IReadOnlyEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            TryGetEdgeById(TIdEdge Id,
                           out IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)

        {

            if (Id != null && _EdgesWhenGraph.TryGetByKey(Id, out Edge))
                return true;

            Edge = null;
            return false;

        }

        #endregion

        #region HasEdgeId(Id)

        /// <summary>
        /// Check if the given edge identifier already exists within the graph.
        /// </summary>
        /// <param name="Id">An edge identifier.</param>
        /// <returns>True when yes; false otherwise.</returns>
        Boolean IReadOnlyEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            HasEdgeId(TIdEdge Id)

        {

            if (Id == null)
                return false;

            return _EdgesWhenGraph.ContainsKey(Id);

        }

        #endregion

        #region EdgesById(params Ids)

        /// <summary>
        /// Return the edges referenced by the given array of edge identifiers.
        /// If no edge is referenced by a given identifier this value will be null.
        /// </summary>
        /// <param name="Ids">An array of edge identifiers.</param>
        IEnumerable<IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IReadOnlyEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                EdgesById(params TIdEdge[] Ids)

        {

            IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge;

            if (Ids != null)
                foreach (var Id in Ids)
                {

                    if (Id != null && _EdgesWhenGraph.TryGetByKey(Id, out Edge))
                        yield return Edge;

                    else
                        yield return null;

                }

        }

        #endregion

        #region EdgesByLabel(params Labels)

        /// <summary>
        /// Return all edges having one of the given labels.
        /// </summary>
        /// <param name="Labels">An array of edge labels.</param>
        IEnumerable<IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
            IReadOnlyEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                EdgesByLabel(params TEdgeLabel[] Labels)

        {

            IEnumerable<IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Edges;

            if (Labels != null)
                foreach (var Label in Labels)
                    if (Label != null && _EdgesWhenGraph.TryGetByGroup(Label, out Edges))
                        foreach (var Edge in Edges)
                            yield return Edge;

        }

        #endregion

        #region Edges(Include = null)

        /// <summary>
        /// Return all edges or only the ones matching the given filter delegate.
        /// </summary>
        /// <param name="Include">An optional delegate to select the edges to be returned.</param>
        IEnumerable<IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IReadOnlyEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                Edges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Include)
        {

            return (Include == null) ? _EdgesWhenGraph
                                     : _EdgesWhenGraph.Where(e => Include(e));

        }

        #endregion

        #region NumberOfEdges(Include = null)

        /// <summary>
        /// Return the total number of edges or only the number of edges matching the given filter delegate.
        /// When the filter is null, this method should implement an optimized
        /// way to get the currenty number of edges.
        /// </summary>
        /// <param name="Include">An optional delegate to select the edges to be counted.</param>
        UInt64 IReadOnlyEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            NumberOfEdges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Include)

        {

            return (Include == null) ? _EdgesWhenGraph.Count()
                                     : Convert.ToUInt64(_EdgesWhenGraph.LongCount(e => Include(e)));

        }

        #endregion


        #region OnEdgeRemoval

        private readonly IVotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyEdge <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> EdgeRemoval;


        /// <summary>
        /// Called whenever an edge will be or was removed from the graph.
        /// </summary>
        IVotingNotification<IReadOnlyGenericPropertyGraph<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex,
                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            IReadOnlyGenericPropertyEdge  <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            Boolean>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.OnEdgeRemoval

        {
            get
            {
                return EdgeRemoval;
            }
        }

        #endregion

        #region RemoveEdgesById(params EdgeIds)

        /// <summary>
        /// Remove the given array of edges identified by their EdgeIds.
        /// </summary>
        /// <param name="EdgeIds">An array of EdgeIds of the edges to remove</param>
        void IEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            RemoveEdgesById(params TIdEdge[] EdgeIds)

        {

            #region Initial checks

            if (EdgeIds == null)
                return;

            IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge;

            #endregion

            foreach (var EdgeId in EdgeIds)
            {
                if (_EdgesWhenGraph.TryGetByKey(EdgeId, out Edge))
                    this.Graph.AsMutable().RemoveEdges(Edge);
                else
                    throw new ArgumentException("The given edge identifier '" + EdgeId.ToString() + "' is unknown!");
            }

        }

        #endregion

        #region RemoveEdges(params Edges)    // RemoveEdges()!

        /// <summary>
        /// Remove the given array of edges from the graph.
        /// </summary>
        /// <param name="Edges">An array of edges to be removed from the graph.</param>
        void IEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

             RemoveEdges(params IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Edges)

        {

            #region Initial checks

            if (Edges == null)
                return;

            #endregion

            lock (this)
            {

                foreach (var Edge in Edges)
                    if (_EdgesWhenGraph.ContainsKey(Edge.Id) &&
                        EdgeRemoval.SendVoting(this, Edge))
                        {

                            Edge.OutVertex.AsMutable().RemoveOutEdges(Edge.AsMutable());
                            Edge.InVertex. AsMutable().RemoveInEdges (Edge.AsMutable());

                            // Remove the edge from any 'indexing' multiedge...
                            Edge.MultiEdges().ForEach(MultiEdge => MultiEdge.AsMutable().RemoveEdges(Edge));

                            _EdgesWhenGraph.TryRemoveValue(Edge.Id, Edge.AsMutable(), Edge.Label);

                            EdgeRemoval.SendNotification(this, Edge);

                        }

            }

        }

        #endregion

        #region RemoveEdges(EdgeFilter = null)

        /// <summary>
        /// Remove any edge matching the given edge filter.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        void IEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

             RemoveEdges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)
        {

            lock (this)
            {

                if (EdgeFilter == null)
                    _EdgesWhenGraph.ToArray().
                        ForEach(Edge => this.Graph.AsMutable().
                            RemoveEdges(Edge));

                else
                    _EdgesWhenGraph.
                        Where(Edge => EdgeFilter(Edge)).ToArray().
                        ForEach(Edge => this.Graph.AsMutable().
                            RemoveEdges(Edge));

            }

        }

        #endregion

        #endregion

        #region MultiEdge methods [IGenericPropertyGraph]

        #region OnMultiEdgeAddition
        
        private readonly IVotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> MultiEdgeAddition_WhenGraph;

        /// <summary>
        /// Called whenever a multiedge will be or was added to the graph.
        /// </summary>
        IVotingNotification<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            Boolean>
            
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.OnMultiEdgeAddition

        {
            get
            {
                return MultiEdgeAddition_WhenGraph;
            }
        }

        #endregion

        #region (private) AddMultiEdgeToGraph(MultiEdge)

        /// <summary>
        /// Add the given multiedge to the graph.
        /// </summary>
        /// <param name="MultiEdge">A multiedge.</param>
        /// <returns>The given multiedge</returns>
        IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

               AddMultiEdgeToGraph(IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdge)

        {

            if (MultiEdgeAddition_WhenGraph.SendVoting(this, MultiEdge))
            {
                if (_MultiEdgesWhenGraph.TryAddValue(MultiEdge.Id, MultiEdge, MultiEdge.Label))
                {

                    foreach (var Edge in MultiEdge.Edges())
                        Edge.AsMutable().AddMultiEdge(MultiEdge);

                    MultiEdgeAddition_WhenGraph.SendNotification(this, MultiEdge);

                    return MultiEdge;

                }
            }

            return null;

        }

        #endregion

        #region AddMultiEdge(EdgeSelector, params Edges)

        /// <summary>
        /// Add a multiedge based on the given enumeration
        /// of edges to the graph.
        /// </summary>
        /// <param name="EdgeSelector">A delegate to match the edges to be connected by the multiedge.</param>
        /// <param name="Edges">An enumeration of edges.</param>
        /// <returns>The new multiedge</returns>
        IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                AddMultiEdge(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeSelector,

                            params IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Edges)

        {
            return Subgraph.AddMultiEdge(_MultiEdgeIdCreatorDelegate(this), _DefaultMultiEdgeLabel, null, EdgeSelector, Edges);
        }

        #endregion

        #region AddMultiEdge(Label, EdgeSelector, params Edges)

        /// <summary>
        /// Add a multiedge based on the given multiedge label and
        /// an enumeration of edges to the graph.
        /// </summary>
        /// <param name="Label">The multiedge label.</param>
        /// <param name="EdgeSelector">A delegate to match the edges to be connected by the multiedge.</param>
        /// <param name="Edges">An enumeration of edges.</param>
        /// <returns>The new multiedge</returns>
        IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            AddMultiEdge(TMultiEdgeLabel Label,

                         EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeSelector,

                         params IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Edges)

        {
            return Subgraph.AddMultiEdge(_MultiEdgeIdCreatorDelegate(this), Label, null, EdgeSelector, Edges);
        }

        #endregion

        #region AddMultiEdge(Label, MultiEdgeInitializer, EdgeSelector, params Edges)

        /// <summary>
        /// Add a multiedge based on the given multiedge label and
        /// an enumeration of edges to the graph and initialize
        /// it by invoking the given MultiEdgeInitializer.
        /// </summary>
        /// <param name="Label">The multiedge label.</param>
        /// <param name="MultiEdgeInitializer">A delegate to initialize the newly generated multiedge.</param>
        /// <param name="EdgeSelector">A delegate to match the edges to be connected by the multiedge.</param>
        /// <param name="Edges">An enumeration of edges.</param>
        /// <returns>The new multiedge</returns>
        IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                AddMultiEdge(TMultiEdgeLabel Label,

                             MultiEdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeInitializer,

                             EdgeFilter          <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeSelector,

                             params IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Edges)

        {
            return Subgraph.AddMultiEdge(_MultiEdgeIdCreatorDelegate(this), Label, MultiEdgeInitializer, EdgeSelector, Edges);
        }

        #endregion

        #region AddMultiEdge(Id, Label, MultiEdgeInitializer, EdgeSelector, params Edges)

        /// <summary>
        /// Add a multiedge based on the given multiedge label and
        /// an enumeration of edges to the graph and initialize
        /// it by invoking the given MultiEdgeInitializer.
        /// </summary>
        /// <param name="Id">A MultiEdgeId. If none was given a new one will be generated.</param>
        /// <param name="Label">The multiedge label.</param>
        /// <param name="MultiEdgeInitializer">A delegate to initialize the newly generated multiedge.</param>
        /// <param name="EdgeSelector">A delegate to match the edges to be connected by the multiedge.</param>
        /// <param name="Edges">An enumeration of edges.</param>
        /// <returns>The new multiedge</returns>
        IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> 

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            AddMultiEdge(TIdMultiEdge    Id,
                         TMultiEdgeLabel Label,

                         MultiEdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeInitializer,

                         EdgeFilter          <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeSelector,

                         params IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Edges)

        {

            #region Initial checks

            if (Id == null)
                Id = _MultiEdgeIdCreatorDelegate(this);

            if (_MultiEdgesWhenGraph.ContainsKey(Id))
                throw new DuplicateMultiEdgeIdException<TIdMultiEdge>(Id);

            if (Label == null)
                Label = _DefaultMultiEdgeLabel;

            #endregion

            return AddMultiEdgeToGraph(_MultiEdgeCreatorDelegate(this, Id, Label, MultiEdgeInitializer, EdgeSelector, Edges));

        }

        #endregion

        #region AddMultiEdge(MultiEdge)

        /// <summary>
        /// Adds the given multiedge to the graph, and returns it again.
        /// An exception will be thrown if the multiedge identifier is already being
        /// used by the graph to reference another multiedge.
        /// </summary>
        /// <param name="MultiEdge">A generic property multiedge.</param>
        IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IMultiEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                AddMultiEdge(IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdge)

        {

            #region Initial checks

            if (MultiEdge == null)
                throw new ArgumentNullException("The given multiedge must not be null!");

            if (MultiEdge.Id == null || MultiEdge.Id.Equals(default(TIdMultiEdge)))
                throw new ArgumentNullException("The unique identification of multiedge must not be null!");

            if (_MultiEdgesWhenGraph.ContainsKey(MultiEdge.Id))
                throw new DuplicateMultiEdgeIdException<TIdMultiEdge>(MultiEdge.Id);

            #endregion

            return AddMultiEdgeToGraph(MultiEdge);

        }

        #endregion


        #region MultiEdgeById(Id)

        /// <summary>
        /// Return the multiedge referenced by the given multiedge identifier.
        /// If no multiedge is referenced by a given identifier return null.
        /// </summary>
        /// <param name="Id">A multiedge identifier.</param>
        IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IReadOnlyMultiEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                MultiEdgeById(TIdMultiEdge Id)

        {

            IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdge = null;

            if (Id != null && _MultiEdgesWhenGraph.TryGetByKey(Id, out MultiEdge))
                return MultiEdge;

            return null;

        }

        #endregion

        #region TryGetMultiEdgeById(Id)

        /// <summary>
        /// Try to return the multiedge referenced by the given multiedge identifier.
        /// </summary>
        /// <param name="Id">A multiedge identifier.</param>
        /// <param name="MultiEdge">A multiedge.</param>
        /// <returns>True when success; false otherwise.</returns>
        Boolean IReadOnlyMultiEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            TryGetMultiEdgeById(TIdMultiEdge Id,
                                out IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdge)

        {

            if (Id != null && _MultiEdgesWhenGraph.TryGetByKey(Id, out MultiEdge))
                return true;

            MultiEdge = null;
            return false;

        }

        #endregion

        #region HasMultiEdgeId(Id)

        /// <summary>
        /// Check if the given multiedge identifier already exists within the graph.
        /// </summary>
        /// <param name="Id">A multiedge identifier.</param>
        /// <returns>True when yes; false otherwise.</returns>
        Boolean IReadOnlyMultiEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            HasMultiEdgeId(TIdMultiEdge Id)

        {

            if (Id == null)
                return false;

            return _MultiEdgesWhenGraph.ContainsKey(Id);

        }

        #endregion

        #region MultiEdgesById(params Ids)

        /// <summary>
        /// Return the multiedges referenced by the given array of multiedge identifiers.
        /// If no multiedge is referenced by a given identifier this value will be null.
        /// </summary>
        /// <param name="Ids">An array of multiedge identifiers.</param>
        IEnumerable<IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IReadOnlyMultiEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                MultiEdgesById(params TIdMultiEdge[] Ids)

        {

            IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdge;

            if (Ids != null)
                foreach (var Id in Ids)
                {

                    if (Id != null && _MultiEdgesWhenGraph.TryGetByKey(Id, out MultiEdge))
                        yield return MultiEdge;

                    else
                        yield return null;

                }

        }

        #endregion

        #region MultiEdgesByLabel(params Labels)

        /// <summary>
        /// Return all multiedges having one of the given labels.
        /// </summary>
        /// <param name="Labels">An array of multiedge labels.</param>
        IEnumerable<IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IReadOnlyMultiEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                MultiEdgesByLabel(params TMultiEdgeLabel[] Labels)

        {

            IEnumerable<IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> MultiEdges;

            if (Labels != null)
                foreach (var Label in Labels)
                    if (Label != null && _MultiEdgesWhenGraph.TryGetByGroup(Label, out MultiEdges))
                        foreach (var MultiEdge in MultiEdges)
                            yield return MultiEdge;

        }

        #endregion

        #region MultiEdges(Include = null)

        /// <summary>
        /// Return all multiedges or only the ones matching the given filter delegate.
        /// </summary>
        /// <param name="Include">An optional delegate to select the multiedges to be returned.</param>
        IEnumerable<IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IReadOnlyMultiEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                MultiEdges(MultiEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Include)
        {

            return (Include == null) ? _MultiEdgesWhenGraph
                                     : _MultiEdgesWhenGraph.Where(me => Include(me));

        }

        #endregion

        #region NumberOfMultiEdges(Include = null)

        /// <summary>
        /// Return the total number of multiedges or only the number of multiedges matching the given filter delegate.
        /// When the filter is null, this method should implement an optimized
        /// way to get the currenty number of multiedges.
        /// </summary>
        /// <param name="Include">An optional delegate to select the multiedges to be counted.</param>
        UInt64 IReadOnlyMultiEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            NumberOfMultiEdges(MultiEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Include)

        {

            return (Include == null) ? _MultiEdgesWhenGraph.Count()
                                     : Convert.ToUInt64(_MultiEdgesWhenGraph.LongCount(me => Include(me)));

        }

        #endregion


        #region OnMultiEdgeRemoval

        private readonly IVotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> MultiEdgeRemoval_WhenGraph;


        /// <summary>
        /// Called whenever a multiedge will be or was removed from the graph.
        /// </summary>
        IVotingNotification<IReadOnlyGenericPropertyGraph<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex,
                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            Boolean>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.OnMultiEdgeRemoval

        {
            get
            {
                return MultiEdgeRemoval_WhenGraph;
            }
        }

        #endregion

        #region RemoveMultiEdgesById(params MultiEdgeIds)

        /// <summary>
        /// Remove the given array of multiedges identified by their MultiEdgeIds.
        /// </summary>
        /// <param name="MultiEdgeIds">An array of MultiEdgeIds of the multiedges to remove.</param>
        void IMultiEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            RemoveMultiEdgesById(params TIdMultiEdge[] MultiEdgeIds)

        {

            #region Initial checks

            if (MultiEdgeIds == null)
                return;

            IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdge;

            #endregion

            foreach (var MultiEdgeId in MultiEdgeIds)
            {
                if (_MultiEdgesWhenGraph.TryGetByKey(MultiEdgeId, out MultiEdge))
                    this.Graph.AsMutable().RemoveMultiEdges(MultiEdge);
                else
                    throw new ArgumentException("The given multiedge identifier '" + MultiEdgeId.ToString() + "' is unknown!");
            }

        }

        #endregion

        #region RemoveMultiEdges(params MultiEdges)    // RemoveMultiEdges()!

        /// <summary>
        /// Remove the given array of multiedges from the graph.
        /// </summary>
        /// <param name="MultiEdges">An array of multiedges to be removed from the graph.</param>
        void IMultiEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            RemoveMultiEdges(params IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] MultiEdges)

        {

            #region Initial checks

            if (MultiEdges == null)
                return;

            #endregion

            lock (this)
            {

                foreach (var MultiEdge in MultiEdges)
                    if (_MultiEdgesWhenGraph.ContainsKey(MultiEdge.Id) &&
                        MultiEdgeRemoval_WhenGraph.SendVoting(this, MultiEdge))
                        {

                            MultiEdge.Edges().ForEach(Edge => Edge.AsMutable().RemoveMultiEdges(MultiEdge.AsMutable()));

                            _MultiEdgesWhenGraph.TryRemoveValue(MultiEdge.Id, MultiEdge.AsMutable(), MultiEdge.Label);

                            MultiEdgeRemoval_WhenGraph.SendNotification(this, MultiEdge);

                        }

            }

        }

        #endregion

        #region RemoveMultiEdges(MultiEdgeFilter = null)

        /// <summary>
        /// Remove any multiedge matching the given multiedge filter.
        /// </summary>
        /// <param name="MultiEdgeFilter">A delegate for multiedge filtering.</param>
        void IMultiEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            RemoveMultiEdges(MultiEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeFilter)

        {

            lock (this)
            {

                if (MultiEdgeFilter == null)
                    _MultiEdgesWhenGraph.ToArray().
                        ForEach(MultiEdge => this.Graph.AsMutable().
                            RemoveMultiEdges(MultiEdge));

                else
                    _MultiEdgesWhenGraph.
                        Where(MultiEdge => MultiEdgeFilter(MultiEdge)).ToArray().
                        ForEach(MultiEdge => this.Graph.AsMutable().
                            RemoveMultiEdges(MultiEdge));

            }

        }

        #endregion

        #endregion

        #region HyperEdge methods [IGenericPropertyGraph]

        #region OnHyperEdgeAddition
        
        private readonly IVotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> HyperEdgeAddition_WhenGraph;

        /// <summary>
        /// Called whenever a hyperedge will be or was added to the graph.
        /// </summary>
        IVotingNotification<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            Boolean>
            
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.OnHyperEdgeAddition

        {
            get
            {
                return HyperEdgeAddition_WhenGraph;
            }
        }

        #endregion

        #region (private) AddHyperEdgeToGraph(HyperEdge)

        /// <summary>
        /// Add the given hyperedge to the graph.
        /// </summary>
        /// <param name="HyperEdge">A hyperedge.</param>
        /// <returns>The given hyperedge</returns>
        IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

               AddHyperEdgeToGraph(IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdge)

        {

            if (HyperEdgeAddition_WhenGraph.SendVoting(this, HyperEdge))
            {
                if (_HyperEdgesWhenGraph.TryAddValue(HyperEdge.Id, HyperEdge, HyperEdge.Label))
                {

                    foreach (var Vertex in HyperEdge.Vertices())
                        Vertex.AsMutable().AddHyperEdge(HyperEdge);

                    HyperEdgeAddition_WhenGraph.SendNotification(this, HyperEdge);

                    return HyperEdge;

                }
            }

            return null;

        }

        #endregion

        #region AddHyperEdge(VertexSelector, params Vertices)

        /// <summary>
        /// Add a hyperedge based on the given enumeration
        /// of vertices to the graph.
        /// </summary>
        /// <param name="VertexSelector">A delegate to match the vertices to be connected by the hyperedge.</param>
        /// <param name="Vertices">An enumeration of vertices to be connected by the hyperedge.</param>
        /// <returns>The new hyperedge.</returns>
        IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                AddHyperEdge(VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexSelector,

                             params IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)

        {
            return Subgraph.AddHyperEdge(_HyperEdgeIdCreatorDelegate(this), _DefaultHyperEdgeLabel, null, VertexSelector, Vertices);
        }

        #endregion

        #region AddHyperEdge(Label, VertexSelector, params Vertices)

        /// <summary>
        /// Add a multiedge based on the given multiedge label and
        /// an enumeration of vertices to the graph.
        /// </summary>
        /// <param name="Label">The multiedge label.</param>
        /// <param name="VertexSelector">A delegate to match the vertices to be connected by the hyperedge.</param>
        /// <param name="Vertices">An enumeration of vertices to be connected by the hyperedge.</param>
        /// <returns>The new multiedge</returns>
        IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                AddHyperEdge(THyperEdgeLabel Label,

                             VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexSelector,

                             params IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)

        {
            return Subgraph.AddHyperEdge(_HyperEdgeIdCreatorDelegate(this), Label, null, VertexSelector, Vertices);
        }

        #endregion

        #region AddHyperEdge(Label, HyperEdgeInitializer, VertexSelector, params Vertices)

        /// <summary>
        /// Add a hyperedge based on the given hyperedge label and
        /// an enumeration of vertices to the graph and initialize
        /// it by invoking the given HyperEdgeInitializer.
        /// </summary>
        /// <param name="Label">The multiedge label.</param>
        /// <param name="HyperEdgeInitializer">A delegate to initialize the newly generated hyperedge.</param>
        /// <param name="VertexSelector">A delegate to match the vertices to be connected by the hyperedge.</param>
        /// <param name="Vertices">An enumeration of vertices to be connected by the hyperedge.</param>
        /// <returns>The new hyperedge</returns>
        IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                AddHyperEdge(THyperEdgeLabel Label,

                             HyperEdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeInitializer,

                             VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexSelector,

                             params IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)

        {
            return Subgraph.AddHyperEdge(_HyperEdgeIdCreatorDelegate(this), Label, HyperEdgeInitializer, VertexSelector, Vertices);
        }

        #endregion

        #region AddHyperEdge(Id, Label, HyperEdgeInitializer, VertexSelector, params Vertices)

        /// <summary>
        /// Add a hyperedge based on the given hyperedge label and
        /// an enumeration of vertices to the graph and initialize
        /// it by invoking the given HyperEdgeInitializer.
        /// </summary>
        /// <param name="Id">A HyperEdgeId. If none was given a new one will be generated.</param>
        /// <param name="Label">The multiedge label.</param>
        /// <param name="HyperEdgeInitializer">A delegate to initialize the newly generated multiedge.</param>
        /// <param name="VertexSelector">A delegate to match the vertices to be connected by the hyperedge.</param>
        /// <param name="Vertices">An enumeration of vertices to be connected by the hyperedge.</param>
        /// <returns>The new multiedge</returns>
        IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                AddHyperEdge(TIdHyperEdge     Id,
                             THyperEdgeLabel  Label,

                             HyperEdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeInitializer,

                             VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexSelector,

                             params IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)

        {

            #region Initial checks

            if (Id == null)
                Id = _HyperEdgeIdCreatorDelegate(this);

            if (_HyperEdgesWhenGraph.ContainsKey(Id))
                throw new DuplicateHyperEdgeIdException<TIdHyperEdge>(Id);

            if (Label == null)
                Label = _DefaultHyperEdgeLabel;

            #endregion

            return AddHyperEdgeToGraph(_HyperEdgeCreatorDelegate(this, Id, Label, HyperEdgeInitializer, null, Vertices));

        }

        #endregion

        #region AddHyperEdge(HyperEdge)

        /// <summary>
        /// Adds the given hyperedge to the graph, and returns it again.
        /// An exception will be thrown if the hyperedge identifier is already being
        /// used by the graph to reference another hyperedge.
        /// </summary>
        /// <param name="HyperEdge">An IGenericPropertyHyperEdge.</param>
        /// <returns>The given IGenericPropertyHyperEdge.</returns>
        IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IHyperEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                AddHyperEdge(IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdge)

        {

            #region Initial checks

            if (HyperEdge == null)
                throw new ArgumentNullException("The given hyperedge must not be null!");

            if (HyperEdge.Id == null || HyperEdge.Id.Equals(default(TIdHyperEdge)))
                throw new ArgumentNullException("The unique identification of hyperedge must not be null!");

            if (_HyperEdgesWhenGraph.ContainsKey(HyperEdge.Id))
                throw new DuplicateHyperEdgeIdException<TIdHyperEdge>(HyperEdge.Id);

            #endregion

            return AddHyperEdgeToGraph(HyperEdge);

        }

        #endregion


        #region HyperEdgeById(Id)

        /// <summary>
        /// Return the hyperedge referenced by the given hyperedge identifier.
        /// If no hyperedge is referenced by a given identifier return null.
        /// </summary>
        /// <param name="Id">A hyperedge identifier.</param>
        IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IReadOnlyHyperEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                HyperEdgeById(TIdHyperEdge Id)

        {

            IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdge = null;

            if (Id != null && _HyperEdgesWhenGraph.TryGetByKey(Id, out HyperEdge))
                return HyperEdge;

            return null;

        }

        #endregion

        #region TryGetHyperEdgeById(Id)

        /// <summary>
        /// Try to return the hyperedge referenced by the given hyperedge identifier.
        /// </summary>
        /// <param name="Id">A hyperedge identifier.</param>
        /// <param name="HyperEdge">A hyperedge.</param>
        /// <returns>True when success; false otherwise.</returns>
        Boolean IReadOnlyHyperEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            TryGetHyperEdgeById(TIdHyperEdge Id,
                                out IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdge)

        {

            if (Id != null && _HyperEdgesWhenGraph.TryGetByKey(Id, out HyperEdge))
                return true;

            HyperEdge = null;
            return false;

        }

        #endregion

        #region HasHyperEdgeId(Id)

        /// <summary>
        /// Check if the given hyperedge identifier already exists within the graph.
        /// </summary>
        /// <param name="Id">A hyperedge identifier.</param>
        /// <returns>True when yes; false otherwise.</returns>
        Boolean IReadOnlyHyperEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            HasHyperEdgeId(TIdHyperEdge Id)

        {

            if (Id == null)
                return false;

            return _HyperEdgesWhenGraph.ContainsKey(Id);

        }

        #endregion

        #region HyperEdgesById(params Ids)

        /// <summary>
        /// Return the hyperedges referenced by the given array of hyperedge identifiers.
        /// If no hyperedge is referenced by a given identifier this value will be null.
        /// </summary>
        /// <param name="Ids">An array of hyperedge identifiers.</param>
        IEnumerable<IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IReadOnlyHyperEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                HyperEdgesById(params TIdHyperEdge[] Ids)

        {

            IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdge;

            if (Ids != null)
                foreach (var Id in Ids)
                {

                    if (Id != null && _HyperEdgesWhenGraph.TryGetByKey(Id, out HyperEdge))
                        yield return HyperEdge;

                    else
                        yield return null;

                }

        }

        #endregion

        #region HyperEdgesByLabel(params Labels)

        /// <summary>
        /// Return all hyperedges having one of the given labels.
        /// </summary>
        /// <param name="Labels">An array of hyperedge labels.</param>
        IEnumerable<IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IReadOnlyHyperEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                HyperEdgesByLabel(params THyperEdgeLabel[] Labels)

        {

            IEnumerable<IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> HyperEdges;

            if (Labels != null)
                foreach (var Label in Labels)
                    if (Label != null && _HyperEdgesWhenGraph.TryGetByGroup(Label, out HyperEdges))
                        foreach (var HyperEdge in HyperEdges)
                            yield return HyperEdge;

        }

        #endregion

        #region HyperEdges(Include = null)

        /// <summary>
        /// Return all hyperedges or only the ones matching the given filter delegate.
        /// </summary>
        /// <param name="Include">An optional delegate to select the hyperedges to be returned.</param>
        IEnumerable<IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IReadOnlyHyperEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                HyperEdges(HyperEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Include)
        {

            return (Include == null) ? _HyperEdgesWhenGraph
                                     : _HyperEdgesWhenGraph.Where(he => Include(he));

        }

        #endregion

        #region NumberOfHyperEdges(Include = null)

        /// <summary>
        /// Return the total number of hyperedges or only the number of hyperedges matching the given filter delegate.
        /// When the filter is null, this method should implement an optimized
        /// way to get the currenty number of hyperedges.
        /// </summary>
        /// <param name="Include">An optional delegate to select the hyperedges to be counted.</param>
        UInt64 IReadOnlyHyperEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            NumberOfHyperEdges(HyperEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Include)

        {

            return (Include == null) ? _HyperEdgesWhenGraph.Count()
                                     : Convert.ToUInt64(_HyperEdgesWhenGraph.LongCount(he => Include(he)));

        }

        #endregion


        #region OnHyperEdgeRemoval

        private readonly IVotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> HyperEdgeRemoval_WhenGraph;


        /// <summary>
        /// Called whenever a hyperedge will be or was removed from the graph.
        /// </summary>
        IVotingNotification<IReadOnlyGenericPropertyGraph<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex,
                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            Boolean>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.OnHyperEdgeRemoval

        {
            get
            {
                return HyperEdgeRemoval_WhenGraph;
            }
        }

        #endregion

        #region RemoveHyperEdgesById(params HyperEdgeIds)

        /// <summary>
        /// Remove the given array of hyperedges identified by their HyperEdgeIds.
        /// </summary>
        /// <param name="HyperEdgeIds">An array of HyperEdgeIds of the hyperedges to remove.</param>
        void IHyperEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            RemoveHyperEdgesById(params TIdHyperEdge[] HyperEdgeIds)

        {

            #region Initial checks

            if (HyperEdgeIds == null)
                return;

            IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdge;

            #endregion

            foreach (var HyperEdgeId in HyperEdgeIds)
            {
                if (_HyperEdgesWhenGraph.TryGetByKey(HyperEdgeId, out HyperEdge))
                    this.Graph.AsMutable().RemoveHyperEdges(HyperEdge);
                else
                    throw new ArgumentException("The given hyperedge identifier '" + HyperEdgeId.ToString() + "' is unknown!");
            }

        }

        #endregion

        #region RemoveHyperEdges(params HyperEdges)    // RemoveHyperEdges()!

        /// <summary>
        /// Remove hyperedges.
        /// </summary>
        /// <param name="HyperEdges">An array of outgoing edges to be removed.</param>
        void IHyperEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            RemoveHyperEdges(params IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] HyperEdges)
        {

            #region Initial checks

            if (HyperEdges == null)
                return;

            #endregion

            lock (this)
            {

                foreach (var HyperEdge in HyperEdges)
                    if (_HyperEdgesWhenGraph.ContainsKey(HyperEdge.Id) &&
                        HyperEdgeRemoval_WhenGraph.SendVoting(this, HyperEdge))
                    {

                        HyperEdge.Vertices().ForEach(Vertex => Vertex.AsMutable().RemoveHyperEdges(HyperEdge.AsMutable()));

                        _HyperEdgesWhenGraph.TryRemoveValue(HyperEdge.Id, HyperEdge.AsMutable(), HyperEdge.Label);

                        HyperEdgeRemoval_WhenGraph.SendNotification(this, HyperEdge);

                    }


            }

        }

        #endregion

        #region RemoveHyperEdges(HyperEdgeFilter = null)

        /// <summary>
        /// Remove any outgoing hyperedge matching
        /// the given hyperedge filter delegate.
        /// </summary>
        /// <param name="HyperEdgeFilter">A delegate for hyperedge filtering.</param>
        void IHyperEdgeMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            RemoveHyperEdges(HyperEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter)

        {

            lock (this)
            {

                if (HyperEdgeFilter == null)
                    _HyperEdgesWhenGraph.ToArray().
                        ForEach(HyperEdge => this.Graph.AsMutable().RemoveHyperEdges(HyperEdge));

                else
                    _HyperEdgesWhenGraph.
                        Where(HyperEdge => HyperEdgeFilter(HyperEdge)).ToArray().
                        ForEach(HyperEdge => this.Graph.AsMutable().RemoveHyperEdges(HyperEdge));

            }

        }

        #endregion

        #endregion


        #region BeginTransaction(...)

        private static ThreadLocal<Transaction<TIdVertex, TIdVertex, IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> _ThreadLocalTransaction;

        /// <summary>
        /// Start a new transaction.
        /// </summary>
        /// <param name="Name">A name or identification for this transaction.</param>
        /// <param name="Distributed">Indicates that this transaction should synched within the entire cluster.</param>
        /// <param name="LongRunning">Indicates that this transaction is a long-running transaction.</param>
        /// <param name="IsolationLevel">The isolation level of this transaction.</param>
        /// <param name="CreationTime">The timestamp when this transaction started.</param>
        /// <param name="InvalidationTime">The timestamp when this transaction will be invalid.</param>
        /// <returns>A new transaction object.</returns>
        Transaction<TIdVertex, TIdVertex, IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            
                                 // Default values are set via the interface definition!
                BeginTransaction(String         Name,
                                 Boolean        Distributed,
                                 Boolean        LongRunning,
                                 IsolationLevel IsolationLevel,
                                 DateTime?      CreationTime,
                                 DateTime?      InvalidationTime)

        {

            if (_ThreadLocalTransaction != null &&
                _ThreadLocalTransaction.IsValueCreated &&
               (_ThreadLocalTransaction.Value.State == TransactionState.Running ||
                _ThreadLocalTransaction.Value.State == TransactionState.NestedTransaction ||
                _ThreadLocalTransaction.Value.State == TransactionState.Committing ||
                _ThreadLocalTransaction.Value.State == TransactionState.RollingBack))
            {

                throw new CouldNotBeginTransactionException<TIdVertex, TIdVertex, IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>(
                    _ThreadLocalTransaction.Value,
                    Message: "Transaction still in state '" + _ThreadLocalTransaction.Value.State.ToString() + "' on Thread " + Thread.CurrentThread.ManagedThreadId + "!");

            }


            _ThreadLocalTransaction = new ThreadLocal<Transaction<TIdVertex, TIdVertex, IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>(

                () => new Transaction<TIdVertex, TIdVertex, IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>(
                    default(TIdVertex),
                    this.Id,
                    Name,
                    Distributed,
                    LongRunning,
                    IsolationLevel,
                    CreationTime,
                    InvalidationTime,
                    () => new GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>(
                                                            this.Id,
                                                            "Transaction Graph",
                                                            //GraphFactory.cre<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            //                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            //                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            //                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>(this.Id,),
                                                            ReadGraphs: this)));


            //ToDo: Add delegates denying all modifications of the underlying graph!

            return _ThreadLocalTransaction.Value;

        }

        #endregion


        #region Clear

        #region Clear()

        /// <summary>
        /// Removes all the vertices, edges and hyperedges from the graph.
        /// </summary>
        void IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            Clear()

        {
            _VerticesWhenGraph.Clear();
            _EdgesWhenGraph.Clear();
            _HyperEdgesWhenVertex.Clear();
        }

        #endregion

        #endregion

        #region Shutdown

        #region OnGraphShuttingdown

        protected GraphShuttingdownEventHandler<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _OnGraphShuttingdown;

        /// <summary>
        /// Called whenever a property graph will be shutting down.
        /// </summary>
        event GraphShuttingdownEventHandler<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                       
              IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            OnGraphShuttingdown
            {

                add
                {
                    _OnGraphShuttingdown += value;
                }

                remove
                {
                    _OnGraphShuttingdown -= value;
                }

            }

        #endregion
        
        #region (internal) SendGraphShuttingdownNotification(Message = "")

        /// <summary>
        /// Notify about a property graph to be shutted down.
        /// </summary>
        /// <param name="Message">An optional message, e.g. a reason for the shutdown.</param>
        internal void SendGraphShuttingdownNotification(String Message = "")
        {
            if (_OnGraphShuttingdown != null)
                _OnGraphShuttingdown(this, Message);
        }

        #endregion

        #region Shutdown()

        /// <summary>
        /// Shutdown and close the graph.
        /// </summary>
        /// <param name="Message">An optional message, e.g. a reason for the shutdown.</param>
        void IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            Shutdown(String Message)

        {
            SendGraphShuttingdownNotification(Message);
            //Clear();
            SendGraphShutteddownNotification();
        }

        #endregion

        #region OnGraphShutteddown

        protected GraphShutteddownEventHandler<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _OnGraphShutteddown;

        /// <summary>
        /// Called whenever a property graph was shutted down.
        /// </summary>
        event GraphShutteddownEventHandler<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                       
              IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            OnGraphShutteddown
            {

                add
                {
                    _OnGraphShutteddown += value;
                }

                remove
                {
                    _OnGraphShutteddown -= value;
                }

            }

        #endregion

        #region (internal) SendGraphShutteddownNotification()

        /// <summary>
        /// Notify about a shutted down property graph.
        /// </summary>
        internal void SendGraphShutteddownNotification()
        {
            if (_OnGraphShutteddown != null)
                _OnGraphShutteddown(this);
        }

        #endregion

        #endregion


        #region Operator overloading

        #region Operator == (PropertyVertex1, PropertyVertex2)

        /// <summary>
        /// Compares two vertices for equality.
        /// </summary>
        /// <param name="PropertyVertex1">A vertex.</param>
        /// <param name="PropertyVertex2">Another vertex.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public static Boolean operator == (GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> PropertyVertex1,
                                           GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> PropertyVertex2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(PropertyVertex1, PropertyVertex2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) PropertyVertex1 == null) || ((Object) PropertyVertex2 == null))
                return false;

            return PropertyVertex1.Equals(PropertyVertex2);

        }

        #endregion

        #region Operator != (PropertyVertex1, PropertyVertex2)

        /// <summary>
        /// Compares two vertices for inequality.
        /// </summary>
        /// <param name="PropertyVertex1">A vertex.</param>
        /// <param name="PropertyVertex2">Another vertex.</param>
        /// <returns>False if both match; True otherwise.</returns>
        public static Boolean operator != (GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> PropertyVertex1,
                                           GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> PropertyVertex2)
        {
            return !(PropertyVertex1 == PropertyVertex2);
        }

        #endregion

        #region Operator <  (PropertyVertex1, PropertyVertex2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyVertex1">A Vertex.</param>
        /// <param name="PropertyVertex2">Another Vertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator  < (GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> PropertyVertex1,
                                           GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> PropertyVertex2)
        {

            if ((Object) PropertyVertex1 == null)
                throw new ArgumentNullException("The given PropertyVertex1 must not be null!");

            if ((Object) PropertyVertex2 == null)
                throw new ArgumentNullException("The given PropertyVertex2 must not be null!");

            return PropertyVertex1.CompareTo(PropertyVertex2) < 0;

        }

        #endregion

        #region Operator <= (PropertyVertex1, PropertyVertex2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyVertex1">A Vertex.</param>
        /// <param name="PropertyVertex2">Another Vertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> PropertyVertex1,
                                           GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> PropertyVertex2)
        {
            return !(PropertyVertex1 > PropertyVertex2);
        }

        #endregion

        #region Operator >  (PropertyVertex1, PropertyVertex2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyVertex1">A Vertex.</param>
        /// <param name="PropertyVertex2">Another Vertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >  (GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> PropertyVertex1,
                                           GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> PropertyVertex2)
        {

            if ((Object) PropertyVertex1 == null)
                throw new ArgumentNullException("The given PropertyVertex1 must not be null!");

            if ((Object) PropertyVertex2 == null)
                throw new ArgumentNullException("The given PropertyVertex2 must not be null!");

            return PropertyVertex1.CompareTo(PropertyVertex2) > 0;

        }

        #endregion

        #region Operator >= (PropertyVertex1, PropertyVertex2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyVertex1">A Vertex.</param>
        /// <param name="PropertyVertex2">Another Vertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> PropertyVertex1,
                                           GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> PropertyVertex2)
        {
            return !(PropertyVertex1 < PropertyVertex2);
        }

        #endregion

        #endregion

        #region IDynamicGraphObject<GenericPropertyVertex<...>> Members

        #region GetMetaObject(Expression)

        /// <summary>
        /// Return the appropriate DynamicMetaObject.
        /// </summary>
        /// <param name="Expression">An Expression.</param>
        public virtual DynamicMetaObject GetMetaObject(Expression Expression)
        {
            return new DynamicGraphElement<GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
                                                                 (Expression, this);
        }

        #endregion

        #endregion

        #region IComparable Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public override Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given Object must not be null!");

            // Check if the given object can be casted to a IGenericPropertyVertex<...>
            var IGenericPropertyVertex = Object as IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;

            if ((Object) IGenericPropertyVertex == null)
                throw new ArgumentException("The given object is not a IGenericPropertyVertex<...>!");

            return CompareTo(IGenericPropertyVertex);

        }

        #endregion

        #region CompareTo(IReadOnlyGenericPropertyVertex)

        /// <summary>
        /// Compares two generic property vertices.
        /// </summary>
        /// <param name="IReadOnlyGenericPropertyVertex">A generic property vertex to compare with.</param>
        public Int32 CompareTo(IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IReadOnlyGenericPropertyVertex)
        {
            
            if ((Object) IReadOnlyGenericPropertyVertex == null)
                throw new ArgumentNullException("IReadOnlyGenericPropertyVertex", "The given IReadOnlyGenericPropertyVertex must not be null!");

            return Id.CompareTo(IReadOnlyGenericPropertyVertex.Id);

        }
        
        #endregion

        #region CompareTo(IGenericPropertyVertex)

        /// <summary>
        /// Compares two generic property vertices.
        /// </summary>
        /// <param name="IGenericPropertyVertex">A generic property vertex to compare with.</param>
        public Int32 CompareTo(IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyVertex)
        {
            
            if ((Object) IGenericPropertyVertex == null)
                throw new ArgumentNullException("IGenericPropertyVertex", "The given IGenericPropertyVertex must not be null!");

            return Id.CompareTo(IGenericPropertyVertex.Id);

        }
        
        #endregion

        #endregion

        #region IEquatable Members

        #region Equals(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public override Boolean Equals(Object Object)
        {

            if (Object == null)
                return false;

            // Check if the given object can be casted to a GenericPropertyVertex
            var PropertyVertex = Object as GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;

            if ((Object) PropertyVertex == null)
                return false;

            return this.Equals(PropertyVertex);

        }

        #endregion

        #region Equals(IReadOnlyGenericPropertyVertex)

        /// <summary>
        /// Compares two generic property vertices for equality.
        /// </summary>
        /// <param name="IReadOnlyGenericPropertyVertex">A generic property vertex to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IReadOnlyGenericPropertyVertex)
        {
            
            if ((Object) IReadOnlyGenericPropertyVertex == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return Id.Equals(IReadOnlyGenericPropertyVertex.Id);

        }

        #endregion

        #region Equals(IGenericPropertyVertex)

        /// <summary>
        /// Compares two generic property vertices for equality.
        /// </summary>
        /// <param name="IGenericPropertyVertex">A generic property vertex to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyVertex)
        {
            
            if ((Object) IGenericPropertyVertex == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return Id.Equals(IGenericPropertyVertex.Id);

        }

        #endregion

        #endregion

        #region GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override Int32 GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #region ToString()

        // It's always a vertex and sometimes a graph...

        /// <summary>
        /// Return a string representation of this object as a generic property vertex.
        /// </summary>
        public override String ToString()
        {

            return String.Concat("GenericPropertyVertex [Id: " + Id.ToString(), ", ",
                                 _OutEdgesWhenVertex.Count(),   " out edges, ",
                                 _InEdgesWhenVertex.Count(),    " in edges, ",
                                 _HyperEdgesWhenVertex.Count(), " hyperedges]");

        }

        /// <summary>
        /// Return a string representation of this object as a generic property graph.
        /// </summary>
        String IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.ToString()
        {

            return String.Concat("GenericPropertyGraph [Id: " + Id.ToString(), ", ",
                                 _VerticesWhenGraph.Count(),   " vertices: ",
                                 _EdgesWhenGraph.Count(),      " edges: ",
                                 _MultiEdgesWhenGraph.Count(), " multiedges: ",
                                 _HyperEdgesWhenGraph.Count(), " hyperedges]");

        }

        #endregion

    }

}
