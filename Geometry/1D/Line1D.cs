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

using de.ahzf.Blueprints.Maths;

#endregion

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// A 1-dimensional line of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the line.</typeparam>
    public class Line1D<T> : ILine1D<T>
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
        /// The left-coordinate of the line.
        /// </summary>
        public T Left   { get; private set; }

        #endregion

        #region Right

        /// <summary>
        /// The right-coordinate of the line.
        /// </summary>
        public T Right  { get; private set; }

        #endregion


        #region Length

        /// <summary>
        /// The length of the line.
        /// </summary>
        public T Length
        {
            get
            {
                return Math.Distance(Left, Right);
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region Line1D(Left, Right)

        /// <summary>
        /// Create a 1-dimensional line of type T.
        /// </summary>
        /// <param name="Left">The left-coordinate of the line.</param>
        /// <param name="Right">The right-coordinate of the line.</param>
        public Line1D(T Left, T Right)
        {

            #region Initial Checks

            if (Left   == null)
                throw new ArgumentNullException("The given left-coordinate must not be null!");

            if (Right  == null)
                throw new ArgumentNullException("The given right-coordinate must not be null!");

            #endregion

            this.Math  = MathsFactory<T>.Instance;

            #region Math Checks

            if (Math.Distance(Left, Right).Equals(Math.Zero))
                throw new ArgumentException("The resulting size must not be zero!");

            #endregion

            this.Left  = Math.Min(Left, Right);
            this.Right = Math.Max(Left, Right);

        }

        #endregion

        #endregion


        #region Contains(Element)

        /// <summary>
        /// Checks if the given element is located on this line.
        /// </summary>
        /// <param name="Element">An element.</param>
        /// <returns>True if the element is located on this line; False otherwise.</returns>
        public Boolean Contains(T Element)
        {

            #region Initial Checks

            if (Element == null)
                throw new ArgumentNullException("The given element must not be null!");

            #endregion

            if (Element.CompareTo(Left)  >= 0 &&
                Element.CompareTo(Right) <= 0)
                return true;

            return false;

        }

        #endregion

        #region Contains(Line)

        /// <summary>
        /// Checks if the given line is located
        /// within this line.
        /// </summary>
        /// <param name="Line">A line of type T.</param>
        /// <returns>True if the line is located within this line; False otherwise.</returns>
        public Boolean Contains(ILine1D<T> Line)
        {

            #region Initial Checks

            if (Line == null)
                throw new ArgumentNullException("The given line must not be null!");

            #endregion


            // Verify that every corner of the given line
            // is located within this line

            if (!Contains(Line.Left))
                return false;

            if (!Contains(Line.Right))
                return false;

            return true;

        }

        #endregion

        #region Overlaps(Line)

        /// <summary>
        /// Checks if the given line shares some
        /// area with this line.
        /// </summary>
        /// <param name="Line">A line of type T.</param>
        /// <returns>True if the line shares some area with this line; False otherwise.</returns>
        public Boolean Overlaps(ILine1D<T> Line)
        {

            #region Initial Checks

            if (Line == null)
                throw new ArgumentNullException("The given line must not be null!");

            #endregion


            // Check if any corner of the given line
            // is located within this line

            if (Contains(Line.Left))
                return true;

            if (Contains(Line.Right))
                return true;

            return false;

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
        public static Boolean operator == (Line1D<T> Line1, Line1D<T> Line2)
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
        public static Boolean operator != (Line1D<T> Line1, Line1D<T> Line2)
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

            // Check if the given object is an Line<T>.
            var LineT = (Line1D<T>) Object;
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
        public Boolean Equals(ILine1D<T> ILine)
        {

            if ((Object) ILine == null)
                return false;

            return this.Left. Equals(ILine.Left) &&
                   this.Right.Equals(ILine.Right);

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
            return Left.GetHashCode() ^ 1 + Right.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Return a string represtentation of this object.
        /// </summary>
        public override String ToString()
        {
            return String.Format("Line1D: Left={0}, Right={2}",
                                 Left.  ToString(),
                                 Right. ToString());
        }

        #endregion

    }

}
