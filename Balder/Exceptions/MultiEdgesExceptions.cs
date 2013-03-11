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

#endregion

namespace eu.Vanaheimr.Balder
{

    #region MultiEdgesException

    /// <summary>
    /// An exception during multiedge processing occurred!
    /// </summary>
    public class MultiEdgesException : PropertyGraphException
    {

        /// <summary>
        /// An exception during multiedge processing occurred!
        /// </summary>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public MultiEdgesException(String Message = null, Exception InnerException = null)
            : base(Message, InnerException)
        { }

    }

    #endregion


    #region DuplicateMultiEdgeIdException

    /// <summary>
    /// A duplicate MultiEdgeId was detected.
    /// </summary>
    /// <typeparam name="TId">The type of the multiedge ids.</typeparam>
    public class DuplicateMultiEdgeIdException<TId> : MultiEdgesException
    {

        /// <summary>
        /// A duplicate multiedge identification was detected.
        /// </summary>
        /// <param name="MultiEdgeId">The unique id of the multiedge.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public DuplicateMultiEdgeIdException(TId MultiEdgeId, Exception InnerException = null)
            : base("Another multiedge with identification " + MultiEdgeId + " already exists!", InnerException)
        { }

    }

    #endregion

}
