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

#endregion

namespace de.ahzf.Blueprints
{

    #region OctreeException(Octree, Voxel = null, Message = null, InnerException = null)

    /// <summary>
    /// The base class for all octree exceptions.
    /// </summary>
    /// <typeparam name="T">The internal datatype of the octree.</typeparam>
    public class OctreeException<T> : Exception
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        /// <summary>
        /// The octree causing this exception.
        /// </summary>
        public Octree<T> Octree { get; private set; }

        /// <summary>
        /// An optional voxel causing this exception.
        /// </summary>
        public IVoxel<T>   Voxel    { get; private set; }

        /// <summary>
        /// A general octree exception occurred!
        /// </summary>
        /// <param name="Octree">The octree causing this exception.</param>
        /// <param name="Voxel">An optional voxel causing this exception.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public OctreeException(Octree<T> Octree, IVoxel<T> Voxel = null, String Message = null, Exception InnerException = null)
            : base(Message, InnerException)
        {
            this.Octree = Octree;
            this.Voxel    = Voxel;
        }

    }

    #endregion


    #region OT_ZeroDimensionException(Quadtree, Message = null, InnerException = null)

    /// <summary>
    /// An exception thrown when at least one dimension
    /// of the octree is zero.
    /// </summary>
    /// <typeparam name="T">The internal datatype of the octree.</typeparam>
    public class OT_ZeroDimensionException<T> : OctreeException<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        /// <summary>
        /// An exception thrown when at least one dimension
        /// of the octree is zero.
        /// </summary>
        /// <param name="Octree">The octree causing this exception.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public OT_ZeroDimensionException(Octree<T> Octree, String Message = null, Exception InnerException = null)
            : base(Octree, Message: "The given octree has at least one zero dimension!" + Message, InnerException: InnerException)
        { }

    }

    #endregion

    #region OT_OutOfBoundsException(Octree, Voxel, Message = null, InnerException = null)

    /// <summary>
    /// An exception thrown when the given voxel is
    /// located outside of the octree bounds!
    /// </summary>
    /// <typeparam name="T">The internal datatype of the octree.</typeparam>
    public class OT_OutOfBoundsException<T> : OctreeException<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        /// <summary>
        /// An exception thrown when the given voxel is
        /// located outside of the octree bounds!
        /// </summary>
        /// <param name="Octree">The octree causing this exception.</param>
        /// <param name="Voxel">The voxel causing this exception.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public OT_OutOfBoundsException(Octree<T> Octree, IVoxel<T> Voxel, String Message = null, Exception InnerException = null)
            : base(Octree, Voxel, "The given voxel is located outside of the octree bounds!" + Message, InnerException)
        { }

    }

    #endregion

}
