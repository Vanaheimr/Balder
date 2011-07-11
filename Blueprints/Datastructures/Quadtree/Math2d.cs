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
    /// 2d Math operations on type T.
    /// </summary>
    /// <typeparam name="T">The internal datatype.</typeparam>
    public abstract class Math2D<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Abstract Math Operations

        #region Min(a, b)

        /// <summary>
        /// A method to get the minimum of two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The minimum of a and b: Min(a, b)</returns>
        protected abstract T Min(T a, T b);

        #endregion

        #region Max(a, b)

        /// <summary>
        /// A method to get the maximum of two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The maximum of a and b: Max(a, b)</returns>
        protected abstract T Max(T a, T b);

        #endregion


        #region Add(a, b)

        /// <summary>
        /// A method to add two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The addition of a and b: a + b</returns>
        protected abstract T Add(T a, T b);

        #endregion

        #region Sub(a, b)

        /// <summary>
        /// A method to sub two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The subtraction of b from a: a - b</returns>
        protected abstract T Sub(T a, T b);

        #endregion

        #region Mul(a, b)

        /// <summary>
        /// A method to multiply two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The multiplication of a and b: a * b</returns>
        protected abstract T Mul(T a, T b);

        #endregion

        #region Div(a, b)

        /// <summary>
        /// A method to divide two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The division of a by b: a / b</returns>
        protected abstract T Div(T a, T b);

        #endregion

        #region Pow(a, b)

        /// <summary>
        /// A method to calculate an internal datatype raised to the specified power.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The value a raised to the specified power of b: a^b</returns>
        protected abstract T Pow(T a, T b);

        #endregion


        #region Abs(a)

        /// <summary>
        /// A method to calculate the absolute value of the internal datatype.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <returns>The absolute value of a: Abs(a)</returns>
        protected abstract T Abs(T a);

        #endregion

        #region Sqrt(a)

        /// <summary>
        /// A method to calculate the square root of the internal datatype.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <returns>The square root of a: Sqrt(a)</returns>
        protected abstract T Sqrt(T a);

        #endregion

        #endregion


        #region Distance(a, b)

        /// <summary>
        /// A method to calculate the distance between two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The distance between a and b.</returns>
        public T Distance(T a, T b)
        {
            return Abs(Sub(a, b));
        }

        #endregion

    }

}
