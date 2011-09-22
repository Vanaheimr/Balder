/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET <http://www.github.com/ahzf/Blueprints.NET>
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
using System.Linq;
using System.Collections.Generic;

using NUnit.Framework;
using de.ahzf.Blueprints.PropertyGraphs.InMemory;
using de.ahzf.Blueprints.PropertyGraphs;
using de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable;

#endregion

namespace de.ahzf.Blueprints.UnitTests.PropertyGraphs.InMemory
{

    /// <summary>
    /// Unit tests for the PropertyVertex class.
    /// </summary>
    [TestFixture]
    public class PropertyVertexTests
    {

        #region AddVertexEmptyAddMethodTest()

        /// <summary>
        /// A test for the empty PropertyGraph.AddVertex() method.
        /// </summary>
        [Test]
        public void AddVertexEmptyAddMethodTest()
        {
            
            var _Graph  = new PropertyGraph();
            Assert.AreEqual(0, _Graph.NumberOfVertices());

            var _Vertex = _Graph.AddVertex();
            Assert.AreEqual(1, _Graph.NumberOfVertices());
            Assert.AreEqual(0, _Vertex.OutEdges().Count());
            Assert.AreEqual(0, _Vertex.InEdges().Count());

            Assert.NotNull(_Vertex.Graph);
            Assert.AreEqual(_Graph, _Vertex.Graph);
            Assert.NotNull(_Vertex.IdKey);
            Assert.NotNull(_Vertex.Id);
            Assert.NotNull(_Vertex.RevIdKey);
            Assert.NotNull(_Vertex.RevisionId);

            Assert.IsTrue(_Vertex.ContainsKey(_Graph.IdKey));
            Assert.IsTrue(_Vertex.ContainsKey(_Graph.RevIdKey));

            var _Properties = _Vertex.ToList();
            Assert.AreEqual(2, _Properties.Count);
            foreach (var _KeyValuePair in _Properties)
                Assert.IsTrue(_KeyValuePair.Key == _Graph.IdKey ||
                              _KeyValuePair.Key == _Graph.RevIdKey);

        }

        #endregion

        #region AddVertexWithIdTest()

        /// <summary>
        /// A test for the PropertyGraph.AddVertex(VertexId) method.
        /// </summary>
        [Test]
        public void AddVertexWithIdTest()
        {

            var _Graph = new PropertyGraph();
            Assert.AreEqual(0, _Graph.NumberOfVertices());

            var _Vertex = _Graph.AddVertex(5);
            Assert.AreEqual(1, _Graph.NumberOfVertices());
            Assert.AreEqual(0, _Vertex.OutEdges().Count());
            Assert.AreEqual(0, _Vertex.InEdges().Count());

            Assert.NotNull(_Vertex.Graph);
            Assert.AreEqual(_Graph, _Vertex.Graph);
            Assert.NotNull(_Vertex.IdKey);
            Assert.AreEqual(5, _Vertex.Id);
            Assert.NotNull(_Vertex.RevIdKey);
            Assert.NotNull(_Vertex.RevisionId);

            Assert.IsTrue(_Vertex.ContainsKey(_Graph.IdKey));
            Assert.IsTrue(_Vertex.ContainsKey(_Graph.RevIdKey));
            Assert.IsTrue(_Vertex.ContainsValue(5UL));

            var _Properties = _Vertex.ToList();
            Assert.AreEqual(2, _Properties.Count);
            foreach (var _KeyValuePair in _Properties)
                Assert.IsTrue(_KeyValuePair.Key == _Graph.IdKey ||
                              _KeyValuePair.Key == _Graph.RevIdKey);

        }

        #endregion

        #region AddVertexWithVertexInitializerTest()

        /// <summary>
        /// A test for the PropertyGraph.AddVertex(VertexInitializer) method.
        /// </summary>
        [Test]
        public void AddVertexWithVertexInitializerTest()
        {

            var _Graph = new PropertyGraph();
            Assert.AreEqual(0, _Graph.NumberOfVertices());

            var _Vertex = _Graph.AddVertex(v => v.SetProperty("key1", "value1").
                                                  SetProperty("key2", 42));
            Assert.AreEqual(1, _Graph.NumberOfVertices());
            Assert.AreEqual(0, _Vertex.OutEdges().Count());
            Assert.AreEqual(0, _Vertex.InEdges().Count());

            Assert.NotNull(_Vertex.Graph);
            Assert.AreEqual(_Graph, _Vertex.Graph);
            Assert.NotNull(_Vertex.IdKey);
            Assert.NotNull(_Vertex.Id);
            Assert.NotNull(_Vertex.RevIdKey);
            Assert.NotNull(_Vertex.RevisionId);

            Assert.IsTrue(_Vertex.ContainsKey(_Graph.IdKey));
            Assert.IsTrue(_Vertex.ContainsKey(_Graph.RevIdKey));
            Assert.IsTrue(_Vertex.ContainsKey("key1"));
            Assert.IsTrue(_Vertex.ContainsKey("key2"));
            Assert.IsTrue(_Vertex.ContainsValue("value1"));
            Assert.IsTrue(_Vertex.ContainsValue(42));

            var _Properties = _Vertex.ToList();
            Assert.NotNull(_Properties);
            Assert.AreEqual(4, _Properties.Count);
            foreach (var _KeyValuePair in _Properties)
                Assert.IsTrue( _KeyValuePair.Key == _Graph.IdKey    ||
                               _KeyValuePair.Key == _Graph.RevIdKey ||
                              (_KeyValuePair.Key == "key1" && _KeyValuePair.Value.ToString() == "value1") ||
                              (_KeyValuePair.Key == "key2" && ((Int32) _KeyValuePair.Value   == 42)));

            var _PropertyKeys = _Vertex.Keys;
            Assert.NotNull(_PropertyKeys);
            Assert.AreEqual(4, _PropertyKeys.Count());
            foreach (var _Keys in _PropertyKeys)
                Assert.IsTrue(_Keys == _Graph.IdKey    ||
                              _Keys == _Graph.RevIdKey ||
                              _Keys == "key1"          ||
                              _Keys == "key2");

            var _PropertyValues = _Vertex.Values;
            Assert.NotNull(_PropertyValues);
            Assert.AreEqual(4, _PropertyValues.Count());
            Assert.IsTrue(_PropertyValues.Contains("value1"));
            Assert.IsTrue(_PropertyValues.Contains(42));

        }

        #endregion

        #region AddVertexWithIdAndVertexInitializerTest()

        /// <summary>
        /// A test for the PropertyGraph.AddVertex(VertexId, VertexInitializer) method.
        /// </summary>
        [Test]
        public void AddVertexWithIdAndVertexInitializerTest()
        {

            var _Graph = new PropertyGraph();
            Assert.AreEqual(0, _Graph.NumberOfVertices());

            var _Vertex = _Graph.AddVertex(23, v => v.SetProperty("key1", "value1").
                                                      SetProperty(new KeyValuePair<String, Object>("key2", 42)));
            Assert.AreEqual(1, _Graph.NumberOfVertices());
            Assert.AreEqual(0, _Vertex.OutEdges().Count());
            Assert.AreEqual(0, _Vertex.InEdges().Count());

            Assert.NotNull(_Vertex.Graph);
            Assert.AreEqual(_Graph, _Vertex.Graph);
            Assert.NotNull(_Vertex.IdKey);
            Assert.AreEqual(23, _Vertex.Id);
            Assert.NotNull(_Vertex.RevIdKey);
            Assert.NotNull(_Vertex.RevisionId);

            Assert.IsTrue(_Vertex.ContainsKey(_Graph.IdKey));
            Assert.IsTrue(_Vertex.ContainsKey(_Graph.RevIdKey));
            Assert.IsTrue(_Vertex.ContainsKey("key1"));
            Assert.IsTrue(_Vertex.ContainsKey("key2"));
            Assert.IsTrue(_Vertex.ContainsValue("value1"));
            Assert.IsTrue(_Vertex.ContainsValue(42));

            var _Properties = _Vertex.ToList();
            Assert.NotNull(_Properties);
            Assert.AreEqual(4, _Properties.Count);
            foreach (var _KeyValuePair in _Properties)
                Assert.IsTrue((_KeyValuePair.Key == _Graph.IdKey && ((UInt64)_KeyValuePair.Value == 23))  ||
                               _KeyValuePair.Key == _Graph.RevIdKey                                       ||
                              (_KeyValuePair.Key == "key1" && _KeyValuePair.Value.ToString() == "value1") ||
                              (_KeyValuePair.Key == "key2" && ((Int32) _KeyValuePair.Value   == 42)));

            var _PropertyKeys = _Vertex.Keys;
            Assert.NotNull(_PropertyKeys);
            Assert.AreEqual(4, _PropertyKeys.Count());
            foreach (var _Keys in _PropertyKeys)
                Assert.IsTrue(_Keys == _Graph.IdKey    ||
                              _Keys == _Graph.RevIdKey ||
                              _Keys == "key1"          ||
                              _Keys == "key2");

            var _PropertyValues = _Vertex.Values;
            Assert.NotNull(_PropertyValues);
            Assert.AreEqual(4, _PropertyValues.Count());
            Assert.IsTrue(_PropertyValues.Contains(23UL));
            Assert.IsTrue(_PropertyValues.Contains("value1"));
            Assert.IsTrue(_PropertyValues.Contains(42));

        }

        #endregion


        #region TryToChangeTheVertexIdentification()

        /// <summary>
        /// A test for trying to change the vertex identification.
        /// </summary>
        [Test]
        [ExpectedException(typeof(IdentificationChangeException))]
        public void TryToChangeTheVertexIdentification()
        {

            var _Graph = new PropertyGraph();
            Assert.AreEqual(0, _Graph.NumberOfVertices());

            var _Vertex1 = _Graph.AddVertex(5);
            _Vertex1.SetProperty(_Graph.IdKey, 23);

        }

        #endregion

        #region TryToChangeTheVertexRevisionIdentification()

        /// <summary>
        /// A test for trying to change the vertex revision identification.
        /// </summary>
        [Test]
        [ExpectedException(typeof(RevisionIdentificationChangeException))]
        public void TryToChangeTheVertexRevisionIdentification()
        {

            var _Graph = new PropertyGraph();
            Assert.AreEqual(0, _Graph.NumberOfVertices());

            var _Vertex1 = _Graph.AddVertex(5);
            _Vertex1.SetProperty(_Graph.RevIdKey, 23);

        }

        #endregion


        #region AddTwoVerticesHavingTheSameIdentification()

        /// <summary>
        /// A test for adding two vertices having the same identification.
        /// </summary>
        [Test]
        [ExpectedException(typeof(DuplicateVertexIdException))]
        public void AddTwoVerticesHavingTheSameIdentification()
        {

            var _Graph = new PropertyGraph();
            Assert.AreEqual(0, _Graph.NumberOfVertices());

            var _Vertex1 = _Graph.AddVertex(5);
            Assert.AreEqual(1, _Graph.NumberOfVertices());

            var _Vertex2 = _Graph.AddVertex(5);

        }

        #endregion

        #region AddMultipleVertices()

        /// <summary>
        /// A test for adding multiple vertices.
        /// </summary>
        [Test]
        public void AddMultipleVertices()
        {

            var _Graph = new PropertyGraph();
            Assert.AreEqual(0, _Graph.NumberOfVertices());

            var _Random = new Random().Next(100);

            for (var i = 1; i <= _Random; i++)
            {
                _Graph.AddVertex((UInt64)i);
                Assert.AreEqual(i, _Graph.NumberOfVertices());
            }

        }

        #endregion

    }

}
