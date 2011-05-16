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
using System.Dynamic;
using System.Linq.Expressions;
using System.Collections.Generic;

#endregion

namespace de.ahzf.blueprints.PropertyGraph.InMemory
{

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    public class PropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                TEdgeCollection>

                                : APropertyElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex, TDatastructureVertex>,

                                  IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>,

                                  IDynamicGraphObject<PropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                                     TEdgeCollection>>


        where TEdgeCollection         : ICollection<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

        where TIdVertex               : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge                 : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdHyperEdge            : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TRevisionIdVertex       : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge         : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdHyperEdge    : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

        where TDatastructureVertex    : IDictionary<TKeyVertex,    TValueVertex>
        where TDatastructureEdge      : IDictionary<TKeyEdge,      TValueEdge>
        where TDatastructureHyperEdge : IDictionary<TKeyHyperEdge, TValueHyperEdge>

    {

        #region Constructor(s)

        #region PropertyVertex(myIGraph, myVertexId, myIdKey, myRevisonIdKey, myDataInitializer, myEdgeCollectionInitializer = null, myVertexInitializer = null)

        /// <summary>
        /// Creates a new vertex.
        /// </summary>
        /// <param name="myIGraph">The associated graph.</param>
        /// <param name="myVertexId">The identification of this vertex.</param>
        /// <param name="myIdKey">The key to access the Id of this vertex.</param>
        /// <param name="myRevisonIdKey">The key to access the RevisionId of this vertex.</param>
        /// <param name="myDataInitializer">A func to initialize the datastructure of this vertex.</param></param>
        /// <param name="myEdgeCollectionInitializer">A func to initialize the datastructure for storing all edges.</param>
        /// <param name="myVertexInitializer">A delegate to initialize the newly created vertex.</param>
        public PropertyVertex(TIdVertex                    myVertexId,
                              TKeyVertex                   myIdKey,
                              TKeyVertex                   myRevisonIdKey,
                              Func<TDatastructureVertex>   myDataInitializer,
                              Func<TEdgeCollection>        myEdgeCollectionInitializer,
                              Action<IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> myVertexInitializer = null)

            : base(myVertexId, myIdKey, myRevisonIdKey, myDataInitializer)

        {

            if (myVertexInitializer != null)
                myVertexInitializer(this);

            _OutEdges = myEdgeCollectionInitializer();
            _InEdges  = myEdgeCollectionInitializer();

        }

        #endregion

        #endregion


        #region OutEdges

        #region AddOutEdge(myIEdge)

        /// <summary>
        /// Add an outgoing edge.
        /// </summary>
        /// <param name="myIEdge">The edge to add.</param>
        public void AddOutEdge(IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                             myIEdge)
        {
            _OutEdges.Add(myIEdge);
        }

        #endregion

        #region OutEdges

        /// <summary>
        /// The edges emanating from, or leaving, this vertex.
        /// </summary>
        protected readonly TEdgeCollection _OutEdges;

        /// <summary>
        /// The edges emanating from, or leaving, this vertex.
        /// </summary>
        public IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,      TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,        TValueEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
                                         OutEdges
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
        public IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
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
        public void RemoveOutEdge(IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
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
        public void AddInEdge(IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                            myIEdge)
        {
            _InEdges.Add(myIEdge);
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
        public IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
                                         InEdges
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
        public IEnumerable<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
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
        public void RemoveInEdge(IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
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

        public static Boolean operator == (PropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                          TEdgeCollection> myPropertyVertex1,
                                           PropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
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
        public static Boolean operator != (PropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                          TEdgeCollection> myPropertyVertex1,
                                           PropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
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
        public static Boolean operator < (PropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                         TEdgeCollection> myPropertyVertex1,
                                          PropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
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
        public static Boolean operator > (PropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                         TEdgeCollection> myPropertyVertex1,
                                          PropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
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
        public static Boolean operator <= (PropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                          TEdgeCollection> myPropertyVertex1,
                                           PropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
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
        public static Boolean operator >= (PropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                          TEdgeCollection> myPropertyVertex1,
                                           PropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                          TEdgeCollection> myPropertyVertex2)
        {
            return !(myPropertyVertex1 < myPropertyVertex2);
        }

        #endregion

        #endregion


        #region IDynamicGraphObject<PropertyVertex> Members

        #region GetMetaObject(myExpression)

        /// <summary>
        /// Return the appropriate DynamicMetaObject.
        /// </summary>
        /// <param name="myExpression">An Expression.</param>
        public DynamicMetaObject GetMetaObject(Expression myExpression)
        {
            return new DynamicGraphObject<PropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                         TEdgeCollection>>(myExpression, this);
        }

        #endregion

        #region GetDynamicMemberNames()

        /// <summary>
        /// Returns an enumeration of all property keys.
        /// </summary>
        public virtual IEnumerable<String> GetDynamicMemberNames()
        {
            foreach (var _PropertyKey in _Data.PropertyKeys)
                yield return _PropertyKey.ToString();
        }

        #endregion


        #region SetMember(myBinder, myObject)

        /// <summary>
        /// Sets a new property or overwrites an existing.
        /// </summary>
        /// <param name="myBinder">The property key</param>
        /// <param name="myObject">The property value</param>
        public virtual Object SetMember(String myBinder, Object myObject)
        {
            return _Data.SetProperty((TKeyVertex) (Object) myBinder, (TValueVertex) myObject);
        }

        #endregion

        #region GetMember(myBinder)

        /// <summary>
        /// Returns the value of the given property key.
        /// </summary>
        /// <param name="myBinder">The property key.</param>
        public virtual Object GetMember(String myBinder)
        {
            TValueVertex myObject;
            _Data.TryGetProperty((TKeyVertex) (Object) myBinder, out myObject);
            return myObject as Object;
        }

        #endregion

        #region DeleteMember(myBinder)

        /// <summary>
        /// Tries to remove the property identified by the given property key.
        /// </summary>
        /// <param name="myBinder">The property key.</param>
        public virtual Object DeleteMember(String myBinder)
        {

            try
            {
                _Data.RemoveProperty((TKeyVertex) (Object) myBinder);
                return true;
            }
            catch
            { }

            return false;

        }

        #endregion

        #endregion

        #region IComparable<TIdVertex> Members

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

            return CompareTo((TIdVertex) myObject);

        }

        #endregion

        #region CompareTo(myVertexId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myVertexId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(TIdVertex myVertexId)
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
        public Int32 CompareTo(IPropertyElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex> myIPropertyElement)
        {

            // Check if myIPropertyElement is null
            if (myIPropertyElement == null)
                throw new ArgumentNullException("myIPropertyElement must not be null!");

            return Id.CompareTo(myIPropertyElement.Properties.GetProperty(_IdKey));

        }

        #endregion

        #endregion

        #region IEquatable<TIdVertex> Members

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

            return Equals((TIdVertex) myObject);

        }

        #endregion

        #region Equals(myVertexId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myVertexId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(TIdVertex myVertexId)
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
        public Boolean Equals(IPropertyElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex> myIPropertyElement)
        {

            if ((Object) myIPropertyElement == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return Id.Equals(myIPropertyElement.Properties.GetProperty(_IdKey));

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
            return base.GetHashCode();
        }

        #endregion


        public new IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                                   SetProperty(TKeyVertex myKey, TValueVertex myValue)

        {
            Data.SetProperty(myKey, myValue);
            return this;
        }

    }

}
