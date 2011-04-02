/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET
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
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

using de.ahzf.blueprints;
using de.ahzf.blueprints.Datastructures;
using de.ahzf.blueprints.InMemory.PropertyGraph;

#endregion

namespace de.ahzf.Pipes.UnitTests
{

    public static class TinkerGraphFactory
    {

        public static IPropertyGraph CreateTinkerGraph()
        {

            var _TinkerGraph = new InMemoryPropertyGraph();

            var marko  = _TinkerGraph.AddVertex(new PropertyVertex(new VertexId("1"), v => v.SetProperty("name", "marko"). SetProperty("age",   29)))    as IPropertyVertex;
            var vadas  = _TinkerGraph.AddVertex(new PropertyVertex(new VertexId("2"), v => v.SetProperty("name", "vadas"). SetProperty("age",   27)))    as IPropertyVertex;
            var lop    = _TinkerGraph.AddVertex(new PropertyVertex(new VertexId("3"), v => v.SetProperty("name", "lop").   SetProperty("lang", "java"))) as IPropertyVertex;
            var josh   = _TinkerGraph.AddVertex(new PropertyVertex(new VertexId("4"), v => v.SetProperty("name", "josh").  SetProperty("age",   32)))    as IPropertyVertex;
            var ripple = _TinkerGraph.AddVertex(new PropertyVertex(new VertexId("5"), v => v.SetProperty("name", "ripple").SetProperty("lang", "java"))) as IPropertyVertex;
            var peter  = _TinkerGraph.AddVertex(new VertexId("6"), v => v.SetProperty("name", "peter"). SetProperty("age",   35));

            marko.CollectionChanged += (o, p) => Console.WriteLine("CollChanged: " + p.Action + " - " + (p.NewItems as IList)[0] + "/" + (p.NewItems as IList)[1]);
            marko.PropertyChanged   += (o, p) => Console.WriteLine("PropChanging: " + p.PropertyName);
            marko.PropertyChanged   += (o, p) => Console.WriteLine("PropChanged: "  + p.PropertyName);

            marko.SetProperty("event", "raised 1!");
            marko.SetProperty("event", "raised 2!");

            var _dynamicMarko = marko.AsDynamic();
            _dynamicMarko.lala    = "123";
            //_dynamicMarko["lala"] = "456";
            _dynamicMarko.doIt = (Action<String>) ((name) => Console.WriteLine("Hello " + name + "!"));
            _dynamicMarko.doIt("world");

            var _MarkosName = _dynamicMarko.name;
            var _MarkosAge  = _dynamicMarko.age;
            var _MarkosLala = _dynamicMarko.lala;
            Console.WriteLine(_MarkosName + "/" + _MarkosAge + "/" + _MarkosLala);

            var _MarkosNewAge = marko.GetProperty("age");


            var e7  = _TinkerGraph.AddEdge(marko, vadas,  new EdgeId("7"),  "knows",   e => e.SetProperty("weight", 0.5));
            var e8  = _TinkerGraph.AddEdge(marko, josh,   new EdgeId("8"),  "knows",   e => e.SetProperty("weight", 1.0));
            var e9  = _TinkerGraph.AddEdge(marko, lop,    new EdgeId("9"),  "created", e => e.SetProperty("weight", 0.4));

            var e10 = _TinkerGraph.AddEdge(josh,  ripple, new EdgeId("10"), "created", e => e.SetProperty("weight", 1.0));
            var e11 = _TinkerGraph.AddEdge(josh,  lop,    new EdgeId("11"), "created", e => e.SetProperty("weight", 0.4));

            var e12 = _TinkerGraph.AddEdge(peter, lop,    new EdgeId("12"), "created", e => e.SetProperty("weight", 0.2));

            return _TinkerGraph as IPropertyGraph;

        }

    }

}
