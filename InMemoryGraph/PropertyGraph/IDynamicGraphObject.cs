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
using System.Dynamic;
using System.ComponentModel;

#endregion

namespace de.ahzf.blueprints
{

    /// <summary>
    /// An interface for simplified interaction with dynamic objects.
    /// </summary>
    /// <typeparam name="T">The compile time type of the DynamicMetaObject.</typeparam>
    public interface IDynamicGraphObject<T> : IDynamicMetaObjectProvider
    {

        /// <summary>
        /// Assign the given value to the given binder name.
        /// </summary>
        /// <param name="myBinder">A binder name.</param>
        /// <param name="myObject">A value.</param>
        Object SetMember    (String myBinder, Object myObject);

        /// <summary>
        /// Return the value of the given binder name.
        /// </summary>
        /// <param name="myBinder">A binder name.</param>
        Object GetMember    (String myBinder);

        /// <summary>
        /// Delete the given binder name.
        /// </summary>
        /// <param name="myBinder">A binder name.</param>
        Object DeleteMember (String myBinder);

    }

}
