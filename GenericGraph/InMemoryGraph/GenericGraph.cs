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
using System.Collections.Generic;

#endregion

namespace de.ahzf.Blueprints.GenericGraph.InMemory
{

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TDataVertex">The type of the data stored within a vertex.</typeparam>
    /// <typeparam name="TVertex"></typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TDataEdge">The type of the data stored within an edge.</typeparam>
    /// <typeparam name="TEdge"></typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TDataHyperEdge">The type of the data stored within a hyperedge.</typeparam>
    /// <typeparam name="THyperEdge"></typeparam>
    public class GenericGraph<TIdVertex,    TRevisionIdVertex,                     TDataVertex,    TVertex,
                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,      TEdge,
                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge, THyperEdge>

                              : IGenericGraph<TIdVertex,    TRevisionIdVertex,                     TDataVertex,    TVertex,    
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,      TEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge, THyperEdge>

        where TVertex              : IGenericVertex   <TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>

        where TEdge                : IGenericEdge     <TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>
        
        where THyperEdge           : IGenericHyperEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>

        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable
        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable
        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable

        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable
        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable
        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable

    {

        #region Data

        // Make it more generic??!
        private readonly IDictionary<TIdVertex,    IGenericVertex   <TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>> _Vertices;

        private readonly IDictionary<TIdEdge,      IGenericEdge     <TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>> _Edges;

        private readonly IDictionary<TIdHyperEdge, IGenericHyperEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>> _HyperEdges;

        private readonly VertexCreatorDelegate   <TIdVertex,    TRevisionIdVertex,                     TDataVertex,    TVertex,
                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,      TEdge,
                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge, THyperEdge> VertexCreatorDelegate;

        private readonly EdgeCreatorDelegate     <TIdVertex,    TRevisionIdVertex,                     TDataVertex,    TVertex,
                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,      TEdge,
                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge, THyperEdge> EdgeCreatorDelegate;

        private readonly HyperEdgeCreatorDelegate<TIdVertex,    TRevisionIdVertex,                     TDataVertex,    TVertex,
                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,      TEdge,
                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge, THyperEdge> HyperEdgeCreatorDelegate;


        #endregion


        /// <summary>
        /// Create a new GenericGraph
        /// </summary>
        public GenericGraph(VertexCreatorDelegate   <TIdVertex,    TRevisionIdVertex,                     TDataVertex,    TVertex,
                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,      TEdge,
                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge, THyperEdge> VertexCreatorDelegate,
                            EdgeCreatorDelegate     <TIdVertex,    TRevisionIdVertex,                     TDataVertex,    TVertex,
                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,      TEdge,
                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge, THyperEdge> EdgeCreatorDelegate,
                            HyperEdgeCreatorDelegate<TIdVertex,    TRevisionIdVertex,                     TDataVertex,    TVertex,
                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,      TEdge,
                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge, THyperEdge> HyperEdgeCreatorDelegate)
        {
            
            _Vertices   = new Dictionary<TIdVertex,    IGenericVertex   <TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>>();
            
            _Edges      = new Dictionary<TIdEdge,      IGenericEdge     <TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>>();

            _HyperEdges = new Dictionary<TIdHyperEdge, IGenericHyperEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>>();

            this.VertexCreatorDelegate    = VertexCreatorDelegate;
            this.EdgeCreatorDelegate      = EdgeCreatorDelegate;
            this.HyperEdgeCreatorDelegate = HyperEdgeCreatorDelegate;

        }

        /// <summary>
        /// Create a new vertex, add it to the graph, and return the newly created vertex.
        /// The provided object identifier is a recommendation for the identifier to use.
        /// It is not required that the implementation use this identifier.
        /// If the object identifier is already being used by the graph to reference a vertex,
        /// then that reference vertex is returned and no vertex is created.
        /// If the identifier is a vertex (perhaps from another graph),
        /// then the vertex is duplicated for this graph. Thus, a vertex can not be an identifier.
        /// </summary>
        /// <param name="VertexId">The recommended object identifier.</param>
        /// <param name="VertexInitializer">A delegate to initialize the new vertex.</param>
        /// <returns>The newly created vertex or the vertex already referenced by the provided identifier.</returns>
        public TVertex AddVertex(TIdVertex VertexId = default(TIdVertex), Action<TDataVertex> VertexInitializer = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myVertexId"></param>
        /// <returns></returns>
        public TVertex AddVertex(TVertex myVertexId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the vertex referenced by the given vertex identifier.
        /// If no vertex is referenced by that identifier, then return null.
        /// </summary>
        /// <param name="myVertexId">The identifier of the vertex.</param>
        /// <returns>The vertex referenced by the provided identifier or null when no such edge exists.</returns>
        public TVertex GetVertex(TIdVertex myVertexId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// </summary>
        public IEnumerable<TVertex> Vertices
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Return a collection of vertices referenced by the given array of vertex identifiers.
        /// </summary>
        /// <param name="myVertexIds">An array of vertex identifiers.</param>
        public IEnumerable<TVertex> GetVertices(params TIdVertex[] myVertexIds)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// An additional vertex filter may be applied for filtering.
        /// </summary>
        /// <param name="myVertexFilter">A delegate for vertex filtering.</param>
        public IEnumerable<TVertex> GetVertices(Func<TVertex, bool> myVertexFilter = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove the provided vertex from the graph.
        /// Upon removing the vertex, all the edges by which the vertex is connected will be removed as well.
        /// </summary>
        /// <param name="myIVertex">The vertex to be removed from the graph</param>
        public void RemoveVertex(TVertex myIVertex)
        {
            throw new NotImplementedException();
        }





        /// <summary>
        /// Add an edge to the graph. The added edges requires a recommended identifier, a tail vertex, an head vertex, and a label.
        /// Like adding a vertex, the provided object identifier is can be ignored by the implementation.
        /// </summary>
        /// <param name="myOutVertex">The vertex on the tail of the edge.</param>
        /// <param name="myInVertex">The vertex on the head of the edge.</param>
        /// <param name="EdgeId">The recommended object identifier.</param>
        /// <param name="Label">The label associated with the edge.</param>
        /// <param name="EdgeInitializer">A delegate to initialize the new edge.</param>
        /// <returns>The newly created edge</returns>
        public TEdge AddEdge(TVertex myOutVertex, TVertex myInVertex, TIdEdge EdgeId = default(TIdEdge), string Label = null, Action<TDataEdge> EdgeInitializer = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the edge referenced by the given edge identifier.
        /// If no edge is referenced by that identifier, then return null.
        /// </summary>
        /// <param name="myEdgeId">The identifier of the edge.</param>
        /// <returns>The edge referenced by the provided identifier or null when no such edge exists.</returns>
        public TEdge GetEdge(TIdEdge myEdgeId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// </summary>
        public IEnumerable<TEdge> Edges
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// An additional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="myEdgeIds">An array of edge identifiers.</param>
        public IEnumerable<TEdge> GetEdges(params TIdEdge[] myEdgeIds)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// An additional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="myEdgeFilter">A delegate for edge filtering.</param>
        public IEnumerable<TEdge> GetEdges(Func<TEdge, bool> myEdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove the provided edge from the graph.
        /// </summary>
        /// <param name="myIEdge">The edge to be removed from the graph</param>
        public void RemoveEdge(TEdge myIEdge)
        {
            throw new NotImplementedException();
        }




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
        public void Shutdown(String Reason = null)
        {
            Clear();
        }

        #endregion



        /// <summary>
        /// 
        /// </summary>
        public TDataVertex Data
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public TIdVertex Id
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(TIdVertex other)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(TIdVertex other)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public TRevisionIdVertex RevisionId
        {
            get { throw new NotImplementedException(); }
        }

    }

}
