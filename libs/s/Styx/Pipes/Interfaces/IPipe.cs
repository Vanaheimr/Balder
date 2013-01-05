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

    #region IPipe

    /// <summary>
    /// The non-generic interface for any pipe implementation.
    /// </summary>
    public interface IPipe
        : IStartPipe,
          IEndPipe,
          IDisposable
    { }

    #endregion

    #region IPipe<in S, out E>

    /// <summary>
    /// The generic interface for any single-element pipe implementation.
    /// Such a pipe takes/consumes objects of type S and returns/emits objects of type E.
    /// S refers to <i>starts</i> and the E refers to <i>ends</i>.
    /// </summary>
    /// <typeparam name="S">The type of the consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    public interface IPipe<in S, out E>
        : IStartPipe<S>,
          IEndPipe<E>,
          IPipe
	{ }

    #endregion

    #region IPipe<in S1, in S2, out E>

    /// <summary>
    /// The generic interface for any two-element pipe implementation.
    /// Such a pipe takes/consumes objects of type S1 and S2 and returns/emits objects of type E.
    /// S1 and S2 refers to <i>starts</i> and the E refers to <i>ends</i>.
    /// </summary>
    /// <typeparam name="S1">The type of the first consuming objects.</typeparam>
    /// <typeparam name="S2">The type of the second consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    public interface IPipe<in S1, in S2, out E>
        : IStartPipe<S1, S2>,
          IEndPipe<E>,
          IPipe
    { }

    #endregion

    #region IPipe<in S1, in S2, in S3, out E>

    /// <summary>
    /// The generic interface for any three-element pipe implementation.
    /// Such a pipe takes/consumes objects of type S1, S2 and S3 and returns/emits objects of type E.
    /// S1, S2 and S3 refers to <i>starts</i> and the E refers to <i>ends</i>.
    /// </summary>
    /// <typeparam name="S1">The type of the first consuming objects.</typeparam>
    /// <typeparam name="S2">The type of the second consuming objects.</typeparam>
    /// <typeparam name="S3">The type of the third consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    public interface IPipe<in S1, in S2, in S3, out E>
        : IStartPipe<S1, S2, S3>,
          IEndPipe<E>,
          IPipe
    { }

    #endregion

    #region IPipe<in S1, in S2, in S3, in S4, out E>

    /// <summary>
    /// The generic interface for any four-element pipe implementation.
    /// Such a pipe takes/consumes objects of type S1, S2, S3 and S4 and returns/emits objects of type E.
    /// S1, S2, S3 and S4 refers to <i>starts</i> and the E refers to <i>ends</i>.
    /// </summary>
    /// <typeparam name="S1">The type of the first consuming objects.</typeparam>
    /// <typeparam name="S2">The type of the second consuming objects.</typeparam>
    /// <typeparam name="S3">The type of the third consuming objects.</typeparam>
    /// <typeparam name="S4">The type of the fourth consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    public interface IPipe<in S1, in S2, in S3, in S4, out E>
        : IStartPipe<S1, S2, S3, S4>,
          IEndPipe<E>,
          IPipe
    { }

    #endregion

    #region IPipe<in S1, in S2, in S3, in S4, in S5, out E>

    /// <summary>
    /// The generic interface for any five-element pipe implementation.
    /// Such a pipe takes/consumes objects of type S1, S2, S3, S4 and S5 and returns/emits objects of type E.
    /// S1, S2, S3, S4 and S5 refers to <i>starts</i> and the E refers to <i>ends</i>.
    /// </summary>
    /// <typeparam name="S1">The type of the first consuming objects.</typeparam>
    /// <typeparam name="S2">The type of the second consuming objects.</typeparam>
    /// <typeparam name="S3">The type of the third consuming objects.</typeparam>
    /// <typeparam name="S4">The type of the fourth consuming objects.</typeparam>
    /// <typeparam name="S5">The type of the fifth consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    public interface IPipe<in S1, in S2, in S3, in S4, in S5, out E>
        : IStartPipe<S1, S2, S3, S4, S5>,
          IEndPipe<E>,
          IPipe
    { }

    #endregion

}
