/*
 * Copyright (c) 2010-2013 Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Alviss <http://www.github.com/Vanaheimr/Alviss>
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

namespace eu.Vanaheimr.Alviss
{

    /// <summary>
    /// A Quad is a little fragment of a graph.
    /// QuadId: Subject -Predicate-> Object [Context/Graph] or
    /// VertexId: Vertex -Edge-> AnotherVertex [HyperEdge]
    /// </summary>
    /// <typeparam name="TSystemId">The type of the SystemId.</typeparam>
    /// <typeparam name="TQuadId">The type of the QuadId.</typeparam>
    /// <typeparam name="TTransactionId">The type of the transaction id.</typeparam>
    /// <typeparam name="TSPOC">The type of the subject, predicate, object and context.</typeparam>
    public interface IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>
                         : IEquatable <IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>,
                           IComparable<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>,
                           IComparable

        where TSystemId      : IEquatable<TSystemId>,      IComparable<TSystemId>,      IComparable
        where TQuadId        : IEquatable<TQuadId>,        IComparable<TQuadId>,        IComparable
        where TTransactionId : IEquatable<TTransactionId>, IComparable<TTransactionId>, IComparable
        where TSPOC          : IEquatable<TSPOC>,          IComparable<TSPOC>,          IComparable

    {

        #region SystemId, QuadId and TransactionId

        /// <summary>
        /// The Id of the QuadStore which created this quad.
        /// This Id has to be unique within the distributed cluster of QuadStores.
        /// </summary>
        TSystemId      SystemId      { get; }

        /// <summary>
        /// The Id of the quad.
        /// This Id is just local unique. To get a global unique
        /// Id add the SystemId of the QuadStore.
        /// From the perspective of graphs this is an EdgeId.
        /// </summary>
        TQuadId        QuadId        { get; }

        /// <summary>
        /// The Id of the transaction this quad was build in.
        /// This Id is just local unique. To get a global unique
        /// Id add the SystemId of the QuadStore.
        /// </summary>
        TTransactionId TransactionId { get; }

        #endregion

        #region Subject, Predicate, Object and Context

        /// <summary>
        /// The Subject of this quad.
        /// From another point of view this is an VertexId.
        /// </summary>
        TSPOC Subject   { get; }

        /// <summary>
        /// The Predicate of this quad.
        /// From another point of view this is a PropertyId.
        /// </summary>
        TSPOC Predicate { get; }

        /// <summary>
        /// The Object of this quad.
        /// </summary>
        TSPOC Object    { get; }

        /// <summary>
        /// The Context or Graph of this quad.
        /// From another point of view this is a HyperEdgeId.
        /// </summary>
        TSPOC Context   { get; }

        #endregion

        #region ObjectReference

        /// <summary>
        /// A hashset of references to quads having
        /// the Object of this quad as Subject.
        /// </summary>
        HashSet<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>> ObjectReference { get; set; }

        #endregion

        #region ObjectOnDisc

        /// <summary>
        /// The on disc position of this quad.
        /// </summary>
        UInt64 ObjectOnDisc { get; }

        #endregion

        #region Helpers

        /// <summary>
        /// Returns the HashCode of this object.
        /// </summary>
        Int32 GetHashCode();

        /// <summary>
        /// Shows debug information about this quad.
        /// </summary>
        String ToString();

        #endregion

    }

}
