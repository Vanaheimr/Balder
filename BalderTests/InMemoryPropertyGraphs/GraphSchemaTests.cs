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
using org.GraphDefined.Vanaheimr.Balder.InMemory;
using org.GraphDefined.Vanaheimr.Balder.Schema;

#endregion

namespace org.GraphDefined.Vanaheimr.Balder.UnitTests.InMemoryPropertyGraphs
{

    /// <summary>
    /// Unit tests for creating schema graphs for generic property graphs.
    /// </summary>
    [TestFixture]
    public class GraphSchemaTests
    {

        #region CreateSimpleSchemaGraph()

        /// <summary>
        /// A test for the empty PropertyGraph.AddVertex() method.
        /// </summary>
        [Test]
        public void CreateSimpleSchemaGraph()
        {

            var graph = GraphFactory.CreateGenericPropertyGraph_WithStringIds();

            // vertex labels
            var a_person  = "a_person";

            // edge labels
            var loves     = "loves";
            var dislikes  = "dislikes";

            var Alice     = graph.AddVertex("Alice", a_person);
            var Bob       = graph.AddVertex("Bob",   a_person);
            var Carol     = graph.AddVertex("Carol", a_person);

            var e1        = graph.AddEdge(Alice, loves,    Bob);
            var e2        = graph.AddEdge(Bob,   loves,    Carol);
            var e3        = graph.AddEdge(Alice, dislikes, Carol);

            var schema    = graph.StrictSchemaGraph("Schema01");

            Assert.IsNotNull(schema);

            Assert.AreEqual(1UL, schema.NumberOfVertices());    // a_person
            Assert.AreEqual(2UL, schema.NumberOfEdges());       // loves, dislikes
            Assert.AreEqual(0UL, schema.NumberOfMultiEdges());
            Assert.AreEqual(0UL, schema.NumberOfHyperEdges());

            var a_person_vertex = schema.VertexById(a_person);
            Assert.IsNotNull(a_person_vertex);
            Assert.AreEqual(2UL, a_person_vertex.OutDegree());

            var loves_edge      = schema.EdgeById(loves);
            Assert.IsNotNull(loves_edge);

            var dislikes_edge   = schema.EdgeById(dislikes);
            Assert.IsNotNull(dislikes_edge);

        }

        #endregion

        #region CreateSimpleSchemaGraph_ShouldFail()

        /// <summary>
        /// A test for the empty PropertyGraph.AddVertex() method.
        /// </summary>
        [Test]
        [ExpectedException(typeof(SchemaViolation))]
        public void CreateSimpleSchemaGraph_ShouldFail()
        {

            var graph = GraphFactory.CreateGenericPropertyGraph_WithStringIds();

            // vertex labels
            var a_person  = "a_person";
            var a_pet     = "a_pet";

            // edge labels
            var loves     = "loves";
            var dislikes  = "dislikes";

            var Alice     = graph.AddVertex("Alice", a_person);
            var Bob       = graph.AddVertex("Bob",   a_person);
            var Carol     = graph.AddVertex("Carol", a_person);

            var Gizmo     = graph.AddVertex("Gizmo", a_pet);

            var e1        = graph.AddEdge(Alice, loves,    Bob);
            var e2        = graph.AddEdge(Bob,   loves,    Carol);
            var e3        = graph.AddEdge(Alice, dislikes, Carol);
            var e4        = graph.AddEdge(Alice, loves,    Gizmo);

            var schema    = graph.StrictSchemaGraph("Schema01");

        }

        #endregion

    }

}
