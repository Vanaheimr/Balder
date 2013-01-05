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

namespace de.ahzf.Styx
{

    /// <summary>
    /// The SameValueFilterArrow will not allow to send two
    /// consecutive identical messages twice.
    /// </summary>
    public static class SameValueFilterArrowExtensions
    {

        #region SameValueFilterArrow(this ArrowSender)

        /// <summary>
        /// The SameValueFilterArrow will not allow to send two
        /// consecutive identical messages twice.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        public static SameValueFilterArrow<TMessage> SameValueFilter<TMessage>(this IArrowSender<TMessage> ArrowSender)
            where TMessage : IEquatable<TMessage>
        {
            var _SameValueFilterArrow = new SameValueFilterArrow<TMessage>();
            ArrowSender.OnMessageAvailable += _SameValueFilterArrow.ReceiveMessage;
            return _SameValueFilterArrow;
        }

        #endregion

        #region SameValueFilterArrow(this ArrowSender, LastMessage)

        /// <summary>
        /// The SameValueFilterArrow will not allow to send two
        /// consecutive identical messages twice.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="LastMessage">Sets the initial value of LastMessage.</param>
        public static SameValueFilterArrow<TMessage> SameValueFilter<TMessage>(this IArrowSender<TMessage> ArrowSender, TMessage LastMessage)
            where TMessage : IEquatable<TMessage>
        {
            var _SameValueFilterArrow = new SameValueFilterArrow<TMessage>(LastMessage);
            ArrowSender.OnMessageAvailable += _SameValueFilterArrow.ReceiveMessage;
            return _SameValueFilterArrow;
        }

        #endregion

        #region SameValueFilterArrow(this ArrowSender, MessageRecipient.Recipient, params MessageRecipient.Recipients)

        /// <summary>
        /// The SameValueFilterArrow will not allow to send two
        /// consecutive identical messages twice.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static SameValueFilterArrow<TMessage> SameValueFilter<TMessage>(this IArrowSender<TMessage> ArrowSender, MessageRecipient<TMessage> Recipient, params MessageRecipient<TMessage>[] Recipients)
            where TMessage : IEquatable<TMessage>
        {
            var _SameValueFilterArrow = new SameValueFilterArrow<TMessage>(Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _SameValueFilterArrow.ReceiveMessage;
            return _SameValueFilterArrow;
        }

        #endregion

        #region SameValueFilterArrow(this ArrowSender, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// The SameValueFilterArrow will not allow to send two
        /// consecutive identical messages twice.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static SameValueFilterArrow<TMessage> SameValueFilter<TMessage>(this IArrowSender<TMessage> ArrowSender, IArrowReceiver<TMessage> Recipient, params IArrowReceiver<TMessage>[] Recipients)
            where TMessage : IEquatable<TMessage>
        {
            var _SameValueFilterArrow = new SameValueFilterArrow<TMessage>(Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _SameValueFilterArrow.ReceiveMessage;
            return _SameValueFilterArrow;
        }

        #endregion

    }

}
