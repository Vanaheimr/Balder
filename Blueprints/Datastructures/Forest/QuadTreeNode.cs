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

    /// <summary>
    /// A node within a QuadTree.
    /// </summary>
    /// <typeparam name="T">The internal type of the QuadTreeNode.</typeparam>
    public class QuadTreeNode<T> : Rectangle<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Properties

        #region ChildNode1

        /// <summary>
        /// The first childnode.
        /// </summary>
        public QuadTreeNode<T> ChildNode1 { get; private set; }

        #endregion

        #region ChildNode2

        /// <summary>
        /// The second childnode.
        /// </summary>
        public QuadTreeNode<T> ChildNode2 { get; private set; }

        #endregion

        #region ChildNode3

        /// <summary>
        /// The third childnode.
        /// </summary>
        public QuadTreeNode<T> ChildNode3 { get; private set; }

        #endregion

        #region ChildNode4

        /// <summary>
        /// The fourth childnode.
        /// </summary>
        public QuadTreeNode<T> ChildNode4 { get; private set; }

        #endregion


        #region EmbeddedData

        private HashSet<IPixel<T>> _EmbeddedData;

        /// <summary>
        /// The pixels storted within this QuadTreeNode.
        /// </summary>
        public IEnumerable<IPixel<T>> EmbeddedData
        {
            get
            {
                return _EmbeddedData;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region QuadTreeNode(Left, Top, Right, Bottom)

        /// <summary>
        /// Create a QuadTreeNode of type T.
        /// </summary>
        /// <param name="Left">The left parameter.</param>
        /// <param name="Top">The top parameter.</param>
        /// <param name="Right">The right parameter.</param>
        /// <param name="Bottom">The bottom parameter.</param>
        public QuadTreeNode(T Left, T Top, T Right, T Bottom)
            : base(Left, Top, Right, Bottom)
        {
            _EmbeddedData = new HashSet<IPixel<T>>();
        }

        #endregion

        #region QuadTreeNode(Pixel1, Pixel2)

        /// <summary>
        /// Create a QuadTreeNode of type T.
        /// </summary>
        /// <param name="Pixel1">A pixel of type T.</param>
        /// <param name="Pixel2">A pixel of type T.</param>
        public QuadTreeNode(Pixel<T> Pixel1, Pixel<T> Pixel2)
            : base(Pixel1, Pixel2)
        {
            _EmbeddedData = new HashSet<IPixel<T>>();
        }

        #endregion

        #region QuadTreeNode(Pixel, Width, Height)

        /// <summary>
        /// Create a QuadTreeNode of type T.
        /// </summary>
        /// <param name="Pixel">A pixel of type T in the upper left corner of the QuadTreeNode.</param>
        /// <param name="Width">The width of the QuadTreeNode.</param>
        /// <param name="Height">The height of the QuadTreeNode.</param>
        public QuadTreeNode(Pixel<T> Pixel, T Width, T Height)
            : base(Pixel, Width, Height)
        {
            _EmbeddedData = new HashSet<IPixel<T>>();
        }

        #endregion

        #endregion


        public void Add(IPixel<T> IPixel)
        {

            if (this.Contains(IPixel.X, IPixel.Y))
            {
                
                if      (ChildNode1 != null && ChildNode1.Contains(IPixel.X, IPixel.Y))
                    ChildNode1.Add(IPixel);
                
                else if (ChildNode2 != null && ChildNode2.Contains(IPixel.X, IPixel.Y))
                    ChildNode2.Add(IPixel);
                
                else if (ChildNode3 != null && ChildNode3.Contains(IPixel.X, IPixel.Y))
                    ChildNode3.Add(IPixel);
                
                else if (ChildNode4 != null && ChildNode4.Contains(IPixel.X, IPixel.Y))
                    ChildNode4.Add(IPixel);

                else
                    _EmbeddedData.Add(IPixel);

            }

        }

        public IEnumerable<IPixel<T>> Get(IRectangle<T> Rectangle)
        {
            throw new NotImplementedException();
        }

        public void Remove(IPixel<T> IPixel)
        {
            _EmbeddedData.Remove(IPixel);
        }


    }

}
