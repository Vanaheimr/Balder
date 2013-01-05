/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
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

#endregion

namespace de.ahzf.Styx
{

    #region PipesException

    /// <summary>
    /// A general pipes exception.
    /// </summary>
    public class PipesException : Exception
    {

        /// <summary>
        /// A general pipes exception.
        /// </summary>
        public PipesException()
            : base()
        { }

        /// <summary>
        /// A general pipes exception.
        /// </summary>
        /// <param name="myMessage">An additional message.</param>
        public PipesException(String myMessage)
            : base(myMessage)
        { }

    }

    #endregion

    #region NoSuchElementException

    /// <summary>
    /// No such element could be found.
    /// </summary>
    public class NoSuchElementException : PipesException
	{

        /// <summary>
        /// No such element could be found.
        /// </summary>
        public NoSuchElementException()
            : base()
        { }

        /// <summary>
        /// No such element could be found.
        /// </summary>
        /// <param name="myMessage">An additional message.</param>
        public NoSuchElementException(String myMessage)
            : base(myMessage)
        { }

    }

    #endregion

    #region IllegalStateException

    /// <summary>
    /// An illegal state had been reached.
    /// </summary>
    public class IllegalStateException : PipesException
    {

        /// <summary>
        /// An illegal state had been reached.
        /// </summary>
        public IllegalStateException(String myMessage)
            : base(myMessage)
        { }

    }

    #endregion

}
