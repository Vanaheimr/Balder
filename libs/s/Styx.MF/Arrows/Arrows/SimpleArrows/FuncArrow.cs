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
    /// A generic Arrow transforming incoming messages into outgoing messages.
    /// </summary>
    /// <typeparam name="TMessage">The type of the consuming messages/objects.</typeparam>
    /// <typeparam name="TOutput">The type of the emitted messages/objects.</typeparam>
    public class FuncArrow<TMessage, TOutput> : AbstractArrow<TMessage, TOutput>
    {

        #region Data

        /// <summary>
        /// A generic transformation of incoming messages into outgoing messages.
        /// </summary>
        protected readonly Func<TMessage, TOutput> Func;

        #endregion

        #region Constructor(s)

        #region FuncArrow(Func)

        /// <summary>
        /// A generic Arrow transforming incoming messages into outgoing messages.
        /// </summary>
        /// <param name="Func">A generic transformation of incoming messages into outgoing messages.</param>
        public FuncArrow(Func<TMessage, TOutput> Func)
        {
            
            if (Func == null)
                throw new ArgumentNullException("The given message transformation func must not be null!");

            this.Func = Func;

        }

        #endregion

        #region FuncArrow(Func, MessageRecipients.Recipient, params MessageRecipients.Recipients)

        /// <summary>
        /// A generic Arrow transforming incoming messages into outgoing messages.
        /// </summary>
        /// <param name="Func">A generic transformation of incoming messages into outgoing messages.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public FuncArrow(Func<TMessage, TOutput> Func, MessageRecipient<TOutput> Recipient, params MessageRecipient<TOutput>[] Recipients)
            : base(Recipient, Recipients)
        {

            if (Func == null)
                throw new ArgumentNullException("The given message transformation func must not be null!");

            this.Func = Func;

        }

        #endregion

        #region FuncArrow(Func, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// A generic Arrow transforming incoming messages into outgoing messages.
        /// </summary>
        /// <param name="Func">A generic transformation of incoming messages into outgoing messages.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public FuncArrow(Func<TMessage, TOutput> Func, IArrowReceiver<TOutput> Recipient, params IArrowReceiver<TOutput>[] Recipients)
            : base(Recipient, Recipients)
        {

            if (Func == null)
                throw new ArgumentNullException("The given message transformation func must not be null!");

            this.Func = Func;

        }

        #endregion

        #endregion

        #region ProcessMessage(MessageIn, out MessageOut)

        /// <summary>
        /// Process the incoming message and return an outgoing message.
        /// </summary>
        /// <param name="MessageIn">The incoming message.</param>
        /// <param name="MessageOut">The outgoing message.</param>
        protected override Boolean ProcessMessage(TMessage MessageIn, out TOutput MessageOut)
        {
            MessageOut = Func(MessageIn);
            return true;
        }

        #endregion

    }

}
