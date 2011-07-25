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
    /// Defining math operations on Single.
    /// </summary>
    public class MathsSingle : IMaths<Single>
    {

        #region Singelton

        #region Static constructor

        // Explicit static constructor to tell the compiler
        // not to mark type as 'beforefieldinit'!
        static MathsSingle()
        { }

        #endregion

        #region Instance

        private static readonly IMaths<Single> _Instance = new MathsSingle();

        /// <summary>
        /// Return a singelton instance of this maths class.
        /// </summary>
        public static IMaths<Single> Instance
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
        public Single Zero
        {
            get
            {
                return 0;
            }
        }

        #endregion


        #region Min(params Values)

        /// <summary>
        /// A method to get the minimum of an array of Singles.
        /// </summary>
        /// <param name="Values">An array of Singles.</param>
        /// <returns>The minimum of all values: Min(a, b, ...)</returns>
        public Single Min(params Single[] Values)
        {

            if (Values == null)
                throw new ArgumentException("The given values must not be null!");

            if (Values.Length == 0)
                return 0;

            if (Values.Length == 1)
                return Values[0];

            var result = Values[0];

            for (var i = Values.Length - 1; i >= 1; i--)
                result = Math.Min(result, Values[i]);

            return result;

        }

        #endregion

        #region Max(params Values)

        /// <summary>
        /// A method to get the maximum of an array of Singles.
        /// </summary>
        /// <param name="Values">An array of Singles.</param>
        /// <returns>The maximum of all values: Min(a, b, ...)</returns>
        public Single Max(params Single[] Values)
        {

            if (Values == null)
                throw new ArgumentException("The given values must not be null!");

            if (Values.Length == 0)
                return 0;

            if (Values.Length == 1)
                return Values[0];

            var result = Values[0];

            for (var i = Values.Length - 1; i >= 1; i--)
                result = Math.Max(result, Values[i]);

            return result;

        }

        #endregion


        #region Add(params Summands)

        /// <summary>
        /// A method to add Singles.
        /// </summary>
        /// <param name="Summands">An array of Singles.</param>
        /// <returns>The addition of all summands: a + b + ...</returns>
        public Single Add(params Single[] Summands)
        {

            if (Summands == null)
                throw new ArgumentException("The given summands must not be null!");

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
        /// A method to sub two Singles.
        /// </summary>
        /// <param name="a">A Single.</param>
        /// <param name="b">A Single.</param>
        /// <returns>The subtraction of b from a: a - b</returns>
        public Single Sub(Single a, Single b)
        {
            return a - b;
        }

        #endregion

        #region Mul(params Multiplicators)

        /// <summary>
        /// A method to multiply Singles.
        /// </summary>
        /// <param name="Multiplicators">An array of Singles.</param>
        /// <returns>The multiplication of all multiplicators: a * b * ...</returns>
        public Single Mul(params Single[] Multiplicators)
        {

            if (Multiplicators == null)
                throw new ArgumentException("The given multiplicators must not be null!");

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

        #region Mul2(a)

        /// <summary>
        /// A method to multiply a Single by 2.
        /// </summary>
        /// <param name="a">A Single.</param>
        /// <returns>The multiplication of a by 2: 2*a</returns>
        public Single Mul2(Single a)
        {
            return 2 * a;
        }

        #endregion

        #region Div(a, b)

        /// <summary>
        /// A method to divide two Singles.
        /// </summary>
        /// <param name="a">A Single.</param>
        /// <param name="b">A Single.</param>
        /// <returns>The division of a by b: a / b</returns>
        public Single Div(Single a, Single b)
        {
            return a / b;
        }

        #endregion

        #region Div2(a)

        /// <summary>
        /// A method to divide a Single by 2.
        /// </summary>
        /// <param name="a">A Single.</param>
        /// <returns>The division of a by 2: a / 2</returns>
        public Single Div2(Single a)
        {
            return a / 2;
        }

        #endregion

        #region Pow(a, b)

        /// <summary>
        /// A method to calculate a Single raised to the specified power.
        /// </summary>
        /// <param name="a">A Single.</param>
        /// <param name="b">A Single.</param>
        /// <returns>The value a raised to the specified power of b: a^b</returns>
        public Single Pow(Single a, Single b)
        {
            return Convert.ToSingle(Math.Pow(a, b));
        }

        #endregion


        #region Inv(a)

        /// <summary>
        /// A method to calculate the inverse value of the given Single.
        /// </summary>
        /// <param name="a">A Single.</param>
        /// <returns>The inverse value of a: -a</returns>
        public Single Inv(Single a)
        {
            return -a;
        }

        #endregion

        #region Abs(a)

        /// <summary>
        /// A method to calculate the absolute value of the given Single.
        /// </summary>
        /// <param name="a">A Single.</param>
        /// <returns>The absolute value of a: |a|</returns>
        public Single Abs(Single a)
        {
            return Math.Abs(a);
        }

        #endregion

        #region Sqrt(a)

        /// <summary>
        /// A method to calculate the square root of the Single.
        /// </summary>
        /// <param name="a">A Single.</param>
        /// <returns>The square root of a: Sqrt(a)</returns>
        public Single Sqrt(Single a)
        {
            return Convert.ToSingle(Math.Sqrt(a));
        }

        #endregion


        #region Distance(a, b)

        /// <summary>
        /// A method to calculate the distance between two Singles.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The distance between a and b.</returns>
        public Single Distance(Single a, Single b)
        {
            return Abs(Sub(a, b));
        }

        #endregion

    }

}
