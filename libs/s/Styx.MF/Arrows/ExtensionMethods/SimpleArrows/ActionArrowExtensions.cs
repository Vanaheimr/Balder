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
    /// The ActionArrow is much like the IdentityArrow, but calls
    /// an Action &lt;S&gt; on every accepted message/object before
    /// forwarding it.
    /// </summary>
    public static class ActionArrowExtensions
    {

        #region ActionArrowExtensions(this ArrowSender, Action)

        /// <summary>
        /// The ActionArrow is much like the IdentityArrow, but calls
        /// an Action &lt;S&gt; on every accepted message/object before
        /// forwarding it.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="Action">An Action &lt;S&gt; to invoke on every accepted message/object before forwarding it.</param>
        public static ActionArrow ActionArrow(this IArrowSender ArrowSender, Action_Object Action)
        {
            var _ActionArrow = new ActionArrow(Action);
            ArrowSender.OnMessageAvailable += _ActionArrow.ReceiveMessage;
            return _ActionArrow;
        }

        #endregion

        #region ActionArrowExtensions(this ArrowSender, Action, MessageRecipient.Recipient, params MessageRecipient.Recipients)

        /// <summary>
        /// The ActionArrow is much like the IdentityArrow, but calls
        /// an Action &lt;S&gt; on every accepted message/object before
        /// forwarding it.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="Action">An Action &lt;S&gt; to invoke on every accepted message/object before forwarding it.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static ActionArrow ActionArrow(this IArrowSender ArrowSender, Action_Object Action, MessageRecipient Recipient, params MessageRecipient[] Recipients)
        {
            var _ActionArrow = new ActionArrow(Action, Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _ActionArrow.ReceiveMessage;
            return _ActionArrow;
        }

        #endregion

        #region ActionArrowExtensions(this ArrowSender, Action, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// The ActionArrow is much like the IdentityArrow, but calls
        /// an Action &lt;S&gt; on every accepted message/object before
        /// forwarding it.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="Action">An Action &lt;S&gt; to invoke on every accepted message/object before forwarding it.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static ActionArrow ActionArrow(this IArrowSender ArrowSender, Action_Object Action, IArrowReceiver Recipient, params IArrowReceiver[] Recipients)
        {
            var _ActionArrow = new ActionArrow(Action, Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _ActionArrow.ReceiveMessage;
            return _ActionArrow;
        }

        #endregion

    }

}
