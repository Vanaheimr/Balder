///*
// * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
// * This file is part of Blueprints.NET
// *
// * Licensed under the Apache License, Version 2.0 (the "License");
// * you may not use this file except in compliance with the License.
// * You may obtain a copy of the License at
// *
// *     http://www.apache.org/licenses/LICENSE-2.0
// *
// * Unless required by applicable law or agreed to in writing, software
// * distributed under the License is distributed on an "AS IS" BASIS,
// * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// * See the License for the specific language governing permissions and
// * limitations under the License.
// */

//#region Usings

//using System;
//using System.Collections;
//using System.Collections.Generic;

//using de.ahzf.blueprints.Datastructures;

//#endregion

//namespace de.ahzf.blueprints.InMemoryGraph
//{

//    /// <summary>
//    /// A hyperedge links multiple vertices. Along with its key/value properties,
//    /// a hyperedge has both a directionality and a label.
//    /// The directionality determines which vertex is the tail vertex
//    /// (out vertex) and which vertices are the head vertices (in vertices).
//    /// The hyperedge label determines the type of relationship that exists
//    /// between these vertices.
//    /// Diagrammatically, outVertex ---label---> inVertex1.
//    ///                                      \--> inVertex2.
//    /// </summary>
//    public class HyperEdge : AProperties<HyperEdgeId, RevisionId, String, Object, IDictionary<String, Object>>,
//                             IPropertyHyperEdge<String, Object, IDictionary<String, Object>,
//                                           VertexId,    IProperties<String, Object, IDictionary<String, Object>>,
//                                           EdgeId,      IProperties<String, Object, IDictionary<String, Object>>,
//                                           HyperEdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                                           RevisionId>
//    {

//        #region Data

//        /// <summary>
//        /// The vertex at the tail of this hyperedge.
//        /// </summary>
//        protected readonly IGenericVertex<VertexId, IProperties<String, Object, IDictionary<String, Object>>,
//                              EdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                              HyperEdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                              RevisionId> _OutVertex;

//        /// <summary>
//        /// The vertices at the head of this hyperedge.
//        /// </summary>
//        protected readonly IEnumerable<IGenericVertex<VertexId, IProperties<String, Object, IDictionary<String, Object>>,
//                              EdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                              HyperEdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                              RevisionId>> _InVertices;

//        /// <summary>
//        /// The property key of the Label property
//        /// </summary>
//        private   const    String  __Label = "Label";

//        #endregion

//        #region Properties

//        // Edge properties

//        #region Id

//        /// <summary>
//        /// An identifier that is unique to its inheriting class.
//        /// All vertices of a graph must have unique identifiers.
//        /// All edges of a graph must have unique identifiers.
//        /// </summary>
//        public new HyperEdgeId Id
//        {
//            get
//            {

//                Object _Object = null;

//                if (_Properties.TryGetValue(_IdKey, out _Object))
//                    return _Object as HyperEdgeId;

//                return null;

//            }
//        }

//        #endregion

//        #region Label

//        /// <summary>
//        /// The label associated with this edge.
//        /// </summary>
//        public String Label
//        {
//            get
//            {

//                Object _Object = null;

//                if (_Properties.TryGetValue(__Label, out _Object))
//                    return _Object as String;

//                return null;

//            }
//        }

//        #endregion


//        // Links to the associated vertices

//        #region OutVertex

//        /// <summary>
//        /// The vertex at the tail of this edge.
//        /// </summary>
//        public IGenericVertex<VertexId, IProperties<String, Object, IDictionary<String, Object>>,
//                              EdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                              HyperEdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                              RevisionId> OutVertex
//        {
//            get
//            {
//                return _OutVertex;
//            }
//        }

//        #endregion

//        #region InVertices

//        /// <summary>
//        /// The vertices at the head of this hyperedge.
//        /// </summary>
//        public IEnumerable<IGenericVertex<VertexId, IProperties<String, Object, IDictionary<String, Object>>,
//                              EdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                              HyperEdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                              RevisionId>> InVertices
//        {
//            get
//            {
//                return _InVertices;
//            }
//        }

//        #endregion

//        #endregion

//        #region Constructor(s)

//        #region Edge(myIGraph, myOutVertex, myInVertices, myHyperEdgeId, myHyperEdgeInitializer = null)

//        /// <summary>
//        /// Creates a new edge.
//        /// </summary>
//        /// <param name="myIGraph">The associated graph.</param>
//        /// <param name="myOutVertex">The vertex at the tail of the edge.</param>
//        /// <param name="myInVertices">The vertices at the head of the hyperedge.</param>
//        /// <param name="myHyperEdgeId">The identification of this hyperedge.</param>
//        /// <param name="myLabel">A label stored within this hyperedge.</param>
//        /// <param name="myHyperEdgeInitializer">A delegate to initialize the newly created hyperedge.</param>
//        internal protected HyperEdge(IGenericGraph<

//                                        // Vertex definition
//                                        IPropertyVertex<String, Object, IDictionary<String, Object>,
//                                          VertexId, IProperties<String, Object, IDictionary<String, Object>>,
//                                          EdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                                          HyperEdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                                          RevisionId>,
//                                        VertexId,
//                                        IProperties<String, Object, IDictionary<String, Object>>,

//                                        // Edge definition
//                                        IPropertyEdge<String, Object, IDictionary<String, Object>,
//                                          VertexId, IProperties<String, Object, IDictionary<String, Object>>,
//                                          EdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                                          HyperEdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                                          RevisionId>,
//                                        VertexId,
//                                        IProperties<String, Object, IDictionary<String, Object>>,

//                                        // Hyperedge definition
//                                        IPropertyHyperEdge<String, Object, IDictionary<String, Object>,
//                                          VertexId, IProperties<String, Object, IDictionary<String, Object>>,
//                                          EdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                                          HyperEdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                                          RevisionId>,
//                                        VertexId,
//                                        IProperties<String, Object, IDictionary<String, Object>>,

//                                        // RevisionId definition
//                                        RevisionId, Object> myIGraph,
//                               IGenericVertex<VertexId, IProperties<String, Object, IDictionary<String, Object>>,
//                              EdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                              HyperEdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                              RevisionId> myOutVertex,

//                               IEnumerable<IGenericVertex<VertexId, IProperties<String, Object, IDictionary<String, Object>>,
//                              EdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                              HyperEdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                              RevisionId>> myInVertices,

//                               HyperEdgeId myHyperEdgeId, String myLabel, Action<IPropertyHyperEdge<String, Object, IDictionary<String, Object>,
//                                      VertexId, IProperties<String, Object, IDictionary<String, Object>>,
//                                      EdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                                      HyperEdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                                      RevisionId>> myHyperEdgeInitializer = null)

//            : base(myIGraph, myHyperEdgeId, "Id", "RevisionId", () => new Dictionary<String, Object>())
//        {

//            if (myOutVertex == null)
//                throw new ArgumentNullException("The OutVertex must not be null!");

//            if (myInVertices == null)
//                throw new ArgumentNullException("The InVertices must not be null!");

//            _OutVertex  = myOutVertex;
//            _InVertices = myInVertices;

//            // Add the label
//            _Properties.Add(__Label, myLabel);

//            if (myHyperEdgeInitializer != null)
//                myHyperEdgeInitializer(this);

//        }

//        #endregion

//        #endregion


//        #region Operator overloading

//        #region Operator == (myHyperEdge1, myHyperEdge2)

//        /// <summary>
//        /// Compares two instances of this object.
//        /// </summary>
//        /// <param name="myHyperEdge1">A HyperEdge.</param>
//        /// <param name="myHyperEdge2">Another HyperEdge.</param>
//        /// <returns>true|false</returns>
//        public static Boolean operator == (HyperEdge myHyperEdge1, HyperEdge myHyperEdge2)
//        {

//            // If both are null, or both are same instance, return true.
//            if (Object.ReferenceEquals(myHyperEdge1, myHyperEdge2))
//                return true;

//            // If one is null, but not both, return false.
//            if (((Object) myHyperEdge1 == null) || ((Object) myHyperEdge2 == null))
//                return false;

//            return myHyperEdge1.Equals(myHyperEdge2);

//        }

//        #endregion

//        #region Operator != (myHyperEdge1, myHyperEdge2)

//        /// <summary>
//        /// Compares two instances of this object.
//        /// </summary>
//        /// <param name="myHyperEdge1">A HyperEdge.</param>
//        /// <param name="myHyperEdge2">Another HyperEdge.</param>
//        /// <returns>true|false</returns>
//        public static Boolean operator != (HyperEdge myHyperEdge1, HyperEdge myHyperEdge2)
//        {
//            return !(myHyperEdge1 == myHyperEdge2);
//        }

//        #endregion

//        #region Operator <  (myHyperEdge1, myHyperEdge2)

//        /// <summary>
//        /// Compares two instances of this object.
//        /// </summary>
//        /// <param name="myHyperEdge1">A HyperEdge.</param>
//        /// <param name="myHyperEdge2">Another HyperEdge.</param>
//        /// <returns>true|false</returns>
//        public static Boolean operator < (HyperEdge myHyperEdge1, HyperEdge myHyperEdge2)
//        {

//            // Check if myHyperEdge1 is null
//            if ((Object) myHyperEdge1 == null)
//                throw new ArgumentNullException("Parameter myHyperEdge1 must not be null!");

//            // Check if myHyperEdge2 is null
//            if ((Object) myHyperEdge2 == null)
//                throw new ArgumentNullException("Parameter myHyperEdge2 must not be null!");

//            return myHyperEdge1.CompareTo(myHyperEdge2) < 0;

//        }

//        #endregion

//        #region Operator >  (myHyperEdge1, myHyperEdge2)

//        /// <summary>
//        /// Compares two instances of this object.
//        /// </summary>
//        /// <param name="myHyperEdge1">A HyperEdge.</param>
//        /// <param name="myHyperEdge2">Another HyperEdge.</param>
//        /// <returns>true|false</returns>
//        public static Boolean operator > (HyperEdge myHyperEdge1, HyperEdge myHyperEdge2)
//        {

//            // Check if myHyperEdge1 is null
//            if ((Object) myHyperEdge1 == null)
//                throw new ArgumentNullException("Parameter myHyperEdge1 must not be null!");

//            // Check if myHyperEdge2 is null
//            if ((Object) myHyperEdge2 == null)
//                throw new ArgumentNullException("Parameter myHyperEdge2 must not be null!");

//            return myHyperEdge1.CompareTo(myHyperEdge2) > 0;

//        }

//        #endregion

//        #region Operator <= (myHyperEdge1, myHyperEdge2)

//        /// <summary>
//        /// Compares two instances of this object.
//        /// </summary>
//        /// <param name="myHyperEdge1">A HyperEdge.</param>
//        /// <param name="myHyperEdge2">Another HyperEdge.</param>
//        /// <returns>true|false</returns>
//        public static Boolean operator <= (HyperEdge myHyperEdge1, HyperEdge myHyperEdge2)
//        {
//            return !(myHyperEdge1 > myHyperEdge2);
//        }

//        #endregion

//        #region Operator >= (myHyperEdge1, myHyperEdge2)

//        /// <summary>
//        /// Compares two instances of this object.
//        /// </summary>
//        /// <param name="myHyperEdge1">A HyperEdge.</param>
//        /// <param name="myHyperEdge2">Another HyperEdge.</param>
//        /// <returns>true|false</returns>
//        public static Boolean operator >= (HyperEdge myHyperEdge1, HyperEdge myHyperEdge2)
//        {
//            return !(myHyperEdge1 < myHyperEdge2);
//        }

//        #endregion

//        #endregion

//        #region IComparable<IHyperEdge> Members

//        #region CompareTo(myObject)

//        /// <summary>
//        /// Compares two instances of this object.
//        /// </summary>
//        /// <param name="myObject">An object to compare with.</param>
//        /// <returns>true|false</returns>
//        public override Int32 CompareTo(Object myObject)
//        {

//            // Check if myObject is null
//            if (myObject == null)
//                throw new ArgumentNullException("myObject must not be null!");

//            // Check if myObject can be casted to an IHyperEdge object
//            var myIHyperEdge = myObject as IPropertyHyperEdge<String, Object, IDictionary<String, Object>,
//                                      VertexId, IProperties<String, Object, IDictionary<String, Object>>,
//                                      EdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                                      HyperEdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                                      RevisionId>;
//            if ((Object) myIHyperEdge == null)
//                throw new ArgumentException("myObject is not of type IHyperEdge!");

//            return CompareTo(myIHyperEdge);

//        }

//        #endregion

//        //#region CompareTo(myIGenericHyperEdge)

//        ///// <summary>
//        ///// Compares two instances of this object.
//        ///// </summary>
//        ///// <param name="myIGenericHyperEdge">An object to compare with.</param>
//        ///// <returns>true|false</returns>
//        //public Int32 CompareTo(IGenericHyperEdge<HyperEdgeId> myIGenericHyperEdge)
//        //{

//        //    // Check if myIHyperEdge is null
//        //    if (myIGenericHyperEdge == null)
//        //        throw new ArgumentNullException("myIGenericHyperEdge must not be null!");

//        //    return Id.CompareTo(myIGenericHyperEdge.Id);

//        //}

//        //#endregion

//        #region CompareTo(myIGenericHyperEdgeIPropertiesString)

//        /// <summary>
//        /// Compares two instances of this object.
//        /// </summary>
//        /// <param name="myIGenericHyperEdgeIPropertiesString">An object to compare with.</param>
//        /// <returns>true|false</returns>
//        public Int32 CompareTo(IPropertyHyperEdge<String, Object, IDictionary<String, Object>,
//                                      VertexId, IProperties<String, Object, IDictionary<String, Object>>,
//                                      EdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                                      HyperEdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                                      RevisionId> myIGenericHyperEdgeIPropertiesString)
//        {

//            // Check if myIHyperEdge is null
//            if (myIGenericHyperEdgeIPropertiesString == null)
//                throw new ArgumentNullException("myIGenericHyperEdgeIPropertiesString must not be null!");

//            return Id.CompareTo(myIGenericHyperEdgeIPropertiesString.Id);

//        }

//        #endregion

//        #endregion

//        #region IEquatable<IHyperEdge> Members

//        #region Equals(myObject)

//        /// <summary>
//        /// Compares two instances of this object.
//        /// </summary>
//        /// <param name="myObject">An object to compare with.</param>
//        /// <returns>true|false</returns>
//        public override Boolean Equals(Object myObject)
//        {

//            if (myObject == null)
//                return false;

//            var _Object = myObject as IPropertyHyperEdge<String, Object, IDictionary<String, Object>,
//                                      VertexId, IProperties<String, Object, IDictionary<String, Object>>,
//                                      EdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                                      HyperEdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                                      RevisionId>;
//            if (_Object != null)
//                return Equals(_Object);

//            return false;

//        }

//        #endregion

//        #region Equals(myIGenericHyperEdge)

//        /// <summary>
//        /// Compares two instances of this object.
//        /// </summary>
//        /// <param name="myIGenericHyperEdge">An object to compare with.</param>
//        /// <returns>true|false</returns>
//        public Boolean Equals(IPropertyHyperEdge<String, Object, IDictionary<String, Object>,
//                                      VertexId, IProperties<String, Object, IDictionary<String, Object>>,
//                                      EdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                                      HyperEdgeId, IProperties<String, Object, IDictionary<String, Object>>,
//                                      RevisionId> myIGenericHyperEdge)
//        {

//            if ((Object) myIGenericHyperEdge == null)
//                return false;

//            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
//            return (this.Id == myIGenericHyperEdge.Id);

//        }

//        #endregion

//        //#region Equals(myIGenericHyperEdgeIPropertiesString)

//        ///// <summary>
//        ///// Compares two instances of this object.
//        ///// </summary>
//        ///// <param name="myIGenericHyperEdgeIPropertiesString">An object to compare with.</param>
//        ///// <returns>true|false</returns>
//        //public Boolean Equals(IGenericHyperEdge<IProperties<String>> myIGenericHyperEdgeIPropertiesString)
//        //{

//        //    if ((Object) myIGenericHyperEdgeIPropertiesString == null)
//        //        return false;

//        //    //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
//        //    return (this.Id == myIGenericHyperEdgeIPropertiesString.Id);

//        //}

//        //#endregion

//        #endregion

//        #region GetHashCode()

//        /// <summary>
//        /// Return the HashCode of this object.
//        /// </summary>
//        /// <returns>The HashCode of this object.</returns>
//        public override Int32 GetHashCode()
//        {

//            if (Id == null)
//                return 0;

//            return Id.GetHashCode();

//        }

//        #endregion











//        public IProperties<string, object, IDictionary<string, object>> data
//        {
//            get
//            {
//                throw new NotImplementedException();
//            }
//            set
//            {
//                throw new NotImplementedException();
//            }
//        }



//    }

//}
