/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
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
    public class AutoDiscoveryIGraphs : AutoDiscovery<IGraph>
    {

        #region Constructor(s)

        #region AutoDiscoveryIGraphs()

        /// <summary>
        /// Create a new AutoDiscovery instance and start the discovery
        /// of IGraph implementations.
        /// </summary>
        public AutoDiscoveryIGraphs()
            : base()
        { }

        #endregion

        #region AutoDiscoveryIGraphs(myAutostart)

        /// <summary>
        /// Create a new AutoDiscovery instance. An automatic discovery
        /// of IGraph implementations can be avoided.
        /// </summary>
        public AutoDiscoveryIGraphs(Boolean myAutostart)
            : base(myAutostart)
        { }

        #endregion

        #endregion

    }

}
