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

    public delegate void QuadTreeSplitEventHandler<T>(QuadTree<T> QuadTree, IPixel<T> Pixel)
        where T : IEquatable<T>, IComparable<T>, IComparable;

    /// <summary>
    /// A QuadTree is an indexing structure for two-dimensional data.
    /// Note: This datastructure is not self-balancing!
    /// </summary>
    /// <typeparam name="T">The internal type of the QuadTree.</typeparam>
    public class QuadTree<T> : Rectangle<T>, IEnumerable<IPixel<T>>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Data

        private QuadTree<T> Subtree1;
        private QuadTree<T> Subtree2;
        private QuadTree<T> Subtree3;
        private QuadTree<T> Subtree4;

        private List<IPixel<T>> EmbeddedPixels;

        #endregion

        #region Events

        #region OnQuadTreeSplit

        /// <summary>
        /// An event to inform about an QuadTree split happening.
        /// </summary>
        public event QuadTreeSplitEventHandler<T> OnQuadTreeSplit;

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

        #region QuadTree(Left, Top, Right, Bottom, MaxNumberOfEmbeddedData = 256)

        /// <summary>
        /// Create a QuadTree of type T.
        /// </summary>
        /// <param name="Left">The left parameter.</param>
        /// <param name="Top">The top parameter.</param>
        /// <param name="Right">The right parameter.</param>
        /// <param name="Bottom">The bottom parameter.</param>
        /// <param name="MaxNumberOfEmbeddedData">The maximum number of embedded elements before four child node will be created.</param>
        public QuadTree(T Left, T Top, T Right, T Bottom, UInt32 MaxNumberOfEmbeddedData = 256)
            : base(Left, Top, Right, Bottom)
        {

            if (Width.Equals(default(T)))
                throw new Exception("Width == 0!");

            if (Height.Equals(default(T)))
                throw new Exception("Height == 0!");

            this.MaxNumberOfEmbeddedData = MaxNumberOfEmbeddedData;
            this.EmbeddedPixels = new List<IPixel<T>>();

        }

        #endregion

        #region QuadTree(Pixel1, Pixel2, MaxNumberOfEmbeddedData = 256)

        /// <summary>
        /// Create a QuadTree of type T.
        /// </summary>
        /// <param name="Pixel1">A pixel of type T.</param>
        /// <param name="Pixel2">A pixel of type T.</param>
        /// <param name="MaxNumberOfEmbeddedData">The maximum number of embedded elements before four child node will be created.</param>
        public QuadTree(Pixel<T> Pixel1, Pixel<T> Pixel2, UInt32 MaxNumberOfEmbeddedData = 256)
            : base(Pixel1, Pixel2)
        {
            this.MaxNumberOfEmbeddedData = MaxNumberOfEmbeddedData;
            this.EmbeddedPixels = new List<IPixel<T>>();
        }

        #endregion

        #region QuadTree(Pixel, Width, Height, MaxNumberOfEmbeddedData = 256)

        /// <summary>
        /// Create a QuadTree of type T.
        /// </summary>
        /// <param name="Pixel">A pixel of type T in the upper left corner of the QuadTreeNode.</param>
        /// <param name="Width">The width of the QuadTreeNode.</param>
        /// <param name="Height">The height of the QuadTreeNode.</param>
        /// <param name="MaxNumberOfEmbeddedData">The maximum number of embedded elements before four child node will be created.</param>
        public QuadTree(Pixel<T> Pixel, T Width, T Height, UInt32 MaxNumberOfEmbeddedData = 256)
            : base(Pixel, Width, Height)
        {
            this.MaxNumberOfEmbeddedData = MaxNumberOfEmbeddedData;
            this.EmbeddedPixels = new List<IPixel<T>>();
        }

        #endregion

        #endregion


        #region Add(Pixel)

        /// <summary>
        /// Add a pixel to the QuadTree.
        /// </summary>
        /// <param name="Pixel">A pixel of type T.</param>
        public void Add(IPixel<T> Pixel)
        {

            if (this.Contains(Pixel.X, Pixel.Y))
            {

                #region Check subtrees...

                if      (Subtree1 != null && Subtree1.Contains(Pixel.X, Pixel.Y))
                    Subtree1.Add(Pixel);
                
                else if (Subtree2 != null && Subtree2.Contains(Pixel.X, Pixel.Y))
                    Subtree2.Add(Pixel);
                
                else if (Subtree3 != null && Subtree3.Contains(Pixel.X, Pixel.Y))
                    Subtree3.Add(Pixel);
                
                else if (Subtree4 != null && Subtree4.Contains(Pixel.X, Pixel.Y))
                    Subtree4.Add(Pixel);

                #endregion

                #region ...or the embedded data.

                else if (EmbeddedPixels.Count < MaxNumberOfEmbeddedData)
                    EmbeddedPixels.Add(Pixel);

                #endregion

                #region If necessary create subtrees...

                else
                {

                    #region Create Subtrees

                    if (Subtree1 == null)
                    {
                        Subtree1 = new QuadTree<T>(Left,
                                                   Top,
                                                   Math.Add(Left, Math.Div2(Width)),
                                                   Math.Add(Top,  Math.Div2(Height)),
                                                   MaxNumberOfEmbeddedData);
                        Subtree1.OnQuadTreeSplit += OnQuadTreeSplit;
                    }

                    if (Subtree2 == null)
                    {
                        Subtree2 = new QuadTree<T>(Math.Add(Left, Math.Div2(Width)),
                                                   Top,
                                                   Right,
                                                   Math.Add(Top, Math.Div2(Height)),
                                                   MaxNumberOfEmbeddedData);
                        Subtree2.OnQuadTreeSplit += OnQuadTreeSplit;
                    }

                    if (Subtree3 == null)
                    {
                        Subtree3 = new QuadTree<T>(Left,
                                                   Math.Add(Top,  Math.Div2(Height)),
                                                   Math.Add(Left, Math.Div2(Width)),
                                                   Bottom,
                                                   MaxNumberOfEmbeddedData);
                        Subtree3.OnQuadTreeSplit += OnQuadTreeSplit;
                    }

                    if (Subtree4 == null)
                    {
                        Subtree4 = new QuadTree<T>(Math.Add(Left, Math.Div2(Width)),
                                                   Math.Add(Top,  Math.Div2(Height)),
                                                   Right,
                                                   Bottom,
                                                   MaxNumberOfEmbeddedData);
                        Subtree4.OnQuadTreeSplit += OnQuadTreeSplit;
                    }

                    #endregion

                    #region Fire QuadTreeSplit event

                    if (OnQuadTreeSplit != null)
                        OnQuadTreeSplit(this, Pixel);

                    #endregion

                    #region Move all embedded data into the subtrees

                    EmbeddedPixels.Add(Pixel);

                    foreach (var _Pixel in EmbeddedPixels)
                    {

                        if      (Subtree1.Contains(_Pixel.X, _Pixel.Y))
                            Subtree1.Add(_Pixel);

                        else if (Subtree2.Contains(_Pixel.X, _Pixel.Y))
                            Subtree2.Add(_Pixel);

                        else if (Subtree3.Contains(_Pixel.X, _Pixel.Y))
                            Subtree3.Add(_Pixel);

                        else if (Subtree4.Contains(_Pixel.X, _Pixel.Y))
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
                throw new ArgumentException("The given pixel(" + Pixel.ToString() + ") is not valid!");

        }

        #endregion

        #region Get(PixelSelector)

        /// <summary>
        /// Return all pixels matching the given pixel selector delegate.
        /// </summary>
        /// <param name="PixelSelector">A delegate selecting which pixel to return.</param>
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
        /// <param name="Rectangle">A rectangle selecting which pixel to return.</param>
        public IEnumerable<IPixel<T>> Get(IRectangle<T> Rectangle)
        {
            return Get(p => Rectangle.Contains(p));
        }

        #endregion

        #region Remove(Pixel)

        /// <summary>
        /// Remove a pixel from the QuadTree.
        /// </summary>
        /// <param name="Pixel"></param>
        public void Remove(IPixel<T> Pixel)
        {
            EmbeddedPixels.Remove(Pixel);
        }

        #endregion

        #region EmbeddedCount

        /// <summary>
        /// Return the number of embedded pixels
        /// stored within this QuadTree(Node).
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
        /// within the entire QuadTree.
        /// </summary>
        public UInt64 Count
        {
            get
            {
                //FIXME: !!!
                return (UInt64) EmbeddedPixels.Count;
            }
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

}
