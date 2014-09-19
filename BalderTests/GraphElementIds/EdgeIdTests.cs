/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Balder <http://www.github.com/Vanaheimr/Balder>
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
using org.GraphDefined.Vanaheimr.Balder;

#endregion

namespace org.GraphDefined.Vanaheimr.Balder.UnitTests.GraphElementIds
{

    /// <summary>
    /// Unit tests for the EdgeId class.
    /// </summary>
    [TestFixture]
    public class EdgeIdTests
    {

        #region EdgeIdEmptyConstructorTest()

        /// <summary>
        /// A test for an empty EdgeId constructor.
        /// </summary>
        [Test]
        public void EdgeIdEmptyConstructorTest()
        {
            var _EdgeId1 = new EdgeId();
            var _EdgeId2 = new EdgeId();
            Assert.IsTrue(_EdgeId1.Length > 0);
            Assert.IsTrue(_EdgeId2.Length > 0);
            Assert.AreNotEqual(_EdgeId1, _EdgeId2);
        }

        #endregion

        #region EdgeIdStringConstructorTest()

        /// <summary>
        /// A test for the EdgeId string constructor.
        /// </summary>
        [Test]
        public void EdgeIdStringConstructorTest()
        {
            var _EdgeId = new EdgeId("123");
            Assert.AreEqual("123", _EdgeId.ToString());
            Assert.AreEqual(3,     _EdgeId.Length);
        }

        #endregion

        #region EdgeIdInt32ConstructorTest()

        /// <summary>
        /// A test for the EdgeId Int32 constructor.
        /// </summary>
        [Test]
        public void EdgeIdInt32ConstructorTest()
        {
            var _EdgeId = new EdgeId(5);
            Assert.AreEqual("5", _EdgeId.ToString());
            Assert.AreEqual(1,   _EdgeId.Length);
        }

        #endregion

        #region EdgeIdUInt32ConstructorTest()

        /// <summary>
        /// A test for the EdgeId UInt32 constructor.
        /// </summary>
        [Test]
        public void EdgeIdUInt32ConstructorTest()
        {
            var _EdgeId = new EdgeId(23U);
            Assert.AreEqual("23", _EdgeId.ToString());
            Assert.AreEqual(2,    _EdgeId.Length);
        }

        #endregion

        #region EdgeIdInt64ConstructorTest()

        /// <summary>
        /// A test for the EdgeId Int64 constructor.
        /// </summary>
        [Test]
        public void EdgeIdInt64ConstructorTest()
        {
            var _EdgeId = new EdgeId(42L);
            Assert.AreEqual("42", _EdgeId.ToString());
            Assert.AreEqual(2,    _EdgeId.Length);
        }

        #endregion

        #region EdgeIdUInt64ConstructorTest()

        /// <summary>
        /// A test for the EdgeId UInt64 constructor.
        /// </summary>
        [Test]
        public void EdgeIdUInt64ConstructorTest()
        {
            var _EdgeId = new EdgeId(123UL);
            Assert.AreEqual("123", _EdgeId.ToString());
            Assert.AreEqual(3,     _EdgeId.Length);
        }

        #endregion

        #region EdgeIdEdgeIdConstructorTest()

        /// <summary>
        /// A test for the EdgeId EdgeId constructor.
        /// </summary>
        [Test]
        public void EdgeIdEdgeIdConstructorTest()
        {
            var _EdgeId1 = EdgeId.NewEdgeId;
            var _EdgeId2 = new EdgeId(_EdgeId1);
            Assert.AreEqual(_EdgeId1.ToString(), _EdgeId2.ToString());
            Assert.AreEqual(_EdgeId1.Length,     _EdgeId2.Length);
            Assert.AreEqual(_EdgeId1,            _EdgeId2);
        }

        #endregion

        #region EdgeIdUriConstructorTest()

        /// <summary>
        /// A test for the EdgeId Uri constructor.
        /// </summary>
        [Test]
        public void EdgeIdUriConstructorTest()
        {
            var _EdgeId = new EdgeId(new Uri("http://example.org"));
            Assert.AreEqual("http://example.org/", _EdgeId.ToString());
            Assert.AreEqual(19,                    _EdgeId.Length);
        }

        #endregion


        #region NewEdgeIdMethodTest()

        /// <summary>
        /// A test for the static newEdgeId method.
        /// </summary>
        [Test]
        public void NewEdgeIdMethodTest()
        {
            var _EdgeId1 = EdgeId.NewEdgeId;
            var _EdgeId2 = EdgeId.NewEdgeId;
            Assert.AreNotEqual(_EdgeId1, _EdgeId2);
        }

        #endregion


        #region op_Equality_Null_Test1()

        /// <summary>
        /// A test for the equality operator null.
        /// </summary>
        [Test]
        public void op_Equality_Null_Test1()
        {
            var      _EdgeId1 = new EdgeId();
            EdgeId _EdgeId2 = null;
            Assert.IsFalse(_EdgeId1 == _EdgeId2);
        }

        #endregion

        #region op_Equality_Null_Test2()

        /// <summary>
        /// A test for the equality operator null.
        /// </summary>
        [Test]
        public void op_Equality_Null_Test2()
        {
            EdgeId _EdgeId1 = null;
            var    _EdgeId2 = new EdgeId();
            Assert.IsFalse(_EdgeId1 == _EdgeId2);
        }

        #endregion

        #region op_Equality_BothNull_Test()

        /// <summary>
        /// A test for the equality operator both null.
        /// </summary>
        [Test]
        public void op_Equality_BothNull_Test()
        {
            EdgeId _EdgeId1 = null;
            EdgeId _EdgeId2 = null;
            Assert.IsTrue(_EdgeId1 == _EdgeId2);
        }

        #endregion

        #region op_Equality_SameReference_Test()

        /// <summary>
        /// A test for the equality operator same reference.
        /// </summary>
        [Test]
        
        public void op_Equality_SameReference_Test()
        {
            var _EdgeId1 = new EdgeId();
            #pragma warning disable
            Assert.IsTrue(_EdgeId1 == _EdgeId1);
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
            var _EdgeId1 = new EdgeId(1);
            var _EdgeId2 = new EdgeId(1);
            Assert.IsTrue(_EdgeId1 == _EdgeId2);
        }

        #endregion

        #region op_Equality_NotEquals_Test()

        /// <summary>
        /// A test for the equality operator not-equals.
        /// </summary>
        [Test]
        public void op_Equality_NotEquals_Test()
        {
            var _EdgeId1 = new EdgeId(1);
            var _EdgeId2 = new EdgeId(2);
            Assert.IsFalse(_EdgeId1 == _EdgeId2);
        }

        #endregion


        #region op_Inequality_Null_Test1()

        /// <summary>
        /// A test for the inequality operator null.
        /// </summary>
        [Test]
        public void op_Inequality_Null_Test1()
        {
            var      _EdgeId1 = new EdgeId();
            EdgeId _EdgeId2 = null;
            Assert.IsTrue(_EdgeId1 != _EdgeId2);
        }

        #endregion

        #region op_Inequality_Null_Test2()

        /// <summary>
        /// A test for the inequality operator null.
        /// </summary>
        [Test]
        public void op_Inequality_Null_Test2()
        {
            EdgeId _EdgeId1 = null;
            var      _EdgeId2 = new EdgeId();
            Assert.IsTrue(_EdgeId1 != _EdgeId2);
        }

        #endregion

        #region op_Inequality_BothNull_Test()

        /// <summary>
        /// A test for the inequality operator both null.
        /// </summary>
        [Test]
        public void op_Inequality_BothNull_Test()
        {
            EdgeId _EdgeId1 = null;
            EdgeId _EdgeId2 = null;
            Assert.IsFalse(_EdgeId1 != _EdgeId2);
        }

        #endregion

        #region op_Inequality_SameReference_Test()

        /// <summary>
        /// A test for the inequality operator same reference.
        /// </summary>
        [Test]
        public void op_Inequality_SameReference_Test()
        {
            var _EdgeId1 = new EdgeId();
            #pragma warning disable
            Assert.IsFalse(_EdgeId1 != _EdgeId1);
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
            var _EdgeId1 = new EdgeId(1);
            var _EdgeId2 = new EdgeId(1);
            Assert.IsFalse(_EdgeId1 != _EdgeId2);
        }

        #endregion

        #region op_Inequality_NotEquals1_Test()

        /// <summary>
        /// A test for the inequality operator not-equals.
        /// </summary>
        [Test]
        public void op_Inequality_NotEquals1_Test()
        {
            var _EdgeId1 = new EdgeId(1);
            var _EdgeId2 = new EdgeId(2);
            Assert.IsTrue(_EdgeId1 != _EdgeId2);
        }

        #endregion

        #region op_Inequality_NotEquals2_Test()

        /// <summary>
        /// A test for the inequality operator not-equals.
        /// </summary>
        [Test]
        public void op_Inequality_NotEquals2_Test()
        {
            var _EdgeId1 = new EdgeId(5);
            var _EdgeId2 = new EdgeId(23);
            Assert.IsTrue(_EdgeId1 != _EdgeId2);
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
            var      _EdgeId1 = new EdgeId();
            EdgeId _EdgeId2 = null;
            Assert.IsTrue(_EdgeId1 < _EdgeId2);
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
            EdgeId _EdgeId1 = null;
            var      _EdgeId2 = new EdgeId();
            Assert.IsTrue(_EdgeId1 < _EdgeId2);
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
            EdgeId _EdgeId1 = null;
            EdgeId _EdgeId2 = null;
            Assert.IsFalse(_EdgeId1 < _EdgeId2);
        }

        #endregion

        #region op_Smaller_SameReference_Test()

        /// <summary>
        /// A test for the smaller operator same reference.
        /// </summary>
        [Test]
        public void op_Smaller_SameReference_Test()
        {
            var _EdgeId1 = new EdgeId();
            #pragma warning disable
            Assert.IsFalse(_EdgeId1 < _EdgeId1);
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
            var _EdgeId1 = new EdgeId(1);
            var _EdgeId2 = new EdgeId(1);
            Assert.IsFalse(_EdgeId1 < _EdgeId2);
        }

        #endregion

        #region op_Smaller_Smaller1_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Smaller1_Test()
        {
            var _EdgeId1 = new EdgeId(1);
            var _EdgeId2 = new EdgeId(2);
            Assert.IsTrue(_EdgeId1 < _EdgeId2);
        }

        #endregion

        #region op_Smaller_Smaller2_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Smaller2_Test()
        {
            var _EdgeId1 = new EdgeId(5);
            var _EdgeId2 = new EdgeId(23);
            Assert.IsTrue(_EdgeId1 < _EdgeId2);
        }

        #endregion

        #region op_Smaller_Bigger1_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Bigger1_Test()
        {
            var _EdgeId1 = new EdgeId(2);
            var _EdgeId2 = new EdgeId(1);
            Assert.IsFalse(_EdgeId1 < _EdgeId2);
        }

        #endregion

        #region op_Smaller_Bigger2_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Bigger2_Test()
        {
            var _EdgeId1 = new EdgeId(23);
            var _EdgeId2 = new EdgeId(5);
            Assert.IsFalse(_EdgeId1 < _EdgeId2);
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
            var      _EdgeId1 = new EdgeId();
            EdgeId _EdgeId2 = null;
            Assert.IsTrue(_EdgeId1 <= _EdgeId2);
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
            EdgeId _EdgeId1 = null;
            var      _EdgeId2 = new EdgeId();
            Assert.IsTrue(_EdgeId1 <= _EdgeId2);
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
            EdgeId _EdgeId1 = null;
            EdgeId _EdgeId2 = null;
            Assert.IsFalse(_EdgeId1 <= _EdgeId2);
        }

        #endregion

        #region op_SmallerOrEqual_SameReference_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator same reference.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_SameReference_Test()
        {
            var _EdgeId1 = new EdgeId();
            #pragma warning disable
            Assert.IsTrue(_EdgeId1 <= _EdgeId1);
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
            var _EdgeId1 = new EdgeId(1);
            var _EdgeId2 = new EdgeId(1);
            Assert.IsTrue(_EdgeId1 <= _EdgeId2);
        }

        #endregion

        #region op_SmallerOrEqual_SmallerThan1_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_SmallerThan1_Test()
        {
            var _EdgeId1 = new EdgeId(1);
            var _EdgeId2 = new EdgeId(2);
            Assert.IsTrue(_EdgeId1 <= _EdgeId2);
        }

        #endregion

        #region op_SmallerOrEqual_SmallerThan2_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_SmallerThan2_Test()
        {
            var _EdgeId1 = new EdgeId(5);
            var _EdgeId2 = new EdgeId(23);
            Assert.IsTrue(_EdgeId1 <= _EdgeId2);
        }

        #endregion

        #region op_SmallerOrEqual_Bigger1_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_Bigger1_Test()
        {
            var _EdgeId1 = new EdgeId(2);
            var _EdgeId2 = new EdgeId(1);
            Assert.IsFalse(_EdgeId1 <= _EdgeId2);
        }

        #endregion

        #region op_SmallerOrEqual_Bigger2_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_Bigger2_Test()
        {
            var _EdgeId1 = new EdgeId(23);
            var _EdgeId2 = new EdgeId(5);
            Assert.IsFalse(_EdgeId1 <= _EdgeId2);
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
            var      _EdgeId1 = new EdgeId();
            EdgeId _EdgeId2 = null;
            Assert.IsTrue(_EdgeId1 > _EdgeId2);
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
            EdgeId _EdgeId1 = null;
            var      _EdgeId2 = new EdgeId();
            Assert.IsTrue(_EdgeId1 > _EdgeId2);
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
            EdgeId _EdgeId1 = null;
            EdgeId _EdgeId2 = null;
            Assert.IsFalse(_EdgeId1 > _EdgeId2);
        }

        #endregion

        #region op_Bigger_SameReference_Test()

        /// <summary>
        /// A test for the bigger operator same reference.
        /// </summary>
        [Test]
        public void op_Bigger_SameReference_Test()
        {
            var _EdgeId1 = new EdgeId();
            #pragma warning disable
            Assert.IsFalse(_EdgeId1 > _EdgeId1);
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
            var _EdgeId1 = new EdgeId(1);
            var _EdgeId2 = new EdgeId(1);
            Assert.IsFalse(_EdgeId1 > _EdgeId2);
        }

        #endregion

        #region op_Bigger_Smaller1_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Smaller1_Test()
        {
            var _EdgeId1 = new EdgeId(1);
            var _EdgeId2 = new EdgeId(2);
            Assert.IsFalse(_EdgeId1 > _EdgeId2);
        }

        #endregion

        #region op_Bigger_Smaller2_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Smaller2_Test()
        {
            var _EdgeId1 = new EdgeId(5);
            var _EdgeId2 = new EdgeId(23);
            Assert.IsFalse(_EdgeId1 > _EdgeId2);
        }

        #endregion

        #region op_Bigger_Bigger1_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Bigger1_Test()
        {
            var _EdgeId1 = new EdgeId(2);
            var _EdgeId2 = new EdgeId(1);
            Assert.IsTrue(_EdgeId1 > _EdgeId2);
        }

        #endregion

        #region op_Bigger_Bigger2_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Bigger2_Test()
        {
            var _EdgeId1 = new EdgeId(23);
            var _EdgeId2 = new EdgeId(5);
            Assert.IsTrue(_EdgeId1 > _EdgeId2);
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
            var      _EdgeId1 = new EdgeId();
            EdgeId _EdgeId2 = null;
            Assert.IsTrue(_EdgeId1 >= _EdgeId2);
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
            EdgeId _EdgeId1 = null;
            var      _EdgeId2 = new EdgeId();
            Assert.IsTrue(_EdgeId1 >= _EdgeId2);
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
            EdgeId _EdgeId1 = null;
            EdgeId _EdgeId2 = null;
            Assert.IsFalse(_EdgeId1 >= _EdgeId2);
        }

        #endregion

        #region op_BiggerOrEqual_SameReference_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator same reference.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_SameReference_Test()
        {
            var _EdgeId1 = new EdgeId();
            #pragma warning disable
            Assert.IsTrue(_EdgeId1 >= _EdgeId1);
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
            var _EdgeId1 = new EdgeId(1);
            var _EdgeId2 = new EdgeId(1);
            Assert.IsTrue(_EdgeId1 >= _EdgeId2);
        }

        #endregion

        #region op_BiggerOrEqual_SmallerThan1_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_SmallerThan1_Test()
        {
            var _EdgeId1 = new EdgeId(1);
            var _EdgeId2 = new EdgeId(2);
            Assert.IsFalse(_EdgeId1 >= _EdgeId2);
        }

        #endregion

        #region op_BiggerOrEqual_SmallerThan2_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_SmallerThan2_Test()
        {
            var _EdgeId1 = new EdgeId(5);
            var _EdgeId2 = new EdgeId(23);
            Assert.IsFalse(_EdgeId1 >= _EdgeId2);
        }

        #endregion

        #region op_BiggerOrEqual_Bigger1_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_Bigger1_Test()
        {
            var _EdgeId1 = new EdgeId(2);
            var _EdgeId2 = new EdgeId(1);
            Assert.IsTrue(_EdgeId1 >= _EdgeId2);
        }

        #endregion

        #region op_BiggerOrEqual_Bigger2_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_Bigger2_Test()
        {
            var _EdgeId1 = new EdgeId(23);
            var _EdgeId2 = new EdgeId(5);
            Assert.IsTrue(_EdgeId1 >= _EdgeId2);
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
            var    _EdgeId = EdgeId.NewEdgeId;
            Object _Object   = null;
            _EdgeId.CompareTo(_Object);
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
            var      _EdgeId = EdgeId.NewEdgeId;
            EdgeId _Object   = null;
            _EdgeId.CompareTo(_Object);
        }

        #endregion

        #region CompareToNonEdgeIdTest()

        /// <summary>
        /// A test for CompareTo a non-EdgeId.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CompareToNonEdgeIdTest()
        {
            var _EdgeId = EdgeId.NewEdgeId;
            var _Object   = "123";
            _EdgeId.CompareTo(_Object);
        }

        #endregion

        #region CompareToSmallerTest1()

        /// <summary>
        /// A test for CompareTo smaller.
        /// </summary>
        [Test]
        public void CompareToSmallerTest1()
        {
            var _EdgeId1 = new EdgeId(1);
            var _EdgeId2 = new EdgeId(2);
            Assert.IsTrue(_EdgeId1.CompareTo(_EdgeId2) < 0);
        }

        #endregion

        #region CompareToSmallerTest2()

        /// <summary>
        /// A test for CompareTo smaller.
        /// </summary>
        [Test]
        public void CompareToSmallerTest2()
        {
            var _EdgeId1 = new EdgeId(5);
            var _EdgeId2 = new EdgeId(23);
            Assert.IsTrue(_EdgeId1.CompareTo(_EdgeId2) < 0);
        }

        #endregion

        #region CompareToEqualsTest()

        /// <summary>
        /// A test for CompareTo equals.
        /// </summary>
        [Test]
        public void CompareToEqualsTest()
        {
            var _EdgeId1 = new EdgeId(1);
            var _EdgeId2 = new EdgeId(1);
            Assert.IsTrue(_EdgeId1.CompareTo(_EdgeId2) == 0);
        }

        #endregion

        #region CompareToBiggerTest()

        /// <summary>
        /// A test for CompareTo bigger.
        /// </summary>
        [Test]
        public void CompareToBiggerTest()
        {
            var _EdgeId1 = new EdgeId(2);
            var _EdgeId2 = new EdgeId(1);
            Assert.IsTrue(_EdgeId1.CompareTo(_EdgeId2) > 0);
        }

        #endregion


        #region EqualsNullTest1()

        /// <summary>
        /// A test for equals null.
        /// </summary>
        [Test]
        public void EqualsNullTest1()
        {
            var    _EdgeId = EdgeId.NewEdgeId;
            Object _Object   = null;
            Assert.IsFalse(_EdgeId.Equals(_Object));
        }

        #endregion

        #region EqualsNullTest2()

        /// <summary>
        /// A test for equals null.
        /// </summary>
        [Test]
        public void EqualsNullTest2()
        {
            var    _EdgeId = EdgeId.NewEdgeId;
            EdgeId _Object   = null;
            Assert.IsFalse(_EdgeId.Equals(_Object));
        }

        #endregion

        #region EqualsNonEdgeIdTest()

        /// <summary>
        /// A test for equals a non-EdgeId.
        /// </summary>
        [Test]
        public void EqualsNonEdgeIdTest()
        {
            var _EdgeId = EdgeId.NewEdgeId;
            var _Object   = "123";
            Assert.IsFalse(_EdgeId.Equals(_Object));
        }

        #endregion

        #region EqualsEqualsTest()

        /// <summary>
        /// A test for equals.
        /// </summary>
        [Test]
        public void EqualsEqualsTest()
        {
            var _EdgeId1 = new EdgeId(1);
            var _EdgeId2 = new EdgeId(1);
            Assert.IsTrue(_EdgeId1.Equals(_EdgeId2));
        }

        #endregion

        #region EqualsNotEqualsTest()

        /// <summary>
        /// A test for not-equals.
        /// </summary>
        [Test]
        public void EqualsNotEqualsTest()
        {
            var _EdgeId1 = new EdgeId(1);
            var _EdgeId2 = new EdgeId(2);
            Assert.IsFalse(_EdgeId1.Equals(_EdgeId2));
        }

        #endregion


        #region GetHashCodeEqualTest()

        /// <summary>
        /// A test for GetHashCode
        /// </summary>
        [Test]
        public void GetHashCodeEqualTest()
        {
            var _SensorHashCode1 = new EdgeId(5).GetHashCode();
            var _SensorHashCode2 = new EdgeId(5).GetHashCode();
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
            var _SensorHashCode1 = new EdgeId(1).GetHashCode();
            var _SensorHashCode2 = new EdgeId(2).GetHashCode();
            Assert.AreNotEqual(_SensorHashCode1, _SensorHashCode2);
        }

        #endregion


        #region EdgeIdsAndNUnitTest()

        /// <summary>
        /// Tests EdgeIds in combination with NUnit.
        /// </summary>
        [Test]
        public void EdgeIdsAndNUnitTest()
        {

            var a = new EdgeId(1);
            var b = new EdgeId(2);
            var c = new EdgeId(1);

            Assert.AreEqual(a, a);
            Assert.AreEqual(b, b);
            Assert.AreEqual(c, c);
            
            Assert.AreEqual(a, c);
            Assert.AreNotEqual(a, b);
            Assert.AreNotEqual(b, c);

        }

        #endregion

        #region EdgeIdsInHashSetTest()

        /// <summary>
        /// Test EdgeIds within a HashSet.
        /// </summary>
        [Test]
        public void EdgeIdsInHashSetTest()
        {

            var a = new EdgeId(1);
            var b = new EdgeId(2);
            var c = new EdgeId(1);

            var _HashSet = new HashSet<EdgeId>();
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
