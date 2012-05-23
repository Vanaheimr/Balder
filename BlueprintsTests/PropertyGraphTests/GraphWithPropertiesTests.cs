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
using System.Collections.Generic;

using de.ahzf.Illias.Commons;
using de.ahzf.Illias.Commons.Collections;
using de.ahzf.Blueprints.PropertyGraphs;
using de.ahzf.Blueprints.PropertyGraphs.InMemory;

using NUnit.Framework;

#endregion

namespace de.ahzf.Blueprints.UnitTests.PropertyGraphTests
{

    /// <summary>
    /// PropertyGraph unit tests for creating vertices.
    /// </summary>
    [TestFixture]
    public class GraphWithPropertiesTests : InitGraph
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
            Assert.IsNotNull(graph.IdKey);
            Assert.IsNotNull(graph.RevIdKey);
            Assert.IsNotNull(graph.DescriptionKey);
            Assert.IsNull   (graph.Description);

        }

        #endregion

        #region GraphIdConstructorTest()

        [Test]
        public void GraphIdConstructorTest()
        {

            var graph = new PropertyGraph(123, null);

            Assert.IsNotNull(graph);
            Assert.IsNotNull(graph.IdKey);
            Assert.AreEqual(123UL, graph.Id);
            Assert.IsNotNull(graph.RevIdKey);
            Assert.IsNotNull(graph.DescriptionKey);
            Assert.IsNull   (graph.Description);

        }

        #endregion

        #region GraphIdAndHashCodeTest()

        [Test]
        public void GraphIdAndHashCodeTest()
        {

            var graph1 = new PropertyGraph(123, null) { Description = "The first graph." };
            var graph2 = new PropertyGraph(256, null) { Description = "The second graph." };
            var graph3 = new PropertyGraph(123, null) { Description = "The third graph." };

            Assert.IsNotNull(graph1.Id);
            Assert.IsNotNull(graph2.Id);
            Assert.IsNotNull(graph3.Id);

            Assert.IsNotNull(graph1.Description);
            Assert.IsNotNull(graph2.Description);
            Assert.IsNotNull(graph3.Description);

            Assert.IsNotNull(graph1.GetHashCode());
            Assert.IsNotNull(graph2.GetHashCode());
            Assert.IsNotNull(graph3.GetHashCode());

            Assert.AreEqual(graph1.Id, graph1.GetHashCode());
            Assert.AreEqual(graph2.Id, graph2.GetHashCode());
            Assert.AreEqual(graph3.Id, graph3.GetHashCode());

            Assert.AreEqual(graph1.Id, graph3.Id);
            Assert.AreEqual(graph1.GetHashCode(), graph3.GetHashCode());

            Assert.AreNotEqual(graph1.Description, graph2.Description);
            Assert.AreNotEqual(graph2.Description, graph3.Description);
            Assert.AreNotEqual(graph3.Description, graph1.Description);

        }

        #endregion

        #region GraphDescriptionTest()

        [Test]
        public void GraphDescriptionTest()
        {

            var graph = new PropertyGraph(g => g.SetProperty("hello", "world!"));

            Assert.IsNotNull(graph);
            Assert.IsNotNull(graph.IdKey);
            Assert.IsNotNull(graph.RevIdKey);
            Assert.IsNotNull(graph.DescriptionKey);
            Assert.IsNull(graph.Description);

            graph.Description = "This is a property graph!";
            Assert.IsNotNull(graph.Description);
            Assert.AreEqual("This is a property graph!", graph.Description);

        }

        #endregion

        #region GraphIdAndInitializerConstructor_AGraphElementTest()

        [Test]
        public void GraphIdAndInitializerConstructor_AGraphElementTest()
        {

            var graph = new PropertyGraph(123UL, g => g.SetProperty("hello",  "world!").
                                                        SetProperty("graphs", "are cool!").
                                                        SetProperty("Keep",   "it simple!"));

            Assert.IsNotNull(graph);
            Assert.IsNotNull(graph.IdKey);
            Assert.AreEqual(123UL, graph.Id);
            Assert.IsNotNull(graph.RevIdKey);

            // - AGraphElement -------------------------------------------------------------
            Assert.IsTrue (graph.ContainsKey("Id"));
            Assert.IsTrue (graph.ContainsKey("RevId"));
            Assert.IsTrue (graph.ContainsKey("hello"));
            Assert.IsFalse(graph.ContainsKey("world!"));
            Assert.IsTrue (graph.ContainsKey("graphs"));
            Assert.IsFalse(graph.ContainsKey("are cool!"));
            Assert.IsTrue (graph.ContainsKey("Keep"));
            Assert.IsFalse(graph.ContainsKey("it simple!"));

            Assert.IsTrue (graph.ContainsValue(123UL));
            Assert.IsFalse(graph.ContainsValue("hello"));
            Assert.IsTrue (graph.ContainsValue("world!"));
            Assert.IsFalse(graph.ContainsValue("graphs"));
            Assert.IsTrue (graph.ContainsValue("are cool!"));
            Assert.IsFalse(graph.ContainsValue("Keep"));
            Assert.IsTrue (graph.ContainsValue("it simple!"));

            Assert.IsTrue(graph.Contains("Id",     123UL));
            Assert.IsTrue(graph.Contains("hello",  "world!"));
            Assert.IsTrue(graph.Contains("graphs", "are cool!"));
            Assert.IsTrue(graph.Contains("Keep",   "it simple!"));

            Assert.IsTrue(graph.Contains(new KeyValuePair<String, Object>("Id",     123UL)));
            Assert.IsTrue(graph.Contains(new KeyValuePair<String, Object>("hello",  "world!")));
            Assert.IsTrue(graph.Contains(new KeyValuePair<String, Object>("graphs", "are cool!")));
            Assert.IsTrue(graph.Contains(new KeyValuePair<String, Object>("Keep",   "it simple!")));

            Assert.AreEqual(123UL,        graph["Id"]);
            Assert.AreEqual("world!",     graph["hello"]);
            Assert.AreEqual("are cool!",  graph["graphs"]);
            Assert.AreEqual("it simple!", graph["Keep"]);

            Object _Value;
            Assert.IsTrue(graph.TryGetProperty("Id", out _Value));
            Assert.AreEqual(123UL, _Value);

            Assert.IsTrue(graph.TryGetProperty("hello", out _Value));
            Assert.AreEqual("world!", _Value);

            Assert.IsTrue(graph.TryGetProperty("graphs", out _Value));
            Assert.AreEqual("are cool!", _Value);

            Assert.IsTrue(graph.TryGetProperty("Keep", out _Value));
            Assert.AreEqual("it simple!", _Value);


            var filtered01 = graph.GetProperties(null).ToList();
            Assert.NotNull(filtered01);
            Assert.AreEqual(5, filtered01.Count);

            var filtered02 = graph.GetProperties((k, v) => true).ToList();
            Assert.NotNull(filtered02);
            Assert.AreEqual(5, filtered02.Count);

            var filtered03 = graph.GetProperties((k, v) => k.StartsWith("he")).ToList();
            Assert.NotNull(filtered03);
            Assert.AreEqual(1, filtered03.Count);

            var filtered04 = graph.GetProperties((k, v) => k.StartsWith("xcvhe")).ToList();
            Assert.NotNull(filtered04);
            Assert.AreEqual(0, filtered04.Count);


            var AllProperties = new Dictionary<String, Object>();
            foreach (var property in graph)
                AllProperties.Add(property.Key, property.Value);

            Assert.AreEqual(5, AllProperties.Count);

            var keys   = graph.Keys.ToList();
            Assert.NotNull(keys);
            Assert.AreEqual(5, keys.Count);

            var values = graph.Values.ToList();
            Assert.NotNull(values);
            Assert.AreEqual(5, values.Count);


            var deleted1 = graph.Remove("hello");
            Assert.AreEqual("world!", deleted1);
            Assert.IsFalse (graph.ContainsKey("hello"));
            Assert.IsFalse (graph.ContainsValue("world!"));

            var deleted2 = graph.Remove("graphs", "are cool!");
            Assert.AreEqual("are cool!", deleted2);
            Assert.IsFalse (graph.ContainsKey("graphs"));
            Assert.IsFalse (graph.ContainsValue("are cool!"));

            var deleted3 = graph.Remove().ToList();
            Assert.IsNotNull(deleted3);
            Assert.AreEqual(0, deleted3.Count);

            var deleted4 = graph.Remove((k, v) => false).ToList();
            Assert.IsNotNull(deleted4);
            Assert.AreEqual(0, deleted4.Count);

            var deleted5 = graph.Remove((k, v) => k.StartsWith("Kee")).ToList();
            Assert.IsNotNull(deleted5);
            Assert.AreEqual(1, deleted5.Count);
            Assert.AreEqual("Keep",       deleted5[0].Key);
            Assert.AreEqual("it simple!", deleted5[0].Value);

        }

        #endregion


        #region ChangeIdTest1

        [Test]
        [ExpectedException(typeof(IdentificationChangeException))]
        public void ChangeIdTest1()
        {
            var graph = new PropertyGraph(123UL);
            graph.SetProperty("Id", 256UL);
        }

        #endregion

        #region ChangeRevIdTest1

        [Test]
        [ExpectedException(typeof(RevIdentificationChangeException))]
        public void ChangeRevIdTest1()
        {
            var graph = new PropertyGraph(123UL);
            graph.SetProperty("RevId", 256UL);
        }

        #endregion


        #region RemoveIdTest1

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveIdTest1()
        {
            var graph = new PropertyGraph(123UL);
            graph.Remove("Id");
        }

        #endregion

        #region RemoveRevIdTest1

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveRevIdTest1()
        {
            var graph = new PropertyGraph(123UL);
            graph.Remove("RevId");
        }

        #endregion

        #region RemoveIdTest2

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveIdTest2()
        {
            var graph = new PropertyGraph(123UL);
            graph.Remove("Id", 123UL);
        }

        #endregion

        #region RemoveRevIdTest2

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveRevIdTest2()
        {
            var graph = new PropertyGraph(123UL);
            graph.Remove("RevId", 0);
        }

        #endregion

        #region RemoveIdTest3

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveIdTest3()
        {
            var graph = new PropertyGraph(123UL);
            graph.Remove((k, v) => k == "Id");
        }

        #endregion

        #region RemoveRevIdTest3

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveRevIdTest3()
        {
            var graph = new PropertyGraph(123UL);
            graph.Remove((k, v) => k == "RevId");
        }

        #endregion


        #region AddPropertyEventTest()

        [Test]
        public void AddPropertyEventTest()
        {

            Nullable<Boolean> check = null;

            var graph = new PropertyGraph(123UL);

            graph.OnPropertyAdding += (g, key, value, vote) => { if (key.StartsWith("ke")) vote.Ok(); else vote.Deny(); };
            graph.OnPropertyAdded  += (g, key, value)       => check = true;

            graph.SetProperty("nokey", "value");
            Assert.IsNull(check);

            graph.SetProperty("key", "value");
            Assert.IsTrue(check.Value);

        }

        #endregion

        #region ChangePropertyEventTest()

        [Test]
        public void ChangePropertyEventTest()
        {

            Nullable<Boolean> check = null;

            var graph = new PropertyGraph(123UL, g => g.SetProperty("key", "value").
                                                        SetProperty("nokey", "value"));

            graph.OnPropertyChanging += (g, key, oldvalue, newvalue, vote) => { if (key.StartsWith("ke")) vote.Ok(); else vote.Deny(); };
            graph.OnPropertyChanged  += (g, key, oldvalue, newvalue)       => check = true;

            graph.SetProperty("nokey", "value");
            Assert.IsNull(check);

            graph.SetProperty("key", "value");
            Assert.IsTrue(check.Value);

        }

        #endregion

        #region RemovePropertyEventTest()

        [Test]
        public void RemovePropertyEventTest()
        {

            Nullable<Boolean> check = null;

            var graph = new PropertyGraph(123UL, g => g.SetProperty("key",   "value").
                                                        SetProperty("nokey", "value"));

            graph.OnPropertyRemoving += (g, key, value, vote) => { if (key.StartsWith("ke")) vote.Ok(); else vote.Deny(); };
            graph.OnPropertyRemoved  += (g, key, value)       => check = true;

            graph.Remove("nokey", "value");
            Assert.IsNull(check);
            Assert.IsTrue(graph.ContainsKey("nokey"));

            graph.Remove("key", "value");
            Assert.IsTrue(check.Value);
            Assert.IsFalse(graph.ContainsKey("key"));

        }

        #endregion


        #region IPropertiesExtensionMethodsTest()

        [Test]
        public void IPropertiesExtensionMethodsTest()
        {

            var graph = new PropertyGraph(123UL, g => g.SetProperty  (new KeyValuePair<String, Object>("hello",  "world!")).
                                                        SetProperties(new List<KeyValuePair<String, Object>>() { new KeyValuePair<String, Object>("graphs", "are cool!"),
                                                                                                                 new KeyValuePair<String, Object>("Keep",   "it simple!")}).
                                                        SetProperties(new Dictionary<String, Object>() { { "a", "b" }, { "c", "d" } }).
                                                        SetProperty  ("e", "f"));


            Assert.AreEqual(123UL,        graph.GetProperty("Id"));
            Assert.AreEqual("world!",     graph.GetProperty("hello"));
            Assert.AreEqual("are cool!",  graph.GetProperty("graphs"));
            Assert.AreEqual("it simple!", graph.GetProperty("Keep"));
            Assert.AreEqual("b",          graph.GetProperty("a"));
            Assert.AreEqual("d",          graph.GetProperty("c"));

            Assert.AreEqual(123UL,        graph.GetProperty("Id",     typeof(UInt64)));
            Assert.AreEqual("world!",     graph.GetProperty("hello",  typeof(String)));
            Assert.AreEqual("are cool!",  graph.GetProperty("graphs", typeof(String)));
            Assert.AreEqual("it simple!", graph.GetProperty("Keep",   typeof(String)));

            Assert.AreNotEqual("world!",  graph.GetProperty("hello",  typeof(UInt64)));


            // --[Action<TValue>]--------------------------------------------------------------

            Nullable<Boolean> check;
            check = null;
            graph.UseProperty("false",  OnSuccess_PropertyValue => check = true);
            Assert.IsNull(check);

            check = null;
            graph.UseProperty("Id",     OnSuccess_PropertyValue => check = true);
            Assert.IsTrue(check.Value);

            check = null;
            graph.UseProperty("hello",  OnSuccess_PropertyValue => check = true);
            Assert.IsTrue(check.Value);

            check = null;
            graph.UseProperty("graphs", OnSuccess_PropertyValue => check = true);
            Assert.IsTrue(check.Value);

            // --[Action<TKey, TValue>]--------------------------------------------------------

            check = null;
            graph.UseProperty("false",  (OnSuccess_PropertyKey, PropertyValue) => check = true);
            Assert.IsNull(check);

            check = null;
            graph.UseProperty("Id",     (OnSuccess_PropertyKey, PropertyValue) => check = true);
            Assert.IsTrue(check.Value);

            check = null;
            graph.UseProperty("hello",  (OnSuccess_PropertyKey, PropertyValue) => check = true);
            Assert.IsTrue(check.Value);

            check = null;
            graph.UseProperty("graphs", (OnSuccess_PropertyKey, PropertyValue) => check = true);
            Assert.IsTrue(check.Value);

            // --[Action<TValue>]--[Error]-----------------------------------------------------

            check = null;
            graph.UseProperty("false",  OnSuccess_PropertyValue => check = true, OnError => check = false);
            Assert.IsFalse(check.Value);

            check = null;
            graph.UseProperty("Id",     OnSuccess_PropertyValue => check = true, OnError => check = false);
            Assert.IsTrue(check.Value);

            check = null;
            graph.UseProperty("hello",  OnSuccess_PropertyValue => check = true, OnError => check = false);
            Assert.IsTrue(check.Value);

            check = null;
            graph.UseProperty("graphs", OnSuccess_PropertyValue => check = true, OnError => check = false);
            Assert.IsTrue(check.Value);

            // --[Action<TKey, TValue>]--[Error]-----------------------------------------------

            check = null;
            graph.UseProperty("false",  (OnSuccess_PropertyKey, PropertyValue) => check = true, OnError => check = false);
            Assert.IsFalse(check.Value);

            check = null;
            graph.UseProperty("Id",     (OnSuccess_PropertyKey, PropertyValue) => check = true, OnError => check = false);
            Assert.IsTrue(check.Value);

            check = null;
            graph.UseProperty("hello",  (OnSuccess_PropertyKey, PropertyValue) => check = true, OnError => check = false);
            Assert.IsTrue(check.Value);

            check = null;
            graph.UseProperty("graphs", (OnSuccess_PropertyKey, PropertyValue) => check = true, OnError => check = false);
            Assert.IsTrue(check.Value);


            // --[Func<TValue, TResult>]-------------------------------------------------------
#if !__MonoCS__
            Assert.AreEqual("world!?",      graph.PropertyFunc("hello",                 PropertyValue => { return (PropertyValue as String) + "?"; }));
            Assert.AreEqual(124UL,          graph.PropertyFunc("Id",                    PropertyValue => { return ( (UInt64) PropertyValue ) + 1; }));
            Assert.AreEqual(0,              graph.PropertyFunc("XYZ",                   PropertyValue => { return ( (UInt64) PropertyValue ) + 1; }));
            Assert.AreEqual(null,           graph.PropertyFunc("XYZ",                   PropertyValue => { return ( (String) PropertyValue ); }));
#endif
            Assert.AreEqual("world!?",      graph.PropertyFunc("hello", typeof(String), PropertyValue => { return (PropertyValue as String) + "?"; }));
            Assert.AreEqual(124UL,          graph.PropertyFunc("Id",    typeof(UInt64), PropertyValue => { return ( (UInt64) PropertyValue ) + 1; }));
            Assert.AreEqual(null,           graph.PropertyFunc("XYZ",   typeof(UInt64), PropertyValue => { return ( (UInt64) PropertyValue ) + 1; }));
            Assert.AreEqual(null,           graph.PropertyFunc("XYZ",   typeof(String), PropertyValue => { return ( (String) PropertyValue ); }));

#if !__MonoCS__
            // --[Func<TKey, TValue, TResult>]-------------------------------------------------
            Assert.AreEqual("hello world!", graph.PropertyFunc("hello",                 (OnSuccess_PropertyKey, PropertyValue) => { return OnSuccess_PropertyKey + " " + (PropertyValue as String); }));
            Assert.AreEqual("Id124",        graph.PropertyFunc("Id",                    (OnSuccess_PropertyKey, PropertyValue) => { return OnSuccess_PropertyKey + ((UInt64) PropertyValue + 1); }));
#endif
            Assert.AreEqual("hello world!", graph.PropertyFunc("hello", typeof(String), (OnSuccess_PropertyKey, PropertyValue) => { return OnSuccess_PropertyKey + " " + (PropertyValue as String); }));
            Assert.AreEqual("Id124",        graph.PropertyFunc("Id",    typeof(UInt64), (OnSuccess_PropertyKey, PropertyValue) => { return OnSuccess_PropertyKey + ((UInt64) PropertyValue + 1); }));


            // Filtered Keys/Values
            var FilteredKeys1   = graph.FilteredKeys  ((k, v) => true).ToList();
            Assert.NotNull(FilteredKeys1);
            Assert.AreEqual(8, FilteredKeys1.Count);

            var FilteredKeys2   = graph.FilteredKeys  ((k, v) => { if ((v as String).IsNotNullAndContains("!")) return true; else return false;}).ToList();
            Assert.NotNull(FilteredKeys2);
            Assert.AreEqual(3, FilteredKeys2.Count);


            var FilteredValues1 = graph.FilteredValues((k, v) => true).ToList();
            Assert.NotNull(FilteredValues1);
            Assert.AreEqual(8, FilteredValues1.Count);

            var FilteredValues2 = graph.FilteredValues((k, v) => { if ((v as String).IsNotNullAndContains("!")) return true; else return false; }).ToList();
            Assert.NotNull(FilteredValues2);
            Assert.AreEqual(3, FilteredValues2.Count);

        }

        #endregion

    }

}
