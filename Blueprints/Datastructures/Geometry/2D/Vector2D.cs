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

using de.ahzf.Blueprints.Maths;

#endregion

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// A 2-dimensional vector of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the vector.</typeparam>
    public class Vector2D<T> : IVector2D<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Data

        /// <summary>
        /// Mathoperation helpers.
        /// </summary>
        protected readonly IMaths<T> Math;

        #endregion

        #region Properties

        #region X

        /// <summary>
        /// The x-component of the vector.
        /// </summary>
        public T X { get; private set; }

        #endregion

        #region Y

        /// <summary>
        /// The y-component of the vector.
        /// </summary>
        public T Y { get; private set; }

        #endregion

        #region Length

        /// <summary>
        /// The length of the vector.
        /// </summary>
        public T Length { get; private set; }

        #endregion


        #region NormVector

        /// <summary>
        /// Return a normalized vector.
        /// </summary>
        public IVector2D<T> NormVector
        {
            get
            {
                return new Vector2D<T>(Math.Zero,
                                       Math.Zero,
                                       Math.Div(X, Length),
                                       Math.Div(Y, Length));
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region Vector(X, Y)

        /// <summary>
        /// Create a 2-dimensional vector of type T.
        /// </summary>
        /// <param name="X">The x-component of the vector.</param>
        /// <param name="Y">The y-component of the vector.</param>
        public Vector2D(T X, T Y)
        {

            #region Initial Checks

            if (X == null)
                throw new ArgumentNullException("The given x-component must not be null!");

            if (Y == null)
                throw new ArgumentNullException("The given y-component must not be null!");

            #endregion

            this.Math   = MathsFactory<T>.Instance;

            this.X      = X;
            this.Y      = Y;
            this.Length = new Pixel<T>(Math.Zero, Math.Zero).DistanceTo(X, Y);

        }

        #endregion

        #region Vector(X1, Y1, X2, Y2)

        /// <summary>
        /// Create a 2-dimensional vector of type T.
        /// </summary>
        /// <param name="X1">The first x-coordinate of the vector.</param>
        /// <param name="Y1">The first y-coordinate of the vector.</param>
        /// <param name="X2">The second x-coordinate of the vector.</param>
        /// <param name="Y2">The second y-coordinate of the vector.</param>
        public Vector2D(T X1, T Y1, T X2, T Y2)
        {

            #region Initial Checks

            if (X1   == null)
                throw new ArgumentNullException("The given left-coordinate must not be null!");

            if (Y1    == null)
                throw new ArgumentNullException("The given top-coordinate must not be null!");

            if (X2  == null)
                throw new ArgumentNullException("The given right-coordinate must not be null!");

            if (Y2 == null)
                throw new ArgumentNullException("The given bottom-coordinate must not be null!");

            #endregion

            this.Math   = MathsFactory<T>.Instance;
            
            this.X      = Math.Sub(X1, X2);
            this.Y      = Math.Sub(Y1, Y2);
            this.Length = new Pixel<T>(X1, Y1).DistanceTo(X2, Y2);

        }

        #endregion

        #region Vector(Pixel1, Pixel2)

        /// <summary>
        /// Create a 2-dimensional vector of type T.
        /// </summary>
        /// <param name="Pixel1">A pixel of type T.</param>
        /// <param name="Pixel2">A pixel of type T.</param>
        public Vector2D(IPixel<T> Pixel1, IPixel<T> Pixel2)
        {

            #region Initial Checks

            if (Pixel1 == null)
                throw new ArgumentNullException("The first pixel must not be null!");

            if (Pixel2 == null)
                throw new ArgumentNullException("The second pixel must not be null!");

            #endregion

            this.Math   = MathsFactory<T>.Instance;

            this.X      = Math.Sub(Pixel1.X, Pixel2.X);
            this.Y      = Math.Sub(Pixel1.Y, Pixel2.Y);
            this.Length = Pixel1.DistanceTo(Pixel2);

        }

        #endregion

        #region Vector(Vector1, Vector2)

        /// <summary>
        /// Create a 2-dimensional vector of type T.
        /// </summary>
        /// <param name="Vector1">A vector of type T.</param>
        /// <param name="Vector2">A vector of type T.</param>
        public Vector2D(IVector2D<T> Vector1, IVector2D<T> Vector2)
        {

            #region Initial Checks

            if (Vector1 == null)
                throw new ArgumentNullException("The first vector must not be null!");

            if (Vector2 == null)
                throw new ArgumentNullException("The second vector must not be null!");

            #endregion

            this.Math   = MathsFactory<T>.Instance;

            this.X      = Math.Sub(Vector1.X, Vector2.X);
            this.Y      = Math.Sub(Vector1.Y, Vector2.Y);
            this.Length = Vector1.DistanceTo(Vector2);

        }

        #endregion

        #endregion


        #region IMaths Members

        #region Zero

        /// <summary>
        /// Return the zero value of this datatype.
        /// </summary>
        public IVector2D<T> Zero
        {
            get
            {
                return new Vector2D<T>(Math.Zero, Math.Zero);
            }
        }

        #endregion

        #region NegativeInfinity

        /// <summary>
        /// Return the negative infinity of this datatype.
        /// </summary>
        public IVector2D<T> NegativeInfinity
        {
            get
            {
                return new Vector2D<T>(Math.NegativeInfinity, Math.NegativeInfinity);
            }
        }

        #endregion

        #region PositiveInfinity

        /// <summary>
        /// Return the positive infinity of this datatype.
        /// </summary>
        public IVector2D<T> PositiveInfinity
        {
            get
            {
                return new Vector2D<T>(Math.PositiveInfinity, Math.PositiveInfinity);
            }
        }

        #endregion


        #region Min(params Values)

        /// <summary>
        /// A method to get the minimum of an array of Doubles.
        /// </summary>
        /// <param name="Values">An array of Doubles.</param>
        /// <returns>The minimum of all values: Min(a, b, ...)</returns>
        public IVector2D<T> Min(params IVector2D<T>[] Values)
        {

            if (Values == null || Values.Length == 0)
                throw new ArgumentException("The given values must not be null or zero!");

            if (Values.Length == 1)
                return Values[0];

            var _X = Values[0].X;
            var _Y = Values[0].Y;

            for (var i = Values.Length - 1; i >= 1; i--)
            {
                _X = Math.Min(_X, Values[i].X);
                _Y = Math.Min(_Y, Values[i].Y);
            }

            return new Vector2D<T>(Math.Zero, Math.Zero, _X, _Y);

        }

        #endregion

        #region Max(params Values)

        /// <summary>
        /// A method to get the maximum of an array of Doubles.
        /// </summary>
        /// <param name="Values">An array of Doubles.</param>
        /// <returns>The maximum of all values: Min(a, b, ...)</returns>
        public IVector2D<T> Max(params IVector2D<T>[] Values)
        {

            if (Values == null || Values.Length == 0)
                throw new ArgumentException("The given values must not be null or zero!");

            if (Values.Length == 1)
                return Values[0];

            var _X = Values[0].X;
            var _Y = Values[0].Y;

            for (var i = Values.Length - 1; i >= 1; i--)
            {
                _X = Math.Max(_X, Values[i].X);
                _Y = Math.Max(_Y, Values[i].Y);
            }

            return new Vector2D<T>(Math.Zero, Math.Zero, _X, _Y);

        }

        #endregion


        #region Add(params Summands)

        /// <summary>
        /// A method to add vectors.
        /// </summary>
        /// <param name="Summands">An array of vectors.</param>
        /// <returns>The addition of all summands: v1 + v2 + ...</returns>
        public IVector2D<T> Add(params IVector2D<T>[] Summands)
        {

            if (Summands == null || Summands.Length == 0)
                throw new ArgumentException("The given summands must not be null!");

            if (Summands.Length == 1)
                return Summands[0];

            var _X = Summands[0].X;
            var _Y = Summands[0].Y;

            for (var i = Summands.Length - 1; i >= 1; i--)
            {
                _X = Math.Add(_X, Summands[i].X);
                _Y = Math.Add(_Y, Summands[i].Y);
            }

            return new Vector2D<T>(Math.Zero, Math.Zero, _X, _Y);

        }

        #endregion

        #region Sub(v1, v2)

        /// <summary>
        /// A method to sub two vectors.
        /// </summary>
        /// <param name="v1">A vector.</param>
        /// <param name="v2">A vector.</param>
        /// <returns>The subtraction of v2 from v1: v1 - v2</returns>
        public IVector2D<T> Sub(IVector2D<T> v1, IVector2D<T> v2)
        {
            return new Vector2D<T>(Math.Sub(v1.X,   v2.X),
                                   Math.Sub(v1.Y, v2.Y));
        }

        #endregion

        #region Mul(params Multiplicators)

        /// <summary>
        /// A method to multiply vectors.
        /// </summary>
        /// <param name="Multiplicators">An array of vectors.</param>
        /// <returns>The multiplication of all multiplicators: v1 * v2 * ...</returns>
        public IVector2D<T> Mul(params IVector2D<T>[] Multiplicators)
        {

            if (Multiplicators == null || Multiplicators.Length == 0)
                throw new ArgumentException("The given multiplicators must not be null!");

            if (Multiplicators.Length == 1)
                return Multiplicators[0];

            var _X = Multiplicators[0].X;
            var _Y = Multiplicators[0].Y;

            for (var i = Multiplicators.Length - 1; i >= 1; i--)
            {
                _X = Math.Mul(_X, Multiplicators[i].X);
                _Y = Math.Mul(_Y, Multiplicators[i].Y);
            }

            return new Vector2D<T>(Math.Zero, Math.Zero, _X, _Y);

        }

        #endregion

        #region Mul2(a)

        /// <summary>
        /// A method to multiply a vector by 2.
        /// </summary>
        /// <param name="v">A vector.</param>
        /// <returns>The multiplication of v by 2: (2*x, 2*y)</returns>
        public IVector2D<T> Mul2(IVector2D<T> v)
        {
            return new Vector2D<T>(Math.Mul2(v.X),
                                   Math.Mul2(v.Y));
        }

        #endregion

        #region Div(v1, v2)

        /// <summary>
        /// A method to divide two vectors.
        /// </summary>
        /// <param name="v1">A vector.</param>
        /// <param name="v2">A vector.</param>
        /// <returns>The division of v1 by v2: v1 / v2</returns>
        public IVector2D<T> Div(IVector2D<T> v1, IVector2D<T> v2)
        {
            return new Vector2D<T>(Math.Div(v1.X,   v2.X),
                                   Math.Div(v1.Y, v2.Y));
        }

        #endregion

        #region Div2(v)

        /// <summary>
        /// A method to divide a vector by 2.
        /// </summary>
        /// <param name="v">A vector.</param>
        /// <returns>The division of v by 2: v / 2</returns>
        public IVector2D<T> Div2(IVector2D<T> v)
        {
            return new Vector2D<T>(Math.Div2(v.X),
                                   Math.Div2(v.Y));
        }

        #endregion

        #region Pow(a, b)

        /// <summary>
        /// A method to calculate a Double raised to the specified power.
        /// </summary>
        /// <param name="v1">A vector.</param>
        /// <param name="v2">A vector.</param>
        /// <returns>The values of v1 raised to the specified power of v2: v1^v2</returns>
        public IVector2D<T> Pow(IVector2D<T> v1, IVector2D<T> v2)
        {
            return new Vector2D<T>(Math.Pow(v1.X,   v2.X),
                                   Math.Pow(v1.Y, v2.Y));
        }

        #endregion


        #region Inv(v)

        /// <summary>
        /// A method to calculate the inverse value of the given vector.
        /// </summary>
        /// <param name="v">A vector.</param>
        /// <returns>The inverse value of v: -v</returns>
        public IVector2D<T> Inv(IVector2D<T> v)
        {
            return new Vector2D<T>(Math.Inv(v.X),
                                   Math.Inv(v.Y));
        }

        #endregion

        #region Abs(v)

        /// <summary>
        /// A method to calculate the absolute value of the given vector.
        /// </summary>
        /// <param name="v">A vector.</param>
        /// <returns>The absolute value of v: (|a| |b|)</returns>
        public IVector2D<T> Abs(IVector2D<T> v)
        {
            return new Vector2D<T>(Math.Abs(v.X),
                                   Math.Abs(v.Y));
        }

        #endregion

        #region Sqrt(v)

        /// <summary>
        /// A method to calculate the square root of the vector.
        /// </summary>
        /// <param name="v">A vector.</param>
        /// <returns>The square root of v: Sqrt(v)</returns>
        public IVector2D<T> Sqrt(IVector2D<T> v)
        {
            return new Vector2D<T>(Math.Sqrt(v.X),
                                   Math.Sqrt(v.Y));
        }

        #endregion


        #region Distance(a, b)

        /// <summary>
        /// A method to calculate the distance between two vectors.
        /// </summary>
        /// <param name="v1">A vector.</param>
        /// <param name="v2">A vector.</param>
        /// <returns>The distance between v1 and v2.</returns>
        public IVector2D<T> Distance(IVector2D<T> v1, IVector2D<T> v2)
        {
            return new Vector2D<T>(Math.Abs(Math.Sub(v1.X,   v2.X)),
                                   Math.Abs(Math.Sub(v1.Y, v2.Y)));
        }

        #endregion

        #endregion


        #region DistanceTo(x, y)

        /// <summary>
        /// A method to calculate the distance between this
        /// vector and the given coordinates of type T.
        /// </summary>
        /// <param name="x">A x-coordinate of type T</param>
        /// <param name="y">A y-coordinate of type T</param>
        /// <returns>The distance between this vector and the given coordinates.</returns>
        public T DistanceTo(T x, T y)
        {

            #region Initial Checks

            if (x == null)
                throw new ArgumentNullException("The given x-coordinate must not be null!");

            if (y == null)
                throw new ArgumentNullException("The given y-coordinate must not be null!");

            #endregion

            var dX = Math.Distance(X, x);
            var dY = Math.Distance(Y, y);

            return Math.Sqrt(Math.Add(Math.Mul(dX, dX), Math.Mul(dY, dY)));

        }

        #endregion

        #region DistanceTo(Vector)

        /// <summary>
        /// A method to calculate the distance between
        /// this and another vector of type T.
        /// </summary>
        /// <param name="Vector">A vector of type T</param>
        /// <returns>The distance between this pixel and the given pixel.</returns>
        public T DistanceTo(IVector2D<T> Vector)
        {

            #region Initial Checks

            if (Vector == null)
                throw new ArgumentNullException("The given vector must not be null!");

            #endregion

            var dX = Math.Distance(X, Vector.X);
            var dY = Math.Distance(Y, Vector.Y);

            return Math.Sqrt(Math.Add(Math.Mul(dX, dX), Math.Mul(dY, dY)));

        }

        #endregion


        #region Operator overloadings

        #region Operator == (Vector1, Vector2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Vector1">A Vector&lt;T&gt;.</param>
        /// <param name="Vector2">Another Vector&lt;T&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Vector2D<T> Vector1, Vector2D<T> Vector2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(Vector1, Vector2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) Vector1 == null) || ((Object) Vector2 == null))
                return false;

            return Vector1.Equals(Vector2);

        }

        #endregion

        #region Operator != (Vector1, Vector2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Vector1">A Vector&lt;T&gt;.</param>
        /// <param name="Vector2">Another Vector&lt;T&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Vector2D<T> Vector1, Vector2D<T> Vector2)
        {
            return !(Vector1 == Vector2);
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

            // Check if the given object is an Vector2D<T>.
            var VectorT = (Vector2D<T>) Object;
            if ((Object) VectorT == null)
                return false;

            return this.Equals(VectorT);

        }

        #endregion

        #region Equals(IVector)

        /// <summary>
        /// Compares two vectors for equality.
        /// </summary>
        /// <param name="IVector">A vector to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IVector2D<T> IVector)
        {

            if ((Object) IVector == null)
                return false;

            return this.X.  Equals(IVector.X) &&
                   this.Y.Equals(IVector.Y);

        }

        #endregion

        #endregion

        #region IComparable Members

        public int CompareTo(IVector2D<T> other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

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
            return String.Format("Vector2D: X={0}, Y={1}",
                                 X.ToString(),
                                 Y.ToString());
        }

        #endregion

    }

}
