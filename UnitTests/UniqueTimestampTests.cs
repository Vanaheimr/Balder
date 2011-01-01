/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

using de.ahzf.blueprints.Datastructures;

using NUnit.Framework;

#endregion

namespace de.ahzf.blueprints.UnitTests
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
