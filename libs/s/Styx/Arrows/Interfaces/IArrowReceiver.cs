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

    #region IArrowReceiver

    /// <summary>
    /// The common IArrowReceiver interface
    /// </summary>
    public interface IArrowReceiver
    {

        /// <summary>
        /// Accepts a message of type TIn from a sender for further processing
        /// and delivery to the subscribers.
        /// </summary>
        /// <param name="Sender">The sender of the message.</param>
        /// <param name="MessageIn">The message.</param>
        void ReceiveMessage(dynamic Sender, Object MessageIn);

    }

    #endregion

    #region IArrowReceiver<in TIn>

    /// <summary>
    /// The common interface for any Arrow implementation accepting messages of type TIn.
    /// </summary>
    /// <typeparam name="TIn">The type of the consuming messages/objects.</typeparam>
    public interface IArrowReceiver<in TIn>
    {

        /// <summary>
        /// Accepts a message of type TIn from a sender for further processing
        /// and delivery to the subscribers.
        /// </summary>
        /// <param name="Sender">The sender of the message.</param>
        /// <param name="MessageIn">The message.</param>
        void ReceiveMessage(dynamic Sender, TIn MessageIn);

    }

    #endregion

}
