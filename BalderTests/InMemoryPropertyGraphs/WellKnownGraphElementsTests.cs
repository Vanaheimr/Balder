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
using System.Linq;
using System.Collections.Generic;

using NUnit.Framework;

using de.ahzf.Illias.Commons.Collections;
using de.ahzf.Vanaheimr.Blueprints.InMemory;
using de.ahzf.Vanaheimr.Blueprints;

#endregion

namespace de.ahzf.Vanaheimr.Balder.UnitTests.InMemoryPropertyGraphs
{

    /// <summary>
    /// Unit tests for creating generic property graphs by using the GraphFactory.
    /// </summary>
    [TestFixture]
    public class GraphElementsTests
    {

        #region CheckGraphElements()

        /// <summary>
        /// A test for the empty PropertyGraph.AddVertex() method.
        /// </summary>
        [Test]
        public void CheckGraphElementPipes()
        {

            var _graph = DemoGraphFactory.CreateSimpleGraph();

            Assert.IsNotNull(_graph, "graph must not be null!");


            // Vertices
            Assert.AreEqual(5, _graph.NumberOfVertices(), "Wrong number of vertices!");

            var VertexIdSet = new HashSet<String>(_graph.Vertices().Ids());
            Assert.AreEqual(5, VertexIdSet.Count, "Wrong number of vertex identifications!");
            Assert.IsTrue(VertexIdSet.Contains("Alice"));
            Assert.IsTrue(VertexIdSet.Contains("Bob"));
            Assert.IsTrue(VertexIdSet.Contains("Carol"));
            Assert.IsTrue(VertexIdSet.Contains("Dave"));
            Assert.IsTrue(VertexIdSet.Contains("Rex"));

            Assert.AreEqual(3, new HashSet<String>(_graph.Vertices(v => v.Id.Contains("e")).Ids()).Count);
            Assert.AreEqual(4, new HashSet<String>(_graph.Vertices(DemoGraphFactory.age).Ids()).Count);     // property key filter
            Assert.AreEqual(1, new HashSet<String>(_graph.Vertices(DemoGraphFactory.age, 18).Ids()).Count); // property key+value filter

            var VertexLabelSet = new HashSet<String>(_graph.Vertices().Labels());
            Assert.AreEqual(2, VertexLabelSet.Count, "Wrong number of vertex labels!");
            Assert.IsTrue(VertexLabelSet.Contains(DemoGraphFactory.person));
            Assert.IsTrue(VertexLabelSet.Contains(DemoGraphFactory.pet));

            var VertexRevIdSet = new HashSet<Int64>(_graph.Vertices().RevIds());
            Assert.AreEqual(1, VertexRevIdSet.Count, "Wrong number of vertex revision identifications!");

            var AlicesOutEdges1           = _graph.VertexById("Alice").OutE().Ids().ToArray();
            var AlicesOutEdges2           = _graph.VertexById("Alice").OutEdges().Ids().ToArray();
            Assert.AreEqual("Alice -loves-> Bob|0|2", AlicesOutEdges1.Aggregate((a, b) => a + "|" + b));
            Assert.AreEqual("Alice -loves-> Bob|0|2", AlicesOutEdges2.Aggregate((a, b) => a + "|" + b));

            var AlicesInEdges1            = _graph.VertexById("Alice").InE().Ids().ToArray();
            var AlicesInEdges2            = _graph.VertexById("Alice").InEdges().Ids().ToArray();
            Assert.AreEqual("Carol -loves-> Alice|1|3", AlicesInEdges1.Aggregate((a, b) => a + "|" + b));
            Assert.AreEqual("Carol -loves-> Alice|1|3", AlicesInEdges2.Aggregate((a, b) => a + "|" + b));


            var AlicesOutNeighbors        = _graph.VertexById("Alice").Out().Ids().ToArray();
            var AlicesOutUniqueNeighbors  = _graph.VertexById("Alice").Out().Distinct().Ids().ToArray();
            Assert.AreEqual("Bob|Bob|Carol",  AlicesOutNeighbors.      Aggregate((a, b) => a + "|" + b));
            Assert.AreEqual("Bob|Carol",      AlicesOutUniqueNeighbors.Aggregate((a, b) => a + "|" + b));

            var AlicesInNeighbors         = _graph.VertexById("Alice").In().Ids().ToArray();
            var AlicesInUniqueNeighbors   = _graph.VertexById("Alice").In().Distinct().Ids().ToArray();
            Assert.AreEqual("Carol|Bob|Carol",  AlicesInNeighbors.      Aggregate((a, b) => a + "|" + b));
            Assert.AreEqual("Carol|Bob",        AlicesInUniqueNeighbors.Aggregate((a, b) => a + "|" + b));



            // Edges
            Assert.AreEqual(9, _graph.NumberOfEdges(), "Wrong number of edges!");

            var EdgeIdSet = new HashSet<String>(_graph.Edges().Ids());
            Assert.AreEqual(9, EdgeIdSet.Count, "Wrong number of edge identifications!");
            Assert.IsTrue(EdgeIdSet.Contains("Alice -loves-> Bob"));
            Assert.IsTrue(EdgeIdSet.Contains("Bob -loves-> Carol"));
            Assert.IsTrue(EdgeIdSet.Contains("Carol -loves-> Alice"));

            var EdgeLabelSet = new HashSet<String>(_graph.Edges().Labels());
            Assert.AreEqual(2, EdgeLabelSet.Count, "Wrong number of edge labels!");
            Assert.IsTrue(EdgeLabelSet.Contains(DemoGraphFactory.knows));
            Assert.IsTrue(EdgeLabelSet.Contains(DemoGraphFactory.loves));

            var EdgeRevIdSet = new HashSet<Int64>(_graph.Edges().RevIds());
            Assert.AreEqual(1, EdgeRevIdSet.Count, "Wrong number of edge revision identifications!");


            // Multiedges
            Assert.AreEqual(1, _graph.NumberOfMultiEdges(), "Wrong number of multiedges!");


            // Hyperedges
            Assert.AreEqual(2, _graph.NumberOfHyperEdges(), "Wrong number of hyperedges!");

        }

        #endregion

    }

}
