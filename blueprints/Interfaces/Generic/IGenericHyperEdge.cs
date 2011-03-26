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
using System.Collections.Generic;

using de.ahzf.blueprints.Datastructures;

#endregion

namespace de.ahzf.blueprints
{

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
    /// <typeparam name="TId">The type of the id.</typeparam>
    public interface IGenericHyperEdge<TId> : IGenericElement<TId>
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    {

        /// <summary>
        /// Return the label associated with the hyperedge.
        /// </summary>
        String Label { get; }

    }


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
    /// <typeparam name="TId">The type of the id.</typeparam>
    /// <typeparam name="TEdgeData">The type of the additional data to be stored within an edge.</typeparam>
    public interface IGenericHyperEdge<TId, TEdgeData> : IGenericHyperEdge<TId>
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    {

        /// <summary>
        /// Return the vertex at the tail of the hyperedge.
        /// </summary>
        IVertex OutVertex { get; }


        /// <summary>
        /// Return the vertices at the head of the hyperedge.
        /// </summary>
        IEnumerable<IVertex> InVertices { get; }

    }

}
