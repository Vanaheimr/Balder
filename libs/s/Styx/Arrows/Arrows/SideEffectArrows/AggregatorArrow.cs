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

namespace de.ahzf.Vanaheimr.Styx
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
    /// <typeparam name="TMessage">The type of the consuming and emitting messages/objects.</typeparam>
    public class AggregatorArrow<TMessage> : AbstractSideEffectArrow<TMessage, TMessage, ICollection<TMessage>>
    {

        #region Constructor(s)

        #region AggregatorArrow(ICollection = null)

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
        /// <param name="ICollection">An optional ICollection to store the passed messages/objects.</param>
        public AggregatorArrow(ICollection<TMessage> ICollection = null)
        {
            if (ICollection == null)
                _SideEffect = new List<TMessage>();
            else
                _SideEffect = ICollection;
        }

        #endregion

        #region AggregatorArrow(ICollection, MessageRecipients.Recipient, params MessageRecipients.Recipients)

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
        /// <param name="ICollection">An ICollection to store the passed messages/objects.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">The recipients of the processed messages.</param>
        public AggregatorArrow(ICollection<TMessage> ICollection, MessageRecipient<TMessage> Recipient, params MessageRecipient<TMessage>[] Recipients)
            : base(Recipient, Recipients)
        {
            if (ICollection == null)
                _SideEffect = new List<TMessage>();
            else
                _SideEffect = ICollection;
        }

        #endregion

        #region AggregatorArrow(ICollection, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

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
        /// <param name="ICollection">An ICollection to store the passed messages/objects.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">The recipients of the processed messages.</param>
        public AggregatorArrow(ICollection<TMessage> ICollection, IArrowReceiver<TMessage> Recipient, params IArrowReceiver<TMessage>[] Recipients)
            : base(Recipient, Recipients)
        {
            if (ICollection == null)
                _SideEffect = new List<TMessage>();
            else
                _SideEffect = ICollection;
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
            _SideEffect.Add(MessageIn);
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
