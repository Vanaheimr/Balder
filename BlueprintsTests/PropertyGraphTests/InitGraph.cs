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

using NUnit.Framework;

using de.ahzf.Vanaheimr.Blueprints.InMemory;

#endregion

namespace de.ahzf.Vanaheimr.Blueprints.UnitTests.PropertyGraphTests
{

    /// <summary>
    /// SimplePropertyGraph unit tests.
    /// </summary>
    [TestFixture]
    public class InitGraph
    {

        protected IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                        UInt64, Int64, String, String, Object,
                                        UInt64, Int64, String, String, Object,
                                        UInt64, Int64, String, String, Object> CreateGraph(GraphInitializer<UInt64, Int64, String, String, Object,
                                                                                                            UInt64, Int64, String, String, Object,
                                                                                                            UInt64, Int64, String, String, Object,
                                                                                                            UInt64, Int64, String, String, Object> GraphInitializer = null)
        {
            return GraphFactory.CreateGenericPropertyGraph(Convert.ToUInt64(new Random().Next()), GraphInitializer: GraphInitializer);
        }

        protected IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                        UInt64, Int64, String, String, Object,
                                        UInt64, Int64, String, String, Object,
                                        UInt64, Int64, String, String, Object> CreateGraph(String Description,
                                                                                           GraphInitializer<UInt64, Int64, String, String, Object,
                                                                                                            UInt64, Int64, String, String, Object,
                                                                                                            UInt64, Int64, String, String, Object,
                                                                                                            UInt64, Int64, String, String, Object> GraphInitializer = null)
        {
            return GraphFactory.CreateGenericPropertyGraph(Convert.ToUInt64(new Random().Next()), Description, GraphInitializer);
        }

        protected IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                        UInt64, Int64, String, String, Object,
                                        UInt64, Int64, String, String, Object,
                                        UInt64, Int64, String, String, Object> CreateGraph(UInt64 GraphId,
                                                                                           String Description = null,
                                                                                           GraphInitializer<UInt64, Int64, String, String, Object,
                                                                                                            UInt64, Int64, String, String, Object,
                                                                                                            UInt64, Int64, String, String, Object,
                                                                                                            UInt64, Int64, String, String, Object> GraphInitializer = null)
        {
            return GraphFactory.CreateGenericPropertyGraph(GraphId, Description, GraphInitializer);
        }

    }

}
