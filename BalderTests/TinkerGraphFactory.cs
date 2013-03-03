﻿/*
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
using System.Collections;

using de.ahzf.Illias.Commons.Votes;
using de.ahzf.Illias.Commons.Collections;
using de.ahzf.Vanaheimr.Blueprints.InMemory;
using de.ahzf.Vanaheimr.Blueprints;

#endregion

namespace de.ahzf.Vanaheimr.Balder.UnitTests
{

    public static class TinkerGraphFactory
    {

        public static IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object> CreateTinkerGraph()
        {

            var _TinkerGraph = GraphFactory.CreateGenericPropertyGraph(0, "The Tinker graph");

            _TinkerGraph.OnVertexAddition.OnVoting += (graph, vertex, vote) => {
                Console.WriteLine("I like all vertices!");
            };

            _TinkerGraph.OnVertexAddition.OnVoting += (graph, vertex, vote) =>
            {
                if (vertex.Id == 5) {
                    Console.WriteLine("I'm a Jedi!");
                    vote.Deny();
                }
            };

            var marko  = _TinkerGraph.AddVertex(v => v.Set("name", "marko"). Set("age",   29));
            var vadas  = _TinkerGraph.AddVertex(v => v.Set("name", "vadas"). Set("age",   27));
            var lop    = _TinkerGraph.AddVertex(v => v.Set("name", "lop").   Set("lang", "java"));
            var josh   = _TinkerGraph.AddVertex(v => v.Set("name", "josh").  Set("age",   32));
            var vader  = _TinkerGraph.AddVertex(v => v.Set("name", "darth vader"));
            var ripple = _TinkerGraph.AddVertex(v => v.Set("name", "ripple").Set("lang", "java"));
            var peter  = _TinkerGraph.AddVertex(v => v.Set("name", "peter"). Set("age",   35));

            Console.WriteLine("Number of vertices added: " + _TinkerGraph.Vertices().Count());

            marko.AsMutable().OnPropertyChanging += (sender, Key, oldValue, newValue, vote) =>
                Console.WriteLine("'" + Key + "' property changing: '" + oldValue + "' -> '" + newValue + "'");

            marko.AsMutable().OnPropertyChanged  += (sender, Key, oldValue, newValue) =>
                Console.WriteLine("'" + Key + "' property changed: '"  + oldValue + "' -> '" + newValue + "'");


            var _DynamicMarko = marko.AsDynamic();
            _DynamicMarko.age  += 100;
            _DynamicMarko.doIt  = (Action<String>) ((text) => Console.WriteLine("Some infos: " + text + "!"));
            _DynamicMarko.doIt(_DynamicMarko.name + "/" + marko.GetProperty("age") + "/");


            var e7  = _TinkerGraph.AddEdge(marko, vadas,  7,  "knows",   e => e.Set("weight", 0.5));
            var e8  = _TinkerGraph.AddEdge(marko, josh,   8,  "knows",   e => e.Set("weight", 1.0));
            var e9  = _TinkerGraph.AddEdge(marko, lop,    9,  "created", e => e.Set("weight", 0.4));

            var e10 = _TinkerGraph.AddEdge(josh,  ripple, 10, "created", e => e.Set("weight", 1.0));
            var e11 = _TinkerGraph.AddEdge(josh,  lop,    11, "created", e => e.Set("weight", 0.4));

            var e12 = _TinkerGraph.AddEdge(peter, lop,    12, "created", e => e.Set("weight", 0.2));

            return _TinkerGraph;

        }

    }

}