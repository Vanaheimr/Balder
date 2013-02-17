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

//using NUnit.Framework;

//using de.ahzf.Blueprints.Datastructures;
//using System;

//#endregion

//namespace de.ahzf.Vanaheimr.Balder.UnitTests.Blueprints
//{

//    [TestFixture]
//    public class IdPipeTest
//    {

//        #region testIds()

//        [Test]
//        public void testIds()
//        {

//            var _Graph = TinkerGraphFactory.CreateTinkerGraph();
//            var _Pipe  = new IdPipe<String>();
//            _Pipe.SetSourceCollection(_Graph.GetVertex(new VertexId("1")).OutEdges);
            
//            var _Counter = 0;
//            while (_Pipe.MoveNext())
//            {
//                var _Id = _Pipe.Current;
//                Assert.IsTrue(_Id.Equals(new VertexId("7")) || _Id.Equals(new VertexId("8")) || _Id.Equals(new VertexId("9")));
//                _Counter++;
//            }

//            Assert.AreEqual(3, _Counter);

//        }

//        #endregion

//    }

//}
