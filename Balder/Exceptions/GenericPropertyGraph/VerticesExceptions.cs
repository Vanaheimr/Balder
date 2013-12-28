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

    #region VerticesException

    /// <summary>
    /// An exception during vertex processing occurred!
    /// </summary>
    public class VerticesException : PropertyGraphException
    {

        /// <summary>
        /// An exception during vertex processing occurred!
        /// </summary>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public VerticesException(String Message = null, Exception InnerException = null)
            : base(Message, InnerException)
        { }

    }

    #endregion


    #region DuplicateVertexIdException

    /// <summary>
    /// A duplicate VertexId was detected.
    /// </summary>
    /// <typeparam name="TId">The type of the vertex ids.</typeparam>
    public class DuplicateVertexIdException<TId> : VerticesException
    {

        /// <summary>
        /// A duplicate vertex identification was detected.
        /// </summary>
        /// <param name="VertexId">The unique id of the vertex.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public DuplicateVertexIdException(TId VertexId, Exception InnerException = null)
            : base("Another vertex with identification " + VertexId + " already exists!", InnerException)
        { }

    }

    #endregion

}
