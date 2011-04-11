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

using de.ahzf.blueprints.Datastructures;

#endregion

namespace de.ahzf.blueprints.GeoGraph
{

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    public class GeoEdge : IGeoEdge
    {

        #region Data

        /// <summary>
        /// The vertex at the tail of this edge.
        /// </summary>
        protected readonly IGenericVertex<VertexId,    RevisionId, GeoCoordinate,
                                          EdgeId,      RevisionId, Distance,
                                          HyperEdgeId, RevisionId, Distance> _OutVertex;

        /// <summary>
        /// The vertex at the head of this edge.
        /// </summary>
        protected readonly IGenericVertex<VertexId,    RevisionId, GeoCoordinate,
                                          EdgeId,      RevisionId, Distance,
                                          HyperEdgeId, RevisionId, Distance> _InVertex;

        #endregion

        #region Properties

        // Edge properties

        #region Id

        /// <summary>
        /// An identifier that is unique to its inheriting class.
        /// All vertices of a graph must have unique identifiers.
        /// All edges of a graph must have unique identifiers.
        /// </summary>
        public EdgeId Id { get; private set; }

        #endregion

        public RevisionId RevisionId { get; private set; }

        #region Label

        /// <summary>
        /// The label associated with this edge.
        /// </summary>
        public String Label { get; private set; }

        #endregion

        public Distance Data { get; private set; }


        // Links to the associated vertices

        #region OutVertex

        /// <summary>
        /// The vertex at the tail of this edge.
        /// </summary>
        public IGenericVertex<VertexId,    RevisionId, GeoCoordinate,
                              EdgeId,      RevisionId, Distance,
                              HyperEdgeId, RevisionId, Distance> OutVertex
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
        public IGenericVertex<VertexId,    RevisionId, GeoCoordinate,
                              EdgeId,      RevisionId, Distance,
                              HyperEdgeId, RevisionId, Distance> InVertex
        {
            get
            {
                return _InVertex;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region GeoEdge(myIGraph, myOutVertex, myInVertex, myEdgeId, myEdgeInitializer = null)

        /// <summary>
        /// Creates a new edge.
        /// </summary>
        /// <param name="myIGraph">The associated graph.</param>
        /// <param name="myOutVertex">The vertex at the tail of the edge.</param>
        /// <param name="myInVertex">The vertex at the head of the edge.</param>
        /// <param name="myEdgeId">The identification of this vertex.</param>
        /// <param name="myLabel">A label stored within this edge.</param>
        /// <param name="myEdgeInitializer">A delegate to initialize the newly created edge.</param>
        public GeoEdge(IPropertyGraph myIGraph,
                       IGenericVertex<VertexId,    RevisionId, GeoCoordinate,
                                      EdgeId,      RevisionId, Distance,
                                      HyperEdgeId, RevisionId, Distance> myOutVertex,
                       IGenericVertex<VertexId,    RevisionId, GeoCoordinate,
                                      EdgeId,      RevisionId, Distance,
                                      HyperEdgeId, RevisionId, Distance> myInVertex,
                       EdgeId myEdgeId, String myLabel, Action<IGeoEdge> myEdgeInitializer = null)
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


        #region IGeoEdge Members

        public Distance Distance { get; set; }

        #endregion


        #region Operator overloading

        #region Operator == (myGeoEdge1, myGeoEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myGeoEdge1">A GeoEdge.</param>
        /// <param name="myGeoEdge2">Another GeoEdge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (GeoEdge myGeoEdge1, GeoEdge myGeoEdge2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(myGeoEdge1, myGeoEdge2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) myGeoEdge1 == null) || ((Object) myGeoEdge2 == null))
                return false;

            return myGeoEdge1.Equals(myGeoEdge2);

        }

        #endregion

        #region Operator != (myGeoEdge1, myGeoEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myGeoEdge1">A GeoEdge.</param>
        /// <param name="myGeoEdge2">Another GeoEdge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (GeoEdge myGeoEdge1, GeoEdge myGeoEdge2)
        {
            return !(myGeoEdge1 == myGeoEdge2);
        }

        #endregion

        #region Operator <  (myGeoEdge1, myGeoEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myGeoEdge1">A GeoEdge.</param>
        /// <param name="myGeoEdge2">Another GeoEdge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (GeoEdge myGeoEdge1, GeoEdge myGeoEdge2)
        {

            // Check if myGeoEdge1 is null
            if ((Object) myGeoEdge1 == null)
                throw new ArgumentNullException("Parameter myGeoEdge1 must not be null!");

            // Check if myGeoEdge2 is null
            if ((Object) myGeoEdge2 == null)
                throw new ArgumentNullException("Parameter myGeoEdge2 must not be null!");

            return myGeoEdge1.CompareTo(myGeoEdge2) < 0;

        }

        #endregion

        #region Operator >  (myGeoEdge1, myGeoEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myGeoEdge1">A GeoEdge.</param>
        /// <param name="myGeoEdge2">Another GeoEdge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (GeoEdge myGeoEdge1, GeoEdge myGeoEdge2)
        {

            // Check if myGeoEdge1 is null
            if ((Object) myGeoEdge1 == null)
                throw new ArgumentNullException("Parameter myGeoEdge1 must not be null!");

            // Check if myGeoEdge2 is null
            if ((Object) myGeoEdge2 == null)
                throw new ArgumentNullException("Parameter myGeoEdge2 must not be null!");

            return myGeoEdge1.CompareTo(myGeoEdge2) > 0;

        }

        #endregion

        #region Operator <= (myGeoEdge1, myGeoEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myGeoEdge1">A GeoEdge.</param>
        /// <param name="myGeoEdge2">Another GeoEdge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (GeoEdge myGeoEdge1, GeoEdge myGeoEdge2)
        {
            return !(myGeoEdge1 > myGeoEdge2);
        }

        #endregion

        #region Operator >= (myGeoEdge1, myGeoEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myGeoEdge1">A GeoEdge.</param>
        /// <param name="myGeoEdge2">Another GeoEdge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (GeoEdge myGeoEdge1, GeoEdge myGeoEdge2)
        {
            return !(myGeoEdge1 < myGeoEdge2);
        }

        #endregion

        #endregion

        #region IComparable<IGeoEdge> Members

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

            // Check if myObject can be casted to an IGeoEdge object
            var myIGeoEdge = myObject as IGeoEdge;
            if ((Object) myIGeoEdge == null)
                throw new ArgumentException("myObject is not of type IGeoEdge!");

            return CompareTo(myIGeoEdge);

        }

        #endregion

        #region CompareTo(myIGenericEdge)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myIGenericEdge">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(IGenericEdge<VertexId,    RevisionId, GeoCoordinate,
                                            EdgeId,      RevisionId, Distance,
                                            HyperEdgeId, RevisionId, Distance> myIGenericEdge)
        {

            // Check if myIGeoEdge is null
            if (myIGenericEdge == null)
                throw new ArgumentNullException("myIGenericEdge must not be null!");

            return Id.CompareTo(myIGenericEdge.Id);

        }

        #endregion

        #region CompareTo(myIGenericEdge)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myIGenericEdge">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(EdgeId myIGenericEdge)
        {

            // Check if myIGeoEdge is null
            if (myIGenericEdge == null)
                throw new ArgumentNullException("myIGenericEdge must not be null!");

            return Id.CompareTo(myIGenericEdge);

        }

        #endregion

        #endregion

        #region IEquatable<IGeoEdge> Members

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

            var _Object = myObject as IGeoEdge;
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
        public Boolean Equals(IGenericEdge<VertexId,    RevisionId, GeoCoordinate,
                                           EdgeId,      RevisionId, Distance,
                                           HyperEdgeId, RevisionId, Distance> myIGenericEdge)
        {

            if ((Object) myIGenericEdge == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return (this.Id == myIGenericEdge.Id);

        }

        #endregion

        #region Equals(myIGenericEdge)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myIGenericEdge">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(EdgeId myIGenericEdge)
        {

            if ((Object)myIGenericEdge == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return (this.Id == myIGenericEdge);

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
