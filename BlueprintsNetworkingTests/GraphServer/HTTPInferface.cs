/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET <http://www.github.com/Vanaheimr/Blueprints.NET>
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

using de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable;

using NUnit.Framework;
using de.ahzf.Blueprints.PropertyGraphs;
using de.ahzf.Blueprints.HTTP.Server;

#endregion

namespace de.ahzf.Blueprints.UnitTests.GraphServerTests
{

    /// <summary>
    /// HTTPInferface unit tests.
    /// </summary>
    [TestFixture]
    public class HTTPInferface : InitGraphServer
    {

        [Test]
        public void GraphServerConstructorTest()
        {

            var HTTPREST = new String[] {
               

                "GET /graphs",

                "FILTER /graphs",
                  "HTTPBody: {",
                     "\"GraphFilter\" : \"...\"",
                     "\"SELECT\"      : [ \"Name\", \"Age\" ],",
                  "}",

                "COUNT /graphs",
                  "HTTPBody: {",
                     "\"GraphFilter\" : \"...\"",
                     "\"SELECT\"      : [ \"Name\", \"Age\" ],",
                  "}",


            // return a graph

                // Include the Name & Age property of vertex Alice
                "GET /graph/1?SELECT=Id,Description",

 
            // return a single vertex

                // Get all of vertex Alice
                "GET /graph/1/vertex/Alice",

                // Include the Name & Age property of vertex Alice
                "GET /graph/1/vertex/Alice?SELECT=Name,Age",


                // Include all outedges with edge label Friends of the vertex Alice
                "GET /graph/1/vertex/Alice?SELECT=OutE(Friends)",

                // Include all inedges with edge label Friends of the vertex Alice
                "GET /graph/1/vertex/Alice?SELECT=InE(Friends)",

                // Include all in- and outedges with edge label Friends of the vertex Alice
                "GET /graph/1/vertex/Alice?SELECT=BothE(Friends)",


                // Include all outedges with edge label Friends of the vertex Alice
                "GET /graph/1/vertex/Alice?SELECT_OutE=Friends.weight",

                // Include all inedges with edge label Friends of the vertex Alice
                "GET /graph/1/vertex/Alice?SELECT_InE=Friends.weight",

                // Include all in- and outedges with edge label Friends of the vertex Alice
                "GET /graph/1/vertex/Alice?SELECT_BothE=Friends.weight",


                // Include all outgoing adjacent vertices reachable via edge label Friends of the vertex Alice
                "GET /graph/1/vertex/Alice?SELECT_Out=Friends",

                // Include all incoming adjacent vertices reachable via edge label Friends of the vertex Alice
                "GET /graph/1/vertex/Alice?SELECT_In=Friends",

                // Include all incoming and outgoing adjacent vertices reachable via edge label Friends of the vertex Alice
                "GET /graph/1/vertex/Alice?SELECT_Both=Friends",


                // Include all outgoing adjacent vertices reachable via edge label Friends of the vertex Alice
                "GET /graph/1/vertex/Alice?SELECT_Out=Friends.Name",

                // Include all incoming adjacent vertices reachable via edge label Friends of the vertex Alice
                "GET /graph/1/vertex/Alice?SELECT_In=Friends.Name",

                // Include all incoming and outgoing adjacent vertices reachable via edge label Friends of the vertex Alice
                "GET /graph/1/vertex/Alice?SELECT_Both=Friends.Name",


                // Get all of an older revision of the vertex Alice
                "GET /graph/1/vertex/Alice?RevId=20110815",


                // Get all of vertex Alice
                "FILTER /graph/1/vertex/Alice",
                " + JavaScript SELECT function",



            // return an enumeration of vertices

                "FILTER /graph/1/vertices",
                  "HTTPBody: {",
                    "\"VertexFilter\" : \"...\"",
                    "\"SELECT\"       : [ \"Name\", \"Age\" ],",
                  "}",

                "GET /graph/1/vertices/ids/Alice,Bob,Carol?SELECT=Name,Age",

                "FILTER /graph/1/vertices/ids",
                  "HTTPBody: {",
                    "\"VertexIds\"    : [ \"Alice\", \"Bob\", \"Carol\" ],",
                    "\"VertexFilter\" : \"...\"",
                    "\"SELECT\"       : [ \"Name\", \"Age\" ],",
                  "}",

                "GET /graph/1/vertices/labels/Friends,likes?SELECT=Name,Age",

                "FILTER /graph/1/vertices/labels",
                  "HTTPBody: {",
                    "\"VertexLabels\" : [ \"Friends\", \"likes\" ],",
                    "\"VertexFilter\" : \"...\"",
                    "\"SELECT\"       : [ \"Name\", \"Age\" ],",
                  "}",


            // return an UInt64

                "COUNT /graph/1/vertices",
                  "HTTPBody: {",
                    "\"VertexFilter\" : \"...\"",
                  "}",

                "COUNT /graph/1/vertices/ids",
                  "HTTPBody: {",
                    "\"VertexIds\"    : [ \"Alice\", \"Bob\", \"Carol\" ],",
                    "\"VertexFilter\" : \"...\"",
                  "}",



            // return Objects

                "GET /graph/1/vertex/Alice/p",

                "GET /graph/1/vertex/Alice/p/Name",





                "GET /graph/1/vertices/ids/Alice,Bob,Carol?SELECT=Name,Age"

            };
            
        }

    }

}
