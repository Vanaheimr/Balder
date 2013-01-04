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

using de.ahzf.Illias.Commons.Collections;

using NUnit.Framework;

#endregion

namespace de.ahzf.Vanaheimr.Blueprints.UnitTests
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

            Object _Object;

            var _Graph = DemoGraphFactory.CreateDemoGraph();

            var Alice1 = _Graph.Vertices(v => v["name"].ToString() == "Alice").First();
            Assert.IsNotNull(Alice1);

            Alice1.GetProperty("key");
            Alice1.TryGetProperty("key", out _Object);
            Alice1.GetProperty("key", typeof(String));
            Alice1.UseProperty("key", (obj)      => { Console.WriteLine(obj); }, (key) => { Console.WriteLine(key); });
            Alice1.UseProperty("key", (key, obj) => { Console.WriteLine(obj); }, (key) => { Console.WriteLine(key); });
#if !__MonoCS__            
            Console.WriteLine(Alice1.PropertyFunc("l", (obj) => { return obj; }));
#endif
            Alice1.UseProperty("key", typeof(String), (obj)      => { Console.WriteLine(obj); }, (key) => { Console.WriteLine(key); });
            Alice1.UseProperty("key", typeof(String), (key, obj) => { Console.WriteLine(obj); }, (key) => { Console.WriteLine(key); });
            Alice1.GetProperties(null);
            

            var Alice2 = _Graph.Vertices("name", "Alice").First();
            Assert.IsNotNull(Alice2);

            Alice2.GetProperty("key");
            Alice2.TryGetProperty("key", out _Object);
            Alice2.GetProperty("key", typeof(String));
            Alice2.UseProperty("key", (obj)      => { Console.WriteLine(obj); }, (key) => { Console.WriteLine(key); });
            Alice2.UseProperty("key", (key, obj) => { Console.WriteLine(obj); }, (key) => { Console.WriteLine(key); });
#if !__MonoCS__
            Console.WriteLine(Alice2.PropertyFunc("l", (obj) => { return obj; }));
#endif
            Alice2.UseProperty("key", typeof(String), (obj)      => { Console.WriteLine(obj); }, (key) => { Console.WriteLine(key); });
            Alice2.UseProperty("key", typeof(String), (key, obj) => { Console.WriteLine(obj); }, (key) => { Console.WriteLine(key); });
            Alice2.GetProperties(null);

        }

        #endregion


    }


}
