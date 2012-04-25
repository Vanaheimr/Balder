/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET <http://www.github.com/Vanaheimr/Blueprints.NET>
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

using de.ahzf.Illias.Commons;
using de.ahzf.Blueprints.HTTP.Server;
using de.ahzf.Blueprints.PropertyGraphs;
using de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable;
using de.ahzf.Hermod.Datastructures;

#endregion

namespace de.ahzf.Blueprints.UnitTests.GraphServerTests
{

    /// <summary>
    /// GraphServer initialization unit tests.
    /// </summary>
    [TestFixture]
    public class GraphServerInitializationTests : InitGraphServer
    {

        #region GraphServerConstructorTest()

        [Test]
        public void GraphServerConstructorTest()
        {

            using (var GraphServer = new GraphServer(new PropertyGraph(123UL), new IPPort(8080)) {
                                         ServerName = "GraphServer v0.1"
                                     } as IGraphServer)
            {

                Assert.IsNotNull(GraphServer);
                Assert.IsNotNull(GraphServer.ServerName);
                Assert.AreEqual("GraphServer v0.1", GraphServer.ServerName);

                var graphs = GraphServer.AllGraphs().ToList();
                Assert.IsNotNull(graphs);
                Assert.AreEqual(1, graphs.Count);

                var graph1 = graphs[0];
                Assert.IsNotNull(graph1);
                Assert.IsNotNull(graph1.IdKey);
                Assert.IsNotNull(graph1.RevIdKey);
                Assert.AreEqual(123UL, graph1.Id);

            }

        }

        #endregion

        #region AddPropertyGraphTest()

        [Test]
        public void AddPropertyGraphTest()
        {

            using (var GraphServer = new GraphServer(new PropertyGraph(123UL), new IPPort(8080)) {
                                         ServerName = "GraphServer v0.1"
                                     } as IGraphServer)
            {

                var graph = GraphServer.AddPropertyGraph(new PropertyGraph(256UL));
                Assert.IsNotNull(graph);
                Assert.IsNotNull(graph.IdKey);
                Assert.IsNotNull(graph.RevIdKey);
                Assert.AreEqual(256UL, graph.Id);

                var graphs = GraphServer.AllGraphs().ToList();
                Assert.IsNotNull(graphs);
                Assert.AreEqual(2, graphs.Count);

                var graphIds = (from _graph in graphs select _graph.Id).ToList();
                Assert.IsTrue(graphIds.Contains(123UL));
                Assert.IsTrue(graphIds.Contains(256UL));

            }

        }

        #endregion

        #region NewPropertyGraphTest()

        [Test]
        public void NewPropertyGraphTest()
        {

            using (var GraphServer = new GraphServer(new PropertyGraph(123UL), new IPPort(8080)) {
                                         ServerName = "GraphServer v0.1"
                                     } as IGraphServer)
            {

                var graph = GraphServer.NewPropertyGraph(512UL, "demo graph", g => g.SetProperty("hello", "world!"));
                Assert.IsNotNull(graph);
                Assert.IsNotNull(graph.IdKey);
                Assert.IsNotNull(graph.RevIdKey);
                Assert.AreEqual(512UL, graph.Id);
                Assert.IsTrue(graph.ContainsKey("hello"));
                Assert.IsTrue(graph.ContainsValue("world!"));
                Assert.IsTrue(graph.Contains("hello", "world!"));

                var graphs = GraphServer.AllGraphs().ToList();
                Assert.IsNotNull(graphs);
                Assert.AreEqual(2, graphs.Count);

                var graphIds = (from _graph in graphs select _graph.Id).ToList();
                Assert.IsTrue(graphIds.Contains(123UL));
                Assert.IsTrue(graphIds.Contains(512UL));

            }

        }

        #endregion

        //#region GraphIdAndHashCodeTest()

        //[Test]
        //public void GraphIdAndHashCodeTest()
        //{

        //    var graph1 = new PropertyGraph(123, null);
        //    var graph2 = new PropertyGraph(256, null);
        //    var graph3 = new PropertyGraph(123, null);

        //    Assert.IsNotNull(graph1.Id);
        //    Assert.IsNotNull(graph2.Id);
        //    Assert.IsNotNull(graph3.Id);

        //    Assert.IsNotNull(graph1.GetHashCode());
        //    Assert.IsNotNull(graph2.GetHashCode());
        //    Assert.IsNotNull(graph3.GetHashCode());

        //    Assert.AreEqual(graph1.Id, graph1.GetHashCode());
        //    Assert.AreEqual(graph2.Id, graph2.GetHashCode());
        //    Assert.AreEqual(graph3.Id, graph3.GetHashCode());

        //    Assert.AreEqual(graph1.Id, graph3.Id);
        //    Assert.AreEqual(graph1.GetHashCode(), graph3.GetHashCode());

        //}

        //#endregion

    }

}
