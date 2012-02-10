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
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// Extensions to the IPixel interface.
    /// </summary>
    public static class IPixelExtensions
    {

        #region IsInRectangle(this IPixel, X1, Y1, X2, Y2)

        /// <summary>
        /// Checks if the given pixel is located
        /// within the given rectangle.
        /// </summary>
        /// <param name="Pixel">A pixel of type T.</param>
        /// <param name="X1"></param>
        /// <param name="Y1"></param>
        /// <param name="X2"></param>
        /// <param name="Y2"></param>        
        /// <returns>True if the pixel is located within the given rectangle; False otherwise.</returns>
        public static Boolean IsInRectangle<T>(this IPixel<T> Pixel, T X1, T Y1, T X2, T Y2)
            where T : IEquatable<T>, IComparable<T>, IComparable
        {

            #region Initial Checks

            if (Pixel == null)
                throw new ArgumentNullException("The given pixel must not be null!");

            #endregion

            return new Rectangle<T>(X1, Y1, X2, Y2).Contains(Pixel);

        }

        #endregion

        #region IsInRectangle(this IPixel, Pixel1, Pixel2)

        /// <summary>
        /// Checks if the given pixel is located
        /// within the given rectangle.
        /// </summary>
        /// <param name="Pixel">A pixel of type T.</param>
        /// <param name="Pixel1">The left/top pixel of the rectangle.</param>
        /// <param name="Pixel2">The right/bottom pixel of the rectangle.</param>
        /// <returns>True if the pixel is located within the given rectangle; False otherwise.</returns>
        public static Boolean IsInRectangle<T>(this IPixel<T> Pixel, IPixel<T> Pixel1, IPixel<T> Pixel2)
            where T : IEquatable<T>, IComparable<T>, IComparable
        {

            #region Initial Checks

            if (Pixel == null)
                throw new ArgumentNullException("The given pixel must not be null!");

            #endregion

            return new Rectangle<T>(Pixel1, Pixel2).Contains(Pixel);

        }

        #endregion

        #region IsInRectangle(this IPixel, Rectangle)

        /// <summary>
        /// Checks if the given pixel is located
        /// within the given rectangle.
        /// </summary>
        /// <param name="Pixel">A pixel of type T.</param>
        /// <param name="Rectangle">A rectangle of type T.</param>
        /// <returns>True if the pixel is located within the given rectangle; False otherwise.</returns>
        public static Boolean IsInRectangle<T>(this IPixel<T> Pixel, IRectangle<T> Rectangle)
            where T : IEquatable<T>, IComparable<T>, IComparable
        {

            #region Initial Checks

            if (Pixel == null)
                throw new ArgumentNullException("The given pixel must not be null!");

            if (Rectangle == null)
                throw new ArgumentNullException("The given rectangle must not be null!");

            #endregion

            return Rectangle.Contains(Pixel);

        }

        #endregion


        #region IsInCircle(this IPixel, Circle)

        /// <summary>
        /// Checks if the given pixel is located
        /// within the given rectangle.
        /// </summary>
        /// <param name="Pixel">A pixel of type T.</param>
        /// <param name="Circle">A circle of type T.</param>
        /// <returns>True if the pixel is located within the given circle; False otherwise.</returns>
        public static Boolean IsInCircle<T>(this IPixel<T> Pixel, ICircle<T> Circle)
            where T : IEquatable<T>, IComparable<T>, IComparable
        {

            #region Initial Checks

            if (Pixel == null)
                throw new ArgumentNullException("The given first pixel must not be null!");

            if (Circle == null)
                throw new ArgumentNullException("The given first edgepixel must not be null!");

            #endregion

            return Circle.Contains(Pixel);

        }

        #endregion

    }

}
