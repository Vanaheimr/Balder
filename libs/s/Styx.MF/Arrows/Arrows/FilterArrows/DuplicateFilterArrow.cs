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
    /// The DuplicateFilterArrow will not allow a duplicate object to pass through it.
    /// This is accomplished by the Arrow maintaining an internal HashSet that is used
    /// to store a history of previously seen objects.
    /// Thus, the more unique objects that pass through this Arrow, the slower it
    /// becomes as a log_2 index is checked for every object.
    /// </summary>
    /// <typeparam name="TMessage">The type of the consuming and emitting messages/objects.</typeparam>
    public class DuplicateFilterArrow<TMessage> : AbstractArrow<TMessage, TMessage>, IFilterArrow<TMessage>
    {

        #region Data

        private readonly HashSet<TMessage> _HistorySet;

        #endregion

        #region Constructor(s)

        #region DuplicateFilterArrow()

        /// <summary>
        /// The DuplicateFilterArrow will not allow a duplicate object to pass through it.
        /// This is accomplished by the Arrow maintaining an internal HashSet that is used
        /// to store a history of previously seen objects.
        /// Thus, the more unique objects that pass through this Arrow, the slower it
        /// becomes as a log_2 index is checked for every object.
        /// </summary>
        public DuplicateFilterArrow()
        {
            _HistorySet = new HashSet<TMessage>();
        }

        #endregion

        #region DuplicateFilterArrow(MessageRecipients.Recipient, params MessageRecipients.Recipients)

        /// <summary>
        /// The DuplicateFilterArrow will not allow a duplicate object to pass through it.
        /// This is accomplished by the Arrow maintaining an internal HashSet that is used
        /// to store a history of previously seen objects.
        /// Thus, the more unique objects that pass through this Arrow, the slower it
        /// becomes as a log_2 index is checked for every object.
        /// </summary>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">The recipients of the processed messages.</param>
        public DuplicateFilterArrow(MessageRecipient<TMessage> Recipient, params MessageRecipient<TMessage>[] Recipients)
            : base(Recipient, Recipients)
        {
            _HistorySet = new HashSet<TMessage>();
        }

        #endregion

        #region DuplicateFilterArrow(IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// The DuplicateFilterArrow will not allow a duplicate object to pass through it.
        /// This is accomplished by the Arrow maintaining an internal HashSet that is used
        /// to store a history of previously seen objects.
        /// Thus, the more unique objects that pass through this Arrow, the slower it
        /// becomes as a log_2 index is checked for every object.
        /// </summary>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">The recipients of the processed messages.</param>
        public DuplicateFilterArrow(IArrowReceiver<TMessage> Recipient, params IArrowReceiver<TMessage>[] Recipients)
            : base(Recipient, Recipients)
        {
            _HistorySet = new HashSet<TMessage>();
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

            if (!_HistorySet.Contains(MessageIn))
            {
                _HistorySet.Add(MessageIn);
                return true;
            }

            return false;

        }

        #endregion

    }

}
