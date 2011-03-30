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
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq.Expressions;

#endregion

namespace de.ahzf.blueprints.InMemory.PropertyGraph.Generic
{

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    public class PropertyEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                              TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                              THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
        
                              : AElement<TEdgeId, TEdgeRevisionId, TKeyEdge, TValueEdge, TDatastructureEdge>,
                                IPropertyEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                              TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                              THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
        
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

        #region Data

        /// <summary>
        /// The property key of the Label property
        /// </summary>
        protected const String __Label = "Label";

        #endregion

        #region Properties

        // Edge properties

        #region Label

        /// <summary>
        /// The label associated with this edge.
        /// </summary>
        public String Label
        {
            get
            {

                //TValue _Object;

                //if (_Properties.TryGetValue(__Label, out _Object))
                //    return _Object as String;

                return null;

            }
        }

        #endregion

        // Links to the associated vertices

        #region OutVertex

        /// <summary>
        /// The vertex at the tail of this edge.
        /// </summary>
        protected readonly IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                          TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                          THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>
                                          _OutVertex;

        /// <summary>
        /// The vertex at the tail of this edge.
        /// </summary>
        public IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                              TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                              THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>
                              OutVertex
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
        protected readonly IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                          TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                          THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>
                                          _InVertex;

        /// <summary>
        /// The vertex at the head of this edge.
        /// </summary>
        public IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                              TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                              THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>
                              InVertex
        {
            get
            {
                return _InVertex;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region PropertyEdge(myIGraph, myOutVertex, myInVertex, myEdgeId, myEdgeInitializer = null)

        /// <summary>
        /// Creates a new edge.
        /// </summary>
        /// <param name="myIGraph">The associated graph.</param>
        /// <param name="myOutVertex">The vertex at the tail of the edge.</param>
        /// <param name="myInVertex">The vertex at the head of the edge.</param>
        /// <param name="myEdgeId">The identification of this edge.</param>
        /// <param name="myLabel">A label stored within this edge.</param>
        /// <param name="myEdgeInitializer">A delegate to initialize the newly created edge.</param>
        internal protected PropertyEdge(IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                                       TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                                       THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>
                                                       myOutVertex,

                                        IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                                       TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                                       THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>
                                                       myInVertex,

                                        TEdgeId                  myEdgeId,
                                        String                   myLabel,
                                        TKeyEdge                 myIdKey,
                                        TKeyEdge                 myRevisonIdKey,
                                        Func<TDatastructureEdge> myDataInitializer,
                                        Action<IProperties<TKeyEdge, TValueEdge, TDatastructureEdge>> myEdgeInitializer = null)
            
            : base(myEdgeId, myIdKey, myRevisonIdKey, myDataInitializer, myEdgeInitializer)

        {

            if (myOutVertex == null)
                throw new ArgumentNullException("The OutVertex must not be null!");

            if (myInVertex == null)
                throw new ArgumentNullException("The InVertex must not be null!");

            _OutVertex    = myOutVertex;
            _InVertex     = myInVertex;

            // Add the label
            //_Properties.Add(__Label, myLabel);

            if (myEdgeInitializer != null)
                myEdgeInitializer(Data);

        }

        #endregion

        #endregion

        #region Operator overloading

        #region Operator == (myPropertyEdge1, myPropertyEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myPropertyEdge1">A Edge.</param>
        /// <param name="myPropertyEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (PropertyEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                        TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                        THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                        myPropertyEdge1,
                                           PropertyEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                        TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                        THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                        myPropertyEdge2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(myPropertyEdge1, myPropertyEdge2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) myPropertyEdge1 == null) || ((Object) myPropertyEdge2 == null))
                return false;

            return myPropertyEdge1.Equals(myPropertyEdge2);

        }

        #endregion

        #region Operator != (myPropertyEdge1, myPropertyEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myPropertyEdge1">A Edge.</param>
        /// <param name="myPropertyEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (PropertyEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                        TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                        THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                        myPropertyEdge1,
                                           PropertyEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                        TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                        THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                        myPropertyEdge2)
        {
            return !(myPropertyEdge1 == myPropertyEdge2);
        }

        #endregion

        #region Operator <  (myPropertyEdge1, myPropertyEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myPropertyEdge1">A Edge.</param>
        /// <param name="myPropertyEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (PropertyEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                       TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                       THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                       myPropertyEdge1,
                                          PropertyEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                       TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                       THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                       myPropertyEdge2)
        {

            // Check if myPropertyEdge1 is null
            if ((Object) myPropertyEdge1 == null)
                throw new ArgumentNullException("Parameter myPropertyEdge1 must not be null!");

            // Check if myPropertyEdge2 is null
            if ((Object) myPropertyEdge2 == null)
                throw new ArgumentNullException("Parameter myPropertyEdge2 must not be null!");

            return myPropertyEdge1.CompareTo(myPropertyEdge2) < 0;

        }

        #endregion

        #region Operator >  (myPropertyEdge1, myPropertyEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myPropertyEdge1">A Edge.</param>
        /// <param name="myPropertyEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (PropertyEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                       TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                       THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                       myPropertyEdge1,
                                          PropertyEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                       TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                       THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                       myPropertyEdge2)
        {

            // Check if myPropertyEdge1 is null
            if ((Object) myPropertyEdge1 == null)
                throw new ArgumentNullException("Parameter myPropertyEdge1 must not be null!");

            // Check if myPropertyEdge2 is null
            if ((Object) myPropertyEdge2 == null)
                throw new ArgumentNullException("Parameter myPropertyEdge2 must not be null!");

            return myPropertyEdge1.CompareTo(myPropertyEdge2) > 0;

        }

        #endregion

        #region Operator <= (myPropertyEdge1, myPropertyEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myPropertyEdge1">A Edge.</param>
        /// <param name="myPropertyEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (PropertyEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                        TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                        THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                        myPropertyEdge1,
                                           PropertyEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                        TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                        THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                        myPropertyEdge2)
        {
            return !(myPropertyEdge1 > myPropertyEdge2);
        }

        #endregion

        #region Operator >= (myPropertyEdge1, myPropertyEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myPropertyEdge1">A Edge.</param>
        /// <param name="myPropertyEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (PropertyEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                        TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                        THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                        myPropertyEdge1,
                                           PropertyEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                        TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                        THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                        myPropertyEdge2)
        {
            return !(myPropertyEdge1 < myPropertyEdge2);
        }

        #endregion

        #endregion

        #region IComparable<TEdgeId> Members

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

            return CompareTo((TEdgeId) myObject);

        }

        #endregion

        #region CompareTo(myEdgeId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myEdgeId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(TEdgeId myEdgeId)
        {

            // Check if myIPropertyEdge is null
            if (myEdgeId == null)
                throw new ArgumentNullException("myEdgeId must not be null!");

            return Id.CompareTo(myEdgeId);

        }

        #endregion

        #endregion

        #region IEquatable<TEdgeId> Members

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

            return Equals((TEdgeId) myObject);

        }

        #endregion

        #region Equals(myEdgeId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myEdgeId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(TEdgeId myEdgeId)
        {

            if ((Object) myEdgeId == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return Id.Equals(myEdgeId);

        }

        #endregion

        #endregion

    }

}
