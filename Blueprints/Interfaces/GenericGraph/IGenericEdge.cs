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

#endregion

namespace de.ahzf.Blueprints.GenericGraph
{

    #region IGenericEdge

    /// <summary>
    /// An edge links two vertices. Along with its key/value properties,
    /// an edge has both a directionality and a label.
    /// The directionality determines which vertex is the tail vertex
    /// (out vertex) and which vertex is the head vertex (in vertex).
    /// The edge label determines the type of relationship that exists
    /// between the two vertices.
    /// Diagrammatically, outVertex ---label---> inVertex.
    /// </summary>
    public interface IGenericEdge : IComparable
    {

        /// <summary>
        /// Return the label associated with the edge.
        /// </summary>
        String Label { get; }

    }

    #endregion

    #region IGenericEdge<...>

    /// <summary>
    /// An edge links two vertices. Along with its key/value properties,
    /// an edge has both a directionality and a label.
    /// The directionality determines which vertex is the tail vertex
    /// (out vertex) and which vertex is the head vertex (in vertex).
    /// The edge label determines the type of relationship that exists
    /// between the two vertices.
    /// Diagrammatically, outVertex ---label---> inVertex.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TDataVertex">The type of the embedded vertex data.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TDataEdge">The type of the embedded edge data.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TDataHyperEdge">The type of the embedded hyperedge data.</typeparam>
    public interface IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>

                                  : IGenericElement<TIdEdge, TRevisionIdEdge, TDataEdge>,
                                    IGenericEdge

        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable
        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable
        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable

        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable
        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable
        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable

    {

        /// <summary>
        /// Return the vertex at the tail of the edge.
        /// </summary>
        IGenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> OutVertex { get; }


        /// <summary>
        /// Return the vertex at the head of the edge.
        /// </summary>
        IGenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> InVertex { get; }

    }

    #endregion

}
