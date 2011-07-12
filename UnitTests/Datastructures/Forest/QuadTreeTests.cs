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
using System.Linq;
using System.Collections.Generic;

using NUnit.Framework;

#endregion

namespace de.ahzf.Blueprints.UnitTests.Maths
{

    /// <summary>
    /// Unit tests for the QuadTree class.
    /// </summary>
    [TestFixture]
    public class QuadTreeTests
    {

        #region CheckBounds()

        /// <summary>
        /// A test for the QuadTree bounds.
        /// </summary>
        [Test]
        public void CheckBounds()
        {
            
            var _QuadTree = new QuadTree<Double>(1, 2, 3, 5);
            
            Assert.AreEqual(1, _QuadTree.Left);
            Assert.AreEqual(2, _QuadTree.Top);
            Assert.AreEqual(3, _QuadTree.Right);
            Assert.AreEqual(5, _QuadTree.Bottom);
            Assert.AreEqual(2, _QuadTree.Width);
            Assert.AreEqual(3, _QuadTree.Height);
            Assert.AreEqual(0, _QuadTree.Count);

        }

        #endregion

        #region CheckQuadTreeSplit()

        /// <summary>
        /// A test for the QuadTree bounds.
        /// </summary>
        [Test]
        public void CheckQuadTreeSplit()
        {

            var _QuadTree = new QuadTree<Double>(0, 0, 10, 10, MaxNumberOfEmbeddedData: 4);
            _QuadTree.OnQuadTreeSplit += (qt, p) => { throw new Exception(); };

            _QuadTree.Add(new Pixel<Double>(1, 1));
            Assert.AreEqual(1UL, _QuadTree.EmbeddedCount);
            Assert.AreEqual(1UL, _QuadTree.Count);
            
            _QuadTree.Add(new Pixel<Double>(9, 1));
            Assert.AreEqual(2, _QuadTree.EmbeddedCount);
            Assert.AreEqual(2, _QuadTree.Count);
            
            _QuadTree.Add(new Pixel<Double>(1, 9));
            Assert.AreEqual(3, _QuadTree.EmbeddedCount);
            Assert.AreEqual(3, _QuadTree.Count);
            
            _QuadTree.Add(new Pixel<Double>(9, 9));
            Assert.AreEqual(4, _QuadTree.EmbeddedCount);
            Assert.AreEqual(4, _QuadTree.Count);

        }

        #endregion

        #region CheckQuadTreeSplit()

        /// <summary>
        /// A test for the QuadTree bounds.
        /// </summary>
        [Test]
        public void CheckQuadTreeSplit2()
        {

            Boolean _Split = false;

            var _QuadTree = new QuadTree<Double>(0, 0, 10, 10, MaxNumberOfEmbeddedData: 4);
            _QuadTree.OnQuadTreeSplit += (qt, p) => { _Split = true; };

            _QuadTree.Add(new Pixel<Double>(1, 1));
            Assert.AreEqual(1UL, _QuadTree.EmbeddedCount);
            Assert.AreEqual(1UL, _QuadTree.Count);

            _QuadTree.Add(new Pixel<Double>(9, 1));
            Assert.AreEqual(2, _QuadTree.EmbeddedCount);
            Assert.AreEqual(2, _QuadTree.Count);

            _QuadTree.Add(new Pixel<Double>(1, 9));
            Assert.AreEqual(3, _QuadTree.EmbeddedCount);
            Assert.AreEqual(3, _QuadTree.Count);

            _QuadTree.Add(new Pixel<Double>(9, 9));
            Assert.AreEqual(4, _QuadTree.EmbeddedCount);
            Assert.AreEqual(4, _QuadTree.Count);

            _QuadTree.Add(new Pixel<Double>(4, 4));
            // QuadTree split happens!
      //      Assert.IsTrue(_Split);

            Assert.AreEqual(0, _QuadTree.EmbeddedCount);
        //    Assert.AreEqual(5, _QuadTree.Count);

            _QuadTree.Add(new Pixel<Double>(5, 5));
            Assert.AreEqual(0, _QuadTree.EmbeddedCount);
       //     Assert.AreEqual(6, _QuadTree.Count);

        }

        #endregion

    }

}
