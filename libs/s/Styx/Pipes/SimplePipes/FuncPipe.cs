/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
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

namespace de.ahzf.Vanaheimr.Styx
{

    #region FuncPipe<S, E>

    /// <summary>
    /// Converts the consuming objects to emitting objects
    /// by calling a Func&lt;S, E&gt;.
    /// </summary>
    /// <typeparam name="S">The type of the consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    public class FuncPipe<S, E> : AbstractPipe<S, E>
    {

        #region Data

        private Func<S, E> _Func;

        #endregion

        #region Constructor(s)

        #region FuncPipe(myFunc)

        /// <summary>
        /// Creates a new FuncPipe using the given Func&lt;S, E&gt;.
        /// </summary>
        /// <param name="myFunc">A Func&lt;S, E&gt; converting the consuming objects into emitting objects.</param>
        /// <param name="IEnumerable">An optional IEnumerable&lt;S&gt; as element source.</param>
        /// <param name="IEnumerator">An optional IEnumerator&lt;S&gt; as element source.</param>
        public FuncPipe(Func<S, E> myFunc, IEnumerable<S> IEnumerable = null, IEnumerator<S> IEnumerator = null)
            : base(IEnumerable, IEnumerator)
        {

            if (myFunc == null)
                throw new ArgumentNullException("myFunc must not be null!");

            _Func = myFunc;

        }

        #endregion

        #endregion


        #region MoveNext()

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// True if the enumerator was successfully advanced to the next
        /// element; false if the enumerator has passed the end of the
        /// collection.
        /// </returns>
        public override Boolean MoveNext()
        {

            if (_InputEnumerator == null)
                return false;

            if (_InputEnumerator.MoveNext())
            {
                _CurrentElement = _Func(_InputEnumerator.Current);
                return true;
            }

            return false;

        }

        #endregion

    }

    #endregion

    #region FuncPipe<S1, S2, E>

    /// <summary>
    /// Converts the consuming objects to emitting objects
    /// by calling a Func&lt;S1, S2, E&gt;.
    /// </summary>
    /// <typeparam name="S1">The type of the consuming objects.</typeparam>
    /// <typeparam name="S2">The type of the consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    public class FuncPipe<S1, S2, E> : AbstractPipe<S1, S2, E>
    {

        #region Data

        private Func<S1, S2, E> _Func;

        #endregion

        #region Constructor(s)

        #region FuncPipe(Func)

        /// <summary>
        /// Creates a new FuncPipe using the elements emitted
        /// by the given IEnumerables as input.
        /// </summary>
        /// <param name="Func">A Func&lt;S1, S2, E&gt; converting the consuming objects into emitting objects.</param>
        /// <param name="IEnumerator1">An optional enumerator of directories as element source.</param>
        /// <param name="IEnumerator2">An optional enumerator of directories as element source.</param>
        public FuncPipe(Func<S1, S2, E> Func,
                        IEnumerator<S1> IEnumerator1 = null,
                        IEnumerator<S2> IEnumerator2 = null)
        {

            if (Func == null)
                throw new ArgumentNullException("The given Func must not be null!");

            _Func = Func;

            if (IEnumerator1 != null)
                SetSource1(IEnumerator1);

            if (IEnumerator2 != null)
                SetSource2(IEnumerator2);

        }

        #endregion
		
		#endregion


		#region MoveNext()

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// True if the enumerator was successfully advanced to the next
        /// element; false if the enumerator has passed the end of the
        /// collection.
        /// </returns>
        public override Boolean MoveNext()
        {

            if (_InternalEnumerator1 == null)
                return false;

            if (_InternalEnumerator2 == null)
                return false;

            if (_InternalEnumerator1.MoveNext())
                if (_InternalEnumerator2.MoveNext())
                {
                    _CurrentElement = _Func(_InternalEnumerator1.Current, _InternalEnumerator2.Current);
			    	return true;
                }

            return false;

        }

        #endregion

    }

    #endregion

    #region FuncPipe<S1, S2, S3, E>

    /// <summary>
    /// Converts the consuming objects to emitting objects
    /// by calling a Func&lt;S1, S2, S3, E&gt;.
    /// </summary>
    /// <typeparam name="S1">The type of the consuming objects.</typeparam>
    /// <typeparam name="S2">The type of the consuming objects.</typeparam>
    /// <typeparam name="S3">The type of the consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    public class FuncPipe<S1, S2, S3, E> : AbstractPipe<S1, S2, S3, E>
    {

        #region Data

        private Func<S1, S2, S3, E> _Func;

        #endregion

        #region Constructor(s)

        #region FuncPipe(Func)

        /// <summary>
        /// Creates a new FuncPipe using the elements emitted
        /// by the given IEnumerables as input.
        /// </summary>
        /// <param name="Func">A Func&lt;S1, S2, S3, E&gt; converting the consuming objects into emitting objects.</param>
        /// <param name="IEnumerator1">An optional enumerator of directories as element source.</param>
        /// <param name="IEnumerator2">An optional enumerator of directories as element source.</param>
        /// <param name="IEnumerator3">An optional enumerator of directories as element source.</param>
        public FuncPipe(Func<S1, S2, S3, E> Func,
                        IEnumerator<S1> IEnumerator1 = null,
                        IEnumerator<S2> IEnumerator2 = null,
                        IEnumerator<S3> IEnumerator3 = null)
        {

            if (Func == null)
                throw new ArgumentNullException("The given Func must not be null!");

            _Func = Func;

        }

        #endregion
		
		#endregion


		#region MoveNext()

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// True if the enumerator was successfully advanced to the next
        /// element; false if the enumerator has passed the end of the
        /// collection.
        /// </returns>
        public override Boolean MoveNext()
        {

            if (_InternalEnumerator1 == null)
                return false;

            if (_InternalEnumerator2 == null)
                return false;

            if (_InternalEnumerator3 == null)
                return false;

            if (_InternalEnumerator1.MoveNext())
                if (_InternalEnumerator2.MoveNext())
                    if (_InternalEnumerator3.MoveNext())
                    {
                        _CurrentElement = _Func(_InternalEnumerator1.Current, _InternalEnumerator2.Current, _InternalEnumerator3.Current);
			        	return true;
                    }

            return false;

        }

        #endregion
		
	}

    #endregion

    #region FuncPipe<S1, S2, S3, S4, E>

    /// <summary>
    /// Converts the consuming objects to emitting objects
    /// by calling a Func&lt;S1, S2, S3, S4, E&gt;.
    /// </summary>
    /// <typeparam name="S1">The type of the consuming objects.</typeparam>
    /// <typeparam name="S2">The type of the consuming objects.</typeparam>
    /// <typeparam name="S3">The type of the consuming objects.</typeparam>
    /// <typeparam name="S4">The type of the consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    public class FuncPipe<S1, S2, S3, S4, E> : AbstractPipe<S1, S2, S3, S4, E>
    {

        #region Data

        private Func<S1, S2, S3, S4, E> _Func;

        #endregion

        #region Constructor(s)

        #region FuncPipe(Func)

        /// <summary>
        /// Creates a new FuncPipe using the elements emitted
        /// by the given IEnumerables as input.
        /// </summary>
        /// <param name="Func">A Func&lt;S1, S2, S3, S4, E&gt; converting the consuming objects into emitting objects.</param>
        /// <param name="IEnumerator1">An optional enumerator of directories as element source.</param>
        /// <param name="IEnumerator2">An optional enumerator of directories as element source.</param>
        /// <param name="IEnumerator3">An optional enumerator of directories as element source.</param>
        /// <param name="IEnumerator4">An optional enumerator of directories as element source.</param>
        public FuncPipe(Func<S1, S2, S3, S4, E> Func,
                        IEnumerator<S1> IEnumerator1 = null,
                        IEnumerator<S2> IEnumerator2 = null,
                        IEnumerator<S3> IEnumerator3 = null,
                        IEnumerator<S4> IEnumerator4 = null)
        {

            if (Func == null)
                throw new ArgumentNullException("The given Func must not be null!");

            _Func = Func;

        }

        #endregion
		
		#endregion


		#region MoveNext()

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// True if the enumerator was successfully advanced to the next
        /// element; false if the enumerator has passed the end of the
        /// collection.
        /// </returns>
        public override Boolean MoveNext()
        {

            if (_InternalEnumerator1 == null)
                return false;

            if (_InternalEnumerator2 == null)
                return false;

            if (_InternalEnumerator3 == null)
                return false;

            if (_InternalEnumerator4 == null)
                return false;

            if (_InternalEnumerator1.MoveNext())
                if (_InternalEnumerator2.MoveNext())
                    if (_InternalEnumerator3.MoveNext())
                        if (_InternalEnumerator4.MoveNext())
                        {
                            _CurrentElement = _Func(_InternalEnumerator1.Current, _InternalEnumerator2.Current, _InternalEnumerator3.Current, _InternalEnumerator4.Current);
			        	    return true;
                        }

            return false;

        }

        #endregion
		
	}

    #endregion

    #region FuncPipe<S1, S2, S3, S4, S5, E>

    /// <summary>
    /// Converts the consuming objects to emitting objects
    /// by calling a Func&lt;S1, S2, S3, S4, E&gt;.
    /// </summary>
    /// <typeparam name="S1">The type of the consuming objects.</typeparam>
    /// <typeparam name="S2">The type of the consuming objects.</typeparam>
    /// <typeparam name="S3">The type of the consuming objects.</typeparam>
    /// <typeparam name="S4">The type of the consuming objects.</typeparam>
    /// <typeparam name="S5">The type of the consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    public class FuncPipe<S1, S2, S3, S4, S5, E> : AbstractPipe<S1, S2, S3, S4, S5, E>
    {

        #region Data

        private Func<S1, S2, S3, S4, E> _Func;

        #endregion

        #region Constructor(s)

        #region FuncPipe(Func)

        /// <summary>
        /// Creates a new FuncPipe using the elements emitted
        /// by the given IEnumerables as input.
        /// </summary>
        /// <param name="Func">A Func&lt;S1, S2, S3, S4, E&gt; converting the consuming objects into emitting objects.</param>
        /// <param name="IEnumerator1">An optional enumerator of directories as element source.</param>
        /// <param name="IEnumerator2">An optional enumerator of directories as element source.</param>
        /// <param name="IEnumerator3">An optional enumerator of directories as element source.</param>
        /// <param name="IEnumerator4">An optional enumerator of directories as element source.</param>
        public FuncPipe(Func<S1, S2, S3, S4, E> Func,
                        IEnumerator<S1> IEnumerator1 = null,
                        IEnumerator<S2> IEnumerator2 = null,
                        IEnumerator<S3> IEnumerator3 = null,
                        IEnumerator<S4> IEnumerator4 = null)
        {

            if (Func == null)
                throw new ArgumentNullException("The given Func must not be null!");

            _Func = Func;

        }

        #endregion

        #endregion


        #region MoveNext()

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// True if the enumerator was successfully advanced to the next
        /// element; false if the enumerator has passed the end of the
        /// collection.
        /// </returns>
        public override Boolean MoveNext()
        {

            if (_InternalEnumerator1 == null)
                return false;

            if (_InternalEnumerator2 == null)
                return false;

            if (_InternalEnumerator3 == null)
                return false;

            if (_InternalEnumerator4 == null)
                return false;

            if (_InternalEnumerator1.MoveNext())
                if (_InternalEnumerator2.MoveNext())
                    if (_InternalEnumerator3.MoveNext())
                        if (_InternalEnumerator4.MoveNext())
                        {
                            _CurrentElement = _Func(_InternalEnumerator1.Current, _InternalEnumerator2.Current, _InternalEnumerator3.Current, _InternalEnumerator4.Current);
                            return true;
                        }

            return false;

        }

        #endregion

    }

    #endregion

}
