/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Balder <http://www.github.com/Vanaheimr/Balder>
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

using de.ahzf.Illias.Commons;
using de.ahzf.Vanaheimr.Styx;

#endregion

namespace de.ahzf.Vanaheimr.Balder
{

    /// <summary>
    /// Emits the labels of the given labeled objects.
    /// </summary>
    /// <typeparam name="TLabel">The type of the identifications.</typeparam>
    public class LabelArrow<TLabel> : FuncArrow<ILabel<TLabel>, TLabel>
        where TLabel : IEquatable<TLabel>, IComparable<TLabel>, IComparable
    {

        #region LabelArrow()

        /// <summary>
        /// Emits the labels of the given labeled objects.
        /// </summary>
        public LabelArrow()
            : base(Object => (Object != null) ? Object.Label : default(TLabel))
        { }

        #endregion

        #region LabelArrow(MessageRecipients.Recipient, params MessageRecipients.Recipients)

        /// <summary>
        /// Emits the labels of the given labeled objects.
        /// </summary>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public LabelArrow(MessageRecipient<TLabel> Recipient, params MessageRecipient<TLabel>[] Recipients)
            : base(Object => (Object != null) ? Object.Label : default(TLabel), Recipient, Recipients)
        { }

        #endregion

        #region LabelArrow(MessageProcessor, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// Emits the labels of the given labeled objects.
        /// </summary>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public LabelArrow(IArrowReceiver<TLabel> Recipient, params IArrowReceiver<TLabel>[] Recipients)
            : base(Object => (Object != null) ? Object.Label : default(TLabel), Recipient, Recipients)
        { }

        #endregion

    }

}
