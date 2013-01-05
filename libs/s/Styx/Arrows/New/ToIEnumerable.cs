/*
 * Copyright (c) 2011-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
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
using System.Collections;
using System.Collections.Concurrent;

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    public static class ToIEnumerableExtention
    {

        public static IEnumerable<TMessage> ToIEnumerable<TMessage>(this INotification<TMessage> INotification)
        {
            return new ToIEnumerable<TMessage>(INotification);
        }

    }


    /// <summary>
    /// Filters the consuming objects by calling a Func&lt;S, Boolean&gt;.
    /// </summary>
    /// <typeparam name="TMessage">The type of the consuming and emitting messages/objects.</typeparam>
    public class ToIEnumerable<TMessage> : ITarget<TMessage>, IEnumerable<TMessage>, IEnumerator<TMessage>
    {

        #region Data

        /// <summary>
        /// A blocking collection as inter-thread message pipeline.
        /// </summary>
        private readonly BlockingCollection<TMessage> BlockingCollection;

        private readonly IEnumerator<TMessage> _InternalEnumerator;

        #endregion

        #region Constructor(s)

        #region ToIEnumerable()

        /// <summary>
        /// Filters the consuming objects by calling a Func&lt;S, Boolean&gt;.
        /// </summary>
        /// <param name="Func">A Func&lt;S, Boolean&gt; filtering the consuming objects. True means filter (ignore).</param>
        public ToIEnumerable()
        {
            this.BlockingCollection = new BlockingCollection<TMessage>();
            this._InternalEnumerator = BlockingCollection.GetConsumingEnumerable().GetEnumerator();
        }

        #endregion

        #region (internal) ToIEnumerable(INotification)

        /// <summary>
        /// Filters the consuming objects by calling a Func&lt;S, Boolean&gt;.
        /// </summary>
        /// <param name="Func">A Func&lt;S, Boolean&gt; filtering the consuming objects. True means filter (ignore).</param>
        internal ToIEnumerable(INotification<TMessage> INotification)
        {

            if (INotification != null)
                INotification.SendTo(this);

            this.BlockingCollection = new BlockingCollection<TMessage>();
            this._InternalEnumerator = BlockingCollection.GetConsumingEnumerable().GetEnumerator();

        }

        #endregion

        #endregion


        public void ProcessNotification(TMessage Message)
        {
            BlockingCollection.Add(Message);
        }

        public void ProcessError(dynamic Sender, Exception ExceptionMessage)
        {
            BlockingCollection.CompleteAdding();
        }

        public void ProcessCompleted(dynamic Sender, String Message)
        {
            BlockingCollection.CompleteAdding();
        }



        #region GetEnumerator()

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A IEnumerator&lt;E&gt; that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<TMessage> GetEnumerator()
        {
            return _InternalEnumerator;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A IEnumerator that can be used to iterate through the collection.
        /// </returns>
        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _InternalEnumerator;
        }

        #endregion

        #region Current

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        public TMessage Current
        {
            get
            {
                return _InternalEnumerator.Current;
            }
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        Object System.Collections.IEnumerator.Current
        {
            get
            {
                return _InternalEnumerator.Current;
            }
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
        public Boolean MoveNext()
        {
            return _InternalEnumerator.MoveNext();
        }

        #endregion

        #region Reset()

        /// <summary>
        /// Sets the enumerator to its initial position, which is
        /// before the first element in the collection.
        /// </summary>
        public virtual void Reset()
        {
            _InternalEnumerator.Reset();
        }

        #endregion

        #region Dispose()

        /// <summary>
        /// Disposes this pipe.
        /// </summary>
        public virtual void Dispose()
        {
            _InternalEnumerator.Dispose();
        }

        #endregion

    }

}
