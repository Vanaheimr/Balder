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

    /// <summary>
    /// Extension methods for the BackFilterPipe.
    /// </summary>
    public static class BackFilterPipeExtensions
    {

        #region InV(this IEnumerable<IGenericPropertyEdge<...>>, VertexFilter = null)

        /// <summary>
        /// Returns the incomming vertices of the given PropertyEdges.
        /// </summary>
        /// <param name="IEnumerable">An enumeration of PropertyEdges.</param>
        /// <param name="VertexFilter">An optional delegate for vertex filtering.</param>
        /// <returns>The incomming vertices of the given PropertyEdges.</returns>
        public static BackFilterPipe<E, S> BackFilter<E, S>(this IPipe<S, E> IPipe, UInt64 Steps)
        {
            return new BackFilterPipe<E, S>(IPipe, Steps);
        }

        #endregion

    }


    #region InVertexPipe()

    /// <summary>
    /// BackFilterPipe will fully process the object through its internal pipe.
    /// If the internal pipe yields results, then the original object is emitted
    /// from the BackFilterPipe.
    /// </summary>
    /// <typeparam name="S">The type of the elements within the filter.</typeparam>
    public class BackFilterPipe<E, S> : AbstractPipe<E, S>
    {

        private IPipe<S, E> IPipe;
        private UInt64 Steps;

        //public BackFilterPipe(IPipe<S, Boolean> FilterPipe, IEnumerable<S> IEnumerable)
        //{
        //    this.FilterPipe      = FilterPipe;
        //    _InternalEnumerator = IEnumerable.GetEnumerator();
        //}

        //public BackFilterPipe(Func<S, Boolean> FilterFunc, IEnumerable<S> IEnumerable)
        //{
        //    this.FilterFunc = FilterFunc;
        //    _InternalEnumerator = IEnumerable.GetEnumerator();
        //}

        public BackFilterPipe(IPipe<S, E> IPipe, UInt64 Steps = 1)
        {
            this.IPipe = IPipe;
            this.Steps = Steps;
            _InputEnumerator = IPipe.GetEnumerator();
        }

        
        public override Boolean MoveNext()
        {

            if (_InputEnumerator == null)
                return false;

            while (_InputEnumerator.MoveNext())
            {

                var aa = this.Path;
                var bb = this.Path[this.Path.Count - 2 - (Int32) Steps];

                //_CurrentElement = _InternalEnumerator.Current;
                _CurrentElement = (S) bb;

                return true;

            }

            //while (true)
            //{
            //    S s = this.starts.next();
            //    this.pipe.setStarts(new SingleIterator<S>(s));
            //    if (pipe.hasNext())
            //    {
            //        while (pipe.hasNext())
            //        {
            //            pipe.next();
            //        }
            //        return s;
            //    }
            //}

            return false;

        }


        //#region Pipes

        ///// <summary>
        ///// A MetaPipe is a pipe that "wraps" some collection of pipes.
        ///// </summary>
        //public IEnumerable<IPipe> Pipes
        //{
        //    get
        //    {
        //        return new List<IPipe>() { FilterPipe as IPipe };
        //    }
        //}

        //#endregion


        //#region ToString()

        ///// <summary>
        ///// A string representation of this pipe.
        ///// </summary>
        //public override String ToString()
        //{
        //    return base.ToString() + "[" + FilterPipe + "]";
        //}

        //#endregion

    }

    #endregion

}
