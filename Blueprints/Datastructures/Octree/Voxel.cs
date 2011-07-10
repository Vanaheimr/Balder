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
    /// A voxel of type T.
    /// </summary>
    /// <typeparam name="T">The internal type of the voxel.</typeparam>
    public abstract class Voxel<T> : IEquatable<Voxel<T>>, IComparable<Voxel<T>>, IComparable
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Data

        /// <summary>
        /// X
        /// </summary>
        public T X { get; private set; }

        /// <summary>
        /// Y
        /// </summary>
        public T Y { get; private set; }

        /// <summary>
        /// Z
        /// </summary>
        public T Z { get; private set; }

        #endregion

        #region Constructor(s)

        #region Voxel(x, y, z)

        /// <summary>
        /// Create a voxel of type T.
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="z">Z</param>
        public Voxel(T x, T y, T z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        #endregion

        #endregion


        #region Abstract Math Operations

        /// <summary>
        /// A method to add two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The addition of a and b: a + b</returns>
        protected abstract T Add(T a, T b);

        /// <summary>
        /// A method to sub two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The subtraction of b from a: a - b</returns>
        protected abstract T Sub(T a, T b);

        /// <summary>
        /// A method to multiply two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The multiplication of a and b: a * b</returns>
        protected abstract T Mul(T a, T b);

        /// <summary>
        /// A method to divide two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The division of a by b: a / b</returns>
        protected abstract T Div(T a, T b);

        #endregion


        #region Operator overloadings

        #region Operator == (Voxel1, Voxel2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Voxel1">A Voxel&lt;T&gt;.</param>
        /// <param name="Voxel2">Another Voxel&lt;T&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Voxel<T> Voxel1, Voxel<T> Voxel2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(Voxel1, Voxel2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) Voxel1 == null) || ((Object) Voxel2 == null))
                return false;

            return Voxel1.Equals(Voxel2);

        }

        #endregion

        #region Operator != (Voxel1, Voxel2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Voxel1">A Voxel&lt;T&gt;.</param>
        /// <param name="Voxel2">Another Voxel&lt;T&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Voxel<T> Voxel1, Voxel<T> Voxel2)
        {
            return !(Voxel1.Equals(Voxel2));
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

            // Check if the given object is an Voxel<T>.
            var VoxelT = (Voxel<T>) Object;
            if ((Object) VoxelT == null)
                throw new ArgumentException("The given object is not a Voxel<T>!");

            return CompareTo(VoxelT);

        }

        #endregion

        #region CompareTo(VoxelT)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VoxelT">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(Voxel<T> VoxelT)
        {

            if ((Object) VoxelT == null)
                throw new ArgumentNullException("The given Voxel<T> must not be null!");

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

            // Check if the given object is an Voxel<T>.
            var VoxelT = (Voxel<T>) Object;
            if ((Object) VoxelT == null)
                return false;

            return this.Equals(VoxelT);

        }

        #endregion

        #region Equals(VoxelT)

        /// <summary>
        /// Compares two voxels for equality.
        /// </summary>
        /// <param name="VoxelT">A voxel to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(Voxel<T> VoxelT)
        {

            if ((Object) VoxelT == null)
                return false;

            return X.Equals(VoxelT.X) &&
                   Y.Equals(VoxelT.Y) &&
                   Z.Equals(VoxelT.Z);

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
            return X.GetHashCode() ^ 1 + Y.GetHashCode() ^ 2 + Z.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Return a string represtentation of this object.
        /// </summary>
        public override String ToString()
        {
            return String.Format("{{X={0}, Y={1}, Z={2}}}", X.ToString(), Y.ToString(), Z.ToString());
        }

        #endregion

    }

}
