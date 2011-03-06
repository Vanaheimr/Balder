![Blueprints.NET logo](/ahzf/blueprints.NET/raw/master/doc/blueprints.NET-logo.png)

Blueprints.NET is a [property graph model interface](http://github.com/tinkerpop/gremlin/wiki/Defining-a-Property-Graph) for .NET/Mono. [Graph](http://en.wikipedia.org/wiki/Graph_database) [databases](http://www.graph-database.org) and frameworks that implement the Blueprints interface automatically support Blueprints-enabled applications. Likewise, Blueprints-enabled applications can plug-and-play different Blueprints-enabled graph backends.

Blueprints does not define an interface for remote network access. For a [HTTP/REST](http://en.wikipedia.org/wiki/Representational_State_Transfer) interface please take a look at [Rexster.NET](http://github.com/ahzf/rexster.NET).

#### Implementations

* A simple in-memory Property Graph.
* [Gera](http://github.com/ahzf/Gera) a framework for processing large-scale semantic graphs (coming soon).

#### Usage

    var _TinkerGraph = new InMemoryGraph() as IGraph;

    var marko  = _TinkerGraph.AddVertex(new VertexId("1"), v => v.SetProperty("name", "marko"). SetProperty("age",   29));
    var vadas  = _TinkerGraph.AddVertex(new VertexId("2"), v => v.SetProperty("name", "vadas"). SetProperty("age",   27));
    var lop    = _TinkerGraph.AddVertex(new VertexId("3"), v => v.SetProperty("name", "lop").   SetProperty("lang", "java"));
    var josh   = _TinkerGraph.AddVertex(new VertexId("4"), v => v.SetProperty("name", "josh").  SetProperty("age",   32));
    var ripple = _TinkerGraph.AddVertex(new VertexId("5"), v => v.SetProperty("name", "ripple").SetProperty("lang", "java"));
    var peter  = _TinkerGraph.AddVertex(new VertexId("6"), v => v.SetProperty("name", "peter"). SetProperty("age",   35));

    _TinkerGraph.AddEdge(marko, vadas,  new EdgeId("7"),  "knows",   e => e.SetProperty("weight", 0.5));
    _TinkerGraph.AddEdge(marko, josh,   new EdgeId("8"),  "knows",   e => e.SetProperty("weight", 1.0));
    _TinkerGraph.AddEdge(marko, lop,    new EdgeId("9"),  "created", e => e.SetProperty("weight", 0.4));

    _TinkerGraph.AddEdge(josh,  ripple, new EdgeId("10"), "created", e => e.SetProperty("weight", 1.0));
    _TinkerGraph.AddEdge(josh,  lop,    new EdgeId("11"), "created", e => e.SetProperty("weight", 0.4));

    _TinkerGraph.AddEdge(peter, lop,    new EdgeId("12"), "created", e => e.SetProperty("weight", 0.2));

#### Help and Documentation

Additional help and much more examples can be found in the [Wiki](http://github.com/ahzf/blueprints.NET/wiki).   
News and updates can also be found on twitter by following: [@ahzf](http://www.twitter.com/ahzf) or [@graphdbs](http://www.twitter.com/graphdbs).

#### Installation

The installation of Blueprints.NET is very straightforward. Just check out or download its sources and all its dependencies:

- [NUnit](http://www.nunit.org/) for unit tests

#### License and your contribution

[Blueprints.NET](http://github.com/ahzf/blueprints.NET) is released under the [Apache License 2.0](http://www.apache.org/licenses/LICENSE-2.0). For details see the [LICENSE](/ahzf/blueprints.NET/blob/master/LICENSE) file.    
To suggest a feature, report a bug or general discussion: [http://github.com/ahzf/blueprints.NET/issues](http://github.com/ahzf/blueprints.NET/issues)    
If you want to help or contribute source code to this project, please use the same license.   
The coding standards can be found by reading the code ;)

#### Acknowledgments

Blueprints.NET is a reimplementation of the [blueprints](http://github.com/tinkerpop/blueprints) library for Java provided by [Tinkerpop](http://tinkerpop.com).
Please read the [NOTICE](/ahzf/blueprints.NET/blob/master/NOTICE) file for further credits.
