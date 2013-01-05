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
using System.Collections;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Styx
{

    #region AbstractPipe<S, E>

    /// <summary>
    /// An AbstractPipe provides most of the functionality that is repeated
    /// in every instance of a Pipe. Any subclass of AbstractPipe should simply
    /// implement MoveNext().
    /// </summary>
    /// <typeparam name="S">The type of the consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    public abstract class AbstractPipe<S, E> : IPipe<S, E>
	{
		
		#region Data
		
        /// <summary>
        /// The internal enumerator of the collection.
        /// </summary>
		protected IEnumerator<S> _InternalEnumerator;


        /// <summary>
        /// The internal current element in the collection.
        /// </summary>
	    protected E              _CurrentElement;
		
		#endregion
		
		#region Constructor(s)
		
		#region AbstractPipe()
		
        /// <summary>
        /// Creates a AbstractPipe pipe.
        /// </summary>
		public AbstractPipe()
		{ }
		
		#endregion

        #region AbstractPipe(IEnumerator, IEnumerable)

        /// <summary>
        /// Creates a new AbstractPipe using the elements emitted
        /// by the given IEnumerator as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable&lt;S&gt; as element source.</param>
        /// <param name="IEnumerator">An IEnumerator&lt;S&gt; as element source.</param>
        public AbstractPipe(IEnumerable<S> IEnumerable, IEnumerator<S> IEnumerator)
        {

            if (IEnumerator != null && IEnumerable != null)
                throw new ArgumentException("Please decide between IEnumerator and IEnumerable!");

            if (IEnumerable != null)
                SetSourceCollection(IEnumerable);

            if (IEnumerator != null)
                SetSource(IEnumerator);

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
            SetSource((S) SourceElement);
        }

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        public virtual void SetSource(S SourceElement)
        {
            _InternalEnumerator = new HistoryEnumerator<S>(new List<S>() { SourceElement }.GetEnumerator());
        }

        #endregion

        #region SetSource(IEnumerator)

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S&gt; as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator&lt;S&gt; as element source.</param>
        void IStartPipe.SetSource(IEnumerator IEnumerator)
		{
            SetSource((IEnumerator<S>) IEnumerator);
	    }

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
        /// Set the elements emitted from the given IEnumerable as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable as element source.</param>
        void IStartPipe.SetSourceCollection(IEnumerable IEnumerable)
        {
            SetSourceCollection((IEnumerable<S>) IEnumerable);
        }

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
        public E Current
		{
			get
			{
                return _CurrentElement;
			}
		}

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
		Object System.Collections.IEnumerator.Current
		{	
			get
			{
                return _CurrentElement;
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
        public abstract Boolean MoveNext();

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

                var _PathElements = PathToHere;
                var _Size         = _PathElements.Count;

                // do not repeat filters as they dup the object
                // todo: why is size == 0 required (Pangloss?)	        
                if (_Size == 0 || !_PathElements[_Size - 1].Equals(_CurrentElement))
                    _PathElements.Add(_CurrentElement);

                return _PathElements;

            }

        }

        #endregion

        #region PathToHere

        private List<Object> PathToHere
		{

            get
            {

                if (_InternalEnumerator is IPipe)
                    return ((IPipe) _InternalEnumerator).Path;

                else if (_InternalEnumerator is IHistoryEnumerator)
                {

                    var _List = new List<Object>();
                    var _Last = ((IHistoryEnumerator) _InternalEnumerator).Last;

                    if (_Last == null)
                        _List.Add(_InternalEnumerator.Current);
                    else
                        _List.Add(_Last);

                    return _List;

                }

                else if (_InternalEnumerator is ISingleEnumerator)
                    return new List<Object>() { ((ISingleEnumerator) _InternalEnumerator).Current };

                else
                    return new List<Object>();

            }

		}

        #endregion


        #region ToString()

        /// <summary>
        /// A string representation of this pipe.
        /// </summary>
        public override String ToString()
        {
            return this.GetType().Name;
        }

        #endregion

    }

    #endregion

    #region AbstractPipe<S1, S2, E>

    /// <summary>
    /// An AbstractPipe provides most of the functionality that is repeated
    /// in every instance of a Pipe. Any subclass of AbstractPipe should simply
    /// implement MoveNext().
    /// </summary>
    /// <typeparam name="S1">The type of the first consuming objects.</typeparam>
    /// <typeparam name="S2">The type of the second consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    public abstract class AbstractPipe<S1, S2, E> : IPipe<S1, S2, E>
	{
		
		#region Data
		
        /// <summary>
        /// The internal enumerator of the first collection.
        /// </summary>
		protected IEnumerator<S1> _InternalEnumerator1;


        /// <summary>
        /// The internal enumerator of the second collection.
        /// </summary>
        protected IEnumerator<S2> _InternalEnumerator2;


        /// <summary>
        /// The internal current element in the collection.
        /// </summary>
	    protected E _CurrentElement;
		
		#endregion
		
		#region Constructor(s)
		
		#region AbstractPipe()
		
        /// <summary>
        /// Creates a new abstract pipe.
        /// </summary>
		public AbstractPipe()
		{ }
		
		#endregion

        #region AbstractPipe(IEnumerator1, IEnumerator2)

        /// <summary>
        /// Creates a new abstract pipe using the elements emitted
        /// by the given IEnumerators as input.
        /// </summary>
        /// <param name="IEnumerator1">An IEnumerator&lt;S1&gt; as element source.</param>
        /// <param name="IEnumerator2">An IEnumerator&lt;S2&gt; as element source.</param>
        public AbstractPipe(IEnumerator<S1> IEnumerator1, IEnumerator<S2> IEnumerator2)
        {
            SetSource1(IEnumerator1);
            SetSource2(IEnumerator2);
        }

        #endregion

        #region AbstractPipe(IEnumerable1, IEnumerable2)

        /// <summary>
        /// Creates a new abstract pipe using the elements emitted
        /// by the given IEnumerables as input.
        /// </summary>
        /// <param name="IEnumerable1">An IEnumerable&lt;S1&gt; as element source.</param>
        /// <param name="IEnumerable2">An IEnumerable&lt;S2&gt; as element source.</param>
        public AbstractPipe(IEnumerable<S1> IEnumerable1, IEnumerable<S2> IEnumerable2)
        {   
            SetSourceCollection1(IEnumerable1);
            SetSourceCollection2(IEnumerable2);
        }

        #endregion
		
		#endregion


        #region SetSource(SourceElement)

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        public virtual void SetSource(Object SourceElement)
        {
            SetSource1((S1) SourceElement);
        }

        #endregion

        #region SetSource1(SourceElement)

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        public virtual void SetSource1(S1 SourceElement)
        {
            _InternalEnumerator1 = new HistoryEnumerator<S1>(new List<S1>() { SourceElement }.GetEnumerator());
        }

        #endregion

        #region SetSource2(SourceElement)

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        public virtual void SetSource2(S2 SourceElement)
        {
            _InternalEnumerator2 = new HistoryEnumerator<S2>(new List<S2>() { SourceElement }.GetEnumerator());
        }

        #endregion


        #region SetSource(IEnumerator)

        /// <summary>
        /// Set the elements emitted by the given IEnumerator as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator as element source.</param>
        public virtual void SetSource(IEnumerator IEnumerator)
        {
            SetSource1((IEnumerator<S1>) IEnumerator);
        }

        #endregion

        #region SetSource1(IEnumerator)

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S1&gt; as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator&lt;S1&gt; as element source.</param>
        public virtual void SetSource1(IEnumerator<S1> IEnumerator)
		{

            if (IEnumerator == null)
                throw new ArgumentNullException("IEnumerator must not be null!");

	        if (IEnumerator is IEndPipe<S1>)
	            _InternalEnumerator1 = IEnumerator;
	        else
	            _InternalEnumerator1 = new HistoryEnumerator<S1>(IEnumerator);

	    }

        #endregion

        #region SetSource2(IEnumerator)

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S2&gt; as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator&lt;S2&gt; as element source.</param>
        public virtual void SetSource2(IEnumerator<S2> IEnumerator)
        {

            if (IEnumerator == null)
                throw new ArgumentNullException("IEnumerator must not be null!");

            if (IEnumerator is IEndPipe<S2>)
                _InternalEnumerator2 = IEnumerator;
            else
                _InternalEnumerator2 = new HistoryEnumerator<S2>(IEnumerator);

        }

        #endregion


        #region SetSourceCollection(IEnumerable)

        /// <summary>
        /// Set the elements emitted from the given IEnumerable as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable as element source.</param>
        public virtual void SetSourceCollection(IEnumerable IEnumerable)
		{
            SetSourceCollection((IEnumerable<S1>) IEnumerable);
	    }

        #endregion

        #region SetSourceCollection1(IEnumerable)

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S1&gt; as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable&lt;S1&gt; as element source.</param>
        public virtual void SetSourceCollection1(IEnumerable<S1> IEnumerable)
        {

            if (IEnumerable == null)
                throw new ArgumentNullException("IEnumerable must not be null!");

            SetSource1(IEnumerable.GetEnumerator());

        }

        #endregion

        #region SetSourceCollection2(IEnumerable)

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S2&gt; as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable&lt;S2&gt; as element source.</param>
        public virtual void SetSourceCollection2(IEnumerable<S2> IEnumerable)
        {

            if (IEnumerable == null)
                throw new ArgumentNullException("IEnumerable must not be null!");

            SetSource2(IEnumerable.GetEnumerator());

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
        public E Current
		{
			get
			{
                return _CurrentElement;
			}
		}

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
		Object System.Collections.IEnumerator.Current
		{	
			get
			{
                return _CurrentElement;
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
        public abstract Boolean MoveNext();

        #endregion

        #region Reset()

        /// <summary>
        /// Sets the enumerators to their initial positions, which
        /// is before the first element in the collections.
        /// </summary>
        public virtual void Reset()
		{
            _InternalEnumerator1.Reset();
            _InternalEnumerator2.Reset();
		}

        #endregion

        #region Dispose()

        /// <summary>
        /// Disposes this pipe.
        /// </summary>
        public virtual void Dispose()
		{
            _InternalEnumerator1.Dispose();
            _InternalEnumerator2.Dispose();
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

                var _PathElements = PathToHere;
                var _Size         = _PathElements.Count;

                // do not repeat filters as they dup the object
                // todo: why is size == 0 required (Pangloss?)	        
                if (_Size == 0 || !_PathElements[_Size - 1].Equals(_CurrentElement))
                    _PathElements.Add(_CurrentElement);

                return _PathElements;

            }

        }

        #endregion

        #region PathToHere

        private List<Object> PathToHere
		{

            get
            {

                throw new NotImplementedException();

                //if (_InternalEnumerator is IPipe)
                //    return ((IPipe) _InternalEnumerator).Path;

                //else if (_InternalEnumerator is IHistoryEnumerator)
                //{

                //    var _List = new List<Object>();
                //    var _Last = ((IHistoryEnumerator) _InternalEnumerator).Last;

                //    if (_Last == null)
                //        _List.Add(_InternalEnumerator.Current);
                //    else
                //        _List.Add(_Last);

                //    return _List;

                //}

                //else if (_InternalEnumerator is ISingleEnumerator)
                //    return new List<Object>() { ((ISingleEnumerator) _InternalEnumerator).Current };

                //else
                //    return new List<Object>();

            }

		}

        #endregion


        #region ToString()

        /// <summary>
        /// A string representation of this pipe.
        /// </summary>
        public override String ToString()
        {
            return this.GetType().Name;
        }

        #endregion

    }

    #endregion

    #region AbstractPipe<S1, S2, S3, E>

    /// <summary>
    /// An AbstractPipe provides most of the functionality that is repeated
    /// in every instance of a Pipe. Any subclass of AbstractPipe should simply
    /// implement MoveNext().
    /// </summary>
    /// <typeparam name="S1">The type of the first consuming objects.</typeparam>
    /// <typeparam name="S2">The type of the second consuming objects.</typeparam>
    /// <typeparam name="S3">The type of the third consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    public abstract class AbstractPipe<S1, S2, S3, E> : IPipe<S1, S2, S3, E>
	{
		
		#region Data
		
        /// <summary>
        /// The internal enumerator of the first collection.
        /// </summary>
		protected IEnumerator<S1> _InternalEnumerator1;


        /// <summary>
        /// The internal enumerator of the second collection.
        /// </summary>
        protected IEnumerator<S2> _InternalEnumerator2;


        /// <summary>
        /// The internal enumerator of the third collection.
        /// </summary>
        protected IEnumerator<S3> _InternalEnumerator3;


        /// <summary>
        /// The internal current element in the collection.
        /// </summary>
	    protected E _CurrentElement;
		
		#endregion
		
		#region Constructor(s)
		
		#region AbstractPipe()
		
        /// <summary>
        /// Creates a new abstract pipe.
        /// </summary>
		public AbstractPipe()
		{ }
		
		#endregion

        #region AbstractPipe(IEnumerator1, IEnumerator2, IEnumerator3)

        /// <summary>
        /// Creates a new abstract pipe using the elements emitted
        /// by the given IEnumerators as input.
        /// </summary>
        /// <param name="IEnumerator1">An IEnumerator&lt;S1&gt; as element source.</param>
        /// <param name="IEnumerator2">An IEnumerator&lt;S2&gt; as element source.</param>
        /// <param name="IEnumerator3">An IEnumerator&lt;S3&gt; as element source.</param>
        public AbstractPipe(IEnumerator<S1> IEnumerator1, IEnumerator<S2> IEnumerator2, IEnumerator<S3> IEnumerator3)
        {
            SetSource1(IEnumerator1);
            SetSource2(IEnumerator2);
            SetSource3(IEnumerator3);
        }

        #endregion

        #region AbstractPipe(IEnumerable1, IEnumerable2, IEnumerable3)

        /// <summary>
        /// Creates a new abstract pipe using the elements emitted
        /// by the given IEnumerables as input.
        /// </summary>
        /// <param name="IEnumerable1">An IEnumerable&lt;S1&gt; as element source.</param>
        /// <param name="IEnumerable2">An IEnumerable&lt;S2&gt; as element source.</param>
        /// <param name="IEnumerable3">An IEnumerable&lt;S3&gt; as element source.</param>
        public AbstractPipe(IEnumerable<S1> IEnumerable1, IEnumerable<S2> IEnumerable2, IEnumerable<S3> IEnumerable3)
        {
            SetSourceCollection1(IEnumerable1);
            SetSourceCollection2(IEnumerable2);
            SetSourceCollection3(IEnumerable3);
        }

        #endregion
		
		#endregion


        #region SetSource(SourceElement)

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        public virtual void SetSource(Object SourceElement)
        {
            SetSource1((S1) SourceElement);
        }

        #endregion

        #region SetSource1(SourceElement)

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        public virtual void SetSource1(S1 SourceElement)
        {
            _InternalEnumerator1 = new HistoryEnumerator<S1>(new List<S1>() { SourceElement }.GetEnumerator());
        }

        #endregion

        #region SetSource2(SourceElement)

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        public virtual void SetSource2(S2 SourceElement)
        {
            _InternalEnumerator2 = new HistoryEnumerator<S2>(new List<S2>() { SourceElement }.GetEnumerator());
        }

        #endregion
        
        #region SetSource3(SourceElement)

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        public virtual void SetSource3(S3 SourceElement)
        {
            _InternalEnumerator3 = new HistoryEnumerator<S3>(new List<S3>() { SourceElement }.GetEnumerator());
        }

        #endregion


        #region SetSource(IEnumerator)

        /// <summary>
        /// Set the elements emitted by the given IEnumerator as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator as element source.</param>
        public virtual void SetSource(IEnumerator IEnumerator)
        {
            SetSource1((IEnumerator<S1>) IEnumerator);
        }

        #endregion

        #region SetSource1(IEnumerator)

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S1&gt; as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator&lt;S1&gt; as element source.</param>
        public virtual void SetSource1(IEnumerator<S1> IEnumerator)
        {

            if (IEnumerator == null)
                throw new ArgumentNullException("IEnumerator must not be null!");

            if (IEnumerator is IEndPipe<S1>)
                _InternalEnumerator1 = IEnumerator;
            else
                _InternalEnumerator1 = new HistoryEnumerator<S1>(IEnumerator);

        }

        #endregion

        #region SetSource2(IEnumerator)

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S2&gt; as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator&lt;S2&gt; as element source.</param>
        public virtual void SetSource2(IEnumerator<S2> IEnumerator)
        {

            if (IEnumerator == null)
                throw new ArgumentNullException("IEnumerator must not be null!");

            if (IEnumerator is IEndPipe<S2>)
                _InternalEnumerator2 = IEnumerator;
            else
                _InternalEnumerator2 = new HistoryEnumerator<S2>(IEnumerator);

        }

        #endregion

        #region SetSource3(IEnumerator)

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S3&gt; as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator&lt;S3&gt; as element source.</param>
        public virtual void SetSource3(IEnumerator<S3> IEnumerator)
        {

            if (IEnumerator == null)
                throw new ArgumentNullException("IEnumerator must not be null!");

            if (IEnumerator is IEndPipe<S3>)
                _InternalEnumerator3 = IEnumerator;
            else
                _InternalEnumerator3 = new HistoryEnumerator<S3>(IEnumerator);

        }

        #endregion


        #region SetSourceCollection(IEnumerable)

        /// <summary>
        /// Set the elements emitted from the given IEnumerable as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable as element source.</param>
        public virtual void SetSourceCollection(IEnumerable IEnumerable)
        {
            SetSourceCollection1((IEnumerable<S1>) IEnumerable);
        }

        #endregion

        #region SetSourceCollection1(IEnumerable)

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S1&gt; as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable&lt;S1&gt; as element source.</param>
        public virtual void SetSourceCollection1(IEnumerable<S1> IEnumerable)
		{

            if (IEnumerable == null)
                throw new ArgumentNullException("IEnumerable must not be null!");

	        SetSource1(IEnumerable.GetEnumerator());

	    }

        #endregion

        #region SetSourceCollection2(IEnumerable)

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S2&gt; as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable&lt;S2&gt; as element source.</param>
        public virtual void SetSourceCollection2(IEnumerable<S2> IEnumerable)
        {

            if (IEnumerable == null)
                throw new ArgumentNullException("IEnumerable must not be null!");

            SetSource2(IEnumerable.GetEnumerator());

        }

        #endregion

        #region SetSourceCollection3(IEnumerable)

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S3&gt; as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable&lt;S3&gt; as element source.</param>
        public virtual void SetSourceCollection3(IEnumerable<S3> IEnumerable)
        {

            if (IEnumerable == null)
                throw new ArgumentNullException("IEnumerable must not be null!");

            SetSource3(IEnumerable.GetEnumerator());

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
        public E Current
		{
			get
			{
                return _CurrentElement;
			}
		}

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
		Object System.Collections.IEnumerator.Current
		{	
			get
			{
                return _CurrentElement;
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
        public abstract Boolean MoveNext();

        #endregion

        #region Reset()

        /// <summary>
        /// Sets the enumerators to their initial positions, which
        /// is before the first element in the collections.
        /// </summary>
        public virtual void Reset()
		{
            _InternalEnumerator1.Reset();
            _InternalEnumerator2.Reset();
            _InternalEnumerator3.Reset();
		}

        #endregion

        #region Dispose()

        /// <summary>
        /// Disposes this pipe.
        /// </summary>
        public virtual void Dispose()
		{
            _InternalEnumerator1.Dispose();
            _InternalEnumerator2.Dispose();
            _InternalEnumerator3.Dispose();
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

                var _PathElements = PathToHere;
                var _Size         = _PathElements.Count;

                // do not repeat filters as they dup the object
                // todo: why is size == 0 required (Pangloss?)	        
                if (_Size == 0 || !_PathElements[_Size - 1].Equals(_CurrentElement))
                    _PathElements.Add(_CurrentElement);

                return _PathElements;

            }

        }

        #endregion

        #region PathToHere

        private List<Object> PathToHere
		{

            get
            {

                throw new NotImplementedException();

                //if (_InternalEnumerator is IPipe)
                //    return ((IPipe) _InternalEnumerator).Path;

                //else if (_InternalEnumerator is IHistoryEnumerator)
                //{

                //    var _List = new List<Object>();
                //    var _Last = ((IHistoryEnumerator) _InternalEnumerator).Last;

                //    if (_Last == null)
                //        _List.Add(_InternalEnumerator.Current);
                //    else
                //        _List.Add(_Last);

                //    return _List;

                //}

                //else if (_InternalEnumerator is ISingleEnumerator)
                //    return new List<Object>() { ((ISingleEnumerator) _InternalEnumerator).Current };

                //else
                //    return new List<Object>();

            }

		}

        #endregion


        #region ToString()

        /// <summary>
        /// A string representation of this pipe.
        /// </summary>
        public override String ToString()
        {
            return this.GetType().Name;
        }

        #endregion

    }

    #endregion

    #region AbstractPipe<S1, S2, S3, S4, E>

    /// <summary>
    /// An AbstractPipe provides most of the functionality that is repeated
    /// in every instance of a Pipe. Any subclass of AbstractPipe should simply
    /// implement MoveNext().
    /// </summary>
    /// <typeparam name="S1">The type of the first consuming objects.</typeparam>
    /// <typeparam name="S2">The type of the second consuming objects.</typeparam>
    /// <typeparam name="S3">The type of the third consuming objects.</typeparam>
    /// <typeparam name="S4">The type of the fourth consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    public abstract class AbstractPipe<S1, S2, S3, S4, E> : IPipe<S1, S2, S3, S4, E>
	{
		
		#region Data
		
        /// <summary>
        /// The internal enumerator of the first collection.
        /// </summary>
		protected IEnumerator<S1> _InternalEnumerator1;


        /// <summary>
        /// The internal enumerator of the second collection.
        /// </summary>
        protected IEnumerator<S2> _InternalEnumerator2;


        /// <summary>
        /// The internal enumerator of the third collection.
        /// </summary>
        protected IEnumerator<S3> _InternalEnumerator3;


        /// <summary>
        /// The internal enumerator of the third collection.
        /// </summary>
        protected IEnumerator<S4> _InternalEnumerator4;


        /// <summary>
        /// The internal current element in the collection.
        /// </summary>
	    protected E _CurrentElement;
		
		#endregion
		
		#region Constructor(s)
		
		#region AbstractPipe()
		
        /// <summary>
        /// Creates a new abstract pipe.
        /// </summary>
		public AbstractPipe()
		{ }
		
		#endregion

        #region AbstractPipe(IEnumerator1, IEnumerator2, IEnumerator3, IEnumerator4)

        /// <summary>
        /// Creates a new abstract pipe using the elements emitted
        /// by the given IEnumerators as input.
        /// </summary>
        /// <param name="IEnumerator1">An IEnumerator&lt;S1&gt; as element source.</param>
        /// <param name="IEnumerator2">An IEnumerator&lt;S2&gt; as element source.</param>
        /// <param name="IEnumerator3">An IEnumerator&lt;S3&gt; as element source.</param>
        /// <param name="IEnumerator4">An IEnumerator&lt;S4&gt; as element source.</param>
        public AbstractPipe(IEnumerator<S1> IEnumerator1, IEnumerator<S2> IEnumerator2, IEnumerator<S3> IEnumerator3, IEnumerator<S4> IEnumerator4)
        {
            SetSource1(IEnumerator1);
            SetSource2(IEnumerator2);
            SetSource3(IEnumerator3);
            SetSource4(IEnumerator4);
        }

        #endregion

        #region AbstractPipe(IEnumerable1, IEnumerable2, IEnumerable3, IEnumerable4)

        /// <summary>
        /// Creates a new abstract pipe using the elements emitted
        /// by the given IEnumerables as input.
        /// </summary>
        /// <param name="IEnumerable1">An IEnumerable&lt;S1&gt; as element source.</param>
        /// <param name="IEnumerable2">An IEnumerable&lt;S2&gt; as element source.</param>
        /// <param name="IEnumerable3">An IEnumerable&lt;S3&gt; as element source.</param>
        /// <param name="IEnumerable4">An IEnumerable&lt;S4&gt; as element source.</param>
        public AbstractPipe(IEnumerable<S1> IEnumerable1, IEnumerable<S2> IEnumerable2, IEnumerable<S3> IEnumerable3, IEnumerable<S4> IEnumerable4)
        {
            SetSourceCollection1(IEnumerable1);
            SetSourceCollection2(IEnumerable2);
            SetSourceCollection3(IEnumerable3);
            SetSourceCollection4(IEnumerable4);
        }

        #endregion
		
		#endregion


        #region SetSource(SourceElement)

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        public virtual void SetSource(Object SourceElement)
        {
            SetSource1((S1) SourceElement);
        }

        #endregion

        #region SetSource1(SourceElement)

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        public virtual void SetSource1(S1 SourceElement)
        {
            _InternalEnumerator1 = new HistoryEnumerator<S1>(new List<S1>() { SourceElement }.GetEnumerator());
        }

        #endregion

        #region SetSource2(SourceElement)

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        public virtual void SetSource2(S2 SourceElement)
        {
            _InternalEnumerator2 = new HistoryEnumerator<S2>(new List<S2>() { SourceElement }.GetEnumerator());
        }

        #endregion

        #region SetSource3(SourceElement)

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        public virtual void SetSource3(S3 SourceElement)
        {
            _InternalEnumerator3 = new HistoryEnumerator<S3>(new List<S3>() { SourceElement }.GetEnumerator());
        }

        #endregion

        #region SetSource4(SourceElement)

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        public virtual void SetSource4(S4 SourceElement)
        {
            _InternalEnumerator4 = new HistoryEnumerator<S4>(new List<S4>() { SourceElement }.GetEnumerator());
        }

        #endregion


        #region SetSource(IEnumerator)

        /// <summary>
        /// Set the elements emitted by the given IEnumerator as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator as element source.</param>
        public virtual void SetSource(IEnumerator IEnumerator)
        {
            SetSource1((IEnumerator<S1>) IEnumerator);
        }

        #endregion

        #region SetSource1(IEnumerator)

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S1&gt; as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator&lt;S1&gt; as element source.</param>
        public virtual void SetSource1(IEnumerator<S1> IEnumerator)
        {

            if (IEnumerator == null)
                throw new ArgumentNullException("IEnumerator must not be null!");

            if (IEnumerator is IEndPipe<S1>)
                _InternalEnumerator1 = IEnumerator;
            else
                _InternalEnumerator1 = new HistoryEnumerator<S1>(IEnumerator);

        }

        #endregion

        #region SetSource2(IEnumerator)

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S2&gt; as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator&lt;S2&gt; as element source.</param>
        public virtual void SetSource2(IEnumerator<S2> IEnumerator)
        {

            if (IEnumerator == null)
                throw new ArgumentNullException("IEnumerator must not be null!");

            if (IEnumerator is IEndPipe<S2>)
                _InternalEnumerator2 = IEnumerator;
            else
                _InternalEnumerator2 = new HistoryEnumerator<S2>(IEnumerator);

        }

        #endregion

        #region SetSource3(IEnumerator)

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S3&gt; as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator&lt;S3&gt; as element source.</param>
        public virtual void SetSource3(IEnumerator<S3> IEnumerator)
        {

            if (IEnumerator == null)
                throw new ArgumentNullException("IEnumerator must not be null!");

            if (IEnumerator is IEndPipe<S3>)
                _InternalEnumerator3 = IEnumerator;
            else
                _InternalEnumerator3 = new HistoryEnumerator<S3>(IEnumerator);

        }

        #endregion

        #region SetSource4(IEnumerator)

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S4&gt; as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator&lt;S4&gt; as element source.</param>
        public virtual void SetSource4(IEnumerator<S4> IEnumerator)
        {

            if (IEnumerator == null)
                throw new ArgumentNullException("IEnumerator must not be null!");

            if (IEnumerator is IEndPipe<S4>)
                _InternalEnumerator4 = IEnumerator;
            else
                _InternalEnumerator4 = new HistoryEnumerator<S4>(IEnumerator);

        }

        #endregion


        #region SetSourceCollection(IEnumerable)

        /// <summary>
        /// Set the elements emitted from the given IEnumerable as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable as element source.</param>
        public virtual void SetSourceCollection(IEnumerable IEnumerable)
        {
            SetSourceCollection1((IEnumerable<S1>) IEnumerable);
        }

        #endregion
        
        #region SetSourceCollection1(IEnumerable)

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S1&gt; as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable&lt;S1&gt; as element source.</param>
        public virtual void SetSourceCollection1(IEnumerable<S1> IEnumerable)
		{

            if (IEnumerable == null)
                throw new ArgumentNullException("IEnumerable must not be null!");

	        SetSource1(IEnumerable.GetEnumerator());

	    }

        #endregion

        #region SetSourceCollection2(IEnumerable)

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S2&gt; as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable&lt;S2&gt; as element source.</param>
        public virtual void SetSourceCollection2(IEnumerable<S2> IEnumerable)
        {

            if (IEnumerable == null)
                throw new ArgumentNullException("IEnumerable must not be null!");

            SetSource2(IEnumerable.GetEnumerator());

        }

        #endregion

        #region SetSourceCollection3(IEnumerable)

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S3&gt; as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable&lt;S3&gt; as element source.</param>
        public virtual void SetSourceCollection3(IEnumerable<S3> IEnumerable)
        {

            if (IEnumerable == null)
                throw new ArgumentNullException("IEnumerable must not be null!");

            SetSource3(IEnumerable.GetEnumerator());

        }

        #endregion

        #region SetSourceCollection4(IEnumerable)

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S4&gt; as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable&lt;S4&gt; as element source.</param>
        public virtual void SetSourceCollection4(IEnumerable<S4> IEnumerable)
        {

            if (IEnumerable == null)
                throw new ArgumentNullException("IEnumerable must not be null!");

            SetSource4(IEnumerable.GetEnumerator());

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
        public E Current
		{
			get
			{
                return _CurrentElement;
			}
		}

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
		Object System.Collections.IEnumerator.Current
		{	
			get
			{
                return _CurrentElement;
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
        public abstract Boolean MoveNext();

        #endregion

        #region Reset()

        /// <summary>
        /// Sets the enumerators to their initial positions, which
        /// is before the first element in the collections.
        /// </summary>
        public virtual void Reset()
		{
            _InternalEnumerator1.Reset();
            _InternalEnumerator2.Reset();
            _InternalEnumerator3.Reset();
            _InternalEnumerator4.Reset();
		}

        #endregion

        #region Dispose()

        /// <summary>
        /// Disposes this pipe.
        /// </summary>
        public virtual void Dispose()
		{
            _InternalEnumerator1.Dispose();
            _InternalEnumerator2.Dispose();
            _InternalEnumerator3.Dispose();
            _InternalEnumerator4.Dispose();
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

                var _PathElements = PathToHere;
                var _Size         = _PathElements.Count;

                // do not repeat filters as they dup the object
                // todo: why is size == 0 required (Pangloss?)	        
                if (_Size == 0 || !_PathElements[_Size - 1].Equals(_CurrentElement))
                    _PathElements.Add(_CurrentElement);

                return _PathElements;

            }

        }

        #endregion

        #region PathToHere

        private List<Object> PathToHere
		{

            get
            {

                throw new NotImplementedException();

                //if (_InternalEnumerator is IPipe)
                //    return ((IPipe) _InternalEnumerator).Path;

                //else if (_InternalEnumerator is IHistoryEnumerator)
                //{

                //    var _List = new List<Object>();
                //    var _Last = ((IHistoryEnumerator) _InternalEnumerator).Last;

                //    if (_Last == null)
                //        _List.Add(_InternalEnumerator.Current);
                //    else
                //        _List.Add(_Last);

                //    return _List;

                //}

                //else if (_InternalEnumerator is ISingleEnumerator)
                //    return new List<Object>() { ((ISingleEnumerator) _InternalEnumerator).Current };

                //else
                //    return new List<Object>();

            }

		}

        #endregion


        #region ToString()

        /// <summary>
        /// A string representation of this pipe.
        /// </summary>
        public override String ToString()
        {
            return this.GetType().Name;
        }

        #endregion


    }

    #endregion

    #region AbstractPipe<S1, S2, S3, S4, S5, E>

    /// <summary>
    /// An AbstractPipe provides most of the functionality that is repeated
    /// in every instance of a Pipe. Any subclass of AbstractPipe should simply
    /// implement MoveNext().
    /// </summary>
    /// <typeparam name="S1">The type of the first consuming objects.</typeparam>
    /// <typeparam name="S2">The type of the second consuming objects.</typeparam>
    /// <typeparam name="S3">The type of the third consuming objects.</typeparam>
    /// <typeparam name="S4">The type of the fourth consuming objects.</typeparam>
    /// <typeparam name="S5">The type of the fifth consuming objects.</typeparam>
    /// <typeparam name="E">The type of the emitting objects.</typeparam>
    public abstract class AbstractPipe<S1, S2, S3, S4, S5, E> : IPipe<S1, S2, S3, S4, S5, E>
    {

        #region Data

        /// <summary>
        /// The internal enumerator of the first collection.
        /// </summary>
        protected IEnumerator<S1> _InternalEnumerator1;

        /// <summary>
        /// The internal enumerator of the second collection.
        /// </summary>
        protected IEnumerator<S2> _InternalEnumerator2;

        /// <summary>
        /// The internal enumerator of the third collection.
        /// </summary>
        protected IEnumerator<S3> _InternalEnumerator3;

        /// <summary>
        /// The internal enumerator of the third collection.
        /// </summary>
        protected IEnumerator<S4> _InternalEnumerator4;

        /// <summary>
        /// The internal enumerator of the third collection.
        /// </summary>
        protected IEnumerator<S5> _InternalEnumerator5;


        /// <summary>
        /// The internal current element in the collection.
        /// </summary>
        protected E _CurrentElement;

        #endregion

        #region Constructor(s)

        #region AbstractPipe()

        /// <summary>
        /// Creates a new abstract pipe.
        /// </summary>
        public AbstractPipe()
        { }

        #endregion

        #region AbstractPipe(IEnumerator1, IEnumerator2, IEnumerator3, IEnumerator4, IEnumerator5)

        /// <summary>
        /// Creates a new abstract pipe using the elements emitted
        /// by the given IEnumerators as input.
        /// </summary>
        /// <param name="IEnumerator1">An IEnumerator&lt;S1&gt; as element source.</param>
        /// <param name="IEnumerator2">An IEnumerator&lt;S2&gt; as element source.</param>
        /// <param name="IEnumerator3">An IEnumerator&lt;S3&gt; as element source.</param>
        /// <param name="IEnumerator4">An IEnumerator&lt;S4&gt; as element source.</param>
        /// <param name="IEnumerator5">An IEnumerator&lt;S5&gt; as element source.</param>
        public AbstractPipe(IEnumerator<S1> IEnumerator1,
                            IEnumerator<S2> IEnumerator2,
                            IEnumerator<S3> IEnumerator3,
                            IEnumerator<S4> IEnumerator4,
                            IEnumerator<S5> IEnumerator5)
        {
            SetSource1(IEnumerator1);
            SetSource2(IEnumerator2);
            SetSource3(IEnumerator3);
            SetSource4(IEnumerator4);
            SetSource5(IEnumerator5);
        }

        #endregion

        #region AbstractPipe(IEnumerable1, IEnumerable2, IEnumerable3, IEnumerable4, IEnumerable5)

        /// <summary>
        /// Creates a new abstract pipe using the elements emitted
        /// by the given IEnumerables as input.
        /// </summary>
        /// <param name="IEnumerable1">An IEnumerable&lt;S1&gt; as element source.</param>
        /// <param name="IEnumerable2">An IEnumerable&lt;S2&gt; as element source.</param>
        /// <param name="IEnumerable3">An IEnumerable&lt;S3&gt; as element source.</param>
        /// <param name="IEnumerable4">An IEnumerable&lt;S4&gt; as element source.</param>
        /// <param name="IEnumerable5">An IEnumerable&lt;S5&gt; as element source.</param>
        public AbstractPipe(IEnumerable<S1> IEnumerable1,
                            IEnumerable<S2> IEnumerable2,
                            IEnumerable<S3> IEnumerable3,
                            IEnumerable<S4> IEnumerable4,
                            IEnumerable<S5> IEnumerable5)
        {
            SetSourceCollection1(IEnumerable1);
            SetSourceCollection2(IEnumerable2);
            SetSourceCollection3(IEnumerable3);
            SetSourceCollection4(IEnumerable4);
            SetSourceCollection5(IEnumerable5);
        }

        #endregion

        #endregion


        #region SetSource(SourceElement)

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        public virtual void SetSource(Object SourceElement)
        {
            SetSource1((S1)SourceElement);
        }

        #endregion

        #region SetSource1(SourceElement)

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        public virtual void SetSource1(S1 SourceElement)
        {
            _InternalEnumerator1 = new HistoryEnumerator<S1>(new List<S1>() { SourceElement }.GetEnumerator());
        }

        #endregion

        #region SetSource2(SourceElement)

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        public virtual void SetSource2(S2 SourceElement)
        {
            _InternalEnumerator2 = new HistoryEnumerator<S2>(new List<S2>() { SourceElement }.GetEnumerator());
        }

        #endregion

        #region SetSource3(SourceElement)

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        public virtual void SetSource3(S3 SourceElement)
        {
            _InternalEnumerator3 = new HistoryEnumerator<S3>(new List<S3>() { SourceElement }.GetEnumerator());
        }

        #endregion

        #region SetSource4(SourceElement)

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        public virtual void SetSource4(S4 SourceElement)
        {
            _InternalEnumerator4 = new HistoryEnumerator<S4>(new List<S4>() { SourceElement }.GetEnumerator());
        }

        #endregion

        #region SetSource5(SourceElement)

        /// <summary>
        /// Set the given element as source.
        /// </summary>
        /// <param name="SourceElement">A single source element.</param>
        public virtual void SetSource5(S5 SourceElement)
        {
            _InternalEnumerator5 = new HistoryEnumerator<S5>(new List<S5>() { SourceElement }.GetEnumerator());
        }

        #endregion


        #region SetSource(IEnumerator)

        /// <summary>
        /// Set the elements emitted by the given IEnumerator as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator as element source.</param>
        public virtual void SetSource(IEnumerator IEnumerator)
        {
            SetSource1((IEnumerator<S1>)IEnumerator);
        }

        #endregion

        #region SetSource1(IEnumerator)

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S1&gt; as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator&lt;S1&gt; as element source.</param>
        public virtual void SetSource1(IEnumerator<S1> IEnumerator)
        {

            if (IEnumerator == null)
                throw new ArgumentNullException("IEnumerator must not be null!");

            if (IEnumerator is IEndPipe<S1>)
                _InternalEnumerator1 = IEnumerator;
            else
                _InternalEnumerator1 = new HistoryEnumerator<S1>(IEnumerator);

        }

        #endregion

        #region SetSource2(IEnumerator)

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S2&gt; as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator&lt;S2&gt; as element source.</param>
        public virtual void SetSource2(IEnumerator<S2> IEnumerator)
        {

            if (IEnumerator == null)
                throw new ArgumentNullException("IEnumerator must not be null!");

            if (IEnumerator is IEndPipe<S2>)
                _InternalEnumerator2 = IEnumerator;
            else
                _InternalEnumerator2 = new HistoryEnumerator<S2>(IEnumerator);

        }

        #endregion

        #region SetSource3(IEnumerator)

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S3&gt; as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator&lt;S3&gt; as element source.</param>
        public virtual void SetSource3(IEnumerator<S3> IEnumerator)
        {

            if (IEnumerator == null)
                throw new ArgumentNullException("IEnumerator must not be null!");

            if (IEnumerator is IEndPipe<S3>)
                _InternalEnumerator3 = IEnumerator;
            else
                _InternalEnumerator3 = new HistoryEnumerator<S3>(IEnumerator);

        }

        #endregion

        #region SetSource4(IEnumerator)

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S4&gt; as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator&lt;S4&gt; as element source.</param>
        public virtual void SetSource4(IEnumerator<S4> IEnumerator)
        {

            if (IEnumerator == null)
                throw new ArgumentNullException("IEnumerator must not be null!");

            if (IEnumerator is IEndPipe<S4>)
                _InternalEnumerator4 = IEnumerator;
            else
                _InternalEnumerator4 = new HistoryEnumerator<S4>(IEnumerator);

        }

        #endregion

        #region SetSource5(IEnumerator)

        /// <summary>
        /// Set the elements emitted by the given IEnumerator&lt;S5&gt; as input.
        /// </summary>
        /// <param name="IEnumerator">An IEnumerator&lt;S5&gt; as element source.</param>
        public virtual void SetSource5(IEnumerator<S5> IEnumerator)
        {

            if (IEnumerator == null)
                throw new ArgumentNullException("IEnumerator must not be null!");

            if (IEnumerator is IEndPipe<S5>)
                _InternalEnumerator5 = IEnumerator;
            else
                _InternalEnumerator5 = new HistoryEnumerator<S5>(IEnumerator);

        }

        #endregion


        #region SetSourceCollection(IEnumerable)

        /// <summary>
        /// Set the elements emitted from the given IEnumerable as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable as element source.</param>
        public virtual void SetSourceCollection(IEnumerable IEnumerable)
        {
            SetSourceCollection1((IEnumerable<S1>)IEnumerable);
        }

        #endregion

        #region SetSourceCollection1(IEnumerable)

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S1&gt; as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable&lt;S1&gt; as element source.</param>
        public virtual void SetSourceCollection1(IEnumerable<S1> IEnumerable)
        {

            if (IEnumerable == null)
                throw new ArgumentNullException("IEnumerable must not be null!");

            SetSource1(IEnumerable.GetEnumerator());

        }

        #endregion

        #region SetSourceCollection2(IEnumerable)

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S2&gt; as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable&lt;S2&gt; as element source.</param>
        public virtual void SetSourceCollection2(IEnumerable<S2> IEnumerable)
        {

            if (IEnumerable == null)
                throw new ArgumentNullException("IEnumerable must not be null!");

            SetSource2(IEnumerable.GetEnumerator());

        }

        #endregion

        #region SetSourceCollection3(IEnumerable)

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S3&gt; as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable&lt;S3&gt; as element source.</param>
        public virtual void SetSourceCollection3(IEnumerable<S3> IEnumerable)
        {

            if (IEnumerable == null)
                throw new ArgumentNullException("IEnumerable must not be null!");

            SetSource3(IEnumerable.GetEnumerator());

        }

        #endregion

        #region SetSourceCollection4(IEnumerable)

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S4&gt; as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable&lt;S4&gt; as element source.</param>
        public virtual void SetSourceCollection4(IEnumerable<S4> IEnumerable)
        {

            if (IEnumerable == null)
                throw new ArgumentNullException("IEnumerable must not be null!");

            SetSource4(IEnumerable.GetEnumerator());

        }

        #endregion

        #region SetSourceCollection5(IEnumerable)

        /// <summary>
        /// Set the elements emitted from the given IEnumerable&lt;S5&gt; as input.
        /// </summary>
        /// <param name="IEnumerable">An IEnumerable&lt;S5&gt; as element source.</param>
        public virtual void SetSourceCollection5(IEnumerable<S5> IEnumerable)
        {

            if (IEnumerable == null)
                throw new ArgumentNullException("IEnumerable must not be null!");

            SetSource5(IEnumerable.GetEnumerator());

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
        public E Current
        {
            get
            {
                return _CurrentElement;
            }
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        Object System.Collections.IEnumerator.Current
        {
            get
            {
                return _CurrentElement;
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
        public abstract Boolean MoveNext();

        #endregion

        #region Reset()

        /// <summary>
        /// Sets the enumerators to their initial positions, which
        /// is before the first element in the collections.
        /// </summary>
        public virtual void Reset()
        {
            _InternalEnumerator1.Reset();
            _InternalEnumerator2.Reset();
            _InternalEnumerator3.Reset();
            _InternalEnumerator4.Reset();
            _InternalEnumerator5.Reset();
        }

        #endregion

        #region Dispose()

        /// <summary>
        /// Disposes this pipe.
        /// </summary>
        public virtual void Dispose()
        {
            _InternalEnumerator1.Dispose();
            _InternalEnumerator2.Dispose();
            _InternalEnumerator3.Dispose();
            _InternalEnumerator4.Dispose();
            _InternalEnumerator5.Dispose();
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

                var _PathElements = PathToHere;
                var _Size = _PathElements.Count;

                // do not repeat filters as they dup the object
                // todo: why is size == 0 required (Pangloss?)	        
                if (_Size == 0 || !_PathElements[_Size - 1].Equals(_CurrentElement))
                    _PathElements.Add(_CurrentElement);

                return _PathElements;

            }

        }

        #endregion

        #region PathToHere

        private List<Object> PathToHere
        {

            get
            {

                throw new NotImplementedException();

                //if (_InternalEnumerator is IPipe)
                //    return ((IPipe) _InternalEnumerator).Path;

                //else if (_InternalEnumerator is IHistoryEnumerator)
                //{

                //    var _List = new List<Object>();
                //    var _Last = ((IHistoryEnumerator) _InternalEnumerator).Last;

                //    if (_Last == null)
                //        _List.Add(_InternalEnumerator.Current);
                //    else
                //        _List.Add(_Last);

                //    return _List;

                //}

                //else if (_InternalEnumerator is ISingleEnumerator)
                //    return new List<Object>() { ((ISingleEnumerator) _InternalEnumerator).Current };

                //else
                //    return new List<Object>();

            }

        }

        #endregion


        #region ToString()

        /// <summary>
        /// A string representation of this pipe.
        /// </summary>
        public override String ToString()
        {
            return this.GetType().Name;
        }

        #endregion

    }

    #endregion

}
