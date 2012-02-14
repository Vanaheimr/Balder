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

#endregion

namespace de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable
{

    /// <summary>
    /// A class-based in-memory implementation of a common generic property graph.
    /// </summary>
    /// <typeparam name="TId">The type of the graph element identifiers.</typeparam>
    /// <typeparam name="TRevId">The type of the graph element revision identifiers.</typeparam>
    /// <typeparam name="TLabel">The type of the labels.</typeparam>
    /// <typeparam name="TKey">The type of the graph element property keys.</typeparam>
    /// <typeparam name="TValue">The type of the graph element property values.</typeparam>
    public class CommonGenericPropertyGraph<TId, TRevId, TLabel, TKey, TValue>

                     : GenericPropertyGraph<TId, TRevId, TLabel, TKey, TValue,   // Vertex definition
                                            TId, TRevId, TLabel, TKey, TValue,   // Edge definition
                                            TId, TRevId, TLabel, TKey, TValue,   // MultiEdge definition
                                            TId, TRevId, TLabel, TKey, TValue>,  // Hyperedge definition

                       ICommonGenericPropertyGraph<TId, TRevId, TLabel, TKey, TValue>

        where TId     : IEquatable<TId>,    IComparable<TId>,    IComparable, TValue
        where TRevId  : IEquatable<TRevId>, IComparable<TRevId>, IComparable, TValue
        where TKey    : IEquatable<TKey>,   IComparable<TKey>,   IComparable
        where TLabel  : IEquatable<TLabel>, IComparable<TLabel>, IComparable

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

        #region RevIdCreatorDelegate()

        /// <summary>
        /// A delegate for creating a new RevisionId
        /// (if no one was provided by the user).
        /// </summary>
        /// <returns>A valid RevisionId.</returns>
        public delegate TRevId RevIdCreatorDelegate();

        #endregion

        #endregion

        #region Constructor(s)

        #region CommonGenericPropertyGraph()

        /// <summary>
        /// Created a new class-based in-memory implementation of a common generic property graph.
        /// </summary>
        public CommonGenericPropertyGraph(TId                  GraphId,
                                          TKey                 IdKey,
                                          IdCreatorDelegate    VertexIdCreatorDelegate,
                                          IdCreatorDelegate    EdgeIdCreatorDelegate,
                                          IdCreatorDelegate    MultiEdgeIdCreatorDelegate,
                                          IdCreatorDelegate    HyperEdgeIdCreatorDelegate,
                                          TKey                 RevIdKey,
                                          RevIdCreatorDelegate RevIdCreatorDelegate,
                                          GraphInitializer<TId, TRevId, TLabel, TKey, TValue,
                                                           TId, TRevId, TLabel, TKey, TValue,
                                                           TId, TRevId, TLabel, TKey, TValue,
                                                           TId, TRevId, TLabel, TKey, TValue> GraphInitializer = null)

            : base (IdKey,
                    RevIdKey,
                    GraphId,
                    
                    // Create a new vertex
                    IdKey,
                    RevIdKey,
                    (a) => VertexIdCreatorDelegate(),

                    // Create a new edge
                    IdKey,
                    RevIdKey,
                    (Graph) => EdgeIdCreatorDelegate(),

                    // Create a new multiedge
                    IdKey,
                    RevIdKey,
                    (Graph) => MultiEdgeIdCreatorDelegate(),

                    // Create a new hyperedge
                    IdKey,
                    RevIdKey,
                    (Graph) => HyperEdgeIdCreatorDelegate(),

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
        public static Boolean operator == (CommonGenericPropertyGraph<TId, TRevId, TLabel, TKey, TValue> PropertyGraph1,
                                           CommonGenericPropertyGraph<TId, TRevId, TLabel, TKey, TValue> PropertyGraph2)
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
        public static Boolean operator != (CommonGenericPropertyGraph<TId, TRevId, TLabel, TKey, TValue> PropertyGraph1,
                                           CommonGenericPropertyGraph<TId, TRevId, TLabel, TKey, TValue> PropertyGraph2)
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
        public static Boolean operator < (CommonGenericPropertyGraph<TId, TRevId, TLabel, TKey, TValue> PropertyGraph1,
                                          CommonGenericPropertyGraph<TId, TRevId, TLabel, TKey, TValue> PropertyGraph2)
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
        public static Boolean operator <= (CommonGenericPropertyGraph<TId, TRevId, TLabel, TKey, TValue> PropertyGraph1,
                                           CommonGenericPropertyGraph<TId, TRevId, TLabel, TKey, TValue> PropertyGraph2)
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
        public static Boolean operator > (CommonGenericPropertyGraph<TId, TRevId, TLabel, TKey, TValue> PropertyGraph1,
                                          CommonGenericPropertyGraph<TId, TRevId, TLabel, TKey, TValue> PropertyGraph2)
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
        public static Boolean operator >= (CommonGenericPropertyGraph<TId, TRevId, TLabel, TKey, TValue> PropertyGraph1,
                                           CommonGenericPropertyGraph<TId, TRevId, TLabel, TKey, TValue> PropertyGraph2)
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
            var PropertyGraph = Object as CommonGenericPropertyGraph<TId, TRevId, TLabel, TKey, TValue>;
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
