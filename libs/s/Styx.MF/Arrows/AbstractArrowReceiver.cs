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
using System.Collections;

#endregion

namespace de.ahzf.Styx
{

    /// <summary>
    /// An AbstractArrow provides most of the functionality that is repeated
    /// in every instance of an Arrow. Any subclass of AbstractPipe should simply
    /// implement ProcessMessage(MessageIn, out MessageOut).
    /// An Arrow accepts/consumes messages/objects of type TIn and emits
    /// messages/objects of type TOut via an event.
    /// </summary>
    /// <typeparam name="TMessage">The type of the consuming messages/objects.</typeparam>
    public abstract class AbstractArrowReceiver : IArrowReceiver
	{

        #region Events

        /// <summary>
        /// An event for signaling the completion of a message delivery.
        /// </summary>
        public event CompletionRecipient OnCompleted;

        /// <summary>
        /// An event for signaling an exception.
        /// </summary>
        public event ExceptionRecipient OnError;

        #endregion
        
        #region Properties

        /// <summary>
        /// Turns the recording of the message delivery path ON or OFF.
        /// </summary>
        public Boolean RecordMessagePath { get; set; }

        /// <summary>
        /// Returns the message path.
        /// </summary>
        public IEnumerable Path { get; protected set;  }

        #endregion

		#region Constructor(s)

        #region AbstractArrowReceiver()

        /// <summary>
        /// Creates a new AbstractArrowReceiver.
        /// </summary>
		public AbstractArrowReceiver()
		{ }
		
		#endregion

        #endregion


        #region (abstract) ReceiveMessage(Sender, MessageIn)

        /// <summary>
        /// Accepts a message of type S from a sender for further processing
        /// and delivery to the subscribers.
        /// </summary>
        /// <param name="Sender">The sender of the message.</param>
        /// <param name="MessageIn">The message.</param>
        /// <returns>True if the message was accepted and could be processed; False otherwise.</returns>
        public abstract Boolean ReceiveMessage(Object Sender, Object MessageIn);

        #endregion

        #region Complete(Sender)

        /// <summary>
        /// Signale the completion of the message delivery.
        /// </summary>
        /// <param name="Sender">The sender of the completion signal.</param>
        public void Complete(Object Sender)
        {
            try
            {
                if (OnCompleted != null)
                    OnCompleted(this);
            }
            catch (Exception e)
            {
                if (OnError != null)
                    OnError(this, e);
            }
        }

        #endregion


        #region Dispose()

        /// <summary>
        /// Disposes this pipe.
        /// </summary>
        public virtual void Dispose()
		{ }

        #endregion

        #region ToString()

        /// <summary>
        /// A string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return this.GetType().Name;
        }

        #endregion

    }

}
