/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET
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

    #region InMemoryGenericPropertyGraph<...>

    /// <summary>
    /// An in-memory implementation of the IGraph interface.
    /// </summary>
    public class InMemoryGenericPropertyGraph<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                              TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                              THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                              TGraphDatastructure>
       
                                              : IPropertyGraph<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                               TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                               THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                               TGraphDatastructure>

        where TDatastructureVertex    : IDictionary<TKeyVertex,    TValueVertex>
        where TDatastructureEdge      : IDictionary<TKeyEdge,      TValueEdge>
        where TDatastructureHyperEdge : IDictionary<TKeyHyperEdge, TValueHyperEdge>

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable
                                                                                                            
        where TVertexId               : IEquatable<TVertexId>,            IComparable<TVertexId>,            IComparable, TValueVertex
        where TEdgeId                 : IEquatable<TEdgeId>,              IComparable<TEdgeId>,              IComparable, TValueEdge
        where THyperEdgeId            : IEquatable<THyperEdgeId>,         IComparable<THyperEdgeId>,         IComparable, TValueHyperEdge

        where TVertexRevisionId       : IEquatable<TVertexRevisionId>,    IComparable<TVertexRevisionId>,    IComparable, TValueVertex
        where TEdgeRevisionId         : IEquatable<TEdgeRevisionId>,      IComparable<TEdgeRevisionId>,      IComparable, TValueEdge
        where THyperEdgeRevisionId    : IEquatable<THyperEdgeRevisionId>, IComparable<THyperEdgeRevisionId>, IComparable, TValueHyperEdge

    {

        #region Data

        private TKeyVertex                    _VertexIdKey;
        private TKeyEdge                      _EdgeIdKey;
        private TKeyHyperEdge                 _HyperEdgeIdKey;
                                             
        private TKeyVertex                    _VertexRevisionIdKey;
        private TKeyEdge                      _EdgeRevisionIdKey;
        private TKeyHyperEdge                 _HyperEdgeRevisionIdKey;

        private Func<TDatastructureVertex>    _VertexDatastructureInitializer;
        private Func<TDatastructureEdge>      _EdgeDatastructureInitializer;
        private Func<TDatastructureHyperEdge> _HyperEdgeDatastructureInitializer;

        private readonly IDictionary<TVertexId,    IGenericVertex   <TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                                                     TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                                                     THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>
                                                                     _Vertices;

        private readonly IDictionary<TEdgeId,      IGenericEdge     <TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                                                     TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                                                     THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>
                                                                     _Edges;

        private readonly IDictionary<THyperEdgeId, IGenericHyperEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                                                     TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                                                     THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>
                                                                     _HyperEdges;

        //protected Map<String, TinkerIndex>          indices     = new HashMap<String, TinkerIndex>();
        //protected Map<String, TinkerAutomaticIndex> autoIndices = new HashMap<String, TinkerAutomaticIndex>();

        #endregion

        #region Constructor(s)

        #region InMemoryGraph(myTKeyVertex, myTKeyEdge, myTKeyHyperEdge)

        /// <summary>
        /// Created a new in-memory graph.
        /// </summary>
        public InMemoryGenericPropertyGraph(TKeyVertex    myVertexIdKey,    TKeyVertex    myVertexRevisionIdKey,    Func<TDatastructureVertex>    myVertexDatastructureInitializer,
                                            TKeyEdge      myEdgeIdKey,      TKeyEdge      myEdgeRevisionIdKey,      Func<TDatastructureEdge>      myEdgeDatastructureInitializer,
                                            TKeyHyperEdge myHyperEdgeIdKey, TKeyHyperEdge myHyperEdgeRevisionIdKey, Func<TDatastructureHyperEdge> myHyperEdgeDatastructureInitializer)
        {

            _VertexIdKey                       = myVertexIdKey;
            _EdgeIdKey                         = myEdgeIdKey;
            _HyperEdgeIdKey                    = myHyperEdgeIdKey;
                                                
            _VertexRevisionIdKey               = myVertexRevisionIdKey;
            _EdgeRevisionIdKey                 = myEdgeRevisionIdKey;
            _HyperEdgeRevisionIdKey            = myHyperEdgeRevisionIdKey;

            _VertexDatastructureInitializer    = myVertexDatastructureInitializer;
            _EdgeDatastructureInitializer      = myEdgeDatastructureInitializer;
            _HyperEdgeDatastructureInitializer = myHyperEdgeDatastructureInitializer;

            _Vertices   = new ConcurrentDictionary<TVertexId,    IGenericVertex   <TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                                                                   TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                                                                   THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>();

            _Edges      = new ConcurrentDictionary<TEdgeId,      IGenericEdge     <TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                                                                   TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                                                                   THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>();

            _HyperEdges = new ConcurrentDictionary<THyperEdgeId, IGenericHyperEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                                                                   TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                                                                   THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>();

            //this.createIndex(Index.VERTICES, TinkerVertex.class, Index.Type.AUTOMATIC);
            //this.createIndex(Index.EDGES, TinkerEdge.class, Index.Type.AUTOMATIC);

        }

        #endregion

        #endregion


        #region Vertex methods

        #region AddVertex(myVertexId = null, myVertexInitializer = null)

        /// <summary>
        /// Adds a vertex to the graph using the given VertexId and initializes
        /// its properties by invoking the given vertex initializer.
        /// </summary>
        /// <param name="myVertexId">A VertexId. If none was given a new one will be generated.</param>
        /// <param name="myVertexInitializer">A delegate to initialize the newly generated vertex</param>
        /// <returns>The new vertex</returns>
        
        public IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                              TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                              THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>

               AddVertex(TVertexId myVertexId = default(TVertexId),
                         Action<IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                               TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                               THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>
                                               myVertexInitializer = null)

        {

            if (myVertexId != null && _Vertices.ContainsKey(myVertexId))
                throw new ArgumentException("Another vertex with id " + myVertexId + " already exists");

            //if (myVertexId == null)
            //    myVertexId = new VertexId(Guid.NewGuid().ToString());

            var _Vertex = new Vertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                     TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                     THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                     ICollection<IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                                              TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                                              THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>>
                                                              (myVertexId, _VertexRevisionIdKey, _VertexIdKey, _VertexDatastructureInitializer,
                                                              () => new HashSet<IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                                                                             TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                                                                             THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>());

            _Vertices.Add(myVertexId, _Vertex);

            return _Vertex as IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                             TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                             THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>;

        }

        #endregion

        #region AddVertex(myIVertex)

        /// <summary>
        /// Adds a vertex to the graph using the given VertexId and initializes
        /// its properties by invoking the given vertex initializer.
        /// </summary>
        /// <param name="myIVertex"></param>
        /// <returns>The new vertex</returns>
        public IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                              TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                              THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>

               AddVertex(IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                        TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                        THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>
                                        myIVertex)
        {

            if (myIVertex == null)
                throw new ArgumentNullException("myIVertex must not be null!");

            if (myIVertex.Id == null)
                throw new ArgumentNullException("The Id of myIVertex must not be null!");

            if (myIVertex != null || _Vertices.ContainsKey(myIVertex.Id))
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
        public IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                              TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                              THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>
               GetVertex(TVertexId myVertexId)
        {

            IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                           TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                           THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>
                           _IVertex;

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
        public IEnumerable<IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                          TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                          THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>
            GetVertices(params TVertexId[] myVertexIds)
        {

            IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                           TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                           THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>> _IVertex;

            foreach (var _IVertexId in myVertexIds)
                if (_IVertexId != null)
                {
                    _Vertices.TryGetValue(_IVertexId, out _IVertex);
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
        public IEnumerable<IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                          TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                          THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>

            GetVertices(Func<IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                            TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                            THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>,
                                            Boolean> myVertexFilter = null)
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
        public void RemoveVertex(TVertexId myVertexId)
        {
            RemoveVertex(GetVertex(myVertexId));
        }

        #endregion

        #region RemoveVertex(myIVertex)

        /// <summary>
        ///  Remove the given vertex from the graph
        /// </summary>
        /// <param name="myIVertex">A vertex</param>
        public void RemoveVertex(IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                                TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                                THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>
                                                myIVertex)
        {

            lock (this)
            {

                if (_Vertices.ContainsKey(myIVertex.Id))
                {

                    var _EdgeList = new List<IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                                          TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                                          THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>();

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

        #region AddEdge(myOutVertex, myInVertex, myEdgeId = null, myLabel = null, myEdgeInitializer = null)

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
        public IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                            TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                            THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>

            AddEdge(IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                   TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                   THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>> myOutVertex,

                    IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                   TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                   THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>> myInVertex,

                    TEdgeId myEdgeId = default(TEdgeId),
                    String  myLabel  = null,

                    Action<IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                        TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                        THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>> myEdgeInitializer = null)

        {

            if (myEdgeId != null && _Edges.ContainsKey(myEdgeId))
                throw new ArgumentException("Another edge with id " + myEdgeId + " already exists!");

            //if (myEdgeId == null)
            //    myEdgeId = new EdgeId(Guid.NewGuid().ToString());

            var _Edge = new Edge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                 TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                 THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                 ICollection<IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                                            TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                                            THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>>
                                 (myOutVertex, myInVertex, myEdgeId, myLabel, _EdgeIdKey, _EdgeRevisionIdKey, _EdgeDatastructureInitializer,
                                  () => new HashSet<IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                                                   TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                                                   THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>(),
                                  myEdgeInitializer);

            _Edges.Add(myEdgeId, _Edge);

            myOutVertex.AddOutEdge(_Edge);
            myInVertex.AddInEdge(_Edge);

            return _Edge as IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                         TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                         THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>;

        }

        #endregion

        #region GetEdge(myEdgeId)

        /// <summary>
        /// Return the edge referenced by the given edge identifier.
        /// If no edge is referenced by that identifier, then return null.
        /// </summary>
        /// <param name="myEdgeId">The identifier of the edge.</param>
        /// <returns>The edge referenced by the provided identifier or null when no such edge exists.</returns>
        public IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                            TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                            THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>
            GetEdge(TEdgeId myEdgeId)
        {

            IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                         TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                         THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>> _IEdge;

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
        public IEnumerable<IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                        TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                        THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>

            GetEdges(params TEdgeId[] myEdgeIds)

        {

            IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                         TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                         THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>> _IEdge;

            foreach (var _IEdgeId in myEdgeIds)
                if (_IEdgeId != null)
                {
                    _Edges.TryGetValue(_IEdgeId, out _IEdge);
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
        public IEnumerable<IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                        TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                        THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>
            
            GetEdges(Func<IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                       TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                       THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>, Boolean> myEdgeFilter = null)
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
        public void RemoveEdge(TEdgeId myEdgeId)
        {
            RemoveEdge(GetEdge(myEdgeId));
        }

        #endregion

        #region RemoveEdge(myIEdge)

        /// <summary>
        ///  Remove the given edge from the graph
        /// </summary>
        /// <param name="myIEdge">An edge</param>
        public void RemoveEdge(IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                            TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                            THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>> myIEdge)
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

    #region InMemoryPropertyGraph<TId, TRevisionId>

    /// <summary>
    /// A generic in-memory implementation of a property graph
    /// which allow to change the types of the Id and RevisionId
    /// properties.
    /// </summary>
    /// <typeparam name="TId">The type of the Id property.</typeparam>
    /// <typeparam name="TRevisionId">The type of the TRevisionId property.</typeparam>
    public class InMemoryPropertyGraph<TId, TRevisionId> : InMemoryGenericPropertyGraph<TId, TRevisionId, String, Object, IDictionary<String, Object>,
                                                                                        TId, TRevisionId, String, Object, IDictionary<String, Object>,
                                                                                        TId, TRevisionId, String, Object, IDictionary<String, Object>,
                                                                                        Object>

        where TId         : IEquatable<TId>,         IComparable<TId>,         IComparable
        where TRevisionId : IEquatable<TRevisionId>, IComparable<TRevisionId>, IComparable
    {

        #region Constructor(s)

        #region InMemoryPropertyGraph()

        /// <summary>
        /// Created a new in-memory property graph.
        /// </summary>
        public InMemoryPropertyGraph()
            : base("Id", "RevId", () => new Dictionary<String, Object>(),
                   "Id", "RevId", () => new Dictionary<String, Object>(),
                   "Id", "RevId", () => new Dictionary<String, Object>())
        { }

        #endregion

        #endregion

    }

    #endregion

}
