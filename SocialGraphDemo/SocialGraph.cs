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
using System.IO;
using System.Linq;
using System.Diagnostics;

using de.ahzf.Blueprints;
using de.ahzf.Blueprints.UnitTests;
using de.ahzf.Blueprints.PropertyGraph;
using de.ahzf.Blueprints.Tools;
using de.ahzf.Blueprints.PropertyGraph.InMemory;

#endregion

namespace SocialGraphDemo
{

    /// <summary>
    /// A simple demo implementing a social graph.
    /// </summary>
    public class SocialGraph
    {

        #region Data

        private const Int32  _NumberOfVertices = 100000;
        private const Int32  _NumberOfEdges    = 500000;
        private const String _FileName         = "SocialGraph_v100000-e500000.csv";

        #endregion

        #region Generate a simple SocialGraph and write it into a CSV-file

        private static void GenerateSocialGraph()
        {

            var _Stopwatch = new Stopwatch();

            Console.WriteLine("Generating {0} vertices having {1} edges...", _NumberOfVertices, _NumberOfEdges);
            _Stopwatch.Start();

            var _SocialGraph = SocialGraphGenerator.Generate(_NumberOfVertices,
                                                             _NumberOfEdges,
                                                             PreferentialAttachment: 3,
                                                             BatchNumber: 5000,
                                                             BatchAction: status =>
                                                                 {
                                                                     Console.SetCursorPosition(0, Console.CursorTop);
                                                                     Console.Write(status);
                                                                 });

            Console.WriteLine();
            Console.WriteLine("Time: {0}:{1:00} min", _Stopwatch.Elapsed.Minutes, _Stopwatch.Elapsed.Seconds);
            Console.WriteLine("Writing data to file...");

            var Histogram = SocialGraphGenerator.Histogram(_SocialGraph);

            _Stopwatch.Restart();
            CSV.WriteToFile(_SocialGraph, _FileName);

            Console.WriteLine("Time: {0}:{1:00} min", _Stopwatch.Elapsed.Minutes, _Stopwatch.Elapsed.Seconds);

        }

        #endregion

        #region Import vertices

        private static void ImportVertices(IPropertyGraph mySocialGraph)
        {

            var _Stopwatch = new Stopwatch();
            _Stopwatch.Start();

            CSV.ParseFile(_FileName, _CSVLine =>
                                          {
                                              mySocialGraph.AddVertex(new VertexId(_CSVLine[0]));
                                          }).Wait();

            _Stopwatch.Stop();

            Console.WriteLine("Vertex import: {0}:{1:00} min", _Stopwatch.Elapsed.Minutes, _Stopwatch.Elapsed.Seconds);


        }

        #endregion

        #region Import edges

        private static void ImportEdges(IPropertyGraph mySocialGraph)
        {

            var _Stopwatch = new Stopwatch();
            _Stopwatch.Start();

            CSV.ParseFile(_FileName, _CSVLine =>
                                          {

                                              var _VertexId0 = new VertexId(_CSVLine[0]);

                                              for (var i = 1; i < _CSVLine.Count(); i++)
                                                  mySocialGraph.AddEdge(
                                                      mySocialGraph.GetVertex(_VertexId0),
                                                      mySocialGraph.GetVertex(new VertexId(_CSVLine[i]))
                                                  );

                                          }).Wait();

            _Stopwatch.Stop();

            Console.WriteLine("Edge import: {0}:{1:00} min", _Stopwatch.Elapsed.Minutes, _Stopwatch.Elapsed.Seconds);


        }

        #endregion


        #region Main(myArgs)

        /// <summary>
        /// Main routine!
        /// </summary>
        /// <param name="myArgs">The command line arguments.</param>
        public static void Main(String[] myArgs)
        {

            var _graph  = DemoGraphFactory.CreateDemoGraph();

            var _index1 = _graph.CreateVerticesIndex("IdxNames",
                                                     new DictionaryIndex<String, IPropertyVertex<UInt64, Int64,         String, Object,
                                                                                                 UInt64, Int64, String, String, Object,
                                                                                                 UInt64, Int64, String, String, Object>>(),
                                                     e => e.GetProperty("name").ToString().ToLower() +
                                                          e.GetProperty("age" ).ToString(),
                                                     e => Indexing.HasKeys(e, "name", "age"));

            var _index2 = _graph.CreateVerticesIndex<Int32>("IdxAges",
                                                            new DictionaryIndex<Int32, IPropertyVertex<UInt64, Int64,         String, Object,
                                                                                                       UInt64, Int64, String, String, Object,
                                                                                                       UInt64, Int64, String, String, Object>>(),
                                                            e => (Int32) e.GetProperty("age"),
                                                            e => Indexing.HasKeys(e, "age"));

            var _Idx = _graph.VerticesIndices().First();
            _Idx.Insert(_graph.Vertices);
            _index2.Insert(_graph.Vertices);

            //var x = _Idx.As();
            var y = _Idx.Equals("alice18").ToList();
            var z = _Idx.Equals(18).ToList();


            //_Idx.GetType().ContainsGenericParameters

            var m = _index2.Equals(18).First();

            // Create SocialGraph, if not existant!
            if (!File.Exists(_FileName))
                GenerateSocialGraph();


            // Create an in-memory graph using reflection
            IPropertyGraph _SocialGraph = null;
            if (!new AutoDiscovery<IPropertyGraph>().TryActivate("InMemoryGraph", out _SocialGraph))
            {
                Console.WriteLine("Could not find the 'InMemoryGraph' implementation!");
                Environment.Exit(1);
            }


            // Import vertices and edges
            ImportVertices(_SocialGraph);
            ImportEdges(_SocialGraph);


            var all1 = _SocialGraph.GetVertices().Count();
            var all2 = _SocialGraph.GetVertices(v => v.Id > new VertexId(10)).Count();

            Console.ReadLine();

        }

        #endregion

    }

}
