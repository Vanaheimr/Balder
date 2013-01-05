///*
// * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <achim@graph-database.org>
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

//#endregion

//namespace de.ahzf.Vanaheimr.Balder.UnitTests.Blueprints
//{

//    [TestFixture]
//    public class IdVertexPipeTest
//    {

//        #region testIdVertexPipeGraph()

//        [Test]
//        public void testIdVertexPipeGraph()
//        {

//            var _Graph = TinkerGraphFactory.CreateTinkerGraph();
//            var _Ids   = new List<VertexId>() { new VertexId("1"), new VertexId("6"), new VertexId("5") };
//            var _Pipe  = new IdVertexPipe<VertexId>(_Graph);
//            _Pipe.SetSourceCollection(_Ids);

//            var _Counter = 0;
//            while (_Pipe.MoveNext())
//            {

//                var _Vertex = _Pipe.Current;
                
//                if (_Counter == 0)
//                {
//                    Assert.AreEqual(new VertexId("1"), _Vertex.Id);
//                    Assert.AreEqual("marko", _Vertex.GetProperty("name"));
//                }
                
//                else if (_Counter == 1)
//                {
//                    Assert.AreEqual(new VertexId("6"), _Vertex.Id);
//                    Assert.AreEqual("peter", _Vertex.GetProperty("name"));
//                }
                
//                else if (_Counter == 2)
//                {
//                    Assert.AreEqual(new VertexId("5"), _Vertex.Id);
//                    Assert.AreEqual("ripple", _Vertex.GetProperty("name"));
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
