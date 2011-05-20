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

#endregion

namespace de.ahzf.Blueprints.GenericGraph.InMemory
{

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    public class GenericEdge<TIdVertex,    TRevisionIdVertex,    TVertexData,
                             TIdEdge,      TRevisionIdEdge,      TEdgeData,
                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData>

                             : IGenericEdge<TIdVertex,    TRevisionIdVertex,    TVertexData,
                                            TIdEdge,      TRevisionIdEdge,      TEdgeData,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData>

        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable
        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable
        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable

        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable
        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable
        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable

    {

        #region Data

        /// <summary>
        /// The vertex at the tail of this edge.
        /// </summary>
        protected readonly IGenericVertex<TIdVertex,    TRevisionIdVertex,    TVertexData,
                                          TIdEdge,      TRevisionIdEdge,      TEdgeData,
                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData> _OutVertex;

        /// <summary>
        /// The vertex at the head of this edge.
        /// </summary>
        protected readonly IGenericVertex<TIdVertex,    TRevisionIdVertex,    TVertexData,
                                          TIdEdge,      TRevisionIdEdge,      TEdgeData,
                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData> _InVertex;

        #endregion

        #region Properties

        // Edge properties

        #region Id

        /// <summary>
        /// An identifier that is unique to its inheriting class.
        /// All vertices of a graph must have unique identifiers.
        /// All edges of a graph must have unique identifiers.
        /// </summary>
        public TIdEdge Id { get; private set; }

        #endregion

        public TRevisionIdEdge RevisionId { get; private set; }

        #region Label

        /// <summary>
        /// The label associated with this edge.
        /// </summary>
        public String Label { get; private set; }

        #endregion

        public TEdgeData Data { get; private set; }


        // Links to the associated vertices

        #region OutVertex

        /// <summary>
        /// The vertex at the tail of this edge.
        /// </summary>
        public IGenericVertex<TIdVertex,    TRevisionIdVertex,    TVertexData,
                              TIdEdge,      TRevisionIdEdge,      TEdgeData,
                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData> OutVertex
        {
            get
            {
                return _OutVertex;
            }
        }

        #endregion

        #region InVertex

        /// <summary>
        /// The vertex at the head of this edge.
        /// </summary>
        public IGenericVertex<TIdVertex,    TRevisionIdVertex,    TVertexData,
                              TIdEdge,      TRevisionIdEdge,      TEdgeData,
                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData> InVertex
        {
            get
            {
                return _InVertex;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region GenericEdge(myIGraph, myOutVertex, myInVertex, myEdgeId, myEdgeInitializer = null)

        /// <summary>
        /// Creates a new edge.
        /// </summary>
        /// <param name="myIGraph">The associated graph.</param>
        /// <param name="myOutVertex">The vertex at the tail of the edge.</param>
        /// <param name="myInVertex">The vertex at the head of the edge.</param>
        /// <param name="myEdgeId">The identification of this vertex.</param>
        /// <param name="myLabel">A label stored within this edge.</param>
        /// <param name="myEdgeInitializer">A delegate to initialize the newly created edge.</param>
        public GenericEdge(IGenericGraph myIGraph,
                           IGenericVertex<TIdVertex,    TRevisionIdVertex,    TVertexData,
                                          TIdEdge,      TRevisionIdEdge,      TEdgeData,
                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData> myOutVertex,
                           IGenericVertex<TIdVertex,    TRevisionIdVertex,    TVertexData,
                                          TIdEdge,      TRevisionIdEdge,      TEdgeData,
                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData> myInVertex,
                           TIdEdge              myEdgeId,
                           String               myLabel,
                           Action<IGenericEdge<TIdVertex,    TRevisionIdVertex,    TVertexData,
                                               TIdEdge,      TRevisionIdEdge,      TEdgeData,
                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData>> myEdgeInitializer = null)
        {

            if (myOutVertex == null)
                throw new ArgumentNullException("The OutVertex must not be null!");

            if (myInVertex == null)
                throw new ArgumentNullException("The InVertex must not be null!");

            _OutVertex = myOutVertex;
            _InVertex  = myInVertex;

            // Add the id and label
            Id    = myEdgeId;
            Label = myLabel;

            if (myEdgeInitializer != null)
                myEdgeInitializer(this);

        }

        #endregion

        #endregion


        #region Operator overloading

        #region Operator == (myGenericEdge1, myGenericEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myGenericEdge1">A GenericEdge.</param>
        /// <param name="myGenericEdge2">Another GenericEdge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (GenericEdge<TIdVertex,    TRevisionIdVertex,    TVertexData,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeData,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData> myGenericEdge1,
                                           GenericEdge<TIdVertex,    TRevisionIdVertex,    TVertexData,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeData,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData> myGenericEdge2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(myGenericEdge1, myGenericEdge2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) myGenericEdge1 == null) || ((Object) myGenericEdge2 == null))
                return false;

            return myGenericEdge1.Equals(myGenericEdge2);

        }

        #endregion

        #region Operator != (myGenericEdge1, myGenericEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myGenericEdge1">A GenericEdge.</param>
        /// <param name="myGenericEdge2">Another GenericEdge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (GenericEdge<TIdVertex,    TRevisionIdVertex,    TVertexData,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeData,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData> myGenericEdge1,
                                           GenericEdge<TIdVertex,    TRevisionIdVertex,    TVertexData,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeData,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData> myGenericEdge2)
        {
            return !(myGenericEdge1 == myGenericEdge2);
        }

        #endregion

        #region Operator <  (myGenericEdge1, myGenericEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myGenericEdge1">A GenericEdge.</param>
        /// <param name="myGenericEdge2">Another GenericEdge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (GenericEdge<TIdVertex,    TRevisionIdVertex,    TVertexData,
                                                      TIdEdge,      TRevisionIdEdge,      TEdgeData,
                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData> myGenericEdge1,
                                          GenericEdge<TIdVertex,    TRevisionIdVertex,    TVertexData,
                                                      TIdEdge,      TRevisionIdEdge,      TEdgeData,
                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData> myGenericEdge2)
        {

            // Check if myGenericEdge1 is null
            if ((Object) myGenericEdge1 == null)
                throw new ArgumentNullException("Parameter myGenericEdge1 must not be null!");

            // Check if myGenericEdge2 is null
            if ((Object) myGenericEdge2 == null)
                throw new ArgumentNullException("Parameter myGenericEdge2 must not be null!");

            return myGenericEdge1.CompareTo(myGenericEdge2) < 0;

        }

        #endregion

        #region Operator >  (myGenericEdge1, myGenericEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myGenericEdge1">A GenericEdge.</param>
        /// <param name="myGenericEdge2">Another GenericEdge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (GenericEdge<TIdVertex,    TRevisionIdVertex,    TVertexData,
                                                      TIdEdge,      TRevisionIdEdge,      TEdgeData,
                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData> myGenericEdge1,
                                          GenericEdge<TIdVertex,    TRevisionIdVertex,    TVertexData,
                                                      TIdEdge,      TRevisionIdEdge,      TEdgeData,
                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData> myGenericEdge2)
        {

            // Check if myGenericEdge1 is null
            if ((Object) myGenericEdge1 == null)
                throw new ArgumentNullException("Parameter myGenericEdge1 must not be null!");

            // Check if myGenericEdge2 is null
            if ((Object) myGenericEdge2 == null)
                throw new ArgumentNullException("Parameter myGenericEdge2 must not be null!");

            return myGenericEdge1.CompareTo(myGenericEdge2) > 0;

        }

        #endregion

        #region Operator <= (myGenericEdge1, myGenericEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myGenericEdge1">A GenericEdge.</param>
        /// <param name="myGenericEdge2">Another GenericEdge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (GenericEdge<TIdVertex,    TRevisionIdVertex,    TVertexData,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeData,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData> myGenericEdge1,
                                           GenericEdge<TIdVertex,    TRevisionIdVertex,    TVertexData,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeData,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData> myGenericEdge2)
        {
            return !(myGenericEdge1 > myGenericEdge2);
        }

        #endregion

        #region Operator >= (myGenericEdge1, myGenericEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myGenericEdge1">A GenericEdge.</param>
        /// <param name="myGenericEdge2">Another GenericEdge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (GenericEdge<TIdVertex,    TRevisionIdVertex,    TVertexData,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeData,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData> myGenericEdge1,
                                           GenericEdge<TIdVertex,    TRevisionIdVertex,    TVertexData,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeData,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData> myGenericEdge2)
        {
            return !(myGenericEdge1 < myGenericEdge2);
        }

        #endregion

        #endregion

        #region IComparable<IGenericEdge> Members

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

            // Check if myObject can be casted to an IGenericEdge object
            var myIGenericEdge = myObject as IGenericEdge;
            if ((Object) myIGenericEdge == null)
                throw new ArgumentException("myObject is not of type IGenericEdge!");

            return CompareTo(myIGenericEdge);

        }

        #endregion

        #region CompareTo(myIGenericEdge)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myIGenericEdge">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(IGenericEdge<TIdVertex,    TRevisionIdVertex,    TVertexData,
                                            TIdEdge,      TRevisionIdEdge,      TEdgeData,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData> myIGenericEdge)
        {

            // Check if myIGenericEdge is null
            if (myIGenericEdge == null)
                throw new ArgumentNullException("myIGenericEdge must not be null!");

            return Id.CompareTo(myIGenericEdge.Id);

        }

        #endregion

        #region CompareTo(myTIdEdge)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myTIdEdge">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(TIdEdge myTIdEdge)
        {

            // Check if myIGenericEdge is null
            if (myTIdEdge == null)
                throw new ArgumentNullException("myTIdEdge must not be null!");

            return Id.CompareTo(myTIdEdge);

        }

        #endregion

        #endregion

        #region IEquatable<IGenericEdge> Members

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

            var _Object = myObject as IGenericEdge;
            if (_Object != null)
                return Equals(_Object);

            return false;

        }

        #endregion

        #region Equals(myIGenericEdge)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myIGenericEdge">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(IGenericEdge<TIdVertex,    TRevisionIdVertex,    TVertexData,
                                           TIdEdge,      TRevisionIdEdge,      TEdgeData,
                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeData> myIGenericEdge)
        {

            if ((Object) myIGenericEdge == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return (this.Id.Equals(myIGenericEdge.Id));

        }

        #endregion

        #region Equals(myIGenericEdge)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myTIdEdge">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(TIdEdge myTIdEdge)
        {

            if ((Object) myTIdEdge == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return (this.Id.Equals(myTIdEdge));

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
