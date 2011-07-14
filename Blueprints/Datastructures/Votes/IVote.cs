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

    #region IVote

    /// <summary>
    /// A vote is a simple way to ask multiple event subscribers
    /// about their opinion and to evaluate the results.
    /// </summary>
    public interface IVote
    {

        /// <summary>
        /// The current number of votes.
        /// </summary>
        UInt32  NumberOfVotes { get; }

    }

    #endregion

    #region IVote<TResult>

    /// <summary>
    /// A vote is a simple way to ask multiple event subscribers
    /// about their opinion and to evaluate the results.
    /// </summary>
    /// <typeparam name="TResult">The type of the voting result.</typeparam>
    public interface IVote<TResult> : IVote
    {

        /// <summary>
        /// The result of the voting.
        /// </summary>
        TResult Result { get; }

    }

    #endregion

}
