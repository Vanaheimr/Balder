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
    /// A rectangle of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the rectangle.</typeparam>
    public class Rectangle<T> : AMath<T>, IRectangle<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Properties

        #region Left

        /// <summary>
        /// Left
        /// </summary>
        public T Left   { get; private set; }

        #endregion

        #region Top

        /// <summary>
        /// Top
        /// </summary>
        public T Top    { get; private set; }

        #endregion

        #region Right

        /// <summary>
        /// Right
        /// </summary>
        public T Right  { get; private set; }

        #endregion

        #region Bottom

        /// <summary>
        /// Bottom
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
        /// <param name="Left">The left parameter.</param>
        /// <param name="Top">The top parameter.</param>
        /// <param name="Right">The right parameter.</param>
        /// <param name="Bottom">The bottom parameter.</param>
        public Rectangle(T Left, T Top, T Right, T Bottom)
        {
            this.Left   = Math.Min(Left, Right);
            this.Top    = Math.Min(Top,  Bottom);
            this.Right  = Math.Max(Left, Right);
            this.Bottom = Math.Max(Top,  Bottom);
        }

        #endregion

        #region Rectangle(Pixel1, Pixel2)

        /// <summary>
        /// Create a rectangle of type T.
        /// </summary>
        /// <param name="Pixel1">A pixel of type T.</param>
        /// <param name="Pixel2">A pixel of type T.</param>
        public Rectangle(Pixel<T> Pixel1, Pixel<T> Pixel2)
        {
            this.Left   = Math.Min(Pixel1.X, Pixel2.X);
            this.Top    = Math.Min(Pixel1.Y, Pixel2.Y);
            this.Right  = Math.Max(Pixel1.X, Pixel2.X);
            this.Bottom = Math.Max(Pixel1.Y, Pixel2.Y);
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
            this.Left   = Pixel.X;
            this.Top    = Pixel.Y;
            this.Right  = Math.Add(Pixel.X, Width);
            this.Bottom = Math.Add(Pixel.Y, Height);
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
            
            if (x.CompareTo(Left)   >= 0 &&
                x.CompareTo(Right)  <= 0 &&
                y.CompareTo(Top)    >= 0 &&
                y.CompareTo(Bottom) <= 0)
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
        public Boolean Contains(Rectangle<T> Rectangle)
        {
            
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
