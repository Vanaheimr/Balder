/*
 * Copyright (c) 2010-2014, Achim 'ahzf' Friedland <achim@graphdefined.org>
 * This file is part of Balder <http://www.github.com/Vanaheimr/Balder>
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

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Balder;

#endregion

namespace org.GraphDefined.Vanaheimr.Balder.DependentGraphs
{

    /// <summary>
    /// Extention methods for adjacency matrix graphs.
    /// </summary>
    public static class AdjacencyMatrixGraphExtentions
    {

        #region ToAdjacencyMatrixGraph<TEdgeData>(this PropertyGraph, EdgeValueConverter = null, MaxNumberOfVertices = 0, VertexSelector = null, EdgeSelector = null, ContinuousLearning = true)

        /// <summary>
        /// Attach a adjacency matrix graph to the given property graph.
        /// </summary>
        /// <typeparam name="TEdgeData">The type of the edge data to be stored within the adjacency matrix graph.</typeparam>
        /// <param name="PropertyGraph">A generic property graph to connect to.</param>
        /// <param name="EdgeValueConverter">A delegate to convert an edge to a value of type TEdgeData to be stored within the adjacency matrix graph.</param>
        /// <param name="MaxNumberOfVertices">The maximum number of vertices to be stored.</param>
        /// <param name="VertexSelector">A delegate to select the interesting vertices to be processed.</param>
        /// <param name="EdgeSelector">A delegate to select the interesting edges to be processed.</param>
        /// <param name="ContinuousLearning">If set to true, the adjacency matrix graph will subsribe vertex/edge additions in order to continuously update itself.</param>
        public static AdjacencyMatrixGraph<TEdgeData> ToAdjacencyMatrixGraph<TEdgeData>(

                          this IReadOnlyGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object> PropertyGraph,

                          Func<IReadOnlyGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                            UInt64, Int64, String, String, Object,
                                                            UInt64, Int64, String, String, Object,
                                                            UInt64, Int64, String, String, Object>, TEdgeData> EdgeValueConverter = null,

                          UInt64 MaxNumberOfVertices = 0,

                          VertexFilter<UInt64, Int64, String, String, Object,
                                       UInt64, Int64, String, String, Object,
                                       UInt64, Int64, String, String, Object,
                                       UInt64, Int64, String, String, Object> VertexSelector = null,

                          EdgeFilter  <UInt64, Int64, String, String, Object,
                                       UInt64, Int64, String, String, Object,
                                       UInt64, Int64, String, String, Object,
                                       UInt64, Int64, String, String, Object> EdgeSelector = null,

                          Boolean ContinuousLearning = true)

        {

            #region Create AdjacencyMatrixGraph

            var _AdjacencyMatrixGraph = new AdjacencyMatrixGraph<TEdgeData>((MaxNumberOfVertices > 0) ? MaxNumberOfVertices : PropertyGraph.NumberOfVertices(VertexSelector));

            if (EdgeValueConverter == null)
                EdgeValueConverter = PropertyEdge => (TEdgeData) (Object) PropertyEdge.Label;

            if (VertexSelector == null)
                VertexSelector = v => true;

            if (EdgeSelector == null)
                EdgeSelector = e => true;

            #endregion

            #region Copy all current vertices and edges to the AdjacencyMatrixGraph

            PropertyGraph.Vertices(VertexSelector).ForEach(Vertex => {

                var EdgeValueAndVertex = (from   Edge
                                          in     Vertex.OutEdges(EdgeSelector)
                                          where  EdgeValueConverter(Edge) != null
                                          select new {
                                              Value     = EdgeValueConverter(Edge),
                                              InVertex  = Edge.InVertex
                                          }).FirstOrDefault();

                _AdjacencyMatrixGraph.AddEdge(Vertex.Id, EdgeValueAndVertex.Value, EdgeValueAndVertex.InVertex.Id);

            });

            #endregion

            #region Setup continuous learning

            if (ContinuousLearning)
            {

                var MutabelPropertyGraph = PropertyGraph.AsMutable();

                if (MutabelPropertyGraph != null)
                {

                    // Edge addition
                    MutabelPropertyGraph.OnEdgeAddition.OnNotification += (g, Edge) => {

                        if (VertexSelector(Edge.OutVertex))
                            if (EdgeSelector(Edge))
                                if (EdgeValueConverter(Edge) != null)
                                    _AdjacencyMatrixGraph.AddEdge(Edge.OutVertex.Id, EdgeValueConverter(Edge), Edge.InVertex.Id);

                    };

                    // Edge deletion
                    MutabelPropertyGraph.OnEdgeRemoval.OnNotification += (g, Edge) =>
                        _AdjacencyMatrixGraph.RemoveEdge(Edge.OutVertex.Id, Edge.InVertex.Id);

                }

            }

            #endregion

            return _AdjacencyMatrixGraph;

        }

        #endregion

        #region ToAdjacencyMatrixGraph<TEdgeData>(this PropertyGraph, EdgeKey, PropertyValue2EdgeDataConverter = null, MaxNumberOfVertices = 0, VertexSelector = null, EdgeSelector = null, ContinuousLearning = true)

        /// <summary>
        /// Attach a adjacency matrix graph to the given property graph.
        /// </summary>
        /// <typeparam name="TEdgeData">The type of the edge data to be stored within the adjacency matrix graph.</typeparam>
        /// <param name="PropertyGraph">A generic property graph to connect to.</param>
        /// <param name="EdgeKey">A property key to access the edge data to be stored within the adjacency matrix graph.</param>
        /// <param name="PropertyValue2EdgeDataConverter">A delegate to convert the property value of an edge to the edge data which will be stored within the adjacency matrix graph.</param>
        /// <param name="MaxNumberOfVertices">The maximum number of vertices to be stored.</param>
        /// <param name="VertexSelector">A delegate to select the interesting vertices to be processed.</param>
        /// <param name="EdgeSelector">A delegate to select the interesting edges to be processed.</param>
        /// <param name="ContinuousLearning">If set to true, the adjacency matrix graph will subsribe vertex/edge additions in order to continuously update itself.</param>
        public static AdjacencyMatrixGraph<TEdgeData> ToAdjacencyMatrixGraph<TEdgeData>(

                          this IReadOnlyGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object> PropertyGraph,

                          String                   EdgeKey,
                          Func<Object, TEdgeData>  PropertyValue2EdgeDataConverter  = null,
                          UInt64                   MaxNumberOfVertices = 0,

                          VertexFilter<UInt64, Int64, String, String, Object,
                                       UInt64, Int64, String, String, Object,
                                       UInt64, Int64, String, String, Object,
                                       UInt64, Int64, String, String, Object> VertexSelector = null,

                          EdgeFilter  <UInt64, Int64, String, String, Object,
                                       UInt64, Int64, String, String, Object,
                                       UInt64, Int64, String, String, Object,
                                       UInt64, Int64, String, String, Object> EdgeSelector = null,

                          Boolean ContinuousLearning = true)

        {

            #region Create AdjacencyMatrixGraph

            var _AdjacencyMatrixGraph = new AdjacencyMatrixGraph<TEdgeData>((MaxNumberOfVertices > 0) ? MaxNumberOfVertices : PropertyGraph.NumberOfVertices(VertexSelector));

            if (PropertyValue2EdgeDataConverter == null)
                PropertyValue2EdgeDataConverter = Value => (TEdgeData) (Object) Value;

            if (VertexSelector == null)
                VertexSelector = v => true;

            if (EdgeSelector == null)
                EdgeSelector = e => true;

            #endregion

            #region Copy all current vertices and edges to the AdjacencyMatrixGraph

            PropertyGraph.Vertices(VertexSelector).ForEach(Vertex => {

                var EdgeValueAndVertex = (from   Edge
                                          in     Vertex.OutEdges(EdgeSelector)
                                          select new {
                                              Value     = PropertyValue2EdgeDataConverter(Edge[EdgeKey]),
                                              InVertex  = Edge.InVertex
                                          }).FirstOrDefault();

                _AdjacencyMatrixGraph.AddEdge(Vertex.Id, EdgeValueAndVertex.Value, EdgeValueAndVertex.InVertex.Id);

            });

            #endregion

            #region Setup continuous learning

            if (ContinuousLearning)
            {

                var MutabelPropertyGraph = PropertyGraph.AsMutable();

                if (MutabelPropertyGraph != null)
                {

                    // Edge addition
                    MutabelPropertyGraph.OnEdgeAddition.OnNotification += (g, Edge) => {

                        Object EdgeValue = null;

                        if (VertexSelector(Edge.OutVertex))
                            if (EdgeSelector(Edge))
                                if (Edge.TryGetProperty(EdgeKey, out EdgeValue))
                                    _AdjacencyMatrixGraph.AddEdge(Edge.OutVertex.Id, PropertyValue2EdgeDataConverter(EdgeValue), Edge.InVertex.Id);

                    };

                    // Edge deletion
                    MutabelPropertyGraph.OnEdgeRemoval.OnNotification += (g, Edge) =>
                        _AdjacencyMatrixGraph.RemoveEdge(Edge.OutVertex.Id, Edge.InVertex.Id);

                }

            }

            #endregion

            return _AdjacencyMatrixGraph;

        }

        #endregion

    }


    #region AdjacencyMatrixGraph<TEdgeData>

    /// <summary>
    /// Storing a dense graph data within an adjacency matrix.
    /// </summary>
    /// <typeparam name="TEdgeData">The type of the edge data to be stored within the adjacency matrix graph.</typeparam>
    public class AdjacencyMatrixGraph<TEdgeData> : IEnumerable<AdjacencyMatrixGraph<TEdgeData>.Triple<TEdgeData>>
    {

        // Replace this by the "global" triple definition of Alviss!
        #region (class) Tripe<TEdgeData>

        /// <summary>
        /// A graph data triple.
        /// </summary>
        /// <typeparam name="TEdgeData">The type of the edge data to be stored.</typeparam>
        public struct Triple<TEdgeData>
        {

            #region Properties

            /// <summary>
            /// The outgoing vertex.
            /// </summary>
            public readonly UInt64     OutVertex;

            /// <summary>
            /// The edge data.
            /// </summary>
            public readonly TEdgeData  EdgeData;

            /// <summary>
            /// The incoming vertex.
            /// </summary>
            public readonly UInt64     InVertex;

            #endregion

            #region Triple(OutVertex, EdgeData, InVertex)

            /// <summary>
            /// A graph data triple.
            /// </summary>
            /// <param name="OutVertex"></param>
            /// <param name="EdgeData"></param>
            /// <param name="InVertex"></param>
            public Triple(UInt64 OutVertex, TEdgeData EdgeData, UInt64 InVertex)
            {
                this.OutVertex = OutVertex;
                this.EdgeData  = EdgeData;
                this.InVertex  = InVertex;
            }

            #endregion

        }

        #endregion


        #region Data

        private readonly TEdgeData[,]  AdjacencyMatrix;

        #endregion

        #region Properties

        /// <summary>
        /// The maximum number of vertices to be stored.
        /// </summary>
        public UInt64 MaxNumberOfVertices { get; private set; }

        #endregion

        #region Constructor(s)

        #region AdjacencyMatrixGraph(MaxNumberOfVertices)

        /// <summary>
        /// Create a new adjacency matrix graph.
        /// </summary>
        /// <param name="MaxNumberOfVertices">The maximum number of vertices to be stored.</param>
        public AdjacencyMatrixGraph(UInt64 MaxNumberOfVertices)
        {
            this.MaxNumberOfVertices  = MaxNumberOfVertices;
            this.AdjacencyMatrix      = new TEdgeData[MaxNumberOfVertices, MaxNumberOfVertices];
        }

        #endregion

        #endregion


        #region AddEdge(OutVertexId, EdgeValue, InVertexId)

        /// <summary>
        /// Add an edge to the adjacency matrix graph.
        /// </summary>
        /// <param name="OutVertexId">The identification of the outgoing vertex.</param>
        /// <param name="EdgeValue">The edge data to be stored.</param>
        /// <param name="InVertexId">The identification of the incoming vertex.</param>
        public void AddEdge(UInt64 OutVertexId, TEdgeData EdgeValue, UInt64 InVertexId)
        {

            if (OutVertexId >= MaxNumberOfVertices)
                throw new Exception("The value of the OutVertexId is invalid!");

            if (InVertexId >= MaxNumberOfVertices)
                throw new Exception("The value of the InVertexId is invalid!");

            AdjacencyMatrix[OutVertexId, InVertexId] = EdgeValue;

        }

        #endregion


        #region OutEdges(VertexId)

        public IEnumerable<TEdgeData> OutEdges(UInt64 VertexId)
        {
            for (var i = 0U; i < MaxNumberOfVertices; i++)
                if (AdjacencyMatrix[VertexId, i] != null)
                    yield return AdjacencyMatrix[VertexId, i];
        }

        #endregion

        #region Out(VertexId)

        public IEnumerable<UInt32> Out(UInt64 VertexId)
        {
            for (var i = 0U; i < MaxNumberOfVertices; i++)
                if (AdjacencyMatrix[VertexId, i] != null)
                    yield return i;
        }

        #endregion

        #region InEdges(VertexId)

        public IEnumerable<TEdgeData> InEdges(UInt64 VertexId)
        {
            for (var i = 0U; i < MaxNumberOfVertices; i++)
                if (AdjacencyMatrix[i, VertexId] != null)
                    yield return AdjacencyMatrix[VertexId, i];
        }

        #endregion

        #region In(VertexId)

        public IEnumerable<UInt32> In(UInt64 VertexId)
        {
            for (var i = 0U; i < MaxNumberOfVertices; i++)
                if (AdjacencyMatrix[i, VertexId] != null)
                    yield return i;
        }

        #endregion


        #region RemoveEdge(OutVertexId, InVertexId)

        /// <summary>
        /// Remove an edge from the adjacency matrix graph.
        /// </summary>
        /// <param name="OutVertexId">The identification of the outgoing vertex.</param>
        /// <param name="InVertexId">The identification of the incoming vertex.</param>
        public void RemoveEdge(UInt64 OutVertexId, UInt64 InVertexId)
        {

            if (OutVertexId >= MaxNumberOfVertices || InVertexId >= MaxNumberOfVertices)
                throw new Exception();

            AdjacencyMatrix[OutVertexId, InVertexId] = default(TEdgeData);

        }

        #endregion


        #region GetEnumerator()

        /// <summary>
        /// Enumerate all triples of the graph.
        /// </summary>
        public IEnumerator<Triple<TEdgeData>> GetEnumerator()
        {
            for (var OutVertex = 0UL; OutVertex < MaxNumberOfVertices; OutVertex++)
                for (var InVertex = 0UL; InVertex < MaxNumberOfVertices; InVertex++)
                    if (AdjacencyMatrix[OutVertex, InVertex] != null)
                        if (!AdjacencyMatrix[OutVertex, InVertex].Equals(default(TEdgeData)))
                            yield return new Triple<TEdgeData>(OutVertex, AdjacencyMatrix[OutVertex, InVertex], InVertex);
        }

        /// <summary>
        /// Enumerate all triples of the graph.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var OutVertex = 0UL; OutVertex < MaxNumberOfVertices; OutVertex++)
                for (var InVertex = 0UL; InVertex < MaxNumberOfVertices; InVertex++)
                    if (AdjacencyMatrix[OutVertex, InVertex] != null)
                        if (!AdjacencyMatrix[OutVertex, InVertex].Equals(default(TEdgeData)))
                            yield return new Triple<TEdgeData>(OutVertex, AdjacencyMatrix[OutVertex, InVertex], InVertex);
        }

        #endregion

    }

    #endregion

}
