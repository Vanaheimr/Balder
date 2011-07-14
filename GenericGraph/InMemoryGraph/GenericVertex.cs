/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET <http://www.github.com/ahzf/Blueprints.NET>
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

namespace de.ahzf.Blueprints.GenericGraph.InMemory
{

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    public class GenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>

                               : IGenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>

        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable
        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable
        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable

        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable
        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable
        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable

    {

        #region Data

        /// <summary>
        /// The edges emanating from, or leaving, this vertex.
        /// </summary>
        protected readonly HashSet<IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>> _OutEdges;

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex.
        /// </summary>
        protected readonly HashSet<IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>> _InEdges;

        #endregion

        #region Properties

        #region Id

        /// <summary>
        /// An identifier that is unique to its inheriting class.
        /// All vertices of a graph must have unique identifiers.
        /// All edges of a graph must have unique identifiers.
        /// </summary>
        public TIdVertex Id { get; private set; }

        #endregion

        public TRevisionIdVertex RevisionId { get; private set; }


        //public String Type
        //{
        //    get { throw new NotImplementedException(); }
        //}

        //public IEnumerable<String> Properties
        //{
        //    get { throw new NotImplementedException(); }
        //}



        public TDataVertex Data { get; private set; }

        #endregion

        #region Constructor(s)

        #region GeoVertex(myIGraph, myVertexId, myVertexInitializer = null)

        /// <summary>
        /// Creates a new vertex.
        /// </summary>
        /// <param name="myIGraph">The associated graph.</param>
        /// <param name="myVertexId">The identification of this vertex.</param>
        /// <param name="myVertexInitializer">A delegate to initialize the newly created vertex.</param>
        internal protected GenericVertex(IGenericGraph myIGraph, TIdVertex myVertexId, Action<IGenericVertex> myVertexInitializer = null)
        {
            
            _OutEdges = new HashSet<IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>>();
            _InEdges  = new HashSet<IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>>();

            Id = myVertexId;

            if (myVertexInitializer != null)
                myVertexInitializer(this);

        }

        #endregion

        #endregion


        #region OutEdges

        #region AddOutEdge(myIEdge)

        /// <summary>
        /// Add an outgoing edge.
        /// </summary>
        /// <param name="myIEdge">The edge to add.</param>
        public void AddOutEdge(IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myIEdge)
        {
            _OutEdges.Add(myIEdge);
        }

        #endregion

        #region OutEdges

        /// <summary>
        /// The edges emanating from, or leaving, this vertex.
        /// </summary>
        public IEnumerable<IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>> OutEdges
        {
            get
            {
                return _OutEdges;
            }
        }

        #endregion

        #region GetOutEdges(myLabel)

        /// <summary>
        /// The edges emanating from, or leaving, this vertex
        /// filtered by their label.
        /// </summary>
        public IEnumerable<IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>> GetOutEdges(String myLabel)
        {
            return from _Edge in _OutEdges where _Edge.Label == myLabel select _Edge;
        }

        #endregion

        #region RemoveOutEdge(myIEdge)

        /// <summary>
        /// Remove an outgoing edge.
        /// </summary>
        /// <param name="myIEdge">The edge to remove.</param>
        public void RemoveOutEdge(IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myIEdge)
        {
            lock (this)
            {
                _OutEdges.Remove(myIEdge);
            }
        }

        #endregion

        #endregion

        #region InEdges

        #region AddInEdge(myIEdge)

        /// <summary>
        /// Add an incoming edge.
        /// </summary>
        /// <param name="myIEdge">The edge to add.</param>
        public void AddInEdge(IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myIEdge)
        {
            _InEdges.Add(myIEdge);
        }

        #endregion

        #region InEdges

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex.
        /// </summary>
        public IEnumerable<IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>> InEdges
        {
            get
            {
                return _InEdges;
            }
        }

        #endregion

        #region GetInEdges(myLabel)

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex
        /// filtered by their label.
        /// </summary>
        public IEnumerable<IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>> GetInEdges(String myLabel)
        {
            return from _Edge in _InEdges where _Edge.Label == myLabel select _Edge;
        }

        #endregion

        #region RemoveInEdge(myIEdge)

        /// <summary>
        /// Remove an incoming edge.
        /// </summary>
        /// <param name="myIEdge">The edge to remove.</param>
        public void RemoveInEdge(IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myIEdge)
        {
            lock (this)
            {
                _InEdges.Remove(myIEdge);
            }
        }

        #endregion

        #endregion


        #region Operator overloading

        #region Operator == (myGenericVertex1, myGenericVertex2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myGenericVertex1">A GenericVertex.</param>
        /// <param name="myGenericVertex2">Another GenericVertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (GenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myGenericVertex1,
                                           GenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myGenericVertex2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(myGenericVertex1, myGenericVertex2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) myGenericVertex1 == null) || ((Object) myGenericVertex2 == null))
                return false;

            return myGenericVertex1.Equals(myGenericVertex2);

        }

        #endregion

        #region Operator != (myGenericVertex1, myGenericVertex2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myGenericVertex1">A GenericVertex.</param>
        /// <param name="myGenericVertex2">Another GenericVertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (GenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myGenericVertex1,
                                           GenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myGenericVertex2)
        {
            return !(myGenericVertex1 == myGenericVertex2);
        }

        #endregion

        #region Operator <  (myGenericVertex1, myGenericVertex2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myGenericVertex1">A GenericVertex.</param>
        /// <param name="myGenericVertex2">Another GenericVertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (GenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myGenericVertex1,
                                          GenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myGenericVertex2)
        {

            // Check if myGenericVertex1 is null
            if ((Object) myGenericVertex1 == null)
                throw new ArgumentNullException("Parameter myGenericVertex1 must not be null!");

            // Check if myGenericVertex2 is null
            if ((Object) myGenericVertex2 == null)
                throw new ArgumentNullException("Parameter myGenericVertex2 must not be null!");

            return myGenericVertex1.CompareTo(myGenericVertex2) < 0;

        }

        #endregion

        #region Operator >  (myGenericVertex1, myGenericVertex2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myGenericVertex1">A GenericVertex.</param>
        /// <param name="myGenericVertex2">Another GenericVertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (GenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myGenericVertex1,
                                          GenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myGenericVertex2)
        {

            // Check if myGenericVertex1 is null
            if ((Object) myGenericVertex1 == null)
                throw new ArgumentNullException("Parameter myGenericVertex1 must not be null!");

            // Check if myGenericVertex2 is null
            if ((Object) myGenericVertex2 == null)
                throw new ArgumentNullException("Parameter myGenericVertex2 must not be null!");

            return myGenericVertex1.CompareTo(myGenericVertex2) > 0;

        }

        #endregion

        #region Operator <= (myGenericVertex1, myGenericVertex2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myGenericVertex1">A GenericVertex.</param>
        /// <param name="myGenericVertex2">Another GenericVertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (GenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myGenericVertex1,
                                           GenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myGenericVertex2)
        {
            return !(myGenericVertex1 > myGenericVertex2);
        }

        #endregion

        #region Operator >= (myGenericVertex1, myGenericVertex2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myGenericVertex1">A GenericVertex.</param>
        /// <param name="myGenericVertex2">Another GenericVertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (GenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myGenericVertex1,
                                           GenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myGenericVertex2)
        {
            return !(myGenericVertex1 < myGenericVertex2);
        }

        #endregion

        #endregion

        #region IComparable<IGenericVertex> Members

        #region CompareTo(myObject)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myObject">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(Object myObject)
        {

            // Check if myObject is null
            if (myObject == null)
                throw new ArgumentNullException("myObject must not be null!");

            // Check if myObject can be casted to an IEdge object
            var myIGenericVertex = myObject as IGenericVertex;
            if ((Object) myIGenericVertex == null)
                throw new ArgumentException("myObject is not of type myIGenericVertex!");

            return CompareTo(myIGenericVertex);

        }

        #endregion

        #region CompareTo(myIGenericVertex)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myIGenericVertex">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(IGenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myIGenericVertex)
        {

            // Check if myIGenericVertex is null
            if (myIGenericVertex == null)
                throw new ArgumentNullException("myIGenericVertex must not be null!");

            return Id.CompareTo(myIGenericVertex.Id);

        }

        #endregion

        #region CompareTo(myIGenericVertex)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myIGenericVertex">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(TIdVertex myIGenericVertex)
        {

            // Check if myIGenericVertex is null
            if (myIGenericVertex == null)
                throw new ArgumentNullException("myIGenericVertex must not be null!");

            return Id.CompareTo(myIGenericVertex);

        }

        #endregion

        #endregion

        #region IEquatable<IGenericVertex> Members

        #region Equals(myObject)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myObject">An object to compare with.</param>
        /// <returns>true|false</returns>
        public override Boolean Equals(Object myObject)
        {

            if (myObject == null)
                return false;

            var _Object = myObject as IGenericVertex;
            if (_Object != null)
                return Equals(_Object);

            return false;

        }

        #endregion

        #region Equals(myIGenericVertex)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myIGenericVertex">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(IGenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> myIGenericVertex)
        {

            if ((Object)myIGenericVertex == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return (this.Id.Equals(myIGenericVertex.Id));

        }

        #endregion

        #region Equals(myIGenericVertex)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myIGenericVertex">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(TIdVertex myIGenericVertex)
        {

            if ((Object) myIGenericVertex == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return (this.Id.Equals(myIGenericVertex));

        }

        #endregion

        #endregion

        #region GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override Int32 GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion

    }

}
