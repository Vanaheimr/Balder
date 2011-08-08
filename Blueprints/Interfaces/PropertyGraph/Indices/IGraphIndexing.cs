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

using de.ahzf.Blueprints.Indices;

#endregion

namespace de.ahzf.Blueprints.PropertyGraph.Indices
{

    /// <summary>
    /// An indexable graph is a graph that supports the indexing of its elements.
    /// An index is typically some sort of tree structure that allows for the fast lookup of elements by key/value pairs.
    /// All indexable graphs are initially constructed with two automatic indices called "vertices" and "edges".
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
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
    public interface IGraphIndexing<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

        where TEdgeLabel           : IEquatable<TEdgeLabel>,           IComparable<TEdgeLabel>,           IComparable
        where THyperEdgeLabel      : IEquatable<THyperEdgeLabel>,      IComparable<THyperEdgeLabel>,      IComparable

        where TKeyVertex           : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge             : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyHyperEdge        : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

    {

        #region CreateVerticesIndex<TIndexKey>(Name, IndexClassName, Transformation, Selector = null, IsAutomaticIndex = false)

        /// <summary>
        /// Generate an index for vertex lookups.
        /// </summary>
        /// <typeparam name="TIndexKey">The type of the index keys.</typeparam>
        /// <param name="Name">A human-friendly name for the index.</param>
        /// <param name="IndexClassName">The class name of a datastructure maintaining the index.</param>
        /// <param name="Transformation">A delegate for transforming a vertex into an index key.</param>
        /// <param name="Selector">A delegate for deciding if a vertex should be indexed or not.</param>
        /// <param name="IsAutomaticIndex">Should this index be maintained by the database or by the user?</param>
        /// <returns>A new index data structure.</returns>
        IPropertyElementIndex<TIndexKey, IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
               CreateVerticesIndex<TIndexKey>(String Name,
                                              String IndexClassName,
                                              IndexTransformation<TIndexKey,
                                                                  IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Transformation,
                                              IndexSelector      <TIndexKey,
                                                                  IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Selector = null,
                                              Boolean IsAutomaticIndex = false)

            where TIndexKey : IEquatable<TIndexKey>, IComparable<TIndexKey>, IComparable;

        #endregion

        #region CreateEdgesIndex<TIndexKey>(Name, IndexClassName, Transformation, Selector = null, IsAutomaticIndex = false)

        /// <summary>
        /// Generate an index for edge lookups.
        /// </summary>
        /// <typeparam name="TIndexKey">The type of the index keys.</typeparam>
        /// <param name="Name">A human-friendly name for the index.</param>
        /// <param name="IndexClassName">The class name of a datastructure maintaining the index.</param>
        /// <param name="Transformation">A delegate for transforming a edge into an index key.</param>
        /// <param name="Selector">A delegate for deciding if a edge should be indexed or not.</param>
        /// <param name="IsAutomaticIndex">Should this index be maintained by the database or by the user?</param>
        /// <returns>A new index data structure.</returns>
        IPropertyElementIndex<TIndexKey, IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

               CreateEdgesIndex<TIndexKey>(String Name,
                                           String IndexClassName,
                                           IndexTransformation<TIndexKey,
                                                               IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Transformation,
                                           IndexSelector      <TIndexKey,
                                                               IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Selector = null,
                                           Boolean IsAutomaticIndex = false)

            where TIndexKey : IEquatable<TIndexKey>, IComparable<TIndexKey>, IComparable;

        #endregion

        #region CreateHyperEdgesIndex<TIndexKey>(Name, IndexClassName, Transformation, Selector = null, IsAutomaticIndex = false)

        /// <summary>
        /// Generate an index for hyperedge lookups.
        /// </summary>
        /// <typeparam name="TIndexKey">The type of the index keys.</typeparam>
        /// <param name="Name">A human-friendly name for the index.</param>
        /// <param name="IndexClassName">The class name of a datastructure maintaining the index.</param>
        /// <param name="Transformation">A delegate for transforming a hyperedge into an index key.</param>
        /// <param name="Selector">A delegate for deciding if a hyperedge should be indexed or not.</param>
        /// <param name="IsAutomaticIndex">Should this index be maintained by the database or by the user?</param>
        /// <returns>A new index data structure.</returns>
        IPropertyElementIndex<TIndexKey, IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

               CreateHyperEdgesIndex<TIndexKey>(String Name,
                                                String IndexClassName,
                                                IndexTransformation<TIndexKey,
                                                                    IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Transformation,
                                                IndexSelector      <TIndexKey,
                                                                    IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Selector = null,
                                                Boolean IsAutomaticIndex = false)

            where TIndexKey : IEquatable<TIndexKey>, IComparable<TIndexKey>, IComparable;

        #endregion


        #region VerticesIndices(IndexFilter = null)

        /// <summary>
        /// Get all vertices indices maintained by the graph.
        /// </summary>
        /// <param name="IndexFilter">An optional index filter.</param>
        /// <returns>The indices associated with the graph.</returns>
        IEnumerable<IPropertyElementIndex<IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>
            
            VerticesIndices(IndexFilter<IPropertyVertex  <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> IndexFilter = null);

        #endregion

        #region EdgesIndices(IndexFilter = null)

        /// <summary>
        /// Get all edges indices maintained by the graph.
        /// </summary>
        /// <param name="IndexFilter">An optional index filter.</param>
        /// <returns>The indices associated with the graph.</returns>
        IEnumerable<IPropertyElementIndex<IPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>
            
            EdgesIndices(IndexFilter<IPropertyEdge     <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> IndexFilter = null);

        #endregion

        #region HyperEdgesIndices(IndexFilter = null)

        /// <summary>
        /// Get all hyperedges indices maintained by the graph.
        /// </summary>
        /// <param name="IndexFilter">An optional index filter.</param>
        /// <returns>The indices associated with the graph.</returns>
        IEnumerable<IPropertyElementIndex<IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>
            
            HyperEdgesIndices(IndexFilter<IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> IndexFilter = null);

        #endregion


        #region DropVerticesIndex(Name)

        /// <summary>
        /// Remove a vertices index associated with the graph.
        /// </summary>
        /// <param name="Name">The name of the index to drop.</param>
        void DropVerticesIndex(String Name);

        #endregion

        #region DropEdgesIndex(Name)

        /// <summary>
        /// Remove an edges index associated with the graph.
        /// </summary>
        /// <param name="Name">The name of the index to drop.</param>
        void DropEdgesIndex(String Name);

        #endregion

        #region DropHyperEdgesIndex(Name)

        /// <summary>
        /// Remove a hyperedges index associated with the graph.
        /// </summary>
        /// <param name="Name">The name of the index to drop.</param>
        void DropHyperEdgesIndex(String Name);

        #endregion


        #region DropVerticesIndices(IndexNameEvaluator = null)

        /// <summary>
        /// Remove vertices indices associated with the graph.
        /// </summary>
        /// <param name="IndexNameEvaluator">A delegate evaluating the indices to drop.</param>
        void DropVerticesIndex(IndexNameFilter IndexNameEvaluator = null);

        #endregion

        #region DropEdgesIndices(IndexNameEvaluator = null)

        /// <summary>
        /// Remove edges indices associated with the graph.
        /// </summary>
        /// <param name="IndexNameEvaluator">A delegate evaluating the indices to drop.</param>
        void DropEdgesIndex(IndexNameFilter IndexNameEvaluator = null);

        #endregion

        #region DropHyperEdgesIndices(IndexNameEvaluator = null)

        /// <summary>
        /// Remove hyperedge indices associated with the graph.
        /// </summary>
        /// <param name="IndexNameEvaluator">A delegate evaluating the indices to drop.</param>
        void DropHyperEdgesIndex(IndexNameFilter IndexNameEvaluator = null);

        #endregion

    }

}
