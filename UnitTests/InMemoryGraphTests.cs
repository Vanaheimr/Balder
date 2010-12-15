/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using System;
using System.Linq;

using de.ahzf.blueprints.InMemoryGraph;

using NUnit.Framework;

#endregion

namespace de.ahzf.blueprints.UnitTests
{

    [TestFixture]
    public class InMemoryGraphTests
    {

        #region Data

        private IGraph _InMemoryGraph;

        #endregion


        [SetUp]
        public void Init()
        {
            _InMemoryGraph = new InMemoryGraph.InMemoryGraph();
        }


        [Test]
        public void GraphInit()
        {
            
            Assert.AreEqual(_InMemoryGraph.GetVertices().Count(), 0);
            Assert.AreEqual(_InMemoryGraph.GetEdges().Count(), 0);

        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        [Ignore("Decide how to implement this!")]
        public void GraphInit2()
        {
            
        }

    }

}
