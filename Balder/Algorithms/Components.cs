///*
// * Copyright (c) 2010-2015, Achim 'ahzf' Friedland <achim@graphdefined.org>
// * This file is part of Balder <http://www.github.com/Vanaheimr/Balder>
// *
// * Licensed under the Apache License, Version 2.0 (the "License");
// * you may not use this file except in compliance with the License.
// * You may obtain a copy of the License at
// *
// *     http://www.apache.org/licenses/LICENSE-2.0
// *
// * Unless required by applicable law or agreed to in writing, software
// * distributed under the License is distributed on an "AS IS" BASIS,
// * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// * See the License for the specific language governing permissions and
// * limitations under the License.
// */

//#region Usings

//using System;
//using System.Linq;
//using System.Collections.Generic;

//using org.GraphDefined.Vanaheimr.Styx;
//using org.GraphDefined.Vanaheimr.Balder;

//#endregion

//namespace org.GraphDefined.Vanaheimr.Balder
//{

//    /// <summary>
//    /// Extension methods for the BothPipe.
//    /// </summary>
//    public static class Algorithms
//    {

//        #region GetNeighborEdges

//        private static void GetNeighborEdges<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
//                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
//                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
//                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>(

//                                   IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
//                                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
//                                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
//                                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge,

//                                   ref HashSet<IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
//                                                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
//                                                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
//                                                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> HashSet,

//                                   EdgeFilter<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex,
//                                                                             TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge,
//                                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
//                                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeSelector = null)


//            where TIdVertex        : IEquatable<TIdVertex>,       IComparable<TIdVertex>,       IComparable, TValueVertex
//            where TIdEdge          : IEquatable<TIdEdge>,         IComparable<TIdEdge>,         IComparable, TValueEdge
//            where TIdMultiEdge     : IEquatable<TIdMultiEdge>,    IComparable<TIdMultiEdge>,    IComparable, TValueMultiEdge
//            where TIdHyperEdge     : IEquatable<TIdHyperEdge>,    IComparable<TIdHyperEdge>,    IComparable, TValueHyperEdge

//            where TRevIdVertex     : IEquatable<TRevIdVertex>,    IComparable<TRevIdVertex>,    IComparable, TValueVertex
//            where TRevIdEdge       : IEquatable<TRevIdEdge>,      IComparable<TRevIdEdge>,      IComparable, TValueEdge
//            where TRevIdMultiEdge  : IEquatable<TRevIdMultiEdge>, IComparable<TRevIdMultiEdge>, IComparable, TValueMultiEdge
//            where TRevIdHyperEdge  : IEquatable<TRevIdHyperEdge>, IComparable<TRevIdHyperEdge>, IComparable, TValueHyperEdge

//            where TVertexLabel     : IEquatable<TVertexLabel>,    IComparable<TVertexLabel>,    IComparable, TValueVertex
//            where TEdgeLabel       : IEquatable<TEdgeLabel>,      IComparable<TEdgeLabel>,      IComparable, TValueEdge
//            where TMultiEdgeLabel  : IEquatable<TMultiEdgeLabel>, IComparable<TMultiEdgeLabel>, IComparable, TValueMultiEdge
//            where THyperEdgeLabel  : IEquatable<THyperEdgeLabel>, IComparable<THyperEdgeLabel>, IComparable, TValueHyperEdge

//            where TKeyVertex       : IEquatable<TKeyVertex>,      IComparable<TKeyVertex>,      IComparable
//            where TKeyEdge         : IEquatable<TKeyEdge>,        IComparable<TKeyEdge>,        IComparable
//            where TKeyMultiEdge    : IEquatable<TKeyMultiEdge>,   IComparable<TKeyMultiEdge>,   IComparable
//            where TKeyHyperEdge    : IEquatable<TKeyHyperEdge>,   IComparable<TKeyHyperEdge>,   IComparable


//        {

//            var Hash = new HashSet<IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
//                                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
//                                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
//                                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>();

//            foreach (var e in Edge.InVertex.BothE(EdgeSelector))
//                Hash.Add(e);

//            foreach (var e in Edge.OutVertex.BothE(EdgeSelector))
//                Hash.Add(e);

////            foreach (var edge in Edge.BothV().BothE(EdgeSelector))// <-- leads to an error!
//            foreach (var edge in Hash)
//            {
//                if (!HashSet.Contains(edge))
//                {
//                    HashSet.Add(edge);
//                    GetNeighborEdges(edge, ref HashSet, EdgeSelector);
//                }
//            }

//        }

//        #endregion


//        #region Components(this Graph, NewGraphGenerator = null)

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
//        /// <typeparam name="TRevIdVertex">The type of the vertex revision identifiers.</typeparam>
//        /// <typeparam name="TVertexLabel">The type of the vertex type.</typeparam>
//        /// <typeparam name="TKeyVertex">The type of the vertex property keys.</typeparam>
//        /// <typeparam name="TValueVertex">The type of the vertex property values.</typeparam>
//        /// 
//        /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
//        /// <typeparam name="TRevIdEdge">The type of the edge revision identifiers.</typeparam>
//        /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
//        /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
//        /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
//        /// 
//        /// <typeparam name="TIdMultiEdge">The type of the multiedge identifiers.</typeparam>
//        /// <typeparam name="TRevIdMultiEdge">The type of the multiedge revision identifiers.</typeparam>
//        /// <typeparam name="TMultiEdgeLabel">The type of the multiedge label.</typeparam>
//        /// <typeparam name="TKeyMultiEdge">The type of the multiedge property keys.</typeparam>
//        /// <typeparam name="TValueMultiEdge">The type of the multiedge property values.</typeparam>
//        /// 
//        /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
//        /// <typeparam name="TRevIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
//        /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
//        /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
//        /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
//        public static IEnumerable<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
//                                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
//                                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
//                                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

//                               Components<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
//                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
//                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
//                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>(

//                                          this IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
//                                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
//                                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
//                                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph,

//                                                      Func<IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
//                                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
//                                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
//                                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> NewGraphGenerator = null,

//                                          EdgeFilter<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex,
//                                                                             TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge,
//                                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
//                                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> SourceEdgeSelector = null)


//            where TIdVertex        : IEquatable<TIdVertex>,       IComparable<TIdVertex>,       IComparable, TValueVertex
//            where TIdEdge          : IEquatable<TIdEdge>,         IComparable<TIdEdge>,         IComparable, TValueEdge
//            where TIdMultiEdge     : IEquatable<TIdMultiEdge>,    IComparable<TIdMultiEdge>,    IComparable, TValueMultiEdge
//            where TIdHyperEdge     : IEquatable<TIdHyperEdge>,    IComparable<TIdHyperEdge>,    IComparable, TValueHyperEdge

//            where TRevIdVertex     : IEquatable<TRevIdVertex>,    IComparable<TRevIdVertex>,    IComparable, TValueVertex
//            where TRevIdEdge       : IEquatable<TRevIdEdge>,      IComparable<TRevIdEdge>,      IComparable, TValueEdge
//            where TRevIdMultiEdge  : IEquatable<TRevIdMultiEdge>, IComparable<TRevIdMultiEdge>, IComparable, TValueMultiEdge
//            where TRevIdHyperEdge  : IEquatable<TRevIdHyperEdge>, IComparable<TRevIdHyperEdge>, IComparable, TValueHyperEdge

//            where TVertexLabel     : IEquatable<TVertexLabel>,    IComparable<TVertexLabel>,    IComparable, TValueVertex
//            where TEdgeLabel       : IEquatable<TEdgeLabel>,      IComparable<TEdgeLabel>,      IComparable, TValueEdge
//            where TMultiEdgeLabel  : IEquatable<TMultiEdgeLabel>, IComparable<TMultiEdgeLabel>, IComparable, TValueMultiEdge
//            where THyperEdgeLabel  : IEquatable<THyperEdgeLabel>, IComparable<THyperEdgeLabel>, IComparable, TValueHyperEdge

//            where TKeyVertex       : IEquatable<TKeyVertex>,      IComparable<TKeyVertex>,      IComparable
//            where TKeyEdge         : IEquatable<TKeyEdge>,        IComparable<TKeyEdge>,        IComparable
//            where TKeyMultiEdge    : IEquatable<TKeyMultiEdge>,   IComparable<TKeyMultiEdge>,   IComparable
//            where TKeyHyperEdge    : IEquatable<TKeyHyperEdge>,   IComparable<TKeyHyperEdge>,   IComparable


//        {

//            if (SourceEdgeSelector == null)
//                SourceEdgeSelector = edge => true;

//            var SourceEdgeHashSet = new HashSet<IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
//                                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
//                                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
//                                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>(Graph.Edges(SourceEdgeSelector));

//            var GraphComponents   = new List<IReadOnlyGenericPropertyGraph  <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
//                                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
//                                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
//                                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>();

//            while (SourceEdgeHashSet.Count > 0)
//            {

//                var CurrentEdge  = SourceEdgeHashSet.First();
//                var CurrentEdges = new HashSet<IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
//                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
//                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
//                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>() { CurrentEdge };

//                GetNeighborEdges(CurrentEdge, ref CurrentEdges, SourceEdgeSelector);

//                foreach (var e in CurrentEdges)
//                    SourceEdgeHashSet.Remove(e);

//                var CurrentVertices = new HashSet<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
//                                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
//                                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
//                                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>();

//                foreach (var v in CurrentEdges.AsPipe().SelectMany(e => e.BothV().Select(v => v)))
//                    CurrentVertices.Add(v);

//                var CurrentGraph = NewGraphGenerator();

//                foreach (var v in CurrentVertices)
//                    CurrentGraph.AddVertex(v);

//                foreach (var e in CurrentEdges)
//                    CurrentGraph.AddEdge(e);

//                // What to do with multi- and hyperedges?

//                GraphComponents.Add(CurrentGraph);

//            }

//            return GraphComponents;

//        }

//        #endregion

//    }

//}
