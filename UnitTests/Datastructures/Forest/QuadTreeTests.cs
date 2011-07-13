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
using System.Threading;

#endregion

namespace de.ahzf.Blueprints.UnitTests.Forest
{

    /// <summary>
    /// Unit tests for the quadtree class.
    /// </summary>
    [TestFixture]
    public class QuadtreeTests
    {

        #region CheckBounds()

        /// <summary>
        /// A test for the quadtree bounds.
        /// </summary>
        [Test]
        public void CheckBounds()
        {
            
            var _Quadtree = new Quadtree<Double>(1, 2, 3, 5);
            
            Assert.AreEqual(1, _Quadtree.Left);
            Assert.AreEqual(2, _Quadtree.Top);
            Assert.AreEqual(3, _Quadtree.Right);
            Assert.AreEqual(5, _Quadtree.Bottom);
            Assert.AreEqual(2, _Quadtree.Width);
            Assert.AreEqual(3, _Quadtree.Height);

            Assert.AreEqual(0, _Quadtree.EmbeddedCount);
            Assert.AreEqual(0, _Quadtree.Count);

        }

        #endregion

        #region VerifyOutOfBoundsException()

        /// <summary>
        /// A test verifying the OutOfBoundsException.
        /// </summary>
        [Test]
        [ExpectedException(typeof(QT_OutOfBoundsException<Double>))]
        public void VerifyOutOfBoundsException()
        {
            var _Quadtree = new Quadtree<Double>(1, 2, 3, 5);
            _Quadtree.Add(new Pixel<Double>(10, 10));
        }

        #endregion

        #region CheckSplit()

        /// <summary>
        /// A test for splitting the quadtree.
        /// </summary>
        [Test]
        public void CheckSplit()
        {

            var _NumberOfSplits = 0L;

            var _Quadtree = new Quadtree<Double>(0, 0, 10, 10, MaxNumberOfEmbeddedPixels: 4);
            _Quadtree.OnTreeSplit += (Quadtree, Pixel) =>
            {
                Interlocked.Increment(ref _NumberOfSplits);
            };

            _Quadtree.Add(new Pixel<Double>(1, 1));
            Assert.AreEqual(1UL, _Quadtree.EmbeddedCount);
            Assert.AreEqual(1UL, _Quadtree.Count);

            _Quadtree.Add(new Pixel<Double>(9, 1));
            Assert.AreEqual(2, _Quadtree.EmbeddedCount);
            Assert.AreEqual(2, _Quadtree.Count);

            _Quadtree.Add(new Pixel<Double>(1, 9));
            Assert.AreEqual(3, _Quadtree.EmbeddedCount);
            Assert.AreEqual(3, _Quadtree.Count);

            _Quadtree.Add(new Pixel<Double>(9, 9));
            Assert.AreEqual(4, _Quadtree.EmbeddedCount);
            Assert.AreEqual(4, _Quadtree.Count);

            // Add the fifth pixel -> Should cause a split!
            _Quadtree.Add(new Pixel<Double>(4, 4));
            Assert.AreEqual(1L, _NumberOfSplits);

            Assert.AreEqual(0, _Quadtree.EmbeddedCount);
        //    Assert.AreEqual(5, _Quadtree.Count);

        }

        #endregion

        #region CheckRecursiveSplits()

        /// <summary>
        /// A test for splitting the quadtree recursively.
        /// </summary>
        [Test]
        public void CheckRecursiveSplits()
        {

            var _NumberOfSplits = 0L;

            var _Quadtree = new Quadtree<Double>(0, 0, 100, 100, MaxNumberOfEmbeddedPixels: 4);
            _Quadtree.OnTreeSplit += (Quadtree, Pixel) =>
            {
                Interlocked.Increment(ref _NumberOfSplits);
            };

            _Quadtree.Add(new Pixel<Double>(1, 1));
            _Quadtree.Add(new Pixel<Double>(9, 1));
            _Quadtree.Add(new Pixel<Double>(1, 9));
            _Quadtree.Add(new Pixel<Double>(9, 9));
            _Quadtree.Add(new Pixel<Double>(4, 4));
            _Quadtree.Add(new Pixel<Double>(5, 5));
            _Quadtree.Add(new Pixel<Double>(50, 5));
            _Quadtree.Add(new Pixel<Double>(51, 5));
            _Quadtree.Add(new Pixel<Double>(52, 5));
            _Quadtree.Add(new Pixel<Double>(53, 5));
            _Quadtree.Add(new Pixel<Double>(54, 5));
            _Quadtree.Add(new Pixel<Double>(55, 5));
            _Quadtree.Add(new Pixel<Double>(56, 5));
            _Quadtree.Add(new Pixel<Double>(57, 5));
            _Quadtree.Add(new Pixel<Double>(58, 5));
            _Quadtree.Add(new Pixel<Double>(59, 5));
            _Quadtree.Add(new Pixel<Double>(60, 5));

            Assert.AreEqual(8L, _NumberOfSplits);

        }

        #endregion





        #region CheckSplit()

        /// <summary>
        /// A test for splitting the quadtree.
        /// </summary>
        [Test]
        public void CheckSplit2()
        {

            var _NumberOfSplits = 0L;

            var _Quadtree = new Quadtree<Double, String>(0, 0, 10, 10, MaxNumberOfEmbeddedPixels: 4);
            _Quadtree.OnTreeSplit += (Quadtree, Pixel) =>
            {
                Interlocked.Increment(ref _NumberOfSplits);
            };

            _Quadtree.Add(new Pixel<Double>(1, 1), "a");
            Assert.AreEqual(1UL, _Quadtree.EmbeddedCount);
            Assert.AreEqual(1UL, _Quadtree.Count);

            _Quadtree.Add(new Pixel<Double>(9, 1), "b");
            Assert.AreEqual(2, _Quadtree.EmbeddedCount);
            Assert.AreEqual(2, _Quadtree.Count);

            _Quadtree.Add(new Pixel<Double>(1, 9), "c");
            Assert.AreEqual(3, _Quadtree.EmbeddedCount);
            Assert.AreEqual(3, _Quadtree.Count);

            _Quadtree.Add(new Pixel<Double>(9, 9), "d");
            Assert.AreEqual(4, _Quadtree.EmbeddedCount);
            Assert.AreEqual(4, _Quadtree.Count);

            // Add the fifth pixel -> Should cause a split!
            _Quadtree.Add(new Pixel<Double>(4, 4), "e");
            Assert.AreEqual(1L, _NumberOfSplits);

            Assert.AreEqual(0, _Quadtree.EmbeddedCount);
            //    Assert.AreEqual(5, _Quadtree.Count);

            _Quadtree.Add(new Pixel<Double>(5, 5), "f");


            var a = _Quadtree.Get(new Rectangle<Double>(3, 3, 6, 6)).ToArray();
            Assert.AreEqual(2, a.Count());
            Assert.IsTrue(a[0].Value == "e" || a[1].Value == "f");

        }

        #endregion

    }

}
