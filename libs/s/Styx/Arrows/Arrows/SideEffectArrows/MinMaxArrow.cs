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
    /// <typeparam name="TMessage">The type of the consuming and emitting messages/objects.</typeparam>
    public class MinMaxArrow<TMessage> : AbstractSideEffectArrow<TMessage, TMessage, TMessage, TMessage>
        where TMessage : IComparable, IComparable<TMessage>, IEquatable<TMessage>
    {

        #region Properties

        #region Min

        /// <summary>
        /// The minimum of the passed values.
        /// </summary>
        public TMessage Min
        {
            get
            {
                return _SideEffect1;
            }
        }

        #endregion

        #region Max

        /// <summary>
        /// The maximum of the passed values.
        /// </summary>
        public TMessage Max
        {
            get
            {
                return _SideEffect2;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region MinMaxArrow(Min, Max)

        /// <summary>
        /// The MinMaxArrow produces two side effects which keep
        /// track of the Min and Max values of S.
        /// </summary>
        /// <param name="Min">The initial minimum.</param>
        /// <param name="Max">The initial maximum.</param>
        public MinMaxArrow(TMessage Min, TMessage Max)
        {
            this.SideEffect1 = Min;
            this.SideEffect2 = Max;
        }

        #endregion

        #region MinMaxArrow(Min, Max, MessageRecipients.Recipient, params MessageRecipients.Recipients)

        /// <summary>
        /// The MinMaxArrow produces two side effects which keep
        /// track of the Min and Max values of S.
        /// </summary>
        /// <param name="Min">The initial minimum.</param>
        /// <param name="Max">The initial maximum.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">The recipients of the processed messages.</param>
        public MinMaxArrow(TMessage Min, TMessage Max, MessageRecipient<TMessage> Recipient, params MessageRecipient<TMessage>[] Recipients)
            : base(Recipient, Recipients)
        {
            this.SideEffect1 = Min;
            this.SideEffect2 = Max;
        }

        #endregion

        #region MinMaxArrow(Min, Max, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// The MinMaxArrow produces two side effects which keep
        /// track of the Min and Max values of S.
        /// </summary>
        /// <param name="Min">The initial minimum.</param>
        /// <param name="Max">The initial maximum.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">The recipients of the processed messages.</param>
        public MinMaxArrow(TMessage Min, TMessage Max, IArrowReceiver<TMessage> Recipient, params IArrowReceiver<TMessage>[] Recipients)
            : base(Recipient, Recipients)
        {
            this.SideEffect1 = Min;
            this.SideEffect2 = Max;
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

            if (Min.CompareTo(MessageIn) > 0)
                SideEffect1 = MessageIn;

            if (Max.CompareTo(MessageIn) < 0)
                SideEffect2 = MessageIn;

            return true;

        }

        #endregion

        #region ToString()

        /// <summary>
        /// Returns a string representation of this Arrow.
        /// </summary>
        public override String ToString()
        {
            return base.ToString() + "<Min: " + Min + ", Max: " + Max + ">";
        }

        #endregion

    }

}
