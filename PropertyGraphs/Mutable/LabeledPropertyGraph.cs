/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <code@ahzf.de>
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

#endregion

namespace de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable
{

    /// <summary>
    /// A class-based in-memory implementation of a labeled property graph.
    /// </summary>
    /// <typeparam name="TVertexLabel">The type of the vertex type.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TMultiEdgeLabel">The type of the multiedge label.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    public class LabeledPropertyGraph<TVertexLabel, TEdgeLabel, TMultiEdgeLabel, THyperEdgeLabel>

                     : GenericPropertyGraph<UInt64, Int64, TVertexLabel,    String, Object,   // Vertex definition
                                             UInt64, Int64, TEdgeLabel,      String, Object,   // Edge definition
                                             UInt64, Int64, TMultiEdgeLabel, String, Object,   // MultiEdge definition
                                             UInt64, Int64, THyperEdgeLabel, String, Object>,  // Hyperedge definition

                       ILabeledPropertyGraph<TVertexLabel, TEdgeLabel, TMultiEdgeLabel, THyperEdgeLabel>

        where TVertexLabel    : IEquatable<TVertexLabel>,    IComparable<TVertexLabel>,    IComparable
        where TEdgeLabel      : IEquatable<TEdgeLabel>,      IComparable<TEdgeLabel>,      IComparable
        where TMultiEdgeLabel : IEquatable<TMultiEdgeLabel>, IComparable<TMultiEdgeLabel>, IComparable
        where THyperEdgeLabel : IEquatable<THyperEdgeLabel>, IComparable<THyperEdgeLabel>, IComparable

    {

        #region Delegates

        #region IdCreatorDelegate()

        /// <summary>
        /// A delegate for creating a new TId
        /// (if no one was provided by the user).
        /// </summary>
        /// <returns>A valid TId.</returns>
        public delegate UInt64 IdCreatorDelegate();

        #endregion

        #region RevisionIdCreatorDelegate()

        /// <summary>
        /// A delegate for creating a new TRevisionId
        /// (if no one was provided by the user).
        /// </summary>
        /// <returns>A valid TRevisionId.</returns>
        public delegate Int64 RevisionIdCreatorDelegate();

        #endregion

        #endregion

        #region Constructor(s)

        #region TypedPropertyGraph()

        /// <summary>
        /// Created a new class-based in-memory implementation of a simplified generic property graph.
        /// </summary>
        public LabeledPropertyGraph(UInt64                    GraphId,
                                    String                    IdKey,
                                    IdCreatorDelegate         IdCreatorDelegate,
                                    String                    RevisionIdKey,
                                    RevisionIdCreatorDelegate RevisionIdCreatorDelegate,
                                    GraphInitializer<UInt64, Int64, TVertexLabel,    String, Object,
                                                     UInt64, Int64, TEdgeLabel,      String, Object,
                                                     UInt64, Int64, TMultiEdgeLabel, String, Object,
                                                     UInt64, Int64, THyperEdgeLabel, String, Object> GraphInitializer = null)

            : base (IdKey,
                    RevisionIdKey,
                    GraphId,

                    // Create a new vertex
                    IdKey,
                    RevisionIdKey,
                    (Graph) => IdCreatorDelegate(),

                    // Create a new edge
                    IdKey,
                    RevisionIdKey,
                    (Graph) => IdCreatorDelegate(),

                    // Create a new multiedge
                    IdKey,
                    RevisionIdKey,
                    (Graph) => IdCreatorDelegate(),

                    // Create a new HyperEdge
                    IdKey,
                    RevisionIdKey,
                    (Graph) => IdCreatorDelegate(),

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
        public static Boolean operator == (LabeledPropertyGraph<TVertexLabel, TEdgeLabel, TMultiEdgeLabel, THyperEdgeLabel> PropertyGraph1,
                                           LabeledPropertyGraph<TVertexLabel, TEdgeLabel, TMultiEdgeLabel, THyperEdgeLabel> PropertyGraph2)
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
        public static Boolean operator != (LabeledPropertyGraph<TVertexLabel, TEdgeLabel, TMultiEdgeLabel, THyperEdgeLabel> PropertyGraph1,
                                           LabeledPropertyGraph<TVertexLabel, TEdgeLabel, TMultiEdgeLabel, THyperEdgeLabel> PropertyGraph2)
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
        public static Boolean operator < (LabeledPropertyGraph<TVertexLabel, TEdgeLabel, TMultiEdgeLabel, THyperEdgeLabel> PropertyGraph1,
                                          LabeledPropertyGraph<TVertexLabel, TEdgeLabel, TMultiEdgeLabel, THyperEdgeLabel> PropertyGraph2)
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
        public static Boolean operator <= (LabeledPropertyGraph<TVertexLabel, TEdgeLabel, TMultiEdgeLabel, THyperEdgeLabel> PropertyGraph1,
                                           LabeledPropertyGraph<TVertexLabel, TEdgeLabel, TMultiEdgeLabel, THyperEdgeLabel> PropertyGraph2)
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
        public static Boolean operator > (LabeledPropertyGraph<TVertexLabel, TEdgeLabel, TMultiEdgeLabel, THyperEdgeLabel> PropertyGraph1,
                                          LabeledPropertyGraph<TVertexLabel, TEdgeLabel, TMultiEdgeLabel, THyperEdgeLabel> PropertyGraph2)
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
        public static Boolean operator >= (LabeledPropertyGraph<TVertexLabel, TEdgeLabel, TMultiEdgeLabel, THyperEdgeLabel> PropertyGraph1,
                                           LabeledPropertyGraph<TVertexLabel, TEdgeLabel, TMultiEdgeLabel, THyperEdgeLabel> PropertyGraph2)
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
            var PropertyGraph = Object as LabeledPropertyGraph<TVertexLabel, TEdgeLabel, TMultiEdgeLabel, THyperEdgeLabel>;
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

}
