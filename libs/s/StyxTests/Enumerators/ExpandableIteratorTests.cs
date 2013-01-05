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
    public class ExpandableIteratorTests
    {

        #region testIdentityPipeNormal()

        [Test]
        public void testIdentityPipeNormal()
        {

            var _Enumerator = new ExpandableEnumerator<Int32>((new List<Int32>() { 1, 2, 3 }).GetEnumerator());

            Assert.IsTrue(_Enumerator.MoveNext());
            Assert.AreEqual(1, _Enumerator.Current);

            _Enumerator.Add(4);
            Assert.IsTrue(_Enumerator.MoveNext());
            Assert.AreEqual(4, _Enumerator.Current);
            _Enumerator.Add(5);
            _Enumerator.Add(6);
            Assert.IsTrue(_Enumerator.MoveNext());
            Assert.AreEqual(5, _Enumerator.Current);
            Assert.IsTrue(_Enumerator.MoveNext());
            Assert.AreEqual(6, _Enumerator.Current);
            Assert.IsTrue(_Enumerator.MoveNext());
            Assert.AreEqual(2, _Enumerator.Current);
            Assert.IsTrue(_Enumerator.MoveNext());
            Assert.AreEqual(3, _Enumerator.Current);
            Assert.IsFalse(_Enumerator.MoveNext());

        }

        #endregion

    }

}
