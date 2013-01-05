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

#region Usings

using System;

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    #region IArrow

    /// <summary>
    /// The common interface for any Arrow implementation.
    /// </summary>
    public interface INewArrow : IDisposable
    { }

    #endregion

    #region IArrow<in TIn, TOut>

    /// <summary>
    /// The generic interface for any Arrow implementation.
    /// An Arrow accepts/consumes messages/objects of type S and emits messages/objects
    /// of type E via an event.
    /// </summary>
    /// <typeparam name="TIn">The type of the consuming messages/objects.</typeparam>
    /// <typeparam name="TOut">The type of the emitted messages/objects.</typeparam>
    public interface INewArrow<TIn, in TOut> : INotification<TIn>, ITarget<TOut>, INewArrow
    { }

    #endregion

}
