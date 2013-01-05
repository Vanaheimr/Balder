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
    /// The SkipArrow simply sends the incoming message to the recipients
    /// without any processing, but skips the first n messages.
    /// </summary>
    public class SkipArrow : AbstractArrow
    {

        #region Data

        private readonly UInt32 NumberOfMessagesToSkip;
        private          Int32  Counter;

        #endregion

        #region Constructor(s)

        #region SkipArrow(NumberOfMessagesToSkip)

        /// <summary>
        /// The SkipArrow simply sends the incoming message to the recipients
        /// without any processing, but skips the first n messages.
        /// </summary>
        /// <param name="NumberOfMessagesToSkip">The number of messages to skip.</param>
        public SkipArrow(UInt32 NumberOfMessagesToSkip)
        {
            this.NumberOfMessagesToSkip = NumberOfMessagesToSkip;
        }

        #endregion

        #region SkipArrow(NumberOfMessagesToSkip, MessageRecipients.Recipient, params MessageRecipients.Recipients)

        /// <summary>
        /// The SkipArrow simply sends the incoming message to the recipients
        /// without any processing, but skips the first n messages.
        /// </summary>
        /// <param name="NumberOfMessagesToSkip">The number of messages to skip.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">The recipients of the processed messages.</param>
        public SkipArrow(UInt32 NumberOfMessagesToSkip, MessageRecipient Recipient, params MessageRecipient[] Recipients)
            : base(Recipient, Recipients)
        {
            this.NumberOfMessagesToSkip = NumberOfMessagesToSkip;
        }

        #endregion

        #region SkipArrow(NumberOfMessagesToSkip, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// The SkipArrow simply sends the incoming message to the recipients
        /// without any processing, but skips the first n messages.
        /// </summary>
        /// <param name="NumberOfMessagesToSkip">The number of messages to skip.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">The recipients of the processed messages.</param>
        public SkipArrow(UInt32 NumberOfMessagesToSkip, IArrowReceiver Recipient, params IArrowReceiver[] Recipients)
            : base(Recipient, Recipients)
        {
            this.NumberOfMessagesToSkip = NumberOfMessagesToSkip;
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

            var _Counter = Interlocked.Increment(ref Counter);

            if (_Counter > NumberOfMessagesToSkip)
            {
                MessageOut = MessageIn;
                return true;
            }

            MessageOut = null;
            return false;

        }

        #endregion

    }

}
