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

    #region IFilterArrow

    /// <summary>
    /// A FilterArrow is much like the IdentityArrow, but may or may not filter 
    /// some of the messages/objects instead of emitting everything.
    /// </summary>
    public interface INewFilterArrow : INewArrow
    { }

    #endregion

    #region IFilterArrow<TMessage>

    /// <summary>
    /// A FilterArrow is much like the IdentityArrow, but may or may not filter 
    /// some of the messages/objects instead of emitting everything.
    /// </summary>
    /// <typeparam name="TMessage">The type of the consuming messages/objects.</typeparam>
    public interface INewFilterArrow<TMessage> : INewFilterArrow, INewArrow<TMessage, TMessage>
    { }

    #endregion

}
