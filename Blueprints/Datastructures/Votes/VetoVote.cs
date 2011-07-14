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

    /// <summary>
    /// A veto vote is a simple way to ask multiple event subscribers
    /// if an action, e.g. AddVertex(...) should be processed or suspended.
    /// If anyone is unhappy with it, the result of the vote will be false.
    /// </summary>
    public class VetoVote : AVote<Boolean>
    {

        #region Constructor(s)

        #region Veto()

        /// <summary>
        /// A veto vote is a simple way to ask multiple event subscribers
        /// if an action, e.g. AddVertex(...) should be processed or suspended.
        /// If anyone is unhappy with it, the result of the vote will be false.
        /// </summary>
        public VetoVote()
            : base((number, vote) => { if (vote > 0) return false; else return true; })
        { }

        #endregion

        #endregion

        #region OK()

        /// <summary>
        /// OK
        /// </summary>
        public void OK()
        {
            Interlocked.Increment(ref _NumberOfVotes);
        }

        #endregion

        #region Veto()

        /// <summary>
        /// Veto
        /// </summary>
        public void Veto()
        {
            Interlocked.Increment(ref _NumberOfVotes);
            Interlocked.Increment(ref _Vote);
        }

        #endregion

    }

}
