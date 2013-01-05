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
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Diagnostics;

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    /// <summary>
    /// A generic Arrow transforming incoming messages into outgoing messages.
    /// </summary>
    /// <typeparam name="TIn1">The type of the first consuming messages/objects.</typeparam>
    /// <typeparam name="TIn2">The type of the second consuming messages/objects.</typeparam>
    /// <typeparam name="TOut">The type of the emitted messages/objects.</typeparam>
    public class CombineArrow<TIn1, TIn2, TOut> : AbstractArrowSender<TOut>
    {

        #region Data

        private readonly IArrowSender<TIn1> ArrowSender1;
        private readonly IArrowSender<TIn2> ArrowSender2;

        /// <summary>
        /// A concurrent queue as inter-thread message pipeline.
        /// </summary>
        private readonly ConcurrentQueue<TIn1> Queue1;

        /// <summary>
        /// A concurrent queue as inter-thread message pipeline.
        /// </summary>
        private readonly ConcurrentQueue<TIn2> Queue2;

        /// <summary>
        /// A delegate for transforming two incoming messages into a single outgoing message.
        /// </summary>
        private readonly Func<TIn1, TIn2, TOut> MessageProcessor;

        /// <summary>
        /// The internal arrow sender task.
        /// </summary>
        private readonly Task ArrowSenderTask;

        #endregion

        #region Properties

        #region MaxQueueSize

        /// <summary>
        /// The maximum number of queued messages for both arrow senders.
        /// </summary>
        public UInt32 MaxQueueSize { get; set; }

        #endregion

        #region MultiThreadedSender

        /// <summary>
        /// The arrow sender will use a seperate task.
        /// </summary>
        public Boolean MultiThreadedSender { get; private set; }

        #endregion

        #endregion

        #region Constructor(s)

        #region CombineArrow(MessageProcessor, ArrowSender1 = null, ArrowSender2 = null)

        /// <summary>
        /// An arrow for transforming two incoming messages into a single outgoing message.
        /// </summary>
        /// <param name="MessageProcessor">A delegate for transforming two incoming messages into a single outgoing message.</param>
        /// <param name="ArrowSender1">An optional first arrow sender.</param>
        /// <param name="ArrowSender2">An optional second arrow sender.</param>
        public CombineArrow(Func<TIn1, TIn2, TOut> MessageProcessor,
                            IArrowSender<TIn1>     ArrowSender1 = null,
                            IArrowSender<TIn2>     ArrowSender2 = null)
        {

            #region Initial checks

            if (ArrowSender1 == null)
                throw new ArgumentNullException("ArrowSender1", "The parameter 'ArrowSender1' must not be null!");

            if (ArrowSender2 == null)
                throw new ArgumentNullException("ArrowSender2", "The parameter 'ArrowSender2' must not be null!");

            if (MessageProcessor == null)
                throw new ArgumentNullException("MessageProcessor", "The parameter 'MessageProcessor' must not be null!");

            #endregion
            
            this.MessageProcessor    = MessageProcessor;
            this.MultiThreadedSender = MultiThreadedSender;
            this.MaxQueueSize        = 1000;
            this.Queue1              = new ConcurrentQueue<TIn1>();
            this.Queue2              = new ConcurrentQueue<TIn2>();

            if (ArrowSender1 != null)
                ArrowSender1.OnMessageAvailable += ReceiveMessage1;

            if (ArrowSender2 != null)
                ArrowSender2.OnMessageAvailable += ReceiveMessage2;

        }

        #endregion

        #region CombineArrow(MessageProcessor, MessageRecipients.Recipient, params MessageRecipients.Recipients)

        /// <summary>
        /// An arrow for transforming two incoming messages into a single outgoing message.
        /// </summary>
        /// <param name="MessageProcessor">A delegate for transforming two incoming messages into a single outgoing message.</param>
        /// <param name="ArrowSender1">An optional first arrow sender.</param>
        /// <param name="ArrowSender2">An optional second arrow sender.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public CombineArrow(Func<TIn1, TIn2, TOut>          MessageProcessor,
                            IArrowSender<TIn1>              ArrowSender1 = null,
                            IArrowSender<TIn2>              ArrowSender2 = null,
                            params MessageRecipient<TOut>[] Recipients)

            : this(MessageProcessor, ArrowSender1, ArrowSender2)

        {

            if (Recipients != null)
                SendTo(Recipients);

        }

        #endregion

        #region CombineArrow(MessageProcessor, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// An arrow for transforming two incoming messages into a single outgoing message.
        /// </summary>
        /// <param name="MessageProcessor">A delegate for transforming two incoming messages into a single outgoing message.</param>
        /// <param name="ArrowSender1">An optional first arrow sender.</param>
        /// <param name="ArrowSender2">An optional second arrow sender.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public CombineArrow(Func<TIn1, TIn2, TOut>        MessageProcessor,
                            IArrowSender<TIn1>            ArrowSender1 = null,
                            IArrowSender<TIn2>            ArrowSender2 = null,
                            params IArrowReceiver<TOut>[] Recipients)

            : this(MessageProcessor, ArrowSender1, ArrowSender2)

        {

            if (Recipients != null)
                SendTo(Recipients);

        }

        #endregion

        #endregion


        private void ReceiveMessage1(Object Sender, TIn1 MessageIn1)
        {

            TIn2 MessageIn2 = default(TIn2);

            if (Queue2.TryDequeue(out MessageIn2))
                base.NotifyRecipients(this, MessageProcessor(MessageIn1, MessageIn2));

            else if (this.Queue1.Count < this.MaxQueueSize)
                this.Queue1.Enqueue(MessageIn1);

        }

        private void ReceiveMessage2(Object Sender, TIn2 MessageIn2)
        {

            TIn1 MessageIn1 = default(TIn1);

            if (Queue1.TryDequeue(out MessageIn1))
                base.NotifyRecipients(this, MessageProcessor(MessageIn1, MessageIn2));

            else if (this.Queue2.Count < this.MaxQueueSize)
                this.Queue2.Enqueue(MessageIn2);

        }


    }

}
