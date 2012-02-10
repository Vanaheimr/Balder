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
    /// The interface of a sphere of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the sphere.</typeparam>
    public interface ISphere<T> : IEquatable<ISphere<T>>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Properties

        /// <summary>
        /// The left-coordinate of the sphere.
        /// </summary>
        T         Left     { get; }

        /// <summary>
        /// The top-coordinate of the sphere.
        /// </summary>
        T         Top      { get; }

        /// <summary>
        /// The front-coordinate of the sphere.
        /// </summary>
        T         Front    { get; }

        /// <summary>
        /// The center of the sphere.
        /// </summary>
        IVoxel<T> Center   { get; }

        /// <summary>
        /// Radius
        /// </summary>
        T         Radius   { get; }


        /// <summary>
        /// The diameter of the sphere.
        /// </summary>
        T         Diameter { get; }

        #endregion


        #region Contains(x, y, z)

        /// <summary>
        /// Checks if the given x-, y- and z-coordinates
        /// are located within this sphere.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="z">The z-coordinate.</param>
        /// <returns>True if the coordinates are located within this sphere; False otherwise.</returns>
        Boolean Contains(T x, T y, T z);

        #endregion

        #region Contains(Voxel)

        /// <summary>
        /// Checks if the given voxel is located
        /// within this sphere.
        /// </summary>
        /// <param name="Voxel">A voxel.</param>
        /// <returns>True if the voxel is located within this sphere; False otherwise.</returns>
        Boolean Contains(IVoxel<T> Voxel);

        #endregion

        #region Contains(Sphere)

        /// <summary>
        /// Checks if the given sphere is located
        /// within this sphere.
        /// </summary>
        /// <param name="Sphere">A sphere of type T.</param>
        /// <returns>True if the sphere is located within this sphere; False otherwise.</returns>
        Boolean Contains(ISphere<T> Sphere);

        #endregion
        
        #region Overlaps(Sphere)

        /// <summary>
        /// Checks if the given sphere shares some
        /// area with this sphere.
        /// </summary>
        /// <param name="Sphere">A sphere of type T.</param>
        /// <returns>True if the sphere shares some area with this sphere; False otherwise.</returns>
        Boolean Overlaps(ISphere<T> Sphere);

        #endregion

    }

}
