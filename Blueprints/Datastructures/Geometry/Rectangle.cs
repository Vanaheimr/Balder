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
    /// A rectangle of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the rectangle.</typeparam>
    public class Rectangle<T> : IRectangle<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Data

        /// <summary>
        /// Mathoperation helpers.
        /// </summary>
        protected readonly IMaths<T> Math;

        #endregion

        #region Properties

        #region Left

        /// <summary>
        /// The left-coordinate of the rectangle.
        /// </summary>
        public T Left   { get; private set; }

        #endregion

        #region Top

        /// <summary>
        /// The top-coordinate of the rectangle.
        /// </summary>
        public T Top    { get; private set; }

        #endregion

        #region Right

        /// <summary>
        /// The right-coordinate of the rectangle.
        /// </summary>
        public T Right  { get; private set; }

        #endregion

        #region Bottom

        /// <summary>
        /// The bottom-coordinate of the rectangle.
        /// </summary>
        public T Bottom { get; private set; }

        #endregion


        #region Width

        /// <summary>
        /// The width of the rectangle.
        /// </summary>
        public T Width
        {
            get
            {
                return Math.Distance(Left, Right);
            }
        }

        #endregion

        #region Height

        /// <summary>
        /// The height of the rectangle.
        /// </summary>
        public T Height
        {
            get
            {
                return Math.Distance(Top, Bottom);
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region Rectangle(Left, Top, Right, Bottom)

        /// <summary>
        /// Create a rectangle of type T.
        /// </summary>
        /// <param name="Left">The left-coordinate of the rectangle.</param>
        /// <param name="Top">The top-coordinate of the rectangle.</param>
        /// <param name="Right">The right-coordinate of the rectangle.</param>
        /// <param name="Bottom">The bottom-coordinate of the rectangle.</param>
        public Rectangle(T Left, T Top, T Right, T Bottom)
        {

            #region Initial Checks

            if (Left   == null)
                throw new ArgumentNullException("The given left-coordinate must not be null!");

            if (Top    == null)
                throw new ArgumentNullException("The given top-coordinate must not be null!");

            if (Right  == null)
                throw new ArgumentNullException("The given right-coordinate must not be null!");

            if (Bottom == null)
                throw new ArgumentNullException("The given bottom-coordinate must not be null!");

            #endregion

            this.Math   = MathsFactory<T>.Instance;
            this.Left   = Math.Min(Left, Right);
            this.Top    = Math.Min(Top,  Bottom);
            this.Right  = Math.Max(Left, Right);
            this.Bottom = Math.Max(Top,  Bottom);

            #region Math Checks

            if (Math.Distance(Left, Right).Equals(Math.Zero))
                throw new ArgumentException("The resulting width must not be zero!");

            if (Math.Distance(Top, Bottom).Equals(Math.Zero))
                throw new ArgumentException("The resulting height must not be zero!");

            #endregion

        }

        #endregion

        #region Rectangle(Pixel1, Pixel2)

        /// <summary>
        /// Create a rectangle of type T.
        /// </summary>
        /// <param name="Pixel1">A pixel of type T.</param>
        /// <param name="Pixel2">A pixel of type T.</param>
        public Rectangle(IPixel<T> Pixel1, IPixel<T> Pixel2)
        {

            #region Initial Checks

            if (Pixel1 == null)
                throw new ArgumentNullException("The given first pixel must not be null!");

            if (Pixel2 == null)
                throw new ArgumentNullException("The given second pixel must not be null!");

            #endregion

            this.Math   = MathsFactory<T>.Instance;
            this.Left   = Math.Min(Pixel1.X, Pixel2.X);
            this.Top    = Math.Min(Pixel1.Y, Pixel2.Y);
            this.Right  = Math.Max(Pixel1.X, Pixel2.X);
            this.Bottom = Math.Max(Pixel1.Y, Pixel2.Y);

            #region Math Checks

            if (Math.Distance(Left, Right).Equals(Math.Zero))
                throw new ArgumentException("The resulting width must not be zero!");

            if (Math.Distance(Top, Bottom).Equals(Math.Zero))
                throw new ArgumentException("The resulting height must not be zero!");

            #endregion

        }

        #endregion

        #region Rectangle(Pixel, Width, Height)

        /// <summary>
        /// Create a rectangle of type T.
        /// </summary>
        /// <param name="Pixel">A pixel of type T in the upper left corner of the rectangle.</param>
        /// <param name="Width">The width of the rectangle.</param>
        /// <param name="Height">The height of the rectangle.</param>
        public Rectangle(Pixel<T> Pixel, T Width, T Height)
        {

            #region Initial Checks

            if (Pixel  == null)
                throw new ArgumentNullException("The given pixel must not be null!");

            if (Width  == null)
                throw new ArgumentNullException("The given width must not be null!");

            if (Height == null)
                throw new ArgumentNullException("The given height must not be null!");

            #endregion

            this.Math   = MathsFactory<T>.Instance;
            this.Left   = Pixel.X;
            this.Top    = Pixel.Y;            
            this.Right  = Math.Add(Pixel.X, Width);
            this.Bottom = Math.Add(Pixel.Y, Height);

            #region Math Checks

            if (Math.Distance(Left, Right).Equals(Math.Zero))
                throw new ArgumentException("The resulting width must not be zero!");

            if (Math.Distance(Top, Bottom).Equals(Math.Zero))
                throw new ArgumentException("The resulting height must not be zero!");

            #endregion

        }

        #endregion

        #endregion


        #region Contains(x, y)

        /// <summary>
        /// Checks if the given x- and y-coordinates are
        /// located within this rectangle.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>True if the coordinates are located within this rectangle; False otherwise.</returns>
        public Boolean Contains(T x, T y)
        {

            #region Initial Checks

            if (x == null)
                throw new ArgumentNullException("The given x-coordinate must not be null!");

            if (y == null)
                throw new ArgumentNullException("The given y-coordinate must not be null!");

            #endregion
            
            if (x.CompareTo(Left)   >= 0 &&
                x.CompareTo(Right)  <= 0 &&
                y.CompareTo(Top)    >= 0 &&
                y.CompareTo(Bottom) <= 0)
                return true;

            return false;

        }

        #endregion

        #region Contains(Pixel)

        /// <summary>
        /// Checks if the given pixel is located
        /// within this rectangle.
        /// </summary>
        /// <param name="Pixel">A pixel.</param>
        /// <returns>True if the pixel is located within this rectangle; False otherwise.</returns>
        public Boolean Contains(IPixel<T> Pixel)
        {

            #region Initial Checks

            if (Pixel == null)
                throw new ArgumentNullException("The given pixel must not be null!");

            #endregion

            if (Pixel.X.CompareTo(Left)   >= 0 &&
                Pixel.X.CompareTo(Right)  <= 0 &&
                Pixel.Y.CompareTo(Top)    >= 0 &&
                Pixel.Y.CompareTo(Bottom) <= 0)
                return true;

            return false;

        }

        #endregion

        #region Contains(Rectangle)

        /// <summary>
        /// Checks if the given rectangle is located
        /// within this rectangle.
        /// </summary>
        /// <param name="Rectangle">A rectangle of type T.</param>
        /// <returns>True if the rectangle is located within this rectangle; False otherwise.</returns>
        public Boolean Contains(IRectangle<T> Rectangle)
        {

            #region Initial Checks

            if (Rectangle == null)
                throw new ArgumentNullException("The given rectangle must not be null!");

            #endregion


            // Verify that every corner of the given rectangle
            // is located within this rectangle

            if (!Contains(Rectangle.Left,  Rectangle.Top))
                return false;

            if (!Contains(Rectangle.Right, Rectangle.Top))
                return false;

            if (!Contains(Rectangle.Left,  Rectangle.Bottom))
                return false;

            if (!Contains(Rectangle.Right, Rectangle.Bottom))
                return false;

            return true;

        }

        #endregion

        #region Overlaps(Rectangle)

        /// <summary>
        /// Checks if the given rectangle shares some
        /// area with this rectangle.
        /// </summary>
        /// <param name="Rectangle">A rectangle of type T.</param>
        /// <returns>True if the rectangle shares some area with this rectangle; False otherwise.</returns>
        public Boolean Overlaps(IRectangle<T> Rectangle)
        {

            #region Initial Checks

            if (Rectangle == null)
                throw new ArgumentNullException("The given rectangle must not be null!");

            #endregion


            // Check if any corner of the given rectangle
            // is located within this rectangle

            if (Contains(Rectangle.Left,  Rectangle.Top))
                return true;

            if (Contains(Rectangle.Right, Rectangle.Top))
                return true;

            if (Contains(Rectangle.Left,  Rectangle.Bottom))
                return true;

            if (Contains(Rectangle.Right, Rectangle.Bottom))
                return true;

            return false;

        }

        #endregion


        #region Operator overloadings

        #region Operator == (Rectangle1, Rectangle2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Rectangle1">A Rectangle&lt;T&gt;.</param>
        /// <param name="Rectangle2">Another Rectangle&lt;T&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Rectangle<T> Rectangle1, Rectangle<T> Rectangle2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(Rectangle1, Rectangle2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) Rectangle1 == null) || ((Object) Rectangle2 == null))
                return false;

            return Rectangle1.Equals(Rectangle2);

        }

        #endregion

        #region Operator != (Rectangle1, Rectangle2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Rectangle1">A Rectangle&lt;T&gt;.</param>
        /// <param name="Rectangle2">Another Rectangle&lt;T&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Rectangle<T> Rectangle1, Rectangle<T> Rectangle2)
        {
            return !(Rectangle1 == Rectangle2);
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

            // Check if the given object is an Rectangle<T>.
            var RectangleT = (Rectangle<T>) Object;
            if ((Object) RectangleT == null)
                return false;

            return this.Equals(RectangleT);

        }

        #endregion

        #region Equals(IRectangle)

        /// <summary>
        /// Compares two rectangles for equality.
        /// </summary>
        /// <param name="IRectangle">A rectangle to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IRectangle<T> IRectangle)
        {

            if ((Object) IRectangle == null)
                return false;

            return this.Left.  Equals(IRectangle.Left)  &&
                   this.Top.   Equals(IRectangle.Top)   &&
                   this.Right. Equals(IRectangle.Right) &&
                   this.Bottom.Equals(IRectangle.Bottom);

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
            return Left.GetHashCode() ^ 1 + Top.GetHashCode() ^ 2 + Right.GetHashCode() ^ 3 + Bottom.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Return a string represtentation of this object.
        /// </summary>
        public override String ToString()
        {
            return String.Format("Left={0}, Top={1}, Right={2}, Bottom={3}",
                                 Left.  ToString(),
                                 Top.   ToString(),
                                 Right. ToString(),
                                 Bottom.ToString());
        }

        #endregion

    }

}
