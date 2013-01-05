/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Styx <http://www.github.com/Vanaheimr/Styx>
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
using System.Collections;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    #region IEndPipe

    /// <summary>
    /// An interface for the element emitting part of a pipe.
    /// Pipes implementing just this interface do not neccessarily
    /// consume elements, but e.g. might receive them via network.
    /// </summary>
    public interface IEndPipe : IEnumerator, IEnumerable, IDisposable
    {

        /// <summary>
        /// Returns the path traversed to arrive at the current result of the pipe.
        /// </summary> 
        /// <returns>A List of all of the objects traversed for the current iterator position of the pipe.</returns>
        List<Object> Path { get; }

    }

    #endregion

    #region IEndPipe<out E>

    /// <summary>
    /// An interface for the element emitting part of a pipe.
    /// Pipes implementing just this interface do not neccessarily
    /// consume elements, but e.g. might receive them via network.
    /// </summary>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    public interface IEndPipe<out E> : IEndPipe, IEnumerator<E>, IEnumerable<E>
    { }

    #endregion

}
