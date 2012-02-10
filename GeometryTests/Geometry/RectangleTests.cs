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

using NUnit.Framework;

#endregion

namespace de.ahzf.Blueprints.UnitTests.Geometry
{

    /// <summary>
    /// Unit tests for the Rectangle class.
    /// </summary>
    [TestFixture]
    public class RectangleTests
    {

        #region WidthAndHeight()

        /// <summary>
        /// A test for the pixel equality.
        /// </summary>
        [Test]
        public void WidthAndHeight()
        {
            var _Rectangle = new Rectangle<Double>(1, 1, 5, 10);
            Assert.AreEqual(4.0, _Rectangle.Width);
            Assert.AreEqual(9.0, _Rectangle.Height);
        }

        #endregion


        #region ContainsPixelTest()

        /// <summary>
        /// A test for the pixel equality.
        /// </summary>
        [Test]
        public void ContainsPixelTest()
        {
            var _Rectangle = new Rectangle<Double>(1, 1, 10, 10);
            var _Pixel     = new Pixel<Double>(5, 5);
            Assert.IsTrue(_Rectangle.Contains(_Pixel));
        }

        #endregion

        #region NotContainsPixelTest()

        /// <summary>
        /// A test for the pixel equality.
        /// </summary>
        [Test]
        public void NotContainsPixelTest()
        {
            var _Rectangle = new Rectangle<Double>(1, 1, 10, 10);
            var _Pixel     = new Pixel<Double>(15, 5);
            Assert.IsFalse(_Rectangle.Contains(_Pixel));
        }

        #endregion

        #region ContainsCornerPixelsTest()

        /// <summary>
        /// A test for the pixel equality.
        /// </summary>
        [Test]
        public void ContainsCornerPixelsTest()
        {
            var _Rectangle = new Rectangle<Double>(10, 20, 30, 40);
            var _Pixel1    = new Pixel<Double>(10, 20);
            var _Pixel2    = new Pixel<Double>(30, 20);
            var _Pixel3    = new Pixel<Double>(10, 40);
            var _Pixel4    = new Pixel<Double>(30, 40);
            Assert.IsTrue(_Rectangle.Contains(_Pixel1));
            Assert.IsTrue(_Rectangle.Contains(_Pixel2));
            Assert.IsTrue(_Rectangle.Contains(_Pixel3));
            Assert.IsTrue(_Rectangle.Contains(_Pixel4));
        }

        #endregion


        #region ContainsSmallerRectangleTest()

        /// <summary>
        /// A test for the pixel equality.
        /// </summary>
        [Test]
        public void ContainsSmallerRectangleTest()
        {
            var _Rectangle1 = new Rectangle<Double>(10, 10, 20, 20);
            var _Rectangle2 = new Rectangle<Double>(12, 12, 18, 18);
            Assert.IsTrue(_Rectangle1.Contains(_Rectangle2));
        }

        #endregion

        #region ContainsEqualRectangleTest()

        /// <summary>
        /// A test for the pixel equality.
        /// </summary>
        [Test]
        public void ContainsEqualRectangleTest()
        {
            var _Rectangle1 = new Rectangle<Double>(10, 10, 20, 20);
            var _Rectangle2 = new Rectangle<Double>(10, 10, 20, 20);
            Assert.IsTrue(_Rectangle1.Contains(_Rectangle2));
        }

        #endregion

        #region NotContainsRectangleTest1()

        /// <summary>
        /// A test for the pixel equality.
        /// </summary>
        [Test]
        public void NotContainsRectangleTest1()
        {
            var _Rectangle1 = new Rectangle<Double>(10, 10, 20, 20);
            var _Rectangle2 = new Rectangle<Double>( 9, 12, 18, 18);
            Assert.IsFalse(_Rectangle1.Contains(_Rectangle2));
        }

        #endregion

    }

}
