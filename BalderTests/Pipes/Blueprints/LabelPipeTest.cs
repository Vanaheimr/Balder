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

using NUnit.Framework;

using de.ahzf.Illias.Commons;
using de.ahzf.Vanaheimr.Blueprints.UnitTests;

#endregion

namespace de.ahzf.Vanaheimr.Balder.UnitTests.Blueprints
{

    [TestFixture]
    public class LabelPipeTest
    {

        #region testLabels()

        [Test]
        public void testLabels()
        {

            var _Graph     = TinkerGraphFactory.CreateTinkerGraph();
            var _LabelPipe = new LabelPipe<String>();

            _LabelPipe.SetSourceCollection(_Graph.VertexById(1).OutEdges());

            var _Counter = 0;
            while (_LabelPipe.MoveNext())
            {
                String label = _LabelPipe.Current;
                Assert.IsTrue(label.Equals("knows") || label.Equals("created"));
                _Counter++;
            }

            Assert.AreEqual(3, _Counter);

        }

        #endregion

    }

}
