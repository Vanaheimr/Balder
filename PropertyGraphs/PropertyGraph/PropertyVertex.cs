/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <code@ahzf.de>
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
    public class PropertyVertex : GenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object>,
                                  IPropertyVertex,
                                  IPropertyGraph,
                                  IDynamicGraphElement<PropertyVertex>

    {

        #region Properties

        #region Graph

        /// <summary>
        /// The associated property graph.
        /// </summary>
        IPropertyGraph IPropertyVertex.Graph
        {
            get
            {
                return Graph as IPropertyGraph;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region (internal) PropertyVertex(VertexId, IdKey, RevIdKey, DescriptionKey, DatastructureInitializer)

        /// <summary>
        /// The PropertyVertex constructor for creating a new PropertyGraph.
        /// </summary>
        /// <param name="VertexId"></param>
        /// <param name="IdKey"></param>
        /// <param name="RevIdKey"></param>
        /// <param name="DescriptionKey"></param>
        /// <param name="DatastructureInitializer"></param>
        /// <param name="VertexIdCreatorDelegate"></param>
        /// <param name="VertexCreatorDelegate"></param>
        /// <param name="VerticesCollectionInitializer"></param>
        /// <param name="EdgeIdCreatorDelegate"></param>
        /// <param name="EdgeCreatorDelegate"></param>
        /// <param name="EdgesCollectionInitializer"></param>
        /// <param name="MultiEdgeIdCreatorDelegate"></param>
        /// <param name="MultiEdgeCreatorDelegate"></param>
        /// <param name="MultiEdgesCollectionInitializer"></param>
        /// <param name="HyperEdgeIdCreatorDelegate"></param>
        /// <param name="HyperEdgeCreatorDelegate"></param>
        /// <param name="HyperEdgesCollectionInitializer"></param>
        internal PropertyVertex(UInt64  VertexId,
                                String  IdKey,
                                String  RevIdKey,
                                String  DescriptionKey,
                                IDictionaryInitializer<String, Object> DatastructureInitializer,

                                #region Create a new vertex

                                       VertexIdCreatorDelegate   <UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object> VertexIdCreatorDelegate,

                                       VertexCreatorDelegate     <UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object> VertexCreatorDelegate,

                                       Func<IGroupedCollection<String, UInt64, IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object>>>
                                       VerticesCollectionInitializer,

                                       #endregion

                                #region Create a new edge

                                EdgeIdCreatorDelegate     <UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object> EdgeIdCreatorDelegate,

                                EdgeCreatorDelegate       <UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object> EdgeCreatorDelegate,

                                Func<IGroupedCollection<String, UInt64, IGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object>>>
                                EdgesCollectionInitializer,

                                #endregion

                                #region Create a new multiedge

                                MultiEdgeIdCreatorDelegate<UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object> MultiEdgeIdCreatorDelegate,

                                MultiEdgeCreatorDelegate  <UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object> MultiEdgeCreatorDelegate,

                                Func<IGroupedCollection<String, UInt64, IGenericPropertyMultiEdge<UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object>>>
                                MultiEdgesCollectionInitializer,

                                #endregion

                                #region Create a new hyperedge

                                HyperEdgeIdCreatorDelegate<UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object> HyperEdgeIdCreatorDelegate,

                                HyperEdgeCreatorDelegate  <UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object> HyperEdgeCreatorDelegate,

                                Func<IGroupedCollection<String, UInt64, IGenericPropertyHyperEdge<UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object>>>
                                HyperEdgesCollectionInitializer)

                                #endregion

            : base(VertexId, IdKey, RevIdKey, DescriptionKey, DatastructureInitializer,
                   VertexIdCreatorDelegate,    VertexCreatorDelegate,    VerticesCollectionInitializer,
                   EdgeIdCreatorDelegate,      EdgeCreatorDelegate,      EdgesCollectionInitializer,
                   MultiEdgeIdCreatorDelegate, MultiEdgeCreatorDelegate, MultiEdgesCollectionInitializer,
                   HyperEdgeIdCreatorDelegate, HyperEdgeCreatorDelegate, HyperEdgesCollectionInitializer)

        { }

        #endregion

        #region PropertyVertex(Graph, VertexId, IdKey, RevIdKey, DescriptionKey, DatastructureInitializer, EdgeCollectionInitializer, HyperEdgeCollectionInitializer, VertexInitializer = null)

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
        public PropertyVertex(IPropertyGraph Graph,
                              UInt64         VertexId,
                              String         IdKey,
                              String         RevIdKey,
                              String         DescriptionKey,

                              IDictionaryInitializer<String, Object> DatastructureInitializer,

                              Func<IGroupedCollection<String, UInt64, IGenericPropertyVertex   <UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object>>> VerticesCollectionInitializer,

                              Func<IGroupedCollection<String, UInt64, IGenericPropertyEdge     <UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object>>> EdgesCollectionInitializer,

                              Func<IGroupedCollection<String, UInt64, IGenericPropertyMultiEdge<UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object>>> MultiEdgesCollectionInitializer,

                              Func<IGroupedCollection<String, UInt64, IGenericPropertyHyperEdge<UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object>>> HyperEdgesCollectionInitializer,

                              VertexInitializer<UInt64, Int64, String, String, Object,
                                                UInt64, Int64, String, String, Object,
                                                UInt64, Int64, String, String, Object,
                                                UInt64, Int64, String, String, Object> VertexInitializer = null)

            : base(Graph, VertexId, IdKey, RevIdKey, DescriptionKey, DatastructureInitializer,
                   VerticesCollectionInitializer, EdgesCollectionInitializer, MultiEdgesCollectionInitializer, HyperEdgesCollectionInitializer,
                   VertexInitializer)

        { }

        #endregion

        #endregion


        //// IGenericPropertyVertex

        //#region OutEdge methods...

        //#region AddOutEdge(Edge)

        ///// <summary>
        ///// Add an outgoing edge.
        ///// </summary>
        ///// <param name="Edge">The edge to add.</param>
        //void IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

        //            AddOutEdge(IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        //                                            Edge)
        //{

        //    if (SendOutEdgeAddingVote(Edge))
        //    {
        //        _OutEdges.TryAddValue(Edge.Label, Edge.Id, Edge);    // Is supposed to be thread-safe!
        //        Interlocked.Increment(ref _NumberOfOutEdges);
        //        SendOutEdgeAddedNotification(Edge);
        //    }
            
        //}

        //#endregion


        //#region OutEdges(params EdgeLabels)      // OutEdges()!

        ///// <summary>
        ///// The edges emanating from, or leaving, this vertex
        ///// filtered by their label. If no label was given,
        ///// all edges will be returned.
        ///// </summary>
        //IEnumerable<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

        //    IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //    OutEdges(params TEdgeLabel[] EdgeLabels)

        //{

        //    if (EdgeLabels != null && EdgeLabels.Any())
        //    {
        //        foreach (var _Edge in _OutEdges)
        //            foreach (var _Label in EdgeLabels)
        //                if (_Edge.Label.Equals(_Label))
        //                    yield return _Edge;
        //    }

        //    else
        //        foreach (var _Edge in _OutEdges)
        //            yield return _Edge;

        //}

        //#endregion

        //#region OutEdges(EdgeFilter)

        ///// <summary>
        ///// The edges emanating from, or leaving, this vertex
        ///// filtered by the given edge filter delegate.
        ///// </summary>
        //IEnumerable<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
        
        //    IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

        //    OutEdges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)
        
        //{
        //    return from _Edge in _OutEdges where EdgeFilter(_Edge) select _Edge;
        //}

        //#endregion

        //#region OutDegree(params EdgeLabels)     // OutDegree()!

        ///// <summary>
        ///// The number of edges emanating from, or leaving, this vertex
        ///// filtered by their label. If no label was given,
        ///// all edges will be returned.
        ///// </summary>
        //UInt64 IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.            

        //       OutDegree(params TEdgeLabel[] EdgeLabels)

        //{
            
        //    if (EdgeLabels.Length == 0)
        //        return (UInt64) _NumberOfOutEdges;

        //    var Counter = 0UL;

        //    if (EdgeLabels != null && EdgeLabels.Any())
        //    {
        //        foreach (var _Edge in _OutEdges)
        //            foreach (var _Label in EdgeLabels)
        //                if (_Edge.Label.Equals(_Label))
        //                    Counter++;
        //    }

        //    else
        //        foreach (var _Edge in _OutEdges)
        //            Counter++;

        //    return Counter;

        //}

        //#endregion

        //#region OutDegree(EdgeFilter)

        ///// <summary>
        ///// The number of edges emanating from, or leaving, this vertex
        ///// filtered by the given edge filter delegate.
        ///// </summary>
        //UInt64 IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

        //       OutDegree(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)

        //{

        //    if (EdgeFilter == null)
        //        return (UInt64) _NumberOfOutEdges;

        //    return (UInt64) (from _Edge in _OutEdges where EdgeFilter(_Edge) select _Edge).Count();

        //}

        //#endregion


        //#region RemoveOutEdges(params Edges)    // RemoveOutEdges()!

        ///// <summary>
        ///// Remove outgoing edges.
        ///// </summary>
        ///// <param name="Edges">An array of outgoing edges to be removed.</param>
        //void IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.            
            
        //     RemoveOutEdges(params IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[]
        //                                                Edges)

        //{

        //    if (Edges.Any())
        //    {
        //        foreach (var _Edge in Edges)
        //        {
        //            _OutEdges.TryRemoveValue(_Edge.Label, _Edge.Id, _Edge);    // Is supposed to be thread-safe!
        //            Interlocked.Decrement(ref _NumberOfOutEdges);
        //        }
        //    }
        //    else
        //    {
        //        lock (this)
        //        {
        //            _OutEdges.Clear();
        //            _NumberOfOutEdges = 0;
        //        }
        //    }

        //}

        //#endregion

        //#region RemoveOutEdges(EdgeFilter)

        ///// <summary>
        ///// Remove any outgoing edge matching the
        ///// given edge filter delegate.
        ///// </summary>
        ///// <param name="EdgeFilter">A delegate for edge filtering.</param>
        //void IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.            
            
        //     RemoveOutEdges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)

        //{

        //    if (EdgeFilter == null)
        //        throw new ArgumentNullException("The given edge filter delegate must not be null!");

        //    lock (this)
        //    {

        //        var _tmp = new List<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>();

        //        if (EdgeFilter == null)
        //            foreach (var _IEdge in _OutEdges)
        //                _tmp.Add(_IEdge);

        //        else foreach (var _IEdge in _OutEdges)
        //            if (EdgeFilter(_IEdge))
        //                _tmp.Add(_IEdge);

        //        foreach (var _Edge in _tmp)
        //        {
        //            _OutEdges.TryRemoveValue(_Edge.Label, _Edge.Id, _Edge);
        //            Interlocked.Decrement(ref _NumberOfOutEdges);
        //        }

        //    }

        //}

        //#endregion

        //#endregion

        //#region InEdge methods...

        //#region AddInEdge(Edge)

        ///// <summary>
        ///// Add an incoming edge.
        ///// </summary>
        ///// <param name="Edge">The edge to add.</param>
        //void IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.            
            
        //     AddInEdge(IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        //                                    Edge)

        //{

        //    if (SendInEdgeAddingVote(Edge))
        //    {

        //        _InEdges.TryAddValue(Edge.Label, Edge.Id, Edge);     // Is supposed to be thread-safe!
        //        Interlocked.Increment(ref _NumberOfInEdges);

        //        foreach (var _MultiEdge in _MultiEdges)
        //            _MultiEdge.AddIfMatches(Edge); 
                
        //        SendInEdgeAddedNotification(Edge);

        //    }

        //}

        //#endregion


        //#region InEdges(params EdgeLabels)      // InEdges()!

        ///// <summary>
        ///// The edges incoming to, or arriving at, this vertex
        ///// filtered by their label. If no label was given,
        ///// all edges will be returned.
        ///// </summary>
        //IEnumerable<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

        //    IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

        //    InEdges(params TEdgeLabel[] EdgeLabels)

        //{

        //    if (EdgeLabels != null && EdgeLabels.Any())
        //    {
        //        foreach (var _Edge in _InEdges)
        //            foreach (var _Label in EdgeLabels)
        //                if (_Edge.Label.Equals(_Label))
        //                    yield return _Edge;
        //    }

        //    else
        //        foreach (var _Edge in _InEdges)
        //            yield return _Edge;

        //}

        //#endregion

        //#region InEdges(EdgeFilter)

        ///// <summary>
        ///// The edges incoming to, or arriving at, this vertex
        ///// filtered by the given edge filter delegate.
        ///// </summary>
        //IEnumerable<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
        
        //    IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

        //    InEdges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)
        
        //{
        //    return from _Edge in _OutEdges where EdgeFilter(_Edge) select _Edge;
        //}

        //#endregion

        //#region InDegree(params EdgeLabels)     // InDegree()!

        ///// <summary>
        ///// The number of edges incoming to, or arriving at, this vertex
        ///// filtered by their label. If no label was given,
        ///// all edges will be returned.
        ///// </summary>
        //UInt64 IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

        //    InDegree(params TEdgeLabel[] EdgeLabels)

        //{

        //    if (EdgeLabels.Length == 0)
        //        return (UInt64) _NumberOfInEdges;

        //    var Counter = 0UL;

        //    if (EdgeLabels != null && EdgeLabels.Any())
        //    {
        //        foreach (var _Edge in _InEdges)
        //            foreach (var _Label in EdgeLabels)
        //                if (_Edge.Label.Equals(_Label))
        //                    Counter++;
        //    }

        //    else
        //        foreach (var _Edge in _InEdges)
        //            Counter++;

        //    return Counter;

        //}

        //#endregion

        //#region InDegree(EdgeFilter)

        ///// <summary>
        ///// The number of edges incoming to, or arriving at, this vertex
        ///// filtered by the given edge filter delegate.
        ///// </summary>
        //UInt64 IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
            
        //    InDegree(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)

        //{

        //    if (EdgeFilter == null)
        //        return (UInt64) _NumberOfInEdges;

        //    return (UInt64) (from _Edge in _OutEdges where EdgeFilter(_Edge) select _Edge).Count();

        //}

        //#endregion


        //#region RemoveInEdges(params Edges)    // RemoveInEdges()!

        ///// <summary>
        ///// Remove incoming edges.
        ///// </summary>
        ///// <param name="Edges">An array of incoming edges to be removed.</param>
        //void IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

        //    RemoveInEdges(params IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[]
        //                                              Edges)

        //{

        //    if (Edges.Any())
        //    {
        //        foreach (var _Edge in Edges)
        //        {
        //            _InEdges.TryRemoveValue(_Edge.Label, _Edge.Id, _Edge);     // Is supposed to be thread-safe!
        //            Interlocked.Decrement(ref _NumberOfInEdges);
        //        }
        //    }
        //    else
        //    {
        //        lock (this)
        //        {
        //            _InEdges.Clear();
        //            _NumberOfInEdges = 0;
        //        }
        //    }

        //}

        //#endregion

        //#region RemoveInEdges(EdgeFilter)

        ///// <summary>
        ///// Remove any incoming edge matching the
        ///// given edge filter delegate.
        ///// </summary>
        ///// <param name="EdgeFilter">A delegate for edge filtering.</param>
        //void IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

        //    RemoveInEdges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)

        //{

        //    if (EdgeFilter == null)
        //        throw new ArgumentNullException("The given edge filter delegate must not be null!");

        //    lock (this)
        //    {

        //        var _tmp = new List<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>();

        //        if (EdgeFilter == null)
        //            foreach (var _IEdge in _InEdges)
        //                _tmp.Add(_IEdge);

        //        else foreach (var _IEdge in _InEdges)
        //            if (EdgeFilter(_IEdge))
        //                _tmp.Add(_IEdge);

        //        foreach (var _Edge in _tmp)
        //        {
        //            _InEdges.TryRemoveValue(_Edge.Label, _Edge.Id, _Edge);    // Is supposed to be thread-safe!
        //            Interlocked.Decrement(ref _NumberOfInEdges);
        //        }

        //    }

        //}

        //#endregion

        //#endregion


        //// IGenericPropertyGraph

        //#region Vertex methods [IGenericPropertyGraph]

        //#region AddVertex(VertexInitializer = null)

        ///// <summary>
        ///// Adds a vertex to the graph using the given VertexId and initializes
        ///// the vertex by invoking the given vertex initializer.
        ///// </summary>
        ///// <param name="VertexInitializer">A delegate to initialize the new vertex.</param>
        ///// <returns>The new vertex</returns>
        //IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //       AddVertex(VertexInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexInitializer = null)

        //{
        //    return (this as IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>).
        //                                          AddVertex(_VertexIdCreatorDelegate(this), VertexInitializer);
        //}

        //#endregion

        //#region AddVertex(VertexId, VertexInitializer = null)

        ///// <summary>
        ///// Adds a vertex to the graph using the given VertexId and initializes
        ///// the vertex by invoking the given vertex initializer.
        ///// </summary>
        ///// <param name="VertexId">A VertexId. If none was given a new one will be generated.</param>
        ///// <param name="VertexInitializer">A delegate to initialize the new vertex.</param>
        ///// <returns>The new vertex</returns>
        //IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //       AddVertex(TIdVertex VertexId,
        //                 VertexInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexInitializer = null)

        //{

        //    #region Initial checks

        //    if (VertexId == null)
        //        VertexId = _VertexIdCreatorDelegate(this);

        //    if (_Vertices.ContainsId(VertexId))
        //        throw new DuplicateVertexIdException("Another vertex with id " + VertexId + " already exists!");

        //    #endregion

        //    var _Vertex = _VertexCreatorDelegate(this, VertexId, VertexInitializer);

        //    if (SendVertexAddingVote(_Vertex))
        //    {
        //        _Vertices.TryAddValue(_Vertex.Label, VertexId, _Vertex);
        //        _NumberOfVertices++;
        //        SendVertexAddedNotification(_Vertex);
        //        return _Vertex;
        //    }

        //    return null;

        //}

        //#endregion

        //#region AddVertex(IPropertyVertex)

        ///// <summary>
        ///// Adds the given vertex to the graph.
        ///// Will fail if the Id of the vertex is already present in the graph.
        ///// </summary>
        ///// <param name="IPropertyVertex">An IPropertyVertex.</param>
        ///// <returns>The given vertex.</returns>
        //IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        
        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.    
        //               AddVertex(IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IPropertyVertex)
        //{

        //    #region Initial checks

        //    if (IPropertyVertex == null)
        //        throw new ArgumentNullException("The given vertex must not be null!");

        //    if (IPropertyVertex.Id == null || IPropertyVertex.Id.Equals(default(TIdVertex)))
        //        throw new ArgumentNullException("The Id of vertex must not be null!");

        //    if (_Vertices.ContainsId(IPropertyVertex.Id))
        //        throw new DuplicateVertexIdException("Another vertex with id " + IPropertyVertex.Id + " already exists!");

        //    #endregion

        //    if (SendVertexAddingVote(IPropertyVertex))
        //    {
        //        _Vertices.TryAddValue(IPropertyVertex.Label, IPropertyVertex.Id, IPropertyVertex);
        //        _NumberOfVertices++;
        //        SendVertexAddedNotification(IPropertyVertex);
        //        return IPropertyVertex;
        //    }

        //    return null;

        //}

        //#endregion


        //#region VertexById(VertexId)

        ///// <summary>
        ///// Return the vertex referenced by the given vertex identifier.
        ///// If no vertex is referenced by the identifier return null.
        ///// </summary>
        ///// <param name="VertexId">A vertex identifier.</param>
        //IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //       VertexById(TIdVertex VertexId)

        //{
            
        //    #region Initial checks

        //    if (VertexId == null)
        //        throw new ArgumentNullException("VertexId", "The given vertex identifier must not be null!");

        //    #endregion

        //    IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _Vertex = null;

        //    if (_Vertices.TryGetById(VertexId, out _Vertex))
        //        return _Vertex;
        //    else
        //        return null;

        //}

        //#endregion

        //#region VerticesById(params VertexIds)

        ///// <summary>
        ///// Return the vertices referenced by the given array of vertex identifiers.
        ///// If no vertex is referenced by a given identifier this value will be
        ///// skipped.
        ///// </summary>
        ///// <param name="VertexIds">An array of vertex identifiers.</param>
        //IEnumerable<IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //       VerticesById(params TIdVertex[] VertexIds)

        //{

        //    #region Initial checks

        //    if (VertexIds == null || !VertexIds.Any())
        //        throw new ArgumentNullException("The array of vertex identifiers must not be null or its length zero!");

        //    #endregion

        //    IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _IVertex;

        //    foreach (var _VertexId in VertexIds)
        //    {
        //        if (_VertexId != null)
        //        {
        //            if (_Vertices.TryGetById(_VertexId, out _IVertex))
        //                yield return _IVertex;
        //        }
        //    }

        //}

        //#endregion

        //#region VerticesByLabel(params VertexLabels)

        ///// <summary>
        ///// Return an enumeration of all vertices having one of the
        ///// given vertex labels.
        ///// </summary>
        ///// <param name="VertexLabels">An array of vertex labels.</param>
        //IEnumerable<IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //                          VerticesByLabel(params TVertexLabel[] VertexLabels)

        //{

        //    foreach (var Vertex in _Vertices)
        //    {
        //        foreach (var VertexLabel in VertexLabels)
        //        {
        //            if (Vertex.Label != null &&
        //                Vertex.Label.Equals(VertexLabel))
        //                yield return Vertex;
        //        }
        //    }

        //}

        //#endregion

        //#region Vertices(VertexFilter = null)

        ///// <summary>
        ///// Get an enumeration of all vertices in the graph.
        ///// An optional vertex filter may be applied for filtering.
        ///// </summary>
        ///// <param name="VertexFilter">A delegate for vertex filtering.</param>
        //IEnumerable<IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
        
        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.    
        //                          Vertices(VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexFilter = null)

        //{

        //    if (VertexFilter == null)
        //        return from   Vertex
        //               in     _Vertices
        //               select Vertex;

        //    else
        //        return from   Vertex
        //               in     _Vertices
        //               where  VertexFilter(Vertex)
        //               select Vertex;

        //}

        //#endregion

        //#region NumberOfVertices(VertexFilter = null)

        ///// <summary>
        ///// Return the current number of vertices matching the given optional vertex filter.
        ///// When the filter is null, this method should use implement an optimized
        ///// way to get the currenty number of vertices.
        ///// </summary>
        ///// <param name="VertexFilter">A delegate for vertex filtering.</param>
        //UInt64 IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.            
        //    NumberOfVertices(VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexFilter = null)
        //{

        //    if (VertexFilter == null)
        //        return (UInt64) _NumberOfVertices;

        //    else
        //    {
        //        lock (this)
        //        {

        //            var _Counter = 0UL;

        //            foreach (var _Vertex in _Vertices)
        //                if (VertexFilter(_Vertex))
        //                    _Counter++;

        //            return _Counter;

        //        }
        //    }

        //}

        //#endregion


        //#region RemoveVerticesById(params VertexIds)

        ///// <summary>
        ///// Remove the vertex identified by the given VertexId from the graph
        ///// </summary>
        ///// <param name="VertexIds">An array of VertexIds of the vertices to remove.</param>
        //void IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //                           RemoveVerticesById(params TIdVertex[] VertexIds)

        //{

        //    #region Initial checks

        //    if (VertexIds == null || !VertexIds.Any())
        //        throw new ArgumentNullException("VertexIds", "The given array of vertex identifiers must not be null or its length zero!");

        //    #endregion

        //    IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _Vertex;

        //    foreach (var VertexId in VertexIds)
        //    {
        //        if (_Vertices.TryGetById(VertexId, out _Vertex))
        //            (this as IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>).
        //            RemoveVertices(_Vertex);
        //        else
        //            throw new ArgumentException("The given vertex identifier '" + VertexId.ToString() + "' is unknowen!");
        //    }

        //}

        //#endregion

        //#region RemoveVertices(params Vertices)

        ///// <summary>
        ///// Remove the given array of vertices from the graph.
        ///// Upon removing a vertex, all the edges by which the vertex
        ///// is connected will be removed as well.
        ///// </summary>
        ///// <param name="Vertices">An array of vertices to be removed from the graph.</param>
        //void IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //     RemoveVertices(params IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)
        //{

        //    #region Initial checks

        //    if (Vertices == null || !Vertices.Any())
        //        throw new ArgumentNullException("Vertices", "The array of vertices must not be null or its length zero!");

        //    #endregion

        //    lock (this)
        //    {

        //        foreach (var _Vertex in Vertices)
        //        {
        //            if (_Vertices.ContainsId(_Vertex.Id))
        //            {
    
        //                var _EdgeList = new List<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>();
    
        //                _EdgeList.AddRange(_Vertex.InEdges());
        //                _EdgeList.AddRange(_Vertex.OutEdges());
    
        //                // removal requires removal from all indices
        //                //for (TinkerIndex index : this.indices.values()) {
        //                //    index.remove(vertex);
        //                //}
    
        //                _Vertices.TryRemoveValue(_Vertex.Label, _Vertex.Id, _Vertex);
        //                _NumberOfVertices--;
    
        //            }
        //        }

        //    }

        //}

        //#endregion

        //#region RemoveVertices(VertexFilter = null)

        ///// <summary>
        ///// Remove each vertex matching the given filter.
        ///// </summary>
        ///// <param name="VertexFilter">A delegate for vertex filtering.</param>
        //void IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //     RemoveVertices(VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexFilter)
        //{

        //    lock (this)
        //    {

        //        var _tmp = new List<IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>();

        //        if (VertexFilter == null)
        //            foreach (var _Vertex in _Vertices)
        //                _tmp.Add(_Vertex);

        //        else    
        //            foreach (var _Vertex in _Vertices)
        //                if (VertexFilter(_Vertex))
        //                    _tmp.Add(_Vertex);

        //        foreach (var _Vertex in _tmp)
        //            (this as IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>).
        //            RemoveVertices(_Vertex);

        //    }

        //}

        //#endregion

        //#endregion

        //#region Edge methods [IGenericPropertyGraph]

        //#region AddEdge(OutVertex, Label, InVertex, EdgeInitializer = null)

        ///// <summary>
        ///// Add an edge to the graph. The added edge requires a tail vertex,
        ///// a head vertex, a label and initializes the edge
        ///// by invoking the given EdgeInitializer.
        ///// OutVertex --Label-> InVertex is the "Semantic Web Notation" ;)
        ///// </summary>
        ///// <param name="OutVertex">The vertex on the tail of the edge.</param>
        ///// <param name="Label">The label associated with the edge.</param>
        ///// <param name="InVertex">The vertex on the head of the edge.</param>
        ///// <param name="EdgeInitializer">A delegate to initialize the new edge.</param>
        ///// <returns>The new edge.</returns>
        //IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        
        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.    
        //    AddEdge(IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

        //                             TEdgeLabel      Label,

        //                             IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

        //                             EdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer = null)


        //{
        //    return (this as IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>).
        //                                          AddEdge(OutVertex, InVertex, _EdgeIdCreatorDelegate(this), Label, EdgeInitializer);
        //}

        //#endregion

        //#region AddEdge(EdgeId, OutVertex, Label, InVertex, EdgeInitializer = null)

        ///// <summary>
        ///// Add an edge to the graph. The added edge requires a tail vertex,
        ///// a head vertex, an identifier, a label and initializes the edge
        ///// by invoking the given EdgeInitializer.
        ///// OutVertex --Label-> InVertex is the "Semantic Web Notation" ;)
        ///// </summary>
        ///// <param name="EdgeId">A EdgeId. If none was given a new one will be generated.</param>
        ///// <param name="OutVertex">The vertex on the tail of the edge.</param>
        ///// <param name="Label">The label associated with the edge.</param>
        ///// <param name="InVertex">The vertex on the head of the edge.</param>
        ///// <param name="EdgeInitializer">A delegate to initialize the new edge.</param>
        ///// <returns>The new edge.</returns>
        //IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //                     AddEdge(TIdEdge         EdgeId,

        //                             IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

        //                             TEdgeLabel      Label,

        //                             IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

        //                             EdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer = null)


        //{
        //    return (this as IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>).
        //                                          AddEdge(OutVertex, InVertex, _EdgeIdCreatorDelegate(this), Label, EdgeInitializer);
        //}

        //#endregion

        //#region AddEdge(OutVertex, InVertex, Label = default, EdgeInitializer = null)

        ///// <summary>
        ///// Add an edge to the graph. The added edge requires a tail vertex,
        ///// a head vertex, an identifier, a label and initializes the edge
        ///// by invoking the given EdgeInitializer.
        ///// </summary>
        ///// <param name="OutVertex">The vertex on the tail of the edge.</param>
        ///// <param name="InVertex">The vertex on the head of the edge.</param>
        ///// <param name="Label">The label associated with the edge.</param>
        ///// <param name="EdgeInitializer">A delegate to initialize the new edge.</param>
        ///// <returns>The new edge.</returns>
        //IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        
        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.    
        //                     AddEdge(IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

        //                             IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

        //                             TEdgeLabel      Label  = default(TEdgeLabel),

        //                             EdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer = null)


        //{
        //    return (this as IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>).
        //                                          AddEdge(OutVertex, InVertex, _EdgeIdCreatorDelegate(this), Label, EdgeInitializer);
        //}

        //#endregion

        //#region AddEdge(OutVertex, InVertex, EdgeId, Label = default, EdgeInitializer = null)

        ///// <summary>
        ///// Add an edge to the graph. The added edge requires a tail vertex,
        ///// a head vertex, an identifier, a label and initializes the edge
        ///// by invoking the given EdgeInitializer.
        ///// </summary>
        ///// <param name="OutVertex">The vertex on the tail of the edge.</param>
        ///// <param name="InVertex">The vertex on the head of the edge.</param>
        ///// <param name="EdgeId">A EdgeId. If none was given a new one will be generated.</param>
        ///// <param name="Label">The label associated with the edge.</param>
        ///// <param name="EdgeInitializer">A delegate to initialize the new edge.</param>
        ///// <returns>The new edge.</returns>
        //IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        
        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

        //                     AddEdge(IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex,

        //                             IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex,

        //                             TIdEdge         EdgeId,
        //                             TEdgeLabel      Label  = default(TEdgeLabel),

        //                             EdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer = null)


        //{

        //    if (EdgeId == null)
        //        EdgeId = _EdgeIdCreatorDelegate(this);

        //    if (_ForeignEdges.ContainsId(EdgeId))
        //        throw new ArgumentException("Another edge with id " + EdgeId + " already exists!");

        //    var _Edge = _EdgeCreatorDelegate(this, OutVertex, InVertex, EdgeId, Label, EdgeInitializer);

        //    if (SendEdgeAddingVote(_Edge))
        //    {
        //        _ForeignEdges.TryAddValue(_Edge.Label, EdgeId, _Edge);
        //        _NumberOfEdges++;
        //        OutVertex.AddOutEdge(_Edge);
        //        InVertex.AddInEdge(_Edge);
        //        SendEdgeAddedNotification(_Edge);
        //        return _Edge;
        //    }

        //    return null;

        //}

        //#endregion


        //#region EdgeById(EdgeId)

        ///// <summary>
        ///// Return the edge referenced by the given edge identifier.
        ///// If no edge is referenced by a given identifier return null.
        ///// </summary>
        ///// <param name="EdgeId">An edge identifier.</param>
        //IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //                          EdgeById(TIdEdge EdgeId)

        //{

        //    #region Initial checks

        //    if (EdgeId == null)
        //        throw new ArgumentNullException("EdgeId", "The given Edge identifier must not be null!");

        //    #endregion

        //    IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _Edge = null;

        //    if (_ForeignEdges.TryGetById(EdgeId, out _Edge))
        //        return _Edge;
        //    else
        //        return null;

        //}

        //#endregion

        //#region EdgesById(params EdgeIds)

        ///// <summary>
        ///// Return the edges referenced by the given array of edge identifiers.
        ///// If no edge is referenced by a given identifier this value will be
        ///// skipped.
        ///// </summary>
        ///// <param name="EdgeIds">An array of edge identifiers.</param>
        //IEnumerable<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //                          EdgesById(params TIdEdge[] EdgeIds)

        //{

        //    #region Initial checks

        //    if (EdgeIds == null || !EdgeIds.Any())
        //        throw new ArgumentNullException("EdgeIds", "The given array of edge identifiers must not be null or its length zero!");

        //    #endregion

        //    IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _Edge;

        //    foreach (var _EdgeId in EdgeIds)
        //    {
        //        if (_EdgeId != null)
        //        {
        //            _ForeignEdges.TryGetById(_EdgeId, out _Edge);
        //            yield return _Edge;
        //        }
        //    }

        //}

        //#endregion

        //#region EdgesByLabel(params EdgeLabels)

        ///// <summary>
        ///// Return an enumeration of all edges having one of the
        ///// given edge labels.
        ///// </summary>
        ///// <param name="EdgeLabels">An array of edge labels.</param>
        //IEnumerable<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //                          EdgesByLabel(params TEdgeLabel[] EdgeLabels)

        //{

        //    foreach (var Edge in _ForeignEdges)
        //    {
        //        foreach (var EdgeLabel in EdgeLabels)
        //        {
        //            if (Edge.Label != null &&
        //                Edge.Label.Equals(EdgeLabel))
        //                yield return Edge;
        //        }
        //    }

        //}

        //#endregion

        //#region Edges(EdgeFilter = null)

        ///// <summary>
        ///// Return an enumeration of all edges in the graph.
        ///// An optional edge filter may be applied for filtering.
        ///// </summary>
        ///// <param name="EdgeFilter">A delegate for edge filtering.</param>
        //IEnumerable<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //                          Edges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter = null)
        //{

        //    if (EdgeFilter == null)
        //        return from   Edge
        //               in     _ForeignEdges
        //               select Edge;

        //    else
        //        return from   Edge
        //               in     _ForeignEdges
        //               where  EdgeFilter(Edge)
        //               select Edge;

        //}

        //#endregion

        //#region NumberOfEdges(EdgeFilter = null)

        ///// <summary>
        ///// Return the current number of edges matching the given optional edge filter.
        ///// When the filter is null, this method should use implement an optimized
        ///// way to get the currenty number of edges.
        ///// </summary>
        ///// <param name="EdgeFilter">A delegate for edge filtering.</param>
        //UInt64 IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //       NumberOfEdges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter = null)

        //{

        //    if (EdgeFilter == null)
        //        return (UInt64) _NumberOfEdges;

        //    else
        //    {
        //        lock (this)
        //        {

        //            var _Counter = 0UL;

        //            foreach (var _Edge in _ForeignEdges)
        //                if (EdgeFilter(_Edge))
        //                    _Counter++;

        //            return _Counter;

        //        }
        //    }
        
        //}

        //#endregion


        //#region RemoveEdgesById(params EdgeIds)

        ///// <summary>
        ///// Remove the given array of edges identified by their EdgeIds.
        ///// </summary>
        ///// <param name="EdgeIds">An array of EdgeIds of the edges to remove</param>
        //void IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.            
        //                           RemoveEdgesById(params TIdEdge[] EdgeIds)

        //{

        //    #region Initial checks

        //    if (EdgeIds == null || !EdgeIds.Any())
        //        throw new ArgumentNullException("EdgeIds", "The given array of edge identifiers must not be null or its length zero!");

        //    #endregion

        //    IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _Edge;

        //    if (EdgeIds.Any())
        //    {
        //        foreach (var _EdgeId in EdgeIds)
        //        {
        //            if (_ForeignEdges.TryGetById(_EdgeId, out _Edge))
        //                (this as IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>).
        //                RemoveEdges(_Edge);
        //            else
        //                throw new ArgumentException("The given edge identifier '" + _EdgeId.ToString() + "' is unknowen!");
        //        }
        //    }

        //}

        //#endregion

        //#region RemoveEdges(params Edges)

        ///// <summary>
        ///// Remove the given array of edges from the graph.
        ///// </summary>
        ///// <param name="Edges">An array of edges to be removed from the graph.</param>
        //void IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //     RemoveEdges(params IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Edges)
        //{

        //    #region Initial checks

        //    if (Edges == null || !Edges.Any())
        //        throw new ArgumentNullException("Edges", "The given array of edges must not be null or its length zero!");

        //    #endregion

        //    lock (this)
        //    {

        //        foreach (var _Edge in Edges)
        //        {
        //            if (_ForeignEdges.ContainsId(_Edge.Id))
        //            {

        //                var _OutVertex = _Edge.OutVertex;
        //                var _InVertex  = _Edge.InVertex;

        //                if (_OutVertex != null && _OutVertex.OutEdges() != null)
        //                    _OutVertex.RemoveOutEdges(Edges);

        //                if (_InVertex != null && _InVertex.InEdges() != null)
        //                    _InVertex.RemoveInEdges(_Edge);

        //                // removal requires removal from all indices
        //                //for (TinkerIndex index : this.indices.values()) {
        //                //    index.remove(edge);
        //                //}

        //                _ForeignEdges.TryRemoveValue(_Edge.Label, _Edge.Id, _Edge);
        //                _NumberOfEdges--;

        //            }
        //        }

        //    }

        //}

        //#endregion

        //#region RemoveEdges(EdgeFilter = null)

        ///// <summary>
        ///// Remove any edge matching the given edge filter.
        ///// </summary>
        ///// <param name="EdgeFilter">A delegate for edge filtering.</param>
        //void IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //     RemoveEdges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)
        //{

        //    lock (this)
        //    {

        //        var _tmp = new List<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>();

        //        if (EdgeFilter == null)
        //            foreach (var _IEdge in _ForeignEdges)
        //                _tmp.Add(_IEdge);

        //        else
        //            foreach (var _IEdge in _ForeignEdges)
        //                if (EdgeFilter(_IEdge))
        //                    _tmp.Add(_IEdge);

        //        foreach (var _IEdge in _tmp)
        //            (this as IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>).
        //            RemoveEdges(_IEdge);

        //    }

        //}

        //#endregion

        //#endregion

        //#region MultiEdge methods [IGenericPropertyGraph]

        //#region MultiEdgeById(MultiEdgeId)

        ///// <summary>
        ///// Return the MultiEdge referenced by the given MultiEdge identifier.
        ///// If no MultiEdge is referenced by the identifier return null.
        ///// </summary>
        ///// <param name="MultiEdgeId">A MultiEdge identifier.</param>
        //IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //    MultiEdgeById(TIdMultiEdge MultiEdgeId)

        //{

        //    #region Initial checks

        //    if (MultiEdgeId == null)
        //        throw new ArgumentNullException("MultiEdgeId", "The given MultiEdge identifier must not be null!");

        //    #endregion

        //    IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _MultiEdge = null;

        //    if (_MultiEdges.TryGetById(MultiEdgeId, out _MultiEdge))
        //        return _MultiEdge;
        //    else
        //        return null;

        //}

        //#endregion

        //#region MultiEdgesById(params MultiEdgeIds)

        ///// <summary>
        ///// Return the MultiEdges referenced by the given array of MultiEdge identifiers.
        ///// If no MultiEdge is referenced by a given identifier this value will be
        ///// skipped.
        ///// </summary>
        ///// <param name="MultiEdgeIds">An array of MultiEdge identifiers.</param>
        //IEnumerable<IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //    MultiEdgesById(params TIdMultiEdge[] MultiEdgeIds)

        //{

        //    #region Initial checks

        //    if (MultiEdgeIds == null || !MultiEdgeIds.Any())
        //        throw new ArgumentNullException("MultiEdgeIds", "The given array of MultiEdge identifiers must not be null or its length zero!");

        //    #endregion

        //    IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _MultiEdge;

        //    foreach (var _MultiEdgeId in MultiEdgeIds)
        //    {
        //        if (_MultiEdgeId != null)
        //        {
        //            _MultiEdges.TryGetById(_MultiEdgeId, out _MultiEdge);
        //            yield return _MultiEdge;
        //        }
        //    }

        //}

        //#endregion

        //#region MultiEdgesByLabel(params MultiEdgeLabels)

        ///// <summary>
        ///// Return an enumeration of all multiedges having one
        ///// of the given multiedge labels.
        ///// </summary>
        ///// <param name="MultiEdgeLabels">An array of multiedge labels.</param>
        //IEnumerable<IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //    MultiEdgesByLabel(params TMultiEdgeLabel[] MultiEdgeLabels)

        //{

        //    foreach (var MultiEdge in _MultiEdges)
        //    {
        //        foreach (var MultiEdgeLabel in MultiEdgeLabels)
        //        {
        //            if (MultiEdge.Label != null &&
        //                MultiEdge.Label.Equals(MultiEdgeLabel))
        //                yield return MultiEdge;
        //        }
        //    }

        //}

        //#endregion

        //#region MultiEdges(MultiEdgeFilter = null)

        ///// <summary>
        ///// Get an enumeration of all MultiEdges in the graph.
        ///// An optional MultiEdge filter may be applied for filtering.
        ///// </summary>
        ///// <param name="MultiEdgeFilter">A delegate for MultiEdge filtering.</param>
        //IEnumerable<IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //    MultiEdges(MultiEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeFilter = null)

        //{

        //    if (MultiEdgeFilter == null)
        //        return from   MultiEdge
        //               in     _MultiEdges
        //               select MultiEdge;

        //    else
        //        return from   MultiEdge
        //               in     _MultiEdges
        //               where  MultiEdgeFilter(MultiEdge)
        //               select MultiEdge;

        //}

        //#endregion

        //#region NumberOfMultiEdges(MultiEdgeFilter = null)

        ///// <summary>
        ///// Return the current number of MultiEdges matching the given optional MultiEdge filter.
        ///// When the filter is null, this method should implement an optimized
        ///// way to get the currenty number of edges.
        ///// </summary>
        ///// <param name="MultiEdgeFilter">A delegate for MultiEdge filtering.</param>
        //UInt64
            
        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //    NumberOfMultiEdges(MultiEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeFilter = null)

        //{

        //    if (MultiEdgeFilter == null)
        //        return (UInt64) _NumberOfMultiEdges;

        //    else
        //    {
        //        lock (this)
        //        {

        //            var _Counter = 0UL;

        //            foreach (var _MultiEdge in _MultiEdges)
        //                if (MultiEdgeFilter(_MultiEdge))
        //                    _Counter++;

        //            return _Counter;

        //        }
        //    }

        //}

        //#endregion

        //#endregion

        //#region HyperEdge methods [IGenericPropertyGraph]

        //#region AddHyperEdge(HyperEdge)

        ///// <summary>
        ///// Add a hyperedge.
        ///// </summary>
        ///// <param name="HyperEdge">The hyperedge to add.</param>
        //public void AddHyperEdge(IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdge)
        //{
        //    _HyperEdges.TryAddValue(HyperEdge.Label, HyperEdge.Id, HyperEdge);    // Is supposed to be thread-safe!
        //    Interlocked.Increment(ref _NumberOfHyperEdges);
        //}

        //#endregion


        //#region AddHyperEdge(params Vertices)

        ///// <summary>
        ///// Add a multiedge based on the given enumeration
        ///// of vertices to the graph.
        ///// </summary>
        ///// <param name="Vertices">An enumeration of vertices.</param>
        ///// <returns>The new multiedge</returns>
        //IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //    AddHyperEdge(params IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)

        //{
        //    return (this as IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>).
        //                                          AddHyperEdge(_HyperEdgeIdCreatorDelegate(this), default(THyperEdgeLabel), null, Vertices);
        //}

        //#endregion

        //#region AddHyperEdge(Label, params Vertices)

        ///// <summary>
        ///// Add a multiedge based on the given multiedge label and
        ///// an enumeration of vertices to the graph.
        ///// </summary>
        ///// <param name="Label">The multiedge label.</param>
        ///// <param name="Vertices">An enumeration of vertices.</param>
        ///// <returns>The new multiedge</returns>
        //IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //    AddHyperEdge(THyperEdgeLabel Label,            
        //                 params IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)

        //{
        //    return (this as IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>).
        //                                          AddHyperEdge(_HyperEdgeIdCreatorDelegate(this), Label, null, Vertices);
        //}

        //#endregion

        //#region AddHyperEdge(Label, HyperEdgeInitializer, params Vertices)

        ///// <summary>
        ///// Add a multiedge based on the given multiedge label and
        ///// an enumeration of vertices to the graph and initialize
        ///// it by invoking the given HyperEdgeInitializer.
        ///// </summary>
        ///// <param name="Label">The multiedge label.</param>
        ///// <param name="HyperEdgeInitializer">A delegate to initialize the newly generated multiedge.</param>
        ///// <param name="Vertices">An enumeration of vertices.</param>
        ///// <returns>The new multiedge</returns>
        //IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //    AddHyperEdge(THyperEdgeLabel Label,

        //                 HyperEdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeInitializer,
            
        //                 params IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)


        //{
        //    return (this as IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>).
        //                                          AddHyperEdge(_HyperEdgeIdCreatorDelegate(this), Label, HyperEdgeInitializer, Vertices);
        //}

        //#endregion

        //#region AddHyperEdge(HyperEdgeId, Label, HyperEdgeInitializer, params Vertices)

        ///// <summary>
        ///// Add a multiedge based on the given multiedge label and
        ///// an enumeration of vertices to the graph and initialize
        ///// it by invoking the given HyperEdgeInitializer.
        ///// </summary>
        ///// <param name="HyperEdgeId">A HyperEdgeId. If none was given a new one will be generated.</param>
        ///// <param name="Label">The multiedge label.</param>
        ///// <param name="HyperEdgeInitializer">A delegate to initialize the newly generated multiedge.</param>
        ///// <param name="Vertices">An enumeration of vertices.</param>
        ///// <returns>The new multiedge</returns>
        //IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
        //    IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.
        //    AddHyperEdge(TIdHyperEdge    HyperEdgeId,
        //                 THyperEdgeLabel Label,

        //                 HyperEdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeInitializer,
            
        //                 params IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)


        //{

        //    if (HyperEdgeId == null)
        //        HyperEdgeId = _HyperEdgeIdCreatorDelegate(this);

        //    if (_HyperEdges.ContainsId(HyperEdgeId))
        //        throw new ArgumentException("Another hyperedge with id " + HyperEdgeId + " already exists!");

        //    var _HyperEdge = _HyperEdgeCreatorDelegate(this, Vertices, HyperEdgeId, Label, HyperEdgeInitializer);

        //    if (SendHyperEdgeAddingVote(_HyperEdge))
        //    {
        //        _HyperEdges.TryAddValue(_HyperEdge.Label, HyperEdgeId, _HyperEdge);
        //        _NumberOfHyperEdges++;
        //        //OutVertex.AddOutEdge(_HyperEdge);
        //        //InVertex.AddInEdge(_HyperEdge);
        //        SendHyperEdgeAddedNotification(_HyperEdge);
        //        return _HyperEdge;
        //    }

        //    return null;

        //}

        //#endregion


        //#region HyperEdgeById(HyperEdgeId)

        ///// <summary>
        ///// Return the HyperEdge referenced by the given HyperEdge identifier.
        ///// If no HyperEdge is referenced by the identifier return null.
        ///// </summary>
        ///// <param name="HyperEdgeId">A HyperEdge identifier.</param>
        //public IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

        //           HyperEdgeById(TIdHyperEdge HyperEdgeId)
        //{

        //    #region Initial checks

        //    if (HyperEdgeId == null)
        //        throw new ArgumentNullException("HyperEdgeId", "The given HyperEdge identifier must not be null!");

        //    #endregion

        //    IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _HyperEdge = null;

        //    throw new NotImplementedException();
        //    //if (_HyperEdges.TryGetValue(HyperEdgeId, out _HyperEdge))
        //    //    return _HyperEdge;
        //    //else
        //    //    return null;

        //}

        //#endregion

        //#region HyperEdgesById(params HyperEdgeIds)

        ///// <summary>
        ///// Return the HyperEdges referenced by the given array of HyperEdge identifiers.
        ///// If no HyperEdge is referenced by a given identifier this value will be
        ///// skipped.
        ///// </summary>
        ///// <param name="HyperEdgeIds">An array of HyperEdge identifiers.</param>
        //public IEnumerable<IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

        //           HyperEdgesById(params TIdHyperEdge[] HyperEdgeIds)
        //{

        //    #region Initial checks

        //    if (HyperEdgeIds == null || !HyperEdgeIds.Any())
        //        throw new ArgumentNullException("HyperEdgeIds", "The given array of HyperEdge identifiers must not be null or its length zero!");

        //    #endregion

        //    IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _HyperEdge;

        //    throw new NotImplementedException();
        //    //foreach (var _HyperEdgeId in HyperEdgeIds)
        //    //{
        //    //    if (_HyperEdgeId != null)
        //    //    {
        //    //        _HyperEdges.TryGetValue( .TryGetValue(_HyperEdgeId, out _HyperEdge);
        //    //        yield return _HyperEdge;
        //    //    }
        //    //}

        //}

        //#endregion

        //#region HyperEdgesByLabel(params HyperEdgeLabels)

        ///// <summary>
        ///// Return an enumeration of all multiedges having one
        ///// of the given multiedge labels.
        ///// </summary>
        ///// <param name="HyperEdgeLabels">An array of multiedge labels.</param>
        //public IEnumerable<IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

        //           HyperEdgesByLabel(params THyperEdgeLabel[] HyperEdgeLabels)
        //{

        //    // This should be optimized in the future!

        //    return HyperEdges(HyperEdge =>
        //    {

        //        foreach (var HyperEdgeLabel in HyperEdgeLabels)
        //        {
        //            if (HyperEdge.Label != null &&
        //                HyperEdge.Label.Equals(HyperEdgeLabel))
        //                return true;
        //        }

        //        return false;

        //    });

        //}

        //#endregion

        //#region HyperEdges(HyperEdgeFilter = null)

        ///// <summary>
        ///// Get an enumeration of all HyperEdges in the graph.
        ///// An optional HyperEdge filter may be applied for filtering.
        ///// </summary>
        ///// <param name="HyperEdgeFilter">A delegate for HyperEdge filtering.</param>
        //public IEnumerable<IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

        //            HyperEdges(HyperEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter = null)
        //{

        //    if (HyperEdgeFilter == null)
        //        return from   HyperEdge
        //               in     _HyperEdges
        //               select HyperEdge;

        //    else
        //        return from   HyperEdge
        //               in     _HyperEdges
        //               where  HyperEdgeFilter(HyperEdge)
        //               select HyperEdge;

        //}

        //#endregion

        //#region NumberOfHyperEdges(HyperEdgeFilter = null)

        ///// <summary>
        ///// Return the current number of HyperEdges matching the given optional HyperEdge filter.
        ///// When the filter is null, this method should implement an optimized
        ///// way to get the currenty number of edges.
        ///// </summary>
        ///// <param name="HyperEdgeFilter">A delegate for HyperEdge filtering.</param>
        //public UInt64 NumberOfHyperEdges(HyperEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter = null)
        //{

        //    if (HyperEdgeFilter == null)
        //        return (UInt64) _NumberOfHyperEdges;

        //    else
        //    {
        //        lock (this)
        //        {

        //            var _Counter = 0UL;

        //            foreach (var _HyperEdge in _HyperEdges)
        //                if (HyperEdgeFilter(_HyperEdge))
        //                    _Counter++;

        //            return _Counter;

        //        }
        //    }

        //}

        //#endregion

        //public IEnumerable<IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
        //                                             HyperEdges(params THyperEdgeLabel[] HyperEdgeLabels)
        //{
        //    throw new NotImplementedException();
        //}


        //#region RemoveHyperEdges(params HyperEdges)    // RemoveHyperEdges()!

        ///// <summary>
        ///// Remove hyperedges.
        ///// </summary>
        ///// <param name="HyperEdges">An array of outgoing edges to be removed.</param>
        //public void RemoveHyperEdges(params IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] HyperEdges)
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion

        //#region RemoveHyperEdges(HyperEdgeFilter = null)

        ///// <summary>
        ///// Remove any outgoing hyperedge matching
        ///// the given hyperedge filter delegate.
        ///// </summary>
        ///// <param name="HyperEdgeFilter">A delegate for hyperedge filtering.</param>
        //public void RemoveHyperEdges(HyperEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter = null)
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion

        //#endregion


        //#region Clear()

        ///// <summary>
        ///// Removes all the vertices, edges and hyperedges from the graph.
        ///// </summary>
        //void IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.Clear()
        //{
        //    _Vertices.Clear();
        //    _ForeignEdges.Clear();
        //    _HyperEdges.Clear();
        //}

        //#endregion

        //#region Shutdown()

        ///// <summary>
        ///// Shutdown and close the graph.
        ///// </summary>
        ///// <param name="Message">An optional message, e.g. a reason for the shutdown.</param>
        //void IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.Shutdown(String Message = "")
        //{
        //    SendGraphShuttingdownNotification(Message);
        //    //Clear();
        //    SendGraphShutteddownNotification();
        //}

        //#endregion


        #region Operator overloading

        #region Operator == (PropertyVertex1, PropertyVertex2)

        /// <summary>
        /// Compares two vertices for equality.
        /// </summary>
        /// <param name="PropertyVertex1">A vertex.</param>
        /// <param name="PropertyVertex2">Another vertex.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public static Boolean operator == (PropertyVertex PropertyVertex1,
                                           PropertyVertex PropertyVertex2)
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
        public static Boolean operator != (PropertyVertex PropertyVertex1,
                                           PropertyVertex PropertyVertex2)
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
        public static Boolean operator  < (PropertyVertex PropertyVertex1,
                                           PropertyVertex PropertyVertex2)
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
        public static Boolean operator <= (PropertyVertex PropertyVertex1,
                                           PropertyVertex PropertyVertex2)
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
        public static Boolean operator >  (PropertyVertex PropertyVertex1,
                                           PropertyVertex PropertyVertex2)
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
        public static Boolean operator >= (PropertyVertex PropertyVertex1,
                                           PropertyVertex PropertyVertex2)
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
            return new DynamicGraphElement<PropertyVertex> (myExpression, this);
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
            return SetProperty((String) (Object) myBinder, (Object) myObject);
        }

        #endregion

        #region GetMember(myBinder)

        /// <summary>
        /// Returns the value of the given property key.
        /// </summary>
        /// <param name="myBinder">The property key.</param>
        public virtual Object GetMember(String myBinder)
        {
            Object myObject;
            TryGet((String) (Object) myBinder, out myObject);
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
                PropertyData.Remove((String) (Object) myBinder);
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

            return CompareTo((UInt64) Object);

        }

        #endregion

        #region CompareTo(IPropertyVertex)

        /// <summary>
        /// Compares two generic property vertices.
        /// </summary>
        /// <param name="IGenericPropertyVertex">A generic property vertex to compare with.</param>
        public Int32 CompareTo(IPropertyVertex IPropertyVertex)
        {
            
            if ((Object) IPropertyVertex == null)
                throw new ArgumentNullException("The given IPropertyVertex must not be null!");

            return Id.CompareTo(IPropertyVertex[IdKey]);

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

            // Check if the given object can be casted to a PropertyVertex
            var PropertyVertex = Object as PropertyVertex;

            if ((Object) PropertyVertex == null)
                return false;

            return this.Equals(PropertyVertex);

        }

        #endregion

        #region Equals(IPropertyVertex)

        /// <summary>
        /// Compares two generic property vertices for equality.
        /// </summary>
        /// <param name="IGenericPropertyVertex">A generic property vertex to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IPropertyVertex IPropertyVertex)
        {
            
            if ((Object) IPropertyVertex == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return Id.Equals(IPropertyVertex[IdKey]);

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
            return "IPropertyVertex [Id: " + Id.ToString() + ", " + _OutEdges.Count() + " OutEdges, " + _InEdges.Count() + " InEdges]";
        }

        #endregion



        // ----------------------------------------------

        #region Vertex methods

        #region VertexById(VertexId)

        /// <summary>
        /// Return the vertex referenced by the given vertex identifier.
        /// If no vertex is referenced by the identifier return null.
        /// </summary>
        /// <param name="VertexId">A vertex identifier.</param>
        IPropertyVertex IPropertyGraph.VertexById(UInt64 VertexId)
        {
            return this.VertexById(VertexId) as IPropertyVertex;
        }

        #endregion

        #region VerticesById(params VertexIds)

        /// <summary>
        /// Return the vertices referenced by the given array of vertex identifiers.
        /// If no vertex is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="VertexIds">An array of vertex identifiers.</param>
        IEnumerable<IPropertyVertex> IPropertyGraph.VerticesById(params UInt64[] VertexIds)
        {

            return from Vertex
                   in (this as IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object>).VerticesById(VertexIds)
                   select Vertex as IPropertyVertex;

        }

        #endregion

        #region VerticesByLabel(params VertexLabels)

        /// <summary>
        /// Return an enumeration of all vertices having one of the
        /// given vertex labels.
        /// </summary>
        /// <param name="VertexLabels">An array of vertex labels.</param>
        IEnumerable<IPropertyVertex> IPropertyGraph.VerticesByLabel(params String[] VertexLabels)
        {

            return from Vertex
                   in (this as IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object>).VerticesByLabel(VertexLabels)
                   select Vertex as IPropertyVertex;

        }

        #endregion

        #region Vertices(VertexFilter = null)

        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// An optional vertex filter may be applied for filtering.
        /// </summary>
        /// <param name="VertexFilter">A delegate for vertex filtering.</param>
        IEnumerable<IPropertyVertex> IPropertyGraph.Vertices(VertexFilter<UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object> VertexFilter = null)
        {

            return from Vertex
                   in (this as IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object>).Vertices(VertexFilter)
                   select Vertex as IPropertyVertex;

        }

        #endregion

        #endregion

        #region Edge methods

        #region EdgeById(EdgeId)

        /// <summary>
        /// Return the edge referenced by the given edge identifier.
        /// If no edge is referenced by a given identifier return null.
        /// </summary>
        /// <param name="EdgeId">An edge identifier.</param>
        IPropertyEdge IPropertyGraph.EdgeById(UInt64 EdgeId)
        {
            return this.EdgeById(EdgeId) as IPropertyEdge;
        }

        #endregion

        #region EdgesById(params EdgeIds)

        /// <summary>
        /// Return the edges referenced by the given array of edge identifiers.
        /// If no edge is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="EdgeIds">An array of edge identifiers.</param>
        IEnumerable<IPropertyEdge> IPropertyGraph.EdgesById(params UInt64[] EdgeIds)
        {

            return from Edge
                   in (this as IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object>).EdgesById(EdgeIds)
                   select Edge as IPropertyEdge;

        }

        #endregion

        #region EdgesByLabel(params EdgeLabels)

        /// <summary>
        /// Return an enumeration of all edges having one of the
        /// given edge labels.
        /// </summary>
        /// <param name="EdgeLabels">An array of edge labels.</param>
        IEnumerable<IPropertyEdge> IPropertyGraph.EdgesByLabel(params String[] EdgeLabels)
        {

            return from Edge
                   in (this as IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object>).EdgesByLabel(EdgeLabels)
                   select Edge as IPropertyEdge;

        }

        #endregion

        #region Edges(EdgeFilter = null)

        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// An optional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        IEnumerable<IPropertyEdge> IPropertyGraph.Edges(EdgeFilter<UInt64, Int64, String, String, Object,
                                                                   UInt64, Int64, String, String, Object,
                                                                   UInt64, Int64, String, String, Object,
                                                                   UInt64, Int64, String, String, Object> EdgeFilter = null)
        {

            return from Edge
                   in (this as IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object>).Edges(EdgeFilter)
                   select Edge as IPropertyEdge;

        }

        #endregion

        #endregion
        // ----------------------------------------------

    }

}
