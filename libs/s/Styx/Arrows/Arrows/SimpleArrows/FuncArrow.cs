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
    /// A generic Arrow transforming incoming messages into outgoing messages.
    /// </summary>
    /// <typeparam name="TIn">The type of the consuming messages/objects.</typeparam>
    /// <typeparam name="TOut">The type of the emitted messages/objects.</typeparam>
    public class FuncArrow<TIn, TOut> : AbstractArrow<TIn, TOut>
    {

        #region Data

        /// <summary>
        /// A delegate for transforming incoming messages into outgoing messages.
        /// </summary>
        protected readonly Func<TIn, TOut> MessageProcessor;

        #endregion

        #region Constructor(s)

        #region FuncArrow(MessageProcessor)

        /// <summary>
        /// An arrow transforming incoming messages into outgoing messages.
        /// </summary>
        /// <param name="MessageProcessor">A delegate for transforming incoming messages into outgoing messages.</param>
        public FuncArrow(Func<TIn, TOut> MessageProcessor)
        {

            #region Initial checks

            if (MessageProcessor == null)
                throw new ArgumentNullException("MessageProcessor", "The given 'MessageProcessor' delegate must not be null!");

            #endregion

            this.MessageProcessor = MessageProcessor;

        }

        #endregion

        #region FuncArrow(MessageProcessor, MessageRecipients.Recipient, params MessageRecipients.Recipients)

        /// <summary>
        /// An arrow transforming incoming messages into outgoing messages.
        /// </summary>
        /// <param name="MessageProcessor">A delegate for transforming incoming messages into outgoing messages.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public FuncArrow(Func<TIn, TOut> MessageProcessor, MessageRecipient<TOut> Recipient, params MessageRecipient<TOut>[] Recipients)
            : this(MessageProcessor)
        {
            if (Recipient  != null) SendTo(Recipient);
            if (Recipients != null) SendTo(Recipients);
        }

        #endregion

        #region FuncArrow(MessageProcessor, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// An arrow transforming incoming messages into outgoing messages.
        /// </summary>
        /// <param name="MessageProcessor">A delegate for transforming incoming messages into outgoing messages.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public FuncArrow(Func<TIn, TOut> MessageProcessor, IArrowReceiver<TOut> Recipient, params IArrowReceiver<TOut>[] Recipients)
            : this(MessageProcessor)
        {
            if (Recipient  != null) SendTo(Recipient);
            if (Recipients != null) SendTo(Recipients);
        }

        #endregion

        #endregion

        #region ProcessMessage(MessageIn, out MessageOut)

        /// <summary>
        /// Process the incoming message and return an outgoing message.
        /// </summary>
        /// <param name="MessageIn">The incoming message.</param>
        /// <param name="MessageOut">The outgoing message.</param>
        protected override Boolean ProcessMessage(TIn MessageIn, out TOut MessageOut)
        {
            MessageOut = MessageProcessor(MessageIn);
            return true;
        }

        #endregion

    }

}
