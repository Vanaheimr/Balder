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
    /// The SkipArrow simply sends the incoming message to the recipients
    /// without any processing, but skips the first n messages.
    /// </summary>
    public static class SkipArrowExtensions
    {

        #region SkipArrow(this ArrowSender, NumberOfMessagesToSkip)

        /// <summary>
        /// The SkipArrow simply sends the incoming message to the recipients
        /// without any processing, but skips the first n messages.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="NumberOfMessagesToSkip">The number of messages to skip.</param>
        public static SkipArrow<TMessage> SkipArrow<TMessage>(this IArrowSender<TMessage> ArrowSender, UInt32 NumberOfMessagesToSkip)
        {
            var _SkipArrow = new SkipArrow<TMessage>(NumberOfMessagesToSkip);
            ArrowSender.OnMessageAvailable += _SkipArrow.ReceiveMessage;
            return _SkipArrow;
        }

        #endregion

        #region SkipArrow(this ArrowSender, NumberOfMessagesToSkip, MessageRecipient.Recipient, params MessageRecipient.Recipients)

        /// <summary>
        /// The SkipArrow simply sends the incoming message to the recipients
        /// without any processing, but skips the first n messages.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="NumberOfMessagesToSkip">The number of messages to skip.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static SkipArrow<TMessage> SkipArrow<TMessage>(this IArrowSender<TMessage> ArrowSender, UInt32 NumberOfMessagesToSkip, MessageRecipient<TMessage> Recipient, params MessageRecipient<TMessage>[] Recipients)
        {
            var _SkipArrow = new SkipArrow<TMessage>(NumberOfMessagesToSkip, Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _SkipArrow.ReceiveMessage;
            return _SkipArrow;
        }

        #endregion

        #region SkipArrow(this ArrowSender, NumberOfMessagesToSkip, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// The SkipArrow simply sends the incoming message to the recipients
        /// without any processing, but skips the first n messages.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="NumberOfMessagesToSkip">The number of messages to skip.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static SkipArrow<TMessage> SkipArrow<TMessage>(this IArrowSender<TMessage> ArrowSender, UInt32 NumberOfMessagesToSkip, IArrowReceiver<TMessage> Recipient, params IArrowReceiver<TMessage>[] Recipients)
        {
            var _SkipArrow = new SkipArrow<TMessage>(NumberOfMessagesToSkip, Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _SkipArrow.ReceiveMessage;
            return _SkipArrow;
        }

        #endregion

    }

}
