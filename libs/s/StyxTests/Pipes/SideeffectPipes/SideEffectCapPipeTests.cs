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

namespace de.ahzf.Vanaheimr.Styx.UnitTests.SideeffectPipes
{

    [TestFixture]
    public class SideEffectCapPipeTests
    {

        #region testSideEffectCapPipeNormalCount()
		
		//TODO: Mono build problems!

        [Test]
        public void testSideEffectCapPipeNormalCount()
        {

            var _List = new List<String>() { "marko", "antonio", "rodriguez", "was", "here", "." };
            var _Pipe = new SideEffectCapPipe<String, Int64>(new CountPipe<String>());
            _Pipe.SetSourceCollection(_List);

            Assert.IsTrue(_Pipe.MoveNext());
            Assert.AreEqual(6UL, _Pipe.Current);
            Assert.IsFalse(_Pipe.MoveNext());

        }

        #endregion

        #region testSideEffectCapPipeZeroCount()

        [Test]
        public void testSideEffectCapPipeZeroCount()
        {

            var _List = new List<String>();
            var _Pipe = new SideEffectCapPipe<String, Int64>(new CountPipe<String>());
            _Pipe.SetSourceCollection(_List);

            Assert.IsTrue(_Pipe.MoveNext());
            Assert.AreEqual(0UL, _Pipe.Current);
            Assert.IsFalse(_Pipe.MoveNext());

        }

        #endregion

    }

}
