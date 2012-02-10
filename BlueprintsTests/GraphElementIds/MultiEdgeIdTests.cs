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

namespace de.ahzf.Blueprints.UnitTests
{

    /// <summary>
    /// Unit tests for the MultiEdgeId class.
    /// </summary>
    [TestFixture]
    public class MultiEdgeIdTests
    {

        #region MultiEdgeIdEmptyConstructorTest()

        /// <summary>
        /// A test for an empty MultiEdgeId constructor.
        /// </summary>
        [Test]
        public void MultiEdgeIdEmptyConstructorTest()
        {
            var _MultiEdgeId1 = new MultiEdgeId();
            var _MultiEdgeId2 = new MultiEdgeId();
            Assert.IsTrue(_MultiEdgeId1.Length > 0);
            Assert.IsTrue(_MultiEdgeId2.Length > 0);
            Assert.AreNotEqual(_MultiEdgeId1, _MultiEdgeId2);
        }

        #endregion

        #region MultiEdgeIdStringConstructorTest()

        /// <summary>
        /// A test for the MultiEdgeId string constructor.
        /// </summary>
        [Test]
        public void MultiEdgeIdStringConstructorTest()
        {
            var _MultiEdgeId = new MultiEdgeId("123");
            Assert.AreEqual("123", _MultiEdgeId.ToString());
            Assert.AreEqual(3,     _MultiEdgeId.Length);
        }

        #endregion

        #region MultiEdgeIdInt32ConstructorTest()

        /// <summary>
        /// A test for the MultiEdgeId Int32 constructor.
        /// </summary>
        [Test]
        public void MultiEdgeIdInt32ConstructorTest()
        {
            var _MultiEdgeId = new MultiEdgeId(5);
            Assert.AreEqual("5", _MultiEdgeId.ToString());
            Assert.AreEqual(1,   _MultiEdgeId.Length);
        }

        #endregion

        #region MultiEdgeIdUInt32ConstructorTest()

        /// <summary>
        /// A test for the MultiEdgeId UInt32 constructor.
        /// </summary>
        [Test]
        public void MultiEdgeIdUInt32ConstructorTest()
        {
            var _MultiEdgeId = new MultiEdgeId(23U);
            Assert.AreEqual("23", _MultiEdgeId.ToString());
            Assert.AreEqual(2,    _MultiEdgeId.Length);
        }

        #endregion

        #region MultiEdgeIdInt64ConstructorTest()

        /// <summary>
        /// A test for the MultiEdgeId Int64 constructor.
        /// </summary>
        [Test]
        public void MultiEdgeIdInt64ConstructorTest()
        {
            var _MultiEdgeId = new MultiEdgeId(42L);
            Assert.AreEqual("42", _MultiEdgeId.ToString());
            Assert.AreEqual(2,    _MultiEdgeId.Length);
        }

        #endregion

        #region MultiEdgeIdUInt64ConstructorTest()

        /// <summary>
        /// A test for the MultiEdgeId UInt64 constructor.
        /// </summary>
        [Test]
        public void MultiEdgeIdUInt64ConstructorTest()
        {
            var _MultiEdgeId = new MultiEdgeId(123UL);
            Assert.AreEqual("123", _MultiEdgeId.ToString());
            Assert.AreEqual(3,     _MultiEdgeId.Length);
        }

        #endregion

        #region MultiEdgeIdMultiEdgeIdConstructorTest()

        /// <summary>
        /// A test for the MultiEdgeId MultiEdgeId constructor.
        /// </summary>
        [Test]
        public void MultiEdgeIdMultiEdgeIdConstructorTest()
        {
            var _MultiEdgeId1 = MultiEdgeId.NewMultiEdgeId;
            var _MultiEdgeId2 = new MultiEdgeId(_MultiEdgeId1);
            Assert.AreEqual(_MultiEdgeId1.ToString(), _MultiEdgeId2.ToString());
            Assert.AreEqual(_MultiEdgeId1.Length,     _MultiEdgeId2.Length);
            Assert.AreEqual(_MultiEdgeId1,            _MultiEdgeId2);
        }

        #endregion

        #region MultiEdgeIdUriConstructorTest()

        /// <summary>
        /// A test for the MultiEdgeId Uri constructor.
        /// </summary>
        [Test]
        public void MultiEdgeIdUriConstructorTest()
        {
            var _MultiEdgeId = new MultiEdgeId(new Uri("http://example.org"));
            Assert.AreEqual("http://example.org/", _MultiEdgeId.ToString());
            Assert.AreEqual(19,                    _MultiEdgeId.Length);
        }

        #endregion


        #region NewMultiEdgeIdMethodTest()

        /// <summary>
        /// A test for the static newMultiEdgeId method.
        /// </summary>
        [Test]
        public void NewMultiEdgeIdMethodTest()
        {
            var _MultiEdgeId1 = MultiEdgeId.NewMultiEdgeId;
            var _MultiEdgeId2 = MultiEdgeId.NewMultiEdgeId;
            Assert.AreNotEqual(_MultiEdgeId1, _MultiEdgeId2);
        }

        #endregion


        #region op_Equality_Null_Test1()

        /// <summary>
        /// A test for the equality operator null.
        /// </summary>
        [Test]
        public void op_Equality_Null_Test1()
        {
            var      _MultiEdgeId1 = new MultiEdgeId();
            MultiEdgeId _MultiEdgeId2 = null;
            Assert.IsFalse(_MultiEdgeId1 == _MultiEdgeId2);
        }

        #endregion

        #region op_Equality_Null_Test2()

        /// <summary>
        /// A test for the equality operator null.
        /// </summary>
        [Test]
        public void op_Equality_Null_Test2()
        {
            MultiEdgeId _MultiEdgeId1 = null;
            var    _MultiEdgeId2 = new MultiEdgeId();
            Assert.IsFalse(_MultiEdgeId1 == _MultiEdgeId2);
        }

        #endregion

        #region op_Equality_BothNull_Test()

        /// <summary>
        /// A test for the equality operator both null.
        /// </summary>
        [Test]
        public void op_Equality_BothNull_Test()
        {
            MultiEdgeId _MultiEdgeId1 = null;
            MultiEdgeId _MultiEdgeId2 = null;
            Assert.IsTrue(_MultiEdgeId1 == _MultiEdgeId2);
        }

        #endregion

        #region op_Equality_SameReference_Test()

        /// <summary>
        /// A test for the equality operator same reference.
        /// </summary>
        [Test]
        
        public void op_Equality_SameReference_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId();
            #pragma warning disable
            Assert.IsTrue(_MultiEdgeId1 == _MultiEdgeId1);
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
            var _MultiEdgeId1 = new MultiEdgeId(1);
            var _MultiEdgeId2 = new MultiEdgeId(1);
            Assert.IsTrue(_MultiEdgeId1 == _MultiEdgeId2);
        }

        #endregion

        #region op_Equality_NotEquals_Test()

        /// <summary>
        /// A test for the equality operator not-equals.
        /// </summary>
        [Test]
        public void op_Equality_NotEquals_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId(1);
            var _MultiEdgeId2 = new MultiEdgeId(2);
            Assert.IsFalse(_MultiEdgeId1 == _MultiEdgeId2);
        }

        #endregion


        #region op_Inequality_Null_Test1()

        /// <summary>
        /// A test for the inequality operator null.
        /// </summary>
        [Test]
        public void op_Inequality_Null_Test1()
        {
            var      _MultiEdgeId1 = new MultiEdgeId();
            MultiEdgeId _MultiEdgeId2 = null;
            Assert.IsTrue(_MultiEdgeId1 != _MultiEdgeId2);
        }

        #endregion

        #region op_Inequality_Null_Test2()

        /// <summary>
        /// A test for the inequality operator null.
        /// </summary>
        [Test]
        public void op_Inequality_Null_Test2()
        {
            MultiEdgeId _MultiEdgeId1 = null;
            var      _MultiEdgeId2 = new MultiEdgeId();
            Assert.IsTrue(_MultiEdgeId1 != _MultiEdgeId2);
        }

        #endregion

        #region op_Inequality_BothNull_Test()

        /// <summary>
        /// A test for the inequality operator both null.
        /// </summary>
        [Test]
        public void op_Inequality_BothNull_Test()
        {
            MultiEdgeId _MultiEdgeId1 = null;
            MultiEdgeId _MultiEdgeId2 = null;
            Assert.IsFalse(_MultiEdgeId1 != _MultiEdgeId2);
        }

        #endregion

        #region op_Inequality_SameReference_Test()

        /// <summary>
        /// A test for the inequality operator same reference.
        /// </summary>
        [Test]
        public void op_Inequality_SameReference_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId();
            #pragma warning disable
            Assert.IsFalse(_MultiEdgeId1 != _MultiEdgeId1);
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
            var _MultiEdgeId1 = new MultiEdgeId(1);
            var _MultiEdgeId2 = new MultiEdgeId(1);
            Assert.IsFalse(_MultiEdgeId1 != _MultiEdgeId2);
        }

        #endregion

        #region op_Inequality_NotEquals1_Test()

        /// <summary>
        /// A test for the inequality operator not-equals.
        /// </summary>
        [Test]
        public void op_Inequality_NotEquals1_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId(1);
            var _MultiEdgeId2 = new MultiEdgeId(2);
            Assert.IsTrue(_MultiEdgeId1 != _MultiEdgeId2);
        }

        #endregion

        #region op_Inequality_NotEquals2_Test()

        /// <summary>
        /// A test for the inequality operator not-equals.
        /// </summary>
        [Test]
        public void op_Inequality_NotEquals2_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId(5);
            var _MultiEdgeId2 = new MultiEdgeId(23);
            Assert.IsTrue(_MultiEdgeId1 != _MultiEdgeId2);
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
            var      _MultiEdgeId1 = new MultiEdgeId();
            MultiEdgeId _MultiEdgeId2 = null;
            Assert.IsTrue(_MultiEdgeId1 < _MultiEdgeId2);
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
            MultiEdgeId _MultiEdgeId1 = null;
            var      _MultiEdgeId2 = new MultiEdgeId();
            Assert.IsTrue(_MultiEdgeId1 < _MultiEdgeId2);
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
            MultiEdgeId _MultiEdgeId1 = null;
            MultiEdgeId _MultiEdgeId2 = null;
            Assert.IsFalse(_MultiEdgeId1 < _MultiEdgeId2);
        }

        #endregion

        #region op_Smaller_SameReference_Test()

        /// <summary>
        /// A test for the smaller operator same reference.
        /// </summary>
        [Test]
        public void op_Smaller_SameReference_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId();
            #pragma warning disable
            Assert.IsFalse(_MultiEdgeId1 < _MultiEdgeId1);
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
            var _MultiEdgeId1 = new MultiEdgeId(1);
            var _MultiEdgeId2 = new MultiEdgeId(1);
            Assert.IsFalse(_MultiEdgeId1 < _MultiEdgeId2);
        }

        #endregion

        #region op_Smaller_Smaller1_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Smaller1_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId(1);
            var _MultiEdgeId2 = new MultiEdgeId(2);
            Assert.IsTrue(_MultiEdgeId1 < _MultiEdgeId2);
        }

        #endregion

        #region op_Smaller_Smaller2_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Smaller2_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId(5);
            var _MultiEdgeId2 = new MultiEdgeId(23);
            Assert.IsTrue(_MultiEdgeId1 < _MultiEdgeId2);
        }

        #endregion

        #region op_Smaller_Bigger1_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Bigger1_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId(2);
            var _MultiEdgeId2 = new MultiEdgeId(1);
            Assert.IsFalse(_MultiEdgeId1 < _MultiEdgeId2);
        }

        #endregion

        #region op_Smaller_Bigger2_Test()

        /// <summary>
        /// A test for the smaller operator not-equals.
        /// </summary>
        [Test]
        public void op_Smaller_Bigger2_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId(23);
            var _MultiEdgeId2 = new MultiEdgeId(5);
            Assert.IsFalse(_MultiEdgeId1 < _MultiEdgeId2);
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
            var      _MultiEdgeId1 = new MultiEdgeId();
            MultiEdgeId _MultiEdgeId2 = null;
            Assert.IsTrue(_MultiEdgeId1 <= _MultiEdgeId2);
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
            MultiEdgeId _MultiEdgeId1 = null;
            var      _MultiEdgeId2 = new MultiEdgeId();
            Assert.IsTrue(_MultiEdgeId1 <= _MultiEdgeId2);
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
            MultiEdgeId _MultiEdgeId1 = null;
            MultiEdgeId _MultiEdgeId2 = null;
            Assert.IsFalse(_MultiEdgeId1 <= _MultiEdgeId2);
        }

        #endregion

        #region op_SmallerOrEqual_SameReference_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator same reference.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_SameReference_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId();
            #pragma warning disable
            Assert.IsTrue(_MultiEdgeId1 <= _MultiEdgeId1);
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
            var _MultiEdgeId1 = new MultiEdgeId(1);
            var _MultiEdgeId2 = new MultiEdgeId(1);
            Assert.IsTrue(_MultiEdgeId1 <= _MultiEdgeId2);
        }

        #endregion

        #region op_SmallerOrEqual_SmallerThan1_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_SmallerThan1_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId(1);
            var _MultiEdgeId2 = new MultiEdgeId(2);
            Assert.IsTrue(_MultiEdgeId1 <= _MultiEdgeId2);
        }

        #endregion

        #region op_SmallerOrEqual_SmallerThan2_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_SmallerThan2_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId(5);
            var _MultiEdgeId2 = new MultiEdgeId(23);
            Assert.IsTrue(_MultiEdgeId1 <= _MultiEdgeId2);
        }

        #endregion

        #region op_SmallerOrEqual_Bigger1_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_Bigger1_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId(2);
            var _MultiEdgeId2 = new MultiEdgeId(1);
            Assert.IsFalse(_MultiEdgeId1 <= _MultiEdgeId2);
        }

        #endregion

        #region op_SmallerOrEqual_Bigger2_Test()

        /// <summary>
        /// A test for the smallerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_SmallerOrEqual_Bigger2_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId(23);
            var _MultiEdgeId2 = new MultiEdgeId(5);
            Assert.IsFalse(_MultiEdgeId1 <= _MultiEdgeId2);
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
            var      _MultiEdgeId1 = new MultiEdgeId();
            MultiEdgeId _MultiEdgeId2 = null;
            Assert.IsTrue(_MultiEdgeId1 > _MultiEdgeId2);
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
            MultiEdgeId _MultiEdgeId1 = null;
            var      _MultiEdgeId2 = new MultiEdgeId();
            Assert.IsTrue(_MultiEdgeId1 > _MultiEdgeId2);
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
            MultiEdgeId _MultiEdgeId1 = null;
            MultiEdgeId _MultiEdgeId2 = null;
            Assert.IsFalse(_MultiEdgeId1 > _MultiEdgeId2);
        }

        #endregion

        #region op_Bigger_SameReference_Test()

        /// <summary>
        /// A test for the bigger operator same reference.
        /// </summary>
        [Test]
        public void op_Bigger_SameReference_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId();
            #pragma warning disable
            Assert.IsFalse(_MultiEdgeId1 > _MultiEdgeId1);
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
            var _MultiEdgeId1 = new MultiEdgeId(1);
            var _MultiEdgeId2 = new MultiEdgeId(1);
            Assert.IsFalse(_MultiEdgeId1 > _MultiEdgeId2);
        }

        #endregion

        #region op_Bigger_Smaller1_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Smaller1_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId(1);
            var _MultiEdgeId2 = new MultiEdgeId(2);
            Assert.IsFalse(_MultiEdgeId1 > _MultiEdgeId2);
        }

        #endregion

        #region op_Bigger_Smaller2_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Smaller2_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId(5);
            var _MultiEdgeId2 = new MultiEdgeId(23);
            Assert.IsFalse(_MultiEdgeId1 > _MultiEdgeId2);
        }

        #endregion

        #region op_Bigger_Bigger1_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Bigger1_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId(2);
            var _MultiEdgeId2 = new MultiEdgeId(1);
            Assert.IsTrue(_MultiEdgeId1 > _MultiEdgeId2);
        }

        #endregion

        #region op_Bigger_Bigger2_Test()

        /// <summary>
        /// A test for the bigger operator not-equals.
        /// </summary>
        [Test]
        public void op_Bigger_Bigger2_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId(23);
            var _MultiEdgeId2 = new MultiEdgeId(5);
            Assert.IsTrue(_MultiEdgeId1 > _MultiEdgeId2);
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
            var      _MultiEdgeId1 = new MultiEdgeId();
            MultiEdgeId _MultiEdgeId2 = null;
            Assert.IsTrue(_MultiEdgeId1 >= _MultiEdgeId2);
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
            MultiEdgeId _MultiEdgeId1 = null;
            var      _MultiEdgeId2 = new MultiEdgeId();
            Assert.IsTrue(_MultiEdgeId1 >= _MultiEdgeId2);
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
            MultiEdgeId _MultiEdgeId1 = null;
            MultiEdgeId _MultiEdgeId2 = null;
            Assert.IsFalse(_MultiEdgeId1 >= _MultiEdgeId2);
        }

        #endregion

        #region op_BiggerOrEqual_SameReference_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator same reference.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_SameReference_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId();
            #pragma warning disable
            Assert.IsTrue(_MultiEdgeId1 >= _MultiEdgeId1);
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
            var _MultiEdgeId1 = new MultiEdgeId(1);
            var _MultiEdgeId2 = new MultiEdgeId(1);
            Assert.IsTrue(_MultiEdgeId1 >= _MultiEdgeId2);
        }

        #endregion

        #region op_BiggerOrEqual_SmallerThan1_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_SmallerThan1_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId(1);
            var _MultiEdgeId2 = new MultiEdgeId(2);
            Assert.IsFalse(_MultiEdgeId1 >= _MultiEdgeId2);
        }

        #endregion

        #region op_BiggerOrEqual_SmallerThan2_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_SmallerThan2_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId(5);
            var _MultiEdgeId2 = new MultiEdgeId(23);
            Assert.IsFalse(_MultiEdgeId1 >= _MultiEdgeId2);
        }

        #endregion

        #region op_BiggerOrEqual_Bigger1_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_Bigger1_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId(2);
            var _MultiEdgeId2 = new MultiEdgeId(1);
            Assert.IsTrue(_MultiEdgeId1 >= _MultiEdgeId2);
        }

        #endregion

        #region op_BiggerOrEqual_Bigger2_Test()

        /// <summary>
        /// A test for the biggerOrEqual operator not-equals.
        /// </summary>
        [Test]
        public void op_BiggerOrEqual_Bigger2_Test()
        {
            var _MultiEdgeId1 = new MultiEdgeId(23);
            var _MultiEdgeId2 = new MultiEdgeId(5);
            Assert.IsTrue(_MultiEdgeId1 >= _MultiEdgeId2);
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
            var    _MultiEdgeId = MultiEdgeId.NewMultiEdgeId;
            Object _Object   = null;
            _MultiEdgeId.CompareTo(_Object);
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
            var      _MultiEdgeId = MultiEdgeId.NewMultiEdgeId;
            MultiEdgeId _Object   = null;
            _MultiEdgeId.CompareTo(_Object);
        }

        #endregion

        #region CompareToNonMultiEdgeIdTest()

        /// <summary>
        /// A test for CompareTo a non-MultiEdgeId.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CompareToNonMultiEdgeIdTest()
        {
            var _MultiEdgeId = MultiEdgeId.NewMultiEdgeId;
            var _Object   = "123";
            _MultiEdgeId.CompareTo(_Object);
        }

        #endregion

        #region CompareToSmallerTest1()

        /// <summary>
        /// A test for CompareTo smaller.
        /// </summary>
        [Test]
        public void CompareToSmallerTest1()
        {
            var _MultiEdgeId1 = new MultiEdgeId(1);
            var _MultiEdgeId2 = new MultiEdgeId(2);
            Assert.IsTrue(_MultiEdgeId1.CompareTo(_MultiEdgeId2) < 0);
        }

        #endregion

        #region CompareToSmallerTest2()

        /// <summary>
        /// A test for CompareTo smaller.
        /// </summary>
        [Test]
        public void CompareToSmallerTest2()
        {
            var _MultiEdgeId1 = new MultiEdgeId(5);
            var _MultiEdgeId2 = new MultiEdgeId(23);
            Assert.IsTrue(_MultiEdgeId1.CompareTo(_MultiEdgeId2) < 0);
        }

        #endregion

        #region CompareToEqualsTest()

        /// <summary>
        /// A test for CompareTo equals.
        /// </summary>
        [Test]
        public void CompareToEqualsTest()
        {
            var _MultiEdgeId1 = new MultiEdgeId(1);
            var _MultiEdgeId2 = new MultiEdgeId(1);
            Assert.IsTrue(_MultiEdgeId1.CompareTo(_MultiEdgeId2) == 0);
        }

        #endregion

        #region CompareToBiggerTest()

        /// <summary>
        /// A test for CompareTo bigger.
        /// </summary>
        [Test]
        public void CompareToBiggerTest()
        {
            var _MultiEdgeId1 = new MultiEdgeId(2);
            var _MultiEdgeId2 = new MultiEdgeId(1);
            Assert.IsTrue(_MultiEdgeId1.CompareTo(_MultiEdgeId2) > 0);
        }

        #endregion


        #region EqualsNullTest1()

        /// <summary>
        /// A test for equals null.
        /// </summary>
        [Test]
        public void EqualsNullTest1()
        {
            var    _MultiEdgeId = MultiEdgeId.NewMultiEdgeId;
            Object _Object   = null;
            Assert.IsFalse(_MultiEdgeId.Equals(_Object));
        }

        #endregion

        #region EqualsNullTest2()

        /// <summary>
        /// A test for equals null.
        /// </summary>
        [Test]
        public void EqualsNullTest2()
        {
            var    _MultiEdgeId = MultiEdgeId.NewMultiEdgeId;
            MultiEdgeId _Object   = null;
            Assert.IsFalse(_MultiEdgeId.Equals(_Object));
        }

        #endregion

        #region EqualsNonMultiEdgeIdTest()

        /// <summary>
        /// A test for equals a non-MultiEdgeId.
        /// </summary>
        [Test]
        public void EqualsNonMultiEdgeIdTest()
        {
            var _MultiEdgeId = MultiEdgeId.NewMultiEdgeId;
            var _Object   = "123";
            Assert.IsFalse(_MultiEdgeId.Equals(_Object));
        }

        #endregion

        #region EqualsEqualsTest()

        /// <summary>
        /// A test for equals.
        /// </summary>
        [Test]
        public void EqualsEqualsTest()
        {
            var _MultiEdgeId1 = new MultiEdgeId(1);
            var _MultiEdgeId2 = new MultiEdgeId(1);
            Assert.IsTrue(_MultiEdgeId1.Equals(_MultiEdgeId2));
        }

        #endregion

        #region EqualsNotEqualsTest()

        /// <summary>
        /// A test for not-equals.
        /// </summary>
        [Test]
        public void EqualsNotEqualsTest()
        {
            var _MultiEdgeId1 = new MultiEdgeId(1);
            var _MultiEdgeId2 = new MultiEdgeId(2);
            Assert.IsFalse(_MultiEdgeId1.Equals(_MultiEdgeId2));
        }

        #endregion


        #region GetHashCodeEqualTest()

        /// <summary>
        /// A test for GetHashCode
        /// </summary>
        [Test]
        public void GetHashCodeEqualTest()
        {
            var _SensorHashCode1 = new MultiEdgeId(5).GetHashCode();
            var _SensorHashCode2 = new MultiEdgeId(5).GetHashCode();
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
            var _SensorHashCode1 = new MultiEdgeId(1).GetHashCode();
            var _SensorHashCode2 = new MultiEdgeId(2).GetHashCode();
            Assert.AreNotEqual(_SensorHashCode1, _SensorHashCode2);
        }

        #endregion


        #region MultiEdgeIdsAndNUnitTest()

        /// <summary>
        /// Tests MultiEdgeIds in combination with NUnit.
        /// </summary>
        [Test]
        public void MultiEdgeIdsAndNUnitTest()
        {

            var a = new MultiEdgeId(1);
            var b = new MultiEdgeId(2);
            var c = new MultiEdgeId(1);

            Assert.AreEqual(a, a);
            Assert.AreEqual(b, b);
            Assert.AreEqual(c, c);
            
            Assert.AreEqual(a, c);
            Assert.AreNotEqual(a, b);
            Assert.AreNotEqual(b, c);

        }

        #endregion

        #region MultiEdgeIdsInHashSetTest()

        /// <summary>
        /// Test MultiEdgeIds within a HashSet.
        /// </summary>
        [Test]
        public void MultiEdgeIdsInHashSetTest()
        {

            var a = new MultiEdgeId(1);
            var b = new MultiEdgeId(2);
            var c = new MultiEdgeId(1);

            var _HashSet = new HashSet<MultiEdgeId>();
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
