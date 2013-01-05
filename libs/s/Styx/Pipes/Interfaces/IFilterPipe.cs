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

namespace de.ahzf.Vanaheimr.Styx
{

    /// <summary>
    /// A FilterPipe is much like the IdentityPipe, but may or may not filter 
    /// some of the messages/objects instead of emitting everything.
    /// </summary>
    /// <typeparam name="S">The type of the elements within the filter.</typeparam>
    public interface IFilterPipe<S> : IPipe<S, S>
    { }

}
