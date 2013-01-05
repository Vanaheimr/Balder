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
    /// Extension methods for the IArrowReceiver interface.
    /// </summary>
    public static class IArrowReceiverExtensions
    {

        #region ReceiveMsg(this ArrowReceiver, Message)

        /// <summary>
        /// Accepts a message of type TIn from a sender for further processing
        /// and delivery to the subscribers.
        /// </summary>
        /// <typeparam name="TMessage">The type of the consuming messages/objects.</typeparam>
        /// <param name="ArrowReceiver">The receiver of the message.</param>
        /// <param name="Message">The message.</param>
        /// <returns>True if the message was accepted and could be processed; False otherwise.</returns>
        public static Boolean ReceiveMessage<TMessage>(this IArrowReceiver<TMessage> ArrowReceiver, TMessage Message)
        {
            return ArrowReceiver.ReceiveMessage(null, Message);
        }

        #endregion

    }

}
