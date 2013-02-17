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

namespace de.ahzf.Vanaheimr.Balder.UnitTests.FilterPipes
{

    [TestFixture]
    public class AndFilterPipeTest
    {

        #region testAndPipeBasic()

        [Test]
        public void testAndPipeBasic()
        {

            var _Names = new List<String>() { "marko", "povel", "peter", "povel", "marko" };
            var _Pipe1 = new ObjectFilterPipe<String>("marko", ComparisonFilter.NOT_EQUAL);
            var _Pipe2 = new ObjectFilterPipe<String>("povel", ComparisonFilter.NOT_EQUAL);
            var _AndFilterPipe = new AndFilterPipe<String>(new HasNextPipe<String>(_Pipe1), new HasNextPipe<String>(_Pipe2));
            _AndFilterPipe.SetSourceCollection(_Names);

            var _Counter = 0;
            while (_AndFilterPipe.MoveNext())
                _Counter++;

            Assert.AreEqual(0, _Counter);

        }

        #endregion

        #region testAndPipeBasic2()

        [Test]
        public void testAndPipeBasic2()
        {

            var _Names          = new List<String>() { "marko", "povel", "peter", "povel", "marko" };
            var _Pipe1          = new ObjectFilterPipe<String>("marko", ComparisonFilter.NOT_EQUAL);
            var _Pipe2          = new ObjectFilterPipe<String>("marko", ComparisonFilter.NOT_EQUAL);
            var _AndFilterPipe  = new AndFilterPipe<String>(new HasNextPipe<String>(_Pipe1), new HasNextPipe<String>(_Pipe2));
            _AndFilterPipe.SetSourceCollection(_Names);

            var _Counter = 0;
            while (_AndFilterPipe.MoveNext())
                _Counter++;

            Assert.AreEqual(2, _Counter);

        }

        #endregion

        #region testAndPipeGraph()

        [Test]
        public void testAndPipeGraph()
        {

            var _Graph          = TinkerGraphFactory.CreateTinkerGraph();
            var _Marko          = _Graph.VertexById(1).AsMutable();
            var _Peter          = _Graph.VertexById(6).AsMutable();

            var _Pipe0          = new OutEdgesPipe<UInt64, Int64, String, String, Object,
                                                   UInt64, Int64, String, String, Object,
                                                   UInt64, Int64, String, String, Object,
                                                   UInt64, Int64, String, String, Object>();

            var _Pipe1          = new EdgeLabelFilterPipe<UInt64, Int64, String, String, Object,
                                                          UInt64, Int64, String, String, Object,
                                                          UInt64, Int64, String, String, Object,
                                                          UInt64, Int64, String, String, Object>(v => v != "knows");

            var _Pipe2          = new EdgePropertyFilterPipe<UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object>("weight", v => (0.5).Equals(v));

            var _AndFilterPipe	= new AndFilterPipe<IGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                                         UInt64, Int64, String, String, Object,
                                                                         UInt64, Int64, String, String, Object,
                                                                         UInt64, Int64, String, String, Object>>(

                                      new HasNextPipe<IGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                                           UInt64, Int64, String, String, Object,
                                                                           UInt64, Int64, String, String, Object,
                                                                           UInt64, Int64, String, String, Object>>(_Pipe1),

                                      new HasNextPipe<IGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                                           UInt64, Int64, String, String, Object,
                                                                           UInt64, Int64, String, String, Object,
                                                                           UInt64, Int64, String, String, Object>>(_Pipe2));

            var _Pipeline = new Pipeline<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object>,

                                               IGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                                    UInt64, Int64, String, String, Object,
                                                                    UInt64, Int64, String, String, Object,
                                                                    UInt64, Int64, String, String, Object>>(_Pipe0, _AndFilterPipe);

            _Pipeline.SetSourceCollection(new List<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object>>() { _Marko, _Peter, _Marko });

            var _Counter = 0;
            while (_Pipeline.MoveNext())
            {
                var _Edge = _Pipeline.Current;
                Assert.IsTrue(_Edge.Id.Equals(8));
                Assert.IsTrue(((Double)_Edge.GetProperty("weight")) > 0.5f && _Edge.Label.Equals("knows"));
                _Counter++;
            }

            Assert.AreEqual(2, _Counter);

        }

        #endregion

    }

}
