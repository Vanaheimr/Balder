/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
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

namespace de.ahzf.Blueprints.PropertyGraph
{

    /// <summary>
    /// The interface for all events and notifications
    /// of property graph elements.
    /// </summary>
    public interface IPropertiesNotifications<TKey, TValue>
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
    {

        /// <summary>
        /// Called whenever a property value will be added.
        /// </summary>
        event PropertyAdditionEventHandler<TKey, TValue> OnPropertyAddition;

        /// <summary>
        /// Called whenever a property value was added.
        /// </summary>
        event PropertyAddedEventHandler<TKey, TValue> OnPropertyAdded;


        /// <summary>
        /// Called whenever a property value will be changed.
        /// </summary>
        event PropertyChangingEventHandler<TKey, TValue> OnPropertyChanging;

        /// <summary>
        /// Called whenever a property value was changed.
        /// </summary>
        event PropertyChangedEventHandler<TKey, TValue> OnPropertyChanged;


        /// <summary>
        /// Called whenever a property value will be removed.
        /// </summary>
        event PropertyRemovalEventHandler<TKey, TValue> OnPropertyRemoval;

        /// <summary>
        /// Called whenever a property value was removed.
        /// </summary>
        event PropertyRemovedEventHandler<TKey, TValue> OnPropertyRemoved;

    }

}
