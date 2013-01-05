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

namespace de.ahzf.Vanaheimr.Styx
{

    /// <summary>
    /// The DuplicateFilterArrow will not allow a duplicate object to pass through it.
    /// This is accomplished by the Arrow maintaining an internal HashSet that is used
    /// to store a history of previously seen objects.
    /// Thus, the more unique objects that pass through this Arrow, the slower it
    /// becomes as a log_2 index is checked for every object.
    /// </summary>
    public static class DuplicateFilterArrowExtensions
    {

        #region DuplicateFilter(this ArrowSender)

        /// <summary>
        /// The DuplicateFilterArrow will not allow a duplicate object to pass through it.
        /// This is accomplished by the Arrow maintaining an internal HashSet that is used
        /// to store a history of previously seen objects.
        /// Thus, the more unique objects that pass through this Arrow, the slower it
        /// becomes as a log_2 index is checked for every object.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        public static DuplicateFilterArrow<TMessage> DuplicateFilter<TMessage>(this IArrowSender<TMessage> ArrowSender)
        {
            var _DuplicateFilterArrow = new DuplicateFilterArrow<TMessage>();
            ArrowSender.OnMessageAvailable += _DuplicateFilterArrow.ReceiveMessage;
            return _DuplicateFilterArrow;
        }

        #endregion

        #region DuplicateFilter(this ArrowSender, MessageRecipient.Recipient, params MessageRecipient.Recipients)

        /// <summary>
        /// The DuplicateFilterArrow will not allow a duplicate object to pass through it.
        /// This is accomplished by the Arrow maintaining an internal HashSet that is used
        /// to store a history of previously seen objects.
        /// Thus, the more unique objects that pass through this Arrow, the slower it
        /// becomes as a log_2 index is checked for every object.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static DuplicateFilterArrow<TMessage> DuplicateFilter<TMessage>(this IArrowSender<TMessage> ArrowSender, MessageRecipient<TMessage> Recipient, params MessageRecipient<TMessage>[] Recipients)
        {
            var _DuplicateFilterArrow = new DuplicateFilterArrow<TMessage>(Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _DuplicateFilterArrow.ReceiveMessage;
            return _DuplicateFilterArrow;
        }

        #endregion

        #region DuplicateFilter(this ArrowSender, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// The DuplicateFilterArrow will not allow a duplicate object to pass through it.
        /// This is accomplished by the Arrow maintaining an internal HashSet that is used
        /// to store a history of previously seen objects.
        /// Thus, the more unique objects that pass through this Arrow, the slower it
        /// becomes as a log_2 index is checked for every object.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static DuplicateFilterArrow<TMessage> DuplicateFilter<TMessage>(this IArrowSender<TMessage> ArrowSender, IArrowReceiver<TMessage> Recipient, params IArrowReceiver<TMessage>[] Recipients)
        {
            var _DuplicateFilterArrow = new DuplicateFilterArrow<TMessage>(Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _DuplicateFilterArrow.ReceiveMessage;
            return _DuplicateFilterArrow;
        }

        #endregion

    }

}
