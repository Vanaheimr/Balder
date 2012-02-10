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
using System.Collections.Generic;

#endregion

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// The interface of a triangle of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the triangle.</typeparam>
    public interface ITriangle<T> : IEquatable<ITriangle<T>>, IComparable<ITriangle<T>>, IComparable
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Properties

        /// <summary>
        /// The first pixel of the triangle.
        /// </summary>
        IPixel<T> P1 { get; }

        /// <summary>
        /// The second pixel of the triangle.
        /// </summary>
        IPixel<T> P2 { get; }

        /// <summary>
        /// The third pixel of the triangle.
        /// </summary>
        IPixel<T> P3 { get; }


        /// <summary>
        /// Return the cirumcenter of the triangle.
        /// </summary>
        IPixel<T> CircumCenter { get; }

        //orthocenter
        //centroid

        /// <summary>
        /// Return the circumcircle of the triangle.
        /// </summary>
        ICircle<T> CircumCircle { get; }

        /// <summary>
        /// Return an enumeration of lines representing the
        /// surrounding borders of the triangle.
        /// </summary>
        IEnumerable<ILine2D<T>> Borders { get; }

        #endregion

    }

}
