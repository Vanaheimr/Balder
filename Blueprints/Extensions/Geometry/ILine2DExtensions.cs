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
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// Extensions to the ILine2D interface.
    /// </summary>
    public static class ILine2DExtensions
    {

        #region IntersectsWith<T>(this Line1, Line2, InfiniteLines = false)

        /// <summary>
        /// Checks if the given lines intersect.
        /// </summary>
        /// <param name="Line1">A line.</param>
        /// <param name="Line2">A line.</param>
        /// <param name="InfiniteLines">Wether the lines should be treated as infinite or not.</param>
        /// <returns>True if the lines intersect; False otherwise.</returns>
        public static Boolean IntersectsWith<T>(this ILine2D<T> Line1, ILine2D<T> Line2, Boolean InfiniteLines = false)
            where T : IEquatable<T>, IComparable<T>, IComparable
        {
            IPixel<T> Intersection;
            return Line1.IntersectsWith(Line2, out Intersection, InfiniteLines);
        }

        #endregion

        #region Intersection<T>(this Line1, Line2, InfiniteLines = false)

        /// <summary>
        /// Returns the intersection of both lines.
        /// </summary>
        /// <param name="Line1">A line.</param>
        /// <param name="Line2">A line.</param>
        /// <param name="InfiniteLines">Wether the lines should be treated as infinite or not.</param>
        public static IPixel<T> Intersection<T>(this ILine2D<T> Line1, ILine2D<T> Line2, Boolean InfiniteLines = false)
            where T : IEquatable<T>, IComparable<T>, IComparable
        {
            IPixel<T> Intersection;
            Line1.IntersectsWith(Line2, out Intersection, InfiniteLines);
            return Intersection;
        }

        #endregion

        #region IsJustAPixel<T>(this Line)

        /// <summary>
        /// Checks if the given line is "just a pixel".
        /// </summary>
        /// <param name="Line">A line.</param>
        public static Boolean IsJustAPixel<T>(this ILine2D<T> Line)
            where T : IEquatable<T>, IComparable<T>, IComparable
        {
            return (Line.X1.Equals(Line.X2) &&
                    Line.Y1.Equals(Line.Y2));
        }

        #endregion

    }

}
