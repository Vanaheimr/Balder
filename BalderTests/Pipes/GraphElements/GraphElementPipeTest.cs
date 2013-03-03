/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
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
using de.ahzf.Vanaheimr.Balder.UnitTests;
using de.ahzf.Vanaheimr.Blueprints;

#endregion

namespace de.ahzf.Vanaheimr.Balder.UnitTests.Blueprints
{

    [TestFixture]
    public class GraphElementPipeTest
    {

        #region testVertexIterator()

        [Test]
        public void testVertexIterator()
        {

            var _Graph = TinkerGraphFactory.CreateTinkerGraph();
            var _Pipe = new AllVerticesPipe<UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object>();

            _Pipe.SetSource(new SingleEnumerator<IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object>>(_Graph));
            
            var _Counter = 0;
            var _Vertices = new HashSet<IReadOnlyGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object>>();

            while (_Pipe.MoveNext())
            {
                _Counter++;
                var _Vertex = _Pipe.Current;
                _Vertices.Add(_Vertex);
            }

            Assert.AreEqual(6, _Counter);
            Assert.AreEqual(6, _Vertices.Count);

        }

        #endregion

        #region testEdgeIterator()

        [Test]
        public void testEdgeIterator()
        {

            var _Graph = TinkerGraphFactory.CreateTinkerGraph();

            var _Pipe = new AllEdgesPipe<UInt64, Int64, String, String, Object,
                                         UInt64, Int64, String, String, Object,
                                         UInt64, Int64, String, String, Object,
                                         UInt64, Int64, String, String, Object>();

            _Pipe.SetSource(new SingleEnumerator<IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object>>(_Graph));

            var _Counter = 0;
            var _Edges = new HashSet<IReadOnlyGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object>>();

            while (_Pipe.MoveNext())
            {
                _Counter++;
                var _Edge = _Pipe.Current;
                _Edges.Add(_Edge);
            }

            Assert.AreEqual(6, _Counter);
            Assert.AreEqual(6, _Edges.Count);

        }

        #endregion

        #region testEdgeIteratorThreeGraphs()

        [Test]
        public void testEdgeIteratorThreeGraphs()
        {

            var _Graph = TinkerGraphFactory.CreateTinkerGraph();

            var _Pipe = new AllEdgesPipe<UInt64, Int64, String, String, Object,
                                         UInt64, Int64, String, String, Object,
                                         UInt64, Int64, String, String, Object,
                                         UInt64, Int64, String, String, Object>();

            _Pipe.SetSourceCollection(new List<IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                                     UInt64, Int64, String, String, Object,
                                                                     UInt64, Int64, String, String, Object,
                                                                     UInt64, Int64, String, String, Object>>() { _Graph, _Graph, _Graph });
            
            var _Counter = 0;
            var _Edges = new HashSet<IReadOnlyGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object>>();

            while (_Pipe.MoveNext())
            {
                _Counter++;
                var _Edge = _Pipe.Current;
                _Edges.Add(_Edge);
            }

            Assert.AreEqual(18, _Counter);
            Assert.AreEqual(6, _Edges.Count);

        }

        #endregion

    }

}
