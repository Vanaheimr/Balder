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

namespace de.ahzf.Blueprints.QuadStore
{

    /// <summary>
    /// A Quad is a little fragment of a graph.
    /// QuadId: Subject -Predicate-> Object [Context/Graph] or
    /// VertexId: Vertex -Edge-> AnotherVertex [HyperEdge]
    /// </summary>
    /// <typeparam name="T">The type of the subject, predicate, objects and context of a quad.</typeparam>
    public interface IQuad<T> : IEquatable<IQuad<T>>, IComparable, IComparable<IQuad<T>>
        where T : IEquatable<T>, IComparable, IComparable<T>
    {

        #region SystemId, TransactionId and QuadId

        /// <summary>
        /// The Id of the quad.
        /// From another point of view this is an EdgeId.
        /// This Id is just local unique. To get a global unique
        /// Id add the SystemId of the QuadStore.
        /// </summary>
        T QuadId        { get; }

        /// <summary>
        /// The Id of the QuadStore which created this quad.
        /// </summary>
        T SystemId      { get; }

        /// <summary>
        /// The Id of the transaction this quad was build in.
        /// This Id is just local unique. To get a global unique
        /// Id add the SystemId of the QuadStore.
        /// </summary>
        T TransactionId { get; }

        #endregion

        #region Subject, Predicate, Object and Context/Graph

        /// <summary>
        /// The Subject of this quad.
        /// From another point of view this is an VertexId.
        /// </summary>
        T Subject       { get; }

        /// <summary>
        /// The Predicate of this quad.
        /// From another point of view this is a PropertyId.
        /// </summary>
        T Predicate     { get; }

        /// <summary>
        /// The Object of this quad.
        /// </summary>
        T Object        { get; }

        /// <summary>
        /// The Context or Graph of this quad.
        /// From another point of view this is a HyperEdgeId.
        /// </summary>
        T Context       { get; }

        /// <summary>
        /// The Context or Graph of this quad.
        /// From another point of view this is a HyperEdgeId.
        /// </summary>
        T Graph         { get; }

        #endregion

        #region ObjectOnDisc and ObjectReference

        /// <summary>
        /// The on disc position of this quad.
        /// </summary>
        T ObjectOnDisc  { get; }

        /// <summary>
        /// A hashset of references to quads having
        /// the Object of this quad as Subject.
        /// </summary>
        HashSet<IQuad<T>> ObjectReference { get; set; }

        #endregion

        #region Helpers

        /// <summary>
        /// Returns the HashCode of this object.
        /// </summary>
        Int32 GetHashCode();

        /// <summary>
        /// Shows information on this quad.
        /// </summary>
        String ToString();

        #endregion

    }

}
