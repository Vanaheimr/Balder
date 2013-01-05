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
	/// PipeHelper provides a collection of static methods that are useful when dealing with Pipes.
	/// </summary>
	public static class PipeHelper
    {

        #region FillCollection<T>(this myIEnumerator, myICollection)

        /// <summary>
        /// Fill the given collection with the elements emitted by the IEnumerator&lt;T&gt;.
        /// </summary>
        /// <typeparam name="T">The type of the emitted objects.</typeparam>
        /// <param name="myIEnumerator">An IEnumerator&lt;T&gt;.</param>
        /// <param name="myICollection">An ICollection&lt;T&gt;.</param>
	    public static void FillCollection<T>(this IEnumerator<T> myIEnumerator, ICollection<T> myICollection)
		{
	        while (myIEnumerator.MoveNext())
			{
	            myICollection.Add(myIEnumerator.Current);
	        }
	    }

        #endregion

        #region Counter<T>(this myIEnumerator)

        /// <summary>
        /// Counts the elements emitted by the IEnumerator&lt;T&gt;.
        /// </summary>
        /// <typeparam name="T">The type of the emitted objects.</typeparam>
        /// <param name="myIEnumerator">An IEnumerator&lt;T&gt;.</param>
        /// <returns>The number of elements emitted by the IEnumerator&lt;T&gt;.</returns>
        public static UInt64 Counter<T>(this IEnumerator<T> myIEnumerator)
		{
			
	        var _Counter = 0UL;
	        
			while (myIEnumerator.MoveNext())
	            _Counter++;
			
	        return _Counter;

        }

        #endregion

    }

}
