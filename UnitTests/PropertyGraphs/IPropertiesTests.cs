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

using de.ahzf.Blueprints.PropertyGraphs;
using de.ahzf.Blueprints.PropertyGraphs.InMemory;

using NUnit.Framework;

#endregion

namespace de.ahzf.Blueprints.UnitTests
{

    /// <summary>
    /// Unit tests for the IProperties class.
    /// </summary>
    [TestFixture]
    public class IPropertiesTests
    {

        #region Test()

        [Test]
        public void Test()
        {

            var _Graph = DemoGraphFactory.CreateDemoGraph();

            var Alice = _Graph.Vertices(v => v.GetProperty("name").ToString() == "Alice").First();
            Assert.IsNotNull(Alice);
            
            Alice = _Graph.Vertices("name", "Alice").First();
            Assert.IsNotNull(Alice);

            Object _Object;

            Alice.GetProperty("key");
            Alice.GetProperty("key", out _Object);
            Alice.GetProperty("key", typeof(String));
            Alice.GetProperty("key", (p) => { Console.WriteLine(p); }, (v) => { Console.WriteLine(v.Id); });
            Console.WriteLine(Alice.GetProperty("l", (p) => { return p; }, (v) => { return v.Id; }));
            Alice.GetProperty("key", typeof(String), (p) => { Console.WriteLine(p); }, (v) => { Console.WriteLine(v.Id); });
            Alice.GetProperties(null);

        }

        #endregion


    }


}
