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
    /// GRAPHML content representation.
    /// </summary>
    public class GraphService_GRAPHML : AGraphService
    {

        #region Constructor(s)

        #region GraphService_GRAPHML()

        /// <summary>
        /// GRAPHML content representation.
        /// </summary>
        public GraphService_GRAPHML()
            : base(HTTPContentType.GRAPHML_UTF8)
        { }

        #endregion

        #region GraphService_GRAPHML(IHTTPConnection)

        /// <summary>
        /// GRAPHML content representation.
        /// </summary>
        /// <param name="IHTTPConnection">The http connection for this request.</param>
        public GraphService_GRAPHML(IHTTPConnection IHTTPConnection)
            : base(IHTTPConnection, HTTPContentType.GRAPHML_UTF8)
        {
            this.CallingAssembly = Assembly.GetExecutingAssembly();
        }

        #endregion

        #endregion


        #region /graph/{GraphId}

        /// <summary>
        /// Return the graph associated with the given graph identification.
        /// </summary>
        /// <param name="GraphId">The identification of the graph to return.</param>
        public override HTTPResponse GET_GraphById(String GraphId)
        {

            var StringBuilder = new StringBuilder();
            var GraphResult   = base.GET_GraphById_protected(GraphId);

            if (GraphResult.HasErrors)
                return GraphResult.Error;

            if (GraphResult.Data != null)
            {

                StringBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                StringBuilder.AppendLine("<graphml xmlns=\"http://graphml.graphdrawing.org/xmlns\"");
                StringBuilder.AppendLine("    xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
                StringBuilder.AppendLine("    xsi:schemaLocation=\"http://graphml.graphdrawing.org/xmlns");
                StringBuilder.AppendLine("     http://graphml.graphdrawing.org/xmlns/1.0/graphml.xsd\">");
                StringBuilder.AppendLine("  <graph id=\"" + GraphResult.Data.Id + "\" edgedefault=\"directed\">");

                GraphResult.Data.Vertices().ForEach(Vertex => StringBuilder.AppendLine("    <node id=\"" + Vertex.Id + "\" />"));

                GraphResult.Data.Edges().   ForEach(Edge   => StringBuilder.AppendLine("    <edge id=\"" + Edge.Id + "\" directed=\"true\" source=\"" + Edge.OutVertex.Id + "\" target=\"" + Edge.InVertex.Id + "\" />"));

                StringBuilder.AppendLine("  </graph>");
                StringBuilder.AppendLine("</graphml>");

            }

            return new HTTPResponseBuilder()
            {
                HTTPStatusCode = HTTPStatusCode.OK,
                ContentType    = HTTPContentTypes.First(),
                Content        = StringBuilder.ToString().ToUTF8Bytes()
            };

        }

        #endregion


    }

}
