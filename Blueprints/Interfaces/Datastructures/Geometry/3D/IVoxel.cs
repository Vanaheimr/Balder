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

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// The interface of a voxel of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the voxel.</typeparam>
    public interface IVoxel<T> : IEquatable<IVoxel<T>>, IComparable<IVoxel<T>>, IComparable
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Properties

        /// <summary>
        /// The X-coordinate.
        /// </summary>
        T X { get; }

        /// <summary>
        /// The Y-coordinate.
        /// </summary>
        T Y { get; }

        /// <summary>
        /// The Z-coordinate.
        /// </summary>
        T Z { get; }

        #endregion


        #region DistanceTo(x, y, z)

        /// <summary>
        /// A method to calculate the distance between this
        /// voxel and the given coordinates of type T.
        /// </summary>
        /// <param name="x">A x-coordinate of type T</param>
        /// <param name="y">A y-coordinate of type T</param>
        /// <param name="z">A z-coordinate of type T</param>
        /// <returns>The distance between this voxel and the given coordinates.</returns>
        T DistanceTo(T x, T y, T z);

        #endregion

        #region DistanceTo(Voxel)

        /// <summary>
        /// A method to calculate the distance between
        /// this and another voxel of type T.
        /// </summary>
        /// <param name="Voxel">A voxel of type T</param>
        /// <returns>The distance between this voxel and the given voxel.</returns>
        T DistanceTo(IVoxel<T> Voxel);

        #endregion

    }

}
