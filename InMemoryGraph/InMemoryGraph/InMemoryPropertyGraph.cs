﻿/*
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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

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

                                              : APropertyElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex, TDatastructureVertex>,

                                                IPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>

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

        // Make it more generic??!

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


        private readonly VertexIdCreatorDelegate    _VertexIdCreatorDelegate;
        private readonly VertexCreatorDelegate      _VertexCreatorDelegate;

        private readonly EdgeIdCreatorDelegate      _EdgeIdCreatorDelegate;
        private readonly EdgeCreatorDelegate        _EdgeCreatorDelegate;

        private readonly HyperEdgeIdCreatorDelegate _HyperEdgeIdCreatorDelegate;
        private readonly HyperEdgeCreatorDelegate   _HyperEdgeCreatorDelegate;

        //protected Map<String, TinkerIndex>          indices     = new HashMap<String, TinkerIndex>();
        //protected Map<String, TinkerAutomaticIndex> autoIndices = new HashMap<String, TinkerAutomaticIndex>();

        #endregion

        #region Delegates

        //public delegate void VertexConfiguratorDelegate(IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
        //                                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex);

        #region VertexIdCreatorDelegate(...)

        /// <summary>
        /// A delegate for creating a new VertexId
        /// (if no one was provided by the user).
        /// </summary>
        /// <returns>A valid VertexId.</returns>
        public delegate TIdVertex VertexIdCreatorDelegate();

        #endregion

        #region VertexCreatorDelegate(...)

        /// <summary>
        /// A delegate for creating a new vertex.
        /// </summary>
        /// <param name="VertexId">The Id of the vertex.</param>
        /// <param name="VertexInitializer">A delegate to initialize this edge with custom data.</param>
        public delegate IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                        VertexCreatorDelegate(TIdVertex VertexId,
                                              VertexInitializer<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexInitializer);

        #endregion

        #region EdgeIdCreatorDelegate(...)

        /// <summary>
        /// A delegate for creating a new EdgeId
        /// (if no one was provided by the user).
        /// </summary>
        /// <returns>A valid EdgeId.</returns>
        public delegate TIdEdge EdgeIdCreatorDelegate();

        #endregion

        #region EdgeCreatorDelegate(...)

        /// <summary>
        /// A delegate for creating a new edge.
        /// </summary>
        /// <param name="SourceVertex">The source vertex.</param>
        /// <param name="TargetVertex">The target vertex.</param>
        /// <param name="EdgeId">The Id of this edge.</param>
        /// <param name="EdgeLabel">The label of this edge.</param>
        /// <param name="EdgeInitializer">A delegate to initialize this edge with custom data.</param>
        public delegate IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                        EdgeCreatorDelegate(
                              IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> SourceVertex,
                              IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> TargetVertex,
                              TIdEdge         EdgeId,
                              TEdgeLabel      EdgeLabel,
                              EdgeInitializer<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer);

        #endregion

        #region HyperEdgeIdCreatorDelegate(...)

        /// <summary>
        /// A delegate for creating a new HyperEdgeId
        /// (if no one was provided by the user).
        /// </summary>
        /// <returns>A valid HyperEdgeId.</returns>
        public delegate TIdHyperEdge HyperEdgeIdCreatorDelegate();

        #endregion

        #region HyperEdgeCreatorDelegate(...)

        /// <summary>
        /// A delegate for creating a new hyperedge.
        /// </summary>
        /// <param name="Edges">The edges of the hyperedge.</param>
        /// <param name="HyperEdgeId">The Id of this hyperedge.</param>
        /// <param name="HyperEdgeLabel">The label of this hyperedge.</param>
        /// <param name="HyperEdgeInitializer">A delegate to initialize this hyperedge with custom data.</param>
        /// <returns></returns>
        public delegate IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
                        HyperEdgeCreatorDelegate(
                              IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Edges,
                              TIdHyperEdge              HyperEdgeId,
                              THyperEdgeLabel           HyperEdgeLabel,
                              Action<IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> HyperEdgeInitializer);

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

                                            VertexIdCreatorDelegate      VertexIdCreatorDelegate,
                                            VertexCreatorDelegate        VertexCreatorDelegate,

                                            EdgeIdCreatorDelegate        EdgeIdCreatorDelegate,
                                            EdgeCreatorDelegate          EdgeCreatorDelegate,

                                            HyperEdgeIdCreatorDelegate   HyperEdgeIdCreatorDelegate,
                                            HyperEdgeCreatorDelegate     HyperEdgeCreatorDelegate,

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

            //this.createIndex(Index.VERTICES, TinkerVertex.class, Index.Type.AUTOMATIC);
            //this.createIndex(Index.EDGES, TinkerEdge.class, Index.Type.AUTOMATIC);

        }

        #endregion

        #endregion


        #region Vertex methods

        #region AddVertex(VertexId = null, VertexInitializer = null)

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

                               AddVertex(TIdVertex VertexId = default(TIdVertex),
                                         VertexInitializer<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexInitializer = null)

        {

            if (VertexId == null || VertexId.Equals(default(TIdVertex)))
                VertexId = _VertexIdCreatorDelegate();

            if (_Vertices.ContainsKey(VertexId))
                throw new ArgumentException("Another vertex with id " + VertexId + " already exists");

            var _Vertex = _VertexCreatorDelegate(VertexId, VertexInitializer);

            _Vertices.Add(VertexId, _Vertex);

            return _Vertex;

        }

        #endregion

        #region AddVertex(VertexInitializer)

        /// <summary>
        /// Adds a vertex to the graph having an autogenerated vertex id and
        /// initializes the vertex by invoking the given vertex initializer.
        /// </summary>
        /// <param name="VertexInitializer">A delegate to initialize the new vertex.</param>
        /// <returns>The new vertex</returns>
        public IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                               AddVertex(VertexInitializer<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexInitializer)

        {
            return AddVertex(default(TIdVertex), VertexInitializer);
        }

        #endregion

        #region AddVertex(IPropertyVertex)

        /// <summary>
        /// Adds the given vertex to the graph.
        /// Will fail if the Id of the vertex is already present in the graph.
        /// </summary>
        /// <param name="IPropertyVertex">A IPropertyVertex.</param>
        /// <returns>The given vertex.</returns>
        public virtual IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
                       AddVertex(IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IPropertyVertex)
        {

            if (IPropertyVertex == null)
                throw new ArgumentNullException("The given IVertex must not be null!");

            if (IPropertyVertex.Id == null || IPropertyVertex.Id.Equals(default(TIdVertex)))
                throw new ArgumentNullException("The Id of IVertex must not be null!");

            if (_Vertices.ContainsKey(IPropertyVertex.Id))
                throw new ArgumentException("Another vertex with id " + IPropertyVertex.Id + " already exists!");

            _Vertices.Add(IPropertyVertex.Id, IPropertyVertex);

            return IPropertyVertex;

        }

        #endregion

        #region GetVertex(VertexId)

        /// <summary>
        /// Return the vertex referenced by the given vertex identifier.
        /// If no vertex is referenced by that identifier, then return null.
        /// </summary>
        /// <param name="VertexId">The identifier of the vertex.</param>
        /// <returns>The vertex referenced by the provided identifier or null when no such edge exists.</returns>
        public virtual IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                                       GetVertex(TIdVertex VertexId)

        {


            if (VertexId == null)
                throw new ArgumentNullException("The VertexId must not be null!");

            IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _IVertex;

            _Vertices.TryGetValue(VertexId, out _IVertex);

            return _IVertex;

        }

        #endregion

        #region GetVertices(params VertexIds)

        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// An additional vertex filter may be applied for filtering.
        /// </summary>
        /// <param name="VertexIds">An array of vertex identifiers.</param>
        public virtual IEnumerable<IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                                                   GetVertices(params TIdVertex[] VertexIds)

        {

            if (VertexIds == null || !VertexIds.Any())
                throw new ArgumentNullException("The VertexIds array must not be null or its length zero!");

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

        #region Vertices

        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// </summary>
        public virtual IEnumerable<IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                                                   Vertices

        {

            get
            {
                foreach (var _IVertex in _Vertices.Values)
                    yield return _IVertex;
            }

        }

        #endregion

        #region GetVertices(VertexFilter = null)

        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// An additional vertex filter may be applied for filtering.
        /// </summary>
        /// <param name="PropertyVertexFilter">A delegate for property vertex filtering.</param>
        public virtual IEnumerable<IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
               GetVertices(VertexFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> PropertyVertexFilter = null)
        {

            foreach (var _IVertex in _Vertices.Values)
                if (PropertyVertexFilter == null)
                    yield return _IVertex;

                else if (PropertyVertexFilter(_IVertex))
                    yield return _IVertex;

        }

        #endregion

        #region RemoveVertex(myVertexId)

        /// <summary>
        /// Remove the vertex identified by the given VertexId from the graph
        /// </summary>
        /// <param name="myVertexId">The VertexId of the vertex to remove</param>
        public void RemoveVertex(TIdVertex myVertexId)
        {
            RemoveVertex(GetVertex(myVertexId));
        }

        #endregion

        #region RemoveVertex(myIVertex)

        /// <summary>
        ///  Remove the given vertex from the graph
        /// </summary>
        /// <param name="myIVertex">A vertex</param>
        public void RemoveVertex(IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> myIVertex)
        {

            lock (this)
            {

                if (_Vertices.ContainsKey(myIVertex.Id))
                {

                    var _EdgeList = new List<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>();

                    _EdgeList.AddRange(myIVertex.InEdges);
                    _EdgeList.AddRange(myIVertex.OutEdges);

                    // removal requires removal from all indices
                    //for (TinkerIndex index : this.indices.values()) {
                    //    index.remove(vertex);
                    //}

                    _Vertices.Remove(myIVertex.Id);

                }

            }

        }

        #endregion

        #endregion

        #region Edge methods

        #region AddEdge(OutVertex, InVertex, EdgeId = null, Label = null, EdgePropertyInitializer = null)

        /// <summary>
        /// Adds an edge to the graph using the given myEdgeId and initializes
        /// its properties by invoking the given edge initializer.
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

                                     TIdEdge         EdgeId = default(TIdEdge),
                                     TEdgeLabel      Label  = default(TEdgeLabel),

                                     EdgeInitializer<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer = null)


        {

            if (EdgeId == null || EdgeId.Equals(default(TIdEdge)))
                EdgeId = _EdgeIdCreatorDelegate();

            if (_Edges.ContainsKey(EdgeId))
                throw new ArgumentException("Another edge with id " + EdgeId + " already exists!");

            var _Edge = _EdgeCreatorDelegate(OutVertex, InVertex, EdgeId, Label, EdgeInitializer);
            
            _Edges.Add(EdgeId, _Edge);

            OutVertex.AddOutEdge(_Edge);
            InVertex.AddInEdge(_Edge);

            return _Edge;

        }

        #endregion

        #region GetEdge(myEdgeId)

        /// <summary>
        /// Return the edge referenced by the given edge identifier.
        /// If no edge is referenced by that identifier, then return null.
        /// </summary>
        /// <param name="myEdgeId">The identifier of the edge.</param>
        /// <returns>The edge referenced by the provided identifier or null when no such edge exists.</returns>
        public IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GetEdge(TIdEdge myEdgeId)
        {

            IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _IEdge;

            _Edges.TryGetValue(myEdgeId, out _IEdge);

            return _IEdge;

        }

        #endregion

        #region GetEdges(params EdgeIds)

        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// An additional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="EdgeIds">An array of edge identifiers.</param>
        public IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> GetEdges(params TIdEdge[] EdgeIds)
        {

            IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _IEdge;

            foreach (var _IEdgeId in EdgeIds)
                if (_IEdgeId != null)
                {
                    _Edges.TryGetValue(_IEdgeId, out _IEdge);
                    yield return _IEdge;
                }

        }

        #endregion

        #region Edges

        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// </summary>
        public IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Edges
        {

            get
            {
                foreach (var _IEdge in _Edges.Values)
                    yield return _IEdge;
            }

        }

        #endregion

        #region GetEdges(EdgeFilter = null)

        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// An additional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        public IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
                 GetEdges(EdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter = null)
        {

            foreach (var _IEdge in _Edges.Values)
                if (EdgeFilter == null)
                    yield return _IEdge;

                else if (EdgeFilter(_IEdge))
                    yield return _IEdge;

        }

        #endregion

        #region RemoveEdge(myEdgeId)

        /// <summary>
        /// Remove the edge identified by the given EdgeId from the graph
        /// </summary>
        /// <param name="myEdgeId">The myEdgeId of the edge to remove</param>
        public void RemoveEdge(TIdEdge myEdgeId)
        {
            RemoveEdge(GetEdge(myEdgeId));
        }

        #endregion

        #region RemoveEdge(myIEdge)

        /// <summary>
        ///  Remove the given edge from the graph
        /// </summary>
        /// <param name="myIEdge">An edge</param>
        public void RemoveEdge(IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> myIEdge)
        {

            lock (this)
            {

                if (_Edges.ContainsKey(myIEdge.Id))
                {

                    var _OutVertex = myIEdge.OutVertex;
                    var _InVertex = myIEdge.InVertex;

                    if (_OutVertex != null && _OutVertex.OutEdges != null)
                        _OutVertex.RemoveOutEdge(myIEdge);

                    if (_InVertex != null && _InVertex.InEdges != null)
                        _InVertex.RemoveInEdge(myIEdge);

                    // removal requires removal from all indices
                    //for (TinkerIndex index : this.indices.values()) {
                    //    index.remove(edge);
                    //}

                    _Edges.Remove(myIEdge.Id);

                }

            }

        }

        #endregion

        #endregion

        #region HyperEdge methods

        /// <summary>
        /// Return the hyperedge referenced by the given hyperedge identifier.
        /// If no hyperedge is referenced by that identifier, then return null.
        /// </summary>
        /// <param name="HyperEdgeId">The identifier of the edge.</param>
        /// <returns>The edge referenced by the provided identifier or null when no such edge exists.</returns>
        public IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                      GetHyperEdge(TIdHyperEdge HyperEdgeId)
        {
            throw new NotImplementedException();
        }

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
        public void Shutdown()
        {
            Clear();
        }

        #endregion



        public bool Equals(TIdVertex other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(TIdVertex other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public new IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Equals(IPropertyElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex> other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(IPropertyElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex> other)
        {
            throw new NotImplementedException();
        }

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

        #region InMemoryPropertyGraph(GraphInitializer = null)

        /// <summary>
        /// Created a new in-memory property graph.
        /// </summary>
        /// <param name="GraphInitializer">A delegate to initialize the graph.</param>
        public InMemoryPropertyGraph(GraphInitializer<VertexId,    RevisionId,         String, Object,
                                                      EdgeId,      RevisionId, String, String, Object,
                                                      HyperEdgeId, RevisionId, String, String, Object> GraphInitializer = null)
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
                    () => VertexId.NewVertexId, // Automatic VertexId generation
                    (_VertexId, VertexInitializer) =>
                        new PropertyVertex<VertexId,    RevisionId,         String, Object, IDictionary<String, Object>,
                                           EdgeId,      RevisionId, String, String, Object, IDictionary<String, Object>,
                                           HyperEdgeId, RevisionId, String, String, Object, IDictionary<String, Object>,
                                           ICollection<IPropertyEdge<VertexId,    RevisionId,         String, Object,
                                                                     EdgeId,      RevisionId, String, String, Object,
                                                                     HyperEdgeId, RevisionId, String, String, Object>>>
                            (_VertexId, _VertexIdKey, _VertexRevisionIdKey,
                             () => new Dictionary<String, Object>(),
                             () => new HashSet<IPropertyEdge<VertexId,    RevisionId,         String, Object,
                                                             EdgeId,      RevisionId, String, String, Object,
                                                             HyperEdgeId, RevisionId, String, String, Object>>(),
                             VertexInitializer
                            ),

                   
                   // Create a new Edge
                   () => EdgeId.NewEdgeId,  // Automatic EdgeId generation
                   (OutVertex, InVertex, _EdgeId, EdgeLabel, EdgeInitializer) =>
                        new PropertyEdge<VertexId,    RevisionId,         String, Object, IDictionary<String, Object>,
                                         EdgeId,      RevisionId, String, String, Object, IDictionary<String, Object>,
                                         HyperEdgeId, RevisionId, String, String, Object, IDictionary<String, Object>>
                            (OutVertex, InVertex, _EdgeId, EdgeLabel, _EdgeIdKey, _EdgeRevisionIdKey,
                             () => new Dictionary<String, Object>(),
                             EdgeInitializer
                            ),

                   // Create a new HyperEdge
                   () => HyperEdgeId.NewHyperEdgeId,  // Automatic HyperEdgeId generation
                   (Edges, _HyperEdgeId, HyperEdgeLabel, HyperEdgeInitializer) =>
                       new PropertyHyperEdge<VertexId,    RevisionId,         String, Object, IDictionary<String, Object>,
                                             EdgeId,      RevisionId, String, String, Object, IDictionary<String, Object>,
                                             HyperEdgeId, RevisionId, String, String, Object, IDictionary<String, Object>,
                                             ICollection<IPropertyEdge<VertexId,    RevisionId,         String, Object,
                                                                       EdgeId,      RevisionId, String, String, Object,
                                                                       HyperEdgeId, RevisionId, String, String, Object>>>
                            (Edges, _HyperEdgeId, HyperEdgeLabel, _HyperEdgeIdKey, _HyperEdgeRevisionIdKey,
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

    }

    #endregion

}
