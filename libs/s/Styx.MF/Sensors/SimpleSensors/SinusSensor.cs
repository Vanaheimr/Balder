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

#endregion

namespace de.ahzf.Styx.Sensors.Simple
{

    #region SinusSensor

    /// <summary>
    /// A sensor returning Sinus numbers.
    /// </summary>
    public class SinusSensor : SinusSensor<SensorId>
    {

        #region SinusSensor(SensorName)

        /// <summary>
        /// A sensor returning a sinus curve.
        /// </summary>
        /// <param name="SensorName">The name of the sensor.</param>
        public SinusSensor(String SensorName)
            : base(SensorId.NewSensorId, SensorName)
        { }

        #endregion

        #region SinusSensor(SensorId, SensorName)

        /// <summary>
        /// A sensor returning sinus curve.
        /// </summary>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        public SinusSensor(SensorId SensorId, String SensorName)
            : base(SensorId, SensorName)
        { }

        #endregion

        #region SinusSensor(SensorId_UInt64, SensorName)

        /// <summary>
        /// A sensor returning sinus curve.
        /// </summary>
        /// <param name="SensorId_UInt64">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        public SinusSensor(UInt64 SensorId_UInt64, String SensorName)
            : base(new SensorId(SensorId_UInt64), SensorName)
        { }

        #endregion

    }

    #endregion

    #region SinusSensor<TId>

    /// <summary>
    /// A sensor returning Sinus numbers.
    /// </summary>
    /// <typeparam name="TId">The type of the unique identification.</typeparam>
    public class SinusSensor<TId> : AbstractSensor<TId, Double>
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    {

        #region Data

        #endregion

        #region Properties

        /// <summary>
        /// The current arc of the sinus.
        /// </summary>
        public Double Arc       { get; set; }

        /// <summary>
        /// The frequency of the sinus wave.
        /// </summary>
        public Double Frequency { get; set; }

        /// <summary>
        /// The amplitude of the sinus wave.
        /// </summary>
        public Double Amplitude { get; set; }

        /// <summary>
        /// The XOffset of the arc of the sinus wave.
        /// </summary>
        public Double XOffset   { get; set; }

        /// <summary>
        /// The YOffset of the sinus wave.
        /// </summary>
        public Double YOffset   { get; set; }

        #endregion

        #region SinusSensor(SensorId, SensorName)

        /// <summary>
        /// A sensor returning Sinus numbers.
        /// </summary>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        public SinusSensor(TId SensorId, String SensorName)
            : base(SensorId, SensorName, "Double")
        {
            Arc              = 0;
            Frequency            = 1.0;
            Amplitude        = 1.0;
            XOffset          = 0.0;
            YOffset          = 0.0;
        }

        #endregion

        #region MoveNext()

        /// <summary>
        /// The current value of this performance counter.
        /// </summary>
        public override Boolean MoveNext()
        {

            // Respect sensor throttling in the AbstractSensor class.
            base.MoveNext();

            var diff      = (Now - LastMeasurementAt);
            Arc          += (diff.TotalMilliseconds / 1000 * Frequency) * 2 * Math.PI;
            Arc           = Arc % (2 * Math.PI);
            var diff2     = LastMeasurementAt - Now;

            _Current = Amplitude * Math.Sin(Arc + XOffset) + YOffset;

            return true;

        }

        #endregion

    }

    #endregion

}
