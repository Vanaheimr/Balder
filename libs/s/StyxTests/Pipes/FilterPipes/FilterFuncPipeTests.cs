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
    public class FuncFilterPipeTests
    {

        #region testFuncFilterPipeNormal()

        [Test]
        public void testFuncFilterPipeNormal()
        {

            var _Numbers = Enumerable.Range(1, 10);

            // Even or odd?
            var _Pipe = new FuncFilterPipe<Int32>((_Int32) => ((_Int32 & 0x1) != 0) ? true : false);
            _Pipe.SetSourceCollection(_Numbers);

            var _Counter = 0;
            while (_Pipe.MoveNext())
            {
                Assert.IsTrue((_Pipe.Current & 0x01) == 0);
                _Counter++;
            }

            Assert.AreEqual(_Counter, 5);

        }

        #endregion

        #region testFuncFilterPipeTrue()

        [Test]
        public void testFuncFilterPipeTrue()
        {

            var _Numbers = Enumerable.Range(1, 10);

            // Even or odd?
            var _Pipe = new FuncFilterPipe<Int32>((_Int32) => true);
            _Pipe.SetSourceCollection(_Numbers);

            var _Counter = 0;
            while (_Pipe.MoveNext())
            {
                _Counter++;
            }

            Assert.AreEqual(_Counter, 0);

        }

        #endregion

        #region testFuncFilterPipeFalse()

        [Test]
        public void testFuncFilterPipeFalse()
        {

            var _Numbers = Enumerable.Range(1, 10);

            // Even or odd?
            var _Pipe = new FuncFilterPipe<Int32>((_Int32) => false);
            _Pipe.SetSourceCollection(_Numbers);

            var _Counter = 0;
            while (_Pipe.MoveNext())
            {
                _Counter++;
            }

            Assert.AreEqual(_Counter, 10);

        }

        #endregion

        #region testFuncFilterPipeZero

        [Test]
        public void testFuncFilterPipeZero()
        {

            var _Numbers = new List<Int32>();
            var _Pipe    = new FuncFilterPipe<Int32>((_Int32) => false);
            _Pipe.SetSourceCollection(_Numbers);

            var _Counter = 0;
            Assert.IsFalse(_Pipe.Any());
            Assert.AreEqual(_Counter, 0);
            Assert.IsFalse(_Pipe.Any());

        }

        #endregion

        #region testFuncFilterPipeNull()

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testFuncFilterPipeNull()
        {

            Func<Int32, Boolean> myFuncFilter = null;
            var _Pipe = new FuncFilterPipe<Int32>(myFuncFilter);

        }

        #endregion

    }

}
