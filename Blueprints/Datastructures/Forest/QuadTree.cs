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
    /// A QuadTree is an indexing structure for two-dimensional data.
    /// Note: This datastructure is not self-balancing!
    /// </summary>
    /// <typeparam name="T">The internal type of the QuadTree.</typeparam>
    public class QuadTree<T> : AMath<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Data

        private readonly QuadTreeNode<T> _RootNode;

        #endregion

        #region Constructor(s)

        #region QuadTree(Left, Top, Right, Bottom)

        /// <summary>
        /// Create a QuadTree of type T.
        /// </summary>
        /// <param name="Left">The left parameter.</param>
        /// <param name="Top">The top parameter.</param>
        /// <param name="Right">The right parameter.</param>
        /// <param name="Bottom">The bottom parameter.</param>
        public QuadTree(T Left, T Top, T Right, T Bottom)
        {

            var _Left   = Math.Min(Left, Right);
            var _Top    = Math.Min(Top,  Bottom);
            var _Right  = Math.Max(Left, Right);
            var _Bottom = Math.Max(Top,  Bottom);

            _RootNode = new QuadTreeNode<T>(_Left, _Top, _Right, _Bottom);

        }

        #endregion

        #endregion


        public void Add(IPixel<T> IPixel)
        {
            _RootNode.Add(IPixel);
        }

        public void Remove(IPixel<T> IPixel)
        {
            _RootNode.Remove(IPixel);
        }

    }

}
