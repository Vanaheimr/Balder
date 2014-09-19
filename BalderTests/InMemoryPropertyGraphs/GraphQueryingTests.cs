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

using org.GraphDefined.Vanaheimr.Illias.Collections;
using org.GraphDefined.Vanaheimr.Balder;
using org.GraphDefined.Vanaheimr.Balder.InMemory;

#endregion

namespace org.GraphDefined.Vanaheimr.Balder.UnitTests.InMemoryPropertyGraphs
{

    /// <summary>
    /// Unit tests for querying generic property graphs.
    /// </summary>
    [TestFixture]
    public class GraphQueryingTests
    {

        #region CheckGraphQueriesVertices()

        /// <summary>
        /// A test for graph-centric property graph queries for vertices.
        /// </summary>
        [Test]
        public void CheckGraphQueriesVertices()
        {

            // Setup
            var _graph = DemoGraphFactory.CreateSimpleGraph();
            Assert.IsNotNull(_graph, "graph must not be null!");


            // VertexById(...)
            Assert.IsNull(_graph.VertexById(null));
            Assert.IsNull(_graph.VertexById("Eve"));
            Assert.AreEqual("Alice", _graph.VertexById("Alice").Id);

            // TryGetVertexById(...)
            IReadOnlyGenericPropertyVertex<String, Int64, String, String, Object,
                                           String, Int64, String, String, Object,
                                           String, Int64, String, String, Object,
                                           String, Int64, String, String, Object> Vertex;

            Assert.IsFalse(_graph.TryGetVertexById(null, out Vertex));
            Assert.IsNull(Vertex);
            Assert.IsFalse(_graph.TryGetVertexById("Eve", out Vertex));
            Assert.IsNull(Vertex);
            Assert.IsTrue(_graph.TryGetVertexById("Alice", out Vertex));
            Assert.IsNotNull(Vertex);
            Assert.AreEqual("Alice", Vertex.Id);

            // HasVertexId(...)
            Assert.IsFalse(_graph.HasVertexId(null));
            Assert.IsFalse(_graph.HasVertexId("Eve"));
            Assert.IsTrue (_graph.HasVertexId("Alice"));

            // VerticesById(...)
            Assert.IsFalse(               _graph.VerticesById().    Any());
            Assert.IsFalse(               _graph.VerticesById(null).Any());
            Assert.AreEqual(0,            _graph.VerticesById(null).Count());
            Assert.AreEqual(2,            _graph.VerticesById(null, null).Count());  // Compared to VerticesById(null) perhaps a bit surprising!
            Assert.AreEqual("|",          _graph.VerticesById(null, null).Ids().AggString());
            Assert.AreEqual(3,            _graph.VerticesById(null, null, null).Count());
            Assert.AreEqual("||",         _graph.VerticesById(null, null, null).Ids().AggString());
            Assert.AreEqual("Alice",      _graph.VerticesById("Alice").Ids().AggString());
            Assert.AreEqual("Alice|Bob",  _graph.VerticesById("Alice", "Bob").      Ids().AggString());
            Assert.AreEqual("Alice||Bob", _graph.VerticesById("Alice", null, "Bob").Ids().AggString());
            Assert.AreEqual("|Alice|Bob", _graph.VerticesById(null, "Alice", "Bob").Ids().AggString());

            // VerticesByLabel(...)
            Assert.IsFalse(               _graph.VerticesByLabel().Any());
            Assert.IsFalse(               _graph.VerticesByLabel(null).Any());
            Assert.AreEqual(0,            _graph.VerticesByLabel(null).Count());
            Assert.AreEqual(4,            _graph.VerticesByLabel(DemoGraphFactory.person).Count());
            Assert.AreEqual(1,            _graph.VerticesByLabel(DemoGraphFactory.pet).Count());
            Assert.AreEqual(5,            _graph.VerticesByLabel(DemoGraphFactory.person, DemoGraphFactory.pet).Count());
            Assert.AreEqual(5,            _graph.VerticesByLabel(DemoGraphFactory.person, null, DemoGraphFactory.pet).Count());
            Assert.AreEqual(5,            _graph.VerticesByLabel(null, DemoGraphFactory.person, DemoGraphFactory.pet).Count());

            // Vertices(...)
            Assert.AreEqual(5,            _graph.Vertices().Count());
            Assert.AreEqual(5,            _graph.Vertices(null).Count());
            Assert.AreEqual(5,            _graph.Vertices(v => true).Count());
            Assert.AreEqual(0,            _graph.Vertices(v => false).Count());
            Assert.AreEqual(1,            _graph.Vertices(v => v.Id == "Alice").Count());


            // Multiple graphs...
            var _graphs = new List<IReadOnlyGenericPropertyGraph<String, Int64, String, String, Object,
                                                                 String, Int64, String, String, Object,
                                                                 String, Int64, String, String, Object,
                                                                 String, Int64, String, String, Object>>() { _graph, _graph, _graph };

            Assert.AreEqual(15,           _graphs.V().          Count());
            Assert.AreEqual(15,           _graphs.V(null).      Count());
            Assert.AreEqual(15,           _graphs.V(v => true). Count());
            Assert.AreEqual(0,            _graphs.V(v => false).Count());
            Assert.AreEqual(3,            _graphs.V(v => v.Id == "Alice").Count());


        }

        #endregion

        #region CheckGraphQueriesEdges()

        /// <summary>
        /// A test for graph-centric property graph queries for edges.
        /// </summary>
        [Test]
        public void CheckGraphQueriesEdges()
        {

            // Setup
            var _graph = DemoGraphFactory.CreateSimpleGraph();
            Assert.IsNotNull(_graph, "graph must not be null!");


            // VertexById(...)
            Assert.IsNull(_graph.EdgeById(null));
            Assert.IsNull(_graph.EdgeById("Eve"));
            Assert.AreEqual("Alice", _graph.EdgeById("Alice -loves-> Bob").OutVertex.Id);

            // TryGetVertexById(...)
            IReadOnlyGenericPropertyEdge<String, Int64, String, String, Object,
                                         String, Int64, String, String, Object,
                                         String, Int64, String, String, Object,
                                         String, Int64, String, String, Object> Edge;

            Assert.IsFalse(_graph.TryGetEdgeById(null, out Edge));
            Assert.IsNull(Edge);
            Assert.IsFalse(_graph.TryGetEdgeById("Eve", out Edge));
            Assert.IsNull(Edge);
            Assert.IsTrue(_graph.TryGetEdgeById("Alice -loves-> Bob", out Edge));
            Assert.IsNotNull(Edge);
            Assert.AreEqual("Alice -loves-> Bob", Edge.Id);

            // HasEdgeId(...)
            Assert.IsFalse(_graph.HasEdgeId(null));
            Assert.IsFalse(_graph.HasEdgeId("Eve"));
            Assert.IsTrue(_graph.HasEdgeId("Alice -loves-> Bob"));

            // EdgesById(...)
            Assert.IsFalse(               _graph.EdgesById().    Any());
            Assert.IsFalse(               _graph.EdgesById(null).Any());
            Assert.AreEqual(0,            _graph.EdgesById(null).Count());
            Assert.AreEqual(2,            _graph.EdgesById(null, null).Count());  // Compared to EdgesById(null) perhaps a bit surprising!
            Assert.AreEqual("|",          _graph.EdgesById(null, null).Ids().AggString());
            Assert.AreEqual(3,            _graph.EdgesById(null, null, null).Count());
            Assert.AreEqual("||",         _graph.EdgesById(null, null, null).Ids().AggString());
            Assert.AreEqual("Alice",      _graph.EdgesById("Alice -loves-> Bob").OutV().Ids().AggString());
            Assert.AreEqual("Alice|Bob",  _graph.EdgesById("Alice -loves-> Bob", "Bob -loves-> Carol").OutV().Ids().AggString());
            Assert.AreEqual("Alice||Bob", _graph.EdgesById("Alice -loves-> Bob", null, "Bob -loves-> Carol").OutV().Ids().AggString());
            Assert.AreEqual("|Alice|Bob", _graph.EdgesById(null, "Alice -loves-> Bob", "Bob -loves-> Carol").OutV().Ids().AggString());

            // EdgesByLabel(...)
            Assert.IsFalse(               _graph.EdgesByLabel().Any());
            Assert.IsFalse(               _graph.EdgesByLabel(null).Any());
            Assert.AreEqual(0,            _graph.EdgesByLabel(null).Count());
            Assert.AreEqual(6,            _graph.EdgesByLabel(DemoGraphFactory.knows).Count());
            Assert.AreEqual(3,            _graph.EdgesByLabel(DemoGraphFactory.loves).Count());
            Assert.AreEqual(9,            _graph.EdgesByLabel(DemoGraphFactory.knows, DemoGraphFactory.loves).Count());
            Assert.AreEqual(9,            _graph.EdgesByLabel(DemoGraphFactory.knows, null, DemoGraphFactory.loves).Count());
            Assert.AreEqual(9,            _graph.EdgesByLabel(null, DemoGraphFactory.knows, DemoGraphFactory.loves).Count());

            // Edges(...)
            Assert.AreEqual(9,            _graph.Edges().          Count());
            Assert.AreEqual(9,            _graph.Edges(null).      Count());
            Assert.AreEqual(9,            _graph.Edges(e => true). Count());
            Assert.AreEqual(0,            _graph.Edges(e => false).Count());
            Assert.AreEqual(1,            _graph.Edges(e => e.Id == "Alice -loves-> Bob").Count());


            // Multiple graphs...
            var _graphs = new List<IReadOnlyGenericPropertyGraph<String, Int64, String, String, Object,
                                                                 String, Int64, String, String, Object,
                                                                 String, Int64, String, String, Object,
                                                                 String, Int64, String, String, Object>>() { _graph, _graph, _graph };

            Assert.AreEqual(27,           _graphs.E().          Count());
            Assert.AreEqual(27,           _graphs.E(null).      Count());
            Assert.AreEqual(27,           _graphs.E(e => true). Count());
            Assert.AreEqual(0,            _graphs.E(e => false).Count());
            Assert.AreEqual(3,            _graphs.E(e => e.Id == "Alice -loves-> Bob").Count());

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

            Assert.AreEqual("Alice|Dave|Rex",       _graph.Vertices(v => v.Id.Contains("e")).Ids().Distinct().OrderBy(s => s).AggString());
            Assert.AreEqual("Alice|Bob|Carol|Dave", _graph.Vertices(DemoGraphFactory.age).Ids().Distinct().OrderBy(s => s).AggString());      // property key filter
            Assert.AreEqual("Alice",                _graph.Vertices(DemoGraphFactory.age, 18).Ids().AggString());  // property key+value filter

            // Labels
            var VertexLabelSet = _graph.Vertices().Labels().Distinct().ToArray();
            Assert.AreEqual(2, VertexLabelSet.Length, "Wrong number of vertex labels!");
            Assert.IsTrue(VertexLabelSet.Contains(DemoGraphFactory.person));
            Assert.IsTrue(VertexLabelSet.Contains(DemoGraphFactory.pet));

            // RevIds
            var VertexRevIdSet = _graph.Vertices().RevIds().Distinct().ToArray();
            Assert.AreEqual(1, VertexRevIdSet.Length, "Wrong number of vertex revision identifications!");

            // OutEdges (pure, edge filter delegate, edge labels)
            Assert.AreEqual("0|2|Alice -loves-> Bob", _graph.VertexById("Alice").OutE    ().                       Ids().Sort().AggString());
            Assert.AreEqual("0|2|Alice -loves-> Bob", _graph.VertexById("Alice").OutEdges().                       Ids().Sort().AggString());
            Assert.AreEqual("0|2|Alice -loves-> Bob", _graph.VertexById("Alice").OutE    (e => true).              Ids().Sort().AggString());
            Assert.AreEqual("0|2|Alice -loves-> Bob", _graph.VertexById("Alice").OutEdges(e => true).              Ids().Sort().AggString());
            Assert.AreEqual("0|2",                    _graph.VertexById("Alice").OutE    (e => e.Label == "knows").Ids().Sort().AggString());
            Assert.AreEqual("0|2",                    _graph.VertexById("Alice").OutEdges(e => e.Label == "knows").Ids().Sort().AggString());
            Assert.AreEqual("Alice -loves-> Bob",     _graph.VertexById("Alice").OutE    (e => e.Label == "loves").Ids().Sort().AggString());
            Assert.AreEqual("Alice -loves-> Bob",     _graph.VertexById("Alice").OutEdges(e => e.Label == "loves").Ids().Sort().AggString());
            Assert.AreEqual("",                       _graph.VertexById("Alice").OutE    (e => false).             Ids().Sort().AggString());
            Assert.AreEqual("",                       _graph.VertexById("Alice").OutEdges(e => false).             Ids().Sort().AggString());
            Assert.AreEqual("",                       _graph.VertexById("Alice").OutE    ("evil").                 Ids().Sort().AggString());
            Assert.AreEqual("",                       _graph.VertexById("Alice").OutEdges("evil").                 Ids().Sort().AggString());
            Assert.AreEqual("0|2",                    _graph.VertexById("Alice").OutE    ("knows").                Ids().Sort().AggString());
            Assert.AreEqual("0|2",                    _graph.VertexById("Alice").OutEdges("knows").                Ids().Sort().AggString());
            Assert.AreEqual("Alice -loves-> Bob",     _graph.VertexById("Alice").OutE    ("loves").                Ids().Sort().AggString());
            Assert.AreEqual("Alice -loves-> Bob",     _graph.VertexById("Alice").OutEdges("loves").                Ids().Sort().AggString());
            Assert.AreEqual("0|2|Alice -loves-> Bob", _graph.VertexById("Alice").OutE    ("knows", "loves").       Ids().Sort().AggString());
            Assert.AreEqual("0|2|Alice -loves-> Bob", _graph.VertexById("Alice").OutEdges("knows", "loves").       Ids().Sort().AggString());

            // OutNeighbors (pure, edge filter delegate, edge labels)
            Assert.AreEqual("Bob|Bob|Carol",          _graph.VertexById("Alice").Out().                                  Ids().Sort().AggString());
            Assert.AreEqual("Bob|Carol",              _graph.VertexById("Alice").Out().Distinct().                       Ids().Sort().AggString());
            Assert.AreEqual("Bob|Bob|Carol",          _graph.VertexById("Alice").Out(e => true).                         Ids().Sort().AggString());
            Assert.AreEqual("Bob|Carol",              _graph.VertexById("Alice").Out(e => true).Distinct().              Ids().Sort().AggString());
            Assert.AreEqual("Bob|Carol",              _graph.VertexById("Alice").Out(e => e.Label == "knows").           Ids().Sort().AggString());
            Assert.AreEqual("Bob|Carol",              _graph.VertexById("Alice").Out(e => e.Label == "knows").Distinct().Ids().Sort().AggString());
            Assert.AreEqual("Bob",                    _graph.VertexById("Alice").Out(e => e.Label == "loves").           Ids().Sort().AggString());
            Assert.AreEqual("Bob",                    _graph.VertexById("Alice").Out(e => e.Label == "loves").Distinct().Ids().Sort().AggString());
            Assert.AreEqual("",                       _graph.VertexById("Alice").Out(e => false).                        Ids().Sort().AggString());
            Assert.AreEqual("",                       _graph.VertexById("Alice").Out(e => false).Distinct().             Ids().Sort().AggString());


            // InEdges
            var AlicesInEdges1             = _graph.VertexById("Alice").InE().Ids().ToArray();
            var AlicesInEdges2             = _graph.VertexById("Alice").InEdges().Ids().ToArray();
            Assert.AreEqual("Carol -loves-> Alice|1|3", AlicesInEdges1.AggString());
            Assert.AreEqual("Carol -loves-> Alice|1|3", AlicesInEdges2.AggString());

            // InNeighbors
            var AlicesInNeighbors          = _graph.VertexById("Alice").In().Ids().ToArray();
            var AlicesInNeighborsUnique    = _graph.VertexById("Alice").In().Distinct().Ids().ToArray();
            Assert.AreEqual("Carol|Bob|Carol",  AlicesInNeighbors.      AggString());
            Assert.AreEqual("Carol|Bob",        AlicesInNeighborsUnique.AggString());



            var AlicesBothEdges            = _graph.VertexById("Alice").BothE().Ids().ToArray();
            var AlicesBothEdgesUnique      = _graph.VertexById("Alice").BothE().Distinct().Ids().ToArray();
            Assert.AreEqual("Alice -loves-> Bob|0|2|Carol -loves-> Alice|1|3",  AlicesBothEdges.      AggString());
            Assert.AreEqual("Alice -loves-> Bob|0|2|Carol -loves-> Alice|1|3",  AlicesBothEdgesUnique.AggString());

            var AlicesBothEdges2           = _graph.VertexById("Alice").BothE(e => e.Label == "knows").Ids().ToArray();
            var AlicesBothEdges2Unique     = _graph.VertexById("Alice").BothE(e => e.Label == "knows").Distinct().Ids().ToArray();
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

