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
using System.Text;

#endregion

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// The state of a transaction.
    /// </summary>
    public enum TransactionState
    {

        /// <summary>
        /// The transaction is still running.
        /// </summary>
        Running,

        /// <summary>
        /// The transaction has nested transactions.
        /// </summary>
        NestedTransaction,

        /// <summary>
        /// The transaction is currently comitting.
        /// </summary>
        Committing,

        /// <summary>
        /// The transaction was comitted.
        /// </summary>
        Committed,

        /// <summary>
        /// The transaction is currently rolling back.
        /// </summary>
        RollingBack,

        /// <summary>
        /// The transaction was rolled back.
        /// </summary>
        RolledBack

    }

}
