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
    /// The MinMaxArrow produces two side effects which keep
    /// track of the Min and Max values of S.
    /// </summary>
    public static class MinMaxArrowExtensions
    {

        #region MinMax(this ArrowSender, InitialValue)

        /// <summary>
        /// The MinMaxArrow produces two side effects which keep
        /// track of the Min and Max values of S.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="Min">The initial minimum.</param>
        /// <param name="Max">The initial maximum.</param>
        public static MinMaxArrow<TMessage> MinMax<TMessage>(this IArrowSender<TMessage> ArrowSender, TMessage Min, TMessage Max)
            where TMessage : IComparable, IComparable<TMessage>, IEquatable<TMessage>
        {
            var _MinMaxArrow = new MinMaxArrow<TMessage>(Min, Max);
            ArrowSender.OnMessageAvailable += _MinMaxArrow.ReceiveMessage;
            return _MinMaxArrow;
        }

        #endregion

        #region MinMax(this ArrowSender, InitialValue, MessageRecipient.Recipient, params MessageRecipient.Recipients)

        /// <summary>
        /// The MinMaxArrow produces two side effects which keep
        /// track of the Min and Max values of S.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="Min">The initial minimum.</param>
        /// <param name="Max">The initial maximum.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static MinMaxArrow<TMessage> MinMax<TMessage>(this IArrowSender<TMessage> ArrowSender, TMessage Min, TMessage Max, MessageRecipient<TMessage> Recipient, params MessageRecipient<TMessage>[] Recipients)
            where TMessage : IComparable, IComparable<TMessage>, IEquatable<TMessage>
        {
            var _MinMaxArrow = new MinMaxArrow<TMessage>(Min, Max, Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _MinMaxArrow.ReceiveMessage;
            return _MinMaxArrow;
        }

        #endregion

        #region MinMax(this ArrowSender, InitialValue, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// The MinMaxArrow produces two side effects which keep
        /// track of the Min and Max values of S.
        /// </summary>
        /// <param name="ArrowSender">The sender of the messages/objects.</param>
        /// <param name="Min">The initial minimum.</param>
        /// <param name="Max">The initial maximum.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public static MinMaxArrow<TMessage> MinMax<TMessage>(this IArrowSender<TMessage> ArrowSender, TMessage Min, TMessage Max, IArrowReceiver<TMessage> Recipient, params IArrowReceiver<TMessage>[] Recipients)
            where TMessage : IComparable, IComparable<TMessage>, IEquatable<TMessage>
        {
            var _MinMaxArrow = new MinMaxArrow<TMessage>(Min, Max, Recipient, Recipients);
            ArrowSender.OnMessageAvailable += _MinMaxArrow.ReceiveMessage;
            return _MinMaxArrow;
        }

        #endregion

    }

}
