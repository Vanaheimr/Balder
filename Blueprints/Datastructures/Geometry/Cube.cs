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
    /// A cube of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the cube.</typeparam>
    public class Cube<T> : ICube<T>
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
        /// The left-coordinate of the circle.
        /// </summary>
        public T Left   { get; private set; }

        #endregion

        #region Top

        /// <summary>
        /// The top-coordinate of the circle.
        /// </summary>
        public T Top    { get; private set; }

        #endregion

        #region Front

        /// <summary>
        /// Front
        /// </summary>
        public T Front  { get; private set; }

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

            #region Initial Checks

            if (Left   == null)
                throw new ArgumentNullException("The given left coordinate must not be null!");

            if (Top    == null)
                throw new ArgumentNullException("The given top coordinate must not be null!");

            if (Front  == null)
                throw new ArgumentNullException("The given front coordinate must not be null!");

            if (Right  == null)
                throw new ArgumentNullException("The given right coordinate must not be null!");

            if (Bottom == null)
                throw new ArgumentNullException("The given bottom coordinate must not be null!");

            if (Behind == null)
                throw new ArgumentNullException("The given behind coordinate must not be null!");

            #endregion

            this.Math   = MathsFactory<T>.Instance;

            #region Math Checks

            if (Math.Distance(Left, Right).Equals(Math.Zero))
                throw new ArgumentException("The resulting width must not be zero!");

            if (Math.Distance(Top, Bottom).Equals(Math.Zero))
                throw new ArgumentException("The resulting height must not be zero!");

            if (Math.Distance(Front, Behind).Equals(Math.Zero))
                throw new ArgumentException("The resulting depth must not be zero!");

            #endregion

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

            #region Initial Checks

            if (Voxel1 == null)
                throw new ArgumentNullException("The given first voxel must not be null!");

            if (Voxel2 == null)
                throw new ArgumentNullException("The given second voxel must not be null!");

            #endregion

            this.Math   = MathsFactory<T>.Instance;

            #region Math Checks

            if (Math.Distance(Left, Right).Equals(Math.Zero))
                throw new ArgumentException("The resulting width must not be zero!");

            if (Math.Distance(Top, Bottom).Equals(Math.Zero))
                throw new ArgumentException("The resulting height must not be zero!");

            if (Math.Distance(Front, Behind).Equals(Math.Zero))
                throw new ArgumentException("The resulting depth must not be zero!");

            #endregion

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

            #region Initial Checks

            if (Voxel  == null)
                throw new ArgumentNullException("The given voxel must not be null!");

            if (Width  == null)
                throw new ArgumentNullException("The given width must not be null!");

            if (Height == null)
                throw new ArgumentNullException("The given height must not be null!");

            if (Depth  == null)
                throw new ArgumentNullException("The given depth must not be null!");

            #endregion

            this.Math   = MathsFactory<T>.Instance;

            #region Math Checks

            if (Math.Distance(Left, Right).Equals(Math.Zero))
                throw new ArgumentException("The resulting width must not be zero!");

            if (Math.Distance(Top, Bottom).Equals(Math.Zero))
                throw new ArgumentException("The resulting height must not be zero!");

            if (Math.Distance(Front, Behind).Equals(Math.Zero))
                throw new ArgumentException("The resulting depth must not be zero!");

            #endregion

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

            #region Initial Checks

            if (x == null)
                throw new ArgumentNullException("The given x-coordinate must not be null!");

            if (y == null)
                throw new ArgumentNullException("The given y-coordinate must not be null!");

            if (z == null)
                throw new ArgumentNullException("The given z-coordinate must not be null!");

            #endregion

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
        public Boolean Contains(IVoxel<T> Voxel)
        {

            #region Initial Checks

            if (Voxel == null)
                throw new ArgumentNullException("The given voxel must not be null!");

            #endregion
            
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

            #region Initial Checks

            if (Cube == null)
                throw new ArgumentNullException("The given cube must not be null!");

            #endregion


            // Verify that every corner of the given cube
            // is located within this cube

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

        #region Overlaps(Cube)

        /// <summary>
        /// Checks if the given cube shares some
        /// area with this cube.
        /// </summary>
        /// <param name="Cube">A cube of type T.</param>
        /// <returns>True if the cube shares some area with this cube; False otherwise.</returns>
        public Boolean Overlaps(ICube<T> Cube)
        {

            #region Initial Checks

            if (Cube == null)
                throw new ArgumentNullException("The given cube must not be null!");

            #endregion


            // Check if any corner of the given cube
            // is located within this cube

            if (Contains(Cube.Left,  Cube.Top,    Cube.Front))
                return true;

            if (Contains(Cube.Right, Cube.Top,    Cube.Front))
                return true;

            if (Contains(Cube.Left,  Cube.Bottom, Cube.Front))
                return true;

            if (Contains(Cube.Right, Cube.Bottom, Cube.Front))
                return true;

            if (Contains(Cube.Left,  Cube.Top,    Cube.Behind))
                return true;

            if (Contains(Cube.Right, Cube.Top,    Cube.Behind))
                return true;

            if (Contains(Cube.Left,  Cube.Bottom, Cube.Behind))
                return true;

            if (Contains(Cube.Right, Cube.Bottom, Cube.Behind))
                return true;

            return false;

        }

        #endregion


        #region Operator overloadings

        #region Operator == (Cube1, Cube2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Cube1">A Cube&lt;T&gt;.</param>
        /// <param name="Cube2">Another Cube&lt;T&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Cube<T> Cube1, Cube<T> Cube2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(Cube1, Cube2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) Cube1 == null) || ((Object) Cube2 == null))
                return false;

            return Cube1.Equals(Cube2);

        }

        #endregion

        #region Operator != (Cube1, Cube2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Cube1">A Cube&lt;T&gt;.</param>
        /// <param name="Cube2">Another Cube&lt;T&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Cube<T> Cube1, Cube<T> Cube2)
        {
            return !(Cube1 == Cube2);
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
            return String.Format("Left={0}, Top={1}, Front={2}, Right={3}, Bottom={4}, Behind={5}",
                                 Left.  ToString(),
                                 Top.   ToString(),
                                 Front. ToString(),
                                 Right. ToString(),
                                 Bottom.ToString(),
                                 Behind.ToString());
        }

        #endregion

    }

}
