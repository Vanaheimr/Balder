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

using NUnit.Framework;
using de.ahzf.Vanaheimr.Styx;
using de.ahzf.Illias.Commons;
using de.ahzf.Vanaheimr.Balder.UnitTests;
using de.ahzf.Vanaheimr.Blueprints;

#endregion

namespace de.ahzf.Vanaheimr.Balder.UnitTests.Blueprints
{

    [TestFixture]
    public class LabelFilterPipeTest
    {

        #region testFilterLabels()

        [Test]
        public void testFilterLabels()
        {

            var _Graph = TinkerGraphFactory.CreateTinkerGraph();

            var _Marko = _Graph.VertexById(1);

            var _LFP   = new EdgeLabelFilterPipe<UInt64, Int64, String, String, Object,
                                                 UInt64, Int64, String, String, Object,
                                                 UInt64, Int64, String, String, Object,
                                                 UInt64, Int64, String, String, Object>(v => v == "knows");

            _LFP.SetSourceCollection(_Marko.OutEdges());

            var _Counter = 0;
            while (_LFP.MoveNext())
            {
                var _E = _LFP.Current;
                Assert.AreEqual(_Marko, _E.OutVertex);
                Assert.IsTrue(_E.InVertex.Id.Equals(new VertexId("2")) || _E.InVertex.Id.Equals(new VertexId("4")));
                _Counter++;
            }

            Assert.AreEqual(2, _Counter);


            _LFP = new EdgeLabelFilterPipe<UInt64, Int64, String, String, Object,
                                           UInt64, Int64, String, String, Object,
                                           UInt64, Int64, String, String, Object,
                                           UInt64, Int64, String, String, Object>(v => v == "knows");

            _LFP.SetSourceCollection(_Marko.OutEdges());

            _Counter = 0;
            while (_LFP.MoveNext())
            {
                var _E = _LFP.Current;
                Assert.AreEqual(_Marko, _E.OutVertex);
                Assert.IsTrue(_E.InVertex.Id.Equals(new VertexId("3")));
                _Counter++;
            }

            Assert.AreEqual(1, _Counter);

        }

        #endregion

    }

}
