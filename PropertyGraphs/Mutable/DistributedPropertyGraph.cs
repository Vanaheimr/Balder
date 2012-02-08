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
using de.ahzf.Illias.Commons;

#endregion

namespace de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable
{

    /// <summary>
    /// A class-based in-memory implementation of a distributed property graph.
    /// </summary>
    public class DistributedPropertyGraph

                     : GenericPropertyGraph<VertexId,    RevisionId, String, String, Object,   // Vertex definition
                                            EdgeId,      RevisionId, String, String, Object,   // Edge definition
                                            MultiEdgeId, RevisionId, String, String, Object,   // MultiEdge definition
                                            HyperEdgeId, RevisionId, String, String, Object>,  // Hyperedge definition

                       IDistributedPropertyGraph

    {

        #region Constructor(s)

        #region DistributedPropertyGraph()

        /// <summary>
        /// Created a new class-based in-memory implementation of a distributed property graph.
        /// (This constructor is needed for automatic activation!)
        /// </summary>
        public DistributedPropertyGraph()
            : this(VertexId.NewVertexId)
        { }

        #endregion

        #region DistributedPropertyGraph(GraphInitializer)

        /// <summary>
        /// Created a new class-based in-memory implementation of a distributed property graph.
        /// </summary>
        /// <param name="GraphInitializer">A delegate to initialize the graph.</param>
        public DistributedPropertyGraph(GraphInitializer<VertexId,    RevisionId, String, String, Object,
                                                         EdgeId,      RevisionId, String, String, Object,
                                                         MultiEdgeId, RevisionId, String, String, Object,
                                                         HyperEdgeId, RevisionId, String, String, Object> GraphInitializer)
            : this(VertexId.NewVertexId, GraphInitializer)
        { }

        #endregion

        #region DistributedPropertyGraph(GraphId, GraphInitializer = null)

        /// <summary>
        /// Created a new class-based in-memory implementation of a distributed property graph.
        /// </summary>
        /// <param name="GraphId">A unique identification for this graph.</param>
        /// <param name="GraphInitializer">A delegate to initialize the graph.</param>
        public DistributedPropertyGraph(VertexId GraphId,
                                        GraphInitializer<VertexId,    RevisionId, String, String, Object,
                                                         EdgeId,      RevisionId, String, String, Object,
                                                         MultiEdgeId, RevisionId, String, String, Object,
                                                         HyperEdgeId, RevisionId, String, String, Object> GraphInitializer = null)

            : base (GraphDBOntology.Id().Suffix,
                    GraphDBOntology.RevId().Suffix,
                    GraphId,

                    // Create a new vertex
                    GraphDBOntology.Id().Suffix,
                    GraphDBOntology.RevId().Suffix,
                    (Graph) => VertexId.NewVertexId,
           
                    // Create a new edge
                    GraphDBOntology.Id().Suffix,
                    GraphDBOntology.RevId().Suffix,
                    (Graph) => EdgeId.NewEdgeId,

                    // Create a new multiedge
                    GraphDBOntology.Id().Suffix,
                    GraphDBOntology.RevId().Suffix,
                    (Graph) => MultiEdgeId.NewMultiEdgeId,

                    // Create a new hyperedge
                    GraphDBOntology.Id().Suffix,
                    GraphDBOntology.RevId().Suffix,
                    (Graph) => HyperEdgeId.NewHyperEdgeId,

                    GraphInitializer)

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
        public static Boolean operator == (DistributedPropertyGraph PropertyGraph1,
                                           DistributedPropertyGraph PropertyGraph2)
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
        public static Boolean operator != (DistributedPropertyGraph PropertyGraph1,
                                           DistributedPropertyGraph PropertyGraph2)
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
        public static Boolean operator < (DistributedPropertyGraph PropertyGraph1,
                                          DistributedPropertyGraph PropertyGraph2)
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
        public static Boolean operator <= (DistributedPropertyGraph PropertyGraph1,
                                           DistributedPropertyGraph PropertyGraph2)
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
        public static Boolean operator > (DistributedPropertyGraph PropertyGraph1,
                                          DistributedPropertyGraph PropertyGraph2)
        {

            if ((Object) PropertyGraph1 == null)
                throw new ArgumentNullException("The given PropertyGraph1 must not be null!");

            if ((Object) PropertyGraph2 == null)
                throw new ArgumentNullException("The given PropertyGraph2 must not be null!");

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
        public static Boolean operator >= (DistributedPropertyGraph PropertyGraph1,
                                           DistributedPropertyGraph PropertyGraph2)
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

            // Check if the given object can be casted to a InMemoryPropertyGraph
            var PropertyGraph = Object as DistributedPropertyGraph;
            if ((Object)PropertyGraph == null)
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

}
