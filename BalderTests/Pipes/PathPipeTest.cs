/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Styx <http://www.github.com/Vanaheimr/Styx>
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

using org.GraphDefined.Vanaheimr.Styx;

using NUnit.Framework;
using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace org.GraphDefined.Vanaheimr.Balder.UnitTests.Pipes
{

    [TestFixture]
    public class PathPipeTest
    {

        #region TestPathPipe()

        [Test]
        public void TestPathPipe()
        {

            var _graph = DemoGraphFactory.CreateSimpleGraph();

            var Alice = _graph.VertexById("Alice").AsMutable();

            var _Pipe1 = new OutEdgesPipe<String, Int64, String, String, Object,
                                          String, Int64, String, String, Object,
                                          String, Int64, String, String, Object,
                                          String, Int64, String, String, Object>();

            var _Pipe2 = new InVertexPipe<String, Int64, String, String, Object,
                                          String, Int64, String, String, Object,
                                          String, Int64, String, String, Object,
                                          String, Int64, String, String, Object>();

            var _Pipe3 = new PathPipe<IReadOnlyGenericPropertyVertex<String, Int64, String, String, Object,
                                                                     String, Int64, String, String, Object,
                                                                     String, Int64, String, String, Object,
                                                                     String, Int64, String, String, Object>>();

            var _Pipeline = new Pipeline<IReadOnlyGenericPropertyVertex<String, Int64, String, String, Object,
                                                                        String, Int64, String, String, Object,
                                                                        String, Int64, String, String, Object,
                                                                        String, Int64, String, String, Object>,

                                         IEnumerable<Object>>(_Pipe1, _Pipe2, _Pipe3);

            _Pipeline.SetSource(new SingleEnumerator<IReadOnlyGenericPropertyVertex<String, Int64, String, String, Object,
                                                                                    String, Int64, String, String, Object,
                                                                                    String, Int64, String, String, Object,
                                                                                    String, Int64, String, String, Object>>(Alice));


            // Via pipeline...
            var query1 = _Pipeline.Select(path => path.Select(q => ((IIdentifier<String>) q).Id)).ToArray();

            Assert.AreEqual(3, query1.Length);

            Assert.AreEqual(3, query1[0].Count(), "'" + query1[0].AggString() + "' is invalid!");
            Assert.AreEqual(3, query1[1].Count(), "'" + query1[1].AggString() + "' is invalid!");
            Assert.AreEqual(3, query1[2].Count(), "'" + query1[2].AggString() + "' is invalid!");

            Assert.AreEqual("Alice|Alice -loves-> Bob|Bob", query1[0].AggString());
            Assert.AreEqual("Alice|0|Bob",                  query1[1].AggString());
            Assert.AreEqual("Alice|2|Carol",                query1[2].AggString());


            // Via extention methods...
            var query2 = Alice.OutE().InV().Paths().ToArray();
            Assert.AreEqual(3, query2.Length);
            Assert.AreEqual(3, query2[0].Count(), "'" + query2[0].Select(q => ((IIdentifier<String>) q).Id).AggString() + "' is invalid!");
            Assert.AreEqual(3, query2[1].Count(), "'" + query2[1].Select(q => ((IIdentifier<String>) q).Id).AggString() + "' is invalid!");
            Assert.AreEqual(3, query2[2].Count(), "'" + query2[2].Select(q => ((IIdentifier<String>) q).Id).AggString() + "' is invalid!");

            var query3 = Alice.Out().Paths().ToArray();
            Assert.AreEqual(3, query2.Length);
            Assert.AreEqual(3, query2[0].Count(), "'" + query2[0].Select(q => ((IIdentifier<String>) q).Id).AggString() + "' is invalid!");
            Assert.AreEqual(3, query2[1].Count(), "'" + query2[1].Select(q => ((IIdentifier<String>) q).Id).AggString() + "' is invalid!");
            Assert.AreEqual(3, query2[2].Count(), "'" + query2[2].Select(q => ((IIdentifier<String>) q).Id).AggString() + "' is invalid!");

        }

        #endregion

    }

}
