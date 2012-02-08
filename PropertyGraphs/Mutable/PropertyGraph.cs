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
using System.Linq;
using System.Threading;
using System.Collections.Generic;

using de.ahzf.Illias.Commons;

#endregion

namespace de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable
{

    /// <summary>
    /// A class-based in-memory implementation of a property graph.
    /// </summary>
    public class PropertyGraph : CommonGenericPropertyGraph<UInt64, Int64, String, String, Object>,
                                 IPropertyGraph

    {

        #region Constructor(s)

        #region PropertyGraph()

        /// <summary>
        /// Created a new class-based in-memory implementation of a property graph.
        /// (This constructor is needed for automatic activation!)
        /// </summary>
        public PropertyGraph()
            : this(PropertyGraph.NewVertexId)
        { }

        #endregion

        #region PropertyGraph(GraphInitializer)

        /// <summary>
        /// Created a new class-based in-memory implementation of a property graph.
        /// </summary>
        /// <param name="GraphInitializer">A delegate to initialize the graph.</param>
        public PropertyGraph(GraphInitializer<UInt64, Int64, String, String, Object,
                                              UInt64, Int64, String, String, Object,
                                              UInt64, Int64, String, String, Object,
                                              UInt64, Int64, String, String, Object> GraphInitializer)
            : this(PropertyGraph.NewVertexId, GraphInitializer)
        { }

        #endregion

        #region PropertyGraph(GraphId, GraphInitializer = null)

        /// <summary>
        /// Created a new class-based in-memory implementation of a property graph.
        /// </summary>
        /// <param name="GraphId">A unique identification for this graph.</param>
        /// <param name="GraphInitializer">A delegate to initialize the graph.</param>
        public PropertyGraph(UInt64 GraphId,
                             GraphInitializer<UInt64, Int64, String, String, Object,
                                              UInt64, Int64, String, String, Object,
                                              UInt64, Int64, String, String, Object,
                                              UInt64, Int64, String, String, Object> GraphInitializer = null)
            : base (GraphId,

                    // TId key
                    GraphDBOntology.Id().Suffix,

                    // TId creator delegate
                    () => PropertyGraph.NewVertexId,
                    () => PropertyGraph.NewEdgeId,
                    () => PropertyGraph.NewMultiEdgeId,
                    () => PropertyGraph.NewHyperEdgeId,

                    // RevisionId key
                    GraphDBOntology.RevId().Suffix,

                    // RevisionId creator delegate
                    () => PropertyGraph.NewRevisionId,

                    GraphInitializer)

        {
            _NewVertexId    = 0;
            _NewEdgeId      = 0;
            _NewMultiEdgeId = 0;
            _NewHyperEdgeId = 0;
        }

        #endregion

        #endregion


        #region (private) NewIds

        private static Int64 _NewVertexId;
        private static Int64 _NewEdgeId;
        private static Int64 _NewMultiEdgeId;
        private static Int64 _NewHyperEdgeId;

        /// <summary>
        /// Return a new VertexId.
        /// </summary>
        private static UInt64 NewVertexId
        {
            get
            {
                var _NewLocalId = Interlocked.Increment(ref _NewVertexId);
                return (UInt64) _NewLocalId;
            }
        }

        /// <summary>
        /// Return a new NewEdgeId.
        /// </summary>
        private static UInt64 NewEdgeId
        {
            get
            {
                var _NewLocalId = Interlocked.Increment(ref _NewEdgeId);
                return (UInt64) _NewLocalId;
            }
        }

        /// <summary>
        /// Return a new NewMultiEdgeId.
        /// </summary>
        private static UInt64 NewMultiEdgeId
        {
            get
            {
                var _NewLocalId = Interlocked.Increment(ref _NewMultiEdgeId);
                return (UInt64) _NewLocalId;
            }
        }

        /// <summary>
        /// Return a new NewHyperEdgeId.
        /// </summary>
        private static UInt64 NewHyperEdgeId
        {
            get
            {
                var _NewLocalId = Interlocked.Increment(ref _NewHyperEdgeId);
                return (UInt64) _NewLocalId;
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
                return (Int64) UniqueTimestamp.Ticks;
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
        public static Boolean operator == (PropertyGraph PropertyGraph1,
                                           PropertyGraph PropertyGraph2)
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
        public static Boolean operator != (PropertyGraph PropertyGraph1,
                                           PropertyGraph PropertyGraph2)
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
        public static Boolean operator < (PropertyGraph PropertyGraph1,
                                          PropertyGraph PropertyGraph2)
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
        public static Boolean operator <= (PropertyGraph PropertyGraph1,
                                           PropertyGraph PropertyGraph2)
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
        public static Boolean operator > (PropertyGraph PropertyGraph1,
                                          PropertyGraph PropertyGraph2)
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
        public static Boolean operator >= (PropertyGraph PropertyGraph1,
                                           PropertyGraph PropertyGraph2)
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

            // Check if the given object can be casted to a SimplePropertyGraph
            var PropertyGraph = Object as PropertyGraph;
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


        // Currently not working!

        #region Vertex methods

        #region VertexById(VertexId)

        /// <summary>
        /// Return the vertex referenced by the given vertex identifier.
        /// If no vertex is referenced by the identifier return null.
        /// </summary>
        /// <param name="VertexId">A vertex identifier.</param>
        IPropertyVertex IPropertyGraph.VertexById(UInt64 VertexId)
        {
            return this.VertexById(VertexId) as IPropertyVertex;
        }

        #endregion

        #region VerticesById(params VertexIds)

        /// <summary>
        /// Return the vertices referenced by the given array of vertex identifiers.
        /// If no vertex is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="VertexIds">An array of vertex identifiers.</param>
        IEnumerable<IPropertyVertex> IPropertyGraph.VerticesById(params UInt64[] VertexIds)
        {

            return from Vertex
                   in (this as IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object>).VerticesById(VertexIds)
                   select Vertex as IPropertyVertex;

        }

        #endregion

        #region VerticesByLabel(params VertexLabels)

        /// <summary>
        /// Return an enumeration of all vertices having one of the
        /// given vertex labels.
        /// </summary>
        /// <param name="VertexLabels">An array of vertex labels.</param>
        IEnumerable<IPropertyVertex> IPropertyGraph.VerticesByLabel(params String[] VertexLabels)
        {

            return from Vertex
                   in (this as IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object>).VerticesByLabel(VertexLabels)
                   select Vertex as IPropertyVertex;

        }

        #endregion

        #region Vertices(VertexFilter = null)

        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// An optional vertex filter may be applied for filtering.
        /// </summary>
        /// <param name="VertexFilter">A delegate for vertex filtering.</param>
        IEnumerable<IPropertyVertex> IPropertyGraph.Vertices(VertexFilter<UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object> VertexFilter = null)
        {

            return from   Vertex
                   in (this as IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object>).Vertices(VertexFilter)
                   select Vertex as IPropertyVertex;

        }

        #endregion

        #endregion

        #region Edge methods

        #region EdgeById(EdgeId)

        /// <summary>
        /// Return the edge referenced by the given edge identifier.
        /// If no edge is referenced by a given identifier return null.
        /// </summary>
        /// <param name="EdgeId">An edge identifier.</param>
        IPropertyEdge IPropertyGraph.EdgeById(UInt64 EdgeId)
        {
            return this.EdgeById(EdgeId) as IPropertyEdge;
        }

        #endregion

        #region EdgesById(params EdgeIds)

        /// <summary>
        /// Return the edges referenced by the given array of edge identifiers.
        /// If no edge is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="EdgeIds">An array of edge identifiers.</param>
        IEnumerable<IPropertyEdge> IPropertyGraph.EdgesById(params UInt64[] EdgeIds)
        {

            return from Edge
                   in (this as IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object>).EdgesById(EdgeIds)
                   select Edge as IPropertyEdge;

        }

        #endregion

        #region EdgesByLabel(params EdgeLabels)

        /// <summary>
        /// Return an enumeration of all edges having one of the
        /// given edge labels.
        /// </summary>
        /// <param name="EdgeLabels">An array of edge labels.</param>
        IEnumerable<IPropertyEdge> IPropertyGraph.EdgesByLabel(params String[] EdgeLabels)
        {

            return from Edge
                   in (this as IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object>).EdgesByLabel(EdgeLabels)
                   select Edge as IPropertyEdge;

        }

        #endregion

        #region Edges(EdgeFilter = null)

        /// <summary>
        /// Get an enumeration of all edges in the graph.
        /// An optional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        IEnumerable<IPropertyEdge> IPropertyGraph.Edges(EdgeFilter<UInt64, Int64, String, String, Object,
                                                                   UInt64, Int64, String, String, Object,
                                                                   UInt64, Int64, String, String, Object,
                                                                   UInt64, Int64, String, String, Object> EdgeFilter = null)
        {

            return from Edge
                   in (this as IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object>).Edges(EdgeFilter)
                   select Edge as IPropertyEdge;

        }

        #endregion

        #endregion

    }

}
