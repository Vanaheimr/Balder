/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET
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
        
                                : IPropertyVertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                  TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                  THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
        
        where TEdgeCollection : ICollection<IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                                         TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                                         THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>

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

        #region Id

        protected readonly TKeyVertex _IdKey;

        public TVertexId Id
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region RevisionId

        protected readonly TKeyVertex _RevisonIdKey;

        public TVertexRevisionId RevisionId
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Data

        protected readonly IProperties<TKeyVertex, TValueVertex, TDatastructureVertex> _Data;

        public IProperties<TKeyVertex, TValueVertex, TDatastructureVertex> Data
        {
            get
            {
                return _Data;
            }
        }

        #endregion

        
        // Links to the associated edges

        #region OutEdges

        /// <summary>
        /// The edges emanating from, or leaving, this vertex.
        /// </summary>
        protected readonly TEdgeCollection _OutEdges;

        /// <summary>
        /// The edges emanating from, or leaving, this vertex.
        /// </summary>
        public IEnumerable<IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                        TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                        THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>
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
        public IEnumerable<IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                        TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                        THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>
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
        public PropertyVertex(TVertexId                  myVertexId,
                              TKeyVertex                 myIdKey,
                              TKeyVertex                 myRevisonIdKey,
                              Func<TDatastructureVertex> myDataInitializer,
                              Func<TEdgeCollection>      myEdgeCollectionInitializer = null,
                              Action<IPropertyVertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                     TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                     THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>
                                                     myVertexInitializer = null)
        {

            _OutEdges     = myEdgeCollectionInitializer();
            _InEdges      = myEdgeCollectionInitializer();
            _IdKey        = myIdKey;
            _RevisonIdKey = myRevisonIdKey;
            _Data         = new Properties<TVertexId, TVertexRevisionId, TKeyVertex, TValueVertex, TDatastructureVertex>
                                          (myVertexId, myIdKey, myRevisonIdKey, myDataInitializer);

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
        public void AddOutEdge(IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                            TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                            THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>
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
        public IEnumerable<IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                        TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                        THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>
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
        public void RemoveOutEdge(IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                               TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                               THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>
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
        public void AddInEdge(IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                           TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                           THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>
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
        public IEnumerable<IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                        TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                        THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>
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
        public void RemoveInEdge(IGenericEdge<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                              TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                              THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>
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

        #region IComparable<IPropertyVertex> Members

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
            var myIPropertyVertex = myObject as IPropertyVertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                        TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                        THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>;
            if ((Object) myIPropertyVertex == null)
                throw new ArgumentException("myObject is not of type myIPropertyVertex!");

            return CompareTo(myIPropertyVertex);

        }

        #endregion

        #region CompareTo(myIPropertyVertex)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myIGenericVertex">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(IPropertyVertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                               TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                               THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                               myIGenericVertex)
        {

            // Check if myIPropertyVertex is null
            if (myIGenericVertex == null)
                throw new ArgumentNullException("myIGenericVertex must not be null!");

            return Id.CompareTo(myIGenericVertex.Id);

        }

        #endregion

        //#region CompareTo(myIGenericVertexIPropertiesString)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="myIGenericVertexIPropertiesString">An object to compare with.</param>
        ///// <returns>true|false</returns>
        //public Int32 CompareTo(IGenericVertex<IProperties<String>> myIGenericVertexIPropertiesString)
        //{

        //    // Check if myIPropertyVertex is null
        //    if (myIGenericVertexIPropertiesString == null)
        //        throw new ArgumentNullException("myIGenericVertexIPropertiesString must not be null!");

        //    return Id.CompareTo(myIGenericVertexIPropertiesString.Id);

        //}

        //#endregion

        #endregion

        #region IEquatable<IPropertyVertex> Members

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

            var _Object = myObject as IPropertyVertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                      TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                      THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>;
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
        public Boolean Equals(IPropertyVertex<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                              TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                              THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                              myIGenericVertex)
        {

            if ((Object) myIGenericVertex == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return false; //(this.Id == myIGenericVertex.Id);

        }

        #endregion

        //#region Equals(myIGenericVertexIPropertiesString)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="myIGenericVertexIPropertiesString">An object to compare with.</param>
        ///// <returns>true|false</returns>
        //public Boolean Equals(IGenericVertex<IProperties<String>> myIGenericVertexIPropertiesString)
        //{

        //    if ((Object) myIGenericVertexIPropertiesString == null)
        //        return false;

        //    //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
        //    return (this.Id == myIGenericVertexIPropertiesString.Id);

        //}

        //#endregion

        #endregion

        #region GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override Int32 GetHashCode()
        {

            if (Id == null)
                return 0;

            return Id.GetHashCode();

        }

        #endregion





        public bool Equals(TVertexId other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(TVertexId other)
        {
            throw new NotImplementedException();
        }


    }

}
