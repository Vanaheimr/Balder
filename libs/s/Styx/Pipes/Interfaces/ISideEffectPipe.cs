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

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    #region ISideEffectPipe

    /// <summary>
    /// This SideEffectPipe will produce a side effect which can be
    /// retrieved by the SideEffect property.
    /// </summary>
    public interface ISideEffectPipe : IPipe, IDisposable
    { }

    #endregion

    #region ISideEffectPipe<in S, out E, out T>

    /// <summary>
    /// This SideEffectPipe will produce a side effect which can be
    /// retrieved by the SideEffect property.
    /// </summary>
    /// <typeparam name="S">The type of the consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    /// <typeparam name="T">The type of the sideeffect.</typeparam>
    public interface ISideEffectPipe<in S, out E, out T> : ISideEffectPipe, IPipe<S, E>
    {

        /// <summary>
        /// The SideEffect produced by this Pipe.
        /// </summary>
        T SideEffect { get; }

    }

    #endregion

    #region ISideEffectPipe<in S, out E, out T1, out T2>

    /// <summary>
    /// This SideEffectPipe will produce two side effects which can
    /// be retrieved by the SideEffect properties.
    /// </summary>
    /// <typeparam name="S">The type of the consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    /// <typeparam name="T1">The type of the first sideeffect.</typeparam>
    /// <typeparam name="T2">The type of the second sideeffect.</typeparam>
    public interface ISideEffectPipe<in S, out E, out T1, out T2> : ISideEffectPipe, IPipe<S, E>
    {

        /// <summary>
        /// The first SideEffect produced by this Pipe.
        /// </summary>
        T1 SideEffect1 { get; }

        /// <summary>
        /// The second SideEffect produced by this Pipe.
        /// </summary>
        T2 SideEffect2 { get; }

    }

    #endregion

    #region ISideEffectPipe<in S, out E, out T1, out T2, out T3>

    /// <summary>
    /// This SideEffectPipe will produce two side effects which can
    /// be retrieved by the SideEffect properties.
    /// </summary>
    /// <typeparam name="S">The type of the consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    /// <typeparam name="T1">The type of the first sideeffect.</typeparam>
    /// <typeparam name="T2">The type of the second sideeffect.</typeparam>
    /// <typeparam name="T3">The type of the third sideeffect.</typeparam>
    public interface ISideEffectPipe<in S, out E, out T1, out T2, out T3> : ISideEffectPipe, IPipe<S, E>
    {

        /// <summary>
        /// The first SideEffect produced by this Pipe.
        /// </summary>
        T1 SideEffect1 { get; }

        /// <summary>
        /// The second SideEffect produced by this Pipe.
        /// </summary>
        T2 SideEffect2 { get; }

        /// <summary>
        /// The third SideEffect produced by this Pipe.
        /// </summary>
        T3 SideEffect3 { get; }

    }

    #endregion

}
