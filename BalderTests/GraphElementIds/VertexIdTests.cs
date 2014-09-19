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
    /// Unit tests for the VertexId class.
    /// </summary>
    [TestFixture]
    public class VertexIdTests
    {

        #region VertexIdEmptyConstructorTest()

        /// <summary>
        /// A test for an empty VertexId constructor.
        /// </summary>
        [Test]
        public void VertexIdEmptyConstructorTest()
        {
            var _VertexId1 = new VertexId();
            var _VertexId2 = new VertexId();
            Assert.IsTrue(_VertexId1.Length > 0);
            Assert.IsTrue(_VertexId2.Length > 0);
            Assert.AreNotEqual(_VertexId1, _VertexId2);
        }

        #endregion

        #region VertexIdStringConstructorTest()

        /// <summary>
        /// A test for the VertexId string constructor.
        /// </summary>
        [Test]
        public void VertexIdStringConstructorTest()
        {
            var _VertexId = new VertexId("123");
            Assert.AreEqual("123", _VertexId.ToString());
            Assert.AreEqual(3,     _VertexId.Length);
        }

        #endregion

        #region VertexIdInt32ConstructorTest()

        /// <summary>
        /// A test for the VertexId Int32 constructor.
        /// </summary>
        [Test]
        public void VertexIdInt32ConstructorTest()
        {
            var _VertexId = new VertexId(5);
            Assert.AreEqual("5", _VertexId.ToString());
            Assert.AreEqual(1,   _VertexId.Length);
        }

        #endregion

        #region VertexIdUInt32ConstructorTest()

        /// <summary>
        /// A test for the VertexId UInt32 constructor.
        /// </summary>
        [Test]
        public void VertexIdUInt32ConstructorTest()
        {
            var _VertexId = new VertexId(23U);
            Assert.AreEqual("23", _VertexId.ToString());
            Assert.AreEqual(2,    _VertexId.Length);
        }

        #endregion

        #region VertexIdInt64ConstructorTest()

        /// <summary>
        /// A test for the VertexId Int64 constructor.
        /// </summary>
        [Test]
        public void VertexIdInt64ConstructorTest()
        {
            var _VertexId = new VertexId(42L);
            Assert.AreEqual("42", _VertexId.ToString());
            Assert.AreEqual(2,    _VertexId.Length);
        }

        #endregion

        #region VertexIdUInt64ConstructorTest()

        /// <summary>
        /// A test for the VertexId UInt64 constructor.
        /// </summary>
        [Test]
        public void VertexIdUInt64ConstructorTest()
        {
            var _VertexId = new VertexId(123UL);
            Assert.AreEqual("123", _VertexId.ToString());
            Assert.AreEqual(3,     _VertexId.Length);
        }

        #endregion

        #region VertexIdVertexIdConstructorTest()

        /// <summary>
        /// A test for the VertexId VertexId constructor.
        /// </summary>
        [Test]
        public void VertexIdVertexIdConstructorTest()
        {
            var _VertexId1 = VertexId.NewVertexId;
            var _VertexId2 = new VertexId(_VertexId1);
            Assert.AreEqual(_VertexId1.ToString(), _VertexId2.ToString());
            Assert.AreEqual(_VertexId1.Length,     _VertexId2.Length);
            Assert.AreEqual(_VertexId1,            _VertexId2);
        }

        #endregion

        #region VertexIdUriConstructorTest()

        /// <summary>
        /// A test for the VertexId Uri constructor.
        /// </summary>
        [Test]
        public void VertexIdUriConstructorTest()
        {
            var _VertexId = new VertexId(new Uri("http://example.org"));
            Assert.AreEqual("http://example.org/", _VertexId.ToString());
            Assert.AreEqual(19,                    _VertexId.Length);
        }

        #endregion


        #region NewVertexIdMethodTest()

        /// <summary>
        /// A test for the static newVertexId method.
        /// </summary>
        [Test]
        public void NewVertexIdMethodTest()
        {
            var _VertexId1 = VertexId.NewVertexId;
            var _VertexId2 = VertexId.NewVertexId;
            Assert.AreNotEqual(_VertexId1, _VertexId2);
        }

        #endregion


        #region op_Equality_Null_Test1()

        /// <summary>
        /// A test for the equality operator null.
        /// </summary>
        [Test]
        public void op_Equality_Null_Test1()
        {
            var      _VertexId1 = new VertexId();
            VertexId _VertexId2 = null;
            Assert.IsFalse(_VertexId1 == _VertexId2);
        }

        #endregion

        #region op_Equality_Null_Test2()

        /// <summary>
        /// A test for the equality operator null.
        /// </summary>
        [Test]
        public void op_Equality_Null_Test2()
        {
            VertexId _VertexId1 = null;
            var      _VertexId2 = new VertexId();
            Assert.IsFalse(_VertexId1 == _VertexId2);
        }

        #endregion

        #region op_Equality_BothNull_Test()

        /// <summary>
        /// A test for the equality operator both null.
        /// </summary>
        [Test]
        public void op_Equality_BothNull_Test()
        {
            VertexId _VertexId1 = null;
            VertexId _VertexId2 = null;
            Assert.IsTrue(_VertexId1 == _VertexId2);
        }

        #endregion

        #region op_Equality_SameReference_Test()

        /// <summary>
        /// A test for the equality operator same reference.
        /// </summary>
        [Test]
        
        public void op_Equality_SameReference_Test()
        {
            var _VertexId1 = new VertexId();
            #pragma warning disable
            Assert.IsTrue(_VertexId1 == _VertexId1);
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
            var _VertexId1 = new VertexId(1);
            var _VertexId2 = new VertexId(1);
            Assert.IsTrue(_VertexId1 == _VertexId2);
        }

        #endregion

        #region op_Equality_NotEquals_Test()

        /// <summary>
        /// A test for the equality operator not-equals.
        /// </summary>
        [Test]
        public void op_Equality_NotEquals_Test()
        {
            var _VertexId1 = new VertexId(1);
            var _VertexId2 = new VertexId(2);
            Assert.IsFalse(_VertexId1 == _VertexId2);
        }

        #endregion


        #region op_Inequality_Null_Test1()

        /// <summary>
        /// A test for the inequality operator null.
        /// </summary>
        [Test]
        public void op_Inequality_Null_Test1()
        {
            var      _VertexId1 = new VertexId();
            VertexId _VertexId2 = null;
            Assert.IsTrue(_VertexId1 != _VertexId2);
        }

        #endregion

        #region op_Inequality_Null_Test2()

        /// <summary>
        /// A test for the inequality operator null.
        /// </summary>
        [Test]
        public void op_Inequality_Null_Test2()
        {
            VertexId _VertexId1 = null;
            var      _VertexId2 = new VertexId();
            Assert.IsTrue(_VertexId1 != _VertexId2);
        }

        #endregion

        #region op_Inequality_BothNull_Test()

        /// <summary>
        /// A test for the inequality operator both null.
        /// </summary>
        [Test]
        public void op_Inequality_BothNull_Test()
        {
            VertexId _VertexId1 = null;
            VertexId _VertexId2 = null;
            Assert.IsFalse(_VertexId1 != _VertexId2);
        }

        #endregion

        #region op_Inequality_SameReference_Test()

        /// <summary>
        /// A test for the inequality operator same reference.
        /// </summary>
        [Test]
        public void op_Inequality_SameReference_Test()
        {
            var _VertexId1 = new VertexId();
            #pragma warning disable
            Assert.IsFalse(_VertexId1 != _VertexId1);
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
            var _VertexId1 = new VertexId(1);
            var _VertexId2 = new VertexId(1);
            Assert.IsFalse(_VertexId1 != _VertexId2);
        }

        #endregion

        #region op_Inequality_NotEquals1_Test()

        /// <summary>
        /// A test for the inequality operator not-equals.
        /// </summary>
        [Test]
        public void op_Inequality_NotEquals1_Test()
        {
            var _VertexId1 = new VertexId(1);
            var _VertexId2 = new VertexId(2);
            Assert.IsTrue(_VertexId1 != _VertexId2);
        }

        #endregion

        #region op_Inequality_NotEquals2_Test()

        /// <summary>
        /// A test for the inequality operator not-equals.
        /// </summary>
        [Test]
        public void op_Inequality_NotEquals2_Test()
        {
            var _VertexId1 = new VertexId(5);
            var _VertexId2 = new VertexId(23);
            Assert.IsTrue(_VertexId1 != _VertexId2);
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
            var      _VertexId1 = new VertexId();
            VertexId _VertexId2 = null;
            Assert.IsTrue(_VertexId1 < _VertexId2);
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
            VertexId _VertexId1 = null;
            var      _VertexId2 = new VertexId();
            Assert.IsTrue(_VertexId1 < _VertexId2);
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
            VertexId _VertexId1 = null;
            VertexId _VertexId2 = null;
            Assert.IsFalse(_VertexId1 < _VertexId2);
        }

        #endregion

        #region op_Smaller_SameReference_Test()

        /// <summary>
        /// A test for the smaller operator same reference.
        /// </summary>
        [Test]
        public void op_Smaller_SameReference_Test()
        {
            var _VertexId1 = new VertexId();
            #pragma warning disable
            Assert.IsFalse(_VertexId1 < _VertexId1);
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
            var _VertexId1 = new VertexId(1);
            var _VertexId2 = new VertexId(1);
            Assert.IsFalse(_VertexId1 < _VertexId2);
        }

        #endregion

        #region op_Smaller_Smaller1_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Smaller1_Test()
        {
            var _VertexId1 = new VertexId(1);
            var _VertexId2 = new VertexId(2);
            Assert.IsTrue(_VertexId1 < _VertexId2);
        }

        #endregion

        #region op_Smaller_Smaller2_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Smaller2_Test()
        {
            var _VertexId1 = new VertexId(5);
            var _VertexId2 = new VertexId(23);
            Assert.IsTrue(_VertexId1 < _VertexId2);
        }

        #endregion

        #region op_Smaller_Bigger1_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Bigger1_Test()
        {
            var _VertexId1 = new VertexId(2);
            var _VertexId2 = new VertexId(1);
            Assert.IsFalse(_VertexId1 < _VertexId2);
        }

        #endregion

        #region op_Smaller_Bigger2_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Bigger2_Test()
        {
            var _VertexId1 = new VertexId(23);
            var _VertexId2 = new VertexId(5);
            Assert.IsFalse(_VertexId1 < _VertexId2);
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
            var      _VertexId1 = new VertexId();
            VertexId _VertexId2 = null;
            Assert.IsTrue(_VertexId1 <= _VertexId2);
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
            VertexId _VertexId1 = null;
            var      _VertexId2 = new VertexId();
            Assert.IsTrue(_VertexId1 <= _VertexId2);
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
            VertexId _VertexId1 = null;
            VertexId _VertexId2 = null;
            Assert.IsFalse(_VertexId1 <= _VertexId2);
        }

        #endregion

        #region op_SmallerOrEqual_SameReference_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator same reference.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_SameReference_Test()
        {
            var _VertexId1 = new VertexId();
            #pragma warning disable
            Assert.IsTrue(_VertexId1 <= _VertexId1);
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
            var _VertexId1 = new VertexId(1);
            var _VertexId2 = new VertexId(1);
            Assert.IsTrue(_VertexId1 <= _VertexId2);
        }

        #endregion

        #region op_SmallerOrEqual_SmallerThan1_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_SmallerThan1_Test()
        {
            var _VertexId1 = new VertexId(1);
            var _VertexId2 = new VertexId(2);
            Assert.IsTrue(_VertexId1 <= _VertexId2);
        }

        #endregion

        #region op_SmallerOrEqual_SmallerThan2_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_SmallerThan2_Test()
        {
            var _VertexId1 = new VertexId(5);
            var _VertexId2 = new VertexId(23);
            Assert.IsTrue(_VertexId1 <= _VertexId2);
        }

        #endregion

        #region op_SmallerOrEqual_Bigger1_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_Bigger1_Test()
        {
            var _VertexId1 = new VertexId(2);
            var _VertexId2 = new VertexId(1);
            Assert.IsFalse(_VertexId1 <= _VertexId2);
        }

        #endregion

        #region op_SmallerOrEqual_Bigger2_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_Bigger2_Test()
        {
            var _VertexId1 = new VertexId(23);
            var _VertexId2 = new VertexId(5);
            Assert.IsFalse(_VertexId1 <= _VertexId2);
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
            var      _VertexId1 = new VertexId();
            VertexId _VertexId2 = null;
            Assert.IsTrue(_VertexId1 > _VertexId2);
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
            VertexId _VertexId1 = null;
            var      _VertexId2 = new VertexId();
            Assert.IsTrue(_VertexId1 > _VertexId2);
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
            VertexId _VertexId1 = null;
            VertexId _VertexId2 = null;
            Assert.IsFalse(_VertexId1 > _VertexId2);
        }

        #endregion

        #region op_Bigger_SameReference_Test()

        /// <summary>
        /// A test for the bigger operator same reference.
        /// </summary>
        [Test]
        public void op_Bigger_SameReference_Test()
        {
            var _VertexId1 = new VertexId();
            #pragma warning disable
            Assert.IsFalse(_VertexId1 > _VertexId1);
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
            var _VertexId1 = new VertexId(1);
            var _VertexId2 = new VertexId(1);
            Assert.IsFalse(_VertexId1 > _VertexId2);
        }

        #endregion

        #region op_Bigger_Smaller1_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Smaller1_Test()
        {
            var _VertexId1 = new VertexId(1);
            var _VertexId2 = new VertexId(2);
            Assert.IsFalse(_VertexId1 > _VertexId2);
        }

        #endregion

        #region op_Bigger_Smaller2_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Smaller2_Test()
        {
            var _VertexId1 = new VertexId(5);
            var _VertexId2 = new VertexId(23);
            Assert.IsFalse(_VertexId1 > _VertexId2);
        }

        #endregion

        #region op_Bigger_Bigger1_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Bigger1_Test()
        {
            var _VertexId1 = new VertexId(2);
            var _VertexId2 = new VertexId(1);
            Assert.IsTrue(_VertexId1 > _VertexId2);
        }

        #endregion

        #region op_Bigger_Bigger2_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Bigger2_Test()
        {
            var _VertexId1 = new VertexId(23);
            var _VertexId2 = new VertexId(5);
            Assert.IsTrue(_VertexId1 > _VertexId2);
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
            var      _VertexId1 = new VertexId();
            VertexId _VertexId2 = null;
            Assert.IsTrue(_VertexId1 >= _VertexId2);
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
            VertexId _VertexId1 = null;
            var      _VertexId2 = new VertexId();
            Assert.IsTrue(_VertexId1 >= _VertexId2);
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
            VertexId _VertexId1 = null;
            VertexId _VertexId2 = null;
            Assert.IsFalse(_VertexId1 >= _VertexId2);
        }

        #endregion

        #region op_BiggerOrEqual_SameReference_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator same reference.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_SameReference_Test()
        {
            var _VertexId1 = new VertexId();
            #pragma warning disable
            Assert.IsTrue(_VertexId1 >= _VertexId1);
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
            var _VertexId1 = new VertexId(1);
            var _VertexId2 = new VertexId(1);
            Assert.IsTrue(_VertexId1 >= _VertexId2);
        }

        #endregion

        #region op_BiggerOrEqual_SmallerThan1_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_SmallerThan1_Test()
        {
            var _VertexId1 = new VertexId(1);
            var _VertexId2 = new VertexId(2);
            Assert.IsFalse(_VertexId1 >= _VertexId2);
        }

        #endregion

        #region op_BiggerOrEqual_SmallerThan2_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_SmallerThan2_Test()
        {
            var _VertexId1 = new VertexId(5);
            var _VertexId2 = new VertexId(23);
            Assert.IsFalse(_VertexId1 >= _VertexId2);
        }

        #endregion

        #region op_BiggerOrEqual_Bigger1_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_Bigger1_Test()
        {
            var _VertexId1 = new VertexId(2);
            var _VertexId2 = new VertexId(1);
            Assert.IsTrue(_VertexId1 >= _VertexId2);
        }

        #endregion

        #region op_BiggerOrEqual_Bigger2_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_Bigger2_Test()
        {
            var _VertexId1 = new VertexId(23);
            var _VertexId2 = new VertexId(5);
            Assert.IsTrue(_VertexId1 >= _VertexId2);
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
            var    _VertexId = VertexId.NewVertexId;
            Object _Object   = null;
            _VertexId.CompareTo(_Object);
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
            var      _VertexId = VertexId.NewVertexId;
            VertexId _Object   = null;
            _VertexId.CompareTo(_Object);
        }

        #endregion

        #region CompareToNonVertexIdTest()

        /// <summary>
        /// A test for CompareTo a non-VertexId.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CompareToNonVertexIdTest()
        {
            var _VertexId = VertexId.NewVertexId;
            var _Object   = "123";
            _VertexId.CompareTo(_Object);
        }

        #endregion

        #region CompareToSmallerTest1()

        /// <summary>
        /// A test for CompareTo smaller.
        /// </summary>
        [Test]
        public void CompareToSmallerTest1()
        {
            var _VertexId1 = new VertexId(1);
            var _VertexId2 = new VertexId(2);
            Assert.IsTrue(_VertexId1.CompareTo(_VertexId2) < 0);
        }

        #endregion

        #region CompareToSmallerTest2()

        /// <summary>
        /// A test for CompareTo smaller.
        /// </summary>
        [Test]
        public void CompareToSmallerTest2()
        {
            var _VertexId1 = new VertexId(5);
            var _VertexId2 = new VertexId(23);
            Assert.IsTrue(_VertexId1.CompareTo(_VertexId2) < 0);
        }

        #endregion

        #region CompareToEqualsTest()

        /// <summary>
        /// A test for CompareTo equals.
        /// </summary>
        [Test]
        public void CompareToEqualsTest()
        {
            var _VertexId1 = new VertexId(1);
            var _VertexId2 = new VertexId(1);
            Assert.IsTrue(_VertexId1.CompareTo(_VertexId2) == 0);
        }

        #endregion

        #region CompareToBiggerTest()

        /// <summary>
        /// A test for CompareTo bigger.
        /// </summary>
        [Test]
        public void CompareToBiggerTest()
        {
            var _VertexId1 = new VertexId(2);
            var _VertexId2 = new VertexId(1);
            Assert.IsTrue(_VertexId1.CompareTo(_VertexId2) > 0);
        }

        #endregion


        #region EqualsNullTest1()

        /// <summary>
        /// A test for equals null.
        /// </summary>
        [Test]
        public void EqualsNullTest1()
        {
            var    _VertexId = VertexId.NewVertexId;
            Object _Object   = null;
            Assert.IsFalse(_VertexId.Equals(_Object));
        }

        #endregion

        #region EqualsNullTest2()

        /// <summary>
        /// A test for equals null.
        /// </summary>
        [Test]
        public void EqualsNullTest2()
        {
            var      _VertexId = VertexId.NewVertexId;
            VertexId _Object   = null;
            Assert.IsFalse(_VertexId.Equals(_Object));
        }

        #endregion

        #region EqualsNonVertexIdTest()

        /// <summary>
        /// A test for equals a non-VertexId.
        /// </summary>
        [Test]
        public void EqualsNonVertexIdTest()
        {
            var _VertexId = VertexId.NewVertexId;
            var _Object   = "123";
            Assert.IsFalse(_VertexId.Equals(_Object));
        }

        #endregion

        #region EqualsEqualsTest()

        /// <summary>
        /// A test for equals.
        /// </summary>
        [Test]
        public void EqualsEqualsTest()
        {
            var _VertexId1 = new VertexId(1);
            var _VertexId2 = new VertexId(1);
            Assert.IsTrue(_VertexId1.Equals(_VertexId2));
        }

        #endregion

        #region EqualsNotEqualsTest()

        /// <summary>
        /// A test for not-equals.
        /// </summary>
        [Test]
        public void EqualsNotEqualsTest()
        {
            var _VertexId1 = new VertexId(1);
            var _VertexId2 = new VertexId(2);
            Assert.IsFalse(_VertexId1.Equals(_VertexId2));
        }

        #endregion


        #region GetHashCodeEqualTest()

        /// <summary>
        /// A test for GetHashCode
        /// </summary>
        [Test]
        public void GetHashCodeEqualTest()
        {
            var _SensorHashCode1 = new VertexId(5).GetHashCode();
            var _SensorHashCode2 = new VertexId(5).GetHashCode();
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
            var _SensorHashCode1 = new VertexId(1).GetHashCode();
            var _SensorHashCode2 = new VertexId(2).GetHashCode();
            Assert.AreNotEqual(_SensorHashCode1, _SensorHashCode2);
        }

        #endregion


        #region VertexIdsAndNUnitTest()

        /// <summary>
        /// Tests VertexIds in combination with NUnit.
        /// </summary>
        [Test]
        public void VertexIdsAndNUnitTest()
        {

            var a = new VertexId(1);
            var b = new VertexId(2);
            var c = new VertexId(1);

            Assert.AreEqual(a, a);
            Assert.AreEqual(b, b);
            Assert.AreEqual(c, c);

            Assert.AreEqual(a, c);
            Assert.AreNotEqual(a, b);
            Assert.AreNotEqual(b, c);

        }

        #endregion

        #region VertexIdsInHashSetTest()

        /// <summary>
        /// Test VertexIds within a HashSet.
        /// </summary>
        [Test]
        public void VertexIdsInHashSetTest()
        {

            var a = new VertexId(1);
            var b = new VertexId(2);
            var c = new VertexId(1);

            var _HashSet = new HashSet<VertexId>();
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
