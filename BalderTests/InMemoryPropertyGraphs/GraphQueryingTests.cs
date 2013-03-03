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
    public class GraphQueryingTests
    {

        #region CheckGraphQueries()

        /// <summary>
        /// A test for vertex-centric property graph queries.
        /// </summary>
        [Test]
        public void CheckGraphQueries()
        {

            // Setup
            var _graph = DemoGraphFactory.CreateSimpleGraph();

            Assert.IsNotNull(_graph, "graph must not be null!");


            // Vertices
            Assert.IsNull(_graph.VertexById(null));
            Assert.IsNull(_graph.VertexById("Eve"));
            Assert.AreEqual("Alice", _graph.VertexById("Alice").Id);

            Assert.AreEqual(false,        _graph.VerticesById().    Any());
            Assert.AreEqual(false,        _graph.VerticesById(null).Any());
            Assert.AreEqual("Alice|Bob",  _graph.VerticesById("Alice", "Bob").      Ids().AggString());
            Assert.AreEqual("Alice||Bob", _graph.VerticesById("Alice", null, "Bob").Ids().AggString());

            Assert.IsFalse(_graph.HasVertexId(null));
            Assert.IsFalse(_graph.HasVertexId("Eve"));
            Assert.IsTrue (_graph.HasVertexId("Alice"));


            // Edges
            Assert.IsNull(_graph.EdgeById(null));
            Assert.IsNull(_graph.EdgeById("Eve"));
            Assert.AreEqual("Alice", _graph.EdgeById("Alice -loves-> Bob").OutVertex.Id);

            Assert.AreEqual(false,        _graph.EdgesById().    Any());
            Assert.AreEqual(false,        _graph.EdgesById(null).Any());
            Assert.AreEqual("Alice|Bob",  _graph.EdgesById("Alice -loves-> Bob", "Bob -loves-> Carol").      OutV().Ids().AggString());
            Assert.AreEqual("Alice||Bob", _graph.EdgesById("Alice -loves-> Bob", null, "Bob -loves-> Carol").OutV().Ids().AggString());

            Assert.IsFalse(_graph.HasEdgeId(null));
            Assert.IsFalse(_graph.HasEdgeId("Eve"));
            Assert.IsTrue (_graph.HasEdgeId("Alice -loves-> Bob"));

        }

        #endregion


        #region CheckVertexQueries()

        /// <summary>
        /// A test for vertex-centric property graph queries.
        /// </summary>
        [Test]
        public void CheckVertexQueries()
        {

            // Setup
            var _graph = DemoGraphFactory.CreateSimpleGraph();

            Assert.IsNotNull(_graph, "graph must not be null!");


            // Vertex queries
            Assert.AreEqual(5, _graph.NumberOfVertices(), "Wrong number of vertices!");

            // Ids
            var VertexIdSet = _graph.Vertices().Ids().Distinct().ToArray();
            Assert.AreEqual(5, VertexIdSet.Length, "Wrong number of vertex identifications!");
            Assert.IsTrue(VertexIdSet.Contains("Alice"));
            Assert.IsTrue(VertexIdSet.Contains("Bob"));
            Assert.IsTrue(VertexIdSet.Contains("Carol"));
            Assert.IsTrue(VertexIdSet.Contains("Dave"));
            Assert.IsTrue(VertexIdSet.Contains("Rex"));

            Assert.AreEqual("Rex|Alice|Dave",       _graph.Vertices(v => v.Id.Contains("e")).Ids().Distinct().AggString());
            Assert.AreEqual("Alice|Bob|Carol|Dave", _graph.Vertices(DemoGraphFactory.age).Ids().Distinct().AggString());      // property key filter
            Assert.AreEqual("Alice",                _graph.Vertices(DemoGraphFactory.age, 18).Ids().Distinct().AggString());  // property key+value filter

            // Labels
            var VertexLabelSet = _graph.Vertices().Labels().Distinct().ToArray();
            Assert.AreEqual(2, VertexLabelSet.Length, "Wrong number of vertex labels!");
            Assert.IsTrue(VertexLabelSet.Contains(DemoGraphFactory.person));
            Assert.IsTrue(VertexLabelSet.Contains(DemoGraphFactory.pet));

            // RevIds
            var VertexRevIdSet = _graph.Vertices().RevIds().Distinct().ToArray();
            Assert.AreEqual(1, VertexRevIdSet.Length, "Wrong number of vertex revision identifications!");

            // Edges and neighbors
            var AlicesOutEdges1            = _graph.VertexById("Alice").OutE().Ids().ToArray();
            var AlicesOutEdges2            = _graph.VertexById("Alice").OutEdges().Ids().ToArray();
            Assert.AreEqual("Alice -loves-> Bob|0|2", AlicesOutEdges1.AggString());
            Assert.AreEqual("Alice -loves-> Bob|0|2", AlicesOutEdges2.AggString());

            var AlicesOutNeighbors         = _graph.VertexById("Alice").Out().Ids().ToArray();
            var AlicesOutNeighborsUnique   = _graph.VertexById("Alice").Out().Distinct().Ids().ToArray();
            Assert.AreEqual("Bob|Bob|Carol",  AlicesOutNeighbors.      AggString());
            Assert.AreEqual("Bob|Carol",      AlicesOutNeighborsUnique.AggString());


            var AlicesInEdges1             = _graph.VertexById("Alice").InE().Ids().ToArray();
            var AlicesInEdges2             = _graph.VertexById("Alice").InEdges().Ids().ToArray();
            Assert.AreEqual("Carol -loves-> Alice|1|3", AlicesInEdges1.AggString());
            Assert.AreEqual("Carol -loves-> Alice|1|3", AlicesInEdges2.AggString());

            var AlicesInNeighbors          = _graph.VertexById("Alice").In().Ids().ToArray();
            var AlicesInNeighborsUnique    = _graph.VertexById("Alice").In().Distinct().Ids().ToArray();
            Assert.AreEqual("Carol|Bob|Carol",  AlicesInNeighbors.      AggString());
            Assert.AreEqual("Carol|Bob",        AlicesInNeighborsUnique.AggString());


            var AlicesBothEdges            = _graph.VertexById("Alice").BothE().Ids().ToArray();
            var AlicesBothEdgesUnique      = _graph.VertexById("Alice").BothE().Distinct().Ids().ToArray();
            Assert.AreEqual("Alice -loves-> Bob|0|2|Carol -loves-> Alice|1|3",  AlicesBothEdges.      AggString());
            Assert.AreEqual("Alice -loves-> Bob|0|2|Carol -loves-> Alice|1|3",  AlicesBothEdgesUnique.AggString());

            var AlicesBothNeighbors        = _graph.VertexById("Alice").Both().Ids().ToArray();
            var AlicesBothUniqueNeighbors  = _graph.VertexById("Alice").Both().Distinct().Ids().ToArray();
            Assert.AreEqual("Bob|Bob|Carol|Carol|Bob|Carol",  AlicesBothNeighbors.      AggString());
            Assert.AreEqual("Bob|Carol",                      AlicesBothUniqueNeighbors.AggString());

        }

        #endregion

        #region CheckEdgeQueries()

        /// <summary>
        /// A test for edge-centric property graph queries.
        /// </summary>
        [Test]
        public void CheckEdgeQueries()
        {

            // Setup
            var _graph = DemoGraphFactory.CreateSimpleGraph();

            Assert.IsNotNull(_graph, "graph must not be null!");


            // Edge queries
            Assert.AreEqual(9, _graph.NumberOfEdges(), "Wrong number of edges!");

            // Ids
            var EdgeIdSet     = _graph.Edges().Ids().Distinct().ToArray();
            Assert.AreEqual(9, EdgeIdSet.Length, "Wrong number of edge identifications!");
            Assert.IsTrue(EdgeIdSet.Contains("Alice -loves-> Bob"));
            Assert.IsTrue(EdgeIdSet.Contains("Bob -loves-> Carol"));
            Assert.IsTrue(EdgeIdSet.Contains("Carol -loves-> Alice"));

            // Labels
            var EdgeLabelSet  = _graph.Edges().Labels().Distinct().ToArray();
            Assert.AreEqual(2, EdgeLabelSet.Length, "Wrong number of edge labels!");
            Assert.IsTrue(EdgeLabelSet.Contains(DemoGraphFactory.knows));
            Assert.IsTrue(EdgeLabelSet.Contains(DemoGraphFactory.loves));

            // RevIds
            var EdgeRevIdSet  = _graph.Edges().RevIds().Distinct().ToArray();
            Assert.AreEqual(1, EdgeRevIdSet.Length, "Wrong number of edge revision identifications!");


            // Multiedges
            Assert.AreEqual(1, _graph.NumberOfMultiEdges(), "Wrong number of multiedges!");


            // Hyperedges
            Assert.AreEqual(2, _graph.NumberOfHyperEdges(), "Wrong number of hyperedges!");

        }

        #endregion

    }

}
