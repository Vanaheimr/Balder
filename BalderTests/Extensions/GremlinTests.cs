/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <achim@graph-database.org>
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
using System.Linq;
using System.Collections.Generic;

using NUnit.Framework;

using de.ahzf.Vanaheimr.Blueprints.UnitTests;

#endregion

namespace de.ahzf.Vanaheimr.Balder.UnitTests.Pipes
{

    [TestFixture]
    public class GremlinTests
    {

        #region Gremlin01()

        [Test]
        public void Gremlin01()
        {

            var _ToyGraph = DemoGraphFactory.Create();

            var _AllEdges01 = _ToyGraph.Vertices().OutE().ToList();
            var _AllEdges02 = _ToyGraph.Vertices().OutE("knows").ToList();

            var a = _ToyGraph.VerticesById(3).OutE("knows");

        }

        #endregion

    }

}
