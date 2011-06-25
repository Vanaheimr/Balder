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
using System.Collections;

using de.ahzf.Blueprints.PropertyGraph;
using de.ahzf.Blueprints.PropertyGraph.InMemory;

#endregion

namespace de.ahzf.Blueprints.UnitTests
{

    public static class DemoGraphFactory
    {

        public static IPropertyGraph CreateDemoGraph()
        {

            var _Graph = new InMemoryPropertyGraph() as IPropertyGraph;
            var _g     = new SimplePropertyGraph();
            var _A     = _g.AddVertex();

            var _Alice1 = _Graph.AddVertex();

            var _Alice = _Graph.AddVertex(new VertexId("1"), v => v.SetProperty("name", "Alice").SetProperty("age", 18));
            var _Bob   = _Graph.AddVertex(new VertexId("2"), v => v.SetProperty("name", "Bob").  SetProperty("age", 20));
            var _Carol = _Graph.AddVertex(new VertexId("3"), v => v.SetProperty("name", "Carol").SetProperty("age", 22));
            var _Dave  = _Graph.AddVertex(new VertexId("4"), v => v.SetProperty("name", "Dave"). SetProperty("age", 23));

            //var e7 = _Graph.AddEdge(marko, vadas, new EdgeId("7"), "knows", e => e.SetProperty("weight", 0.5));
            //var e8 = _Graph.AddEdge(marko, josh, new EdgeId("8"), "knows", e => e.SetProperty("weight", 1.0));
            //var e9 = _Graph.AddEdge(marko, lop, new EdgeId("9"), "created", e => e.SetProperty("weight", 0.4));

            //var e10 = _Graph.AddEdge(josh, ripple, new EdgeId("10"), "created", e => e.SetProperty("weight", 1.0));
            //var e11 = _Graph.AddEdge(josh, lop, new EdgeId("11"), "created", e => e.SetProperty("weight", 0.4));

            //var e12 = _Graph.AddEdge(peter, lop, new EdgeId("12"), "created", e => e.SetProperty("weight", 0.2));

            return _Graph;

        }

    }

}
