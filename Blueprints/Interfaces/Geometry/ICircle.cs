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
    /// The interface of a circle of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the circle.</typeparam>
    public interface ICircle<T> : IEquatable<ICircle<T>>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Properties

        /// <summary>
        /// The left-coordinate of the circle.
        /// </summary>
        T         Left     { get; }

        /// <summary>
        /// The top-coordinate of the circle.
        /// </summary>
        T         Top      { get; }

        /// <summary>
        /// The center of the circle.
        /// </summary>
        IPixel<T> Center   { get; }

        /// <summary>
        /// Radius
        /// </summary>
        T         Radius   { get; }


        /// <summary>
        /// The diameter of the circle.
        /// </summary>
        T         Diameter { get; }

        #endregion


        #region Contains(x, y)

        /// <summary>
        /// Checks if the given x- and y-coordinates are
        /// located within this circle.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>True if the coordinates are located within this circle; False otherwise.</returns>
        Boolean Contains(T x, T y);

        #endregion

        #region Contains(Pixel)

        /// <summary>
        /// Checks if the given pixel is located
        /// within this circle.
        /// </summary>
        /// <param name="Pixel">A pixel.</param>
        /// <returns>True if the pixel is located within this circle; False otherwise.</returns>
        Boolean Contains(IPixel<T> Pixel);

        #endregion

        #region Contains(Circle)

        /// <summary>
        /// Checks if the given circle is located
        /// within this circle.
        /// </summary>
        /// <param name="Circle">A circle of type T.</param>
        /// <returns>True if the circle is located within this circle; False otherwise.</returns>
        Boolean Contains(ICircle<T> Circle);

        #endregion
        
        #region Overlaps(Circle)

        /// <summary>
        /// Checks if the given circle shares some
        /// area with this circle.
        /// </summary>
        /// <param name="Circle">A circle of type T.</param>
        /// <returns>True if the circle shares some area with this circle; False otherwise.</returns>
        Boolean Overlaps(ICircle<T> Circle);

        #endregion

    }

}
