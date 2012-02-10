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
    /// Unit tests for the Pixel class.
    /// </summary>
    [TestFixture]
    public class PixelTests
    {

        #region PixelEqualityTest1()

        /// <summary>
        /// A test for the pixel equality.
        /// </summary>
        [Test]
        public void PixelEqualityTest1()
        {
            var _Pixel1 = new Pixel<Double>(23, 5);
            var _Pixel2 = new Pixel<Double>(23, 5);
            Assert.AreEqual(_Pixel1, _Pixel2);
        }

        #endregion

        #region PixelEqualityTest2()

        /// <summary>
        /// A test for the pixel equality.
        /// </summary>
        [Test]
        public void PixelEqualityTest2()
        {
            var _Pixel1 = new Pixel<Double>(23, 5);
            var _Pixel2 = new Pixel<Double>(23, 5);
            Assert.IsTrue(_Pixel1 == _Pixel2);
        }

        #endregion

        #region PixelInequalityTest1()

        /// <summary>
        /// A test for the pixel inequality.
        /// </summary>
        [Test]
        public void PixelInequalityTest1()
        {
            var _Pixel1 = new Pixel<Double>(23, 5);
            var _Pixel2 = new Pixel<Double>(5, 23);
            Assert.AreNotEqual(_Pixel1, _Pixel2);
        }

        #endregion

        #region PixelInequalityTest2()

        /// <summary>
        /// A test for the pixel inequality.
        /// </summary>
        [Test]
        public void PixelInequalityTest2()
        {
            var _Pixel1 = new Pixel<Double>(23, 5);
            var _Pixel2 = new Pixel<Double>(5, 23);
            Assert.IsTrue(_Pixel1 != _Pixel2);
        }

        #endregion


        #region PixelDistanceTest1()

        /// <summary>
        /// A test for the pixel distance.
        /// </summary>
        [Test]
        public void PixelDistanceTest1()
        {
            var _Pixel1 = new Pixel<Double>(0, 0);
            var _Pixel2 = new Pixel<Double>(1, 1);
            Assert.AreEqual(Math.Sqrt(2), _Pixel1.DistanceTo(_Pixel2));
        }

        #endregion

        #region PixelDistanceTest2()

        /// <summary>
        /// A test for the pixel distance.
        /// </summary>
        [Test]
        public void PixelDistanceTest2()
        {
            var _Pixel1 = new Pixel<Double>(1, 1);
            var _Pixel2 = new Pixel<Double>(3, 3);
            Assert.AreEqual(Math.Sqrt(8), _Pixel1.DistanceTo(_Pixel2));
        }

        #endregion

    }

}
