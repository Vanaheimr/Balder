/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
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

namespace de.ahzf.Styx
{

    #region ISplitPipe

    /// <summary>
    /// A SplitPipe consumes objects of type S and emits objects of type E1 and E2.
    /// </summary>
    public interface ISplitPipe : IDisposable
    { }

    #endregion

    #region ISplitPipe<in S, out E1, out E2>

    /// <summary>
    /// A SplitPipe consumes objects of type S and emits objects of type E1 and E2.
    /// </summary>
    /// <typeparam name="S">The type of the consuming objects.</typeparam>
    /// <typeparam name="E1">The type of the first emitting objects.</typeparam>
    /// <typeparam name="E2">The type of the second emitting objects.</typeparam>
    public interface ISplitPipe<in S, out E1, out E2> : ISplitPipe
	{ }

    #endregion

}
