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
using System.Linq;
using System.Collections.Generic;

using NUnit.Framework;

using de.ahzf.Blueprints.PropertyGraphs;
using de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable;

#endregion

namespace de.ahzf.Blueprints.UnitTests.SimplePropertyGraphTests
{

    /// <summary>
    /// SimplePropertyGraph unit tests for creating vertices.
    /// </summary>
    [TestFixture]
    public class CreateGraphTests : InitGraph
    {

        #region EmptyConstructorTest()

        [Test]
        public void EmptyConstructorTest()
        {
            Assert.IsNotNull(new PropertyGraph());
        }

        #endregion

        #region GraphInitializerConstructorTest()

        [Test]
        public void GraphInitializerConstructorTest()
        {

            var graph = new PropertyGraph(g => g.SetProperty("hello", "world!"));

            Assert.IsNotNull(graph);

        }

        #endregion

        #region GraphIdConstructorTest()

        [Test]
        public void GraphIdConstructorTest()
        {

            var graph = new PropertyGraph(123, null);

            Assert.IsNotNull(graph);
            Assert.AreEqual(123, graph.Id);

        }

        #endregion

        #region GraphIdAndInitializerConstructorTest()

        [Test]
        public void GraphIdAndInitializerConstructorTest()
        {

            var graph = new PropertyGraph(123UL, g => g.SetProperty("hello",  "world!").
                                                        SetProperty("graphs", "are cool!"));

            Assert.IsNotNull(graph);
            Assert.AreEqual(123UL, graph.Id);

            // - via AGraphElement ---------------------------------
            Assert.IsTrue (graph.ContainsKey("Id"));
            Assert.IsTrue (graph.ContainsKey("hello"));
            Assert.IsFalse(graph.ContainsKey("world!"));
            Assert.IsTrue (graph.ContainsKey("graphs"));
            Assert.IsFalse(graph.ContainsKey("are cool!"));

            Assert.IsTrue (graph.ContainsValue(123UL));
            Assert.IsFalse(graph.ContainsValue("hello"));
            Assert.IsTrue (graph.ContainsValue("world!"));
            Assert.IsFalse(graph.ContainsValue("graphs"));
            Assert.IsTrue (graph.ContainsValue("are cool!"));

            Assert.IsTrue(graph.Contains("Id", 123UL));
            Assert.IsTrue(graph.Contains("hello",  "world!"));
            Assert.IsTrue(graph.Contains("graphs", "are cool!"));

            Assert.IsTrue(graph.Contains(new KeyValuePair<String, Object>("Id",     123UL)));
            Assert.IsTrue(graph.Contains(new KeyValuePair<String, Object>("hello",  "world!")));
            Assert.IsTrue(graph.Contains(new KeyValuePair<String, Object>("graphs", "are cool!")));

            Assert.AreEqual(123UL,       graph["Id"]);
            Assert.AreEqual("world!",    graph["hello"]);
            Assert.AreEqual("are cool!", graph["graphs"]);

            Assert.AreEqual(123UL,       graph.GetProperty("Id"));
            Assert.AreEqual("world!",    graph.GetProperty("hello"));
            Assert.AreEqual("are cool!", graph.GetProperty("graphs"));

            Assert.AreEqual(123UL,       graph.GetProperty("Id",     typeof(UInt64)));
            Assert.AreEqual("world!",    graph.GetProperty("hello",  typeof(String)));
            Assert.AreEqual("are cool!", graph.GetProperty("graphs", typeof(String)));


            Object _Value;
            Assert.IsTrue(graph.TryGet("Id",     out _Value));
            Assert.AreEqual(123UL,       _Value);

            Assert.IsTrue(graph.TryGet("hello",  out _Value));
            Assert.AreEqual("world!",    _Value);

            Assert.IsTrue(graph.TryGet("graphs", out _Value));
            Assert.AreEqual("are cool!", _Value);

            // ------------------------------------------

            Nullable<Boolean> t;
            t = null;
            graph.GetProperty("false",  OnSuccess_PropertyValue => t = true);
            Assert.IsNull(t);

            t = null;
            graph.GetProperty("Id",     OnSuccess_PropertyValue => t = true);
            Assert.IsTrue(t.Value);

            t = null;
            graph.GetProperty("hello",  OnSuccess_PropertyValue => t = true);
            Assert.IsTrue(t.Value);

            t = null;
            graph.GetProperty("graphs", OnSuccess_PropertyValue => t = true);
            Assert.IsTrue(t.Value);


            // ------------------------------------------

            t = null;
            graph.GetProperty("false",  (OnSuccess_PropertyKey, PropertyValue) => t = true);
            Assert.IsNull(t);

            t = null;
            graph.GetProperty("Id",     (OnSuccess_PropertyKey, PropertyValue) => t = true);
            Assert.IsTrue(t.Value);

            t = null;
            graph.GetProperty("hello",  (OnSuccess_PropertyKey, PropertyValue) => t = true);
            Assert.IsTrue(t.Value);

            t = null;
            graph.GetProperty("graphs", (OnSuccess_PropertyKey, PropertyValue) => t = true);
            Assert.IsTrue(t.Value);
















            // IGenericPropertyVertexExtensions

            t = null;
            graph.GetProperty("false",  OnSuccess_PropertyValue => t = true, OnError => t = false);
            Assert.IsFalse(t.Value);

            t = null;
            graph.GetProperty("Id",     OnSuccess_PropertyValue => t = true, OnError => t = false);
            Assert.IsTrue(t.Value);

            t = null;
            graph.GetProperty("hello",  OnSuccess_PropertyValue => t = true, OnError => t = false);
            Assert.IsTrue(t.Value);

            t = null;
            graph.GetProperty("graphs", OnSuccess_PropertyValue => t = true, OnError => t = false);
            Assert.IsTrue(t.Value);



            t = null;
            graph.GetProperty("false",  (OnSuccess_PropertyKey, PropertyValue) => t = true, OnError => t = false);
            Assert.IsFalse(t.Value);

            t = null;
            graph.GetProperty("Id",     (OnSuccess_PropertyKey, PropertyValue) => t = true, OnError => t = false);
            Assert.IsTrue(t.Value);

            t = null;
            graph.GetProperty("hello",  (OnSuccess_PropertyKey, PropertyValue) => t = true, OnError => t = false);
            Assert.IsTrue(t.Value);

            t = null;
            graph.GetProperty("graphs", (OnSuccess_PropertyKey, PropertyValue) => t = true, OnError => t = false);
            Assert.IsTrue(t.Value);


            //Alice1.GetProperty("key", (p) => { Console.WriteLine(p); }, (v) => { Console.WriteLine(v.Id); });
            //Console.WriteLine(Alice1.GetProperty("l", (p) => { return p; }, (v) => { return v.Id; }));
            //Alice1.GetProperty("key", typeof(String), (p) => { Console.WriteLine(p); }, (v) => { Console.WriteLine(v.Id); });
            //Alice1.GetProperties(null);


        }

        #endregion

    }

}
