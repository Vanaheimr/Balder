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
using System.Linq;
using System.Text;
using System.Reflection;

using de.ahzf.Illias.Commons;
using de.ahzf.Hermod;
using de.ahzf.Hermod.HTTP;
using de.ahzf.Blueprints.PropertyGraphs;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Blueprints.HTTP.Server
{

    /// <summary>
    /// HTML content representation.
    /// </summary>
    public class GraphService_HTML : AGraphService
    {

        #region Constructor(s)

        #region GraphService_HTML()

        /// <summary>
        /// HTML content representation.
        /// </summary>
        public GraphService_HTML()
            : base(HTTPContentType.HTML_UTF8)
        { }

        #endregion

        #region GraphService_HTML(IHTTPConnection)

        /// <summary>
        /// HTML content representation.
        /// </summary>
        /// <param name="IHTTPConnection">The http connection for this request.</param>
        public GraphService_HTML(IHTTPConnection IHTTPConnection)
            : base(IHTTPConnection, HTTPContentType.HTML_UTF8)
        {
            this.CallingAssembly = Assembly.GetExecutingAssembly();
        }

        #endregion

        #endregion


        #region (private) HTMLBuilder(Headline, StringBuilderFunc)

        private String HTMLBuilder(String Headline, Action<StringBuilder> StringBuilderFunc)
        {

            var _StringBuilder = new StringBuilder();

            _StringBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            _StringBuilder.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\" \"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\">");
            _StringBuilder.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            _StringBuilder.AppendLine("<head>");
            _StringBuilder.AppendLine("<title>GraphServer v0.1</title>");
            _StringBuilder.AppendLine("</head>");
            _StringBuilder.AppendLine("<body>");
            _StringBuilder.Append("<h2>").Append(Headline).AppendLine("</h2>");
            _StringBuilder.AppendLine("<table>");
            _StringBuilder.AppendLine("<tr>");
            _StringBuilder.AppendLine("<td style=\"width: 100px\">&nbsp;</td>");
            _StringBuilder.AppendLine("<td>");

            StringBuilderFunc(_StringBuilder);

            _StringBuilder.AppendLine("</td>");
            _StringBuilder.AppendLine("</tr>");
            _StringBuilder.AppendLine("</table>");
            _StringBuilder.AppendLine("</body>").AppendLine("</html>").AppendLine();

            return _StringBuilder.ToString();

        }

        #endregion


        #region GetRoot()

        public override HTTPResponse GetRoot()
        {

            var Graphs = GraphServer.AllGraphs().
                                     Select(graph => "<a href=\"/graph/" + graph.Id + "\">" + graph.Id + " - " + graph.Description + "</a> " +
                                                     "<a href=\"/graph/" + graph.Id + "/vertices\">[All Vertices]</a> " +
                                                     "<a href=\"/graph/" + graph.Id + "/edges\">[All Edge]</a>").
                                     Aggregate((a, b) =>  a + "<br>" + b);

            return new HTTPResponseBuilder() {
                HTTPStatusCode = HTTPStatusCode.OK,
                ContentType    = HTTPContentType.HTML_UTF8,
                Content        = HTMLBuilder("GraphServer v0.1", b => b.AppendLine("Hello world!<br>").AppendLine(Graphs)).ToUTF8Bytes()
            };

        }

        #endregion


        #region AllGraphs()

        public override HTTPResponse AllGraphs()
        {

            var AllGraphs = GraphServer.AllGraphs().
                                        Select(graph => "<a href=\"/graph/" + graph.Id + "\">" + graph.Id + " - " + graph.Description + "</a> " +
                                                        "<a href=\"/graph/" + graph.Id + "/vertices\">[All Vertices]</a> " +
                                                        "<a href=\"/graph/" + graph.Id + "/edges\">[All Edge]</a>").
                                        Aggregate((a, b) => a + "<br>" + b);

            return new HTTPResponseBuilder()
            {
                HTTPStatusCode = HTTPStatusCode.OK,
                ContentType    = HTTPContentType.HTML_UTF8,
                Content        = HTMLBuilder("", b => b.AppendLine(AllGraphs)).ToUTF8Bytes()
            };

        }

        #endregion


        #region (protected) VerticesSerialization(...)

        /// <summary>
        /// Serialize an enumeration of vertices.
        /// </summary>
        /// <param name="Vertex">A single vertex.</param>
        /// <returns>The serialized vertex.</returns>
        protected override Byte[] VerticesSerialization(IEnumerable<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                                           UInt64, Int64, String, String, Object,
                                                                                           UInt64, Int64, String, String, Object,
                                                                                           UInt64, Int64, String, String, Object>> Vertices)
        {

            return HTMLBuilder("www.graph-database.org v0.1", b =>
            {

                b.AppendLine("Vertices<br>");

                foreach (var Vertex in Vertices)
                {

                    foreach (var KVP in Vertex)
                        b.Append(KVP.Key).
                          Append(" = ").
                          Append(KVP.Value.ToString()).
                          AppendLine("<br>");

                    b.AppendLine("<br>");

                }

            }).ToUTF8Bytes();

        }

        #endregion



        #region Vertices(GraphId)

        /// <summary>
        /// Return all vertices of the given graph.
        /// </summary>
        /// <param name="GraphId">The identification of the graph.</param>
        public override HTTPResponse Vertices(String GraphId)
        {
            
            var StringBuilder  = new StringBuilder();
            var Result         = base.GET_Vertices_protected(GraphId);

            if (Result.HasErrors)
                return Result.Error;

            if (Result.Data.Any())
            {

                StringBuilder.Append("<table>");

                Result.Data.ForEach(Vertex =>
                    {

                        StringBuilder.Append("<tr><td><table>");

                        Vertex.ForEach(KeyValuePair =>
                            StringBuilder.Append("<tr><td>").
                                          Append(KeyValuePair.Key.ToString()).
                                          Append("</td><td>").
                                          Append(KeyValuePair.Value.ToString()).
                                          Append("</td></tr>"));

                        StringBuilder.Append("</table></td></tr>");

                    });

                StringBuilder.Append("</table>");

            }

            return new HTTPResponseBuilder() {
                HTTPStatusCode = HTTPStatusCode.OK,
                ContentType    = this.HTTPContentTypes.First(),
                Content        = HTMLBuilder("All vertices", sb => sb.Append(StringBuilder.ToString())).ToUTF8Bytes()
            };

        }

        #endregion

        #region Edges(GraphId)

        /// <summary>
        /// Return all edges of the given graph.
        /// </summary>
        /// <param name="GraphId">The identification of the graph.</param>
        public override HTTPResponse Edges(String GraphId)
        {

            var StringBuilder = new StringBuilder();
            var Result = base.GET_Edges_protected(GraphId);

            if (Result.HasErrors)
                return Result.Error;

            if (Result.Data.Any())
            {

                StringBuilder.Append("<table>");

                Result.Data.ForEach(Edge =>
                {

                    StringBuilder.Append("<tr><td><table>");

                    Edge.ForEach(KeyValuePair =>
                        StringBuilder.Append("<tr><td>").
                                      Append(KeyValuePair.Key.ToString()).
                                      Append("</td><td>").
                                      Append(KeyValuePair.Value.ToString()).
                                      Append("</td></tr>"));

                    StringBuilder.Append("</table></td></tr>");

                });

                StringBuilder.Append("</table>");

            }

            return new HTTPResponseBuilder()
            {
                HTTPStatusCode = HTTPStatusCode.OK,
                ContentType = this.HTTPContentTypes.First(),
                Content = HTMLBuilder("All edges", sb => sb.Append(StringBuilder.ToString())).ToUTF8Bytes()
            };

        }

        #endregion


    }

}
