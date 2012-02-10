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

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// The interface of a VoxelValuePair.
    /// </summary>
    /// <typeparam name="TKey">The internal type of the voxel.</typeparam>
    /// <typeparam name="TValue">The type of the stored values.</typeparam>
    public interface IVoxelValuePair<TKey, TValue> : IVoxel<TKey>,
                                                     IEquatable <IVoxelValuePair<TKey, TValue>>,
                                                     IComparable<IVoxelValuePair<TKey, TValue>>,
                                                     IComparable

        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

    {

        /// <summary>
        /// The value stored together with a voxel.
        /// </summary>
        TValue Value { get; }

    }

}
