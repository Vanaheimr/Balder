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

    public static class NewFuncArrowExtention
    {

        public static INotification<TOut> NewFuncArrow<TIn, TOut>(this INotification<TIn> In, Func<TIn, TOut> MessageProcessor)
        {
            var a = new NewFuncArrow<TIn, TOut>(MessageProcessor);
            In.SendTo(a);
            return a;
        }

        public static INotification<TOut> NewFuncArrow<TIn1, TIn2, TOut>(this INotification<TIn1, TIn2> In, Func<TIn1, TIn2, TOut> MessageProcessor)
        {
            var a = new NewFuncArrow<TIn1, TIn2, TOut>(MessageProcessor);
            In.SendTo(a);
            return a;
        }

    }


    /// <summary>
    /// Filters the consuming objects by calling a Func&lt;S, Boolean&gt;.
    /// </summary>
    /// <typeparam name="TMessage">The type of the consuming and emitting messages/objects.</typeparam>
    public class NewFuncArrow<TIn, TOut> : ANewArrow<TIn, TOut>
    {

        #region Data

        private readonly Func<TIn, TOut> _MessageProcessor;

        #endregion

        #region Constructor(s)

        #region FuncFilter(MessageProcessor)

        /// <summary>
        /// Filters the consuming objects by calling a Func&lt;S, Boolean&gt;.
        /// </summary>
        /// <param name="Func">A Func&lt;S, Boolean&gt; filtering the consuming objects. True means filter (ignore).</param>
        public NewFuncArrow(Func<TIn, TOut> MessageProcessor)
        {

            if (MessageProcessor == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            _MessageProcessor = MessageProcessor;

        }

        #endregion

        #endregion

        #region ProcessMessage(MessageIn, out MessageOut)

        /// <summary>
        /// Process the incoming message and return an outgoing message.
        /// </summary>
        /// <param name="MessageIn">The incoming message.</param>
        /// <param name="MessageOut">The outgoing message.</param>
        protected override Boolean ProcessMessage(TIn MessageIn, out TOut MessageOut)
        {
            MessageOut = _MessageProcessor(MessageIn);
            return true;
        }

        #endregion

    }



    /// <summary>
    /// Filters the consuming objects by calling a Func&lt;S, Boolean&gt;.
    /// </summary>
    /// <typeparam name="TMessage">The type of the consuming and emitting messages/objects.</typeparam>
    public class NewFuncArrow<TIn1, TIn2, TOut> : ITarget<TIn1, TIn2>, INotification<TOut>
    {

        #region Data

        private readonly Func<TIn1, TIn2, TOut> _MessageProcessor;

        #endregion

        #region Events

        public event NotificationEventHandler<TOut> OnNotification;

        public event ExceptionEventHandler OnError;

        public event CompletedEventHandler OnCompleted;

        #endregion

        #region Constructor(s)

        #region FuncFilter(MessageProcessor)

        /// <summary>
        /// Filters the consuming objects by calling a Func&lt;S, Boolean&gt;.
        /// </summary>
        /// <param name="Func">A Func&lt;S, Boolean&gt; filtering the consuming objects. True means filter (ignore).</param>
        public NewFuncArrow(Func<TIn1, TIn2, TOut> MessageProcessor)
        {

            if (MessageProcessor == null)
                throw new ArgumentNullException("The given delegate must not be null!");

            _MessageProcessor = MessageProcessor;

        }

        #endregion

        #endregion

        #region ProcessShoot(MessageIn1, MessageIn2)

        /// <summary>
        /// Process the incoming message and send an outgoing message.
        /// </summary>
        /// <param name="MessageIn1">The first incoming message.</param>
        /// <param name="MessageIn2">The second incoming message.</param>
        public void ProcessNotification(TIn1 MessageIn1, TIn2 MessageIn2)
        {

            if (OnNotification != null)
                OnNotification(_MessageProcessor(MessageIn1, MessageIn2));

        }

        #endregion

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
