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
using System.Collections.Generic;

using de.ahzf.Hermod.HTTP;
using de.ahzf.Blueprints.PropertyGraphs;

#endregion

namespace de.ahzf.Blueprints.HTTP.Server
{

    public abstract class AGraphService : AHTTPService, IGraphService
    {

        #region Properties

        /// <summary>
        /// The internal GraphServer object.
        /// </summary>
        public GraphServer GraphServer { get; set; }

        #endregion

        #region Constructor(s)

        #region AProjectService()

        /// <summary>
        /// Creates a new abstract OCPP CIMS service.
        /// </summary>
        public AGraphService()
        { }

        #endregion

        #region AProjectService(HTTPContentType)

        /// <summary>
        /// Creates a new abstract OCPP CIMS service.
        /// </summary>
        /// <param name="HTTPContentType">A content type.</param>
        public AGraphService(HTTPContentType HTTPContentType)
            : base(HTTPContentType)
        { }

        #endregion

        #region AProjectService(HTTPContentTypes)

        /// <summary>
        /// Creates a new abstract OCPP CIMS service.
        /// </summary>
        /// <param name="HTTPContentTypes">A content type.</param>
        public AGraphService(IEnumerable<HTTPContentType> HTTPContentTypes)
            : base(HTTPContentTypes)
        { }

        #endregion

        #region AProjectService(IHTTPConnection, HTTPContentType)

        /// <summary>
        /// Creates a new abstract OCPP CIMS service.
        /// </summary>
        /// <param name="IHTTPConnection">The http connection for this request.</param>
        /// <param name="HTTPContentType">A content type.</param>
        /// <param name="ResourcePath">The path to internal resources.</param>
        public AGraphService(IHTTPConnection IHTTPConnection, HTTPContentType HTTPContentType)
            : base(IHTTPConnection, HTTPContentType, "GraphServer.resources.")
        { }

        #endregion

        #region AProjectService(IHTTPConnection, HTTPContentTypes)

        /// <summary>
        /// Creates a new abstract OCPP CIMS service.
        /// </summary>
        /// <param name="IHTTPConnection">The http connection for this request.</param>
        /// <param name="HTTPContentTypes">An enumeration of content types.</param>
        /// <param name="ResourcePath">The path to internal resources.</param>
        public AGraphService(IHTTPConnection IHTTPConnection, IEnumerable<HTTPContentType> HTTPContentTypes)
            : base(IHTTPConnection, HTTPContentTypes, "GraphServer.resources.")
        { }

        #endregion

        #endregion


        #region (protected) GetStartOffset()

        protected UInt32 GetStartOffset()
        {

            List<String> _StringValues = null;

            if (IHTTPConnection.InHTTPRequest.QueryString != null)
                if (IHTTPConnection.InHTTPRequest.QueryString.TryGetValue(Tokens.OFFSET + "." + Tokens.START, out _StringValues))
                {

                    UInt32 _Value;

                    if (UInt32.TryParse(_StringValues[0], out _Value))
                        return _Value;

                }

            return 0;

        }

        #endregion

        #region (protected) GetEndOffset()

        protected UInt32 GetEndOffset()
        {

            List<String> _StringValues = null;

            if (IHTTPConnection.InHTTPRequest.QueryString != null)
                if (IHTTPConnection.InHTTPRequest.QueryString.TryGetValue(Tokens.OFFSET + "." + Tokens.END, out _StringValues))
                {

                    UInt32 _Value;

                    if (UInt32.TryParse(_StringValues[0], out _Value))
                        return _Value;

                }

            return 0;

        }

        #endregion



        #region GetRoot()

        /// <summary>
        /// Get the landing page.
        /// </summary>
        public virtual HTTPResponse GetRoot()
        {
            return Error406_NotAcceptable();
        }

        #endregion

        #region AllGraphs()

        /// <summary>
        /// Get a list of all graphs.
        /// </summary>
        public virtual HTTPResponse AllGraphs()
        {
            return Error406_NotAcceptable();
        }

        #endregion


        #region InfosOnGraph(GraphId)

        /// <summary>
        /// Return the general information of a graph.
        /// </summary>
        /// <param name="GraphId">The identification of the graph.</param>
        public HTTPResponse InfosOnGraph(String GraphId)
        {
            return Error406_NotAcceptable();
        }

        #endregion


        #region Description(GraphId)

        /// <summary>
        /// Return the description of a graph.
        /// </summary>
        /// <param name="GraphId">The identification of the graph.</param>
        public virtual HTTPResponse Description(String GraphId)
        {
            return Error406_NotAcceptable();
        }

        #endregion

        #region VertexById(GraphId, VertexId)

        /// <summary>
        /// Return the vertex referenced by the given vertex identifier.
        /// If no vertex is referenced by the identifier return null.
        /// </summary>
        /// <param name="GraphId">The identification of the graph.</param>
        /// <param name="VertexId">The vertex identification.</param>
        public virtual HTTPResponse VertexById(String GraphId, String VertexId)
        {

            #region Process request

            UInt64 _VertexId = 0;

            if (!UInt64.TryParse(VertexId, out _VertexId))
                return Error400_BadRequest();

            #endregion

            var Vertex = GraphServer.GetPropertyGraph(UInt64.Parse(GraphId)).VertexById(_VertexId);

            #region Process response

            if (Vertex == null)
                return Error404_NotFound();

            var Content = VertexSerialization(Vertex);
            if (Content == null)
                return Error400_BadRequest();

            return new HTTPResponseBuilder()
            {
                HTTPStatusCode = HTTPStatusCode.OK,
                ContentType    = this.HTTPContentTypes.First(),
                Content        = Content
            };

            #endregion

        }

        #endregion

        #region VerticesById(GraphId)

        /// <summary>
        /// Return the vertices referenced by the given array of vertex identifiers.
        /// If no vertex is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="GraphId">The identification of the graph.</param>
        /// <remarks>Include a JSON array having vertex identifiers.</remarks>
        public virtual HTTPResponse VerticesById(String GraphId)
        {

            #region Process request

            IEnumerable<UInt64> Ids = null;

            List<String> List = null;
            var QueryString = IHTTPConnection.InHTTPRequest.QueryString;

            if (QueryString != null)
            {
                if (QueryString.TryGetValue("Id", out List))
                    if (List != null && List.Count >= 1)
                        Ids = from s in List select UInt64.Parse(s);
            }

            #endregion

            var Vertices = GraphServer.GetPropertyGraph(UInt64.Parse(GraphId)).VerticesById(Ids.ToArray());

            #region Process response

            if (Vertices == null || !Vertices.Any())
                return Error404_NotFound();

            var Content = VerticesSerialization(Vertices);
            if (Content == null)
                return Error400_BadRequest();

            return new HTTPResponseBuilder()
            {
                HTTPStatusCode = HTTPStatusCode.OK,
                ContentType    = this.HTTPContentTypes.First(),
                Content        = Content
            };

            #endregion

        }

        #endregion

        #region NumberOfVertices(GraphId)

        /// <summary>
        /// Return the current number of vertices which match the given optional filter.
        /// When the filter is null, this method should implement an optimized
        /// way to get the currenty number of vertices.
        /// </summary>
        /// <param name="GraphId">The identification of the graph.</param>
        public HTTPResponse NumberOfVertices(String GraphId)
        {
            return Error406_NotAcceptable();
        }

        #endregion

        #region Vertices(GraphId)

        /// <summary>
        /// Return all vertices.
        /// </summary>
        /// <param name="GraphId">The identification of the graph.</param>
        public virtual HTTPResponse Vertices(String GraphId)
        {
            return Error406_NotAcceptable();
        }

        #endregion



        #region GetEvents()

        public virtual HTTPResponse GetEvents()
        {
            return Error406_NotAcceptable();
        }

        #endregion


        #region (protected) VertexSerialization(...)

        /// <summary>
        /// Serialize a single vertex.
        /// </summary>
        /// <param name="Vertex">A single vertex.</param>
        /// <returns>The serialized vertex.</returns>
        protected virtual Byte[] VertexSerialization(IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                            UInt64, Int64, String, String, Object,
                                                                            UInt64, Int64, String, String, Object,
                                                                            UInt64, Int64, String, String, Object> Vertex)
        {
            return null;
        }

        #endregion

        #region (protected) VerticesSerialization(...)

        /// <summary>
        /// Serialize an enumeration of vertices.
        /// </summary>
        /// <param name="Vertex">A single vertex.</param>
        /// <returns>The serialized vertex.</returns>
        protected virtual Byte[] VerticesSerialization(IEnumerable<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                                          UInt64, Int64, String, String, Object,
                                                                                          UInt64, Int64, String, String, Object,
                                                                                          UInt64, Int64, String, String, Object>> Vertices)
        {
            return null;
        }

        #endregion


    }

}
