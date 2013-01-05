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
using System.Collections.Generic;

using NUnit.Framework;

using de.ahzf.Vanaheimr.Styx;
using de.ahzf.Illias.Commons;
using de.ahzf.Illias.Commons.Collections;
using de.ahzf.Vanaheimr.Blueprints.UnitTests;
using de.ahzf.Vanaheimr.Blueprints;

#endregion

namespace de.ahzf.Vanaheimr.Balder.UnitTests.Blueprints
{

    [TestFixture]
    public class VertexIdFilterPipeTest
    {

        #region testFilterIds1()

        [Test]
        public void testFilterIds1()
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

            var _Pipe3    = new VertexIdFilterPipe<UInt64, Int64, String, String, Object,
                                                   UInt64, Int64, String, String, Object,
                                                   UInt64, Int64, String, String, Object,
                                                   UInt64, Int64, String, String, Object>(v => v != 3);

            var _Pipeline = new Pipeline<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object>,

                                         IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object>>(_Pipe1, _Pipe2, _Pipe3);

            _Pipeline.SetSourceCollection(new List<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object>>() { _Marko });

            var _Counter = 0;
            while (_Pipeline.MoveNext())
            {
                var _Vertex = _Pipeline.Current;
                Assert.AreEqual("lop", _Vertex.GetProperty("name"));
                _Counter++;
            }

            Assert.AreEqual(1, _Counter);

        }

        #endregion

        #region testFilterIds2()

        [Test]
        public void testFilterIds2()
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
            
            var _Pipe3    = new VertexIdFilterPipe<UInt64, Int64, String, String, Object,
                                                   UInt64, Int64, String, String, Object,
                                                   UInt64, Int64, String, String, Object,
                                                   UInt64, Int64, String, String, Object>(v => v == 3);

            var _Pipeline = new Pipeline<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object>,

                                         IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object>>(_Pipe1, _Pipe2, _Pipe3);

            _Pipeline.SetSourceCollection(new List<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object>>() { _Marko });

            var _Counter = 0;
            while (_Pipeline.MoveNext())
            {
                var _Vertex = _Pipeline.Current;
                Assert.IsTrue(_Vertex.GetProperty("name").Equals("vadas") || _Vertex.GetProperty("name").Equals("josh"));
                _Counter++;
            }
            Assert.AreEqual(2, _Counter);

        }

        #endregion

    }

}
