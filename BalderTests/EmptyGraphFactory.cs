/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Balder <http://www.github.com/Vanaheimr/Balder>
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

using NUnit.Framework;

using de.ahzf.Vanaheimr.Blueprints.InMemory;
using de.ahzf.Vanaheimr.Blueprints;

#endregion

namespace de.ahzf.Vanaheimr.Balder.UnitTests.PropertyGraphTests
{

    /// <summary>
    /// Create some empty graphs with random graph
    /// identifications for unit tests.
    /// </summary>
    [TestFixture]
    public static class EmptyGraphFactory
    {

        #region CreateGraph(GraphInitializer = null)

        /// <summary>
        /// Create an empty property graph with a random graph identification.
        /// </summary>
        /// <param name="GraphInitializer">An optional graph initializer.</param>
        public static IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object>

            CreateGraph(GraphInitializer<UInt64, Int64, String, String, Object,
                                         UInt64, Int64, String, String, Object,
                                         UInt64, Int64, String, String, Object,
                                         UInt64, Int64, String, String, Object> GraphInitializer = null)

        {
            return GraphFactory.CreateGenericPropertyGraph(Convert.ToUInt64(new Random().Next()), GraphInitializer: GraphInitializer);
        }

        #endregion

        #region CreateGraph(Description, GraphInitializer = null)

        /// <summary>
        /// Create an empty property graph with a random graph identification
        /// and the given description.
        /// </summary>
        /// <param name="Description">A description for this graph.</param>
        /// <param name="GraphInitializer">An optional graph initializer.</param>
        public static IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object>

            CreateGraph(String Description,
                        GraphInitializer<UInt64, Int64, String, String, Object,
                                         UInt64, Int64, String, String, Object,
                                         UInt64, Int64, String, String, Object,
                                         UInt64, Int64, String, String, Object> GraphInitializer = null)

        {
            return GraphFactory.CreateGenericPropertyGraph(Convert.ToUInt64(new Random().Next()), Description, GraphInitializer);
        }

        #endregion

        #region CreateGraph(GraphId, Description = null, GraphInitializer = null)

        /// <summary>
        /// Create an empty property graph with a random graph identification
        /// the given description and identification.
        /// </summary>
        /// <param name="GraphId">A graph identification.</param>
        /// <param name="Description">A description for this graph.</param>
        /// <param name="GraphInitializer">An optional graph initializer.</param>
        public static IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object>

            CreateGraph(UInt64 GraphId,
                        String Description = null,
                        GraphInitializer<UInt64, Int64, String, String, Object,
                                         UInt64, Int64, String, String, Object,
                                         UInt64, Int64, String, String, Object,
                                         UInt64, Int64, String, String, Object> GraphInitializer = null)

        {
            return GraphFactory.CreateGenericPropertyGraph(GraphId, Description, GraphInitializer);
        }

        #endregion

    }

}
