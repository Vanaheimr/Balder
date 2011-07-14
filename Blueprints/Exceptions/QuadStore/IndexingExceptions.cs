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

namespace de.ahzf.Blueprints.QuadStore
{

    #region IndexingException<T>

    /// <summary>
    /// An exception during index processing occurred!
    /// </summary>
    public class IndexingException<T> : QuadStoreException
        where T : IEquatable<T>, IComparable, IComparable<T>
    {

        /// <summary>
        /// The quad causing this exception.
        /// </summary>
        public IQuad<T> Quad { get; private set; }

        /// <summary>
        /// An exception during transaction processing occurred!
        /// </summary>
        /// <param name="Quad">The quad causing the exception.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public IndexingException(IQuad<T> Quad, String Message = null, Exception InnerException = null)
            : base(Message, InnerException)
        {
            this.Quad = Quad;
        }

    }

    #endregion


    #region AddToIndexException<T>

    /// <summary>
    /// A quad could not be added to an index.
    /// </summary>
    public class AddToIndexException<T> : IndexingException<T>
        where T : IEquatable<T>, IComparable, IComparable<T>
    {

        /// <summary>
        /// Throw a new AddToIndexException as a quad
        /// could not be added to an index.
        /// </summary>
        /// <param name="Quad">The quad causing the exception.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public AddToIndexException(IQuad<T> Quad = null, String Message = null, Exception InnerException = null)
            : base(Quad, Message, InnerException)
        { }

    }

    #endregion


    #region AddToQuadIdIndexException<T>

    /// <summary>
    /// A quad could not be added to the QuadIdIndex.
    /// </summary>
    public class AddToQuadIdIndexException<T> : AddToIndexException<T>
        where T : IEquatable<T>, IComparable, IComparable<T>
    {

        /// <summary>
        /// Throw a new AddToQuadIdIndexException as a quad
        /// could not be added to the QuadIdIndex.
        /// </summary>
        /// <param name="Quad">The quad causing the exception.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public AddToQuadIdIndexException(IQuad<T> Quad = null, String Message = null, Exception InnerException = null)
            : base(Quad, Message, InnerException)
        { }

    }

    #endregion

    #region AddToSubjectIndexException<T>

    /// <summary>
    /// A quad could not be added to the SubjectIndex.
    /// </summary>
    public class AddToSubjectIndexException<T> : AddToIndexException<T>
        where T : IEquatable<T>, IComparable, IComparable<T>
    {

        /// <summary>
        /// Throw a new AddToSubjectIndexException as a quad
        /// could not be added to the SubjectIndex.
        /// </summary>
        /// <param name="Quad">The quad causing the exception.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public AddToSubjectIndexException(IQuad<T> Quad = null, String Message = null, Exception InnerException = null)
            : base(Quad, Message, InnerException)
        { }

    }

    #endregion

    #region AddToPredicateIndexException<T>

    /// <summary>
    /// A quad could not be added to the PredicateIndex.
    /// </summary>
    public class AddToPredicateIndexException<T> : AddToIndexException<T>
        where T : IEquatable<T>, IComparable, IComparable<T>
    {

        /// <summary>
        /// Throw a new AddToPredicateIndexException as a quad
        /// could not be added to the PredicateIndex.
        /// </summary>
        /// <param name="Quad">The quad causing the exception.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public AddToPredicateIndexException(IQuad<T> Quad = null, String Message = null, Exception InnerException = null)
            : base(Quad, Message, InnerException)
        { }

    }

    #endregion

    #region AddToObjectIndexException<T>

    /// <summary>
    /// A quad could not be added to the ObjectIndex.
    /// </summary>
    public class AddToObjectIndexException<T> : AddToIndexException<T>
        where T : IEquatable<T>, IComparable, IComparable<T>
    {

        /// <summary>
        /// Throw a new AddToObjectIndexException as a quad
        /// could not be added to the ObjectIndex.
        /// </summary>
        /// <param name="Quad">The quad causing the exception.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public AddToObjectIndexException(IQuad<T> Quad = null, String Message = null, Exception InnerException = null)
            : base(Quad, Message, InnerException)
        { }

    }

    #endregion

    #region AddToContextIndexException<T>

    /// <summary>
    /// A quad could not be added to the ContextIndex.
    /// </summary>
    public class AddToContextIndexException<T> : AddToIndexException<T>
        where T : IEquatable<T>, IComparable, IComparable<T>
    {

        /// <summary>
        /// Throw a new AddToContextIndexException as a quad
        /// could not be added to the ContextIndex.
        /// </summary>
        /// <param name="Quad">The quad causing the exception.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public AddToContextIndexException(IQuad<T> Quad = null, String Message = null, Exception InnerException = null)
            : base(Quad, Message, InnerException)
        { }

    }

    #endregion

}
