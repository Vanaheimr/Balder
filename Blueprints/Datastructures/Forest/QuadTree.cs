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

    #region QuadtreeSplitEventHandler<T>(Quadtree, Pixel)

    /// <summary>
    /// An event handler delegate definition whenever an
    /// quadtree splits an internal rectangle.
    /// </summary>
    /// <typeparam name="T">The type of the Quadtree.</typeparam>
    /// <param name="Quadtree">The sending quadtree.</param>
    /// <param name="Pixel">The pixel causing the split.</param>
    public delegate void QuadtreeSplitEventHandler<T>(Quadtree<T> Quadtree, IPixel<T> Pixel)
        where T : IEquatable<T>, IComparable<T>, IComparable;

    #endregion

    #region QuadtreeSplitEventHandler<T, TValue>(Quadtree, Pixel)

    /// <summary>
    /// An event handler delegate definition whenever an
    /// quadtree splits an internal rectangle.
    /// </summary>
    /// <typeparam name="T">The type of the Quadtree.</typeparam>
    /// <param name="Quadtree">The sending quadtree.</param>
    /// <param name="Pixel">The pixel causing the split.</param>
    public delegate void QuadtreeSplitEventHandler<T, TValue>(Quadtree<T, TValue> Quadtree, IPixel<T> Pixel)
        where T : IEquatable<T>, IComparable<T>, IComparable;

    #endregion


    #region Quadtree<T>

    /// <summary>
    /// A quadtree is an indexing structure for 2-dimensional spartial data.
    /// It stores the given maximum number of pixels and forkes itself
    /// into four subtrees if this number becomes larger.
    /// Note: This datastructure is not self-balancing!
    /// </summary>
    /// <typeparam name="T">The internal datatype of the quadtree.</typeparam>
    public class Quadtree<T> : Rectangle<T>, IQuadtree<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Data

        private Quadtree<T> Subtree1;
        private Quadtree<T> Subtree2;
        private Quadtree<T> Subtree3;
        private Quadtree<T> Subtree4;

        private HashSet<IPixel<T>> EmbeddedPixels;

        #endregion

        #region Events

        #region OnTreeSplit

        /// <summary>
        /// An event to notify about a quadtree split happening.
        /// </summary>
        public event QuadtreeSplitEventHandler<T> OnTreeSplit;

        #endregion

        #endregion

        #region Properties

        #region MaxNumberOfEmbeddedPixels

        /// <summary>
        /// The maximum number of embedded pixels before
        /// four subtrees will be created.
        /// </summary>
        public UInt32 MaxNumberOfEmbeddedPixels { get; private set; }

        #endregion

        #region EmbeddedCount

        /// <summary>
        /// Return the number of embedded pixels
        /// stored within this quadtree(-node).
        /// </summary>
        public UInt64 EmbeddedCount
        {
            get
            {
                return (UInt64) EmbeddedPixels.Count;
            }
        }

        #endregion

        #region Count

        /// <summary>
        /// Return the number of pixels stored
        /// within the entire quadtree.
        /// </summary>
        public UInt64 Count
        {
            get
            {
                
                var _Count = (UInt64) EmbeddedPixels.Count;

                if (Subtree1 != null)
                    _Count += Subtree1.Count;

                if (Subtree2 != null)
                    _Count += Subtree2.Count;

                if (Subtree3 != null)
                    _Count += Subtree3.Count;

                if (Subtree4 != null)
                    _Count += Subtree4.Count;

                return _Count;

            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region Quadtree(Left, Top, Right, Bottom, MaxNumberOfEmbeddedPixels = 256)

        /// <summary>
        /// Create a quadtree of type T.
        /// </summary>
        /// <param name="Left">The left-coordinate.</param>
        /// <param name="Top">The top-coordinate.</param>
        /// <param name="Right">The right-coordinate.</param>
        /// <param name="Bottom">The bottom-coordinate.</param>
        /// <param name="MaxNumberOfEmbeddedPixels">The maximum number of embedded pixels before four subtrees will be created.</param>
        public Quadtree(T Left, T Top, T Right, T Bottom, UInt32 MaxNumberOfEmbeddedPixels = 256)
            : base(Left, Top, Right, Bottom)
        {

            if (Width.Equals(default(T)))
                throw new QT_ZeroDimensionException<T>(this, "Width == 0!");

            if (Height.Equals(default(T)))
                throw new QT_ZeroDimensionException<T>(this, "Height == 0!");

            this.MaxNumberOfEmbeddedPixels = MaxNumberOfEmbeddedPixels;
            this.EmbeddedPixels            = new HashSet<IPixel<T>>();

        }

        #endregion

        #region Quadtree(Pixel1, Pixel2, MaxNumberOfEmbeddedPixels = 256)

        /// <summary>
        /// Create a quadtree of type T.
        /// </summary>
        /// <param name="Pixel1">A pixel of type T.</param>
        /// <param name="Pixel2">A pixel of type T.</param>
        /// <param name="MaxNumberOfEmbeddedPixels">The maximum number of embedded pixels before four subtrees will be created.</param>
        public Quadtree(Pixel<T> Pixel1, Pixel<T> Pixel2, UInt32 MaxNumberOfEmbeddedPixels = 256)
            : base(Pixel1, Pixel2)
        {

            if (Pixel1.Equals(Pixel2))
                throw new QT_ZeroDimensionException<T>(this, "Width == 0 && Height == 0!");

            if (Width.Equals(default(T)))
                throw new QT_ZeroDimensionException<T>(this, "Width == 0!");

            if (Height.Equals(default(T)))
                throw new QT_ZeroDimensionException<T>(this, "Height == 0!");

            this.MaxNumberOfEmbeddedPixels = MaxNumberOfEmbeddedPixels;
            this.EmbeddedPixels            = new HashSet<IPixel<T>>();

        }

        #endregion

        #region Quadtree(Pixel, Width, Height, MaxNumberOfEmbeddedPixels = 256)

        /// <summary>
        /// Create a quadtree of type T.
        /// </summary>
        /// <param name="Pixel">A pixel of type T in the upper left corner of the quadtree.</param>
        /// <param name="Width">The width of the quadtree.</param>
        /// <param name="Height">The height of the quadtree.</param>
        /// <param name="MaxNumberOfEmbeddedPixels">The maximum number of embedded pixels before four subtrees will be created.</param>
        public Quadtree(Pixel<T> Pixel, T Width, T Height, UInt32 MaxNumberOfEmbeddedPixels = 256)
            : base(Pixel, Width, Height)
        {

            if (Width.Equals(default(T)))
                throw new QT_ZeroDimensionException<T>(this, "Width == 0!");

            if (Height.Equals(default(T)))
                throw new QT_ZeroDimensionException<T>(this, "Height == 0!");

            this.MaxNumberOfEmbeddedPixels = MaxNumberOfEmbeddedPixels;
            this.EmbeddedPixels            = new HashSet<IPixel<T>>();

        }

        #endregion

        #endregion


        #region Add(Pixel)

        public void Add(T X, T Y)
        {
            Add(new Pixel<T>(X, Y));
        }

        /// <summary>
        /// Add a pixel to the quadtree.
        /// </summary>
        /// <param name="Pixel">A pixel of type T.</param>
        public void Add(IPixel<T> Pixel)
        {

            if (this.Contains(Pixel))
            {

                #region Check subtrees...

                if      (Subtree1 != null && Subtree1.Contains(Pixel))
                    Subtree1.Add(Pixel);
                
                else if (Subtree2 != null && Subtree2.Contains(Pixel))
                    Subtree2.Add(Pixel);
                
                else if (Subtree3 != null && Subtree3.Contains(Pixel))
                    Subtree3.Add(Pixel);
                
                else if (Subtree4 != null && Subtree4.Contains(Pixel))
                    Subtree4.Add(Pixel);

                #endregion

                #region ...or the embedded data.

                else if (EmbeddedPixels.Count < MaxNumberOfEmbeddedPixels)
                    EmbeddedPixels.Add(Pixel);

                #endregion

                #region If necessary create subtrees...

                else
                {

                    #region Create Subtrees

                    if (Subtree1 == null)
                    {
                        Subtree1 = new Quadtree<T>(Left,
                                                   Top,
                                                   Math.Add(Left, Math.Div2(Width)),
                                                   Math.Add(Top,  Math.Div2(Height)),
                                                   MaxNumberOfEmbeddedPixels);
                        Subtree1.OnTreeSplit += OnTreeSplit;
                    }

                    if (Subtree2 == null)
                    {
                        Subtree2 = new Quadtree<T>(Math.Add(Left, Math.Div2(Width)),
                                                   Top,
                                                   Right,
                                                   Math.Add(Top, Math.Div2(Height)),
                                                   MaxNumberOfEmbeddedPixels);
                        Subtree2.OnTreeSplit += OnTreeSplit;
                    }

                    if (Subtree3 == null)
                    {
                        Subtree3 = new Quadtree<T>(Left,
                                                   Math.Add(Top,  Math.Div2(Height)),
                                                   Math.Add(Left, Math.Div2(Width)),
                                                   Bottom,
                                                   MaxNumberOfEmbeddedPixels);
                        Subtree3.OnTreeSplit += OnTreeSplit;
                    }

                    if (Subtree4 == null)
                    {
                        Subtree4 = new Quadtree<T>(Math.Add(Left, Math.Div2(Width)),
                                                   Math.Add(Top,  Math.Div2(Height)),
                                                   Right,
                                                   Bottom,
                                                   MaxNumberOfEmbeddedPixels);
                        Subtree4.OnTreeSplit += OnTreeSplit;
                    }

                    #endregion

                    #region Fire QuadtreeSplit event

                    if (OnTreeSplit != null)
                        OnTreeSplit(this, Pixel);

                    #endregion

                    #region Move all embedded data into the subtrees

                    EmbeddedPixels.Add(Pixel);

                    foreach (var _Pixel in EmbeddedPixels)
                    {

                        if      (Subtree1.Contains(_Pixel))
                            Subtree1.Add(_Pixel);

                        else if (Subtree2.Contains(_Pixel))
                            Subtree2.Add(_Pixel);

                        else if (Subtree3.Contains(_Pixel))
                            Subtree3.Add(_Pixel);

                        else if (Subtree4.Contains(_Pixel))
                            Subtree4.Add(_Pixel);

                        else
                            throw new Exception("Mist!");

                    }

                    EmbeddedPixels.Clear();

                    #endregion

                }

                #endregion

            }

            else
                throw new QT_OutOfBoundsException<T>(this, Pixel);

        }

        #endregion

        #region Get(PixelSelector)

        /// <summary>
        /// Return all pixels matching the given pixelselector delegate.
        /// </summary>
        /// <param name="PixelSelector">A delegate selecting which pixels to return.</param>
        public IEnumerable<IPixel<T>> Get(PixelSelector<T> PixelSelector)
        {

            #region Initial Checks

            if (PixelSelector == null)
                throw new ArgumentNullException("The given PixelSelector must not be null!");

            #endregion

            #region Check embedded pixels

            foreach (var _Pixel in EmbeddedPixels)
                if (PixelSelector(_Pixel))
                    yield return _Pixel;

            #endregion

            #region Check subtrees

            if (Subtree1 != null)
                foreach (var _Pixel in Subtree1.Get(PixelSelector))
                    yield return _Pixel;

            if (Subtree2 != null)
                foreach (var _Pixel in Subtree2.Get(PixelSelector))
                    yield return _Pixel;

            if (Subtree3 != null)
                foreach (var _Pixel in Subtree3.Get(PixelSelector))
                    yield return _Pixel;

            if (Subtree4 != null)
                foreach (var _Pixel in Subtree4.Get(PixelSelector))
                    yield return _Pixel;

            #endregion

        }

        #endregion

        #region Get(Rectangle)

        /// <summary>
        /// Return all pixels within the given rectangle.
        /// </summary>
        /// <param name="Rectangle">A rectangle selecting which pixels to return.</param>
        public IEnumerable<IPixel<T>> Get(IRectangle<T> Rectangle)
        {

            #region Initial Checks

            if (Rectangle == null)
                throw new ArgumentNullException("The given Rectangle must not be null!");

            #endregion

            #region Check embedded pixels

            foreach (var _Pixel in EmbeddedPixels)
                if (Rectangle.Contains(_Pixel))
                    yield return _Pixel;

            #endregion

            #region Check subtrees

            if (Subtree1 != null)
                if (Subtree1.Overlaps(Rectangle))
                    foreach (var _Pixel in Subtree1.Get(Rectangle))
                        yield return _Pixel;

            if (Subtree2 != null)
                if (Subtree2.Overlaps(Rectangle))
                    foreach (var _Pixel in Subtree2.Get(Rectangle))
                        yield return _Pixel;

            if (Subtree3 != null)
                if (Subtree3.Overlaps(Rectangle))
                    foreach (var _Pixel in Subtree3.Get(Rectangle))
                        yield return _Pixel;

            if (Subtree4 != null)
                if (Subtree4.Overlaps(Rectangle))
                    foreach (var _Pixel in Subtree4.Get(Rectangle))
                        yield return _Pixel;

            #endregion

        }

        #endregion

        #region Remove(Pixel)

        /// <summary>
        /// Remove a pixel from the quadtree.
        /// </summary>
        /// <param name="Pixel">A pixel of type T.</param>
        public void Remove(IPixel<T> Pixel)
        {

            #region Initial Checks

            if (Pixel == null)
                throw new ArgumentNullException("The given pixel must not be null!");

            #endregion

            #region Remove embedded voxel

            EmbeddedPixels.Remove(Pixel);

            #endregion

            #region Check subtrees

            if (Subtree1 != null)
                if (Subtree1.Contains(Pixel))
                    Subtree1.Remove(Pixel);

            if (Subtree2 != null)
                if (Subtree2.Contains(Pixel))
                    Subtree2.Remove(Pixel);

            if (Subtree3 != null)
                if (Subtree3.Contains(Pixel))
                    Subtree3.Remove(Pixel);

            if (Subtree4 != null)
                if (Subtree4.Contains(Pixel))
                    Subtree4.Remove(Pixel);

            #endregion

        }

        #endregion

        #region Remove(Rectangle)

        /// <summary>
        /// Remove all pixels located within the given rectangle.
        /// </summary>
        /// <param name="Rectangle">A rectangle selecting which pixels to remove.</param>
        public void Remove(IRectangle<T> Rectangle)
        {

            #region Initial Checks

            if (Rectangle == null)
                throw new ArgumentNullException("The given rectangle must not be null!");

            #endregion

            #region Remove embedded voxel

            lock (this)
            {

                var _List = new List<IPixel<T>>();

                foreach (var _Pixel in EmbeddedPixels)
                    if (Rectangle.Contains(_Pixel))
                        _List.Add(_Pixel);

                foreach (var _Pixel in _List)
                    EmbeddedPixels.Remove(_Pixel);

            }

            #endregion

            #region Check subtrees

            if (Subtree1 != null)
                if (Subtree1.Overlaps(Rectangle))
                    Subtree1.Remove(Rectangle);

            if (Subtree2 != null)
                if (Subtree2.Overlaps(Rectangle))
                    Subtree2.Remove(Rectangle);

            if (Subtree3 != null)
                if (Subtree3.Overlaps(Rectangle))
                    Subtree3.Remove(Rectangle);

            if (Subtree4 != null)
                if (Subtree4.Overlaps(Rectangle))
                    Subtree4.Remove(Rectangle);

            #endregion

        }

        #endregion


        #region IEnumerable Members

        /// <summary>
        /// Return an enumeration of all stored pixels.
        /// </summary>
        public IEnumerator<IPixel<T>> GetEnumerator()
        {

            foreach (var _Pixel in EmbeddedPixels)
                yield return _Pixel;

            if (Subtree1 != null)
                foreach (var _Pixel in Subtree1)
                    yield return _Pixel;

            if (Subtree2 != null)
                foreach (var _Pixel in Subtree2)
                    yield return _Pixel;

            if (Subtree3 != null)
                foreach (var _Pixel in Subtree3)
                    yield return _Pixel;

            if (Subtree4 != null)
                foreach (var _Pixel in Subtree4)
                    yield return _Pixel;

        }

        /// <summary>
        /// Return an enumeration of all stored pixels.
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {

            foreach (var _Pixel in EmbeddedPixels)
                yield return _Pixel;

            if (Subtree1 != null)
                foreach (var _Pixel in Subtree1)
                    yield return _Pixel;

            if (Subtree2 != null)
                foreach (var _Pixel in Subtree2)
                    yield return _Pixel;

            if (Subtree3 != null)
                foreach (var _Pixel in Subtree3)
                    yield return _Pixel;

            if (Subtree4 != null)
                foreach (var _Pixel in Subtree4)
                    yield return _Pixel;

        }

        #endregion


        #region ToString()

        /// <summary>
        /// Return a string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return String.Format("{0} Element(s); Bounds = [Left={1}, Top={2}, Right={3}, Bottom={4}]",
                                 EmbeddedPixels.Count,
                                 Left.  ToString(),
                                 Top.   ToString(),
                                 Right. ToString(),
                                 Bottom.ToString());
        }

        #endregion

    }

    #endregion

    #region Quadtree<T, TValue>

    /// <summary>
    /// A quadtree is an indexing structure for 2-dimensional spartial data.
    /// It stores the given maximum number of pixels and forkes itself
    /// into four subtrees if this number becomes larger.
    /// Note: This datastructure is not self-balancing!
    /// </summary>
    /// <typeparam name="T">The internal datatype of the quadtree.</typeparam>
    public class Quadtree<T, TValue> : Rectangle<T>, IQuadtree<T, TValue>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Data

        private Quadtree<T, TValue> Subtree1;
        private Quadtree<T, TValue> Subtree2;
        private Quadtree<T, TValue> Subtree3;
        private Quadtree<T, TValue> Subtree4;

        private HashSet<IPixelValuePair<T, TValue>> EmbeddedPixels;

        #endregion

        #region Events

        #region OnTreeSplit

        /// <summary>
        /// An event to notify about a quadtree split happening.
        /// </summary>
        public event QuadtreeSplitEventHandler<T, TValue> OnTreeSplit;

        #endregion

        #endregion

        #region Properties

        #region MaxNumberOfEmbeddedPixels

        /// <summary>
        /// The maximum number of embedded pixels before
        /// four subtrees will be created.
        /// </summary>
        public UInt32 MaxNumberOfEmbeddedPixels { get; private set; }

        #endregion

        #region EmbeddedCount

        /// <summary>
        /// Return the number of embedded pixels
        /// stored within this quadtree(-node).
        /// </summary>
        public UInt64 EmbeddedCount
        {
            get
            {
                return (UInt64) EmbeddedPixels.Count;
            }
        }

        #endregion

        #region Count

        /// <summary>
        /// Return the number of pixels stored
        /// within the entire quadtree.
        /// </summary>
        public UInt64 Count
        {
            get
            {
                
                var _Count = (UInt64) EmbeddedPixels.Count;

                if (Subtree1 != null)
                    _Count += Subtree1.Count;

                if (Subtree2 != null)
                    _Count += Subtree2.Count;

                if (Subtree3 != null)
                    _Count += Subtree3.Count;

                if (Subtree4 != null)
                    _Count += Subtree4.Count;

                return _Count;

            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region Quadtree(Left, Top, Right, Bottom, MaxNumberOfEmbeddedPixels = 256)

        /// <summary>
        /// Create a quadtree of type T.
        /// </summary>
        /// <param name="Left">The left-coordinate.</param>
        /// <param name="Top">The top-coordinate.</param>
        /// <param name="Right">The right-coordinate.</param>
        /// <param name="Bottom">The bottom-coordinate.</param>
        /// <param name="MaxNumberOfEmbeddedPixels">The maximum number of embedded pixels before four subtrees will be created.</param>
        public Quadtree(T Left, T Top, T Right, T Bottom, UInt32 MaxNumberOfEmbeddedPixels = 256)
            : base(Left, Top, Right, Bottom)
        {

            if (Width.Equals(default(T)))
                throw new QT_ZeroDimensionException<T, TValue>(this, "Width == 0!");

            if (Height.Equals(default(T)))
                throw new QT_ZeroDimensionException<T, TValue>(this, "Height == 0!");

            this.MaxNumberOfEmbeddedPixels = MaxNumberOfEmbeddedPixels;
            this.EmbeddedPixels            = new HashSet<IPixelValuePair<T, TValue>>();

        }

        #endregion

        #region Quadtree(Pixel1, Pixel2, MaxNumberOfEmbeddedPixels = 256)

        /// <summary>
        /// Create a quadtree of type T.
        /// </summary>
        /// <param name="Pixel1">A pixel of type T.</param>
        /// <param name="Pixel2">A pixel of type T.</param>
        /// <param name="MaxNumberOfEmbeddedPixels">The maximum number of embedded pixels before four subtrees will be created.</param>
        public Quadtree(Pixel<T> Pixel1, Pixel<T> Pixel2, UInt32 MaxNumberOfEmbeddedPixels = 256)
            : base(Pixel1, Pixel2)
        {

            if (Pixel1.Equals(Pixel2))
                throw new QT_ZeroDimensionException<T, TValue>(this, "Width == 0 && Height == 0!");

            if (Width.Equals(default(T)))
                throw new QT_ZeroDimensionException<T, TValue>(this, "Width == 0!");

            if (Height.Equals(default(T)))
                throw new QT_ZeroDimensionException<T, TValue>(this, "Height == 0!");

            this.MaxNumberOfEmbeddedPixels = MaxNumberOfEmbeddedPixels;
            this.EmbeddedPixels            = new HashSet<IPixelValuePair<T, TValue>>();

        }

        #endregion

        #region Quadtree(Pixel, Width, Height, MaxNumberOfEmbeddedPixels = 256)

        /// <summary>
        /// Create a quadtree of type T.
        /// </summary>
        /// <param name="Pixel">A pixel of type T in the upper left corner of the quadtree.</param>
        /// <param name="Width">The width of the quadtree.</param>
        /// <param name="Height">The height of the quadtree.</param>
        /// <param name="MaxNumberOfEmbeddedPixels">The maximum number of embedded pixels before four subtrees will be created.</param>
        public Quadtree(Pixel<T> Pixel, T Width, T Height, UInt32 MaxNumberOfEmbeddedPixels = 256)
            : base(Pixel, Width, Height)
        {

            if (Width.Equals(default(T)))
                throw new QT_ZeroDimensionException<T, TValue>(this, "Width == 0!");

            if (Height.Equals(default(T)))
                throw new QT_ZeroDimensionException<T, TValue>(this, "Height == 0!");

            this.MaxNumberOfEmbeddedPixels = MaxNumberOfEmbeddedPixels;
            this.EmbeddedPixels            = new HashSet<IPixelValuePair<T, TValue>>();

        }

        #endregion

        #endregion


        #region Add(Pixel)

        public void Add(T X, T Y, TValue Value)
        {
            Add(new PixelValuePair<T, TValue>(X, Y, Value));
        }

        public void Add(IPixel<T> IPixel, TValue Value)
        {
            Add(new PixelValuePair<T, TValue>(IPixel.X, IPixel.Y, Value));
        }

        /// <summary>
        /// Add a pixel to the quadtree.
        /// </summary>
        /// <param name="Pixel">A pixel of type T.</param>
        public void Add(IPixelValuePair<T, TValue> IPixelValuePair)
        {

            if (this.Contains(IPixelValuePair))
            {

                #region Check subtrees...

                if      (Subtree1 != null && Subtree1.Contains(IPixelValuePair))
                    Subtree1.Add(IPixelValuePair);

                else if (Subtree2 != null && Subtree2.Contains(IPixelValuePair))
                    Subtree2.Add(IPixelValuePair);

                else if (Subtree3 != null && Subtree3.Contains(IPixelValuePair))
                    Subtree3.Add(IPixelValuePair);

                else if (Subtree4 != null && Subtree4.Contains(IPixelValuePair))
                    Subtree4.Add(IPixelValuePair);

                #endregion

                #region ...or the embedded data.

                else if (EmbeddedPixels.Count < MaxNumberOfEmbeddedPixels)
                    EmbeddedPixels.Add(IPixelValuePair);

                #endregion

                #region If necessary create subtrees...

                else
                {

                    #region Create Subtrees

                    if (Subtree1 == null)
                    {
                        Subtree1 = new Quadtree<T, TValue>(Left,
                                                   Top,
                                                   Math.Add(Left, Math.Div2(Width)),
                                                   Math.Add(Top,  Math.Div2(Height)),
                                                   MaxNumberOfEmbeddedPixels);
                        Subtree1.OnTreeSplit += OnTreeSplit;
                    }

                    if (Subtree2 == null)
                    {
                        Subtree2 = new Quadtree<T, TValue>(Math.Add(Left, Math.Div2(Width)),
                                                   Top,
                                                   Right,
                                                   Math.Add(Top, Math.Div2(Height)),
                                                   MaxNumberOfEmbeddedPixels);
                        Subtree2.OnTreeSplit += OnTreeSplit;
                    }

                    if (Subtree3 == null)
                    {
                        Subtree3 = new Quadtree<T, TValue>(Left,
                                                   Math.Add(Top,  Math.Div2(Height)),
                                                   Math.Add(Left, Math.Div2(Width)),
                                                   Bottom,
                                                   MaxNumberOfEmbeddedPixels);
                        Subtree3.OnTreeSplit += OnTreeSplit;
                    }

                    if (Subtree4 == null)
                    {
                        Subtree4 = new Quadtree<T, TValue>(Math.Add(Left, Math.Div2(Width)),
                                                   Math.Add(Top,  Math.Div2(Height)),
                                                   Right,
                                                   Bottom,
                                                   MaxNumberOfEmbeddedPixels);
                        Subtree4.OnTreeSplit += OnTreeSplit;
                    }

                    #endregion

                    #region Fire QuadtreeSplit event

                    if (OnTreeSplit != null)
                        OnTreeSplit(this, IPixelValuePair);

                    #endregion

                    #region Move all embedded data into the subtrees

                    EmbeddedPixels.Add(IPixelValuePair);

                    foreach (var _Pixel in EmbeddedPixels)
                    {

                        if      (Subtree1.Contains(_Pixel))
                            Subtree1.Add(_Pixel);

                        else if (Subtree2.Contains(_Pixel))
                            Subtree2.Add(_Pixel);

                        else if (Subtree3.Contains(_Pixel))
                            Subtree3.Add(_Pixel);

                        else if (Subtree4.Contains(_Pixel))
                            Subtree4.Add(_Pixel);

                        else
                            throw new Exception("Mist!");

                    }

                    EmbeddedPixels.Clear();

                    #endregion

                }

                #endregion

            }

            else
                throw new QT_OutOfBoundsException<T, TValue>(this, IPixelValuePair);

        }

        #endregion

        #region Get(PixelSelector)

        /// <summary>
        /// Return all pixels matching the given pixelselector delegate.
        /// </summary>
        /// <param name="PixelSelector">A delegate selecting which pixels to return.</param>
        public IEnumerable<IPixelValuePair<T, TValue>> Get(PixelSelector<T> PixelSelector)
        {

            #region Initial Checks

            if (PixelSelector == null)
                throw new ArgumentNullException("The given PixelSelector must not be null!");

            #endregion

            #region Check embedded pixels

            foreach (var _Pixel in EmbeddedPixels)
                if (PixelSelector(_Pixel))
                    yield return _Pixel;

            #endregion

            #region Check subtrees

            if (Subtree1 != null)
                foreach (var _Pixel in Subtree1.Get(PixelSelector))
                    yield return _Pixel;

            if (Subtree2 != null)
                foreach (var _Pixel in Subtree2.Get(PixelSelector))
                    yield return _Pixel;

            if (Subtree3 != null)
                foreach (var _Pixel in Subtree3.Get(PixelSelector))
                    yield return _Pixel;

            if (Subtree4 != null)
                foreach (var _Pixel in Subtree4.Get(PixelSelector))
                    yield return _Pixel;

            #endregion

        }

        #endregion

        #region Get(Rectangle)

        /// <summary>
        /// Return all pixels within the given rectangle.
        /// </summary>
        /// <param name="Rectangle">A rectangle selecting which pixels to return.</param>
        public IEnumerable<IPixelValuePair<T, TValue>> Get(IRectangle<T> Rectangle)
        {

            #region Initial Checks

            if (Rectangle == null)
                throw new ArgumentNullException("The given Rectangle must not be null!");

            #endregion

            #region Check embedded pixels

            foreach (var _Pixel in EmbeddedPixels)
                if (Rectangle.Contains(_Pixel))
                    yield return _Pixel;

            #endregion

            #region Check subtrees

            if (Subtree1 != null)
                if (Subtree1.Overlaps(Rectangle))
                    foreach (var _Pixel in Subtree1.Get(Rectangle))
                        yield return _Pixel;

            if (Subtree2 != null)
                if (Subtree2.Overlaps(Rectangle))
                    foreach (var _Pixel in Subtree2.Get(Rectangle))
                        yield return _Pixel;

            if (Subtree3 != null)
                if (Subtree3.Overlaps(Rectangle))
                    foreach (var _Pixel in Subtree3.Get(Rectangle))
                        yield return _Pixel;

            if (Subtree4 != null)
                if (Subtree4.Overlaps(Rectangle))
                    foreach (var _Pixel in Subtree4.Get(Rectangle))
                        yield return _Pixel;

            #endregion

        }

        #endregion

        #region Remove(Pixel)

        /// <summary>
        /// Remove a pixel from the quadtree.
        /// </summary>
        /// <param name="Pixel">A pixel of type T.</param>
        public void Remove(IPixelValuePair<T, TValue> Pixel)
        {

            #region Initial Checks

            if (Pixel == null)
                throw new ArgumentNullException("The given pixel must not be null!");

            #endregion

            #region Remove embedded voxel

            EmbeddedPixels.Remove(Pixel);

            #endregion

            #region Check subtrees

            if (Subtree1 != null)
                if (Subtree1.Contains(Pixel))
                    Subtree1.Remove(Pixel);

            if (Subtree2 != null)
                if (Subtree2.Contains(Pixel))
                    Subtree2.Remove(Pixel);

            if (Subtree3 != null)
                if (Subtree3.Contains(Pixel))
                    Subtree3.Remove(Pixel);

            if (Subtree4 != null)
                if (Subtree4.Contains(Pixel))
                    Subtree4.Remove(Pixel);

            #endregion

        }

        #endregion

        #region Remove(Rectangle)

        /// <summary>
        /// Remove all pixels located within the given rectangle.
        /// </summary>
        /// <param name="Rectangle">A rectangle selecting which pixels to remove.</param>
        public void Remove(IRectangle<T> Rectangle)
        {

            #region Initial Checks

            if (Rectangle == null)
                throw new ArgumentNullException("The given rectangle must not be null!");

            #endregion

            #region Remove embedded voxel

            lock (this)
            {

                var _List = new List<IPixelValuePair<T, TValue>>();

                foreach (var _Pixel in EmbeddedPixels)
                    if (Rectangle.Contains(_Pixel))
                        _List.Add(_Pixel);

                foreach (var _Pixel in _List)
                    EmbeddedPixels.Remove(_Pixel);

            }

            #endregion

            #region Check subtrees

            if (Subtree1 != null)
                if (Subtree1.Overlaps(Rectangle))
                    Subtree1.Remove(Rectangle);

            if (Subtree2 != null)
                if (Subtree2.Overlaps(Rectangle))
                    Subtree2.Remove(Rectangle);

            if (Subtree3 != null)
                if (Subtree3.Overlaps(Rectangle))
                    Subtree3.Remove(Rectangle);

            if (Subtree4 != null)
                if (Subtree4.Overlaps(Rectangle))
                    Subtree4.Remove(Rectangle);

            #endregion

        }

        #endregion


        #region IEnumerable Members

        /// <summary>
        /// Return an enumeration of all stored pixels.
        /// </summary>
        public IEnumerator<IPixelValuePair<T, TValue>> GetEnumerator()
        {

            foreach (var _Pixel in EmbeddedPixels)
                yield return _Pixel;

            if (Subtree1 != null)
                foreach (var _Pixel in Subtree1)
                    yield return _Pixel;

            if (Subtree2 != null)
                foreach (var _Pixel in Subtree2)
                    yield return _Pixel;

            if (Subtree3 != null)
                foreach (var _Pixel in Subtree3)
                    yield return _Pixel;

            if (Subtree4 != null)
                foreach (var _Pixel in Subtree4)
                    yield return _Pixel;

        }

        /// <summary>
        /// Return an enumeration of all stored pixels.
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {

            foreach (var _Pixel in EmbeddedPixels)
                yield return _Pixel;

            if (Subtree1 != null)
                foreach (var _Pixel in Subtree1)
                    yield return _Pixel;

            if (Subtree2 != null)
                foreach (var _Pixel in Subtree2)
                    yield return _Pixel;

            if (Subtree3 != null)
                foreach (var _Pixel in Subtree3)
                    yield return _Pixel;

            if (Subtree4 != null)
                foreach (var _Pixel in Subtree4)
                    yield return _Pixel;

        }

        #endregion


        #region ToString()

        /// <summary>
        /// Return a string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return String.Format("{0} Element(s); Bounds = [Left={1}, Top={2}, Right={3}, Bottom={4}]",
                                 EmbeddedPixels.Count,
                                 Left.  ToString(),
                                 Top.   ToString(),
                                 Right. ToString(),
                                 Bottom.ToString());
        }

        #endregion

    }

    #endregion


}
