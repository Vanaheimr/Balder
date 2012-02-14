﻿/*
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

using de.ahzf.Blueprints.HTTPREST;
using de.ahzf.Blueprints.PropertyGraphs;
using de.ahzf.Blueprints.PropertyGraphs.InMemory;
using de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable;
using de.ahzf.Hermod.Datastructures;
using de.ahzf.Hermod.HTTP;
using de.ahzf.Illias.Commons;

#endregion

namespace de.ahzf.Blueprints.TestApplication
{

    public class Program
    {

        public static void Main(String[] args)
        {

            var graph  = new PropertyGraph(123, g => g.SetProperty("hello", "world!").SetProperty("graphs", "are cool!"));
            var server = new GraphServer(graph, new IPPort(8080));

            var a1 = graph.ContainsKey("hello");
            var a2 = graph.ContainsKey("world!");
            var a3 = graph.ContainsKey("graphs");
            var a4 = graph.ContainsKey("are cool!");

            var c1 = graph.ContainsValue(123UL);

            var t = false;
            graph.UseProperty("Id", success => t = true);
            var ii = "i = " + t;

            var b1 = graph.Contains("Id", 123UL);
            var b2 = graph is IProperties<String, Object>;

            var aa = graph.GetProperties(null);

            var deleted3 = graph.Remove().ToList();
            var deleted1 = graph.Remove("hello");

           // var a = graph.PropertyData.GetFilteredKeys.ContainsKey("hello");


            var _clientB  = new HTTPClient(IPv4Address.Parse("127.0.0.1"), new IPPort(8080));
            var _requestB = _clientB.GET("/").//AccountId/RepositoryId/TransactionId/GraphId/VerticesById?Id=2&Id=3").
                                     SetProtocolVersion(HTTPVersion.HTTP_1_1).
                                     SetUserAgent("Hermod HTTP Client v0.1").
                                     SetConnection("keep-alive").
                                     AddAccept(HTTPContentType.JSON_UTF8, 1);

            _clientB.Execute(_requestB, response => Console.WriteLine(response.Content.ToUTF8String()));

            while (true)
                Thread.Sleep(100);

        }

    }

}
