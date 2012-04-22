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
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

using de.ahzf.Illias.Commons;
using de.ahzf.Blueprints.PropertyGraphs;
using de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable;
using de.ahzf.Hermod.HTTP;
using de.ahzf.Hermod.Datastructures;

#endregion

namespace de.ahzf.Blueprints.HTTP.Client
{

    /// <summary>
    /// Access a remote PropertyGraph via HTTP/REST (client).
    /// </summary>
    public class RemotePropertyGraph : IPropertyGraph
    {

        #region Data

        private readonly HTTPClient HTTPClient;

        #endregion

        #region Properties

        #region RemoteIPAddress

        /// <summary>
        /// The IP address to connect to.
        /// </summary>
        public IIPAddress RemoteIPAddress
        {

            get
            {
                return HTTPClient.RemoteIPAddress;
            }

            set
            {
                HTTPClient.RemoteIPAddress = value;
            }

        }

        #endregion

        #region RemotePort

        /// <summary>
        /// The IP port to connect to.
        /// </summary>
        public IPPort RemotePort
        {

            get
            {
                return HTTPClient.RemotePort;
            }

            set
            {
                HTTPClient.RemotePort = value;
            }

        }

        #endregion

        #region RemoteSocket

        /// <summary>
        /// The IP socket to connect to.
        /// </summary>
        public IPSocket RemoteSocket
        {

            get
            {
                return HTTPClient.RemoteSocket;
            }

            set
            {
                HTTPClient.RemoteSocket = value;
            }

        }

        #endregion


        #region HTTPContentType

        public HTTPContentType HTTPContentType { get; set; }

        #endregion


        #region Id

        public ulong Id { get; set; }

        #endregion

        #region Description

        // {
        //   "AllGraphs": {
        //     "123": "the first graph",
        //     "512": "the second graph"
        //   }
        // }

        /// <summary>
        /// Provides a description of this graph element.
        /// </summary>
        public Object Description
        {

            get
            {
                var JSON = JSONResponse.ParseJSON(WaitGET("/graph/" + Id + "/description"));
                return (String) JSON.Result["description"];

                //var a = (j as JToken).SelectToken("description", errorWhenNoMatch: false);

                //JObject googleSearch = JObject.Parse(googleSearchText);

                //// get JSON result objects into a list
                //IList<JToken> results = googleSearch["responseData"]["results"].Children().ToList();

                //// serialize JSON results into .NET objects
                //IList<SearchResult> searchResults = new List<SearchResult>();
                //foreach (JToken result in results)
                //{
                //    SearchResult searchResult = JsonConvert.DeserializeObject<SearchResult>(result.ToString());
                //    searchResults.Add(searchResult);
                //}

            }

            set
            {
                throw new NotImplementedException();
            }

        }

        #endregion

        #region NumberOfVertices

        public ulong NumberOfVertices(VertexFilter<ulong, long, string, string, object,
                                                   ulong, long, string, string, object,
                                                   ulong, long, string, string, object,
                                                   ulong, long, string, string, object> VertexFilter = null)
        {

            if (VertexFilter != null)
                throw new NotImplementedException();

            throw new NotImplementedException();

        }

        #endregion

        #endregion

        #region Constructor(s)

        #region GraphClient(RemoteIPAddress = null, RemotePort = null)

        /// <summary>
        /// Create a new GraphClient using the given optional parameters.
        /// </summary>
        /// <param name="RemoteIPAddress">The IP address to connect to.</param>
        /// <param name="RemotePort">The IP port to connect to.</param>
        public RemotePropertyGraph(IIPAddress RemoteIPAddress = null, IPPort RemotePort = null)
        {

            this.HTTPClient      = new HTTPClient(RemoteIPAddress, RemotePort);
            this.HTTPContentType = HTTPContentType.JSON_UTF8;

            //HTTPClient.SetProtocolVersion(HTTPVersion.HTTP_1_1).
            //           SetUserAgent("Hermod HTTP Client v0.1").
            //           SetConnection("keep-alive");

        }

        #endregion

        #region GraphClient(RemoteSocket)

        /// <summary>
        /// Create a new GraphClient using the given optional parameters.
        /// </summary>
        /// <param name="RemoteSocket">The IP socket to connect to.</param>
        public RemotePropertyGraph(IPSocket RemoteSocket)
        {
            this.HTTPClient      = new HTTPClient(RemoteSocket);
            this.HTTPContentType = HTTPContentType.JSON_UTF8;
        }

        #endregion

        #endregion


        #region (private) WaitGET(Url, HTTPContentType = null)

        private String WaitGET(String Url, HTTPContentType HTTPContentType = null)
        {
            var result = "";
            DoGET(Url, HTTPContentType, r => { result = r; }).Wait();
            return result;
        }

        #endregion

        #region (private) DoGET(Url, HTTPContentType = null, Action = null)

        private Task<HTTPClient> DoGET(String Url, HTTPContentType HTTPContentType = null, Action<String> Action = null)
        {

            var _HTTPContentType = (HTTPContentType != null) ? HTTPContentType : this.HTTPContentType;

            var _HTTPClient = new HTTPClient(RemoteIPAddress, RemotePort);
            var _Request    = _HTTPClient.GET(Url).
                                            SetProtocolVersion(HTTPVersion.HTTP_1_1).
                                            SetUserAgent("GraphClient v0.1").
                                            SetConnection("keep-alive").
                                            AddAccept(_HTTPContentType, 1);

            return HTTPClient.Execute(_Request, response => { if (Action != null) Action(response.Content.ToUTF8String()); } );

        }

        #endregion


        #region District of chaos, discord and confusion!

        public IPropertyVertex AddVertex(VertexInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> VertexInitializer = null)
        {
            throw new NotImplementedException();
        }

        public IPropertyVertex AddVertex(ulong VertexId, VertexInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> VertexInitializer = null)
        {
            throw new NotImplementedException();
        }

        public IPropertyVertex AddVertex(IPropertyVertex Vertex)
        {
            throw new NotImplementedException();
        }

        public IPropertyVertex VertexById(ulong VertexId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPropertyVertex> VerticesById(params ulong[] VertexIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPropertyVertex> VerticesByLabel(params string[] VertexLabels)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPropertyVertex> Vertices(VertexFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> VertexFilter = null)
        {
            throw new NotImplementedException();
        }

        public void RemoveVerticesById(params ulong[] VertexIds)
        {
            throw new NotImplementedException();
        }

        public void RemoveVertices(params IPropertyVertex[] Vertices)
        {
            throw new NotImplementedException();
        }

        public void RemoveVertices(VertexFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> VertexFilter = null)
        {
            throw new NotImplementedException();
        }

        public IPropertyEdge AddEdge(IPropertyVertex OutVertex, string Label, IPropertyVertex InVertex, EdgeInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> EdgeInitializer = null)
        {
            throw new NotImplementedException();
        }

        public IPropertyEdge AddEdge(ulong EdgeId, IPropertyVertex OutVertex, string Label, IPropertyVertex InVertex, EdgeInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> EdgeInitializer = null)
        {
            throw new NotImplementedException();
        }

        public IPropertyEdge AddEdge(IPropertyVertex OutVertex, IPropertyVertex InVertex, string Label = default(String), EdgeInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> EdgeInitializer = null)
        {
            throw new NotImplementedException();
        }

        public IPropertyEdge AddEdge(IPropertyVertex OutVertex, IPropertyVertex InVertex, ulong EdgeId, string Label = default(String), EdgeInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> EdgeInitializer = null)
        {
            throw new NotImplementedException();
        }

        public IPropertyEdge EdgeById(ulong EdgeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPropertyEdge> EdgesById(params ulong[] EdgeIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPropertyEdge> EdgesByLabel(params string[] EdgeLabels)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPropertyEdge> Edges(EdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> EdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        public ulong NumberOfEdges(EdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> EdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        public void RemoveEdgesById(params ulong[] EdgeIds)
        {
            throw new NotImplementedException();
        }

        public void RemoveEdges(params IPropertyEdge[] Edges)
        {
            throw new NotImplementedException();
        }

        public void RemoveEdges(EdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> EdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        public event VertexAddingEventHandler<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> OnVertexAdding;

        IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> IGenericPropertyGraph<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.AddVertex(VertexInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> VertexInitializer = null)
        {
            throw new NotImplementedException();
        }

        public IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddVertex(IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> Vertex)
        {
            throw new NotImplementedException();
        }

        public event VertexAddedEventHandler<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> OnVertexAdded;

        public void RemoveVertices(params IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>[] Vertices)
        {
            throw new NotImplementedException();
        }

        public event EdgeAddingEventHandler<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> OnEdgeAdding;

        public IGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddEdge(IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> OutVertex, string Label, IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> InVertex, EdgeInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> EdgeInitializer = null)
        {
            throw new NotImplementedException();
        }

        public IGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddEdge(ulong EdgeId, IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> OutVertex, string Label, IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> InVertex, EdgeInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> EdgeInitializer = null)
        {
            throw new NotImplementedException();
        }

        public IGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddEdge(IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> OutVertex, IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> InVertex, string Label = default(String), EdgeInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> EdgeInitializer = null)
        {
            throw new NotImplementedException();
        }

        public IGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddEdge(IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> OutVertex, IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> InVertex, ulong EdgeId, string Label = default(String), EdgeInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> EdgeInitializer = null)
        {
            throw new NotImplementedException();
        }

        public event EdgeAddedEventHandler<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> OnEdgeAdded;

        public void RemoveEdges(params IGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>[] Edges)
        {
            throw new NotImplementedException();
        }

        public event MultiEdgeAddingEventHandler<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> OnMultiEdgeAdding;

        public IGenericPropertyMultiEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddMultiEdge(params IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>[] Vertices)
        {
            throw new NotImplementedException();
        }

        public IGenericPropertyMultiEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddMultiEdge(string Label, params IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>[] Vertices)
        {
            throw new NotImplementedException();
        }

        public IGenericPropertyMultiEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddMultiEdge(string Label, MultiEdgeInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> MultiEdgeInitializer, params IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>[] Vertices)
        {
            throw new NotImplementedException();
        }

        public IGenericPropertyMultiEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddMultiEdge(ulong MultiEdgeId, string Label, MultiEdgeInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> MultiEdgeInitializer, params IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>[] Vertices)
        {
            throw new NotImplementedException();
        }

        public event MultiEdgeAddedEventHandler<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> OnMultiEdgeAdded;

        public void RemoveMultiEdgesById(params ulong[] MultiEdgeIds)
        {
            throw new NotImplementedException();
        }

        public void RemoveMultiEdges(params IGenericPropertyMultiEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>[] MultiEdges)
        {
            throw new NotImplementedException();
        }

        public void RemoveMultiEdges(MultiEdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> MultiEdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        public event HyperEdgeAddingEventHandler<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> OnHyperEdgeAdding;

        public IGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddHyperEdge(params IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>[] Vertices)
        {
            throw new NotImplementedException();
        }

        public IGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddHyperEdge(string Label, params IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>[] Vertices)
        {
            throw new NotImplementedException();
        }

        public IGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddHyperEdge(string Label, HyperEdgeInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> HyperEdgeInitializer, params IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>[] Vertices)
        {
            throw new NotImplementedException();
        }

        public IGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddHyperEdge(ulong HyperEdgeId, string Label, HyperEdgeInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> HyperEdgeInitializer, params IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>[] Vertices)
        {
            throw new NotImplementedException();
        }

        public IGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddHyperEdge(IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> OutVertex, string Label, HyperEdgeInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> HyperEdgeInitializer, params IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>[] InVertices)
        {
            throw new NotImplementedException();
        }

        public event HyperEdgeAddedEventHandler<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> OnHyperEdgeAdded;

        public void RemoveHyperEdgesById(params ulong[] HyperEdgeIds)
        {
            throw new NotImplementedException();
        }

        public void RemoveHyperEdges(params IGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>[] HyperEdges)
        {
            throw new NotImplementedException();
        }

        public void RemoveHyperEdges(HyperEdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> HyperEdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> IGenericReadOnlyPropertyGraph<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.VertexById(ulong VertexId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IGenericReadOnlyPropertyGraph<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.VerticesById(params ulong[] VertexIds)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IGenericReadOnlyPropertyGraph<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.VerticesByLabel(params string[] VertexLabels)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IGenericReadOnlyPropertyGraph<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.Vertices(VertexFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> VertexFilter = null)
        {
            throw new NotImplementedException();
        }

        IGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> IGenericReadOnlyPropertyGraph<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.EdgeById(ulong EdgeId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IGenericReadOnlyPropertyGraph<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.EdgesById(params ulong[] EdgeIds)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IGenericReadOnlyPropertyGraph<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.EdgesByLabel(params string[] EdgeLabels)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IGenericReadOnlyPropertyGraph<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.Edges(EdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> EdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        public IGenericPropertyMultiEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> MultiEdgeById(ulong MultiEdgeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGenericPropertyMultiEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> MultiEdgesById(params ulong[] MultiEdgeIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGenericPropertyMultiEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> MultiEdgesByLabel(params string[] MultiEdgeLabels)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGenericPropertyMultiEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> MultiEdges(MultiEdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> MultiEdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        public ulong NumberOfMultiEdges(MultiEdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> MultiEdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        public IGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> HyperEdgeById(ulong HyperEdgeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> HyperEdgesById(params ulong[] HyperEdgeIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> HyperEdgesByLabel(params string[] HyperEdgeLabels)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> HyperEdges(HyperEdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> HyperEdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        public ulong NumberOfHyperEdges(HyperEdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> HyperEdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        public event GraphShuttingdownEventHandler<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> OnGraphShuttingdown;

        public void Shutdown(string Message = "")
        {
            throw new NotImplementedException();
        }

        public event GraphShutteddownEventHandler<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> OnGraphShutteddown;

        public bool Equals(ulong other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(ulong other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public long RevId
        {
            get { throw new NotImplementedException(); }
        }

        public string IdKey
        {
            get { throw new NotImplementedException(); }
        }

        public string RevIdKey
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<string> Keys
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<object> Values
        {
            get { throw new NotImplementedException(); }
        }

        public bool ContainsKey(string Key)
        {
            throw new NotImplementedException();
        }

        public bool ContainsValue(object Value)
        {
            throw new NotImplementedException();
        }

        public bool Contains(string Key, object Value)
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<string, object> KeyValuePair)
        {
            throw new NotImplementedException();
        }

        public object this[string Key]
        {
            get { throw new NotImplementedException(); }
        }

        public bool TryGetProperty(string Key, out object Value)
        {
            throw new NotImplementedException();
        }

        public bool TryGetProperty<T>(string Key, out T Value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<KeyValuePair<string, object>> GetProperties(KeyValueFilter<string, object> KeyValueFilter = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Equals(IReadOnlyGraphElement<ulong, long, string, string, object> other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(IReadOnlyGraphElement<ulong, long, string, string, object> other)
        {
            throw new NotImplementedException();
        }

        public event PropertyAdditionEventHandler<string, object> OnPropertyAddition;

        public event PropertyAddedEventHandler<string, object> OnPropertyAdded;

        public event PropertyChangingEventHandler<string, object> OnPropertyChanging;

        public event PropertyChangedEventHandler<string, object> OnPropertyChanged;

        public event PropertyRemovalEventHandler<string, object> OnPropertyRemoval;

        public event PropertyRemovedEventHandler<string, object> OnPropertyRemoved;

        public IProperties<string, object> SetProperty(string Key, object Value)
        {
            throw new NotImplementedException();
        }

        public object Remove(string Key)
        {
            throw new NotImplementedException();
        }

        public object Remove(string Key, object Value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<KeyValuePair<string, object>> Remove(KeyValueFilter<string, object> KeyValueFilter = null)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IGraphElement<ulong, long, string, string, object> other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(IGraphElement<ulong, long, string, string, object> other)
        {
            throw new NotImplementedException();
        }

        #endregion



        public IPropertyVertex AddVertex(string VertexLabel, VertexInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> VertexInitializer = null)
        {
            throw new NotImplementedException();
        }

        public IPropertyVertex AddVertex(ulong VertexId, string VertexLabel, VertexInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> VertexInitializer = null)
        {
            throw new NotImplementedException();
        }


        IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> IGenericPropertyGraph<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.AddVertex(string Label, VertexInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> VertexInitializer = null)
        {
            throw new NotImplementedException();
        }

        IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> IGenericPropertyGraph<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.AddVertex(ulong Id, string Label, VertexInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> VertexInitializer = null)
        {
            throw new NotImplementedException();
        }


        public IPropertyEdge AddEdge(IPropertyEdge IPropertyEdge)
        {
            throw new NotImplementedException();
        }


        public IGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddEdge(IGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> IGenericPropertyEdge)
        {
            throw new NotImplementedException();
        }

        public IGenericPropertyMultiEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddMultiEdge(IGenericPropertyMultiEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> IGenericPropertyMultiEdge)
        {
            throw new NotImplementedException();
        }

        public IGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddHyperEdge(IGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> IGenericPropertyHyperEdge)
        {
            throw new NotImplementedException();
        }


        public IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddVertexIfNotExists(ulong Id, string Label, VertexInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> VertexInitializer = null)
        {
            throw new NotImplementedException();
        }

        public IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddVertexIfNotExists(IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> IGenericPropertyVertex, CheckVertexExistance<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> CheckExistanceDelegate)
        {
            throw new NotImplementedException();
        }

        public IGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddEdgeIfNotExists(IGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> Edge, CheckEdgeExistance<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> CheckExistanceDelegate = null)
        {
            throw new NotImplementedException();
        }



        public IGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddEdgeIfNotExists(ulong EdgeId, IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> OutVertex, string Label, IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> InVertex, EdgeInitializer<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> EdgeInitializer = null)
        {
            throw new NotImplementedException();
        }
    }

}
