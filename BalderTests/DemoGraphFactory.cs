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
using System.Collections;

using eu.Vanaheimr.Illias.Commons.Votes;
using eu.Vanaheimr.Illias.Commons.Collections;
using eu.Vanaheimr.Balder.InMemory;
using eu.Vanaheimr.Balder;

#endregion

namespace eu.Vanaheimr.Balder.UnitTests
{

    public static class assa
    {

        //public static T Set2Property<T, K, V>(this T Properties, K Key, V Value)
        //    where T : IProperties<K, V>
        //    where K : IEquatable<K>, IComparable<K>, IComparable
        //{
        //    return Properties;
        //}

    }


    public static class DemoGraphFactory
    {

        // Vertex labels
        public const String person  = "person";
        public const String pet     = "pet";

        // Vertex property keys
        public const String age     = "age";

        // Edge labels
        public const String knows   = "knows";
        public const String loves   = "loves";

        // Edge property keys
        public const String since   = "since";


        #region Create()

        /// <summary>
        /// Create a generic demo graph.
        /// </summary>
        public static IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object> Create()
        {

            var _graph = GraphFactory.CreateGenericPropertyGraph(0);

            // Before adding a vertex/edge/multiedge/hyperedge call the following delegates
            // which might deny the addition of the given graph element!
            _graph.OnVertexAddition.OnVoting += (graph, vertex,    vote) => { Console.WriteLine("Vertex " + vertex.Id + " is announced to be added to graph " + graph.Id + "!"); };
            _graph.OnVertexAddition.OnVoting += (graph, vertex,    vote) => {
                if (vertex.Id < 2) { Console.WriteLine("Addition of vertex " + vertex.Id + " denied!"); vote.Deny(); }
            };

            _graph.OnEdgeAddition.OnVoting   += (graph, edge,      vote) => { Console.WriteLine("Edge " + edge.Id + " is announced to be added to graph " + graph.Id + "!"); };
            _graph.OnEdgeAddition.OnVoting   += (graph, edge,      vote) => {
                if (edge.Id < 1) { Console.WriteLine("Addition of edge " + edge.Id + " denied!"); vote.Deny(); }
            };

            _graph.OnMultiEdgeAddition.OnVoting += (graph, multiedge, vote) => { Console.WriteLine("MultiEdge " + multiedge.Id + " is announced to be added to graph " + graph.Id + "!"); };
            _graph.OnMultiEdgeAddition.OnVoting += (graph, multiedge, vote) => {
                if (multiedge.Id < 1) { Console.WriteLine("Addition of multiedge " + multiedge.Id + " denied!"); vote.Deny(); }
            };

            _graph.OnHyperEdgeAddition.OnVoting += (graph, hyperedge, vote) => { Console.WriteLine("HyperEdge " + hyperedge.Id + " is announced to be added to graph " + graph.Id + "!"); };
            _graph.OnHyperEdgeAddition.OnVoting += (graph, hyperedge, vote) => {
                if (hyperedge.Id < 1) { Console.WriteLine("Addition of hyperedge " + hyperedge.Id + " denied!"); vote.Deny(); }
            };

            // Call the following delegate for every vertex/edge/multiedge/hyperedge added
            _graph.OnVertexAddition.OnNotification += (graph, vertex) => { Console.WriteLine("Vertex " + vertex.Id + " was added to graph " + graph.Id + "!"); };
            _graph.OnEdgeAddition.OnNotification   += (graph, edge)   => { Console.WriteLine("Edge " + edge.Id + " was added to graph " + graph.Id + "!"); };
            _graph.OnMultiEdgeAddition.OnNotification += (graph, multiedge)    => { Console.WriteLine("MultiEdge " + multiedge.Id + " was added to graph " + graph.Id + "!"); };
            _graph.OnHyperEdgeAddition.OnNotification += (graph, hyperedge)    => { Console.WriteLine("HyperEdge " + hyperedge.Id + " was added to graph " + graph.Id + "!"); };


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
            var e1      = _graph.AddEdge(_Alice, _Bob,   "knows", edge => edge.SetProperty("weight", 0.5));
            var e2      = _graph.AddEdge(_Alice, _Bob,   "knows", edge => edge.SetProperty("weight", 0.5));
            var e3      = _graph.AddEdge(_Alice, _Bob,   "likes", edge => edge.SetProperty("weight", 1.0));
            var e4      = _graph.AddEdge(_Bob,   _Alice, "likes", edge => edge.SetProperty("weight", 0.1));
            var e5      = _graph.AddEdge(_Alice, _Carol, "likes", edge => edge.SetProperty("weight", 0.3));


            // A hyperedge connectes multiple vertices
            var he1     = _graph.AddHyperEdge("all", hyperedge => hyperedge.SetProperty("a", "b"), null, _Alice, _Bob, _Carol, _Dave);
            var he2     = _graph.AddHyperEdge("all", hyperedge => hyperedge.SetProperty("c", "d"), null, _Alice, _Bob, _Carol, _Dave);


            _Alice.UseProperty("name",
                               OnSuccess: (key, value) => { Console.WriteLine(key + " => " + value); },
                               OnError:   (key)        => { Console.WriteLine("Key " + key + " not found!"); });

            _Alice.UseProperty("name",
                               OnSuccess: (v, key, value) => { Console.WriteLine(key + " => " + value); },
                               OnError:   (v, key)        => { Console.WriteLine("Key " + key + " not found!"); });

            //var _AliceSubgraph = _Alice.AsSubgraph;
            //_AliceSubgraph.AddVertex(1, v => v.SetProperty("name", "SubAlice1"));
            //_AliceSubgraph.AddVertex(2, v => v.SetProperty("name", "SubAlice2"));


            return _graph;

        }

        #endregion

        #region CreateIntegerDemoGraph(GraphCreator)

        public static IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object>

            CreateIntegerDemoGraph(Func<IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object>> GraphCreator)

        {

            var _Graph = GraphCreator();

            _Graph.OnVertexAddition.OnVoting += (graph, vertex, vote) => { Console.WriteLine("OnVertexAdding1() called!"); };
            _Graph.OnVertexAddition.OnVoting += (graph, vertex, vote) => { Console.WriteLine("OnVertexAdding2() called!"); if (vertex.Id < 2) vote.Deny(); };
            _Graph.OnVertexAddition.OnVoting += (graph, vertex, vote) => { Console.WriteLine("OnVertexAdding3() called!"); };


            var _Alice1 = _Graph.AddVertex();
            var _Alice2 = _Graph.AddVertex(v => v.Set("name", "Alice"));

            var _Alice = _Graph.AddVertex(v => v.Set("name", "Alice").Set("age", 18));
            var _Bob   = _Graph.AddVertex(v => v.Set("name", "Bob").  Set("age", 20));
            var _Carol = _Graph.AddVertex(v => v.Set("name", "Carol").Set("age", 22));
            var _Dave  = _Graph.AddVertex(v => v.Set("name", "Dave"). Set("age", 23));

            //var e7 = _Graph.AddEdge(marko, vadas, new EdgeId("7"), "knows", e => e.SetProperty("weight", 0.5));
            //var e8 = _Graph.AddEdge(marko, josh, new EdgeId("8"), "knows", e => e.SetProperty("weight", 1.0));
            //var e9 = _Graph.AddEdge(marko, lop, new EdgeId("9"), "created", e => e.SetProperty("weight", 0.4));

            //var e10 = _Graph.AddEdge(josh, ripple, new EdgeId("10"), "created", e => e.SetProperty("weight", 1.0));
            //var e11 = _Graph.AddEdge(josh, lop, new EdgeId("11"), "created", e => e.SetProperty("weight", 0.4));

            //var e12 = _Graph.AddEdge(peter, lop, new EdgeId("12"), "created", e => e.SetProperty("weight", 0.2));

            return _Graph;

        }

        #endregion

        #region CreateSimpleGraph(GraphCreator = null)

        public static IGenericPropertyGraph<String, Int64, String, String, Object,
                                            String, Int64, String, String, Object,
                                            String, Int64, String, String, Object,
                                            String, Int64, String, String, Object>

            CreateSimpleGraph(Func<IGenericPropertyGraph<String, Int64, String, String, Object,
                                                         String, Int64, String, String, Object,
                                                         String, Int64, String, String, Object,
                                                         String, Int64, String, String, Object>> GraphCreator = null)

        {

            var _graph = (GraphCreator != null) ? GraphCreator()
                                                : GraphFactory.CreateGenericPropertyGraph_WithStringIds("DemoGraph");

            var _Alice              = _graph.AddVertex("Alice", person, v => v.SetProperty(age, 18));
            var _Bob                = _graph.AddVertex("Bob",   person, v => v.SetProperty(age, 20));
            var _Carol              = _graph.AddVertex("Carol", person, v => v.SetProperty(age, 22));
            var _Dave               = _graph.AddVertex("Dave",  person, v => v.SetProperty(age, 23));

            var _rex                = _graph.AddVertex("Rex",   pet);

            // DoubleEdges: v <=> v
            var _e_k01              = _graph.AddDoubleEdge(_Alice, knows, _Bob,   EdgeInitializer: e => e.SetProperty(since, new DateTime(2000, 08, 01)));
            var _e_k02              = _graph.AddDoubleEdge(_Alice, knows, _Carol, EdgeInitializer: e => e.SetProperty(since, new DateTime(2001, 03, 10)));
            var _e_k03              = _graph.AddDoubleEdge(_Bob,   knows, _Carol, EdgeInitializer: e => e.SetProperty(since, new DateTime(2002, 12, 23)));

            // Edges: v -> v
            var _Alice_loves_Bob    = _graph.AddEdge("Alice -loves-> Bob",   _Alice, loves, _Bob);
            var _Bob_loves_Carol    = _graph.AddEdge("Bob -loves-> Carol",   _Bob,   loves, _Carol);
            var _Carol_loves_Alice  = _graph.AddEdge("Carol -loves-> Alice", _Carol, loves, _Alice);

            // Multiedges:
            var _ItsComplicated     = _graph.AddMultiEdge("It's complicated", null, _Alice_loves_Bob, _Bob_loves_Carol, _Carol_loves_Alice);

            // Hyperedges:
            var _AllPersons         = _graph.AddHyperEdge("All persons",  "index", he => { }, v => v.Label == person);
            var _Clique01           = _graph.AddHyperEdge("Clique01",     "he",    he => { }, null, _Alice, _Bob, _Carol);

            return _graph;

        }

        #endregion

        #region CreateStringIdGraph(GraphCreator = null)

        public static IGenericPropertyGraph<String, Int64, String, String, Object,
                                            String, Int64, String, String, Object,
                                            String, Int64, String, String, Object,
                                            String, Int64, String, String, Object>

            CreateStringIdGraph(Func<IGenericPropertyGraph<String, Int64, String, String, Object,
                                                           String, Int64, String, String, Object,
                                                           String, Int64, String, String, Object,
                                                           String, Int64, String, String, Object>> GraphCreator = null)

        {

            var _graph = (GraphCreator != null) ? GraphCreator() : GraphFactory.CreateGenericPropertyGraph_WithStringIds("DemoGraph");

            //_graph.OnVertexAddition.OnVoting += (graph, vertex, vote) => { Console.WriteLine("OnVertexAdding1() called!"); };
            //_graph.OnVertexAddition.OnVoting += (graph, vertex, vote) => { Console.WriteLine("OnVertexAdding2() called!"); if (vertex.Id < 2) vote.Deny(); };
            //_graph.OnVertexAddition.OnVoting += (graph, vertex, vote) => { Console.WriteLine("OnVertexAdding3() called!"); };


            var _Alice          = _graph.AddVertex("Alice", person, v => v.SetProperty(age, 18));
            var _Bob            = _graph.AddVertex("Bob",   person, v => v.SetProperty(age, 20));
            var _Carol          = _graph.AddVertex("Carol", person, v => v.SetProperty(age, 22));
            var _Dave           = _graph.AddVertex("Dave",  person, v => v.SetProperty(age, 23));

            // v <=> v
            var _e01            = _graph.AddDoubleEdge(_Alice, knows, _Bob,   EdgeInitializer: e => e.SetProperty(since, new DateTime(2000, 08, 01)));
            var _e02            = _graph.AddDoubleEdge(_Alice, knows, _Carol, EdgeInitializer: e => e.SetProperty(since, new DateTime(2002, 04, 12)));
            var _e03            = _graph.AddDoubleEdge(_Bob,   knows, _Carol, EdgeInitializer: e => e.SetProperty(since, new DateTime(2002, 04, 12)));

            // v -> v
            var _e13            = _graph.AddEdge(_Alice, loves, _Bob,   e => e.SetProperty(since, ""));
            var _e14            = _graph.AddEdge(_Bob,   loves, _Carol, e => e.SetProperty(since, ""));
            var _e15            = _graph.AddEdge(_Carol, loves, _Alice, e => e.SetProperty(since, ""));

            var _AllPersons     = _graph.AddHyperEdge("All persons",  "index", he => { }, v => v.Label == person);
            var _PersonGroup1   = _graph.AddHyperEdge("PersonGroup1", "he",    he => { }, null, _Alice, _Bob, _Carol);


            return _graph;

        }

        #endregion

    }

}
