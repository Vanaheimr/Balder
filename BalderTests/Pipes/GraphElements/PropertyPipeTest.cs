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
    public class PropertyPipeTest
    {

        #region testSingleProperty()

        [Test]
        public void testSingleProperty()
        {

            var _Graph = TinkerGraphFactory.CreateTinkerGraph();
            var _Marko = _Graph.VertexById(1).AsMutable();

            var _PPipe = new PropertyPipe<String, Object>(Keys: "name");

            _PPipe.SetSource(new List<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object>>() { _Marko }.GetEnumerator());

            var _Counter = 0;
            while (_PPipe.MoveNext())
            {
                var name = _PPipe.Current;
                Assert.AreEqual("marko", name);
                _Counter++;
            }

        }

        #endregion

        #region testMultiProperty()

        [Test]
        public void testMultiProperty()
        {

            var _Graph    = TinkerGraphFactory.CreateTinkerGraph();
            var _Marko    = _Graph.VertexById(1).AsMutable();

            var _EVP      = new InVertexPipe<UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object>();

            var _PPipe    = new PropertyPipe<String, Object>(Keys: "name");

            var _Pipeline = new Pipeline<IReadOnlyGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object>, String>(_EVP, _PPipe);

            _Pipeline.SetSourceCollection(_Marko.OutEdges());

            var _Counter = 0;
            while (_Pipeline.MoveNext())
            {
                var _Name = _Pipeline.Current;
                Assert.IsTrue(_Name.Equals("vadas") || _Name.Equals("josh") || _Name.Equals("lop"));
                _Counter++;
            }

            Assert.AreEqual(3, _Counter);

        }

        #endregion

        #region testListProperty()

        [Test]
        public void testListProperty()
        {

            var _Graph    = TinkerGraphFactory.CreateTinkerGraph();
            var _Marko    = _Graph.VertexById(1).AsMutable();
            var _Vadas    = _Graph.VertexById(2).AsMutable();

            var _Pipe = new PropertyPipe<String, Object>(Keys: "name");

            var _Pipeline = new Pipeline<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object>, String>(_Pipe);

            _Pipeline.SetSource(new List<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object>>() { _Marko, _Vadas }.GetEnumerator());

            var _Counter = 0;
            while (_Pipeline.MoveNext())
            {
                var _Name = _Pipeline.Current;
                Assert.IsTrue(_Name.Equals("vadas") || _Name.Equals("marko"));
                _Counter++;
            }

            Assert.AreEqual(2, _Counter);

        }

        #endregion

    }

}
