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

namespace de.ahzf.Blueprints
{

    #region QuadtreeException(Quadtree, Pixel = null, Message = null, InnerException = null)

    /// <summary>
    /// The base class for all QuadtreeExceptions.
    /// </summary>
    public class QuadtreeException<T> : Exception
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        /// <summary>
        /// The Quadtree causing this exception.
        /// </summary>
        public Quadtree<T> Quadtree { get; private set; }

        /// <summary>
        /// An optional pixel causing this exception.
        /// </summary>
        public IPixel<T>   Pixel    { get; private set; }

        /// <summary>
        /// A general QuadtreeException exception occurred!
        /// </summary>
        /// <param name="Quadtree">The Quadtree causing this exception.</param>
        /// <param name="Pixel">An optional pixel causing this exception.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public QuadtreeException(Quadtree<T> Quadtree, IPixel<T> Pixel = null, String Message = null, Exception InnerException = null)
            : base(Message, InnerException)
        {
            this.Quadtree = Quadtree;
            this.Pixel    = Pixel;
        }

    }

    #endregion


    #region QT_ZeroDimensionException(Quadtree, Message = null, InnerException = null)

    /// <summary>
    /// An exception thrown when at least one dimension
    /// of the Quadtree is zero.
    /// </summary>
    public class QT_ZeroDimensionException<T> : QuadtreeException<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        /// <summary>
        /// An exception thrown when at least one dimension
        /// of the Quadtree is zero.
        /// </summary>
        /// <param name="Quadtree">The Quadtree causing this exception.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public QT_ZeroDimensionException(Quadtree<T> Quadtree, String Message = null, Exception InnerException = null)
            : base(Quadtree, Message: "The given Quadtree has at least one zero dimension!" + Message, InnerException: InnerException)
        { }

    }

    #endregion

    #region QT_OutOfBoundsException(Quadtree, Pixel, Message = null, InnerException = null)

    /// <summary>
    /// An exception thrown when the given pixel is
    /// located outside of the Quadtree bounds!
    /// </summary>
    public class QT_OutOfBoundsException<T> : QuadtreeException<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        /// <summary>
        /// A general QuadtreeException exception occurred!
        /// </summary>
        /// <param name="Quadtree">The Quadtree causing this exception.</param>
        /// <param name="Pixel">The pixel causing this exception.</param>
        /// <param name="Message">The message that describes the error.</param>
        /// <param name="InnerException">The exception that is the cause of the current exception.</param>
        public QT_OutOfBoundsException(Quadtree<T> Quadtree, IPixel<T> Pixel, String Message = null, Exception InnerException = null)
            : base(Quadtree, Pixel, "The given pixel is located outside of the Quadtree bounds!" + Message, InnerException)
        { }

    }

    #endregion

}
