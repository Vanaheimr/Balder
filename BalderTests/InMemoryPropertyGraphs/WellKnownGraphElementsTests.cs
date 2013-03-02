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

        #region CreateMultipleEmptyGenericPropertyGraphs()

        /// <summary>
        /// A test for the empty PropertyGraph.AddVertex() method.
        /// </summary>
        [Test]
        public void GetIds()
        {

            var _graph = DemoGraphFactory.CreateSimpleGraph();

            Assert.IsNotNull(_graph, "graph must not be null!");


            // Vertices
            Assert.AreEqual(5, _graph.NumberOfVertices(), "Wrong number of vertices!");
            var VertexIdSet = new HashSet<String>(_graph.Vertices().Ids());
            Assert.AreEqual(5, VertexIdSet.Count, "Wrong number of vertices!");

            Assert.IsTrue(VertexIdSet.Contains("Alice"));
            Assert.IsTrue(VertexIdSet.Contains("Bob"));
            Assert.IsTrue(VertexIdSet.Contains("Carol"));
            Assert.IsTrue(VertexIdSet.Contains("Dave"));
            Assert.IsTrue(VertexIdSet.Contains("Rex"));


            // Edges
            Assert.AreEqual(9, _graph.NumberOfEdges(), "Wrong number of edges!");
            var EdgeIdSet = new HashSet<String>(_graph.Edges().Ids());
            Assert.AreEqual(9, EdgeIdSet.Count, "Wrong number of edges!");

            Assert.IsTrue(EdgeIdSet.Contains("Alice -loves-> Bob"));
            Assert.IsTrue(EdgeIdSet.Contains("Bob -loves-> Carol"));
            Assert.IsTrue(EdgeIdSet.Contains("Carol -loves-> Alice"));


            // Multiedges
            Assert.AreEqual(1, _graph.NumberOfMultiEdges(), "Wrong number of multiedges!");


            // Hyperedges
            Assert.AreEqual(2, _graph.NumberOfHyperEdges(), "Wrong number of hyperedges!");

        }

        #endregion

    }

}
