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

using de.ahzf.Blueprints.PropertyGraphs;
using de.ahzf.Hermod.HTTP;
using de.ahzf.Hermod.Datastructures;

#endregion

namespace de.ahzf.Blueprints.REST
{

    /// <summary>
    /// A tcp/http based Blueprints graph server.
    /// </summary>
    public class BlueprintsServer : HTTPServer<IBlueprintsService>, IBlueprintsServer
    {

        #region Data

        private readonly IDictionary<String, IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                    UInt64, Int64, String, String, Object,
                                                                    UInt64, Int64, String, String, Object,
                                                                    UInt64, Int64, String, String, Object>> VertexLookup;

        #endregion
        
        #region Properties

        #region PropertyGraph

        public IPropertyGraph PropertyGraph { get; private set; }

        #endregion

        #region DefaultServerName

        /// <summary>
        /// The default server name.
        /// </summary>
        public override String DefaultServerName
        {
            get
            {
                return "www.graph-database.org v0.1";
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region BlueprintsServer(PropertyGraph)

        /// <summary>
        /// Initialize the GraphServer using IPAddress.Any, http port 8182 and start the server.
        /// </summary>
        public BlueprintsServer(IPropertyGraph PropertyGraph)
            : base(IPv4Address.Any, new IPPort(80), Autostart: true)
        {

            #region Initial Checks

            if (PropertyGraph == null)
                throw new ArgumentNullException("PropertyGraph", "The given PropertyGraph must not be null!");

            #endregion

            this.PropertyGraph = PropertyGraph;
            this.ServerName    = DefaultServerName;
            this.VertexLookup  = new Dictionary<String, IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object>>();

            base.OnNewHTTPService += CentralService => { CentralService.GraphServer = this; };

        }

        #endregion

        #region BlueprintsServer(PropertyGraph, Port, AutoStart = false)

        /// <summary>
        /// Initialize the GraphServer using IPAddress.Any and the given parameters.
        /// </summary>
        /// <param name="Port">The listening port</param>
        /// <param name="Autostart"></param>
        public BlueprintsServer(IPropertyGraph PropertyGraph, IPPort Port, Boolean Autostart = false)
            : base(IPv4Address.Any, Port, Autostart: true)
        {

            #region Initial Checks

            if (PropertyGraph == null)
                throw new ArgumentNullException("PropertyGraph", "The given PropertyGraph must not be null!");

            #endregion

            this.PropertyGraph = PropertyGraph;
            this.ServerName    = DefaultServerName;
            this.VertexLookup = new Dictionary<String, IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object>>();

            base.OnNewHTTPService += CentralService => { CentralService.GraphServer = this; };

        }

        #endregion

        #region BlueprintsServer(PropertyGraph, IIPAddress, Port, AutoStart = false)

        /// <summary>
        /// Initialize the GraphServer using the given parameters.
        /// </summary>
        /// <param name="IIPAddress">The listening IP address(es)</param>
        /// <param name="Port">The listening port</param>
        /// <param name="Autostart"></param>
        public BlueprintsServer(IPropertyGraph PropertyGraph, IIPAddress IIPAddress, IPPort Port, Boolean Autostart = false)
            : base(IIPAddress, Port, Autostart: true)
        {

            #region Initial Checks

            if (PropertyGraph == null)
                throw new ArgumentNullException("PropertyGraph", "The given PropertyGraph must not be null!");

            #endregion

            this.PropertyGraph = PropertyGraph;
            this.ServerName    = DefaultServerName;
            this.VertexLookup = new Dictionary<String, IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object>>();

            base.OnNewHTTPService += CentralService => { CentralService.GraphServer = this; };

        }

        #endregion

        #region BlueprintsServer(PropertyGraph, IPSocket, Autostart = false)

        /// <summary>
        /// Initialize the GraphServer using the given parameters.
        /// </summary>
        /// <param name="IPSocket">The listening IPSocket.</param>
        /// <param name="Autostart"></param>
        public BlueprintsServer(IPropertyGraph PropertyGraph, IPSocket IPSocket, Boolean Autostart = false)
            : base(IPSocket.IPAddress, IPSocket.Port, Autostart: true)
        {

            #region Initial Checks

            if (PropertyGraph == null)
                throw new ArgumentNullException("PropertyGraph", "The given PropertyGraph must not be null!");

            #endregion

            this.PropertyGraph = PropertyGraph;
            this.ServerName    = DefaultServerName;
            this.VertexLookup = new Dictionary<String, IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object>>();

            base.OnNewHTTPService += CentralService => { CentralService.GraphServer = this; };

        }

        #endregion

        #endregion




        public void AddToIndex(IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> Vertex)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> Vertices()
        {
            throw new NotImplementedException();
        }

    }

}
