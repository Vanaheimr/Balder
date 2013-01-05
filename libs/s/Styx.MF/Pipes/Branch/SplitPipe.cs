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
using System.Linq;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Styx
{


    public class Stub<S, E> : AbstractPipe<S, E>
    {

        Byte _Id;
        Func<Byte, Boolean> _MoveFunc;
        Func<E> _CurrentElementFunc;

        public Stub(Byte Id, Func<Byte, Boolean> MoveFunc, Func<E> CurrentElementFunc)
        {
            _Id = Id;
            _CurrentElementFunc = CurrentElementFunc;
            _MoveFunc = MoveFunc;
        }

        public override Boolean MoveNext()
        {

            if (_MoveFunc(_Id))
            {
                _CurrentElement = _CurrentElementFunc();
                return true;
            }

            return false;                

        }

        public override void Dispose()
        { }

    }


    #region SplitPipe<S, E>

    /// <summary>
    /// Converts the consuming objects to emitting objects
    /// by calling a Func&lt;S, E&gt;.
    /// </summary>
    /// <typeparam name="S">The type of the consuming and emitting objects.</typeparam>
    public class SplitPipe<S> : ISplitPipe<S, S, S>
    {

        #region Data

        /// <summary>
        /// The internal enumerator of the collection.
        /// </summary>
        protected IEnumerator<S> _InternalEnumerator;

        /// <summary>
        /// The internal current element in the collection.
        /// </summary>
        protected S _CurrentElement;

        private readonly Byte _Ids;
        private Boolean[] Moved;
        public Stub<S, S>[] Emit { get; private set; }


        #endregion

        #region Constructor(s)

        #region SplitPipe(Ids, IEnumerator = null, IEnumerable = null)

        /// <summary>
        /// Creates a new FuncPipe using the given Func&lt;S, E&gt;.
        /// </summary>
        public SplitPipe(Byte Ids, IEnumerable<S> IEnumerable = null, IEnumerator<S> IEnumerator = null)
        {

            if (IEnumerator != null && IEnumerable != null)
                throw new ArgumentException("Please decide between IEnumerator and IEnumerable!");

            if (IEnumerable != null)
                SetSourceCollection(IEnumerable);

            if (IEnumerator != null)
                SetSource(IEnumerator);

            _Ids  = Ids;
            Emit  = new Stub<S, S>[Ids];
            Moved = new Boolean[Ids];

            for (Byte i = 0; i < Ids; i++)
            {
                Emit[i]  = new Stub<S, S>(i, (Id) => MoveNext(Id), () => _CurrentElement);
                Moved[i] = true;
            }

        }

        #endregion

        #endregion

        
        #region SetSource(IEnumerator)

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S&gt; as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator&lt;S&gt; as element source.</param>
        public virtual void SetSource(IEnumerator<S> IEnumerator)
		{

            if (IEnumerator == null)
                throw new ArgumentNullException("IEnumerator must not be null!");

	        if (IEnumerator is IEndPipe<S>)
	            _InternalEnumerator = IEnumerator;
	        else
	            _InternalEnumerator = new HistoryEnumerator<S>(IEnumerator);

	    }

        #endregion

        #region SetSourceCollection(IEnumerable)

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S&gt; as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable&lt;S&gt; as element source.</param>
        public virtual void SetSourceCollection(IEnumerable<S> IEnumerable)
		{

            if (IEnumerable == null)
                throw new ArgumentNullException("IEnumerable must not be null!");

	        SetSource(IEnumerable.GetEnumerator());

	    }

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
        private Boolean MoveNext(Byte Id)
        {

            if (_InternalEnumerator == null)
                return false;

            Boolean _Moved = true;
            foreach (var _bool in Moved)
                _Moved &= _bool;

            if (_Moved)
            {
                if (_InternalEnumerator.MoveNext())
                {
                    _CurrentElement = _InternalEnumerator.Current;

                    for (Byte i = 0; i < _Ids; i++)
                    {
                        Moved[i] = false;
                    }

                    Moved[Id] = true;
                    return true;

                }
            }
            else
            {
                Moved[Id] = true;
                // WHEN WE ARE MULTITHREADED => WAIT FOR ALL OTHERS!
                return true;
            }

            return false;

        }

        #endregion


        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }

    #endregion


}
