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

using de.ahzf.blueprints;
using de.ahzf.blueprints.InMemory.PropertyGraph.Generic;
using de.ahzf.blueprints.Datastructures;
using System;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Pipes.UnitTests
{

    public static class TinkerGraphFactory
    {

        public static IPropertyGraph CreateTinkerGraph()
        {

            var _TinkerGraph = new InMemoryPropertyGraph<VertexId,    RevisionId, String, Object, IDictionary<String, Object>,
                                                         EdgeId,      RevisionId, String, Object, IDictionary<String, Object>,
                                                         HyperEdgeId, RevisionId, String, Object, IDictionary<String, Object>,
                                                         Object>("Id", "RevId", () => new Dictionary<String, Object>(),
                                                                 "Id", "RevId", () => new Dictionary<String, Object>(),
                                                                 "Id", "RevId", () => new Dictionary<String, Object>());

            var marko  = _TinkerGraph.AddVertex(new VertexId("1"), v => v.Data.SetProperty("name", "marko"). SetProperty("age",   29));
            var vadas  = _TinkerGraph.AddVertex(new VertexId("2"), v => v.Data.SetProperty("name", "vadas"). SetProperty("age",   27));
            var lop    = _TinkerGraph.AddVertex(new VertexId("3"), v => v.Data.SetProperty("name", "lop").   SetProperty("lang", "java"));
            var josh   = _TinkerGraph.AddVertex(new VertexId("4"), v => v.Data.SetProperty("name", "josh").  SetProperty("age",   32));
            var ripple = _TinkerGraph.AddVertex(new VertexId("5"), v => v.Data.SetProperty("name", "ripple").SetProperty("lang", "java"));
            var peter  = _TinkerGraph.AddVertex(new VertexId("6"), v => v.Data.SetProperty("name", "peter"). SetProperty("age",   35));

            _TinkerGraph.AddEdge(marko, vadas,  new EdgeId("7"),  "knows",   e => e.Data.SetProperty("weight", 0.5));
            _TinkerGraph.AddEdge(marko, josh,   new EdgeId("8"),  "knows",   e => e.Data.SetProperty("weight", 1.0));
            _TinkerGraph.AddEdge(marko, lop,    new EdgeId("9"),  "created", e => e.Data.SetProperty("weight", 0.4));

            _TinkerGraph.AddEdge(josh,  ripple, new EdgeId("10"), "created", e => e.Data.SetProperty("weight", 1.0));
            _TinkerGraph.AddEdge(josh,  lop,    new EdgeId("11"), "created", e => e.Data.SetProperty("weight", 0.4));

            _TinkerGraph.AddEdge(peter, lop,    new EdgeId("12"), "created", e => e.Data.SetProperty("weight", 0.2));

            return _TinkerGraph as IPropertyGraph;

        }

    }

}
