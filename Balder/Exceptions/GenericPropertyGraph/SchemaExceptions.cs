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

namespace eu.Vanaheimr.Balder.Schema
{

    #region SchemaException

    /// <summary>
    /// A schema exception.
    /// </summary>
    public class SchemaException : PropertyGraphException
    {

        /// <summary>
        /// Create a new schema exception.
        /// </summary>
        /// <param name="Message">The error message that explains the reason for the exception.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
        public SchemaException(String Message = null, Exception InnerException = null)
            : base(Message, InnerException)
        { }

    }

    #endregion


    #region SchemaViolation

    /// <summary>
    /// A schema violation exception.
    /// </summary>
    public class SchemaViolation : SchemaException
    {

        /// <summary>
        /// Create a new schema violation exception.
        /// </summary>
        /// <param name="Message">The error message that explains the reason for the exception.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
        public SchemaViolation(String Message = null, Exception InnerException = null)
            : base(Message, InnerException)
        { }

    }

    #endregion

}
