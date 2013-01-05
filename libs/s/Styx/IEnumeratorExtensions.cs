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

    /// <summary>
    /// Extension methods for the IEnumerator interface.
    /// </summary>
    public static class IEnumeratorExtensions
    {

#if SILVERLIGHT

        // todo!

#else

        #region ToSniper(this IEnumerator, Autostart = false, Async = false)

        /// <summary>
        /// Creates a new Sniper fireing the content of the given IEnumerable.
        /// </summary>
        /// <typeparam name="TMessage">The type of the emitted messages/objects.</typeparam>
        /// <param name="IEnumerator">An enumerator of messages/objects to send.</param>
        /// <param name="Autostart">Start the sniper automatically.</param>
        /// <param name="StartAsTask">Start the sniper within its own task.</param>
        /// <param name="InitialDelay">Set the initial delay of the sniper in milliseconds.</param>
        /// <returns>A new Sniper.</returns>
        public static Sniper<TMessage> ToSniper<TMessage>(this IEnumerator<TMessage> IEnumerator, Boolean Autostart = false, Boolean StartAsTask = false, Nullable<TimeSpan> InitialDelay = null)
        {
            return new Sniper<TMessage>(IEnumerator, Autostart, StartAsTask, InitialDelay);
        }

        #endregion

#endif

    }

}
