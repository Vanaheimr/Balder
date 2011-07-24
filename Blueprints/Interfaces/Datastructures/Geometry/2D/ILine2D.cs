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
    /// The interface of a 2-dimensional line of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the line.</typeparam>
    public interface ILine2D<T> : IEquatable<ILine2D<T>>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Properties

        /// <summary>
        /// The first x-coordinate of the line.
        /// </summary>
        T X1 { get; }

        /// <summary>
        /// The first y-coordinate of the line.
        /// </summary>
        T Y1 { get; }

        /// <summary>
        /// The second x-coordinate of the line.
        /// </summary>
        T X2 { get; }

        /// <summary>
        /// The second y-coordinate of the line.
        /// </summary>
        T Y2 { get; }

        
        /// <summary>
        /// The length of the line.
        /// </summary>
        T Length { get; }


        /// <summary>
        /// The gradient/inclination of the line.
        /// </summary>
        T Gradient { get; }

        /// <summary>
        /// The interception of the line with the y-axis.
        /// </summary>
        T YIntercept { get; }

        /// <summary>
        /// The center pixel of the line.
        /// </summary>
        IPixel<T> Center { get; }

        /// <summary>
        /// The vector of the line.
        /// </summary>
        IVector2D<T> Vector  { get; }

        /// <summary>
        /// The normale vector of the line.
        /// </summary>
        IVector2D<T> Normale { get; }

        #endregion


        /// <summary>
        /// Checks if the given pixel is located on this line.
        /// </summary>
        /// <param name="Pixel">A pixel of type T.</param>
        /// <returns>True if the pixel is located on this line; False otherwise.</returns>
        Boolean Contains(IPixel<T> Pixel);

        /// <summary>
        /// Checks if and where the given lines intersect.
        /// </summary>
        /// <param name="Line">A line.</param>
        /// <param name="Pixel">The intersection of both lines.</param>        
        /// <returns>True if the lines intersect; False otherwise.</returns>
        Boolean Intersect(ILine2D<T> Line, out IPixel<T> Pixel);

    }

}
