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
    /// Filters the consuming objects by calling a Func&lt;S, Boolean&gt;.
    /// </summary>
    /// <typeparam name="TMessage">The type of the consuming and emitting messages/objects.</typeparam>
    public class FuncFilterArrow<TMessage> : AbstractArrow<TMessage, TMessage>, IFilterArrow<TMessage>
    {

        #region Data

        private readonly Func<TMessage, Boolean> _FilterFunc;

        #endregion

        #region Constructor(s)

        #region FuncFilterArrow()

        /// <summary>
        /// Filters the consuming objects by calling a Func&lt;S, Boolean&gt;.
        /// </summary>
        /// <param name="FilterFunc">A Func&lt;S, Boolean&gt; filtering the consuming objects. True means filter (ignore).</param>
        public FuncFilterArrow(Func<TMessage, Boolean> FilterFunc)
        {

            if (FilterFunc == null)
                throw new ArgumentNullException("The given FilterFunc must not be null!");

            _FilterFunc = FilterFunc;

        }

        #endregion

        #region FuncFilterArrow(MessageRecipients.Recipient, params MessageRecipients.Recipients)

        /// <summary>
        /// Filters the consuming objects by calling a Func&lt;S, Boolean&gt;.
        /// </summary>
        /// <param name="FilterFunc">A Func&lt;S, Boolean&gt; filtering the consuming objects. True means filter (ignore).</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">The recipients of the processed messages.</param>
        public FuncFilterArrow(Func<TMessage, Boolean> FilterFunc, MessageRecipient<TMessage> Recipient, params MessageRecipient<TMessage>[] Recipients)
            : base(Recipient, Recipients)
        {

            if (FilterFunc == null)
                throw new ArgumentNullException("The given FilterFunc must not be null!");

            _FilterFunc = FilterFunc;

        }

        #endregion

        #region FuncFilterArrow(IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// Filters the consuming objects by calling a Func&lt;S, Boolean&gt;.
        /// </summary>
        /// <param name="FilterFunc">A Func&lt;S, Boolean&gt; filtering the consuming objects. True means filter (ignore).</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">The recipients of the processed messages.</param>
        public FuncFilterArrow(Func<TMessage, Boolean> FilterFunc, IArrowReceiver<TMessage> Recipient, params IArrowReceiver<TMessage>[] Recipients)
            : base(Recipient, Recipients)
        {

            if (FilterFunc == null)
                throw new ArgumentNullException("The given FilterFunc must not be null!");

            _FilterFunc = FilterFunc;

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

            if (!_FilterFunc(MessageIn))
            {
                MessageOut = MessageIn;
                return true;
            }

            MessageOut = default(TMessage);

            return false;

        }

        #endregion

    }

}
