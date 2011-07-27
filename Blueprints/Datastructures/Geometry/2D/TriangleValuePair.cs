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
using System.Collections.Generic;

using de.ahzf.Blueprints.Maths;

#endregion

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// A triangle of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the triangle.</typeparam>
    public class TriangleValuePair<T, TValue> : Triangle<T>, ITriangleValuePair<T, TValue>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Properties

        #region Value

        /// <summary>
        /// The Value.
        /// </summary>
        public TValue Value { get; private set; }

        #endregion

        #endregion

        #region Constructor(s)

        #region TriangleValuePair(Pixel1, Pixel2, Pixel3)

        /// <summary>
        /// Create a triangle of type T.
        /// </summary>
        /// <param name="Pixel1">The first pixel of the triangle.</param>
        /// <param name="Pixel2">The second pixel of the triangle.</param>
        /// <param name="Pixel3">The third pixel of the triangle.</param>
        /// <param name="Value">The value.</param>
        public TriangleValuePair(IPixel<T> Pixel1, IPixel<T> Pixel2, IPixel<T> Pixel3, TValue Value)
            : base(Pixel1, Pixel2, Pixel3)
        {
            this.Value = Value;
        }

        #endregion

        #endregion


        #region Operator overloadings

        #region Operator == (TriangleValuePair1, TriangleValuePair2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="TriangleValuePair1">A Triangle&lt;T&gt;.</param>
        /// <param name="TriangleValuePair2">Another Triangle&lt;T&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (TriangleValuePair<T, TValue> TriangleValuePair1, TriangleValuePair<T, TValue> TriangleValuePair2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(TriangleValuePair1, TriangleValuePair2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) TriangleValuePair1 == null) || ((Object) TriangleValuePair2 == null))
                return false;

            return TriangleValuePair1.Equals(TriangleValuePair2);

        }

        #endregion

        #region Operator != (TriangleValuePair1, TriangleValuePair2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="TriangleValuePair1">A Triangle&lt;T&gt;.</param>
        /// <param name="TriangleValuePair2">Another Triangle&lt;T&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (TriangleValuePair<T, TValue> TriangleValuePair1, TriangleValuePair<T, TValue> TriangleValuePair2)
        {
            return !(TriangleValuePair1 == TriangleValuePair2);
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

            // Check if the given object is an ITriangleValuePair<T, TValue>.
            var ITriangleValuePair = (ITriangleValuePair<T, TValue>)Object;
            if ((Object) ITriangleValuePair != null)
                return ITriangleValuePair.CompareTo(this);

            // Check if the given object is an ITriangle<T>.
            var ITriangle = (ITriangle<T>) Object;
            if ((Object) ITriangle != null)
                return ITriangle.CompareTo(this);

            throw new ArgumentException("The given object is neither a ITriangleValuePair<T, TValue> nor a ITriangle<T>!");

        }

        #endregion

        #region CompareTo(ITriangleValuePair)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="IPixelValuePair">An object to compare with.</param>
        public Int32 CompareTo(ITriangleValuePair<T, TValue> ITriangleValuePair)
        {
            return base.CompareTo(ITriangleValuePair);
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

            // Check if the given object is an ITriangleValuePair<T, TValue>.
            var TriangleValuePair = (ITriangleValuePair<T, TValue>)Object;
            if ((Object) TriangleValuePair == null)
                return false;

            return this.Equals(TriangleValuePair);

        }

        #endregion

        #region Equals(ITriangleValuePair)

        /// <summary>
        /// Compares two triangles for equality.
        /// </summary>
        /// <param name="ITriangleValuePair">A triangle to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(ITriangleValuePair<T, TValue> ITriangleValuePair)
        {

            if ((Object) ITriangleValuePair == null)
                return false;

            return this.P1.Equals(ITriangleValuePair.P1) &&
                   this.P2.Equals(ITriangleValuePair.P2) &&
                   this.P3.Equals(ITriangleValuePair.P3);

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
            return P1.GetHashCode() ^ 1 + P2.GetHashCode() ^ 2 + P3.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Return a string represtentation of this object.
        /// </summary>
        public override String ToString()
        {
            return String.Format("Triangle: Pixel1={0}, Pixel2={1}, Pixel3={2}, Value={3}",
                                 P1.ToString(),
                                 P2.ToString(),
                                 P3.ToString(),
                                 Value.ToString());
        }

        #endregion

    }

}
