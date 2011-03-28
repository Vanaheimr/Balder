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
    public class PropertyHyperEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                   TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                   THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                   TVerticesCollection>
                                   
                                   : IPropertyHyperEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                        TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                        THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>

        where TVerticesCollection : ICollection<IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
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

        #region Data

        /// <summary>
        /// The property key of the Label property
        /// </summary>
        private   const    String  __Label = "Label";

        protected readonly TKeyHyperEdge _IdKey;
        protected readonly TKeyHyperEdge _RevisonIdKey;

        #endregion

        #region Properties

        // Edge properties

        #region Id

        public THyperEdgeId Id
        {
            get
            {
                return (THyperEdgeId) Data.GetProperty(_IdKey);
            }
        }

        #endregion

        #region RevisionId

        public THyperEdgeRevisionId RevisionId
        {
            get
            {
                return (THyperEdgeRevisionId) Data.GetProperty(_RevisonIdKey);
            }
        }

        #endregion

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

        #region Data

        protected readonly IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge> _Data;

        public IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge> Data
        {
            get
            {
                return _Data;
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

        #region InVertices

        /// <summary>
        /// The vertex at the head of this edge.
        /// </summary>
        protected readonly TVerticesCollection _InVertices;

        /// <summary>
        /// The vertex at the head of this edge.
        /// </summary>
        public IEnumerable<IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                          TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                          THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>> InVertices
        {
            get
            {
                return _InVertices;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region PropertyHyperEdge(myIGraph, myOutVertex, myInVertex, myEdgeId, myEdgeInitializer = null)

        /// <summary>
        /// Creates a new edge.
        /// </summary>
        /// <param name="myIGraph">The associated graph.</param>
        /// <param name="myOutVertex">The vertex at the tail of the edge.</param>
        /// <param name="myInVertex">The vertex at the head of the edge.</param>
        /// <param name="myEdgeId">The identification of this edge.</param>
        /// <param name="myLabel">A label stored within this edge.</param>
        /// <param name="myEdgeInitializer">A delegate to initialize the newly created edge.</param>
        internal protected PropertyHyperEdge(IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                                            TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                                            THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>
                                                            myOutVertex,

                                             IEnumerable<IGenericVertex<TVertexId,    TVertexRevisionId,    IProperties<TKeyVertex,    TValueVertex,    TDatastructureVertex>,
                                                                        TEdgeId,      TEdgeRevisionId,      IProperties<TKeyEdge,      TValueEdge,      TDatastructureEdge>,
                                                                        THyperEdgeId, THyperEdgeRevisionId, IProperties<TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>>
                                                                        myInVertices,

                                             THyperEdgeId  myHyperEdgeId,
                                             String        myLabel,
                                             TKeyHyperEdge myIdKey,
                                             TKeyHyperEdge myRevisonIdKey,

                                             Func<TDatastructureHyperEdge> myDataInitializer,
                                             Func<TVerticesCollection>     myVertexCollectionInitializer,

                                             Action<IPropertyHyperEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                                       TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                       THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>
                                                                       myEdgeInitializer = null)
        {

            if (myOutVertex == null)
                throw new ArgumentNullException("The OutVertex must not be null!");

            if (myInVertices == null)
                throw new ArgumentNullException("The InVertices must not be null!");

            _OutVertex    = myOutVertex;
            _InVertices   = myVertexCollectionInitializer();

            foreach (var _Vertex in myInVertices)
                _InVertices.Add(_Vertex);

            _IdKey        = myIdKey;
            _RevisonIdKey = myRevisonIdKey;
            _Data         = new Properties<THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                          (myHyperEdgeId, myIdKey, myRevisonIdKey, myDataInitializer);

            // Add the label
            //_Properties.Add(__Label, myLabel);

            if (myEdgeInitializer != null)
                myEdgeInitializer(this);

        }

        #endregion

        #endregion

        #region Operator overloading

        #region Operator == (myPropertyHyperEdge1, myPropertyHyperEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myPropertyHyperEdge1">A Edge.</param>
        /// <param name="myPropertyHyperEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (PropertyHyperEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                             TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                             THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                             TVerticesCollection> myPropertyHyperEdge1,
                                           PropertyHyperEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                             TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                             THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
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
        public static Boolean operator != (PropertyHyperEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                             TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                             THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                             TVerticesCollection> myPropertyHyperEdge1,
                                           PropertyHyperEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                             TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                             THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
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
        public static Boolean operator < (PropertyHyperEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                            TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                            THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                             TVerticesCollection> myPropertyHyperEdge1,
                                          PropertyHyperEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                            TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                            THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
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
        public static Boolean operator > (PropertyHyperEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                            TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                            THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                             TVerticesCollection> myPropertyHyperEdge1,
                                          PropertyHyperEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                            TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                            THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
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
        public static Boolean operator <= (PropertyHyperEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                             TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                             THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                             TVerticesCollection> myPropertyHyperEdge1,
                                           PropertyHyperEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                             TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                             THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
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
        public static Boolean operator >= (PropertyHyperEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                             TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                             THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                             TVerticesCollection> myPropertyHyperEdge1,
                                           PropertyHyperEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                             TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                             THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                             TVerticesCollection> myPropertyHyperEdge2)
        {
            return !(myPropertyHyperEdge1 < myPropertyHyperEdge2);
        }

        #endregion

        #endregion

        #region IComparable<IPropertyHyperEdge> Members

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
            var myIPropertyHyperEdge = myObject as IPropertyEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                    TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                    THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>;
            if ((Object) myIPropertyHyperEdge == null)
                throw new ArgumentException("myObject is not of type IPropertyHyperEdge!");

            return CompareTo(myIPropertyHyperEdge);

        }

        #endregion

        #region CompareTo(myIGenericEdge)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myIGenericEdge">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(IPropertyEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                             TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                             THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                             myIGenericEdge)
        {

            // Check if myIPropertyHyperEdge is null
            if (myIGenericEdge == null)
                throw new ArgumentNullException("myIGenericEdge must not be null!");

            return Id.CompareTo(myIGenericEdge.Id);

        }

        #endregion

        //#region CompareTo(myIGenericEdgeIPropertiesString)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="myIGenericEdgeIPropertiesString">An object to compare with.</param>
        ///// <returns>true|false</returns>
        //public Int32 CompareTo(IGenericEdge<IProperties<String>> myIGenericEdgeIPropertiesString)
        //{

        //    // Check if myIPropertyHyperEdge is null
        //    if (myIGenericEdgeIPropertiesString == null)
        //        throw new ArgumentNullException("myIGenericEdgeIPropertiesString must not be null!");

        //    return Id.CompareTo(myIGenericEdgeIPropertiesString.Id);

        //}

        //#endregion

        #endregion

        #region IEquatable<IPropertyHyperEdge> Members

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

            var _Object = myObject as IPropertyEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                    TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                    THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>;
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
        public Boolean Equals(IPropertyEdge<TVertexId,    TVertexRevisionId,    TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                            TEdgeId,      TEdgeRevisionId,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                            THyperEdgeId, THyperEdgeRevisionId, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                            myIGenericEdge)
        {

            if ((Object) myIGenericEdge == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return false;// (this.Id == myIGenericEdge.Id);

        }

        #endregion

        //#region Equals(myIGenericEdgeIPropertiesString)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="myIGenericEdgeIPropertiesString">An object to compare with.</param>
        ///// <returns>true|false</returns>
        //public Boolean Equals(IGenericEdge<IProperties<String>> myIGenericEdgeIPropertiesString)
        //{

        //    if ((Object) myIGenericEdgeIPropertiesString == null)
        //        return false;

        //    //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
        //    return (this.Id == myIGenericEdgeIPropertiesString.Id);

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


        public bool Equals(THyperEdgeId other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(THyperEdgeId other)
        {
            throw new NotImplementedException();
        }

    }

}
