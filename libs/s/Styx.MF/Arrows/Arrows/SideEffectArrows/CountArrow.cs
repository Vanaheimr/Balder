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
using System.Threading;

#endregion

namespace de.ahzf.Styx
{

    /// <summary>
    /// The CountArrow produces a side effect that is the total
    /// number of messages/objects that have passed through it.
    /// </summary>
    /// <typeparam name="TMessage">The type of the consuming and emitting messages/objects.</typeparam>
    public class CountArrow<TMessage> : AbstractSideEffectArrow<TMessage, TMessage, Int64>
    {

        #region Constructor(s)

        #region CountArrow(InitialValue = 0)

        /// <summary>
        /// The CountArrow produces a side effect that is the total
        /// number of messages/objects that have passed through it.
        /// </summary>
        /// <param name="InitialValue">The initial value of the counter.</param>
        public CountArrow(Int64 InitialValue = 0L)
        {
            _SideEffect = InitialValue;
        }

        #endregion

        #region CountArrow(InitialValue, MessageRecipients.Recipient, params MessageRecipients.Recipients)

        /// <summary>
        /// The CountArrow produces a side effect that is the total
        /// number of messages/objects that have passed through it.
        /// </summary>
        /// <param name="InitialValue">The initial value of the counter.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">The recipients of the processed messages.</param>
        public CountArrow(Int64 InitialValue, MessageRecipient<TMessage> Recipient, params MessageRecipient<TMessage>[] Recipients)
            : base(Recipient, Recipients)
        {
            _SideEffect = InitialValue;
        }

        #endregion

        #region CountArrow(InitialValue, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// The CountArrow produces a side effect that is the total
        /// number of messages/objects that have passed through it.
        /// </summary>
        /// <param name="InitialValue">The initial value of the counter.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">The recipients of the processed messages.</param>
        public CountArrow(Int64 InitialValue, IArrowReceiver<TMessage> Recipient, params IArrowReceiver<TMessage>[] Recipients)
            : base(Recipient, Recipients)
        {
            _SideEffect = InitialValue;
        }

        #endregion

        #endregion

        #region ProcessMessage(MessageIn, out MessageOut)

        /// <summary>
        /// Process the incoming message and return an outgoing message.
        /// </summary>
        /// <param name="MessageIn">The incoming message.</param>
        /// <param name="MessageOut">The outgoing message.</param>
        protected override Boolean ProcessMessage(TMessage MessageIn, out TMessage MessageOut)
        {
            MessageOut = MessageIn;
            Interlocked.Increment(ref _SideEffect);
            return true;
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Returns a string representation of this Arrow.
        /// </summary>
        public override String ToString()
        {
            return base.ToString() + "<" + _SideEffect + ">";
        }

        #endregion

    }

}
