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
    /// FairMergePipe will, in a round robin fashion,
    /// emit the objects of its internal pipes.
    /// </summary>
    /// <typeparam name="S">The type of the consuming and emitting objects.</typeparam>
    public class FairMergePipe<S> : AbstractPipe<S, S>, IMetaPipe<S, S>
    {

        #region Data

        private IEnumerable<IPipe> _Pipes;
        Int32 current = 0;
        Int32 total;

        #endregion

        #region Constructor(s)

        #region FairMergePipe(Pipes)

        /// <summary>
        /// Creates a new FairMergePipe based on the given Pipes.
        /// </summary>
        public FairMergePipe(IEnumerable<IPipe> Pipes)
        {
            this._Pipes = Pipes;
            this.total  = Pipes.Count();
        }

        #endregion

        #region FairMergePipe(params Pipes)

        /// <summary>
        /// Creates a new FairMergePipe based on the given Pipes.
        /// </summary>
        public FairMergePipe(params IPipe[] Pipes)
        {
            this._Pipes = Pipes;
            this.total  = Pipes.Count();
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

            Int32 _Counter = 0;

            while (true)
            {

                _Counter++;

                var _CurrentPipe = _Pipes.ElementAt(this.current);

                if (_CurrentPipe.MoveNext())
                {
                    _CurrentElement = _InternalEnumerator.Current;
                    this.current = (this.current + 1) % this.total;
                    return true;
                }
                
                else if (_Counter == this.total)
                    throw new NoSuchElementException();
                
                else
                    this.current = (this.current + 1) % this.total;

            }

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

            foreach (var _Pipe in _Pipes)
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
