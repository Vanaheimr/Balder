/*
 * Copyright (c) 2011-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
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

#if !SILVERLIGHT

#region Usings

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    // Attention: TIn and TOutput reversed ;)

    /// <summary>
    /// The common interface for any Arrow implementations sending messages of type E.
    /// </summary>
    /// <typeparam name="TOut">The type of the emitted messages/objects.</typeparam>
    public interface ISniper<TOut> : IArrowSender<TOut>
    {

        #region Properties

        /// <summary>
        /// The initial delay before starting to fire asynchronously.
        /// </summary>
        Nullable<TimeSpan> InitialDelay { get; }

        /// <summary>
        /// Whether the sniper is running asynchronously or not.
        /// </summary>
        Boolean IsTask { get; }

        /// <summary>
        /// Signals to a FireCancellationToken that it should be canceled.
        /// </summary>
        CancellationTokenSource FireCancellationTokenSource { get; }

        /// <summary>
        /// Propogates notification that the asynchronous fireing should be canceled.
        /// </summary>
        CancellationToken FireCancellationToken { get; }

        /// <summary>
        /// The internal task for fireing the messages/objects.
        /// </summary>
        Task FireTask { get; }

        /// <summary>
        /// The intervall will throttle the automatic measurement of passive
        /// sensors and the event notifications of active sensors.
        /// </summary>
        TimeSpan Intervall { get; set; }

        /// <summary>
        /// The amount of time in milliseconds a passive sensor
        /// will sleep if it is in throttling mode.
        /// </summary>
        Int32 ThrottlingSleepDuration { get; set; }

        /// <summary>
        /// The last time the sniper fired.
        /// </summary>
        DateTime LastFireTime { get; }

        #endregion


        /// <summary>
        /// Create as task for the message/object fireing.
        /// </summary>
        /// <param name="TaskCreationOption">Specifies flags that control optional behavior for the creation and execution of tasks.</param>
        /// <returns>The created task.</returns>
        Task AsTask(TaskCreationOptions TaskCreationOption = TaskCreationOptions.AttachedToParent);

        /// <summary>
        /// Starts the sniper fire!
        /// </summary>
        /// <param name="Async">Whether to run within a seperate task or not.</param>
        void StartToFire(Boolean Async = false);

    }

}

#endif
