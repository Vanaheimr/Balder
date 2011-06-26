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
using System.Linq;
using System.Threading;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// A transaction.
    /// </summary>
    public class Transaction<T> : IDisposable
        where T : IEquatable<T>, IComparable, IComparable<T>
    {

        #region Data

        /// <summary>
        /// The Id of this transaction.
        /// </summary>
        public T Id { get; private set; }

        /// <summary>
        /// The SystemId of the QuadStore initiating this transaction.</param>
        /// </summary>
        public T SystemId { get; private set; }

        /// <summary>
        /// A user-friendly name or identification for this transaction.
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// The parent transaction, if this is a nested transaction.
        /// </summary>
        public Transaction<T> ParentTransaction { get; private set; }

        /// <summary>
        /// The creation time of this transaction.
        /// </summary>
        public DateTime CreationTime { get; private set; }

        /// <summary>
        /// The isolation level of this transaction.
        /// </summary>
        public IsolationLevel IsolationLevel { get; private set; }

        /// <summary>
        /// Wether this transaction should be synched within an distributed QuadStore.
        /// </summary>
        public Boolean Distributed { get; private set; }

        /// <summary>
        /// Wether this transaction is a long-running transaction.
        /// Long-running transactions may e.g. be swapped on disc.
        /// </summary>
        public Boolean LongRunning { get; private set; }

        /// <summary>
        /// A timestamp after this transaction will no longer be valid.
        /// </summary>
        public DateTime InvalidationTime { get; private set; }


        internal readonly List<Transaction<T>> _NestedTransactions;

        #endregion

        #region Events

        ///<summary>
        /// Subscribe to this event to get informed if the transaction was closed unexpected.
        /// </summary>
        //public event TransactionDisposedHandler OnDispose;

        #endregion

        #region Properties

        #region Finished

        /// <summary>
        /// The timestamp when this transaction was finished (committed or rolled-back).
        /// </summary>
        public DateTime FinishingTime { get; private set; }

        #endregion

        #region State

        private TransactionState _State;

        /// <summary>
        /// The current state of this transaction.
        /// </summary>
        public TransactionState State
        {
            get
            {
                switch (_State)
                {

                    case TransactionState.Running:
                        if (HasNestedTransactions)
                            return TransactionState.NestedTransaction;
                        return TransactionState.Running;

                    default:
                        return _State;

                }
            }
        }

        #endregion

        #region IsNestedTransaction

        /// <summary>
        /// Returns true if this transaction is a nested transaction.
        /// </summary>
        public Boolean IsNestedTransaction
        {
            get
            {
                return ParentTransaction != null;
            }
        }

        #endregion

        #region HasNestedTransactions

        /// <summary>
        /// Returns true if this transaction contains nested transactions.
        /// </summary>
        public Boolean HasNestedTransactions
        {
            get
            {

                if (_NestedTransactions == null || !_NestedTransactions.Any())
                    return false;

                return true;

            }
        }

        #endregion

        #endregion


        #region Constructor(s)

        #region Transaction(Id, SystemId, Name = "", Distributed = false, LongRunning = false, IsolationLevel = IsolationLevel.Read, CreationTime = null, InvalidationTime = null)

        /// <summary>
        /// Creates a new transaction having the given parameters.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="SystemId"></param>
        /// <param name="Name">A name or identification for this transaction.</param>
        /// <param name="Distributed">Indicates that this transaction should synched within the entire cluster.</param>
        /// <param name="LongRunning">Indicates that this transaction is a long-running transaction.</param>
        /// <param name="IsolationLevel">The isolation level of this transaction.</param>
        /// <param name="CreationTime"></param>
        /// <param name="InvalidationTime"></param>
        public Transaction(T              Id,
                           T              SystemId,
                           String         Name             = "",
                           Boolean        Distributed      = false,
                           Boolean        LongRunning      = false,
                           IsolationLevel IsolationLevel   = IsolationLevel.Write,
                           DateTime?      CreationTime     = null,
                           DateTime?      InvalidationTime = null)
        {

            this._NestedTransactions  = new List<Transaction<T>>();
            this.Id                   = Id;
            this.SystemId             = SystemId;
            this._State               = TransactionState.Running;

            if (CreationTime.HasValue)
                this.CreationTime     = CreationTime.Value;
            else
                this.CreationTime     = UniqueTimestamp.Now;

            this.IsolationLevel       = IsolationLevel;
            this.Distributed          = Distributed;

            if (InvalidationTime.HasValue)
                this.InvalidationTime = InvalidationTime.Value;

            this.LongRunning          = LongRunning;
            this.Name                 = Name;

        }

        #endregion

        #region (internal) Transaction(Id, ParentTransaction)

        /// <summary>
        /// Creates a new nested transaction.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="SystemId"></param>
        /// <param name="ParentTransaction"></param>
        internal Transaction(T Id, T SystemId, Transaction<T> ParentTransaction)
            : this(Id, SystemId, "", ParentTransaction.Distributed, ParentTransaction.LongRunning, ParentTransaction.IsolationLevel)
        {
            this.ParentTransaction = ParentTransaction;
            Name = ParentTransaction.Name + "#" + ParentTransaction._NestedTransactions.Count + 1;
        }

        #endregion

        #endregion


        #region Commit(Comment = "", Async = false)

        /// <summary>
        /// Mark this transaction as committed.
        /// Will not invoke the OnDispose event to clean up the ressources
        /// </summary>
        /// <param name="Comment">A comment.</param>
        /// <param name="Async">if true commit will be async; default: false</param>
        public Boolean Commit(String Comment = "", Boolean Async = false)
        {

            switch (State)
            {

                // Running => Committed
                case TransactionState.Running:
                    _State = TransactionState.Committing;
                    // Do actual some work!
                    FinishingTime = UniqueTimestamp.Now;
                    _State = TransactionState.Committed;
                    return true;


                // NestedTransactions => Error!
                // At the moment do not allow to auto-commit the nested transaction!
                case TransactionState.NestedTransaction:
                    if (_NestedTransactions.Last().State == TransactionState.Committed)
                        goto case TransactionState.Running;
                    throw new CouldNotCommitNestedTransactionException<T>(this);


                // Committing => OK!
                case TransactionState.Committing:
                    return true;

                // Commited => Error!
                case TransactionState.Committed:
                    throw new TransactionAlreadyCommitedException<T>(this);


                // RollingBack => Error!
                case TransactionState.RollingBack:
                    throw new TransactionAlreadyRolledbackException<T>(this);

                // Rolledback => Error!
                case TransactionState.RolledBack:
                    throw new TransactionAlreadyRolledbackException<T>(this);


                default:
                    throw new TransactionException<T>(this, Message: "Transaction.Commit() is invalid!");

            }

        }

        #endregion

        #region Rollback(Comment = "", Async = false)

        /// <summary>
        /// Mark this transaction as rolledback. Will invoke the event OnDispose to clean up ressources.
        /// </summary>
        /// <param name="Comment">A comment.</param>
        /// <param name="Async">if true rollback will be async; default: false</param>
        public Boolean Rollback(String Comment = "", Boolean Async = false)
        {

            switch (State)
            {

                // Running => RollingBack => Rolledback
                case TransactionState.Running:
                    _State = TransactionState.RollingBack;
                    // Do actual some work!
                    //        //if (OnDispose != null)
                    //        //    OnDispose(this, new TransactionDisposedEventArgs(this, _SessionTokenReference));
                    FinishingTime = UniqueTimestamp.Now;
                    _State = TransactionState.RolledBack;
                    Console.WriteLine("Transaction rolledback on Thread " + Thread.CurrentThread.ManagedThreadId + "!");
                    return true;


                // NestedTransactions => Error!
                // At the moment do not allow to auto-rollback the nested transaction!
                case TransactionState.NestedTransaction:
                    throw new CouldNotRolleBackNestedTransactionException<T>(this);


                // Committing => Error!
                case TransactionState.Committing:
                    throw new TransactionAlreadyCommitedException<T>(this);

                // Commited => Error!
                case TransactionState.Committed:
                    throw new TransactionAlreadyCommitedException<T>(this);


                // RollingBack => OK!
                case TransactionState.RollingBack:
                    return true;

                // RolledBack => Error!
                case TransactionState.RolledBack:
                    throw new TransactionAlreadyRolledbackException<T>(this);


                default:
                    throw new TransactionException<T>(this, Message: "Transaction.Rollback() is invalid!");

            }

        }

        #endregion


        #region BeginNestedTransaction(Distributed = false, LongRunning = false, IsolationLevel = IsolationLevel.Read, Name = "", TimeStamp = null)

        /// <summary>
        /// Creates a nested transaction having the given parameters.
        /// </summary>
        /// <param name="Distributed">Indicates that the nested transaction should synched within the entire cluster.</param>
        /// <param name="LongRunning">Indicates that the nested transaction is a long-running transaction.</param>
        /// <param name="IsolationLevel">The isolation level of the nested transaction.</param>
        /// <param name="Name">A name or identification for the nested transaction.</param>
        public Transaction<T> BeginNestedTransaction(Boolean Distributed = false, Boolean LongRunning = false, IsolationLevel IsolationLevel = IsolationLevel.Read, String Name = "", DateTime? TimeStamp = null)
        {

            switch (State)
            {

                // Running => Rolledback
                case TransactionState.Running:
                    var _NestedTransaction = new Transaction<T>(default(T), SystemId, this);
                    _NestedTransactions.Add(_NestedTransaction);
                    return _NestedTransaction;


                // NestedTransactions => Error!
                // At the moment do not allow to auto-commit the nested transactions!
                case TransactionState.NestedTransaction:
                    throw new TransactionAlreadyRolledbackException<T>(this);


                // Committing => Error!
                case TransactionState.Committing:
                    throw new TransactionCurrentlyCommittingException<T>(this);

                // Commited => Error!
                case TransactionState.Committed:
                    throw new TransactionAlreadyCommitedException<T>(this);


                // RollingBack => Error!
                case TransactionState.RollingBack:
                    throw new TransactionCurrentlyRollingBackException<T>(this);

                // RolledBack => Error!
                case TransactionState.RolledBack:
                    throw new TransactionAlreadyRolledbackException<T>(this);


                default:
                    throw new TransactionException<T>(this, Message: "Transaction.BeginNestedTransaction() is invalid!");

            }

        }

        #endregion

        #region GetNestedTransaction()

        /// <summary>
        /// Return the current nested transaction.
        /// </summary>
        public Transaction<T> GetNestedTransaction()
        {
            return _NestedTransactions.Last();
        }

        #endregion


        #region IDisposable Members

        /// <summary>
        /// Dispose this transaction
        /// </summary>
        public void Dispose()
        {
            if (State != TransactionState.Committed)
                Rollback("Dispose()");
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Return a string representation of this object.
        /// </summary>
        public override String ToString()
        {

            var _Nested      = "";
            var _Distributed = "";
            var _LongRunning = "";

            if (HasNestedTransactions)
                _Nested = " nested";

            if (Distributed)
                _Distributed = " distributed";

            if (LongRunning)
                _LongRunning = " long-running";

            var _ReturnValue = String.Format("{0}{1}{2}{3}{4}, Lifetime: {5} => {6}, UUID {7}", Name, State, _Nested, _Distributed, _LongRunning, CreationTime, FinishingTime, Id);

            return _ReturnValue;

        }

        #endregion

    }

}
