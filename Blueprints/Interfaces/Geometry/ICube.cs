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
    /// The interface of a cube of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the cube.</typeparam>
    public interface ICube<T> : IEquatable<ICube<T>>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Properties

        /// <summary>
        /// Left
        /// </summary>
        T Left { get; }

        /// <summary>
        /// Top
        /// </summary>
        T Top { get; }

        /// <summary>
        /// Front
        /// </summary>
        T Front { get; }

        /// <summary>
        /// Right
        /// </summary>
        T Right { get; }

        /// <summary>
        /// Bottom
        /// </summary>
        T Bottom { get; }

        /// <summary>
        /// Behind
        /// </summary>
        T Behind { get; }


        /// <summary>
        /// The width of the cube.
        /// </summary>
        T Width { get; }

        /// <summary>
        /// The height of the cube.
        /// </summary>
        T Height { get; }

        /// <summary>
        /// The depth of the cube.
        /// </summary>
        T Depth { get; }

        #endregion


        #region Contains(x, y, z)

        /// <summary>
        /// Checks if the given x-, y- and z-coordinates
        /// are located within this cube.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="z">The z-coordinate.</param>
        /// <returns>True if the coordinates are located within this cube; False otherwise.</returns>
        Boolean Contains(T x, T y, T z);

        #endregion

        #region Contains(Voxel)

        /// <summary>
        /// Checks if the given voxel is located
        /// within this cube.
        /// </summary>
        /// <param name="Voxel">A voxel of type T.</param>
        /// <returns>True if the voxel is located within this cube; False otherwise.</returns>
        Boolean Contains(IVoxel<T> Voxel);

        #endregion

        #region Contains(Cube)

        /// <summary>
        /// Checks if the given cube is located
        /// within this cube.
        /// </summary>
        /// <param name="Cube">A cube of type T.</param>
        /// <returns>True if the cube is located within this cube; False otherwise.</returns>
        Boolean Contains(ICube<T> Cube);

        #endregion

        #region Overlaps(Cube)

        /// <summary>
        /// Checks if the given cube shares some
        /// area with this cube.
        /// </summary>
        /// <param name="Cube">A cube of type T.</param>
        /// <returns>True if the cube shares some area with this cube; False otherwise.</returns>
        Boolean Overlaps(ICube<T> Cube);

        #endregion

    }

}
