/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET <http://www.github.com/ahzf/Blueprints.NET>
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

namespace de.ahzf.Blueprints.PropertyGraphs
{

    /// <summary>
    /// A simplified generic property graph having the same depended
    /// types for vertices, edges, multiedges and hyperedges.
    /// </summary>
    public interface ICommonGenericPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue>
                         : IGenericPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue,  // Vertex definition
                                                 TId, TRevisionId, TLabel, TKey, TValue,  // Edge definition
                                                 TId, TRevisionId, TLabel, TKey, TValue,  // MultiEdge definition
                                                 TId, TRevisionId, TLabel, TKey, TValue>  // Hyperedge definition

        where TId         : IEquatable<TId>,         IComparable<TId>,         IComparable, TValue
        where TRevisionId : IEquatable<TRevisionId>, IComparable<TRevisionId>, IComparable, TValue
        where TKey        : IEquatable<TKey>,        IComparable<TKey>,        IComparable
        where TLabel      : IEquatable<TLabel>,      IComparable<TLabel>,      IComparable

    { }

}
