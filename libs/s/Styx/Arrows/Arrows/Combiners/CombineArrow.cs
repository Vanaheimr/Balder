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
using System.Collections.Generic;

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMessage">The type of the consuming and emitting messages/objects.</typeparam>
    public class CombineArrow<TIn1, TIn2, TOut> : AbstractArrowSender<TOut>
    {

        #region Data

        private readonly IArrowSender<TIn1>      ArrowSender1;
        private readonly IArrowSender<TIn2>      ArrowSender2;

        private readonly Func<TIn1, TIn2, TOut>  MessageProcessor;

        private readonly Queue<TIn1>             Queue1;
        private readonly Queue<TIn2>             Queue2;

        #endregion

        #region Constructor(s)

        #region CombineArrow(ArrowSender1, ArrowSender2, MessageProcessor)

        public CombineArrow(IArrowSender<TIn1> ArrowSender1, IArrowSender<TIn2> ArrowSender2, Func<TIn1, TIn2, TOut> MessageProcessor)
        {
            
            this.ArrowSender1     = ArrowSender1;
            this.ArrowSender2     = ArrowSender2;

            this.MessageProcessor = MessageProcessor;

            this.Queue1           = new Queue<TIn1>();
            this.Queue2           = new Queue<TIn2>();

            this.ArrowSender1.OnMessageAvailable += ReceiveMessage1;
            this.ArrowSender2.OnMessageAvailable += ReceiveMessage2;

        }

        #endregion

        //#region SkipArrow(NumberOfMessagesToSkip, MessageRecipients.Recipient, params MessageRecipients.Recipients)

        ///// <summary>
        ///// The SkipArrow simply sends the incoming message to the recipients
        ///// without any processing, but skips the first n messages.
        ///// </summary>
        ///// <param name="NumberOfMessagesToSkip">The number of messages to skip.</param>
        ///// <param name="Recipient">A recipient of the processed messages.</param>
        ///// <param name="Recipients">The recipients of the processed messages.</param>
        //public SkipArrow(UInt32 NumberOfMessagesToSkip, MessageRecipient<TMessage> Recipient, params MessageRecipient<TMessage>[] Recipients)
        //    : base(Recipient, Recipients)
        //{
        //    this.NumberOfMessagesToSkip = NumberOfMessagesToSkip;
        //}

        //#endregion

        //#region SkipArrow(NumberOfMessagesToSkip, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        ///// <summary>
        ///// The SkipArrow simply sends the incoming message to the recipients
        ///// without any processing, but skips the first n messages.
        ///// </summary>
        ///// <param name="NumberOfMessagesToSkip">The number of messages to skip.</param>
        ///// <param name="Recipient">A recipient of the processed messages.</param>
        ///// <param name="Recipients">The recipients of the processed messages.</param>
        //public SkipArrow(UInt32 NumberOfMessagesToSkip, IArrowReceiver<TMessage> Recipient, params IArrowReceiver<TMessage>[] Recipients)
        //    : base(Recipient, Recipients)
        //{
        //    this.NumberOfMessagesToSkip = NumberOfMessagesToSkip;
        //}

        //#endregion

        #endregion

        //#region ProcessMessage(MessageIn, out MessageOut)

        ///// <summary>
        ///// Process the incoming message and return an outgoing message.
        ///// </summary>
        ///// <param name="MessageIn">The incoming message.</param>
        ///// <param name="MessageOut">The outgoing message.</param>
        //protected override Boolean ProcessMessage(TMessage MessageIn, out TMessage MessageOut)
        //{

        //    //var _Counter = Interlocked.Increment(ref Counter);

        //    //if (_Counter > NumberOfMessagesToSkip)
        //    //{
        //    //    MessageOut = MessageIn;
        //    //    return true;
        //    //}

        //    MessageOut = default(TMessage);
        //    return false;

        //}

        //#endregion


        #region ReceiveMessage1(Sender, MessageIn1)

        /// <summary>
        /// Accepts a message of type S from a sender for further processing
        /// and delivery to the subscribers.
        /// </summary>
        /// <param name="Sender">The sender of the message.</param>
        /// <param name="MessageIn">The message.</param>
        public void ReceiveMessage1(dynamic Sender, TIn1 MessageIn1)
        {

            if (this.Queue2.Count > 0)
            {

                TIn2 MessageIn2 = this.Queue2.Dequeue();

                NotifyRecipients(this, MessageProcessor(MessageIn1, MessageIn2));

            }

            else
                this.Queue1.Enqueue(MessageIn1);

        }

        #endregion

        #region ReceiveMessage2(Sender, MessageIn2)

        /// <summary>
        /// Accepts a message of type S from a sender for further processing
        /// and delivery to the subscribers.
        /// </summary>
        /// <param name="Sender">The sender of the message.</param>
        /// <param name="MessageIn">The message.</param>
        public void ReceiveMessage2(dynamic Sender, TIn2 MessageIn2)
        {

            if (this.Queue1.Count > 0)
            {

                TIn1 MessageIn1 = this.Queue1.Dequeue();

                NotifyRecipients(this, MessageProcessor(MessageIn1, MessageIn2));

            }

            else
                this.Queue2.Enqueue(MessageIn2);

        }

        #endregion


    }

}
