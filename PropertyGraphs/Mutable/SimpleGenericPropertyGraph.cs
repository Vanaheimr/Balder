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

#endregion

namespace de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable
{

    /// <summary>
    /// A class-based in-memory implementation of a simplified generic property graph.
    /// </summary>
    /// <typeparam name="TId">The type of the graph element identifiers.</typeparam>
    /// <typeparam name="TRevisionId">The type of the graph element revision identifiers.</typeparam>
    /// <typeparam name="TLabel">The type of the labels.</typeparam>
    /// <typeparam name="TKey">The type of the graph element property keys.</typeparam>
    /// <typeparam name="TValue">The type of the graph element property values.</typeparam>
    public class SimpleGenericPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue>

                     : GenericPropertyGraph<// Vertex definition
                                            TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>,

                                            // Edge definition
                                            TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>,
                                                ICollection<        IPropertyEdge     <TId, TRevisionId, TLabel, TKey, TValue,
                                                                                       TId, TRevisionId, TLabel, TKey, TValue,
                                                                                       TId, TRevisionId, TLabel, TKey, TValue,
                                                                                       TId, TRevisionId, TLabel, TKey, TValue>>,

                                            // MultiEdge definition
                                            TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>,
                                                IDictionary<TLabel, IPropertyMultiEdge<TId, TRevisionId, TLabel, TKey, TValue,
                                                                                       TId, TRevisionId, TLabel, TKey, TValue,
                                                                                       TId, TRevisionId, TLabel, TKey, TValue,
                                                                                       TId, TRevisionId, TLabel, TKey, TValue>>,

                                            // Hyperedge definition
                                            TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>,
                                                IDictionary<TLabel, IPropertyHyperEdge<TId, TRevisionId, TLabel, TKey, TValue,
                                                                                       TId, TRevisionId, TLabel, TKey, TValue,
                                                                                       TId, TRevisionId, TLabel, TKey, TValue,
                                                                                       TId, TRevisionId, TLabel, TKey, TValue>>>,

                        ISimpleGenericPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue>

        where TId         : IEquatable<TId>,         IComparable<TId>,         IComparable, TValue
        where TRevisionId : IEquatable<TRevisionId>, IComparable<TRevisionId>, IComparable, TValue
        where TKey        : IEquatable<TKey>,        IComparable<TKey>,        IComparable
        where TLabel      : IEquatable<TLabel>,      IComparable<TLabel>,      IComparable

    {

        #region Delegates

        #region IdCreatorDelegate()

        /// <summary>
        /// A delegate for creating a new TId
        /// (if no one was provided by the user).
        /// </summary>
        /// <returns>A valid TId.</returns>
        public delegate TId IdCreatorDelegate();

        #endregion

        #region RevisionIdCreatorDelegate()

        /// <summary>
        /// A delegate for creating a new TRevisionId
        /// (if no one was provided by the user).
        /// </summary>
        /// <returns>A valid TRevisionId.</returns>
        public delegate TRevisionId RevisionIdCreatorDelegate();

        #endregion

        #endregion

        #region Constructor(s)

        #region SimplePropertyGraph()

        /// <summary>
        /// Created a new class-based in-memory implementation of a simplified generic property graph.
        /// </summary>
        public SimpleGenericPropertyGraph(TId                       GraphId,
                             TKey                      IdKey,
                             IdCreatorDelegate         IdCreatorDelegate,
                             TKey                      RevisionIdKey,
                             RevisionIdCreatorDelegate RevisionIdCreatorDelegate,
                             GraphInitializer<TId, TRevisionId, TLabel, TKey, TValue,
                                              TId, TRevisionId, TLabel, TKey, TValue,
                                              TId, TRevisionId, TLabel, TKey, TValue,
                                              TId, TRevisionId, TLabel, TKey, TValue> GraphInitializer = null)

            : base (GraphId,
                    IdKey,
                    RevisionIdKey,
                    () => new Dictionary<TKey, TValue>(),

                    // Create a new Vertex
                    (a) => IdCreatorDelegate(),
                    (Graph, VertexId, VertexInitializer) =>
                        new PropertyVertex<TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>,
                                           TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>, ICollection<IPropertyEdge<TId, TRevisionId, TLabel, TKey, TValue,
                                                                                                                                        TId, TRevisionId, TLabel, TKey, TValue,
                                                                                                                                        TId, TRevisionId, TLabel, TKey, TValue,
                                                                                                                                        TId, TRevisionId, TLabel, TKey, TValue>>,
                                           TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>, IDictionary<TLabel, IPropertyMultiEdge<TId, TRevisionId, TLabel, TKey, TValue,
                                                                                                                                                     TId, TRevisionId, TLabel, TKey, TValue,
                                                                                                                                                     TId, TRevisionId, TLabel, TKey, TValue,
                                                                                                                                                     TId, TRevisionId, TLabel, TKey, TValue>>,
                                           TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>, IDictionary<TLabel, IPropertyHyperEdge<TId, TRevisionId, TLabel, TKey, TValue,
                                                                                                                                                     TId, TRevisionId, TLabel, TKey, TValue,
                                                                                                                                                     TId, TRevisionId, TLabel, TKey, TValue,
                                                                                                                                                     TId, TRevisionId, TLabel, TKey, TValue>>>
                            (Graph, VertexId, IdKey, RevisionIdKey,
                             () => new Dictionary<TKey, TValue>(),
                             () => new HashSet<IPropertyEdge<TId, TRevisionId, TLabel, TKey, TValue,
                                                             TId, TRevisionId, TLabel, TKey, TValue,
                                                             TId, TRevisionId, TLabel, TKey, TValue,
                                                             TId, TRevisionId, TLabel, TKey, TValue>>(),
                             () => new Dictionary<TLabel, IPropertyHyperEdge<TId, TRevisionId, TLabel, TKey, TValue,
                                                                             TId, TRevisionId, TLabel, TKey, TValue,
                                                                             TId, TRevisionId, TLabel, TKey, TValue,
                                                                             TId, TRevisionId, TLabel, TKey, TValue>>(),
                             VertexInitializer
                            ),

                   // Create a new Edge
                   (Graph) => IdCreatorDelegate(),
                   (Graph, OutVertex, InVertex, EdgeId, Label, EdgeInitializer) =>
                        new PropertyEdge<TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>,
                                         TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>,
                                         TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>,
                                         TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>>
                            (Graph, OutVertex, InVertex, EdgeId, Label, IdKey, RevisionIdKey,
                             () => new Dictionary<TKey, TValue>(),
                             EdgeInitializer
                            ),


                   // Create a new MultiEdge
                   (Graph) => IdCreatorDelegate(),
                   (Graph, EdgeSelector, MultiEdgeId, Label, MultiEdgeInitializer) =>
                       new PropertyMultiEdge<TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>,
                                             TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>,
                                             TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>,
                                             TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>,
                                             ICollection<IPropertyVertex<TId, TRevisionId, TLabel, TKey, TValue,
                                                                         TId, TRevisionId, TLabel, TKey, TValue,
                                                                         TId, TRevisionId, TLabel, TKey, TValue,
                                                                         TId, TRevisionId, TLabel, TKey, TValue>>>
                            (Graph, EdgeSelector, MultiEdgeId, Label, IdKey, RevisionIdKey,
                             () => new Dictionary<TKey, TValue>(),
                             () => new HashSet<IPropertyVertex<TId, TRevisionId, TLabel, TKey, TValue,
                                                               TId, TRevisionId, TLabel, TKey, TValue,
                                                               TId, TRevisionId, TLabel, TKey, TValue,
                                                               TId, TRevisionId, TLabel, TKey, TValue>>(),
                             MultiEdgeInitializer
                            ),

                    // Create a new HyperEdge
                   (Graph) => IdCreatorDelegate(),
                   (Graph, EdgeSelector, HyperEdgeId, Label, HyperEdgeInitializer) =>
                       new PropertyHyperEdge<TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>,
                                             TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>,
                                             TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>,
                                             TId, TRevisionId, TLabel, TKey, TValue, IDictionary<TKey, TValue>,
                                             ICollection<IPropertyEdge<TId, TRevisionId, TLabel, TKey, TValue,
                                                                       TId, TRevisionId, TLabel, TKey, TValue,
                                                                       TId, TRevisionId, TLabel, TKey, TValue,
                                                                       TId, TRevisionId, TLabel, TKey, TValue>>>
                            (Graph, EdgeSelector, HyperEdgeId, Label, IdKey, RevisionIdKey,
                             () => new Dictionary<TKey, TValue>(),
                             () => new HashSet<IPropertyEdge<TId, TRevisionId, TLabel, TKey, TValue,
                                                             TId, TRevisionId, TLabel, TKey, TValue,
                                                             TId, TRevisionId, TLabel, TKey, TValue,
                                                             TId, TRevisionId, TLabel, TKey, TValue>>(),
                             HyperEdgeInitializer
                            ),


#if SILVERLIGHT
                   // The vertices collection
                   new Dictionary<TId, IPropertyVertex   <TId, TRevisionId, TLabel, TKey, TValue,
                                                          TId, TRevisionId, TLabel, TKey, TValue,
                                                          TId, TRevisionId, TLabel, TKey, TValue,
                                                          TId, TRevisionId, TLabel, TKey, TValue>>(),

                   // The edges collection
                   new Dictionary<TId, IPropertyEdge     <TId, TRevisionId, TLabel, TKey, TValue,
                                                          TId, TRevisionId, TLabel, TKey, TValue,
                                                          TId, TRevisionId, TLabel, TKey, TValue,
                                                          TId, TRevisionId, TLabel, TKey, TValue>>(),

                   // The multiedges collection
                   new Dictionary<TId, IPropertyMultiEdge<TId, TRevisionId, TLabel, TKey, TValue,
                                                          TId, TRevisionId, TLabel, TKey, TValue,
                                                          TId, TRevisionId, TLabel, TKey, TValue,
                                                          TId, TRevisionId, TLabel, TKey, TValue>>(),

                   // The hyperedges collection
                   new Dictionary<TId, IPropertyHyperEdge<TId, TRevisionId, TLabel, TKey, TValue,
                                                          TId, TRevisionId, TLabel, TKey, TValue,
                                                          TId, TRevisionId, TLabel, TKey, TValue,
                                                          TId, TRevisionId, TLabel, TKey, TValue>>(),
#else
                   // The vertices collection
                   new ConcurrentDictionary<TId, IPropertyVertex   <TId, TRevisionId, TLabel, TKey, TValue,
                                                                    TId, TRevisionId, TLabel, TKey, TValue,
                                                                    TId, TRevisionId, TLabel, TKey, TValue,
                                                                    TId, TRevisionId, TLabel, TKey, TValue>>(),

                   // The edges collection
                   new ConcurrentDictionary<TId, IPropertyEdge     <TId, TRevisionId, TLabel, TKey, TValue,
                                                                    TId, TRevisionId, TLabel, TKey, TValue,
                                                                    TId, TRevisionId, TLabel, TKey, TValue,
                                                                    TId, TRevisionId, TLabel, TKey, TValue>>(),

                   // The multiedges collection
                   new ConcurrentDictionary<TId, IPropertyMultiEdge<TId, TRevisionId, TLabel, TKey, TValue,
                                                                    TId, TRevisionId, TLabel, TKey, TValue,
                                                                    TId, TRevisionId, TLabel, TKey, TValue,
                                                                    TId, TRevisionId, TLabel, TKey, TValue>>(),

                   // The hyperedges collection
                   new ConcurrentDictionary<TId, IPropertyHyperEdge<TId, TRevisionId, TLabel, TKey, TValue,
                                                                    TId, TRevisionId, TLabel, TKey, TValue,
                                                                    TId, TRevisionId, TLabel, TKey, TValue,
                                                                    TId, TRevisionId, TLabel, TKey, TValue>>(),
#endif

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
        public static Boolean operator == (SimpleGenericPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph1,
                                           SimpleGenericPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph2)
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
        public static Boolean operator != (SimpleGenericPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph1,
                                           SimpleGenericPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph2)
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
        public static Boolean operator < (SimpleGenericPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph1,
                                          SimpleGenericPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph2)
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
        public static Boolean operator <= (SimpleGenericPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph1,
                                           SimpleGenericPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph2)
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
        public static Boolean operator > (SimpleGenericPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph1,
                                          SimpleGenericPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph2)
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
        public static Boolean operator >= (SimpleGenericPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph1,
                                           SimpleGenericPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue> PropertyGraph2)
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
            var PropertyGraph = Object as SimpleGenericPropertyGraph<TId, TRevisionId, TLabel, TKey, TValue>;
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
