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
    /// The IdentityArrow is much like the IdentityArrow, but calls
    /// an Action &lt;S&gt; on every accepted message/object before
    /// forwarding it.
    /// </summary>
    public static class IdentityArrowExtensions
    {

        #region IdentityArrowExtensions(this ArrowSender)

        /// <summary>
        /// The IdentityArrow is much like the IdentityArrow, but calls
        /// an Action &lt;S&gt; on every accepted message/object before
        /// forwarding it.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        public static IdentityArrow<TMessage> IdentityArrow<TMessage>(this IArrowSender<TMessage> ArrowSender)
        {
            var _IdentityArrow = new IdentityArrow<TMessage>();
            ArrowSender.OnMessageAvailable += _IdentityArrow.ReceiveMessage;
            return _IdentityArrow;
        }

        #endregion

        #region IdentityArrowExtensions(this ArrowSender, MessageRecipient.Recipient, params MessageRecipient.Recipients)

        /// <summary>
        /// The IdentityArrow is much like the IdentityArrow, but calls
        /// an Action &lt;S&gt; on every accepted message/object before
        /// forwarding it.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static IdentityArrow<TMessage> IdentityArrow<TMessage>(this IArrowSender<TMessage> ArrowSender, MessageRecipient<TMessage> Recipient, params MessageRecipient<TMessage>[] Recipients)
        {
            var _IdentityArrow = new IdentityArrow<TMessage>(Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _IdentityArrow.ReceiveMessage;
            return _IdentityArrow;
        }

        #endregion

        #region IdentityArrowExtensions(this ArrowSender, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// The IdentityArrow is much like the IdentityArrow, but calls
        /// an Action &lt;S&gt; on every accepted message/object before
        /// forwarding it.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static IdentityArrow<TMessage> IdentityArrow<TMessage>(this IArrowSender<TMessage> ArrowSender, IArrowReceiver<TMessage> Recipient, params IArrowReceiver<TMessage>[] Recipients)
        {
            var _IdentityArrow = new IdentityArrow<TMessage>(Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _IdentityArrow.ReceiveMessage;
            return _IdentityArrow;
        }

        #endregion

    }

}
