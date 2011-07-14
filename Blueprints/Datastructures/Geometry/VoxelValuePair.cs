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
    /// A voxel of type T together with a value of type TValue.
    /// </summary>
    /// <typeparam name="T">The internal type of the voxel.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class VoxelValuePair<T, TValue> : Voxel<T>, IVoxelValuePair<T, TValue>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Properties

        #region Value

        /// <summary>
        /// The Value.
        /// </summary>
        public TValue Value { get; private set; }

        #endregion

        #endregion

        #region Constructor(s)

        #region VoxelValuePair(X, Y, Z, Value)

        /// <summary>
        /// Create a voxel of type T together with a value of type TValue.
        /// </summary>
        /// <param name="X">The X-coordinate.</param>
        /// <param name="Y">The Y-coordinate.</param>
        /// <param name="Z">The Z-coordinate.</param>
        /// <param name="Value">The value.</param>
        public VoxelValuePair(T X, T Y, T Z, TValue Value)
            : base(X, Y, Z)
        {
            this.Value = Value;
        }

        #endregion

        #endregion


        #region Operator overloadings

        #region Operator == (VoxelValuePair1, VoxelValuePair2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VoxelValuePair1">A VoxelValuePair.</param>
        /// <param name="VoxelValuePair2">Another VoxelValuePair.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (VoxelValuePair<T, TValue> VoxelValuePair1, VoxelValuePair<T, TValue> VoxelValuePair2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(VoxelValuePair1, VoxelValuePair2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) VoxelValuePair1 == null) || ((Object) VoxelValuePair2 == null))
                return false;

            return VoxelValuePair1.Equals(VoxelValuePair2);

        }

        #endregion

        #region Operator != (VoxelValuePair1, VoxelValuePair2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VoxelValuePair1">A VoxelValuePair.</param>
        /// <param name="VoxelValuePair2">Another VoxelValuePair.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (VoxelValuePair<T, TValue> VoxelValuePair1, VoxelValuePair<T, TValue> VoxelValuePair2)
        {
            return !(VoxelValuePair1 == VoxelValuePair2);
        }

        #endregion

        #endregion

        #region IComparable Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public override Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given object must not be null!");

            // Check if the given object is an IVoxelValuePair<T, TValue>.
            var IVoxelValuePair = (IVoxelValuePair<T, TValue>) Object;
            if ((Object) IVoxelValuePair != null)
                return IVoxelValuePair.CompareTo(this);

            // Check if the given object is an IVoxel<T>.
            var IVoxel = (IVoxel<T>) Object;
            if ((Object) IVoxel != null)
                return IVoxel.CompareTo(this);

            throw new ArgumentException("The given object is neither a VoxelValuePair<T, TValue> nor a Voxel<T>!");

        }

        #endregion

        #region CompareTo(IVoxelT)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="IVoxelValuePair">An object to compare with.</param>
        public Int32 CompareTo(IVoxelValuePair<T, TValue> IVoxelValuePair)
        {
            return base.CompareTo(IVoxelValuePair);
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

            // Check if the given object is an IVoxelValuePair<T, TValue>.
            var IVoxelValuePair = (IVoxelValuePair<T, TValue>) Object;
            if ((Object) IVoxelValuePair != null)
                return this.Equals(IVoxelValuePair);

            // Check if the given object is an IVoxel<T>.
            var IVoxel = (IVoxel<T>) Object;
            if ((Object) IVoxel != null)
                return this.Equals(IVoxel);

            return false;

        }

        #endregion

        #region Equals(IVoxelValuePair)

        /// <summary>
        /// Compares two voxels for equality.
        /// </summary>
        /// <param name="IVoxelValuePair">A voxel to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IVoxelValuePair<T, TValue> IVoxelValuePair)
        {

            if ((Object) IVoxelValuePair == null)
                return false;

            return X.Equals(IVoxelValuePair.X) &&
                   Y.Equals(IVoxelValuePair.Y) &&
                   Z.Equals(IVoxelValuePair.Z);

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
            return String.Format("X={0}, Y={1}, Z={2}",
                                 X.ToString(),
                                 Y.ToString(),
                                 Z.ToString());
        }

        #endregion
        
    }

}
