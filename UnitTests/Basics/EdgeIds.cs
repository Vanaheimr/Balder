/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Pipes.NET
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

using System.Collections.Generic;

using NUnit.Framework;

#endregion

namespace de.ahzf.blueprints.UnitTests.Basics
{

    [TestFixture]
    public class EdgeIds
    {

        #region testEdgeIds()

        [Test]
        public void testEdgeIds()
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

        #region testEdgeIdsInHashSet()

        [Test]
        public void testEdgeIdsInHashSet()
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
