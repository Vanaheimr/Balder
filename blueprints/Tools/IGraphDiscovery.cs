/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using System;

#endregion

namespace de.ahzf.blueprints
{

    /// <summary>
    /// Discovers automagically all implementations of the IGraph interface.
    /// </summary>
    public class GraphAutoDiscovery : AutoDiscovery<IGraph>
    {

        #region Constructor(s)

        #region GraphAutoDiscovery()

        /// <summary>
        /// Create a new GraphAutoDiscovery instance
        /// </summary>
        public GraphAutoDiscovery()
            : base()
        { }

        #endregion

        #region GraphAutoDiscovery(myAutostart)

        /// <summary>
        /// Create a new GraphAutoDiscovery instance and start the discovery
        /// </summary>
        public GraphAutoDiscovery(Boolean myAutostart)
            : base(myAutostart)
        { }

        #endregion

        #endregion

    }

}
