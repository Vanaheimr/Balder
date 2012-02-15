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
using System.Reflection;
using System.Collections.Generic;

using de.ahzf.Illias.Commons;
using de.ahzf.Hermod;
using de.ahzf.Hermod.HTTP;
using de.ahzf.Blueprints.PropertyGraphs;

using Newtonsoft.Json.Linq;

#endregion

namespace de.ahzf.Blueprints.HTTPREST
{

    /// <summary>
    /// JSON content representation.
    /// </summary>
    public class GraphService_JSON : AGraphService
    {

        #region Constructor(s)

        #region GraphService_JSON()

        /// <summary>
        /// Creates a new CIMS service.
        /// </summary>
        public GraphService_JSON()
            : base(HTTPContentType.JSON_UTF8)
        { }

        #endregion

        #region GraphService_JSON(IHTTPConnection)

        /// <summary>
        /// Creates a new CIMS service.
        /// </summary>
        /// <param name="IHTTPConnection">The http connection for this request.</param>
        public GraphService_JSON(IHTTPConnection IHTTPConnection)
            : base(IHTTPConnection, HTTPContentType.JSON_UTF8)
        {
            this.CallingAssembly = Assembly.GetExecutingAssembly();
        }

        #endregion

        #endregion


        public override HTTPResponse GetRoot()
        {
            return AllGraphs();
        }

        #region AllGraphs()

        public override HTTPResponse AllGraphs()
        {

            var _Content = new JObject(
                                   new JProperty("AllGraphs",
                                       new JObject(
                                           from graph in GraphServer.AllGraphs() select new JProperty(graph.Id.ToString(), graph.Description)
                                       )
                                   )
                               ).ToString();

            return new HTTPResponseBuilder()
            {
                HTTPStatusCode = HTTPStatusCode.OK,
                ContentType    = HTTPContentType.JSON_UTF8,
                Content        = _Content.ToUTF8Bytes()
            };

        }

        #endregion


        #region (protected) VertexSerialization(...)

        /// <summary>
        /// Serialize a single vertex.
        /// </summary>
        /// <param name="Vertex">A single vertex.</param>
        /// <returns>The serialized vertex.</returns>
        protected override Byte[] VertexSerialization(IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object> Vertex)
        {

            return new JObject(
                       new JProperty("PropertyVertex",
                           new JObject(
                               from   KeyValuePair
                               in     Vertex
                               select new JProperty(KeyValuePair.Key, KeyValuePair.Value)
                           )
                       )
                     ).ToString().
                       ToUTF8Bytes();

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

            return new JArray(  ( from Vertex
                                  in   Vertices
                                  select
                                      new JObject(
                                          new JProperty("PropertyVertex",
                                              new JObject(
                                                  from   KeyValuePair
                                                  in     Vertex
                                                  select new JProperty(KeyValuePair.Key, KeyValuePair.Value)
                                              )
                                          )
                                      )
                                  ).ToArray()
                              ).ToString().
                                ToUTF8Bytes();

        }

        #endregion


    }

}
