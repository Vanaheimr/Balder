/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET <http://www.github.com/ahzf/blueprints.NET>
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

namespace de.ahzf.blueprints.InMemory.PropertyGraph.Generic
{

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    public class PropertyVertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                TEdgeCollection>

                                : APropertyElement<TVertexId, TVertexRevisionId, TKeyVertex, TValueVertex, TDatastructureVertex>,
                                  IPropertyVertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                  TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                  THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
        
        where TEdgeCollection : ICollection<IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex>,
                                                         TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge>,
                                                         THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge>>>

        where TDatastructureVertex    : IDictionary<TKeyVertex,    TValueVertex>
        where TDatastructureEdge      : IDictionary<TKeyEdge,      TValueEdge>
        where TDatastructureHyperEdge : IDictionary<TKeyHyperEdge, TValueHyperEdge>

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable
                                                                                                            
        where TVertexId               : IEquatable<TVertexId>,            IComparable<TVertexId>,            IComparable, TValueVertex
        where TEdgeId                 : IEquatable<TEdgeId>,              IComparable<TEdgeId>,              IComparable, TValueEdge
        where THyperEdgeId            : IEquatable<THyperEdgeId>,         IComparable<THyperEdgeId>,         IComparable, TValueHyperEdge

        where TVertexRevisionId       : IEquatable<TVertexRevisionId>,    IComparable<TVertexRevisionId>,    IComparable, TValueVertex
        where TEdgeRevisionId         : IEquatable<TEdgeRevisionId>,      IComparable<TEdgeRevisionId>,      IComparable, TValueEdge
        where THyperEdgeRevisionId    : IEquatable<THyperEdgeRevisionId>, IComparable<THyperEdgeRevisionId>, IComparable, TValueHyperEdge

    {

        #region Properties

        // Links to the associated edges

        #region OutEdges

        /// <summary>
        /// The edges emanating from, or leaving, this vertex.
        /// </summary>
        protected readonly TEdgeCollection _OutEdges;

        /// <summary>
        /// The edges emanating from, or leaving, this vertex.
        /// </summary>
        public IEnumerable<IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex>,
                                        TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge>,
                                        THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge>>>
                                        OutEdges
        {
            get
            {
                return _OutEdges;
            }
        }

        #endregion

        #region InEdges

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex.
        /// </summary>
        protected readonly TEdgeCollection _InEdges;

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex.
        /// </summary>
        public IEnumerable<IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex>,
                                        TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge>,
                                        THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge>>>
                                        InEdges
        {
            get
            {
                return _InEdges;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region PropertyVertex(myIGraph, myVertexId, myVertexInitializer = null)

        /// <summary>
        /// Creates a new vertex.
        /// </summary>
        /// <param name="myIGraph">The associated graph.</param>
        /// <param name="myVertexId">The identification of this vertex.</param>
        /// <param name="myVertexInitializer">A delegate to initialize the newly created vertex.</param>
        public PropertyVertex(TVertexId                    myVertexId,
                              TKeyVertex                   myIdKey,
                              TKeyVertex                   myRevisonIdKey,
                              Func<TDatastructureVertex>   myDataInitializer,
                              Func<TEdgeCollection>        myEdgeCollectionInitializer = null,
                              Action<IProperties<TKeyVertex, TValueVertex>> myVertexInitializer = null)

            : base(myVertexId, myIdKey, myRevisonIdKey, myDataInitializer, myVertexInitializer)

        {

            _OutEdges     = myEdgeCollectionInitializer();
            _InEdges      = myEdgeCollectionInitializer();

        }

        #endregion

        #endregion


        #region OutEdges

        #region AddOutEdge(myIEdge)

        /// <summary>
        /// Add an outgoing edge.
        /// </summary>
        /// <param name="myIEdge">The edge to add.</param>
        public void AddOutEdge(IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex>,
                                            TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge>,
                                            THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge>>
                                            myIEdge)
        {
            _OutEdges.Add(myIEdge);
        }

        #endregion

        #region GetOutEdges(myLabel)

        /// <summary>
        /// The edges emanating from, or leaving, this vertex
        /// filtered by their label.
        /// </summary>
        public IEnumerable<IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex>,
                                        TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge>,
                                        THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge>>>
                                        GetOutEdges(String myLabel)
        {
            return from _Edge in _OutEdges where _Edge.Label == myLabel select _Edge;
        }

        #endregion

        #region RemoveOutEdge(myIEdge)

        /// <summary>
        /// Remove an outgoing edge.
        /// </summary>
        /// <param name="myIEdge">The edge to remove.</param>
        public void RemoveOutEdge(IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex>,
                                               TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge>,
                                               THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge>>
                                               myIEdge)
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
        public void AddInEdge(IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex>,
                                           TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge>,
                                           THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge>>
                                           myIEdge)
        {
            _InEdges.Add(myIEdge);
        }

        #endregion

        #region GetInEdges(myLabel)

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex
        /// filtered by their label.
        /// </summary>
        public IEnumerable<IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex>,
                                        TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge>,
                                        THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge>>>
                                        GetInEdges(String myLabel)
        {
            return from _Edge in _InEdges where _Edge.Label == myLabel select _Edge;
        }

        #endregion

        #region RemoveInEdge(myIEdge)

        /// <summary>
        /// Remove an incoming edge.
        /// </summary>
        /// <param name="myIEdge">The edge to remove.</param>
        public void RemoveInEdge(IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex>,
                                              TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge>,
                                              THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge>>
                                              myIEdge)
        {
            lock (this)
            {
                _InEdges.Remove(myIEdge);
            }
        }

        #endregion

        #endregion


        #region Operator overloading

        #region Operator == (myPropertyVertex1, myPropertyVertex2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myPropertyVertex1">A Vertex.</param>
        /// <param name="myPropertyVertex2">Another Vertex.</param>
        /// <returns>true|false</returns>
        /// 

        public static Boolean operator == (PropertyVertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                          TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                          THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                          TEdgeCollection> myPropertyVertex1,
                                           PropertyVertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                          TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                          THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                          TEdgeCollection> myPropertyVertex2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(myPropertyVertex1, myPropertyVertex2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) myPropertyVertex1 == null) || ((Object) myPropertyVertex2 == null))
                return false;

            return myPropertyVertex1.Equals(myPropertyVertex2);

        }

        #endregion

        #region Operator != (myPropertyVertex1, myPropertyVertex2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myPropertyVertex1">A Vertex.</param>
        /// <param name="myPropertyVertex2">Another Vertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (PropertyVertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                          TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                          THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                          TEdgeCollection> myPropertyVertex1,
                                           PropertyVertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                          TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                          THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                          TEdgeCollection> myPropertyVertex2)
        {
            return !(myPropertyVertex1 == myPropertyVertex2);
        }

        #endregion

        #region Operator <  (myPropertyVertex1, myPropertyVertex2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myPropertyVertex1">A Vertex.</param>
        /// <param name="myPropertyVertex2">Another Vertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (PropertyVertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                         TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                         THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                         TEdgeCollection> myPropertyVertex1,
                                          PropertyVertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                         TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                         THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                         TEdgeCollection> myPropertyVertex2)
        {

            // Check if myPropertyVertex1 is null
            if ((Object) myPropertyVertex1 == null)
                throw new ArgumentNullException("Parameter myPropertyVertex1 must not be null!");

            // Check if myPropertyVertex2 is null
            if ((Object) myPropertyVertex2 == null)
                throw new ArgumentNullException("Parameter myPropertyVertex2 must not be null!");

            return myPropertyVertex1.CompareTo(myPropertyVertex2) < 0;

        }

        #endregion

        #region Operator >  (myPropertyVertex1, myPropertyVertex2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myPropertyVertex1">A Vertex.</param>
        /// <param name="myPropertyVertex2">Another Vertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (PropertyVertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                         TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                         THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                         TEdgeCollection> myPropertyVertex1,
                                          PropertyVertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                         TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                         THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                         TEdgeCollection> myPropertyVertex2)
        {

            // Check if myPropertyVertex1 is null
            if ((Object) myPropertyVertex1 == null)
                throw new ArgumentNullException("Parameter myPropertyVertex1 must not be null!");

            // Check if myPropertyVertex2 is null
            if ((Object) myPropertyVertex2 == null)
                throw new ArgumentNullException("Parameter myPropertyVertex2 must not be null!");

            return myPropertyVertex1.CompareTo(myPropertyVertex2) > 0;

        }

        #endregion

        #region Operator <= (myPropertyVertex1, myPropertyVertex2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myPropertyVertex1">A Vertex.</param>
        /// <param name="myPropertyVertex2">Another Vertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (PropertyVertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                          TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                          THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                          TEdgeCollection> myPropertyVertex1,
                                           PropertyVertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                          TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                          THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                          TEdgeCollection> myPropertyVertex2)
        {
            return !(myPropertyVertex1 > myPropertyVertex2);
        }

        #endregion

        #region Operator >= (myPropertyVertex1, myPropertyVertex2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myPropertyVertex1">A Vertex.</param>
        /// <param name="myPropertyVertex2">Another Vertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (PropertyVertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                          TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                          THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                          TEdgeCollection> myPropertyVertex1,
                                           PropertyVertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                          TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                          THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                          TEdgeCollection> myPropertyVertex2)
        {
            return !(myPropertyVertex1 < myPropertyVertex2);
        }

        #endregion

        #endregion

        #region IComparable<TVertexId> Members

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

            return CompareTo((TVertexId) myObject);

        }

        #endregion

        #region CompareTo(myVertexId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myVertexId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(TVertexId myVertexId)
        {

            // Check if myVertexId is null
            if (myVertexId == null)
                throw new ArgumentNullException("myVertexId must not be null!");

            return Id.CompareTo(myVertexId);

        }

        #endregion

        #region CompareTo(myIPropertyElement)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myIPropertyElement">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(IPropertyElement<TVertexId, TVertexRevisionId, TKeyVertex, TValueVertex> myIPropertyElement)
        {

            // Check if myIPropertyElement is null
            if (myIPropertyElement == null)
                throw new ArgumentNullException("myIPropertyElement must not be null!");

            return Id.CompareTo(myIPropertyElement.Properties.GetProperty(_IdKey));

        }

        #endregion

        #endregion

        #region IEquatable<TVertexId> Members

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

            return Equals((TVertexId) myObject);

        }

        #endregion

        #region Equals(myVertexId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myVertexId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(TVertexId myVertexId)
        {

            if ((Object) myVertexId == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return Id.Equals(myVertexId);

        }

        #endregion

        #region Equals(myIPropertyElement)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myIPropertyElement">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(IPropertyElement<TVertexId, TVertexRevisionId, TKeyVertex, TValueVertex> myIPropertyElement)
        {

            if ((Object) myIPropertyElement == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return Id.Equals(myIPropertyElement.Properties.GetProperty(_IdKey));

        }

        #endregion

        #endregion

    }

}
