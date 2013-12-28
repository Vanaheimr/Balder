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

#endregion

namespace eu.Vanaheimr.Balder
{

    #region HyperEdgesException

    /// <summary>
    /// An exception during hyperedge processing occurred!
    /// </summary>
    public class HyperEdgesException : PropertyGraphException
    {

        /// <summary>
        /// An exception during hyperedge processing occurred!
        /// </summary>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public HyperEdgesException(String Message = null, Exception InnerException = null)
            : base(Message, InnerException)
        { }

    }

    #endregion


    #region DuplicateHyperEdgeIdException

    /// <summary>
    /// A duplicate HyperEdgeId was detected.
    /// </summary>
    /// <typeparam name="TId">The type of the hyperedge ids.</typeparam>
    public class DuplicateHyperEdgeIdException<TId> : HyperEdgesException
    {

        /// <summary>
        /// A duplicate hyperedge identification was detected.
        /// </summary>
        /// <param name="HyperEdgeId">The unique id of the hyperedge.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public DuplicateHyperEdgeIdException(TId HyperEdgeId, Exception InnerException = null)
            : base("Another hyperedge with identification " + HyperEdgeId + " already exists!", InnerException)
        { }

    }

    #endregion

}
