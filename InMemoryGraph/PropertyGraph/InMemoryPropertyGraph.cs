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
using System.Linq;
using System.Linq.Expressions;
using System.Dynamic;
using System.Collections.Generic;
using System.Collections.Concurrent;

using de.ahzf.Blueprints.Indices;
using de.ahzf.Blueprints.PropertyGraph.Indices;

#endregion

namespace de.ahzf.Blueprints.PropertyGraph.InMemory
{

    #region InMemoryGenericPropertyGraph<...>

    /// <summary>
    /// An in-memory implementation of the IGraph interface.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TKeyVertex">The type of the vertex property keys.</typeparam>
    /// <typeparam name="TValueVertex">The type of the vertex property values.</typeparam>
    /// <typeparam name="TDatastructureVertex">The datastructure for hosting the keyvalue-pairs of the vertices.</typeparam>
    /// <typeparam name="TEdgeCollection">The datastructure for hosting the edges within the vertices.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
    /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
    /// <typeparam name="TDatastructureEdge">The datastructure for hosting the keyvalue-pairs of the edges.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
    /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
    /// <typeparam name="TDatastructureHyperEdge">The datastructure for hosting the keyvalue-pairs of the hyperedges.</typeparam>
    public class InMemoryGenericPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>

                                              : AGraphElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex, TDatastructureVertex>,

                                                IPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>,

                                                IDynamicGraphElement<InMemoryGenericPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>

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

    {

        #region Data

        /// <summary>
        /// The cached number of vertices.
        /// </summary>
        protected UInt64 _NumberOfVertices;

        /// <summary>
        /// The cached number of edges.
        /// </summary>
        protected UInt64 _NumberOfEdges;

        /// <summary>
        /// The cached number of hyperedges.
        /// </summary>
        protected UInt64 _NumberOfHyperEdges;


        // Make IDictionaries more generic??!

        /// <summary>
        /// The collection of vertices.
        /// </summary>
        protected readonly IDictionary<TIdVertex,    IPropertyVertex   <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> _Vertices;

        /// <summary>
        /// The collection of edges.
        /// </summary>
        protected readonly IDictionary<TIdEdge,      IPropertyEdge     <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> _Edges;

        /// <summary>
        /// The collection of hyperedges.
        /// </summary>
        protected readonly IDictionary<TIdHyperEdge, IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> _HyperEdges;


        private readonly VertexIdCreatorDelegate   <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _VertexIdCreatorDelegate;
        private readonly VertexCreatorDelegate     <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _VertexCreatorDelegate;
                                                   
        private readonly EdgeIdCreatorDelegate     <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _EdgeIdCreatorDelegate;
        private readonly EdgeCreatorDelegate       <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _EdgeCreatorDelegate;

        private readonly HyperEdgeIdCreatorDelegate<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _HyperEdgeIdCreatorDelegate;
        private readonly HyperEdgeCreatorDelegate  <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _HyperEdgeCreatorDelegate;


        private readonly IDictionary<String, IPropertyElementIndex<IPropertyVertex   <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> _ManualVerticesIndices;
        private readonly IDictionary<String, IPropertyElementIndex<IPropertyVertex   <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> _AutomaticVerticesIndices;

        private readonly IDictionary<String, IPropertyElementIndex<IPropertyEdge     <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> _ManualEdgesIndices;
        private readonly IDictionary<String, IPropertyElementIndex<IPropertyEdge     <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> _AutomaticEdgesIndices;

        private readonly IDictionary<String, IPropertyElementIndex<IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,      TValueVertex,
                                                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> _ManualHyperEdgesIndices;
        private readonly IDictionary<String, IPropertyElementIndex<IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,      TValueVertex,
                                                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> _AutomaticHyperEdgesIndices;

        #endregion

        #region Events

        #region OnVertexAdding

        /// <summary>
        /// Called whenever a vertex will be added to the property graph.
        /// </summary>
        public event VertexAddingEventHandler<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnVertexAdding;

        #endregion

        #region OnVertexAdded

        /// <summary>
        /// Called whenever a vertex was added to the property graph.
        /// </summary>
        public event VertexAddedEventHandler<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnVertexAdded;

        #endregion


        #region OnEdgeAdding

        /// <summary>
        /// Called whenever an edge will be added to the property graph.
        /// </summary>
        public event EdgeAddingEventHandler<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnEdgeAdding;

        #endregion

        #region OnEdgeAdded

        /// <summary>
        /// Called whenever an edge was added to the property graph.
        /// </summary>
        public event EdgeAddedEventHandler<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnEdgeAdded;

        #endregion


        #region OnGraphShuttingdown

        /// <summary>
        /// Called whenever a property graph will be shutting down.
        /// </summary>
        public event GraphShuttingdownEventHandler<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnGraphShuttingdown;

        #endregion

        #region OnGraphShutteddown

        /// <summary>
        /// Called whenever a property graph was shutted down.
        /// </summary>
        public event GraphShutteddownEventHandler <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnGraphShutteddown;

        #endregion

        #endregion

        #region (internal) Send[...]Notifications(...)

        #region (internal) SendVertexAddingNotification(IPropertyVertex)

        /// <summary>
        /// Notify about a vertex to be added to the graph.
        /// </summary>
        internal Boolean SendVertexAddingNotification(IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IPropertyVertex)
        {

            var _VetoVote = new VetoVote();

            if (OnVertexAdding != null)
                OnVertexAdding(this, IPropertyVertex, _VetoVote);

            return _VetoVote.Result;

        }

        #endregion

        #region (internal) SendVertexAddedNotification(IPropertyVertex)

        /// <summary>
        /// Notify about a vertex to be added to the graph.
        /// </summary>
        internal void SendVertexAddedNotification(IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IPropertyVertex)
        {
            if (OnVertexAdded != null)
                OnVertexAdded(this, IPropertyVertex);
        }

        #endregion


        #region (internal) SendEdgeAddingNotification(IPropertyEdge)

        /// <summary>
        /// Notify about an edge to be added to the graph.
        /// </summary>
        internal Boolean SendEdgeAddingNotification(IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IPropertyEdge)
        {

            var _VetoVote = new VetoVote();

            if (OnEdgeAdding != null)
                OnEdgeAdding(this, IPropertyEdge, _VetoVote);

            return _VetoVote.Result;

        }

        #endregion

        #region (internal) SendEdgeAddedNotification(IPropertyEdge)

        /// <summary>
        /// Notify about an edge to be added to the graph.
        /// </summary>
        internal void SendEdgeAddedNotification(IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IPropertyEdge)
        {
            if (OnEdgeAdded != null)
                OnEdgeAdded(this, IPropertyEdge);
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

        #region Constructor(s)

        #region InMemoryGenericPropertyGraph(VertexCreatorDelegate, EdgeCreatorDelegate, HyperEdgeCreatorDelegate, VerticesCollectionInitializer, EdgesCollectionInitializer, HyperEdgesCollectionInitializer)

        /// <summary>
        /// Created a new genric and in-memory property graph.
        /// </summary>
        /// <param name="GraphId">The identification of this graph.</param>
        /// <param name="IdKey">The key to access the Id of this graph.</param>
        /// <param name="RevisonIdKey">The key to access the Id of this graph.</param>
        /// <param name="DataInitializer"></param>
        /// <param name="VertexIdCreatorDelegate">A delegate for creating a new VertexId (if no one was provided by the user).</param>
        /// <param name="VertexCreatorDelegate">A delegate for creating a new vertex.</param>
        /// <param name="EdgeIdCreatorDelegate">A delegate for creating a new EdgeId (if no one was provided by the user).</param>
        /// <param name="EdgeCreatorDelegate">A delegate for creating a new edge.</param>
        /// <param name="HyperEdgeIdCreatorDelegate">A delegate for creating a new HyperEdgeId (if no one was provided by the user).</param>
        /// <param name="HyperEdgeCreatorDelegate">A delegate for creating a new hyperedge.</param>
        /// <param name="VerticesCollectionInitializer">A delegate for initializing a new vertex with custom data.</param>
        /// <param name="EdgesCollectionInitializer">A delegate for initializing a new edge with custom data.</param>
        /// <param name="HyperEdgesCollectionInitializer">A delegate for initializing a new hyperedge with custom data.</param>
        /// <param name="GraphInitializer">A delegate to initialize the newly created graph.</param>
        public InMemoryGenericPropertyGraph(TIdVertex                    GraphId,
                                            TKeyVertex                   IdKey,
                                            TKeyVertex                   RevisonIdKey,
                                            Func<TDatastructureVertex>   DataInitializer,

                                            VertexIdCreatorDelegate   <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexIdCreatorDelegate,
                                            VertexCreatorDelegate     <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexCreatorDelegate,
                                                                      
                                            EdgeIdCreatorDelegate     <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeIdCreatorDelegate,
                                            EdgeCreatorDelegate       <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeCreatorDelegate,

                                            HyperEdgeIdCreatorDelegate<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeIdCreatorDelegate,
                                            HyperEdgeCreatorDelegate  <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeCreatorDelegate,

                                            // Vertices Collection
                                            IDictionary<TIdVertex,    IPropertyVertex   <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
                                            VerticesCollectionInitializer,

                                            // Edges Collection
                                            IDictionary<TIdEdge,      IPropertyEdge     <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
                                            EdgesCollectionInitializer,

                                            // Hyperedges Collection
                                            IDictionary<TIdHyperEdge, IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
                                            HyperEdgesCollectionInitializer,

                                            GraphInitializer<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GraphInitializer = null)


            : base(GraphId, IdKey, RevisonIdKey, DataInitializer)

        {

            _VertexIdCreatorDelegate    = VertexIdCreatorDelegate;
            _VertexCreatorDelegate      = VertexCreatorDelegate;

            _EdgeIdCreatorDelegate      = EdgeIdCreatorDelegate;
            _EdgeCreatorDelegate        = EdgeCreatorDelegate;

            _HyperEdgeIdCreatorDelegate = HyperEdgeIdCreatorDelegate;
            _HyperEdgeCreatorDelegate   = HyperEdgeCreatorDelegate;

            _Vertices                   = VerticesCollectionInitializer;
            _Edges                      = EdgesCollectionInitializer;
            _HyperEdges                 = HyperEdgesCollectionInitializer;


            _ManualVerticesIndices      = new Dictionary<String, IPropertyElementIndex<IPropertyVertex   <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>();
            _AutomaticVerticesIndices   = new Dictionary<String, IPropertyElementIndex<IPropertyVertex   <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>();

            _ManualEdgesIndices         = new Dictionary<String, IPropertyElementIndex<IPropertyEdge     <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>();
            _AutomaticEdgesIndices      = new Dictionary<String, IPropertyElementIndex<IPropertyEdge     <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>();

            _ManualHyperEdgesIndices    = new Dictionary<String, IPropertyElementIndex<IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>();
            _AutomaticHyperEdgesIndices = new Dictionary<String, IPropertyElementIndex<IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>();

        }

        #endregion

        #endregion


        #region Vertex methods

        #region AddVertex(VertexInitializer = null)

        /// <summary>
        /// Adds a vertex to the graph using the given VertexId and initializes
        /// the vertex by invoking the given vertex initializer.
        /// </summary>
        /// <param name="VertexInitializer">A delegate to initialize the new vertex.</param>
        /// <returns>The new vertex</returns>
        public IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                               AddVertex(VertexInitializer<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexInitializer = null)

        {
            return AddVertex(_VertexIdCreatorDelegate(this), VertexInitializer);
        }

        #endregion

        #region AddVertex(VertexId, VertexInitializer = null)

        /// <summary>
        /// Adds a vertex to the graph using the given VertexId and initializes
        /// the vertex by invoking the given vertex initializer.
        /// </summary>
        /// <param name="VertexId">A VertexId. If none was given a new one will be generated.</param>
        /// <param name="VertexInitializer">A delegate to initialize the new vertex.</param>
        /// <returns>The new vertex</returns>
        public IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                               AddVertex(TIdVertex VertexId,
                                         VertexInitializer<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexInitializer = null)

        {

            if (VertexId == null)
                VertexId = _VertexIdCreatorDelegate(this);

            if (_Vertices.ContainsKey(VertexId))
                throw new DuplicateVertexIdException("Another vertex with id " + VertexId + " already exists!");

            var _Vertex = _VertexCreatorDelegate(this, VertexId, VertexInitializer);

            if (SendVertexAddingNotification(_Vertex))
            {
                _Vertices.Add(VertexId, _Vertex);
                _NumberOfVertices++;
            }

            return _Vertex;

        }

        #endregion

        #region AddVertex(IPropertyVertex)

        /// <summary>
        /// Adds the given vertex to the graph.
        /// Will fail if the Id of the vertex is already present in the graph.
        /// </summary>
        /// <param name="Vertex">A IPropertyVertex.</param>
        /// <returns>The given vertex.</returns>
        public virtual IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
                       AddVertex(IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex)
        {

            if (Vertex == null)
                throw new ArgumentNullException("The given vertex must not be null!");

            if (Vertex.Id == null || Vertex.Id.Equals(default(TIdVertex)))
                throw new ArgumentNullException("The Id of vertex must not be null!");

            if (_Vertices.ContainsKey(Vertex.Id))
                throw new DuplicateVertexIdException("Another vertex with id " + Vertex.Id + " already exists!");

            if (SendVertexAddingNotification(Vertex))
            {
                _Vertices.Add(Vertex.Id, Vertex);
                _NumberOfVertices++;
            }

            return Vertex;

        }

        #endregion


        #region VerticesById(params VertexIds)

        /// <summary>
        /// Return the vertices referenced by the given array of vertex identifiers.
        /// If no vertex is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="VertexIds">An array of vertex identifiers.</param>
        public virtual IEnumerable<IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                                                   VerticesById(params TIdVertex[] VertexIds)

        {

            if (VertexIds == null || !VertexIds.Any())
                throw new ArgumentNullException("The array of vertex identifiers must not be null or its length zero!");

            IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _IVertex;

            foreach (var _VertexId in VertexIds)
            {
                if (_VertexId != null)
                {
                    _Vertices.TryGetValue(_VertexId, out _IVertex);
                    yield return _IVertex;
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
        public virtual IEnumerable<IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
               Vertices(VertexFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexFilter = null)

        {

            if (VertexFilter == null)
                foreach (var _Vertex in _Vertices.Values)
                    yield return _Vertex;

            else
                foreach (var _Vertex in _Vertices.Values)
                    if (VertexFilter(_Vertex))
                        yield return _Vertex;

        }

        #endregion

        #region NumberOfVertices(VertexFilter = null)

        /// <summary>
        /// Return the current number of vertices matching the given optional vertex filter.
        /// When the filter is null, this method should use implement an optimized
        /// way to get the currenty number of vertices.
        /// </summary>
        /// <param name="VertexFilter">A delegate for vertex filtering.</param>
        public UInt64 NumberOfVertices(VertexFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexFilter = null)
        {

            if (VertexFilter == null)
                return _NumberOfVertices;

            else
            {
                lock (this)
                {

                    var _Counter = 0UL;

                    foreach (var _Vertex in _Vertices.Values)
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
        public void RemoveVerticesById(params TIdVertex[] VertexIds)
        {

            if (VertexIds == null || !VertexIds.Any())
                throw new ArgumentNullException("The given array of vertex identifiers must not be null or its length zero!");

            IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _Vertex;

            if (VertexIds.Any())
            {
                foreach (var _VertexId in VertexIds)
                {
                    if (_Vertices.TryGetValue(_VertexId, out _Vertex))
                        RemoveVertices(_Vertex);
                    else
                        throw new ArgumentException("The given vertex identifier '" + _VertexId.ToString() + "' is unknowen!");
                }
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
        public void RemoveVertices(params IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)
        {

            if (Vertices == null || !Vertices.Any())
                throw new ArgumentNullException("The array of vertices must not be null or its length zero!");

            lock (this)
            {

                foreach (var _Vertex in Vertices)
                {
                    if (_Vertices.ContainsKey(_Vertex.Id))
                    {
    
                        var _EdgeList = new List<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>();
    
                        _EdgeList.AddRange(_Vertex.InEdges());
                        _EdgeList.AddRange(_Vertex.OutEdges());
    
                        // removal requires removal from all indices
                        //for (TinkerIndex index : this.indices.values()) {
                        //    index.remove(vertex);
                        //}
    
                        _Vertices.Remove(_Vertex.Id);
                        _NumberOfVertices--;
    
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
        public void RemoveVertices(VertexFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexFilter)
        {

            lock (this)
            {

                var _tmp = new List<IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>();

                if (VertexFilter == null)
                    foreach (var _Vertex in _Vertices.Values)
                        _tmp.Add(_Vertex);

                else    
                    foreach (var _Vertex in _Vertices.Values)
                        if (VertexFilter(_Vertex))
                            _tmp.Add(_Vertex);

                foreach (var _Vertex in _tmp)
                    RemoveVertices(_Vertex);

            }

        }

        #endregion

        #endregion

        #region Edge methods

        #region AddEdge(OutVertex, InVertex, Label = default, EdgeInitializer = null)

        /// <summary>
        /// Add an edge to the graph. The added edge requires a tail vertex,
        /// a head vertex, an identifier, a label and initializes the edge
        /// by invoking the given EdgeInitializer.
        /// </summary>
        /// <param name="OutVertex"></param>
        /// <param name="InVertex"></param>
        /// <param name="Label"></param>
        /// <param name="EdgeInitializer">A delegate to initialize the newly generated edge.</param>
        /// <returns>The new edge</returns>
        public IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
                             AddEdge(IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

                                     IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

                                     TEdgeLabel      Label  = default(TEdgeLabel),

                                     EdgeInitializer<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer = null)


        {
            return AddEdge(OutVertex, InVertex, _EdgeIdCreatorDelegate(this), Label, EdgeInitializer);
        }

        #endregion

        #region AddEdge(OutVertex, InVertex, EdgeId, Label = default, EdgeInitializer = null)

        /// <summary>
        /// Add an edge to the graph. The added edge requires a tail vertex,
        /// a head vertex, an identifier, a label and initializes the edge
        /// by invoking the given EdgeInitializer.
        /// </summary>
        /// <param name="OutVertex"></param>
        /// <param name="InVertex"></param>
        /// <param name="EdgeId">A EdgeId. If none was given a new one will be generated.</param>
        /// <param name="Label"></param>
        /// <param name="EdgeInitializer">A delegate to initialize the newly generated edge.</param>
        /// <returns>The new edge</returns>
        public IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
                             AddEdge(IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

                                     IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

                                     TIdEdge         EdgeId,
                                     TEdgeLabel      Label  = default(TEdgeLabel),

                                     EdgeInitializer<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer = null)


        {

            if (EdgeId == null)
                EdgeId = _EdgeIdCreatorDelegate(this);

            if (_Edges.ContainsKey(EdgeId))
                throw new ArgumentException("Another edge with id " + EdgeId + " already exists!");

            var _Edge = _EdgeCreatorDelegate(this, OutVertex, InVertex, EdgeId, Label, EdgeInitializer);

            if (SendEdgeAddingNotification(_Edge))
            {
                _Edges.Add(EdgeId, _Edge);
                _NumberOfEdges++;
                OutVertex.AddOutEdge(_Edge);
                InVertex.AddInEdge(_Edge);
            }

            return _Edge;

        }

        #endregion


        #region EdgesById(params EdgeIds)

        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// An additional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="EdgeIds">An array of edge identifiers.</param>
        public IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            EdgesById(params TIdEdge[] EdgeIds)
        {

            if (EdgeIds == null || !EdgeIds.Any())
                throw new ArgumentNullException("The given array of edge identifiers must not be null or its length zero!");

            IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _Edge;

            foreach (var _EdgeId in EdgeIds)
            {
                if (_EdgeId != null)
                {
                    _Edges.TryGetValue(_EdgeId, out _Edge);
                    yield return _Edge;
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
        public IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                 Edges(EdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter = null)
        {

            if (EdgeFilter == null)
                foreach (var _Edge in _Edges.Values)
                    yield return _Edge;

            else foreach (var _Edge in _Edges.Values)
                if (EdgeFilter(_Edge))
                    yield return _Edge;

        }

        #endregion

        #region NumberOfEdges(EdgeFilter = null)

        /// <summary>
        /// Return the current number of edges matching the given optional edge filter.
        /// When the filter is null, this method should use implement an optimized
        /// way to get the currenty number of edges.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        public UInt64 NumberOfEdges(EdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter = null)
        {

            if (EdgeFilter == null)
                return _NumberOfEdges;

            else
            {
                lock (this)
                {

                    var _Counter = 0UL;

                    foreach (var _Edge in _Edges.Values)
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
        public void RemoveEdgesById(params TIdEdge[] EdgeIds)
        {

            if (EdgeIds == null || !EdgeIds.Any())
                throw new ArgumentNullException("The given array of edge identifiers must not be null or its length zero!");

            IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _Edge;

            if (EdgeIds.Any())
            {
                foreach (var _EdgeId in EdgeIds)
                {
                    if (_Edges.TryGetValue(_EdgeId, out _Edge))
                        RemoveEdges(_Edge);
                    else
                        throw new ArgumentException("The given edge identifier '" + _EdgeId.ToString() + "' is unknowen!");
                }
            }

        }

        #endregion

        #region RemoveEdges(params Edges)

        /// <summary>
        /// Remove the given array of edges from the graph.
        /// </summary>
        /// <param name="Edges">An array of edges to be removed from the graph.</param>
        public void RemoveEdges(params IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Edges)
        {

            if (Edges == null || !Edges.Any())
                throw new ArgumentNullException("The given array of edges must not be null or its length zero!");

            lock (this)
            {

                foreach (var _Edge in Edges)
                {
                    if (_Edges.ContainsKey(_Edge.Id))
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

                        _Edges.Remove(_Edge.Id);
                        _NumberOfEdges--;

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
        public void RemoveEdges(EdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)
        {

            lock (this)
            {

                var _tmp = new List<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>();

                if (EdgeFilter == null)
                    foreach (var _IEdge in _Edges.Values)
                        _tmp.Add(_IEdge);

                else
                    foreach (var _IEdge in _Edges.Values)
                        if (EdgeFilter(_IEdge))
                            _tmp.Add(_IEdge);

                foreach (var _IEdge in _tmp)
                    RemoveEdges(_IEdge);

            }

        }

        #endregion

        #endregion

        #region HyperEdge methods

        #region HyperEdgesById(params HyperEdgeIds)

        /// <summary>
        /// Return the HyperEdges referenced by the given array of HyperEdge identifiers.
        /// If no HyperEdge is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="HyperEdgeIds">An array of HyperEdge identifiers.</param>
        public IEnumerable<IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                                              HyperEdgesById(params TIdHyperEdge[] HyperEdgeIds)
        {
            
            if (HyperEdgeIds == null || !HyperEdgeIds.Any())
                throw new ArgumentNullException("The given array of HyperEdge identifiers must not be null or its length zero!");

            IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _HyperEdge;

            foreach (var _HyperEdgeId in HyperEdgeIds)
            {
                if (_HyperEdgeId != null)
                {
                    _HyperEdges.TryGetValue(_HyperEdgeId, out _HyperEdge);
                    yield return _HyperEdge;
                }
            }

        }

        #endregion

        #region HyperEdges(HyperEdgeFilter = null)

        /// <summary>
        /// Get an enumeration of all HyperEdges in the graph.
        /// An optional HyperEdge filter may be applied for filtering.
        /// </summary>
        /// <param name="HyperEdgeFilter">A delegate for HyperEdge filtering.</param>
        public IEnumerable<IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                                              HyperEdges(HyperEdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter = null)
        {

            if (HyperEdgeFilter == null)
                foreach (var _HyperEdge in _HyperEdges.Values)
                    yield return _HyperEdge;

            else
                foreach (var _HyperEdge in _HyperEdges.Values)
                    if (HyperEdgeFilter(_HyperEdge))
                        yield return _HyperEdge;

        }

        #endregion

        #region NumberOfHyperEdges(HyperEdgeFilter = null)

        /// <summary>
        /// Return the current number of HyperEdges matching the given optional HyperEdge filter.
        /// When the filter is null, this method should implement an optimized
        /// way to get the currenty number of edges.
        /// </summary>
        /// <param name="HyperEdgeFilter">A delegate for HyperEdge filtering.</param>
        public UInt64 NumberOfHyperEdges(HyperEdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter = null)
        {

            if (HyperEdgeFilter == null)
                return _NumberOfHyperEdges;

            else
            {
                lock (this)
                {

                    var _Counter = 0UL;

                    foreach (var _HyperEdge in _HyperEdges.Values)
                        if (HyperEdgeFilter(_HyperEdge))
                            _Counter++;

                    return _Counter;

                }
            }

        }

        #endregion

        #endregion


        #region GraphIndexing

        #region (private) ActiveIndex(IndexClassName)

        private IIndex<TIndexKey, TIndexValue>

                ActiveIndex<TIndexKey, TIndexValue>(String IndexClassName)

            where TIndexKey   : IEquatable<TIndexKey>,   IComparable<TIndexKey>,   IComparable
            where TIndexValue : IEquatable<TIndexValue>, IComparable<TIndexValue>, IComparable

        {

            Type IndexType = null;
            //if (IndexDatastructure == "DictionaryIndex")
            //    IndexType = typeof(DictionaryIndex<,>);
            //var IndexType = IndexDatastructure.GetType().GetGenericTypeDefinition();

            var myAssembly = this.GetType().Assembly;

            if (IndexClassName.IndexOf('.') < 0)
                IndexType = myAssembly.GetType(this.GetType().Namespace + "." + IndexClassName + "`2");
            else
                IndexType = myAssembly.GetType(IndexClassName + "`2");


            // Check if the index type implements the ILookup interface
            if (!IndexType.FindInterfaces((type, o) => { if (type == typeof(IIndex)) return true; else return false; }, null).Any())
                throw new ArgumentException("The given class does not implement the ILookup interface!");

            var typeArgs = new Type[] { typeof(TIndexKey), typeof(TIndexValue) };
            
            Type constructed = IndexType.MakeGenericType(typeArgs);

            var Index = Activator.CreateInstance(constructed) as IIndex<TIndexKey, TIndexValue>;

            if (Index == null)
                throw new ArgumentException("The given class does not implement the ILookup<TIndexKey, TIndexValue> interface!");

            return Index;
        
        }

        #endregion


        #region CreateVerticesIndex<TIndexKey>(Name, IndexClassName, Transformation, Selector = null, AutomaticIndex = false)

        /// <summary>
        /// Generate an index for vertex lookups.
        /// </summary>
        /// <typeparam name="TIndexKey">The type of the index keys.</typeparam>
        /// <param name="Name">A human-friendly name for the index.</param>
        /// <param name="IndexClassName">The class name of a datastructure maintaining the index.</param>
        /// <param name="Transformation">A delegate for transforming a vertex into an index key.</param>
        /// <param name="Selector">A delegate for deciding if a vertex should be indexed or not.</param>
        /// <param name="IsAutomaticIndex">Should this index be maintained by the database or by the user?</param>
        /// <returns>A new index data structure.</returns>
        public IPropertyElementIndex<TIndexKey, IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
               CreateVerticesIndex<TIndexKey>(String Name,                                              
                                              String IndexClassName,
                                              IndexTransformation<TIndexKey,
                                                                  IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Transformation,
                                              IndexSelector      <TIndexKey,
                                                                  IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Selector = null,
                                              Boolean IsAutomaticIndex = false)

            where TIndexKey : IEquatable<TIndexKey>, IComparable<TIndexKey>, IComparable

        {

            var IndexDatastructure = ActiveIndex<TIndexKey, IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>(IndexClassName);

            var _NewIndex = new PropertyVertexIndex<TIndexKey,
                                                    TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,  
                                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,    
                                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                                   (Name, IndexDatastructure, Transformation, Selector, IsAutomaticIndex);

            if (IsAutomaticIndex)
                _AutomaticVerticesIndices.Add(_NewIndex.Name, _NewIndex);
            else
                _ManualVerticesIndices.Add(_NewIndex.Name, _NewIndex);

            return _NewIndex;

        }

        #endregion

        #region CreateEdgesIndex<TIndexKey>(Name, IndexClassName, Transformation, Selector = null, IsAutomaticIndex = false)

        /// <summary>
        /// Generate an index for edge lookups.
        /// </summary>
        /// <typeparam name="TIndexKey">The type of the index keys.</typeparam>
        /// <param name="Name">A human-friendly name for the index.</param>
        /// <param name="IndexClassName">The class name of a datastructure maintaining the index.</param>
        /// <param name="Transformation">A delegate for transforming a vertex into an index key.</param>
        /// <param name="Selector">A delegate for deciding if a vertex should be indexed or not.</param>
        /// <param name="IsAutomaticIndex">Should this index be maintained by the database or by the user?</param>
        /// <returns>A new index data structure.</returns>
        public IPropertyElementIndex<TIndexKey, IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
               CreateEdgesIndex<TIndexKey>(String Name,                                              
                                           String IndexClassName,
                                           IndexTransformation<TIndexKey,
                                                               IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Transformation,
                                           IndexSelector      <TIndexKey,
                                                               IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Selector = null,
                                           Boolean IsAutomaticIndex = false)

            where TIndexKey : IEquatable<TIndexKey>, IComparable<TIndexKey>, IComparable

        {

            var IndexDatastructure = ActiveIndex<TIndexKey, IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>(IndexClassName);

            var _NewIndex = new PropertyEdgeIndex<TIndexKey,
                                                  TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,  
                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,    
                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                                 (Name, IndexDatastructure, Transformation, Selector, IsAutomaticIndex);

            if (IsAutomaticIndex)
                _AutomaticEdgesIndices.Add(_NewIndex.Name, _NewIndex);
            else
                _ManualEdgesIndices.Add(_NewIndex.Name, _NewIndex);

            return _NewIndex;

        }

        #endregion

        #region CreateHyperEdgesIndex<TIndexKey>(Name, IndexClassName, Transformation, Selector = null, IsAutomaticIndex = false)

        /// <summary>
        /// Generate an index for hyperedge lookups.
        /// </summary>
        /// <typeparam name="TIndexKey">The type of the index keys.</typeparam>
        /// <param name="Name">A human-friendly name for the index.</param>
        /// <param name="IndexClassName">The class name of a datastructure maintaining the index.</param>
        /// <param name="Transformation">A delegate for transforming a vertex into an index key.</param>
        /// <param name="Selector">A delegate for deciding if a vertex should be indexed or not.</param>
        /// <param name="IsAutomaticIndex">Should this index be maintained by the database or by the user?</param>
        /// <returns>A new index data structure.</returns>
        public IPropertyElementIndex<TIndexKey, IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>







               CreateHyperEdgesIndex<TIndexKey>(String Name,
                                                String IndexClassName,
                                                IndexTransformation   <TIndexKey,
                                                                       IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Transformation,
                                                IndexSelector         <TIndexKey,
                                                                       IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Selector = null,
                                                Boolean IsAutomaticIndex = false)

            where TIndexKey : IEquatable<TIndexKey>, IComparable<TIndexKey>, IComparable

        {

            var IndexDatastructure = ActiveIndex<TIndexKey, IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>(IndexClassName);

            var _NewIndex = new PropertyHyperEdgeIndex<TIndexKey,
                                                       TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,  
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,    
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                                      (Name, IndexDatastructure, Transformation, Selector, IsAutomaticIndex);

            if (IsAutomaticIndex)
                _AutomaticHyperEdgesIndices.Add(_NewIndex.Name, _NewIndex);
            else
                _ManualHyperEdgesIndices.Add(_NewIndex.Name, _NewIndex);

            return _NewIndex;

        }

        #endregion


        #region VerticesIndices(IndexFilter = null)

        /// <summary>
        /// Get all vertices indices maintained by the graph.
        /// </summary>
        /// <param name="IndexFilter">An optional index filter.</param>
        /// <returns>The indices associated with the graph.</returns>
        public IEnumerable<IPropertyElementIndex<IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>

               VerticesIndices(IndexFilter<IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> IndexFilter = null)

        {
            
            if (IndexFilter == null)
            {
                foreach (var _Index in _AutomaticVerticesIndices.Values)
                    yield return _Index;
                foreach (var _Index in _ManualVerticesIndices.Values)
                    yield return _Index;
            }

            else
            {
                foreach (var _Index in _AutomaticVerticesIndices.Values)
                    if (IndexFilter(_Index))
                        yield return _Index;
                foreach (var _Index in _ManualVerticesIndices.Values)
                    if (IndexFilter(_Index))
                        yield return _Index;
            }

        }

        #endregion

        #region VerticesIndices<T>(IndexFilter = null)

        /// <summary>
        /// Get all vertices indices maintained by the graph.
        /// </summary>
        /// <typeparam name="T">The type of the elements to be indexed.</typeparam>
        /// <param name="IndexFilter">An optional index filter.</param>
        /// <returns>The indices associated with the graph.</returns>
        public IEnumerable<IPropertyElementIndex<T>> VerticesIndices<T>(IndexFilter<T> IndexFilter = null)
            where T : IEquatable<T>, IComparable<T>, IComparable
        {
            throw new NotImplementedException();
        }

        #endregion

        #region EdgesIndices(IndexFilter = null)

        /// <summary>
        /// Get all edges indices maintained by the graph.
        /// </summary>
        /// <param name="IndexFilter">An optional index filter.</param>
        /// <returns>The indices associated with the graph.</returns>
        public IEnumerable<IPropertyElementIndex<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>

               EdgesIndices(IndexFilter<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> IndexFilter = null)

        {

            if (IndexFilter == null)
            {
                foreach (var _Index in _AutomaticEdgesIndices.Values)
                    yield return _Index;
                foreach (var _Index in _ManualEdgesIndices.Values)
                    yield return _Index;
            }

            else
            {
                foreach (var _Index in _AutomaticEdgesIndices.Values)
                    if (IndexFilter(_Index))
                        yield return _Index;
                foreach (var _Index in _ManualEdgesIndices.Values)
                    if (IndexFilter(_Index))
                        yield return _Index;
            }

        }

        #endregion

        #region EdgesIndices<T>(IndexFilter = null)

        /// <summary>
        /// Get all edges indices maintained by the graph.
        /// </summary>
        /// <typeparam name="T">The type of the elements to be indexed.</typeparam>
        /// <param name="IndexFilter">An optional index filter.</param>
        /// <returns>The indices associated with the graph.</returns>
        public IEnumerable<IPropertyElementIndex<T>> EdgesIndices<T>(IndexFilter<T> IndexFilter = null)
            where T : IEquatable<T>, IComparable<T>, IComparable
        {
            throw new NotImplementedException();
        }

        #endregion

        #region HyperEdgesIndices(IndexFilter = null)

        /// <summary>
        /// Get all hyperedges indices maintained by the graph.
        /// </summary>
        /// <param name="IndexFilter">An optional index filter.</param>
        /// <returns>The indices associated with the graph.</returns>
        public IEnumerable<IPropertyElementIndex<IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>

            HyperEdgesIndices(IndexFilter<IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> IndexFilter = null)

        {

            if (IndexFilter == null)
            {
                foreach (var _Index in _AutomaticHyperEdgesIndices.Values)
                    yield return _Index;
                foreach (var _Index in _ManualHyperEdgesIndices.Values)
                    yield return _Index;
            }

            else
            {
                foreach (var _Index in _AutomaticHyperEdgesIndices.Values)
                    if (IndexFilter(_Index))
                        yield return _Index;
                foreach (var _Index in _ManualHyperEdgesIndices.Values)
                    if (IndexFilter(_Index))
                        yield return _Index;
            }

        }

        #endregion

        #region HyperEdgesIndices<T>(IndexFilter = null)

        /// <summary>
        /// Get all hyperedges indices maintained by the graph.
        /// </summary>
        /// <typeparam name="T">The type of the elements to be indexed.</typeparam>
        /// <param name="IndexFilter">An optional index filter.</param>
        /// <returns>The indices associated with the graph.</returns>
        public IEnumerable<IPropertyElementIndex<T>> HyperEdgesIndices<T>(IndexFilter<T> IndexFilter = null)
            where T : IEquatable<T>, IComparable<T>, IComparable
        {
            throw new NotImplementedException();
        }

        #endregion


        #region DropVerticesIndex(Name)

        /// <summary>
        /// Remove a vertices index associated with the graph.
        /// </summary>
        /// <param name="Name">The name of the index to drop.</param>
        public void DropVerticesIndex(String Name)
        {

            lock (_AutomaticVerticesIndices)
            {
                if (_AutomaticVerticesIndices.ContainsKey(Name))
                    _AutomaticVerticesIndices.Remove(Name);
            }

            lock (_ManualVerticesIndices)
            {
                if (_ManualVerticesIndices.ContainsKey(Name))
                    _ManualVerticesIndices.Remove(Name);
            }

        }

        #endregion

        #region DropEdgesIndex(Name)

        /// <summary>
        /// Remove an edges index associated with the graph.
        /// </summary>
        /// <param name="Name">The name of the index to drop.</param>
        public void DropEdgesIndex(String Name)
        {

            lock (_AutomaticEdgesIndices)
            {
                if (_AutomaticEdgesIndices.ContainsKey(Name))
                    _AutomaticEdgesIndices.Remove(Name);
            }

            lock (_ManualEdgesIndices)
            {
                if (_ManualEdgesIndices.ContainsKey(Name))
                    _ManualEdgesIndices.Remove(Name);
            }

        }


        #endregion

        #region DropHyperEdgesIndex(Name)

        /// <summary>
        /// Remove a hyperedges index associated with the graph.
        /// </summary>
        /// <param name="Name">The name of the index to drop.</param>
        public void DropHyperEdgesIndex(String Name)
        {

            lock (_AutomaticHyperEdgesIndices)
            {
                if (_AutomaticHyperEdgesIndices.ContainsKey(Name))
                    _AutomaticHyperEdgesIndices.Remove(Name);
            }

            lock (_ManualHyperEdgesIndices)
            {
                if (_ManualHyperEdgesIndices.ContainsKey(Name))
                    _ManualHyperEdgesIndices.Remove(Name);
            }

        }

        #endregion


        #region DropVerticesIndices(IndexNameEvaluator = null)

        /// <summary>
        /// Remove vertices indices associated with the graph.
        /// </summary>
        /// <param name="IndexNameEvaluator">A delegate evaluating the indices to drop.</param>
        public void DropVerticesIndex(IndexNameFilter IndexNameEvaluator = null)
        {

            lock (_AutomaticVerticesIndices)
            {

                if (IndexNameEvaluator == null)
                    _AutomaticEdgesIndices.Clear();

                else
                {
                    
                    var _RemoveList = new List<String>();
                    
                    foreach (var _IndexName in _AutomaticVerticesIndices.Keys)
                        if (IndexNameEvaluator(_IndexName))
                            _RemoveList.Add(_IndexName);

                    foreach (var _IndexName in _RemoveList)
                        _AutomaticVerticesIndices.Remove(_IndexName);

                }

            }

            lock (_ManualVerticesIndices)
            {

                if (IndexNameEvaluator == null)
                    _ManualVerticesIndices.Clear();

                else
                {

                    var _RemoveList = new List<String>();

                    foreach (var _IndexName in _ManualVerticesIndices.Keys)
                        if (IndexNameEvaluator(_IndexName))
                            _RemoveList.Add(_IndexName);

                    foreach (var _IndexName in _RemoveList)
                        _ManualVerticesIndices.Remove(_IndexName);

                }

            }

        }

        #endregion

        #region DropEdgesIndices(IndexNameEvaluator = null)

        /// <summary>
        /// Remove edges indices associated with the graph.
        /// </summary>
        /// <param name="IndexNameEvaluator">A delegate evaluating the indices to drop.</param>
        public void DropEdgesIndex(IndexNameFilter IndexNameEvaluator = null)
        {

            lock (_AutomaticEdgesIndices)
            {

                if (IndexNameEvaluator == null)
                    _AutomaticEdgesIndices.Clear();

                else
                {

                    var _RemoveList = new List<String>();

                    foreach (var _IndexName in _AutomaticEdgesIndices.Keys)
                        if (IndexNameEvaluator(_IndexName))
                            _RemoveList.Add(_IndexName);

                    foreach (var _IndexName in _RemoveList)
                        _AutomaticEdgesIndices.Remove(_IndexName);

                }

            }

            lock (_ManualEdgesIndices)
            {

                if (IndexNameEvaluator == null)
                    _ManualEdgesIndices.Clear();

                else
                {

                    var _RemoveList = new List<String>();

                    foreach (var _IndexName in _ManualEdgesIndices.Keys)
                        if (IndexNameEvaluator(_IndexName))
                            _RemoveList.Add(_IndexName);

                    foreach (var _IndexName in _RemoveList)
                        _ManualEdgesIndices.Remove(_IndexName);

                }

            }

        }


        #endregion

        #region DropHyperEdgesIndices(IndexNameEvaluator = null)

        /// <summary>
        /// Remove hyperedge indices associated with the graph.
        /// </summary>
        /// <param name="IndexNameEvaluator">A delegate evaluating the indices to drop.</param>
        public void DropHyperEdgesIndex(IndexNameFilter IndexNameEvaluator = null)
        {

            lock (_AutomaticHyperEdgesIndices)
            {

                if (IndexNameEvaluator == null)
                    _AutomaticHyperEdgesIndices.Clear();

                else
                {

                    var _RemoveList = new List<String>();

                    foreach (var _IndexName in _AutomaticHyperEdgesIndices.Keys)
                        if (IndexNameEvaluator(_IndexName))
                            _RemoveList.Add(_IndexName);

                    foreach (var _IndexName in _RemoveList)
                        _AutomaticHyperEdgesIndices.Remove(_IndexName);

                }

            }

            lock (_ManualHyperEdgesIndices)
            {

                if (IndexNameEvaluator == null)
                    _ManualHyperEdgesIndices.Clear();

                else
                {

                    var _RemoveList = new List<String>();

                    foreach (var _IndexName in _ManualHyperEdgesIndices.Keys)
                        if (IndexNameEvaluator(_IndexName))
                            _RemoveList.Add(_IndexName);

                    foreach (var _IndexName in _RemoveList)
                        _ManualHyperEdgesIndices.Remove(_IndexName);

                }

            }

        }

        #endregion

        #endregion


        #region Clear()

        /// <summary>
        /// Removes all the vertices, edges and hyperedges from the graph.
        /// </summary>
        public void Clear()
        {
            _Vertices.Clear();
            _Edges.Clear();
            _HyperEdges.Clear();
        }

        #endregion

        #region Shutdown()

        /// <summary>
        /// Shutdown and close the graph.
        /// </summary>
        /// <param name="Message">An optional message, e.g. a reason for the shutdown.</param>
        public void Shutdown(String Message = "")
        {
            SendGraphShuttingdownNotification(Message);
            Clear();
            SendGraphShutteddownNotification();
        }

        #endregion


        #region Operator overloading

        #region Operator == (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two property graphs for equality.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public static Boolean operator == (InMemoryGenericPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                                        PropertyGraph1,
                                           InMemoryGenericPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                                        PropertyGraph2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(PropertyGraph1, PropertyGraph2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) PropertyGraph1 == null) || ((Object) PropertyGraph2 == null))
                return false;

            return PropertyGraph1.Equals(PropertyGraph2);

        }

        #endregion

        #region Operator != (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two property graphs for inequality.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>False if both match; True otherwise.</returns>
        public static Boolean operator != (InMemoryGenericPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                                        PropertyGraph1,
                                           InMemoryGenericPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                                        PropertyGraph2)
        {
            return !(PropertyGraph1 == PropertyGraph2);
        }

        #endregion

        #region Operator <  (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (InMemoryGenericPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                                       PropertyGraph1,
                                          InMemoryGenericPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                                       PropertyGraph2)
        {

            if ((Object) PropertyGraph1 == null)
                throw new ArgumentNullException("The given PropertyGraph1 must not be null!");

            if ((Object) PropertyGraph2 == null)
                throw new ArgumentNullException("The given PropertyGraph2 must not be null!");

            return PropertyGraph1.CompareTo(PropertyGraph2) < 0;

        }

        #endregion

        #region Operator <= (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (InMemoryGenericPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                                        PropertyGraph1,
                                           InMemoryGenericPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                                        PropertyGraph2)
        {
            return !(PropertyGraph1 > PropertyGraph2);
        }

        #endregion

        #region Operator >  (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (InMemoryGenericPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                                       PropertyGraph1,
                                          InMemoryGenericPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                                       PropertyGraph2)
        {

            if ((Object) PropertyGraph1 == null)
                throw new ArgumentNullException("The given PropertyGraph1 must not be null!");

            if ((Object) PropertyGraph2 == null)
                throw new ArgumentNullException("The given PropertyGraph2 must not be null!");

            return PropertyGraph1.CompareTo(PropertyGraph2) > 0;

        }

        #endregion

        #region Operator >= (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (InMemoryGenericPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                                        PropertyGraph1,
                                           InMemoryGenericPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                                        PropertyGraph2)
        {
            return !(PropertyGraph1 < PropertyGraph2);
        }

        #endregion

        #endregion

        #region IDynamicGraphObject<InMemoryGenericPropertyGraph> Members

        #region GetMetaObject(myExpression)

        /// <summary>
        /// Return the appropriate DynamicMetaObject.
        /// </summary>
        /// <param name="myExpression">An Expression.</param>
        public DynamicMetaObject GetMetaObject(Expression myExpression)
        {
            return new DynamicGraphElement<InMemoryGenericPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>(myExpression, this);
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
            return PropertyData.SetProperty((TKeyVertex) (Object) myBinder, (TValueVertex) myObject);
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
            PropertyData.GetProperty((TKeyVertex) (Object) myBinder, out myObject);
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

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public Int32 CompareTo(Object Object)
        {

            if ((Object) Object == null)
                throw new ArgumentNullException("The given Object must not be null!");

            return CompareTo((TIdVertex) Object);

        }

        #endregion

        #region CompareTo(VertexId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VertexId">An object to compare with.</param>
        public Int32 CompareTo(TIdVertex VertexId)
        {

            if ((Object) VertexId == null)
                throw new ArgumentNullException("The given VertexId must not be null!");

            return Id.CompareTo(VertexId);

        }

        #endregion

        #region CompareTo(IReadOnlyGraphElement)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="IReadOnlyGraphElement">An object to compare with.</param>
        public Int32 CompareTo(IReadOnlyGraphElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex> IReadOnlyGraphElement)
        {

            if ((Object) IReadOnlyGraphElement == null)
                throw new ArgumentNullException("The given IReadOnlyGraphElement must not be null!");

            return Id.CompareTo(IReadOnlyGraphElement.PropertyData[IdKey]);

        }

        #endregion

        #region CompareTo(IGraphElement)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="IGraphElement">An object to compare with.</param>
        public Int32 CompareTo(IGraphElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex> IGraphElement)
        {

            if ((Object) IGraphElement == null)
                throw new ArgumentNullException("The given IGraphElement must not be null!");

            return Id.CompareTo(IGraphElement.PropertyData[IdKey]);

        }

        #endregion

        #region CompareTo(IReadOnlyPropertyGraph)

        /// <summary>
        /// Compares two read-only property graphs.
        /// </summary>
        /// <param name="IReadOnlyPropertyGraph">A read-only property graph to compare with.</param>
        public Int32 CompareTo(IReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IReadOnlyPropertyGraph)
        {

            if ((Object) IReadOnlyPropertyGraph == null)
                throw new ArgumentNullException("The given IReadOnlyPropertyGraph must not be null!");

            return Id.CompareTo(IReadOnlyPropertyGraph.PropertyData[IdKey]);

        }

        #endregion

        #region CompareTo(IPropertyGraph)

        /// <summary>
        /// Compares two property graphs.
        /// </summary>
        /// <param name="IPropertyGraph">A property graph to compare with.</param>
        public Int32 CompareTo(IPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IPropertyGraph)
        {
            
            if ((Object) IPropertyGraph == null)
                throw new ArgumentNullException("The given IPropertyGraph must not be null!");

            return Id.CompareTo(IPropertyGraph.PropertyData[IdKey]);

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

            // Check if the given object can be casted to a InMemoryGenericPropertyGraph
            var PropertyGraph = Object as InMemoryGenericPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>;
            if ((Object) PropertyGraph == null)
                return false;

            return this.Equals(PropertyGraph);

        }

        #endregion

        #region Equals(TIdVertex VertexId)

        /// <summary>
        /// Compares the identification of the property graph with another vertex id.
        /// </summary>
        /// <param name="VertexId">A vertex identification to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(TIdVertex VertexId)
        {

            if ((Object)VertexId == null)
                return false;

            return Id.Equals(VertexId);

        }

        #endregion

        #region Equals(IReadOnlyGraphElement)

        /// <summary>
        /// Compares this property graph to another graph element.
        /// </summary>
        /// <param name="IReadOnlyGraphElement">An object to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IReadOnlyGraphElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex> IReadOnlyGraphElement)
        {

            if ((Object) IReadOnlyGraphElement == null)
                return false;

            return Id.Equals(IReadOnlyGraphElement.PropertyData[IdKey]);

        }

        #endregion

        #region Equals(IGraphElement)

        /// <summary>
        /// Compares this property graph to another graph element.
        /// </summary>
        /// <param name="IGraphElement">An object to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IGraphElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex> IGraphElement)
        {

            if ((Object) IGraphElement == null)
                return false;

            return Id.Equals(IGraphElement.PropertyData[IdKey]);

        }

        #endregion

        #region Equals(IReadOnlyPropertyGraph)

        /// <summary>
        /// Compares two property graphs for equality.
        /// </summary>
        /// <param name="IPropertyGraph">A property graph to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IReadOnlyPropertyGraph)
        {

            if ((Object) IReadOnlyPropertyGraph == null)
                return false;

            return Id.Equals(IReadOnlyPropertyGraph.PropertyData[IdKey]);

        }

        #endregion

        #region Equals(IPropertyGraph)

        /// <summary>
        /// Compares two property graphs for equality.
        /// </summary>
        /// <param name="IPropertyGraph">A property graph to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IPropertyGraph)
        {
            
            if ((Object) IPropertyGraph == null)
                return false;

            return Id.Equals(IPropertyGraph.PropertyData[IdKey]);

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
            return "PropertyGraph [Id: " + Id.ToString() + ", " + _Vertices.Count() + " Vertices, " + _Edges.Count() + " Edges]";
        }

        #endregion

    }

    #endregion

    #region InMemoryPropertyGraph

    /// <summary>
    /// An in-memory implementation of a property graph.
    /// </summary>
    public class InMemoryPropertyGraph : InMemoryGenericPropertyGraph<// Vertices definition
                                                                      VertexId,    RevisionId,         String, Object, IDictionary<String, Object>,
                                                                      ICollection<IPropertyEdge<VertexId,    RevisionId,         String, Object,
                                                                                                EdgeId,      RevisionId, String, String, Object,
                                                                                                HyperEdgeId, RevisionId, String, String, Object>>,

                                                                      // Edges definition
                                                                      EdgeId,      RevisionId, String, String, Object, IDictionary<String, Object>,

                                                                      // Hyperedges definition
                                                                      HyperEdgeId, RevisionId, String, String, Object, IDictionary<String, Object>>,
                                        IPropertyGraph
    {

        #region Data

        private const String _VertexIdKey            = "Id";
        private const String _EdgeIdKey              = "Id";
        private const String _HyperEdgeIdKey         = "Id";

        private const String _VertexRevisionIdKey    = "RevId";
        private const String _EdgeRevisionIdKey      = "RevId";
        private const String _HyperEdgeRevisionIdKey = "RevId";

        #endregion

        #region Constructor(s)

        #region InMemoryPropertyGraph()
        // This constructor is needed for automatic activation!

        /// <summary>
        /// Created a new in-memory property graph.
        /// </summary>
        public InMemoryPropertyGraph()
            : this(VertexId.NewVertexId)
        { }

        #endregion

        #region InMemoryPropertyGraph(GraphInitializer)

        /// <summary>
        /// Created a new in-memory property graph.
        /// </summary>
        /// <param name="GraphInitializer">A delegate to initialize the graph.</param>
        public InMemoryPropertyGraph(GraphInitializer<VertexId,    RevisionId,         String, Object,
                                                      EdgeId,      RevisionId, String, String, Object,
                                                      HyperEdgeId, RevisionId, String, String, Object> GraphInitializer)
            : this(VertexId.NewVertexId, GraphInitializer)
        { }

        #endregion

        #region InMemoryPropertyGraph(GraphId, GraphInitializer = null)

        /// <summary>
        /// Created a new in-memory property graph.
        /// </summary>
        /// <param name="GraphId">A unique identification for this graph.</param>
        /// <param name="GraphInitializer">A delegate to initialize the graph.</param>
        public InMemoryPropertyGraph(VertexId GraphId,
                                     GraphInitializer<VertexId,    RevisionId,         String, Object,
                                                      EdgeId,      RevisionId, String, String, Object,
                                                      HyperEdgeId, RevisionId, String, String, Object> GraphInitializer = null)
            : base (GraphId,
                    "Id",
                    "RevId",
                    () => new Dictionary<String, Object>(),

                    // Create a new Vertex
                    (Graph) => VertexId.NewVertexId, // Automatic VertexId generation
                    (Graph, _VertexId, VertexInitializer) =>
                        new PropertyVertex<VertexId,    RevisionId,         String, Object, IDictionary<String, Object>,
                                           EdgeId,      RevisionId, String, String, Object, IDictionary<String, Object>,
                                           HyperEdgeId, RevisionId, String, String, Object, IDictionary<String, Object>,
                                           ICollection<IPropertyEdge<VertexId,    RevisionId,         String, Object,
                                                                     EdgeId,      RevisionId, String, String, Object,
                                                                     HyperEdgeId, RevisionId, String, String, Object>>>
                            (Graph, _VertexId, _VertexIdKey, _VertexRevisionIdKey,
                             () => new Dictionary<String, Object>(),
                             () => new HashSet<IPropertyEdge<VertexId,    RevisionId,         String, Object,
                                                             EdgeId,      RevisionId, String, String, Object,
                                                             HyperEdgeId, RevisionId, String, String, Object>>(),
                             VertexInitializer
                            ),

                   
                   // Create a new Edge
                   (Graph) => EdgeId.NewEdgeId,  // Automatic EdgeId generation
                   (Graph, OutVertex, InVertex, _EdgeId, EdgeLabel, EdgeInitializer) =>
                        new PropertyEdge<VertexId,    RevisionId,         String, Object, IDictionary<String, Object>,
                                         EdgeId,      RevisionId, String, String, Object, IDictionary<String, Object>,
                                         HyperEdgeId, RevisionId, String, String, Object, IDictionary<String, Object>>
                            (Graph, OutVertex, InVertex, _EdgeId, EdgeLabel, _EdgeIdKey, _EdgeRevisionIdKey,
                             () => new Dictionary<String, Object>(),
                             EdgeInitializer
                            ),

                   // Create a new HyperEdge
                   (Graph) => HyperEdgeId.NewHyperEdgeId,  // Automatic HyperEdgeId generation
                   (Graph, Edges, _HyperEdgeId, HyperEdgeLabel, HyperEdgeInitializer) =>
                       new PropertyHyperEdge<VertexId,    RevisionId,         String, Object, IDictionary<String, Object>,
                                             EdgeId,      RevisionId, String, String, Object, IDictionary<String, Object>,
                                             HyperEdgeId, RevisionId, String, String, Object, IDictionary<String, Object>,
                                             ICollection<IPropertyEdge<VertexId,    RevisionId,         String, Object,
                                                                       EdgeId,      RevisionId, String, String, Object,
                                                                       HyperEdgeId, RevisionId, String, String, Object>>>
                            (Graph, Edges, _HyperEdgeId, HyperEdgeLabel, _HyperEdgeIdKey, _HyperEdgeRevisionIdKey,
                             () => new Dictionary<String, Object>(),
                             () => new HashSet<IPropertyEdge<VertexId,    RevisionId,         String, Object,
                                                             EdgeId,      RevisionId, String, String, Object,
                                                             HyperEdgeId, RevisionId, String, String, Object>>(),
                             HyperEdgeInitializer
                            ),

                   // The vertices collection
                   new ConcurrentDictionary<VertexId,    IPropertyVertex   <VertexId,    RevisionId,         String, Object,
                                                                            EdgeId,      RevisionId, String, String, Object,
                                                                            HyperEdgeId, RevisionId, String, String, Object>>(),

                   // The edges collection
                   new ConcurrentDictionary<EdgeId,      IPropertyEdge     <VertexId,    RevisionId,         String, Object,
                                                                            EdgeId,      RevisionId, String, String, Object,
                                                                            HyperEdgeId, RevisionId, String, String, Object>>(),

                   // The hyperedges collection
                   new ConcurrentDictionary<HyperEdgeId, IPropertyHyperEdge<VertexId,    RevisionId,         String, Object,
                                                                            EdgeId,      RevisionId, String, String, Object,
                                                                            HyperEdgeId, RevisionId, String, String, Object>>(),

                   GraphInitializer)

        { }

        #endregion

        #endregion


        #region Operator overloading

        #region Operator == (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two property graphs for equality.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public static Boolean operator == (InMemoryPropertyGraph PropertyGraph1,
                                           InMemoryPropertyGraph PropertyGraph2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(PropertyGraph1, PropertyGraph2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) PropertyGraph1 == null) || ((Object) PropertyGraph2 == null))
                return false;

            return PropertyGraph1.Equals(PropertyGraph2);

        }

        #endregion

        #region Operator != (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two property graphs for inequality.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>False if both match; True otherwise.</returns>
        public static Boolean operator != (InMemoryPropertyGraph PropertyGraph1,
                                           InMemoryPropertyGraph PropertyGraph2)
        {
            return !(PropertyGraph1 == PropertyGraph2);
        }

        #endregion

        #region Operator <  (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (InMemoryPropertyGraph PropertyGraph1,
                                          InMemoryPropertyGraph PropertyGraph2)
        {

            if ((Object) PropertyGraph1 == null)
                throw new ArgumentNullException("The given PropertyGraph1 must not be null!");

            if ((Object) PropertyGraph2 == null)
                throw new ArgumentNullException("The given PropertyGraph2 must not be null!");

            return PropertyGraph1.CompareTo(PropertyGraph2) < 0;

        }

        #endregion

        #region Operator <= (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (InMemoryPropertyGraph PropertyGraph1,
                                           InMemoryPropertyGraph PropertyGraph2)
        {
            return !(PropertyGraph1 > PropertyGraph2);
        }

        #endregion

        #region Operator >  (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (InMemoryPropertyGraph PropertyGraph1,
                                          InMemoryPropertyGraph PropertyGraph2)
        {

            if ((Object) PropertyGraph1 == null)
                throw new ArgumentNullException("The given PropertyGraph1 must not be null!");

            if ((Object) PropertyGraph2 == null)
                throw new ArgumentNullException("The given PropertyGraph2 must not be null!");

            return PropertyGraph1.CompareTo(PropertyGraph2) > 0;

        }

        #endregion

        #region Operator >= (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (InMemoryPropertyGraph PropertyGraph1,
                                           InMemoryPropertyGraph PropertyGraph2)
        {
            return !(PropertyGraph1 < PropertyGraph2);
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

            // Check if the given object can be casted to a InMemoryPropertyGraph
            var PropertyGraph = Object as InMemoryPropertyGraph;
            if ((Object)PropertyGraph == null)
                return false;

            return this.Equals(PropertyGraph);

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

    }

    #endregion

}
