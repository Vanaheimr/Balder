![Blueprints.NET logo](/ahzf/blueprints.NET/raw/master/logo.png)

Blueprints.NET is a generic graph model [[1](http://en.wikipedia.org/wiki/Graph_(mathematics\)), 
[2](http://en.wikipedia.org/wiki/Graph_(data_structure\)), [3](http://en.wikipedia.org/wiki/Graph_theory)] 
interface for .NET, Silverlight and Mono. Graph libraries, Graph databases 
[[1](http://en.wikipedia.org/wiki/Graph_database), [2](http://www.graph-database.org)] and frameworks 
that implement the Blueprints interface automatically support Blueprints-enabled applications. Likewise, 
Blueprints-enabled applications can plug-and-play different Blueprints-enabled graph backends.

Blueprints does not define an interface for remote network access. For a
[HTTP/REST](http://en.wikipedia.org/wiki/Representational_State_Transfer) interface please take a look
at [Gera](http://github.com/ahzf/Gera).

#### Implementations

* In-Memory Generic Graphs
* In-Memory Generic Property Graphs
* In-Memory Simplified Property Graphs

#### An usage example for property graphs

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


#### Help and Documentation

Additional help and background information can be found in the [Wiki](http://github.com/ahzf/blueprints.NET/wiki).
For more examples and tutorials please look at the [Thor](http://github.com/ahzf/Thor) project if you are interessted
in simple but interactive graph visualizations consult the [Loki](http://github.com/ahzf/Loki) project.    
News and updates can also be found on twitter by following: [@ahzf](http://www.twitter.com/ahzf) or [@graphdbs](http://www.twitter.com/graphdbs).

#### Installation

The installation of Blueprints.NET is very straightforward.    
Just check out or download its sources and all its dependencies:

- [NUnit](http://www.nunit.org/) for unit tests

#### License and your contribution

[Blueprints.NET](http://github.com/ahzf/blueprints.NET) is released under the [Apache License 2.0](http://www.apache.org/licenses/LICENSE-2.0). For details see the [LICENSE](/ahzf/blueprints.NET/blob/master/LICENSE) file.    
To suggest a feature, report a bug or general discussion: [http://github.com/ahzf/blueprints.NET/issues](http://github.com/ahzf/blueprints.NET/issues)    
If you want to help or contribute source code to this project, please use the same license.   
The coding standards can be found by reading the code ;)

#### Acknowledgments

Blueprints.NET is a reimplementation of the [blueprints](http://github.com/tinkerpop/blueprints) library for Java
provided by [Tinkerpop](http://tinkerpop.com). Additional ideas are based on the [Boost Graph Library](http://www.boost.org/doc/libs/1_47_0/libs/graph/doc/index.html).    
Please read the [NOTICE](/ahzf/blueprints.NET/blob/master/NOTICE) file for further credits.
