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
    public class CSVPipeTests
    {

        #region testCSVPipeNormal1()

        [Test]
        public void testCSVPipeNormal1()
        {

            var _Pipe = new CSVReaderPipe(ExpectedNumberOfColumns:    5,
                                          FailOnWrongNumberOfColumns: true,
                                          IEnumerable: new List<String>() {
                                                           "#Id,Name,Verb,Help,Action",
                                                           "0,Alice,loves,to,read",
                                                           "1,Bob,likes,to,ski"
                                                       });

            var _Counter = 0;
            while (_Pipe.MoveNext())
            {
                Assert.IsTrue(_Pipe.Current[1] == "Alice" | _Pipe.Current[1] == "Bob");
                _Counter++;
            }

            Assert.AreEqual(_Counter, 2);

        }

        #endregion

        #region testCSVPipeNormal2()

        [Test]
        public void testCSVPipeNormal2()
        {

            var _Result = new List<String>() { "#Id,Name,Verb,Help,Action",
                                               "0, Alice ,loves,to,read",
                                               "1,Bob ,likes,to,ski" }.
                          CSVPipe(ExpectedNumberOfColumns:    5,
                                  FailOnWrongNumberOfColumns: true,
                                  TrimColumns:                true).
                          ToArray();

            Assert.AreEqual(2, _Result.Length);
            Assert.AreEqual("Alice", _Result[0][1]);
            Assert.AreEqual("Bob",   _Result[1][1]);

        }

        #endregion

        #region testCSVPipeNormal3()

        [Test]
        public void testCSVPipeNormal3()
        {

            var _Pipe = new CSVReaderPipe(StringSplitOptions: StringSplitOptions.RemoveEmptyEntries,
                                          IEnumerable:        new List<String>() {
                                                                  "#Id,Name,Friendlist",
                                                                  "    0,Alice,   a,,b,c, ,d,e     ,f,g   ",
                                                                  "",
                                                                  ",",
                                                                  "1,Bob,a,g,h"
                                                              });

            var _Counter = 0;
            while (_Pipe.MoveNext())
            {
                Assert.IsTrue(_Pipe.Current[1] == "Alice" | _Pipe.Current[1] == "Bob");
                Assert.IsTrue(_Pipe.Current[3] == "b"     | _Pipe.Current[3] == "g");
                _Counter++;
            }

            Assert.AreEqual(_Counter, 2);

        }

        #endregion

    }

}
