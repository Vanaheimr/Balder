/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Blueprints.NET <http://www.github.com/Vanaheimr/Blueprints.NET>
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

using de.ahzf.Illias.Commons;

#endregion

namespace de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable
{

    /// <summary>
    /// A class-based in-memory implementation of a simplified generic property graph.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TVertexLabel">The type of the vertex type.</typeparam>
    /// <typeparam name="TKeyVertex">The type of the vertex property keys.</typeparam>
    /// <typeparam name="TValueVertex">The type of the vertex property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionVertex">A data structure to store the properties of the vertices.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
    /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionEdge">A data structure to store the properties of the edges.</typeparam>
    /// 
    /// <typeparam name="TIdMultiEdge">The type of the multiedge identifiers.</typeparam>
    /// <typeparam name="TRevIdMultiEdge">The type of the multiedge revision identifiers.</typeparam>
    /// <typeparam name="TMultiEdgeLabel">The type of the multiedge label.</typeparam>
    /// <typeparam name="TKeyMultiEdge">The type of the multiedge property keys.</typeparam>
    /// <typeparam name="TValueMultiEdge">The type of the multiedge property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionMultiEdge">A data structure to store the properties of the multiedges.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
    /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionHyperEdge">A data structure to store the properties of the hyperedges.</typeparam>
    public class GenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                     : GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

        where TIdVertex        : IEquatable<TIdVertex>,       IComparable<TIdVertex>,       IComparable, TValueVertex
        where TIdEdge          : IEquatable<TIdEdge>,         IComparable<TIdEdge>,         IComparable, TValueEdge
        where TIdMultiEdge     : IEquatable<TIdMultiEdge>,    IComparable<TIdMultiEdge>,    IComparable, TValueMultiEdge
        where TIdHyperEdge     : IEquatable<TIdHyperEdge>,    IComparable<TIdHyperEdge>,    IComparable, TValueHyperEdge

        where TRevIdVertex     : IEquatable<TRevIdVertex>,    IComparable<TRevIdVertex>,    IComparable, TValueVertex
        where TRevIdEdge       : IEquatable<TRevIdEdge>,      IComparable<TRevIdEdge>,      IComparable, TValueEdge
        where TRevIdMultiEdge  : IEquatable<TRevIdMultiEdge>, IComparable<TRevIdMultiEdge>, IComparable, TValueMultiEdge
        where TRevIdHyperEdge  : IEquatable<TRevIdHyperEdge>, IComparable<TRevIdHyperEdge>, IComparable, TValueHyperEdge

        where TVertexLabel     : IEquatable<TVertexLabel>,    IComparable<TVertexLabel>,    IComparable
        where TEdgeLabel       : IEquatable<TEdgeLabel>,      IComparable<TEdgeLabel>,      IComparable
        where TMultiEdgeLabel  : IEquatable<TMultiEdgeLabel>, IComparable<TMultiEdgeLabel>, IComparable
        where THyperEdgeLabel  : IEquatable<THyperEdgeLabel>, IComparable<THyperEdgeLabel>, IComparable

        where TKeyVertex       : IEquatable<TKeyVertex>,      IComparable<TKeyVertex>,      IComparable
        where TKeyEdge         : IEquatable<TKeyEdge>,        IComparable<TKeyEdge>,        IComparable
        where TKeyMultiEdge    : IEquatable<TKeyMultiEdge>,   IComparable<TKeyMultiEdge>,   IComparable
        where TKeyHyperEdge    : IEquatable<TKeyHyperEdge>,   IComparable<TKeyHyperEdge>,   IComparable

    {

        #region Constructor(s)

        #region GenericPropertyGraph(...IdCreatorDelegates...)

        /// <summary>
        /// Creates a new class-based in-memory implementation of a generic property graph.
        /// </summary>
        /// <param name="GraphId">The identification of this generic property graph.</param>
        /// 
        /// <param name="VertexIdKey">The property key to access the Ids of the vertices.</param>
        /// <param name="VertexRevIdKey">The property key to access the RevisionIds of the vertices.</param>
        /// <param name="VertexDescriptionKey">The property key to access the descriptions of the vertices.</param>
        /// <param name="VertexIdCreatorDelegate">A delegate to create a new vertex identification.</param>
        /// <param name="DefaultVertexLabel">The default label of the vertices.</param>
        /// 
        /// <param name="EdgeIdKey">The property key to access the Ids of the edges.</param>
        /// <param name="EdgeRevIdKey">The property key to access the RevisionIds of the edges.</param>
        /// <param name="EdgeDescriptionKey">The property key to access the descriptions of the edges.</param>
        /// <param name="EdgeIdCreatorDelegate">A delegate to create a new edge identification.</param>
        /// <param name="DefaultEdgeLabel">The default label of the edges.</param>
        /// 
        /// <param name="MultiEdgeIdKey">The property key to access the Ids of the multiedges.</param>
        /// <param name="MultiEdgeRevIdKey">The property key to access the RevisionIds of the multiedges.</param>
        /// <param name="MultiEdgeDescriptionKey">The property key to access the descriptions of the multiedges.</param>
        /// <param name="MultiEdgeIdCreatorDelegate">A delegate to create a new multiedge identification.</param>
        /// <param name="DefaultMultiEdgeLabel">The default label of the multiedges.</param>
        /// 
        /// <param name="HyperEdgeIdKey">The property key to access the Ids of the hyperedges.</param>
        /// <param name="HyperEdgeRevIdKey">The property key to access the RevisionIds of the hyperedges.</param>
        /// <param name="HyperEdgeDescriptionKey">The property key to access the descriptions of the hyperedges.</param>
        /// <param name="HyperEdgeIdCreatorDelegate">A delegate to create a new hyperedge identification.</param>
        /// <param name="DefaultHyperEdgeLabel">The default label of the hyperedges.</param>
        /// 
        /// <param name="GraphInitializer">A delegate to initialize the new property graph.</param>
        public GenericPropertyGraph(TIdVertex  GraphId,

                                    TKeyVertex VertexIdKey,
                                    TKeyVertex VertexRevIdKey,
                                    TKeyVertex VertexDescriptionKey,
                                    VertexIdCreatorDelegate   <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexIdCreatorDelegate,
                                    TVertexLabel DefaultVertexLabel,


                                    TKeyEdge   EdgeIdKey,
                                    TKeyEdge   EdgeRevIdKey,
                                    TKeyEdge   EdgeDescriptionKey,
                                    EdgeIdCreatorDelegate     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeIdCreatorDelegate,
                                    TEdgeLabel DefaultEdgeLabel,


                                    TKeyMultiEdge MultiEdgeIdKey,
                                    TKeyMultiEdge MultiEdgeRevIdKey,
                                    TKeyMultiEdge MultiEdgeDescriptionKey,
                                    MultiEdgeIdCreatorDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeIdCreatorDelegate,
                                    TMultiEdgeLabel DefaultMultiEdgeLabel,


                                    TKeyHyperEdge HyperEdgeIdKey,
                                    TKeyHyperEdge HyperEdgeRevIdKey,
                                    TKeyHyperEdge HyperEdgeDescriptionKey,
                                    HyperEdgeIdCreatorDelegate<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeIdCreatorDelegate,
                                    THyperEdgeLabel DefaultHyperEdgeLabel,


                                    GraphInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GraphInitializer = null)

            : base (

                    #region Vertices
            
                    GraphId,
                    DefaultVertexLabel,
                    VertexIdKey,
                    VertexRevIdKey,
                    VertexDescriptionKey,

                    () => new Dictionary<TKeyVertex, TValueVertex>(),
                    
                    // Create a new vertex identification
                    VertexIdCreatorDelegate,
                    DefaultVertexLabel,

                    // Create a new vertex
                    (Graph, _VertexId, _VertexLabel, VertexInitializer) =>
                        new GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                            (Graph,
                             _VertexId,
                             _VertexLabel,
                             VertexIdKey,
                             VertexRevIdKey,
                             VertexDescriptionKey,
                             () => new Dictionary<TKeyVertex, TValueVertex>(),

                             () => new GroupedCollection<TIdVertex,    IGenericPropertyVertex   <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TVertexLabel>(),

                             () => new GroupedCollection<TIdEdge,      IGenericPropertyEdge     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TEdgeLabel>(),

                             () => new GroupedCollection<TIdMultiEdge, IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TMultiEdgeLabel>(),

                             () => new GroupedCollection<TIdHyperEdge, IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, THyperEdgeLabel>(),
                             VertexInitializer),

                    // The vertices collection
                    () => new GroupedCollection<TIdVertex, IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TVertexLabel>(),

                    #endregion

                    #region Edges

                    // Create a new edge identification
                    EdgeIdCreatorDelegate,
                    DefaultEdgeLabel,

                    // Create a new edge
                    (Graph, OutVertex, InVertex, EdgeId, EdgeLabel, EdgeInitializer) =>
                        new GenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                            (Graph,
                             OutVertex,
                             InVertex,
                             EdgeId,
                             EdgeLabel,
                             EdgeIdKey,
                             EdgeRevIdKey,
                             EdgeDescriptionKey,
                             () => new Dictionary<TKeyEdge, TValueEdge>(),
                             EdgeInitializer),

                    // The edges collection
                    () => new GroupedCollection<TIdEdge, IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TEdgeLabel>(),

                    #endregion

                    #region MultiEdges

                    // Create a new multiedge identification
                    MultiEdgeIdCreatorDelegate,
                    DefaultMultiEdgeLabel,

                    // Create a new multiedge
                    (Graph, EdgeSelector, MultiEdgeId, MultiEdgeLabel, MultiEdgeInitializer) =>

                       new GenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                            (Graph,
                             EdgeSelector,
                             MultiEdgeId,
                             MultiEdgeLabel,
                             MultiEdgeIdKey,
                             MultiEdgeRevIdKey,
                             MultiEdgeDescriptionKey,

                             () => new Dictionary<TKeyMultiEdge, TValueMultiEdge>(),
                             () => new GroupedCollection<TEdgeLabel, TIdEdge, IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>(),

                             MultiEdgeInitializer),

                    // The multiedges collection
                    () => new GroupedCollection<TIdMultiEdge, IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TMultiEdgeLabel>(),
                    #endregion

                    #region HyperEdges

                    // Create a new hyperedge identification
                    HyperEdgeIdCreatorDelegate,
                    DefaultHyperEdgeLabel,

                    // Create a new hyperedge
                    (Graph, EdgeSelector, HyperEdgeId, HyperEdgeLabel, HyperEdgeInitializer) =>

                       new GenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                            (Graph,
                             EdgeSelector,
                             HyperEdgeId,
                             HyperEdgeLabel,
                             HyperEdgeIdKey,
                             HyperEdgeRevIdKey,
                             HyperEdgeDescriptionKey,

                             () => new Dictionary<TKeyHyperEdge, TValueHyperEdge>(),
                             () => new GroupedCollection<TIdVertex, IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TVertexLabel>(),

                             HyperEdgeInitializer),

                    // The hyperedges collection
                    () => new GroupedCollection<TIdHyperEdge, IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, THyperEdgeLabel>()

                    #endregion

                   )

        {

            if (GraphInitializer != null)
                GraphInitializer(this);

        }

        #endregion

        #region GenericPropertyGraph(...IIdCreators...)

        /// <summary>
        /// Creates a new class-based in-memory implementation of a generic property graph.
        /// </summary>
        /// <param name="GraphId">The identification of this generic property graph.</param>
        /// 
        /// <param name="VertexIdKey">The property key to access the Ids of the vertices.</param>
        /// <param name="VertexRevIdKey">The property key to access the RevisionIds of the vertices.</param>
        /// <param name="VertexDescriptionKey">The property key to access the descriptions of the vertices.</param>
        /// <param name="VertexIdCreator">A vertex identification creator.</param>
        /// <param name="DefaultVertexLabel">The default label of the vertices.</param>
        /// 
        /// <param name="EdgeIdKey">The property key to access the Ids of the edges.</param>
        /// <param name="EdgeRevIdKey">The property key to access the RevisionIds of the edges.</param>
        /// <param name="EdgeDescriptionKey">The property key to access the descriptions of the edges.</param>
        /// <param name="EdgeIdCreator">A edge identification creator.</param>
        /// <param name="DefaultEdgeLabel">The default label of the edges.</param>
        /// 
        /// <param name="MultiEdgeIdKey">The property key to access the Ids of the multiedges.</param>
        /// <param name="MultiEdgeRevIdKey">The property key to access the RevisionIds of the multiedges.</param>
        /// <param name="MultiEdgeDescriptionKey">The property key to access the descriptions of the multiedges.</param>
        /// <param name="MultiEdgeIdCreator">A multiedge identification creator.</param>
        /// <param name="DefaultMultiEdgeLabel">The default label of the multiedges.</param>
        /// 
        /// <param name="HyperEdgeIdKey">The property key to access the Ids of the hyperedges.</param>
        /// <param name="HyperEdgeRevIdKey">The property key to access the RevisionIds of the hyperedges.</param>
        /// <param name="HyperEdgeDescriptionKey">The property key to access the descriptions of the hyperedges.</param>
        /// <param name="HyperEdgeIdCreator">A hyperedge identification creator.</param>
        /// <param name="DefaultHyperEdgeLabel">The default label of the hyperedges.</param>
        /// 
        /// <param name="GraphInitializer">A delegate to initialize the new property graph.</param>
        public GenericPropertyGraph(TIdVertex  GraphId,

                                    TKeyVertex VertexIdKey,
                                    TKeyVertex VertexRevIdKey,
                                    TKeyVertex VertexDescriptionKey,
                                    IIdGenerator<TIdVertex> VertexIdCreator,
                                    TVertexLabel DefaultVertexLabel,


                                    TKeyEdge   EdgeIdKey,
                                    TKeyEdge   EdgeRevIdKey,
                                    TKeyEdge   EdgeDescriptionKey,
                                    IIdGenerator<TIdEdge> EdgeIdCreator,
                                    TEdgeLabel DefaultEdgeLabel,


                                    TKeyMultiEdge MultiEdgeIdKey,
                                    TKeyMultiEdge MultiEdgeRevIdKey,
                                    TKeyMultiEdge MultiEdgeDescriptionKey,
                                    IIdGenerator<TIdMultiEdge> MultiEdgeIdCreator,
                                    TMultiEdgeLabel DefaultMultiEdgeLabel,


                                    TKeyHyperEdge HyperEdgeIdKey,
                                    TKeyHyperEdge HyperEdgeRevIdKey,
                                    TKeyHyperEdge HyperEdgeDescriptionKey,
                                    IIdGenerator<TIdHyperEdge> HyperEdgeIdCreator,
                                    THyperEdgeLabel DefaultHyperEdgeLabel,


                                    GraphInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GraphInitializer = null)

            : base (

                    #region Vertices
            
                    GraphId,
                    DefaultVertexLabel,
                    VertexIdKey,
                    VertexRevIdKey,
                    VertexDescriptionKey,

                    () => new Dictionary<TKeyVertex, TValueVertex>(),
                    
                    // Create a new vertex identification
                    VertexId => VertexIdCreator.NewId,
                    DefaultVertexLabel,

                    // Create a new vertex
                    (Graph, _VertexId, _VertexLabel, VertexInitializer) =>
                        new GenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                            (Graph,
                             _VertexId,
                             _VertexLabel,
                             VertexIdKey,
                             VertexRevIdKey,
                             VertexDescriptionKey,
                             () => new Dictionary<TKeyVertex, TValueVertex>(),

                             () => new GroupedCollection<TIdVertex,    IGenericPropertyVertex   <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TVertexLabel>(),

                             () => new GroupedCollection<TIdEdge,      IGenericPropertyEdge     <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TEdgeLabel>(),

                             () => new GroupedCollection<TIdMultiEdge, IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TMultiEdgeLabel>(),

                             () => new GroupedCollection<TIdHyperEdge, IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, THyperEdgeLabel>(),
                             VertexInitializer),

                    // The vertices collection
                    () => new GroupedCollection<TIdVertex, IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TVertexLabel>(),

                    #endregion

                    #region Edges

                    // Create a new edge identification
                    EdgeId => EdgeIdCreator.NewId,
                    DefaultEdgeLabel,

                    // Create a new edge
                    (Graph, OutVertex, InVertex, EdgeId, EdgeLabel, EdgeInitializer) =>
                        new GenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                            (Graph,
                             OutVertex,
                             InVertex,
                             EdgeId,
                             EdgeLabel,
                             EdgeIdKey,
                             EdgeRevIdKey,
                             EdgeDescriptionKey,
                             () => new Dictionary<TKeyEdge, TValueEdge>(),
                             EdgeInitializer),

                    // The edges collection
                    () => new GroupedCollection<TIdEdge, IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TEdgeLabel>(),

                    #endregion

                    #region MultiEdges

                    // Create a new multiedge identification
                    MultiEdgeId => MultiEdgeIdCreator.NewId,
                    DefaultMultiEdgeLabel,

                    // Create a new multiedge
                    (Graph, EdgeSelector, MultiEdgeId, MultiEdgeLabel, MultiEdgeInitializer) =>

                       new GenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                            (Graph,
                             EdgeSelector,
                             MultiEdgeId,
                             MultiEdgeLabel,
                             MultiEdgeIdKey,
                             MultiEdgeRevIdKey,
                             MultiEdgeDescriptionKey,

                             () => new Dictionary<TKeyMultiEdge, TValueMultiEdge>(),
                             () => new GroupedCollection<TEdgeLabel, TIdEdge, IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>(),

                             MultiEdgeInitializer),

                    // The multiedges collection
                    () => new GroupedCollection<TIdMultiEdge, IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TMultiEdgeLabel>(),
                    #endregion

                    #region HyperEdges

                    // Create a new hyperedge identification
                    HyperEdgeId => HyperEdgeIdCreator.NewId,
                    DefaultHyperEdgeLabel,

                    // Create a new hyperedge
                    (Graph, EdgeSelector, HyperEdgeId, HyperEdgeLabel, HyperEdgeInitializer) =>

                       new GenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                            (Graph,
                             EdgeSelector,
                             HyperEdgeId,
                             HyperEdgeLabel,
                             HyperEdgeIdKey,
                             HyperEdgeRevIdKey,
                             HyperEdgeDescriptionKey,

                             () => new Dictionary<TKeyHyperEdge, TValueHyperEdge>(),
                             () => new GroupedCollection<TIdVertex, IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TVertexLabel>(),

                             HyperEdgeInitializer),

                    // The hyperedges collection
                    () => new GroupedCollection<TIdHyperEdge, IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, THyperEdgeLabel>()

                    #endregion

                   )

        {

            if (GraphInitializer != null)
                GraphInitializer(this);

        }

        #endregion

        #endregion

        #region ToString()

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return String.Concat("GenericPropertyGraph [Id: ", Id.ToString(),
                                 ", vertices: ",   _VerticesWhenGraph.Count(),
                                 ", edges: ",      _EdgesWhenGraph.Count(),
                                 ", multiedges: ", _MultiEdgesWhenGraph.Count(),
                                 ", hyperedges: ", _HyperEdgesWhenGraph.Count(), "]");
        }

        #endregion

    }

}
