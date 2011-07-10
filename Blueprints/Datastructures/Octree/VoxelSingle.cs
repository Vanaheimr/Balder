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
    /// A voxel of type Single.
    /// </summary>
    public class VoxelSingle : Voxel<Single>
    {

        #region Constructor(s)

        #region VoxelSingle(x, y, z)

        /// <summary>
        /// Create a voxel of type Single.
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="z">Z</param>
        public VoxelSingle(Single x, Single y, Single z)
            : base(x, y, z)
        { }

        #endregion

        #endregion


        #region Abstract Math Operations

        #region Add(a, b)

        /// <summary>
        /// A method to add two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The addition of a and b: a + b</returns>
        protected override Single Add(Single a, Single b)
        {
            return a + b;
        }

        #endregion

        #region Sub(a, b)

        /// <summary>
        /// A method to sub two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The subtraction of b from a: a - b</returns>
        protected override Single Sub(Single a, Single b)
        {
            return a - b;
        }

        #endregion

        #region Mul(a, b)

        /// <summary>
        /// A method to multiply two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The multiplication of a and b: a * b</returns>
        protected override Single Mul(Single a, Single b)
        {
            return a * b;
        }

        #endregion

        #region Div(a, b)

        /// <summary>
        /// A method to divide two internal datatypes.
        /// </summary>
        /// <param name="a">An object of type T</param>
        /// <param name="b">An object of type T</param>
        /// <returns>The division of a by b: a / b</returns>
        protected override Single Div(Single a, Single b)
        {
            return a / b;
        }

        #endregion

        #endregion


        #region Operator overloadings

        #region Operator == (Voxel1, Voxel2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Voxel1">A Voxel&lt;Single&gt;.</param>
        /// <param name="Voxel2">Another Voxel&lt;Single&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (VoxelSingle Voxel1, VoxelSingle Voxel2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(Voxel1, Voxel2))
                return true;

            // If one is null, but not both, return false.
            if (((Object)Voxel1 == null) || ((Object)Voxel2 == null))
                return false;

            return Voxel1.Equals(Voxel2);

        }

        #endregion

        #region Operator != (Voxel1, Voxel2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Voxel1">A Voxel&lt;Single&gt;.</param>
        /// <param name="Voxel2">Another Voxel&lt;Single&gt;.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (VoxelSingle Voxel1, VoxelSingle Voxel2)
        {
            return !(Voxel1.Equals(Voxel2));
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
            return base.Equals(Object);
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
            return base.GetHashCode();
        }

        #endregion

    }

}
