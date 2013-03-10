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
    public class GraphFactoryTests
    {

        #region CreateMultipleEmptyGenericPropertyGraphs()

        /// <summary>
        /// A test for the empty PropertyGraph.AddVertex() method.
        /// </summary>
        [Test]
        public void CreateMultipleEmptyGenericPropertyGraphs()
        {

            var graph1 = GraphFactory.CreateGenericPropertyGraph();
            var graph2 = GraphFactory.CreateGenericPropertyGraph();
            var graph3 = GraphFactory.CreateGenericPropertyGraph();

            Assert.IsNotNull(graph1, "graph1 must not be null!");
            Assert.IsNotNull(graph2, "graph2 must not be null!");
            Assert.IsNotNull(graph3, "graph3 must not be null!");

            Assert.IsNotNull(graph1.Id, "graph1.Id must not be null!");
            Assert.IsNotNull(graph2.Id, "graph2.Id must not be null!");
            Assert.IsNotNull(graph3.Id, "graph3.Id must not be null!");

            Assert.AreNotEqual(graph1.Id, graph2.Id, "graph1.Id must be different from graph2.Id!");
            Assert.AreNotEqual(graph2.Id, graph3.Id, "graph2.Id must be different from graph3.Id!");
            Assert.AreNotEqual(graph3.Id, graph1.Id, "graph3.Id must be different from graph1.Id!");

        }

        #endregion

        #region CreateGenericPropertyGraph()

        /// <summary>
        /// A test for the empty PropertyGraph.AddVertex() method.
        /// </summary>
        [Test]
        public void CreateGenericPropertyGraph()
        {

            var graph = GraphFactory.CreateGenericPropertyGraph();

            // graph identification
            Assert.IsNotNull(graph,       "graph must not be null!");
            Assert.IsNotNull(graph.Id,    "graph.Id must not be null!");
            Assert.IsNotNull(graph.IdKey, "graph.IdKey must not be null!");
            Assert.AreEqual(GraphDBOntology.Id.Suffix, graph.IdKey);
            Assert.AreEqual(typeof(UInt64), graph.Id.GetType());

            Assert.IsNotNull(graph[graph.IdKey], "graph[graph.IdKey] must not be null!");
            Assert.AreEqual(graph.Id, graph[graph.IdKey]);
            Assert.IsTrue(graph.Keys.  Contains(graph.IdKey));
            Assert.IsTrue(graph.Values.Contains(graph.Id));

            Assert.IsTrue(graph.Contains(graph.IdKey, graph.Id));

            UInt64 _GraphId;
            Assert.IsTrue(graph.TryGetProperty(graph.IdKey, out _GraphId));
            Assert.AreEqual(graph.Id, _GraphId);

            // graph revision identification

            // graph label

            // graph description
            Assert.IsNull(graph["Description"], "graph.Description must be null!");

            // graph properties
            Assert.AreEqual(3UL, graph.Count(), "Instead of the expected number of KeyValuePairs the following property keys had been found: " + graph.Select(kvp => kvp.Key).Aggregate((a, b) => a + ", " + b) + "...");
            Assert.IsNotNull(graph.RevIdKey,    "graph.RevIdKey must not be null!");
            Assert.IsNotNull(graph.LabelKey,    "graph.LabelKey must not be null!");

            // Vertices and edges
            Assert.AreEqual(0UL, graph.NumberOfVertices());
            Assert.AreEqual(0UL, graph.NumberOfEdges());
            Assert.AreEqual(0UL, graph.NumberOfMultiEdges());
            Assert.AreEqual(0UL, graph.NumberOfHyperEdges());

        }

        #endregion

        #region CreateGenericPropertyGraphWithDescription()

        /// <summary>
        /// A test for the empty PropertyGraph.AddVertex() method.
        /// </summary>
        [Test]
        public void CreateGenericPropertyGraphWithDescription()
        {

            var Random            = new Random();
            var GraphDescription  = Random.Next().ToString();
            var graph             = GraphFactory.CreateGenericPropertyGraph(Description: GraphDescription);

            // graph identification
            Assert.IsNotNull(graph,    "graph must not be null!");
            Assert.IsNotNull(graph.Id, "graph.Id must not be null!");

            // graph revision identification

            // graph label

            // graph description
            Assert.IsNotNull(graph["Description"], "graph.Description must not be null!");
            Assert.AreEqual(GraphDescription, graph["Description"], "The description of the graph has not the expected value!");

            // graph properties
            Assert.AreEqual(4, graph.Count(), "Instead of the expected number of KeyValuePairs the following property keys had been found: " + graph.Select(kvp => kvp.Key).Aggregate((a, b) => a + ", " + b) + "...");

        }

        #endregion

        #region CreateGenericPropertyGraphWithIdAndDescription()

        /// <summary>
        /// A test for the empty PropertyGraph.AddVertex() method.
        /// </summary>
        [Test]
        public void CreateGenericPropertyGraphWithIdAndDescription()
        {

            var Random            = new Random();
            var GraphId           = (UInt64) Random.Next();
            var GraphDescription  =          Random.Next().ToString();
            var graph             = GraphFactory.CreateGenericPropertyGraph(GraphId, GraphDescription);

            // graph identification
            Assert.IsNotNull(graph,    "graph must not be null!");
            Assert.IsNotNull(graph.Id, "graph.Id must not be null!");

            Assert.AreEqual (GraphId, graph.Id, "The identification of the graph has not the expected value!");
            Assert.IsTrue(graph.Values.Contains(GraphId));

            // graph revision identification

            // graph label

            // graph description
            Assert.IsNotNull(graph["Description"], "graph.Description must not be null!");
            Assert.AreEqual(GraphDescription, graph["Description"], "The description of the graph has not the expected value!");

            // graph properties
            Assert.AreEqual(4, graph.Count(), "Instead of the expected number of KeyValuePairs the following property keys had been found: " + graph.Select(kvp => kvp.Key).Aggregate((a, b) => a + ", " + b) + "...");


        }

        #endregion

        #region CreateGenericPropertyGraphWithIdDescriptionAndGraphInitializer()

        /// <summary>
        /// A test for the empty PropertyGraph.AddVertex() method.
        /// </summary>
        [Test]
        public void CreateGenericPropertyGraphWithIdDescriptionAndGraphInitializer()
        {

            var Random            = new Random();
            var GraphId           = (UInt64) Random.Next();
            var GraphDescription  =          Random.Next().ToString();
            var PropertyKey       =          Random.Next().ToString();
            var PropertyValue     =          Random.Next();
            var graph             = GraphFactory.CreateGenericPropertyGraph(GraphId, GraphDescription, GraphInitializer: GraphInitializer => GraphInitializer.Set(PropertyKey, PropertyValue));

            // graph identification
            Assert.IsNotNull(graph,    "graph must not be null!");
            Assert.IsNotNull(graph.Id, "graph.Id must not be null!");

            Assert.AreEqual (GraphId, graph.Id, "The identification of the graph has not the expected value!");
            Assert.IsTrue(graph.Values.Contains(GraphId));

            // graph revision identification

            // graph label

            // graph description
            Assert.IsNotNull(graph["Description"], "graph.Description must not be null!");
            Assert.AreEqual(GraphDescription, graph["Description"], "The description of the graph has not the expected value!");
            Assert.IsTrue(graph.Values.Contains(GraphDescription));

            // graph properties
            Assert.AreEqual(5, graph.Count(), "Instead of the expected number of KeyValuePairs the following property keys had been found: " + graph.Select(kvp => kvp.Key).Aggregate((a, b) => a + ", " + b) + "...");
            Assert.IsTrue(graph.Keys.Contains(PropertyKey));
            Assert.IsTrue(graph.Values.Contains(PropertyValue));

        }

        #endregion

    }

}
