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
 * distributed under the License is distributed on asn "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the Licesnse.
 */

#region Usings

using System;

#endregion

namespace de.ahzf.Blueprints
{

    #region BintreeException(Bintree, Element = default(T), Message = null, InnerException = null)

    /// <summary>
    /// The base class for all bintree exceptions.
    /// </summary>
    /// <typeparam name="T">The internal datatype of the bintree.</typeparam>
    public class BintreeException<T> : Exception
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        /// <summary>
        /// The bintree causing this exception.
        /// </summary>
        public Bintree<T> Bintree { get; private set; }

        /// <summary>
        /// An optional element causing this exception.
        /// </summary>
        public T          Element { get; private set; }

        /// <summary>
        /// A general bintree exception occurred!
        /// </summary>
        /// <param name="Bintree">The bintree causing this exception.</param>
        /// <param name="Element">An optional element causing this exception.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public BintreeException(Bintree<T> Bintree, T Element = default(T), String Message = null, Exception InnerException = null)
            : base(Message, InnerException)
        {
            this.Bintree = Bintree;
            this.Element = Element;
        }

    }

    #endregion


    #region BT_ZeroDimensionException(Bintree, Message = null, InnerException = null)

    /// <summary>
    /// An exception thrown when at least one dimension
    /// of the bintree is zero.
    /// </summary>
    /// <typeparam name="T">The internal datatype of the bintree.</typeparam>
    public class BT_ZeroDimensionException<T> : BintreeException<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        /// <summary>
        /// An exception thrown when at least one dimension
        /// of the bintree is zero.
        /// </summary>
        /// <param name="Bintree">The bintree causing this exception.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public BT_ZeroDimensionException(Bintree<T> Bintree, String Message = null, Exception InnerException = null)
            : base(Bintree, Message: "The given bintree has at least one zero dimension!" + Message, InnerException: InnerException)
        { }

    }

    #endregion

    #region BT_OutOfBoundsException(Bintree, Element, Message = null, InnerException = null)

    /// <summary>
    /// An exception thrown when the given element is
    /// located outside of the bintree bounds!
    /// </summary>
    /// <typeparam name="T">The internal datatype of the bintree.</typeparam>
    public class BT_OutOfBoundsException<T> : BintreeException<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        /// <summary>
        /// An exception thrown when the given element is
        /// located outside of the bintree bounds!
        /// </summary>
        /// <param name="Bintree">The bintree causing this exception.</param>
        /// <param name="Element">The element causing this exception.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public BT_OutOfBoundsException(Bintree<T> Bintree, T Element, String Message = null, Exception InnerException = null)
            : base(Bintree, Element, "The given element is located outside of the bintree bounds!" + Message, InnerException)
        { }

    }

    #endregion

}
