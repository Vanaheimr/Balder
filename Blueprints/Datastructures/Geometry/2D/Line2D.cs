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
    /// A 2-dimensional line of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the line.</typeparam>
    public class Line2D<T> : ILine2D<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Data

        /// <summary>
        /// Mathoperation helpers.
        /// </summary>
        protected readonly IMaths<T> Math;

        #endregion

        #region Properties

        #region X1

        /// <summary>
        /// The fist x-coordinate of the line.
        /// </summary>
        public T X1 { get; private set; }

        #endregion

        #region Y1

        /// <summary>
        /// The first y-coordinate of the line.
        /// </summary>
        public T Y1 { get; private set; }

        #endregion

        #region X2

        /// <summary>
        /// The second x-coordinate of the line.
        /// </summary>
        public T X2 { get; private set; }

        #endregion

        #region Y2

        /// <summary>
        /// The second y-coordinate of the line.
        /// </summary>
        public T Y2 { get; private set; }

        #endregion

        #region Length

        /// <summary>
        /// The length of the line.
        /// </summary>
        public T Length { get; private set; }

        #endregion


        #region Pixel1

        /// <summary>
        /// The left/top pixel of the line.
        /// </summary>
        public IPixel<T> Pixel1 { get; private set; }

        #endregion

        #region Pixel2

        /// <summary>
        /// The right/bottom pixel of the line.
        /// </summary>
        public IPixel<T> Pixel2 { get; private set; }

        #endregion


        #region Gradient

        /// <summary>
        /// The gradient/inclination of the line.
        /// </summary>
        public T Gradient
        {
            get
            {
                var _Vector = this.Vector;
                return Math.Div(_Vector.Y, _Vector.X);
            }
        }

        #endregion

        #region YIntercept

        /// <summary>
        /// The interception of the line with the y-axis.
        /// </summary>
        public T YIntercept
        {
            get
            {
                return Math.Sub(Pixel1.Y, Math.Mul(Gradient, Pixel1.X));
            }
        }

        #endregion

        #region Center

        /// <summary>
        /// The center pixel of the line.
        /// </summary>
        public IPixel<T> Center
        {
            get
            {
                return new Pixel<T>(Math.Add(X1, Math.Div2(Math.Sub(X2, X1))),
                                    Math.Add(Y1, Math.Div2(Math.Sub(Y2, Y1))));
            }
        }

        #endregion

        #region Vector

        /// <summary>
        /// The vector of the line.
        /// </summary>
        public IVector2D<T> Vector
        {
            get
            {
                return new Vector2D<T>(X1, Y1, X2, Y2);
            }
        }

        #endregion

        #region Normale

        /// <summary>
        /// The normale vector of the line.
        /// </summary>
        public IVector2D<T> Normale
        {
            get
            {
                // Normale := (-b, a)
                var _Vector = this.Vector;
                return new Vector2D<T>(Math.Inv(_Vector.Y), _Vector.X);
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region Line2D(X1, Y1, X2, Y2)

        /// <summary>
        /// Create a 2-dimensional line of type T.
        /// </summary>
        /// <param name="X1">The first x-coordinate of the line.</param>
        /// <param name="Y1">The first y-coordinate of the line.</param>
        /// <param name="X2">The second x-coordinate of the line.</param>
        /// <param name="Y2">The second y-coordinate of the line.</param>
        public Line2D(T X1, T Y1, T X2, T Y2)
        {

            #region Initial Checks

            if (X1   == null)
                throw new ArgumentNullException("The first x-coordinate must not be null!");

            if (Y1    == null)
                throw new ArgumentNullException("The first y-coordinate must not be null!");

            if (X2  == null)
                throw new ArgumentNullException("The second x-coordinate must not be null!");

            if (Y2 == null)
                throw new ArgumentNullException("The second y-coordinate must not be null!");

            #endregion

            this.Math   = MathsFactory<T>.Instance;
            
            this.X1     = X1;
            this.Y1     = Y1;
            this.X2     = X2;
            this.Y2     = Y2;

            this.Pixel1 = new Pixel<T>(X1, Y1);
            this.Pixel2 = new Pixel<T>(X2, Y2);

            this.Length = Pixel1.DistanceTo(Pixel2);

        }

        #endregion

        #region Line2D(Pixel1, X, Y)

        /// <summary>
        /// Create a 2-dimensional line of type T.
        /// </summary>
        /// <param name="Pixel">A pixel of type T.</param>
        /// <param name="X">The x-component.</param>
        /// <param name="Y">The y-component.</param>
        public Line2D(IPixel<T> Pixel, T X, T Y)
        {

            #region Initial Checks

            if (Pixel == null)
                throw new ArgumentNullException("The given pixel must not be null!");

            if (X == null)
                throw new ArgumentNullException("The given x-component must not be null!");

            if (Y == null)
                throw new ArgumentNullException("The given y-component must not be null!");

            #endregion

            this.Math   = MathsFactory<T>.Instance;

            this.X1     = Pixel.X;
            this.Y1     = Pixel.Y;
            this.X2     = Math.Add(Pixel.X, X);
            this.Y2     = Math.Add(Pixel.Y, Y);

            this.Pixel1 = new Pixel<T>(X1, Y1);
            this.Pixel2 = new Pixel<T>(X2, Y2);

            this.Length = Pixel1.DistanceTo(Pixel2);

        }

        #endregion

        #region Line2D(Pixel1, Pixel2)

        /// <summary>
        /// Create a 2-dimensional line of type T.
        /// </summary>
        /// <param name="Pixel1">A pixel of type T.</param>
        /// <param name="Pixel2">A pixel of type T.</param>
        public Line2D(IPixel<T> Pixel1, IPixel<T> Pixel2)
        {

            #region Initial Checks

            if (Pixel1   == null)
                throw new ArgumentNullException("The given left-coordinate must not be null!");

            if (Pixel2  == null)
                throw new ArgumentNullException("The given right-coordinate must not be null!");

            #endregion

            this.Math   = MathsFactory<T>.Instance;

            this.X1     = Pixel1.X;
            this.Y1     = Pixel1.Y;
            this.X2     = Pixel2.X;
            this.Y2     = Pixel2.Y;

            this.Pixel1 = Pixel1;
            this.Pixel2 = Pixel2;

            this.Length = Pixel1.DistanceTo(Pixel2);

        }

        #endregion

        #endregion


        #region Contains(Pixel)

        /// <summary>
        /// Checks if the given pixel is located on this line.
        /// </summary>
        /// <param name="Pixel">A pixel of type T.</param>
        /// <returns>True if the pixel is located on this line; False otherwise.</returns>
        public Boolean Contains(IPixel<T> Pixel)
        {

            #region Initial Checks

            if (Pixel == null)
                throw new ArgumentNullException("The given pixel must not be null!");

            #endregion

            #region Check line bounds

            if (Pixel.X.IsLessThan(X1)   && Pixel.X.IsLessThan(X2))
                return false;

            if (Pixel.X.IsLargerThan(X1) && Pixel.X.IsLargerThan(X2))
                return false;

            if (Pixel.Y.IsLessThan(Y1)   && Pixel.Y.IsLessThan(Y2))
                return false;

            if (Pixel.Y.IsLargerThan(Y1) && Pixel.Y.IsLargerThan(Y2))
                return false;

            #endregion

            // Check equation: Pixel.Y = m*Pixel.X + t
            return Pixel.Y.Equals(Math.Add(Math.Mul(Gradient, Pixel.X), YIntercept));

        }

        #endregion



        #region Intersect(Line, out Pixel)

        /// <summary>
        /// Checks if and where the given lines intersect.
        /// </summary>
        /// <param name="Line">A line.</param>
        /// <param name="Pixel">The intersection of both lines.</param>        
        /// <returns>True if the lines intersect; False otherwise.</returns>
        public Boolean Intersect(ILine2D<T> Line, out IPixel<T> Pixel)
        {

            #region Initial Checks

            if (Line == null)
                throw new ArgumentNullException("The given line must not be null!");

            #endregion

            // Assume both lines are infinite in order to get their intersection

            T x, y;

            // this line is parallel to the y-axis!
            if (this.Normale.Y.Equals(Math.Zero))
            {
                x = this.Pixel1.X;
                y = Math.Add(Math.Mul(Line.Gradient, this.Pixel1.X), Line.YIntercept);
            }

            // the given line is parallel to the y-axis!
            else if (Line.Normale.Y.Equals(Math.Zero))
            {
                x = Line.X1;
                y = Math.Add(Math.Mul(this.Gradient, Line.X1), this.YIntercept);
            }

            else
            {

                x = Math.Div(Math.Sub(Line.YIntercept, this.YIntercept),
                             Math.Sub(this.Gradient,   Line.Gradient));

                y = Math.Div(Math.Sub(Math.Mul(this.YIntercept, Line.Gradient),
                                      Math.Mul(Line.YIntercept, this.Gradient)),
                             Math.Sub(Line.Gradient, this.Gradient));

            }

            Pixel = new Pixel<T>(x, y);

            return this.Contains(Pixel);

        }

        #endregion


        #region Operator overloadings

        #region Operator == (Line1, Line2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Line1">A Line&lt;T&gt;.</param>
        /// <param name="Line2">Another Line&lt;T&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Line2D<T> Line1, Line2D<T> Line2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(Line1, Line2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) Line1 == null) || ((Object) Line2 == null))
                return false;

            return Line1.Equals(Line2);

        }

        #endregion

        #region Operator != (Line1, Line2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Line1">A Line&lt;T&gt;.</param>
        /// <param name="Line2">Another Line&lt;T&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Line2D<T> Line1, Line2D<T> Line2)
        {
            return !(Line1 == Line2);
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

            // Check if the given object is an Line2D<T>.
            var LineT = (Line2D<T>) Object;
            if ((Object) LineT == null)
                return false;

            return this.Equals(LineT);

        }

        #endregion

        #region Equals(ILine)

        /// <summary>
        /// Compares two lines for equality.
        /// </summary>
        /// <param name="ILine">A line to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(ILine2D<T> ILine)
        {

            if ((Object) ILine == null)
                return false;

                   // Normal direction
            return (this.X1.Equals(ILine.X1) &&
                    this.Y1.Equals(ILine.Y1) &&
                    this.X2.Equals(ILine.X2) &&
                    this.Y2.Equals(ILine.Y2))
                    ||
                   // Opposite direction
                   (this.X1.Equals(ILine.X2) &&
                    this.Y1.Equals(ILine.Y2) &&
                    this.X2.Equals(ILine.X1) &&
                    this.Y2.Equals(ILine.Y1));

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
            return X1.GetHashCode() ^ 1 + Y1.GetHashCode() ^ 2 + X2.GetHashCode() ^ 3 + Y2.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Return a string represtentation of this object.
        /// </summary>
        public override String ToString()
        {
            return String.Format("Line2D: X1={0}, Y1={1}, X2={2}, Y2={3}",
                                 X1.ToString(),
                                 Y1.ToString(),
                                 X2.ToString(),
                                 Y2.ToString());
        }

        #endregion

    }

}
