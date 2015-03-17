/*
 * Copyright (c) 2010-2015, Achim 'ahzf' Friedland <achim@graphdefined.org>
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

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Styx.Arrows;

#endregion

namespace org.GraphDefined.Vanaheimr.Balder
{

    /// <summary>
    /// Emits the revision identifications of the given revisioned objects.
    /// </summary>
    /// <typeparam name="TRevId">The type of the revision identifications.</typeparam>
    public class RevIdArrow<TRevId> : MapArrow<IRevisionId<TRevId>, TRevId>
        where TRevId : IEquatable<TRevId>, IComparable<TRevId>, IComparable
    {

        /// <summary>
        /// Emits the revision identifications of the given revisioned objects.
        /// </summary>
        public RevIdArrow()
            : base(Object => (Object != null) ? Object.RevId : default(TRevId))
        { }

    }

}
