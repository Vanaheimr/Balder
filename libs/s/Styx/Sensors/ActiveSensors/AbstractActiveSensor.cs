/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
 * Autor: Achim 'ahzf' Friedland <achim.friedland@belectric.com>
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
using System.Threading;
using System.Threading.Tasks;

using de.ahzf.Vanaheimr.Styx;

#endregion

namespace de.ahzf.Vanaheimr.Styx.Sensors.Active
{

    /// <summary>
    /// The abstract active sensor.
    /// </summary>
    /// <typeparam name="TId">The type of the unique identification.</typeparam>
    /// <typeparam name="TValue">The type of the returned sensor data.</typeparam>
    public abstract class AbstractActiveSensor<TId, TValue> : AbstractSensor<TId, TValue>,
                                                              IActiveSensor<TId, TValue>

        where TId : IComparable, IComparable<TId>, IEquatable<TId>

    {

        #region Data

        private  Task                    _MeasurementTask;
        private  CancellationTokenSource _CancellationTokenSource;
        internal CancellationToken       _CancellationToken;

        #endregion

        #region Events

        #region OnSensorStarting / SensorIsStarting()

        /// <summary>
        /// An event that listeners can use to be notified whenever
        /// the sensor starts the OnNewDataAvailable service.
        /// </summary>
        public event SensorIsStartingEventHandler<TId> OnSensorStarting;

        /// <summary>
        /// Notify all OnSensorStarting event listeners.
        /// </summary>
        internal void SensorIsStarting()
        {
            if (OnSensorStarting != null)
                OnSensorStarting(this);
        }

        #endregion

        #region OnSensorStarted / SensorStarted()

        /// <summary>
        /// An event that listeners can use to be notified whenever the
        /// sensor finished to start the OnNewDataAvailable service.
        /// </summary>
        public event SensorStartedEventHandler<TId> OnSensorStarted;

        /// <summary>
        /// Notify all OnSensorStarted event listeners.
        /// </summary>
        internal void SensorStarted()
        {
            if (OnSensorStarted != null)
                OnSensorStarted(this);
        }

        #endregion

        #region OnSensorStopping / SensorIsStopping()

        /// <summary>
        /// An event that listeners can use to be notified whenever
        /// the sensor stops the OnNewDataAvailable service.
        /// </summary>
        public event SensorIsStoppingEventHandler<TId> OnSensorStopping;

        /// <summary>
        /// Notify all SensorIsStopping event listeners.
        /// </summary>
        internal void SensorIsStopping()
        {
            if (OnSensorStopping != null)
                OnSensorStopping(this);
        }

        #endregion

        #region OnSensorStopped / SensorStopped()

        /// <summary>
        /// An event that listeners can use to be notified whenever the
        /// sensor finished to stop the OnNewDataAvailable service.
        /// </summary>
        public event SensorStoppedEventHandler<TId> OnSensorStopped;

        /// <summary>
        /// Notify all OnSensorStopped event listeners.
        /// </summary>
        internal void SensorStopped()
        {
            if (OnSensorStopped != null)
                OnSensorStopped(this);
        }

        #endregion

        #region OnMessageAvailable

        /// <summary>
        /// An event that listeners can use to be notified whenever
        /// new sensor data is available.
        /// </summary>
        public event MessageRecipient<TValue> OnMessageAvailable;

        /// <summary>
        /// Notify all OnNewDataAvailable event listeners.
        /// </summary>
        internal void NewDataAvailable()
        {            
            if (OnMessageAvailable != null)
                OnMessageAvailable(this, this.Current);
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region AbstractActiveSensor(SensorId, SensorName, ValueUnit)

        /// <summary>
        /// Creates a new sensor having the given SensorId, SensorName and ValueUnit.
        /// </summary>
        /// <param name="SensorId">The unique identification of the sensor.</param>
        /// <param name="SensorName">The name of the sensor.</param>
        /// <param name="ValueUnit">The unit of the value (m, m², °C, °F, MBytes, ...).</param>
        public AbstractActiveSensor(TId SensorId, String SensorName, String ValueUnit)
            : base(SensorId, SensorName, ValueUnit)
        {
            this.IsActive = true;
        }

        #endregion

        #endregion


        #region Action<Object> MeasurementTask

        /// <summary>
        /// The measurement task.
        /// </summary>
        Action<Object> MeasurementTask = (Object SensorObject) =>
        {

            var      _IActiveSensor = SensorObject as AbstractActiveSensor<TId, TValue>;
            //DateTime _LastCheckTime = DateTime.MinValue;

            // Notify all event listeners
            _IActiveSensor.SensorStarted();

            while (!_IActiveSensor._CancellationToken.IsCancellationRequested)
            {

                //// Sleep if we are in throttling mode
                //while (_LastCheckTime + _IActiveSensor.Intervall > DateTime.Now)
                //    Thread.Sleep(_IActiveSensor.ThrottlingSleepDuration);

                // Get a new measurement
                _IActiveSensor.MoveNext();
                //_LastCheckTime = DateTime.Now;

                // Notify all event listeners
                if (!_IActiveSensor._CancellationToken.IsCancellationRequested)
                    _IActiveSensor.NewDataAvailable();

            }

            _IActiveSensor.SensorStopped();

        };

        #endregion


        #region StartMeasurements()

        /// <summary>
        /// Start sensor measurements.
        /// </summary>
        public Boolean StartMeasurements()
        {

            // Notify all event listeners
            SensorIsStarting();

            _CancellationTokenSource = new CancellationTokenSource();
            _CancellationToken       = _CancellationTokenSource.Token;
            _MeasurementTask         = new Task(MeasurementTask, this, _CancellationToken, TaskCreationOptions.AttachedToParent);
            _MeasurementTask.Start();

            return true;

        }

        #endregion

        #region StopMeasurements()

        /// <summary>
        /// Stop sensor measurements.
        /// </summary>
        public Boolean StopMeasurements()
        {

            // Notify all event listeners
            SensorIsStopping();

            // Cancel the MeasurementTask
            _CancellationTokenSource.Cancel();

            return true;

        }

        #endregion


        #region IArrowSender<TValue> Members

        public event CompletionRecipient OnCompleted;

        public event ExceptionRecipient OnError;

        public void SendTo(params MessageRecipient<TValue>[] Recipients)
        {
            throw new NotImplementedException();
        }

        public void SendTo(params IArrowReceiver<TValue>[] Recipients)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}
