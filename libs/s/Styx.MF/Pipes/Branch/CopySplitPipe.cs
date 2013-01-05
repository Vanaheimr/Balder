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

    /// <summary>
    /// CopySplitPipe takes a number of pipes during construction.
    /// Every object pulled through CopySplitPipe is copied to each of the internal pipes.
    /// </summary>
    /// <typeparam name="S">The type of the consuming and emitting objects.</typeparam>
    public class CopySplitPipe<S> : AbstractPipe<S, S>, IMetaPipe<S,S>
    {

        #region Data

        private IEnumerable<IPipe> _Pipes;
        private List<CopyExpandablePipe<S>> _PipeStarts = new List<CopyExpandablePipe<S>>();

        #endregion

        #region CopyExpandablePipe class

        private class CopyExpandablePipe<T> : AbstractPipe<T, T>
        {

            #region Data

            private Queue<T> _Queue = new Queue<T>();

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

                if (_InternalEnumerator == null || _Queue == null)
                    return false;

                while (true)
                {

                    if (!_Queue.Any())
                    {
                        if (_InternalEnumerator.MoveNext())
                        {
                            _CurrentElement = _InternalEnumerator.Current;
                            return true;
                        }
                    }

                    else
                    {
                        _CurrentElement = _Queue.Dequeue();
                        return true;
                    }

                }

            }

            #endregion

            #region Add(t)

            public void Add(T t)
            {
                this._Queue.Enqueue(t);
            }

            #endregion

        }

        #endregion

        #region Constructor(s)

        #region SplitPipe(Pipes)

        /// <summary>
        /// Creates a new CopySplitPipe based on the given Pipes.
        /// </summary>
        public CopySplitPipe(IEnumerable<IPipe> Pipes)
        {

            _Pipes = Pipes;

            foreach(var _Pipe in _Pipes)
            {
                var _Expandable = new CopyExpandablePipe<S>();
                _Expandable.SetSource(this);
                _Pipe.SetSource(_Expandable);
                _PipeStarts.Add(_Expandable);
            }

        }

        #endregion

        #region SplitPipe(params Pipes)

        /// <summary>
        /// Creates a new CopySplitPipe based on the given Pipes.
        /// </summary>
        public CopySplitPipe(params IPipe[] Pipes)
        {

            _Pipes = Pipes;

            foreach (var _Pipe in _Pipes)
            {
                var _Expandable = new CopyExpandablePipe<S>();
                _Expandable.SetSource(this);
                _Pipe.SetSource(_Expandable);
                _PipeStarts.Add(_Expandable);
            }

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

            if (_InternalEnumerator == null)
                return false;

            if (_InternalEnumerator.MoveNext())
            {
                
                foreach(var _Expandable in _PipeStarts)
                    _Expandable.Add(_CurrentElement);

                return true;

            }

            return false;

        }

        #endregion


        #region Pipes

        /// <summary>
        /// A MetaPipe is a pipe that "wraps" some collection of pipes.
        /// </summary>
        public IEnumerable<IPipe> Pipes
        {
            get
            {
                return _Pipes;
            }
        }

        #endregion

        #region Reset()

        /// <summary>
        /// A pipe may maintain state. Reset is used to remove state.
        /// </summary>
        public override void Reset()
        {
            
            foreach(var _Pipe in _Pipes)
            {
                _Pipe.Reset();
            }

            base.Reset();

        }

        #endregion


        #region ToString()

        /// <summary>
        /// A string representation of this pipe.
        /// </summary>
        public override String ToString()
        {
 	        return base.ToString() + this._Pipes;
        }

        #endregion

    }

}
