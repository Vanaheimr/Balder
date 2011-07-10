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
    /// A pixel of type Int32.
    /// </summary>
    public class PixelInt32 : Pixel<Int32>
    {

        #region Constructor(s)

        #region PixelInt32(x, y)

        /// <summary>
        /// Create a pixel of type Int32.
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public PixelInt32(Int32 x, Int32 y)
            : base(x, y)
        { }

        #endregion

        #endregion


        #region Abstract Math Operations

        #region Add(a, b)

        /// <summary>
        /// A method to add two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The addition of a and b: a + b</returns>
        protected override Int32 Add(Int32 a, Int32 b)
        {
            return a + b;
        }

        #endregion

        #region Sub(a, b)

        /// <summary>
        /// A method to sub two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The subtraction of b from a: a - b</returns>
        protected override Int32 Sub(Int32 a, Int32 b)
        {
            return a - b;
        }

        #endregion

        #region Mul(a, b)

        /// <summary>
        /// A method to multiply two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The multiplication of a and b: a * b</returns>
        protected override Int32 Mul(Int32 a, Int32 b)
        {
            return a * b;
        }

        #endregion

        #region Div(a, b)

        /// <summary>
        /// A method to divide two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The division of a by b: a / b</returns>
        protected override Int32 Div(Int32 a, Int32 b)
        {
            return a / b;
        }

        #endregion

        #endregion


        #region Operator overloadings

        #region Operator == (Pixel1, Pixel2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Pixel1">A Pixel&lt;Int32&gt;.</param>
        /// <param name="Pixel2">Another Pixel&lt;Int32&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (PixelInt32 Pixel1, PixelInt32 Pixel2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(Pixel1, Pixel2))
                return true;

            // If one is null, but not both, return false.
            if (((Object)Pixel1 == null) || ((Object)Pixel2 == null))
                return false;

            return Pixel1.Equals(Pixel2);

        }

        #endregion

        #region Operator != (Pixel1, Pixel2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Pixel1">A Pixel&lt;Int32&gt;.</param>
        /// <param name="Pixel2">Another Pixel&lt;Int32&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (PixelInt32 Pixel1, PixelInt32 Pixel2)
        {
            return !(Pixel1.Equals(Pixel2));
        }

        #endregion

        #endregion

        #region IEquatable Members

        #region Equals(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>true|false</returns>
        public override Boolean Equals(Object Object)
        {
            return base.Equals(Object);
        }

        #endregion

        #endregion

        #region GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override Int32 GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

    }

}
