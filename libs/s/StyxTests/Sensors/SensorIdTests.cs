/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Styx <http://www.github.com/Vanaheimr/Styx>
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
using de.ahzf.Vanaheimr.Styx.Sensors;

#endregion

namespace de.ahzf.Vanaheimr.Styx.Sensors.UnitTests
{

    /// <summary>
    /// Unit tests for the SensorId class.
    /// </summary>
    [TestFixture]
    public class SensorIdTests
    {

        #region SensorIdEmptyConstructorTest()

        /// <summary>
        /// A test for an empty SensorId constructor.
        /// </summary>
        [Test]
        public void SensorIdEmptyConstructorTest()
        {
            var _SensorId1 = new SensorId();
            var _SensorId2 = new SensorId();
            Assert.IsTrue(_SensorId1.Length > 0);
            Assert.IsTrue(_SensorId2.Length > 0);
            Assert.AreNotEqual(_SensorId1, _SensorId2);
        }

        #endregion

        #region SensorIdStringConstructorTest()

        /// <summary>
        /// A test for the SensorId string constructor.
        /// </summary>
        [Test]
        public void SensorIdStringConstructorTest()
        {
            var _SensorId = new SensorId("123");
            Assert.AreEqual("123", _SensorId.ToString());
            Assert.AreEqual(3,     _SensorId.Length);
        }

        #endregion

        #region SensorIdInt32ConstructorTest()

        /// <summary>
        /// A test for the SensorId Int32 constructor.
        /// </summary>
        [Test]
        public void SensorIdInt32ConstructorTest()
        {
            var _SensorId = new SensorId(5);
            Assert.AreEqual("5", _SensorId.ToString());
            Assert.AreEqual(1,   _SensorId.Length);
        }

        #endregion

        #region SensorIdUInt32ConstructorTest()

        /// <summary>
        /// A test for the SensorId UInt32 constructor.
        /// </summary>
        [Test]
        public void SensorIdUInt32ConstructorTest()
        {
            var _SensorId = new SensorId(23U);
            Assert.AreEqual("23", _SensorId.ToString());
            Assert.AreEqual(2,    _SensorId.Length);
        }

        #endregion

        #region SensorIdInt64ConstructorTest()

        /// <summary>
        /// A test for the SensorId Int64 constructor.
        /// </summary>
        [Test]
        public void SensorIdInt64ConstructorTest()
        {
            var _SensorId = new SensorId(42L);
            Assert.AreEqual("42", _SensorId.ToString());
            Assert.AreEqual(2,    _SensorId.Length);
        }

        #endregion

        #region SensorIdUInt64ConstructorTest()

        /// <summary>
        /// A test for the SensorId UInt64 constructor.
        /// </summary>
        [Test]
        public void SensorIdUInt64ConstructorTest()
        {
            var _SensorId = new SensorId(123UL);
            Assert.AreEqual("123", _SensorId.ToString());
            Assert.AreEqual(3,     _SensorId.Length);
        }

        #endregion

        #region SensorIdSensorIdConstructorTest()

        /// <summary>
        /// A test for the SensorId SensorId constructor.
        /// </summary>
        [Test]
        public void SensorIdSensorIdConstructorTest()
        {
            var _SensorId1 = SensorId.NewSensorId;
            var _SensorId2 = new SensorId(_SensorId1);
            Assert.AreEqual(_SensorId1.ToString(), _SensorId2.ToString());
            Assert.AreEqual(_SensorId1.Length,     _SensorId2.Length);
            Assert.AreEqual(_SensorId1,            _SensorId2);
        }

        #endregion

        #region SensorIdUriConstructorTest()

        /// <summary>
        /// A test for the SensorId Uri constructor.
        /// </summary>
        [Test]
        public void SensorIdUriConstructorTest()
        {
            var _SensorId = new SensorId(new Uri("http://example.org"));
            Assert.AreEqual("http://example.org/", _SensorId.ToString());
            Assert.AreEqual(19,                    _SensorId.Length);
        }

        #endregion


        #region NewSensorIdMethodTest()

        /// <summary>
        /// A test for the static newSensorId method.
        /// </summary>
        [Test]
        public void NewSensorIdMethodTest()
        {
            var _SensorId1 = SensorId.NewSensorId;
            var _SensorId2 = SensorId.NewSensorId;
            Assert.AreNotEqual(_SensorId1, _SensorId2);
        }

        #endregion


        #region op_Equality_Null_Test1()

        /// <summary>
        /// A test for the equality operator null.
        /// </summary>
        [Test]
        public void op_Equality_Null_Test1()
        {
            var      _SensorId1 = new SensorId();
            SensorId _SensorId2 = null;
            Assert.IsFalse(_SensorId1 == _SensorId2);
        }

        #endregion

        #region op_Equality_Null_Test2()

        /// <summary>
        /// A test for the equality operator null.
        /// </summary>
        [Test]
        public void op_Equality_Null_Test2()
        {
            SensorId _SensorId1 = null;
            var      _SensorId2 = new SensorId();
            Assert.IsFalse(_SensorId1 == _SensorId2);
        }

        #endregion

        #region op_Equality_BothNull_Test()

        /// <summary>
        /// A test for the equality operator both null.
        /// </summary>
        [Test]
        public void op_Equality_BothNull_Test()
        {
            SensorId _SensorId1 = null;
            SensorId _SensorId2 = null;
            Assert.IsTrue(_SensorId1 == _SensorId2);
        }

        #endregion

        #region op_Equality_SameReference_Test()

        /// <summary>
        /// A test for the equality operator same reference.
        /// </summary>
        [Test]
        
        public void op_Equality_SameReference_Test()
        {
            var _SensorId1 = new SensorId();
            #pragma warning disable
            Assert.IsTrue(_SensorId1 == _SensorId1);
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
            var _SensorId1 = new SensorId(1);
            var _SensorId2 = new SensorId(1);
            Assert.IsTrue(_SensorId1 == _SensorId2);
        }

        #endregion

        #region op_Equality_NotEquals_Test()

        /// <summary>
        /// A test for the equality operator not-equals.
        /// </summary>
        [Test]
        public void op_Equality_NotEquals_Test()
        {
            var _SensorId1 = new SensorId(1);
            var _SensorId2 = new SensorId(2);
            Assert.IsFalse(_SensorId1 == _SensorId2);
        }

        #endregion


        #region op_Inequality_Null_Test1()

        /// <summary>
        /// A test for the inequality operator null.
        /// </summary>
        [Test]
        public void op_Inequality_Null_Test1()
        {
            var      _SensorId1 = new SensorId();
            SensorId _SensorId2 = null;
            Assert.IsTrue(_SensorId1 != _SensorId2);
        }

        #endregion

        #region op_Inequality_Null_Test2()

        /// <summary>
        /// A test for the inequality operator null.
        /// </summary>
        [Test]
        public void op_Inequality_Null_Test2()
        {
            SensorId _SensorId1 = null;
            var      _SensorId2 = new SensorId();
            Assert.IsTrue(_SensorId1 != _SensorId2);
        }

        #endregion

        #region op_Inequality_BothNull_Test()

        /// <summary>
        /// A test for the inequality operator both null.
        /// </summary>
        [Test]
        public void op_Inequality_BothNull_Test()
        {
            SensorId _SensorId1 = null;
            SensorId _SensorId2 = null;
            Assert.IsFalse(_SensorId1 != _SensorId2);
        }

        #endregion

        #region op_Inequality_SameReference_Test()

        /// <summary>
        /// A test for the inequality operator same reference.
        /// </summary>
        [Test]
        public void op_Inequality_SameReference_Test()
        {
            var _SensorId1 = new SensorId();
            #pragma warning disable
            Assert.IsFalse(_SensorId1 != _SensorId1);
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
            var _SensorId1 = new SensorId(1);
            var _SensorId2 = new SensorId(1);
            Assert.IsFalse(_SensorId1 != _SensorId2);
        }

        #endregion

        #region op_Inequality_NotEquals1_Test()

        /// <summary>
        /// A test for the inequality operator not-equals.
        /// </summary>
        [Test]
        public void op_Inequality_NotEquals1_Test()
        {
            var _SensorId1 = new SensorId(1);
            var _SensorId2 = new SensorId(2);
            Assert.IsTrue(_SensorId1 != _SensorId2);
        }

        #endregion

        #region op_Inequality_NotEquals2_Test()

        /// <summary>
        /// A test for the inequality operator not-equals.
        /// </summary>
        [Test]
        public void op_Inequality_NotEquals2_Test()
        {
            var _SensorId1 = new SensorId(5);
            var _SensorId2 = new SensorId(23);
            Assert.IsTrue(_SensorId1 != _SensorId2);
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
            var      _SensorId1 = new SensorId();
            SensorId _SensorId2 = null;
            Assert.IsTrue(_SensorId1 < _SensorId2);
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
            SensorId _SensorId1 = null;
            var      _SensorId2 = new SensorId();
            Assert.IsTrue(_SensorId1 < _SensorId2);
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
            SensorId _SensorId1 = null;
            SensorId _SensorId2 = null;
            Assert.IsFalse(_SensorId1 < _SensorId2);
        }

        #endregion

        #region op_Smaller_SameReference_Test()

        /// <summary>
        /// A test for the smaller operator same reference.
        /// </summary>
        [Test]
        public void op_Smaller_SameReference_Test()
        {
            var _SensorId1 = new SensorId();
            #pragma warning disable
            Assert.IsFalse(_SensorId1 < _SensorId1);
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
            var _SensorId1 = new SensorId(1);
            var _SensorId2 = new SensorId(1);
            Assert.IsFalse(_SensorId1 < _SensorId2);
        }

        #endregion

        #region op_Smaller_Smaller1_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Smaller1_Test()
        {
            var _SensorId1 = new SensorId(1);
            var _SensorId2 = new SensorId(2);
            Assert.IsTrue(_SensorId1 < _SensorId2);
        }

        #endregion

        #region op_Smaller_Smaller2_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Smaller2_Test()
        {
            var _SensorId1 = new SensorId(5);
            var _SensorId2 = new SensorId(23);
            Assert.IsTrue(_SensorId1 < _SensorId2);
        }

        #endregion

        #region op_Smaller_Bigger1_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Bigger1_Test()
        {
            var _SensorId1 = new SensorId(2);
            var _SensorId2 = new SensorId(1);
            Assert.IsFalse(_SensorId1 < _SensorId2);
        }

        #endregion

        #region op_Smaller_Bigger2_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Bigger2_Test()
        {
            var _SensorId1 = new SensorId(23);
            var _SensorId2 = new SensorId(5);
            Assert.IsFalse(_SensorId1 < _SensorId2);
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
            var      _SensorId1 = new SensorId();
            SensorId _SensorId2 = null;
            Assert.IsTrue(_SensorId1 <= _SensorId2);
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
            SensorId _SensorId1 = null;
            var      _SensorId2 = new SensorId();
            Assert.IsTrue(_SensorId1 <= _SensorId2);
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
            SensorId _SensorId1 = null;
            SensorId _SensorId2 = null;
            Assert.IsFalse(_SensorId1 <= _SensorId2);
        }

        #endregion

        #region op_SmallerOrEqual_SameReference_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator same reference.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_SameReference_Test()
        {
            var _SensorId1 = new SensorId();
            #pragma warning disable
            Assert.IsTrue(_SensorId1 <= _SensorId1);
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
            var _SensorId1 = new SensorId(1);
            var _SensorId2 = new SensorId(1);
            Assert.IsTrue(_SensorId1 <= _SensorId2);
        }

        #endregion

        #region op_SmallerOrEqual_SmallerThan1_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_SmallerThan1_Test()
        {
            var _SensorId1 = new SensorId(1);
            var _SensorId2 = new SensorId(2);
            Assert.IsTrue(_SensorId1 <= _SensorId2);
        }

        #endregion

        #region op_SmallerOrEqual_SmallerThan2_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_SmallerThan2_Test()
        {
            var _SensorId1 = new SensorId(5);
            var _SensorId2 = new SensorId(23);
            Assert.IsTrue(_SensorId1 <= _SensorId2);
        }

        #endregion

        #region op_SmallerOrEqual_Bigger1_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_Bigger1_Test()
        {
            var _SensorId1 = new SensorId(2);
            var _SensorId2 = new SensorId(1);
            Assert.IsFalse(_SensorId1 <= _SensorId2);
        }

        #endregion

        #region op_SmallerOrEqual_Bigger2_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_Bigger2_Test()
        {
            var _SensorId1 = new SensorId(23);
            var _SensorId2 = new SensorId(5);
            Assert.IsFalse(_SensorId1 <= _SensorId2);
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
            var      _SensorId1 = new SensorId();
            SensorId _SensorId2 = null;
            Assert.IsTrue(_SensorId1 > _SensorId2);
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
            SensorId _SensorId1 = null;
            var      _SensorId2 = new SensorId();
            Assert.IsTrue(_SensorId1 > _SensorId2);
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
            SensorId _SensorId1 = null;
            SensorId _SensorId2 = null;
            Assert.IsFalse(_SensorId1 > _SensorId2);
        }

        #endregion

        #region op_Bigger_SameReference_Test()

        /// <summary>
        /// A test for the bigger operator same reference.
        /// </summary>
        [Test]
        public void op_Bigger_SameReference_Test()
        {
            var _SensorId1 = new SensorId();
            #pragma warning disable
            Assert.IsFalse(_SensorId1 > _SensorId1);
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
            var _SensorId1 = new SensorId(1);
            var _SensorId2 = new SensorId(1);
            Assert.IsFalse(_SensorId1 > _SensorId2);
        }

        #endregion

        #region op_Bigger_Smaller1_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Smaller1_Test()
        {
            var _SensorId1 = new SensorId(1);
            var _SensorId2 = new SensorId(2);
            Assert.IsFalse(_SensorId1 > _SensorId2);
        }

        #endregion

        #region op_Bigger_Smaller2_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Smaller2_Test()
        {
            var _SensorId1 = new SensorId(5);
            var _SensorId2 = new SensorId(23);
            Assert.IsFalse(_SensorId1 > _SensorId2);
        }

        #endregion

        #region op_Bigger_Bigger1_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Bigger1_Test()
        {
            var _SensorId1 = new SensorId(2);
            var _SensorId2 = new SensorId(1);
            Assert.IsTrue(_SensorId1 > _SensorId2);
        }

        #endregion

        #region op_Bigger_Bigger2_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Bigger2_Test()
        {
            var _SensorId1 = new SensorId(23);
            var _SensorId2 = new SensorId(5);
            Assert.IsTrue(_SensorId1 > _SensorId2);
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
            var      _SensorId1 = new SensorId();
            SensorId _SensorId2 = null;
            Assert.IsTrue(_SensorId1 >= _SensorId2);
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
            SensorId _SensorId1 = null;
            var      _SensorId2 = new SensorId();
            Assert.IsTrue(_SensorId1 >= _SensorId2);
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
            SensorId _SensorId1 = null;
            SensorId _SensorId2 = null;
            Assert.IsFalse(_SensorId1 >= _SensorId2);
        }

        #endregion

        #region op_BiggerOrEqual_SameReference_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator same reference.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_SameReference_Test()
        {
            var _SensorId1 = new SensorId();
            #pragma warning disable
            Assert.IsTrue(_SensorId1 >= _SensorId1);
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
            var _SensorId1 = new SensorId(1);
            var _SensorId2 = new SensorId(1);
            Assert.IsTrue(_SensorId1 >= _SensorId2);
        }

        #endregion

        #region op_BiggerOrEqual_SmallerThan1_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_SmallerThan1_Test()
        {
            var _SensorId1 = new SensorId(1);
            var _SensorId2 = new SensorId(2);
            Assert.IsFalse(_SensorId1 >= _SensorId2);
        }

        #endregion

        #region op_BiggerOrEqual_SmallerThan2_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_SmallerThan2_Test()
        {
            var _SensorId1 = new SensorId(5);
            var _SensorId2 = new SensorId(23);
            Assert.IsFalse(_SensorId1 >= _SensorId2);
        }

        #endregion

        #region op_BiggerOrEqual_Bigger1_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_Bigger1_Test()
        {
            var _SensorId1 = new SensorId(2);
            var _SensorId2 = new SensorId(1);
            Assert.IsTrue(_SensorId1 >= _SensorId2);
        }

        #endregion

        #region op_BiggerOrEqual_Bigger2_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_Bigger2_Test()
        {
            var _SensorId1 = new SensorId(23);
            var _SensorId2 = new SensorId(5);
            Assert.IsTrue(_SensorId1 >= _SensorId2);
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
            var    _SensorId = SensorId.NewSensorId;
            Object _Object   = null;
            _SensorId.CompareTo(_Object);
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
            var      _SensorId = SensorId.NewSensorId;
            SensorId _Object   = null;
            _SensorId.CompareTo(_Object);
        }

        #endregion

        #region CompareToNonSensorIdTest()

        /// <summary>
        /// A test for CompareTo a non-SensorId.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CompareToNonSensorIdTest()
        {
            var _SensorId = SensorId.NewSensorId;
            var _Object   = "123";
            _SensorId.CompareTo(_Object);
        }

        #endregion

        #region CompareToSmallerTest1()

        /// <summary>
        /// A test for CompareTo smaller.
        /// </summary>
        [Test]
        public void CompareToSmallerTest1()
        {
            var _SensorId1 = new SensorId(1);
            var _SensorId2 = new SensorId(2);
            Assert.IsTrue(_SensorId1.CompareTo(_SensorId2) < 0);
        }

        #endregion

        #region CompareToSmallerTest2()

        /// <summary>
        /// A test for CompareTo smaller.
        /// </summary>
        [Test]
        public void CompareToSmallerTest2()
        {
            var _SensorId1 = new SensorId(5);
            var _SensorId2 = new SensorId(23);
            Assert.IsTrue(_SensorId1.CompareTo(_SensorId2) < 0);
        }

        #endregion

        #region CompareToEqualsTest()

        /// <summary>
        /// A test for CompareTo equals.
        /// </summary>
        [Test]
        public void CompareToEqualsTest()
        {
            var _SensorId1 = new SensorId(1);
            var _SensorId2 = new SensorId(1);
            Assert.IsTrue(_SensorId1.CompareTo(_SensorId2) == 0);
        }

        #endregion

        #region CompareToBiggerTest()

        /// <summary>
        /// A test for CompareTo bigger.
        /// </summary>
        [Test]
        public void CompareToBiggerTest()
        {
            var _SensorId1 = new SensorId(2);
            var _SensorId2 = new SensorId(1);
            Assert.IsTrue(_SensorId1.CompareTo(_SensorId2) > 0);
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
            var    _SensorId = SensorId.NewSensorId;
            Object _Object   = null;
            _SensorId.Equals(_Object);
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
            var      _SensorId = SensorId.NewSensorId;
            SensorId _Object   = null;
            _SensorId.Equals(_Object);
        }

        #endregion

        #region EqualsNonSensorIdTest()

        /// <summary>
        /// A test for equals a non-SensorId.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EqualsNonSensorIdTest()
        {
            var _SensorId = SensorId.NewSensorId;
            var _Object   = "123";
            _SensorId.Equals(_Object);
        }

        #endregion

        #region EqualsEqualsTest()

        /// <summary>
        /// A test for equals.
        /// </summary>
        [Test]
        public void EqualsEqualsTest()
        {
            var _SensorId1 = new SensorId(1);
            var _SensorId2 = new SensorId(1);
            Assert.IsTrue(_SensorId1.Equals(_SensorId2));
        }

        #endregion

        #region EqualsNotEqualsTest()

        /// <summary>
        /// A test for not-equals.
        /// </summary>
        [Test]
        public void EqualsNotEqualsTest()
        {
            var _SensorId1 = new SensorId(1);
            var _SensorId2 = new SensorId(2);
            Assert.IsFalse(_SensorId1.Equals(_SensorId2));
        }

        #endregion


        #region GetHashCodeEqualTest()

        /// <summary>
        /// A test for GetHashCode
        /// </summary>
        [Test]
        public void GetHashCodeEqualTest()
        {
            var _SensorHashCode1 = new SensorId(5).GetHashCode();
            var _SensorHashCode2 = new SensorId(5).GetHashCode();
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
            var _SensorHashCode1 = new SensorId(1).GetHashCode();
            var _SensorHashCode2 = new SensorId(2).GetHashCode();
            Assert.AreNotEqual(_SensorHashCode1, _SensorHashCode2);
        }

        #endregion


        #region SensorIdsAndNUnitTest()

        /// <summary>
        /// Tests SensorIds in combination with NUnit.
        /// </summary>
        [Test]
        public void SensorIdsAndNUnitTest()
        {

            var a = new SensorId(1);
            var b = new SensorId(2);
            var c = new SensorId(1);

            Assert.AreEqual(a, a);
            Assert.AreEqual(b, b);
            Assert.AreEqual(c, c);

            Assert.AreEqual(a, c);
            Assert.AreNotEqual(a, b);
            Assert.AreNotEqual(b, c);

        }

        #endregion

        #region SensorIdsInHashSetTest()

        /// <summary>
        /// Test SensorIds within a HashSet.
        /// </summary>
        [Test]
        public void SensorIdsInHashSetTest()
        {

            var a = new SensorId(1);
            var b = new SensorId(2);
            var c = new SensorId(1);

            var _HashSet = new HashSet<SensorId>();
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
