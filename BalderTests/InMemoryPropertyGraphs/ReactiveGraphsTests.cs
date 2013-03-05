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
    /// Unit tests for reactive generic property graphs.
    /// </summary>
    [TestFixture]
    public class ReactiveGraphsTests
    {

        #region CheckReactiveGraphs()

        /// <summary>
        /// A test for reactive property graphs.
        /// </summary>
        [Test]
        public void CheckReactiveGraphs()
        {

            var Vertices      = new List<IReadOnlyGenericPropertyVertex   <String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object>>();

            var Edges         = new List<IReadOnlyGenericPropertyEdge     <String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object>>();

            var MultiEdges    = new List<IReadOnlyGenericPropertyMultiEdge<String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object>>();

            var HyperEdges    = new List<IReadOnlyGenericPropertyHyperEdge<String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object>>();


            // Create an empty graph
            var _graph = GraphFactory.CreateGenericPropertyGraph_WithStringIds("DemoGraph");


            // Wire graph events...
            _graph.OnVertexAddition.   OnNotification += (g,  v)  =>  Vertices.  Add(v);
            _graph.OnEdgeAddition.     OnNotification += (g,  e)  =>  Edges.     Add(e);
            _graph.OnMultiEdgeAddition.OnNotification += (g, me)  =>  MultiEdges.Add(me);
            _graph.OnHyperEdgeAddition.OnNotification += (g, he)  =>  HyperEdges.Add(he);


            // Add some vertices, edges, multiedges and hyperedges
            DemoGraphFactory.CreateSimpleGraph(() => _graph);
            Assert.IsNotNull(_graph, "graph must not be null!");


            // Checks...
            Assert.AreEqual(5, Vertices.  Count);
            Assert.AreEqual(9, Edges.     Count);
            Assert.AreEqual(1, MultiEdges.Count);
            Assert.AreEqual(2, HyperEdges.Count);

        }

        #endregion

        #region CheckReactiveGraphsWithVoting()

        /// <summary>
        /// A test for reactive property graphs with voting.
        /// </summary>
        [Test]
        public void CheckReactiveGraphsWithVoting()
        {

            var Vertices      = new List<IReadOnlyGenericPropertyVertex   <String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object>>();

            var Edges         = new List<IReadOnlyGenericPropertyEdge     <String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object>>();

            var MultiEdges    = new List<IReadOnlyGenericPropertyMultiEdge<String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object>>();

            var HyperEdges    = new List<IReadOnlyGenericPropertyHyperEdge<String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object,
                                                                           String, Int64, String, String, Object>>();


            // Create an empty graph
            var _graph = GraphFactory.CreateGenericPropertyGraph_WithStringIds("DemoGraph");


            // Wire graph voting events... and deny all
            _graph.OnVertexAddition.   OnVoting += (g,  v, Vote)  =>  { if (v.Id.   Contains("1")) { Vote.VoteFor(false); } else  Vote.VoteFor(true); };
            _graph.OnEdgeAddition.     OnVoting += (g,  e, Vote)  =>  { if (e.Label.Contains("1")) { Vote.VoteFor(false); } else  Vote.VoteFor(true); };
            _graph.OnMultiEdgeAddition.OnVoting += (g, me, Vote)  =>  Vote.VoteFor(false);
            _graph.OnHyperEdgeAddition.OnVoting += (g, he, Vote)  =>  Vote.VoteFor(false);

            // Wire graph events...
            _graph.OnVertexAddition.   OnNotification += (g,  v)  =>  Vertices.  Add(v);
            _graph.OnEdgeAddition.     OnNotification += (g,  e)  =>  Edges.     Add(e);
            _graph.OnMultiEdgeAddition.OnNotification += (g, me)  =>  MultiEdges.Add(me);
            _graph.OnHyperEdgeAddition.OnNotification += (g, he)  =>  HyperEdges.Add(he);


            // Add some vertices, edges, multiedges and hyperedges
            var v1 = _graph.AddVertex("Vertex1", "Label1");
            Assert.IsNull(v1);
            var v2 = _graph.AddVertex("Vertex2", "Label2");
            Assert.IsNotNull(v2);
            var v3 = _graph.AddVertex("Vertex3", "Label3");
            Assert.IsNotNull(v3);

            var e1 = _graph.AddEdge(v2, "Label1", v3);
            Assert.IsNull(e1);
            var e2 = _graph.AddEdge(v2, "Label2", v3);
            Assert.IsNotNull(e2);


            // Checks...
            Assert.AreEqual(2, Vertices.  Count);
            Assert.AreEqual(1, Edges.     Count);
            Assert.AreEqual(0, MultiEdges.Count);
            Assert.AreEqual(0, HyperEdges.Count);

        }

        #endregion

    }

}

