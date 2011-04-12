﻿/*
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
    public class PropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                   TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                   TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                   TVerticesCollection>

                                   : APropertyElement<TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>,
                                     IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>

        where TVerticesCollection : ICollection<IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                                                TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                                                TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>>

                                                                                                            
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

        #region PropertyHyperEdge(myIGraph, myOutVertex, myInVertices, myEdgeId, myEdgeInitializer = null)

        /// <summary>
        /// Creates a new edge.
        /// </summary>
        /// <param name="myIGraph">The associated graph.</param>
        /// <param name="myOutVertex">The vertex at the tail of the edge.</param>
        /// <param name="myInVertex">The vertex at the head of the edge.</param>
        /// <param name="myEdgeId">The identification of this edge.</param>
        /// <param name="myLabel">A label stored within this edge.</param>
        /// <param name="myEdgeInitializer">A delegate to initialize the newly created edge.</param>
        public PropertyHyperEdge(IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                                 TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>
                                                 myOutVertex,

                                 IEnumerable<IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,
                                                             TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,
                                                             TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>>
                                                             myInVertices,

                                 TIdHyperEdge                  myHyperEdgeId,
                                 String                        myLabel,
                                 TKeyHyperEdge                 myIdKey,
                                 TKeyHyperEdge                 myRevisonIdKey,
                                 Func<TDatastructureHyperEdge> myDataInitializer,
                                 Func<TVerticesCollection>     myVertexCollectionInitializer,

                                 Action<IProperties<TKeyHyperEdge, TValueHyperEdge>> myHyperEdgeInitializer = null)

            : base(myHyperEdgeId, myIdKey, myRevisonIdKey, myDataInitializer, myHyperEdgeInitializer)

        {

            if (myOutVertex == null)
                throw new ArgumentNullException("The OutVertex must not be null!");

            if (myInVertices == null)
                throw new ArgumentNullException("The InVertices must not be null!");

            _OutVertex    = myOutVertex;
            _InVertices   = myVertexCollectionInitializer();

            foreach (var _Vertex in myInVertices)
                _InVertices.Add(_Vertex);

            // Add the label
            //_Properties.Add(__Label, myLabel);

        }

        #endregion

        #endregion


        #region OutVertex

        /// <summary>
        /// The vertex at the tail of this edge.
        /// </summary>
        protected readonly IPropertyVertex<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                           TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                           TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>
                                           _OutVertex;

        /// <summary>
        /// The vertex at the tail of this edge.
        /// </summary>
        public IPropertyVertex<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                               TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                               TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>
                               OutVertex
        {
            get
            {
                return _OutVertex;
            }
        }

        #endregion

        #region InVertices

        /// <summary>
        /// The vertex at the head of this edge.
        /// </summary>
        protected readonly TVerticesCollection _InVertices;

        /// <summary>
        /// The vertex at the head of this edge.
        /// </summary>
        public IEnumerable<IPropertyVertex<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                           TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge,
                                           TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge>> InVertices
        {
            get
            {
                return _InVertices;
            }
        }

        #endregion


        #region Operator overloading

        #region Operator == (myPropertyHyperEdge1, myPropertyHyperEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myPropertyHyperEdge1">A Edge.</param>
        /// <param name="myPropertyHyperEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (PropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                             TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                             TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                             TVerticesCollection> myPropertyHyperEdge1,
                                           PropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                             TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                             TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                             TVerticesCollection> myPropertyHyperEdge2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(myPropertyHyperEdge1, myPropertyHyperEdge2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) myPropertyHyperEdge1 == null) || ((Object) myPropertyHyperEdge2 == null))
                return false;

            return myPropertyHyperEdge1.Equals(myPropertyHyperEdge2);

        }

        #endregion

        #region Operator != (myPropertyHyperEdge1, myPropertyHyperEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myPropertyHyperEdge1">A Edge.</param>
        /// <param name="myPropertyHyperEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (PropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                             TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                             TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                             TVerticesCollection> myPropertyHyperEdge1,
                                           PropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                             TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                             TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                             TVerticesCollection> myPropertyHyperEdge2)
        {
            return !(myPropertyHyperEdge1 == myPropertyHyperEdge2);
        }

        #endregion

        #region Operator <  (myPropertyHyperEdge1, myPropertyHyperEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myPropertyHyperEdge1">A Edge.</param>
        /// <param name="myPropertyHyperEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (PropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                            TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                            TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                             TVerticesCollection> myPropertyHyperEdge1,
                                          PropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                            TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                            TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                             TVerticesCollection> myPropertyHyperEdge2)
        {

            // Check if myPropertyHyperEdge1 is null
            if ((Object) myPropertyHyperEdge1 == null)
                throw new ArgumentNullException("Parameter myPropertyHyperEdge1 must not be null!");

            // Check if myPropertyHyperEdge2 is null
            if ((Object) myPropertyHyperEdge2 == null)
                throw new ArgumentNullException("Parameter myPropertyHyperEdge2 must not be null!");

            return myPropertyHyperEdge1.CompareTo(myPropertyHyperEdge2) < 0;

        }

        #endregion

        #region Operator >  (myPropertyHyperEdge1, myPropertyHyperEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myPropertyHyperEdge1">A Edge.</param>
        /// <param name="myPropertyHyperEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (PropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                            TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                            TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                             TVerticesCollection> myPropertyHyperEdge1,
                                          PropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                            TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                            TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                             TVerticesCollection> myPropertyHyperEdge2)
        {

            // Check if myPropertyHyperEdge1 is null
            if ((Object) myPropertyHyperEdge1 == null)
                throw new ArgumentNullException("Parameter myPropertyHyperEdge1 must not be null!");

            // Check if myPropertyHyperEdge2 is null
            if ((Object) myPropertyHyperEdge2 == null)
                throw new ArgumentNullException("Parameter myPropertyHyperEdge2 must not be null!");

            return myPropertyHyperEdge1.CompareTo(myPropertyHyperEdge2) > 0;

        }

        #endregion

        #region Operator <= (myPropertyHyperEdge1, myPropertyHyperEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myPropertyHyperEdge1">A Edge.</param>
        /// <param name="myPropertyHyperEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (PropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                             TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                             TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                             TVerticesCollection> myPropertyHyperEdge1,
                                           PropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                             TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                             TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                             TVerticesCollection> myPropertyHyperEdge2)
        {
            return !(myPropertyHyperEdge1 > myPropertyHyperEdge2);
        }

        #endregion

        #region Operator >= (myPropertyHyperEdge1, myPropertyHyperEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myPropertyHyperEdge1">A Edge.</param>
        /// <param name="myPropertyHyperEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (PropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                             TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                             TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                             TVerticesCollection> myPropertyHyperEdge1,
                                           PropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                             TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                             TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                             TVerticesCollection> myPropertyHyperEdge2)
        {
            return !(myPropertyHyperEdge1 < myPropertyHyperEdge2);
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

            // Check if myObject can be casted to an IPropertyHyperEdge object
            var myIPropertyHyperEdge = myObject as IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                                 TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                 TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>;
            if ((Object) myIPropertyHyperEdge == null)
                throw new ArgumentException("myObject is not of type IPropertyHyperEdge!");

            return CompareTo(myIPropertyHyperEdge);

        }

        #endregion

        #region CompareTo(myHyperEdgeId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myHyperEdgeId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(TIdHyperEdge myHyperEdgeId)
        {

            // Check if myHyperEdgeId is null
            if (myHyperEdgeId == null)
                throw new ArgumentNullException("myHyperEdgeId must not be null!");

            return Id.CompareTo(myHyperEdgeId);

        }

        #endregion

        #region CompareTo(myIPropertyElement)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myIPropertyElement">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(IPropertyElement<TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> myIPropertyElement)
        {

            // Check if myIPropertyElement is null
            if (myIPropertyElement == null)
                throw new ArgumentNullException("myIPropertyElement must not be null!");

            return Id.CompareTo(myIPropertyElement.Properties.GetProperty(_IdKey));

        }

        #endregion

        #endregion

        #region IEquatable<TIdHyperEdge> Members

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

            return Equals((TIdHyperEdge) myObject);

        }

        #endregion

        #region Equals(myHyperEdgeId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myHyperEdgeId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(TIdHyperEdge myHyperEdgeId)
        {

            if ((Object) myHyperEdgeId == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return Id.Equals(myHyperEdgeId);

        }

        #endregion

        #region Equals(myIPropertyElement)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myIPropertyElement">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(IPropertyElement<TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge> myIPropertyElement)
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

    }

}
