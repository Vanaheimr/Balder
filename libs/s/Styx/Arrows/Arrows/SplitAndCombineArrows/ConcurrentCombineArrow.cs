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
    public class ConcurrentCombineArrow<TIn1, TIn2, TOut> : AbstractArrowSender<TOut>
    {

        #region Data

        /// <summary>
        /// A blocking collection as inter-thread message pipeline.
        /// </summary>
        private readonly BlockingCollection<TIn1> BlockingCollection1;

        /// <summary>
        /// A blocking collection as inter-thread message pipeline.
        /// </summary>
        private readonly BlockingCollection<TIn2> BlockingCollection2;

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

        #endregion

        #region Constructor(s)

        #region ConcurrentCombineArrow(MessageProcessor, ArrowSender1 = null, ArrowSender2 = null)

        /// <summary>
        /// An arrow for transforming two incoming messages into a single outgoing message.
        /// </summary>
        /// <param name="MessageProcessor">A delegate for transforming two incoming messages into a single outgoing message.</param>
        /// <param name="ArrowSender1">An optional first arrow sender.</param>
        /// <param name="ArrowSender2">An optional second arrow sender.</param>
        public ConcurrentCombineArrow(Func<TIn1, TIn2, TOut> MessageProcessor,
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
            this.MaxQueueSize        = 1000;
            this.BlockingCollection1 = new BlockingCollection<TIn1>();
            this.BlockingCollection2 = new BlockingCollection<TIn2>();

            if (ArrowSender1 != null)
                ArrowSender1.OnMessageAvailable += ReceiveMessage1;

            if (ArrowSender2 != null)
                ArrowSender2.OnMessageAvailable += ReceiveMessage2;

            this.ArrowSenderTask = Task.Factory.StartNew(() => {

                var Enumerator1 = BlockingCollection1.GetConsumingEnumerable().GetEnumerator();
                var Enumerator2 = BlockingCollection2.GetConsumingEnumerable().GetEnumerator();

                while (!BlockingCollection1.IsCompleted &&
                       !BlockingCollection2.IsCompleted)
                {

                    // Both will block until something is available!
                    Enumerator1.MoveNext();
                    Enumerator2.MoveNext();

                    Debug.WriteLine(MessageProcessor(Enumerator1.Current, Enumerator2.Current));

                    base.NotifyRecipients(this, MessageProcessor(Enumerator1.Current, Enumerator2.Current));

                }
                
            });

        }

        #endregion

        #region ConcurrentCombineArrow(MessageProcessor, ArrowSender1 = null, ArrowSender2 = null, params MessageRecipients.Recipients)

        /// <summary>
        /// An arrow for transforming two incoming messages into a single outgoing message.
        /// </summary>
        /// <param name="MessageProcessor">A delegate for transforming two incoming messages into a single outgoing message.</param>
        /// <param name="ArrowSender1">An optional first arrow sender.</param>
        /// <param name="ArrowSender2">An optional second arrow sender.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public ConcurrentCombineArrow(Func<TIn1, TIn2, TOut>          MessageProcessor,
                                      IArrowSender<TIn1>              ArrowSender1 = null,
                                      IArrowSender<TIn2>              ArrowSender2 = null,
                                      params MessageRecipient<TOut>[] Recipients)

            : this(MessageProcessor, ArrowSender1, ArrowSender2)

        {

            if (Recipients != null)
                SendTo(Recipients);

        }

        #endregion

        #region ConcurrentCombineArrow(MessageProcessor, ArrowSender1 = null, ArrowSender2 = null, params IArrowReceiver.Recipients)

        /// <summary>
        /// An arrow for transforming two incoming messages into a single outgoing message.
        /// </summary>
        /// <param name="MessageProcessor">A delegate for transforming two incoming messages into a single outgoing message.</param>
        /// <param name="ArrowSender1">An optional first arrow sender.</param>
        /// <param name="ArrowSender2">An optional second arrow sender.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public ConcurrentCombineArrow(Func<TIn1, TIn2, TOut>        MessageProcessor,
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


        #region ReceiveMessage1(Sender, MessageIn1)

        /// <summary>
        /// Accepts a message of type TIn1 from a sender for further
        /// processing and delivery to the subscribers.
        /// </summary>
        /// <param name="Sender">The sender of the message.</param>
        /// <param name="MessageIn1">The message.</param>
        public void ReceiveMessage1(Object Sender, TIn1 MessageIn1)
        {
            BlockingCollection1.Add(MessageIn1);
        }

        #endregion

        #region ReceiveMessage2(Sender, MessageIn2)

        /// <summary>
        /// Accepts a message of type TIn2 from a sender for further
        /// processing and delivery to the subscribers.
        /// </summary>
        /// <param name="Sender">The sender of the message.</param>
        /// <param name="MessageIn2">The message.</param>
        public void ReceiveMessage2(Object Sender, TIn2 MessageIn2)
        {
            BlockingCollection2.Add(MessageIn2);
        }

        #endregion

    }

}
