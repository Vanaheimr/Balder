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

namespace de.ahzf.Blueprints.Maths
{

    /// <summary>
    /// An interface defining maths operations on the given datatype.
    /// </summary>
    /// <typeparam name="T">The internal dataype.</typeparam>
    public interface IMaths<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Zero

        /// <summary>
        /// Return the zero value of this datatype.
        /// </summary>
        T Zero { get; }

        #endregion


        #region Min(params Values)

        /// <summary>
        /// A method to get the minimum of an array of internal datatypes.
        /// </summary>
        /// <param name="Values">An array of type T</param>
        /// <returns>The minimum of all values: Min(a, b, ...)</returns>
        T Min(params T[] Values);

        #endregion

        #region Max(params Values)

        /// <summary>
        /// A method to get the maximum of an array of internal datatypes.
        /// </summary>
        /// <param name="Values">An array of type T</param>
        /// <returns>The maximum of all values: Max(a, b, ...)</returns>
        T Max(params T[] Values);

        #endregion


        #region Add(params Summands)

        /// <summary>
        /// A method to add internal datatypes.
        /// </summary>
        /// <param name="Summands">An array of Doubles.</param>
        /// <returns>The addition of all summands: a + b + ...</returns>
        T Add(params T[] Summands);

        #endregion

        #region Sub(a, b)

        /// <summary>
        /// A method to sub two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The subtraction of b from a: a - b</returns>
        T Sub(T a, T b);

        #endregion

        #region Mul(params Multiplicators)

        /// <summary>
        /// A method to multiply internal datatypes.
        /// </summary>
        /// <param name="Multiplicators">An array of type T.</param>
        /// <returns>The multiplication of all multiplicators: a * b * ...</returns>
        T Mul(params T[] Multiplicators);

        #endregion

        #region Div(a, b)

        /// <summary>
        /// A method to divide two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The division of a by b: a / b</returns>
        T Div(T a, T b);

        #endregion

        #region Div2(a)

        /// <summary>
        /// A method to divide the internal datatype by 2.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <returns>The division of a by 2: a / 2</returns>
        T Div2(T a);

        #endregion

        #region Pow(a, b)

        /// <summary>
        /// A method to calculate an internal datatype raised to the specified power.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The value a raised to the specified power of b: a^b</returns>
        T Pow(T a, T b);

        #endregion


        #region Inv(a)

        /// <summary>
        /// A method to calculate the inverse value of the internal datatype.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <returns>The inverse value of a: -a</returns>
        T Inv(T a);

        #endregion

        #region Abs(a)

        /// <summary>
        /// A method to calculate the absolute value of the internal datatype.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <returns>The absolute value of a: |a|</returns>
        T Abs(T a);

        #endregion

        #region Sqrt(a)

        /// <summary>
        /// A method to calculate the square root of the internal datatype.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <returns>The square root of a: Sqrt(a)</returns>
        T Sqrt(T a);

        #endregion


        #region Distance(a, b)

        /// <summary>
        /// A method to calculate the distance between two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The distance between a and b.</returns>
        T Distance(T a, T b);

        #endregion

    }

}
