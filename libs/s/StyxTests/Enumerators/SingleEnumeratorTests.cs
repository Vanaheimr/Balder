/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <achim@graph-database.org>
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

#endregion

namespace de.ahzf.Vanaheimr.Styx.UnitTests.Enumerators
{

    [TestFixture]
    public class SingleEnumeratorTests
    {

        #region testSingleIterator()

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void testSingleIterator()
        {

            var _Enumerator = new SingleEnumerator<String>("marko");

            Assert.IsTrue(_Enumerator.MoveNext());
            Assert.AreEqual("marko", _Enumerator.Current);
            Assert.IsFalse(_Enumerator.MoveNext());

            // Will throw an InvalidOperationException!
            Assert.AreEqual(null, _Enumerator.Current);

        }

        #endregion

        #region SingleEnumeratorTest()

        [Test]
        public void SingleEnumeratorTest()
        {

            var _SingleEnumerator = new SingleEnumerator<UInt64>(123);
            var _List             = new List<UInt64>();
                _List.Add(123);
            var _ListEnumerator   = _List.GetEnumerator();

            var _List1 = new List<UInt64>();
            var _List2 = new List<UInt64>();

            while (_SingleEnumerator.MoveNext())
                _List1.Add(_SingleEnumerator.Current);

            while (_ListEnumerator.MoveNext())
                _List2.Add(_ListEnumerator.Current);

            Assert.AreEqual(_List1.Count, _List2.Count);

        }

        #endregion

    }

}
