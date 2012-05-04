/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
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

using de.ahzf.Blueprints.PropertyGraphs;

#endregion

namespace de.ahzf.Blueprints.UnitTests
{

    /// <summary>
    /// Create a generic demo graph.
    /// </summary>
    public static class GenericDemoGraphFactory
    {

        /// <summary>
        /// Create a generic demo graph.
        /// </summary>
        public static IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object> Create()
        {

            var _graph = GraphFactory.CreateGenericPropertyGraph(1);

            // Before adding a vertex/edge/multiedge/hyperedge call the following delegates
            // which might deny the addition of the given graph element!
            _graph.OnVertexAdding    += (graph, vertex,    vote) => { Console.WriteLine("Vertex " + vertex.Id + " is announced to be added to graph " + graph.Id + "!"); };
            _graph.OnVertexAdding    += (graph, vertex,    vote) => { if (vertex.Id < 3) { Console.WriteLine("Addition of vertex " + vertex.Id + " denied!"); vote.Veto(); } };

            _graph.OnEdgeAdding      += (graph, edge,      vote) => { Console.WriteLine("Edge " + edge.Id + " is announced to be added to graph " + graph.Id + "!"); };
            _graph.OnEdgeAdding      += (graph, edge,      vote) => { if (edge.Id < 2) { Console.WriteLine("Addition of edge " + edge.Id + " denied!"); vote.Veto(); } };

            _graph.OnMultiEdgeAdding += (graph, multiedge, vote) => { Console.WriteLine("MultiEdge " + multiedge.Id + " is announced to be added to graph " + graph.Id + "!"); };
            _graph.OnMultiEdgeAdding += (graph, multiedge, vote) => { if (multiedge.Id < 2) { Console.WriteLine("Addition of multiedge " + multiedge.Id + " denied!"); vote.Veto(); } };

            _graph.OnHyperEdgeAdding += (graph, hyperedge, vote) => { Console.WriteLine("HyperEdge " + hyperedge.Id + " is announced to be added to graph " + graph.Id + "!"); };
            _graph.OnHyperEdgeAdding += (graph, hyperedge, vote) => { if (hyperedge.Id < 2) { Console.WriteLine("Addition of hyperedge " + hyperedge.Id + " denied!"); vote.Veto(); } };

            // Call the following delegate for every vertex/edge/multiedge/hyperedge added
            _graph.OnVertexAdded    += (graph, vertex)       => { Console.WriteLine("Vertex "    + vertex.Id    + " was added to graph " + graph.Id + "!"); };
            _graph.OnEdgeAdded      += (graph, edge)         => { Console.WriteLine("Edge "      + edge.Id      + " was added to graph " + graph.Id + "!"); };
            _graph.OnMultiEdgeAdded += (graph, multiedge)    => { Console.WriteLine("MultiEdge " + multiedge.Id + " was added to graph " + graph.Id + "!"); };
            _graph.OnHyperEdgeAdded += (graph, hyperedge)    => { Console.WriteLine("HyperEdge " + hyperedge.Id + " was added to graph " + graph.Id + "!"); };


            // The following two vertices will not be added because of the veto vote above!
            // (the method call will return null!)
            var _Alice1 = _graph.AddVertex();
            var _Alice2 = _graph.AddVertex(v => v.SetProperty("name", "Alice"));

            var _Alice  = _graph.AddVertex(v => v.SetProperty("name", "Alice").SetProperty("age", 18));
            var _Bob    = _graph.AddVertex(v => v.SetProperty("name", "Bob").  SetProperty("age", 20));
            var _Carol  = _graph.AddVertex(v => v.SetProperty("name", "Carol").SetProperty("age", 22));
            var _Dave   = _graph.AddVertex(v => v.SetProperty("name", "Dave"). SetProperty("age", 23));

            // The following edge will not be added because of the veto vote above!
            // (the method call will return null!)
            var e1      = _graph.AddEdge(_Alice, _Bob, "knows", edge => edge.SetProperty("weight", 0.5));
            var e2      = _graph.AddEdge(_Alice, _Bob, "knows", edge => edge.SetProperty("weight", 0.5));


            // A hyperedge connectes multiple vertices
            var he1     = _graph.AddHyperEdge("all", hyperedge => hyperedge.SetProperty("a", "b"), _Alice, _Bob, _Carol, _Dave);
            var he2     = _graph.AddHyperEdge("all", hyperedge => hyperedge.SetProperty("c", "d"), _Alice, _Bob, _Carol, _Dave);


            _Alice.UseProperty("name",
                               OnSuccess: (key, value) => { Console.WriteLine(key + " => " + value); },
                               OnError:   (key)        => { Console.WriteLine("Key " + key + " not found!"); });

            //var _AliceSubgraph = _Alice.AsSubgraph;
            //_AliceSubgraph.AddVertex(1, v => v.SetProperty("name", "SubAlice1"));
            //_AliceSubgraph.AddVertex(2, v => v.SetProperty("name", "SubAlice2"));


            return _graph;

        }

    }

}
