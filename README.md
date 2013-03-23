![Blueprints.NET logo](/Vanaheimr/blueprints.NET/raw/master/logo.png)

Balder is the generic property [graph](http://en.wikipedia.org/wiki/Graph_(data_structure\))
[model](http://en.wikipedia.org/wiki/Graph_theory) of the [Vanaheimr](http://vanaheimr.eu) research
project. In contrast to other graph libraries and [graph databases](http://en.wikipedia.org/wiki/Graph_database)
Balder focus is to provide a versatile and expressive graph interface to model your domain specific
knowledge easily, keep the quality of your data as high as possible and to achieve high-speed speed
calculations in a distributed enviroment not by implementing an optimization oracle, but by letting
you choose and tweak the right optimizations.

Balder provides a property graph model with some currently unique features:

 - Like any other graph model Balder provides **vertices** and **edges** which can hold any kind of properties
   (key/value pairs). **Multiedges** and **hyperedges** are a natural extention to this model and define graph
   elementes which connect multiple edges (multiedge) or multiple vertices (hyperedge). This allows Balder to
   be a fully self-contained graph model without the need of external indices for practical usage of graphs.
 - A Balder graph provides 20 **generic parameters** for type-safety and to allow Balder to adapt to your
   domain model more easily. These parameters control the type of the *unique identification*, of the *revision
   identification*, of the *label*, of the *property keys* and of the *property values* of every graph element.
 - Balder does not belief in OOP types, as it is nothing compared to the flexibility of e.g. RDF schema. Therefore
   Balder uses a light-weight approach of **labels** for every graph element to describe the intended nature of
   this graph element.
 - Balder uses the [Vanaheimr Styx](http://www.github.com/Vanaheimr/Styx) project not only to provide a user-friendly
   way to query the graph, but also to provide a **reactive graph**. This allows you not only to review and
   control graph modifications, but also communicate between different graph instances and to use your graph
   both as an event source and an event filter.
 - Balder uses its reactivity to feed **dependent graphs**. Such graphs are graphs not controlled by the user
   or application directly, but by another graph. This can be usefull if you need a better, faster or more memory
   efficient representation of graph data without loosing the flexibility of a Balder property graph.
 - **Schema graphs** are an application of *dependent graphs*. A schema graph connected to your Balder graph can
   learn and/or control the schema of your graph and thus help to explore and to improve the overall quality of
   your data.
 - Balder is **read-only on default** to improve the safety of your data and to provide a foundation for future
   multi-threading optimizations.
 - 


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

Additional help and background information can be found in the [Wiki](http://github.com/Vanaheimr/Blueprints.NET/wiki).
For more examples and tutorials please look at the [Bragi](http://github.com/Vanaheimr/Bragi) project if you are interessted
in simple but interactive graph visualizations consult the [Loki](http://github.com/Vanaheimr/Loki) project or look at [Aegir](http://github.com/Vanaheimr/Aegir) for mapping applications.    
News and updates can also be found on twitter by following: [@ahzf](http://www.twitter.com/ahzf) or [@graphdbs](http://www.twitter.com/graphdbs).

#### Installation

The installation of Blueprints.NET is very straightforward.    
Just check out or download its sources and all its dependencies:

- [NUnit](http://www.nunit.org/) for unit tests
- [Newtonsoft.JSON](http://github.com/JamesNK/Newtonsoft.Json) for JSON processing
- [Jurassic](http://github.com/Alxandr/Jurassic) for JavaScript processing and networking

if you want to clone the entire Vanaheimr graph processing stack just run the following commands:

    git clone git://github.com/Vanaheimr/Aegir
    git clone git://github.com/Vanaheimr/Balder
    git clone git://github.com/Vanaheimr/Blueprints.NET
    git clone git://github.com/Vanaheimr/Bragi
    git clone git://github.com/Vanaheimr/Eunomia
    git clone git://github.com/Vanaheimr/Glyphe
    git clone git://github.com/Vanaheimr/Hermod
    git clone git://github.com/Vanaheimr/Illias
    git clone git://github.com/Vanaheimr/Loki
    git clone git://github.com/Vanaheimr/Styx
    git clone git://github.com/Vanaheimr/Vanir
    git clone git://github.com/Vanaheimr/Walkyr
    git clone git://github.com/Vanaheimr/libs
        
    mkdir srclibs
    cd srclibs
    git clone git://github.com/ahzf/MonoCompilerAsAService.git
    git clone git://github.com/Alxandr/Jurassic.git

#### License and your contribution

[Blueprints.NET](http://github.com/ahzf/blueprints.NET) is released under the [Apache License 2.0](http://www.apache.org/licenses/LICENSE-2.0). For details see the [LICENSE](/Vanaheimr/Blueprints.NET/blob/master/LICENSE) file.    
To suggest a feature, report a bug or general discussion: [http://github.com/Vanaheimr/Blueprints.NET/issues](http://github.com/Vanaheimr/Blueprints.NET/issues)    
If you want to help or contribute source code to this project, please use the same license.   
The coding standards can be found by reading the code ;)

#### Acknowledgments

Blueprints.NET is a reimplementation of the [Blueprints](http://github.com/tinkerpop/blueprints) library for Java
provided by [Tinkerpop](http://tinkerpop.com). Additional ideas are based on the [Boost Graph Library](http://www.boost.org/doc/libs/1_47_0/libs/graph/doc/index.html).    
Please read the [NOTICE](/Vanaheimr/Blueprints.NET/blob/master/NOTICE) file for further credits.
