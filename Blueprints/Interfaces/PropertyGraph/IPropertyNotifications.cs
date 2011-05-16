﻿/*
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

using System.ComponentModel;
using System.Collections.Specialized;

#endregion

namespace de.ahzf.blueprints.PropertyGraph
{

    /// <summary>
    /// An interface for all events and notifications of a
    /// property graph element.
    /// </summary>
    public interface IPropertyNotifications : INotifyCollectionChanged,
                                              INotifyPropertyChanging,
                                              INotifyPropertyChanged

    { }

}