/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET <http://www.github.com/ahzf/Blueprints.NET>
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

using de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable;
using de.ahzf.Blueprints.PropertyGraphs;
using System.Threading;
using de.ahzf.Illias.Commons;

#endregion

namespace de.ahzf.Blueprints.UnitTests
{

    public static class GenericDemoGraphFactory
    {


        #region (private) NewIds

        private static Int64 _NewVertexId;
        private static Int64 _NewEdgeId;
        private static Int64 _NewMultiEdgeId;
        private static Int64 _NewHyperEdgeId;

        /// <summary>
        /// Return a new VertexId.
        /// </summary>
        private static UInt64 NewVertexId
        {
            get
            {
                var _NewLocalId = Interlocked.Increment(ref _NewVertexId);
                return (UInt64)_NewLocalId;
            }
        }

        /// <summary>
        /// Return a new NewEdgeId.
        /// </summary>
        private static UInt64 NewEdgeId
        {
            get
            {
                var _NewLocalId = Interlocked.Increment(ref _NewEdgeId);
                return (UInt64)_NewLocalId;
            }
        }

        /// <summary>
        /// Return a new NewMultiEdgeId.
        /// </summary>
        private static UInt64 NewMultiEdgeId
        {
            get
            {
                var _NewLocalId = Interlocked.Increment(ref _NewMultiEdgeId);
                return (UInt64)_NewLocalId;
            }
        }

        /// <summary>
        /// Return a new NewHyperEdgeId.
        /// </summary>
        private static UInt64 NewHyperEdgeId
        {
            get
            {
                var _NewLocalId = Interlocked.Increment(ref _NewHyperEdgeId);
                return (UInt64)_NewLocalId;
            }
        }

        #endregion

        #region (private) NewRevId

        /// <summary>
        /// Return a new random RevId.
        /// </summary>
        private static Int64 NewRevId
        {
            get
            {
                return (Int64) UniqueTimestamp.Ticks;
            }
        }

        #endregion


        public static IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object> CreateGenericDemoGraph()
        {

            #region Create a new generic property graph

            var _graph = new GenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object,
                                                  UInt64, Int64, String, String, Object>(1,
                                                                                         GraphDBOntology.Id().Suffix,
                                                                                         GraphDBOntology.RevId().Suffix,
                                                                                         GraphDBOntology.Description().Suffix,
                                                                                         (graph) => GenericDemoGraphFactory.NewVertexId,

                                                                                         GraphDBOntology.Id().Suffix,
                                                                                         GraphDBOntology.RevId().Suffix,
                                                                                         GraphDBOntology.Description().Suffix,
                                                                                         (graph) => GenericDemoGraphFactory.NewEdgeId,

                                                                                         GraphDBOntology.Id().Suffix,
                                                                                         GraphDBOntology.RevId().Suffix,
                                                                                         GraphDBOntology.Description().Suffix,
                                                                                         (graph) => GenericDemoGraphFactory.NewMultiEdgeId,

                                                                                         GraphDBOntology.Id().Suffix,
                                                                                         GraphDBOntology.RevId().Suffix,
                                                                                         GraphDBOntology.Description().Suffix,
                                                                                         (graph) => GenericDemoGraphFactory.NewHyperEdgeId)
                                                  
                                                  as IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                                           UInt64, Int64, String, String, Object,
                                                                           UInt64, Int64, String, String, Object,
                                                                           UInt64, Int64, String, String, Object>;

            #endregion

            // Before adding a vertex/edge call the following delegates
            // which might deny the addition of the given vertex!
            _graph.OnVertexAdding    += (graph, vertex,    vote) => { Console.WriteLine("vertex " + vertex.Id + " is announced to be added to graph " + graph.Id + "!"); };
            _graph.OnVertexAdding    += (graph, vertex,    vote) => { if (vertex.Id < 3) { Console.WriteLine("Addition of vertex " + vertex.Id + " denied!"); vote.Veto(); } };

            _graph.OnEdgeAdding      += (graph, edge,      vote) => { Console.WriteLine("edge " + edge.Id + " is announced to be added to graph " + graph.Id + "!"); };
            _graph.OnEdgeAdding      += (graph, edge,      vote) => { if (edge.Id < 2) { Console.WriteLine("Addition of edge " + edge.Id + " denied!"); vote.Veto(); } };

            _graph.OnMultiEdgeAdding += (graph, multiedge, vote) => { Console.WriteLine("multiedge " + multiedge.Id + " is announced to be added to graph " + graph.Id + "!"); };
            _graph.OnMultiEdgeAdding += (graph, multiedge, vote) => { if (multiedge.Id < 2) { Console.WriteLine("Addition of edge " + multiedge.Id + " denied!"); vote.Veto(); } };

            // Call the following delegate for every vertex/edge added
            _graph.OnVertexAdded  += (graph, vertex)       => { Console.WriteLine("vertex " + vertex.Id + " was added to graph " + graph.Id + "!"); };

            _graph.OnEdgeAdded    += (graph, edge)         => { Console.WriteLine("edge "   + edge.Id   + " was added to graph " + graph.Id + "!"); };


            // The following two vertices will not be added because of the veto vote above!
            // (the method call will return null!)
            var _Alice1 = _graph.AddVertex();
            var _Alice2 = _graph.AddVertex(v => v.SetProperty("name", "Alice"));

            var _Alice  = _graph.AddVertex(10, v => v.SetProperty("name", "Alice").SetProperty("age", 18));
            var _Bob    = _graph.AddVertex(20, v => v.SetProperty("name", "Bob").  SetProperty("age", 20));
            var _Carol  = _graph.AddVertex(30, v => v.SetProperty("name", "Carol").SetProperty("age", 22));
            var _Dave   = _graph.AddVertex(40, v => v.SetProperty("name", "Dave"). SetProperty("age", 23));

            // The following edge will not be added because of the veto vote above!
            // (the method call will return null!)
            var e1      = _graph.AddEdge(_Alice, _Bob, 1, "knows", e => e.SetProperty("weight", 0.5));
            var e2      = _graph.AddEdge(_Alice, _Bob, 2, "knows", e => e.SetProperty("weight", 0.5));


            // A multiedge connectes multiple vertices
            //var me1     = _graph.AddMultiEdge(1, "all", me => me.SetProperty("a", "b"),  _Alice, _Bob, _Carol, _Dave);


            //var _AliceSubgraph = _Alice as IGenericPropertyGraph<UInt64, Int64, String, String, Object,
            //                                                     UInt64, Int64, String, String, Object,
            //                                                     UInt64, Int64, String, String, Object,
            //                                                     UInt64, Int64, String, String, Object>;

            //_AliceSubgraph.AddVertex(1, v => v.SetProperty("name", "SubAlice1"));
            //_AliceSubgraph.AddVertex(2, v => v.SetProperty("name", "SubAlice2"));


            return _graph;

        }

    }

}
