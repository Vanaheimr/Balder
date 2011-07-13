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
using System.Collections.Generic;

#endregion

namespace de.ahzf.Blueprints
{

    public delegate void OctreeSplitEventHandler<T>(Octree<T> Octree, IVoxel<T> Voxel)
        where T : IEquatable<T>, IComparable<T>, IComparable;

    /// <summary>
    /// A Octree is an indexing structure for two-dimensional data.
    /// Note: This datastructure is not self-balancing!
    /// </summary>
    /// <typeparam name="T">The internal type of the Octree.</typeparam>
    public class Octree<T> : Cube<T>, IEnumerable<IVoxel<T>>
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

        private List<IVoxel<T>> EmbeddedVoxels;

        #endregion

        #region Events

        #region OnTreeSplit

        /// <summary>
        /// An event to inform about an Octree split happening.
        /// </summary>
        public event OctreeSplitEventHandler<T> OnTreeSplit;

        #endregion

        #endregion

        #region Properties

        #region MaxNumberOfEmbeddedData

        /// <summary>
        /// The maximum number of embedded elements before
        /// four child node will be created.
        /// </summary>
        public UInt32 MaxNumberOfEmbeddedData { get; private set; }

        #endregion

        #endregion

        #region Constructor(s)

        #region Octree(Left, Top, Front, Right, Bottom, Behind, MaxNumberOfEmbeddedData = 256)

        /// <summary>
        /// Create a Octree of type T.
        /// </summary>
        /// <param name="Left">The left parameter.</param>
        /// <param name="Top">The top parameter.</param>
        /// <param name="Front">The front parameter.</param>
        /// <param name="Right">The right parameter.</param>
        /// <param name="Bottom">The bottom parameter.</param>
        /// <param name="Behind">The behind parameter.</param>
        /// <param name="MaxNumberOfEmbeddedData">The maximum number of embedded elements before four child node will be created.</param>
        public Octree(T Left, T Top, T Front, T Right, T Bottom, T Behind, UInt32 MaxNumberOfEmbeddedData = 256)
            : base(Left, Top, Front, Right, Bottom, Behind)
        {

            if (Width.Equals(default(T)))
                throw new OT_ZeroDimensionException<T>(this, "Width == 0!");

            if (Height.Equals(default(T)))
                throw new OT_ZeroDimensionException<T>(this, "Height == 0!");

            if (Depth.Equals(default(T)))
                throw new OT_ZeroDimensionException<T>(this, "Depth == 0!");

            this.MaxNumberOfEmbeddedData = MaxNumberOfEmbeddedData;
            this.EmbeddedVoxels          = new List<IVoxel<T>>();

        }

        #endregion

        #region Octree(Voxel1, Voxel2, MaxNumberOfEmbeddedData = 256)

        /// <summary>
        /// Create a Octree of type T.
        /// </summary>
        /// <param name="Voxel1">A voxel of type T.</param>
        /// <param name="Voxel2">A voxel of type T.</param>
        /// <param name="MaxNumberOfEmbeddedData">The maximum number of embedded elements before four child node will be created.</param>
        public Octree(Voxel<T> Voxel1, Voxel<T> Voxel2, UInt32 MaxNumberOfEmbeddedData = 256)
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

            this.MaxNumberOfEmbeddedData = MaxNumberOfEmbeddedData;
            this.EmbeddedVoxels          = new List<IVoxel<T>>();

        }

        #endregion

        #region Octree(Voxel, Width, Height, Depth, MaxNumberOfEmbeddedData = 256)

        /// <summary>
        /// Create a Octree of type T.
        /// </summary>
        /// <param name="Voxel">A voxel of type T in the upper left corner of the OctreeNode.</param>
        /// <param name="Width">The width of the OctreeNode.</param>
        /// <param name="Height">The height of the OctreeNode.</param>
        /// <param name="Depth">The depth of the cube.</param>
        /// <param name="MaxNumberOfEmbeddedData">The maximum number of embedded elements before four child node will be created.</param>
        public Octree(Voxel<T> Voxel, T Width, T Height, T Depth, UInt32 MaxNumberOfEmbeddedData = 256)
            : base(Voxel, Width, Height, Depth)
        {

            if (Width.Equals(default(T)))
                throw new OT_ZeroDimensionException<T>(this, "Width == 0!");

            if (Height.Equals(default(T)))
                throw new OT_ZeroDimensionException<T>(this, "Height == 0!");

            if (Depth.Equals(default(T)))
                throw new OT_ZeroDimensionException<T>(this, "Depth == 0!");

            this.MaxNumberOfEmbeddedData = MaxNumberOfEmbeddedData;
            this.EmbeddedVoxels          = new List<IVoxel<T>>();

        }

        #endregion

        #endregion


        #region Add(Voxel)

        /// <summary>
        /// Add a voxel to the Octree.
        /// </summary>
        /// <param name="Voxel">A voxel of type T.</param>
        public void Add(IVoxel<T> Voxel)
        {

            if (this.Contains(Voxel.X, Voxel.Y, Voxel.Z))
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

                else if (EmbeddedVoxels.Count < MaxNumberOfEmbeddedData)
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
                                                 MaxNumberOfEmbeddedData);
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
                                                 MaxNumberOfEmbeddedData);
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
                                                 MaxNumberOfEmbeddedData);
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
                                                 MaxNumberOfEmbeddedData);
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
                                                 MaxNumberOfEmbeddedData);
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
                                                 MaxNumberOfEmbeddedData);
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
                                                 MaxNumberOfEmbeddedData);
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
                                                 MaxNumberOfEmbeddedData);
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
        /// Return all voxels matching the given voxel selector delegate.
        /// </summary>
        /// <param name="VoxelSelector">A delegate selecting which voxel to return.</param>
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
        /// <param name="Cube">A cube selecting which voxel to return.</param>
        public IEnumerable<IVoxel<T>> Get(ICube<T> Cube)
        {

            #region Initial Checks

            if (Cube == null)
                throw new ArgumentNullException("The given Cube must not be null!");

            #endregion

            return Get(p => Cube.Contains(p));

        }

        #endregion

        #region Remove(Voxel)

        /// <summary>
        /// Remove a voxel from the Octree.
        /// </summary>
        /// <param name="Voxel"></param>
        public void Remove(IVoxel<T> Voxel)
        {
            EmbeddedVoxels.Remove(Voxel);
        }

        #endregion

        #region EmbeddedCount

        /// <summary>
        /// Return the number of embedded voxels
        /// stored within this Octree(Node).
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
        /// within the entire Octree.
        /// </summary>
        public UInt64 Count
        {
            get
            {
                //FIXME: !!!
                return (UInt64) EmbeddedVoxels.Count;
            }
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

}
