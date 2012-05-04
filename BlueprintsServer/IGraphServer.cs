/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
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
using System.Collections.Generic;

using de.ahzf.Hermod;
using de.ahzf.Hermod.HTTP;
using de.ahzf.Hermod.Datastructures;
using de.ahzf.Blueprints.PropertyGraphs;

#endregion

namespace de.ahzf.Blueprints.HTTP.Server
{

    /// <summary>
    /// Simple PropertyGraph TCP/HTTP/REST access.
    /// </summary>
    public interface IGraphServer : IHTTPServer,
                                    IEnumerable<IGenericPropertyGraph<String, Int64, String, String, Object,
                                                                      String, Int64, String, String, Object,
                                                                      String, Int64, String, String, Object,
                                                                      String, Int64, String, String, Object>>
    {

        #region AddGraph(Graph)

        /// <summary>
        /// Adds the given property graph to the server.
        /// </summary>
        /// <param name="Graph">A property graph.</param>
        IGenericPropertyGraph<String, Int64, String, String, Object,
                              String, Int64, String, String, Object,
                              String, Int64, String, String, Object,
                              String, Int64, String, String, Object>

            AddGraph(IGenericPropertyGraph<String, Int64, String, String, Object,
                                           String, Int64, String, String, Object,
                                           String, Int64, String, String, Object,
                                           String, Int64, String, String, Object> Graph);

        #endregion

        #region CreateNewGraph(GraphId, Description = null, GraphInitializer = null)

        /// <summary>
        /// Creates a new class-based in-memory implementation of a property graph
        /// and adds it to the server.
        /// </summary>
        /// <param name="GraphId">A unique identification for this graph (which is also a vertex!).</param>
        /// <param name="Description">The description of the graph.</param>
        /// <param name="GraphInitializer">A delegate to initialize the new property graph.</param>
        IGenericPropertyGraph<String, Int64, String, String, Object,
                              String, Int64, String, String, Object,
                              String, Int64, String, String, Object,
                              String, Int64, String, String, Object>

            CreateNewGraph(String GraphId,
                           String Description = null,
                           GraphInitializer<String, Int64, String, String, Object,
                                            String, Int64, String, String, Object,
                                            String, Int64, String, String, Object,
                                            String, Int64, String, String, Object> GraphInitializer = null);

        #endregion


        #region GetGraph(GraphId)

        /// <summary>
        /// Return the graph identified by the given GraphId.
        /// If the graph does not exist rturn null.
        /// </summary>
        /// <param name="GraphId">The unique identifier of the graph to return.</param>
        IGenericPropertyGraph<String, Int64, String, String, Object,
                              String, Int64, String, String, Object,
                              String, Int64, String, String, Object,
                              String, Int64, String, String, Object> GetGraph(String GraphId);

        #endregion

        #region TryGetGraph(GraphId, out Graph)

        /// <summary>
        /// Try to return the graph identified by the given GraphId.
        /// </summary>
        /// <param name="GraphId">The unique identifier of the graph to return.</param>
        /// <param name="Graph">The Graph to return.</param>
        Boolean TryGetGraph(String GraphId,
                            out IGenericPropertyGraph<String, Int64, String, String, Object,
                                                      String, Int64, String, String, Object,
                                                      String, Int64, String, String, Object,
                                                      String, Int64, String, String, Object> Graph);

        #endregion

        #region NumberOfGraphs(GraphFilter = null)

        /// <summary>
        /// Return the number of graphs matching the
        /// optional graph filter delegate.
        /// </summary>
        /// <param name="GraphFilter">An optional graph filter.</param>
        UInt64 NumberOfGraphs(GraphFilter<String, Int64, String, String, Object,
                                          String, Int64, String, String, Object,
                                          String, Int64, String, String, Object,
                                          String, Int64, String, String, Object> GraphFilter = null);

        #endregion


        #region RemovePropertyGraph(GraphId)

        /// <summary>
        /// Removes the graph identified by the given GraphId.
        /// </summary>
        /// <param name="GraphId">The unique identifier of the graph to remove.</param>
        /// <returns>True on success, false otherwise.</returns>
        Boolean RemovePropertyGraph(String GraphId);

        #endregion

    }

}
