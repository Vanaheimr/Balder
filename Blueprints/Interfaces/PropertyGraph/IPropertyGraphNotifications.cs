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

#endregion

namespace de.ahzf.Blueprints.PropertyGraph
{

    /// <summary>
    /// The interface for all events and notifications of property graphs.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TVertexType">The type of the vertex type.</typeparam>
    /// <typeparam name="TKeyVertex">The type of the vertex property keys.</typeparam>
    /// <typeparam name="TValueVertex">The type of the vertex property values.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
    /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
    /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
    public interface IPropertyGraphNotifications<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        
        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

        where TVertexType           : IEquatable<TVertexType>,           IComparable<TVertexType>,           IComparable
            where TEdgeLabel          : IEquatable<TEdgeLabel>,           IComparable<TEdgeLabel>,           IComparable
        where THyperEdgeLabel      : IEquatable<THyperEdgeLabel>,      IComparable<THyperEdgeLabel>,      IComparable

        where TKeyVertex           : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge             : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyHyperEdge        : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

    {

        #region OnVertexAdding

        /// <summary>
        /// Called whenever a vertex will be added to the property graph.
        /// </summary>
        event VertexAddingEventHandler<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnVertexAdding;

        #endregion

        #region OnVertexAdded

        /// <summary>
        /// Called whenever a vertex was added to the property graph.
        /// </summary>
        event VertexAddedEventHandler<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnVertexAdded;

        #endregion


        #region OnEdgeAdding

        /// <summary>
        /// Called whenever an edge will be added to the property graph.
        /// </summary>
        event EdgeAddingEventHandler<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnEdgeAdding;

        #endregion

        #region OnEdgeAdded

        /// <summary>
        /// Called whenever an edge was added to the property graph.
        /// </summary>
        event EdgeAddedEventHandler<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnEdgeAdded;

        #endregion


        #region OnHyperEdgeAdding

        /// <summary>
        /// Called whenever a hyperedge will be added to the property graph.
        /// </summary>
        event HyperEdgeAddingEventHandler<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnHyperEdgeAdding;

        #endregion

        #region OnHyperEdgeAdded

        /// <summary>
        /// Called whenever a hyperedge was added to the property graph.
        /// </summary>
        event HyperEdgeAddedEventHandler<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnHyperEdgeAdded;

        #endregion


        #region OnGraphShuttingdown

        /// <summary>
        /// Called whenever a property graph will be shutting down.
        /// </summary>
        event GraphShuttingdownEventHandler<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnGraphShuttingdown;

        #endregion

        #region OnGraphShutteddown

        /// <summary>
        /// Called whenever a property graphy was shutted down.
        /// </summary>
        event GraphShutteddownEventHandler <TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnGraphShutteddown;

        #endregion

    }

}
