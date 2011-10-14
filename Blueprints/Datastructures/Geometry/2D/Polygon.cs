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
using System.Linq;
using System.Collections.Generic;

using de.ahzf.Blueprints.Maths;
using System.Text;

#endregion

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// A polygon of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the polygon.</typeparam>
    public class Polygon<T> : IPolygon<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Data

        /// <summary>
        /// Mathoperation helpers.
        /// </summary>
        protected readonly IMaths<T> Math;


        protected readonly List<IPixel<T>> _Pixels;

        #endregion

        #region Properties

        #region Pixels

        /// <summary>
        /// The pixels of the polygon.
        /// </summary>
        public IEnumerable<IPixel<T>> Pixels
        {
            get
            {
                return _Pixels;
            }
        }

        #endregion


        #region Borders

        /// <summary>
        /// Return an enumeration of lines representing the
        /// surrounding borders of the polygon.
        /// </summary>
        public IEnumerable<ILine2D<T>> Borders
        {
            get
            {

                var _Array = _Pixels.ToArray();
                var _Lines = new List<ILine2D<T>>();

                for (var i = 0; i<_Array.Length-2; i++)
                    _Lines.Add(new Line2D<T>(_Array[i], _Array[i+1]));

                _Lines.Add(new Line2D<T>(_Array[_Array.Length], _Array[0]));

                return _Lines;

            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region Polygon(Pixel1, Pixel2, Pixel3)

        /// <summary>
        /// Create a polygon of type T.
        /// </summary>
        /// <param name="Pixels">The pixels of the polygon.</param>
        public Polygon(params IPixel<T>[] Pixels)
        {

            #region Initial Checks

            if (Pixels == null || Pixels.Length <= 3)
                throw new ArgumentNullException("The given array of pixels must not be null or smaller than three!");

            #endregion

            this.Math = MathsFactory<T>.Instance;

            #region Math Checks

            //if (Pixel1.Equals(Pixel2) ||
            //    Pixel1.Equals(Pixel3) ||
            //    Pixel2.Equals(Pixel3))
            //    throw new ArgumentException("All distances between the pixels must be larger than zero!");

            //if (Pixel1.X.Equals(Pixel2.X) &&
            //    Pixel2.X.Equals(Pixel3.X))
            //    throw new ArgumentException("All three pixels must not be on a single line!");

            //if (Pixel1.Y.Equals(Pixel2.Y) &&
            //    Pixel2.Y.Equals(Pixel3.Y))
            //    throw new ArgumentException("All three pixels must not be on a single line!");

            #endregion

            this._Pixels = new List<IPixel<T>>(Pixels);

        }

        #endregion

        #endregion


        #region Operator overloadings

        //#region Operator == (Triangle1, Triangle2)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="Triangle1">A Triangle&lt;T&gt;.</param>
        ///// <param name="Triangle2">Another Triangle&lt;T&gt;.</param>
        ///// <returns>true|false</returns>
        //public static Boolean operator == (Triangle<T> Triangle1, Triangle<T> Triangle2)
        //{

        //    // If both are null, or both are same instance, return true.
        //    if (Object.ReferenceEquals(Triangle1, Triangle2))
        //        return true;

        //    // If one is null, but not both, return false.
        //    if (((Object) Triangle1 == null) || ((Object) Triangle2 == null))
        //        return false;

        //    return Triangle1.Equals(Triangle2);

        //}

        //#endregion

        //#region Operator != (Triangle1, Triangle2)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="Triangle1">A Triangle&lt;T&gt;.</param>
        ///// <param name="Triangle2">Another Triangle&lt;T&gt;.</param>
        ///// <returns>true|false</returns>
        //public static Boolean operator != (Triangle<T> Triangle1, Triangle<T> Triangle2)
        //{
        //    return !(Triangle1 == Triangle2);
        //}

        //#endregion

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
            var IPolygon = Object as IPolygon<T>;
            if ((Object) IPolygon == null)
                throw new ArgumentException("The given object is not a valid polygon!");

            return CompareTo(IPolygon);

        }

        #endregion

        #region CompareTo(IPolygon)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="IPolygon">An object to compare with.</param>
        public Int32 CompareTo(IPolygon<T> IPolygon)
        {
            
            if ((Object) IPolygon == null)
                throw new ArgumentNullException("The given polygon must not be null!");

            // Compare the x-coordinate of the circumcenter
            var _Result = IPolygon.Pixels.First().X.CompareTo(IPolygon.Pixels.First().X);

            // If equal: Compare the y-coordinate of the circumcenter
            if (_Result == 0)
                _Result = this.Pixels.First().Y.CompareTo(IPolygon.Pixels.First().Y);

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

            // Check if the given object is an Polygon<T>.
            var PolygonT = (IPolygon<T>) Object;
            if ((Object) PolygonT == null)
                return false;

            return this.Equals(PolygonT);

        }

        #endregion

        #region Equals(IPolygon)

        /// <summary>
        /// Compares two triangles for equality.
        /// </summary>
        /// <param name="IPolygon">A polygon to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IPolygon<T> IPolygon)
        {

            if ((Object) IPolygon == null)
                return false;

            foreach (var _Pixel1 in _Pixels)
                foreach (var _Pixel2 in IPolygon.Pixels)
                    if (!_Pixel1.Equals(_Pixel2))
                        return false;

            return true;

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

            var _Hashcode = 0;

            foreach (var _Pixel in _Pixels)
                _Hashcode ^= _Pixel.GetHashCode();

            return _Hashcode;

        }

        #endregion

        #region ToString()

        /// <summary>
        /// Return a string represtentation of this object.
        /// </summary>
        public override String ToString()
        {

            var _StringBuilder = new StringBuilder("Polygon: ");

            foreach (var _Pixel in _Pixels)
                _StringBuilder.Append(_Pixel.ToString() + ", ");

            _StringBuilder.Length = _StringBuilder.Length - 2;

            return _StringBuilder.ToString();

        }

        #endregion

    }

}
