///*
// * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
// * This file is part of Balder <http://www.github.com/Vanaheimr/Balder>
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
//using System.Collections.Generic;

//using NUnit.Framework;

//using de.ahzf.Blueprints.Datastructures;
//using de.ahzf.Blueprints;

//#endregion

//namespace de.ahzf.Vanaheimr.Balder.UnitTests.Blueprints
//{

//    [TestFixture]
//    public class IdEdgePipeTest
//    {

//        #region testIdEdgePipeGraph()

//        [Test]
//        public void testIdEdgePipeGraph()
//        {

//            var _Graph = TinkerGraphFactory.CreateTinkerGraph();
//            var _IDs  = new List<EdgeId>() { new EdgeId("9"), new EdgeId("11"), new EdgeId("12") };
//            var _Pipe = new IdEdgePipe<EdgeId>(_Graph);
//            _Pipe.SetSourceCollection(_IDs);

//            var _Counter = 0;
//            while (_Pipe.MoveNext())
//            {
                
//                var _Edge = _Pipe.Current;

//                if (_Counter == 0)
//                {
//                    Assert.AreEqual(new EdgeId("9"), _Edge.Id);
//                    Assert.AreEqual(0.4, _Edge.GetProperty("weight"));
//                }

//                else if (_Counter == 1)
//                {
//                    Assert.AreEqual(new EdgeId("11"), _Edge.Id);
//                    Assert.AreEqual(_Graph.GetVertex(new VertexId("3")), _Edge.InVertex);
//                }

//                else if (_Counter == 2)
//                {
//                    Assert.AreEqual(new EdgeId("12"), _Edge.Id);
//                    Assert.AreEqual("created", _Edge.Label);
//                }

//                else
//                    throw new Exception("Illegal state.");

//                _Counter++;

//            }

//            Assert.AreEqual(3, _Counter);

//        }

//        #endregion

//    }

//}
