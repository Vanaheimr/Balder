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
using de.ahzf.Blueprints.PropertyGraph.ReadOnly;
using de.ahzf.Blueprints.PropertyGraph.ReadOnly.Indices;

#endregion

namespace de.ahzf.Blueprints.PropertyGraph.InMemory.ReadOnly
{

    #region ReadOnlyPropertyGraph<...>

    /// <summary>
    /// A read-only in-memory implementation of the IGraph interface.
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
    public class ReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>

                                       : AReadOnlyGraphElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex, TDatastructureVertex>,

                                         IReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>,

                                         IDynamicGraphElement<ReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
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

        protected readonly Boolean SyncedVertexIds;
        protected readonly Boolean SyncedEdgeIds;
        protected readonly Boolean SyncedHyperEdgeIds;

        private readonly Func<TDatastructureVertex> _DatastructureInitializer;

        /// <summary>
        /// The cached number of vertices.
        /// </summary>
        protected readonly UInt64 _NumberOfVertices;

        /// <summary>
        /// The cached number of edges.
        /// </summary>
        protected readonly UInt64 _NumberOfEdges;

        /// <summary>
        /// The cached number of hyperedges.
        /// </summary>
        protected readonly UInt64 _NumberOfHyperEdges;


        protected readonly IReadOnlyPropertyVertex   <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] _Vertices;

        protected readonly IReadOnlyPropertyEdge     <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] _Edges;

        protected readonly IReadOnlyPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] _HyperEdges;



        // Make IDictionaries more generic??!

        /// <summary>
        /// A mapping of VertexIds to their place within the vertex array.
        /// </summary>
        protected readonly IDictionary<TIdVertex,    UInt64> _VertexIdIndex;

        /// <summary>
        /// A mapping of EdgeIds to their place within the edge array.
        /// </summary>
        protected readonly IDictionary<TIdEdge,      UInt64> _EdgeIdIndex;

        /// <summary>
        /// A mapping of HyperEdgeIds to their place within the HyperEdge array.
        /// </summary>
        protected readonly IDictionary<TIdHyperEdge, UInt64> _HyperEdgeIdIndex;


        private readonly IDictionary<String, IReadOnlyPropertyElementIndex<IReadOnlyPropertyVertex   <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> _ManualVerticesIndices;
        private readonly IDictionary<String, IReadOnlyPropertyElementIndex<IReadOnlyPropertyVertex   <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> _AutomaticVerticesIndices;

        private readonly IDictionary<String, IReadOnlyPropertyElementIndex<IReadOnlyPropertyEdge     <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> _ManualEdgesIndices;
        private readonly IDictionary<String, IReadOnlyPropertyElementIndex<IReadOnlyPropertyEdge     <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> _AutomaticEdgesIndices;

        private readonly IDictionary<String, IReadOnlyPropertyElementIndex<IReadOnlyPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,      TValueVertex,
                                                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> _ManualHyperEdgesIndices;
        private readonly IDictionary<String, IReadOnlyPropertyElementIndex<IReadOnlyPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,      TValueVertex,
                                                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> _AutomaticHyperEdgesIndices;

        #endregion

        #region Constructor(s)

        #region ReadOnlyPropertyGraph(GraphId, IPropertyGraph)

        /// <summary>
        /// Created a new genric and in-memory property graph.
        /// </summary>
        /// <param name="GraphId">A unique identification for this graph.</param>
        /// <param name="IPropertyGraph">A property graph to copy the data from.</param>
        public ReadOnlyPropertyGraph(TIdVertex GraphId,
                                     IPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IPropertyGraph,
                                            //Func<TDatastructureVertex>   DataInitializer,

                                            ReadOnlyVertexCreatorDelegate     <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexCreatorDelegate,
                                                                      
                                            ReadOnlyEdgeCreatorDelegate     <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeCreatorDelegate,

                                            //HyperEdgeCreatorDelegate  <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                            //                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            //                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeCreatorDelegate,

                                            //GraphInitializer<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                            //                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            //                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GraphInitializer = null,

                                     UInt64  NumberOfVertices   = 0,
                                     Boolean SyncedVertexIds    = false,
                                     UInt64  NumberOfEdges      = 0,
                                     Boolean SyncedEdgeIds      = false,
                                     UInt64  NumberOfHyperEdges = 0,
                                     Boolean SyncedHyperEdgeIds = false)

            : base(GraphId, IPropertyGraph.IdKey, IPropertyGraph.RevIdKey, null)

        {

            this.SyncedVertexIds    = SyncedVertexIds;
            this.SyncedEdgeIds      = SyncedEdgeIds;
            this.SyncedHyperEdgeIds = SyncedHyperEdgeIds;

            _NumberOfVertices   = (NumberOfVertices   > 0) ? NumberOfVertices   : IPropertyGraph.NumberOfVertices();
            _NumberOfEdges      = (NumberOfEdges      > 0) ? NumberOfEdges      : IPropertyGraph.NumberOfEdges();
            _NumberOfHyperEdges = (NumberOfHyperEdges > 0) ? NumberOfHyperEdges : IPropertyGraph.NumberOfHyperEdges();

            //ToDo: Remove size limitations!
            _Vertices   = new IReadOnlyPropertyVertex   <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[_NumberOfVertices];

            _Edges      = new IReadOnlyPropertyEdge     <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[_NumberOfEdges];

            _HyperEdges = new IReadOnlyPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[_NumberOfHyperEdges];

            #region Copy Vertices

            if (SyncedVertexIds)
            {

                if (typeof(TIdVertex) != typeof(Int32)  ||
                    typeof(TIdVertex) != typeof(UInt32) ||
                    typeof(TIdVertex) != typeof(Int64)  ||
                    typeof(TIdVertex) != typeof(UInt64))
                {
                    foreach (var _Vertex in IPropertyGraph.Vertices())
                        _Vertices[(UInt64) (Object) _Vertex.Id] = VertexCreatorDelegate(this, _Vertex);
                }

                else
                    throw new ArgumentException("The type of the VertexId must be an integer!");

            }

            else
            {
                
                _VertexIdIndex = new Dictionary<TIdVertex, UInt64>();
                var i = 0UL;

                foreach (var _Vertex in IPropertyGraph.Vertices())
                {
                    _VertexIdIndex.Add(_Vertex.Id, i);
                    _Vertices[i++] = VertexCreatorDelegate(this, _Vertex);
                }

            }

            #endregion

            #region Copy Edges

            if (SyncedEdgeIds)
            {

                if (typeof(TIdVertex) != typeof(Int32)  ||
                    typeof(TIdVertex) != typeof(UInt32) ||
                    typeof(TIdVertex) != typeof(Int64)  ||
                    typeof(TIdVertex) != typeof(UInt64))
                {
                    foreach (var _Edge in IPropertyGraph.Edges())
                        _Edges[(UInt64) (Object) _Edge.Id] = EdgeCreatorDelegate(this, _Edge);
                }

                else
                    throw new ArgumentException("The type of the EdgeId must be an integer!");

            }

            else
            {

                _EdgeIdIndex = new Dictionary<TIdEdge, UInt64>();
                var i = 0UL;

                foreach (var _Edge in IPropertyGraph.Edges())
                {
                    _EdgeIdIndex.Add(_Edge.Id, i);
                    _Edges[i++] = EdgeCreatorDelegate(this, _Edge);
                }

            }

            #endregion

            #region Copy HyperEdges

            if (SyncedHyperEdgeIds)
            {

                if (typeof(TIdVertex) != typeof(Int32)  ||
                    typeof(TIdVertex) != typeof(UInt32) ||
                    typeof(TIdVertex) != typeof(Int64)  ||
                    typeof(TIdVertex) != typeof(UInt64))
                {
                    foreach (var _HyperEdge in IPropertyGraph.HyperEdges())
                        _HyperEdges[(UInt64) (Object) _HyperEdge.Id] = null;// _HyperEdge;
                }

                else
                    throw new ArgumentException("The type of the HyperEdgeId must be an integer!");

            }

            else
            {
                
                _HyperEdgeIdIndex = new Dictionary<TIdHyperEdge, UInt64>();
                var i = 0UL;

                foreach (var _HyperEdge in IPropertyGraph.HyperEdges())
                {
                    _HyperEdgeIdIndex.Add(_HyperEdge.Id, i);
                    _HyperEdges[i++] = null;// _HyperEdge;
                }

            }

            #endregion


            _ManualVerticesIndices      = new Dictionary<String, IReadOnlyPropertyElementIndex<IReadOnlyPropertyVertex   <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>();
            _AutomaticVerticesIndices   = new Dictionary<String, IReadOnlyPropertyElementIndex<IReadOnlyPropertyVertex   <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>();

            _ManualEdgesIndices         = new Dictionary<String, IReadOnlyPropertyElementIndex<IReadOnlyPropertyEdge     <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>();
            _AutomaticEdgesIndices      = new Dictionary<String, IReadOnlyPropertyElementIndex<IReadOnlyPropertyEdge     <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>();

            _ManualHyperEdgesIndices    = new Dictionary<String, IReadOnlyPropertyElementIndex<IReadOnlyPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>();
            _AutomaticHyperEdgesIndices = new Dictionary<String, IReadOnlyPropertyElementIndex<IReadOnlyPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>();

        }

        #endregion

        #endregion


        #region Vertex methods

        #region VerticesById(params VertexIds)

        /// <summary>
        /// Return the vertices referenced by the given array of vertex identifiers.
        /// If no vertex is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="VertexIds">An array of vertex identifiers.</param>
        public virtual IEnumerable<IReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                                                           VerticesById(params TIdVertex[] VertexIds)

        {

            if (VertexIds == null || !VertexIds.Any())
                throw new ArgumentNullException("The array of vertex identifiers must not be null or its length zero!");

            UInt64 _VertexIndexId;

            foreach (var _VertexId in VertexIds)
            {
                if (_VertexId != null)
                {
                    _VertexIdIndex.TryGetValue(_VertexId, out _VertexIndexId);
                    yield return _Vertices[_VertexIndexId];
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
        public virtual IEnumerable<IReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
               Vertices(ReadOnlyVertexFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexFilter = null)

        {

            if (VertexFilter == null)
                foreach (var _Vertex in _Vertices)
                    yield return _Vertex;

            else
                foreach (var _Vertex in _Vertices)
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
        public UInt64 NumberOfVertices(ReadOnlyVertexFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexFilter = null)
        {

            if (VertexFilter == null)
                return _NumberOfVertices;

            else
            {

                var _Counter = 0UL;

                foreach (var _Vertex in _Vertices)
                    if (VertexFilter(_Vertex))
                        _Counter++;

                return _Counter;

            }

        }

        #endregion

        #endregion

        #region Edge methods

        #region EdgesById(params EdgeIds)

        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// An additional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="EdgeIds">An array of edge identifiers.</param>
        public IEnumerable<IReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            EdgesById(params TIdEdge[] EdgeIds)

        {

            if (EdgeIds == null || !EdgeIds.Any())
                throw new ArgumentNullException("The given array of edge identifiers must not be null or its length zero!");

            UInt64 _EdgeIndexId;

            foreach (var _EdgeId in EdgeIds)
                if (_EdgeId != null)
                {
                    _EdgeIdIndex.TryGetValue(_EdgeId, out _EdgeIndexId);
                    yield return _Edges[_EdgeIndexId];
                }

        }

        #endregion

        #region Edges(EdgeFilter = null)

        /// <summary>
        /// Return an enumeration of all edges in the graph.
        /// An optional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        public IEnumerable<IReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                 Edges(ReadOnlyEdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter = null)

        {

            if (EdgeFilter == null)
                foreach (var _Edge in _Edges)
                    yield return _Edge;

            else foreach (var _Edge in _Edges)
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
        public UInt64 NumberOfEdges(ReadOnlyEdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter = null)
        {

            if (EdgeFilter == null)
                return _NumberOfEdges;

            else
            {

                var _Counter = 0UL;

                foreach (var _Edge in _Edges)
                    if (EdgeFilter(_Edge))
                        _Counter++;

                return _Counter;

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
        public IEnumerable<IReadOnlyPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                                                      HyperEdgesById(params TIdHyperEdge[] HyperEdgeIds)
        {
            
            if (HyperEdgeIds == null || !HyperEdgeIds.Any())
                throw new ArgumentNullException("The given array of HyperEdge identifiers must not be null or its length zero!");

            UInt64 _HyperEdgeIndexId;

            foreach (var _HyperEdgeId in HyperEdgeIds)
            {
                if (_HyperEdgeId != null)
                {
                    _HyperEdgeIdIndex.TryGetValue(_HyperEdgeId, out _HyperEdgeIndexId);
                    yield return _HyperEdges[_HyperEdgeIndexId];
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
        public IEnumerable<IReadOnlyPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                                                      HyperEdges(ReadOnlyHyperEdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter = null)
        {

            if (HyperEdgeFilter == null)
                foreach (var _HyperEdge in _HyperEdges)
                    yield return _HyperEdge;

            else
                foreach (var _HyperEdge in _HyperEdges)
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
        public UInt64 NumberOfHyperEdges(ReadOnlyHyperEdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter = null)
        {

            if (HyperEdgeFilter == null)
                return _NumberOfHyperEdges;

            else
            {

                var _Counter = 0UL;

                foreach (var _HyperEdge in _HyperEdges)
                    if (HyperEdgeFilter(_HyperEdge))
                        _Counter++;

                return _Counter;

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


        #region VerticesIndices(IndexFilter = null)

        /// <summary>
        /// Get all vertices indices maintained by the graph.
        /// </summary>
        /// <param name="IndexFilter">An optional index filter.</param>
        /// <returns>The indices associated with the graph.</returns>
        public IEnumerable<IReadOnlyPropertyElementIndex<IReadOnlyPropertyVertex<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>

               VerticesIndices(ReadOnlyIndexFilter<IReadOnlyPropertyVertex<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
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
        public IEnumerable<IReadOnlyPropertyElementIndex<T>> VerticesIndices<T>(ReadOnlyIndexFilter<T> IndexFilter = null)
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
        public IEnumerable<IReadOnlyPropertyElementIndex<IReadOnlyPropertyEdge<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>

               EdgesIndices(ReadOnlyIndexFilter<IReadOnlyPropertyEdge<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
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
        public IEnumerable<IReadOnlyPropertyElementIndex<T>> EdgesIndices<T>(ReadOnlyIndexFilter<T> IndexFilter = null)
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
        public IEnumerable<IReadOnlyPropertyElementIndex<IReadOnlyPropertyHyperEdge<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>

            HyperEdgesIndices(ReadOnlyIndexFilter<IReadOnlyPropertyHyperEdge<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
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
        public IEnumerable<IReadOnlyPropertyElementIndex<T>> HyperEdgesIndices<T>(ReadOnlyIndexFilter<T> IndexFilter = null)
            where T : IEquatable<T>, IComparable<T>, IComparable
        {
            throw new NotImplementedException();
        }

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
        public static Boolean operator == (ReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                                        PropertyGraph1,
                                           ReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
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
        public static Boolean operator != (ReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                                        PropertyGraph1,
                                           ReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
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
        public static Boolean operator < (ReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                                       PropertyGraph1,
                                          ReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
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
        public static Boolean operator <= (ReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                                        PropertyGraph1,
                                           ReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
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
        public static Boolean operator > (ReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                                       PropertyGraph1,
                                          ReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
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
        public static Boolean operator >= (ReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                                        PropertyGraph1,
                                           ReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
                                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                                        PropertyGraph2)
        {
            return !(PropertyGraph1 < PropertyGraph2);
        }

        #endregion

        #endregion

        #region IDynamicGraphObject<ReadOnlyPropertyGraph> Members

        #region GetMetaObject(myExpression)

        /// <summary>
        /// Return the appropriate DynamicMetaObject.
        /// </summary>
        /// <param name="myExpression">An Expression.</param>
        public DynamicMetaObject GetMetaObject(Expression myExpression)
        {
            return new DynamicGraphElement<ReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
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
            throw new NotSupportedException("This data structure is read-only!");
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
            throw new NotSupportedException("This data structure is read-only!");
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

            // Check if the given object can be casted to a ReadOnlyPropertyGraph
            var PropertyGraph = Object as ReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex, TEdgeCollection,
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
            return "PropertyGraph [Id: " + Id.ToString() + ", " + _VertexIdIndex.Count() + " Vertices, " + _EdgeIdIndex.Count() + " Edges]";
        }

        #endregion

    }

    #endregion

    #region ReadOnlyPropertyGraph

    /// <summary>
    /// A read-only in-memory implementation of a property graph.
    /// </summary>
    public class ReadOnlyPropertyGraph : ReadOnlyPropertyGraph<// Vertices definition
                                                               VertexId,    RevisionId,         String, Object, IDictionary<String, Object>,
                                                               ICollection<IPropertyEdge<VertexId,    RevisionId,         String, Object,
                                                                                         EdgeId,      RevisionId, String, String, Object,
                                                                                         HyperEdgeId, RevisionId, String, String, Object>>,

                                                               // Edges definition
                                                               EdgeId,      RevisionId, String, String, Object, IDictionary<String, Object>,

                                                               // Hyperedges definition
                                                               HyperEdgeId, RevisionId, String, String, Object, IDictionary<String, Object>>,
                                        IReadOnlyPropertyGraph
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

        #region ReadOnlyPropertyGraph(IPropertyGraph)

        /// <summary>
        /// Created a new in-memory property graph.
        /// </summary>
        /// <param name="IPropertyGraph">A property graph to copy the data from.</param>
        public ReadOnlyPropertyGraph(IPropertyGraph<VertexId,    RevisionId,         String, Object,
                                                    EdgeId,      RevisionId, String, String, Object,
                                                    HyperEdgeId, RevisionId, String, String, Object> IPropertyGraph,
                                     UInt64  NumberOfVertices   = 0,
                                     Boolean SyncedVertexIds    = false,
                                     UInt64  NumberOfEdges      = 0,
                                     Boolean SyncedEdgeIds      = false,
                                     UInt64  NumberOfHyperEdges = 0,
                                     Boolean SyncedHyperEdgeIds = false)
            : this(new VertexId(), IPropertyGraph, NumberOfVertices, SyncedVertexIds, NumberOfEdges, SyncedEdgeIds, NumberOfHyperEdges, SyncedHyperEdgeIds)

        { }

        #endregion

        #region ReadOnlyPropertyGraph(GraphId, IPropertyGraph)

        /// <summary>
        /// Created a new in-memory property graph.
        /// </summary>
        /// <param name="GraphId">A unique identification for this graph.</param>
        /// <param name="IPropertyGraph">A property graph to copy the data from.</param>
        public ReadOnlyPropertyGraph(VertexId GraphId,
                                     IPropertyGraph<VertexId,    RevisionId,         String, Object,
                                                    EdgeId,      RevisionId, String, String, Object,
                                                    HyperEdgeId, RevisionId, String, String, Object> IPropertyGraph,
                                     UInt64  NumberOfVertices   = 0,
                                     Boolean SyncedVertexIds    = false,
                                     UInt64  NumberOfEdges      = 0,
                                     Boolean SyncedEdgeIds      = false,
                                     UInt64  NumberOfHyperEdges = 0,
                                     Boolean SyncedHyperEdgeIds = false)
            : base(GraphId,
                   IPropertyGraph,
                   (_Graph, _Vertex) => new ReadOnlyPropertyVertex<VertexId,    RevisionId,         String, Object, IDictionary<String, Object>,
                                                                   EdgeId,      RevisionId, String, String, Object, IDictionary<String, Object>,
                                                                   HyperEdgeId, RevisionId, String, String, Object, IDictionary<String, Object>,
                                                                   ICollection<IReadOnlyPropertyEdge<VertexId,    RevisionId,         String, Object,
                                                                                                     EdgeId,      RevisionId, String, String, Object,
                                                                                                     HyperEdgeId, RevisionId, String, String, Object>>>(
                                                                                                     _Graph, _Vertex, null, () => new List<IReadOnlyPropertyEdge<VertexId,    RevisionId,         String, Object,
                                                                                                                                                                 EdgeId,      RevisionId, String, String, Object,
                                                                                                                                                                 HyperEdgeId, RevisionId, String, String, Object>>()),
                   (_Graph, _Edge) => new ReadOnlyPropertyEdge<VertexId,    RevisionId,         String, Object, IDictionary<String, Object>,
                                                               EdgeId,      RevisionId, String, String, Object, IDictionary<String, Object>,
                                                               HyperEdgeId, RevisionId, String, String, Object, IDictionary<String, Object>>(
                                                                                                     _Graph, _Edge, null),
                   NumberOfVertices, SyncedVertexIds, NumberOfEdges, SyncedEdgeIds, NumberOfHyperEdges, SyncedHyperEdgeIds)

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
        public static Boolean operator == (ReadOnlyPropertyGraph PropertyGraph1,
                                           ReadOnlyPropertyGraph PropertyGraph2)
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
        public static Boolean operator != (ReadOnlyPropertyGraph PropertyGraph1,
                                           ReadOnlyPropertyGraph PropertyGraph2)
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
        public static Boolean operator < (ReadOnlyPropertyGraph PropertyGraph1,
                                          ReadOnlyPropertyGraph PropertyGraph2)
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
        public static Boolean operator <= (ReadOnlyPropertyGraph PropertyGraph1,
                                           ReadOnlyPropertyGraph PropertyGraph2)
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
        public static Boolean operator > (ReadOnlyPropertyGraph PropertyGraph1,
                                          ReadOnlyPropertyGraph PropertyGraph2)
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
        public static Boolean operator >= (ReadOnlyPropertyGraph PropertyGraph1,
                                           ReadOnlyPropertyGraph PropertyGraph2)
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

            // Check if the given object can be casted to a ReadOnlyPropertyGraph
            var PropertyGraph = Object as ReadOnlyPropertyGraph;
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
