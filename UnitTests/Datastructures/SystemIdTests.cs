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
    /// Unit tests for the SystemId class.
    /// </summary>
    [TestFixture]
    public class SystemIdTests
    {

        #region SystemIdEmptyConstructorTest()

        /// <summary>
        /// A test for an empty SystemId constructor.
        /// </summary>
        [Test]
        public void SystemIdEmptyConstructorTest()
        {
            var _SystemId1 = new SystemId();
            var _SystemId2 = new SystemId();
            Assert.IsTrue(_SystemId1.Length > 0);
            Assert.IsTrue(_SystemId2.Length > 0);
            Assert.AreNotEqual(_SystemId1, _SystemId2);
        }

        #endregion

        #region SystemIdStringConstructorTest()

        /// <summary>
        /// A test for the SystemId string constructor.
        /// </summary>
        [Test]
        public void SystemIdStringConstructorTest()
        {
            var _SystemId = new SystemId("123");
            Assert.AreEqual("123", _SystemId.ToString());
            Assert.AreEqual(3,     _SystemId.Length);
        }

        #endregion

        #region SystemIdInt32ConstructorTest()

        /// <summary>
        /// A test for the SystemId Int32 constructor.
        /// </summary>
        [Test]
        public void SystemIdInt32ConstructorTest()
        {
            var _SystemId = new SystemId(5);
            Assert.AreEqual("5", _SystemId.ToString());
            Assert.AreEqual(1,   _SystemId.Length);
        }

        #endregion

        #region SystemIdUInt32ConstructorTest()

        /// <summary>
        /// A test for the SystemId UInt32 constructor.
        /// </summary>
        [Test]
        public void SystemIdUInt32ConstructorTest()
        {
            var _SystemId = new SystemId(23U);
            Assert.AreEqual("23", _SystemId.ToString());
            Assert.AreEqual(2,    _SystemId.Length);
        }

        #endregion

        #region SystemIdInt64ConstructorTest()

        /// <summary>
        /// A test for the SystemId Int64 constructor.
        /// </summary>
        [Test]
        public void SystemIdInt64ConstructorTest()
        {
            var _SystemId = new SystemId(42L);
            Assert.AreEqual("42", _SystemId.ToString());
            Assert.AreEqual(2,    _SystemId.Length);
        }

        #endregion

        #region SystemIdUInt64ConstructorTest()

        /// <summary>
        /// A test for the SystemId UInt64 constructor.
        /// </summary>
        [Test]
        public void SystemIdUInt64ConstructorTest()
        {
            var _SystemId = new SystemId(123UL);
            Assert.AreEqual("123", _SystemId.ToString());
            Assert.AreEqual(3,     _SystemId.Length);
        }

        #endregion

        #region SystemIdSystemIdConstructorTest()

        /// <summary>
        /// A test for the SystemId SystemId constructor.
        /// </summary>
        [Test]
        public void SystemIdSystemIdConstructorTest()
        {
            var _SystemId1 = SystemId.NewSystemId;
            var _SystemId2 = new SystemId(_SystemId1);
            Assert.AreEqual(_SystemId1.ToString(), _SystemId2.ToString());
            Assert.AreEqual(_SystemId1.Length,     _SystemId2.Length);
            Assert.AreEqual(_SystemId1,            _SystemId2);
        }

        #endregion

        #region SystemIdUriConstructorTest()

        /// <summary>
        /// A test for the SystemId Uri constructor.
        /// </summary>
        [Test]
        public void SystemIdUriConstructorTest()
        {
            var _SystemId = new SystemId(new Uri("http://example.org"));
            Assert.AreEqual("http://example.org/", _SystemId.ToString());
            Assert.AreEqual(19,                    _SystemId.Length);
        }

        #endregion


        #region NewSystemIdMethodTest()

        /// <summary>
        /// A test for the static newSystemId method.
        /// </summary>
        [Test]
        public void NewSystemIdMethodTest()
        {
            var _SystemId1 = SystemId.NewSystemId;
            var _SystemId2 = SystemId.NewSystemId;
            Assert.AreNotEqual(_SystemId1, _SystemId2);
        }

        #endregion


        #region op_Equality_Null_Test1()

        /// <summary>
        /// A test for the equality operator null.
        /// </summary>
        [Test]
        public void op_Equality_Null_Test1()
        {
            var      _SystemId1 = new SystemId();
            SystemId _SystemId2 = null;
            Assert.IsFalse(_SystemId1 == _SystemId2);
        }

        #endregion

        #region op_Equality_Null_Test2()

        /// <summary>
        /// A test for the equality operator null.
        /// </summary>
        [Test]
        public void op_Equality_Null_Test2()
        {
            SystemId _SystemId1 = null;
            var      _SystemId2 = new SystemId();
            Assert.IsFalse(_SystemId1 == _SystemId2);
        }

        #endregion

        #region op_Equality_BothNull_Test()

        /// <summary>
        /// A test for the equality operator both null.
        /// </summary>
        [Test]
        public void op_Equality_BothNull_Test()
        {
            SystemId _SystemId1 = null;
            SystemId _SystemId2 = null;
            Assert.IsTrue(_SystemId1 == _SystemId2);
        }

        #endregion

        #region op_Equality_SameReference_Test()

        /// <summary>
        /// A test for the equality operator same reference.
        /// </summary>
        [Test]
        
        public void op_Equality_SameReference_Test()
        {
            var _SystemId1 = new SystemId();
            #pragma warning disable
            Assert.IsTrue(_SystemId1 == _SystemId1);
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
            var _SystemId1 = new SystemId(1);
            var _SystemId2 = new SystemId(1);
            Assert.IsTrue(_SystemId1 == _SystemId2);
        }

        #endregion

        #region op_Equality_NotEquals_Test()

        /// <summary>
        /// A test for the equality operator not-equals.
        /// </summary>
        [Test]
        public void op_Equality_NotEquals_Test()
        {
            var _SystemId1 = new SystemId(1);
            var _SystemId2 = new SystemId(2);
            Assert.IsFalse(_SystemId1 == _SystemId2);
        }

        #endregion


        #region op_Inequality_Null_Test1()

        /// <summary>
        /// A test for the inequality operator null.
        /// </summary>
        [Test]
        public void op_Inequality_Null_Test1()
        {
            var      _SystemId1 = new SystemId();
            SystemId _SystemId2 = null;
            Assert.IsTrue(_SystemId1 != _SystemId2);
        }

        #endregion

        #region op_Inequality_Null_Test2()

        /// <summary>
        /// A test for the inequality operator null.
        /// </summary>
        [Test]
        public void op_Inequality_Null_Test2()
        {
            SystemId _SystemId1 = null;
            var      _SystemId2 = new SystemId();
            Assert.IsTrue(_SystemId1 != _SystemId2);
        }

        #endregion

        #region op_Inequality_BothNull_Test()

        /// <summary>
        /// A test for the inequality operator both null.
        /// </summary>
        [Test]
        public void op_Inequality_BothNull_Test()
        {
            SystemId _SystemId1 = null;
            SystemId _SystemId2 = null;
            Assert.IsFalse(_SystemId1 != _SystemId2);
        }

        #endregion

        #region op_Inequality_SameReference_Test()

        /// <summary>
        /// A test for the inequality operator same reference.
        /// </summary>
        [Test]
        public void op_Inequality_SameReference_Test()
        {
            var _SystemId1 = new SystemId();
            #pragma warning disable
            Assert.IsFalse(_SystemId1 != _SystemId1);
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
            var _SystemId1 = new SystemId(1);
            var _SystemId2 = new SystemId(1);
            Assert.IsFalse(_SystemId1 != _SystemId2);
        }

        #endregion

        #region op_Inequality_NotEquals1_Test()

        /// <summary>
        /// A test for the inequality operator not-equals.
        /// </summary>
        [Test]
        public void op_Inequality_NotEquals1_Test()
        {
            var _SystemId1 = new SystemId(1);
            var _SystemId2 = new SystemId(2);
            Assert.IsTrue(_SystemId1 != _SystemId2);
        }

        #endregion

        #region op_Inequality_NotEquals2_Test()

        /// <summary>
        /// A test for the inequality operator not-equals.
        /// </summary>
        [Test]
        public void op_Inequality_NotEquals2_Test()
        {
            var _SystemId1 = new SystemId(5);
            var _SystemId2 = new SystemId(23);
            Assert.IsTrue(_SystemId1 != _SystemId2);
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
            var      _SystemId1 = new SystemId();
            SystemId _SystemId2 = null;
            Assert.IsTrue(_SystemId1 < _SystemId2);
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
            SystemId _SystemId1 = null;
            var      _SystemId2 = new SystemId();
            Assert.IsTrue(_SystemId1 < _SystemId2);
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
            SystemId _SystemId1 = null;
            SystemId _SystemId2 = null;
            Assert.IsFalse(_SystemId1 < _SystemId2);
        }

        #endregion

        #region op_Smaller_SameReference_Test()

        /// <summary>
        /// A test for the smaller operator same reference.
        /// </summary>
        [Test]
        public void op_Smaller_SameReference_Test()
        {
            var _SystemId1 = new SystemId();
            #pragma warning disable
            Assert.IsFalse(_SystemId1 < _SystemId1);
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
            var _SystemId1 = new SystemId(1);
            var _SystemId2 = new SystemId(1);
            Assert.IsFalse(_SystemId1 < _SystemId2);
        }

        #endregion

        #region op_Smaller_Smaller1_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Smaller1_Test()
        {
            var _SystemId1 = new SystemId(1);
            var _SystemId2 = new SystemId(2);
            Assert.IsTrue(_SystemId1 < _SystemId2);
        }

        #endregion

        #region op_Smaller_Smaller2_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Smaller2_Test()
        {
            var _SystemId1 = new SystemId(5);
            var _SystemId2 = new SystemId(23);
            Assert.IsTrue(_SystemId1 < _SystemId2);
        }

        #endregion

        #region op_Smaller_Bigger1_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Bigger1_Test()
        {
            var _SystemId1 = new SystemId(2);
            var _SystemId2 = new SystemId(1);
            Assert.IsFalse(_SystemId1 < _SystemId2);
        }

        #endregion

        #region op_Smaller_Bigger2_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Bigger2_Test()
        {
            var _SystemId1 = new SystemId(23);
            var _SystemId2 = new SystemId(5);
            Assert.IsFalse(_SystemId1 < _SystemId2);
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
            var      _SystemId1 = new SystemId();
            SystemId _SystemId2 = null;
            Assert.IsTrue(_SystemId1 <= _SystemId2);
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
            SystemId _SystemId1 = null;
            var      _SystemId2 = new SystemId();
            Assert.IsTrue(_SystemId1 <= _SystemId2);
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
            SystemId _SystemId1 = null;
            SystemId _SystemId2 = null;
            Assert.IsFalse(_SystemId1 <= _SystemId2);
        }

        #endregion

        #region op_SmallerOrEqual_SameReference_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator same reference.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_SameReference_Test()
        {
            var _SystemId1 = new SystemId();
            #pragma warning disable
            Assert.IsTrue(_SystemId1 <= _SystemId1);
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
            var _SystemId1 = new SystemId(1);
            var _SystemId2 = new SystemId(1);
            Assert.IsTrue(_SystemId1 <= _SystemId2);
        }

        #endregion

        #region op_SmallerOrEqual_SmallerThan1_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_SmallerThan1_Test()
        {
            var _SystemId1 = new SystemId(1);
            var _SystemId2 = new SystemId(2);
            Assert.IsTrue(_SystemId1 <= _SystemId2);
        }

        #endregion

        #region op_SmallerOrEqual_SmallerThan2_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_SmallerThan2_Test()
        {
            var _SystemId1 = new SystemId(5);
            var _SystemId2 = new SystemId(23);
            Assert.IsTrue(_SystemId1 <= _SystemId2);
        }

        #endregion

        #region op_SmallerOrEqual_Bigger1_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_Bigger1_Test()
        {
            var _SystemId1 = new SystemId(2);
            var _SystemId2 = new SystemId(1);
            Assert.IsFalse(_SystemId1 <= _SystemId2);
        }

        #endregion

        #region op_SmallerOrEqual_Bigger2_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_Bigger2_Test()
        {
            var _SystemId1 = new SystemId(23);
            var _SystemId2 = new SystemId(5);
            Assert.IsFalse(_SystemId1 <= _SystemId2);
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
            var      _SystemId1 = new SystemId();
            SystemId _SystemId2 = null;
            Assert.IsTrue(_SystemId1 > _SystemId2);
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
            SystemId _SystemId1 = null;
            var      _SystemId2 = new SystemId();
            Assert.IsTrue(_SystemId1 > _SystemId2);
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
            SystemId _SystemId1 = null;
            SystemId _SystemId2 = null;
            Assert.IsFalse(_SystemId1 > _SystemId2);
        }

        #endregion

        #region op_Bigger_SameReference_Test()

        /// <summary>
        /// A test for the bigger operator same reference.
        /// </summary>
        [Test]
        public void op_Bigger_SameReference_Test()
        {
            var _SystemId1 = new SystemId();
            #pragma warning disable
            Assert.IsFalse(_SystemId1 > _SystemId1);
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
            var _SystemId1 = new SystemId(1);
            var _SystemId2 = new SystemId(1);
            Assert.IsFalse(_SystemId1 > _SystemId2);
        }

        #endregion

        #region op_Bigger_Smaller1_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Smaller1_Test()
        {
            var _SystemId1 = new SystemId(1);
            var _SystemId2 = new SystemId(2);
            Assert.IsFalse(_SystemId1 > _SystemId2);
        }

        #endregion

        #region op_Bigger_Smaller2_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Smaller2_Test()
        {
            var _SystemId1 = new SystemId(5);
            var _SystemId2 = new SystemId(23);
            Assert.IsFalse(_SystemId1 > _SystemId2);
        }

        #endregion

        #region op_Bigger_Bigger1_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Bigger1_Test()
        {
            var _SystemId1 = new SystemId(2);
            var _SystemId2 = new SystemId(1);
            Assert.IsTrue(_SystemId1 > _SystemId2);
        }

        #endregion

        #region op_Bigger_Bigger2_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Bigger2_Test()
        {
            var _SystemId1 = new SystemId(23);
            var _SystemId2 = new SystemId(5);
            Assert.IsTrue(_SystemId1 > _SystemId2);
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
            var      _SystemId1 = new SystemId();
            SystemId _SystemId2 = null;
            Assert.IsTrue(_SystemId1 >= _SystemId2);
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
            SystemId _SystemId1 = null;
            var      _SystemId2 = new SystemId();
            Assert.IsTrue(_SystemId1 >= _SystemId2);
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
            SystemId _SystemId1 = null;
            SystemId _SystemId2 = null;
            Assert.IsFalse(_SystemId1 >= _SystemId2);
        }

        #endregion

        #region op_BiggerOrEqual_SameReference_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator same reference.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_SameReference_Test()
        {
            var _SystemId1 = new SystemId();
            #pragma warning disable
            Assert.IsTrue(_SystemId1 >= _SystemId1);
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
            var _SystemId1 = new SystemId(1);
            var _SystemId2 = new SystemId(1);
            Assert.IsTrue(_SystemId1 >= _SystemId2);
        }

        #endregion

        #region op_BiggerOrEqual_SmallerThan1_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_SmallerThan1_Test()
        {
            var _SystemId1 = new SystemId(1);
            var _SystemId2 = new SystemId(2);
            Assert.IsFalse(_SystemId1 >= _SystemId2);
        }

        #endregion

        #region op_BiggerOrEqual_SmallerThan2_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_SmallerThan2_Test()
        {
            var _SystemId1 = new SystemId(5);
            var _SystemId2 = new SystemId(23);
            Assert.IsFalse(_SystemId1 >= _SystemId2);
        }

        #endregion

        #region op_BiggerOrEqual_Bigger1_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_Bigger1_Test()
        {
            var _SystemId1 = new SystemId(2);
            var _SystemId2 = new SystemId(1);
            Assert.IsTrue(_SystemId1 >= _SystemId2);
        }

        #endregion

        #region op_BiggerOrEqual_Bigger2_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_Bigger2_Test()
        {
            var _SystemId1 = new SystemId(23);
            var _SystemId2 = new SystemId(5);
            Assert.IsTrue(_SystemId1 >= _SystemId2);
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
            var    _SystemId = SystemId.NewSystemId;
            Object _Object   = null;
            _SystemId.CompareTo(_Object);
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
            var      _SystemId = SystemId.NewSystemId;
            SystemId _Object   = null;
            _SystemId.CompareTo(_Object);
        }

        #endregion

        #region CompareToNonSystemIdTest()

        /// <summary>
        /// A test for CompareTo a non-SystemId.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CompareToNonSystemIdTest()
        {
            var _SystemId = SystemId.NewSystemId;
            var _Object   = "123";
            _SystemId.CompareTo(_Object);
        }

        #endregion

        #region CompareToSmallerTest1()

        /// <summary>
        /// A test for CompareTo smaller.
        /// </summary>
        [Test]
        public void CompareToSmallerTest1()
        {
            var _SystemId1 = new SystemId(1);
            var _SystemId2 = new SystemId(2);
            Assert.IsTrue(_SystemId1.CompareTo(_SystemId2) < 0);
        }

        #endregion

        #region CompareToSmallerTest2()

        /// <summary>
        /// A test for CompareTo smaller.
        /// </summary>
        [Test]
        public void CompareToSmallerTest2()
        {
            var _SystemId1 = new SystemId(5);
            var _SystemId2 = new SystemId(23);
            Assert.IsTrue(_SystemId1.CompareTo(_SystemId2) < 0);
        }

        #endregion

        #region CompareToEqualsTest()

        /// <summary>
        /// A test for CompareTo equals.
        /// </summary>
        [Test]
        public void CompareToEqualsTest()
        {
            var _SystemId1 = new SystemId(1);
            var _SystemId2 = new SystemId(1);
            Assert.IsTrue(_SystemId1.CompareTo(_SystemId2) == 0);
        }

        #endregion

        #region CompareToBiggerTest()

        /// <summary>
        /// A test for CompareTo bigger.
        /// </summary>
        [Test]
        public void CompareToBiggerTest()
        {
            var _SystemId1 = new SystemId(2);
            var _SystemId2 = new SystemId(1);
            Assert.IsTrue(_SystemId1.CompareTo(_SystemId2) > 0);
        }

        #endregion


        #region EqualsNullTest1()

        /// <summary>
        /// A test for equals null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EqualsNullTest1()
        {
            var    _SystemId = SystemId.NewSystemId;
            Object _Object   = null;
            _SystemId.Equals(_Object);
        }

        #endregion

        #region EqualsNullTest2()

        /// <summary>
        /// A test for equals null.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EqualsNullTest2()
        {
            var      _SystemId = SystemId.NewSystemId;
            SystemId _Object   = null;
            _SystemId.Equals(_Object);
        }

        #endregion

        #region EqualsNonSystemIdTest()

        /// <summary>
        /// A test for equals a non-SystemId.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EqualsNonSystemIdTest()
        {
            var _SystemId = SystemId.NewSystemId;
            var _Object   = "123";
            _SystemId.Equals(_Object);
        }

        #endregion

        #region EqualsEqualsTest()

        /// <summary>
        /// A test for equals.
        /// </summary>
        [Test]
        public void EqualsEqualsTest()
        {
            var _SystemId1 = new SystemId(1);
            var _SystemId2 = new SystemId(1);
            Assert.IsTrue(_SystemId1.Equals(_SystemId2));
        }

        #endregion

        #region EqualsNotEqualsTest()

        /// <summary>
        /// A test for not-equals.
        /// </summary>
        [Test]
        public void EqualsNotEqualsTest()
        {
            var _SystemId1 = new SystemId(1);
            var _SystemId2 = new SystemId(2);
            Assert.IsFalse(_SystemId1.Equals(_SystemId2));
        }

        #endregion


        #region GetHashCodeEqualTest()

        /// <summary>
        /// A test for GetHashCode
        /// </summary>
        [Test]
        public void GetHashCodeEqualTest()
        {
            var _SensorHashCode1 = new SystemId(5).GetHashCode();
            var _SensorHashCode2 = new SystemId(5).GetHashCode();
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
            var _SensorHashCode1 = new SystemId(1).GetHashCode();
            var _SensorHashCode2 = new SystemId(2).GetHashCode();
            Assert.AreNotEqual(_SensorHashCode1, _SensorHashCode2);
        }

        #endregion


        #region SystemIdsAndNUnitTest()

        /// <summary>
        /// Tests SystemIds in combination with NUnit.
        /// </summary>
        [Test]
        public void SystemIdsAndNUnitTest()
        {

            var a = new SystemId(1);
            var b = new SystemId(2);
            var c = new SystemId(1);

            Assert.AreEqual(a, a);
            Assert.AreEqual(b, b);
            Assert.AreEqual(c, c);

            Assert.AreEqual(a, c);
            Assert.AreNotEqual(a, b);
            Assert.AreNotEqual(b, c);

        }

        #endregion

        #region SystemIdsInHashSetTest()

        /// <summary>
        /// Test SystemIds within a HashSet.
        /// </summary>
        [Test]
        public void SystemIdsInHashSetTest()
        {

            var a = new SystemId(1);
            var b = new SystemId(2);
            var c = new SystemId(1);

            var _HashSet = new HashSet<SystemId>();
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
