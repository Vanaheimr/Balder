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
	
    /// <summary>
    /// A MultiEnumerator takes multiple IEnumerators in its constructor
    /// and makes them behave like a single enumerator.
    /// The order in which objects are returned from both enumerators are with
    /// respect to the order of the enumerators passed into the constructor.
    /// </summary>
    /// <typeparam name="T">The type of the stored elements.</typeparam>
	public class MultiEnumerator<T> : IEnumerator<T>
	{
		
		#region Data

        private readonly IEnumerator<IEnumerator<T>> _IEnumerators;
        private          IEnumerator<T>              _CurrentEnumerator;
	
		#endregion
		
		#region Constructor(s)

        #region MultiEnumerator(myIEnumerators)

        /// <summary>
        /// Creates a new MultiEnumerator based on the given myIEnumerators.
        /// </summary>
        /// <param name="myIEnumerators">The enumerators to be wrapped.</param>
        public MultiEnumerator(params IEnumerator<T>[] myIEnumerators)
            : this(new List<IEnumerator<T>>(myIEnumerators))
        { }

        #endregion

        #region MultiEnumerator(myIEnumerators)

        /// <summary>
        /// Creates a new MultiEnumerator based on the given myIEnumerators.
        /// </summary>
        /// <param name="myIEnumerators">The enumerators to be wrapped.</param>
        public MultiEnumerator(IEnumerable<IEnumerator<T>> myIEnumerators)
        {
            
            _IEnumerators = myIEnumerators.GetEnumerator();

            if (_IEnumerators.MoveNext())
                _CurrentEnumerator = _IEnumerators.Current;

        }

        #endregion

        #endregion


        #region Current

        /// <summary>
        /// Return the current element of the current IEnumertor.
        /// </summary>
		public T Current
		{
			get
			{
                return _CurrentEnumerator.Current;
			}
		}

        /// <summary>
        /// Return the current element of the internal IEnumertor.
        /// </summary>
		Object System.Collections.IEnumerator.Current
		{	
			get
			{
                return _CurrentEnumerator.Current;
			}
		}

        #endregion

        #region MoveNext()

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>True if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		public Boolean MoveNext()
		{

            while (true)
            {

                if (_CurrentEnumerator == null)
                    return false;

                // Move to the next element of the current enumerator
                if (_CurrentEnumerator.MoveNext())
                    return true;

                else
                {

                    // Move to the next enumerator
                    if (_IEnumerators.MoveNext())
                        _CurrentEnumerator = _IEnumerators.Current;

                    else
                        return false;

                }

            }

		}

        #endregion

        #region Reset()

        /// <summary>
        /// Sets the enumerator to its initial position, which is
        /// before the first element in the collection.
        /// </summary>
        public void Reset()
		{
            _IEnumerators.Reset();
		}

        #endregion


        #region Dispose()

        /// <summary>
        /// Dispose this enumerator.
        /// </summary>
        public void Dispose()
        { }

        #endregion
	
	}

}
