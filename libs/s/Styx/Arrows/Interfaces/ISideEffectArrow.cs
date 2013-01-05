/*
 * Copyright (c) 2011-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
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

namespace de.ahzf.Vanaheimr.Styx
{

    #region ISideEffectArrow<in TIn, TOut, out T>

    /// <summary>
    /// This SideEffectArrow will produce a side effect which can
    /// be retrieved by the SideEffect property.
    /// </summary>
    public interface ISideEffectArrow<in TIn, TOut, out T> : IArrow<TIn, TOut>
    {

        /// <summary>
        /// The SideEffect produced by this Arrow.
        /// </summary>
        T SideEffect { get; }

    }

    #endregion

    #region ISideEffectArrow<in TIn, TOut, out T1, out T2>

    /// <summary>
    /// This SideEffectArrow will produce two side effects which can
    /// be retrieved by the SideEffect properties.
    /// </summary>
    public interface ISideEffectArrow<in TIn, TOut, out T1, out T2> : IArrow<TIn, TOut>
    {

        /// <summary>
        /// The first SideEffect produced by this Arrow.
        /// </summary>
        T1 SideEffect1 { get; }

        /// <summary>
        /// The second SideEffect produced by this Arrow.
        /// </summary>
        T2 SideEffect2 { get; }

    }

    #endregion

    #region ISideEffectArrow<in TIn, TOut, out T1, out T2, out T3>

    /// <summary>
    /// This SideEffectArrow will produce three side effects which can
    /// be retrieved by the SideEffect properties.
    /// </summary>
    public interface ISideEffectArrow<in TIn, TOut, out T1, out T2, out T3> : IArrow<TIn, TOut>
    {

        /// <summary>
        /// The first SideEffect produced by this Arrow.
        /// </summary>
        T1 SideEffect1 { get; }

        /// <summary>
        /// The second SideEffect produced by this Arrow.
        /// </summary>
        T2 SideEffect2 { get; }

        /// <summary>
        /// The third SideEffect produced by this Arrow.
        /// </summary>
        T3 SideEffect3 { get; }

    }

    #endregion

}
