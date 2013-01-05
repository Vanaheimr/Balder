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

using NUnit.Framework;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Vanaheimr.Styx.UnitTests.Pipes
{

    [TestFixture]
    public class ActionPipeTests
    {

        #region testActionPipeNormal()

        [Test]
        public void testActionPipeNormal()
        {

            var _Sum     = 0;
            var _Numbers = Enumerable.Range(1, 10);
            var _Pipe    = new ActionPipe<Int32>((_Int32) => _Sum += _Int32);
            _Pipe.SetSourceCollection(_Numbers);

            var _Counter = 0;
            while (_Pipe.MoveNext())
            {
                Assert.AreEqual(_Pipe.Current, _Numbers.ElementAt(_Counter));
                _Counter++;
            }

            Assert.AreEqual(_Counter, 10);
            Assert.AreEqual(_Sum,     55);

        }

        #endregion

        #region testActionPipeZero

        [Test]
        public void testActionPipeZero()
        {

            var _Sum     = 0;
            var _Numbers = new List<Int32>();
            var _Pipe    = new ActionPipe<Int32>((_Int32) => _Sum += _Int32);
            _Pipe.SetSourceCollection(_Numbers);

            var _Counter = 0;
            Assert.IsFalse(_Pipe.Any());
            Assert.AreEqual(_Counter, 0);
            Assert.AreEqual(_Sum,     0);
            Assert.IsFalse(_Pipe.Any());

        }

        #endregion

        #region testActionPipeNull()

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testActionPipeNull()
        {

            Action<Int32> myAction = null;
            var _Pipe = new ActionPipe<Int32>(myAction);

        }

        #endregion

    }

}
