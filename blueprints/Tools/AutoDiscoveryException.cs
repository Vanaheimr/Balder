/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using System;

#endregion

namespace de.ahzf.blueprints
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
