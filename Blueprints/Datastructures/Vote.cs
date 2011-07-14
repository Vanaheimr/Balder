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
using System.Threading;

#endregion

namespace de.ahzf.Blueprints
{

    #region (delegate) VoteEvaluator(Vote)

    /// <summary>
    /// A delegate for evaluating a vote based on a shared integer.
    /// </summary>
    /// <param name="Vote">The vote to evaluate.</param>
    /// <returns>True if the the result of the vote is yes; False otherwise.</returns>
    public delegate Boolean VoteEvaluator(Int32 Vote);

    #endregion

    #region Vote

    /// <summary>
    /// A vote is a simple way to ask multiple event subscribers
    /// if an action, e.g. AddVertex(...) should be processed or
    /// suspended.
    /// </summary>
    public class Vote
    {

        #region Data

        /// <summary>
        /// The current vote.
        /// </summary>
        protected Int32 _Vote;

        /// <summary>
        /// The vote evaluator.
        /// </summary>
        protected VoteEvaluator VoteEvaluator;

        #endregion

        #region Result

        /// <summary>
        /// The result of the voting.
        /// </summary>
        public Boolean Result
        {
            get
            {
                lock (this)
                {
                    return VoteEvaluator(_Vote);
                }
            }
        }

        #endregion

        #region Constructor(s)

        #region Vote(VoteEvaluator = null)

        /// <summary>
        /// A vote is a simple way to ask multiple event subscribers
        /// if an action, e.g. AddVertex(...) should be processed or
        /// suspended.
        /// </summary>
        /// <param name="VoteEvaluator">A delegate for evaluating a vote based on a shared integer.</param>
        public Vote(VoteEvaluator VoteEvaluator = null)
        {

            _Vote = 0;

            if (VoteEvaluator == null)
                this.VoteEvaluator = (vote) => { if (vote == 0) return true; else return false; };

        }

        #endregion

        #region Vote(InitialVote, VoteEvaluator = null)

        /// <summary>
        /// A vote is a simple way to ask multiple event subscribers
        /// if an action, e.g. AddVertex(...) should be processed or
        /// suspended.
        /// </summary>
        /// <param name="InitialVote">An initial vote.</param>
        /// <param name="VoteEvaluator">A delegate for evaluating a vote based on a shared integer.</param>
        public Vote(Int32 InitialVote, VoteEvaluator VoteEvaluator = null)
        {

            _Vote = InitialVote;

            if (VoteEvaluator == null)
                this.VoteEvaluator = (vote) => { if (vote == 0) return true; else return false; };

        }

        #endregion

        #endregion


        #region Yes()

        /// <summary>
        /// Yes!
        /// </summary>
        public void Yes()
        {
            // Just do nothing at all! ;)
        }

        #endregion

        #region No()

        /// <summary>
        /// No!
        /// </summary>
        public void No()
        {
            Interlocked.Increment(ref _Vote);
        }

        #endregion

    }

    #endregion

}
