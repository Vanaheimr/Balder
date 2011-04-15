/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET <http://www.github.com/ahzf/blueprints.NET>
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
using System.Collections.Concurrent;

using de.ahzf.blueprints.PropertyGraph;

#endregion

namespace de.ahzf.blueprints.PropertyGraph.InMemory
{

    #region InMemoryPropertyGraph<TId, TRevisionId, TKey, TValue>

    /// <summary>
    /// A simplified in-memory implementation of a generic property graph.
    /// </summary>
    public class InMemoryPropertyGraph<TId, TRevisionId, TKey, TValue>
                     : InMemoryGenericPropertyGraph<// Vertex definition
                                                    TId, TRevisionId, TKey, TValue, IDictionary<TKey, TValue>,
                                                    ICollection<IPropertyEdge<TId, TRevisionId, TKey, TValue,
                                                                              TId, TRevisionId, TKey, TValue,
                                                                              TId, TRevisionId, TKey, TValue>>,


                                                    // Edge definition
                                                    TId, TRevisionId, TKey, TValue, IDictionary<TKey, TValue>,

                                                    // Hyperedge definition
                                                    TId, TRevisionId, TKey, TValue, IDictionary<TKey, TValue>>

        where TId         : IEquatable<TId>,         IComparable<TId>,         IComparable, TValue
        where TRevisionId : IEquatable<TRevisionId>, IComparable<TRevisionId>, IComparable, TValue
        where TKey        : IEquatable<TKey>,        IComparable<TKey>,        IComparable
    
    {

        #region Constructor(s)

        #region InMemoryPropertyGraph()

        /// <summary>
        /// Created a new in-memory property graph.
        /// </summary>
        public InMemoryPropertyGraph(TKey myIdKey, TKey myRevisionIdKey)
            : base (// Create a new Vertex
                    (myVertexId, myVertexPropertyInitializer) =>
                        new PropertyVertex<TId, TRevisionId, TKey, TValue, IDictionary<TKey, TValue>,
                                           TId, TRevisionId, TKey, TValue, IDictionary<TKey, TValue>,
                                           TId, TRevisionId, TKey, TValue, IDictionary<TKey, TValue>,
                                           ICollection<IPropertyEdge<TId, TRevisionId, TKey, TValue,
                                                                     TId, TRevisionId, TKey, TValue,
                                                                     TId, TRevisionId, TKey, TValue>>>
                            (myVertexId, myIdKey, myRevisionIdKey,
                             () => new Dictionary<TKey, TValue>(),
                             () => new HashSet<IPropertyEdge<TId, TRevisionId, TKey, TValue,
                                                             TId, TRevisionId, TKey, TValue,
                                                             TId, TRevisionId, TKey, TValue>>(),
                             myVertexPropertyInitializer
                            ),

                   // Create a new Edge
                   (myOutVertex, myInVertex, myEdgeId, myLabel, myEdgePropertyInitializer) =>
                        new PropertyEdge<TId, TRevisionId, TKey, TValue, IDictionary<TKey, TValue>,
                                         TId, TRevisionId, TKey, TValue, IDictionary<TKey, TValue>,
                                         TId, TRevisionId, TKey, TValue, IDictionary<TKey, TValue>>
                            (myOutVertex, myInVertex, myEdgeId, myLabel, myIdKey, myRevisionIdKey,
                             () => new Dictionary<TKey, TValue>(),
                             myEdgePropertyInitializer
                            ),

                   // Create a new HyperEdge
                   (myEdges, myHyperEdgeId, myLabel, myHyperEdgePropertyInitializer) =>
                       new PropertyHyperEdge<TId, TRevisionId, TKey, TValue, IDictionary<TKey, TValue>,
                                             TId, TRevisionId, TKey, TValue, IDictionary<TKey, TValue>,
                                             TId, TRevisionId, TKey, TValue, IDictionary<TKey, TValue>,
                                             ICollection<IPropertyEdge<TId, TRevisionId, TKey, TValue,
                                                                       TId, TRevisionId, TKey, TValue,
                                                                       TId, TRevisionId, TKey, TValue>>>
                            (myEdges, myHyperEdgeId, myLabel, myIdKey, myRevisionIdKey,
                             () => new Dictionary<TKey, TValue>(),
                             () => new HashSet<IPropertyEdge<TId, TRevisionId, TKey, TValue,
                                                             TId, TRevisionId, TKey, TValue,
                                                             TId, TRevisionId, TKey, TValue>>(),
                             myHyperEdgePropertyInitializer
                            ),

                   // The vertices collection
                   new ConcurrentDictionary<TId, IPropertyVertex   <TId, TRevisionId, TKey, TValue,
                                                                    TId, TRevisionId, TKey, TValue,
                                                                    TId, TRevisionId, TKey, TValue>>(),

                   // The edges collection
                   new ConcurrentDictionary<TId, IPropertyEdge     <TId, TRevisionId, TKey, TValue,
                                                                    TId, TRevisionId, TKey, TValue,
                                                                    TId, TRevisionId, TKey, TValue>>(),

                   // The hyperedges collection
                   new ConcurrentDictionary<TId, IPropertyHyperEdge<TId, TRevisionId, TKey, TValue,
                                                                    TId, TRevisionId, TKey, TValue,
                                                                    TId, TRevisionId, TKey, TValue>>()

            )

        { }

        #endregion

        #endregion

    }

    #endregion


    #region InMemoryGenericPropertyGraph<...>

    /// <summary>
    /// An in-memory implementation of the IGraph interface.
    /// </summary>
    public class InMemoryGenericPropertyGraph<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                              TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
       
                                              : IPropertyGraph<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                               TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                               TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>

        where TIdVertex               : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge                 : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdHyperEdge            : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TRevisionIdVertex       : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge         : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdHyperEdge    : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

        where TDatastructureVertex    : IDictionary<TKeyVertex,    TValueVertex>
        where TDatastructureEdge      : IDictionary<TKeyEdge,      TValueEdge>
        where TDatastructureHyperEdge : IDictionary<TKeyHyperEdge, TValueHyperEdge>

    {

        #region Data

        // Make it more generic??!
        private readonly IDictionary<TIdVertex,    IPropertyVertex   <TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                                                      TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                                                      TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>> _Vertices;

        private readonly IDictionary<TIdEdge,      IPropertyEdge     <TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                                                      TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                                                      TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>> _Edges;

        private readonly IDictionary<TIdHyperEdge, IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                                                      TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                                                      TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>> _HyperEdges;

        private readonly Func<TIdVertex,
                              Action<IProperties<TKeyVertex,    TValueVertex>>,
                              IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>>

                         _VertexCreatorDelegate;

        private readonly Func<IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>,
                              IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>,
                              TIdEdge,
                              String,
                              Action<IProperties<TKeyEdge, TValueEdge>>,
                              IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                            TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>>

                         _EdgeCreatorDelegate;

        private readonly Func<IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>>,
                              TIdHyperEdge,
                              String,
                              Action<IProperties<TKeyHyperEdge, TValueHyperEdge>>,
                              IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                                 TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>>

                         _HyperEdgeCreatorDelegate;

        //protected Map<String, TinkerIndex>          indices     = new HashMap<String, TinkerIndex>();
        //protected Map<String, TinkerAutomaticIndex> autoIndices = new HashMap<String, TinkerAutomaticIndex>();

        #endregion

        #region Constructor(s)

        #region InMemoryGenericPropertyGraph(myVertexCreatorDelegate, myEdgeCreatorDelegate, myHyperEdgeCreatorDelegate, myVerticesCollectionInitializer, myEdgesCollectionInitializer, myHyperEdgesCollectionInitializer)

        /// <summary>
        /// Created a new genric and in-memory property graph.
        /// </summary>
        /// <param name="myVertexCreatorDelegate"></param>
        /// <param name="myEdgeCreatorDelegate"></param>
        /// <param name="myHyperEdgeCreatorDelegate"></param>
        /// <param name="myVerticesCollectionInitializer"></param>
        /// <param name="myEdgesCollectionInitializer"></param>
        /// <param name="myHyperEdgesCollectionInitializer"></param>
        public InMemoryGenericPropertyGraph(// Vertices creator
                                            Func<TIdVertex,
                                                 Action<IProperties<TKeyVertex, TValueVertex>>,
                                                 IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                                                 TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>>

                                            myVertexCreatorDelegate,


                                            // Edges creator
                                            Func<IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                                                 TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>,
                                                 IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                                                 TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>,
                                                 TIdEdge,
                                                 String,
                                                 Action<IProperties<TKeyEdge, TValueEdge>>,
                                                 IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                                               TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>>

                                            myEdgeCreatorDelegate,


                                            // Hyperedges creator
                                            Func<IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                                                           TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                                                           TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>>,
                                                 TIdHyperEdge,
                                                 String,
                                                 Action<IProperties<TKeyHyperEdge, TValueHyperEdge>>,
                                                 IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                                                    TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>>

                                            myHyperEdgeCreatorDelegate,


                                            // Vertices Collection
                                            IDictionary<TIdVertex,    IPropertyVertex   <TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                                                                         TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                                                                         TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>>
                                            myVerticesCollectionInitializer,

                                            // Edges Collection
                                            IDictionary<TIdEdge,      IPropertyEdge     <TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                                                                         TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                                                                         TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>>
                                            myEdgesCollectionInitializer,

                                            // Hyperedges Collection
                                            IDictionary<TIdHyperEdge, IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                                                                         TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                                                                         TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>>
                                            myHyperEdgesCollectionInitializer

                                           )
        {

            _VertexCreatorDelegate    = myVertexCreatorDelegate;
            _EdgeCreatorDelegate      = myEdgeCreatorDelegate;
            _HyperEdgeCreatorDelegate = myHyperEdgeCreatorDelegate;

            _Vertices                 = myVerticesCollectionInitializer;
            _Edges                    = myEdgesCollectionInitializer;
            _HyperEdges               = myHyperEdgesCollectionInitializer;

            //this.createIndex(Index.VERTICES, TinkerVertex.class, Index.Type.AUTOMATIC);
            //this.createIndex(Index.EDGES, TinkerEdge.class, Index.Type.AUTOMATIC);

        }

        #endregion

        #endregion


        #region Vertex methods

        #region AddVertex(myVertexId = null, myVertexPropertyInitializer = null)

        /// <summary>
        /// Adds a vertex to the graph using the given VertexId and initializes
        /// its properties by invoking the given vertex initializer.
        /// </summary>
        /// <param name="myVertexId">A VertexId. If none was given a new one will be generated.</param>
        /// <param name="myVertexPropertyInitializer">A delegate to initialize the newly generated vertex</param>
        /// <returns>The new vertex</returns>
        public IPropertyVertex<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                     TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> AddVertex(TIdVertex myVertexId = default(TIdVertex), Action<IProperties<TKeyVertex, TValueVertex>> myVertexPropertyInitializer = null)
        {

            if (myVertexId != null && _Vertices.ContainsKey(myVertexId))
                throw new ArgumentException("Another vertex with id " + myVertexId + " already exists");

            var _Vertex = _VertexCreatorDelegate(myVertexId, myVertexPropertyInitializer);

            _Vertices.Add(myVertexId, _Vertex);

            return _Vertex;

        }

        #endregion

        #region AddVertex(myIVertex)

        /// <summary>
        /// Adds a vertex to the graph using the given VertexId and initializes
        /// its properties by invoking the given vertex initializer.
        /// </summary>
        /// <param name="myIVertex"></param>
        /// <returns>The new vertex</returns>
        public IPropertyVertex<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                     TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> AddVertex(IPropertyVertex<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                     TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> myIVertex)
        {

            if (myIVertex == null)
                throw new ArgumentNullException("myIVertex must not be null!");

            if (myIVertex.Id == null)
                throw new ArgumentNullException("The Id of myIVertex must not be null!");

            if (myIVertex != null && _Vertices.ContainsKey(myIVertex.Id))
                throw new ArgumentException("Another vertex with id " + myIVertex.Id + " already exists!");

            _Vertices.Add(myIVertex.Id, myIVertex);

            return myIVertex;

        }

        #endregion

        #region GetVertex(myVertexId)

        /// <summary>
        /// Return the vertex referenced by the given vertex identifier.
        /// If no vertex is referenced by that identifier, then return null.
        /// </summary>
        /// <param name="myVertexId">The identifier of the vertex.</param>
        /// <returns>The vertex referenced by the provided identifier or null when no such edge exists.</returns>
        public IPropertyVertex<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                     TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> GetVertex(TIdVertex myVertexId)
        {

            IPropertyVertex<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                     TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> _IVertex;

            _Vertices.TryGetValue(myVertexId, out _IVertex);

            return _IVertex;

        }

        #endregion

        #region GetVertices(params myVertexIds)

        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// An additional vertex filter may be applied for filtering.
        /// </summary>
        /// <param name="myVertexIds">An array of vertex identifiers.</param>
        public IEnumerable<IPropertyVertex<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                     TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>> GetVertices(params TIdVertex[] myVertexIds)
        {

            IPropertyVertex<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                     TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> _IVertex;

            foreach (var _IVertexId in myVertexIds)
                if (_IVertexId != null)
                {
                    _Vertices.TryGetValue(_IVertexId, out _IVertex);
                    yield return _IVertex;
                }

        }

        #endregion

        #region Vertices

        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// </summary>
        public IEnumerable<IPropertyVertex<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                     TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>> Vertices
        {

            get
            {
                foreach (var _IVertex in _Vertices.Values)
                    yield return _IVertex;
            }

        }

        #endregion

        #region GetVertices(myVertexFilter = null)

        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// An additional vertex filter may be applied for filtering.
        /// </summary>
        /// <param name="myVertexFilter">A delegate for vertex filtering.</param>
        public IEnumerable<IPropertyVertex<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                     TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>> GetVertices(Func<IPropertyVertex<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                     TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>, Boolean> myVertexFilter = null)
        {

            foreach (var _IVertex in _Vertices.Values)
                if (myVertexFilter == null)
                    yield return _IVertex;

                else if (myVertexFilter(_IVertex))
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
        public void RemoveVertex(IPropertyVertex<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                     TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> myIVertex)
        {

            lock (this)
            {

                if (_Vertices.ContainsKey(myIVertex.Id))
                {

                    var _EdgeList = new List<IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                                           TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>>();

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

        #region AddEdge(myOutVertex, myInVertex, myEdgeId = null, myLabel = null, myEdgePropertyInitializer = null)

        /// <summary>
        /// Adds an edge to the graph using the given myEdgeId and initializes
        /// its properties by invoking the given edge initializer.
        /// </summary>
        /// <param name="myOutVertex"></param>
        /// <param name="myInVertex"></param>
        /// <param name="myEdgeId">A EdgeId. If none was given a new one will be generated.</param>
        /// <param name="myLabel"></param>
        /// <param name="myEdgeInitializer">A delegate to initialize the newly generated edge.</param>
        /// <returns>The new edge</returns>
        public IPropertyEdge<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                   TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                   TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> AddEdge(IPropertyVertex<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                     TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> myOutVertex,
                                     IPropertyVertex<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                     TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> myInVertex,
                                     TIdEdge         myEdgeId = default(TIdEdge),
                                     String          myLabel  = null,
                                     Action<IProperties<TKeyEdge, TValueEdge>> myEdgePropertyInitializer = null)

        {

            if (myEdgeId != null && _Edges.ContainsKey(myEdgeId))
                throw new ArgumentException("Another edge with id " + myEdgeId + " already exists!");

            var _Edge = _EdgeCreatorDelegate(myOutVertex, myInVertex, myEdgeId, myLabel, myEdgePropertyInitializer);
            
            _Edges.Add(myEdgeId, _Edge);

            myOutVertex.AddOutEdge(_Edge);
            myInVertex.AddInEdge(_Edge);

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
        public IPropertyEdge<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                   TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                   TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> GetEdge(TIdEdge myEdgeId)
        {

            IPropertyEdge<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                   TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                   TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> _IEdge;

            _Edges.TryGetValue(myEdgeId, out _IEdge);

            return _IEdge;

        }

        #endregion

        #region GetEdges(params myEdgeIds)

        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// An additional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="myEdgeIds">An array of edge identifiers.</param>
        public IEnumerable<IPropertyEdge<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                   TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                   TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>> GetEdges(params TIdEdge[] myEdgeIds)
        {

            IPropertyEdge<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                   TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                   TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> _IEdge;

            foreach (var _IEdgeId in myEdgeIds)
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
        public IEnumerable<IPropertyEdge<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                   TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                   TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>> Edges
        {

            get
            {
                foreach (var _IEdge in _Edges.Values)
                    yield return _IEdge;
            }

        }

        #endregion

        #region GetEdges(myEdgeFilter = null)

        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// An additional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="myEdgeFilter">A delegate for edge filtering.</param>
        public IEnumerable<IPropertyEdge<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                   TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                   TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>> GetEdges(Func<IPropertyEdge<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                   TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                   TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>, Boolean> myEdgeFilter = null)
        {

            foreach (var _IEdge in _Edges.Values)
                if (myEdgeFilter == null)
                    yield return _IEdge;

                else if (myEdgeFilter(_IEdge))
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
        public void RemoveEdge(IPropertyEdge<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                   TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                   TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> myIEdge)
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


        #region Clear()

        /// <summary>
        /// Remove all the edges and vertices from the graph.
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

    }

    #endregion

}
