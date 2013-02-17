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
using de.ahzf.Vanaheimr.Blueprints.UnitTests;
using de.ahzf.Vanaheimr.Blueprints;

#endregion

namespace de.ahzf.Vanaheimr.Balder.UnitTests.Blueprints
{

    [TestFixture]
    public class VertexEdgePipeTest
    {

        #region testOutGoingEdges()

        [Test]
        public void testOutGoingEdges()
        {

            var _Graph = TinkerGraphFactory.CreateTinkerGraph();
            var _Marko = _Graph.VertexById(1).AsMutable();

            var _VSF   = new OutEdgesPipe<UInt64, Int64, String, String, Object,
                                          UInt64, Int64, String, String, Object,
                                          UInt64, Int64, String, String, Object,
                                          UInt64, Int64, String, String, Object>();

            _VSF.SetSource(new List<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                           UInt64, Int64, String, String, Object,
                                                           UInt64, Int64, String, String, Object,
                                                           UInt64, Int64, String, String, Object>>() { _Marko }.GetEnumerator());

            var _Counter = 0;
            while (_VSF.MoveNext())
            {
                var _E = _VSF.Current;
                Assert.AreEqual(_Marko, _E.OutVertex);
                Assert.IsTrue(_E.InVertex.Id.Equals(2) || _E.InVertex.Id.Equals(3) || _E.InVertex.Id.Equals(4));
                _Counter++;
            }

            Assert.AreEqual(3, _Counter);



            var _Josh = _Graph.VertexById(4).AsMutable();

            _VSF = new OutEdgesPipe<UInt64, Int64, String, String, Object,
                                    UInt64, Int64, String, String, Object,
                                    UInt64, Int64, String, String, Object,
                                    UInt64, Int64, String, String, Object>();

            _VSF.SetSource(new List<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                           UInt64, Int64, String, String, Object,
                                                           UInt64, Int64, String, String, Object,
                                                           UInt64, Int64, String, String, Object>>() { _Josh }.GetEnumerator());

            _Counter = 0;
            while (_VSF.MoveNext())
            {
                var e = _VSF.Current;
                Assert.AreEqual(_Josh, e.OutVertex);
                Assert.IsTrue(e.InVertex.Id.Equals(5) || e.InVertex.Id.Equals(3));
                _Counter++;
            }

            Assert.AreEqual(2, _Counter);



            var _Lop = _Graph.VertexById(3).AsMutable();

            _VSF = new OutEdgesPipe<UInt64, Int64, String, String, Object,
                                    UInt64, Int64, String, String, Object,
                                    UInt64, Int64, String, String, Object,
                                    UInt64, Int64, String, String, Object>();

            _VSF.SetSource(new List<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                           UInt64, Int64, String, String, Object,
                                                           UInt64, Int64, String, String, Object,
                                                           UInt64, Int64, String, String, Object>>() { _Lop }.GetEnumerator());

            _Counter = 0;
            while (_VSF.MoveNext())
            {
                _Counter++;
            }

            Assert.AreEqual(0, _Counter);

        }

        #endregion

        #region testInEdges()

        [Test]
        public void testInEdges()
        {

            var _Graph = TinkerGraphFactory.CreateTinkerGraph();
            var _Josh  = _Graph.VertexById(4).AsMutable();

            var _Pipe = new InEdgesPipe<UInt64, Int64, String, String, Object,
                                        UInt64, Int64, String, String, Object,
                                        UInt64, Int64, String, String, Object,
                                        UInt64, Int64, String, String, Object>();

            _Pipe.SetSource(new SingleEnumerator<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object>>(_Josh));

            var _Counter = 0;
            while (_Pipe.MoveNext())
            {
                _Counter++;
                var edge = _Pipe.Current;
                Assert.AreEqual(8, edge.Id);
            }
            
            Assert.AreEqual(1, _Counter);

        }

        #endregion

        #region testBothEdges()

        [Test]
        public void testBothEdges()
        {

            var _Graph = TinkerGraphFactory.CreateTinkerGraph();
            var _Josh  = _Graph.VertexById(4).AsMutable();

            var _Pipe = new BothEdgesPipe<UInt64, Int64, String, String, Object,
                                          UInt64, Int64, String, String, Object,
                                          UInt64, Int64, String, String, Object,
                                          UInt64, Int64, String, String, Object>();

            _Pipe.SetSource(new SingleEnumerator<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object>>(_Josh));

            var _Counter = 0;
            while (_Pipe.MoveNext())
            {
                _Counter++;
                var edge = _Pipe.Current;
                Assert.IsTrue(edge.Id.Equals(8) || edge.Id.Equals(10) || edge.Id.Equals(11));
            }

            Assert.AreEqual(3, _Counter);

        }

        #endregion

        #region testBigGraphWithNoEdges()

        [Test]
        public void testBigGraphWithNoEdges()
        {

            var _Graph = TinkerGraphFactory.CreateTinkerGraph();

            for (var i = 0; i < 100000; i++)
                _Graph.AddVertex();

            var _Vertices = new AllVerticesPipe<UInt64, Int64, String, String, Object,
                                                UInt64, Int64, String, String, Object,
                                                UInt64, Int64, String, String, Object,
                                                UInt64, Int64, String, String, Object>();

            _Vertices.SetSource(new SingleEnumerator<IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                                           UInt64, Int64, String, String, Object,
                                                                           UInt64, Int64, String, String, Object,
                                                                           UInt64, Int64, String, String, Object>>(_Graph));

            var _OutEdges = new OutEdgesPipe<UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object>();

            _OutEdges.SetSource(_Vertices);

            var _Counter = 0;
            while (_OutEdges.MoveNext())
                _Counter++;
            
            Assert.AreEqual(0, _Counter);

        }

        #endregion

    }

}
