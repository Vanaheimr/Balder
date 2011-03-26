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
using System.Collections;
using System.Collections.Generic;

using de.ahzf.blueprints.Datastructures;

#endregion

namespace de.ahzf.blueprints.InMemoryGraph
{

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    public class Edge : AStringProperties<EdgeId>, IEdge
    {

        #region Data

        /// <summary>
        /// The vertex at the tail of this edge.
        /// </summary>
        protected readonly IVertex _OutVertex;

        /// <summary>
        /// The vertex at the head of this edge.
        /// </summary>
        protected readonly IVertex _InVertex;

        /// <summary>
        /// The property key of the Label property
        /// </summary>
        private   const    String  __Label = "Label";

        #endregion

        #region Properties

        // Edge properties

        #region Id

        /// <summary>
        /// An identifier that is unique to its inheriting class.
        /// All vertices of a graph must have unique identifiers.
        /// All edges of a graph must have unique identifiers.
        /// </summary>
        public new EdgeId Id
        {
            get
            {

                Object _Object = null;

                if (_Properties.TryGetValue(__Id, out _Object))
                    return _Object as EdgeId;

                return null;

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

                Object _Object = null;

                if (_Properties.TryGetValue(__Label, out _Object))
                    return _Object as String;

                return null;

            }
        }

        #endregion


        // Links to the associated vertices

        #region OutVertex

        /// <summary>
        /// The vertex at the tail of this edge.
        /// </summary>
        public IVertex OutVertex
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
        public IVertex InVertex
        {
            get
            {
                return _InVertex;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region Edge(myIGraph, myOutVertex, myInVertex, myEdgeId, myEdgeInitializer = null)

        /// <summary>
        /// Creates a new edge.
        /// </summary>
        /// <param name="myIGraph">The associated graph.</param>
        /// <param name="myOutVertex">The vertex at the tail of the edge.</param>
        /// <param name="myInVertex">The vertex at the head of the edge.</param>
        /// <param name="myEdgeId">The identification of this edge.</param>
        /// <param name="myLabel">A label stored within this edge.</param>
        /// <param name="myEdgeInitializer">A delegate to initialize the newly created edge.</param>
        internal protected Edge(IGraph myIGraph, IVertex myOutVertex, IVertex myInVertex, EdgeId myEdgeId, String myLabel, Action<IEdge> myEdgeInitializer = null)
            : base(myIGraph, myEdgeId)
        {

            if (myOutVertex == null)
                throw new ArgumentNullException("The OutVertex must not be null!");

            if (myInVertex == null)
                throw new ArgumentNullException("The InVertex must not be null!");

            _OutVertex = myOutVertex;
            _InVertex  = myInVertex;

            // Add the label
            _Properties.Add(__Label, myLabel);

            if (myEdgeInitializer != null)
                myEdgeInitializer(this);

        }

        #endregion

        #endregion


        #region Operator overloading

        #region Operator == (myEdge1, myEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myEdge1">A Edge.</param>
        /// <param name="myEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Edge myEdge1, Edge myEdge2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(myEdge1, myEdge2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) myEdge1 == null) || ((Object) myEdge2 == null))
                return false;

            return myEdge1.Equals(myEdge2);

        }

        #endregion

        #region Operator != (myEdge1, myEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myEdge1">A Edge.</param>
        /// <param name="myEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Edge myEdge1, Edge myEdge2)
        {
            return !(myEdge1 == myEdge2);
        }

        #endregion

        #region Operator <  (myEdge1, myEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myEdge1">A Edge.</param>
        /// <param name="myEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (Edge myEdge1, Edge myEdge2)
        {

            // Check if myEdge1 is null
            if ((Object) myEdge1 == null)
                throw new ArgumentNullException("Parameter myEdge1 must not be null!");

            // Check if myEdge2 is null
            if ((Object) myEdge2 == null)
                throw new ArgumentNullException("Parameter myEdge2 must not be null!");

            return myEdge1.CompareTo(myEdge2) < 0;

        }

        #endregion

        #region Operator >  (myEdge1, myEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myEdge1">A Edge.</param>
        /// <param name="myEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (Edge myEdge1, Edge myEdge2)
        {

            // Check if myEdge1 is null
            if ((Object) myEdge1 == null)
                throw new ArgumentNullException("Parameter myEdge1 must not be null!");

            // Check if myEdge2 is null
            if ((Object) myEdge2 == null)
                throw new ArgumentNullException("Parameter myEdge2 must not be null!");

            return myEdge1.CompareTo(myEdge2) > 0;

        }

        #endregion

        #region Operator <= (myEdge1, myEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myEdge1">A Edge.</param>
        /// <param name="myEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (Edge myEdge1, Edge myEdge2)
        {
            return !(myEdge1 > myEdge2);
        }

        #endregion

        #region Operator >= (myEdge1, myEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myEdge1">A Edge.</param>
        /// <param name="myEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (Edge myEdge1, Edge myEdge2)
        {
            return !(myEdge1 < myEdge2);
        }

        #endregion

        #endregion

        #region IComparable<IEdge> Members

        #region CompareTo(myObject)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myObject">An object to compare with.</param>
        /// <returns>true|false</returns>
        public override Int32 CompareTo(Object myObject)
        {

            // Check if myObject is null
            if (myObject == null)
                throw new ArgumentNullException("myObject must not be null!");

            // Check if myObject can be casted to an IEdge object
            var myIEdge = myObject as IEdge;
            if ((Object) myIEdge == null)
                throw new ArgumentException("myObject is not of type IEdge!");

            return CompareTo(myIEdge);

        }

        #endregion

        #region CompareTo(myIGenericEdge)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myIGenericEdge">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(IGenericEdge<EdgeId> myIGenericEdge)
        {

            // Check if myIEdge is null
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

        //    // Check if myIEdge is null
        //    if (myIGenericEdgeIPropertiesString == null)
        //        throw new ArgumentNullException("myIGenericEdgeIPropertiesString must not be null!");

        //    return Id.CompareTo(myIGenericEdgeIPropertiesString.Id);

        //}

        //#endregion

        #endregion

        #region IEquatable<IEdge> Members

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

            var _Object = myObject as IEdge;
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
        public Boolean Equals(IGenericEdge<EdgeId> myIGenericEdge)
        {

            if ((Object) myIGenericEdge == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return (this.Id == myIGenericEdge.Id);

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

    }

}
