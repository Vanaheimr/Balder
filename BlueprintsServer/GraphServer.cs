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
using System.Collections.Generic;

using de.ahzf.Blueprints.PropertyGraphs;
using de.ahzf.Hermod.HTTP;
using de.ahzf.Hermod.Datastructures;

#endregion

namespace de.ahzf.Blueprints.HTTP.Server
{

    #region GraphServer

    /// <summary>
    /// A simple property graph HTTP/REST access (server).
    /// </summary>
    public class GraphServer : GraphServer<IGraphService>
    {

        #region GraphServer()

        /// <summary>
        /// Initialize the GraphServer using IPAddress.Any, http port 8182 and start the server.
        /// </summary>
        public GraphServer()
        {
            base.OnNewHTTPService += GraphService => { GraphService.GraphServer = this; };
        }

        #endregion

        #region GraphServer(Port, AutoStart = true)

        /// <summary>
        /// Initialize the GraphServer using IPAddress.Any and the given parameters.
        /// </summary>
        /// <param name="Port">The listening port</param>
        /// <param name="Autostart"></param>
        public GraphServer(IPPort  Port,
                           Boolean Autostart = true)

            : base(Port, Autostart)
        
        {
            base.OnNewHTTPService += GraphService => { GraphService.GraphServer = this; };
        }

        #endregion

        #region GraphServer(IIPAddress, Port, AutoStart = true)

        /// <summary>
        /// Initialize the GraphServer using the given parameters.
        /// </summary>
        /// <param name="IIPAddress">The listening IP address(es)</param>
        /// <param name="Port">The listening port</param>
        /// <param name="Autostart"></param>
        public GraphServer(IIPAddress IIPAddress,
                           IPPort     Port,
                           Boolean    Autostart = false)

            : base(IIPAddress, Port, Autostart)

        {
            base.OnNewHTTPService += GraphService => { GraphService.GraphServer = this; };
        }

        #endregion

        #region GraphServer(IPSocket, Autostart = true)

        /// <summary>
        /// Initialize the GraphServer using the given parameters.
        /// </summary>
        /// <param name="IPSocket">The listening IPSocket.</param>
        /// <param name="Autostart"></param>
        public GraphServer(IPSocket IPSocket,
                           Boolean  Autostart = true)

            : base(IPSocket.IPAddress, IPSocket.Port, Autostart)

        {
            base.OnNewHTTPService += GraphService => { GraphService.GraphServer = this; };
        }

        #endregion

    }

    #endregion

    #region GraphServer<HTTPServiceInterface>

    /// <summary>
    /// A simple property graph HTTP/REST access (server).
    /// </summary>
    public class GraphServer<HTTPServiceInterface> : HTTPServer<HTTPServiceInterface>,
                                                     IGraphServer

        where HTTPServiceInterface : class, IGraphService

    {

        #region Data

        private readonly IDictionary<String, IGenericPropertyGraph <String, Int64, String, String, Object,
                                                                    String, Int64, String, String, Object,
                                                                    String, Int64, String, String, Object,
                                                                    String, Int64, String, String, Object>> GraphLookup;

        private readonly IDictionary<String, IGenericPropertyVertex<String, Int64, String, String, Object,
                                                                    String, Int64, String, String, Object,
                                                                    String, Int64, String, String, Object,
                                                                    String, Int64, String, String, Object>> VertexLookup;

        #endregion

        #region Constructor(s)

        #region GraphServer()

        /// <summary>
        /// Initialize the GraphServer using IPAddress.Any, http port 8182 and start the server.
        /// </summary>
        public GraphServer()
            : base(IPv4Address.Any, new IPPort(8080), Autostart: true)

        {

            this.GraphLookup  = new Dictionary<String, IGenericPropertyGraph<String, Int64, String, String, Object,
                                                                             String, Int64, String, String, Object,
                                                                             String, Int64, String, String, Object,
                                                                             String, Int64, String, String, Object>>();

            this.ServerName   = DefaultServerName;
            this.VertexLookup = new Dictionary<String, IGenericPropertyVertex<String, Int64, String, String, Object,
                                                                              String, Int64, String, String, Object,
                                                                              String, Int64, String, String, Object,
                                                                              String, Int64, String, String, Object>>();

        }

        #endregion

        #region GraphServer(Port, AutoStart = true)

        /// <summary>
        /// Initialize the GraphServer using IPAddress.Any and the given parameters.
        /// </summary>
        /// <param name="Port">The listening port</param>
        /// <param name="Autostart"></param>
        public GraphServer(IPPort  Port,
                           Boolean Autostart = true)

            : base(IPv4Address.Any, Port, Autostart: Autostart)

        {

            this.ServerName = DefaultServerName;

            this.GraphLookup  = new Dictionary<String, IGenericPropertyGraph<String, Int64, String, String, Object,
                                                                             String, Int64, String, String, Object,
                                                                             String, Int64, String, String, Object,
                                                                             String, Int64, String, String, Object>>();

            this.VertexLookup = new Dictionary<String, IGenericPropertyVertex<String, Int64, String, String, Object,
                                                                              String, Int64, String, String, Object,
                                                                              String, Int64, String, String, Object,
                                                                              String, Int64, String, String, Object>>();

        }

        #endregion

        #region GraphServer(IIPAddress, Port, AutoStart = true)

        /// <summary>
        /// Initialize the GraphServer using the given parameters.
        /// </summary>
        /// <param name="IIPAddress">The listening IP address(es)</param>
        /// <param name="Port">The listening port</param>
        /// <param name="Autostart"></param>
        public GraphServer(IIPAddress IIPAddress,
                           IPPort     Port,
                           Boolean    Autostart = true)

            : base(IIPAddress, Port, Autostart: Autostart)

        {

            this.ServerName = DefaultServerName;

            this.GraphLookup = new Dictionary<String, IGenericPropertyGraph<String, Int64, String, String, Object,
                                                                            String, Int64, String, String, Object,
                                                                            String, Int64, String, String, Object,
                                                                            String, Int64, String, String, Object>>();

            this.VertexLookup = new Dictionary<String, IGenericPropertyVertex<String, Int64, String, String, Object,
                                                                              String, Int64, String, String, Object,
                                                                              String, Int64, String, String, Object,
                                                                              String, Int64, String, String, Object>>();

        }

        #endregion

        #region GraphServer(IPSocket, Autostart = true)

        /// <summary>
        /// Initialize the GraphServer using the given parameters.
        /// </summary>
        /// <param name="IPSocket">The listening IPSocket.</param>
        /// <param name="Autostart"></param>
        public GraphServer(IPSocket IPSocket,
                           Boolean  Autostart = true)

            : base(IPSocket.IPAddress, IPSocket.Port, Autostart: Autostart)

        {

            this.ServerName = DefaultServerName;

            this.GraphLookup = new Dictionary<String, IGenericPropertyGraph<String, Int64, String, String, Object,
                                                                            String, Int64, String, String, Object,
                                                                            String, Int64, String, String, Object,
                                                                            String, Int64, String, String, Object>>();

            this.VertexLookup = new Dictionary<String, IGenericPropertyVertex<String, Int64, String, String, Object,
                                                                              String, Int64, String, String, Object,
                                                                              String, Int64, String, String, Object,
                                                                              String, Int64, String, String, Object>>();

        }

        #endregion

        #endregion


        #region AddGraph(Graph)

        /// <summary>
        /// Adds the given property graph to the server.
        /// </summary>
        /// <param name="Graph">A property graph.</param>
        public IGenericPropertyGraph<String, Int64, String, String, Object,
                                     String, Int64, String, String, Object,
                                     String, Int64, String, String, Object,
                                     String, Int64, String, String, Object>

            AddGraph(IGenericPropertyGraph<String, Int64, String, String, Object,
                                           String, Int64, String, String, Object,
                                           String, Int64, String, String, Object,
                                           String, Int64, String, String, Object> Graph)

        {

            GraphLookup.Add(Graph.Id, Graph);
            return Graph;

        }

        #endregion

        #region CreateNewGraph(GraphId, Description = null, GraphInitializer = null)

        /// <summary>
        /// Creates a new class-based in-memory implementation of a property graph
        /// and adds it to the server.
        /// </summary>
        /// <param name="GraphId">A unique identification for this graph (which is also a vertex!).</param>
        /// <param name="Description">The description of the graph.</param>
        /// <param name="GraphInitializer">A delegate to initialize the new property graph.</param>
        public IGenericPropertyGraph<String, Int64, String, String, Object,
                                     String, Int64, String, String, Object,
                                     String, Int64, String, String, Object,
                                     String, Int64, String, String, Object>

            CreateNewGraph(String GraphId,
                           String Description = null,
                           GraphInitializer<String, Int64, String, String, Object,
                                            String, Int64, String, String, Object,
                                            String, Int64, String, String, Object,
                                            String, Int64, String, String, Object> GraphInitializer = null)

        {

            return AddGraph(GraphFactory.CreateGenericPropertyGraph2(GraphId, Description, GraphInitializer));

        }

        #endregion


        #region GetGraph(GraphId)

        /// <summary>
        /// Return the graph identified by the given GraphId.
        /// If the graph does not exist rturn null.
        /// </summary>
        /// <param name="GraphId">The unique identifier of the graph to return.</param>
        public IGenericPropertyGraph<String, Int64, String, String, Object,
                                     String, Int64, String, String, Object,
                                     String, Int64, String, String, Object,
                                     String, Int64, String, String, Object> GetGraph(String GraphId)
        {

            IGenericPropertyGraph<String, Int64, String, String, Object,
                                  String, Int64, String, String, Object,
                                  String, Int64, String, String, Object,
                                  String, Int64, String, String, Object> _Graph;

            if (GraphLookup.TryGetValue(GraphId, out _Graph))
                return _Graph;

            return null;

        }

        #endregion

        #region TryGetGraph(GraphId, out Graph)

        /// <summary>
        /// Try to return the graph identified by the given GraphId.
        /// </summary>
        /// <param name="GraphId">The unique identifier of the graph to return.</param>
        /// <param name="Graph">The Graph to return.</param>
        public Boolean TryGetGraph(String GraphId, out IGenericPropertyGraph<String, Int64, String, String, Object,
                                                                             String, Int64, String, String, Object,
                                                                             String, Int64, String, String, Object,
                                                                             String, Int64, String, String, Object> Graph)
        {
            return GraphLookup.TryGetValue(GraphId, out Graph);
        }

        #endregion

        #region NumberOfGraphs(GraphFilter = null)

        /// <summary>
        /// Return the number of graphs matching the
        /// optional graph filter delegate.
        /// </summary>
        /// <param name="GraphFilter">An optional graph filter.</param>
        public UInt64 NumberOfGraphs(GraphFilter<String, Int64, String, String, Object,
                                                 String, Int64, String, String, Object,
                                                 String, Int64, String, String, Object,
                                                 String, Int64, String, String, Object> GraphFilter = null)
        {

            if (GraphFilter == null)
                return (UInt64) GraphLookup.Count;

            else
                return (UInt64) (from   Graph
                                 in     GraphLookup.Values
                                 where  GraphFilter(Graph)
                                 select Graph).Count();

        }

        #endregion


        #region RemovePropertyGraph(GraphId)

        /// <summary>
        /// Removes the graph identified by the given GraphId.
        /// </summary>
        /// <param name="GraphId">The unique identifier of the graph to remove.</param>
        /// <returns>True on success, false otherwise.</returns>
        public Boolean RemovePropertyGraph(String GraphId)
        {
            return GraphLookup.Remove(GraphId);
        }

        #endregion


        #region IEnumerable<IGenericPropertyGraph<...>> Members

        /// <summary>
        /// Return a graph enumerator.
        /// </summary>
        public IEnumerator<IGenericPropertyGraph<String, Int64, String, String, Object,
                                                 String, Int64, String, String, Object,
                                                 String, Int64, String, String, Object,
                                                 String, Int64, String, String, Object>> GetEnumerator()
        {
            return GraphLookup.Values.GetEnumerator();
        }

        /// <summary>
        /// Return a graph enumerator.
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GraphLookup.Values.GetEnumerator();
        }

        #endregion

    }

    #endregion

}
