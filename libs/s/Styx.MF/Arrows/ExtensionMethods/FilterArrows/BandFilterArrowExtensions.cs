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
    /// Sends a message when then received values are not within
    /// the bounding box of then given Min/Max-values.
    /// </summary>
    public static class BandFilterArrowExtensions
    {

        #region BandFilter(this ArrowSender, Lower, Upper)

        /// <summary>
        /// Sends a message when then received values are not within
        /// the bounding box of then given Min/Max-values.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="Lower">The lower bound of the passed values.</param>
        /// <param name="Upper">The upper bound of the passed values.</param>
        public static BandFilterArrow<TMessage> BandFilter<TMessage>(this IArrowSender<TMessage> ArrowSender, TMessage Lower, TMessage Upper)
            where TMessage : IComparable<TMessage>, IComparable
        {
            var _BandFilterArrow = new BandFilterArrow<TMessage>(Lower, Upper);
            ArrowSender.OnMessageAvailable += _BandFilterArrow.ReceiveMessage;
            return _BandFilterArrow;
        }

        #endregion

        #region BandFilter(this ArrowSender, Lower, Upper, MessageRecipient.Recipient, params MessageRecipient.Recipients)

        /// <summary>
        /// Sends a message when then received values are not within
        /// the bounding box of then given Min/Max-values.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="Lower">The lower bound of the passed values.</param>
        /// <param name="Upper">The upper bound of the passed values.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static BandFilterArrow<TMessage> BandFilter<TMessage>(this IArrowSender<TMessage> ArrowSender, TMessage Lower, TMessage Upper, MessageRecipient<TMessage> Recipient, params MessageRecipient<TMessage>[] Recipients)
            where TMessage : IComparable<TMessage>, IComparable
        {
            var _BandFilterArrow = new BandFilterArrow<TMessage>(Lower, Upper, Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _BandFilterArrow.ReceiveMessage;
            return _BandFilterArrow;
        }

        #endregion

        #region BandFilter(this ArrowSender, Lower, Upper, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// Sends a message when then received values are not within
        /// the bounding box of then given Min/Max-values.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="Lower">The lower bound of the passed values.</param>
        /// <param name="Upper">The upper bound of the passed values.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static BandFilterArrow<TMessage> BandFilter<TMessage>(this IArrowSender<TMessage> ArrowSender, TMessage Lower, TMessage Upper, IArrowReceiver<TMessage> Recipient, params IArrowReceiver<TMessage>[] Recipients)
            where TMessage : IComparable<TMessage>, IComparable
        {
            var _BandFilterArrow = new BandFilterArrow<TMessage>(Lower, Upper, Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _BandFilterArrow.ReceiveMessage;
            return _BandFilterArrow;
        }

        #endregion

    }

}
