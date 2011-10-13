/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
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
using System.Linq;
using System.Collections;

using de.ahzf.Blueprints.PropertyGraphs;
using de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable;

#endregion

namespace de.ahzf.Blueprints.UnitTests
{

    public static class TinkerGraphFactory
    {

        public static PropertyGraph CreateTinkerGraph()
        {

            var _TinkerGraph = new PropertyGraph();

            _TinkerGraph.OnVertexAdding += (graph, vertex, vote) => {
                Console.WriteLine("I like all vertices!");
            };

            _TinkerGraph.OnVertexAdding += (graph, vertex, vote) =>
            {
                if (vertex.Id == 5) {
                    Console.WriteLine("I'm a Jedi!");
                    vote.Veto();
                }
            };

            var marko  = _TinkerGraph.AddVertex(1, v => v.SetProperty("name", "marko"). SetProperty("age",   29));
            var vadas  = _TinkerGraph.AddVertex(2, v => v.SetProperty("name", "vadas"). SetProperty("age",   27));
            var lop    = _TinkerGraph.AddVertex(3, v => v.SetProperty("name", "lop").   SetProperty("lang", "java"));
            var josh   = _TinkerGraph.AddVertex(4, v => v.SetProperty("name", "josh").  SetProperty("age",   32));
            var vader  = _TinkerGraph.AddVertex(5, v => v.SetProperty("name", "darth vader"));
            var ripple = _TinkerGraph.AddVertex(6, v => v.SetProperty("name", "ripple").SetProperty("lang", "java"));
            var peter  = _TinkerGraph.AddVertex(7, v => v.SetProperty("name", "peter"). SetProperty("age",   35));

            Console.WriteLine("Number of vertices added: " + _TinkerGraph.Vertices().Count());

            marko.OnPropertyChanging += (sender, Key, oldValue, newValue, vote) =>
                Console.WriteLine("'" + Key + "' property changing: '" + oldValue + "' -> '" + newValue + "'");

            marko.OnPropertyChanged  += (sender, Key, oldValue, newValue)       =>
                Console.WriteLine("'" + Key + "' property changed: '"  + oldValue + "' -> '" + newValue + "'");


            var _DynamicMarko = marko.AsDynamic();
            _DynamicMarko.age  += 100;
            _DynamicMarko.doIt  = (Action<String>) ((text) => Console.WriteLine("Some infos: " + text + "!"));
            _DynamicMarko.doIt(_DynamicMarko.name + "/" + marko.GetProperty("age") + "/");


            var e7  = _TinkerGraph.AddEdge(marko, vadas,  7,  "knows",   e => e.SetProperty("weight", 0.5));
            var e8  = _TinkerGraph.AddEdge(marko, josh,   8,  "knows",   e => e.SetProperty("weight", 1.0));
            var e9  = _TinkerGraph.AddEdge(marko, lop,    9,  "created", e => e.SetProperty("weight", 0.4));

            var e10 = _TinkerGraph.AddEdge(josh,  ripple, 10, "created", e => e.SetProperty("weight", 1.0));
            var e11 = _TinkerGraph.AddEdge(josh,  lop,    11, "created", e => e.SetProperty("weight", 0.4));

            var e12 = _TinkerGraph.AddEdge(peter, lop,    12, "created", e => e.SetProperty("weight", 0.2));

            return _TinkerGraph;

        }

    }

}
