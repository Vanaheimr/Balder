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
using de.ahzf.blueprints.Datastructures;

#endregion

namespace de.ahzf.blueprints
{

    /// <summary>
    /// This class combines the IProperties interface with an identifier
    /// coming from the IIdentifier interface.
    /// </summary>
    /// <typeparam name="TId">The type of the id.</typeparam>
    /// <typeparam name="TKey">The type of the property keys.</typeparam>
    /// <typeparam name="TValue">The type of the property values.</typeparam>
    /// <typeparam name="TDatastructure">The type of the datastructure to maintain the key/value pairs.</typeparam>
    public interface IPropertiesWithIds<TId, TRevisionId, TKey, TValue, TDatastructure>
                        : IProperties<TKey, TValue, TDatastructure>, IIdentifier<TId>, IRevisionId<TRevisionId>

        where TId            : IEquatable<TId>,         IComparable<TId>,         IComparable
        where TRevisionId    : IEquatable<TRevisionId>, IComparable<TRevisionId>, IComparable
        where TKey           : IEquatable<TKey>,        IComparable<TKey>,        IComparable
        where TDatastructure : IDictionary<TKey, TValue>

    { }

}
