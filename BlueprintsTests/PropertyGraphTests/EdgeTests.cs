///*
// * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <code@ahzf.de>
// * This file is part of Pipes.NET
// *
// * Licensed under the Apache License, Version 2.0 (the "License");
// * you may not use this file except in compliance with the License.
// * You may obtain a copy of the License at
// *
// *     http://www.apache.org/licenses/LICENSE-2.0
// *
// * Unless required by applicable law or agreed to in writing, software
// * distributed under the License is distributed on an "AS IS" BASIS,
// * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// * See the License for the specific language governing permissions and
// * limitations under the License.
// */

//#region Usings

//using System;
//using System.Reflection;
//using System.Collections.Generic;

//using NUnit.Framework;

//using de.ahzf.Blueprints.Datastructures;
//using de.ahzf.Blueprints.InMemoryGraph;

//#endregion

//namespace de.ahzf.Blueprints.UnitTests.Basics
//{

//    [TestFixture]
//    public class EdgeTests
//    {

//        #region testEdgeInitAndEquality()

//        [Test]
//        public void testEdgeInitAndEquality()
//        {

//            var _Graph = new de.ahzf.Blueprints.InMemoryGraph.InMemoryGraph() as IPropertyGraph;

//            var _v1 = _Graph.AddVertex(new VertexId("v1"));
//            var _v2 = _Graph.AddVertex(new VertexId("v2"));
//            var _v3 = _Graph.AddVertex(new VertexId("v3"));

//            var _Type = typeof(de.ahzf.Blueprints.InMemoryGraph.Edge).
//                         GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
//                                        null,
//                                        new Type[] {
//                                            typeof(IPropertyGraph),
//                                            typeof(IVertex),
//                                            typeof(IVertex),
//                                            typeof(EdgeId),
//                                            typeof(String),
//                                            typeof(Action<IEdge>)
//                                        },
//                                        null);

//            var a = new EdgeId("e1");
//            var b = new EdgeId("e2");
//            var c = new EdgeId("e1");

//            var _e1 = _Type.Invoke(new Object[] { new de.ahzf.Blueprints.InMemoryGraph.InMemoryGraph(), _v1, _v2, a, "label1", null }) as Edge;
//            var _e2 = _Type.Invoke(new Object[] { new de.ahzf.Blueprints.InMemoryGraph.InMemoryGraph(), _v2, _v3, b, "label2", null }) as Edge;
//            var _e3 = _Type.Invoke(new Object[] { new de.ahzf.Blueprints.InMemoryGraph.InMemoryGraph(), _v1, _v2, c, "label3", null }) as Edge;

//            Assert.AreEqual(_e1.Id, _e1.Id);
//            Assert.AreEqual(_e2.Id, _e2.Id);
//            Assert.AreEqual(_e3.Id, _e3.Id);

//            var b1 = _e1.Equals(_e1);

//            Assert.IsTrue(_e1.Equals(_e1));
//            Assert.IsTrue(_e2.Equals(_e2));
//            Assert.IsTrue(_e3.Equals(_e3));

//            Assert.IsTrue(_e1 != _e2);
//            Assert.IsTrue(!_e1.Equals(_e2));

//            Assert.IsTrue(_e1.Equals(_e3));
//            Assert.IsTrue(_e1 == _e3);
//            //Assert.AreEqual(_e1, _e3);


//            var _HashSet = new HashSet<IEdge>();
//            Assert.AreEqual(0, _HashSet.Count);

//            _HashSet.Add(_e1);
//            Assert.AreEqual(1, _HashSet.Count);

//            _HashSet.Add(_e2);
//            Assert.AreEqual(2, _HashSet.Count);

//            _HashSet.Add(_e3);
//            Assert.AreEqual(2, _HashSet.Count);

//        }

//        #endregion

//        #region testIEdgeInitAndEquality()

//        [Test]
//        public void testIEdgeInitAndEquality()
//        {

//            var _Graph = new de.ahzf.Blueprints.InMemoryGraph.InMemoryGraph() as IPropertyGraph;

//            var _v1 = _Graph.AddVertex(new VertexId("v1"));
//            var _v2 = _Graph.AddVertex(new VertexId("v2"));
//            var _v3 = _Graph.AddVertex(new VertexId("v3"));

//            var _Type = typeof(de.ahzf.Blueprints.InMemoryGraph.Edge).
//                         GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
//                                        null,
//                                        new Type[] {
//                                            typeof(IPropertyGraph),
//                                            typeof(IVertex),
//                                            typeof(IVertex),
//                                            typeof(EdgeId),
//                                            typeof(String),
//                                            typeof(Action<IEdge>)
//                                        },
//                                        null);

//            var a = new EdgeId("e1");
//            var b = new EdgeId("e2");
//            var c = new EdgeId("e1");

//            var _e1 = _Type.Invoke(new Object[] { new de.ahzf.Blueprints.InMemoryGraph.InMemoryGraph(), _v1, _v2, a, "label1", null }) as IEdge;
//            var _e2 = _Type.Invoke(new Object[] { new de.ahzf.Blueprints.InMemoryGraph.InMemoryGraph(), _v2, _v3, b, "label2", null }) as IEdge;
//            var _e3 = _Type.Invoke(new Object[] { new de.ahzf.Blueprints.InMemoryGraph.InMemoryGraph(), _v1, _v2, c, "label3", null }) as IEdge;

//            Assert.AreEqual(_e1.Id, _e1.Id);
//            Assert.AreEqual(_e2.Id, _e2.Id);
//            Assert.AreEqual(_e3.Id, _e3.Id);

//            var b1 = _e1.Equals(_e1);

//            Assert.IsTrue(_e1.Equals(_e1));
//            Assert.IsTrue(_e2.Equals(_e2));
//            Assert.IsTrue(_e3.Equals(_e3));

//            //Assert.IsTrue(_e1 != _e2);
//            Assert.IsTrue(!_e1.Equals(_e2));

//            Assert.IsTrue(_e1.Equals(_e3));
//            //Assert.IsTrue(_e1 == _e3);
//            //Assert.AreEqual(_e1, _e3);


//            var _HashSet = new HashSet<IEdge>();
//            Assert.AreEqual(0, _HashSet.Count);

//            _HashSet.Add(_e1);
//            Assert.AreEqual(1, _HashSet.Count);

//            _HashSet.Add(_e2);
//            Assert.AreEqual(2, _HashSet.Count);

//            _HashSet.Add(_e3);
//            Assert.AreEqual(2, _HashSet.Count);

//        }

//        #endregion

//    }

//}
