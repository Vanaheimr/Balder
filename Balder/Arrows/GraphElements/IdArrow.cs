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
    /// Emits the identifications of the given identifiable objects.
    /// </summary>
    /// <typeparam name="TId">The type of the identifications.</typeparam>
    public class IdArrow<TId> : FuncArrow<IIdentifier<TId>, TId>
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    {

        #region IdArrow()

        /// <summary>
        /// Emits the identifications of the given identifiable objects.
        /// </summary>
        public IdArrow()
            : base(Object => (Object != null) ? Object.Id : default(TId))
        { }

        #endregion

        #region IdArrow(MessageRecipients.Recipient, params MessageRecipients.Recipients)

        /// <summary>
        /// Emits the identifications of the given identifiable objects.
        /// </summary>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public IdArrow(MessageRecipient<TId> Recipient, params MessageRecipient<TId>[] Recipients)
            : base(Object => (Object != null) ? Object.Id : default(TId), Recipient, Recipients)
        { }

        #endregion

        #region IdArrow(MessageProcessor, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// Emits the identifications of the given identifiable objects.
        /// </summary>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public IdArrow(IArrowReceiver<TId> Recipient, params IArrowReceiver<TId>[] Recipients)
            : base(Object => (Object != null) ? Object.Id : default(TId), Recipient, Recipients)
        { }

        #endregion

    }

}
