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

namespace de.ahzf.Blueprints.Quadtree
{

    /// <summary>
    /// A point of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the point.</typeparam>
    public struct Point<T> : IEquatable<Point<T>>, IComparable<Point<T>>, IComparable
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Data

        /// <summary>
        /// X
        /// </summary>
        public readonly T X;

        /// <summary>
        /// Y
        /// </summary>
        public readonly T Y;

        #endregion

        #region Constructor(s)

        #region Point(x, y)

        /// <summary>
        /// Create a point of type T.
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public Point(T x, T y)
        {
            this.X = x;
            this.Y = y;
        }

        #endregion

        #endregion


        #region Operator overloadings

        #region Operator == (Point1, Point2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Point1">A Point&lt;T&gt;.</param>
        /// <param name="Point2">Another Point&lt;T&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Point<T> Point1, Point<T> Point2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(Point1, Point2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) Point1 == null) || ((Object) Point2 == null))
                return false;

            return Point1.Equals(Point2);

        }

        #endregion

        #region Operator != (Point1, Point2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Point1">A Point&lt;T&gt;.</param>
        /// <param name="Point2">Another Point&lt;T&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Point<T> Point1, Point<T> Point2)
        {
            return !(Point1.Equals(Point2));
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

            // Check if the given object is an Point<T>.
            var PointT = (Point<T>) Object;
            if ((Object) PointT == null)
                throw new ArgumentException("The given object is not a Point<T>!");

            return CompareTo(PointT);

        }

        #endregion

        #region CompareTo(PointT)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PointT">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(Point<T> PointT)
        {

            if ((Object) PointT == null)
                throw new ArgumentNullException("The given Point<T> must not be null!");

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

            // Check if the given object is an Point<T>.
            var PointT = (Point<T>) Object;
            if ((Object) PointT == null)
                return false;

            return this.Equals(PointT);

        }

        #endregion

        #region Equals(PointT)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PointT">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(Point<T> PointT)
        {

            if ((Object) PointT == null)
                return false;

            return X.Equals(PointT.X) && Y.Equals(PointT.Y);

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
