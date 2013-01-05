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
    /// Filters the consuming objects by calling a Func&lt;S, Boolean&gt;.
    /// </summary>
    public static class FuncFilterArrowExtensions
    {

        #region FuncFilter(this ArrowSender, FilterFunc)

        /// <summary>
        /// Filters the consuming objects by calling a Func&lt;S, Boolean&gt;.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="FilterFunc">A Func&lt;S, Boolean&gt; filtering the consuming objects. True means filter (ignore).</param>
        public static FuncFilterArrow<TMessage> FuncFilter<TMessage>(this IArrowSender<TMessage> ArrowSender, Func<TMessage, Boolean> FilterFunc)
        {
            var _FuncFilterArrow = new FuncFilterArrow<TMessage>(FilterFunc);
            ArrowSender.OnMessageAvailable += _FuncFilterArrow.ReceiveMessage;
            return _FuncFilterArrow;
        }

        #endregion

        #region FuncFilter(this ArrowSender, FilterFunc, MessageRecipient.Recipient, params MessageRecipient.Recipients)

        /// <summary>
        /// Filters the consuming objects by calling a Func&lt;S, Boolean&gt;.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="FilterFunc">A Func&lt;S, Boolean&gt; filtering the consuming objects. True means filter (ignore).</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static FuncFilterArrow<TMessage> FuncFilter<TMessage>(this IArrowSender<TMessage> ArrowSender, Func<TMessage, Boolean> FilterFunc, MessageRecipient<TMessage> Recipient, params MessageRecipient<TMessage>[] Recipients)
        {
            var _FuncFilterArrow = new FuncFilterArrow<TMessage>(FilterFunc, Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _FuncFilterArrow.ReceiveMessage;
            return _FuncFilterArrow;
        }

        #endregion

        #region FuncFilter(this ArrowSender, FilterFunc, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// Filters the consuming objects by calling a Func&lt;S, Boolean&gt;.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="FilterFunc">A Func&lt;S, Boolean&gt; filtering the consuming objects. True means filter (ignore).</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static FuncFilterArrow<TMessage> FuncFilter<TMessage>(this IArrowSender<TMessage> ArrowSender, Func<TMessage, Boolean> FilterFunc, IArrowReceiver<TMessage> Recipient, params IArrowReceiver<TMessage>[] Recipients)
        {
            var _FuncFilterArrow = new FuncFilterArrow<TMessage>(FilterFunc, Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _FuncFilterArrow.ReceiveMessage;
            return _FuncFilterArrow;
        }

        #endregion

    }

}
