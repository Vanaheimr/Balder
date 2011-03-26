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

    public interface IGenericVertex<TId> : IGenericElement<TId>//, IEquatable<IGenericVertex>, IComparable<IGenericVertex>, IComparable
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    { }

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    /// <typeparam name="TVertexData">The type of the additional data to be stored within a vertex.</typeparam>
    public interface IGenericVertex<TId, TVertexData> : IGenericVertex<TId>//, IEquatable<IGenericVertex<TVertexData>>, IComparable<IGenericVertex<TVertexData>>
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    {

        #region OutEdges

        /// <summary>
        /// Add an outgoing edge.
        /// </summary>
        /// <param name="myIEdge">The edge to add.</param>
        void AddOutEdge(IEdge myIEdge);

        /// <summary>
        /// The edges emanating from, or leaving, this vertex.
        /// </summary>
        IEnumerable<IEdge> OutEdges { get; }

        /// <summary>
        /// The edges emanating from, or leaving, this vertex
        /// filtered by their label.
        /// </summary>
        IEnumerable<IEdge> GetOutEdges(String myLabel);

        /// <summary>
        /// Remove an outgoing edge.
        /// </summary>
        /// <param name="myIEdge">The edge to remove.</param>
        void RemoveOutEdge(IEdge myIEdge);

        #endregion

        #region InEdges

        /// <summary>
        /// Add an incoming edge.
        /// </summary>
        /// <param name="myIEdge">The edge to add.</param>
        void AddInEdge(IEdge myIEdge);

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex.
        /// </summary>
        IEnumerable<IEdge> InEdges { get; }

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex
        /// filtered by their label.
        /// </summary>
        IEnumerable<IEdge> GetInEdges(String myLabel);

        /// <summary>
        /// Remove an incoming edge.
        /// </summary>
        /// <param name="myIEdge">The edge to remove.</param>
        void RemoveInEdge(IEdge myIEdge);

        #endregion

    }

}
