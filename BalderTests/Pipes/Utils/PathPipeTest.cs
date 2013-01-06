/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Balder <http://www.github.com/Vanaheimr/Balder>
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

using de.ahzf.Illias.Commons;
using de.ahzf.Vanaheimr.Styx;
using de.ahzf.Vanaheimr.Blueprints.UnitTests;
using de.ahzf.Vanaheimr.Blueprints;

#endregion

namespace de.ahzf.Vanaheimr.Balder.UnitTests.util
{

    [TestFixture]
    public class PathPipeTest
    {

        #region testPipeBasic()

        [Test]
        public void testPipeBasic()
        {

            var _Graph    = TinkerGraphFactory.CreateTinkerGraph();

            var _Marko    = _Graph.VertexById(1).AsMutable();

            var _Pipe1    = new OutEdgesPipe<UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object>();

            var _Pipe2    = new InVertexPipe<UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object>();

            var _Pipe3 = new PathPipe<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object>>();

            var _Pipeline = new Pipeline<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object>,

                                         IEnumerable<Object>>(_Pipe1, _Pipe2, _Pipe3);

            _Pipeline.SetSource(new SingleEnumerator<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                            UInt64, Int64, String, String, Object,
                                                                            UInt64, Int64, String, String, Object,
                                                                            UInt64, Int64, String, String, Object>>(_Marko));

            foreach (var _Path in _Pipeline)
            {
                Assert.AreEqual(_Marko, _Path.ElementAt(0));
                Assert.IsTrue(_Path.ElementAt(1) is IGenericPropertyEdge  <UInt64, Int64, String, String, Object,
                                                                           UInt64, Int64, String, String, Object,
                                                                           UInt64, Int64, String, String, Object,
                                                                           UInt64, Int64, String, String, Object>);
                Assert.IsTrue(_Path.ElementAt(2) is IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                           UInt64, Int64, String, String, Object,
                                                                           UInt64, Int64, String, String, Object,
                                                                           UInt64, Int64, String, String, Object>);
            }

        }

        #endregion

    }

}
