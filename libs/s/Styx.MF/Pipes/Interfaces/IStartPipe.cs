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
using System.Collections;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Styx
{

    #region IStartPipe

    /// <summary>
    /// An interface for the element consuming part of a pipe.
    /// Pipes implementing just this interface do not neccessarily
    /// emit elements, but e.g. might send them via network.
    /// </summary>
    public interface IStartPipe
    {

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        void SetSource(Object SourceElement);


        /// <summary>
        /// Set the elements emitted by the given IEnumerator as input.
        /// </summary> 
        /// <param name="IEnumerator">An IEnumerator as element source.</param>
        void SetSource(IEnumerator IEnumerator);


        /// <summary>
        /// Set the elements emitted from the given IEnumerable as input.
        /// </summary> 
        /// <param name="IEnumerable">An IEnumerable as element source.</param>
        void SetSourceCollection(IEnumerable IEnumerable);

    }

    #endregion

    #region IStartPipe<in S>

    /// <summary>
    /// An interface for the element consuming part of a pipe.
    /// Pipes implementing just this interface do not neccessarily
    /// emit elements, but e.g. might send them via network.
    /// </summary>
    /// <typeparam name="S">The type of the consuming objects.</typeparam>
    public interface IStartPipe<in S> : IStartPipe
    {

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        void SetSource(S SourceElement);

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S&gt; as input.
        /// </summary> 
        /// <param name="IEnumerator">An IEnumerator&lt;S&gt; as element source.</param>
        void SetSource(IEnumerator<S> IEnumerator);


        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S&gt; as input.
        /// </summary> 
        /// <param name="IEnumerable">An IEnumerable&lt;S&gt; as element source.</param>
        void SetSourceCollection(IEnumerable<S> IEnumerable);

    }

    #endregion

    #region IStartPipe<in S1, in S2>

    /// <summary>
    /// An interface for the element consuming part of a pipe.
    /// Pipes implementing just this interface do not neccessarily
    /// emit elements, but e.g. might send them via network.
    /// </summary>
    /// <typeparam name="S1">The type of the first consuming objects.</typeparam>
    /// <typeparam name="S2">The type of the second consuming objects.</typeparam>
    public interface IStartPipe<in S1, in S2> : IStartPipe
    {

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        void SetSource1(S1 SourceElement);

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        void SetSource2(S2 SourceElement);


        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S1&gt; as input.
        /// </summary> 
        /// <param name="IEnumerator">An IEnumerator&lt;S1&gt; as element source.</param>
        void SetSource1(IEnumerator<S1> IEnumerator);

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S2&gt; as input.
        /// </summary> 
        /// <param name="IEnumerator">An IEnumerator&lt;S2&gt; as element source.</param>
        void SetSource2(IEnumerator<S2> IEnumerator);


        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S1&gt; as input.
        /// </summary> 
        /// <param name="IEnumerable">An IEnumerable&lt;S1&gt; as element source.</param>
        void SetSourceCollection1(IEnumerable<S1> IEnumerable);

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S2&gt; as input.
        /// </summary> 
        /// <param name="IEnumerable">An IEnumerable&lt;S2&gt; as element source.</param>
        void SetSourceCollection2(IEnumerable<S2> IEnumerable);

    }

    #endregion

    #region IStartPipe<in S1, in S2, in S3>

    /// <summary>
    /// An interface for the element consuming part of a pipe.
    /// Pipes implementing just this interface do not neccessarily
    /// emit elements, but e.g. might send them via network.
    /// </summary>
    /// <typeparam name="S1">The type of the first consuming objects.</typeparam>
    /// <typeparam name="S2">The type of the second consuming objects.</typeparam>
    /// <typeparam name="S3">The type of the third consuming objects.</typeparam>
    public interface IStartPipe<in S1, in S2, in S3> : IStartPipe
    {

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        void SetSource1(S1 SourceElement);

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        void SetSource2(S2 SourceElement);

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        void SetSource3(S3 SourceElement);


        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S1&gt; as input.
        /// </summary> 
        /// <param name="IEnumerator">An IEnumerator&lt;S1&gt; as element source.</param>
        void SetSource1(IEnumerator<S1> IEnumerator);

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S2&gt; as input.
        /// </summary> 
        /// <param name="IEnumerator">An IEnumerator&lt;S2&gt; as element source.</param>
        void SetSource2(IEnumerator<S2> IEnumerator);

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S3&gt; as input.
        /// </summary> 
        /// <param name="IEnumerator">An IEnumerator&lt;S3&gt; as element source.</param>
        void SetSource3(IEnumerator<S3> IEnumerator);


        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S1&gt; as input.
        /// </summary> 
        /// <param name="IEnumerable">An IEnumerable&lt;S1&gt; as element source.</param>
        void SetSourceCollection1(IEnumerable<S1> IEnumerable);

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S2&gt; as input.
        /// </summary> 
        /// <param name="IEnumerable">An IEnumerable&lt;S2&gt; as element source.</param>
        void SetSourceCollection2(IEnumerable<S2> IEnumerable);

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S3&gt; as input.
        /// </summary> 
        /// <param name="IEnumerable">An IEnumerable&lt;S3&gt; as element source.</param>
        void SetSourceCollection3(IEnumerable<S3> IEnumerable);

    }

    #endregion

    #region IStartPipe<in S1, in S2, in S3, in S4>

    /// <summary>
    /// An interface for the element consuming part of a pipe.
    /// Pipes implementing just this interface do not neccessarily
    /// emit elements, but e.g. might send them via network.
    /// </summary>
    /// <typeparam name="S1">The type of the first consuming objects.</typeparam>
    /// <typeparam name="S2">The type of the second consuming objects.</typeparam>
    /// <typeparam name="S3">The type of the third consuming objects.</typeparam>
    /// <typeparam name="S4">The type of the fourth consuming objects.</typeparam>
    public interface IStartPipe<in S1, in S2, in S3, in S4> : IStartPipe
    {

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        void SetSource1(S1 SourceElement);

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        void SetSource2(S2 SourceElement);

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        void SetSource3(S3 SourceElement);

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        void SetSource4(S4 SourceElement);


        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S1&gt; as input.
        /// </summary> 
        /// <param name="IEnumerator">An IEnumerator&lt;S1&gt; as element source.</param>
        void SetSource1(IEnumerator<S1> IEnumerator);

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S2&gt; as input.
        /// </summary> 
        /// <param name="IEnumerator">An IEnumerator&lt;S2&gt; as element source.</param>
        void SetSource2(IEnumerator<S2> IEnumerator);

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S3&gt; as input.
        /// </summary> 
        /// <param name="IEnumerator">An IEnumerator&lt;S3&gt; as element source.</param>
        void SetSource3(IEnumerator<S3> IEnumerator);

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S4&gt; as input.
        /// </summary> 
        /// <param name="IEnumerator">An IEnumerator&lt;S4&gt; as element source.</param>
        void SetSource4(IEnumerator<S4> IEnumerator);


        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S1&gt; as input.
        /// </summary> 
        /// <param name="IEnumerable">An IEnumerable&lt;S1&gt; as element source.</param>
        void SetSourceCollection1(IEnumerable<S1> IEnumerable);

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S2&gt; as input.
        /// </summary> 
        /// <param name="IEnumerable">An IEnumerable&lt;S2&gt; as element source.</param>
        void SetSourceCollection2(IEnumerable<S2> IEnumerable);

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S3&gt; as input.
        /// </summary> 
        /// <param name="IEnumerable">An IEnumerable&lt;S3&gt; as element source.</param>
        void SetSourceCollection3(IEnumerable<S3> IEnumerable);

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S4&gt; as input.
        /// </summary> 
        /// <param name="IEnumerable">An IEnumerable&lt;S4&gt; as element source.</param>
        void SetSourceCollection4(IEnumerable<S4> IEnumerable);

    }

    #endregion

    #region IStartPipe<in S1, in S2, in S3, in S4, in S5>

    /// <summary>
    /// An interface for the element consuming part of a pipe.
    /// Pipes implementing just this interface do not neccessarily
    /// emit elements, but e.g. might send them via network.
    /// </summary>
    /// <typeparam name="S1">The type of the first consuming objects.</typeparam>
    /// <typeparam name="S2">The type of the second consuming objects.</typeparam>
    /// <typeparam name="S3">The type of the third consuming objects.</typeparam>
    /// <typeparam name="S4">The type of the fourth consuming objects.</typeparam>
    /// <typeparam name="S5">The type of the fifth consuming objects.</typeparam>
    public interface IStartPipe<in S1, in S2, in S3, in S4, in S5> : IStartPipe
    {

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        void SetSource1(S1 SourceElement);

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        void SetSource2(S2 SourceElement);

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        void SetSource3(S3 SourceElement);

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        void SetSource4(S4 SourceElement);

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        void SetSource5(S5 SourceElement);


        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S1&gt; as input.
        /// </summary> 
        /// <param name="IEnumerator">An IEnumerator&lt;S1&gt; as element source.</param>
        void SetSource1(IEnumerator<S1> IEnumerator);

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S2&gt; as input.
        /// </summary> 
        /// <param name="IEnumerator">An IEnumerator&lt;S2&gt; as element source.</param>
        void SetSource2(IEnumerator<S2> IEnumerator);

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S3&gt; as input.
        /// </summary> 
        /// <param name="IEnumerator">An IEnumerator&lt;S3&gt; as element source.</param>
        void SetSource3(IEnumerator<S3> IEnumerator);

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S4&gt; as input.
        /// </summary> 
        /// <param name="IEnumerator">An IEnumerator&lt;S4&gt; as element source.</param>
        void SetSource4(IEnumerator<S4> IEnumerator);

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S5&gt; as input.
        /// </summary> 
        /// <param name="IEnumerator">An IEnumerator&lt;S5&gt; as element source.</param>
        void SetSource5(IEnumerator<S5> IEnumerator);


        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S1&gt; as input.
        /// </summary> 
        /// <param name="IEnumerable">An IEnumerable&lt;S1&gt; as element source.</param>
        void SetSourceCollection1(IEnumerable<S1> IEnumerable);

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S2&gt; as input.
        /// </summary> 
        /// <param name="IEnumerable">An IEnumerable&lt;S2&gt; as element source.</param>
        void SetSourceCollection2(IEnumerable<S2> IEnumerable);

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S3&gt; as input.
        /// </summary> 
        /// <param name="IEnumerable">An IEnumerable&lt;S3&gt; as element source.</param>
        void SetSourceCollection3(IEnumerable<S3> IEnumerable);

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S4&gt; as input.
        /// </summary> 
        /// <param name="IEnumerable">An IEnumerable&lt;S4&gt; as element source.</param>
        void SetSourceCollection4(IEnumerable<S4> IEnumerable);

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S5&gt; as input.
        /// </summary> 
        /// <param name="IEnumerable">An IEnumerable&lt;S5&gt; as element source.</param>
        void SetSourceCollection5(IEnumerable<S5> IEnumerable);

    }

    #endregion

}
