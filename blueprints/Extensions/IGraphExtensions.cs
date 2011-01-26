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
using System.Reflection;

using de.ahzf.blueprints.Datastructures;

#endregion

namespace de.ahzf.blueprints
{

    /// <summary>
    /// Extensions to the IGraph interface
    /// </summary>
    public static class IGraphExtensions
    {

        #region AsDynamic(this myIGraph)

        /// <summary>
        /// Converts the given IGraph into a dynamic object
        /// </summary>
        /// <param name="myIGraph">An object implementing IGraph.</param>
        /// <returns>A dynamic object</returns>
        public static dynamic AsDynamic(this IGraph myIGraph)
        {
            return myIGraph as dynamic;
        }

        #endregion


        #region AddVertex<TVertex>(this myIGraph, myVertexId = null, myVertexInitializer = null)

        /// <summary>
        /// Adds a vertex of type TVertex to the graph using the given VertexId and
        /// initializes its properties by invoking the given vertex initializer.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex to add.</typeparam>
        /// <param name="myIGraph"></param>
        /// <param name="myVertexId">A VertexId. If none was given a new one will be generated.</param>
        /// <param name="myVertexInitializer">A delegate to initialize the newly generated vertex.</param>
        /// <returns>The new vertex</returns>
        public static TVertex AddVertex<TVertex>(this IGraph myIGraph, VertexId myVertexId = null, Action<IVertex> myVertexInitializer = null)
            where TVertex : class, IVertex
        {

            if (myIGraph == null)
                throw new ArgumentNullException("myIGraph must not be null!");

            // Get constructor for TVertex
            var _Type = typeof(TVertex).
                        GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                                       null,
                                       new Type[] {
                                           typeof(IGraph),
                                           typeof(VertexId),
                                           typeof(Action<IVertex>)
                                       },
                                       null);

            if (_Type == null)
                throw new ArgumentException("A appropriate constructor for type '" + typeof(TVertex).Name + "' could not be found!");


            // Invoke constructor of TVertex
            var _TVertex = _Type.Invoke(new Object[] { myIGraph, myVertexId, myVertexInitializer }) as TVertex;

            if (_TVertex == null)
                throw new ArgumentException("A vertex of type '" + typeof(TVertex).Name + "' could not be created!");


            // Add to IGraph
            myIGraph.AddVertex(myVertexId, myVertexInitializer);

            return _TVertex;

        }

        #endregion

        #region GetVertex<TVertex>(this myIGraph, myVertexId)

        /// <summary>
        /// Return the vertex referenced by the provided vertex identifier.
        /// If no vertex is referenced by that identifier, then return null.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="myIGraph">An object implementing IGraph.</param>
        /// <param name="myVertexId">The identifier of the vertex to retrieved from the graph.</param>
        /// <returns>The vertex referenced by the provided identifier or null when no such vertex exists.</returns>
        public static TVertex GetVertex<TVertex>(this IGraph myIGraph, VertexId myVertexId)
            where TVertex : class, IVertex
        {
            return myIGraph.GetVertex(myVertexId) as TVertex;
        }

        #endregion


        #region AddEdge(this myIGraph, myOutVertex, myInVertex, myEdgeId = null, myLabel = null, myEdgeInitializer = null)

        /// <summary>
        /// Adds an edge to the graph using the given myEdgeId and initializes
        /// its properties by invoking the given edge initializer.
        /// </summary>
        /// <param name="myIGraph"></param>
        /// <param name="myOutVertexId"></param>
        /// <param name="myInVertexId"></param>
        /// <param name="myEdgeId">A EdgeId. If none was given a new one will be generated.</param>
        /// <param name="myLabel"></param>
        /// <param name="myEdgeInitializer">A delegate to initialize the newly generated edge.</param>
        /// <returns>The new edge</returns>
        public static IEdge AddEdge(this IGraph myIGraph, VertexId myOutVertexId, VertexId myInVertexId, EdgeId myEdgeId = null, String myLabel = null, Action<IEdge> myEdgeInitializer = null)
        {

            if (myIGraph == null)
                throw new ArgumentNullException("myIGraph must not be null!");

            if (myOutVertexId == null)
                throw new ArgumentNullException("myOutVertexId must not be null!");

            if (myInVertexId == null)
                throw new ArgumentNullException("myInVertexId must not be null!");


            var myOutVertex = myIGraph.GetVertex(myOutVertexId);

            if (myOutVertex == null)
                throw new ArgumentException("VertexId '" + myOutVertexId + "' is unknown!");


            var myInVertex  = myIGraph.GetVertex(myInVertexId);

            if (myInVertex == null)
                throw new ArgumentException("VertexId '" + myInVertexId + "' is unknown!");


            return myIGraph.AddEdge(myOutVertex, myInVertex, myEdgeId, myLabel, myEdgeInitializer);


        }

        #endregion

        #region AddEdge<TEdge>(this myIGraph, myOutVertex, myInVertex, myEdgeId = null, myLabel = null, myEdgeInitializer = null)

        /// <summary>
        /// Adds an edge to the graph using the given myEdgeId and initializes
        /// its properties by invoking the given edge initializer.
        /// </summary>
        /// <typeparam name="TEdge">The type of the edge to add.</typeparam>
        /// <param name="myIGraph"></param>
        /// <param name="myOutVertex"></param>
        /// <param name="myInVertex"></param>
        /// <param name="myEdgeId">A EdgeId. If none was given a new one will be generated.</param>
        /// <param name="myLabel"></param>
        /// <param name="myEdgeInitializer">A delegate to initialize the newly generated edge.</param>
        /// <returns>The new edge</returns>
        public static TEdge AddEdge<TEdge>(this IGraph myIGraph, IVertex myOutVertex, IVertex myInVertex, EdgeId myEdgeId = null, String myLabel = null, Action<IEdge> myEdgeInitializer = null)
            where TEdge : class, IEdge
        {

            if (myIGraph == null)
                throw new ArgumentNullException("myIGraph must not be null!");

            // Get constructor for TEdge
            var _Type  = typeof(TEdge).
                         GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                                        null,
                                        new Type[] {
                                            typeof(IGraph),
                                            typeof(IVertex),
                                            typeof(IVertex),
                                            typeof(EdgeId),
                                            typeof(Action<IEdge>)
                                        },
                                        null);

            if (_Type == null)
                throw new ArgumentException("A appropriate constructor for type '" + typeof(TEdge).Name + "' could not be found!");


            // Invoke constructor of TEdge
            var _TEdge = _Type.Invoke(new Object[] { myIGraph, myOutVertex, myInVertex, myEdgeId, myEdgeInitializer }) as TEdge;
            if (_TEdge == null)
                throw new ArgumentException("An edge of type '" + typeof(TEdge).Name + "' could not be created!");


            // Add to IGraph
            myIGraph.AddEdge(myOutVertex, myInVertex, myEdgeId, myLabel, myEdgeInitializer);

            return _TEdge;

        }

        #endregion

        #region AddEdge<TEdge>(this myIGraph, myOutVertex, myInVertex, myEdgeId = null, myLabel = null, myEdgeInitializer = null)

        /// <summary>
        /// Adds an edge to the graph using the given myEdgeId and initializes
        /// its properties by invoking the given edge initializer.
        /// </summary>
        /// <param name="myIGraph"></param>
        /// <param name="myOutVertexId"></param>
        /// <param name="myInVertexId"></param>
        /// <param name="myEdgeId">A EdgeId. If none was given a new one will be generated.</param>
        /// <param name="myLabel"></param>
        /// <param name="myEdgeInitializer">A delegate to initialize the newly generated edge.</param>
        /// <returns>The new edge</returns>
        public static TEdge AddEdge<TEdge>(this IGraph myIGraph, VertexId myOutVertexId, VertexId myInVertexId, EdgeId myEdgeId = null, String myLabel = null, Action<IEdge> myEdgeInitializer = null)
            where TEdge : class, IEdge
        {

            if (myIGraph == null)
                throw new ArgumentNullException("myIGraph must not be null!");

            if (myOutVertexId == null)
                throw new ArgumentNullException("myOutVertexId must not be null!");

            if (myInVertexId == null)
                throw new ArgumentNullException("myInVertexId must not be null!");


            var myOutVertex = myIGraph.GetVertex(myOutVertexId);

            if (myOutVertex == null)
                throw new ArgumentException("VertexId '" + myOutVertexId + "' is unknown!");


            var myInVertex = myIGraph.GetVertex(myInVertexId);

            if (myInVertex == null)
                throw new ArgumentException("VertexId '" + myInVertexId + "' is unknown!");


            return myIGraph.AddEdge<TEdge>(myOutVertex, myInVertex, myEdgeId, myLabel, myEdgeInitializer);


        }

        #endregion


        #region GetEdge<TEdge>(this myIGraph, myEdgeId)

        /// <summary>
        /// Return the edge referenced by the provided edge identifier.
        /// If no edge is referenced by that identifier, then return null.
        /// </summary>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <param name="myIGraph">An object implementing IGraph.</param>
        /// <param name="myEdgeId">The identifier of the edge to retrieved from the graph.</param>
        /// <returns>The edge referenced by the provided identifier or null when no such edge exists.</returns>
        public static TEdge GetEdge<TEdge>(this IGraph myIGraph, EdgeId myEdgeId)
            where TEdge : class, IVertex
        {
            return myIGraph.GetEdge(myEdgeId) as TEdge;
        }

        #endregion


    }

}
