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

#endregion

namespace eu.Vanaheimr.Alviss
{

    /// <summary>
    /// Extension methods for the IQuadStore interface
    /// </summary>
    public static class IQuadStoreExtensions
    {

        #region AllOf(this QuadStore, Subject)

        /// <summary>
        /// Returns all quads having the given subject.
        /// </summary>
        /// <typeparam name="TSystemId">The type of the SystemId.</typeparam>
        /// <typeparam name="TQuadId">The type of the QuadId.</typeparam>
        /// <typeparam name="TTransactionId">The type of the transaction id.</typeparam>
        /// <typeparam name="TSPOC">The type of the subject, predicate, object and context.</typeparam>
        /// <param name="QuadStore">An object implementing the IQuadStore interface.</param>
        /// <param name="Subject">A subject.</param>
        /// <returns>A enumeration of quads.</returns>
        public static IEnumerable<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>
                   AllOf<TSystemId, TQuadId, TTransactionId, TSPOC>(this IQuadStore<TSystemId, TQuadId, TTransactionId, TSPOC> QuadStore, TSPOC Subject)

            where TSystemId      : IEquatable<TSystemId>,      IComparable<TSystemId>,      IComparable
            where TQuadId        : IEquatable<TQuadId>,        IComparable<TQuadId>,        IComparable
            where TTransactionId : IEquatable<TTransactionId>, IComparable<TTransactionId>, IComparable
            where TSPOC          : IEquatable<TSPOC>,          IComparable<TSPOC>,          IComparable

        {
            return QuadStore.GetQuads(Subject: Subject);
        }

        #endregion

        #region AllBy(this QuadStore, Predicate)

        /// <summary>
        /// Returns all quads having the given predicate.
        /// </summary>
        /// <typeparam name="TSystemId">The type of the SystemId.</typeparam>
        /// <typeparam name="TQuadId">The type of the QuadId.</typeparam>
        /// <typeparam name="TTransactionId">The type of the transaction id.</typeparam>
        /// <typeparam name="TSPOC">The type of the subject, predicate, object and context.</typeparam>
        /// <param name="QuadStore">An object implementing the IQuadStore interface.</param>
        /// <param name="Predicate">A predicate.</param>
        /// <returns>A enumeration of quads.</returns>
        public static IEnumerable<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>
                   AllBy<TSystemId, TQuadId, TTransactionId, TSPOC>(this IQuadStore<TSystemId, TQuadId, TTransactionId, TSPOC> QuadStore, TSPOC Predicate)

            where TSystemId      : IEquatable<TSystemId>,      IComparable<TSystemId>,      IComparable
            where TQuadId        : IEquatable<TQuadId>,        IComparable<TQuadId>,        IComparable
            where TTransactionId : IEquatable<TTransactionId>, IComparable<TTransactionId>, IComparable
            where TSPOC          : IEquatable<TSPOC>,          IComparable<TSPOC>,          IComparable

        {
            return QuadStore.GetQuads(Predicate: Predicate);
        }

        #endregion

        #region AllWith(this QuadStore, Object)

        /// <summary>
        /// Returns all quads having the given object.
        /// </summary>
        /// <typeparam name="TSystemId">The type of the SystemId.</typeparam>
        /// <typeparam name="TQuadId">The type of the QuadId.</typeparam>
        /// <typeparam name="TTransactionId">The type of the transaction id.</typeparam>
        /// <typeparam name="TSPOC">The type of the subject, predicate, object and context.</typeparam>
        /// <param name="QuadStore">An object implementing the IQuadStore interface.</param>
        /// <param name="Object">An object.</param>
        /// <returns>A enumeration of quads.</returns>
        public static IEnumerable<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>
                   AllWith<TSystemId, TQuadId, TTransactionId, TSPOC>(this IQuadStore<TSystemId, TQuadId, TTransactionId, TSPOC> QuadStore, TSPOC Object)

            where TSystemId      : IEquatable<TSystemId>,      IComparable<TSystemId>,      IComparable
            where TQuadId        : IEquatable<TQuadId>,        IComparable<TQuadId>,        IComparable
            where TTransactionId : IEquatable<TTransactionId>, IComparable<TTransactionId>, IComparable
            where TSPOC          : IEquatable<TSPOC>,          IComparable<TSPOC>,          IComparable

        {
            return QuadStore.GetQuads(Object: Object);
        }

        #endregion

        #region AllFrom(this QuadStore, Context)

        /// <summary>
        /// Returns all quads having the given context.
        /// </summary>
        /// <typeparam name="TSystemId">The type of the SystemId.</typeparam>
        /// <typeparam name="TQuadId">The type of the QuadId.</typeparam>
        /// <typeparam name="TTransactionId">The type of the transaction id.</typeparam>
        /// <typeparam name="TSPOC">The type of the subject, predicate, object and context.</typeparam>
        /// <param name="QuadStore">An object implementing the IQuadStore interface.</param>
        /// <param name="Context">A context.</param>
        /// <returns>A enumeration of quads.</returns>
        public static IEnumerable<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>
                   AllFrom<TSystemId, TQuadId, TTransactionId, TSPOC>(this IQuadStore<TSystemId, TQuadId, TTransactionId, TSPOC> QuadStore, TSPOC Context)

            where TSystemId      : IEquatable<TSystemId>,      IComparable<TSystemId>,      IComparable
            where TQuadId        : IEquatable<TQuadId>,        IComparable<TQuadId>,        IComparable
            where TTransactionId : IEquatable<TTransactionId>, IComparable<TTransactionId>, IComparable
            where TSPOC          : IEquatable<TSPOC>,          IComparable<TSPOC>,          IComparable

        {
            return QuadStore.GetQuads(Context: Context);
        }

        #endregion

    }

}
