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
using System.Diagnostics;
using System.Collections.Generic;

using NUnit.Framework;

#endregion

namespace de.ahzf.Vanaheimr.Blueprints.UnitTests.PropertyGraphTests
{

    /// <summary>
    /// SimplePropertyGraph unit tests for creating vertices.
    /// </summary>
    [TestFixture]
    public class CreateVerticesTests : InitGraph
    {

        #region VertexIdEmptyConstructorTest()

        /// <summary>
        /// A test for an empty VertexId constructor.
        /// </summary>
        [Test]
        public void VertexIdEmptyConstructorTest()
        {
            var _Graph = CreateGraph();
            Assert.IsTrue(_Graph != null);
        }

        #endregion

    }

}
