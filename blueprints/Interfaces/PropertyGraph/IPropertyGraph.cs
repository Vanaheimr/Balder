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
using System.Collections.Generic;
using de.ahzf.blueprints.Datastructures;

#endregion

namespace de.ahzf.blueprints
{

    #region IPropertyGraph

    /// <summary>
    /// A standardized Property Graph.
    /// </summary>
    public interface IPropertyGraph : IPropertyGraph<VertexId,    RevisionId, String, Object, IDictionary<String, Object>,
                                                     EdgeId,      RevisionId, String, Object, IDictionary<String, Object>,
                                                     HyperEdgeId, RevisionId, String, Object, IDictionary<String, Object>>

    { }

    #endregion

    #region IPropertyGraph<...>

    /// <summary>
    /// A generic property graph.
    /// </summary>
    public interface IPropertyGraph<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>

        where TIdVertex               : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge                 : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdHyperEdge            : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TRevisionIdVertex       : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge         : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdHyperEdge    : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

    {

        #region Vertex methods

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
        IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                        TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                        TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> AddVertex(TIdVertex VertexId = default(TIdVertex), Action<IProperties<TKeyVertex, TValueVertex>> VertexInitializer = null);

        IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                        TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                        TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> AddVertex(IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                        TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                        TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> myVertexId);


        /// <summary>
        /// Return the vertex referenced by the given vertex identifier.
        /// If no vertex is referenced by that identifier, then return null.
        /// </summary>
        /// <param name="myVertexId">The identifier of the vertex.</param>
        /// <returns>The vertex referenced by the provided identifier or null when no such edge exists.</returns>
        IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                        TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                        TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> GetVertex(TIdVertex myVertexId);


        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// </summary>
        IEnumerable<IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                        TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                        TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>> Vertices { get; }


        /// <summary>
        /// Return a collection of vertices referenced by the given array of vertex identifiers.
        /// </summary>
        /// <param name="myVertexIds">An array of vertex identifiers.</param>
        IEnumerable<IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                        TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                        TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>> GetVertices(params TIdVertex[] myVertexIds);


        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// An additional vertex filter may be applied for filtering.
        /// </summary>
        /// <param name="myVertexFilter">A delegate for vertex filtering.</param>
        IEnumerable<IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                        TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                        TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>> GetVertices(Func<IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                        TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                        TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>, Boolean> myVertexFilter = null);


        /// <summary>
        /// Remove the provided vertex from the graph.
        /// Upon removing the vertex, all the edges by which the vertex is connected will be removed as well.
        /// </summary>
        /// <param name="myIVertex">The vertex to be removed from the graph</param>
        void RemoveVertex(IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                        TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                        TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> myIVertex);

        #endregion

        #region Edge methods

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
        IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                      TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                      TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>
        AddEdge(IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                        TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                        TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> myOutVertex, IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                        TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                        TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> myInVertex, TIdEdge EdgeId = default(TIdEdge), String Label = null, Action<IProperties<TKeyEdge, TValueEdge>> EdgeInitializer = null);


        /// <summary>
        /// Return the edge referenced by the given edge identifier.
        /// If no edge is referenced by that identifier, then return null.
        /// </summary>
        /// <param name="myEdgeId">The identifier of the edge.</param>
        /// <returns>The edge referenced by the provided identifier or null when no such edge exists.</returns>
        IPropertyEdge<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                      TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                      TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> GetEdge(TIdEdge myEdgeId);


        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// </summary>
        IEnumerable<IPropertyEdge<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                      TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                      TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>> Edges { get; }


        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// An additional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="myEdgeIds">An array of edge identifiers.</param>
        IEnumerable<IPropertyEdge<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                      TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                      TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>> GetEdges(params TIdEdge[] myEdgeIds);


        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// An additional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="myEdgeFilter">A delegate for edge filtering.</param>
        IEnumerable<IPropertyEdge<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                      TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                      TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>> GetEdges(Func<IPropertyEdge<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                      TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                      TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>, Boolean> myEdgeFilter = null);


        /// <summary>
        /// Remove the provided edge from the graph.
        /// </summary>
        /// <param name="myIEdge">The edge to be removed from the graph</param>
        void RemoveEdge(IPropertyEdge<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                      TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                      TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> myIEdge);

        #endregion
    
    }

    #endregion

    #region IPropertyGraph<..., TDatastructureXXX>

    /// <summary>
    /// Generic property graph helper interface.
    /// </summary>
    public interface IPropertyGraph<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                    TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>

                                    : IPropertyGraph<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                                     TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>

        where TIdVertex               : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge                 : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdHyperEdge            : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TRevisionIdVertex       : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge         : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdHyperEdge    : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

        where TDatastructureVertex    : IDictionary<TKeyVertex,    TValueVertex>
        where TDatastructureEdge      : IDictionary<TKeyEdge,      TValueEdge>
        where TDatastructureHyperEdge : IDictionary<TKeyHyperEdge, TValueHyperEdge>

    { }

    #endregion

}
