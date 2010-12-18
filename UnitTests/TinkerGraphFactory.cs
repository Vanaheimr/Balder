/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using de.ahzf.blueprints.InMemoryGraph;
using de.ahzf.blueprints.Datastructures;

#endregion

namespace UnitTests
{
    
    public static class TinkerGraphFactory
    {

        public static InMemoryGraph CreateTinkerGraph()
        {

            var _TinkerGraph = new InMemoryGraph();

            var marko = _TinkerGraph.AddVertex(new VertexId("1"));
            marko.SetProperty("name", "marko");
            marko.SetProperty("age", 29);

            var vadas = _TinkerGraph.AddVertex(new VertexId("2"));
            vadas.SetProperty("name", "vadas");
            vadas.SetProperty("age", 27);

            var lop = _TinkerGraph.AddVertex(new VertexId("3"));
            lop.SetProperty("name", "lop");
            lop.SetProperty("lang", "java");

            var josh = _TinkerGraph.AddVertex(new VertexId("4"));
            josh.SetProperty("name", "josh");
            josh.SetProperty("age", 32);

            var ripple = _TinkerGraph.AddVertex(new VertexId("5"));
            ripple.SetProperty("name", "ripple");
            ripple.SetProperty("lang", "java");

            var peter = _TinkerGraph.AddVertex(new VertexId("6"));
            peter.SetProperty("name", "peter");
            peter.SetProperty("age", 35);

            _TinkerGraph.AddEdge(marko, vadas,  new EdgeId("7"),  "knows").  SetProperty("weight", 0.5f);
            _TinkerGraph.AddEdge(marko, josh,   new EdgeId("8"),  "knows").  SetProperty("weight", 1.0f);
            _TinkerGraph.AddEdge(marko, lop,    new EdgeId("9"),  "created").SetProperty("weight", 0.4f);

            _TinkerGraph.AddEdge(josh,  ripple, new EdgeId("10"), "created").SetProperty("weight", 1.0f);
            _TinkerGraph.AddEdge(josh,  lop,    new EdgeId("11"), "created").SetProperty("weight", 0.4f);

            _TinkerGraph.AddEdge(peter, lop,    new EdgeId("12"), "created").SetProperty("weight", 0.2f);

            return _TinkerGraph;

        }

    }

}
