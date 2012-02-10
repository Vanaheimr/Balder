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

#endregion

namespace de.ahzf.Blueprints
{

    #region PixelValuePair<T, TValue>

    /// <summary>
    /// A pixel of type T together with a value of type TValue.
    /// </summary>
    /// <typeparam name="T">The internal type of the pixel.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class PixelValuePair<T, TValue> : Pixel<T>, IPixelValuePair<T, TValue>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Properties

        #region Value

        /// <summary>
        /// The Value.
        /// </summary>
        public TValue Value { get; set; }

        #endregion

        #endregion

        #region Constructor(s)

        #region PixelValuePair(X, Y, Value)

        /// <summary>
        /// Create a pixel of type T together with a value of type TValue.
        /// </summary>
        /// <param name="X">The X-coordinate.</param>
        /// <param name="Y">The Y-coordinate.</param>
        /// <param name="Value">The value.</param>
        public PixelValuePair(T X, T Y, TValue Value)
            : base(X, Y)
        {
            this.Value = Value;
        }

        #endregion

        #endregion


        #region Operator overloadings

        #region Operator == (PixelValuePair1, PixelValuePair2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PixelValuePair1">A PixelValuePair.</param>
        /// <param name="PixelValuePair2">Another PixelValuePair.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (PixelValuePair<T, TValue> PixelValuePair1, PixelValuePair<T, TValue> PixelValuePair2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(PixelValuePair1, PixelValuePair2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) PixelValuePair1 == null) || ((Object) PixelValuePair2 == null))
                return false;

            return PixelValuePair1.Equals(PixelValuePair2);

        }

        #endregion

        #region Operator != (PixelValuePair1, PixelValuePair2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PixelValuePair1">A PixelValuePair.</param>
        /// <param name="PixelValuePair2">Another PixelValuePair.</param>
        /// <returns>true|false</returns>
        public static Boolean operator !=  (PixelValuePair<T, TValue> PixelValuePair1, PixelValuePair<T, TValue> PixelValuePair2)
        {
            return !(PixelValuePair1 == PixelValuePair2);
        }

        #endregion

        #endregion

        #region IComparable Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public override Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given object must not be null!");

            // Check if the given object is an IPixelValuePair<T, TValue>.
            var IPixelValuePair = (IPixelValuePair<T, TValue>) Object;
            if ((Object) IPixelValuePair != null)
                return IPixelValuePair.CompareTo(this);

            // Check if the given object is an IPixel<T>.
            var IPixel = (IPixel<T>) Object;
            if ((Object) IPixel != null)
                return IPixel.CompareTo(this);

            throw new ArgumentException("The given object is neither a PixelValuePair<T, TValue> nor a Pixel<T>!");

        }

        #endregion

        #region CompareTo(IPixelT)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="IPixelValuePair">An object to compare with.</param>
        public Int32 CompareTo(IPixelValuePair<T, TValue> IPixelValuePair)
        {
            return base.CompareTo(IPixelValuePair);
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

            // Check if the given object is an IPixelValuePair<T, TValue>.
            var IPixelValuePair = (IPixelValuePair<T, TValue>) Object;
            if ((Object) IPixelValuePair != null)
                return this.Equals(IPixelValuePair);

            // Check if the given object is an IPixel<T>.
            var IPixel = (IPixel<T>) Object;
            if ((Object) IPixel != null)
                return this.Equals(IPixel);

            return false;

        }

        #endregion

        #region Equals(IPixelValuePair)

        /// <summary>
        /// Compares two PixelValuePairs for equality.
        /// </summary>
        /// <param name="IPixelValuePair">A PixelValuePair to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IPixelValuePair<T, TValue> IPixelValuePair)
        {

            if ((Object) IPixelValuePair == null)
                return false;

            return X.Equals(IPixelValuePair.X) &&
                   Y.Equals(IPixelValuePair.Y);

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
            return String.Format("PixelValuePair: X={0}, Y={1}, Value={2}",
                                 X.ToString(),
                                 Y.ToString(),
                                 Value.ToString());
        }

        #endregion

    }

    #endregion

}
