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

    public interface IGenericHyperEdge
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
    /// <typeparam name="THyperEdgeId">The type of the hyperedge identifier.</typeparam>
    /// <typeparam name="THyperEdgeData">The type of the additional data to be stored within an hyperedge.</typeparam>
    public interface IGenericHyperEdge<TVertexId,    TVertexRevisionId,    TVertexData,
                                       TEdgeId,      TEdgeRevisionId,      TEdgeData,
                                       THyperEdgeId, THyperEdgeRevisionId, THyperEdgeData> : IGenericHyperEdge, IIdentifier<THyperEdgeId>, IRevisionId<THyperEdgeRevisionId>

        where TVertexId            : IEquatable<TVertexId>,            IComparable<TVertexId>,            IComparable
        where TEdgeId              : IEquatable<TEdgeId>,              IComparable<TEdgeId>,              IComparable
        where THyperEdgeId         : IEquatable<THyperEdgeId>,         IComparable<THyperEdgeId>,         IComparable

        where TVertexRevisionId    : IEquatable<TVertexRevisionId>,    IComparable<TVertexRevisionId>,    IComparable
        where TEdgeRevisionId      : IEquatable<TEdgeRevisionId>,      IComparable<TEdgeRevisionId>,      IComparable
        where THyperEdgeRevisionId : IEquatable<THyperEdgeRevisionId>, IComparable<THyperEdgeRevisionId>, IComparable

    {

        /// <summary>
        /// Return the vertex at the tail of the hyperedge.
        /// </summary>
        IGenericVertex<TVertexId,    TVertexRevisionId,    TVertexData,
                       TEdgeId,      TEdgeRevisionId,      TEdgeData,
                       THyperEdgeId, THyperEdgeRevisionId, THyperEdgeData> OutVertex { get; }


        /// <summary>
        /// Return the vertices at the head of the hyperedge.
        /// </summary>
        IEnumerable<IGenericVertex<TVertexId,    TVertexRevisionId,    TVertexData,
                                   TEdgeId,      TEdgeRevisionId,      TEdgeData,
                                   THyperEdgeId, THyperEdgeRevisionId, THyperEdgeData>> InVertices { get; }


        /// <summary>
        /// Return the edge data.
        /// </summary>
        THyperEdgeData Data { get; }

    }

}
