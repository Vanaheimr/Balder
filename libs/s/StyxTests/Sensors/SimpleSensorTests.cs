/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
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
using System.Diagnostics;

using de.ahzf.Vanaheimr.Styx;
using de.ahzf.Vanaheimr.Styx.Sensors.Simple;

using NUnit.Framework;

#endregion

namespace de.ahzf.Vanaheimr.Styx.Sensors.UnitTests
{

    /// <summary>
    /// Unit tests for SimpleSensor classes.
    /// </summary>
    [TestFixture]
    public class SimpleSensorTests
    {

        #region testCounterSensor()

        [Test]
        public void testCounterSensor()
        {

            var _CounterSensor = new CounterSensor("c1");
            var _Pipe          = new FuncPipe<Int64, String>(_Int64 => (_Int64 + 1).ToString(), _CounterSensor);
            var _Result        = _Pipe.Take(10).ToList();

            var _Counter = 0L;
            foreach (var _StringValue in _Result)
            {
                Assert.AreEqual((_Counter + 1).ToString(), _StringValue);
                _Counter++;
            }

            Assert.AreEqual(_Counter, 10);

        }

        #endregion

        #region testTimestampSensor()

        [Test]
        public void testTimestampSensor()
        {

            var _Stopwatch = new Stopwatch();
            _Stopwatch.Start();
            var _TimestampSensor = new TimestampSensor("ts1");
            var _Result          = _TimestampSensor.Take(10).ToList();
            _Stopwatch.Stop();

            Assert.AreEqual(10, _Result.Count);
            Assert.IsTrue(_Stopwatch.Elapsed.TotalSeconds < 20);

        }

        #endregion

        #region testTimestampSensorWithThrotteling()

        [Test]
        public void testTimestampSensorWithThrotteling()
        {

            var _Stopwatch = new Stopwatch();
            _Stopwatch.Start();
            var _TimestampSensor = new TimestampSensor("ts1") { MeasurementIntervall = TimeSpan.FromSeconds(2) };
            var _Result          = _TimestampSensor.Take(10).ToArray();
            _Stopwatch.Stop();

            Assert.AreEqual(10, _Result.Length);
            Assert.IsTrue(_Stopwatch.Elapsed.TotalSeconds >= 20);

            for (var i = 1; i < 10; i++)
                Assert.IsTrue(_Result[i] > _Result[i - 1]);

        }

        #endregion

        #region testRandomSensor()

        [Test]
        public void testRandomSensor()
        {

            var _RandomSensor = new RandomSensor("r");
            var _Pipe         = new FuncPipe<Double, Double>(_Double => _Double + 1);
            _Pipe.SetSourceCollection(_RandomSensor);
            var _Result       = _Pipe.Take(1000);

            Double _Sum = 0;
            foreach (var _Value in _Result)
                _Sum += _Value;

            var _Mean = _Sum / 1000;

            Assert.IsTrue(_Mean > 1.45);
            Assert.IsTrue(_Mean < 1.55);

        }

        #endregion

    }

}
