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
    /// Defining math operations on Double.
    /// </summary>
    public sealed class MathDouble : IMath<Double>
    {

        #region Singelton

        #region Static constructor

        // Explicit static constructor to tell the compiler
        // not to mark type as 'beforefieldinit'!
        static MathDouble()
        { }

        #endregion

        #region Instance

        private static readonly IMath<Double> _Instance = new MathDouble();

        /// <summary>
        /// Return a singelton instance of this math class.
        /// </summary>
        public static IMath<Double> Instance
        {
            get
            {
                return _Instance;
            }
        }

        #endregion

        #endregion


        #region Min(a, b)

        /// <summary>
        /// A method to get the minimum of two Doubles.
        /// </summary>
        /// <param name="a">A Double.</param>
        /// <param name="b">A Double.</param>
        /// <returns>The minimum of a and b: Min(a, b)</returns>
        public Double Min(Double a, Double b)
        {
            return Math.Min(a, b);
        }

        #endregion

        #region Max(a, b)

        /// <summary>
        /// A method to get the maximum of two Doubles.
        /// </summary>
        /// <param name="a">A Double.</param>
        /// <param name="b">A Double.</param>
        /// <returns>The maximum of a and b: Max(a, b)</returns>
        public Double Max(Double a, Double b)
        {
            return Math.Max(a, b);
        }

        #endregion


        #region Add(params Summands)

        /// <summary>
        /// A method to add Doubles.
        /// </summary>
        /// <param name="Summands">An array of Doubles.</param>
        /// <returns>The addition of all summands: a + b + ...</returns>
        public Double Add(params Double[] Summands)
        {
            
            if (Summands.Length == 0)
                return 0;

            if (Summands.Length == 1)
                return Summands[0];

            var result = Summands[0];

            for (var i = Summands.Length - 1; i >= 1; i--)
                result += Summands[i];

            return result;

        }

        #endregion

        #region Sub(a, b)

        /// <summary>
        /// A method to sub two Doubles.
        /// </summary>
        /// <param name="a">A Double.</param>
        /// <param name="b">A Double.</param>
        /// <returns>The subtraction of b from a: a - b</returns>
        public Double Sub(Double a, Double b)
        {
            return a - b;
        }

        #endregion

        #region Mul(params Multiplicators)

        /// <summary>
        /// A method to multiply Doubles.
        /// </summary>
        /// <param name="Multiplicators">An array of Doubles.</param>
        /// <returns>The multiplication of all multiplicators: a * b * ...</returns>
        public Double Mul(params Double[] Multiplicators)
        {

            if (Multiplicators.Length == 0)
                return 0;

            if (Multiplicators.Length == 1)
                return Multiplicators[0];

            var result = Multiplicators[0];

            for (var i = Multiplicators.Length - 1; i >= 1; i--)
                result *= Multiplicators[i];

            return result;

        }

        #endregion

        #region Div(a, b)

        /// <summary>
        /// A method to divide two Doubles.
        /// </summary>
        /// <param name="a">A Double.</param>
        /// <param name="b">A Double.</param>
        /// <returns>The division of a by b: a / b</returns>
        public Double Div(Double a, Double b)
        {
            return a / b;
        }

        #endregion

        #region Div2(a)

        /// <summary>
        /// A method to a Double by 2.
        /// </summary>
        /// <param name="a">A Double.</param>
        /// <returns>The division of a by 2: a / 2</returns>
        public Double Div2(Double a)
        {
            return a / 2;
        }

        #endregion

        #region Pow(a, b)

        /// <summary>
        /// A method to calculate a Double raised to the specified power.
        /// </summary>
        /// <param name="a">A Double.</param>
        /// <param name="b">A Double.</param>
        /// <returns>The value a raised to the specified power of b: a^b</returns>
        public Double Pow(Double a, Double b)
        {
            return Math.Pow(a, b);
        }

        #endregion


        #region Abs(a)

        /// <summary>
        /// A method to calculate the absolute value of the Double.
        /// </summary>
        /// <param name="a">An Double.</param>
        /// <returns>The absolute value of a: Abs(a)</returns>
        public Double Abs(Double a)
        {
            return Math.Abs(a);
        }

        #endregion

        #region Sqrt(a)

        /// <summary>
        /// A method to calculate the square root of the Double.
        /// </summary>
        /// <param name="a">A Double.</param>
        /// <returns>The square root of a: Sqrt(a)</returns>
        public Double Sqrt(Double a)
        {
            return Math.Sqrt(a);
        }

        #endregion


        #region Distance(a, b)

        /// <summary>
        /// A method to calculate the distance between two Doubles.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The distance between a and b.</returns>
        public Double Distance(Double a, Double b)
        {
            return Abs(Sub(a, b));
        }

        #endregion

    }

}
