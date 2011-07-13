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

    #region PixelValueSelector<T, TValue>(PixelValuePair)

    /// <summary>
    /// A delegate selecting which PixelValuePairs to return.
    /// </summary>
    /// <typeparam name="T">The internal datatype of the pixel.</typeparam>
    /// <typeparam name="TValue">The type of the stored values.</typeparam>
    /// <param name="PixelValuePair">A PixelValuePair of type T.</param>
    /// <returns>True if the pixel is selected; False otherwise.</returns>
    public delegate Boolean PixelValueSelector<T, TValue>(IPixelValuePair<T, TValue> PixelValuePair)
        where T : IEquatable<T>, IComparable<T>, IComparable;

    #endregion

    #region PixelValuePair<TKey, TValue>

    /// <summary>
    /// A PixelValuePair.
    /// </summary>
    /// <typeparam name="TKey">The internal datatype of the pixel.</typeparam>
    /// <typeparam name="TValue">The type of the stored values.</typeparam>
    public class PixelValuePair<TKey, TValue> : Pixel<TKey>, IPixelValuePair<TKey, TValue>
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
    {

        #region Properties

        #region Value

        /// <summary>
        /// The value.
        /// </summary>
        public TValue Value { get; private set; }

        #endregion

        #endregion

        #region Constructor(s)

        #region PixelValuePair(x, y, value)

        /// <summary>
        /// Create a PixelValuePair.
        /// </summary>
        /// <param name="x">The X-coordinate.</param>
        /// <param name="y">The Y-coordinate.</param>
        /// <param name="value">The value.</param>
        public PixelValuePair(TKey x, TKey y, TValue value)
            : base(x, y)
        {
            this.Value = value;
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
        public static Boolean operator == (PixelValuePair<TKey, TValue> PixelValuePair1, PixelValuePair<TKey, TValue> PixelValuePair2)
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
        /// <param name="Pixel1">A PixelValuePair.</param>
        /// <param name="Pixel2">Another PixelValuePair.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (PixelValuePair<TKey, TValue> PixelValuePair1, PixelValuePair<TKey, TValue> PixelValuePair2)
        {
            return !(PixelValuePair1 == PixelValuePair2);
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

            // Check if the given object is an PixelValuePair<T>.
            var PixelValuePair = (PixelValuePair<TKey, TValue>) Object;
            if ((Object) PixelValuePair == null)
                return false;

            return this.Equals(PixelValuePair);

        }

        #endregion

        #region Equals(IPixelValuePair)

        /// <summary>
        /// Compares two pixels for equality.
        /// </summary>
        /// <param name="IPixelValuePair">A pixel to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IPixelValuePair<TKey, TValue> IPixelValuePair)
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
            return String.Format("X={0}, Y={1}",
                                 X.ToString(),
                                 Y.ToString());
        }

        #endregion


        public int CompareTo(IPixelValuePair<TKey, TValue> other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

    }

    #endregion

}
