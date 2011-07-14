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

    /// <summary>
    /// A vote is a simple way to ask multiple event subscribers
    /// about their opinion and to evaluate the results.
    /// </summary>
    /// <typeparam name="TResult">The type of the voting result.</typeparam>
    public abstract class AVote<TResult> : IVote<TResult>
    {

        #region Data

        /// <summary>
        /// The current vote.
        /// </summary>
        protected Int32 _Vote;

        /// <summary>
        /// The current number of votes.
        /// </summary>
        protected Int32 _NumberOfVotes;

        /// <summary>
        /// A delegate for evaluating a vote based on the
        /// overall number of votes and a shared integer.
        /// </summary>
        protected VoteEvaluator<TResult> VoteEvaluator;

        #endregion

        #region Properties

        #region NumberOfVotes

        /// <summary>
        /// The current number of votes.
        /// </summary>
        public UInt32 NumberOfVotes
        {
            get
            {
                return (UInt32) _NumberOfVotes;
            }
        }

        #endregion

        #region Result

        /// <summary>
        /// The result of the voting.
        /// </summary>
        public TResult Result
        {
            get
            {
                lock (this)
                {
                    return VoteEvaluator(_NumberOfVotes, _Vote);
                }
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region AVote(VoteEvaluator = null)

        /// <summary>
        /// A vote is a simple way to ask multiple event subscribers
        /// about their opinion and to evaluate the results.
        /// </summary>
        /// <param name="VoteEvaluator">A delegate for evaluating a vote based on the overall number of votes and a shared integer.</param>
        public AVote(VoteEvaluator<TResult> VoteEvaluator)
        {
            this._Vote          = 0;
            this._NumberOfVotes = 0;
            this.VoteEvaluator  = VoteEvaluator;
        }

        #endregion

        #region AVote(InitialVote, VoteEvaluator)

        /// <summary>
        /// A vote is a simple way to ask multiple event subscribers
        /// about their opinion and to evaluate the results.
        /// </summary>
        /// <param name="InitialVote">An initial vote.</param>
        /// <param name="VoteEvaluator">A delegate for evaluating a vote based on the overall number of votes and a shared integer.</param>
        public AVote(Int32 InitialVote, VoteEvaluator<TResult> VoteEvaluator)
        {
            this._Vote          = InitialVote;
            this._NumberOfVotes = 0;
            this.VoteEvaluator  = VoteEvaluator;
        }

        #endregion

        #endregion

    }

}
