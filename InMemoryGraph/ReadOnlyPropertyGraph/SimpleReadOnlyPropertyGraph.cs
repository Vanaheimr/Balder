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
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using de.ahzf.Blueprints.PropertyGraph.ReadOnly;

#endregion

namespace de.ahzf.Blueprints.PropertyGraph.InMemory.ReadOnly
{

    #region SimpleReadOnlyPropertyGraph<TId, TRevisionId, TKey, TValue>

    /// <summary>
    /// A simplified in-memory implementation of a generic property graph.
    /// </summary>
    /// <typeparam name="TId">The type of the graph element identifiers.</typeparam>
    /// <typeparam name="TRevisionId">The type of the graph element revision identifiers.</typeparam>
    /// <typeparam name="TLabel">The type of the (hyper-)edge labels.</typeparam>
    /// <typeparam name="TKey">The type of the graph element property keys.</typeparam>
    /// <typeparam name="TValue">The type of the graph element property values.</typeparam>
    public class SimpleReadOnlyPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue>
                     : ReadOnlyPropertyGraph<TId, TRevisionId,         TKey, TValue, IDictionary<TKey, TValue>,  // Vertex definition
                                             TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>,  // Edge definition
                                             TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>>  // Hyperedge definition

        where TId         : IEquatable<TId>,         IComparable<TId>,         IComparable, TValue
        where TRevisionId : IEquatable<TRevisionId>, IComparable<TRevisionId>, IComparable, TValue
        where TKey        : IEquatable<TKey>,        IComparable<TKey>,        IComparable
        where TLabel      : IEquatable<TLabel>,      IComparable<TLabel>,      IComparable

    {

        #region Constructor(s)

        #region SimpleReadOnlyPropertyGraph()

        /// <summary>
        /// Created a new simplyfied in-memory property graph.
        /// </summary>
        /// <param name="GraphId">A unique identification for this graph.</param>
        /// <param name="SimplePropertyGraph">A simple property graph to copy the data from.</param>
        public SimpleReadOnlyPropertyGraph(TId GraphId,
                                           SimplePropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> SimplePropertyGraph,
                                     UInt64  NumberOfVertices   = 0,
                                     Boolean SyncedVertexIds    = false,
                                     UInt64  NumberOfEdges      = 0,
                                     Boolean SyncedEdgeIds      = false,
                                     UInt64  NumberOfHyperEdges = 0,
                                     Boolean SyncedHyperEdgeIds = false)
            : base(GraphId,
                   SimplePropertyGraph,
                   (_Graph, _Vertex) => new ReadOnlyPropertyVertex<TId, TRevisionId,         TKey, TValue, IDictionary<TKey, TValue>,
                                                                   TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>,
                                                                   TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>,
                                                                   ICollection<IReadOnlyPropertyEdge<TId, TRevisionId,         TKey, TValue,
                                                                                                     TId, TRevisionId, TLabel, TKey, TValue,
                                                                                                     TId, TRevisionId, TLabel, TKey, TValue>>,
                                                                   ICollection<IReadOnlyPropertyHyperEdge<TId, TRevisionId,         TKey, TValue,
                                                                                                          TId, TRevisionId, TLabel, TKey, TValue,
                                                                                                          TId, TRevisionId, TLabel, TKey, TValue>>>(
                                                                                                     _Graph,
                                                                                                     _Vertex,
                                                                                                     () => new Dictionary<TKey, TValue>(),
                                                                                                     () => new HashSet<IReadOnlyPropertyEdge<TId, TRevisionId, TKey, TValue,
                                                                                                                                                                 TId, TRevisionId, TLabel, TKey, TValue,
                                                                                                                                                                 TId, TRevisionId, TLabel, TKey, TValue>>()),
            (_Graph, _Edge) => new ReadOnlyPropertyEdge<TId, TRevisionId,         TKey, TValue, IDictionary<TKey, TValue>,
                                                        TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>,
                                                        TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>>(_Graph, _Edge, () => new Dictionary<TKey, TValue>()),

            NumberOfVertices, SyncedVertexIds, NumberOfEdges, SyncedEdgeIds, NumberOfHyperEdges, SyncedHyperEdgeIds)

        { }

        #endregion

        #endregion

        
        #region Operator overloading

        #region Operator == (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two property graphs for equality.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public static Boolean operator == (SimpleReadOnlyPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph1,
                                           SimpleReadOnlyPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(PropertyGraph1, PropertyGraph2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) PropertyGraph1 == null) || ((Object) PropertyGraph2 == null))
                return false;

            return PropertyGraph1.Equals(PropertyGraph2);

        }

        #endregion

        #region Operator != (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two property graphs for inequality.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>False if both match; True otherwise.</returns>
        public static Boolean operator != (SimpleReadOnlyPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph1,
                                           SimpleReadOnlyPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph2)
        {
            return !(PropertyGraph1 == PropertyGraph2);
        }

        #endregion

        #region Operator <  (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (SimpleReadOnlyPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph1,
                                          SimpleReadOnlyPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph2)
        {

            if ((Object) PropertyGraph1 == null)
                throw new ArgumentNullException("The given PropertyGraph1 must not be null!");

            if ((Object) PropertyGraph2 == null)
                throw new ArgumentNullException("The given PropertyGraph2 must not be null!");

            return PropertyGraph1.CompareTo(PropertyGraph2) < 0;

        }

        #endregion

        #region Operator <= (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (SimpleReadOnlyPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph1,
                                           SimpleReadOnlyPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph2)
        {
            return !(PropertyGraph1 > PropertyGraph2);
        }

        #endregion

        #region Operator >  (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (SimpleReadOnlyPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph1,
                                          SimpleReadOnlyPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph2)
        {

            if ((Object) PropertyGraph1 == null)
                throw new ArgumentNullException("The given PropertyGraph1 must not be null!");

            if ((Object) PropertyGraph2 == null)
                throw new ArgumentNullException("The given  PropertyGraph2 must not be null!");

            return PropertyGraph1.CompareTo(PropertyGraph2) > 0;

        }

        #endregion

        #region Operator >= (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (SimpleReadOnlyPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph1,
                                           SimpleReadOnlyPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph2)
        {
            return !(PropertyGraph1 < PropertyGraph2);
        }

        #endregion

        #endregion

        #region IEquatable Members

        #region Equals(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public override Boolean Equals(Object Object)
        {

            if (Object == null)
                return false;

            // Check if the given object can be casted to a SimpleReadOnlyPropertyGraph
            var PropertyGraph = Object as SimpleReadOnlyPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue>;
            if ((Object) PropertyGraph == null)
                return false;

            return this.Equals(PropertyGraph);

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

    #endregion

    #region SimpleReadOnlyPropertyGraph

    /// <summary>
    /// A simplified in-memory implementation of a property graph.
    /// </summary>
    public class SimpleReadOnlyPropertyGraph : SimpleReadOnlyPropertyGraph<UInt64, Int64, String, String, Object>
    {

        #region Constructor(s)

        #region SimpleReadOnlyPropertyGraph(GraphId, GraphInitializer = null)

        /// <summary>
        /// Created a new simple in-memory property graph.
        /// </summary>
        /// <param name="GraphId">A unique identification for this graph.</param>
        /// <param name="SimplePropertyGraph">A simple property graph to copy the data from.</param>
        public SimpleReadOnlyPropertyGraph(UInt64 GraphId,
                                           SimplePropertyGraph<UInt64, Int64, String, String, Object> SimplePropertyGraph,
                                           UInt64  NumberOfVertices   = 0,
                                           Boolean SyncedVertexIds    = false,
                                           UInt64  NumberOfEdges      = 0,
                                           Boolean SyncedEdgeIds      = false,
                                           UInt64  NumberOfHyperEdges = 0,
                                           Boolean SyncedHyperEdgeIds = false)
            : base(GraphId, SimplePropertyGraph, NumberOfVertices, SyncedVertexIds, NumberOfEdges, SyncedEdgeIds, NumberOfHyperEdges, SyncedHyperEdgeIds)
        { }

        #endregion

        #endregion


        #region (private) NewId

        private static Int64 _NewId;

        /// <summary>
        /// Return a new random Id.
        /// </summary>
        private static UInt64 NewId
        {
            get
            {
                Interlocked.Increment(ref _NewId);
                return (UInt64) _NewId - 1;
            }
        }

        #endregion

        #region (private) NewRevisionId

        /// <summary>
        /// Return a new random RevisionId.
        /// </summary>
        private static Int64 NewRevisionId
        {
            get
            {
                return DateTime.Now.Ticks;
            }
        }

        #endregion


        #region Operator overloading

        #region Operator == (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two property graphs for equality.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public static Boolean operator == (SimpleReadOnlyPropertyGraph PropertyGraph1,
                                           SimpleReadOnlyPropertyGraph PropertyGraph2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(PropertyGraph1, PropertyGraph2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) PropertyGraph1 == null) || ((Object) PropertyGraph2 == null))
                return false;

            return PropertyGraph1.Equals(PropertyGraph2);

        }

        #endregion

        #region Operator != (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two property graphs for inequality.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>False if both match; True otherwise.</returns>
        public static Boolean operator != (SimpleReadOnlyPropertyGraph PropertyGraph1,
                                           SimpleReadOnlyPropertyGraph PropertyGraph2)
        {
            return !(PropertyGraph1 == PropertyGraph2);
        }

        #endregion

        #region Operator <  (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (SimpleReadOnlyPropertyGraph PropertyGraph1,
                                          SimpleReadOnlyPropertyGraph PropertyGraph2)
        {

            if ((Object) PropertyGraph1 == null)
                throw new ArgumentNullException("The given PropertyGraph1 must not be null!");

            if ((Object) PropertyGraph2 == null)
                throw new ArgumentNullException("The given PropertyGraph2 must not be null!");

            return PropertyGraph1.CompareTo(PropertyGraph2) < 0;

        }

        #endregion

        #region Operator <= (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (SimpleReadOnlyPropertyGraph PropertyGraph1,
                                           SimpleReadOnlyPropertyGraph PropertyGraph2)
        {
            return !(PropertyGraph1 > PropertyGraph2);
        }

        #endregion

        #region Operator >  (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (SimpleReadOnlyPropertyGraph PropertyGraph1,
                                          SimpleReadOnlyPropertyGraph PropertyGraph2)
        {

            if ((Object) PropertyGraph1 == null)
                throw new ArgumentNullException("The given PropertyGraph1 must not be null!");

            if ((Object) PropertyGraph2 == null)
                throw new ArgumentNullException("The given  PropertyGraph2 must not be null!");

            return PropertyGraph1.CompareTo(PropertyGraph2) > 0;

        }

        #endregion

        #region Operator >= (PropertyGraph1, PropertyGraph2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyGraph1">A graph.</param>
        /// <param name="PropertyGraph2">Another graph.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (SimpleReadOnlyPropertyGraph PropertyGraph1,
                                           SimpleReadOnlyPropertyGraph PropertyGraph2)
        {
            return !(PropertyGraph1 < PropertyGraph2);
        }

        #endregion

        #endregion

        #region IEquatable Members

        #region Equals(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public override Boolean Equals(Object Object)
        {

            if (Object == null)
                return false;

            // Check if the given object can be casted to a SimpleReadOnlyPropertyGraph
            var PropertyGraph = Object as SimpleReadOnlyPropertyGraph;
            if ((Object) PropertyGraph == null)
                return false;

            return this.Equals(PropertyGraph);

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

    #endregion

}
