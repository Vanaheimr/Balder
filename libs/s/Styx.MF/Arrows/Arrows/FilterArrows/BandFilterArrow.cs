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
    public class BandFilterArrow<TMessage> : AbstractArrow<TMessage, TMessage>, IFilterArrow<TMessage>
        where TMessage : IComparable<TMessage>, IComparable
    {

        #region Properties

        #region Lower

        /// <summary>
        /// The lower bound of the passed values.
        /// </summary>
        public TMessage Lower { get; private set; }

        #endregion

        #region Upper

        /// <summary>
        /// The upper bound of the passed values.
        /// </summary>
        public TMessage Upper { get; private set; }

        #endregion

        #endregion

        #region Constructor(s)

        #region BandFilterArrow()

        /// <summary>
        /// Sends a message when then received values are not within
        /// the bounding box of then given Min/Max-values.
        /// </summary>
        /// <param name="Lower">The lower bound of the passed values.</param>
        /// <param name="Upper">The upper bound of the passed values.</param>
        public BandFilterArrow(TMessage Lower, TMessage Upper)
        {
            this.Lower = Lower;
            this.Upper = Upper;
        }

        #endregion

        #region BandFilterArrow(MessageRecipients.Recipient, params MessageRecipients.Recipients)

        /// <summary>
        /// Filters the consuming objects by calling a Func&lt;S, Boolean&gt;.
        /// </summary>
        /// <param name="Lower">The lower bound of the passed values.</param>
        /// <param name="Upper">The upper bound of the passed values.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">The recipients of the processed messages.</param>
        public BandFilterArrow(TMessage Lower, TMessage Upper, MessageRecipient<TMessage> Recipient, params MessageRecipient<TMessage>[] Recipients)
            : base(Recipient, Recipients)
        {
            this.Lower = Lower;
            this.Upper = Upper;
        }

        #endregion

        #region BandFilterArrow(IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// Filters the consuming objects by calling a Func&lt;S, Boolean&gt;.
        /// </summary>
        /// <param name="Lower">The lower bound of the passed values.</param>
        /// <param name="Upper">The upper bound of the passed values.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">The recipients of the processed messages.</param>
        public BandFilterArrow(TMessage Lower, TMessage Upper, IArrowReceiver<TMessage> Recipient, params IArrowReceiver<TMessage>[] Recipients)
            : base(Recipient, Recipients)
        {
            this.Lower = Lower;
            this.Upper = Upper;
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

            // Is MessageIn < Lower?
            if (MessageIn.CompareTo(Lower) < 0)
                return true;

            // Is MessageIn > Upper?
            if (MessageIn.CompareTo(Upper) > 0)
                return true;

            return false;

        }

        #endregion

    }

}
