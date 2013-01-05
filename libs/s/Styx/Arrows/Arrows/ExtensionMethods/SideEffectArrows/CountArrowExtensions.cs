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
    /// The CountArrow produces a side effect that is the total
    /// number of messages/objects that have passed through it.
    /// </summary>
    public static class CountArrowExtensions
    {

        #region Count(this ArrowSender, InitialValue)

        /// <summary>
        /// The CountArrow produces a side effect that is the total
        /// number of messages/objects that have passed through it.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="InitialValue">The initial value of the counter.</param>
        public static CountArrow<TMessage> Count<TMessage>(this IArrowSender<TMessage> ArrowSender, Int64 InitialValue = 0L)
        {
            var _CountArrow = new CountArrow<TMessage>(InitialValue);
            ArrowSender.OnMessageAvailable += _CountArrow.ReceiveMessage;
            return _CountArrow;
        }

        #endregion

        #region Count(this ArrowSender, InitialValue, MessageRecipient.Recipient, params MessageRecipient.Recipients)

        /// <summary>
        /// The CountArrow produces a side effect that is the total
        /// number of messages/objects that have passed through it.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="InitialValue">The initial value of the counter.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static CountArrow<TMessage> Count<TMessage>(this IArrowSender<TMessage> ArrowSender, Int64 InitialValue, MessageRecipient<TMessage> Recipient, params MessageRecipient<TMessage>[] Recipients)
        {
            var _CountArrow = new CountArrow<TMessage>(InitialValue, Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _CountArrow.ReceiveMessage;
            return _CountArrow;
        }

        #endregion

        #region Count(this ArrowSender, InitialValue, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// The CountArrow produces a side effect that is the total
        /// number of messages/objects that have passed through it.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="InitialValue">The initial value of the counter.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static CountArrow<TMessage> Count<TMessage>(this IArrowSender<TMessage> ArrowSender, Int64 InitialValue, IArrowReceiver<TMessage> Recipient, params IArrowReceiver<TMessage>[] Recipients)
        {
            var _CountArrow = new CountArrow<TMessage>(InitialValue, Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _CountArrow.ReceiveMessage;
            return _CountArrow;
        }

        #endregion

    }

}
