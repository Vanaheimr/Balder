///*
// * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
// * This file is part of Blueprints.NET <http://www.github.com/ahzf/Blueprints.NET>
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
//using System.Collections.Generic;

//#endregion

//namespace de.ahzf.Blueprints.GenericGraph
//{

//    #region IGenericVertex<...>

//    /// <summary>
//    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
//    /// The outgoing edges are those edges for which the vertex is the tail.
//    /// The incoming edges are those edges for which the vertex is the head.
//    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
//    /// </summary>
//    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
//    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
//    /// <typeparam name="TDataVertex">The type of the embedded vertex data.</typeparam>
//    /// 
//    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
//    /// <typeparam name="TRevisionIdEdge">The type of the edge identifiers.</typeparam>
//    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
//    /// <typeparam name="TDataEdge">The type of the embedded edge data.</typeparam>
//    /// 
//    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
//    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
//    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
//    /// <typeparam name="TDataHyperEdge">The type of the embedded hyperedge data.</typeparam>
//    public interface IGenericVertex2<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TDataVertex,
//                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
//                                     TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TDataMultiEdge,
//                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>

//                                    : IGenericElement<TIdVertex, TRevisionIdVertex, TDataVertex>,
//                                      IGenericVertex

//        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable
//        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable
//        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable

//        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable
//        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable
//        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable

//    {

//        #region OutEdges

//        /// <summary>
//        /// Add an outgoing edge.
//        /// </summary>
//        /// <param name="myIEdge">The edge to add.</param>
//        void AddOutEdge(IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
//                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
//                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myIEdge);

//        /// <summary>
//        /// The edges emanating from, or leaving, this vertex.
//        /// </summary>
//        IEnumerable<IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
//                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
//                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>> OutEdges { get; }

//        /// <summary>
//        /// The edges emanating from, or leaving, this vertex
//        /// filtered by their label.
//        /// </summary>
//        IEnumerable<IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
//                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
//                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>> GetOutEdges(String myLabel);

//        /// <summary>
//        /// Remove an outgoing edge.
//        /// </summary>
//        /// <param name="myIEdge">The edge to remove.</param>
//        void RemoveOutEdge(IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
//                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
//                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myIEdge);

//        #endregion

//        #region InEdges

//        /// <summary>
//        /// Add an incoming edge.
//        /// </summary>
//        /// <param name="myIEdge">The edge to add.</param>
//        void AddInEdge(IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
//                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
//                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myIEdge);

//        /// <summary>
//        /// The edges incoming to, or arriving at, this vertex.
//        /// </summary>
//        IEnumerable<IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
//                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
//                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>> InEdges { get; }

//        /// <summary>
//        /// The edges incoming to, or arriving at, this vertex
//        /// filtered by their label.
//        /// </summary>
//        IEnumerable<IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
//                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
//                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>> GetInEdges(String myLabel);

//        /// <summary>
//        /// Remove an incoming edge.
//        /// </summary>
//        /// <param name="myIEdge">The edge to remove.</param>
//        void RemoveInEdge(IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
//                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
//                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myIEdge);

//        #endregion

//    }

//    #endregion

//}
