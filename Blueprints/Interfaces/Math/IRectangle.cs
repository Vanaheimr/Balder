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
    /// The interface of a rectangle of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the rectangle.</typeparam>
    public interface IRectangle<T> : IEquatable<IRectangle<T>>
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
        /// Right
        /// </summary>
        T Right { get; }

        /// <summary>
        /// Bottom
        /// </summary>
        T Bottom { get; }


        /// <summary>
        /// The width of the cube.
        /// </summary>
        T Width { get; }

        /// <summary>
        /// The height of the cube.
        /// </summary>
        T Height { get; }

        #endregion

        #region Contains(x, y)

        /// <summary>
        /// Checks if the given x- and y-coordinates are
        /// located within this rectangle.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>True if the coordinates are located within this rectangle; False otherwise.</returns>
        Boolean Contains(T x, T y);

        #endregion

        #region Contains(Rectangle)

        /// <summary>
        /// Checks if the given rectangle is located
        /// within this rectangle.
        /// </summary>
        /// <param name="Rectangle">A rectangle of type T.</param>
        /// <returns>True if the rectangle is located within this rectangle; False otherwise.</returns>
        Boolean Contains(Rectangle<T> Rectangle);

        #endregion

    }

}
