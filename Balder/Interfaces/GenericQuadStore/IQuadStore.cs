/*
 * Copyright (c) 2010-2014 Achim 'ahzf' Friedland <achim@graphdefined.org>
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

using org.GraphDefined.Vanaheimr.Illias.Transactions;

#endregion

namespace org.GraphDefined.Vanaheimr.Alviss
{

    /// <summary>
    /// A QuadStore stores little fragments of a graph called quads.
    /// Subject -Predicate-> Object [Context]
    /// Vertex  -Edge->      Vertex [HyperEdge]
    /// </summary>
    /// <typeparam name="TSystemId">The type of the SystemId.</typeparam>
    /// <typeparam name="TQuadId">The type of the QuadId.</typeparam>
    /// <typeparam name="TTransactionId">The type of the transaction id.</typeparam>
    /// <typeparam name="TSPOC">The type of the subject, predicate, object and context.</typeparam>
    public interface IQuadStore<TSystemId, TQuadId, TTransactionId, TSPOC>

        where TSystemId      : IEquatable<TSystemId>,      IComparable<TSystemId>,      IComparable
        where TQuadId        : IEquatable<TQuadId>,        IComparable<TQuadId>,        IComparable
        where TTransactionId : IEquatable<TTransactionId>, IComparable<TTransactionId>, IComparable
        where TSPOC          : IEquatable<TSPOC>,          IComparable<TSPOC>,          IComparable

    {

        /// <summary>
        /// The identification of this QuadStore.
        /// </summary>
        TSystemId SystemId { get; }

        #region BeginTransaction(...)

        /// <summary>
        /// Start a new transaction.
        /// </summary>
        /// <param name="Name">A name or identification for this transaction.</param>
        /// <param name="Distributed">Indicates that this transaction should synched within the entire cluster.</param>
        /// <param name="LongRunning">Indicates that this transaction is a long-running transaction.</param>
        /// <param name="IsolationLevel">The isolation level of this transaction.</param>
        /// <param name="CreationTime">The timestamp when this transaction started.</param>
        /// <param name="InvalidationTime">The timestamp when this transaction will be invalid.</param>
        /// <returns>A new transaction object.</returns>
        Transaction<TTransactionId, TSystemId, IQuadStore<TSystemId, TQuadId, TTransactionId, TSPOC>>
            BeginTransaction(String         Name             = "",
                             Boolean        Distributed      = false,
                             Boolean        LongRunning      = false,
                             IsolationLevel IsolationLevel   = IsolationLevel.Write,
                             DateTime?      CreationTime     = null,
                             DateTime?      InvalidationTime = null);
        
        #endregion

        #region Add(...)

        /// <summary>
        /// Adds a new quad based on the given parameters to the QuadStore.
        /// </summary>
        /// <param name="Subject">The subject.</param>
        /// <param name="Predicate">The predicate.</param>
        /// <param name="Object">The object.</param>
        /// <param name="Context">The context.</param>
        /// <param name="Connect">Connect this quad to other quads in order to achieve an index-free adjacency.</param>
        /// <returns>A new quad based on the given parameters.</returns>
        IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>
            Add(TSPOC   Subject,
                TSPOC   Predicate,
                TSPOC   Object,
                TSPOC   Context = default(TSPOC),
                Boolean Connect = true);

        #endregion

        #region NumberOf(...)

        /// <summary>
        /// Return the number of quads.
        /// </summary>
        UInt64 NumberOfQuads      { get; }

        /// <summary>
        /// Return the number of unique subjects.
        /// </summary>
        UInt64 NumberOfSubjects   { get; }

        /// <summary>
        /// Return the number of unique predicates.
        /// </summary>
        UInt64 NumberOfPredicates { get; }

        /// <summary>
        /// Return the number of unique objects.
        /// </summary>
        UInt64 NumberOfObjects    { get; }

        /// <summary>
        /// Return the number of unique contexts.
        /// </summary>
        UInt64 NumberOfContexts   { get; }

        #endregion

        #region Get/Select(...)/Traverse

        /// <summary>
        /// Returns the quad having the given QuadId.
        /// </summary>
        /// <param name="QuadId">The QuadId.</param>
        /// <returns>The quad having the given QuadId.</returns>
        IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>
            GetQuad(TQuadId QuadId);


        /// <summary>
        /// Returns all matching quads based on the given parameters.
        /// </summary>
        /// <param name="Subject">The subject.</param>
        /// <param name="Predicate">The predicate.</param>
        /// <param name="Object">The object.</param>
        /// <param name="Context">The context.</param>
        /// <returns>An enumeration of quads matched by the given parameters.</returns>
        IEnumerable<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>
            GetQuads(TSPOC Subject   = default(TSPOC),
                     TSPOC Predicate = default(TSPOC),
                     TSPOC Object    = default(TSPOC),
                     TSPOC Context   = default(TSPOC));


        /// <summary>
        /// Returns all matching quads based on the given selectors.
        /// </summary>
        /// <param name="SubjectSelector">A delegate for selecting subjects.</param>
        /// <param name="PredicateSelector">A delegate for selecting predicates.</param>
        /// <param name="ObjectSelector">A delegate for selecting objects.</param>
        /// <param name="ContextSelector">A delegate for selecting contexts.</param>
        /// <returns>An enumeration of quads matched by the given selectors.</returns>
        IEnumerable<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>
            SelectQuads(SubjectSelector<TSPOC>   SubjectSelector   = null,
                        PredicateSelector<TSPOC> PredicateSelector = null,
                        ObjectSelector<TSPOC>    ObjectSelector    = null,
                        ContextSelector<TSPOC>   ContextSelector   = null);


        /// <summary>
        /// Traverses the graph of quads by following a single given predicate.
        /// </summary>
        /// <param name="Subject">The subject.</param>
        /// <param name="Predicate">The predicate to follow.</param>
        /// <param name="IncludeFirst">Include the subject as first element of the result set.</param>
        /// <returns></returns>
        IEnumerable<TSPOC> Traverse(TSPOC   Subject, 
                                    TSPOC   Predicate,
                                    Boolean IncludeFirst = true);

        #endregion

        #region Remove(...)

        /// <summary>
        /// Removes the quad having the given QuadId.
        /// </summary>
        /// <param name="QuadId">The QuadId.</param>
        /// <returns>The quad after removal having the given QuadId.</returns>
        IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>
            Remove(TQuadId QuadId);

        /// <summary>
        /// Removes all matching quads based on the given parameters.
        /// </summary>
        /// <param name="Subject">The subject.</param>
        /// <param name="Predicate">The predicate.</param>
        /// <param name="Object">The object.</param>
        /// <param name="Context">The context.</param>
        /// <returns>All quads after removal matched by the given parameters.</returns>
        IEnumerable<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>
            Remove(TSPOC Subject   = default(TSPOC),
                   TSPOC Predicate = default(TSPOC),
                   TSPOC Object    = default(TSPOC),
                   TSPOC Context   = default(TSPOC));

        /// <summary>
        /// Removes all matching quads based on the given selectors.
        /// </summary>
        /// <param name="SubjectSelector">A delegate for selecting subjects.</param>
        /// <param name="PredicateSelector">A delegate for selecting predicates.</param>
        /// <param name="ObjectSelector">A delegate for selecting objects.</param>
        /// <param name="ContextSelector">A delegate for selecting contexts.</param>
        /// <returns>All quads after removal matched by the given parameters.</returns>
        IEnumerable<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>
            Remove(SubjectSelector<TSPOC>   SubjectSelector   = null,
                   PredicateSelector<TSPOC> PredicateSelector = null,
                   ObjectSelector<TSPOC>    ObjectSelector    = null,
                   ContextSelector<TSPOC>   ContextSelector   = null);

        #endregion

        #region Utils

        /// <summary>
        /// Update all references of the given Quad to provide an index-free adjacency.
        /// </summary>
        /// <param name="Quad">A quad.</param>
        void UpdateReferences(IQuad<TSystemId, TQuadId, TTransactionId, TSPOC> Quad);

        #endregion

    }

}
