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


        #region AllGraphDBs()

        /// <summary>
        /// Get Landingpage
        /// </summary>
        /// <returns>Some HTML and JavaScript</returns>
        [HTTPMapping(HTTPMethods.GET, "/AllGraphDBs"), NoAuthentication]
        HTTPResponse AllGraphs();

        #endregion


        #region Description(GraphId)

        /// <summary>
        /// Return the description of a property graph.
        /// </summary>
        /// <param name="GraphId">The identification of the property graph.</param>
        [HTTPMapping(HTTPMethods.GET, "/{GraphId}/description"), NoAuthentication]
        HTTPResponse Description(String GraphId);

        #endregion

        #region WorkPackageList(AccountId, ProjectName)

        /// <summary>
        /// Return a list of all work packages within the given project.
        /// </summary>
        /// <param name="AccountId">The account identification.</param>
        /// <param name="ProjectName">The name of the project.</param>
        [HTTPMapping(HTTPMethods.GET, "/{AccountId}/{ProjectName}/WorkPackages"), NoAuthentication]
        HTTPResponse WorkPackageList(String AccountId, String ProjectName);

        #endregion

        #region MilestoneList(AccountId, ProjectName)

        /// <summary>
        /// Return a list of all milestones within the given project.
        /// </summary>
        /// <param name="AccountId">The account identification.</param>
        /// <param name="ProjectName">The name of the project.</param>
        [HTTPMapping(HTTPMethods.GET, "/{AccountId}/{ProjectName}/Milestones"), NoAuthentication]
        HTTPResponse MilestoneList(String AccountId, String ProjectName);

        #endregion



        #region VertexById(AccountId, RepositoryId, TransactionId, GraphId, VertexId)

        /// <summary>
        /// Return the vertex referenced by the given vertex identifier.
        /// If no vertex is referenced by the identifier return null.
        /// </summary>
        /// <param name="AccountId">The account identification.</param>
        /// <param name="RepositoryId">The repository identification.</param>
        /// <param name="TransactionId">The transaction identification.</param>
        /// <param name="GraphId">The graph identification.</param>
        /// <param name="VertexId">The vertex identification.</param>
        [HTTPMapping(HTTPMethods.GET, "/{AccountId}/{RepositoryId}/{TransactionId}/{GraphId}/VertexById/{VertexId}"), NoAuthentication]
        HTTPResponse VertexById(String AccountId, String RepositoryId, String TransactionId, String GraphId, String VertexId);

        #endregion

        #region VerticesById(AccountId, RepositoryId, TransactionId, GraphId)

        /// <summary>
        /// Return the vertices referenced by the given array of vertex identifiers.
        /// If no vertex is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="AccountId">The account identification.</param>
        /// <param name="RepositoryId">The repository identification.</param>
        /// <param name="TransactionId">The transaction identification.</param>
        /// <param name="GraphId">The graph identification.</param>
        /// <remarks>Include a JSON array having vertex identifiers.</remarks>
        [HTTPMapping(HTTPMethods.GET, "/{AccountId}/{RepositoryId}/{TransactionId}/{GraphId}/VerticesById"), NoAuthentication]
        HTTPResponse VerticesById(String AccountId, String RepositoryId, String TransactionId, String GraphId);

        #endregion

        #region VerticesByType(AccountId, RepositoryId, TransactionId, GraphId)

        /// <summary>
        /// Return an enumeration of all vertices having one of the
        /// given vertex types.
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="RepositoryId"></param>
        /// <param name="TransactionId"></param>
        /// <param name="GraphId"></param>
        /// <remarks>Include a JSON array having vertex types.</remarks>
        [HTTPMapping(HTTPMethods.GET, "/{AccountId}/{RepositoryId}/{TransactionId}/{GraphId}/VerticesByType"), NoAuthentication]
        HTTPResponse VerticesByType(String AccountId, String RepositoryId, String TransactionId, String GraphId);

        #endregion

        #region VerticesByType(AccountId, RepositoryId, TransactionId, GraphId, VertexType)

        /// <summary>
        /// Return an enumeration of all vertices having one of the
        /// given vertex types.
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="RepositoryId"></param>
        /// <param name="TransactionId"></param>
        /// <param name="GraphId"></param>
        /// <param name="VertexType"></param>        
        [HTTPMapping(HTTPMethods.GET, "/{AccountId}/{RepositoryId}/{TransactionId}/{GraphId}/VerticesByType/{VertexType}"), NoAuthentication]
        HTTPResponse VerticesByType(String AccountId, String RepositoryId, String TransactionId, String GraphId, String VertexType);

        #endregion

        #region Vertices(AccountId, RepositoryId, TransactionId, GraphId)

        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// An optional vertex filter may be applied for filtering.
        /// </summary>
        /// <remarks>Include $somescript for vertex filtering.</remarks>
        [HTTPMapping(HTTPMethods.GET, "/{AccountId}/{RepositoryId}/{TransactionId}/{GraphId}/Vertices"), NoAuthentication]
        HTTPResponse Vertices(String AccountId, String RepositoryId, String TransactionId, String GraphId);

        #endregion

        #region NumberOfVertices(AccountId, RepositoryId, TransactionId, GraphId, VertexIdList)

        /// <summary>
        /// Return the current number of vertices which match the given optional filter.
        /// When the filter is null, this method should implement an optimized
        /// way to get the currenty number of vertices.
        /// </summary>
        /// <remarks>Include $somescript for vertex filtering.</remarks>
        [HTTPMapping(HTTPMethods.GET, "/{AccountId}/{RepositoryId}/{TransactionId}/{GraphId}/NumberOfVertices"), NoAuthentication]
        HTTPResponse NumberOfVertices(String AccountId, String RepositoryId, String TransactionId, String GraphId);

        #endregion


     }

}
