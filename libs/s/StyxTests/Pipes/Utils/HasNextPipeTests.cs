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
    public class HasNextPipeTests
    {

        #region testPipeBasic()

        [Test]
        public void testPipeBasic()
        {

            var _Names = new List<String>() { "marko", "povel", "peter", "josh" };

            var _Pipe = new HasNextPipe<String>(new IdentityPipe<String>());
            _Pipe.SetSourceCollection(_Names);

            var _Counter = 0;
            while (_Pipe.MoveNext())
            {
                _Counter++;
                Assert.IsTrue(_Pipe.Current);
            }
            
            Assert.AreEqual(4, _Counter);

        }

        #endregion

    }

}
