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
    /// Unit tests for the bintree class.
    /// </summary>
    [TestFixture]
    public class BintreeTests
    {

        #region CheckBounds()

        /// <summary>
        /// A test for the bintree bounds.
        /// </summary>
        [Test]
        public void CheckBounds()
        {
            
            var _Bintree = new Bintree<Double>(1, 3);
            
            Assert.AreEqual(1, _Bintree.Left);
            Assert.AreEqual(3, _Bintree.Right);
            Assert.AreEqual(2, _Bintree.Size);

            Assert.AreEqual(0, _Bintree.EmbeddedCount);
            Assert.AreEqual(0, _Bintree.Count);

        }

        #endregion

        #region VerifyOutOfBoundsException()

        /// <summary>
        /// A test verifying the OutOfBoundsException.
        /// </summary>
        [Test]
        [ExpectedException(typeof(BT_OutOfBoundsException<Double>))]
        public void VerifyOutOfBoundsException()
        {
            var _Bintree = new Bintree<Double>(1, 3);
            _Bintree.Add(10);
        }

        #endregion

        #region CheckSplit()

        /// <summary>
        /// A test for splitting the bintree.
        /// </summary>
        [Test]
        public void CheckSplit()
        {

            var _NumberOfSplits = 0L;

            var _Bintree = new Bintree<Double>(0, 10, MaxNumberOfEmbeddedElements: 4);
            _Bintree.OnTreeSplit += (Bintree, Pixel) =>
            {
                Interlocked.Increment(ref _NumberOfSplits);
            };

            _Bintree.Add(1);
            Assert.AreEqual(1UL, _Bintree.EmbeddedCount);
            Assert.AreEqual(1UL, _Bintree.Count);

            _Bintree.Add(2);
            Assert.AreEqual(2, _Bintree.EmbeddedCount);
            Assert.AreEqual(2, _Bintree.Count);

            _Bintree.Add(8);
            Assert.AreEqual(3, _Bintree.EmbeddedCount);
            Assert.AreEqual(3, _Bintree.Count);

            _Bintree.Add(9);
            Assert.AreEqual(4, _Bintree.EmbeddedCount);
            Assert.AreEqual(4, _Bintree.Count);

            // Add the fifth pixel -> Should cause a split!
            _Bintree.Add(5);
            Assert.AreEqual(1L, _NumberOfSplits);

            Assert.AreEqual(0, _Bintree.EmbeddedCount);
        //    Assert.AreEqual(5, _Bintree.Count);

        }

        #endregion

        #region CheckRecursiveSplits()

        /// <summary>
        /// A test for splitting the bintree recursively.
        /// </summary>
        [Test]
        public void CheckRecursiveSplits()
        {

            var _NumberOfSplits = 0L;

            var _Bintree = new Bintree<Double>(0, 100, MaxNumberOfEmbeddedElements: 4);
            _Bintree.OnTreeSplit += (Bintree, Pixel) =>
            {
                Interlocked.Increment(ref _NumberOfSplits);
            };

            _Bintree.Add(1);
            _Bintree.Add(2);
            _Bintree.Add(3);
            _Bintree.Add(4);
            _Bintree.Add(5);
            _Bintree.Add(6);
            _Bintree.Add(50);
            _Bintree.Add(51);
            _Bintree.Add(52);
            _Bintree.Add(53);
            _Bintree.Add(54);
            _Bintree.Add(55);
            _Bintree.Add(56);
            _Bintree.Add(57);
            _Bintree.Add(58);
            _Bintree.Add(59);
            _Bintree.Add(60);

            Assert.AreEqual(9L, _NumberOfSplits);

        }

        #endregion

    }

}
