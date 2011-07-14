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
    /// A delegate for evaluating a vote based on the
    /// overall number of votes and a shared integer.
    /// </summary>
    /// <typeparam name="TResult">The type of the voting result.</typeparam>
    /// <param name="NumberOfVotes">The current number of votes.</param>
    /// <param name="Vote">The vote to evaluate.</param>
    /// <returns>True if the the result of the vote is yes; False otherwise.</returns>
    public delegate TResult VoteEvaluator<TResult>(Int32 NumberOfVotes, Int32 Vote);

}
