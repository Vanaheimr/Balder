/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <code@ahzf.de>
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

#endregion

namespace de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable
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
    /// <typeparam name="TPropertiesCollectionVertex">A data structure to store the properties of the vertices.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
    /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionEdge">A data structure to store the properties of the edges.</typeparam>
    /// <typeparam name="TEdgeCollection">A data structure to store edges.</typeparam>
    /// 
    /// <typeparam name="TIdMultiEdge">The type of the multiedge identifiers.</typeparam>
    /// <typeparam name="TRevIdMultiEdge">The type of the multiedge revision identifiers.</typeparam>
    /// <typeparam name="TMultiEdgeLabel">The type of the multiedge label.</typeparam>
    /// <typeparam name="TKeyMultiEdge">The type of the multiedge property keys.</typeparam>
    /// <typeparam name="TValueMultiEdge">The type of the multiedge property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionMultiEdge">A data structure to store the properties of the multiedges.</typeparam>
    /// <typeparam name="TMultiEdgeCollection">A data structure to store multiedges.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
    /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionHyperEdge">A data structure to store the properties of the hyperedges.</typeparam>
    /// <typeparam name="THyperEdgeCollection">A data structure to store hyperedges.</typeparam>
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

        where TVertexLabel     : IEquatable<TVertexLabel>,    IComparable<TVertexLabel>,    IComparable
        where TEdgeLabel       : IEquatable<TEdgeLabel>,      IComparable<TEdgeLabel>,      IComparable
        where TMultiEdgeLabel  : IEquatable<TMultiEdgeLabel>, IComparable<TMultiEdgeLabel>, IComparable
        where THyperEdgeLabel  : IEquatable<THyperEdgeLabel>, IComparable<THyperEdgeLabel>, IComparable

        where TKeyVertex       : IEquatable<TKeyVertex>,      IComparable<TKeyVertex>,      IComparable
        where TKeyEdge         : IEquatable<TKeyEdge>,        IComparable<TKeyEdge>,        IComparable
        where TKeyMultiEdge    : IEquatable<TKeyMultiEdge>,   IComparable<TKeyMultiEdge>,   IComparable
        where TKeyHyperEdge    : IEquatable<TKeyHyperEdge>,   IComparable<TKeyHyperEdge>,   IComparable

    {

        #region Data

        #region IGenericPropertyVertex

        /// <summary>
        /// This object as a SuperVertex implementing IGenericPropertyVertex;
        /// </summary>
        protected readonly IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> SuperVertex;


        /// <summary>
        /// The edges emanating from, or leaving, this vertex.
        /// </summary>
        protected readonly IGroupedCollection<TIdEdge,      IGenericPropertyEdge     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TEdgeLabel> _OutEdges;

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex.
        /// </summary>
        protected readonly IGroupedCollection<TIdEdge,      IGenericPropertyEdge     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TEdgeLabel> _InEdges;

        
        /// <summary>
        /// The multiedges of this vertex.
        /// </summary>
        protected readonly IGroupedCollection<TIdMultiEdge, IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TMultiEdgeLabel> _MultiEdgesWhenVertex;

        /// <summary>
        /// The hyperedges of this vertex.
        /// </summary>
        protected readonly IGroupedCollection<TIdHyperEdge, IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, THyperEdgeLabel> _HyperEdgesWhenVertex;

        #endregion

        #region IGenericPropertyGraph

                /// <summary>
        /// This object as an IGenericPropertyGraph;
        /// </summary>
        protected readonly IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Subgraph;



        /// <summary>
        /// The collection of vertices.
        /// </summary>
        protected readonly IGroupedCollection<TIdVertex,    IGenericPropertyVertex   <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TVertexLabel> _VerticesWhenGraph;

        /// <summary>
        /// The edges not conntected to this vertex but to other
        /// vertices when this vertex acts as a graph.
        /// </summary>
        protected readonly IGroupedCollection<TIdEdge,      IGenericPropertyEdge     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TEdgeLabel> _EdgesWhenGraph;

        /// <summary>
        /// The multiedges of this vertex.
        /// </summary>
        protected readonly IGroupedCollection<TIdMultiEdge, IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TMultiEdgeLabel> _MultiEdgesWhenGraph;

        /// <summary>
        /// The hyperedges of this vertex.
        /// </summary>
        protected readonly IGroupedCollection<TIdHyperEdge, IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, THyperEdgeLabel> _HyperEdgesWhenGraph;

        #endregion

        protected readonly VertexIdCreatorDelegate   <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _VertexIdCreatorDelegate;

        protected readonly TVertexLabel _DefaultVertexLabel;

        protected readonly VertexCreatorDelegate     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _VertexCreatorDelegate;
                                                   

        protected readonly EdgeIdCreatorDelegate     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _EdgeIdCreatorDelegate;

        protected readonly TEdgeLabel _DefaultEdgeLabel;

        protected readonly EdgeCreatorDelegate       <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _EdgeCreatorDelegate;


        protected readonly MultiEdgeIdCreatorDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _MultiEdgeIdCreatorDelegate;

        protected readonly TMultiEdgeLabel _DefaultMultiEdgeLabel;

        protected readonly MultiEdgeCreatorDelegate  <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _MultiEdgeCreatorDelegate;


        protected readonly HyperEdgeIdCreatorDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _HyperEdgeIdCreatorDelegate;

        protected readonly THyperEdgeLabel _DefaultHyperEdgeLabel;

        protected readonly HyperEdgeCreatorDelegate  <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _HyperEdgeCreatorDelegate;

        #endregion

        #region Properties

        #region Graph [IGenericPropertyVertex]

        /// <summary>
        /// The associated property graph.
        /// </summary>
        protected readonly IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph;

        /// <summary>
        /// The associated property graph.
        /// </summary>
        IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
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

        #region AsSubgraph [IGenericPropertyVertex]

        /// <summary>
        /// Access this property vertex as a subgraph of the hosting property graph.
        /// </summary>
        IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
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

        #region Events

        #region PropertyVertex events

        #region OnOutEdgeAdding

        /// <summary>
        /// Called whenever an OutEdge will be added to the property graph.
        /// </summary>
        public event EdgeAddingEventHandler<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnOutEdgeAdding;

        #endregion

        #region OnOutEdgeAdded

        /// <summary>
        /// Called whenever an OutEdge was added to the property graph.
        /// </summary>
        public event EdgeAddedEventHandler<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnOutEdgeAdded;

        #endregion


        #region OnInEdgeAdding

        /// <summary>
        /// Called whenever an InEdge will be added to the property graph.
        /// </summary>
        public event EdgeAddingEventHandler<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnInEdgeAdding;

        #endregion

        #region OnInEdgeAdded

        /// <summary>
        /// Called whenever an InEdge was added to the property graph.
        /// </summary>
        public event EdgeAddedEventHandler<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnInEdgeAdded;

        #endregion


        #region OnMultiEdgeAdding

        /// <summary>
        /// Called whenever a multiedge will be added to the property graph.
        /// </summary>
        public event MultiEdgeAddingEventHandler<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnMultiEdgeAdding;

        #endregion

        #region OnMultiEdgeAdded

        /// <summary>
        /// Called whenever a multiedge was added to the property graph.
        /// </summary>
        public event MultiEdgeAddedEventHandler<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnMultiEdgeAdded;

        #endregion


        #region OnHyperEdgeAdding

        /// <summary>
        /// Called whenever a hyperedge will be added to the property graph.
        /// </summary>
        public event HyperEdgeAddingEventHandler<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnHyperEdgeAdding;

        #endregion

        #region OnHyperEdgeAdded

        /// <summary>
        /// Called whenever a hyperedge was added to the property graph.
        /// </summary>
        public event HyperEdgeAddedEventHandler<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnHyperEdgeAdded;

        #endregion

        #endregion

        #region PropertyGraph events

        #region OnVertexAdding

        /// <summary>
        /// Called whenever a vertex will be added to the property graph.
        /// </summary>
        public event VertexAddingEventHandler<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnVertexAdding;

        #endregion

        #region OnVertexAdded

        /// <summary>
        /// Called whenever a vertex was added to the property graph.
        /// </summary>
        public event VertexAddedEventHandler<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnVertexAdded;

        #endregion


        #region OnEdgeAdding

        /// <summary>
        /// Called whenever an edge will be added to the property graph.
        /// </summary>
        public event EdgeAddingEventHandler<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnEdgeAdding;

        #endregion

        #region OnEdgeAdded

        /// <summary>
        /// Called whenever an edge was added to the property graph.
        /// </summary>
        public event EdgeAddedEventHandler<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnEdgeAdded;

        #endregion


        #region OnGraphShuttingdown

        /// <summary>
        /// Called whenever a property graph will be shutting down.
        /// </summary>
        public event GraphShuttingdownEventHandler<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnGraphShuttingdown;

        #endregion

        #region OnGraphShutteddown

        /// <summary>
        /// Called whenever a property graph was shutted down.
        /// </summary>
        public event GraphShutteddownEventHandler<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnGraphShutteddown;

        #endregion

        #endregion

        #endregion

        #region (internal) Send[...]Notifications(...)

        #region (internal) GenericPropertyVertex notifications

        #region (internal) SendOutEdgeAddingVote(IGenericPropertyEdge)

        /// <summary>
        /// Notify about an edge to be added to the graph.
        /// </summary>
        internal Boolean SendOutEdgeAddingVote(IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyEdge)
        {

            var _VetoVote = new VetoVote();

            if (OnOutEdgeAdding != null)
                OnOutEdgeAdding(this, IGenericPropertyEdge, _VetoVote);

            return _VetoVote.Result;

        }

        #endregion

        #region (internal) SendOutEdgeAddedNotification(IGenericPropertyEdge)

        /// <summary>
        /// Notify about an edge to be added to the graph.
        /// </summary>
        internal void SendOutEdgeAddedNotification(IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyEdge)
        {
            if (OnOutEdgeAdded != null)
                OnOutEdgeAdded(this, IGenericPropertyEdge);
        }

        #endregion


        #region (internal) SendInEdgeAddingVote(IGenericPropertyEdge)

        /// <summary>
        /// Notify about an InEdge to be added to the graph.
        /// </summary>
        internal Boolean SendInEdgeAddingVote(IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyEdge)
        {

            var _VetoVote = new VetoVote();

            if (OnInEdgeAdding != null)
                OnInEdgeAdding(this, IGenericPropertyEdge, _VetoVote);

            return _VetoVote.Result;

        }

        #endregion

        #region (internal) SendInEdgeAddedNotification(IGenericPropertyEdge)

        /// <summary>
        /// Notify about an InEdge to be added to the graph.
        /// </summary>
        internal void SendInEdgeAddedNotification(IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyEdge)
        {
            if (OnInEdgeAdded != null)
                OnInEdgeAdded(this, IGenericPropertyEdge);
        }

        #endregion


        #region (internal) SendMultiEdgeAddingVote(IGenericPropertyMultiEdge)

        /// <summary>
        /// Notify about a multiedge to be added to the graph.
        /// </summary>
        internal Boolean SendMultiEdgeAddingVote(IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyMultiEdge)
        {

            var _VetoVote = new VetoVote();

            if (OnMultiEdgeAdding != null)
                OnMultiEdgeAdding(this, IGenericPropertyMultiEdge, _VetoVote);

            return _VetoVote.Result;

        }

        #endregion

        #region (internal) SendMultiEdgeAddedNotification(IGenericPropertyMultiEdge)

        /// <summary>
        /// Notify about a multiedge to be added to the graph.
        /// </summary>
        internal void SendMultiEdgeAddedNotification(IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyMultiEdge)
        {
            if (OnMultiEdgeAdded != null)
                OnMultiEdgeAdded(this, IGenericPropertyMultiEdge);
        }

        #endregion


        #region (internal) SendHyperEdgeAddingNotification(IGenericPropertyHyperEdge)

        /// <summary>
        /// Notify about a hyperedge to be added to the graph.
        /// </summary>
        internal Boolean SendHyperEdgeAddingVote(IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyHyperEdge)
        {

            var _VetoVote = new VetoVote();

            if (OnHyperEdgeAdding != null)
                OnHyperEdgeAdding(this, IGenericPropertyHyperEdge, _VetoVote);

            return _VetoVote.Result;

        }

        #endregion

        #region (internal) SendHyperEdgeAddedNotification(IGenericPropertyHyperEdge)

        /// <summary>
        /// Notify about a hyperedge to be added to the graph.
        /// </summary>
        internal void SendHyperEdgeAddedNotification(IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyHyperEdge)
        {
            if (OnHyperEdgeAdded != null)
                OnHyperEdgeAdded(this, IGenericPropertyHyperEdge);
        }

        #endregion

        #endregion

        #region (internal) GenericPropertyGraph notifications

        #region (internal) SendVertexAddingNotification(IGenericPropertyVertex)

        /// <summary>
        /// Notify about a vertex to be added to the graph.
        /// </summary>
        internal Boolean SendVertexAddingVote(IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyVertex)
        {

            var _VetoVote = new VetoVote();

            if (OnVertexAdding != null)
                OnVertexAdding(this, IGenericPropertyVertex, _VetoVote);

            return _VetoVote.Result;

        }

        #endregion

        #region (internal) SendVertexAddedNotification(IGenericPropertyVertex)

        /// <summary>
        /// Notify about a vertex to be added to the graph.
        /// </summary>
        internal void SendVertexAddedNotification(IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyVertex)
        {
            if (OnVertexAdded != null)
                OnVertexAdded(this, IGenericPropertyVertex);
        }

        #endregion


        #region (internal) SendEdgeAddingVote(IGenericPropertyEdge)

        /// <summary>
        /// Notify about an Edge to be added to the graph.
        /// </summary>
        internal Boolean SendEdgeAddingVote(IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyEdge)
        {

            var _VetoVote = new VetoVote();

            if (OnEdgeAdding != null)
                OnEdgeAdding(this, IGenericPropertyEdge, _VetoVote);

            return _VetoVote.Result;

        }

        #endregion

        #region (internal) SendEdgeAddedNotification(IGenericPropertyEdge)

        /// <summary>
        /// Notify about an Edge to be added to the graph.
        /// </summary>
        internal void SendEdgeAddedNotification(IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyEdge)
        {
            if (OnEdgeAdded != null)
                OnEdgeAdded(this, IGenericPropertyEdge);
        }

        #endregion


        #region (internal) SendMultiEdgeAddingToGraphVote(IGenericPropertyMultiEdge)

        /// <summary>
        /// Notify about an MultiEdge to be added to the graph.
        /// </summary>
        internal Boolean SendMultiEdgeAddingToGraphVote(IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyMultiEdge)
        {

            var _VetoVote = new VetoVote();

            if (OnMultiEdgeAdding != null)
                OnMultiEdgeAdding(this, IGenericPropertyMultiEdge, _VetoVote);

            return _VetoVote.Result;

        }

        #endregion

        #region (internal) SendMultiEdgeAddedToGraphNotification(IGenericPropertyMultiEdge)

        /// <summary>
        /// Notify about an MultiEdge to be added to the graph.
        /// </summary>
        internal void SendMultiEdgeAddedToGraphNotification(IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyMultiEdge)
        {
            if (OnMultiEdgeAdded != null)
                OnMultiEdgeAdded(this, IGenericPropertyMultiEdge);
        }

        #endregion


        #region (internal) SendHyperEdgeAddingToGraphVote(IGenericPropertyHyperEdge)

        /// <summary>
        /// Notify about an HyperEdge to be added to the graph.
        /// </summary>
        internal Boolean SendHyperEdgeAddingToGraphVote(IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyHyperEdge)
        {

            var _VetoVote = new VetoVote();

            if (OnHyperEdgeAdding != null)
                OnHyperEdgeAdding(this, IGenericPropertyHyperEdge, _VetoVote);

            return _VetoVote.Result;

        }

        #endregion

        #region (internal) SendHyperEdgeAddedToGraphNotification(IGenericPropertyHyperEdge)

        /// <summary>
        /// Notify about an HyperEdge to be added to the graph.
        /// </summary>
        internal void SendHyperEdgeAddedToGraphNotification(IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyHyperEdge)
        {
            if (OnHyperEdgeAdded != null)
                OnHyperEdgeAdded(this, IGenericPropertyHyperEdge);
        }

        #endregion


        #region (internal) SendGraphShuttingdownNotification(Message = "")

        /// <summary>
        /// Notify about a property graph to be shutted down.
        /// </summary>
        /// <param name="Message">An optional message, e.g. a reason for the shutdown.</param>
        internal void SendGraphShuttingdownNotification(String Message = "")
        {
            if (OnGraphShuttingdown != null)
                OnGraphShuttingdown(this, Message);
        }

        #endregion

        #region (internal) SendGraphShutteddownNotification()

        /// <summary>
        /// Notify about a shutted down property graph.
        /// </summary>
        internal void SendGraphShutteddownNotification()
        {
            if (OnGraphShutteddown != null)
                OnGraphShutteddown(this);
        }

        #endregion

        #endregion

        #endregion

        #region Constructor(s)

        #region (internal) GenericPropertyVertex(VertexId, IdKey, RevIdKey, DescriptionKey, DatastructureInitializer)

        /// <summary>
        /// Creates a new generic property vertex.
        /// </summary>
        /// <param name="VertexId">The identification of this vertex.</param>
        /// <param name="IdKey">The property key to access the Ids of the vertices.</param>
        /// <param name="RevIdKey">The property key to access the RevisionIds of the vertices.</param>
        /// <param name="DescriptionKey">The property key to access the descriptions of the vertices.</param>
        /// <param name="PropertiesInitializer">A delegate to initialize the properties datastructure.</param>
        /// 
        /// <param name="VertexIdCreatorDelegate">A delegate to create a new vertex identification.</param>
        /// <param name="VertexCreatorDelegate">A delegate to create a new vertex.</param>
        /// <param name="VerticesCollectionInitializer">A delegate to initialize the datastructure for storing all vertices.</param>
        /// 
        /// <param name="EdgeIdCreatorDelegate">A delegate to create a new edge identification.</param>
        /// <param name="EdgeCreatorDelegate">A delegate to create a new edge.</param>
        /// <param name="EdgesCollectionInitializer">A delegate to initialize the datastructure for storing all edges.</param>
        /// 
        /// <param name="MultiEdgeIdCreatorDelegate">A delegate to create a new multiedge identification.</param>
        /// <param name="MultiEdgeCreatorDelegate">A delegate to create a new multiedge.</param>
        /// <param name="MultiEdgesCollectionInitializer">A delegate to initialize the datastructure for storing all multiedges.</param>
        /// 
        /// <param name="HyperEdgeIdCreatorDelegate">A delegate to create a new hyperedge identification.</param>
        /// <param name="HyperEdgeCreatorDelegate">A delegate to create a new hyperedge.</param>
        /// <param name="HyperEdgesCollectionInitializer">A delegate to initialize the datastructure for storing all hyperedges.</param>
        internal GenericPropertyVertex(TIdVertex    VertexId,
                                       TVertexLabel VertexLabel, 
                                       TKeyVertex   IdKey,
                                       TKeyVertex   RevIdKey,
                                       TKeyVertex   DescriptionKey,

                                       IDictionaryInitializer<TKeyVertex, TValueVertex> PropertiesInitializer,

                                       #region Create a new vertex

                                       VertexIdCreatorDelegate   <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexIdCreatorDelegate,

                                       TVertexLabel DefaultVertexLabel,

                                       VertexCreatorDelegate     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexCreatorDelegate,

                                       Func<IGroupedCollection<TIdVertex, IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TVertexLabel>> VerticesCollectionInitializer,

                                       #endregion

                                       #region Create a new edge

                                       EdgeIdCreatorDelegate     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeIdCreatorDelegate,

                                       TEdgeLabel DefaultEdgeLabel,

                                       EdgeCreatorDelegate       <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeCreatorDelegate,

                                       Func<IGroupedCollection<TIdEdge, IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TEdgeLabel>> EdgesCollectionInitializer,

                                       #endregion

                                       #region Create a new multiedge

                                       MultiEdgeIdCreatorDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeIdCreatorDelegate,

                                       TMultiEdgeLabel DefaultMultiEdgeLabel,

                                       MultiEdgeCreatorDelegate  <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeCreatorDelegate,

                                       Func<IGroupedCollection<TIdMultiEdge, IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TMultiEdgeLabel>> MultiEdgesCollectionInitializer,

                                       #endregion

                                       #region Create a new hyperedge

                                       HyperEdgeIdCreatorDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeIdCreatorDelegate,

                                       THyperEdgeLabel DefaultHyperEdgeLabel,

                                       HyperEdgeCreatorDelegate  <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeCreatorDelegate,

                                       Func<IGroupedCollection<TIdHyperEdge, IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, THyperEdgeLabel>> HyperEdgesCollectionInitializer)

                                       #endregion

            : base(VertexId, VertexLabel, IdKey, RevIdKey, DescriptionKey, PropertiesInitializer)

        {

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
            this._VerticesWhenGraph          = VerticesCollectionInitializer();

            this._EdgeIdCreatorDelegate      = EdgeIdCreatorDelegate;
            this._DefaultEdgeLabel           = DefaultEdgeLabel;
            this._EdgeCreatorDelegate        = EdgeCreatorDelegate;
            this._OutEdges                   = EdgesCollectionInitializer();
            this._InEdges                    = EdgesCollectionInitializer();
            this._EdgesWhenGraph             = EdgesCollectionInitializer();

            this._MultiEdgeIdCreatorDelegate = MultiEdgeIdCreatorDelegate;
            this._DefaultMultiEdgeLabel      = DefaultMultiEdgeLabel;
            this._MultiEdgeCreatorDelegate   = MultiEdgeCreatorDelegate;
            this._MultiEdgesWhenVertex       = MultiEdgesCollectionInitializer();
            this._MultiEdgesWhenGraph        = MultiEdgesCollectionInitializer();

            this._HyperEdgeIdCreatorDelegate = HyperEdgeIdCreatorDelegate;
            this._DefaultHyperEdgeLabel      = DefaultHyperEdgeLabel;
            this._HyperEdgeCreatorDelegate   = HyperEdgeCreatorDelegate;
            this._HyperEdgesWhenVertex       = HyperEdgesCollectionInitializer();
            this._HyperEdgesWhenGraph        = HyperEdgesCollectionInitializer();

        }

        #endregion

        #region GenericPropertyVertex(Graph, VertexId, IdKey, RevIdKey, DescriptionKey, DatastructureInitializer, EdgeCollectionInitializer, HyperEdgeCollectionInitializer, VertexInitializer = null)

        /// <summary>
        /// Creates a new vertex.
        /// </summary>
        /// <param name="Graph">The associated property graph.</param>
        /// <param name="VertexId">The identification of this vertex.</param>
        /// <param name="IdKey">The key to access the Id of this vertex.</param>
        /// <param name="RevIdKey">The key to access the RevId of this vertex.</param>
        /// <param name="DatastructureInitializer">A delegate to initialize the properties datastructure.</param>
        /// <param name="EdgesCollectionInitializer">A delegate to initialize the datastructure for storing all edges.</param>
        /// <param name="HyperEdgeCollectionInitializer">A delegate to initialize the datastructure for storing all hyperedges.</param>
        /// <param name="VertexInitializer">A delegate to initialize the newly created vertex.</param>
        public GenericPropertyVertex(IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,  
                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,    
                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph,

                              TIdVertex    VertexId,
                              TVertexLabel VertexLabel,
                              TKeyVertex   IdKey,
                              TKeyVertex   RevIdKey,
                              TKeyVertex   DescriptionKey,

                              IDictionaryInitializer<TKeyVertex, TValueVertex> DatastructureInitializer,

                              Func<IGroupedCollection<TIdVertex,    IGenericPropertyVertex   <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TVertexLabel>> VerticesCollectionInitializer,

                              Func<IGroupedCollection<TIdEdge,      IGenericPropertyEdge     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TEdgeLabel>> EdgesCollectionInitializer,

                              Func<IGroupedCollection<TIdMultiEdge, IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TMultiEdgeLabel>> MultiEdgesCollectionInitializer,

                              Func<IGroupedCollection<TIdHyperEdge, IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, THyperEdgeLabel>> HyperEdgesCollectionInitializer,

                              VertexInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexInitializer = null)

            : base(VertexId, VertexLabel, IdKey, RevIdKey, DescriptionKey, DatastructureInitializer)

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
            this._OutEdges              = EdgesCollectionInitializer();
            this._InEdges               = EdgesCollectionInitializer();
            this._EdgesWhenGraph        = EdgesCollectionInitializer();
            this._MultiEdgesWhenVertex  = MultiEdgesCollectionInitializer();
            this._HyperEdgesWhenVertex  = HyperEdgesCollectionInitializer();

            if (VertexInitializer != null)
                VertexInitializer(this);

        }

        #endregion

        #endregion


        // IGenericPropertyVertex

        #region OutEdge methods [IGenericPropertyVertex]

        #region AddOutEdge(Edge)

        /// <summary>
        /// Add an outgoing edge.
        /// </summary>
        /// <param name="Edge">The edge to add.</param>
        void IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                 AddOutEdge(IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)
        {

            if (SendOutEdgeAddingVote(Edge))
            {
                if (_OutEdges.TryAddValue(Edge.Id, Edge, Edge.Label))    // Is supposed to be thread-safe!
                {
                    SendOutEdgeAddedNotification(Edge);
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
        IEnumerable<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            OutEdges(params TEdgeLabel[] EdgeLabels)

        {

            if (EdgeLabels != null && EdgeLabels.Any())
            {
                foreach (var _Edge in _OutEdges)
                    foreach (var _Label in EdgeLabels)
                        if (_Edge.Label.Equals(_Label))
                            yield return _Edge;
            }

            else
                foreach (var _Edge in _OutEdges)
                    yield return _Edge;

        }

        #endregion

        #region OutEdges(EdgeFilter)

        /// <summary>
        /// The edges emanating from, or leaving, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        IEnumerable<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
        
            IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            OutEdges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)
        
        {

            if (EdgeFilter == null)
                return from   Edge
                       in     _OutEdges
                       select Edge;

            else
                return from   Edge
                       in     _OutEdges
                       where  EdgeFilter(Edge)
                       select Edge;

        }

        #endregion

        #region OutDegree(params EdgeLabels)     // OutDegree()!

        /// <summary>
        /// The number of edges emanating from, or leaving, this vertex
        /// filtered by their label. If no label was given,
        /// all edges will be returned.
        /// </summary>
        UInt64 IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.            

               OutDegree(params TEdgeLabel[] EdgeLabels)

        {

            if (EdgeLabels.Length == 0)
                return _OutEdges.Count();

            var Counter = 0UL;

            if (EdgeLabels != null && EdgeLabels.Any())
            {
                foreach (var _Edge in _OutEdges)
                    foreach (var _Label in EdgeLabels)
                        if (_Edge.Label.Equals(_Label))
                            Counter++;
            }

            else
                foreach (var _Edge in _OutEdges)
                    Counter++;

            return Counter;

        }

        #endregion

        #region OutDegree(EdgeFilter)

        /// <summary>
        /// The number of edges emanating from, or leaving, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        UInt64 IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

               OutDegree(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)

        {

            if (EdgeFilter == null)
                return _OutEdges.Count();

            return (UInt64) SuperVertex.OutEdges(EdgeFilter).Count();

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

            foreach (var _Edge in _OutEdges)
            {
                _OutEdges.TryRemoveValue(_Edge.Id, _Edge, _Edge.Label);    // Is supposed to be thread-safe!
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

                var _tmp = new List<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>();

                if (EdgeFilter == null)
                    foreach (var _IEdge in _OutEdges)
                        _tmp.Add(_IEdge);

                else foreach (var _IEdge in _OutEdges)
                    if (EdgeFilter(_IEdge))
                        _tmp.Add(_IEdge);

                foreach (var _Edge in _tmp)
                {
                    _OutEdges.TryRemoveValue(_Edge.Id, _Edge, _Edge.Label);
                }

            }

        }

        #endregion

        #endregion

        #region InEdge methods [IGenericPropertyVertex]

        #region AddInEdge(Edge)

        /// <summary>
        /// Add an incoming edge.
        /// </summary>
        /// <param name="Edge">The edge to add.</param>
        void IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.            
            
             AddInEdge(IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                            Edge)

        {

            if (SendInEdgeAddingVote(Edge))
            {

                _InEdges.TryAddValue(Edge.Id, Edge, Edge.Label);     // Is supposed to be thread-safe!

                foreach (var _MultiEdge in _MultiEdgesWhenVertex)
                    _MultiEdge.AddIfMatches(Edge); 
                
                SendInEdgeAddedNotification(Edge);

            }

        }

        #endregion


        #region InEdges(params EdgeLabels)      // InEdges()!

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex
        /// filtered by their label. If no label was given,
        /// all edges will be returned.
        /// </summary>
        IEnumerable<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            InEdges(params TEdgeLabel[] EdgeLabels)

        {

            if (EdgeLabels != null && EdgeLabels.Any())
            {
                foreach (var _Edge in _InEdges)
                    foreach (var _Label in EdgeLabels)
                        if (_Edge.Label.Equals(_Label))
                            yield return _Edge;
            }

            else
                foreach (var _Edge in _InEdges)
                    yield return _Edge;

        }

        #endregion

        #region InEdges(EdgeFilter)

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        IEnumerable<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
        
            IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            InEdges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)
        
        {

            return from   Edge
                   in     _InEdges
                   where  EdgeFilter(Edge)
                   select Edge;

        }

        #endregion

        #region InDegree(params EdgeLabels)     // InDegree()!

        /// <summary>
        /// The number of edges incoming to, or arriving at, this vertex
        /// filtered by their label. If no label was given,
        /// all edges will be returned.
        /// </summary>
        UInt64 IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            InDegree(params TEdgeLabel[] EdgeLabels)

        {

            if (EdgeLabels.Length == 0)
                return _InEdges.Count();

            var Counter = 0UL;

            if (EdgeLabels != null && EdgeLabels.Any())
            {
                foreach (var _Edge in _InEdges)
                    foreach (var _Label in EdgeLabels)
                        if (_Edge.Label.Equals(_Label))
                            Counter++;
            }

            else
                foreach (var _Edge in _InEdges)
                    Counter++;

            return Counter;

        }

        #endregion

        #region InDegree(EdgeFilter)

        /// <summary>
        /// The number of edges incoming to, or arriving at, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        UInt64 IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            
            InDegree(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)

        {

            if (EdgeFilter == null)
                return _InEdges.Count();

            return (UInt64) SuperVertex.InEdges(EdgeFilter).Count();

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

            foreach (var _Edge in _InEdges)
            {
                _InEdges.TryRemoveValue(_Edge.Id, _Edge, _Edge.Label);     // Is supposed to be thread-safe!
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

                var _tmp = new List<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>();

                if (EdgeFilter == null)
                    foreach (var _IEdge in _InEdges)
                        _tmp.Add(_IEdge);

                else foreach (var _IEdge in _InEdges)
                    if (EdgeFilter(_IEdge))
                        _tmp.Add(_IEdge);

                foreach (var _Edge in _tmp)
                {
                    _InEdges.TryRemoveValue(_Edge.Id, _Edge, _Edge.Label);    // Is supposed to be thread-safe!
                }

            }

        }

        #endregion

        #endregion

        #region MultiEdge methods [IGenericPropertyVertex]

        #region AddMultiEdge(MultiEdge)

        /// <summary>
        /// Add a multiedge.
        /// </summary>
        /// <param name="MultiEdge">The multiedge to add.</param>
        void IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            AddMultiEdge(IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdge)

        {
            throw new NotImplementedException();
        }

        #endregion


        #region MultiEdges(params MultiEdgeLabels)      // MultiEdges()!

        /// <summary>
        /// The multiedges emanating from, or leaving, this vertex
        /// filtered by their label. If no label was given,
        /// all multiedges will be returned.
        /// </summary>
        IEnumerable<IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            MultiEdges(params TMultiEdgeLabel[] MultiEdgeLabels)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region MultiEdges(MultiEdgeFilter)

        /// <summary>
        /// The multiedges emanating from, or leaving, this vertex
        /// filtered by the given multiedge filter delegate.
        /// </summary>
        /// <param name="MultiEdgeFilter">A delegate for multiedge filtering.</param>
        IEnumerable<IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
            IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            MultiEdges(MultiEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeFilter)

        {
            throw new NotImplementedException();
        }

        #endregion


        #region RemoveMultiEdges(params MultiEdges)    // RemoveMultiEdges()!

        /// <summary>
        /// Remove multiedges.
        /// </summary>
        /// <param name="MultiEdges">An array of outgoing edges to be removed.</param>
        void IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            RemoveMultiEdges(params IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] MultiEdges)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region RemoveMultiEdges(MultiEdgeFilter = null)

        /// <summary>
        /// Remove any outgoing multiedge matching
        /// the given multiedge filter delegate.
        /// </summary>
        /// <param name="MultiEdgeFilter">A delegate for multiedge filtering.</param>
        void IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            
            RemoveMultiEdges(MultiEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeFilter = null)

        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region HyperEdge methods [IGenericPropertyVertex]

        #region AddHyperEdge(HyperEdge)

        /// <summary>
        /// Add a hyperedge.
        /// </summary>
        /// <param name="HyperEdge">The hyperedge to add.</param>
        void IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            AddHyperEdge(IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdge)

        {
            _HyperEdgesWhenVertex.TryAddValue(HyperEdge.Id, HyperEdge, HyperEdge.Label);    // Is supposed to be thread-safe!
        }

        #endregion


        #region HyperEdges(params HyperEdgeLabels)      // HyperEdges()!

        IEnumerable<IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                HyperEdges(params THyperEdgeLabel[] HyperEdgeLabels)

        {

            if (HyperEdgeLabels != null && HyperEdgeLabels.Any())
            {
                foreach (var _HyperEdge in _HyperEdgesWhenVertex)
                    foreach (var _Label in HyperEdgeLabels)
                        if (_HyperEdge.Label.Equals(_Label))
                            yield return _HyperEdge;
            }

            else
                foreach (var _HyperEdge in _HyperEdgesWhenVertex)
                    yield return _HyperEdge;

        }

        #endregion RemoveHyperEdges(params HyperEdges)

        #region HyperEdges(HyperEdgeFilter = null)

        /// <summary>
        /// Get an enumeration of all HyperEdges in the graph.
        /// An optional HyperEdge filter may be applied for filtering.
        /// </summary>
        /// <param name="HyperEdgeFilter">A delegate for HyperEdge filtering.</param>
        IEnumerable<IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                    HyperEdges(HyperEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter = null)
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
            throw new NotImplementedException();
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
                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion


        // IGenericPropertyGraph

        #region Vertex methods [IGenericPropertyGraph]

        #region (private) AddVertexToGraph(Vertex)

        /// <summary>
        /// Add the given vertex to the graph.
        /// </summary>
        /// <param name="Vertex">A vertex.</param>
        /// <returns>The given vertex</returns>
        IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

               AddVertexToGraph(IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex)

        {

            if (SendVertexAddingVote(Vertex))
            {
                if (_VerticesWhenGraph.TryAddValue(Vertex.Id, Vertex, Vertex.Label))
                {
                    SendVertexAddedNotification(Vertex);
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
        IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
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
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexInitializer = null)

        {
            return Subgraph.AddVertex(_VertexIdCreatorDelegate(this), _DefaultVertexLabel, VertexInitializer);
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
        IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
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
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexInitializer = null)

        {

            #region Initial checks

            var VertexId = _VertexIdCreatorDelegate(this);

            if (_VerticesWhenGraph.ContainsKey(VertexId))
                throw new DuplicateVertexIdException("Another vertex with id " + VertexId + " already exists!");

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
        IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
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
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexInitializer = null)

        {

            #region Initial checks

            if (Id == null)
                Id = _VertexIdCreatorDelegate(this);

            if (_VerticesWhenGraph.ContainsKey(Id))
                throw new DuplicateVertexIdException("Another vertex with id " + Id + " already exists!");

            if (Label == null)
                Label = _DefaultVertexLabel;

            #endregion

            return AddVertexToGraph(_VertexCreatorDelegate(this, Id, Label, VertexInitializer));

        }

        #endregion

        #region AddVertexIfNotExists(Id, Label, VertexInitializer = null)

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
        /// <returns>The newly created or already existing vertex.</returns>
        IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
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
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexInitializer = null,

                                     VertexInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> AnywayDo = null)

        {
            
            #region Initial checks

            if (Id == null)
                Id = _VertexIdCreatorDelegate(this);

            IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _Vertex = null;

            if (_VerticesWhenGraph.TryGetByKey(Id, out _Vertex))
            {

                if (Label.Equals(_Vertex.Label))
                {

                    if (AnywayDo != null)
                        AnywayDo(_Vertex);

                    return _Vertex;

                }

                throw new DuplicateVertexIdException("Another vertex with id " + Id + " but different vertex label already exists!");

            }

            if (Label == null)
                Label = _DefaultVertexLabel;

            #endregion

            return AddVertexToGraph(_VertexCreatorDelegate(this, Id, Label, VertexInitializer));

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
        IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.    
                       AddVertex(IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex)
        {

            #region Initial checks

            if (Vertex == null)
                throw new ArgumentNullException("The given vertex must not be null!");

            if (Vertex.Id == null || Vertex.Id.Equals(default(TIdVertex)))
                throw new ArgumentNullException("The unique identification of vertex must not be null!");

            if (_VerticesWhenGraph.ContainsKey(Vertex.Id))
                throw new DuplicateVertexIdException("Another vertex with identification " + Vertex.Id + " already exists!");

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
        IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            AddVertexIfNotExists(IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex,
                                 CheckVertexExistance  <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> CheckExistanceDelegate = null)

        {

            #region Initial checks

            if (Vertex == null)
                throw new ArgumentNullException("The given vertex must not be null!");

            if (Vertex.Id == null || Vertex.Id.Equals(default(TIdVertex)))
                throw new ArgumentNullException("The unique identification of vertex must not be null!");

            if (_VerticesWhenGraph.ContainsKey(Vertex.Id))
                throw new DuplicateVertexIdException("Another vertex with identification " + Vertex.Id + " already exists!");

            #endregion

            if (CheckExistanceDelegate != null && !CheckExistanceDelegate(Subgraph, Vertex))
                throw new Exception("CheckExistanceDelegate constraint is violated!");

            return AddVertexToGraph(Vertex);

        }

        #endregion


        #region VertexById(VertexId)

        /// <summary>
        /// Return the vertex referenced by the given vertex identifier.
        /// If no vertex is referenced by the identifier return null.
        /// </summary>
        /// <param name="VertexId">A vertex identifier.</param>
        IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                VertexById(TIdVertex VertexId)

        {
            
            #region Initial checks

            if (VertexId == null)
                throw new ArgumentNullException("VertexId", "The given vertex identifier must not be null!");

            #endregion

            IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _Vertex = null;

            if (_VerticesWhenGraph.TryGetByKey(VertexId, out _Vertex))
                return _Vertex;

            return null;

        }

        #endregion

        #region TryGetVertexById(VertexId)

        /// <summary>
        /// Try to return the vertex referenced by the given vertex identifier.
        /// </summary>
        /// <param name="VertexId">A vertex identifier.</param>
        /// <param name="Vertex">A vertex.</param>
        /// <returns>True when success; false otherwise.</returns>
        Boolean 
            
            IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                TryGetVertexById(TIdVertex VertexId, out IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex)
        {

            #region Initial checks

            if (VertexId == null)
                throw new ArgumentNullException("VertexId", "The given vertex identifier must not be null!");

            #endregion

            return (_VerticesWhenGraph.TryGetByKey(VertexId, out Vertex));

        }

        #endregion

        #region VerticesById(params VertexIds)

        /// <summary>
        /// Return the vertices referenced by the given array of vertex identifiers.
        /// If no vertex is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="VertexIds">An array of vertex identifiers.</param>
        IEnumerable<IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                VerticesById(params TIdVertex[] VertexIds)

        {

            #region Initial checks

            if (VertexIds == null || !VertexIds.Any())
                throw new ArgumentNullException("VertexIds", "The array of vertex identifiers must not be null or its length zero!");

            #endregion

            IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _IVertex;

            foreach (var _VertexId in VertexIds)
            {

                if (_VertexId != null)
                {
                    if (_VerticesWhenGraph.TryGetByKey(_VertexId, out _IVertex))
                        yield return _IVertex;
                }

                yield return null;

            }

        }

        #endregion

        #region VerticesByLabel(params VertexLabels)

        /// <summary>
        /// Return an enumeration of all vertices having one of the
        /// given vertex labels.
        /// </summary>
        /// <param name="VertexLabels">An array of vertex labels.</param>
        IEnumerable<IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                VerticesByLabel(params TVertexLabel[] VertexLabels)

        {

            foreach (var Vertex in _VerticesWhenGraph)
            {
                foreach (var VertexLabel in VertexLabels)
                {
                    if (Vertex.Label != null &&
                        Vertex.Label.Equals(VertexLabel))
                        yield return Vertex;
                }
            }

        }

        #endregion

        #region Vertices(VertexFilter = null)

        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// An optional vertex filter may be applied for filtering.
        /// </summary>
        /// <param name="VertexFilter">A delegate for vertex filtering.</param>
        IEnumerable<IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
        
            IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                Vertices(VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexFilter = null)

        {

            if (VertexFilter == null)
                return from   Vertex
                       in     _VerticesWhenGraph
                       select Vertex;

            else
                return from   Vertex
                       in     _VerticesWhenGraph
                       where  VertexFilter(Vertex)
                       select Vertex;

        }

        #endregion

        #region NumberOfVertices(VertexFilter = null)

        /// <summary>
        /// Return the current number of vertices matching the given optional vertex filter.
        /// When the filter is null, this method should use implement an optimized
        /// way to get the currenty number of vertices.
        /// </summary>
        /// <param name="VertexFilter">A delegate for vertex filtering.</param>
        UInt64 IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            NumberOfVertices(VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexFilter = null)
        {

            if (VertexFilter == null)
                return _VerticesWhenGraph.Count();

            else
            {
                lock (this)
                {

                    var _Counter = 0UL;

                    foreach (var _Vertex in _VerticesWhenGraph)
                        if (VertexFilter(_Vertex))
                            _Counter++;

                    return _Counter;

                }
            }

        }

        #endregion


        #region RemoveVerticesById(params VertexIds)

        /// <summary>
        /// Remove the vertex identified by the given VertexId from the graph
        /// </summary>
        /// <param name="VertexIds">An array of VertexIds of the vertices to remove.</param>
        void IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                                   RemoveVerticesById(params TIdVertex[] VertexIds)

        {

            #region Initial checks

            if (VertexIds == null || !VertexIds.Any())
                throw new ArgumentNullException("VertexIds", "The given array of vertex identifiers must not be null or its length zero!");

            #endregion

            IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _Vertex;

            foreach (var VertexId in VertexIds)
            {
                if (_VerticesWhenGraph.TryGetByKey(VertexId, out _Vertex))
                    (this as IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>).
                    RemoveVertices(_Vertex);
                else
                    throw new ArgumentException("The given vertex identifier '" + VertexId.ToString() + "' is unknowen!");
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
        void IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
             RemoveVertices(params IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)
        {

            #region Initial checks

            if (Vertices == null || !Vertices.Any())
                throw new ArgumentNullException("Vertices", "The array of vertices must not be null or its length zero!");

            #endregion

            lock (this)
            {

                foreach (var _Vertex in Vertices)
                {
                    if (_VerticesWhenGraph.ContainsKey(_Vertex.Id))
                    {
    
                        var _EdgeList = new List<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>();
    
                        _EdgeList.AddRange(_Vertex.InEdges());
                        _EdgeList.AddRange(_Vertex.OutEdges());
    
                        // removal requires removal from all indices
                        //for (TinkerIndex index : this.indices.values()) {
                        //    index.remove(vertex);
                        //}

                        _VerticesWhenGraph.TryRemoveValue(_Vertex.Id, _Vertex, _Vertex.Label);
    
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
        void IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
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

                var _VerticesToRemove = new List<IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>();

                if (VertexFilter == null)
                    foreach (var _Vertex in _VerticesWhenGraph)
                        _VerticesToRemove.Add(_Vertex);

                else    
                    foreach (var _Vertex in _VerticesWhenGraph)
                        if (VertexFilter(_Vertex))
                            _VerticesToRemove.Add(_Vertex);

                foreach (var _Vertex in _VerticesToRemove)
                    this.Graph.RemoveVertices(_Vertex);

            }

        }

        #endregion

        #endregion

        #region Edge methods [IGenericPropertyGraph]

        #region (private) AddEdgeToGraph(Edge)

        /// <summary>
        /// Add the given edge to the graph.
        /// </summary>
        /// <param name="Edge">An edge.</param>
        /// <returns>The given edge</returns>
        IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

               AddEdgeToGraph(IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)

        {

            if (SendEdgeAddingVote(Edge))
            {
                if (_EdgesWhenGraph.TryAddValue(Edge.Id, Edge, Edge.Label))
                {
                    Edge.OutVertex.AddOutEdge(Edge);
                    Edge.InVertex.AddInEdge(Edge);
                    SendEdgeAddedNotification(Edge);
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
        IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            AddEdge(IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

                                     TEdgeLabel Label,

                                     IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

                                     EdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer = null)
        {

            return Subgraph.AddEdge(OutVertex, InVertex, _EdgeIdCreatorDelegate(this), Label, EdgeInitializer);

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
        IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                             AddEdge(TIdEdge         EdgeId,

                                     IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

                                     TEdgeLabel      Label,

                                     IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

                                     EdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer = null)


        {

            return Subgraph.AddEdge(OutVertex, InVertex, _EdgeIdCreatorDelegate(this), Label, EdgeInitializer);

        }

        #endregion

        #region AddEdgeIfNotExists(EdgeId, OutVertex, Label, InVertex, EdgeInitializer = null)

        /// <summary>
        /// Add an edge to the graph. The added edge requires a tail vertex,
        /// a head vertex, an identifier, a label and initializes the edge
        /// by invoking the given EdgeInitializer.
        /// 'OutVertex --Label-> InVertex' is the "Semantic Web Notation" ;)
        /// </summary>
        /// <param name="EdgeId">A EdgeId. If none was given a new one will be generated.</param>
        /// <param name="OutVertex">The vertex on the tail of the edge.</param>
        /// <param name="Label">The label associated with the edge.</param>
        /// <param name="InVertex">The vertex on the head of the edge.</param>
        /// <param name="EdgeInitializer">A delegate to initialize the new edge.</param>
        /// <returns>The new edge.</returns>
        IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                             AddEdgeIfNotExists(TIdEdge Id,

                                                IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

                                                TEdgeLabel Label,

                                                IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

                                                EdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer = null,

                                                EdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> AnywayDo = null)


        {

            #region Initial checks

            if (Id == null)
                Id = _EdgeIdCreatorDelegate(this);

            IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _Edge = null;

            if (_EdgesWhenGraph.TryGetByKey(Id, out _Edge))
            {

                if (Label.Equals(_Edge.Label))
                {

                    if (AnywayDo != null)
                        AnywayDo(_Edge);
                    
                    return _Edge;

                }

                throw new DuplicateVertexIdException("Another edge with id " + Id + " but different edge label already exists!");

            }

            if (OutVertex == null)
                throw new ArgumentException("The OutVertex must not be null!");

            if (InVertex == null)
                throw new ArgumentException("The InVertex must not be null!");

            if (Label == null)
                Label = _DefaultEdgeLabel;

            #endregion

            return AddEdgeToGraph(_EdgeCreatorDelegate(this, OutVertex, InVertex, Id, Label, EdgeInitializer));

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
        IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                             AddEdge(IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

                                     IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

                                     TEdgeLabel      Label  = default(TEdgeLabel),

                                     EdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer = null)


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
        IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                             AddEdge(IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

                                     IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

                                     TIdEdge         EdgeId,
                                     TEdgeLabel      Label  = default(TEdgeLabel),

                                     EdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer = null)
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
        IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.    
                       AddEdge(IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)
        {

            #region Initial checks

            if (Edge == null)
                throw new ArgumentNullException("The given edge must not be null!");

            if (Edge.Id == null || Edge.Id.Equals(default(TIdEdge)))
                throw new ArgumentNullException("The unique identification of edge must not be null!");

            if (_EdgesWhenGraph.ContainsKey(Edge.Id))
                throw new DuplicateVertexIdException("Another edge with identification " + Edge.Id + " already exists!");

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
        IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.    
                       AddEdgeIfNotExists(IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge,
                                          CheckEdgeExistance  <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> CheckExistanceDelegate = null)
        {

            #region Initial checks

            if (Edge == null)
                throw new ArgumentNullException("The given edge must not be null!");

            if (Edge.Id == null || Edge.Id.Equals(default(TIdEdge)))
                throw new ArgumentNullException("The unique identification of edge must not be null!");

            if (_EdgesWhenGraph.ContainsKey(Edge.Id))
                throw new DuplicateVertexIdException("Another edge with identification " + Edge.Id + " already exists!");

            #endregion

            if (CheckExistanceDelegate != null && !CheckExistanceDelegate(Subgraph, Edge))
                throw new Exception("CheckExistanceDelegate constraint is violated!");

            return AddEdgeToGraph(Edge);

        }

        #endregion


        #region EdgeById(EdgeId)

        /// <summary>
        /// Return the edge referenced by the given edge identifier.
        /// If no edge is referenced by a given identifier return null.
        /// </summary>
        /// <param name="EdgeId">An edge identifier.</param>
        IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                EdgeById(TIdEdge EdgeId)

        {

            #region Initial checks

            if (EdgeId == null)
                throw new ArgumentNullException("EdgeId", "The given edge identifier must not be null!");

            #endregion

            IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _Edge = null;

            if (_EdgesWhenGraph.TryGetByKey(EdgeId, out _Edge))
                return _Edge;

            return null;

        }

        #endregion

        #region TryGetEdgeById(EdgeId)

        /// <summary>
        /// Try to return the edge referenced by the given edge identifier.
        /// </summary>
        /// <param name="EdgeId">A edge identifier.</param>
        /// <param name="Edge">A edge.</param>
        /// <returns>True when success; false otherwise.</returns>
        Boolean 
            
            IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                TryGetEdgeById(TIdEdge EdgeId, out IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)
        {

            #region Initial checks

            if (EdgeId == null)
                throw new ArgumentNullException("EdgeId", "The given edge identifier must not be null!");

            #endregion

            return (_EdgesWhenGraph.TryGetByKey(EdgeId, out Edge));

        }

        #endregion

        #region EdgesById(params EdgeIds)

        /// <summary>
        /// Return the edges referenced by the given array of edge identifiers.
        /// If no edge is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="EdgeIds">An array of edge identifiers.</param>
        IEnumerable<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                EdgesById(params TIdEdge[] EdgeIds)

        {

            #region Initial checks

            if (EdgeIds == null || !EdgeIds.Any())
                throw new ArgumentNullException("EdgeIds", "The given array of edge identifiers must not be null or its length zero!");

            #endregion

            IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _Edge;

            foreach (var _EdgeId in EdgeIds)
            {

                if (_EdgeId != null)
                {
                    if (_EdgesWhenGraph.TryGetByKey(_EdgeId, out _Edge))
                        yield return _Edge;
                }

                yield return null;

            }

        }

        #endregion

        #region EdgesByLabel(params EdgeLabels)

        /// <summary>
        /// Return an enumeration of all edges having one of the
        /// given edge labels.
        /// </summary>
        /// <param name="EdgeLabels">An array of edge labels.</param>
        IEnumerable<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
            IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                EdgesByLabel(params TEdgeLabel[] EdgeLabels)

        {

            foreach (var Edge in _EdgesWhenGraph)
            {
                foreach (var EdgeLabel in EdgeLabels)
                {
                    if (Edge.Label != null &&
                        Edge.Label.Equals(EdgeLabel))
                        yield return Edge;
                }
            }

        }

        #endregion

        #region Edges(EdgeFilter = null)

        /// <summary>
        /// Return an enumeration of all edges in the graph.
        /// An optional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        IEnumerable<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                Edges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter = null)

        {

            if (EdgeFilter == null)
                return from   Edge
                       in     _EdgesWhenGraph
                       select Edge;

            else
                return from   Edge
                       in     _EdgesWhenGraph
                       where  EdgeFilter(Edge)
                       select Edge;

        }

        #endregion

        #region NumberOfEdges(EdgeFilter = null)

        /// <summary>
        /// Return the current number of edges matching the given optional edge filter.
        /// When the filter is null, this method should use implement an optimized
        /// way to get the currenty number of edges.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        UInt64 IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            NumberOfEdges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter = null)

        {

            if (EdgeFilter == null)
                return _EdgesWhenGraph.Count();

            else
            {
                lock (this)
                {

                    var _Counter = 0UL;

                    foreach (var _Edge in _EdgesWhenGraph)
                        if (EdgeFilter(_Edge))
                            _Counter++;

                    return _Counter;

                }
            }
        
        }

        #endregion


        #region RemoveEdgesById(params EdgeIds)

        /// <summary>
        /// Remove the given array of edges identified by their EdgeIds.
        /// </summary>
        /// <param name="EdgeIds">An array of EdgeIds of the edges to remove</param>
        void IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.            
                                   RemoveEdgesById(params TIdEdge[] EdgeIds)

        {

            #region Initial checks

            if (EdgeIds == null || !EdgeIds.Any())
                throw new ArgumentNullException("EdgeIds", "The given array of edge identifiers must not be null or its length zero!");

            #endregion

            IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _Edge;

            if (EdgeIds.Any())
            {
                foreach (var _EdgeId in EdgeIds)
                {
                    if (_EdgesWhenGraph.TryGetByKey(_EdgeId, out _Edge))
                        this.Graph.RemoveEdges(_Edge);
                    else
                        throw new ArgumentException("The given edge identifier '" + _EdgeId.ToString() + "' is unknowen!");
                }
            }

        }

        #endregion

        #region RemoveEdges(params Edges)    // RemoveEdges()!

        /// <summary>
        /// Remove the given array of edges from the graph.
        /// </summary>
        /// <param name="Edges">An array of edges to be removed from the graph.</param>
        void IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
             RemoveEdges(params IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Edges)

        {

            #region Initial checks

            if (Edges == null || !Edges.Any())
                throw new ArgumentNullException("Edges", "The given array of edges must not be null or its length zero!");

            #endregion

            lock (this)
            {

                foreach (var _Edge in Edges)
                {
                    if (_EdgesWhenGraph.ContainsKey(_Edge.Id))
                    {

                        var _OutVertex = _Edge.OutVertex;
                        var _InVertex  = _Edge.InVertex;

                        if (_OutVertex != null && _OutVertex.OutEdges() != null)
                            _OutVertex.RemoveOutEdges(Edges);

                        if (_InVertex != null && _InVertex.InEdges() != null)
                            _InVertex.RemoveInEdges(_Edge);

                        // removal requires removal from all indices
                        //for (TinkerIndex index : this.indices.values()) {
                        //    index.remove(edge);
                        //}

                        _EdgesWhenGraph.TryRemoveValue(_Edge.Id, _Edge, _Edge.Label);

                    }
                }

            }

        }

        #endregion

        #region RemoveEdges(EdgeFilter = null)

        /// <summary>
        /// Remove any edge matching the given edge filter.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        void IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
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

                var _EdgesToRemove = new List<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>();

                if (EdgeFilter == null)
                    foreach (var _IEdge in _EdgesWhenGraph)
                        _EdgesToRemove.Add(_IEdge);

                else
                    foreach (var _IEdge in _EdgesWhenGraph)
                        if (EdgeFilter(_IEdge))
                            _EdgesToRemove.Add(_IEdge);

                foreach (var _IEdge in _EdgesToRemove)
                    this.Graph.RemoveEdges(_IEdge);

            }

        }

        #endregion

        #endregion

        #region MultiEdge methods [IGenericPropertyGraph]

        #region AddMultiEdge(params Vertices)

        /// <summary>
        /// Add a multiedge based on the given enumeration
        /// of vertices to the graph.
        /// </summary>
        /// <param name="Vertices">An enumeration of vertices.</param>
        /// <returns>The new multiedge</returns>
        IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            AddMultiEdge(params IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region AddMultiEdge(Label, params Vertices)

        /// <summary>
        /// Add a multiedge based on the given multiedge label and
        /// an enumeration of vertices to the graph.
        /// </summary>
        /// <param name="Label">The multiedge label.</param>
        /// <param name="Vertices">An enumeration of vertices.</param>
        /// <returns>The new multiedge</returns>
        IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            AddMultiEdge(TMultiEdgeLabel Label,
                         params IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region AddMultiEdge(Label, MultiEdgeInitializer, params Vertices)

        /// <summary>
        /// Add a multiedge based on the given multiedge label and
        /// an enumeration of vertices to the graph and initialize
        /// it by invoking the given MultiEdgeInitializer.
        /// </summary>
        /// <param name="Label">The multiedge label.</param>
        /// <param name="MultiEdgeInitializer">A delegate to initialize the newly generated multiedge.</param>
        /// <param name="Vertices">An enumeration of vertices.</param>
        /// <returns>The new multiedge</returns>
        IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
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
        
                             params IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region AddMultiEdge(MultiEdgeId, Label, MultiEdgeInitializer, params Vertices)

        /// <summary>
        /// Add a multiedge based on the given multiedge label and
        /// an enumeration of vertices to the graph and initialize
        /// it by invoking the given MultiEdgeInitializer.
        /// </summary>
        /// <param name="MultiEdgeId">A MultiEdgeId. If none was given a new one will be generated.</param>
        /// <param name="Label">The multiedge label.</param>
        /// <param name="MultiEdgeInitializer">A delegate to initialize the newly generated multiedge.</param>
        /// <param name="Vertices">An enumeration of vertices.</param>
        /// <returns>The new multiedge</returns>
        IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> 
            
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            AddMultiEdge(TIdMultiEdge    MultiEdgeId,
                         TMultiEdgeLabel Label,
                         MultiEdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeInitializer,        
                         params IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)
        {

            if (MultiEdgeId == null)
                MultiEdgeId = _MultiEdgeIdCreatorDelegate(this);

            if (_MultiEdgesWhenGraph.ContainsKey(MultiEdgeId))
                throw new ArgumentException("Another multiedge with id " + MultiEdgeId + " already exists!");

            var _MultiEdge = _MultiEdgeCreatorDelegate(this, (me) => true, MultiEdgeId, Label, MultiEdgeInitializer);

            if (SendMultiEdgeAddingToGraphVote(_MultiEdge))
            {
                if (_MultiEdgesWhenGraph.TryAddValue(MultiEdgeId, _MultiEdge, _MultiEdge.Label))
                {
                    //OutVertex.AddOutEdge(_Edge);
                    //InVertex.AddInEdge(_Edge);
                    SendMultiEdgeAddedToGraphNotification(_MultiEdge);
                    return _MultiEdge;
                }
            }

            return null;

        }

        #endregion

        #region AddMultiEdge(IGenericPropertyMultiEdge)

        /// <summary>
        /// Adds the given multiedge to the graph, and returns it again.
        /// An exception will be thrown if the multiedge identifier is already being
        /// used by the graph to reference another multiedge.
        /// </summary>
        /// <param name="IGenericPropertyMultiEdge">An IGenericPropertyMultiEdge.</param>
        /// <returns>The given IGenericPropertyMultiEdge.</returns>
        IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                       AddMultiEdge(IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyMultiEdge)
        {

            #region Initial checks

            if (IGenericPropertyMultiEdge == null)
                throw new ArgumentNullException("The given multiedge must not be null!");

            if (IGenericPropertyMultiEdge.Id == null || IGenericPropertyMultiEdge.Id.Equals(default(TIdMultiEdge)))
                throw new ArgumentNullException("The unique identification of multiedge must not be null!");

            if (_MultiEdgesWhenGraph.ContainsKey(IGenericPropertyMultiEdge.Id))
                throw new DuplicateVertexIdException("Another multiedge with identification " + IGenericPropertyMultiEdge.Id + " already exists!");

            #endregion

            if (SendMultiEdgeAddingVote(IGenericPropertyMultiEdge))
            {
                if (_MultiEdgesWhenGraph.TryAddValue(IGenericPropertyMultiEdge.Id, IGenericPropertyMultiEdge, IGenericPropertyMultiEdge.Label))
                {
                    SendMultiEdgeAddedNotification(IGenericPropertyMultiEdge);
                    return IGenericPropertyMultiEdge;
                }
            }

            return null;

        }

        #endregion


        #region MultiEdgeById(MultiEdgeId)

        /// <summary>
        /// Return the MultiEdge referenced by the given MultiEdge identifier.
        /// If no MultiEdge is referenced by the identifier return null.
        /// </summary>
        /// <param name="MultiEdgeId">A MultiEdge identifier.</param>
        IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                MultiEdgeById(TIdMultiEdge MultiEdgeId)

        {

            #region Initial checks

            if (MultiEdgeId == null)
                throw new ArgumentNullException("MultiEdgeId", "The given multiedge identifier must not be null!");

            #endregion

            IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _MultiEdge = null;

            if (_MultiEdgesWhenVertex.TryGetByKey(MultiEdgeId, out _MultiEdge))
                return _MultiEdge;

            return null;

        }

        #endregion

        #region MultiEdgesById(params MultiEdgeIds)

        /// <summary>
        /// Return the MultiEdges referenced by the given array of MultiEdge identifiers.
        /// If no MultiEdge is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="MultiEdgeIds">An array of MultiEdge identifiers.</param>
        IEnumerable<IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                MultiEdgesById(params TIdMultiEdge[] MultiEdgeIds)

        {

            #region Initial checks

            if (MultiEdgeIds == null || !MultiEdgeIds.Any())
                throw new ArgumentNullException("MultiEdgeIds", "The array of multiedge identifiers must not be null or its length zero!");

            #endregion

            IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _MultiEdge;

            foreach (var _MultiEdgeId in MultiEdgeIds)
            {

                if (_MultiEdgeId != null)
                {
                    if (_MultiEdgesWhenGraph.TryGetByKey(_MultiEdgeId, out _MultiEdge))
                        yield return _MultiEdge;
                }

                yield return null;

            }

        }

        #endregion

        #region MultiEdgesByLabel(params MultiEdgeLabels)

        /// <summary>
        /// Return an enumeration of all multiedges having one
        /// of the given multiedge labels.
        /// </summary>
        /// <param name="MultiEdgeLabels">An array of multiedge labels.</param>
        IEnumerable<IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                MultiEdgesByLabel(params TMultiEdgeLabel[] MultiEdgeLabels)

        {

            foreach (var MultiEdge in _MultiEdgesWhenVertex)
            {
                foreach (var MultiEdgeLabel in MultiEdgeLabels)
                {
                    if (MultiEdge.Label != null &&
                        MultiEdge.Label.Equals(MultiEdgeLabel))
                        yield return MultiEdge;
                }
            }

        }

        #endregion

        #region MultiEdges(MultiEdgeFilter = null)

        /// <summary>
        /// Get an enumeration of all MultiEdges in the graph.
        /// An optional MultiEdge filter may be applied for filtering.
        /// </summary>
        /// <param name="MultiEdgeFilter">A delegate for MultiEdge filtering.</param>
        IEnumerable<IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                MultiEdges(MultiEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeFilter = null)

        {

            if (MultiEdgeFilter == null)
                return from   MultiEdge
                       in     _MultiEdgesWhenVertex
                       select MultiEdge;

            else
                return from   MultiEdge
                       in     _MultiEdgesWhenVertex
                       where  MultiEdgeFilter(MultiEdge)
                       select MultiEdge;

        }

        #endregion

        #region NumberOfMultiEdges(MultiEdgeFilter = null)

        /// <summary>
        /// Return the current number of MultiEdges matching the given optional MultiEdge filter.
        /// When the filter is null, this method should implement an optimized
        /// way to get the currenty number of edges.
        /// </summary>
        /// <param name="MultiEdgeFilter">A delegate for MultiEdge filtering.</param>
        UInt64 IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            NumberOfMultiEdges(MultiEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeFilter = null)

        {

            if (MultiEdgeFilter == null)
                return _MultiEdgesWhenVertex.Count();

            else
            {
                lock (this)
                {

                    var _Counter = 0UL;

                    foreach (var _MultiEdge in _MultiEdgesWhenVertex)
                        if (MultiEdgeFilter(_MultiEdge))
                            _Counter++;

                    return _Counter;

                }
            }

        }

        #endregion


        #region RemoveMultiEdgesById(params MultiEdgeIds)

        /// <summary>
        /// Remove the given array of multiedges identified by their MultiEdgeIds.
        /// </summary>
        /// <param name="MultiEdgeIds">An array of MultiEdgeIds of the multiedges to remove.</param>
        void IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            RemoveMultiEdgesById(params TIdMultiEdge[] MultiEdgeIds)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region RemoveMultiEdges(params MultiEdges)    // RemoveMultiEdges()!

        /// <summary>
        /// Remove the given array of multiedges from the graph.
        /// </summary>
        /// <param name="MultiEdges">An array of multiedges to be removed from the graph.</param>
        void IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            RemoveMultiEdges(params IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] MultiEdges)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region RemoveMultiEdges(MultiEdgeFilter = null)

        /// <summary>
        /// Remove any multiedge matching the given multiedge filter.
        /// </summary>
        /// <param name="MultiEdgeFilter">A delegate for multiedge filtering.</param>
        void IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            RemoveMultiEdges(MultiEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeFilter = null)

        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region HyperEdge methods [IGenericPropertyGraph]

        #region AddHyperEdge(params Vertices)

        /// <summary>
        /// Add a multiedge based on the given enumeration
        /// of vertices to the graph.
        /// </summary>
        /// <param name="Vertices">An enumeration of vertices.</param>
        /// <returns>The new multiedge</returns>
        IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            AddHyperEdge(params IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)

        {
            return Subgraph.AddHyperEdge(_HyperEdgeIdCreatorDelegate(this), _DefaultHyperEdgeLabel, null, Vertices);
        }

        #endregion

        #region AddHyperEdge(Label, params Vertices)

        /// <summary>
        /// Add a multiedge based on the given multiedge label and
        /// an enumeration of vertices to the graph.
        /// </summary>
        /// <param name="Label">The multiedge label.</param>
        /// <param name="Vertices">An enumeration of vertices.</param>
        /// <returns>The new multiedge</returns>
        IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            AddHyperEdge(THyperEdgeLabel Label,            
                         params IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)

        {
            return Subgraph.AddHyperEdge(_HyperEdgeIdCreatorDelegate(this), Label, null, Vertices);
        }

        #endregion

        #region AddHyperEdge(Label, HyperEdgeInitializer, params Vertices)

        /// <summary>
        /// Add a multiedge based on the given multiedge label and
        /// an enumeration of vertices to the graph and initialize
        /// it by invoking the given HyperEdgeInitializer.
        /// </summary>
        /// <param name="Label">The multiedge label.</param>
        /// <param name="HyperEdgeInitializer">A delegate to initialize the newly generated hyperedge.</param>
        /// <param name="Vertices">An enumeration of vertices.</param>
        /// <returns>The new hyperedge</returns>
        IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
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
            
                         params IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)


        {
            return Subgraph.AddHyperEdge(_HyperEdgeIdCreatorDelegate(this), Label, HyperEdgeInitializer, Vertices);
        }

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
        IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            AddHyperEdge(TIdHyperEdge    HyperEdgeId,
                         THyperEdgeLabel Label,

                         HyperEdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeInitializer,
            
                         params IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)


        {

            if (HyperEdgeId == null)
                HyperEdgeId = _HyperEdgeIdCreatorDelegate(this);

            if (_HyperEdgesWhenVertex.ContainsKey(HyperEdgeId))
                throw new ArgumentException("Another hyperedge with id " + HyperEdgeId + " already exists!");

            var _HyperEdge = _HyperEdgeCreatorDelegate(this, Vertices, HyperEdgeId, Label, HyperEdgeInitializer);

            if (SendHyperEdgeAddingVote(_HyperEdge))
            {
                if (_HyperEdgesWhenGraph.TryAddValue(HyperEdgeId, _HyperEdge, _HyperEdge.Label))
                {
                    //OutVertex.AddOutEdge(_HyperEdge);
                    //InVertex.AddInEdge(_HyperEdge);
                    SendHyperEdgeAddedNotification(_HyperEdge);
                    return _HyperEdge;
                }
            }

            return null;

        }

        #endregion

        #region AddHyperEdge(Label, HyperEdgeInitializer, params Vertices)

        /// <summary>
        /// Add a multiedge based on the given multiedge label and
        /// an enumeration of vertices to the graph and initialize
        /// it by invoking the given HyperEdgeInitializer.
        /// </summary>
        /// <param name="OutVertex">The outgoing vertex.</param>
        /// <param name="Label">The multiedge label.</param>
        /// <param name="HyperEdgeInitializer">A delegate to initialize the newly generated hyperedge.</param>
        /// <param name="InVertices">An enumeration of vertices.</param>
        /// <returns>The new hyperedge</returns>
        IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            AddHyperEdge(IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

                         THyperEdgeLabel Label,

                         HyperEdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeInitializer,
            
                         params IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] InVertices)


        {
            var _HyperEdge = Subgraph.AddHyperEdge(_HyperEdgeIdCreatorDelegate(this), Label, HyperEdgeInitializer, InVertices);
            OutVertex.AddHyperEdge(_HyperEdge);
            return _HyperEdge;
        }

        #endregion

        #region AddHyperEdge(IGenericPropertyHyperEdge)

        /// <summary>
        /// Adds the given hyperedge to the graph, and returns it again.
        /// An exception will be thrown if the hyperedge identifier is already being
        /// used by the graph to reference another hyperedge.
        /// </summary>
        /// <param name="IGenericPropertyHyperEdge">An IGenericPropertyHyperEdge.</param>
        /// <returns>The given IGenericPropertyHyperEdge.</returns>
        IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.    
                       AddHyperEdge(IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyHyperEdge)
        {

            #region Initial checks

            if (IGenericPropertyHyperEdge == null)
                throw new ArgumentNullException("The given hyperedge must not be null!");

            if (IGenericPropertyHyperEdge.Id == null || IGenericPropertyHyperEdge.Id.Equals(default(TIdHyperEdge)))
                throw new ArgumentNullException("The unique identification of hyperedge must not be null!");

            if (_HyperEdgesWhenGraph.ContainsKey(IGenericPropertyHyperEdge.Id))
                throw new DuplicateVertexIdException("Another hyperedge with identification " + IGenericPropertyHyperEdge.Id + " already exists!");

            #endregion

            if (SendHyperEdgeAddingVote(IGenericPropertyHyperEdge))
            {
                if (_HyperEdgesWhenGraph.TryAddValue(IGenericPropertyHyperEdge.Id, IGenericPropertyHyperEdge, IGenericPropertyHyperEdge.Label))
                {
                    SendHyperEdgeAddedNotification(IGenericPropertyHyperEdge);
                    return IGenericPropertyHyperEdge;
                }
            }

            return null;

        }

        #endregion


        #region HyperEdgeById(HyperEdgeId)

        /// <summary>
        /// Return the HyperEdge referenced by the given HyperEdge identifier.
        /// If no HyperEdge is referenced by the identifier return null.
        /// </summary>
        /// <param name="HyperEdgeId">A HyperEdge identifier.</param>
        IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                HyperEdgeById(TIdHyperEdge HyperEdgeId)

        {

            #region Initial checks

            if (HyperEdgeId == null)
                throw new ArgumentNullException("HyperEdgeId", "The given hyperedge identifier must not be null!");

            #endregion

            IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _HyperEdge = null;

            if (_HyperEdgesWhenGraph.TryGetByKey(HyperEdgeId, out _HyperEdge))
                return _HyperEdge;

            return null;

        }

        #endregion

        #region HyperEdgesById(params HyperEdgeIds)

        /// <summary>
        /// Return the HyperEdges referenced by the given array of HyperEdge identifiers.
        /// If no HyperEdge is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="HyperEdgeIds">An array of HyperEdge identifiers.</param>
        IEnumerable<IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                HyperEdgesById(params TIdHyperEdge[] HyperEdgeIds)

        {

            #region Initial checks

            if (HyperEdgeIds == null || !HyperEdgeIds.Any())
                throw new ArgumentNullException("HyperEdgeIds", "The array of hyperedge identifiers must not be null or its length zero!");

            #endregion

            IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _HyperEdge;

            foreach (var _HyperEdgeId in HyperEdgeIds)
            {

                if (_HyperEdgeId != null)
                {
                    if (_HyperEdgesWhenGraph.TryGetByKey(_HyperEdgeId, out _HyperEdge))
                        yield return _HyperEdge;
                }

                yield return null;

            }

        }

        #endregion

        #region HyperEdgesByLabel(params HyperEdgeLabels)

        /// <summary>
        /// Return an enumeration of all multiedges having one
        /// of the given multiedge labels.
        /// </summary>
        /// <param name="HyperEdgeLabels">An array of multiedge labels.</param>
        IEnumerable<IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                HyperEdgesByLabel(params THyperEdgeLabel[] HyperEdgeLabels)

        {

            throw new NotImplementedException();

            // This should be optimized in the future!

            //return HyperEdges(HyperEdge =>
            //{

            //    foreach (var HyperEdgeLabel in HyperEdgeLabels)
            //    {
            //        if (HyperEdge.Label != null &&
            //            HyperEdge.Label.Equals(HyperEdgeLabel))
            //            return true;
            //    }

            //    return false;

            //});

        }

        #endregion

        #region HyperEdges(HyperEdgeFilter = null)

        /// <summary>
        /// Return an enumeration of all hyperedges in the graph.
        /// An optional hyperedge filter may be applied for filtering.
        /// </summary>
        /// <param name="HyperEdgeFilter">A delegate for hyperedge filtering.</param>
        IEnumerable<IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
            IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
                HyperEdges(HyperEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter = null)

        {

            throw new NotImplementedException();

            //if (HyperEdgeFilter == null)
            //    return from   HyperEdge
            //           in     _HyperEdges
            //           select HyperEdge;

            //else
            //    return from   Edge
            //           in     _ForeignEdges
            //           where  EdgeFilter(Edge)
            //           select Edge;

        }

        #endregion

        #region NumberOfHyperEdges(HyperEdgeFilter = null)

        /// <summary>
        /// Return the current number of HyperEdges matching the given optional HyperEdge filter.
        /// When the filter is null, this method should implement an optimized
        /// way to get the currenty number of edges.
        /// </summary>
        /// <param name="HyperEdgeFilter">A delegate for HyperEdge filtering.</param>
        UInt64 IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            NumberOfHyperEdges(HyperEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter = null)

        {

            if (HyperEdgeFilter == null)
                return _HyperEdgesWhenVertex.Count();

            else
            {
                lock (this)
                {

                    var _Counter = 0UL;

                    foreach (var _HyperEdge in _HyperEdgesWhenVertex)
                        if (HyperEdgeFilter(_HyperEdge))
                            _Counter++;

                    return _Counter;

                }
            }

        }

        #endregion


        #region RemoveHyperEdgesById(params HyperEdgeIds)

        /// <summary>
        /// Remove the given array of hyperedges identified by their HyperEdgeIds.
        /// </summary>
        /// <param name="HyperEdgeIds">An array of HyperEdgeIds of the hyperedges to remove.</param>
        void IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            RemoveHyperEdgesById(params TIdHyperEdge[] HyperEdgeIds)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region RemoveHyperEdges(params HyperEdges)    // RemoveHyperEdges()!

        /// <summary>
        /// Remove hyperedges.
        /// </summary>
        /// <param name="HyperEdges">An array of outgoing edges to be removed.</param>
        void IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            RemoveHyperEdges(params IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] HyperEdges)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region RemoveHyperEdges(HyperEdgeFilter = null)

        /// <summary>
        /// Remove any outgoing hyperedge matching
        /// the given hyperedge filter delegate.
        /// </summary>
        /// <param name="HyperEdgeFilter">A delegate for hyperedge filtering.</param>
        void IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            RemoveHyperEdges(HyperEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter = null)

        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion


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

        #region Shutdown()

        /// <summary>
        /// Shutdown and close the graph.
        /// </summary>
        /// <param name="Message">An optional message, e.g. a reason for the shutdown.</param>
        void IGenericReadOnlyPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            Shutdown(String Message = "")

        {
            SendGraphShuttingdownNotification(Message);
            //Clear();
            SendGraphShutteddownNotification();
        }

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

        #region IDynamicGraphObject<PropertyVertex> Members

        #region GetMetaObject(myExpression)

        /// <summary>
        /// Return the appropriate DynamicMetaObject.
        /// </summary>
        /// <param name="myExpression">An Expression.</param>
        public DynamicMetaObject GetMetaObject(Expression myExpression)
        {
            return new DynamicGraphElement<GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
                                                                 (myExpression, this);
        }

        #endregion

        #region GetDynamicMemberNames()

        /// <summary>
        /// Returns an enumeration of all property keys.
        /// </summary>
        public virtual IEnumerable<String> GetDynamicMemberNames()
        {
            foreach (var _PropertyKey in PropertyData.Keys)
                yield return _PropertyKey.ToString();
        }

        #endregion


        #region SetMember(myBinder, myObject)

        /// <summary>
        /// Sets a new property or overwrites an existing.
        /// </summary>
        /// <param name="myBinder">The property key</param>
        /// <param name="myObject">The property value</param>
        public virtual Object SetMember(String myBinder, Object myObject)
        {
            return SetProperty((TKeyVertex) (Object) myBinder, (TValueVertex) myObject);
        }

        #endregion

        #region GetMember(myBinder)

        /// <summary>
        /// Returns the value of the given property key.
        /// </summary>
        /// <param name="myBinder">The property key.</param>
        public virtual Object GetMember(String myBinder)
        {
            TValueVertex myObject;
            TryGetProperty((TKeyVertex) (Object) myBinder, out myObject);
            return myObject as Object;
        }

        #endregion

        #region DeleteMember(myBinder)

        /// <summary>
        /// Tries to remove the property identified by the given property key.
        /// </summary>
        /// <param name="myBinder">The property key.</param>
        public virtual Object DeleteMember(String myBinder)
        {

            try
            {
                PropertyData.Remove((TKeyVertex) (Object) myBinder);
                return true;
            }
            catch
            { }

            return false;

        }

        #endregion

        #endregion

        #region IComparable Members

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
                throw new ArgumentNullException("The given IPropertyVertex must not be null!");

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
            return Id.Equals(IGenericPropertyVertex[IdKey]);

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

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return "GenericPropertyVertex [Id: " + Id.ToString() + ", " + _OutEdges.Count() + " OutEdges, " + _InEdges.Count() + " InEdges]";
        }

        #endregion


    }

}
