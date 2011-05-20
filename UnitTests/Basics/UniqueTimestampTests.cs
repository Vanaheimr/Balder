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
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

using NUnit.Framework;

#endregion

namespace de.ahzf.Blueprints.UnitTests
{

    [TestFixture]
    public class UniqueTimestampTests
    {

        #region struct: UniqueTimestamps

        private struct UniqueTimestamps
        {

            public Int32  ThreadId;
            public UInt64 Timestamp;

            public override String ToString()
            {
                return ThreadId + ": " + Timestamp;
            }

        }

        #endregion


        #region ParallelTest()

        /// <summary>
        /// This test should use all of your available processors.
        /// Should be okay on single core, but I could not test it!
        /// </summary>
        [Test]
        public void ParallelTest()
        {

            var _NumberOfItems = 100000;
            var _ConcurrentBag = new ConcurrentBag<UniqueTimestamps>();

            var _Task1 = Task.Factory.StartNew(() =>
            {
                Parallel.For(0, _NumberOfItems, i =>
                {
                    var _UniqueTS = new UniqueTimestamps();
                    _UniqueTS.ThreadId  = Thread.CurrentThread.ManagedThreadId;
                    _UniqueTS.Timestamp = UniqueTimestamp.Ticks;
                    _ConcurrentBag.Add(_UniqueTS);
                });
            });

            var _Task2 = Task.Factory.StartNew(() =>
            {
                Parallel.For(0, _NumberOfItems, j =>
                {
                    var _UniqueTS = new UniqueTimestamps();
                    _UniqueTS.ThreadId  = Thread.CurrentThread.ManagedThreadId;
                    _UniqueTS.Timestamp = UniqueTimestamp.Ticks;
                    _ConcurrentBag.Add(_UniqueTS);
                });
            });

            Task.WaitAll(_Task1, _Task2);

            var _NumberOfThreads    = (from item in _ConcurrentBag select item.ThreadId ).Distinct().Count();
            var _NumberOfTimestamps = (from item in _ConcurrentBag select item.Timestamp).Distinct().Count();

            Assert.AreEqual(_NumberOfThreads, Environment.ProcessorCount);
            Assert.AreEqual(_NumberOfTimestamps, 2 * _NumberOfItems);

        }

        #endregion

    }

}
