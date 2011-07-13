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
    /// A sphere of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the sphere.</typeparam>
    public class Sphere<T> : AMath<T>, ISphere<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Properties

        #region Left

        /// <summary>
        /// The left-coordinate of the sphere.
        /// </summary>
        public T Left   { get; private set; }

        #endregion

        #region Top

        /// <summary>
        /// The top-coordinate of the sphere.
        /// </summary>
        public T         Top    { get; private set; }

        #endregion

        #region Front

        /// <summary>
        /// The front-coordinate of the sphere.
        /// </summary>
        public T         Front  { get; private set; }

        #endregion

        #region Radius

        /// <summary>
        /// The radius of the sphere.
        /// </summary>
        public T         Radius { get; private set; }

        #endregion


        #region Center

        /// <summary>
        /// The center of the sphere.
        /// </summary>
        public IVoxel<T> Center { get; private set; }

        #endregion

        #region Diameter

        /// <summary>
        /// The diameter of the sphere.
        /// </summary>
        public T Diameter
        {
            get
            {
                return Math.Add(Radius, Radius);
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region Sphere(Left, Top, Front, Radius)

        /// <summary>
        /// Create a sphere of type T.
        /// </summary>
        /// <param name="Left">The left-coordinate of the sphere.</param>
        /// <param name="Top">The top-coordinate of the sphere.</param>
        /// <param name="Front">The front-coordinate of the sphere.</param>
        /// <param name="Radius">The radius parameter.</param>
        public Sphere(T Left, T Top, T Front, T Radius)
        {

            #region Initial Checks

            if (Left   == null)
                throw new ArgumentNullException("The given left-coordinate must not be null!");

            if (Top    == null)
                throw new ArgumentNullException("The given top-coordinate must not be null!");

            if (Front  == null)
                throw new ArgumentNullException("The given front-coordinate must not be null!");

            if (Radius == null)
                throw new ArgumentNullException("The given radius must not be null!");


            if (Radius.Equals(Math.Zero))
                throw new ArgumentException("The given radius must not be zero!");

            #endregion

            this.Left   = Left;
            this.Top    = Top;
            this.Center = new Voxel<T>(Left, Top, Front);
            this.Radius = Radius;

        }

        #endregion

        #endregion


        #region Contains(x, y, z)

        /// <summary>
        /// Checks if the given x-, y- and z-coordinates
        /// are located within this sphere.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="z">The z-coordinate.</param>
        /// <returns>True if the coordinates are located within this sphere; False otherwise.</returns>
        public Boolean Contains(T x, T y, T z)
        {

            #region Initial Checks

            if (x == null)
                throw new ArgumentNullException("The given x-coordinate must not be null!");

            if (y == null)
                throw new ArgumentNullException("The given y-coordinate must not be null!");

            if (z == null)
                throw new ArgumentNullException("The given z-coordinate must not be null!");

            #endregion

            if (Center.DistanceTo(new Voxel<T>(x, y, z)).IsLessThanOrEquals(Radius))
                return true;

            return false;

        }

        #endregion

        #region Contains(Voxel)

        /// <summary>
        /// Checks if the given voxel is located
        /// within this sphere.
        /// </summary>
        /// <param name="Voxel">A voxel.</param>
        /// <returns>True if the voxel is located within this sphere; False otherwise.</returns>
        public Boolean Contains(IVoxel<T> Voxel)
        {

            #region Initial Checks

            if (Voxel == null)
                throw new ArgumentNullException("The given voxel must not be null!");

            #endregion

            if (Center.DistanceTo(Voxel).IsLessThanOrEquals(Radius))
                return true;

            return false;

        }

        #endregion

        #region Contains(Sphere)

        /// <summary>
        /// Checks if the given sphere is located
        /// within this sphere.
        /// </summary>
        /// <param name="Sphere">A sphere of type T.</param>
        /// <returns>True if the sphere is located within this sphere; False otherwise.</returns>
        public Boolean Contains(ISphere<T> Sphere)
        {

            #region Initial Checks

            if (Sphere == null)
                throw new ArgumentNullException("The given sphere must not be null!");

            #endregion

            if (Center.DistanceTo(Sphere.Center).IsLessThanOrEquals(Math.Sub(Radius, Sphere.Radius)))
                return true;

            return true;

        }

        #endregion

        #region Overlaps(Sphere)

        /// <summary>
        /// Checks if the given sphere shares some
        /// area with this sphere.
        /// </summary>
        /// <param name="Sphere">A sphere of type T.</param>
        /// <returns>True if the sphere shares some area with this sphere; False otherwise.</returns>
        public Boolean Overlaps(ISphere<T> Sphere)
        {

            #region Initial Checks

            if (Sphere == null)
                throw new ArgumentNullException("The given sphere must not be null!");

            #endregion

            if (Center.DistanceTo(Sphere.Center).IsLessThanOrEquals(Math.Add(Radius, Sphere.Radius)))
                return true;

            return true;

        }

        #endregion


        #region Operator overloadings

        #region Operator == (Sphere1, Sphere2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Sphere1">A Sphere&lt;T&gt;.</param>
        /// <param name="Sphere2">Another Sphere&lt;T&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Sphere<T> Sphere1, Sphere<T> Sphere2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(Sphere1, Sphere2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) Sphere1 == null) || ((Object) Sphere2 == null))
                return false;

            return Sphere1.Equals(Sphere2);

        }

        #endregion

        #region Operator != (Sphere1, Sphere2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Sphere1">A Sphere&lt;T&gt;.</param>
        /// <param name="Sphere2">Another Sphere&lt;T&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Sphere<T> Sphere1, Sphere<T> Sphere2)
        {
            return !(Sphere1 == Sphere2);
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

            // Check if the given object is an Sphere<T>.
            var SphereT = (Sphere<T>) Object;
            if ((Object) SphereT == null)
                return false;

            return this.Equals(SphereT);

        }

        #endregion

        #region Equals(ISphere)

        /// <summary>
        /// Compares two spheres for equality.
        /// </summary>
        /// <param name="ISphere">A sphere to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(ISphere<T> ISphere)
        {

            if ((Object) ISphere == null)
                return false;

            return this.Left.  Equals(ISphere.Left)  &&
                   this.Top.   Equals(ISphere.Top)   &&
                   this.Radius.Equals(ISphere.Radius);

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
            return Left.GetHashCode() ^ 1 + Top.GetHashCode() ^ 2 + Front.GetHashCode() ^ 3 + Radius.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Return a string represtentation of this object.
        /// </summary>
        public override String ToString()
        {
            return String.Format("Left={0}, Top={1}, Front={2}, Radius={3}",
                                 Left.  ToString(),
                                 Top.   ToString(),
                                 Front. ToString(),
                                 Radius.ToString());
        }

        #endregion

    }

}
