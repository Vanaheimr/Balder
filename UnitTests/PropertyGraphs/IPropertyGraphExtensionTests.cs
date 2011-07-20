/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
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

using de.ahzf.Blueprints.PropertyGraph;
using de.ahzf.Blueprints.PropertyGraph.InMemory;

using NUnit.Framework;

#endregion

namespace de.ahzf.Blueprints.UnitTests
{

    /// <summary>
    /// Unit tests for the IPropertyGraphExtensions class.
    /// </summary>
    [TestFixture]
    public class IPropertyGraphExtensionTests
    {

        #region CopyGraphTest()

        /// <summary>
        /// A test for copying a graph.
        /// </summary>
        [Test]
        public void CopyGraphTest()
        {
            
            var _SourceGraph = TinkerGraphFactory.CreateTinkerGraph();

            var _NumberOfVertices = _SourceGraph.NumberOfVertices();
            var _NumberOfEdges    = _SourceGraph.NumberOfEdges();

            var _DestinationGraph = new SimplePropertyGraph();
            _SourceGraph.CopyGraph(_DestinationGraph);

            Assert.AreEqual(_NumberOfVertices, _DestinationGraph.NumberOfVertices());
            Assert.AreEqual(_NumberOfEdges,    _DestinationGraph.NumberOfEdges());

        }

        #endregion

    }


}
