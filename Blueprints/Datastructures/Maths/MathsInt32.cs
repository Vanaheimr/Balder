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
    /// Defining math operations on Int32.
    /// </summary>
    public class MathsInt32 : IMaths<Int32>
    {

        #region Singelton

        #region Static constructor

        // Explicit static constructor to tell the compiler
        // not to mark type as 'beforefieldinit'!
        static MathsInt32()
        { }

        #endregion

        #region Instance

        private static readonly IMaths<Int32> _Instance = new MathsInt32();

        /// <summary>
        /// Return a singelton instance of this maths class.
        /// </summary>
        public static IMaths<Int32> Instance
        {
            get
            {
                return _Instance;
            }
        }

        #endregion

        #endregion


        #region Zero

        /// <summary>
        /// Return the zero value of this datatype.
        /// </summary>
        public Int32 Zero
        {
            get
            {
                return 0;
            }
        }

        #endregion


        #region Min(a, b)

        /// <summary>
        /// A method to get the minimum of two Int32s.
        /// </summary>
        /// <param name="a">A Int32.</param>
        /// <param name="b">A Int32.</param>
        /// <returns>The minimum of a and b: Min(a, b)</returns>
        public Int32 Min(Int32 a, Int32 b)
        {
            return Math.Min(a, b);
        }

        #endregion

        #region Max(a, b)

        /// <summary>
        /// A method to get the maximum of two Int32s.
        /// </summary>
        /// <param name="a">A Int32.</param>
        /// <param name="b">A Int32.</param>
        /// <returns>The maximum of a and b: Max(a, b)</returns>
        public Int32 Max(Int32 a, Int32 b)
        {
            return Math.Max(a, b);
        }

        #endregion


        #region Add(params Summands)

        /// <summary>
        /// A method to add Int32s.
        /// </summary>
        /// <param name="Summands">An array of Int32s.</param>
        /// <returns>The addition of all summands: a + b + ...</returns>
        public Int32 Add(params Int32[] Summands)
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
        /// A method to sub two Int32s.
        /// </summary>
        /// <param name="a">A Int32.</param>
        /// <param name="b">A Int32.</param>
        /// <returns>The subtraction of b from a: a - b</returns>
        public Int32 Sub(Int32 a, Int32 b)
        {
            return a - b;
        }

        #endregion

        #region Mul(params Multiplicators)

        /// <summary>
        /// A method to multiply Int32s.
        /// </summary>
        /// <param name="Multiplicators">An array of Int32s.</param>
        /// <returns>The multiplication of all multiplicators: a * b * ...</returns>
        public Int32 Mul(params Int32[] Multiplicators)
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
        /// A method to divide two Int32s.
        /// </summary>
        /// <param name="a">A Int32.</param>
        /// <param name="b">A Int32.</param>
        /// <returns>The division of a by b: a / b</returns>
        public Int32 Div(Int32 a, Int32 b)
        {
            return a / b;
        }

        #endregion

        #region Div2(a)

        /// <summary>
        /// A method to a Int32 by 2.
        /// </summary>
        /// <param name="a">A Int32.</param>
        /// <returns>The division of a by 2: a / 2</returns>
        public Int32 Div2(Int32 a)
        {
            return a / 2;
        }

        #endregion

        #region Pow(a, b)

        /// <summary>
        /// A method to calculate a Int32 raised to the specified power.
        /// </summary>
        /// <param name="a">A Int32.</param>
        /// <param name="b">A Int32.</param>
        /// <returns>The value a raised to the specified power of b: a^b</returns>
        public Int32 Pow(Int32 a, Int32 b)
        {
            return Convert.ToInt32(Math.Pow(a, b));
        }

        #endregion


        #region Abs(a)

        /// <summary>
        /// A method to calculate the absolute value of the Int32.
        /// </summary>
        /// <param name="a">An Int32.</param>
        /// <returns>The absolute value of a: Abs(a)</returns>
        public Int32 Abs(Int32 a)
        {
            return Math.Abs(a);
        }

        #endregion

        #region Sqrt(a)

        /// <summary>
        /// A method to calculate the square root of the Int32.
        /// </summary>
        /// <param name="a">A Int32.</param>
        /// <returns>The square root of a: Sqrt(a)</returns>
        public Int32 Sqrt(Int32 a)
        {
            return Convert.ToInt32(Math.Round(Math.Sqrt(a)));
        }

        #endregion


        #region Distance(a, b)

        /// <summary>
        /// A method to calculate the distance between two Int32s.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The distance between a and b.</returns>
        public Int32 Distance(Int32 a, Int32 b)
        {
            return Abs(Sub(a, b));
        }

        #endregion

    }

}
