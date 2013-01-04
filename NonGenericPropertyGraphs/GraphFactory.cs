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
using System.Linq;
using System.Threading;
using System.Collections.Generic;

using de.ahzf.Illias.Commons;

#endregion

namespace de.ahzf.Vanaheimr.Blueprints.InMemory
{

    /// <summary>
    /// Simplified creation of property graphs.
    /// </summary>
    public static class GraphFactory
    {

        #region Helpers

        #region (private) NewRevId

        /// <summary>
        /// Return a new random RevId.
        /// </summary>
        private static Int64 NewRevId
        {
            get
            {
                return (Int64) UniqueTimestamp.Ticks;
            }
        }

        #endregion

        #region IdCreatorDelegate<TId>()

        /// <summary>
        /// A delegate for creating a new TId
        /// (if no one was provided by the user).
        /// </summary>
        /// <returns>A valid TId.</returns>
        public delegate TId IdCreatorDelegate<TId>();

        #endregion

        #region RevIdCreatorDelegate<TRevId>()

        /// <summary>
        /// A delegate for creating a new RevisionId
        /// (if no one was provided by the user).
        /// </summary>
        /// <returns>A valid RevisionId.</returns>
        public delegate TRevId RevIdCreatorDelegate<TRevId>();

        #endregion

        #endregion


        #region CreatePropertyGraph(GraphId, Description = null, GraphInitializer = null)

        /// <summary>
        /// Create a new property graph.
        /// </summary>
        /// <param name="GraphId">The graph identification.</param>
        /// <param name="Description">The optional description of the graph.</param>
        /// <param name="GraphInitializer">The optional graph initializer.</param>
        public static IPropertyGraph CreatePropertyGraph(UInt64 GraphId,
                                                         String Description = null,
                                                         GraphInitializer<UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object> GraphInitializer = null)
        {

            return new PropertyGraph(GraphId, GraphInitializer) { Description = Description } as IPropertyGraph;

        }

        #endregion


    }

}
