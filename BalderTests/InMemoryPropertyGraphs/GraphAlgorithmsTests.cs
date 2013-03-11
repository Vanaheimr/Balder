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

using eu.Vanaheimr.Illias.Commons.Collections;
using eu.Vanaheimr.Balder.InMemory;
using eu.Vanaheimr.Balder;

#endregion

namespace eu.Vanaheimr.Balder.UnitTests.InMemoryPropertyGraphs
{

    /// <summary>
    /// Unit tests for creating generic property graphs by using the GraphFactory.
    /// </summary>
    [TestFixture]
    public class GraphAlgorithmsTests
    {

        #region GetGraphComponents_1Component()

        /// <summary>
        /// A test for the empty PropertyGraph.AddVertex() method.
        /// </summary>
        [Test]
        public void GetGraphComponents_1Component()
        {

            var graph = GraphFactory.CreateGenericPropertyGraph();

            var v1 = graph.AddVertex();
            var v2 = graph.AddVertex();
            var v3 = graph.AddVertex();

            var e1 = graph.AddEdge(v1, "label", v2);
            var e2 = graph.AddEdge(v2, "label", v3);
            var e3 = graph.AddEdge(v3, "label", v1);

            var GraphComponents = graph.Components(() => GraphFactory.CreateGenericPropertyGraph()).ToArray();

            Assert.AreEqual(1, GraphComponents.Length);

            var component1 = GraphComponents[0];
            Assert.AreEqual(3, component1.NumberOfVertices());
            Assert.AreEqual(3, component1.NumberOfEdges());

        }

        #endregion

        #region GetGraphComponents_2Components()

        /// <summary>
        /// A test for the empty PropertyGraph.AddVertex() method.
        /// </summary>
        [Test]
        public void GetGraphComponents_2Components()
        {

            var graph = GraphFactory.CreateGenericPropertyGraph();

            var v1 = graph.AddVertex();
            var v2 = graph.AddVertex();
            var v3 = graph.AddVertex();

            var v4 = graph.AddVertex();
            var v5 = graph.AddVertex();
            var v6 = graph.AddVertex();

            var e1 = graph.AddEdge(v1, "label", v2);
            var e2 = graph.AddEdge(v2, "label", v3);
            var e3 = graph.AddEdge(v3, "label", v1);

            var e4 = graph.AddEdge(v4, "label", v5);
            var e5 = graph.AddEdge(v5, "label", v6);
            var e6 = graph.AddEdge(v6, "label", v4);

            var GraphComponents = graph.Components(() => GraphFactory.CreateGenericPropertyGraph()).ToArray();

            Assert.AreEqual(2, GraphComponents.Length);

            var component1 = GraphComponents[0];
            Assert.AreEqual(3, component1.NumberOfVertices());
            Assert.AreEqual(3, component1.NumberOfEdges());

            var component2 = GraphComponents[1];
            Assert.AreEqual(3, component1.NumberOfVertices());
            Assert.AreEqual(3, component1.NumberOfEdges());

        }

        #endregion

        #region GetGraphComponents_2ComponentsWithLink()

        /// <summary>
        /// A test for the empty PropertyGraph.AddVertex() method.
        /// </summary>
        [Test]
        public void GetGraphComponents_2ComponentsWithLink()
        {

            var graph = GraphFactory.CreateGenericPropertyGraph();

            var v1 = graph.AddVertex();
            var v2 = graph.AddVertex();
            var v3 = graph.AddVertex();

            var v4 = graph.AddVertex();
            var v5 = graph.AddVertex();
            var v6 = graph.AddVertex();

            var e1 = graph.AddEdge(v1, "label", v2);
            var e2 = graph.AddEdge(v2, "label", v3);
            var e3 = graph.AddEdge(v3, "label", v1);

            var e4 = graph.AddEdge(v4, "label", v5);
            var e5 = graph.AddEdge(v5, "label", v6);
            var e6 = graph.AddEdge(v6, "label", v4);

            var e7 = graph.AddEdge(v1, "link", v4);

            var GraphComponents = graph.Components(() => GraphFactory.CreateGenericPropertyGraph(), e => e.Label == "label").ToArray();

            Assert.AreEqual(2, GraphComponents.Length);

            var component1 = GraphComponents[0];
            Assert.AreEqual(3, component1.NumberOfVertices());
            Assert.AreEqual(3, component1.NumberOfEdges());

            var component2 = GraphComponents[1];
            Assert.AreEqual(3, component1.NumberOfVertices());
            Assert.AreEqual(3, component1.NumberOfEdges());

        }

        #endregion

    }

}
