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

#endregion

namespace de.ahzf.Blueprints.GenericGraph
{

    #region GraphInitializer

    /// <summary>
    /// A delegate for GenericGraph initializing.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TDataVertex">The type of the data stored within a vertex.</typeparam>
    /// <typeparam name="TVertex"></typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TDataEdge">The type of the data stored within an edge.</typeparam>
    /// <typeparam name="TEdge"></typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TDataHyperEdge">The type of the data stored within a hyperedge.</typeparam>
    /// <typeparam name="THyperEdge"></typeparam>
    /// 
    /// <param name="GenericGraph"></param>
    public delegate void GraphInitializer<TIdVertex,    TRevisionIdVertex,                     TDataVertex,    TVertex,    
                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,      TEdge,
                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge, THyperEdge>(

                                          IGenericGraph<TIdVertex,    TRevisionIdVertex,                     TDataVertex,    TVertex,    
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,      TEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge, THyperEdge> GenericGraph)

        where TVertex              : IGenericVertex   <TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>

        where TEdge                : IGenericEdge     <TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>
        
        where THyperEdge           : IGenericHyperEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>

        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable
        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable
        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable

        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable
        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable
        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable;

    #endregion


    #region VertexIdCreatorDelegate

    /// <summary>
    /// A delegate for creating a new VertexId
    /// (if no one was provided by the user).
    /// </summary>
    /// <returns>A valid VertexId.</returns>
    public delegate TIdVertex VertexIdCreatorDelegate<TIdVertex>()
        where TIdVertex : IEquatable<TIdVertex>, IComparable<TIdVertex>, IComparable;

    #endregion

    #region VertexCreatorDelegate

    /// <summary>
    /// A delegate for creating a new vertex.
    /// </summary>
    /// <param name="IGenericGraph">The associated graph.</param>
    /// <param name="VertexId">The Id of the vertex.</param>
    /// <param name="VertexInitializer">A delegate to initialize this edge with custom data.</param>
    public delegate IGenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>

                    VertexCreatorDelegate<TIdVertex,    TRevisionIdVertex,                     TDataVertex,    TVertex,
                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,      TEdge,
                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge, THyperEdge>(
    
                                        IGenericGraph  <TIdVertex,    TRevisionIdVertex,                     TDataVertex,    TVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,      TEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge, THyperEdge> IGenericGraph,

                                        TIdVertex VertexId,
                                        VertexInitializer<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> VertexInitializer)

        where TVertex              : IGenericVertex   <TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>

        where TEdge                : IGenericEdge     <TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>
        
        where THyperEdge           : IGenericHyperEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>

        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable
        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable
        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable

        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable
        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable
        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable;

    #endregion

    #region VertexInitializer

    /// <summary>
    /// A delegate for vertex initializing.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TDataVertex">The type of the data stored within a vertex.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TDataEdge">The type of the data stored within an edge.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TDataHyperEdge">The type of the data stored within a hyperedge.</typeparam>
    /// 
    /// <param name="GenericVertex">A generic vertex.</param>
    public delegate void VertexInitializer<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>(

                                           IGenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> GenericVertex)

        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable
        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable
        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable

        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable
        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable
        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable;

    #endregion

    #region VertexFilter

    /// <summary>
    /// A delegate for vertex filtering.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TDataVertex">The type of the data stored within a vertex.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TDataEdge">The type of the data stored within an edge.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TDataHyperEdge">The type of the data stored within a hyperedge.</typeparam>
    /// 
    /// <param name="GenericVertex">A generic vertex.</param>
    public delegate Boolean VertexFilter<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>(

                                         IGenericVertex<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> GenericVertex)

        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable
        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable
        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable

        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable
        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable
        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable;

    #endregion


    #region EdgeIdCreatorDelegate

    /// <summary>
    /// A delegate for creating a new EdgeId
    /// (if no one was provided by the user).
    /// </summary>
    /// <returns>A valid EdgeId.</returns>
    public delegate TIdEdge EdgeIdCreatorDelegate<TIdEdge>()
        where TIdEdge : IEquatable<TIdEdge>, IComparable<TIdEdge>, IComparable;

    #endregion

    #region EdgeCreatorDelegate

    /// <summary>
    /// A delegate for creating a new edge.
    /// </summary>
    /// <param name="IGenericGraph">The associated graph.</param>
    /// <param name="SourceVertex">The source vertex.</param>
    /// <param name="TargetVertex">The target vertex.</param>
    /// <param name="EdgeId">The Id of this edge.</param>
    /// <param name="EdgeLabel">The label of this edge.</param>
    /// <param name="EdgeInitializer">A delegate to initialize this edge with custom data.</param>
    public delegate IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>

                    EdgeCreatorDelegate<TIdVertex,    TRevisionIdVertex,                     TDataVertex,    TVertex,
                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,      TEdge,
                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge, THyperEdge>(

                            IGenericGraph  <TIdVertex,    TRevisionIdVertex,                     TDataVertex,    TVertex,
                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,      TEdge,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge, THyperEdge> IGenericGraph,
                            IGenericVertex <TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> SourceVertex,
                            IGenericVertex <TIdVertex, TRevisionIdVertex, TDataVertex,
                                            TIdEdge, TRevisionIdEdge, TEdgeLabel, TDataEdge,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> TargetVertex,

                            TIdEdge         EdgeId,
                            TEdgeLabel      EdgeLabel,

                            EdgeInitializer<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> EdgeInitializer)
    
        where TVertex              : IGenericVertex   <TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>

        where TEdge                : IGenericEdge     <TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>
        
        where THyperEdge           : IGenericHyperEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>

        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable
        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable
        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable

        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable
        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable
        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable;

    #endregion

    #region EdgeInitializer

    /// <summary>
    /// A delegate for edge initializing.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TDataVertex">The type of the data stored within a vertex.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TDataEdge">The type of the data stored within an edge.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TDataHyperEdge">The type of the data stored within a hyperedge.</typeparam>
    /// 
    /// <param name="GenericEdge">A generic edge.</param>
    public delegate void EdgeInitializer<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                         TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>(

                                         IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> GenericEdge)

        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable
        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable
        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable

        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable
        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable
        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable;

    #endregion

    #region EdgeFilter

    /// <summary>
    /// A delegate for edge filtering.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TDataVertex">The type of the data stored within a vertex.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TDataEdge">The type of the data stored within an edge.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TDataHyperEdge">The type of the data stored within a hyperedge.</typeparam>
    /// 
    /// <param name="GenericEdge">A generic edge.</param>
    public delegate Boolean EdgeFilter<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>(

                                       IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> GenericEdge)

        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable
        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable
        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable

        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable
        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable
        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable;

    #endregion


    #region HyperEdgeIdCreatorDelegate

    /// <summary>
    /// A delegate for creating a new HyperEdgeId
    /// (if no one was provided by the user).
    /// </summary>
    /// <returns>A valid HyperEdgeId.</returns>
    public delegate TIdHyperEdge HyperEdgeIdCreatorDelegate<TIdHyperEdge>()
        where TIdHyperEdge : IEquatable<TIdHyperEdge>, IComparable<TIdHyperEdge>, IComparable;

    #endregion

    #region HyperEdgeCreatorDelegate

    /// <summary>
    /// A delegate for creating a new hyperedge.
    /// </summary>
    /// <param name="IGenericGraph">The associated graph.</param>
    /// <param name="Edges">The edges of the hyperedge.</param>
    /// <param name="HyperEdgeId">The Id of this hyperedge.</param>
    /// <param name="HyperEdgeLabel">The label of this hyperedge.</param>
    /// <param name="HyperEdgeInitializer">A delegate to initialize this hyperedge with custom data.</param>
    /// <returns></returns>
    public delegate IGenericHyperEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>
            
                    HyperEdgeCreatorDelegate<TIdVertex,    TRevisionIdVertex,                     TDataVertex,    TVertex,
                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,      TEdge,
                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge, THyperEdge>(

                            IGenericGraph           <TIdVertex,    TRevisionIdVertex,                     TDataVertex,    TVertex,
                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,      TEdge,
                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge, THyperEdge> IGenericGraph,
                            IEnumerable<IGenericEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>> Edges,

                            TIdHyperEdge              HyperEdgeId,
                            THyperEdgeLabel           HyperEdgeLabel,

                            HyperEdgeInitializer<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> HyperEdgeInitializer)

        where TVertex              : IGenericVertex   <TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>

        where TEdge                : IGenericEdge     <TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>
        
        where THyperEdge           : IGenericHyperEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>

        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable
        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable
        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable

        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable
        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable
        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable;

    #endregion

    #region HyperEdgeInitializer

    /// <summary>
    /// A delegate for hyperedge initializing.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TDataVertex">The type of the data stored within a vertex.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TDataEdge">The type of the data stored within an edge.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TDataHyperEdge">The type of the data stored within a hyperedge.</typeparam>
    /// 
    /// <param name="GenericHyperEdge">A generic hyperedge.</param>
    public delegate void HyperEdgeInitializer<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>(

                                              IGenericHyperEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> GenericHyperEdge)

        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable
        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable
        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable

        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable
        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable
        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable;

    #endregion

    #region HyperEdgeFilter

    /// <summary>
    /// A delegate for hyperedge filtering.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TDataVertex">The type of the data stored within a vertex.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TDataEdge">The type of the data stored within an edge.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TDataHyperEdge">The type of the data stored within a hyperedge.</typeparam>
    /// 
    /// <param name="GenericHyperEdge">A generic hyperedge.</param>
    public delegate Boolean HyperEdgeFilter<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge>(

                                            IGenericHyperEdge<TIdVertex,    TRevisionIdVertex,                     TDataVertex,
                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TDataEdge,
                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TDataHyperEdge> GenericHyperEdge)

        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable
        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable
        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable

        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable
        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable
        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable;

    #endregion

}
