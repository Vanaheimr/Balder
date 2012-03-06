/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <code@ahzf.de>
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
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

using de.ahzf.Blueprints.HTTP.Server;
using de.ahzf.Blueprints.PropertyGraphs;
using de.ahzf.Blueprints.PropertyGraphs.InMemory;
using de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable;
using de.ahzf.Hermod.Datastructures;
using de.ahzf.Hermod.HTTP;
using de.ahzf.Illias.Commons;
using de.ahzf.Blueprints.HTTP.Client;

#endregion

namespace de.ahzf.Blueprints.TestApplication
{

    public class Program
    {

        public static void Main(String[] args)
        {

            using (var GraphServer = new GraphServer(new PropertyGraph(123UL) { Description = "the first graph" }, new IPPort(8080)) {
                                         ServerName = "GraphServer v0.1"
                                     } as IGraphServer)
            {

                var graph = GraphServer.NewPropertyGraph(512UL, g => g.SetProperty(GraphDBOntology.Description().Suffix, "the second graph").SetProperty("hello", "world!"));
                var a1 = graph.ContainsKey("hello");
                var a2 = graph.ContainsKey("world!");
                var a3 = graph.ContainsKey("graphs");
                var a4 = graph.ContainsKey("are cool!");
                var a5 = graph.ContainsKey(GraphDBOntology.Description().Suffix);

                var c1 = graph.ContainsValue(123UL);

                var t = false;
                graph.UseProperty("Id", success => t = true);
                var ii = "i = " + t;

                var b1 = graph.Contains("Id", 123UL);
                var b2 = graph is IProperties<String, Object>;

                var aa = graph.GetProperties(null);

                var deleted3 = graph.Remove().ToList();
                var deleted1 = graph.Remove("hello");

                // ---------------------------------------------------------------

                var v1 = graph.AddVertex(v => v.SetProperty("Name", "Vertex1"));
                var v2 = graph.AddVertex(v => v.SetProperty("Name", "Vertex2"));
                var e1 = graph.AddEdge(v1, v2, "knows", e => e.SetProperty("Name", "Edge1"));

                var allV = graph.Vertices().ToList();
                var allE = graph.Edges().ToList();


                // ---------------------------------------------------------------


                //var HTTPClient1 = new HTTPClient(IPv4Address.Parse("127.0.0.1"), new IPPort(8080));
                //var _request1 = HTTPClient1.GET("/").//AccountId/RepositoryId/TransactionId/GraphId/VerticesById?Id=2&Id=3").
                //                              SetProtocolVersion(HTTPVersion.HTTP_1_1).
                //                              SetUserAgent("Hermod HTTP Client v0.1").
                //                              SetConnection("keep-alive").
                //                              AddAccept(HTTPContentType.JSON_UTF8, 1);

                //HTTPClient1.Execute(_request1, response => Console.WriteLine(response.Content.ToUTF8String()));

                //// ---------------------------------------------------------------

                //var HTTPClient2 = new HTTPClient(IPv4Address.Parse("127.0.0.1"), new IPPort(8080));
                //var _request2 = HTTPClient2.GET("/123/description").//AccountId/RepositoryId/TransactionId/GraphId/VerticesById?Id=2&Id=3").
                //                              SetProtocolVersion(HTTPVersion.HTTP_1_1).
                //                              SetUserAgent("Hermod HTTP Client v0.1").
                //                              SetConnection("keep-alive").
                //                              AddAccept(HTTPContentType.JSON_UTF8, 1);

                //HTTPClient2.Execute(_request2, response => Console.WriteLine(response.Content.ToUTF8String()));

                // ---------------------------------------------------------------

                var JavaScriptEngine = new Jurassic.ScriptEngine();
                //Console.WriteLine(engine.Evaluate("5 * 10 + 2"));
                //engine.SetGlobalValue("interop", 15);
                //engine.ExecuteFile(@"c:\test.js");
                //engine.Evaluate("interop = interop + 5");
                //Console.WriteLine(engine.GetGlobalValue<int>("interop"));

                JavaScriptEngine.Evaluate("function VertexFilter(vertex) { return vertex.Name == 'Vertex1' }");
                
                foreach (var Vertex in graph.Vertices())
                {
                    //engine.SetGlobalValue("vertex", new JSPropertyVertex(_vv, engine));
                    
                    //engine.SetGlobalFunction("test", new Func<int, int, int>((a, b) => a + b));
                    //Console.WriteLine(engine.Evaluate<int>("test(5, 6)"));

                    //engine.Evaluate("var yesorno = vertex.Id > 1");
                    //var Id      = engine.GetGlobalValue  ("vertex.Id");
                    //var yesorno = engine.GetGlobalValue<Boolean>("yesorno");

                    if (JavaScriptEngine.CallGlobalFunction<Boolean>("VertexFilter", new JSPropertyVertex(Vertex, JavaScriptEngine)))
                        Console.WriteLine(Vertex.Id);

                }

                var aaa = from   V2
                          in     graph.Vertices()
                          where  JavaScriptEngine.CallGlobalFunction<Boolean>("VertexFilter", new JSPropertyVertex(V2, JavaScriptEngine))
                          select V2;

                var aaaa = aaa.ToList();


                var GraphClient = new RemotePropertyGraph(IPv4Address.Parse("127.0.0.1"), new IPPort(8080)) { Id = 512 };
                Console.WriteLine(GraphClient.Description);

                while (true)
                    Thread.Sleep(100);

            }

        }

    }

}
