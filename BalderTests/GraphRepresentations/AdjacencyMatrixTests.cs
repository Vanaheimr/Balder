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
using eu.Vanaheimr.Blueprints.InMemory;
using eu.Vanaheimr.Blueprints;
using eu.Vanaheimr.Balder.DependentGraphs;

#endregion

namespace eu.Vanaheimr.Balder.UnitTests.InMemoryPropertyGraphs
{

    [TestFixture]
    public class AdjacencyMatrixGraphsTests
    {

        #region AdjacencyMatrixTest()

        /// <summary>
        /// Test an adjacency matrix graph.
        /// </summary>
        [Test]
        public void AdjacencyMatrixTest()
        {

            var graph = new AdjacencyMatrixGraph<String>(100);
            Assert.IsNotNull(graph);
            Assert.AreEqual(100, graph.MaxNumberOfVertices);

            graph.AddEdge(1, "likes", 2);
            Assert.AreEqual(1,          graph.Count());
            Assert.AreEqual(1,          graph.First().OutVertex);
            Assert.AreEqual("likes",    graph.First().EdgeData);
            Assert.AreEqual(2,          graph.First().InVertex);
            Assert.AreEqual("2",        graph.Out(1).Select(v => v.ToString()).AggString());
            Assert.AreEqual("likes",    graph.OutEdges(1).AggString());
            Assert.AreEqual("",         graph.In(1).Select(v => v.ToString()).AggString());
            Assert.AreEqual("",         graph.InEdges(1).AggString());

            graph.AddEdge(2, "loves", 1);
            Assert.AreEqual(2,          graph.Count());
            Assert.AreEqual(2,          graph.Skip(1).First().OutVertex);
            Assert.AreEqual("loves",    graph.Skip(1).First().EdgeData);
            Assert.AreEqual(1,          graph.Skip(1).First().InVertex);
            Assert.AreEqual("1",        graph.Out(2).Select(v => v.ToString()).AggString());
            Assert.AreEqual("loves",    graph.OutEdges(2).AggString());
            Assert.AreEqual("1",        graph.In(2).Select(v => v.ToString()).AggString());
            Assert.AreEqual("loves",    graph.InEdges(2).AggString());

            graph.RemoveEdge(2, 1);
            Assert.AreEqual(1, graph.Count());
            Assert.AreEqual(1,          graph.First().OutVertex);
            Assert.AreEqual("likes",    graph.First().EdgeData);
            Assert.AreEqual(2,          graph.First().InVertex);

        }

        #endregion

        #region AdjacencyMatrix_DependentGraphTest()

        /// <summary>
        /// Test an adjacency matrix graph as a dependent graph of a property graph.
        /// </summary>
        [Test]
        public void AdjacencyMatrix_DependentGraphTest()
        {

            var graph            = GraphFactory.CreateGenericPropertyGraph();
            var AdjacencyMatrix1 = graph.ToAdjacencyMatrixGraph<Double>("weight", weight => (Double) weight, 10, v => true, e => true, true);

            var v1 = graph.AddVertex();
            var v2 = graph.AddVertex();

            var e1 = graph.AddEdge(v1, "lala", v2, e => e.SetProperty("weight", 3.14159));
            var e2 = graph.AddEdge(v2, "lala", v1, e => e.SetProperty("weight", 2.71828));

            Assert.AreEqual(2, AdjacencyMatrix1.Count());

            var AdjacencyMatrix2 = graph.ToAdjacencyMatrixGraph<Double>("weight", MaxNumberOfVertices: 10);

            Assert.AreEqual(2, AdjacencyMatrix2.Count());

            var v3 = graph.AddVertex();
            var e3 = graph.AddEdge(v1, "lala", v3, e => e.SetProperty("weight", 9.81));

            Assert.AreEqual(3, AdjacencyMatrix1.Count());
            Assert.AreEqual(3, AdjacencyMatrix2.Count());

            graph.RemoveEdges(e1);

            Assert.AreEqual(2, AdjacencyMatrix1.Count());
            Assert.AreEqual(2, AdjacencyMatrix2.Count());

        }

        #endregion


    }

}
