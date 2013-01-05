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
    public class ActionArrow : AbstractArrow
    {

        #region Data

        private Action_Object Action;

        #endregion

        #region Constructor(s)

        #region ActionArrow(Action)

        /// <summary>
        /// The ActionArrow is much like the IdentityArrow, but calls
        /// an Action &lt;S&gt; on every accepted message/object before
        /// forwarding it.
        /// </summary>
        /// <param name="Action">An Action &lt;S&gt; to invoke on every accepted message/object before forwarding it.</param>
        public ActionArrow(Action_Object Action)
        {

            if (Action == null)
                throw new ArgumentNullException("The given Action<TIn> must not be null!");

            this.Action = Action;

        }

        #endregion

        #region ActionArrow(Action, MessageRecipient.Recipient, params MessageRecipient.Recipients)

        /// <summary>
        /// The ActionArrow is much like the IdentityArrow, but calls
        /// an Action &lt;S&gt; on every accepted message/object before
        /// forwarding it.
        /// </summary>
        /// <param name="Action">An Action &lt;S&gt; to invoke on every accepted message/object before forwarding it.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">The recipients of the processed messages.</param>
        public ActionArrow(Action_Object Action, MessageRecipient Recipient, params MessageRecipient[] Recipients)
            : base(Recipient, Recipients)
        {

            if (Action == null)
                throw new ArgumentNullException("The given Action<TIn> must not be null!");

            this.Action = Action;

        }

        #endregion

        #region ActionArrow(Action, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// The ActionArrow is much like the IdentityArrow, but calls
        /// an Action &lt;S&gt; on every accepted message/object before
        /// forwarding it.
        /// </summary>
        /// <param name="Action">An Action &lt;S&gt; to invoke on every accepted message/object before forwarding it.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">The recipients of the processed messages.</param>
        public ActionArrow(Action_Object Action, IArrowReceiver Recipient, params IArrowReceiver[] Recipients)
            : base(Recipient, Recipients)
        {

            if (Action == null)
                throw new ArgumentNullException("The given Action<TIn> must not be null!");

            this.Action = Action;

        }

        #endregion

        #endregion

        #region ProcessMessage(MessageIn, out MessageOut)

        /// <summary>
        /// Process the incoming message and return an outgoing message.
        /// </summary>
        /// <param name="MessageIn">The incoming message.</param>
        /// <param name="MessageOut">The outgoing message.</param>
        protected override Boolean ProcessMessage(Object MessageIn, out Object MessageOut)
        {
            Action(MessageIn);
            MessageOut = MessageIn;
            return true;
        }

        #endregion

    }

}
