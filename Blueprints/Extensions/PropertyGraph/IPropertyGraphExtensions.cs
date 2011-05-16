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
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

#endregion

namespace de.ahzf.blueprints.PropertyGraph
{

    /// <summary>
    /// Extensions to the IPropertyGraph interface
    /// </summary>
    public static class IPropertyGraphExtensions
    {

        #region AsDynamic(this myIPropertyGraph)

        /// <summary>
        /// Converts the given IPropertyGraph into a dynamic object.
        /// </summary>
        /// <param name="myIPropertyGraph">An object implementing IPropertyGraph.</param>
        /// <returns>A dynamic object</returns>
        public static dynamic AsDynamic<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>(

                              this IPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> myIPropertyGraph)

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

        {
            return myIPropertyGraph as dynamic;
        }

        #endregion


//        //#region AddVertex<TVertex>(this myIGenericGraph, myVertexId = null, myVertexInitializer = null)

//        ///// <summary>
//        ///// Adds a vertex of type TVertex to the graph using the given VertexId and
//        ///// initializes its properties by invoking the given vertex initializer.
//        ///// </summary>
//        ///// <typeparam name="TVertex">The type of the vertex to add.</typeparam>
//        ///// <param name="myIGenericGraph"></param>
//        ///// <param name="myVertexId">A VertexId. If none was given a new one will be generated.</param>
//        ///// <param name="myVertexInitializer">A delegate to initialize the newly generated vertex.</param>
//        ///// <returns>The new vertex</returns>
//        //public static TVertex AddVertex<TVertex, TIdVertex, TVertexData, TEdge, TIdEdge, TEdgeData, THyperEdge, TIdHyperEdge, THyperEdgeData, TRevisionId>(
//        //                      this IGenericGraph<TVertex, TIdVertex, TVertexData, TEdge, TIdEdge, TEdgeData, THyperEdge, TIdHyperEdge, THyperEdgeData, TRevisionId> myIGenericGraph,
//        //                      TIdVertex myVertexId = default(TIdVertex),
//        //                      Action<TVertex> myVertexInitializer = null)
//        //    where TVertex : class, IVertex
//        //{

//        //    if (myIGenericGraph == null)
//        //        throw new ArgumentNullException("myIGenericGraph must not be null!");

//        //    // Get constructor for TVertex
//        //    var _Type = typeof(TVertex).
//        //                GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
//        //                               null,
//        //                               new Type[] {
//        //                                   typeof(IGraph),
//        //                                   typeof(VertexId),
//        //                                   typeof(Action<IVertex>)
//        //                               },
//        //                               null);

//        //    if (_Type == null)
//        //        throw new ArgumentException("A appropriate constructor for type '" + typeof(TVertex).Name + "' could not be found!");


//        //    // Invoke constructor of TVertex
//        //    var _TVertex = _Type.Invoke(new Object[] { myIGenericGraph, myVertexId, myVertexInitializer }) as TVertex;

//        //    if (_TVertex == null)
//        //        throw new ArgumentException("A vertex of type '" + typeof(TVertex).Name + "' could not be created!");


//        //    // Add to IGraph
//        //    myIGenericGraph.AddVertex(myVertexId, myVertexInitializer);

//        //    return _TVertex;

//        //}

//        //#endregion

//        //#region GetVertex<TVertex>(this myIGenericGraph, myVertexId)

//        ///// <summary>
//        ///// Return the vertex referenced by the provided vertex identifier.
//        ///// If no vertex is referenced by that identifier, then return null.
//        ///// </summary>
//        ///// <typeparam name="TVertex">The type of the vertex.</typeparam>
//        ///// <param name="myIGenericGraph">An object implementing IGraph.</param>
//        ///// <param name="myVertexId">The identifier of the vertex to retrieved from the graph.</param>
//        ///// <returns>The vertex referenced by the provided identifier or null when no such vertex exists.</returns>
//        //public static TVertex GetVertex<TVertex, TIdVertex, TVertexData, TEdge, TIdEdge, TEdgeData, THyperEdge, TIdHyperEdge, THyperEdgeData, TRevisionId>(this IGenericGraph<TVertex, TIdVertex, TVertexData, TEdge, TIdEdge, TEdgeData, THyperEdge, TIdHyperEdge, THyperEdgeData, TRevisionId> myIGenericGraph, VertexId myVertexId)
//        //    where TVertex : class, IVertex
//        //{
//        //    return myIGenericGraph.GetVertex(myVertexId) as TVertex;
//        //}

//        //#endregion


//        #region AddEdge(this myIGenericGraph, myOutVertexId, myInVertexId, myEdgeId = null, myLabel = null, myEdgeInitializer = null)

//        /// <summary>
//        /// Adds an edge to the graph using the given myEdgeId and initializes
//        /// its properties by invoking the given edge initializer.
//        /// </summary>
//        /// <param name="myIGenericGraph"></param>
//        /// <param name="myOutVertexId"></param>
//        /// <param name="myInVertexId"></param>
//        /// <param name="myEdgeId">A EdgeId. If none was given a new one will be generated.</param>
//        /// <param name="myLabel"></param>
//        /// <param name="myEdgeInitializer">A delegate to initialize the newly generated edge.</param>
//        /// <returns>The new edge</returns>
//        public static TEdge AddEdge<TVertex, TIdVertex, TVertexData, TEdge, TIdEdge, TEdgeData, THyperEdge, TIdHyperEdge, THyperEdgeData, TRevisionId>(
//                            this IGenericGraph<TVertex, TIdVertex, TVertexData, TEdge, TIdEdge, TEdgeData, THyperEdge, TIdHyperEdge, THyperEdgeData, TRevisionId> myIGenericGraph,
//                            TIdVertex     myOutVertexId,
//                            TIdVertex     myInVertexId,
//                            TIdEdge       myEdgeId          = default(TIdEdge),
//                            String        myLabel           = null,
//                            Action<TEdge> myEdgeInitializer = null)

//            where TVertex      : IGenericVertex<TIdVertex, TVertexData>
//            where TIdVertex    : IEquatable<TIdVertex>,    IComparable<TIdVertex>,    IComparable
//            where TEdge        : IGenericEdge<TIdEdge, TEdgeData>
//            where TIdEdge      : IEquatable<TIdEdge>,      IComparable<TIdEdge>,      IComparable
//            where THyperEdge   : IGenericHyperEdge<TIdHyperEdge, THyperEdgeData>
//            where TIdHyperEdge : IEquatable<TIdHyperEdge>, IComparable<TIdHyperEdge>, IComparable
//        {

//            if (myIGenericGraph == null)
//                throw new ArgumentNullException("myIGenericGraph must not be null!");

//            if (myOutVertexId == null)
//                throw new ArgumentNullException("myOutVertexId must not be null!");

//            if (myInVertexId == null)
//                throw new ArgumentNullException("myInVertexId must not be null!");


//            var myOutVertex = myIGenericGraph.GetVertex(myOutVertexId);

//            if (myOutVertex == null)
//                throw new ArgumentException("VertexId '" + myOutVertexId + "' is unknown!");


//            var myInVertex  = myIGenericGraph.GetVertex(myInVertexId);

//            if (myInVertex == null)
//                throw new ArgumentException("VertexId '" + myInVertexId + "' is unknown!");


//            return myIGenericGraph.AddEdge(myOutVertex, myInVertex, myEdgeId, myLabel, myEdgeInitializer);


//        }

//        #endregion

//        //#region AddEdge<TEdge>(this myIGenericGraph, myOutVertex, myInVertex, myEdgeId = null, myLabel = null, myEdgeInitializer = null)

//        ///// <summary>
//        ///// Adds an edge to the graph using the given myEdgeId and initializes
//        ///// its properties by invoking the given edge initializer.
//        ///// </summary>
//        ///// <typeparam name="TEdge">The type of the edge to add.</typeparam>
//        ///// <param name="myIGenericGraph"></param>
//        ///// <param name="myOutVertex"></param>
//        ///// <param name="myInVertex"></param>
//        ///// <param name="myEdgeId">A EdgeId. If none was given a new one will be generated.</param>
//        ///// <param name="myLabel"></param>
//        ///// <param name="myEdgeInitializer">A delegate to initialize the newly generated edge.</param>
//        ///// <returns>The new edge</returns>
//        //public static TEdge AddEdge<TVertex, TIdVertex, TVertexData, TEdge, TIdEdge, TEdgeData, THyperEdge, TIdHyperEdge, THyperEdgeData, TRevisionId>(this IGenericGraph<TVertex, TIdVertex, TVertexData, TEdge, TIdEdge, TEdgeData, THyperEdge, TIdHyperEdge, THyperEdgeData, TRevisionId> myIGenericGraph, IVertex myOutVertex, IVertex myInVertex, EdgeId myEdgeId = null, String myLabel = null, Action<IEdge> myEdgeInitializer = null)
//        //    where TEdge : class, IEdge
//        //{

//        //    if (myIGenericGraph == null)
//        //        throw new ArgumentNullException("myIGenericGraph must not be null!");

//        //    // Get constructor for TEdge
//        //    var _Type  = typeof(TEdge).
//        //                 GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
//        //                                null,
//        //                                new Type[] {
//        //                                    typeof(IGraph),
//        //                                    typeof(IVertex),
//        //                                    typeof(IVertex),
//        //                                    typeof(EdgeId),
//        //                                    typeof(String),
//        //                                    typeof(Action<IEdge>)
//        //                                },
//        //                                null);

//        //    if (_Type == null)
//        //        throw new ArgumentException("A appropriate constructor for type '" + typeof(TEdge).Name + "' could not be found!");


//        //    // Invoke constructor of TEdge
//        //    var _TEdge = _Type.Invoke(new Object[] { myIGenericGraph, myOutVertex, myInVertex, myEdgeId, myEdgeInitializer }) as TEdge;
//        //    if (_TEdge == null)
//        //        throw new ArgumentException("An edge of type '" + typeof(TEdge).Name + "' could not be created!");


//        //    // Add to IGraph
//        //    myIGenericGraph.AddEdge(myOutVertex, myInVertex, myEdgeId, myLabel, myEdgeInitializer);

//        //    return _TEdge;

//        //}

//        //#endregion

//        //#region AddEdge<TEdge>(this myIGenericGraph, myOutVertexId, myInVertexId, myEdgeId = null, myLabel = null, myEdgeInitializer = null)
        
//        ///// <summary>
//        ///// Adds an edge to the graph using the given myEdgeId and initializes
//        ///// its properties by invoking the given edge initializer.
//        ///// </summary>
//        ///// <param name="myIGenericGraph"></param>
//        ///// <param name="myOutVertexId"></param>
//        ///// <param name="myInVertexId"></param>
//        ///// <param name="myEdgeId">A EdgeId. If none was given a new one will be generated.</param>
//        ///// <param name="myLabel"></param>
//        ///// <param name="myEdgeInitializer">A delegate to initialize the newly generated edge.</param>
//        ///// <returns>The new edge</returns>
//        //public static TEdge AddEdge<TVertex, TIdVertex, TVertexData, TEdge, TIdEdge, TEdgeData, THyperEdge, TIdHyperEdge, THyperEdgeData, TRevisionId>(this IGenericGraph<TVertex, TIdVertex, TVertexData, TEdge, TIdEdge, TEdgeData, THyperEdge, TIdHyperEdge, THyperEdgeData, TRevisionId> myIGenericGraph, VertexId myOutVertexId, VertexId myInVertexId, EdgeId myEdgeId = null, String myLabel = null, Action<IEdge> myEdgeInitializer = null)
//        //    where TEdge : class, IEdge
//        //{

//        //    if (myIGenericGraph == null)
//        //        throw new ArgumentNullException("myIGenericGraph must not be null!");

//        //    if (myOutVertexId == null)
//        //        throw new ArgumentNullException("myOutVertexId must not be null!");

//        //    if (myInVertexId == null)
//        //        throw new ArgumentNullException("myInVertexId must not be null!");


//        //    var myOutVertex = myIGenericGraph.GetVertex(myOutVertexId);

//        //    if (myOutVertex == null)
//        //        throw new ArgumentException("VertexId '" + myOutVertexId + "' is unknown!");


//        //    var myInVertex = myIGenericGraph.GetVertex(myInVertexId);

//        //    if (myInVertex == null)
//        //        throw new ArgumentException("VertexId '" + myInVertexId + "' is unknown!");


//        //    return myIGenericGraph.AddEdge<TEdge>(myOutVertex, myInVertex, myEdgeId, myLabel, myEdgeInitializer);


//        //}

//        //#endregion

        #region AddDoubleEdge(this myIGenericGraph, myOutVertexId, myInVertexId, myEdgeId1 = null, myEdgeId2 = null, myLabel = null, myEdgeInitializer = null)

        /// <summary>
        /// Adds an edge to the graph using the given myEdgeId and initializes
        /// its properties by invoking the given edge initializer.
        /// </summary>
        /// <param name="myIPropertyGraph"></param>
        /// <param name="myOutVertex"></param>
        /// <param name="myInVertex"></param>
        /// <param name="EdgeId1">An EdgeId. If none was given a new one will be generated.</param>
        /// <param name="EdgeId2">An EdgeId. If none was given a new one will be generated.</param>
        /// <param name="Label">A label for both edges.</param>
        /// <param name="Label1">A label for the first edge.</param>
        /// <param name="Label2">A label for the second edge.</param>
        /// <param name="EdgeInitializer1">A delegate to initialize the first generated edge.</param>
        /// <param name="EdgeInitializer2">A delegate to initialize the second generated edge.</param>
        /// <returns>Both new edges.</returns>
        public static Tuple<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,
                            IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                      AddDoubleEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>(

                                          this IPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> myIPropertyGraph,

                                          IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> myOutVertex,

                                          IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> myInVertex,

                                          TIdEdge    EdgeId1 = default(TIdEdge),
                                          TIdEdge    EdgeId2 = default(TIdEdge),
                                          TEdgeLabel Label   = default(TEdgeLabel),
                                          TEdgeLabel Label1  = default(TEdgeLabel),
                                          TEdgeLabel Label2  = default(TEdgeLabel),

                                          Action<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> EdgeInitializer1 = null,

                                          Action<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> EdgeInitializer2 = null)

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

        {

            if (myIPropertyGraph == null)
                throw new ArgumentNullException("myIGenericGraph must not be null!");

            if (myOutVertex == null)
                throw new ArgumentException("Vertex '" + myOutVertex + "' is unknown!");

            if (myInVertex == null)
                throw new ArgumentException("Vertex '" + myInVertex + "' is unknown!");

            if (Label1 == null)
                Label1 = Label;

            if (Label2 == null)
                Label2 = Label;

            return new Tuple<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                             IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                             (myIPropertyGraph.AddEdge(myOutVertex, myInVertex,  EdgeId1, Label1, EdgeInitializer1),
                              myIPropertyGraph.AddEdge(myInVertex,  myOutVertex, EdgeId2, Label2, EdgeInitializer2));


        }

        #endregion

//        //#region GetEdge<TEdge>(this myIGenericGraph, myEdgeId)

//        ///// <summary>
//        ///// Return the edge referenced by the provided edge identifier.
//        ///// If no edge is referenced by that identifier, then return null.
//        ///// </summary>
//        ///// <typeparam name="TEdge">The type of the edge.</typeparam>
//        ///// <param name="myIGenericGraph">An object implementing IGraph.</param>
//        ///// <param name="myEdgeId">The identifier of the edge to retrieved from the graph.</param>
//        ///// <returns>The edge referenced by the provided identifier or null when no such edge exists.</returns>
//        //public static TEdge GetEdge<TVertex, TIdVertex, TVertexData, TEdge, TIdEdge, TEdgeData, THyperEdge, TIdHyperEdge, THyperEdgeData, TRevisionId>(
//        //                            this IGenericGraph<TVertex, TIdVertex, TVertexData, TEdge, TIdEdge, TEdgeData, THyperEdge, TIdHyperEdge, THyperEdgeData, TRevisionId> myIGenericGraph,
//        //                            EdgeId myEdgeId)
//        //    where TEdge : class, IVertex
//        //{
//        //    return myIGenericGraph.GetEdge(myEdgeId) as TEdge;
//        //}

//        //#endregion


//        //#region VertexId(this myIGenericGraph, params myVertexIds)

//        ///// <summary>
//        ///// Transforms the given array of UInt64 into an array of
//        ///// VertexIds and returns a collection of IVertex objects
//        ///// for them.
//        ///// </summary>
//        ///// <param name="myIGenericGraph">A Blueprints graph.</param>
//        ///// <param name="myVertexIds">An array of unsigned intergers which can be transfored to VertexIds.</param>
//        ///// <returns>A collection of IVertex objects.</returns>
//        //public static IEnumerable<TVertex> VertexId<TVertex, TIdVertex, TVertexData, TEdge, TIdEdge, TEdgeData, THyperEdge, TIdHyperEdge, THyperEdgeData, TRevisionId>(
//        //                                   this IGenericGraph<TVertex, TIdVertex, TVertexData, TEdge, TIdEdge, TEdgeData, THyperEdge, TIdHyperEdge, THyperEdgeData, TRevisionId> myIGenericGraph,
//        //                                   params UInt64[] myVertexIds)
//        //{
//        //    return myIGenericGraph.GetVertices((from _VId in myVertexIds select new VertexId(_VId)).ToArray());
//        //}

//        //#endregion

//        //#region VertexId(this myIGenericGraph, params myVertexIds)

//        ///// <summary>
//        ///// Transforms the given array of strings into an array of
//        ///// VertexIds and returns a collection of IVertex objects
//        ///// for them.
//        ///// </summary>
//        ///// <param name="myIGenericGraph">A Blueprints graph.</param>
//        ///// <param name="myVertexIds">An array of strings which can be transfored to VertexIds.</param>
//        ///// <returns>A collection of IVertex objects.</returns>
//        //public static IEnumerable<TVertex> VertexId<TVertex, TIdVertex, TVertexData, TEdge, TIdEdge, TEdgeData, THyperEdge, TIdHyperEdge, THyperEdgeData, TRevisionId>(
//        //                                   this IGenericGraph<TVertex, TIdVertex, TVertexData, TEdge, TIdEdge, TEdgeData, THyperEdge, TIdHyperEdge, THyperEdgeData, TRevisionId> myIGenericGraph,
//        //                                   params String[] myVertexIds)
//        //{
//        //    return myIGenericGraph.GetVertices((from _VId in myVertexIds select new VertexId(_VId)).ToArray());
//        //}

//        //#endregion


//        //#region EdgeId(this myIGenericGraph, params myEdgeIds)

//        ///// <summary>
//        ///// Transforms the given array of UInt64 into an array of
//        ///// myEdgeIds and returns a collection of IEdge objects
//        ///// for them.
//        ///// </summary>
//        ///// <param name="myIGenericGraph">A Blueprints graph.</param>
//        ///// <param name="myEdgeIds">An array of unsigned intergers which can be transfored to myEdgeIds.</param>
//        ///// <returns>A collection of IEdge objects.</returns>
//        //public static IEnumerable<TEdge> EdgeId<TVertex, TIdVertex, TVertexData, TEdge, TIdEdge, TEdgeData, THyperEdge, TIdHyperEdge, THyperEdgeData, TRevisionId>(
//        //                                 this IGenericGraph<TVertex, TIdVertex, TVertexData, TEdge, TIdEdge, TEdgeData, THyperEdge, TIdHyperEdge, THyperEdgeData, TRevisionId> myIGenericGraph,
//        //                                 params UInt64[] myEdgeIds)
//        //{
//        //    return myIGenericGraph.GetEdges((from _EId in myEdgeIds select new EdgeId(_EId)).ToArray());
//        //}

//        //#endregion

//        //#region EdgeId(this myIGenericGraph, params myEdgeIds)

//        ///// <summary>
//        ///// Transforms the given array of strings into an array of
//        ///// myEdgeIds and returns a collection of IEdge objects
//        ///// for them.
//        ///// </summary>
//        ///// <param name="myIGenericGraph">A Blueprints graph.</param>
//        ///// <param name="myEdgeIds">An array of strings which can be transfored to myEdgeIds.</param>
//        ///// <returns>A collection of IEdge objects.</returns>
//        //public static IEnumerable<TEdge> EdgeId<TVertex, TIdVertex, TVertexData, TEdge, TIdEdge, TEdgeData, THyperEdge, TIdHyperEdge, THyperEdgeData, TRevisionId>(
//        //                                 this IGenericGraph<TVertex, TIdVertex, TVertexData, TEdge, TIdEdge, TEdgeData, THyperEdge, TIdHyperEdge, THyperEdgeData, TRevisionId> myIGenericGraph,
//        //                                 params String[] myEdgeIds)
//        //{
//        //    return myIGenericGraph.GetEdges((from _EId in myEdgeIds select new EdgeId(_EId)).ToArray());
//        //}

//        //#endregion

    }

}
