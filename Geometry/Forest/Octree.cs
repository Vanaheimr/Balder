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

    #region OctreeSplitEventHandler<T>(Octree, Voxel)

    /// <summary>
    /// An event handler delegate definition whenever an
    /// octree splits an internal cube.
    /// </summary>
    /// <typeparam name="T">The type of the Octree.</typeparam>
    /// <param name="Octree">The sending octree.</param>
    /// <param name="Voxel">The voxel causing the split.</param>
    public delegate void OctreeSplitEventHandler<T>(Octree<T> Octree, IVoxel<T> Voxel)
        where T : IEquatable<T>, IComparable<T>, IComparable;

    #endregion

    #region Octree<T>

    /// <summary>
    /// A Octree is an indexing structure for two-dimensional spartial data.
    /// It stores the given maximum number of voxels and forkes itself
    /// into eight subtrees if this number becomes larger.
    /// Note: This datastructure is not self-balancing!
    /// </summary>
    /// <typeparam name="T">The internal datatype of the octree.</typeparam>
    public class Octree<T> : Cube<T>, IOctree<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Data

        private Octree<T> Subtree1;
        private Octree<T> Subtree2;
        private Octree<T> Subtree3;
        private Octree<T> Subtree4;
        private Octree<T> Subtree5;
        private Octree<T> Subtree6;
        private Octree<T> Subtree7;
        private Octree<T> Subtree8;

        private HashSet<IVoxel<T>> EmbeddedVoxels;

        #endregion

        #region Events

        #region OnTreeSplit

        /// <summary>
        /// An event to notify about an octree split happening.
        /// </summary>
        public event OctreeSplitEventHandler<T> OnTreeSplit;

        #endregion

        #endregion

        #region Properties

        #region MaxNumberOfEmbeddedVoxels

        /// <summary>
        /// The maximum number of embedded voxels before
        /// eight subtrees will be created.
        /// </summary>
        public UInt32 MaxNumberOfEmbeddedVoxels { get; private set; }

        #endregion

        #region EmbeddedCount

        /// <summary>
        /// Return the number of embedded voxels
        /// stored within this octree(-node).
        /// </summary>
        public UInt64 EmbeddedCount
        {
            get
            {
                return (UInt64) EmbeddedVoxels.Count;
            }
        }

        #endregion

        #region Count

        /// <summary>
        /// Return the number of voxels stored
        /// within the entire octree.
        /// </summary>
        public UInt64 Count
        {
            get
            {

                var _Count = (UInt64) EmbeddedVoxels.Count;

                if (Subtree1 != null)
                    _Count += Subtree1.Count;

                if (Subtree2 != null)
                    _Count += Subtree2.Count;

                if (Subtree3 != null)
                    _Count += Subtree3.Count;

                if (Subtree4 != null)
                    _Count += Subtree4.Count;

                if (Subtree5 != null)
                    _Count += Subtree5.Count;

                if (Subtree6 != null)
                    _Count += Subtree6.Count;

                if (Subtree7 != null)
                    _Count += Subtree7.Count;

                if (Subtree8 != null)
                    _Count += Subtree8.Count;

                return _Count;

            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region Octree(Left, Top, Front, Right, Bottom, Behind, MaxNumberOfEmbeddedVoxels = 256)

        /// <summary>
        /// Create a octree of type T.
        /// </summary>
        /// <param name="Left">The left-coordinate.</param>
        /// <param name="Top">The top-coordinate.</param>
        /// <param name="Front">The front-coordinate.</param>
        /// <param name="Right">The right-coordinate.</param>
        /// <param name="Bottom">The bottom-coordinate.</param>
        /// <param name="Behind">The behind-coordinate.</param>
        /// <param name="MaxNumberOfEmbeddedVoxels">The maximum number of embedded voxels before eight subtrees will be created.</param>
        public Octree(T Left, T Top, T Front, T Right, T Bottom, T Behind, UInt32 MaxNumberOfEmbeddedVoxels = 256)
            : base(Left, Top, Front, Right, Bottom, Behind)
        {

            if (Width.Equals(default(T)))
                throw new OT_ZeroDimensionException<T>(this, "Width == 0!");

            if (Height.Equals(default(T)))
                throw new OT_ZeroDimensionException<T>(this, "Height == 0!");

            if (Depth.Equals(default(T)))
                throw new OT_ZeroDimensionException<T>(this, "Depth == 0!");

            this.MaxNumberOfEmbeddedVoxels = MaxNumberOfEmbeddedVoxels;
            this.EmbeddedVoxels            = new HashSet<IVoxel<T>>();

        }

        #endregion

        #region Octree(Voxel1, Voxel2, MaxNumberOfEmbeddedVoxels = 256)

        /// <summary>
        /// Create a octree of type T.
        /// </summary>
        /// <param name="Voxel1">A voxel of type T.</param>
        /// <param name="Voxel2">A voxel of type T.</param>
        /// <param name="MaxNumberOfEmbeddedVoxels">The maximum number of embedded voxels before eight subtrees will be created.</param>
        public Octree(Voxel<T> Voxel1, Voxel<T> Voxel2, UInt32 MaxNumberOfEmbeddedVoxels = 256)
            : base(Voxel1, Voxel2)
        {

            if (Voxel1.Equals(Voxel2))
                throw new OT_ZeroDimensionException<T>(this, "Width == 0 && Height == 0 && Depth == 0!");

            if (Width.Equals(default(T)))
                throw new OT_ZeroDimensionException<T>(this, "Width == 0!");

            if (Height.Equals(default(T)))
                throw new OT_ZeroDimensionException<T>(this, "Height == 0!");

            if (Depth.Equals(default(T)))
                throw new OT_ZeroDimensionException<T>(this, "Depth == 0!");

            this.MaxNumberOfEmbeddedVoxels = MaxNumberOfEmbeddedVoxels;
            this.EmbeddedVoxels            = new HashSet<IVoxel<T>>();

        }

        #endregion

        #region Octree(Voxel, Width, Height, Depth, MaxNumberOfEmbeddedVoxels = 256)

        /// <summary>
        /// Create a octree of type T.
        /// </summary>
        /// <param name="Voxel">A voxel of type T in the upper left corner of the octree.</param>
        /// <param name="Width">The width of the octree.</param>
        /// <param name="Height">The height of the octree.</param>
        /// <param name="Depth">The depth of the octree.</param>
        /// <param name="MaxNumberOfEmbeddedVoxels">The maximum number of embedded voxels before eight subtrees will be created.</param>
        public Octree(Voxel<T> Voxel, T Width, T Height, T Depth, UInt32 MaxNumberOfEmbeddedVoxels = 256)
            : base(Voxel, Width, Height, Depth)
        {

            if (Width.Equals(default(T)))
                throw new OT_ZeroDimensionException<T>(this, "Width == 0!");

            if (Height.Equals(default(T)))
                throw new OT_ZeroDimensionException<T>(this, "Height == 0!");

            if (Depth.Equals(default(T)))
                throw new OT_ZeroDimensionException<T>(this, "Depth == 0!");

            this.MaxNumberOfEmbeddedVoxels = MaxNumberOfEmbeddedVoxels;
            this.EmbeddedVoxels            = new HashSet<IVoxel<T>>();

        }

        #endregion

        #endregion


        #region Add(Voxel)

        /// <summary>
        /// Add a voxel to the octree.
        /// </summary>
        /// <param name="Voxel">A voxel of type T.</param>
        public void Add(IVoxel<T> Voxel)
        {

            if (this.Contains(Voxel))
            {

                #region Check subtrees...

                if      (Subtree1 != null && Subtree1.Contains(Voxel))
                    Subtree1.Add(Voxel);
                
                else if (Subtree2 != null && Subtree2.Contains(Voxel))
                    Subtree2.Add(Voxel);
                
                else if (Subtree3 != null && Subtree3.Contains(Voxel))
                    Subtree3.Add(Voxel);
                
                else if (Subtree4 != null && Subtree4.Contains(Voxel))
                    Subtree4.Add(Voxel);

                else if (Subtree5 != null && Subtree5.Contains(Voxel))
                    Subtree5.Add(Voxel);

                else if (Subtree6 != null && Subtree6.Contains(Voxel))
                    Subtree6.Add(Voxel);

                else if (Subtree7 != null && Subtree7.Contains(Voxel))
                    Subtree7.Add(Voxel);

                else if (Subtree8 != null && Subtree8.Contains(Voxel))
                    Subtree8.Add(Voxel);

                #endregion

                #region ...or the embedded data.

                else if (EmbeddedVoxels.Count < MaxNumberOfEmbeddedVoxels)
                    EmbeddedVoxels.Add(Voxel);

                #endregion

                #region If necessary create subtrees...

                else
                {

                    #region Create Subtrees

                    if (Subtree1 == null)
                    {
                        Subtree1 = new Octree<T>(Left,
                                                 Top,
                                                 Front,
                                                 Math.Add(Left,  Math.Div2(Width)),
                                                 Math.Add(Top,   Math.Div2(Height)),
                                                 Math.Add(Front, Math.Div2(Depth)),
                                                 MaxNumberOfEmbeddedVoxels);
                        Subtree1.OnTreeSplit += OnTreeSplit;
                    }

                    if (Subtree2 == null)
                    {
                        Subtree2 = new Octree<T>(Math.Add(Left,  Math.Div2(Width)),
                                                 Top,
                                                 Front,
                                                 Right,
                                                 Math.Add(Top,   Math.Div2(Height)),
                                                 Math.Add(Front, Math.Div2(Depth)),
                                                 MaxNumberOfEmbeddedVoxels);
                        Subtree2.OnTreeSplit += OnTreeSplit;
                    }

                    if (Subtree3 == null)
                    {
                        Subtree3 = new Octree<T>(Left,
                                                 Math.Add(Top,   Math.Div2(Height)),
                                                 Front,
                                                 Math.Add(Left,  Math.Div2(Width)),
                                                 Bottom,
                                                 Math.Add(Front, Math.Div2(Depth)),
                                                 MaxNumberOfEmbeddedVoxels);
                        Subtree3.OnTreeSplit += OnTreeSplit;
                    }

                    if (Subtree4 == null)
                    {
                        Subtree4 = new Octree<T>(Math.Add(Left,  Math.Div2(Width)),
                                                 Math.Add(Top,   Math.Div2(Height)),
                                                 Front,
                                                 Right,
                                                 Bottom,
                                                 Math.Add(Front, Math.Div2(Depth)),
                                                 MaxNumberOfEmbeddedVoxels);
                        Subtree4.OnTreeSplit += OnTreeSplit;
                    }

                    if (Subtree5 == null)
                    {
                        Subtree5 = new Octree<T>(Left,
                                                 Top,
                                                 Math.Add(Front, Math.Div2(Depth)),
                                                 Math.Add(Left,  Math.Div2(Width)),
                                                 Math.Add(Top,   Math.Div2(Height)),
                                                 Behind,
                                                 MaxNumberOfEmbeddedVoxels);
                        Subtree5.OnTreeSplit += OnTreeSplit;
                    }

                    if (Subtree6 == null)
                    {
                        Subtree6 = new Octree<T>(Math.Add(Left,  Math.Div2(Width)),
                                                 Top,
                                                 Math.Add(Front, Math.Div2(Depth)),
                                                 Right,
                                                 Math.Add(Top,   Math.Div2(Height)),
                                                 Behind,
                                                 MaxNumberOfEmbeddedVoxels);
                        Subtree6.OnTreeSplit += OnTreeSplit;
                    }

                    if (Subtree7 == null)
                    {
                        Subtree7 = new Octree<T>(Left,
                                                 Math.Add(Top, Math.Div2(Height)),
                                                 Math.Add(Front, Math.Div2(Depth)),
                                                 Math.Add(Left, Math.Div2(Width)),
                                                 Bottom,
                                                 Behind,
                                                 MaxNumberOfEmbeddedVoxels);
                        Subtree7.OnTreeSplit += OnTreeSplit;
                    }

                    if (Subtree8 == null)
                    {
                        Subtree8 = new Octree<T>(Math.Add(Left, Math.Div2(Width)),
                                                 Math.Add(Top, Math.Div2(Height)),
                                                 Math.Add(Front, Math.Div2(Depth)),
                                                 Right,
                                                 Bottom,
                                                 Behind,
                                                 MaxNumberOfEmbeddedVoxels);
                        Subtree8.OnTreeSplit += OnTreeSplit;
                    }

                    #endregion

                    #region Fire OctreeSplit event

                    if (OnTreeSplit != null)
                        OnTreeSplit(this, Voxel);

                    #endregion

                    #region Move all embedded data into the subtrees

                    EmbeddedVoxels.Add(Voxel);

                    foreach (var _Voxel in EmbeddedVoxels)
                    {

                        if      (Subtree1.Contains(_Voxel))
                            Subtree1.Add(_Voxel);

                        else if (Subtree2.Contains(_Voxel))
                            Subtree2.Add(_Voxel);

                        else if (Subtree3.Contains(_Voxel))
                            Subtree3.Add(_Voxel);

                        else if (Subtree4.Contains(_Voxel))
                            Subtree4.Add(_Voxel);

                        else if (Subtree5.Contains(_Voxel))
                            Subtree5.Add(_Voxel);

                        else if (Subtree6.Contains(_Voxel))
                            Subtree6.Add(_Voxel);

                        else if (Subtree7.Contains(_Voxel))
                            Subtree7.Add(_Voxel);

                        else if (Subtree8.Contains(_Voxel))
                            Subtree8.Add(_Voxel);

                        else
                            throw new Exception("Mist!");

                    }

                    EmbeddedVoxels.Clear();

                    #endregion

                }

                #endregion

            }

            else
                throw new OT_OutOfBoundsException<T>(this, Voxel);

        }

        #endregion

        #region Get(VoxelSelector)

        /// <summary>
        /// Return all voxels matching the given voxelselector delegate.
        /// </summary>
        /// <param name="VoxelSelector">A delegate selecting which voxels to return.</param>
        public IEnumerable<IVoxel<T>> Get(VoxelSelector<T> VoxelSelector)
        {

            #region Initial Checks

            if (VoxelSelector == null)
                throw new ArgumentNullException("The given VoxelSelector must not be null!");

            #endregion

            #region Check embedded voxels

            foreach (var _Voxel in EmbeddedVoxels)
                if (VoxelSelector(_Voxel))
                    yield return _Voxel;

            #endregion

            #region Check subtrees

            if (Subtree1 != null)
                foreach (var _Voxel in Subtree1.Get(VoxelSelector))
                    yield return _Voxel;

            if (Subtree2 != null)
                foreach (var _Voxel in Subtree2.Get(VoxelSelector))
                    yield return _Voxel;

            if (Subtree3 != null)
                foreach (var _Voxel in Subtree3.Get(VoxelSelector))
                    yield return _Voxel;

            if (Subtree4 != null)
                foreach (var _Voxel in Subtree4.Get(VoxelSelector))
                    yield return _Voxel;

            #endregion

        }

        #endregion

        #region Get(Cube)

        /// <summary>
        /// Return all voxels within the given cube.
        /// </summary>
        /// <param name="Cube">A cube selecting which voxels to return.</param>
        public IEnumerable<IVoxel<T>> Get(ICube<T> Cube)
        {

            #region Initial Checks

            if (Cube == null)
                throw new ArgumentNullException("The given cube must not be null!");

            #endregion

            #region Check embedded voxels

            foreach (var _Voxel in EmbeddedVoxels)
                if (Cube.Contains(_Voxel))
                    yield return _Voxel;

            #endregion

            #region Check subtrees

            if (Subtree1 != null)
                if (Subtree1.Overlaps(Cube))
                    foreach (var _Voxel in Subtree1.Get(Cube))
                        yield return _Voxel;

            if (Subtree2 != null)
                if (Subtree2.Overlaps(Cube))
                    foreach (var _Voxel in Subtree2.Get(Cube))
                        yield return _Voxel;

            if (Subtree3 != null)
                if (Subtree3.Overlaps(Cube))
                    foreach (var _Voxel in Subtree3.Get(Cube))
                        yield return _Voxel;

            if (Subtree4 != null)
                if (Subtree4.Overlaps(Cube))
                    foreach (var _Voxel in Subtree4.Get(Cube))
                        yield return _Voxel;

            if (Subtree5 != null)
                if (Subtree5.Overlaps(Cube))
                    foreach (var _Voxel in Subtree5.Get(Cube))
                        yield return _Voxel;

            if (Subtree6 != null)
                if (Subtree6.Overlaps(Cube))
                    foreach (var _Voxel in Subtree6.Get(Cube))
                        yield return _Voxel;

            if (Subtree7 != null)
                if (Subtree7.Overlaps(Cube))
                    foreach (var _Voxel in Subtree7.Get(Cube))
                        yield return _Voxel;

            if (Subtree8 != null)
                if (Subtree8.Overlaps(Cube))
                    foreach (var _Voxel in Subtree8.Get(Cube))
                        yield return _Voxel;

            #endregion

        }

        #endregion

        #region Remove(Voxel)

        /// <summary>
        /// Remove a voxel from the octree.
        /// </summary>
        /// <param name="Voxel">A voxel of type T.</param>
        public void Remove(IVoxel<T> Voxel)
        {
            
            #region Initial Checks

            if (Voxel == null)
                throw new ArgumentNullException("The given voxel must not be null!");

            #endregion

            #region Remove embedded voxel

            EmbeddedVoxels.Remove(Voxel);

            #endregion

            #region Check subtrees

            if (Subtree1 != null)
                if (Subtree1.Contains(Voxel))
                    Subtree1.Remove(Voxel);

            if (Subtree2 != null)
                if (Subtree2.Contains(Voxel))
                    Subtree2.Remove(Voxel);

            if (Subtree3 != null)
                if (Subtree3.Contains(Voxel))
                    Subtree3.Remove(Voxel);

            if (Subtree4 != null)
                if (Subtree4.Contains(Voxel))
                    Subtree4.Remove(Voxel);

            if (Subtree5 != null)
                if (Subtree5.Contains(Voxel))
                    Subtree5.Remove(Voxel);

            if (Subtree6 != null)
                if (Subtree6.Contains(Voxel))
                    Subtree6.Remove(Voxel);

            if (Subtree7 != null)
                if (Subtree7.Contains(Voxel))
                    Subtree7.Remove(Voxel);

            if (Subtree8 != null)
                if (Subtree8.Contains(Voxel))
                    Subtree8.Remove(Voxel);

            #endregion

        }

        #endregion

        #region Remove(Cube)

        /// <summary>
        /// Remove all voxels located within the given cube.
        /// </summary>
        /// <param name="Cube">A cube selecting which voxels to remove.</param>
        public void Remove(ICube<T> Cube)
        {

            #region Initial Checks

            if (Cube == null)
                throw new ArgumentNullException("The given cube must not be null!");

            #endregion

            #region Remove embedded voxel

            lock (this)
            {
            
                var _List = new List<IVoxel<T>>();

                foreach (var _Voxel in EmbeddedVoxels)
                    if (Cube.Contains(_Voxel))
                        _List.Add(_Voxel);

                foreach (var _Voxel in _List)
                    EmbeddedVoxels.Remove(_Voxel);

            }

            #endregion

            #region Check subtrees

            if (Subtree1 != null)
                if (Subtree1.Overlaps(Cube))
                    Subtree1.Remove(Cube);

            if (Subtree2 != null)
                if (Subtree2.Overlaps(Cube))
                    Subtree2.Remove(Cube);

            if (Subtree3 != null)
                if (Subtree3.Overlaps(Cube))
                    Subtree3.Remove(Cube);

            if (Subtree4 != null)
                if (Subtree4.Overlaps(Cube))
                    Subtree4.Remove(Cube);

            if (Subtree5 != null)
                if (Subtree5.Overlaps(Cube))
                    Subtree5.Remove(Cube);

            if (Subtree6 != null)
                if (Subtree6.Overlaps(Cube))
                    Subtree6.Remove(Cube);

            if (Subtree7 != null)
                if (Subtree7.Overlaps(Cube))
                    Subtree7.Remove(Cube);

            if (Subtree8 != null)
                if (Subtree8.Overlaps(Cube))
                    Subtree8.Remove(Cube);

            #endregion

        }

        #endregion


        #region IEnumerable Members

        /// <summary>
        /// Return an enumeration of all stored voxels.
        /// </summary>
        public IEnumerator<IVoxel<T>> GetEnumerator()
        {

            foreach (var _Voxel in EmbeddedVoxels)
                yield return _Voxel;

            if (Subtree1 != null)
                foreach (var _Voxel in Subtree1)
                    yield return _Voxel;

            if (Subtree2 != null)
                foreach (var _Voxel in Subtree2)
                    yield return _Voxel;

            if (Subtree3 != null)
                foreach (var _Voxel in Subtree3)
                    yield return _Voxel;

            if (Subtree4 != null)
                foreach (var _Voxel in Subtree4)
                    yield return _Voxel;

        }

        /// <summary>
        /// Return an enumeration of all stored voxels.
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {

            foreach (var _Voxel in EmbeddedVoxels)
                yield return _Voxel;

            if (Subtree1 != null)
                foreach (var _Voxel in Subtree1)
                    yield return _Voxel;

            if (Subtree2 != null)
                foreach (var _Voxel in Subtree2)
                    yield return _Voxel;

            if (Subtree3 != null)
                foreach (var _Voxel in Subtree3)
                    yield return _Voxel;

            if (Subtree4 != null)
                foreach (var _Voxel in Subtree4)
                    yield return _Voxel;

        }

        #endregion


        #region ToString()

        /// <summary>
        /// Return a string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return String.Format("{0} Element(s); Bounds = [Left={1}, Top={2}, Right={3}, Bottom={4}]",
                                 EmbeddedVoxels.Count,
                                 Left.  ToString(),
                                 Top.   ToString(),
                                 Right. ToString(),
                                 Bottom.ToString());
        }

        #endregion

    }

    #endregion

}
