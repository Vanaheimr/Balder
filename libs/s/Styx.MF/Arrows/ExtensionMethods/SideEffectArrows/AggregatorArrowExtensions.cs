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
using System.Collections.Generic;

#endregion

namespace de.ahzf.Styx
{

    /// <summary>
    /// The AggregatorArrow produces a side effect that is the provided collection
    /// filled with the contents of all the objects that have passed through it.
    /// The collection enumerator is used as the emitting enumerator. Thus, what
    /// goes into AggregatorArrow may not be the same as what comes out of
    /// AggregatorPipe.
    /// For example, duplicates removed, different order to the stream, etc.
    /// Finally, note that different Collections have different behaviors and
    /// write/read times.
    /// </summary>
    public static class AggregatorArrowExtensions
    {

        #region Aggregator(this ArrowSender, InitialValue)

        /// <summary>
        /// The AggregatorArrow produces a side effect that is the provided collection
        /// filled with the contents of all the objects that have passed through it.
        /// The collection enumerator is used as the emitting enumerator. Thus, what
        /// goes into AggregatorArrow may not be the same as what comes out of
        /// AggregatorPipe.
        /// For example, duplicates removed, different order to the stream, etc.
        /// Finally, note that different Collections have different behaviors and
        /// write/read times.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="ICollection">An optional ICollection to store the passed messages/objects.</param>
        public static AggregatorArrow<TMessage> Aggregator<TMessage>(this IArrowSender<TMessage> ArrowSender, ICollection<TMessage> ICollection = null)
        {
            var _AggregatorArrow = new AggregatorArrow<TMessage>(ICollection);
            ArrowSender.OnMessageAvailable += _AggregatorArrow.ReceiveMessage;
            return _AggregatorArrow;
        }

        #endregion

        #region Aggregator(this ArrowSender, InitialValue, MessageRecipient.Recipient, params MessageRecipient.Recipients)

        /// <summary>
        /// The AggregatorArrow produces a side effect that is the provided collection
        /// filled with the contents of all the objects that have passed through it.
        /// The collection enumerator is used as the emitting enumerator. Thus, what
        /// goes into AggregatorArrow may not be the same as what comes out of
        /// AggregatorPipe.
        /// For example, duplicates removed, different order to the stream, etc.
        /// Finally, note that different Collections have different behaviors and
        /// write/read times.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="ICollection">An optional ICollection to store the passed messages/objects.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static AggregatorArrow<TMessage> Aggregator<TMessage>(this IArrowSender<TMessage> ArrowSender, ICollection<TMessage> ICollection, MessageRecipient<TMessage> Recipient, params MessageRecipient<TMessage>[] Recipients)
        {
            var _AggregatorArrow = new AggregatorArrow<TMessage>(ICollection, Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _AggregatorArrow.ReceiveMessage;
            return _AggregatorArrow;
        }

        #endregion

        #region Aggregator(this ArrowSender, InitialValue, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// The AggregatorArrow produces a side effect that is the provided collection
        /// filled with the contents of all the objects that have passed through it.
        /// The collection enumerator is used as the emitting enumerator. Thus, what
        /// goes into AggregatorArrow may not be the same as what comes out of
        /// AggregatorPipe.
        /// For example, duplicates removed, different order to the stream, etc.
        /// Finally, note that different Collections have different behaviors and
        /// write/read times.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="ICollection">An optional ICollection to store the passed messages/objects.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static AggregatorArrow<TMessage> Aggregator<TMessage>(this IArrowSender<TMessage> ArrowSender, ICollection<TMessage> ICollection, IArrowReceiver<TMessage> Recipient, params IArrowReceiver<TMessage>[] Recipients)
        {
            var _AggregatorArrow = new AggregatorArrow<TMessage>(ICollection, Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _AggregatorArrow.ReceiveMessage;
            return _AggregatorArrow;
        }

        #endregion

    }

}
