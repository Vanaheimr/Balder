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

    #region IPropertyElement<TId, TRevisionId, TKey, TValue>

    /// <summary>
    /// The common interface for all graph elements (vertex, edge, hyperedge).
    /// </summary>
    /// <typeparam name="TId">The type of the identifiers.</typeparam>
    /// <typeparam name="TRevisionId">The type of the revision identifiers.</typeparam>
    /// <typeparam name="TKey">The type of the property keys.</typeparam>
    /// <typeparam name="TValue">The type of the property values.</typeparam>
    /// <typeparam name="TDatastructure">The type of the datastructure to maintain the key/value pairs.</typeparam>
    public interface IPropertyElement<TId, TRevisionId, TKey, TValue>
                        : IIdentifier<TId>,
                          IRevisionId<TRevisionId>,
                          IPropertyNotifications,
                          IEquatable <IPropertyElement<TId, TRevisionId, TKey, TValue>>,
                          IComparable<IPropertyElement<TId, TRevisionId, TKey, TValue>>,
                          IComparable
                          

        where TId            : IEquatable<TId>,         IComparable<TId>,         IComparable, TValue
        where TRevisionId    : IEquatable<TRevisionId>, IComparable<TRevisionId>, IComparable, TValue
        where TKey           : IEquatable<TKey>,        IComparable<TKey>,        IComparable

    {

        /// <summary>
        /// Return the element properties (its embedded data).
        /// </summary>
        IProperties<TKey, TValue> Properties { get; }

    }

    #endregion

    #region IPropertyElement<TId, TRevisionId, TKey, TValue, TDatastructure>

    /// <summary>
    /// The common interface for all graph elements (vertex, edge, hyperedge).
    /// </summary>
    /// <typeparam name="TId">The type of the identifiers.</typeparam>
    /// <typeparam name="TRevisionId">The type of the revision identifiers.</typeparam>
    /// <typeparam name="TKey">The type of the property keys.</typeparam>
    /// <typeparam name="TValue">The type of the property values.</typeparam>
    /// <typeparam name="TDatastructure">The type of the datastructure to maintain the key/value pairs.</typeparam>
    public interface IPropertyElement<TId, TRevisionId, TKey, TValue, TDatastructure>
                        : IPropertyElement<TId, TRevisionId, TKey, TValue>,
                          IPropertyNotifications
                          //IEquatable <IPropertyElement<TId, TRevisionId, TKey, TValue, TDatastructure>>,
                          //IComparable<IPropertyElement<TId, TRevisionId, TKey, TValue, TDatastructure>>,
                          //IComparable

        where TId            : IEquatable<TId>,         IComparable<TId>,         IComparable, TValue
        where TRevisionId    : IEquatable<TRevisionId>, IComparable<TRevisionId>, IComparable, TValue
        where TKey           : IEquatable<TKey>,        IComparable<TKey>,        IComparable
        where TDatastructure : IDictionary<TKey, TValue>

    {

        ///// <summary>
        ///// Return the element properties (its embedded data).
        ///// </summary>
        //IProperties<TKey, TValue, TDatastructure> Properties { get; }

    }

    #endregion

}
