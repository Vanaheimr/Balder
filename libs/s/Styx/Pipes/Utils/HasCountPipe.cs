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

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    /// <summary>
    /// The pipe must emit at least min and not more than max elements.
    /// </summary>
    /// <typeparam name="S">The type of the consuming objects.</typeparam>
    public class HasCountPipe<S> : AbstractPipe<S, Boolean>
    {

        #region Data

        private readonly Int64   _Minimum;
        private readonly Int64   _Maximum;
        private          Int64   _Counter;
        private          Boolean _Finished;

        #endregion

        #region Constructor(s)

        #region HasCountPipe(Minimum = -1, Maximum = -1)

        /// <summary>
        /// Creates a new HasCountPipe.
        /// </summary>
        /// <param name="Minimum">Minimal number of elements. Use -1 for no minimum.</param>
        /// <param name="Maximum">Maximal number of elements. Use -1 for no maximum.</param>
        public HasCountPipe(Int64 Minimum = -1, Int64 Maximum = -1)
        {
            _Minimum  = Minimum;
            _Maximum  = Maximum;
            _Counter  = 0;
            _Finished = false;
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

            if (_Finished)
                return false;

            this._Finished = true;

            if (_Minimum == -1 && _Maximum == -1)
                return true;

            while (true)
            {

                if (_InputEnumerator.MoveNext())
                {
                    
                    if (_Counter == _Maximum)
                        return false;
                    
                    this._Counter++;
                    
                    if (_Minimum != -1       &&
                        _Counter == _Minimum &&
                        _Maximum == -1)
                        return true;

                }

                else if (_Maximum != -1 && _Counter >= _Minimum)
                    return true;
                
                else
                    return false;

            }

        }

        #endregion


        public override void Reset()
        {            
            base.Reset();
            _Counter  = 0;
            _Finished = false;
        }


    }

}
