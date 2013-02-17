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
    public class OrFilterPipeTest
    {

        #region testOrPipeBasic()

        [Test]
        public void testOrPipeBasic()
        {

            var _Names 			= new List<String>() { "marko", "povel", "peter", "povel", "marko" };
            var _Pipe1 			= new ObjectFilterPipe<String>("marko", ComparisonFilter.NOT_EQUAL);
            var _Pipe2 			= new ObjectFilterPipe<String>("povel", ComparisonFilter.NOT_EQUAL);
            var _ORFilterPipe 	= new OrFilterPipe<String>(new HasNextPipe<String>(_Pipe1), new HasNextPipe<String>(_Pipe2));
            _ORFilterPipe.SetSourceCollection(_Names);

            int _Counter = 0;
            while (_ORFilterPipe.MoveNext())
            {
                var name = _ORFilterPipe.Current;
                Assert.IsTrue(name.Equals("marko") || name.Equals("povel"));
                _Counter++;
            }

            Assert.AreEqual(4, _Counter);

        }

        #endregion

        #region testOrPipeGraph()

        [Test]
        public void testOrPipeGraph()
        {

            // ./outE[@label='created' or @weight > 0.5]

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
                                                          UInt64, Int64, String, String, Object>(v => v != "created");

            var _Pipe2          = new EdgePropertyFilterPipe<UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object>("weight", v => (0.5).Equals(v));

            var _ORFilterPipe   = new OrFilterPipe<IGenericPropertyEdge<UInt64, Int64, String, String, Object,
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
                                                                      UInt64, Int64, String, String, Object>>(_Pipe0, _ORFilterPipe);

            _Pipeline.SetSourceCollection(new List<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object>>() { _Marko, _Peter, _Marko });

            var _Counter = 0;
            while (_Pipeline.MoveNext())
            {
                var _Edge = _Pipeline.Current;
                Assert.IsTrue(_Edge.Id.Equals(8) || _Edge.Id.Equals(9) || _Edge.Id.Equals(12));
                Assert.IsTrue(((Double)_Edge.GetProperty("weight")) > 0.5f || _Edge.Label.Equals("created"));
                _Counter++;
            }

            Assert.AreEqual(5, _Counter);

        }

        #endregion

        #region testAndOrPipeGraph()

        [Test]
        public void testAndOrPipeGraph()
        {

            // ./outE[@label='created' or (@label='knows' and @weight > 0.5)]

            var _Graph      = TinkerGraphFactory.CreateTinkerGraph();
            var _Marko      = _Graph.VertexById(1).AsMutable();

            var _Pipe1      = new OutEdgesPipe   <UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object>();

            var _PipeA      = new EdgeLabelFilterPipe<UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object>(v => v != "created");

            var _PipeB      = new EdgeLabelFilterPipe<UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object>(v => v != "knows");

            var _PipeC      = new EdgePropertyFilterPipe<UInt64, Int64, String, String, Object,
                                                         UInt64, Int64, String, String, Object,
                                                         UInt64, Int64, String, String, Object,
                                                         UInt64, Int64, String, String, Object>("weight", v => (0.5).Equals(v));

            var _PipeD      = new AndFilterPipe<IGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                                     UInt64, Int64, String, String, Object,
                                                                     UInt64, Int64, String, String, Object,
                                                                     UInt64, Int64, String, String, Object>>(

                                  new HasNextPipe<IGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object>>(_PipeB),
                                  new HasNextPipe<IGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object>>(_PipeC));

            var _Pipe2 = new OrFilterPipe<IGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object>>(

                                  new HasNextPipe<IGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object>>(_PipeA),
                                  new HasNextPipe<IGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object>>(_PipeD));

            var _Pipeline 	= new Pipeline<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object>,

                                           IGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object>>(_Pipe1, _Pipe2);

            _Pipeline.SetSourceCollection(new List<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object>>() { _Marko });

            int _Counter = 0;
            while (_Pipeline.MoveNext())
            {
                var _Edge = _Pipeline.Current;
                Assert.IsTrue(_Edge.Id.Equals(8) || _Edge.Id.Equals(9));
                Assert.IsTrue(_Edge.Label.Equals("created") || (((Double)_Edge.GetProperty("weight")) > 0.5 && _Edge.Label.Equals("knows")));
                _Counter++;
            }

            Assert.AreEqual(2, _Counter);

        }

        #endregion

        #region testFutureFilter()

        [Test]
        public void testFutureFilter()
        {

            var _Names 		= new List<String>() { "marko", "peter", "josh", "marko", "jake", "marko", "marko" };
            var _PipeA 		= new CharacterCountPipe();
            var _PipeB 		= new ObjectFilterPipe<UInt64>(4, ComparisonFilter.EQUAL);
            var _Pipe1 		= new OrFilterPipe<String>(new HasNextPipe<String>(new Pipeline<String, UInt64>(_PipeA, _PipeB)));
            var _Pipeline 	= new Pipeline<String, String>(_Pipe1);
            _Pipeline.SetSourceCollection(_Names);

            int _Counter = 0;
            while (_Pipeline.MoveNext())
            {
                var name = _Pipeline.Current;
                _Counter++;
                Assert.IsTrue((name.Equals("marko") || name.Equals("peter")) && !name.Equals("josh") && !name.Equals("jake"));
            }

            Assert.AreEqual(5, _Counter);

        }

        #endregion

        #region testFutureFilterGraph()

        [Test]
        public void testFutureFilterGraph()
        {

            // ./outE[@label='created']/inV[@name='lop']/../../@name

            var _Graph      = TinkerGraphFactory.CreateTinkerGraph();
            var _Marko      = _Graph.VertexById(1).AsMutable();

            var _PipeA      = new OutEdgesPipe   <UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object>();

            var _PipeB      = new EdgeLabelFilterPipe<UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object>(v => v != "created");

            var _PipeC      = new InVertexPipe   <UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object>();

            var _PipeD      = new VertexPropertyFilterPipe<UInt64, Int64, String, String, Object,
                                                           UInt64, Int64, String, String, Object,
                                                           UInt64, Int64, String, String, Object,
                                                           UInt64, Int64, String, String, Object>("name", v => v.ToString() == "lop");

            var _Pipe1      = new AndFilterPipe<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object>>(

                                  new HasNextPipe<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                         UInt64, Int64, String, String, Object,
                                                                         UInt64, Int64, String, String, Object,
                                                                         UInt64, Int64, String, String, Object>>(

                                  new Pipeline<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object>,

                                               IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object>>(_PipeA, _PipeB, _PipeC, _PipeD)));

            var _Pipe2      = new PropertyPipe<String, Object>(Keys: "name");

            var _Pipeline   = new Pipeline<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object>, String>(_Pipe1, _Pipe2);

            _Pipeline.SetSourceCollection(new List<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object>>() { _Marko });

            var _Counter = 0;
            while (_Pipeline.MoveNext())
            {
                var name = _Pipeline.Current;
                Assert.AreEqual("marko", name);
                _Counter++;
            }
            
            Assert.AreEqual(1, _Counter);

        }

        #endregion

        #region testComplexFutureFilterGraph()

        [Test]
        public void testComplexFutureFilterGraph()
        {

            // ./outE[@weight > 0.5]/inV/../../outE/inV/@name

            var _Graph      = TinkerGraphFactory.CreateTinkerGraph();
            var _Marko      = _Graph.VertexById(1).AsMutable();

            var _PipeA      = new OutEdgesPipe<UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object>();
            
            var _PipeB      = new EdgePropertyFilterPipe<UInt64, Int64, String, String, Object,
                                                         UInt64, Int64, String, String, Object,
                                                         UInt64, Int64, String, String, Object,
                                                         UInt64, Int64, String, String, Object>("weight", v => (0.5).Equals(v));

            var _PipeC      = new InVertexPipe<UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object>();

            var _Pipe1      = new AndFilterPipe<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object,
                                                                       UInt64, Int64, String, String, Object>>(

                                  new HasNextPipe<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                         UInt64, Int64, String, String, Object,
                                                                         UInt64, Int64, String, String, Object,
                                                                         UInt64, Int64, String, String, Object>>(

                                      new Pipeline<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object>,

                                                   IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object>>(_PipeA, _PipeB, _PipeC)));

            var _Pipe2      = new OutEdgesPipe<UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object>();

            var _Pipe3      = new InVertexPipe<UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object>();

            var _Pipe4      = new PropertyPipe<String, Object>(Keys: "name");

            var _Pipeline   = new Pipeline<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object>, String>(_Pipe1, _Pipe2, _Pipe3, _Pipe4);

            _Pipeline.SetSourceCollection(new List<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object>>() { _Marko });

            var _Counter = 0;
            while (_Pipeline.MoveNext())
            {
                var _Name = _Pipeline.Current;
                Assert.IsTrue(_Name.Equals("vadas") || _Name.Equals("lop") || _Name.Equals("josh"));
                _Counter++;
            }

            Assert.AreEqual(3, _Counter);

        }

        #endregion

        #region testComplexTwoFutureFilterGraph()

        [Test]
        public void testComplexTwoFutureFilterGraph()
        {

            // ./outE/inV/../../outE/../outE/inV/@name

            var _Graph      = TinkerGraphFactory.CreateTinkerGraph();
            var _Marko      = _Graph.VertexById(1).AsMutable();

            var _PipeA      = new OutEdgesPipe<UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object>();

            var _PipeB      = new InVertexPipe<UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object>();

            var _Pipe1      = new OrFilterPipe<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object>>(

                                  new HasNextPipe<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                         UInt64, Int64, String, String, Object,
                                                                         UInt64, Int64, String, String, Object,
                                                                         UInt64, Int64, String, String, Object>>(

                                      new Pipeline<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object>,

                                                   IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object>>(_PipeA, _PipeB)));

            var _PipeC      = new OutEdgesPipe<UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object>();

            var _Pipe2      = new OrFilterPipe<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object>>(

                                  new HasNextPipe<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                         UInt64, Int64, String, String, Object,
                                                                         UInt64, Int64, String, String, Object,
                                                                         UInt64, Int64, String, String, Object>>(_PipeC));

            var _Pipe3      = new OutEdgesPipe<UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object>();

            var _Pipe4      = new InVertexPipe<UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object>();

            var _Pipe5      = new PropertyPipe<String, Object>(Keys: "name");

            var _Pipeline   = new Pipeline<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object>, String>(_Pipe1, _Pipe2, _Pipe3, _Pipe4, _Pipe5);

            _Pipeline.SetSourceCollection(new List<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object>>() { _Marko });

            var _Counter = 0;
            while (_Pipeline.MoveNext())
            {
                var _Name = _Pipeline.Current;
                Assert.IsTrue(_Name.Equals("vadas") || _Name.Equals("lop") || _Name.Equals("josh"));
                _Counter++;
            }

            Assert.AreEqual(3, _Counter);

        }

        #endregion

    }

}
