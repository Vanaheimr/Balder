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

using de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable;

using NUnit.Framework;
using de.ahzf.Blueprints.PropertyGraphs;
using de.ahzf.Blueprints.HTTPREST;

#endregion

namespace de.ahzf.Blueprints.UnitTests.GraphServerTests
{

    /// <summary>
    /// GraphServer unit tests.
    /// </summary>
    [TestFixture]
    public class InitGraphServer
    {

        protected IGraphServer CreateGraph()
        {
            return new GraphServer(new PropertyGraph());
        }

        protected IGraphServer CreateGraph(UInt64 GraphId)
        {
            return new GraphServer(new PropertyGraph(GraphId));
        }

    }

}
