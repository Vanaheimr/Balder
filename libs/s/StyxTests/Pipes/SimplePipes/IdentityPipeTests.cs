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
    public class IdentityPipeTests
    {

        #region testIdentityPipeNormal()

        [Test]
        public void testIdentityPipeNormal()
        {

            var _UUIDs = BaseTest.GenerateUUIDs(100);
            var _Pipe  = new IdentityPipe<String>();
            _Pipe.SetSourceCollection(_UUIDs);

            var _Counter = 0;
            while (_Pipe.MoveNext())
            {
                Assert.AreEqual(_Pipe.Current, _UUIDs.ElementAt(_Counter));
                _Counter++;
            }

            Assert.AreEqual(_Counter, 100);

        }

        #endregion

        #region testIdentityPipeZero
        
        [Test]
        public void testIdentityPipeZero()
        {

            var _UUIDs = BaseTest.GenerateUUIDs(0);
            var _Pipe  = new IdentityPipe<String>();
            _Pipe.SetSourceCollection(_UUIDs);

            var _Counter = 0;
            Assert.IsFalse(_Pipe.Any());
            Assert.AreEqual(_Counter, 0);
            Assert.IsFalse(_Pipe.Any());

        }

        #endregion

        #region testIdentityPipeInt32()

        [Test]
        public void testIdentityPipeInt32()
        {

            var _Numbers = new List<Int32>() { 1, 2, 3, 4 };
            var _Pipe    = new IdentityPipe<Int32>();
            _Pipe.SetSourceCollection(_Numbers);

            var _Counter = 0;
            while (_Pipe.MoveNext())
            {
                Assert.AreEqual(_Pipe.Current, _Numbers.ElementAt(_Counter));
                _Counter++;
            }

            Assert.AreEqual(_Counter, 4);

        }

        #endregion

    }

}
