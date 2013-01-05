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
    public class DualFuncPipeTests
    {

        #region testDualFuncPipeNormal()

        [Test]
        public void testDualFuncPipeNormal()
        {

            var _Strings = new List<String>() { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" };
            var _Numbers = Enumerable.Range(1, 10);
            var _Pipe    = new FuncPipe<String, Int32, String>((A, B) => A + "-" + B);
            _Pipe.SetSourceCollection1(_Strings);
            _Pipe.SetSourceCollection2(_Numbers);

            var _Counter = 0;
            while (_Pipe.MoveNext())
            {
                Assert.AreEqual(_Pipe.Current, _Strings.ElementAt(_Counter) + "-" + _Numbers.ElementAt(_Counter));
                _Counter++;
            }

            Assert.AreEqual(_Counter, 10);

        }

        #endregion

        #region testDualFuncPipeFirstZero

        [Test]
        public void testDualFuncPipeFirstZero()
        {

            var _Strings = new List<String>();
            var _Numbers = Enumerable.Range(1, 10);
            var _Pipe = new FuncPipe<String, Int32, String>((A, B) => A + "-" + B);
            _Pipe.SetSourceCollection1(_Strings);
            _Pipe.SetSourceCollection2(_Numbers);

            var _Counter = 0;
            Assert.IsFalse(_Pipe.Any());
            Assert.AreEqual(_Counter, 0);
            Assert.IsFalse(_Pipe.Any());

        }

        #endregion

        #region testDualFuncPipeSecondZero

        [Test]
        public void testDualFuncPipeSecondZero()
        {

            var _Strings = new List<String>() { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" };
            var _Numbers = new List<Int32>();
            var _Pipe = new FuncPipe<String, Int32, String>((A, B) => A + "-" + B);
            _Pipe.SetSourceCollection1(_Strings);
            _Pipe.SetSourceCollection2(_Numbers);

            var _Counter = 0;
            Assert.IsFalse(_Pipe.Any());
            Assert.AreEqual(_Counter, 0);
            Assert.IsFalse(_Pipe.Any());

        }

        #endregion

        #region testDualFuncPipeBothZero

        [Test]
        public void testDualFuncPipeBothZero()
        {

            var _Strings = new List<String>();
            var _Numbers = new List<Int32>();
            var _Pipe = new FuncPipe<String, Int32, String>((A, B) => A + "-" + B);
            _Pipe.SetSourceCollection1(_Strings);
            _Pipe.SetSourceCollection2(_Numbers);

            var _Counter = 0;
            Assert.IsFalse(_Pipe.Any());
            Assert.AreEqual(_Counter, 0);
            Assert.IsFalse(_Pipe.Any());

        }

        #endregion

        #region testDualFuncPipeNull()

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testDualFuncPipeNull()
        {

            Func<String,Int32, String> myFunc = null;
            var _Pipe = new FuncPipe<String, Int32, String>(myFunc);

        }

        #endregion

    }

}
