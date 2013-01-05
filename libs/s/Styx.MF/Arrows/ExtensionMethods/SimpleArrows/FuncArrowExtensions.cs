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

namespace de.ahzf.Styx
{

    /// <summary>
    /// A generic Arrow transforming incoming messages into outgoing messages.
    /// </summary>
    public static class FuncArrowExtensions
    {

        #region FuncArrowExtensions(this ArrowSender, Func)

        /// <summary>
        /// A generic Arrow transforming incoming messages into outgoing messages.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="Func">A generic transformation of incoming messages into outgoing messages.</param>
        public static FuncArrow<TIn, TOutput> FuncArrow<TIn, TOutput>(this IArrowSender<TIn> ArrowSender, Func<TIn, TOutput> Func)
        {
            var _FuncArrow = new FuncArrow<TIn, TOutput>(Func);
            ArrowSender.OnMessageAvailable += _FuncArrow.ReceiveMessage;
            return _FuncArrow;
        }

        #endregion

        #region FuncArrowExtensions(this ArrowSender, Func, MessageRecipient.Recipient, params MessageRecipient.Recipients)

        /// <summary>
        /// A generic Arrow transforming incoming messages into outgoing messages.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="Func">A generic transformation of incoming messages into outgoing messages.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static FuncArrow<TIn, TOutput> FuncArrow<TIn, TOutput>(this IArrowSender<TIn> ArrowSender, Func<TIn, TOutput> Func, MessageRecipient<TOutput> Recipient, params MessageRecipient<TOutput>[] Recipients)
        {
            var _FuncArrow = new FuncArrow<TIn, TOutput>(Func, Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _FuncArrow.ReceiveMessage;
            return _FuncArrow;
        }

        #endregion

        #region FuncArrowExtensions(this ArrowSender, Func, IArrowReceiver<TOutput>.Recipient, params IArrowReceiver<TOutput>.Recipients)

        /// <summary>
        /// A generic Arrow transforming incoming messages into outgoing messages.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="Func">A generic transformation of incoming messages into outgoing messages.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static FuncArrow<TIn, TOutput> FuncArrow<TIn, TOutput>(this IArrowSender<TIn> ArrowSender, Func<TIn, TOutput> Func, IArrowReceiver<TOutput> Recipient, params IArrowReceiver<TOutput>[] Recipients)
        {
            var _FuncArrow = new FuncArrow<TIn, TOutput>(Func, Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _FuncArrow.ReceiveMessage;
            return _FuncArrow;
        }

        #endregion

    }

}
