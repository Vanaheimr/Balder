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
    /// A pixel of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the pixel.</typeparam>
    public abstract class Pixel<T> : IEquatable<Pixel<T>>, IComparable<Pixel<T>>, IComparable
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Data

        /// <summary>
        /// X
        /// </summary>
        public T X { get; private set; }

        /// <summary>
        /// Y
        /// </summary>
        public T Y { get; private set; }

        #endregion

        #region Constructor(s)

        #region Pixel(x, y)

        /// <summary>
        /// Create a pixel of type T.
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public Pixel(T x, T y)
        {
            this.X = x;
            this.Y = y;
        }

        #endregion

        #endregion


        #region Abstract Math Operations

        /// <summary>
        /// A method to add two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The addition of a and b: a + b</returns>
        protected abstract T Add(T a, T b);

        /// <summary>
        /// A method to sub two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The subtraction of b from a: a - b</returns>
        protected abstract T Sub(T a, T b);

        /// <summary>
        /// A method to multiply two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The multiplication of a and b: a * b</returns>
        protected abstract T Mul(T a, T b);

        /// <summary>
        /// A method to divide two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The division of a by b: a / b</returns>
        protected abstract T Div(T a, T b);

        #endregion


        #region Operator overloadings

        #region Operator == (Pixel1, Pixel2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Pixel1">A Pixel&lt;T&gt;.</param>
        /// <param name="Pixel2">Another Pixel&lt;T&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Pixel<T> Pixel1, Pixel<T> Pixel2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(Pixel1, Pixel2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) Pixel1 == null) || ((Object) Pixel2 == null))
                return false;

            return Pixel1.Equals(Pixel2);

        }

        #endregion

        #region Operator != (Pixel1, Pixel2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Pixel1">A Pixel&lt;T&gt;.</param>
        /// <param name="Pixel2">Another Pixel&lt;T&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Pixel<T> Pixel1, Pixel<T> Pixel2)
        {
            return !(Pixel1.Equals(Pixel2));
        }

        #endregion

        #endregion


        #region IComparable Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given object must not be null!");

            // Check if the given object is an Pixel<T>.
            var PixelT = (Pixel<T>) Object;
            if ((Object) PixelT == null)
                throw new ArgumentException("The given object is not a Pixel<T>!");

            return CompareTo(PixelT);

        }

        #endregion

        #region CompareTo(PixelT)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PixelT">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(Pixel<T> PixelT)
        {

            if ((Object) PixelT == null)
                throw new ArgumentNullException("The given Pixel<T> must not be null!");

            return 0;

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

            if (Object == null)
                return false;

            // Check if the given object is an Pixel<T>.
            var PixelT = (Pixel<T>) Object;
            if ((Object) PixelT == null)
                return false;

            return this.Equals(PixelT);

        }

        #endregion

        #region Equals(PixelT)

        /// <summary>
        /// Compares two pixels for equality.
        /// </summary>
        /// <param name="PixelT">A pixel to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(Pixel<T> PixelT)
        {

            if ((Object) PixelT == null)
                return false;

            return X.Equals(PixelT.X) &&
                   Y.Equals(PixelT.Y);

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
            return X.GetHashCode() ^ 1 + Y.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Return a string represtentation of this object.
        /// </summary>
        public override String ToString()
        {
            return String.Format("{{X={0}, Y={1}}}", X.ToString(), Y.ToString());
        }

        #endregion

    }

}
