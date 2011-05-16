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

#endregion

namespace de.ahzf.blueprints.GenericGraph
{

    /// <summary>
    /// A generic element is the foundation of all graph elements like
    /// vertices, edges and hyperedges. It gives them their minimal
    /// information like an identifier, a revision identifier and some
    /// sort of embedded data.
    /// </summary>
    /// <typeparam name="TId">The type of the identifiers.</typeparam>
    /// <typeparam name="TRevisionId">The type of the revision identifiers.</typeparam>
    /// <typeparam name="TData">The type of the embedded data.</typeparam>
    public interface IGenericElement<TId, TRevisionId, TData>
                        : IIdentifier<TId>,
                          IRevisionId<TRevisionId>

        where TId         : IEquatable<TId>,         IComparable<TId>,         IComparable
        where TRevisionId : IEquatable<TRevisionId>, IComparable<TRevisionId>, IComparable

    {

        /// <summary>
        /// Return the graph element data.
        /// </summary>
        TData Data { get; }

    }

}
