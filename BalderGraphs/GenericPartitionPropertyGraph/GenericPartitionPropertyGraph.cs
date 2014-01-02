/*
 * Copyright (c) 2010-2014, Achim 'ahzf' Friedland <achim@graphdefined.org>
 * This file is part of Balder <http://www.github.com/Vanaheimr/Balder>
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
using System.Dynamic;
using System.Threading;
using System.Linq.Expressions;
using System.Collections.Generic;

using eu.Vanaheimr.Illias.Commons;
using eu.Vanaheimr.Illias.Commons.Collections;
using eu.Vanaheimr.Styx;
using eu.Vanaheimr.Illias.Commons.Votes;
using eu.Vanaheimr.Illias.Commons.Transactions;
using eu.Vanaheimr.Styx.Arrows;

#endregion

namespace eu.Vanaheimr.Balder.InMemory
{

    /// <summary>
    /// A wrapper to combine multiple graphs to a single graph representation.
    /// The first graph may be used for writing, all others just for reading.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TVertexLabel">The type of the vertex type.</typeparam>
    /// <typeparam name="TKeyVertex">The type of the vertex property keys.</typeparam>
    /// <typeparam name="TValueVertex">The type of the vertex property values.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
    /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
    /// 
    /// <typeparam name="TIdMultiEdge">The type of the multiedge identifiers.</typeparam>
    /// <typeparam name="TRevIdMultiEdge">The type of the multiedge revision identifiers.</typeparam>
    /// <typeparam name="TMultiEdgeLabel">The type of the multiedge label.</typeparam>
    /// <typeparam name="TKeyMultiEdge">The type of the multiedge property keys.</typeparam>
    /// <typeparam name="TValueMultiEdge">The type of the multiedge property values.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
    /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
    public class GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                     : IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                       IDynamicGraphElement<GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

        where TIdVertex        : IEquatable<TIdVertex>,       IComparable<TIdVertex>,       IComparable, TValueVertex
        where TIdEdge          : IEquatable<TIdEdge>,         IComparable<TIdEdge>,         IComparable, TValueEdge
        where TIdMultiEdge     : IEquatable<TIdMultiEdge>,    IComparable<TIdMultiEdge>,    IComparable, TValueMultiEdge
        where TIdHyperEdge     : IEquatable<TIdHyperEdge>,    IComparable<TIdHyperEdge>,    IComparable, TValueHyperEdge

        where TRevIdVertex     : IEquatable<TRevIdVertex>,    IComparable<TRevIdVertex>,    IComparable, TValueVertex
        where TRevIdEdge       : IEquatable<TRevIdEdge>,      IComparable<TRevIdEdge>,      IComparable, TValueEdge
        where TRevIdMultiEdge  : IEquatable<TRevIdMultiEdge>, IComparable<TRevIdMultiEdge>, IComparable, TValueMultiEdge
        where TRevIdHyperEdge  : IEquatable<TRevIdHyperEdge>, IComparable<TRevIdHyperEdge>, IComparable, TValueHyperEdge

        where TVertexLabel     : IEquatable<TVertexLabel>,    IComparable<TVertexLabel>,    IComparable, TValueVertex
        where TEdgeLabel       : IEquatable<TEdgeLabel>,      IComparable<TEdgeLabel>,      IComparable, TValueEdge
        where TMultiEdgeLabel  : IEquatable<TMultiEdgeLabel>, IComparable<TMultiEdgeLabel>, IComparable, TValueMultiEdge
        where THyperEdgeLabel  : IEquatable<THyperEdgeLabel>, IComparable<THyperEdgeLabel>, IComparable, TValueHyperEdge

        where TKeyVertex       : IEquatable<TKeyVertex>,      IComparable<TKeyVertex>,      IComparable
        where TKeyEdge         : IEquatable<TKeyEdge>,        IComparable<TKeyEdge>,        IComparable
        where TKeyMultiEdge    : IEquatable<TKeyMultiEdge>,   IComparable<TKeyMultiEdge>,   IComparable
        where TKeyHyperEdge    : IEquatable<TKeyHyperEdge>,   IComparable<TKeyHyperEdge>,   IComparable

    {

        #region Data

        private readonly IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> WriteGraph;

        private readonly IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] ReadGraphs;

        #endregion

        #region Properties

        #region IdKey

        /// <summary>
        /// The property key of the identification.
        /// </summary>
        public TKeyVertex IdKey
        {
            get
            {
                return WriteGraph.IdKey;
            }
        }

        #endregion

        #region Id

        /// <summary>
        /// An identifier that is unique to its inheriting class.
        /// All vertices, edges, multiedges and hyperedges of a graph must have an unique identifier.
        /// </summary>
        public TIdVertex Id
        {
            get
            {
                return WriteGraph.Id;
            }
        }

        #endregion

        #region RevIdKey

        /// <summary>
        /// The property key of the revision identification.
        /// </summary>
        public TKeyVertex RevIdKey
        {
            get
            {
                return WriteGraph.RevIdKey;
            }
        }

        #endregion

        #region RevId

        /// <summary>
        /// The RevId extends the Id to identify multiple revisions of
        /// an element during the lifetime of a graph. A RevId should
        /// additionally be unique among all elements of a graph.
        /// </summary>
        public TRevIdVertex RevId
        {
            get
            {
                return WriteGraph.RevId;
            }
        }

        #endregion

        #region LabelKey

        /// <summary>
        /// The property key of the label.
        /// </summary>
        public TKeyVertex LabelKey
        {
            get
            {
                return WriteGraph.LabelKey;
            }
        }

        #endregion

        #region Label

        public TVertexLabel Label
        {
            get
            {
                return WriteGraph.Label;
            }
        }

        #endregion

        #endregion

        #region Events

        #region OnGraphShuttingdown

        /// <summary>
        /// Called whenever a property graph will be shutting down.
        /// </summary>
        public event GraphShuttingdownEventHandler<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnGraphShuttingdown;

        #endregion

        #region OnGraphShutteddown

        /// <summary>
        /// Called whenever a property graph was shutted down.
        /// </summary>
        public event GraphShutteddownEventHandler<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnGraphShutteddown;

        #endregion



        public event MultiEdgeAddingEventHandler<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnMultiEdgeAdding;

        public event MultiEdgeAddedEventHandler<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnMultiEdgeAdded;

        public event HyperEdgeAddingEventHandler<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnHyperEdgeAdding;

        public event HyperEdgeAddedEventHandler<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnHyperEdgeAdded;


        #endregion

        #region Constructor(s)

        #region GenericPartitionPropertyGraph(GraphId, Description = null, WriteGraph = null, params ReadGraphs)

        /// <summary>
        /// Create a wrapper to combine multiple graphs to a single graph representation.
        /// The first graph may be used for writing, all others just for reading.
        /// </summary>
        /// <param name="GraphId">The unique identifiction of this graph.</param>
        /// <param name="Description">The description of this graph.</param>
        /// <param name="WriteGraph">An optional single graph for all write operations.</param>
        /// <param name="ReadGraphs">Multiple graphs for read-only operations.</param>
        public GenericPartitionPropertyGraph(TIdVertex GraphId,
                                             String Description = null,
                                             IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> WriteGraph = null,

                                             params IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] ReadGraphs)
        {

            #region Initial checks

            if (GraphId == null)
                throw new ArgumentNullException("GraphId", "The parameter 'GraphId' must not be null!");

            if (WriteGraph == null && (ReadGraphs == null || !ReadGraphs.Any()))
                throw new ArgumentNullException("WriteGraph and ReadGraphs", "The parameters 'WriteGraph' and 'ReadGraphs' must not both be null!");

            #endregion

            #region VertexAddition/-Removal

            this.VertexAddition   = new VotingNotificator<IReadOnlyGenericPropertyGraph <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          Boolean>(() => new VetoVote(), true);


            this.VertexRemoval   = new VotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          Boolean>(() => new VetoVote(), true);

            #endregion

            #region EdgeAddition/-Removal

            this.EdgeAddition     = new VotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          Boolean>(() => new VetoVote(), true);


            this.EdgeRemoval      = new VotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          Boolean>(() => new VetoVote(), true);

            #endregion

            #region MultiEdgeAddition/-Removal

            this.MultiEdgeAddition = new VotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          Boolean>(() => new VetoVote(), true);

            this.MultiEdgeRemoval = new VotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                          Boolean>(() => new VetoVote(), true);

            #endregion

            #region HyperEdgeAddition/-Removal

            this.HyperEdgeAddition = new VotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                        IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                        Boolean>(() => new VetoVote(), true);

            this.HyperEdgeRemoval = new VotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                        IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                        Boolean>(() => new VetoVote(), true);

            #endregion


            #region Process WriteGraph

            if (WriteGraph != null)
            {
                
                this.WriteGraph = WriteGraph;

                #region Subscribe to vertex events

                WriteGraph.OnVertexAddition.OnVoting += (graph, vertex, vote) => 
                    vote.VoteFor(this.VertexAddition.SendVoting(graph, vertex));

                WriteGraph.OnVertexAddition.OnNotification += this.VertexAddition.SendNotification;

                #endregion

                #region Subscribe to edge events

                WriteGraph.OnEdgeAddition.OnVoting += (graph, edge, vote) =>
                    vote.VoteFor(this.EdgeAddition.SendVoting(graph, edge));

                WriteGraph.OnEdgeAddition.OnNotification += this.EdgeAddition.SendNotification;

                #endregion

                //#region Subscribe to multiedge events

                //WriteGraph.OnMultiEdgeAdding += (graph, multiedge, vote) =>
                //{
                //    if (this.OnMultiEdgeAdding != null)
                //        OnMultiEdgeAdding(graph, multiedge, vote);
                //};

                //WriteGraph.OnMultiEdgeAdded += (graph, multiedge) =>
                //{
                //    if (this.OnMultiEdgeAdded != null)
                //        OnMultiEdgeAdded(graph, multiedge);
                //};

                //#endregion

                //#region Subscribe to hyperedge events

                //WriteGraph.OnHyperEdgeAdding += (graph, hyperedge, vote) =>
                //{
                //    if (this.OnHyperEdgeAdding != null)
                //        OnHyperEdgeAdding(graph, hyperedge, vote);
                //};

                //WriteGraph.OnHyperEdgeAdded += (graph, hyperedge) =>
                //{
                //    if (this.OnHyperEdgeAdded != null)
                //        OnHyperEdgeAdded(graph, hyperedge);
                //};

                //#endregion

            }

            #endregion

            #region Process ReadGraphs

            if (ReadGraphs != null)
            {

                this.ReadGraphs = ReadGraphs;

                ReadGraphs.ForEach(ReadGraph => {

                    #region Subscribe to vertex events

                    ReadGraph.OnVertexAddition.OnVoting += (graph, vertex, vote) =>
                        vote.VoteFor(this.VertexAddition.SendVoting(graph, vertex));

                    ReadGraph.OnVertexAddition.OnNotification += this.VertexAddition.SendNotification;

                    #endregion

                    #region Subscribe to edge events

                    ReadGraph.OnEdgeAddition.OnVoting += (graph, edge, vote) =>
                        vote.VoteFor(this.EdgeAddition.SendVoting(graph, edge));

                    ReadGraph.OnEdgeAddition.OnNotification += this.EdgeAddition.SendNotification;

                    #endregion

                    //#region Subscribe to multiedge events

                    //ReadGraph.OnMultiEdgeAdding += (graph, multiedge, vote) =>
                    //{
                    //    if (this.OnMultiEdgeAdding != null)
                    //        OnMultiEdgeAdding(graph, multiedge, vote);
                    //};

                    //ReadGraph.OnMultiEdgeAdded += (graph, multiedge) =>
                    //{
                    //    if (this.OnMultiEdgeAdded != null)
                    //        OnMultiEdgeAdded(graph, multiedge);
                    //};

                    //#endregion

                    //#region Subscribe to hyperedge events

                    //ReadGraph.OnHyperEdgeAdding += (graph, hyperedge, vote) =>
                    //{
                    //    if (this.OnHyperEdgeAdding != null)
                    //        OnHyperEdgeAdding(graph, hyperedge, vote);
                    //};

                    //ReadGraph.OnHyperEdgeAdded += (graph, hyperedge) =>
                    //{
                    //    if (this.OnHyperEdgeAdded != null)
                    //        OnHyperEdgeAdded(graph, hyperedge);
                    //};

                    //#endregion

                });

            }

            #endregion

        }

        #endregion

        #endregion


        #region IProperties Members

        #region Set(Key, Value)

        /// <summary>
        /// Add a KeyValuePair to the graph element.
        /// If a value already exists for the given key, then the previous value is overwritten.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <param name="Value">A value.</param>
        public virtual IProperties<TKeyVertex, TValueVertex> Set(TKeyVertex Key, TValueVertex Value)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region ContainsKey(Key)

        /// <summary>
        /// Determines if the specified key exists.
        /// </summary>
        /// <param name="Key">A key.</param>
        public Boolean ContainsKey(TKeyVertex Key)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ContainsValue(Value)

        /// <summary>
        /// Determines if the specified value exists.
        /// </summary>
        /// <param name="Value">A value.</param>
        public Boolean ContainsValue(TValueVertex Value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Contains(Key, Value)

        /// <summary>
        /// Determines if the given key and value exists.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <param name="Value">A value.</param>
        public Boolean Contains(TKeyVertex Key, TValueVertex Value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Contains(KeyValuePair)

        /// <summary>
        /// Determines if the given KeyValuePair exists.
        /// </summary>
        /// <param name="KeyValuePair">A KeyValuePair.</param>
        public Boolean Contains(KeyValuePair<TKeyVertex, TValueVertex> KeyValuePair)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region this[Key]

        /// <summary>
        /// Return the value associated with the given key.
        /// </summary>
        /// <param name="Key">A key.</param>
        public virtual TValueVertex this[TKeyVertex Key]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region TryGetProperty(Key, out Value)

        /// <summary>
        /// Return the value associated with the given key.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <param name="Value">The associated value.</param>
        /// <returns>True if the returned value is valid. False otherwise.</returns>
        public virtual Boolean TryGetProperty(TKeyVertex Key, out TValueVertex Value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region TryGetProperty<T>(Key, out Value)

        /// <summary>
        /// Return the value associated with the given key.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <param name="Value">The associated value.</param>
        /// <returns>True if the returned value is valid. False otherwise.</returns>
        public virtual Boolean TryGetProperty<T>(TKeyVertex Key, out T Value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region GetProperties(KeyValueFilter = null)

        /// <summary>
        /// Return a filtered enumeration of all KeyValuePairs.
        /// </summary>
        /// <param name="KeyValueFilter">A delegate to filter properties based on their keys and values.</param>
        /// <returns>A enumeration of all key/value pairs matching the given KeyValueFilter.</returns>
        public virtual IEnumerable<KeyValuePair<TKeyVertex, TValueVertex>> GetProperties(KeyValueFilter<TKeyVertex, TValueVertex> KeyValueFilter = null)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Keys

        /// <summary>
        /// An enumeration of all property keys.
        /// </summary>
        public IEnumerable<TKeyVertex> Keys
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Values

        /// <summary>
        /// An enumeration of all property values.
        /// </summary>
        public IEnumerable<TValueVertex> Values
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion


        #region Remove(Key)

        /// <summary>
        /// Removes all KeyValuePairs associated with the given key.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <returns>The value associated with that key prior to the removal.</returns>
        public virtual TValueVertex Remove(TKeyVertex Key)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Remove(Key, Value)

        /// <summary>
        /// Remove the given key and value pair.
        /// </summary>
        /// <param name="Key">A key.</param>
        /// <param name="Value">A value.</param>
        /// <returns>The value associated with that key prior to the removal.</returns>
        public TValueVertex Remove(TKeyVertex Key, TValueVertex Value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Remove(KeyValueFilter = null)

        /// <summary>
        /// Remove all KeyValuePairs specified by the given KeyValueFilter.
        /// Removing the Id or RevId property is not supported!
        /// </summary>
        /// <param name="KeyValueFilter">A delegate to remove properties based on their keys and values.</param>
        /// <returns>A enumeration of all key/value pairs removed by the given KeyValueFilter before their removal.</returns>
        public IEnumerable<KeyValuePair<TKeyVertex, TValueVertex>> Remove(KeyValueFilter<TKeyVertex, TValueVertex> KeyValueFilter = null)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// An enumerator of all key-value pairs stored.
        /// </summary>
        public IEnumerator<KeyValuePair<TKeyVertex, TValueVertex>> GetEnumerator()
        {
            return WriteGraph.GetEnumerator();
        }

        /// <summary>
        /// An enumerator of all key-value pairs stored.
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return WriteGraph.GetEnumerator();
        }

        #endregion


        #region Vertex methods [IGenericPropertyGraph]

        #region OnVertexAddition

        private readonly IVotingNotificator<IReadOnlyGenericPropertyGraph <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> VertexAddition;

        public IVotingSender<IReadOnlyGenericPropertyGraph <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                   IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                   Boolean>

            OnVertexAddition

        {
            get
            {
                return VertexAddition;
            }
        }

        #endregion


        #region AddVertex(Vertex, CheckExistanceDelegate = null)

        /// <summary>
        /// Adds the given vertex to the graph if the given check existance
        /// delegate returns true and the vertex identifier is not already
        /// being used by the graph to reference another vertex.
        /// </summary>
        /// <param name="Vertex">A Vertex.</param>
        /// <param name="CheckExistanceDelegate">A delegate the check the existance of the given vertex within the given graph.</param>
        /// <returns>The given vertex.</returns>
        public IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            AddVertex(IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex,

                      CheckVertexExistanceInGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> CheckExistanceDelegate = null)

        {

            if (WriteGraph != null)
                return WriteGraph.AddVertex(Vertex, CheckExistanceDelegate);

            throw new Exception("No WriteGraph present!");

        }

        #endregion


        #region VertexById(VertexId)

        /// <summary>
        /// Return the vertex referenced by the given vertex identifier.
        /// If no vertex is referenced by the identifier return null.
        /// </summary>
        /// <param name="VertexId">A vertex identifier.</param>
        public IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
            VertexById(TIdVertex VertexId)

        {

            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> _Vertex = null;

            if (WriteGraph != null)
            {

                _Vertex = WriteGraph.VertexById(VertexId);

                if (_Vertex != null)
                    return _Vertex;

            }

            if (ReadGraphs != null)
                foreach (var ReadGraph in ReadGraphs)
                {

                    _Vertex = ReadGraph.VertexById(VertexId);

                    if (_Vertex != null)
                        return _Vertex;

                }

            return null;

        }

        #endregion

        #region TryGetVertexById(VertexId)

        /// <summary>
        /// Try to return the vertex referenced by the given vertex identifier.
        /// </summary>
        /// <param name="VertexId">A vertex identifier.</param>
        /// <param name="Vertex">A vertex.</param>
        /// <returns>True when success; false otherwise.</returns>
        public Boolean TryGetVertexById(TIdVertex VertexId, out IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex)
        {

            if (WriteGraph != null &&
                WriteGraph.TryGetVertexById(VertexId, out Vertex))
                    return true;

            if (ReadGraphs != null)
                foreach (var ReadGraph in ReadGraphs)
                    if (ReadGraph.TryGetVertexById(VertexId, out Vertex))
                        return true;

            Vertex = null;

            return false;

        }

        #endregion

        #region VerticesById(params VertexIds)

        /// <summary>
        /// Return the vertices referenced by the given array of vertex identifiers.
        /// If no vertex is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="VertexIds">An array of vertex identifiers.</param>
        public IEnumerable<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                VerticesById(params TIdVertex[] VertexIds)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region VerticesByLabel(params VertexLabels)

        /// <summary>
        /// Return an enumeration of all vertices having one of the
        /// given vertex labels.
        /// </summary>
        /// <param name="VertexLabels">An array of vertex labels.</param>
        public IEnumerable<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                VerticesByLabel(params TVertexLabel[] VertexLabels)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region Vertices(VertexFilter = null)

        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// An optional vertex filter may be applied for filtering.
        /// </summary>
        /// <param name="VertexFilter">A delegate for vertex filtering.</param>
        public IEnumerable<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
        
                Vertices(VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexFilter = null)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region NumberOfVertices(VertexFilter = null)

        /// <summary>
        /// Return the current number of vertices matching the given optional vertex filter.
        /// When the filter is null, this method should use implement an optimized
        /// way to get the currenty number of vertices.
        /// </summary>
        /// <param name="VertexFilter">A delegate for vertex filtering.</param>
        public UInt64 NumberOfVertices(VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexFilter = null)

        {

            UInt64 _NumberOfVertices = 0;

            if (WriteGraph != null)
                _NumberOfVertices += WriteGraph.NumberOfVertices(VertexFilter);

            if (ReadGraphs != null)
                ReadGraphs.ForEach(ReadGraph => _NumberOfVertices += ReadGraph.NumberOfVertices(VertexFilter));

            return _NumberOfVertices;

        }

        #endregion


        #region OnVertexRemoval

        private readonly IVotingNotificator<IReadOnlyGenericPropertyGraph <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> VertexRemoval;

        public IVotingSender<IReadOnlyGenericPropertyGraph <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                   IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                   Boolean>

            OnVertexRemoval

        {
            get
            {
                return VertexRemoval;
            }
        }

        #endregion

        #region RemoveVerticesById(params VertexIds)

        /// <summary>
        /// Remove the vertex identified by the given VertexId from the graph
        /// </summary>
        /// <param name="VertexIds">An array of VertexIds of the vertices to remove.</param>
        public void RemoveVerticesById(params TIdVertex[] VertexIds)
        {

            if (WriteGraph != null)
                WriteGraph.RemoveVerticesById(VertexIds);

            throw new Exception("No WriteGraph present!");

        }

        #endregion

        #region RemoveVertices(params Vertices)

        /// <summary>
        /// Remove the given array of vertices from the graph.
        /// Upon removing a vertex, all the edges by which the vertex
        /// is connected will be removed as well.
        /// </summary>
        /// <param name="Vertices">An array of vertices to be removed from the graph.</param>
        public void RemoveVertices(params IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)
        {

            if (WriteGraph != null)
                WriteGraph.RemoveVertices(Vertices);

            throw new Exception("No WriteGraph present!");

        }

        #endregion

        #region RemoveVertices(VertexFilter = null)

        /// <summary>
        /// Remove each vertex matching the given filter.
        /// </summary>
        /// <param name="VertexFilter">A delegate for vertex filtering.</param>
        public void RemoveVertices(VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexFilter = null)
        {

            if (WriteGraph != null)
                WriteGraph.RemoveVertices(VertexFilter);

            throw new Exception("No WriteGraph present!");

        }

        #endregion

        #endregion

        #region Edge methods [IGenericPropertyGraph]

        #region OnEdgeAddition

        private readonly IVotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyEdge <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> EdgeAddition;

        public IVotingSender<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                   IReadOnlyGenericPropertyEdge <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                   Boolean>

            OnEdgeAddition

        {
            get
            {
                return OnEdgeAddition;
            }
        }

        #endregion



        #region EdgeById(EdgeId)

        /// <summary>
        /// Return the edge referenced by the given edge identifier.
        /// If no edge is referenced by a given identifier return null.
        /// </summary>
        /// <param name="EdgeId">An edge identifier.</param>
        public IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                EdgeById(TIdEdge EdgeId)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region TryGetEdgeById(EdgeId)

        /// <summary>
        /// Try to return the edge referenced by the given edge identifier.
        /// </summary>
        /// <param name="EdgeId">A edge identifier.</param>
        /// <param name="Edge">A edge.</param>
        /// <returns>True when success; false otherwise.</returns>
        public Boolean TryGetEdgeById(TIdEdge EdgeId, out IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region EdgesById(params EdgeIds)

        /// <summary>
        /// Return the edges referenced by the given array of edge identifiers.
        /// If no edge is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="EdgeIds">An array of edge identifiers.</param>
        public IEnumerable<IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                EdgesById(params TIdEdge[] EdgeIds)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region EdgesByLabel(params EdgeLabels)

        /// <summary>
        /// Return an enumeration of all edges having one of the
        /// given edge labels.
        /// </summary>
        /// <param name="EdgeLabels">An array of edge labels.</param>
        public IEnumerable<IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                EdgesByLabel(params TEdgeLabel[] EdgeLabels)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region Edges(EdgeFilter = null)

        /// <summary>
        /// Return an enumeration of all edges in the graph.
        /// An optional edge filter may be applied for filtering.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        public IEnumerable<IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                Edges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region NumberOfEdges(EdgeFilter = null)

        /// <summary>
        /// Return the current number of edges matching the given optional edge filter.
        /// When the filter is null, this method should use implement an optimized
        /// way to get the currenty number of edges.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        public UInt64 NumberOfEdges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)

        {
            throw new NotImplementedException();
        }

        #endregion


        #region OnEdgeRemoval

        private readonly IVotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyEdge <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> EdgeRemoval;

        public IVotingSender<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                   IReadOnlyGenericPropertyEdge <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                   Boolean>

            OnEdgeRemoval

        {
            get
            {
                return OnEdgeRemoval;
            }
        }

        #endregion

        #region RemoveEdgesById(params EdgeIds)

        /// <summary>
        /// Remove the given array of edges identified by their EdgeIds.
        /// </summary>
        /// <param name="EdgeIds">An array of EdgeIds of the edges to remove</param>
        public void RemoveEdgesById(params TIdEdge[] EdgeIds)
        {

            if (WriteGraph != null)
                WriteGraph.RemoveEdgesById(EdgeIds);

            throw new Exception("No WriteGraph present!");

        }

        #endregion

        #region RemoveEdges(params Edges)    // RemoveEdges()!

        /// <summary>
        /// Remove the given array of edges from the graph.
        /// </summary>
        /// <param name="Edges">An array of edges to be removed from the graph.</param>
        public void RemoveEdges(params IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Edges)
        {

            if (WriteGraph != null)
                WriteGraph.RemoveEdges(Edges);

            throw new Exception("No WriteGraph present!");

        }

        #endregion

        #region RemoveEdges(EdgeFilter = null)

        /// <summary>
        /// Remove any edge matching the given edge filter.
        /// </summary>
        /// <param name="EdgeFilter">A delegate for edge filtering.</param>
        public void RemoveEdges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)
        {

            if (WriteGraph != null)
                WriteGraph.RemoveEdges(EdgeFilter);

            throw new Exception("No WriteGraph present!");

        }

        #endregion

        #endregion

        #region MultiEdge methods [IGenericPropertyGraph]

        #region OnMultiEdgeAddition
        
        private readonly IVotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> MultiEdgeAddition;

        /// <summary>
        /// Called whenever a multiedge will be or was added to the graph.
        /// </summary>
        IVotingSender<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            Boolean>
            
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.OnMultiEdgeAddition

        {
            get
            {
                return MultiEdgeAddition;
            }
        }

        #endregion

        #region AddMultiEdge(params Vertices)

        /// <summary>
        /// Add a multiedge based on the given enumeration
        /// of vertices to the graph.
        /// </summary>
        /// <param name="Vertices">An enumeration of vertices.</param>
        /// <returns>The new multiedge</returns>
        public IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            AddMultiEdge(params IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region AddMultiEdge(Label, params Vertices)

        /// <summary>
        /// Add a multiedge based on the given multiedge label and
        /// an enumeration of vertices to the graph.
        /// </summary>
        /// <param name="Label">The multiedge label.</param>
        /// <param name="Vertices">An enumeration of vertices.</param>
        /// <returns>The new multiedge</returns>
        public IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
            AddMultiEdge(TMultiEdgeLabel Label,
                         params IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region AddMultiEdge(Label, MultiEdgeInitializer, params Vertices)

        /// <summary>
        /// Add a multiedge based on the given multiedge label and
        /// an enumeration of vertices to the graph and initialize
        /// it by invoking the given MultiEdgeInitializer.
        /// </summary>
        /// <param name="Label">The multiedge label.</param>
        /// <param name="MultiEdgeInitializer">A delegate to initialize the newly generated multiedge.</param>
        /// <param name="Vertices">An enumeration of vertices.</param>
        /// <returns>The new multiedge</returns>
        public IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
                AddMultiEdge(TMultiEdgeLabel Label,
                             MultiEdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeInitializer,
        
                             params IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region AddMultiEdge(MultiEdgeId, Label, MultiEdgeInitializer, params Vertices)

        /// <summary>
        /// Add a multiedge based on the given multiedge label and
        /// an enumeration of vertices to the graph and initialize
        /// it by invoking the given MultiEdgeInitializer.
        /// </summary>
        /// <param name="MultiEdgeId">A MultiEdgeId. If none was given a new one will be generated.</param>
        /// <param name="Label">The multiedge label.</param>
        /// <param name="MultiEdgeInitializer">A delegate to initialize the newly generated multiedge.</param>
        /// <param name="Vertices">An enumeration of vertices.</param>
        /// <returns>The new multiedge</returns>
        public IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> 
            
            AddMultiEdge(TIdMultiEdge    MultiEdgeId,
                         TMultiEdgeLabel Label,
                         MultiEdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeInitializer,        
                         params IGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region AddMultiEdge(IGenericPropertyMultiEdge)

        /// <summary>
        /// Adds the given multiedge to the graph, and returns it again.
        /// An exception will be thrown if the multiedge identifier is already being
        /// used by the graph to reference another multiedge.
        /// </summary>
        /// <param name="IGenericPropertyMultiEdge">An IGenericPropertyMultiEdge.</param>
        /// <returns>The given IGenericPropertyMultiEdge.</returns>
        public IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        
                       AddMultiEdge(IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyMultiEdge)
        
        {
            throw new NotImplementedException();
        }

        #endregion


        #region MultiEdgeById(MultiEdgeId)

        /// <summary>
        /// Return the MultiEdge referenced by the given MultiEdge identifier.
        /// If no MultiEdge is referenced by the identifier return null.
        /// </summary>
        /// <param name="MultiEdgeId">A MultiEdge identifier.</param>
        public IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                MultiEdgeById(TIdMultiEdge MultiEdgeId)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region MultiEdgesById(params MultiEdgeIds)

        /// <summary>
        /// Return the MultiEdges referenced by the given array of MultiEdge identifiers.
        /// If no MultiEdge is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="MultiEdgeIds">An array of MultiEdge identifiers.</param>
        public IEnumerable<IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                MultiEdgesById(params TIdMultiEdge[] MultiEdgeIds)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region MultiEdgesByLabel(params MultiEdgeLabels)

        /// <summary>
        /// Return an enumeration of all multiedges having one
        /// of the given multiedge labels.
        /// </summary>
        /// <param name="MultiEdgeLabels">An array of multiedge labels.</param>
        public IEnumerable<IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                MultiEdgesByLabel(params TMultiEdgeLabel[] MultiEdgeLabels)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region MultiEdges(MultiEdgeFilter = null)

        /// <summary>
        /// Get an enumeration of all MultiEdges in the graph.
        /// An optional MultiEdge filter may be applied for filtering.
        /// </summary>
        /// <param name="MultiEdgeFilter">A delegate for MultiEdge filtering.</param>
        public IEnumerable<IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                MultiEdges(MultiEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeFilter)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region NumberOfMultiEdges(MultiEdgeFilter = null)

        /// <summary>
        /// Return the current number of MultiEdges matching the given optional MultiEdge filter.
        /// When the filter is null, this method should implement an optimized
        /// way to get the currenty number of edges.
        /// </summary>
        /// <param name="MultiEdgeFilter">A delegate for MultiEdge filtering.</param>
        public UInt64 NumberOfMultiEdges(MultiEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeFilter)

        {
            throw new NotImplementedException();
        }

        #endregion


        #region OnMultiEdgeRemoval
        
        private readonly IVotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> MultiEdgeRemoval;


        /// <summary>
        /// Called whenever a multiedge will be or was removed from the graph.
        /// </summary>
        IVotingSender<IReadOnlyGenericPropertyGraph<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex,
                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            IReadOnlyGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            Boolean>
            
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.OnMultiEdgeRemoval

        {
            get
            {
                return MultiEdgeRemoval;
            }
        }

        #endregion

        #region RemoveMultiEdgesById(params MultiEdgeIds)

        /// <summary>
        /// Remove the given array of multiedges identified by their MultiEdgeIds.
        /// </summary>
        /// <param name="MultiEdgeIds">An array of MultiEdgeIds of the multiedges to remove.</param>
        public void RemoveMultiEdgesById(params TIdMultiEdge[] MultiEdgeIds)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region RemoveMultiEdges(params MultiEdges)    // RemoveMultiEdges()!

        /// <summary>
        /// Remove the given array of multiedges from the graph.
        /// </summary>
        /// <param name="MultiEdges">An array of multiedges to be removed from the graph.</param>
        public void RemoveMultiEdges(params IGenericPropertyMultiEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] MultiEdges)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region RemoveMultiEdges(MultiEdgeFilter = null)

        /// <summary>
        /// Remove any multiedge matching the given multiedge filter.
        /// </summary>
        /// <param name="MultiEdgeFilter">A delegate for multiedge filtering.</param>
        public void RemoveMultiEdges(MultiEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeFilter)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region HyperEdge methods [IGenericPropertyGraph]

        #region OnHyperEdgeAddition
        
        private readonly IVotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> HyperEdgeAddition;

        /// <summary>
        /// Called whenever a hyperedge will be or was added to the graph.
        /// </summary>
        IVotingSender<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            Boolean>
            
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.OnHyperEdgeAddition

        {
            get
            {
                return HyperEdgeAddition;
            }
        }

        #endregion

        #region AddHyperEdge(VertexSelector, params Vertices)

        /// <summary>
        /// Add a hyperedge based on the given enumeration
        /// of vertices to the graph.
        /// </summary>
        /// <param name="VertexSelector">A delegate to match the vertices to be connected by the hyperedge.</param>
        /// <param name="Vertices">An enumeration of vertices to be connected by the hyperedge.</param>
        /// <returns>The new hyperedge.</returns>
        public IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            AddHyperEdge(VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexSelector,

                         params IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region AddHyperEdge(Label, VertexSelector, params Vertices)

        /// <summary>
        /// Add a hyperedge based on the given multiedge label and
        /// an enumeration of vertices to the graph.
        /// </summary>
        /// <param name="Label">The multiedge label.</param>
        /// <param name="VertexSelector">A delegate to match the vertices to be connected by the hyperedge.</param>
        /// <param name="Vertices">An enumeration of vertices to be connected by the hyperedge.</param>
        /// <returns>The new hyperedge.</returns>
        public IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            AddHyperEdge(THyperEdgeLabel Label,

                         VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexSelector,

                         params IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region AddHyperEdge(Label, HyperEdgeInitializer, VertexFilter, params Vertices)

        /// <summary>
        /// Add a hyperedge based on the given multiedge label and
        /// an enumeration of vertices to the graph and initialize
        /// it by invoking the given HyperEdgeInitializer.
        /// </summary>
        /// <param name="Label">The multiedge label.</param>
        /// <param name="HyperEdgeInitializer">A delegate to initialize the newly generated multiedge.</param>
        /// <param name="VertexSelector">A delegate to match the vertices to be connected by the hyperedge.</param>
        /// <param name="Vertices">An enumeration of vertices to be connected by the hyperedge.</param>
        /// <returns>The new hyperedge.</returns>
        public IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            AddHyperEdge(THyperEdgeLabel Label,

                         HyperEdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeInitializer,

                         VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexSelector,

                         params IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region AddHyperEdge(Id, Label, HyperEdgeInitializer, VertexSelector, params Vertices)

        /// <summary>
        /// Add a hyperedge based on the given hyperedge label and
        /// an enumeration of vertices to the graph and initialize
        /// it by invoking the given HyperEdgeInitializer.
        /// </summary>
        /// <param name="Id">A HyperEdgeId. If none was given a new one will be generated.</param>
        /// <param name="Label">The multiedge label.</param>
        /// <param name="HyperEdgeInitializer">A delegate to initialize the newly generated hyperedge.</param>
        /// <param name="VertexSelector">A delegate to match the vertices to be connected by the hyperedge.</param>
        /// <param name="Vertices">An enumeration of vertices to be connected by the hyperedge.</param>
        /// <returns>The new hyperedge.</returns>
        public IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            AddHyperEdge(TIdHyperEdge     Id,
                         THyperEdgeLabel  Label,

                         HyperEdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeInitializer,

                         VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexSelector,

                         params IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)

        {
            throw new NotImplementedException();
        }

        #endregion


        #region AddHyperEdge(HyperEdge)

        /// <summary>
        /// Adds the given hyperedge to the graph, and returns it again.
        /// An exception will be thrown if the hyperedge identifier is already being
        /// used by the graph to reference another hyperedge.
        /// </summary>
        /// <param name="HyperEdge">A generic property hyperedge.</param>
        /// <returns>The given generic property hyperedge.</returns>
        public IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                       AddHyperEdge(IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdge)
        {

            if (WriteGraph != null)
                return WriteGraph.AddHyperEdge(HyperEdge).AsMutable();

            throw new Exception("No WriteGraph present!");

        }

        #endregion


        #region HyperEdgeById(HyperEdgeId)

        /// <summary>
        /// Return the HyperEdge referenced by the given HyperEdge identifier.
        /// If no HyperEdge is referenced by the identifier return null.
        /// </summary>
        /// <param name="HyperEdgeId">A HyperEdge identifier.</param>
        public IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                HyperEdgeById(TIdHyperEdge HyperEdgeId)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region HyperEdgesById(params HyperEdgeIds)

        /// <summary>
        /// Return the HyperEdges referenced by the given array of HyperEdge identifiers.
        /// If no HyperEdge is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="HyperEdgeIds">An array of HyperEdge identifiers.</param>
        public IEnumerable<IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                HyperEdgesById(params TIdHyperEdge[] HyperEdgeIds)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region HyperEdgesByLabel(params HyperEdgeLabels)

        /// <summary>
        /// Return an enumeration of all multiedges having one
        /// of the given multiedge labels.
        /// </summary>
        /// <param name="HyperEdgeLabels">An array of multiedge labels.</param>
        public IEnumerable<IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                HyperEdgesByLabel(params THyperEdgeLabel[] HyperEdgeLabels)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region HyperEdges(HyperEdgeFilter = null)

        /// <summary>
        /// Return an enumeration of all hyperedges in the graph.
        /// An optional hyperedge filter may be applied for filtering.
        /// </summary>
        /// <param name="HyperEdgeFilter">A delegate for hyperedge filtering.</param>
        public IEnumerable<IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                HyperEdges(HyperEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region NumberOfHyperEdges(HyperEdgeFilter = null)

        /// <summary>
        /// Return the current number of HyperEdges matching the given optional HyperEdge filter.
        /// When the filter is null, this method should implement an optimized
        /// way to get the currenty number of edges.
        /// </summary>
        /// <param name="HyperEdgeFilter">A delegate for HyperEdge filtering.</param>
        public UInt64 NumberOfHyperEdges(HyperEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter)

        {
            throw new NotImplementedException();
        }

        #endregion


        #region OnHyperEdgeRemoval
        
        private readonly IVotingNotificator<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> HyperEdgeRemoval;


        /// <summary>
        /// Called whenever a hyperedge will be or was removed from the graph.
        /// </summary>
        IVotingSender<IReadOnlyGenericPropertyGraph<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex,
                                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            Boolean>
            
            IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.OnHyperEdgeRemoval

        {
            get
            {
                return HyperEdgeRemoval;
            }
        }

        #endregion

        #region RemoveHyperEdgesById(params HyperEdgeIds)

        /// <summary>
        /// Remove the given array of hyperedges identified by their HyperEdgeIds.
        /// </summary>
        /// <param name="HyperEdgeIds">An array of HyperEdgeIds of the hyperedges to remove.</param>
        public void RemoveHyperEdgesById(params TIdHyperEdge[] HyperEdgeIds)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region RemoveHyperEdges(params HyperEdges)    // RemoveHyperEdges()!

        /// <summary>
        /// Remove hyperedges.
        /// </summary>
        /// <param name="HyperEdges">An array of outgoing edges to be removed.</param>
        public void RemoveHyperEdges(params IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] HyperEdges)

        {
            throw new NotImplementedException();
        }

        #endregion

        #region RemoveHyperEdges(HyperEdgeFilter = null)

        /// <summary>
        /// Remove any outgoing hyperedge matching
        /// the given hyperedge filter delegate.
        /// </summary>
        /// <param name="HyperEdgeFilter">A delegate for hyperedge filtering.</param>
        public void RemoveHyperEdges(HyperEdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter)

        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion


        #region Clear()

        /// <summary>
        /// Removes all the vertices, edges and hyperedges from the graph.
        /// </summary>
        public void Clear()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Shutdown()

        /// <summary>
        /// Shutdown and close the graph.
        /// </summary>
        /// <param name="Message">An optional message, e.g. a reason for the shutdown.</param>
        public void Shutdown(String Message)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Operator overloading

        #region Operator == (GenericPropertyGraph1, GenericPropertyGraph2)

        /// <summary>
        /// Compares two vertices for equality.
        /// </summary>
        /// <param name="GenericPropertyGraph1">A generic property graph.</param>
        /// <param name="GenericPropertyGraph2">Another generic property graph.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public static Boolean operator == (GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GenericPropertyGraph1,
                                           GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GenericPropertyGraph2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(GenericPropertyGraph1, GenericPropertyGraph2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) GenericPropertyGraph1 == null) || ((Object) GenericPropertyGraph2 == null))
                return false;

            return GenericPropertyGraph1.Equals(GenericPropertyGraph2);

        }

        #endregion

        #region Operator != (GenericPropertyGraph1, GenericPropertyGraph2)

        /// <summary>
        /// Compares two vertices for inequality.
        /// </summary>
        /// <param name="GenericPropertyGraph1">A generic property graph.</param>
        /// <param name="GenericPropertyGraph2">Another generic property graph.</param>
        /// <returns>False if both match; True otherwise.</returns>
        public static Boolean operator != (GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GenericPropertyGraph1,
                                           GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GenericPropertyGraph2)
        {
            return !(GenericPropertyGraph1 == GenericPropertyGraph2);
        }

        #endregion

        #region Operator <  (GenericPropertyGraph1, GenericPropertyGraph2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="GenericPropertyGraph1">A Vertex.</param>
        /// <param name="GenericPropertyGraph2">Another Vertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator  < (GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GenericPropertyGraph1,
                                           GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GenericPropertyGraph2)
        {

            if ((Object) GenericPropertyGraph1 == null)
                throw new ArgumentNullException("The given GenericPropertyGraph1 must not be null!");

            if ((Object) GenericPropertyGraph2 == null)
                throw new ArgumentNullException("The given GenericPropertyGraph2 must not be null!");

            return GenericPropertyGraph1.CompareTo(GenericPropertyGraph2) < 0;

        }

        #endregion

        #region Operator <= (GenericPropertyGraph1, GenericPropertyGraph2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="GenericPropertyGraph1">A Vertex.</param>
        /// <param name="GenericPropertyGraph2">Another Vertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GenericPropertyGraph1,
                                           GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GenericPropertyGraph2)
        {
            return !(GenericPropertyGraph1 > GenericPropertyGraph2);
        }

        #endregion

        #region Operator >  (GenericPropertyGraph1, GenericPropertyGraph2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="GenericPropertyGraph1">A Vertex.</param>
        /// <param name="GenericPropertyGraph2">Another Vertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >  (GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GenericPropertyGraph1,
                                           GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GenericPropertyGraph2)
        {

            if ((Object) GenericPropertyGraph1 == null)
                throw new ArgumentNullException("The given GenericPropertyGraph1 must not be null!");

            if ((Object) GenericPropertyGraph2 == null)
                throw new ArgumentNullException("The given GenericPropertyGraph2 must not be null!");

            return GenericPropertyGraph1.CompareTo(GenericPropertyGraph2) > 0;

        }

        #endregion

        #region Operator >= (GenericPropertyGraph1, GenericPropertyGraph2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="GenericPropertyGraph1">A Vertex.</param>
        /// <param name="GenericPropertyGraph2">Another Vertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GenericPropertyGraph1,
                                           GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GenericPropertyGraph2)
        {
            return !(GenericPropertyGraph1 < GenericPropertyGraph2);
        }

        #endregion

        #endregion

        #region IDynamicGraphObject<PropertyVertex> Members

        public IEnumerable<string> GetDynamicMemberNames()
        {
            throw new NotImplementedException();
        }

        #region GetMetaObject(Expression)

        /// <summary>
        /// Return the appropriate DynamicMetaObject.
        /// </summary>
        /// <param name="Expression">An Expression.</param>
        public virtual DynamicMetaObject GetMetaObject(Expression Expression)
        {
            return new DynamicGraphElement<GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
                                                                         (Expression, this);
        }

        #endregion


        #region SetMember(Binder, Object)

        /// <summary>
        /// Sets a new property or overwrites an existing.
        /// </summary>
        /// <param name="Binder">The property key</param>
        /// <param name="Object">The property value</param>
        public virtual Object SetMember(String Binder, Object Object)
        {
            return WriteGraph.Set((TKeyVertex) (Object) Binder, (TValueVertex) Object);
        }

        #endregion

        #region GetMember(Binder)

        /// <summary>
        /// Returns the value of the given property key.
        /// </summary>
        /// <param name="Binder">The property key.</param>
        public virtual Object GetMember(String Binder)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region DeleteMember(Binder)

        /// <summary>
        /// Tries to remove the property identified by the given property key.
        /// </summary>
        /// <param name="Binder">The property key.</param>
        public virtual Object DeleteMember(String Binder)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region IComparable Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given Object must not be null!");

            // Check if the given object can be casted to a GenericPartitionPropertyGraph<...>
            var GenericPartitionPropertyGraph = Object as GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;

            if ((Object) GenericPartitionPropertyGraph == null)
                throw new ArgumentException("The given object is not a GenericPartitionPropertyGraph<...>!");

            return CompareTo(GenericPartitionPropertyGraph);

        }

        #endregion

        public int CompareTo(TIdVertex other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(IReadOnlyGraphElement<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex> other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(IGraphElement<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex> other)
        {
            throw new NotImplementedException();
        }

        #region CompareTo(IGenericPartitionPropertyGraph)

        /// <summary>
        /// Compares two generic property vertices.
        /// </summary>
        /// <param name="IGenericPartitionPropertyGraph">A generic property vertex to compare with.</param>
        public Int32 CompareTo(IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyGraph)
        {

            if ((Object) IGenericPropertyGraph == null)
                throw new ArgumentNullException("The given IGenericPropertyGraph must not be null!");

            return Id.CompareTo(IGenericPropertyGraph.Id);

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

            // Check if the given object can be casted to a GenericPartitionPropertyGraph
            var PropertyVertex = Object as GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;

            if ((Object) PropertyVertex == null)
                return false;

            return this.Equals(PropertyVertex);

        }

        #endregion

        public bool Equals(TIdVertex other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IReadOnlyGraphElement<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex> other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IGraphElement<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex> other)
        {
            throw new NotImplementedException();
        }

        #region Equals(GenericPartitionPropertyGraph)

        /// <summary>
        /// Compares two generic property vertices for equality.
        /// </summary>
        /// <param name="GenericPartitionPropertyGraph">A generic property vertex to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GenericPartitionPropertyGraph)
        {
            
            if ((Object) GenericPartitionPropertyGraph == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return Id.Equals(GenericPartitionPropertyGraph.Id);

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

        #region ToString()

        /// <summary>
        /// Return a string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return "GenericPartitionPropertyGraph [Id: ";// +Id.ToString() + ", " + _OutEdges.Count() + " OutEdges, " + _InEdges.Count() + " InEdges]";
        }

        #endregion



        public Func<IVote<bool>> VoteCreator
        {
            get { throw new NotImplementedException(); }
        }

        public IReadOnlyGenericPropertyVertex<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> AddVertex(TVertexLabel Label, VertexInitializer<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexInitializer = null, VertexAction<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnDuplicateVertex = null, VertexInitializer<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> AnywayDo = null)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyGenericPropertyVertex<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> AddVertex(TIdVertex VertexId, TVertexLabel Label, VertexInitializer<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexInitializer = null, VertexAction<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnDuplicateVertex = null, VertexInitializer<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexIdAlreadyUsed = null, VertexInitializer<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> AnywayDo = null)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyGenericPropertyEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> AddEdge(IReadOnlyGenericPropertyVertex<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex, TEdgeLabel Label, IReadOnlyGenericPropertyVertex<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex, EdgeInitializer<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer = null, EdgeAction<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnDuplicateEdge = null, EdgeInitializer<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> AnywayDo = null)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyGenericPropertyEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> AddEdge(TIdEdge EdgeId, IReadOnlyGenericPropertyVertex<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OutVertex, TEdgeLabel Label, IReadOnlyGenericPropertyVertex<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> InVertex, EdgeInitializer<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer = null, EdgeAction<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> OnDuplicateEdge = null, EdgeInitializer<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeIdAlreadyUsed = null, EdgeInitializer<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> AnywayDo = null)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyGenericPropertyMultiEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> AddMultiEdge(EdgeFilter<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeSelector, params IReadOnlyGenericPropertyEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Edges)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyGenericPropertyMultiEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> AddMultiEdge(TMultiEdgeLabel Label, EdgeFilter<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeSelector, params IReadOnlyGenericPropertyEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Edges)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyGenericPropertyMultiEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> AddMultiEdge(TMultiEdgeLabel Label, MultiEdgeInitializer<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeInitializer, EdgeFilter<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeSelector, params IReadOnlyGenericPropertyEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Edges)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyGenericPropertyMultiEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> AddMultiEdge(TIdMultiEdge Id, TMultiEdgeLabel Label, MultiEdgeInitializer<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeInitializer, EdgeFilter<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeSelector, params IReadOnlyGenericPropertyEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Edges)
        {
            throw new NotImplementedException();
        }

        public Transaction<TIdVertex, TIdVertex, IGenericPropertyGraph<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> BeginTransaction(string Name = "", bool Distributed = false, bool LongRunning = false, IsolationLevel IsolationLevel = IsolationLevel.Write, DateTime? CreationTime = null, DateTime? InvalidationTime = null)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyGenericPropertyVertex<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> SuperVertex
        {
            get { throw new NotImplementedException(); }
        }

        public TVertexLabel DefaultVertexLabel
        {
            get { throw new NotImplementedException(); }
        }

        public TKeyEdge EdgeIdKey
        {
            get { throw new NotImplementedException(); }
        }

        public TKeyEdge EdgeRevIdKey
        {
            get { throw new NotImplementedException(); }
        }

        public TKeyEdge EdgeLabelKey
        {
            get { throw new NotImplementedException(); }
        }

        public TEdgeLabel DefaultEdgeLabel
        {
            get { throw new NotImplementedException(); }
        }

        public TKeyMultiEdge MultiEdgeIdKey
        {
            get { throw new NotImplementedException(); }
        }

        public TKeyMultiEdge MultiEdgeRevIdKey
        {
            get { throw new NotImplementedException(); }
        }

        public TKeyMultiEdge MultiEdgeLabelKey
        {
            get { throw new NotImplementedException(); }
        }

        public TMultiEdgeLabel DefaultMultiEdgeLabel
        {
            get { throw new NotImplementedException(); }
        }

        public TKeyHyperEdge HyperEdgeIdKey
        {
            get { throw new NotImplementedException(); }
        }

        public TKeyHyperEdge HyperEdgeRevIdKey
        {
            get { throw new NotImplementedException(); }
        }

        public TKeyHyperEdge HyperEdgeLabelKey
        {
            get { throw new NotImplementedException(); }
        }

        public THyperEdgeLabel DefaultHyperEdgeLabel
        {
            get { throw new NotImplementedException(); }
        }


        public bool HasVertexId(TIdVertex Id)
        {
            throw new NotImplementedException();
        }


        public bool HasEdgeId(TIdEdge Id)
        {
            throw new NotImplementedException();
        }

        IReadOnlyGenericPropertyMultiEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IReadOnlyMultiEdgeMethods<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.MultiEdgeById(TIdMultiEdge Id)
        {
            throw new NotImplementedException();
        }

        public bool TryGetMultiEdgeById(TIdMultiEdge Id, out IReadOnlyGenericPropertyMultiEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdge)
        {
            throw new NotImplementedException();
        }

        public bool HasMultiEdgeId(TIdMultiEdge Id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyMultiEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> IReadOnlyMultiEdgeMethods<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.MultiEdgesById(params TIdMultiEdge[] Ids)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyMultiEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> IReadOnlyMultiEdgeMethods<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.MultiEdgesByLabel(params TMultiEdgeLabel[] Labels)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyMultiEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> IReadOnlyMultiEdgeMethods<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.MultiEdges(MultiEdgeFilter<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Include = null)
        {
            throw new NotImplementedException();
        }

        IReadOnlyGenericPropertyHyperEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IReadOnlyHyperEdgeMethods<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.HyperEdgeById(TIdHyperEdge Id)
        {
            throw new NotImplementedException();
        }

        public bool TryGetHyperEdgeById(TIdHyperEdge Id, out IReadOnlyGenericPropertyHyperEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdge)
        {
            throw new NotImplementedException();
        }

        public bool HasHyperEdgeId(TIdHyperEdge Id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyHyperEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> IReadOnlyHyperEdgeMethods<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.HyperEdgesById(params TIdHyperEdge[] Ids)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyHyperEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> IReadOnlyHyperEdgeMethods<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.HyperEdgesByLabel(params THyperEdgeLabel[] Labels)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyHyperEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> IReadOnlyHyperEdgeMethods<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.HyperEdges(HyperEdgeFilter<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Include = null)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyGenericPropertyEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> AddEdge(IReadOnlyGenericPropertyEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge, CheckEdgeExistanceInGraph<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> CheckExistanceDelegate = null)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyGenericPropertyMultiEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> AddMultiEdge(IReadOnlyGenericPropertyMultiEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdge)
        {
            throw new NotImplementedException();
        }

        public void RemoveMultiEdges(params IReadOnlyGenericPropertyMultiEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] MultiEdges)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyGenericPropertyHyperEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> AddHyperEdge(IReadOnlyGenericPropertyHyperEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdge)
        {
            throw new NotImplementedException();
        }

        public void RemoveHyperEdges(params IReadOnlyGenericPropertyHyperEdge<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex, TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge, TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] HyperEdges)
        {
            throw new NotImplementedException();
        }

        public event PropertyAddingEventHandler<TKeyVertex, TValueVertex> OnPropertyAdding;

        public event PropertyAddedEventHandler<TKeyVertex, TValueVertex> OnPropertyAdded;

        public event PropertyChangingEventHandler<TKeyVertex, TValueVertex> OnPropertyChanging;

        public event PropertyChangedEventHandler<TKeyVertex, TValueVertex> OnPropertyChanged;

        public event PropertyRemovingEventHandler<TKeyVertex, TValueVertex> OnPropertyRemoving;

        public event PropertyRemovedEventHandler<TKeyVertex, TValueVertex> OnPropertyRemoved;


        public TKeyVertex VertexIdKey
        {
            get { throw new NotImplementedException(); }
        }

        public TKeyVertex VertexRevIdKey
        {
            get { throw new NotImplementedException(); }
        }

        public TKeyVertex VertexLabelKey
        {
            get { throw new NotImplementedException(); }
        }
    }

}
