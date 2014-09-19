/*
 * Copyright (c) 2010-2014 Achim 'ahzf' Friedland <achim@graphdefined.org>
 * This file is part of Alviss <http://www.github.com/Vanaheimr/Alviss>
 *
 * Licensed under the General Public License, Version 3.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.gnu.org/licenses/gpl.html
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
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;

using org.GraphDefined.Vanaheimr.Illias.Transactions;

#endregion

namespace org.GraphDefined.Vanaheimr.Alviss
{

    /// <summary>
    /// A QuadStore stores little fragments of a graph called quads.
    /// Subject -Predicate-> Object [Context]
    /// Vertex  -Edge->      Vertex [HyperEdge]
    /// </summary>
    /// <typeparam name="TSystemId">The type of the SystemId.</typeparam>
    /// <typeparam name="TQuadId">The type of the QuadId.</typeparam>
    /// <typeparam name="TTransactionId">The type of the transaction id.</typeparam>
    /// <typeparam name="TSPOC">The type of the subject, predicate, object and context.</typeparam>
    public class QuadStore<TSystemId, TQuadId, TTransactionId, TSPOC>
                     : IQuadStore<TSystemId, TQuadId, TTransactionId, TSPOC>
        
        where TSystemId      : IEquatable<TSystemId>,      IComparable<TSystemId>,      IComparable
        where TQuadId        : IEquatable<TQuadId>,        IComparable<TQuadId>,        IComparable
        where TTransactionId : IEquatable<TTransactionId>, IComparable<TTransactionId>, IComparable
        where TSPOC          : IEquatable<TSPOC>,          IComparable<TSPOC>,          IComparable

    {

        #region Data

        #region System related

        private Int64                            _CurrentQuadId;
        private QuadIdConverterDelegate<TQuadId> _QuadIdConverter;
        private DefaultContextDelegate<TSPOC>    _DefaultContext;

        #endregion

        #region Indices for the Subject, Predicate, Object and Context

        // Maybe look for a better data structure in the future.
        private ConcurrentDictionary<TQuadId,      IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>  _QuadIdIndex;
        private ConcurrentDictionary<TSPOC,   List<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>> _SubjectIndex;
        private ConcurrentDictionary<TSPOC,   List<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>> _PredicateIndex;
        private ConcurrentDictionary<TSPOC,   List<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>> _ObjectIndex;
        private ConcurrentDictionary<TSPOC,   List<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>> _ContextIndex;

        #endregion

        #region Transactions

        private static ThreadLocal<Transaction<TTransactionId, TSystemId, IQuadStore<TSystemId, TQuadId, TTransactionId, TSPOC>>> _ThreadLocalTransaction;

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// The identification of this QuadStore.
        /// </summary>
        public TSystemId SystemId { get; private set; }

        #endregion

        #region Constructor(s)

        #region QuadStore(SystemId, QuadIdConverter, DefaultContext)

        /// <summary>
        /// Creates a new QuadStore storing little fragments of a graph called quads.
        /// Subject -Predicate-> Object [Context]
        /// Vertex  -Edge->      Vertex [HyperEdge]
        /// </summary>
        /// <param name="SystemId">The SystemId for this QuadStore.</param>
        /// <param name="QuadIdConverter">A delegate to convert a QuadId from the internal Int64 representation to the actual type T of a quad.</param>
        /// <param name="DefaultContext">The default context of a quad if none was given.</param>
        public QuadStore(TSystemId SystemId, QuadIdConverterDelegate<TQuadId> QuadIdConverter, DefaultContextDelegate<TSPOC> DefaultContext)
        {

            #region Initial checks

            if (SystemId == null || SystemId.Equals(default(TSystemId)))
                throw new ArgumentNullException("The SystemId must not be null or default(TSystemId)!");

            if (QuadIdConverter == null)
                throw new ArgumentNullException("The QuadIdConverter must not be null!");

            if (DefaultContext == null)
                throw new ArgumentNullException("The DefaultContext must not be null!");

            #endregion

            this.SystemId         = SystemId;
            this._QuadIdConverter = QuadIdConverter;
            this._DefaultContext  = DefaultContext;

            this._QuadIdIndex     = new ConcurrentDictionary<TQuadId,      IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>> ();
            this._SubjectIndex    = new ConcurrentDictionary<TSPOC,   List<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>>();
            this._PredicateIndex  = new ConcurrentDictionary<TSPOC,   List<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>>();
            this._ObjectIndex     = new ConcurrentDictionary<TSPOC,   List<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>>();
            this._ContextIndex    = new ConcurrentDictionary<TSPOC,   List<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>>();

        }

        #endregion

        #endregion


        #region BeginTransaction(...)

        /// <summary>
        /// Start a new transaction.
        /// </summary>
        /// <param name="Name">A name or identification for this transaction.</param>
        /// <param name="Distributed">Indicates that this transaction should synched within the entire cluster.</param>
        /// <param name="LongRunning">Indicates that this transaction is a long-running transaction.</param>
        /// <param name="IsolationLevel">The isolation level of this transaction.</param>
        /// <param name="CreationTime">The timestamp when this transaction started.</param>
        /// <param name="InvalidationTime">The timestamp when this transaction will be invalid.</param>
        /// <returns>A new transaction object.</returns>
        public Transaction<TTransactionId, TSystemId, IQuadStore<TSystemId, TQuadId, TTransactionId, TSPOC>>
            BeginTransaction(String         Name             = "", 
                             Boolean        Distributed      = false,
                             Boolean        LongRunning      = false,
                             IsolationLevel IsolationLevel   = IsolationLevel.Write,
                             DateTime?      CreationTime     = null,
                             DateTime?      InvalidationTime = null)
        {

            if (_ThreadLocalTransaction != null)
                if (_ThreadLocalTransaction.IsValueCreated)
                {
                    if (_ThreadLocalTransaction.Value.State == TransactionState.Running ||
                        _ThreadLocalTransaction.Value.State == TransactionState.NestedTransaction ||
                        _ThreadLocalTransaction.Value.State == TransactionState.Committing ||
                        _ThreadLocalTransaction.Value.State == TransactionState.RollingBack)
                    {
                        throw new CouldNotBeginTransactionException<TTransactionId, TSystemId, IQuadStore<TSystemId, TQuadId, TTransactionId, TSPOC>>(_ThreadLocalTransaction.Value,
                                                                                               Message: "Transaction still in state '" + _ThreadLocalTransaction.Value.State.ToString() +
                                                                                                        "' on Thread " + Thread.CurrentThread.ManagedThreadId + "!");
                    }
                }

            _ThreadLocalTransaction = new ThreadLocal<Transaction<TTransactionId, TSystemId, IQuadStore<TSystemId, TQuadId, TTransactionId, TSPOC>>>(
                () => new Transaction<TTransactionId, TSystemId, IQuadStore<TSystemId, TQuadId, TTransactionId, TSPOC>>(
                    default(TTransactionId),
                    SystemId,
                    Name,
                    Distributed,
                    LongRunning,
                    IsolationLevel,
                    CreationTime,
                    InvalidationTime));

            return _ThreadLocalTransaction.Value;

        }

        #endregion


        #region Add(...)

        #region (private) Add(NewQuad, Connect = true)

        /// <summary>
        /// Adds the given quad to the QuadStore.
        /// </summary>
        /// <param name="NewQuad">The quad to add to the store.</param>
        /// <param name="Connect">Connect this quad to other quads in order to achieve an index-free adjacency.</param>
        /// <returns>The given quad.</returns>
        private IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>
            Add(IQuad<TSystemId, TQuadId, TTransactionId, TSPOC> NewQuad,
                Boolean Connect = true)
        {

            #region Initial checks

            if (NewQuad == null)
                throw new ArgumentNullException("The new quad must not be null!");

            #endregion

            List<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>> _QuadList = null;

            #region Add to QuadId index

            if (_QuadIdIndex.ContainsKey(NewQuad.QuadId) || !_QuadIdIndex.TryAdd(NewQuad.QuadId, NewQuad))
                throw new AddToQuadIdIndexException<TSystemId, TQuadId, TTransactionId, TSPOC>(NewQuad);

            #endregion

            #region Add to Subject index

            if (_SubjectIndex.TryGetValue(NewQuad.Subject, out _QuadList))
                _QuadList.Add(NewQuad);
            else
                if (!_SubjectIndex.TryAdd(NewQuad.Subject, new List<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>() { NewQuad }))
                    throw new AddToSubjectIndexException<TSystemId, TQuadId, TTransactionId, TSPOC>(NewQuad);

            #endregion

            #region Add to Predicate index

            if (_PredicateIndex.TryGetValue(NewQuad.Predicate, out _QuadList))
                _QuadList.Add(NewQuad);
            else
                if (!_PredicateIndex.TryAdd(NewQuad.Predicate, new List<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>() { NewQuad }))
                    throw new AddToPredicateIndexException<TSystemId, TQuadId, TTransactionId, TSPOC>(NewQuad);

            #endregion

            #region Add to Object index

            if (_ObjectIndex.TryGetValue(NewQuad.Object, out _QuadList))
                _QuadList.Add(NewQuad);
            else
                if (!_ObjectIndex.TryAdd(NewQuad.Object, new List<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>() { NewQuad }))
                    throw new AddToObjectIndexException<TSystemId, TQuadId, TTransactionId, TSPOC>(NewQuad);

            #endregion

            #region Add to Context index

            if (_ContextIndex.TryGetValue(NewQuad.Context, out _QuadList))
                _QuadList.Add(NewQuad);
            else
                if (!_ContextIndex.TryAdd(NewQuad.Context, new List<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>() { NewQuad }))
                    throw new AddToContextIndexException<TSystemId, TQuadId, TTransactionId, TSPOC>(NewQuad);

            #endregion

            #region Connect this quad to other quads in order to achieve an index-free adjacency

            if (Connect)
                UpdateReferences(NewQuad);

            #endregion

            return NewQuad;

        }

        #endregion

        #region Add(Subject, Predicate, Object, Context = default(TSPOC), Connect = true)

        /// <summary>
        /// Adds a new quad based on the given parameters to the QuadStore.
        /// </summary>
        /// <param name="Subject">The subject.</param>
        /// <param name="Predicate">The predicate.</param>
        /// <param name="Object">The object.</param>
        /// <param name="Context">The context.</param>
        /// <param name="Connect">Connect this quad to other quads in order to achieve an index-free adjacency.</param>
        /// <returns>A new quad based on the given parameters.</returns>
        public IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>
                   Add(TSPOC   Subject,
                       TSPOC   Predicate,
                       TSPOC   Object,
                       TSPOC   Context = default(TSPOC),
                       Boolean Connect = true)
        {

            #region Initial checks

            if (Subject   == null || Subject.Equals(default(TSPOC)))
                throw new ArgumentNullException("The subject must not be null or default(T)!");

            if (Predicate == null || Predicate.Equals(default(TSPOC)))
                throw new ArgumentNullException("The predicate must not be null or default(T)!");

            if (Object    == null || Object.Equals(default(TSPOC)))
                throw new ArgumentNullException("The object must not be null or default(T)!");

            if (Context   == null || Context.Equals(default(TSPOC)))
                Context = _DefaultContext();

            #endregion

            // Calculate a new QuadId.
            Interlocked.Increment(ref _CurrentQuadId);

            // Create a new quad...
            var _Quad = new Quad<TSystemId, TQuadId, TTransactionId, TSPOC>
                                 (SystemId:      SystemId,
                                  TransactionId: default(TTransactionId),
                                  QuadId:        _QuadIdConverter(_CurrentQuadId),
                                  Subject:       Subject,
                                  Predicate:     Predicate,
                                  Object:        Object,
                                  Context:       Context);

            // ...and add it to the store.
            return Add(_Quad, Connect);

        }

        #endregion

        #endregion

        #region NumberOf(...)

        #region NumberOfQuads

        /// <summary>
        /// Return the number of quads.
        /// </summary>
        public UInt64 NumberOfQuads
        {
            get
            {
                return (UInt64) _QuadIdIndex.Count;
            }
        }

        #endregion

        #region NumberOfSubjects

        /// <summary>
        /// Return the number of unique subjects.
        /// </summary>
        public UInt64 NumberOfSubjects
        {
            get
            {
                return (UInt64) _SubjectIndex.Count;
            }
        }

        #endregion

        #region NumberOfPredicates

        /// <summary>
        /// Return the number of unique predicates.
        /// </summary>
        public UInt64 NumberOfPredicates
        {
            get
            {
                return (UInt64) _PredicateIndex.Count;
            }
        }

        #endregion

        #region NumberOfObjects

        /// <summary>
        /// Return the number of unique objects.
        /// </summary>
        public UInt64 NumberOfObjects
        {
            get
            {
                return (UInt64) _ObjectIndex.Count;
            }
        }

        #endregion

        #region NumberOfContexts

        /// <summary>
        /// Return the number of unique contexts.
        /// </summary>
        public UInt64 NumberOfContexts
        {
            get
            {
                return (UInt64) _ContextIndex.Count;
            }
        }

        #endregion

        #endregion

        #region Get/Select(...)/Traverse

        #region GetQuad(QuadId)

        /// <summary>
        /// Returns the quad having the given QuadId.
        /// </summary>
        /// <param name="QuadId">The QuadId.</param>
        /// <returns>The quad having the given QuadId.</returns>
        public IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>
                   GetQuad(TQuadId QuadId)
        {

            IQuad<TSystemId, TQuadId, TTransactionId, TSPOC> _Quad = null;

            if (_QuadIdIndex.TryGetValue(QuadId, out _Quad))
                return _Quad;

            return null;

        }

        #endregion

        #region GetQuads(Subject, Predicate, Object, Context = default(T))

        /// <summary>
        /// Returns all matching quads based on the given parameters.
        /// </summary>
        /// <param name="Subject">The subject.</param>
        /// <param name="Predicate">The predicate.</param>
        /// <param name="Object">The object.</param>
        /// <param name="Context">The context.</param>
        /// <returns>An enumeration of quads matched by the given parameters.</returns>
        public IEnumerable<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>
                   GetQuads(TSPOC Subject   = default(TSPOC),
                            TSPOC Predicate = default(TSPOC),
                            TSPOC Object    = default(TSPOC),
                            TSPOC Context   = default(TSPOC))
        {

            // Using the QuadId idnex is not optimal by far... but working for the moment.
            foreach (var CurrentQuad in _QuadIdIndex.Values)
            {

                if (Subject   != null && !Subject.  Equals(default(TSPOC)))
                    if (!CurrentQuad.Subject.Equals(Subject))
                        continue;

                if (Predicate != null && !Predicate.Equals(default(TSPOC)))
                    if (!CurrentQuad.Predicate.Equals(Predicate))
                        continue;

                if (Object    != null && !Object.   Equals(default(TSPOC)))
                    if (!CurrentQuad.Object.Equals(Object))
                        continue;

                if (Context   != null && !Context.  Equals(default(TSPOC)))
                    if (!CurrentQuad.Context.Equals(Context))
                        continue;

                yield return CurrentQuad;

            }

        }

        #endregion

        #region SelectQuads(SubjectSelector = null, PredicateSelector = null, ObjectSelector = null, ContextSelector = null)

        /// <summary>
        /// Returns all matching quads based on the given selectors.
        /// </summary>
        /// <param name="SubjectSelector">A delegate for selecting subjects.</param>
        /// <param name="PredicateSelector">A delegate for selecting predicates.</param>
        /// <param name="ObjectSelector">A delegate for selecting objects.</param>
        /// <param name="ContextSelector">A delegate for selecting contexts.</param>
        /// <returns>An enumeration of quads matched by the given selectors.</returns>
        public IEnumerable<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>
                   SelectQuads(SubjectSelector<TSPOC>   SubjectSelector   = null,
                               PredicateSelector<TSPOC> PredicateSelector = null,
                               ObjectSelector<TSPOC>    ObjectSelector    = null,
                               ContextSelector<TSPOC>   ContextSelector   = null)
        {

            // Using the QuadId idnex is not optimal by far... but working for the moment.
            foreach (var CurrentQuad in _QuadIdIndex.Values)
            {

                if (SubjectSelector   != null && !SubjectSelector(CurrentQuad.Subject))
                    continue;

                if (PredicateSelector != null && !PredicateSelector(CurrentQuad.Predicate))
                    continue;

                if (ObjectSelector    != null && !ObjectSelector(CurrentQuad.Object))
                    continue;

                if (ContextSelector   != null && !ContextSelector(CurrentQuad.Context))
                    continue;

                yield return CurrentQuad;

            }

        }

        #endregion

        #region Traverse(Subject, Predicate, IncludeFirst = true)

        /// <summary>
        /// Traverses the graph of quads by following a single given predicate.
        /// </summary>
        /// <param name="Subject">The subject.</param>
        /// <param name="Predicate">The predicate to follow.</param>
        /// <param name="IncludeFirst">Include the subject as first element of the result set.</param>
        /// <returns>The quad having the given QuadId.</returns>
        public IEnumerable<TSPOC> Traverse(TSPOC   Subject, 
                                           TSPOC   Predicate,
                                           Boolean IncludeFirst = true)
        {

            if (IncludeFirst)
                yield return Subject;

            TSPOC CurrentSubject;

            var ToVisitStack  = new Stack<TSPOC>();
            foreach (var StartingQuad in GetQuads(Subject: Subject, Predicate: Predicate))
                ToVisitStack.Push(StartingQuad.Object);

            while (!(ToVisitStack.Count == 0))
            {

                CurrentSubject = ToVisitStack.Pop();

                foreach (var CurrentQuad in GetQuads(Subject: CurrentSubject, Predicate: Predicate))
                    ToVisitStack.Push(CurrentQuad.Object);

                yield return CurrentSubject;

            }

        }

        #endregion

        #endregion

        #region Remove(...)

        #region Remove(QuadId)

        /// <summary>
        /// Removes the quad having the given QuadId.
        /// </summary>
        /// <param name="QuadId">The QuadId.</param>
        /// <returns>The quad after removal having the given QuadId.</returns>
        public IQuad<TSystemId, TQuadId, TTransactionId, TSPOC> Remove(TQuadId QuadId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Remove(Subject, Predicate, Object, Context = default(T))

        /// <summary>
        /// Removes all matching quads based on the given parameters.
        /// </summary>
        /// <param name="Subject">The subject.</param>
        /// <param name="Predicate">The predicate.</param>
        /// <param name="Object">The object.</param>
        /// <param name="Context">The context.</param>
        /// <returns>All quads after removal matched by the given parameters.</returns>
        public IEnumerable<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>
                   Remove(TSPOC Subject   = default(TSPOC),
                          TSPOC Predicate = default(TSPOC),
                          TSPOC Object    = default(TSPOC),
                          TSPOC Context   = default(TSPOC))
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Remove(SubjectSelector = null, PredicateSelector = null, ObjectSelector = null, ContextSelector = null)

        /// <summary>
        /// Removes all matching quads based on the given selectors.
        /// </summary>
        /// <param name="SubjectSelector">A delegate for selcting subjects.</param>
        /// <param name="PredicateSelector">A delegate for selcting predicates.</param>
        /// <param name="ObjectSelector">A delegate for selcting objects.</param>
        /// <param name="ContextSelector">A delegate for selcting contexts.</param>
        /// <returns>All quads after removal matched by the given parameters.</returns>
        public IEnumerable<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>
                   Remove(SubjectSelector<TSPOC>   SubjectSelector   = null,
                          PredicateSelector<TSPOC> PredicateSelector = null,
                          ObjectSelector<TSPOC>    ObjectSelector    = null,
                          ContextSelector<TSPOC>   ContextSelector = null)

        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion


        #region UpdateReferences(NewQuad)

        /// <summary>
        /// Connect this quad to other quads in order to achieve an index-free adjacency.
        /// </summary>
        /// <param name="Quad">A quad to connect to its friends.</param>
        public void UpdateReferences(IQuad<TSystemId, TQuadId, TTransactionId, TSPOC> Quad)
        {

            List<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>> _QuadList = null;

            // Look for other quads having this Subject as an Object.
            // This means: Which quads want to be connected with me.
            //             Friends.Object -> me.Subject
            if (_ObjectIndex.TryGetValue(Quad.Subject, out _QuadList))
                foreach (var _Friends in _QuadList)
                    if (_Friends.ObjectReference == null)
                        _Friends.ObjectReference = new HashSet<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>() { Quad };
                    else
                        _Friends.ObjectReference.Add(Quad);

            // Look for other quads having this Object as a Subject.
            // This means: To which quads I want to be connected to.
            //             me.Object -> Friends.Subject
            if (_SubjectIndex.TryGetValue(Quad.Object, out _QuadList))
                foreach (var _Quad in _QuadList)
                    if (Quad.ObjectReference == null)
                        Quad.ObjectReference = new HashSet<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>>() { _Quad };
                    else
                        Quad.ObjectReference.Add(_Quad);

        }

        #endregion

    }

}
