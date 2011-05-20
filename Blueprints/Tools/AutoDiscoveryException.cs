/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET <http://www.github.com/ahzf/Blueprints.NET>
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

namespace de.ahzf.Blueprints.Tools
{

    /// <summary>
    /// The class for all errors within the AutoDiscovery&lt;T&gt; class
    /// </summary>
    public class AutoDiscoveryException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the AutoDiscoveryException class.
        /// </summary>
        public AutoDiscoveryException()
        { }

        /// <summary>
        /// Initializes a new instance of the AutoDiscoveryException class with a
        /// specified error message.
        /// </summary>
        /// <param name="myMessage">The error message that explains the reason for the exception.</param>
        public AutoDiscoveryException(String myMessage)
            : base(myMessage)
        { }

        /// <summary>
        /// Initializes a new instance of the AutoDiscoveryException class with a
        /// specified error message and a reference to the inner exception that is
        /// the cause of this exception.
        /// </summary>
        /// <param name="myMessage">The error message that explains the reason for the exception.</param>
        /// <param name="myInnerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public AutoDiscoveryException(String myMessage, Exception myInnerException)
            : base(myMessage, myInnerException)
        { }

    }

}
