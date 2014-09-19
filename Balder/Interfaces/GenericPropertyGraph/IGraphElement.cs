/*
 * Copyright (c) 2010-2014, Achim 'ahzf' Friedland <achim@graphdefined.org>
 * This file is part of Balder <http://www.github.com/Vanaheimr/Balder>
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

using org.GraphDefined.Vanaheimr.Illias.Collections;

#endregion

namespace org.GraphDefined.Vanaheimr.Balder
{

    /// <summary>
    /// The common interface for all property graph elements:
    /// </summary>
    /// <typeparam name="TId">The type of the identifiers.</typeparam>
    /// <typeparam name="TRevId">The type of the revision identifiers.</typeparam>
    /// <typeparam name="TLabel">The taype of the labels.</typeparam>
    /// <typeparam name="TKey">The type of the property keys.</typeparam>
    /// <typeparam name="TValue">The type of the property values.</typeparam>
    public interface IGraphElement<TId, TRevId, TLabel, TKey, TValue>
                                  : IReadOnlyGraphElement<TId, TRevId, TLabel, TKey, TValue>,
                                    IProperties<TKey, TValue>,
                                    IEquatable <IGraphElement<TId, TRevId, TLabel, TKey, TValue>>,
                                    IComparable<IGraphElement<TId, TRevId, TLabel, TKey, TValue>>

        where TId     : IEquatable<TId>,    IComparable<TId>,    IComparable, TValue
        where TRevId  : IEquatable<TRevId>, IComparable<TRevId>, IComparable, TValue
        where TLabel  : IEquatable<TLabel>, IComparable<TLabel>, IComparable
        where TKey    : IEquatable<TKey>,   IComparable<TKey>,   IComparable

    { }

}
