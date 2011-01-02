/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET
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
