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
using System.Collections.Generic;

#endregion

namespace de.ahzf.Styx
{

    #region AbstractSideEffectPipe<S, E, T>

    /// <summary>
    /// An AbstractSideEffectPipe provides the same functionality as the 
    /// AbstractPipe, but produces a side effect which can be retrieved
    /// by the SideEffect property.
    /// </summary>
    /// <typeparam name="S">The type of the consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    /// <typeparam name="T">The type of the sideeffect.</typeparam>
    public abstract class AbstractSideEffectPipe<S, E, T> : AbstractPipe<S, E>, ISideEffectPipe<S, E, T>
    {

        #region Properties

        /// <summary>
        /// The SideEffect produced by this Pipe.
        /// Use this reference for operations like:
        /// Interlocked.Increment(ref _SideEffect);
        /// </summary>
        protected T _SideEffect;

        /// <summary>
        /// The SideEffect produced by this Pipe.
        /// </summary>
        public T SideEffect
        {
            
            get
            {
                return _SideEffect;
            }

            protected set
            {
                _SideEffect = value;
            }

        }

        #endregion

        #region Constructor(s)

        #region AbstractSideEffectPipe()

        /// <summary>
        /// Creates a new AbstractSideEffectPipe.
        /// </summary>
		public AbstractSideEffectPipe()
		{ }
		
		#endregion

        #region AbstractSideEffectPipe(IEnumerator, IEnumerable)

        /// <summary>
        /// Creates a new AbstractSideEffectPipe using the elements
        /// emitted by the given IEnumerator as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable&lt;S&gt; as element source.</param>
        /// <param name="IEnumerator">An IEnumerator&lt;S&gt; as element source.</param>
        public AbstractSideEffectPipe(IEnumerable<S> IEnumerable, IEnumerator<S> IEnumerator)
            : base(IEnumerable, IEnumerator)
        { }

        #endregion
		
		#endregion

    }

    #endregion

    #region AbstractSideEffectPipe<S, E, T1, T2>

    /// <summary>
    /// An AbstractSideEffectPipe provides the same functionality as the 
    /// AbstractPipe, but produces a side effect which can be retrieved
    /// by the SideEffect property.
    /// </summary>
    /// <typeparam name="S">The type of the consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    /// <typeparam name="T1">The type of the first sideeffect.</typeparam>
    /// <typeparam name="T2">The type of the second sideeffect.</typeparam>
    public abstract class AbstractSideEffectPipe<S, E, T1, T2> : AbstractPipe<S, E>, ISideEffectPipe<S, E, T1, T2>
    {

        #region Properties

        #region SideEffect1

        /// <summary>
        /// The SideEffect produced by this Pipe.
        /// Use this reference for operations like:
        /// Interlocked.Increment(ref _SideEffect);
        /// </summary>
        protected T1 _SideEffect1;

        /// <summary>
        /// The first SideEffect produced by this Pipe.
        /// </summary>
        public T1 SideEffect1
        {

            get
            {
                return _SideEffect1;
            }

            protected set
            {
                _SideEffect1 = value;
            }

        }

        #endregion

        #region SideEffect2

        /// <summary>
        /// The SideEffect produced by this Pipe.
        /// Use this reference for operations like:
        /// Interlocked.Increment(ref _SideEffect);
        /// </summary>
        protected T2 _SideEffect2;

        /// <summary>
        /// The second SideEffect produced by this Pipe.
        /// </summary>
        public T2 SideEffect2
        {

            get
            {
                return _SideEffect2;
            }

            protected set
            {
                _SideEffect2 = value;
            }

        }

        #endregion

        #endregion

        #region Constructor(s)

        #region AbstractSideEffectPipe()

        /// <summary>
        /// Creates a new AbstractSideEffectPipe.
        /// </summary>
		public AbstractSideEffectPipe()
		{ }
		
		#endregion

        #region AbstractSideEffectPipe(IEnumerator, IEnumerable)

        /// <summary>
        /// Creates a new AbstractSideEffectPipe using the elements
        /// emitted by the given IEnumerator as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable&lt;S&gt; as element source.</param>
        /// <param name="IEnumerator">An IEnumerator&lt;S&gt; as element source.</param>
        public AbstractSideEffectPipe(IEnumerable<S> IEnumerable, IEnumerator<S> IEnumerator)
            : base(IEnumerable, IEnumerator)
        { }

        #endregion
		
		#endregion

    }

    #endregion

    #region AbstractSideEffectPipe<S, E, T1, T2, T3>

    /// <summary>
    /// An AbstractSideEffectPipe provides the same functionality as the 
    /// AbstractPipe, but produces a side effect which can be retrieved
    /// by the SideEffect property.
    /// </summary>
    /// <typeparam name="S">The type of the consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    /// <typeparam name="T1">The type of the first sideeffect.</typeparam>
    /// <typeparam name="T2">The type of the second sideeffect.</typeparam>
    /// <typeparam name="T3">The type of the third sideeffect.</typeparam>
    public abstract class AbstractSideEffectPipe<S, E, T1, T2, T3> : AbstractPipe<S, E>, ISideEffectPipe<S, E, T1, T2, T3>
    {

        #region Properties

        #region SideEffect1

        /// <summary>
        /// The first SideEffect produced by this Pipe.
        /// Use this reference for operations like:
        /// Interlocked.Increment(ref _SideEffect);
        /// </summary>
        protected T1 _SideEffect1;

        /// <summary>
        /// The first SideEffect produced by this Pipe.
        /// </summary>
        public T1 SideEffect1
        {

            get
            {
                return _SideEffect1;
            }

            protected set
            {
                _SideEffect1 = value;
            }

        }

        #endregion

        #region SideEffect2

        /// <summary>
        /// The second SideEffect produced by this Pipe.
        /// Use this reference for operations like:
        /// Interlocked.Increment(ref _SideEffect);
        /// </summary>
        protected T2 _SideEffect2;

        /// <summary>
        /// The second SideEffect produced by this Pipe.
        /// </summary>
        public T2 SideEffect2
        {

            get
            {
                return _SideEffect2;
            }

            protected set
            {
                _SideEffect2 = value;
            }

        }

        #endregion

        #region SideEffect3

        /// <summary>
        /// The third SideEffect produced by this Pipe.
        /// Use this reference for operations like:
        /// Interlocked.Increment(ref _SideEffect);
        /// </summary>
        protected T3 _SideEffect3;

        /// <summary>
        /// The third SideEffect produced by this Pipe.
        /// </summary>
        public T3 SideEffect3
        {

            get
            {
                return _SideEffect3;
            }

            protected set
            {
                _SideEffect3 = value;
            }

        }

        #endregion

        #endregion

        #region Constructor(s)

        #region AbstractSideEffectPipe()

        /// <summary>
        /// Creates a new AbstractSideEffectPipe.
        /// </summary>
        public AbstractSideEffectPipe()
        { }

        #endregion

        #region AbstractSideEffectPipe(IEnumerator, IEnumerable)

        /// <summary>
        /// Creates a new AbstractSideEffectPipe using the elements
        /// emitted by the given IEnumerator as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable&lt;S&gt; as element source.</param>
        /// <param name="IEnumerator">An IEnumerator&lt;S&gt; as element source.</param>
        public AbstractSideEffectPipe(IEnumerable<S> IEnumerable, IEnumerator<S> IEnumerator)
            : base(IEnumerable, IEnumerator)
        { }

        #endregion

        #endregion

    }

    #endregion

}
