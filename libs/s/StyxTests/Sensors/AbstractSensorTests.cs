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
using System.Collections.Generic;

using NUnit.Framework;

#endregion

namespace de.ahzf.Vanaheimr.Styx.Sensors.UnitTests
{

    #region DummySensor for testing the AbstractSensor<TId, TValue> class

    /// <summary>
    /// DummySensor for testing the AbstractSensor&lt;TId, TValue&gt; class
    /// </summary>
    public class DummySensor : AbstractSensor<SensorId, Int64>
    {

        /// <summary>
        /// DummySensor for testing the AbstractSensor&lt;TId, TValue&gt; class
        /// </summary>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        /// <param name="ValueUnit">The unit of the value (m, m², °C, °F, MBytes, ...).</param>
        public DummySensor(SensorId SensorId, String SensorName, String ValueUnit)
            : base(SensorId, SensorName, ValueUnit)
        { }

        /// <summary>
        /// Return an IEnumerator.
        /// </summary>
        public new System.Collections.IEnumerator GetEnumerator()
        {
            return new List<Int64>() { 0 }.GetEnumerator();
        }

        /// <summary>
        /// The current value of the sensor.
        /// </summary>
        public new object Current
        {
            get
            {
                return 0;
            }
        }

    }

    #endregion

    /// <summary>
    /// Unit tests for the AbstractSensor&lt;TId, TValue&gt; class.
    /// </summary>
    [TestFixture]
    public class AnstractSensorTests
    {

        #region testAbstractSensor_SensorIdIsNull()

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testAbstractSensor_SensorIdIsNull()
        {
            var _Sensor = new DummySensor(null, "SensorName", ".");
        }

        #endregion

        #region testAbstractSensor_SensorNameIsNull()

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testAbstractSensor_SensorNameIsNull()
        {
            var _Sensor = new DummySensor(SensorId.NewSensorId, null, ".");
        }

        #endregion

        #region testAbstractSensor_ValueUnitIsNull()

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testAbstractSensor_ValueUnitIsNull()
        {
            var _Sensor = new DummySensor(SensorId.NewSensorId, ".", null);
        }

        #endregion

        #region testAbstractSensor_EverythingIsNull()

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void testAbstractSensor_EverythingIsNull()
        {
            var _Sensor = new DummySensor(null, null, null);
        }

        #endregion

    }

}
