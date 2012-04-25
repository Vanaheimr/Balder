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

using de.ahzf.Hermod.HTTP;

#endregion

namespace de.ahzf.Blueprints.HTTP.Server
{

    //[HTTPService(Host: "localhost:8080", ForceAuthentication: true)]
    [HTTPService(HostAuthentication: true)]
    public interface IGraphService : IHTTPBaseService
    {

        #region Properties

        GraphServer GraphServer { get; set; }

        #endregion



        #region Events

        /// <summary>
        /// Get Events
        /// </summary>
        /// <returns>Endless text</returns>
        [HTTPEventMappingAttribute("GraphEvents", "/Events"), NoAuthentication]
        HTTPResponse GetEvents();

        #endregion



        #region AllGraphs()

        /// <summary>
        /// Get a list of all graphs.
        /// </summary>
        [HTTPMapping(HTTPMethods.GET, "/AllGraphs"), NoAuthentication]
        HTTPResponse AllGraphs();

        #endregion


        #region InfosOnGraph(GraphId)

        /// <summary>
        /// Return the general information of a graph.
        /// </summary>
        /// <param name="GraphId">The identification of the graph.</param>
        [HTTPMapping(HTTPMethods.GET, "/graph/{GraphId}"), NoAuthentication]
        HTTPResponse InfosOnGraph(String GraphId);

        #endregion

        #region Description(GraphId)

        /// <summary>
        /// Return the description of a graph.
        /// </summary>
        /// <param name="GraphId">The identification of the graph.</param>
        [HTTPMapping(HTTPMethods.GET, "/graph/{GraphId}/description"), NoAuthentication]
        HTTPResponse Description(String GraphId);

        #endregion


        #region VertexById(GraphId, VertexId)

        /// <summary>
        /// Return the vertex referenced by the given vertex identifier.
        /// If no vertex is referenced by the identifier return null.
        /// </summary>
        /// <param name="GraphId">The graph identification.</param>
        /// <param name="VertexId">The vertex identification.</param>
        [HTTPMapping(HTTPMethods.GET, "/graph/{GraphId}/VertexById/{VertexId}"), NoAuthentication]
        HTTPResponse VertexById(String GraphId, String VertexId);

        #endregion

        #region VerticesById(GraphId)

        /// <summary>
        /// Return the vertices referenced by the given array of vertex identifiers.
        /// If no vertex is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="GraphId">The graph identification.</param>
        /// <remarks>Include a JSON array having vertex identifiers.</remarks>
        [HTTPMapping(HTTPMethods.GET, "/graph/{GraphId}/VerticesById"), NoAuthentication]
        HTTPResponse VerticesById(String GraphId);

        #endregion

        #region NumberOfVertices(GraphId)

        /// <summary>
        /// Return the current number of vertices which match the given optional filter.
        /// When the filter is null, this method should implement an optimized
        /// way to get the currenty number of vertices.
        /// </summary>
        /// <remarks>Include $somescript for vertex filtering.</remarks>
        [HTTPMapping(HTTPMethods.GET, "/graph/{GraphId}/NumberOfVertices"), NoAuthentication]
        HTTPResponse NumberOfVertices(String GraphId);

        #endregion

        #region Vertices(GraphId)

        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// An optional vertex filter may be applied for filtering.
        /// </summary>
        /// <remarks>Include $somescript for vertex filtering.</remarks>
        [HTTPMapping(HTTPMethods.GET, "/graph/{GraphId}/vertices"), NoAuthentication]
        HTTPResponse Vertices(String GraphId);

        #endregion

        #region Edges(GraphId)

        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// An optional edge filter may be applied for filtering.
        /// </summary>
        /// <remarks>Include $somescript for edge filtering.</remarks>
        [HTTPMapping(HTTPMethods.GET, "/graph/{GraphId}/edges"), NoAuthentication]
        HTTPResponse Edges(String GraphId);

        #endregion

     }

}
