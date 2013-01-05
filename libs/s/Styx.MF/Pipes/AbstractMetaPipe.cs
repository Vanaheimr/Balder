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
using System.Collections;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Styx
{

    #region AbstractMetaPipe<S, E>

    /// <summary>
    /// An AbstractPipe provides most of the functionality that is repeated
    /// in every instance of a Pipe. Any subclass of AbstractPipe should simply
    /// implement MoveNext().
    /// </summary>
    /// <typeparam name="S">The type of the consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    public abstract class AbstractMetaPipe<S, E> : IMetaPipe<S, E>
	{
		
		#region Data

        private readonly IPipe[]       InternalPipes;

        private readonly IStartPipe<S> StartPipe;

        private readonly IEndPipe<E>   EndPipe;
		
		#endregion
		
		#region Constructor(s)

        #region AbstractMetaPipe()

        /// <summary>
        /// Creates a AbstractPipe pipe.
        /// </summary>
		public AbstractMetaPipe()
		{ }
		
		#endregion

        #region AbstractMetaPipe(InternalPipes, IEnumerator, IEnumerable)

        /// <summary>
        /// Creates a new AbstractPipe using the elements emitted
        /// by the given IEnumerator as input.
        /// </summary>
        /// <param name="InternalPipes">The array of all wrapped pipes.</param>
        /// <param name="IEnumerable">An IEnumerable&lt;S&gt; as element source.</param>
        /// <param name="IEnumerator">An IEnumerator&lt;S&gt; as element source.</param>
        public AbstractMetaPipe(IPipe[] InternalPipes, IEnumerable<S> IEnumerable, IEnumerator<S> IEnumerator)
        {

            #region Initial checks

            if (InternalPipes == null)
                throw new ArgumentNullException("The array of wrapped pipes must not be null!");

            if (InternalPipes.Length < 2)
                throw new ArgumentException("The array of wrapped pipes must at least wrap two pipes!");

            if (!(InternalPipes[0] is IStartPipe<S>))
                throw new ArgumentException("The first wrapped pipe must implement IStartPipe<S>!");

            if (!(InternalPipes[InternalPipes.Length-1] is IEndPipe<E>))
                throw new ArgumentException("The last wrapped pipe must implement IEndPipe<E>!");

            if (IEnumerator != null && IEnumerable != null)
                throw new ArgumentException("Please decide between IEnumerator and IEnumerable!");

            #endregion

            this.InternalPipes = InternalPipes;
            this.StartPipe     = InternalPipes[0] as IStartPipe<S>;
            this.EndPipe       = InternalPipes[InternalPipes.Length-1] as IEndPipe<E>;

            if (IEnumerable != null)
                this.StartPipe.SetSourceCollection(IEnumerable);

            if (IEnumerator != null)
                this.StartPipe.SetSource(IEnumerator);

            for (var i = 1; i < InternalPipes.Length; i++)
            {
                InternalPipes[i].SetSource(InternalPipes[i - 1]);
            }

        }

        #endregion
		
		#endregion


        #region SetSource(SourceElement)

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        void IStartPipe.SetSource(Object SourceElement)
        {
            StartPipe.SetSource(SourceElement);
        }

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        public virtual void SetSource(S SourceElement)
        {
            StartPipe.SetSource(SourceElement);
        }

        #endregion

        #region SetSource(IEnumerator)

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S&gt; as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator&lt;S&gt; as element source.</param>
        void IStartPipe.SetSource(IEnumerator IEnumerator)
        {
            StartPipe.SetSource(IEnumerator);
        }

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S&gt; as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator&lt;S&gt; as element source.</param>
        public virtual void SetSource(IEnumerator<S> IEnumerator)
        {
            StartPipe.SetSource(IEnumerator);
        }

        #endregion

        #region SetSourceCollection(IEnumerable)

        /// <summary>
        /// Set the elements emitted from the given IEnumerable as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable as element source.</param>
        void IStartPipe.SetSourceCollection(IEnumerable IEnumerable)
        {
            StartPipe.SetSourceCollection(IEnumerable);
        }

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S&gt; as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable&lt;S&gt; as element source.</param>
        public virtual void SetSourceCollection(IEnumerable<S> IEnumerable)
        {
            StartPipe.SetSourceCollection(IEnumerable);
        }

        #endregion


        #region GetEnumerator()

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A IEnumerator&lt;E&gt; that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<E> GetEnumerator()
        {
            return this;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A IEnumerator that can be used to iterate through the collection.
        /// </returns>
        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this;
        }

        #endregion

        #region Current

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        public virtual E Current
		{
			get
			{
                return EndPipe.Current;
			}
		}

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
		Object System.Collections.IEnumerator.Current
		{	
			get
			{
                return EndPipe.Current;
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
        public virtual Boolean MoveNext()
        {
            return EndPipe.MoveNext();
        }

        #endregion

        #region Reset()

        /// <summary>
        /// Sets the enumerator to its initial position, which is
        /// before the first element in the collection.
        /// </summary>
        public virtual void Reset()
		{
            foreach (var pipe in InternalPipes)
                pipe.Reset();
		}

        #endregion

        #region Dispose()

        /// <summary>
        /// Disposes this pipe.
        /// </summary>
        public virtual void Dispose()
		{
            foreach (var pipe in InternalPipes)
                pipe.Dispose();
		}

        #endregion


        #region Path

        /// <summary>
        /// Returns the transformation path to arrive at the current object
        /// of the pipe. This is a list of all of the objects traversed for
        /// the current iterator position of the pipe.
        /// </summary>
        public virtual List<Object> Path
        {

            get
            {
                throw new NotImplementedException();
            }

        }

        #endregion

        #region PathToHere

        private List<Object> PathToHere
		{

            get
            {
                throw new NotImplementedException();
            }

		}

        #endregion


        #region Pipes

        /// <summary>
        /// A list of all wrapped pipes
        /// </summary>
        public virtual IEnumerable<IPipe> Pipes
        {
            get
            {
                return InternalPipes;
            }
        }

        #endregion

        #region ToString()

        /// <summary>
        /// A string representation of this pipe.
        /// </summary>
        public override String ToString()
        {
            return this.GetType().Name + " (" + String.Join(", ", InternalPipes.Select(p => p.GetType().Name)) + ")";
        }

        #endregion

    }

    #endregion

 }