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

using NUnit.Framework;

#endregion

namespace de.ahzf.Blueprints.UnitTests
{

    /// <summary>
    /// Unit tests for the HyperEdgeId class.
    /// </summary>
    [TestFixture]
    public class HyperEdgeIdTests
    {

        #region HyperEdgeIdEmptyConstructorTest()

        /// <summary>
        /// A test for an empty HyperEdgeId constructor.
        /// </summary>
        [Test]
        public void HyperEdgeIdEmptyConstructorTest()
        {
            var _HyperEdgeId1 = new HyperEdgeId();
            var _HyperEdgeId2 = new HyperEdgeId();
            Assert.IsTrue(_HyperEdgeId1.Length > 0);
            Assert.IsTrue(_HyperEdgeId2.Length > 0);
            Assert.AreNotEqual(_HyperEdgeId1, _HyperEdgeId2);
        }

        #endregion

        #region HyperEdgeIdStringConstructorTest()

        /// <summary>
        /// A test for the HyperEdgeId string constructor.
        /// </summary>
        [Test]
        public void HyperEdgeIdStringConstructorTest()
        {
            var _HyperEdgeId = new HyperEdgeId("123");
            Assert.AreEqual("123", _HyperEdgeId.ToString());
            Assert.AreEqual(3,     _HyperEdgeId.Length);
        }

        #endregion

        #region HyperEdgeIdInt32ConstructorTest()

        /// <summary>
        /// A test for the HyperEdgeId Int32 constructor.
        /// </summary>
        [Test]
        public void HyperEdgeIdInt32ConstructorTest()
        {
            var _HyperEdgeId = new HyperEdgeId(5);
            Assert.AreEqual("5", _HyperEdgeId.ToString());
            Assert.AreEqual(1,   _HyperEdgeId.Length);
        }

        #endregion

        #region HyperEdgeIdUInt32ConstructorTest()

        /// <summary>
        /// A test for the HyperEdgeId UInt32 constructor.
        /// </summary>
        [Test]
        public void HyperEdgeIdUInt32ConstructorTest()
        {
            var _HyperEdgeId = new HyperEdgeId(23U);
            Assert.AreEqual("23", _HyperEdgeId.ToString());
            Assert.AreEqual(2,    _HyperEdgeId.Length);
        }

        #endregion

        #region HyperEdgeIdInt64ConstructorTest()

        /// <summary>
        /// A test for the HyperEdgeId Int64 constructor.
        /// </summary>
        [Test]
        public void HyperEdgeIdInt64ConstructorTest()
        {
            var _HyperEdgeId = new HyperEdgeId(42L);
            Assert.AreEqual("42", _HyperEdgeId.ToString());
            Assert.AreEqual(2,    _HyperEdgeId.Length);
        }

        #endregion

        #region HyperEdgeIdUInt64ConstructorTest()

        /// <summary>
        /// A test for the HyperEdgeId UInt64 constructor.
        /// </summary>
        [Test]
        public void HyperEdgeIdUInt64ConstructorTest()
        {
            var _HyperEdgeId = new HyperEdgeId(123UL);
            Assert.AreEqual("123", _HyperEdgeId.ToString());
            Assert.AreEqual(3,     _HyperEdgeId.Length);
        }

        #endregion

        #region HyperEdgeIdHyperEdgeIdConstructorTest()

        /// <summary>
        /// A test for the HyperEdgeId HyperEdgeId constructor.
        /// </summary>
        [Test]
        public void HyperEdgeIdHyperEdgeIdConstructorTest()
        {
            var _HyperEdgeId1 = HyperEdgeId.NewHyperEdgeId;
            var _HyperEdgeId2 = new HyperEdgeId(_HyperEdgeId1);
            Assert.AreEqual(_HyperEdgeId1.ToString(), _HyperEdgeId2.ToString());
            Assert.AreEqual(_HyperEdgeId1.Length,     _HyperEdgeId2.Length);
            Assert.AreEqual(_HyperEdgeId1,            _HyperEdgeId2);
        }

        #endregion

        #region HyperEdgeIdUriConstructorTest()

        /// <summary>
        /// A test for the HyperEdgeId Uri constructor.
        /// </summary>
        [Test]
        public void HyperEdgeIdUriConstructorTest()
        {
            var _HyperEdgeId = new HyperEdgeId(new Uri("http://example.org"));
            Assert.AreEqual("http://example.org/", _HyperEdgeId.ToString());
            Assert.AreEqual(19,                    _HyperEdgeId.Length);
        }

        #endregion


        #region NewHyperEdgeIdMethodTest()

        /// <summary>
        /// A test for the static newHyperEdgeId method.
        /// </summary>
        [Test]
        public void NewHyperEdgeIdMethodTest()
        {
            var _HyperEdgeId1 = HyperEdgeId.NewHyperEdgeId;
            var _HyperEdgeId2 = HyperEdgeId.NewHyperEdgeId;
            Assert.AreNotEqual(_HyperEdgeId1, _HyperEdgeId2);
        }

        #endregion


        #region op_Equality_Null_Test1()

        /// <summary>
        /// A test for the equality operator null.
        /// </summary>
        [Test]
        public void op_Equality_Null_Test1()
        {
            var      _HyperEdgeId1 = new HyperEdgeId();
            HyperEdgeId _HyperEdgeId2 = null;
            Assert.IsFalse(_HyperEdgeId1 == _HyperEdgeId2);
        }

        #endregion

        #region op_Equality_Null_Test2()

        /// <summary>
        /// A test for the equality operator null.
        /// </summary>
        [Test]
        public void op_Equality_Null_Test2()
        {
            HyperEdgeId _HyperEdgeId1 = null;
            var      _HyperEdgeId2 = new HyperEdgeId();
            Assert.IsFalse(_HyperEdgeId1 == _HyperEdgeId2);
        }

        #endregion

        #region op_Equality_BothNull_Test()

        /// <summary>
        /// A test for the equality operator both null.
        /// </summary>
        [Test]
        public void op_Equality_BothNull_Test()
        {
            HyperEdgeId _HyperEdgeId1 = null;
            HyperEdgeId _HyperEdgeId2 = null;
            Assert.IsTrue(_HyperEdgeId1 == _HyperEdgeId2);
        }

        #endregion

        #region op_Equality_SameReference_Test()

        /// <summary>
        /// A test for the equality operator same reference.
        /// </summary>
        [Test]
        
        public void op_Equality_SameReference_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId();
            #pragma warning disable
            Assert.IsTrue(_HyperEdgeId1 == _HyperEdgeId1);
            #pragma warning restore
        }

        #endregion

        #region op_Equality_Equals_Test()

        /// <summary>
        /// A test for the equality operator equals.
        /// </summary>
        [Test]
        public void op_Equality_Equals_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(1);
            var _HyperEdgeId2 = new HyperEdgeId(1);
            Assert.IsTrue(_HyperEdgeId1 == _HyperEdgeId2);
        }

        #endregion

        #region op_Equality_NotEquals_Test()

        /// <summary>
        /// A test for the equality operator not-equals.
        /// </summary>
        [Test]
        public void op_Equality_NotEquals_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(1);
            var _HyperEdgeId2 = new HyperEdgeId(2);
            Assert.IsFalse(_HyperEdgeId1 == _HyperEdgeId2);
        }

        #endregion


        #region op_Inequality_Null_Test1()

        /// <summary>
        /// A test for the inequality operator null.
        /// </summary>
        [Test]
        public void op_Inequality_Null_Test1()
        {
            var      _HyperEdgeId1 = new HyperEdgeId();
            HyperEdgeId _HyperEdgeId2 = null;
            Assert.IsTrue(_HyperEdgeId1 != _HyperEdgeId2);
        }

        #endregion

        #region op_Inequality_Null_Test2()

        /// <summary>
        /// A test for the inequality operator null.
        /// </summary>
        [Test]
        public void op_Inequality_Null_Test2()
        {
            HyperEdgeId _HyperEdgeId1 = null;
            var      _HyperEdgeId2 = new HyperEdgeId();
            Assert.IsTrue(_HyperEdgeId1 != _HyperEdgeId2);
        }

        #endregion

        #region op_Inequality_BothNull_Test()

        /// <summary>
        /// A test for the inequality operator both null.
        /// </summary>
        [Test]
        public void op_Inequality_BothNull_Test()
        {
            HyperEdgeId _HyperEdgeId1 = null;
            HyperEdgeId _HyperEdgeId2 = null;
            Assert.IsFalse(_HyperEdgeId1 != _HyperEdgeId2);
        }

        #endregion

        #region op_Inequality_SameReference_Test()

        /// <summary>
        /// A test for the inequality operator same reference.
        /// </summary>
        [Test]
        public void op_Inequality_SameReference_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId();
            #pragma warning disable
            Assert.IsFalse(_HyperEdgeId1 != _HyperEdgeId1);
            #pragma warning restore
        }

        #endregion

        #region op_Inequality_Equals_Test()

        /// <summary>
        /// A test for the inequality operator equals.
        /// </summary>
        [Test]
        public void op_Inequality_Equals_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(1);
            var _HyperEdgeId2 = new HyperEdgeId(1);
            Assert.IsFalse(_HyperEdgeId1 != _HyperEdgeId2);
        }

        #endregion

        #region op_Inequality_NotEquals1_Test()

        /// <summary>
        /// A test for the inequality operator not-equals.
        /// </summary>
        [Test]
        public void op_Inequality_NotEquals1_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(1);
            var _HyperEdgeId2 = new HyperEdgeId(2);
            Assert.IsTrue(_HyperEdgeId1 != _HyperEdgeId2);
        }

        #endregion

        #region op_Inequality_NotEquals2_Test()

        /// <summary>
        /// A test for the inequality operator not-equals.
        /// </summary>
        [Test]
        public void op_Inequality_NotEquals2_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(5);
            var _HyperEdgeId2 = new HyperEdgeId(23);
            Assert.IsTrue(_HyperEdgeId1 != _HyperEdgeId2);
        }

        #endregion


        #region op_Smaller_Null_Test1()

        /// <summary>
        /// A test for the smaller operator null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_Smaller_Null_Test1()
        {
            var      _HyperEdgeId1 = new HyperEdgeId();
            HyperEdgeId _HyperEdgeId2 = null;
            Assert.IsTrue(_HyperEdgeId1 < _HyperEdgeId2);
        }

        #endregion

        #region op_Smaller_Null_Test2()

        /// <summary>
        /// A test for the smaller operator null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_Smaller_Null_Test2()
        {
            HyperEdgeId _HyperEdgeId1 = null;
            var      _HyperEdgeId2 = new HyperEdgeId();
            Assert.IsTrue(_HyperEdgeId1 < _HyperEdgeId2);
        }

        #endregion

        #region op_Smaller_BothNull_Test()

        /// <summary>
        /// A test for the smaller operator both null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_Smaller_BothNull_Test()
        {
            HyperEdgeId _HyperEdgeId1 = null;
            HyperEdgeId _HyperEdgeId2 = null;
            Assert.IsFalse(_HyperEdgeId1 < _HyperEdgeId2);
        }

        #endregion

        #region op_Smaller_SameReference_Test()

        /// <summary>
        /// A test for the smaller operator same reference.
        /// </summary>
        [Test]
        public void op_Smaller_SameReference_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId();
            #pragma warning disable
            Assert.IsFalse(_HyperEdgeId1 < _HyperEdgeId1);
            #pragma warning restore
        }

        #endregion

        #region op_Smaller_Equals_Test()

        /// <summary>
        /// A test for the smaller operator equals.
        /// </summary>
        [Test]
        public void op_Smaller_Equals_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(1);
            var _HyperEdgeId2 = new HyperEdgeId(1);
            Assert.IsFalse(_HyperEdgeId1 < _HyperEdgeId2);
        }

        #endregion

        #region op_Smaller_Smaller1_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Smaller1_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(1);
            var _HyperEdgeId2 = new HyperEdgeId(2);
            Assert.IsTrue(_HyperEdgeId1 < _HyperEdgeId2);
        }

        #endregion

        #region op_Smaller_Smaller2_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Smaller2_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(5);
            var _HyperEdgeId2 = new HyperEdgeId(23);
            Assert.IsTrue(_HyperEdgeId1 < _HyperEdgeId2);
        }

        #endregion

        #region op_Smaller_Bigger1_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Bigger1_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(2);
            var _HyperEdgeId2 = new HyperEdgeId(1);
            Assert.IsFalse(_HyperEdgeId1 < _HyperEdgeId2);
        }

        #endregion

        #region op_Smaller_Bigger2_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Bigger2_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(23);
            var _HyperEdgeId2 = new HyperEdgeId(5);
            Assert.IsFalse(_HyperEdgeId1 < _HyperEdgeId2);
        }

        #endregion


        #region op_SmallerOrEqual_Null_Test1()

        /// <summary>
        /// A test for the smallerOrEqual operator null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_SmallerOrEqual_Null_Test1()
        {
            var      _HyperEdgeId1 = new HyperEdgeId();
            HyperEdgeId _HyperEdgeId2 = null;
            Assert.IsTrue(_HyperEdgeId1 <= _HyperEdgeId2);
        }

        #endregion

        #region op_SmallerOrEqual_Null_Test2()

        /// <summary>
        /// A test for the smallerOrEqual operator null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_SmallerOrEqual_Null_Test2()
        {
            HyperEdgeId _HyperEdgeId1 = null;
            var      _HyperEdgeId2 = new HyperEdgeId();
            Assert.IsTrue(_HyperEdgeId1 <= _HyperEdgeId2);
        }

        #endregion

        #region op_SmallerOrEqual_BothNull_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator both null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_SmallerOrEqual_BothNull_Test()
        {
            HyperEdgeId _HyperEdgeId1 = null;
            HyperEdgeId _HyperEdgeId2 = null;
            Assert.IsFalse(_HyperEdgeId1 <= _HyperEdgeId2);
        }

        #endregion

        #region op_SmallerOrEqual_SameReference_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator same reference.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_SameReference_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId();
            #pragma warning disable
            Assert.IsTrue(_HyperEdgeId1 <= _HyperEdgeId1);
            #pragma warning restore
        }

        #endregion

        #region op_SmallerOrEqual_Equals_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_Equals_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(1);
            var _HyperEdgeId2 = new HyperEdgeId(1);
            Assert.IsTrue(_HyperEdgeId1 <= _HyperEdgeId2);
        }

        #endregion

        #region op_SmallerOrEqual_SmallerThan1_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_SmallerThan1_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(1);
            var _HyperEdgeId2 = new HyperEdgeId(2);
            Assert.IsTrue(_HyperEdgeId1 <= _HyperEdgeId2);
        }

        #endregion

        #region op_SmallerOrEqual_SmallerThan2_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_SmallerThan2_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(5);
            var _HyperEdgeId2 = new HyperEdgeId(23);
            Assert.IsTrue(_HyperEdgeId1 <= _HyperEdgeId2);
        }

        #endregion

        #region op_SmallerOrEqual_Bigger1_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_Bigger1_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(2);
            var _HyperEdgeId2 = new HyperEdgeId(1);
            Assert.IsFalse(_HyperEdgeId1 <= _HyperEdgeId2);
        }

        #endregion

        #region op_SmallerOrEqual_Bigger2_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_Bigger2_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(23);
            var _HyperEdgeId2 = new HyperEdgeId(5);
            Assert.IsFalse(_HyperEdgeId1 <= _HyperEdgeId2);
        }

        #endregion


        #region op_Bigger_Null_Test1()

        /// <summary>
        /// A test for the bigger operator null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_Bigger_Null_Test1()
        {
            var      _HyperEdgeId1 = new HyperEdgeId();
            HyperEdgeId _HyperEdgeId2 = null;
            Assert.IsTrue(_HyperEdgeId1 > _HyperEdgeId2);
        }

        #endregion

        #region op_Bigger_Null_Test2()

        /// <summary>
        /// A test for the bigger operator null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_Bigger_Null_Test2()
        {
            HyperEdgeId _HyperEdgeId1 = null;
            var      _HyperEdgeId2 = new HyperEdgeId();
            Assert.IsTrue(_HyperEdgeId1 > _HyperEdgeId2);
        }

        #endregion

        #region op_Bigger_BothNull_Test()

        /// <summary>
        /// A test for the bigger operator both null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_Bigger_BothNull_Test()
        {
            HyperEdgeId _HyperEdgeId1 = null;
            HyperEdgeId _HyperEdgeId2 = null;
            Assert.IsFalse(_HyperEdgeId1 > _HyperEdgeId2);
        }

        #endregion

        #region op_Bigger_SameReference_Test()

        /// <summary>
        /// A test for the bigger operator same reference.
        /// </summary>
        [Test]
        public void op_Bigger_SameReference_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId();
            #pragma warning disable
            Assert.IsFalse(_HyperEdgeId1 > _HyperEdgeId1);
            #pragma warning restore
        }

        #endregion

        #region op_Bigger_Equals_Test()

        /// <summary>
        /// A test for the bigger operator equals.
        /// </summary>
        [Test]
        public void op_Bigger_Equals_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(1);
            var _HyperEdgeId2 = new HyperEdgeId(1);
            Assert.IsFalse(_HyperEdgeId1 > _HyperEdgeId2);
        }

        #endregion

        #region op_Bigger_Smaller1_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Smaller1_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(1);
            var _HyperEdgeId2 = new HyperEdgeId(2);
            Assert.IsFalse(_HyperEdgeId1 > _HyperEdgeId2);
        }

        #endregion

        #region op_Bigger_Smaller2_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Smaller2_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(5);
            var _HyperEdgeId2 = new HyperEdgeId(23);
            Assert.IsFalse(_HyperEdgeId1 > _HyperEdgeId2);
        }

        #endregion

        #region op_Bigger_Bigger1_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Bigger1_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(2);
            var _HyperEdgeId2 = new HyperEdgeId(1);
            Assert.IsTrue(_HyperEdgeId1 > _HyperEdgeId2);
        }

        #endregion

        #region op_Bigger_Bigger2_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Bigger2_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(23);
            var _HyperEdgeId2 = new HyperEdgeId(5);
            Assert.IsTrue(_HyperEdgeId1 > _HyperEdgeId2);
        }

        #endregion


        #region op_BiggerOrEqual_Null_Test1()

        /// <summary>
        /// A test for the biggerOrEqual operator null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_BiggerOrEqual_Null_Test1()
        {
            var      _HyperEdgeId1 = new HyperEdgeId();
            HyperEdgeId _HyperEdgeId2 = null;
            Assert.IsTrue(_HyperEdgeId1 >= _HyperEdgeId2);
        }

        #endregion

        #region op_BiggerOrEqual_Null_Test2()

        /// <summary>
        /// A test for the biggerOrEqual operator null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_BiggerOrEqual_Null_Test2()
        {
            HyperEdgeId _HyperEdgeId1 = null;
            var      _HyperEdgeId2 = new HyperEdgeId();
            Assert.IsTrue(_HyperEdgeId1 >= _HyperEdgeId2);
        }

        #endregion

        #region op_BiggerOrEqual_BothNull_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator both null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void op_BiggerOrEqual_BothNull_Test()
        {
            HyperEdgeId _HyperEdgeId1 = null;
            HyperEdgeId _HyperEdgeId2 = null;
            Assert.IsFalse(_HyperEdgeId1 >= _HyperEdgeId2);
        }

        #endregion

        #region op_BiggerOrEqual_SameReference_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator same reference.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_SameReference_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId();
            #pragma warning disable
            Assert.IsTrue(_HyperEdgeId1 >= _HyperEdgeId1);
            #pragma warning restore
        }

        #endregion

        #region op_BiggerOrEqual_Equals_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_Equals_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(1);
            var _HyperEdgeId2 = new HyperEdgeId(1);
            Assert.IsTrue(_HyperEdgeId1 >= _HyperEdgeId2);
        }

        #endregion

        #region op_BiggerOrEqual_SmallerThan1_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_SmallerThan1_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(1);
            var _HyperEdgeId2 = new HyperEdgeId(2);
            Assert.IsFalse(_HyperEdgeId1 >= _HyperEdgeId2);
        }

        #endregion

        #region op_BiggerOrEqual_SmallerThan2_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_SmallerThan2_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(5);
            var _HyperEdgeId2 = new HyperEdgeId(23);
            Assert.IsFalse(_HyperEdgeId1 >= _HyperEdgeId2);
        }

        #endregion

        #region op_BiggerOrEqual_Bigger1_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_Bigger1_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(2);
            var _HyperEdgeId2 = new HyperEdgeId(1);
            Assert.IsTrue(_HyperEdgeId1 >= _HyperEdgeId2);
        }

        #endregion

        #region op_BiggerOrEqual_Bigger2_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_Bigger2_Test()
        {
            var _HyperEdgeId1 = new HyperEdgeId(23);
            var _HyperEdgeId2 = new HyperEdgeId(5);
            Assert.IsTrue(_HyperEdgeId1 >= _HyperEdgeId2);
        }

        #endregion


        #region CompareToNullTest1()

        /// <summary>
        /// A test for CompareTo null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CompareToNullTest1()
        {
            var    _HyperEdgeId = HyperEdgeId.NewHyperEdgeId;
            Object _Object   = null;
            _HyperEdgeId.CompareTo(_Object);
        }

        #endregion

        #region CompareToNullTest2()

        /// <summary>
        /// A test for CompareTo null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CompareToNullTest2()
        {
            var      _HyperEdgeId = HyperEdgeId.NewHyperEdgeId;
            HyperEdgeId _Object   = null;
            _HyperEdgeId.CompareTo(_Object);
        }

        #endregion

        #region CompareToNonHyperEdgeIdTest()

        /// <summary>
        /// A test for CompareTo a non-HyperEdgeId.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CompareToNonHyperEdgeIdTest()
        {
            var _HyperEdgeId = HyperEdgeId.NewHyperEdgeId;
            var _Object   = "123";
            _HyperEdgeId.CompareTo(_Object);
        }

        #endregion

        #region CompareToSmallerTest1()

        /// <summary>
        /// A test for CompareTo smaller.
        /// </summary>
        [Test]
        public void CompareToSmallerTest1()
        {
            var _HyperEdgeId1 = new HyperEdgeId(1);
            var _HyperEdgeId2 = new HyperEdgeId(2);
            Assert.IsTrue(_HyperEdgeId1.CompareTo(_HyperEdgeId2) < 0);
        }

        #endregion

        #region CompareToSmallerTest2()

        /// <summary>
        /// A test for CompareTo smaller.
        /// </summary>
        [Test]
        public void CompareToSmallerTest2()
        {
            var _HyperEdgeId1 = new HyperEdgeId(5);
            var _HyperEdgeId2 = new HyperEdgeId(23);
            Assert.IsTrue(_HyperEdgeId1.CompareTo(_HyperEdgeId2) < 0);
        }

        #endregion

        #region CompareToEqualsTest()

        /// <summary>
        /// A test for CompareTo equals.
        /// </summary>
        [Test]
        public void CompareToEqualsTest()
        {
            var _HyperEdgeId1 = new HyperEdgeId(1);
            var _HyperEdgeId2 = new HyperEdgeId(1);
            Assert.IsTrue(_HyperEdgeId1.CompareTo(_HyperEdgeId2) == 0);
        }

        #endregion

        #region CompareToBiggerTest()

        /// <summary>
        /// A test for CompareTo bigger.
        /// </summary>
        [Test]
        public void CompareToBiggerTest()
        {
            var _HyperEdgeId1 = new HyperEdgeId(2);
            var _HyperEdgeId2 = new HyperEdgeId(1);
            Assert.IsTrue(_HyperEdgeId1.CompareTo(_HyperEdgeId2) > 0);
        }

        #endregion


        #region EqualsNullTest1()

        /// <summary>
        /// A test for equals null.
        /// </summary>
        [Test]
        public void EqualsNullTest1()
        {
            var    _HyperEdgeId = HyperEdgeId.NewHyperEdgeId;
            Object _Object   = null;
            Assert.IsFalse(_HyperEdgeId.Equals(_Object));
        }

        #endregion

        #region EqualsNullTest2()

        /// <summary>
        /// A test for equals null.
        /// </summary>
        [Test]
        public void EqualsNullTest2()
        {
            var      _HyperEdgeId = HyperEdgeId.NewHyperEdgeId;
            HyperEdgeId _Object   = null;
            Assert.IsFalse(_HyperEdgeId.Equals(_Object));
        }

        #endregion

        #region EqualsNonHyperEdgeIdTest()

        /// <summary>
        /// A test for equals a non-HyperEdgeId.
        /// </summary>
        [Test]
        public void EqualsNonHyperEdgeIdTest()
        {
            var _HyperEdgeId = HyperEdgeId.NewHyperEdgeId;
            var _Object   = "123";
            Assert.IsFalse(_HyperEdgeId.Equals(_Object));
        }

        #endregion

        #region EqualsEqualsTest()

        /// <summary>
        /// A test for equals.
        /// </summary>
        [Test]
        public void EqualsEqualsTest()
        {
            var _HyperEdgeId1 = new HyperEdgeId(1);
            var _HyperEdgeId2 = new HyperEdgeId(1);
            Assert.IsTrue(_HyperEdgeId1.Equals(_HyperEdgeId2));
        }

        #endregion

        #region EqualsNotEqualsTest()

        /// <summary>
        /// A test for not-equals.
        /// </summary>
        [Test]
        public void EqualsNotEqualsTest()
        {
            var _HyperEdgeId1 = new HyperEdgeId(1);
            var _HyperEdgeId2 = new HyperEdgeId(2);
            Assert.IsFalse(_HyperEdgeId1.Equals(_HyperEdgeId2));
        }

        #endregion


        #region GetHashCodeEqualTest()

        /// <summary>
        /// A test for GetHashCode
        /// </summary>
        [Test]
        public void GetHashCodeEqualTest()
        {
            var _SensorHashCode1 = new HyperEdgeId(5).GetHashCode();
            var _SensorHashCode2 = new HyperEdgeId(5).GetHashCode();
            Assert.AreEqual(_SensorHashCode1, _SensorHashCode2);
        }

        #endregion

        #region GetHashCodeNotEqualTest()

        /// <summary>
        /// A test for GetHashCode
        /// </summary>
        [Test]
        public void GetHashCodeNotEqualTest()
        {
            var _SensorHashCode1 = new HyperEdgeId(1).GetHashCode();
            var _SensorHashCode2 = new HyperEdgeId(2).GetHashCode();
            Assert.AreNotEqual(_SensorHashCode1, _SensorHashCode2);
        }

        #endregion


        #region HyperEdgeIdsAndNUnitTest()

        /// <summary>
        /// Tests HyperEdgeIds in combination with NUnit.
        /// </summary>
        [Test]
        public void HyperEdgeIdsAndNUnitTest()
        {

            var a = new HyperEdgeId(1);
            var b = new HyperEdgeId(2);
            var c = new HyperEdgeId(1);

            Assert.AreEqual(a, a);
            Assert.AreEqual(b, b);
            Assert.AreEqual(c, c);

            Assert.AreEqual(a, c);
            Assert.AreNotEqual(a, b);
            Assert.AreNotEqual(b, c);

        }

        #endregion

        #region HyperEdgeIdsInHashSetTest()

        /// <summary>
        /// Test HyperEdgeIds within a HashSet.
        /// </summary>
        [Test]
        public void HyperEdgeIdsInHashSetTest()
        {

            var a = new HyperEdgeId(1);
            var b = new HyperEdgeId(2);
            var c = new HyperEdgeId(1);

            var _HashSet = new HashSet<HyperEdgeId>();
            Assert.AreEqual(0, _HashSet.Count);

            _HashSet.Add(a);
            Assert.AreEqual(1, _HashSet.Count);

            _HashSet.Add(b);
            Assert.AreEqual(2, _HashSet.Count);

            _HashSet.Add(c);
            Assert.AreEqual(2, _HashSet.Count);

        }

        #endregion

    }

}
