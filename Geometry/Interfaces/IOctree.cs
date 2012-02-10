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

#endregion

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// A octree is an indexing structure for 3-dimensional spartial data.
    /// It stores the given maximum number of voxels and forkes itself
    /// into eight subtrees if this number becomes larger.
    /// Note: This datastructure is not self-balancing!
    /// </summary>
    /// <typeparam name="T">The internal datatype of the octree.</typeparam>
    public interface IOctree<T> : IEnumerable<IVoxel<T>>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Events

        /// <summary>
        /// An event to notify about an octree split happening.
        /// </summary>
        event OctreeSplitEventHandler<T> OnTreeSplit;

        #endregion

        #region Properties

        /// <summary>
        /// The maximum number of embedded elements before
        /// four child node will be created.
        /// </summary>
        UInt32 MaxNumberOfEmbeddedVoxels { get; }

        /// <summary>
        /// Return the number of embedded voxels
        /// stored within this Octree(Node).
        /// </summary>
        UInt64 EmbeddedCount{ get; }

        /// <summary>
        /// Return the number of voxels stored
        /// within the entire octree.
        /// </summary>
        UInt64 Count { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Add a voxel to the Octree.
        /// </summary>
        /// <param name="Voxel">A voxel of type T.</param>
        void Add(IVoxel<T> Voxel);

        /// <summary>
        /// Return all voxels matching the given voxelselector delegate.
        /// </summary>
        /// <param name="VoxelSelector">A delegate selecting which voxels to return.</param>
        IEnumerable<IVoxel<T>> Get(VoxelSelector<T> VoxelSelector);

        /// <summary>
        /// Return all voxels within the given cube.
        /// </summary>
        /// <param name="Cube">A cube selecting which voxels to return.</param>
        IEnumerable<IVoxel<T>> Get(ICube<T> Cube);

        /// <summary>
        /// Remove a voxel from the Octree.
        /// </summary>
        /// <param name="Voxel">A voxel of type T.</param>
        void Remove(IVoxel<T> Voxel);

        /// <summary>
        /// Remove all voxels located within the given cube.
        /// </summary>
        /// <param name="Cube">A cube selecting which voxels to remove.</param>
        void Remove(ICube<T> Cube);

        #endregion

    }

}
