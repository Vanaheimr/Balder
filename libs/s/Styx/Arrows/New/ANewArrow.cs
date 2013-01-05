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
    /// Filters the consuming objects by calling a Func&lt;S, Boolean&gt;.
    /// </summary>
    /// <typeparam name="TMessage">The type of the consuming and emitting messages/objects.</typeparam>
    public abstract class ANewArrow<TIn, TOut> : ITarget<TIn>, INotification<TOut>
    {

        #region Events

        public event NotificationEventHandler<TOut> OnNotification;

        public event ExceptionEventHandler OnError;

        public event CompletedEventHandler OnCompleted;

        #endregion

        #region Constructor(s)

        #region ANewArrow()

        public ANewArrow()
        { }

        #endregion

        #endregion

        #region (abstract) ProcessMessage(MessageIn, out MessageOut)

        /// <summary>
        /// Process the incoming message and return an outgoing message.
        /// </summary>
        /// <param name="MessageIn">The incoming message.</param>
        /// <param name="MessageOut">The outgoing message.</param>
        /// <returns>True if the message should be forwarded; False otherwise.</returns>
        protected abstract Boolean ProcessMessage(TIn MessageIn, out TOut MessageOut);

        #endregion


        public void ProcessNotification(TIn Message)
        {

            TOut MessageOut = default(TOut);

            if (ProcessMessage(Message, out MessageOut))
            {
                if (OnNotification != null)
                    OnNotification(MessageOut);
            }

        }

        public void ProcessError(dynamic Sender, Exception ExceptionMessage)
        {
            if (OnError != null)
                OnError(this, ExceptionMessage);
        }

        public void ProcessCompleted(dynamic Sender, String Message)
        {
            if (OnCompleted != null)
                OnCompleted(this, Message);
        }

    }

}
