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

    #region PropertyVertex

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    public interface IPropertyVertex : IPropertyVertex<VertexId,    RevisionId, String, Object,
                                                       EdgeId,      RevisionId, String, Object,
                                                       HyperEdgeId, RevisionId, String, Object>
    { }

    #endregion

    #region IPropertyVertex<...>

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TKeyVertex">The type of the vertex property keys.</typeparam>
    /// <typeparam name="TValueVertex">The type of the vertex property values.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
    /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
    /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
    public interface IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>

                                     : IPropertyElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex>

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

        where TIdVertex               : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge                 : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdHyperEdge            : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TRevisionIdVertex       : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge         : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdHyperEdge    : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

    {
    
        #region OutEdges

        /// <summary>
        /// Add an outgoing edge.
        /// </summary>
        /// <param name="myIEdge">The edge to add.</param>
        void AddOutEdge(IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                      TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> myIEdge);

        /// <summary>
        /// The edges emanating from, or leaving, this vertex.
        /// </summary>
        IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                  TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>> OutEdges { get; }

        /// <summary>
        /// The edges emanating from, or leaving, this vertex
        /// filtered by their label.
        /// </summary>
        IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                  TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>> GetOutEdges(String myLabel);

        /// <summary>
        /// Remove an outgoing edge.
        /// </summary>
        /// <param name="myIEdge">The edge to remove.</param>
        void RemoveOutEdge(IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> myIEdge);

        #endregion

        #region InEdges

        /// <summary>
        /// Add an incoming edge.
        /// </summary>
        /// <param name="myIEdge">The edge to add.</param>
        void AddInEdge(IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> myIEdge);

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex.
        /// </summary>
        IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                  TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>> InEdges { get; }

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex
        /// filtered by their label.
        /// </summary>
        IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                  TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>> GetInEdges(String myLabel);

        /// <summary>
        /// Remove an incoming edge.
        /// </summary>
        /// <param name="myIEdge">The edge to remove.</param>
        void RemoveInEdge(IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                        TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                        TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> myIEdge);

        #endregion

    }

    #endregion

    #region IPropertyVertex<..., TDatastructureXXX>

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TKeyVertex">The type of the vertex property keys.</typeparam>
    /// <typeparam name="TValueVertex">The type of the vertex property values.</typeparam>
    /// <typeparam name="TDatastructureVertex"></typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
    /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
    /// <typeparam name="TDatastructureEdge"></typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
    /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
    /// <typeparam name="TDatastructureHyperEdge"></typeparam>
    public interface IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                     TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>

                                     : IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>,
        
                                       IPropertyElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex, TDatastructureVertex>

        where TDatastructureVertex    : IDictionary<TKeyVertex,    TValueVertex>
        where TDatastructureEdge      : IDictionary<TKeyEdge,      TValueEdge>
        where TDatastructureHyperEdge : IDictionary<TKeyHyperEdge, TValueHyperEdge>

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

        where TIdVertex               : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge                 : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdHyperEdge            : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TRevisionIdVertex       : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge         : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdHyperEdge    : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

    { }

    #endregion

}
