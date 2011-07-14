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
    /// The interface of a 1-dimensional line of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the line.</typeparam>
    public interface ILine<T> : IEquatable<ILine<T>>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Properties

        /// <summary>
        /// The left-coordinate of the line.
        /// </summary>
        T Left { get; }

        /// <summary>
        /// The right-coordinate of the line.
        /// </summary>
        T Right { get; }

        #endregion


        #region Contains(Element)

        /// <summary>
        /// Checks if the given element is located on this line.
        /// </summary>
        /// <param name="Element">An element.</param>
        /// <returns>True if the element is located on this line; False otherwise.</returns>
        Boolean Contains(T Element);

        #endregion

        #region Contains(Line)

        /// <summary>
        /// Checks if the given line is located on this line.
        /// </summary>
        /// <param name="Line">A line of type T.</param>
        /// <returns>True if the line is located on this line; False otherwise.</returns>
        Boolean Contains(ILine<T> Line);

        #endregion

        #region Overlaps(Line)

        /// <summary>
        /// Checks if the given line shares some
        /// area with this line.
        /// </summary>
        /// <param name="Line">A line of type T.</param>
        /// <returns>True if the line shares some area with this line; False otherwise.</returns>
        Boolean Overlaps(ILine<T> Line);

        #endregion

    }

}
