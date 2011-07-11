﻿/*
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
    /// A cube of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the cube.</typeparam>
    public class Cube<T> : AMath<T>, ICube<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Properties

        #region Left

        /// <summary>
        /// Left
        /// </summary>
        public T Left { get; private set; }

        #endregion

        #region Top

        /// <summary>
        /// Top
        /// </summary>
        public T Top { get; private set; }

        #endregion

        #region Front

        /// <summary>
        /// Front
        /// </summary>
        public T Front { get; private set; }

        #endregion

        #region Right

        /// <summary>
        /// Right
        /// </summary>
        public T Right { get; private set; }

        #endregion

        #region Bottom

        /// <summary>
        /// Bottom
        /// </summary>
        public T Bottom { get; private set; }

        #endregion

        #region Behind

        /// <summary>
        /// Behind
        /// </summary>
        public T Behind { get; private set; }

        #endregion


        #region Width

        /// <summary>
        /// The width of the cube.
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
        /// The height of the cube.
        /// </summary>
        public T Height
        {
            get
            {
                return Math.Distance(Top, Bottom);
            }
        }

        #endregion

        #region Depth

        /// <summary>
        /// The depth of the cube.
        /// </summary>
        public T Depth
        {
            get
            {
                return Math.Distance(Front, Behind);
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region Cube(Left, Top, Front, Right, Bottom, Behind)

        /// <summary>
        /// Create a cube of type T.
        /// </summary>
        /// <param name="Left">The left parameter.</param>
        /// <param name="Top">The top parameter.</param>
        /// <param name="Front">The front parameter.</param>
        /// <param name="Right">The right parameter.</param>
        /// <param name="Bottom">The bottom parameter.</param>
        /// <param name="Behind">The behind parameter.</param>
        public Cube(T Left, T Top, T Front, T Right, T Bottom, T Behind)
        {
            this.Left   = Math.Min(Left,  Right);
            this.Top    = Math.Min(Top,   Bottom);
            this.Front  = Math.Min(Front, Behind);
            this.Right  = Math.Max(Left,  Right);
            this.Bottom = Math.Max(Top,   Bottom);
            this.Behind = Math.Max(Front, Behind);
        }

        #endregion

        #region Cube(Voxel1, Voxel2)

        /// <summary>
        /// Create a cube of type T.
        /// </summary>
        /// <param name="Voxel1">A Voxel of type T.</param>
        /// <param name="Voxel2">A Voxel of type T.</param>
        public Cube(Voxel<T> Voxel1, Voxel<T> Voxel2)
        {
            this.Left   = Math.Min(Voxel1.X, Voxel2.X);
            this.Top    = Math.Min(Voxel1.Y, Voxel2.Y);
            this.Front  = Math.Min(Voxel1.Z, Voxel2.Z);
            this.Right  = Math.Max(Voxel1.X, Voxel2.X);
            this.Bottom = Math.Max(Voxel1.Y, Voxel2.Y);
            this.Behind = Math.Max(Voxel1.Z, Voxel2.Z);
        }

        #endregion

        #region Cube(Voxel, Width, Height)

        /// <summary>
        /// Create a cube of type T.
        /// </summary>
        /// <param name="Voxel">A Voxel of type T in the upper left front corner of the cube.</param>
        /// <param name="Width">The width of the cube.</param>
        /// <param name="Height">The height of the cube.</param>
        /// <param name="Depth">The depth of the cube.</param>
        public Cube(Voxel<T> Voxel, T Width, T Height, T Depth)
        {
            this.Left   = Voxel.X;
            this.Top    = Voxel.Y;
            this.Front  = Voxel.Z;
            this.Right  = Math.Add(Voxel.X, Width);
            this.Bottom = Math.Add(Voxel.Y, Height);
            this.Behind = Math.Add(Voxel.Z, Depth);
        }

        #endregion

        #endregion


        #region Contains(x, y, z)

        /// <summary>
        /// Checks if the given x-, y- and z-coordinates
        /// are located within this cube.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="z">The z-coordinate.</param>
        /// <returns>True if the coordinates are located within this cube; False otherwise.</returns>
        public Boolean Contains(T x, T y, T z)
        {
            
            if (x.CompareTo(Left)   >= 0 &&
                x.CompareTo(Right)  <= 0 &&
                y.CompareTo(Top)    >= 0 &&
                y.CompareTo(Bottom) <= 0 &&
                z.CompareTo(Front)  >= 0 &&
                z.CompareTo(Behind) <= 0)
                return true;

            return false;

        }

        #endregion

        #region Contains(Voxel)

        /// <summary>
        /// Checks if the given voxel is located
        /// within this cube.
        /// </summary>
        /// <param name="Voxel">A voxel of type T.</param>
        /// <returns>True if the voxel is located within this cube; False otherwise.</returns>
        public Boolean Contains(Voxel<T> Voxel)
        {
            return Contains(Voxel.X, Voxel.Y, Voxel.Z);
        }

        #endregion

        #region Contains(Cube)

        /// <summary>
        /// Checks if the given cube is located
        /// within this cube.
        /// </summary>
        /// <param name="Cube">A cube of type T.</param>
        /// <returns>True if the cube is located within this cube; False otherwise.</returns>
        public Boolean Contains(ICube<T> Cube)
        {

            if (!Contains(Cube.Left,  Cube.Top,    Cube.Front))
                return false;

            if (!Contains(Cube.Right, Cube.Top,    Cube.Front))
                return false;

            if (!Contains(Cube.Left,  Cube.Bottom, Cube.Front))
                return false;

            if (!Contains(Cube.Right, Cube.Bottom, Cube.Front))
                return false;

            if (!Contains(Cube.Left,  Cube.Top,    Cube.Behind))
                return false;

            if (!Contains(Cube.Right, Cube.Top,    Cube.Behind))
                return false;

            if (!Contains(Cube.Left,  Cube.Bottom, Cube.Behind))
                return false;

            if (!Contains(Cube.Right, Cube.Bottom, Cube.Behind))
                return false;

            return true;

        }

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

            // Check if the given object is an Cube<T>.
            var CubeT = (Cube<T>) Object;
            if ((Object) CubeT == null)
                return false;

            return this.Equals(CubeT);

        }

        #endregion

        #region Equals(ICube)

        /// <summary>
        /// Compares two cubes for equality.
        /// </summary>
        /// <param name="ICube">A cube to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(ICube<T> ICube)
        {

            if ((Object) ICube == null)
                return false;

            return this.Left.  Equals(ICube.Left)   &&
                   this.Top.   Equals(ICube.Top)    &&
                   this.Front. Equals(ICube.Front)  &&
                   this.Right. Equals(ICube.Right)  &&
                   this.Bottom.Equals(ICube.Bottom) &&
                   this.Behind.Equals(ICube.Behind);

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
            return Left.GetHashCode() ^ 1 + Top.GetHashCode() ^ 2 + Front.GetHashCode() ^ 3 + Right.GetHashCode() ^ 4 + Bottom.GetHashCode() ^ 5 + Behind.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Return a string represtentation of this object.
        /// </summary>
        public override String ToString()
        {
            return String.Format("{{Left={0}, Top={1}, Front={2}, Right={3}, Bottom={4}, Behind={5}}}",
                                 Left.ToString(),  Top.ToString(),    Front.ToString(),
                                 Right.ToString(), Bottom.ToString(), Behind.ToString());
        }

        #endregion

    }

}
