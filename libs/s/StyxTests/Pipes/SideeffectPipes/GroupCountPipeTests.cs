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

namespace de.ahzf.Vanaheimr.Styx.UnitTests.SideeffectPipes
{

    [TestFixture]
    public class GroupCountPipeTests
    {

        #region testCountCombinePipeNormal()

        [Test]
        public void testCountCombinePipeNormal()
        {

            var _Names = new List<String>() { "marko", "josh", "peter", "peter", "peter", "josh" };
            var _Pipe  = new GroupCountPipe<String>();
            _Pipe.SetSourceCollection(_Names);

            var _Counter = 0;
            foreach (var name in _Pipe)
            {
                Assert.IsTrue(name.Equals("marko") || name.Equals("josh") || name.Equals("peter"));
                _Counter++;
            }

            Assert.AreEqual(6UL, _Counter);
            Assert.AreEqual(1UL, _Pipe.SideEffect["marko"]);
            Assert.AreEqual(2UL, _Pipe.SideEffect["josh"]);
            Assert.AreEqual(3UL, _Pipe.SideEffect["peter"]);

            Assert.IsFalse(_Pipe.SideEffect.ContainsKey("povel"));

        }

        #endregion

        #region testCountCombinePipeZero()

        [Test]
        public void testCountCombinePipeZero()
        {

            var _Names = new List<String>();
            var _Pipe  = new GroupCountPipe<String>();
            _Pipe.SetSourceCollection(_Names);

            Assert.IsFalse(_Pipe.MoveNext());
            Assert.IsFalse(_Pipe.SideEffect.ContainsKey("povel"));

        }

        #endregion

    }

}
