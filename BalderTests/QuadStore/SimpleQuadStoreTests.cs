/*
 * Copyright (c) 2010-2013 Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Alviss <http://www.github.com/Vanaheimr/Alviss>
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

#endregion

namespace eu.Vanaheimr.Alviss.UnitTests
{

    /// <summary>
    /// Simple unit tests for the QuadStore class.
    /// </summary>
    [TestFixture]
    public class SimpleQuadStoreTests
    {

        #region (static) InitQuadStore()

        /// <summary>
        /// Init a new QuadStore.
        /// </summary>
        public static IQuadStore<String, String, String, String> InitQuadStore()
        {
            return new QuadStore<String, String, String, String>(
                           SystemId:        "System1",
                           QuadIdConverter: (QuadId) => QuadId.ToString(),
                           DefaultContext:  ()       => "0");
        }

        #endregion

        #region (static) CreateTestStore()

        /// <summary>
        /// Init and populate a QuadStore with testing data.
        /// </summary>
        public static IQuadStore<String, String, String, String> CreateTestStore()
        {

            var QuadStore = InitQuadStore();

            Assert.AreEqual(0, QuadStore.NumberOfQuads);
            Assert.AreEqual(0, QuadStore.NumberOfSubjects);
            Assert.AreEqual(0, QuadStore.NumberOfPredicates);
            Assert.AreEqual(0, QuadStore.NumberOfObjects);
            Assert.AreEqual(0, QuadStore.NumberOfContexts);

            // -----------------------------------------------

            var quad1 = QuadStore.Add("Alice", "knows", "Bob");

            Assert.AreEqual(1, QuadStore.NumberOfQuads);
            Assert.AreEqual(1, QuadStore.NumberOfSubjects);
            Assert.AreEqual(1, QuadStore.NumberOfPredicates);
            Assert.AreEqual(1, QuadStore.NumberOfObjects);
            Assert.AreEqual(1, QuadStore.NumberOfContexts);

            // -----------------------------------------------

            var quad2 = QuadStore.Add("Alice", "knows", "Dave");

            Assert.AreEqual(2, QuadStore.NumberOfQuads);
            Assert.AreEqual(1, QuadStore.NumberOfSubjects);
            Assert.AreEqual(1, QuadStore.NumberOfPredicates);
            Assert.AreEqual(2, QuadStore.NumberOfObjects);
            Assert.AreEqual(1, QuadStore.NumberOfContexts);

            // -----------------------------------------------

            var quad3 = QuadStore.Add("Bob", "knows", "Carol");

            Assert.AreEqual(3, QuadStore.NumberOfQuads);
            Assert.AreEqual(2, QuadStore.NumberOfSubjects);
            Assert.AreEqual(1, QuadStore.NumberOfPredicates);
            Assert.AreEqual(3, QuadStore.NumberOfObjects);
            Assert.AreEqual(1, QuadStore.NumberOfContexts);

            // -----------------------------------------------

            var quad4 = QuadStore.Add("Alice", "loves", "Dave");

            Assert.AreEqual(4, QuadStore.NumberOfQuads);
            Assert.AreEqual(2, QuadStore.NumberOfSubjects);
            Assert.AreEqual(2, QuadStore.NumberOfPredicates);
            Assert.AreEqual(3, QuadStore.NumberOfObjects);
            Assert.AreEqual(1, QuadStore.NumberOfContexts);

            // -----------------------------------------------
            
            var quad5 = QuadStore.Add("Eve", "loves", "Alice");

            Assert.AreEqual(5, QuadStore.NumberOfQuads);
            Assert.AreEqual(3, QuadStore.NumberOfSubjects);
            Assert.AreEqual(2, QuadStore.NumberOfPredicates);
            Assert.AreEqual(4, QuadStore.NumberOfObjects);
            Assert.AreEqual(1, QuadStore.NumberOfContexts);

            // -----------------------------------------------

            var quad6 = QuadStore.Add("Carol", "loves", "Alice");

            Assert.AreEqual(6, QuadStore.NumberOfQuads);
            Assert.AreEqual(4, QuadStore.NumberOfSubjects);
            Assert.AreEqual(2, QuadStore.NumberOfPredicates);
            Assert.AreEqual(4, QuadStore.NumberOfObjects);
            Assert.AreEqual(1, QuadStore.NumberOfContexts);

            // -----------------------------------------------

            var quad7 = QuadStore.Add("Alice", "likes", "Bob");

            Assert.AreEqual(7, QuadStore.NumberOfQuads);
            Assert.AreEqual(4, QuadStore.NumberOfSubjects);
            Assert.AreEqual(3, QuadStore.NumberOfPredicates);
            Assert.AreEqual(4, QuadStore.NumberOfObjects);
            Assert.AreEqual(1, QuadStore.NumberOfContexts);

            // ===============================================

            return QuadStore;

        }

        #endregion


        #region ConstructorTest()

        [Test]
        public void ConstructorTest()
        {
            Assert.IsNotNull(InitQuadStore());
        }

        #endregion

        #region AddQuadsTest()

        [Test]
        public void AddQuadsTest()
        {
            Assert.IsNotNull(CreateTestStore());
        }

        #endregion


        #region AllOfTest()

        [Test]
        public void AllOfTest()
        {

            var QuadStore = CreateTestStore();

            var AllOf_Alice1 = QuadStore.AllOf("Alice").ToList();
            var AllOf_Alice2 = QuadStore.GetQuads(Subject: "Alice").ToList();

            Assert.AreEqual(4, AllOf_Alice1.Count);
            Assert.AreEqual(4, AllOf_Alice2.Count);

            foreach (var quad in AllOf_Alice1)
                Assert.AreEqual("Alice", quad.Subject);

            foreach (var quad in AllOf_Alice2)
                Assert.AreEqual("Alice", quad.Subject);

        }

        #endregion

        #region AllByTest()

        [Test]
        public void AllByTest()
        {

            var QuadStore = CreateTestStore();

            var AllBy_Love1 = QuadStore.AllBy("loves").ToList();
            var AllBy_Love2 = QuadStore.GetQuads(Predicate: "loves").ToList();

            Assert.AreEqual(3, AllBy_Love1.Count);
            Assert.AreEqual(3, AllBy_Love2.Count);

            foreach (var quad in AllBy_Love1)
                Assert.AreEqual("loves", quad.Predicate);

            foreach (var quad in AllBy_Love2)
                Assert.AreEqual("loves", quad.Predicate);

        }

        #endregion

        #region AllWithTest()

        [Test]
        public void AllWithTest()
        {

            var QuadStore = CreateTestStore();

            var AllFrom_Context1 = QuadStore.AllFrom("0").ToList();
            var AllFrom_Context2 = QuadStore.GetQuads(Context: "0").ToList();

            Assert.AreEqual(7, AllFrom_Context1.Count);
            Assert.AreEqual(7, AllFrom_Context2.Count);

            foreach (var quad in AllFrom_Context1)
                Assert.AreEqual("0", quad.Context);

            foreach (var quad in AllFrom_Context2)
                Assert.AreEqual("0", quad.Context);

        }

        #endregion


        #region AllBetweenTest()

        [Test]
        public void AllBetweenTest()
        {

            var QuadStore = CreateTestStore();

            var AllBetween = QuadStore.GetQuads(Subject: "Alice", Object: "Bob").ToList();

            Assert.AreEqual(2, AllBetween.Count);

            foreach (var quad in AllBetween)
            {
                Assert.AreEqual("Alice", quad.Subject);
                Assert.AreEqual("Bob",   quad.Object);
            }

        }

        #endregion

        #region SelectTest()

        [Test]
        public void SelectTest()
        {

            var QuadStore = CreateTestStore();

            var SelectQuads = QuadStore.SelectQuads(SubjectSelector: s => String.Compare(s, "Alice") >= 0,
                                                    ObjectSelector:  o => o.EndsWith("e")).ToList();

            Assert.AreEqual(4, SelectQuads.Count);

            foreach (var quad in SelectQuads)
            {
                Assert.IsTrue(String.Compare(quad.Subject, "Alice") >= 0);
                Assert.IsTrue(quad.Object.EndsWith("e"));
            }

        }

        #endregion

    }

}
