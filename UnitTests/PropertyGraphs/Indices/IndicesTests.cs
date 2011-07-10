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
    /// Unit tests for indices.
    /// </summary>
    [TestFixture]
    public class IndicesTests
    {

        #region CopyGraphTest()

        /// <summary>
        /// A test for copying a graph.
        /// </summary>
        [Test]
        public void CopyGraphTest()
        {

            var _graph = DemoGraphFactory.CreateDemoGraph();
            var _index1 = _graph.CreateVerticesIndex("IdxNames",
                                                     "DictionaryIndex",
                                                     e => e["name"].ToString().ToLower() +
                                                          e["age"].ToString(),
                                                     e => Indexing.HasKeys(e, "name", "age"));

            var _index2 = _graph.CreateVerticesIndex<Int32>("IdxAges",
                                                            "DictionaryIndex",
                                                            e => (Int32) e["age"],
                                                            e => Indexing.HasKeys(e, "age"));

            var _Idx = _graph.VerticesIndices().First();
            _Idx.Insert(_graph.Vertices);
            _index2.Insert(_graph.Vertices);

            //var x = _Idx.As();
            var y = _Idx.Equals("alice18").ToList();
            var z = _Idx.Equals(18).ToList();
            var m = _index2.Equals(18);

        }

        #endregion

    }


}
