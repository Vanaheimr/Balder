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
    /// An event handler called whenever a property value will be added.
    /// </summary>
    /// <param name="Sender">The sender of this event.</param>
    /// <param name="Key">The key of the property to be added.</param>
    /// <param name="Value">The value of the property to be added.</param>
    public delegate void PropertyAdditionEventHandler<TKey, TValue>(IProperties<TKey, TValue> Sender, TKey Key, TValue Value)
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable;

    /// <summary>
    /// An event handler called whenever a property value was added.
    /// </summary>
    /// <param name="Sender">The sender of this event.</param>
    /// <param name="Key">The key of the added property.</param>
    /// <param name="Value">The value of the added property.</param>
    public delegate void PropertyAddedEventHandler<TKey, TValue>(IProperties<TKey, TValue> Sender, TKey Key, TValue Value)
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable;


    /// <summary>
    /// An event handler called whenever a property value will be changed.
    /// </summary>
    /// <param name="Sender">The sender of this event.</param>
    /// <param name="Key">The key of the property to be changed.</param>
    /// <param name="OldValue">The old value of the property to be changed.</param>
    /// <param name="NewValue">The new value of the property to be changed.</param>
    public delegate void PropertyChangingEventHandler<TKey, TValue>(IProperties<TKey, TValue> Sender, TKey Key, TValue OldValue, TValue NewValue)
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable;

    /// <summary>
    /// An event handler called whenever a property value was changed.
    /// </summary>
    /// <param name="Sender">The sender of this event.</param>
    /// <param name="Key">The key of the changed property.</param>
    /// <param name="OldValue">The old value of the changed property.</param>
    /// <param name="NewValue">The new value of the changed property.</param>
    public delegate void PropertyChangedEventHandler<TKey, TValue>(IProperties<TKey, TValue> Sender, TKey Key, TValue OldValue, TValue NewValue)
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable;


    /// <summary>
    /// An event handler called whenever a property will be removed.
    /// </summary>
    /// <param name="Sender">The sender of this event.</param>
    /// <param name="Key">The key of the property to be removed.</param>
    /// <param name="Value">The value of the property to be removed.</param>
    public delegate void PropertyRemovalEventHandler<TKey, TValue>(IProperties<TKey, TValue> Sender, TKey Key, TValue Value)
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable;

    /// <summary>
    /// An event handler called whenever a property was removed.
    /// </summary>
    /// <param name="Sender">The sender of this event.</param>
    /// <param name="Key">The key of the removed property.</param>
    /// <param name="Value">The value of the removed property.</param>
    public delegate void PropertyRemovedEventHandler<TKey, TValue>(IProperties<TKey, TValue> Sender, TKey Key, TValue Value)
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable;

}
