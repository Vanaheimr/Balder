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

namespace de.ahzf.Blueprints.GenericGraph
{

    #region IGenericHyperEdge

    /// <summary>
    /// A hyperedge links multiple vertices. Along with its key/value properties,
    /// a hyperedge has both a directionality and a label.
    /// The directionality determines which vertex is the tail vertex
    /// (out vertex) and which vertices are the head vertices (in vertices).
    /// The hyperedge label determines the type of relationship that exists
    /// between these vertices.
    /// Diagrammatically, outVertex ---label---> inVertex1.
    ///                                      \--> inVertex2.
    /// </summary>
    public interface IGenericHyperEdge
    {

        /// <summary>
        /// Return the label associated with the hyperedge.
        /// </summary>
        String Label { get; }

    }

    #endregion

    #region IGenericHyperEdge<...>

    /// <summary>
    /// A hyperedge links multiple vertices. Along with its key/value properties,
    /// a hyperedge has both a directionality and a label.
    /// The directionality determines which vertex is the tail vertex
    /// (out vertex) and which vertices are the head vertices (in vertices).
    /// The hyperedge label determines the type of relationship that exists
    /// between these vertices.
    /// Diagrammatically, outVertex ---label---> inVertex1.
    ///                                      \--> inVertex2.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TVertexData">The type of the embedded vertex data.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TEdgeData">The type of the embedded edge data.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="THyperEdgeData">The type of the embedded hyperedge data.</typeparam>
    public interface IGenericHyperEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>

                                       : IGenericElement<TIdHyperEdge, TRevisionIdHyperEdge, TDataHyperEdge>,
                                         IGenericHyperEdge

        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable
        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable
        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable

        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable
        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable
        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable

    {

        /// <summary>
        /// Return the vertex at the tail of the hyperedge.
        /// </summary>
        IGenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> OutVertex { get; }


        /// <summary>
        /// Return the vertices at the head of the hyperedge.
        /// </summary>
        IEnumerable<IGenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>> InVertices { get; }

    }

    #endregion

}
