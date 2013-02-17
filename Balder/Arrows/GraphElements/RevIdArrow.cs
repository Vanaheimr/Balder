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

    #region RevIdArrow<TRevId>

    /// <summary>
    /// Emits the revision identifications of the given revisioned objects.
    /// </summary>
    /// <typeparam name="TRevId">The type of the revision identifications.</typeparam>
    public class RevIdArrow<TRevId> : FuncArrow<IRevisionId<TRevId>, TRevId>
        where TRevId : IEquatable<TRevId>, IComparable<TRevId>, IComparable
    {

        #region Constructor(s)

        #region RevIdArrow()

        /// <summary>
        /// Emits the revision identifications of the given revisioned objects.
        /// </summary>
        public RevIdArrow()
            : base(Object => Object.RevId)
        { }

        #endregion

        #region RevIdArrow(MessageRecipients.Recipient, params MessageRecipients.Recipients)

        /// <summary>
        /// Emits the revision identifications of the given revisioned objects.
        /// </summary>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public RevIdArrow(MessageRecipient<TRevId> Recipient, params MessageRecipient<TRevId>[] Recipients)
            : base(Object => Object.RevId, Recipient, Recipients)
        { }

        #endregion

        #region RevIdArrow(MessageProcessor, IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// Emits the revision identifications of the given revisioned objects.
        /// </summary>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">Further recipients of the processed messages.</param>
        public RevIdArrow(IArrowReceiver<TRevId> Recipient, params IArrowReceiver<TRevId>[] Recipients)
            : base(Object => Object.RevId, Recipient, Recipients)
        { }

        #endregion

        #endregion

    }

    #endregion

}
