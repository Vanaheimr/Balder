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
using System.Collections.Generic;

using de.ahzf.Blueprints.Maths;

#endregion

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// A triangle of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the triangle.</typeparam>
    public class Triangle<T> : ITriangle<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Data

        /// <summary>
        /// Mathoperation helpers.
        /// </summary>
        protected readonly IMaths<T> Math;

        #endregion

        #region Properties

        #region P1

        /// <summary>
        /// The first pixel of the triangle.
        /// </summary>
        public IPixel<T> P1 { get; private set; }

        #endregion

        #region P2

        /// <summary>
        /// The second pixel of the triangle.
        /// </summary>
        public IPixel<T> P2 { get; private set; }

        #endregion

        #region P3

        /// <summary>
        /// The third pixel of the triangle.
        /// </summary>
        public IPixel<T> P3 { get; private set; }

        #endregion


        #region CircumCenter

        /// <summary>
        /// Return the cirumcenter of the triangle.
        /// </summary>
        public IPixel<T> CircumCenter
        {
            get
            {

                var _Line12     = new Line2D<T>(P1, P2);
                var _Line23     = new Line2D<T>(P2, P3);

                var _Normale12  = _Line12.Normale;
                var _Normale23  = _Line23.Normale;

                return new Line2D<T>(_Line12.Center, _Normale12.X, _Normale12.Y).
                           Intersection(
                       new Line2D<T>(_Line23.Center, _Normale23.X, _Normale23.Y));


            }
        }

        #endregion

        #region CircumCircle

        /// <summary>
        /// Return the circumcircle of the triangle.
        /// </summary>
        public ICircle<T> CircumCircle
        {
            get
            {
                var _CircumCenter = this.CircumCenter;
                return new Circle<T>(_CircumCenter, P1.DistanceTo(_CircumCenter));
            }
        }

        #endregion

        #region Borders

        /// <summary>
        /// Return an enumeration of lines representing the
        /// surrounding borders of the triangle.
        /// </summary>
        public IEnumerable<ILine2D<T>> Borders
        {
            get
            {

                var _Lines = new List<ILine2D<T>>();
                _Lines.Add(new Line2D<T>(P1, P2));
                _Lines.Add(new Line2D<T>(P2, P3));
                _Lines.Add(new Line2D<T>(P3, P1));

                return _Lines;

            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region Triangle(Pixel1, Pixel2, Pixel3)

        /// <summary>
        /// Create a triangle of type T.
        /// </summary>
        /// <param name="Pixel1">The first pixel of the triangle.</param>
        /// <param name="Pixel2">The second pixel of the triangle.</param>
        /// <param name="Pixel3">The third pixel of the triangle.</param>
        public Triangle(IPixel<T> Pixel1, IPixel<T> Pixel2, IPixel<T> Pixel3)
        {

            #region Initial Checks

            if (Pixel1 == null)
                throw new ArgumentNullException("The given first pixel must not be null!");

            if (Pixel2 == null)
                throw new ArgumentNullException("The given second pixel must not be null!");

            if (Pixel3 == null)
                throw new ArgumentNullException("The given third pixel must not be null!");

            #endregion

            this.Math = MathsFactory<T>.Instance;

            #region Math Checks

            if (Pixel1.Equals(Pixel2) ||
                Pixel1.Equals(Pixel3) ||
                Pixel2.Equals(Pixel3))
                throw new ArgumentException("All distances between the pixels must be larger than zero!");

            if (Pixel1.X.Equals(Pixel2.X) &&
                Pixel2.X.Equals(Pixel3.X))
                throw new ArgumentException("All three pixels must not be on a single line!");

            if (Pixel1.Y.Equals(Pixel2.Y) &&
                Pixel2.Y.Equals(Pixel3.Y))
                throw new ArgumentException("All three pixels must not be on a single line!");

            #endregion

            #region Sort Pixels

            // Sort by x-coordinate.
            while (true)
            {

                if (Pixel1.X.IsLargerThan(Pixel2.X))
                {
                    Pixel<T>.Swap(ref Pixel1, ref Pixel2);
                    continue;
                }

                if (Pixel1.X.IsLargerThan(Pixel3.X))
                {
                    Pixel<T>.Swap(ref Pixel1, ref Pixel3);
                    continue;
                }

                if (Pixel2.X.IsLargerThan(Pixel3.X))
                {
                    Pixel<T>.Swap(ref Pixel2, ref Pixel3);
                    continue;
                }

                break;

            }

            // Sort by y-coordinate if x-coordinates are the same
            if (Pixel1.X.Equals(Pixel2.X))
                if (Pixel1.Y.IsLargerThan(Pixel2.Y))
                    Pixel<T>.Swap(ref Pixel1, ref Pixel2);

            if (Pixel2.X.Equals(Pixel3.X))
                if (Pixel2.Y.IsLargerThan(Pixel3.Y))
                    Pixel<T>.Swap(ref Pixel1, ref Pixel2);

            #endregion

            this.P1 = Pixel1;
            this.P2 = Pixel2;
            this.P3 = Pixel3;

        }

        #endregion

        #endregion


        #region Operator overloadings

        #region Operator == (Triangle1, Triangle2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Triangle1">A Triangle&lt;T&gt;.</param>
        /// <param name="Triangle2">Another Triangle&lt;T&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Triangle<T> Triangle1, Triangle<T> Triangle2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(Triangle1, Triangle2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) Triangle1 == null) || ((Object) Triangle2 == null))
                return false;

            return Triangle1.Equals(Triangle2);

        }

        #endregion

        #region Operator != (Triangle1, Triangle2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Triangle1">A Triangle&lt;T&gt;.</param>
        /// <param name="Triangle2">Another Triangle&lt;T&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Triangle<T> Triangle1, Triangle<T> Triangle2)
        {
            return !(Triangle1 == Triangle2);
        }

        #endregion

        #endregion

        #region IComparable Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public virtual Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given object must not be null!");

            // Check if the given object is an ITriangle<T>.
            var ITriangle = Object as ITriangle<T>;
            if ((Object) ITriangle == null)
                throw new ArgumentException("The given object is not a valid triangle!");

            return CompareTo(ITriangle);

        }

        #endregion

        #region CompareTo(ITriangle)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ITriangle">An object to compare with.</param>
        public Int32 CompareTo(ITriangle<T> ITriangle)
        {
            
            if ((Object) ITriangle == null)
                throw new ArgumentNullException("The given triangle must not be null!");

            // Compare the x-coordinate of the circumcenter
            var _Result = CircumCenter.X.CompareTo(ITriangle.CircumCenter.X);

            // If equal: Compare the y-coordinate of the circumcenter
            if (_Result == 0)
                _Result = this.CircumCenter.Y.CompareTo(ITriangle.CircumCenter.Y);

            return _Result;

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

            // Check if the given object is an Triangle<T>.
            var TriangleT = (Triangle<T>) Object;
            if ((Object) TriangleT == null)
                return false;

            return this.Equals(TriangleT);

        }

        #endregion

        #region Equals(ITriangle)

        /// <summary>
        /// Compares two triangles for equality.
        /// </summary>
        /// <param name="ITriangle">A triangle to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(ITriangle<T> ITriangle)
        {

            if ((Object) ITriangle == null)
                return false;

            return (this.P1.Equals(ITriangle.P1) &&
                    this.P2.Equals(ITriangle.P2) &&
                    this.P3.Equals(ITriangle.P3)) ||

                   (this.P1.Equals(ITriangle.P2) &&
                    this.P2.Equals(ITriangle.P3) &&
                    this.P3.Equals(ITriangle.P1)) ||

                   (this.P1.Equals(ITriangle.P3) &&
                    this.P2.Equals(ITriangle.P1) &&
                    this.P3.Equals(ITriangle.P2));

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
            return String.Format("Triangle: Pixel1={0}, Pixel2={1}, Pixel3={2}",
                                 P1.ToString(),
                                 P2.ToString(),
                                 P3.ToString());
        }

        #endregion

    }

}
