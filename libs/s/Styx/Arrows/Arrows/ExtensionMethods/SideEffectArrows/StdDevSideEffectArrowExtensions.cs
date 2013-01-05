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

#region Usings

using System;

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    /// <summary>
    /// The StdDevSideEffectArrow produces a side effect that
    /// is the sliding standard deviation and the average of
    /// messages/objects that have passed through it.
    /// </summary>
    public static class StdDevSideEffectArrowExtensions
    {

        #region StdDevSideEffectArrow(this ArrowSender, InitialValue)

        /// <summary>
        /// The StdDevSideEffectArrow produces a side effect that
        /// is the sliding standard deviation and the average of
        /// messages/objects that have passed through it.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        public static StdDevSideEffectArrow StdDevSideEffectArrow(this IArrowSender<Double> ArrowSender)
        {
            var _StdDevSideEffectArrow = new StdDevSideEffectArrow();
            ArrowSender.OnMessageAvailable += _StdDevSideEffectArrow.ReceiveMessage;
            return _StdDevSideEffectArrow;
        }

        #endregion

        #region StdDevSideEffectArrow(this ArrowSender, MessageRecipient.Recipient, params MessageRecipient.Recipients)

        /// <summary>
        /// The StdDevSideEffectArrow produces a side effect that
        /// is the sliding standard deviation and the average of
        /// messages/objects that have passed through it.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static StdDevSideEffectArrow StdDevSideEffectArrow(this IArrowSender<Double> ArrowSender, MessageRecipient<Double> Recipient, params MessageRecipient<Double>[] Recipients)
        {
            var _CountArrow = new StdDevSideEffectArrow(Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _CountArrow.ReceiveMessage;
            return _CountArrow;
        }

        #endregion

        #region StdDevSideEffectArrow(this ArrowSender, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// The StdDevSideEffectArrow produces a side effect that
        /// is the sliding standard deviation and the average of
        /// messages/objects that have passed through it.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static StdDevSideEffectArrow StdDevSideEffectArrow(this IArrowSender<Double> ArrowSender, IArrowReceiver<Double> Recipient, params IArrowReceiver<Double>[] Recipients)
        {
            var _CountArrow = new StdDevSideEffectArrow(Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _CountArrow.ReceiveMessage;
            return _CountArrow;
        }

        #endregion

    }

}
