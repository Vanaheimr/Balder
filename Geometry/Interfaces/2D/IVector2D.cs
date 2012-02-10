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
using de.ahzf.Blueprints.Maths;

#endregion

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// The interface of a 2-dimensional vector of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the vector.</typeparam>
    public interface IVector2D<T> : IMaths<IVector2D<T>>, IEquatable<IVector2D<T>>, IComparable<IVector2D<T>>, IComparable
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Properties

        /// <summary>
        /// The ToLeft-component of the vector.
        /// </summary>
        T X { get; }

        /// <summary>
        /// The ToBottom-component of the vector.
        /// </summary>
        T Y { get; }

        /// <summary>
        /// The length of the vector.
        /// </summary>
        T Length { get; }


        /// <summary>
        /// Return a normalized vector.
        /// </summary>
        IVector2D<T> NormVector { get; } 

        #endregion


        #region IsParallelTo(Vector)

        /// <summary>
        /// Determines if the given vector is parallel or
        /// antiparallel to this vector.
        /// </summary>
        /// <param name="Vector">A vector.</param>
        Boolean IsParallelTo(IVector2D<T> Vector);

        #endregion

        #region DistanceTo(x, y)

        /// <summary>
        /// A method to calculate the distance between this
        /// vector and the given coordinates of type T.
        /// </summary>
        /// <param name="x">A x-coordinate of type T</param>
        /// <param name="y">A y-coordinate of type T</param>
        /// <returns>The distance between this vector and the given coordinates.</returns>
        T DistanceTo(T x, T y);

        #endregion

        #region DistanceTo(Vector)

        /// <summary>
        /// A method to calculate the distance between
        /// this and another vector of type T.
        /// </summary>
        /// <param name="Vector">A vector of type T</param>
        /// <returns>The distance between this vector and the given vector.</returns>
        T DistanceTo(IVector2D<T> Vector);

        #endregion

    }

}
