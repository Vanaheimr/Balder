/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Blueprints.NET <http://www.github.com/Vanaheimr/Blueprints.NET>
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

namespace de.ahzf.Vanaheimr.Blueprints
{

    #region EdgesException

    /// <summary>
    /// An exception during edge processing occurred!
    /// </summary>
    public class EdgesException : PropertyGraphException
    {

        /// <summary>
        /// An exception during edge processing occurred!
        /// </summary>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public EdgesException(String Message = null, Exception InnerException = null)
            : base(Message, InnerException)
        { }

    }

    #endregion


    #region DuplicateEdgeIdException

    /// <summary>
    /// A duplicate EdgeId was detected.
    /// </summary>
    /// <typeparam name="TId">The type of the edge ids.</typeparam>
    public class DuplicateEdgeIdException<TId> : EdgesException
    {

        /// <summary>
        /// A duplicate edge identification was detected.
        /// </summary>
        /// <param name="EdgeId">The unique id of the edge.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public DuplicateEdgeIdException(TId EdgeId, Exception InnerException = null)
            : base("Another edge with identification " + EdgeId + " already exists!", InnerException)
        { }

    }

    #endregion

}
