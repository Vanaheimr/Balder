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

namespace de.ahzf.Blueprints
{

    #region TransactionException<T>

    /// <summary>
    /// An exception during transaction processing occurred!
    /// </summary>
    public class TransactionException<T> : Exception
        where T : IEquatable<T>, IComparable, IComparable<T>
    {

        protected Transaction<T> _Transaction = null;

        /// <summary>
        /// An exception during transaction processing occurred!
        /// </summary>
        /// <param name="Transaction">A transaction.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public TransactionException(Transaction<T> Transaction, String Message = null, Exception InnerException = null)
            : base(Message, InnerException)
        {
            _Transaction = Transaction;
        }

    }

    #endregion


    #region CouldNotBeginTransactionException<T>

    /// <summary>
    /// A transaction could not be started.
    /// </summary>
    public class CouldNotBeginTransactionException<T> : TransactionException<T>
        where T : IEquatable<T>, IComparable, IComparable<T>
    {

        /// <summary>
        /// A transaction could not be started.
        /// </summary>
        /// <param name="Transaction">A transaction.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public CouldNotBeginTransactionException(Transaction<T> Transaction, String Message = null, Exception InnerException = null)
            : base(Transaction, Message, InnerException)
        { }

    }

    #endregion

    #region CouldNotCommitNestedTransactionException<T>

    /// <summary>
    /// A nested transaction could not be committed.
    /// </summary>
    public class CouldNotCommitNestedTransactionException<T> : TransactionException<T>
        where T : IEquatable<T>, IComparable, IComparable<T>
    {

        /// <summary>
        /// A nested transaction could not be committed.
        /// </summary>
        /// <param name="Transaction">A transaction.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public CouldNotCommitNestedTransactionException(Transaction<T> Transaction, String Message = null, Exception InnerException = null)
            : base(Transaction, Message, InnerException)
        { }

    }

    #endregion

    #region CouldNotRolleBackNestedTransactionException<T>

    /// <summary>
    /// A nested transaction could not be rolled back.
    /// </summary>
    public class CouldNotRolleBackNestedTransactionException<T> : TransactionException<T>
        where T : IEquatable<T>, IComparable, IComparable<T>
    {

        /// <summary>
        /// A nested transaction could not be rolled back.
        /// </summary>
        /// <param name="Transaction">A transaction.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public CouldNotRolleBackNestedTransactionException(Transaction<T> Transaction, String Message = null, Exception InnerException = null)
            : base(Transaction, Message, InnerException)
        { }

    }

    #endregion

    #region TransactionAlreadyCommitedException<T>

    /// <summary>
    /// The transaction was already committed.
    /// </summary>
    public class TransactionAlreadyCommitedException<T> : TransactionException<T>
        where T : IEquatable<T>, IComparable, IComparable<T>
    {

        /// <summary>
        /// The transaction was already committed.
        /// </summary>
        /// <param name="Transaction">A transaction.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public TransactionAlreadyCommitedException(Transaction<T> Transaction, String Message = null, Exception InnerException = null)
            : base(Transaction, Message, InnerException)
        { }

    }

    #endregion

    #region TransactionAlreadyRolledbackException<T>

    /// <summary>
    /// The transaction was already rolled back!
    /// </summary>
    public class TransactionAlreadyRolledbackException<T> : TransactionException<T>
        where T : IEquatable<T>, IComparable, IComparable<T>
    {

        /// <summary>
        /// The transaction was already rolled back!
        /// </summary>
        /// <param name="Transaction">A transaction.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public TransactionAlreadyRolledbackException(Transaction<T> Transaction, String Message = null, Exception InnerException = null)
            : base(Transaction, Message, InnerException)
        { }

    }

    #endregion

    #region TransactionAlreadyRunningException<T>

    /// <summary>
    /// The transaction is already running!
    /// </summary>
    public class TransactionAlreadyRunningException<T> : TransactionException<T>
        where T : IEquatable<T>, IComparable, IComparable<T>
    {

        /// <summary>
        /// The transaction is already running!
        /// </summary>
        /// <param name="Transaction">A transaction.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public TransactionAlreadyRunningException(Transaction<T> Transaction, String Message = null, Exception InnerException = null)
            : base(Transaction, Message, InnerException)
        { }

    }

    #endregion

    #region TransactionCurrentlyCommittingException<T>

    /// <summary>
    /// The transaction is currently committing!
    /// </summary>
    public class TransactionCurrentlyCommittingException<T> : TransactionException<T>
        where T : IEquatable<T>, IComparable, IComparable<T>
    {

        /// <summary>
        /// The transaction is currently committing!
        /// </summary>
        /// <param name="Transaction">A transaction.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public TransactionCurrentlyCommittingException(Transaction<T> Transaction, String Message = null, Exception InnerException = null)
            : base(Transaction, Message, InnerException)
        { }

    }

    #endregion

    #region TransactionCurrentlyRollingBackException<T>

    /// <summary>
    /// The transaction is currently rolling back!
    /// </summary>
    public class TransactionCurrentlyRollingBackException<T> : TransactionException<T>
        where T : IEquatable<T>, IComparable, IComparable<T>
    {

        /// <summary>
        /// The transaction is currently rolling back!
        /// </summary>
        /// <param name="Transaction">A transaction.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public TransactionCurrentlyRollingBackException(Transaction<T> Transaction, String Message = null, Exception InnerException = null)
            : base(Transaction, Message, InnerException)
        { }

    }

    #endregion

}
