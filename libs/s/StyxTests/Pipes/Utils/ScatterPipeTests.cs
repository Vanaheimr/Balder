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
using System.Linq;
using System.Collections.Generic;

using NUnit.Framework;

#endregion

namespace de.ahzf.Vanaheimr.Styx.UnitTests.util
{

    [TestFixture]
    public class ScatterPipeTests
    {

        #region testScatterPipe()

        [Test]
        public void testScatterPipe()
        {

            var _Scatter = new ScatterPipe<Int32, Int32>();
            _Scatter.SetSourceCollection(new List<Int32>() { 1, 2, 3 });

            var _Counter = 0;
            while (_Scatter.MoveNext())
            {
                var _Object = _Scatter.Current;
                Assert.IsTrue(_Object.Equals(1) || _Object.Equals(2) || _Object.Equals(3));
                _Counter++;
            }

            Assert.AreEqual(3, _Counter);

        }

        #endregion

        #region testScatterPipeComplex()

        [Test]
        public void testScatterPipeComplex()
        {

            var _Scatter = new ScatterPipe<Object, Int32>();
            _Scatter.SetSourceCollection(new List<Object>() { 1, 2, new List<Object>() { 3, 4 }, 5, 6 });

            var _Counter = 0;
            while (_Scatter.MoveNext())
            {
                var _Object = _Scatter.Current;
                Assert.IsTrue(_Object.Equals(1) || _Object.Equals(2) || _Object.Equals(3) || _Object.Equals(4) || _Object.Equals(5) || _Object.Equals(6));
                _Counter++;
            }

            Assert.AreEqual(6, _Counter);

        }

        #endregion

    }

}
