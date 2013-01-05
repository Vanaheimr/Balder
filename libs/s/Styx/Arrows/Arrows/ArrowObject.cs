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
    /// A generic Arrow transforming incoming messages into outgoing messages.
    /// </summary>
    /// <typeparam name="T">The type of the emitted messages/objects.</typeparam>
    public class ArrowObject<T> : AbstractArrowSender<T>
    {

        #region Properties

        #region Value

        private T _Value;

        /// <summary>
        /// The value of the ArrowObject.
        /// </summary>
        public T Value
        {

            get
            {
                return _Value;
            }

            set
            {

                if (_Value.Equals(value))
                    return;

                _Value = value;

                base.NotifyRecipients(this, value);

            }

        }

        #endregion

        #endregion

        #region Constructor(s)

        #region ArrowObject(Value)

        /// <summary>
        /// An object sending notifications when its value changed.
        /// </summary>
        /// <param name="Value">The value of the object.</param>
        public ArrowObject(T Value)
        {
            this._Value = Value;
        }

        #endregion

        #region ArrowObject(Value, MessageRecipients.Recipient, params MessageRecipients.Recipients)

        /// <summary>
        /// An object sending notifications when its value changed.
        /// </summary>
        /// <param name="Value">The value of the object.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public ArrowObject(T Value, MessageRecipient<T> Recipient, params MessageRecipient<T>[] Recipients)
            : base(Recipient, Recipients)
        {
            this._Value = Value;
        }

        #endregion

        #region ArrowObject(Value, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// An object sending notifications when its value changed.
        /// </summary>
        /// <param name="Value">The value of the object.</param>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public ArrowObject(T Value, IArrowReceiver<T> Recipient, params IArrowReceiver<T>[] Recipients)
            : base(Recipient, Recipients)
        {
            this._Value = Value;
        }

        #endregion

        #endregion


        #region Implicit conversions

        /// <summary>
        /// Implicit conversion from ArrowObject to T
        /// </summary>
        /// <param name="ArrowObject">An ArrowObject.</param>
        /// <returns>The value of the ArrowObject.</returns>
        public static implicit operator T(ArrowObject<T> ArrowObject)
        {
            return ArrowObject.Value;
        }

        /// <summary>
        /// Implicit conversion from T to ArrowObject
        /// </summary>
        /// <param name="Value">A value.</param>
        /// <returns>A new ArrowObject.</returns>
        public static implicit operator ArrowObject<T>(T Value)
        {
            return new ArrowObject<T>(Value);
        }

        #endregion

    }

}
