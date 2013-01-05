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

#if !SILVERLIGHT

#region Usings

using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace de.ahzf.Styx.Concurrent
{

    /// <summary>
    /// The AndFilterPipe takes a collection of pipes, where E is boolean.
    /// Each provided pipe is fed the same incoming S object concurrently.
    /// If all the pipes emit true, then the AndFilterPipe emits the
    /// incoming S object. If not, then the incoming S object is not emitted.
    /// </summary>
    /// <typeparam name="S">The type of the elements within the filter.</typeparam>
    public class AndFilterPipeConcurrent<S> : AbstractPipe<S, S>, IFilterPipe<S>
    {

        #region Data

        private readonly IEnumerable<IPipe<S, Boolean>> _Pipes;
		private volatile Boolean                        _And;

        #endregion

        #region Constructor(s)

        #region AndFilterPipeConcurrent(myPipes)

        /// <summary>
        /// Creates a new pipe based on the given pipes.
        /// </summary>
        /// <param name="myPipes">Multiple IPipes&lt;S, Boolean&gt;.</param>
        public AndFilterPipeConcurrent(params IPipe<S, Boolean>[] myPipes)
        {
            _Pipes = new List<IPipe<S, Boolean>>(myPipes);
			_And   = true;
        }

        #endregion

        #region AndFilterPipeConcurrent(myPipes)

        /// <summary>
        /// Creates a new pipe based on the given pipes.
        /// </summary>
        /// <param name="myPipes">A collection of IPipes&lt;S, Boolean&gt;.</param>
        public AndFilterPipeConcurrent(IEnumerable<IPipe<S, Boolean>> myPipes)
        {
            _Pipes = myPipes;
			_And   = true;
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

            while (_InternalEnumerator.MoveNext())
            {

                var _S                = _InternalEnumerator.Current;
				var _TaskCancellation = new CancellationTokenSource();
//					var _NumberOfTasks    = _Pipes.Count();
					
				Task[] _Tasks = (from _Pipe in _Pipes
                                    select Task<Boolean>.Factory.StartNew((_Pipe2) =>
                                    {

                                        ((IPipe<S,Boolean>) _Pipe2).SetSource(new SingleEnumerator<S>(_S));
				
				                        var _return = ((IPipe<S,Boolean>) _Pipe2).MoveNext();
						
//										 Interlocked.Decrement(ref _NumberOfTasks);
						
										if (_return == false)
										_And = false;
						
										return _return;
						
                                    },
                                        _Pipe,
                                        _TaskCancellation.Token,
                                        TaskCreationOptions.AttachedToParent,
                                        TaskScheduler.Current)).ToArray();

					
				while (_And == true && !_Tasks.All(_T => _T.IsCompleted))
				{
					// Wait until a task completes, but no longer than 100ms!
					Task.WaitAny(_Tasks, 100);
				}
					
				// Canel remaining tasks!
				_TaskCancellation.Cancel();
					
				return _And;

            }

            return false;

        }

        #endregion


    }

}

#endif
