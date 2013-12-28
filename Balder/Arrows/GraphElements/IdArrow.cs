/*
 * Copyright (c) 2010-2014, Achim 'ahzf' Friedland <achim@graphdefined.org>
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

using eu.Vanaheimr.Illias.Commons;
using eu.Vanaheimr.Styx.Arrows;

#endregion

namespace eu.Vanaheimr.Balder
{

    /// <summary>
    /// Emits the identifications of the given identifiable objects.
    /// </summary>
    /// <typeparam name="TId">The type of the identifications.</typeparam>
    public class IdArrow<TId> : MapArrow<IIdentifier<TId>, TId>
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    {

        /// <summary>
        /// Emits the identifications of the given identifiable objects.
        /// </summary>
        public IdArrow()
            : base(Object => (Object != null) ? Object.Id : default(TId))
        { }

    }

}
