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

#endregion

namespace de.ahzf.blueprints.InMemory.PropertyGraph.Generic
{

    #region InMemoryPropertyGraph<TId, TRevisionId, TKey, TValue>

    /// <summary>
    /// A simplified in-memory implementation of a generic property graph.
    /// </summary>
    public class InMemoryPropertyGraph<TId, TRevisionId, TKey, TValue>
                     : InMemoryGenericPropertyGraph<// Vertex definition
                                                    TId, TRevisionId, TKey, TValue, IDictionary<TKey, TValue>,
                                                    //IPropertyVertex<TId, TRevisionId, IProperties<TKey, TValue>,
                                                    //                TId, TRevisionId, IProperties<TKey, TValue>,
                                                    //                TId, TRevisionId, IProperties<TKey, TValue>>,
                                                    IGenericVertex<TId, TRevisionId, IProperties<TKey, TValue>,
                                                                   TId, TRevisionId, IProperties<TKey, TValue>,
                                                                   TId, TRevisionId, IProperties<TKey, TValue>>,
                                                    ICollection<IGenericEdge<TId, TRevisionId, IProperties<TKey, TValue>,
                                                                             TId, TRevisionId, IProperties<TKey, TValue>,
                                                                             TId, TRevisionId, IProperties<TKey, TValue>>>,

                                                    // Edge definition
                                                    TId, TRevisionId, TKey, TValue, IDictionary<TKey, TValue>,
                                                    IGenericEdge<TId, TRevisionId, IProperties<TKey, TValue>,
                                                                 TId, TRevisionId, IProperties<TKey, TValue>,
                                                                 TId, TRevisionId, IProperties<TKey, TValue>>,

                                                    // Hyperedge definition
                                                    TId, TRevisionId, TKey, TValue, IDictionary<TKey, TValue>,
                                                    IGenericHyperEdge<TId, TRevisionId, IProperties<TKey, TValue>,
                                                                      TId, TRevisionId, IProperties<TKey, TValue>,
                                                                      TId, TRevisionId, IProperties<TKey, TValue>>,

                                                    Object>

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
                                           ICollection<IGenericEdge<TId, TRevisionId, IProperties<TKey, TValue>,
                                                                    TId, TRevisionId, IProperties<TKey, TValue>,
                                                                    TId, TRevisionId, IProperties<TKey, TValue>>>>
                            (myVertexId, myIdKey, myRevisionIdKey,
                             () => new Dictionary<TKey, TValue>(),
                             () => new HashSet<IGenericEdge<TId, TRevisionId, IProperties<TKey, TValue>,
                                                            TId, TRevisionId, IProperties<TKey, TValue>,
                                                            TId, TRevisionId, IProperties<TKey, TValue>>>(),
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
                   (myOutVertex, myInVertices, myHyperEdgeId, myLabel, myHyperEdgePropertyInitializer) =>
                       new PropertyHyperEdge<TId, TRevisionId, TKey, TValue, IDictionary<TKey, TValue>,
                                             TId, TRevisionId, TKey, TValue, IDictionary<TKey, TValue>,
                                             TId, TRevisionId, TKey, TValue, IDictionary<TKey, TValue>,
                                             ICollection<IGenericVertex<TId, TRevisionId, IProperties<TKey, TValue>,
                                                                        TId, TRevisionId, IProperties<TKey, TValue>,
                                                                        TId, TRevisionId, IProperties<TKey, TValue>>>>
                            (myOutVertex, myInVertices, myHyperEdgeId, myLabel, myIdKey, myRevisionIdKey,
                             () => new Dictionary<TKey, TValue>(),
                             () => new HashSet<IGenericVertex<TId, TRevisionId, IProperties<TKey, TValue>,
                                                              TId, TRevisionId, IProperties<TKey, TValue>,
                                                              TId, TRevisionId, IProperties<TKey, TValue>>>(),
                             myHyperEdgePropertyInitializer
                            ),

                   // The vertices collection
                   new ConcurrentDictionary<TId, IGenericVertex   <TId, TRevisionId, IProperties<TKey, TValue>,
                                                                   TId, TRevisionId, IProperties<TKey, TValue>,
                                                                   TId, TRevisionId, IProperties<TKey, TValue>>>(),

                   // The edges collection
                   new ConcurrentDictionary<TId, IGenericEdge     <TId, TRevisionId, IProperties<TKey, TValue>,
                                                                   TId, TRevisionId, IProperties<TKey, TValue>,
                                                                   TId, TRevisionId, IProperties<TKey, TValue>>>(),

                   // The hyperedges collection
                   new ConcurrentDictionary<TId, IGenericHyperEdge<TId, TRevisionId, IProperties<TKey, TValue>,
                                                                   TId, TRevisionId, IProperties<TKey, TValue>,
                                                                   TId, TRevisionId, IProperties<TKey, TValue>>>()

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
    public class InMemoryGenericPropertyGraph<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex,    TVertexExchange, TEdgeCollection,
                                              TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,      TEdgeExchange,
                                              TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge, THyperEdgeExchange,
                                              TGraphDatastructure>
       
                                              : IPropertyGraph<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex,    TVertexExchange,
                                                               TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,      TEdgeExchange,
                                                               TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge, THyperEdgeExchange,
                                                               TGraphDatastructure>

        where TDatastructureVertex    : IDictionary<TKeyVertex,    TValueVertex>
        where TDatastructureEdge      : IDictionary<TKeyEdge,      TValueEdge>
        where TDatastructureHyperEdge : IDictionary<TKeyHyperEdge, TValueHyperEdge>

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable
                                                                                                            
        where TIdVertex               : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge                 : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdHyperEdge            : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TRevisionIdVertex       : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge         : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdHyperEdge    : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

        where TVertexExchange         : IGenericVertex   <TIdVertex,    TRevisionIdVertex,    IProperties<TKeyVertex,    TValueVertex>,
                                                          TIdEdge,      TRevisionIdEdge,      IProperties<TKeyEdge,      TValueEdge>,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, IProperties<TKeyHyperEdge, TValueHyperEdge>>

        where TEdgeExchange           : IGenericEdge     <TIdVertex,    TRevisionIdVertex,    IProperties<TKeyVertex,    TValueVertex>,
                                                          TIdEdge,      TRevisionIdEdge,      IProperties<TKeyEdge,      TValueEdge>,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, IProperties<TKeyHyperEdge, TValueHyperEdge>>

        where THyperEdgeExchange      : IGenericHyperEdge<TIdVertex,    TRevisionIdVertex,    IProperties<TKeyVertex,    TValueVertex>,
                                                          TIdEdge,      TRevisionIdEdge,      IProperties<TKeyEdge,      TValueEdge>,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, IProperties<TKeyHyperEdge, TValueHyperEdge>>

    {

        #region Data

        // Make it more generic??!
        private readonly IDictionary<TIdVertex,    TVertexExchange>    _Vertices;
        private readonly IDictionary<TIdEdge,      TEdgeExchange>      _Edges;
        private readonly IDictionary<TIdHyperEdge, THyperEdgeExchange> _HyperEdges;

        private readonly Func<TIdVertex,
                         Action<IProperties<TKeyVertex,    TValueVertex>>,    TVertexExchange>
                         _VertexCreatorDelegate;

        private readonly Func<TVertexExchange, TVertexExchange, TIdEdge, String,
                         Action<IProperties<TKeyEdge,      TValueEdge>>,      TEdgeExchange>
                         _EdgeCreatorDelegate;

        private readonly Func<TVertexExchange, IEnumerable<TVertexExchange>, TIdHyperEdge, String,
                         Action<IProperties<TKeyHyperEdge, TValueHyperEdge>>, THyperEdgeExchange>
                         _HyperEdgeCreatorDelegate;

        //protected Map<String, TinkerIndex>          indices     = new HashMap<String, TinkerIndex>();
        //protected Map<String, TinkerAutomaticIndex> autoIndices = new HashMap<String, TinkerAutomaticIndex>();

        #endregion

        #region Constructor(s)

        #region InMemoryGraph(myTKeyVertex, myTKeyEdge, myTKeyHyperEdge)

        /// <summary>
        /// Created a new in-memory graph.
        /// </summary>
        public InMemoryGenericPropertyGraph(Func<TIdVertex,
                                            Action<IProperties<TKeyVertex,    TValueVertex>>,    TVertexExchange>
                                            myVertexCreatorDelegate,

                                            Func<TVertexExchange, TVertexExchange, TIdEdge, String,
                                            Action<IProperties<TKeyEdge,      TValueEdge>>,      TEdgeExchange>
                                            myEdgeCreatorDelegate,

                                            Func<TVertexExchange, IEnumerable<TVertexExchange>, TIdHyperEdge, String,
                                            Action<IProperties<TKeyHyperEdge, TValueHyperEdge>>, THyperEdgeExchange>
                                            myHyperEdgeCreatorDelegate,

                                            IDictionary<TIdVertex,    TVertexExchange>    myVerticesCollectionInitializer,
                                            IDictionary<TIdEdge,      TEdgeExchange>      myEdgesCollectionInitializer,
                                            IDictionary<TIdHyperEdge, THyperEdgeExchange> myHyperEdgesCollectionInitializer
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
        public TVertexExchange AddVertex(TIdVertex myVertexId = default(TIdVertex), Action<IProperties<TKeyVertex, TValueVertex>> myVertexPropertyInitializer = null)
        {

            if (myVertexId != null && _Vertices.ContainsKey(myVertexId))
                throw new ArgumentException("Another vertex with id " + myVertexId + " already exists");

            var _Vertex = _VertexCreatorDelegate(myVertexId, myVertexPropertyInitializer);

            _Vertices.Add(myVertexId, _Vertex);

            return (TVertexExchange) _Vertex;

        }

        #endregion

        #region AddVertex(myIVertex)

        /// <summary>
        /// Adds a vertex to the graph using the given VertexId and initializes
        /// its properties by invoking the given vertex initializer.
        /// </summary>
        /// <param name="myIVertex"></param>
        /// <returns>The new vertex</returns>
        public TVertexExchange AddVertex(TVertexExchange myIVertex)
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
        public TVertexExchange GetVertex(TIdVertex myVertexId)
        {

            TVertexExchange _IVertex;

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
        public IEnumerable<TVertexExchange> GetVertices(params TIdVertex[] myVertexIds)
        {

            TVertexExchange _IVertex;

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
        public IEnumerable<TVertexExchange> Vertices
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
        public IEnumerable<TVertexExchange> GetVertices(Func<TVertexExchange, Boolean> myVertexFilter = null)
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
        public void RemoveVertex(TVertexExchange myIVertex)
        {

            lock (this)
            {

                if (_Vertices.ContainsKey(myIVertex.Id))
                {

                    var _EdgeList = new List<IGenericEdge<TIdVertex,    TRevisionIdVertex,    IProperties<TKeyVertex,    TValueVertex>,
                                                          TIdEdge,      TRevisionIdEdge,      IProperties<TKeyEdge,      TValueEdge>,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, IProperties<TKeyHyperEdge, TValueHyperEdge>>>();

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
        public TEdgeExchange AddEdge(TVertexExchange myOutVertex,
                                     TVertexExchange myInVertex,
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

            return (TEdgeExchange) _Edge;

        }

        #endregion

        #region GetEdge(myEdgeId)

        /// <summary>
        /// Return the edge referenced by the given edge identifier.
        /// If no edge is referenced by that identifier, then return null.
        /// </summary>
        /// <param name="myEdgeId">The identifier of the edge.</param>
        /// <returns>The edge referenced by the provided identifier or null when no such edge exists.</returns>
        public TEdgeExchange GetEdge(TIdEdge myEdgeId)
        {

            TEdgeExchange _IEdge;

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
        public IEnumerable<TEdgeExchange> GetEdges(params TIdEdge[] myEdgeIds)
        {

            TEdgeExchange _IEdge;

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
        public IEnumerable<TEdgeExchange> Edges
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
        public IEnumerable<TEdgeExchange> GetEdges(Func<TEdgeExchange, Boolean> myEdgeFilter = null)
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
        public void RemoveEdge(TEdgeExchange myIEdge)
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
