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

#endregion

namespace org.GraphDefined.Vanaheimr.Alviss
{

    #region QuadStoreException

    /// <summary>
    /// The base class for all QuadStoreExceptions.
    /// </summary>
    public class QuadStoreException : Exception
    {

        /// <summary>
        /// A general QuadStore exception occurred!
        /// </summary>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public QuadStoreException(String Message = null, Exception InnerException = null)
            : base(Message, InnerException)
        { }

    }

    #endregion

    #region QuadStoreException<TSystemId, TQuadId, TTransactionId, TSPOC>

    /// <summary>
    /// The base class for all QuadStoreExceptions.
    /// </summary>
    /// <typeparam name="TSystemId">The type of the SystemId.</typeparam>
    /// <typeparam name="TQuadId">The type of the QuadId.</typeparam>
    /// <typeparam name="TTransactionId">The type of the transaction id.</typeparam>
    /// <typeparam name="TSPOC">The type of the subject, predicate, object and context.</typeparam>
    public class QuadStoreException<TSystemId, TQuadId, TTransactionId, TSPOC> : QuadStoreException

        where TSystemId      : IEquatable<TSystemId>,      IComparable<TSystemId>,      IComparable
        where TQuadId        : IEquatable<TQuadId>,        IComparable<TQuadId>,        IComparable
        where TTransactionId : IEquatable<TTransactionId>, IComparable<TTransactionId>, IComparable
        where TSPOC          : IEquatable<TSPOC>,          IComparable<TSPOC>,          IComparable

    {

        /// <summary>
        /// A general QuadStore exception occurred!
        /// </summary>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public QuadStoreException(String Message = null, Exception InnerException = null)
            : base(Message, InnerException)
        { }

    }

    #endregion

}
