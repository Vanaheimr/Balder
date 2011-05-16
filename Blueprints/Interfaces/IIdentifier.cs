/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET <http://www.github.com/ahzf/blueprints.NET>
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
using System.Collections.Generic;

#endregion

namespace de.ahzf.blueprints
{

    /// <summary>
    /// Provides a generic identifier that is unique for its implementing class.
    /// </summary>
    /// <typeparam name="TId">The type of the id.</typeparam>
    public interface IIdentifier<TId> : IEquatable<TId>, IComparable<TId>, IComparable
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    {

        /// <summary>
        /// A generic identifier that is unique to its implementing class.
        /// All vertices, edges and hyper edges of a graph must have unique identifiers.
        /// </summary>
        TId Id { get; }

    }

}
