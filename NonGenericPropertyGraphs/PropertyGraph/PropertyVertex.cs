﻿/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Blueprints.NET <http://www.github.com/Vanaheimr/Balder>
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
using System.Linq.Expressions;
using System.Collections.Generic;

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace org.GraphDefined.Vanaheimr.Balder.InMemory
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

        #region Graph [IPropertyVertex]

        /// <summary>
        /// The associated property graph.
        /// </summary>
        IPropertyGraph IPropertyVertex.Graph
        {
            get
            {
                return Subgraph as IPropertyGraph;
            }
        }

        #endregion

        #region AsSubgraph [IPropertyVertex]

        /// <summary>
        /// Access this property vertex as a subgraph of the hosting property graph.
        /// </summary>
        IPropertyGraph IPropertyVertex.AsSubgraph
        {
            get
            {
                return this as IPropertyGraph;
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
        /// <param name="PropertiesInitializer"></param>
        /// <param name="VertexIdCreatorDelegate"></param>
        /// <param name="VertexCreatorDelegate"></param>
        /// <param name="VertexCollectionInitializer"></param>
        /// <param name="EdgeIdCreatorDelegate"></param>
        /// <param name="EdgeCreatorDelegate"></param>
        /// <param name="EdgeCollectionInitializer"></param>
        /// <param name="MultiEdgeIdCreatorDelegate"></param>
        /// <param name="MultiEdgeCreatorDelegate"></param>
        /// <param name="MultiEdgeCollectionInitializer"></param>
        /// <param name="HyperEdgeIdCreatorDelegate"></param>
        /// <param name="HyperEdgeCreatorDelegate"></param>
        /// <param name="HyperEdgeCollectionInitializer"></param>
        internal PropertyVertex(UInt64  VertexId,
                                String  IdKey,
                                String  RevIdKey,
                                String  LabelKey,
                                String  DescriptionKey,
                                IDictionaryInitializer<String, Object> PropertiesInitializer,

                                #region Create a new vertex

                                VertexIdCreatorDelegate    <UInt64, Int64, String, String, Object,
                                                            UInt64, Int64, String, String, Object,
                                                            UInt64, Int64, String, String, Object,
                                                            UInt64, Int64, String, String, Object> VertexIdCreatorDelegate,

                                VertexCreatorDelegate      <UInt64, Int64, String, String, Object,
                                                            UInt64, Int64, String, String, Object,
                                                            UInt64, Int64, String, String, Object,
                                                            UInt64, Int64, String, String, Object> VertexCreatorDelegate,

                                VertexCollectionInitializer<UInt64, Int64, String, String, Object,
                                                            UInt64, Int64, String, String, Object,
                                                            UInt64, Int64, String, String, Object,
                                                            UInt64, Int64, String, String, Object> VertexCollectionInitializer,

                                #endregion

                                #region Create a new edge

                                EdgeIdCreatorDelegate    <UInt64, Int64, String, String, Object,
                                                          UInt64, Int64, String, String, Object,
                                                          UInt64, Int64, String, String, Object,
                                                          UInt64, Int64, String, String, Object> EdgeIdCreatorDelegate,

                                EdgeCreatorDelegate      <UInt64, Int64, String, String, Object,
                                                          UInt64, Int64, String, String, Object,
                                                          UInt64, Int64, String, String, Object,
                                                          UInt64, Int64, String, String, Object> EdgeCreatorDelegate,

                                EdgeCollectionInitializer<UInt64, Int64, String, String, Object,
                                                          UInt64, Int64, String, String, Object,
                                                          UInt64, Int64, String, String, Object,
                                                          UInt64, Int64, String, String, Object> EdgeCollectionInitializer,

                                #endregion

                                #region Create a new multiedge

                                MultiEdgeIdCreatorDelegate    <UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object> MultiEdgeIdCreatorDelegate,

                                MultiEdgeCreatorDelegate      <UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object> MultiEdgeCreatorDelegate,

                                MultiEdgeCollectionInitializer<UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object> MultiEdgeCollectionInitializer,

                                #endregion

                                #region Create a new hyperedge

                                HyperEdgeIdCreatorDelegate    <UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object> HyperEdgeIdCreatorDelegate,

                                HyperEdgeCreatorDelegate      <UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object> HyperEdgeCreatorDelegate,

                                HyperEdgeCollectionInitializer<UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object> HyperEdgeCollectionInitializer)

                                #endregion

            : base(VertexId, GraphDBOntology.DefaultVertexLabel().Suffix,
                   IdKey, RevIdKey, LabelKey, DescriptionKey,
                   PropertiesInitializer,
                   VertexIdCreatorDelegate,    GraphDBOntology.DefaultVertexLabel().Suffix,    VertexCreatorDelegate,    VertexCollectionInitializer,
                   EdgeIdCreatorDelegate,      GraphDBOntology.DefaultEdgeLabel().Suffix,      EdgeCreatorDelegate,      EdgeCollectionInitializer,
                   MultiEdgeIdCreatorDelegate, GraphDBOntology.DefaultMultiEdgeLabel().Suffix, MultiEdgeCreatorDelegate, MultiEdgeCollectionInitializer,
                   HyperEdgeIdCreatorDelegate, GraphDBOntology.DefaultHyperEdgeLabel().Suffix, HyperEdgeCreatorDelegate, HyperEdgeCollectionInitializer)

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
        /// <param name="LabelKey">The key to access the Label of this graph element.</param>
        /// <param name="PropertiesInitializer">A delegate to initialize the properties datastructure.</param>
        /// <param name="EdgesCollectionInitializer">A delegate to initialize the datastructure for storing all edges.</param>
        /// <param name="HyperEdgeCollectionInitializer">A delegate to initialize the datastructure for storing all hyperedges.</param>
        /// <param name="VertexInitializer">A delegate to initialize the newly created vertex.</param>
        public PropertyVertex(IPropertyGraph Graph,
                              UInt64         VertexId,
                              String         VertexLabel,
                              String         IdKey,
                              String         RevIdKey,
                              String         LabelKey,
                              String         DescriptionKey,

                              IDictionaryInitializer<String, Object> PropertiesInitializer,

                              Func<IGroupedCollection<UInt64, IReadOnlyGenericPropertyVertex   <UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object>, String>> VerticesCollectionInitializer,

                              Func<IGroupedCollection<UInt64, IReadOnlyGenericPropertyEdge     <UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object>, String>> EdgesCollectionInitializer,

                              Func<IGroupedCollection<UInt64, IReadOnlyGenericPropertyMultiEdge<UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object>, String>> MultiEdgesCollectionInitializer,

                              Func<IGroupedCollection<UInt64, IReadOnlyGenericPropertyHyperEdge<UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object,
                                                                                                UInt64, Int64, String, String, Object>, String>> HyperEdgesCollectionInitializer,

                              VertexInitializer<UInt64, Int64, String, String, Object,
                                                UInt64, Int64, String, String, Object,
                                                UInt64, Int64, String, String, Object,
                                                UInt64, Int64, String, String, Object> VertexInitializer = null)

            : base(Graph, VertexId, VertexLabel,
                   IdKey, RevIdKey, LabelKey, DescriptionKey,
                   PropertiesInitializer,
                   VerticesCollectionInitializer, EdgesCollectionInitializer, MultiEdgesCollectionInitializer, HyperEdgesCollectionInitializer,
                   VertexInitializer)

        { }

        #endregion

        #endregion


        // IPropertyVertex

        #region OutEdge methods [IPropertyVertex]

        #region AddOutEdge(Edge)

        /// <summary>
        /// Add an outgoing edge.
        /// </summary>
        /// <param name="Edge">The edge to add.</param>
        void IPropertyVertex.AddOutEdge(IPropertyEdge Edge)
        {
            SuperVertex.AddOutEdge(Edge);
        }

        #endregion


        #region OutEdges(params EdgeLabels)      // OutEdges()!

        /// <summary>
        /// The edges emanating from, or leaving, this vertex
        /// filtered by their label. If no label was given,
        /// all edges will be returned.
        /// </summary>
        IEnumerable<IPropertyEdge> IPropertyVertex.OutEdges(params String[] EdgeLabels)
        {

            return from   Edge
                   in     SuperVertex.OutEdges(EdgeLabels)
                   select Edge as IPropertyEdge;

        }

        #endregion

        #region OutEdges(EdgeFilter)

        /// <summary>
        /// The edges emanating from, or leaving, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        IEnumerable<IPropertyEdge> IPropertyVertex.OutEdges(EdgeFilter<UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object> EdgeFilter)
        
        {

            return from   Edge
                   in     _OutEdgesWhenVertex
                   where  EdgeFilter(Edge)
                   select Edge as IPropertyEdge;

        }

        #endregion

        #region OutDegree(params EdgeLabels)     // OutDegree()!

        /// <summary>
        /// The number of edges emanating from, or leaving, this vertex
        /// filtered by their label. If no label was given,
        /// all edges will be returned.
        /// </summary>
        UInt64 IPropertyVertex.OutDegree(params String[] EdgeLabels)
        {
            return SuperVertex.OutDegree(EdgeLabels);
        }

        #endregion

        #region OutDegree(EdgeFilter)

        /// <summary>
        /// The number of edges emanating from, or leaving, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        UInt64 IPropertyVertex.OutDegree(EdgeFilter<UInt64, Int64, String, String, Object,
                                                    UInt64, Int64, String, String, Object,
                                                    UInt64, Int64, String, String, Object,
                                                    UInt64, Int64, String, String, Object> EdgeFilter)
        {
            return SuperVertex.OutDegree(EdgeFilter);
        }

        #endregion


        #region RemoveOutEdges(params Edges)    // RemoveOutEdges()!

        /// <summary>
        /// Remove outgoing edges.
        /// </summary>
        /// <param name="Edges">An array of outgoing edges to be removed.</param>
        void IPropertyVertex.RemoveOutEdges(params IPropertyEdge[] Edges)
        {
            SuperVertex.RemoveOutEdges(Edges);
        }

        #endregion

        #region RemoveOutEdges(EdgeFilter)

        /// <summary>
        /// Remove any outgoing edge matching the
        /// given edge filter delegate.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        void IPropertyVertex.RemoveOutEdges(EdgeFilter<UInt64, Int64, String, String, Object,
                                                       UInt64, Int64, String, String, Object,
                                                       UInt64, Int64, String, String, Object,
                                                       UInt64, Int64, String, String, Object> EdgeFilter)

        {
            SuperVertex.RemoveOutEdges(EdgeFilter);
        }

        #endregion

        #endregion

        #region InEdge methods [IPropertyVertex]

        #region AddInEdge(Edge)

        /// <summary>
        /// Add an incoming edge.
        /// </summary>
        /// <param name="Edge">The edge to add.</param>
        void IPropertyVertex.AddInEdge(IPropertyEdge Edge)
        {
            SuperVertex.AddInEdge(Edge);
        }

        #endregion


        #region InEdges(params EdgeLabels)      // InEdges()!

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex
        /// filtered by their label. If no label was given,
        /// all edges will be returned.
        /// </summary>
        IEnumerable<IPropertyEdge> IPropertyVertex.InEdges(params String[] EdgeLabels)
        {

            return from   Edge
                   in     SuperVertex.InEdges(EdgeLabels)
                   select Edge as IPropertyEdge;

        }

        #endregion

        #region InEdges(EdgeFilter)

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        IEnumerable<IPropertyEdge> IPropertyVertex.InEdges(EdgeFilter<UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object> EdgeFilter)
        
        {

            return from   Edge
                   in     _InEdgesWhenVertex
                   where  EdgeFilter(Edge)
                   select Edge as IPropertyEdge;

        }

        #endregion

        #region InDegree(params EdgeLabels)     // InDegree()!

        /// <summary>
        /// The number of edges incoming to, or arriving at, this vertex
        /// filtered by their label. If no label was given,
        /// all edges will be returned.
        /// </summary>
        UInt64 IPropertyVertex.InDegree(params String[] EdgeLabels)
        {
            return SuperVertex.InDegree(EdgeLabels);
        }

        #endregion

        #region InDegree(EdgeFilter)

        /// <summary>
        /// The number of edges incoming to, or arriving at, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        UInt64 IPropertyVertex.InDegree(EdgeFilter<UInt64, Int64, String, String, Object,
                                                   UInt64, Int64, String, String, Object,
                                                   UInt64, Int64, String, String, Object,
                                                   UInt64, Int64, String, String, Object> EdgeFilter)

        {
            return SuperVertex.InDegree(EdgeFilter);
        }

        #endregion


        #region RemoveInEdges(params Edges)    // RemoveInEdges()!

        /// <summary>
        /// Remove incoming edges.
        /// </summary>
        /// <param name="Edges">An array of incoming edges to be removed.</param>
        void IPropertyVertex.RemoveInEdges(params IPropertyEdge[] Edges)
        {
            SuperVertex.RemoveInEdges(Edges);
        }

        #endregion

        #region RemoveInEdges(EdgeFilter)

        /// <summary>
        /// Remove any incoming edge matching the
        /// given edge filter delegate.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        void IPropertyVertex.RemoveInEdges(EdgeFilter<UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object> EdgeFilter)

        {
            SuperVertex.RemoveInEdges(EdgeFilter);
        }

        #endregion

        #endregion

        #region MultiEdge methods [IPropertyVertex]

        #region AddMultiEdge(MultiEdge)

        /// <summary>
        /// Add a multiedge.
        /// </summary>
        /// <param name="MultiEdge">The multiedge to add.</param>
        void IPropertyVertex.AddMultiEdge(IPropertyMultiEdge MultiEdge)
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
        IEnumerable<IPropertyMultiEdge> IPropertyVertex.MultiEdges(params String[] MultiEdgeLabels)
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
        IEnumerable<IPropertyMultiEdge> IPropertyVertex.MultiEdges(MultiEdgeFilter<UInt64, Int64, String, String, Object,
                                                                                   UInt64, Int64, String, String, Object,
                                                                                   UInt64, Int64, String, String, Object,
                                                                                   UInt64, Int64, String, String, Object> MultiEdgeFilter)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region RemoveMultiEdges(params MultiEdges)    // RemoveMultiEdges()!

        /// <summary>
        /// Remove multiedges.
        /// </summary>
        /// <param name="MultiEdges">An array of outgoing edges to be removed.</param>
        void IPropertyVertex.RemoveMultiEdges(params IPropertyMultiEdge[] MultiEdges)
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
        void IPropertyVertex.RemoveMultiEdges(MultiEdgeFilter<UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object> MultiEdgeFilter)

        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region HyperEdge methods [IPropertyVertex]

        #region AddHyperEdge(HyperEdge)

        /// <summary>
        /// Add a hyperedge.
        /// </summary>
        /// <param name="HyperEdge">The hyperedge to add.</param>
        void IPropertyVertex.AddHyperEdge(IPropertyHyperEdge HyperEdge)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region HyperEdges(params HyperEdgeLabels)      // HyperEdges()!

        /// <summary>
        /// The hyperedges emanating from, or leaving, this vertex
        /// filtered by their label. If no label was given,
        /// all hyperedges will be returned.
        /// </summary>
        IEnumerable<IPropertyHyperEdge> IPropertyVertex.HyperEdges(params String[] HyperEdgeLabels)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region HyperEdges(HyperEdgeFilter)

        /// <summary>
        /// The hyperedges emanating from, or leaving, this vertex
        /// filtered by the given hyperedge filter delegate.
        /// </summary>
        /// <param name="HyperEdgeFilter">A delegate for hyperedge filtering.</param>
        IEnumerable<IPropertyHyperEdge> IPropertyVertex.HyperEdges(HyperEdgeFilter<UInt64, Int64, String, String, Object,
                                                                                   UInt64, Int64, String, String, Object,
                                                                                   UInt64, Int64, String, String, Object,
                                                                                   UInt64, Int64, String, String, Object> HyperEdgeFilter)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region RemoveHyperEdges(params HyperEdges)    // RemoveHyperEdges()!

        /// <summary>
        /// Remove hyperedges.
        /// </summary>
        /// <param name="HyperEdges">An array of outgoing edges to be removed.</param>
        void IPropertyVertex.RemoveHyperEdges(params IPropertyHyperEdge[] HyperEdges)
        {
            SuperVertex.RemoveHyperEdges(HyperEdges);
        }

        #endregion

        #region RemoveHyperEdges(HyperEdgeFilter = null)

        /// <summary>
        /// Remove any outgoing hyperedge matching
        /// the given hyperedge filter delegate.
        /// </summary>
        /// <param name="HyperEdgeFilter">A delegate for hyperedge filtering.</param>
        void IPropertyVertex.RemoveHyperEdges(HyperEdgeFilter<UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object> HyperEdgeFilter)
        {
            SuperVertex.RemoveHyperEdges(HyperEdgeFilter);
        }

        #endregion

        #endregion


        // IPropertyGraph

        #region Vertex methods [IPropertyGraph]

        #region AddVertex(VertexInitializer = null)

        /// <summary>
        /// Adds a vertex to the graph using the given VertexId and initializes
        /// the vertex by invoking the given vertex initializer.
        /// </summary>
        /// <param name="VertexInitializer">A delegate to initialize the new vertex.</param>
        /// <returns>The new vertex</returns>
        IPropertyVertex IPropertyGraph.AddVertex(VertexInitializer<UInt64, Int64, String, String, Object,
                                                                   UInt64, Int64, String, String, Object,
                                                                   UInt64, Int64, String, String, Object,
                                                                   UInt64, Int64, String, String, Object> VertexInitializer)
        {
            return Subgraph.AddVertex(VertexInitializer) as IPropertyVertex;
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
        IPropertyVertex IPropertyGraph.AddVertex(String Label,
                                                 VertexInitializer<UInt64, Int64, String, String, Object,
                                                                   UInt64, Int64, String, String, Object,
                                                                   UInt64, Int64, String, String, Object,
                                                                   UInt64, Int64, String, String, Object> VertexInitializer)
        {
            return Subgraph.AddVertex(Label, VertexInitializer) as IPropertyVertex;
        }

        #endregion

        #region AddVertex(Id, Label, VertexInitializer = null)

        /// <summary>
        /// Adds a vertex to the graph using the given VertexId and initializes
        /// the vertex by invoking the given vertex initializer.
        /// </summary>
        /// <param name="Id">A VertexId. If none was given a new one will be generated.</param>
        /// <param name="Label">The label (or type) of a vertex.</param>
        /// <param name="VertexInitializer">A delegate to initialize the new vertex.</param>
        /// <returns>The new vertex</returns>
        IPropertyVertex IPropertyGraph.AddVertex(UInt64 Id,
                                                 String Label,
                                                 VertexInitializer<UInt64, Int64, String, String, Object,
                                                                   UInt64, Int64, String, String, Object,
                                                                   UInt64, Int64, String, String, Object,
                                                                   UInt64, Int64, String, String, Object> VertexInitializer)
        {
            return Subgraph.AddVertex(Id, Label, VertexInitializer) as IPropertyVertex;
        }

        #endregion

        #region AddVertex(IPropertyVertex)

        /// <summary>
        /// Adds the given vertex to the graph, and returns it again.
        /// An exception will be thrown if the vertex identifier is already being
        /// used by the graph to reference another vertex.
        /// </summary>
        /// <param name="IPropertyVertex">An IPropertyVertex.</param>
        /// <returns>The given IPropertyVertex.</returns>
        IPropertyVertex IPropertyGraph.AddVertex(IPropertyVertex Vertex)
        {
            return Subgraph.AddVertex(Vertex) as IPropertyVertex;
        }

        #endregion


        #region VertexById(VertexId)

        /// <summary>
        /// Return the vertex referenced by the given vertex identifier.
        /// If no vertex is referenced by the identifier return null.
        /// </summary>
        /// <param name="VertexId">A vertex identifier.</param>
        IPropertyVertex IPropertyGraph.VertexById(UInt64 VertexId)
        {
            return Subgraph.VertexById(VertexId) as IPropertyVertex;
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

            return from   Vertex
                   in     Subgraph.VerticesById(VertexIds)
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

            return from   Vertex
                   in     Subgraph.VerticesByLabel(VertexLabels)
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
                                                                          UInt64, Int64, String, String, Object> VertexFilter)
        {

            return from   Vertex
                   in     Subgraph.Vertices(VertexFilter)
                   select Vertex as IPropertyVertex;

        }

        #endregion

        #region NumberOfVertices(VertexFilter = null)

        /// <summary>
        /// Return the current number of vertices matching the given optional vertex filter.
        /// When the filter is null, this method should use implement an optimized
        /// way to get the currenty number of vertices.
        /// </summary>
        /// <param name="VertexFilter">A delegate for vertex filtering.</param>
        UInt64 IPropertyGraph.NumberOfVertices(VertexFilter<UInt64, Int64, String, String, Object,
                                                            UInt64, Int64, String, String, Object,
                                                            UInt64, Int64, String, String, Object,
                                                            UInt64, Int64, String, String, Object> VertexFilter)
        {
            return Subgraph.NumberOfVertices(VertexFilter);
        }

        #endregion


        #region RemoveVerticesById(params VertexIds)

        /// <summary>
        /// Remove the vertex identified by the given VertexId from the graph
        /// </summary>
        /// <param name="VertexIds">An array of VertexIds of the vertices to remove.</param>
        void IPropertyGraph.RemoveVerticesById(params UInt64[] VertexIds)
        {
            Subgraph.RemoveVerticesById(VertexIds);
        }

        #endregion

        #region RemoveVertices(params Vertices)

        /// <summary>
        /// Remove the given array of vertices from the graph.
        /// Upon removing a vertex, all the edges by which the vertex
        /// is connected will be removed as well.
        /// </summary>
        /// <param name="Vertices">An array of vertices to be removed from the graph.</param>
        void IPropertyGraph.RemoveVertices(params IPropertyVertex[] Vertices)
        {
            Subgraph.RemoveVertices(Vertices);
        }

        #endregion

        #region RemoveVertices(VertexFilter = null)

        /// <summary>
        /// Remove each vertex matching the given filter.
        /// </summary>
        /// <param name="VertexFilter">A delegate for vertex filtering.</param>
        void IPropertyGraph.RemoveVertices(VertexFilter<UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object,
                                                        UInt64, Int64, String, String, Object> VertexFilter)
        {
            Subgraph.RemoveVertices(VertexFilter);
        }

        #endregion

        #endregion

        #region Edge methods [IPropertyGraph]

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
        IPropertyEdge IPropertyGraph.AddEdge(IPropertyVertex OutVertex,
                                             String          Label,
                                             IPropertyVertex InVertex,
                                             EdgeInitializer<UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object> EdgeInitializer)

        {
            return Subgraph.AddEdge(OutVertex, Label, InVertex, EdgeInitializer) as IPropertyEdge;
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
        IPropertyEdge IPropertyGraph.AddEdge(UInt64          EdgeId,
                                             IPropertyVertex OutVertex,
                                             String          Label,
                                             IPropertyVertex InVertex,
                                             EdgeInitializer<UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object> EdgeInitializer)
        {
            return Subgraph.AddEdge(EdgeId, OutVertex, Label, InVertex, EdgeInitializer) as IPropertyEdge;
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
        IPropertyEdge IPropertyGraph.AddEdge(IPropertyVertex OutVertex,
                                             IPropertyVertex InVertex,
                                             String          Label,
                                             EdgeInitializer<UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object> EdgeInitializer)
        {
            return Subgraph.AddEdge(OutVertex, InVertex, Label, EdgeInitializer) as IPropertyEdge;
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
        IPropertyEdge IPropertyGraph.AddEdge(IPropertyVertex OutVertex,
                                             IPropertyVertex InVertex,
                                             UInt64          EdgeId,
                                             String          Label,
                                             EdgeInitializer<UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object> EdgeInitializer)
        {
            return Subgraph.AddEdge(OutVertex, InVertex, EdgeId, Label, EdgeInitializer) as IPropertyEdge;
        }

        #endregion

        #region AddEdge(IPropertyEdge)

        /// <summary>
        /// Adds the given edge to the graph, and returns it again.
        /// An exception will be thrown if the edge identifier is already being
        /// used by the graph to reference another edge.
        /// </summary>
        /// <param name="IPropertyEdge">An IPropertyEdge.</param>
        /// <returns>The given IPropertyEdge.</returns>
        IPropertyEdge IPropertyGraph.AddEdge(IPropertyEdge IPropertyEdge)
        {
            return Subgraph.AddEdge(IPropertyEdge) as IPropertyEdge;
        }

        #endregion


        #region EdgeById(EdgeId)

        /// <summary>
        /// Return the edge referenced by the given edge identifier.
        /// If no edge is referenced by a given identifier return null.
        /// </summary>
        /// <param name="EdgeId">An edge identifier.</param>
        IPropertyEdge IPropertyGraph.EdgeById(UInt64 EdgeId)
        {
            return Subgraph.EdgeById(EdgeId) as IPropertyEdge;
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

            return from   Edge
                   in     Subgraph.EdgesById(EdgeIds)
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

            return from   Edge
                   in     Subgraph.EdgesByLabel(EdgeLabels)
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
                                                                   UInt64, Int64, String, String, Object> EdgeFilter)
        {

            return from Edge
                   in Subgraph.Edges(EdgeFilter)
                   select Edge as IPropertyEdge;

        }

        #endregion

        #region NumberOfEdges(EdgeFilter = null)

        /// <summary>
        /// Return the current number of edges matching the given optional edge filter.
        /// When the filter is null, this method should use implement an optimized
        /// way to get the currenty number of edges.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        UInt64 IPropertyGraph.NumberOfEdges(EdgeFilter<UInt64, Int64, String, String, Object,
                                                       UInt64, Int64, String, String, Object,
                                                       UInt64, Int64, String, String, Object,
                                                       UInt64, Int64, String, String, Object> EdgeFilter)
        {
            return Subgraph.NumberOfEdges(EdgeFilter);
        }

        #endregion


        #region RemoveEdgesById(params EdgeIds)

        /// <summary>
        /// Remove the given array of edges identified by their EdgeIds.
        /// </summary>
        /// <param name="EdgeIds">An array of EdgeIds of the edges to remove</param>
        void IPropertyGraph.RemoveEdgesById(params UInt64[] EdgeIds)
        {
            Subgraph.RemoveEdgesById(EdgeIds);
        }

        #endregion

        #region RemoveEdges(params Edges)

        /// <summary>
        /// Remove the given array of edges from the graph.
        /// </summary>
        /// <param name="Edges">An array of edges to be removed from the graph.</param>
        void IPropertyGraph.RemoveEdges(params IPropertyEdge[] Edges)
        {
            Subgraph.RemoveEdges(Edges);
        }

        #endregion

        #region RemoveEdges(EdgeFilter = null)

        /// <summary>
        /// Remove any edge matching the given edge filter.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        void IPropertyGraph.RemoveEdges(EdgeFilter<UInt64, Int64, String, String, Object,
                                                   UInt64, Int64, String, String, Object,
                                                   UInt64, Int64, String, String, Object,
                                                   UInt64, Int64, String, String, Object> EdgeFilter)
        {
            Subgraph.RemoveEdges(EdgeFilter);
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

        #region GetMetaObject(Expression)

        /// <summary>
        /// Return the appropriate DynamicMetaObject.
        /// </summary>
        /// <param name="Expression">An Expression.</param>
        public override DynamicMetaObject GetMetaObject(Expression Expression)
        {
            return new DynamicGraphElement<PropertyVertex> (Expression, this);
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

            // Check if the given object can be casted to a PropertyVertex
            var PropertyVertex = Object as PropertyVertex;

            if ((Object) PropertyVertex == null)
                throw new ArgumentException("The given object is not a PropertyVertex!");

            return CompareTo(PropertyVertex);

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
        /// Return a string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return "IPropertyVertex [Id: " + Id.ToString() + ", " + _OutEdgesWhenVertex.Count() + " OutEdges, " + _InEdgesWhenVertex.Count() + " InEdges]";
        }

        #endregion





        public new IReadOnlyGenericPropertyGraph<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> Graph
        {
            get { throw new NotImplementedException(); }
        }

        IReadOnlyGenericPropertyGraph<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.AsSubgraph
        {
            get { throw new NotImplementedException(); }
        }

        IEnumerable<IReadOnlyGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.OutEdges(params string[] EdgeLabels)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.OutEdges(EdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> EdgeFilter)
        {
            throw new NotImplementedException();
        }

        ulong IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.OutDegree(params string[] EdgeLabels)
        {
            throw new NotImplementedException();
        }

        ulong IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.OutDegree(EdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> EdgeFilter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.InEdges(params string[] EdgeLabels)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.InEdges(EdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> EdgeFilter)
        {
            throw new NotImplementedException();
        }

        ulong IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.InDegree(params string[] EdgeLabels)
        {
            throw new NotImplementedException();
        }

        ulong IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.InDegree(EdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> EdgeFilter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyMultiEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.MultiEdges(params string[] MultiEdgeLabels)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyMultiEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.MultiEdges(MultiEdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> MultiEdgeFilter)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.HyperEdges(params string[] HyperEdgeLabels)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.HyperEdges(HyperEdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> HyperEdgeFilter)
        {
            throw new NotImplementedException();
        }

        public new System.Collections.IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public event GraphShuttingdownEventHandler<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> OnGraphShuttingdown;

        public void Shutdown(string Message = "")
        {
            throw new NotImplementedException();
        }

        public event GraphShutteddownEventHandler<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> OnGraphShutteddown;

        IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> IReadOnlyVertexMethods<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.VertexById(ulong VertexId)
        {
            throw new NotImplementedException();
        }

        public bool TryGetVertexById(ulong VertexId, out IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> Vertex)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IReadOnlyVertexMethods<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.VerticesById(params ulong[] VertexIds)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IReadOnlyVertexMethods<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.VerticesByLabel(params string[] VertexLabels)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IReadOnlyVertexMethods<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.Vertices(VertexFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> VertexFilter = null)
        {
            throw new NotImplementedException();
        }

        ulong IReadOnlyVertexMethods<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.NumberOfVertices(VertexFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> VertexFilter = null)
        {
            throw new NotImplementedException();
        }

        IReadOnlyGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> IReadOnlyEdgeMethods<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.EdgeById(ulong EdgeId)
        {
            throw new NotImplementedException();
        }

        public bool TryGetEdgeById(ulong EdgeId, out IReadOnlyGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> Edge)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IReadOnlyEdgeMethods<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.EdgesById(params ulong[] EdgeIds)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IReadOnlyEdgeMethods<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.EdgesByLabel(params string[] EdgeLabels)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IReadOnlyEdgeMethods<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.Edges(EdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> EdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        ulong IReadOnlyEdgeMethods<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.NumberOfEdges(EdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> EdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyGenericPropertyMultiEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> MultiEdgeById(ulong MultiEdgeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IReadOnlyGenericPropertyMultiEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> MultiEdgesById(params ulong[] MultiEdgeIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IReadOnlyGenericPropertyMultiEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> MultiEdgesByLabel(params string[] MultiEdgeLabels)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyMultiEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IReadOnlyMultiEdgeMethods<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.MultiEdges(MultiEdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> MultiEdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        public ulong NumberOfMultiEdges(MultiEdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> MultiEdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> HyperEdgeById(ulong HyperEdgeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IReadOnlyGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> HyperEdgesById(params ulong[] HyperEdgeIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IReadOnlyGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> HyperEdgesByLabel(params string[] HyperEdgeLabels)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IReadOnlyHyperEdgeMethods<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.HyperEdges(HyperEdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> HyperEdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        public ulong NumberOfHyperEdges(HyperEdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> HyperEdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddVertex(IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> Vertex)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddVertexIfNotExists(IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> Vertex, CheckVertexExistanceInGraph<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> CheckExistanceDelegate = null)
        {
            throw new NotImplementedException();
        }

        void IVertexMethods<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.RemoveVerticesById(params ulong[] VertexIds)
        {
            throw new NotImplementedException();
        }

        public void RemoveVertices(params IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>[] Vertices)
        {
            throw new NotImplementedException();
        }

        void IVertexMethods<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.RemoveVertices(VertexFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> VertexFilter = null)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddEdge(IReadOnlyGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> Edge)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddEdgeIfNotExists(IReadOnlyGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> Edge, CheckEdgeExistanceInGraph<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> CheckExistanceDelegate = null)
        {
            throw new NotImplementedException();
        }

        void IEdgeMethods<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.RemoveEdgesById(params ulong[] EdgeIds)
        {
            throw new NotImplementedException();
        }

        public void RemoveEdges(params IReadOnlyGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>[] Edges)
        {
            throw new NotImplementedException();
        }

        void IEdgeMethods<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.RemoveEdges(EdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> EdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyGenericPropertyMultiEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddMultiEdge(IReadOnlyGenericPropertyMultiEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> MultiEdge)
        {
            throw new NotImplementedException();
        }

        public void RemoveMultiEdgesById(params ulong[] MultiEdgeIds)
        {
            throw new NotImplementedException();
        }

        public void RemoveMultiEdges(params IReadOnlyGenericPropertyMultiEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>[] MultiEdges)
        {
            throw new NotImplementedException();
        }

        void IMultiEdgeMethods<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.RemoveMultiEdges(MultiEdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> MultiEdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddHyperEdge(IReadOnlyGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> HyperEdge)
        {
            throw new NotImplementedException();
        }

        public void RemoveHyperEdgesById(params ulong[] HyperEdgeIds)
        {
            throw new NotImplementedException();
        }

        public void RemoveHyperEdges(params IReadOnlyGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>[] HyperEdges)
        {
            throw new NotImplementedException();
        }

        void IHyperEdgeMethods<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.RemoveHyperEdges(HyperEdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> HyperEdgeFilter = null)
        {
            throw new NotImplementedException();
        }
    }

}
