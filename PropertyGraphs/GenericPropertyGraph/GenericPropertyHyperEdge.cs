/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
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
using System.Linq.Expressions;
using System.Collections.Generic;

using eu.Vanaheimr.Illias.Commons;
using eu.Vanaheimr.Styx;
using eu.Vanaheimr.Illias.Commons.Votes;

#endregion

namespace eu.Vanaheimr.Balder.InMemory
{

    /// <summary>
    /// A hyperedge maintains pointers to a set of vertices.
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
    public class GenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                          TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                                          : AGraphElement<TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IDynamicGraphElement<GenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
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

        /// <summary>
        /// The vertex at the tail of this edge.
        /// </summary>
        protected readonly IGroupedCollection<TIdVertex, IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>, TVertexLabel> _Vertices;


        private readonly Func<IVote<Boolean>> VoteCreator;


        /// <summary>
        /// A delegate to decide which vertices to match.
        /// </summary>
        public readonly VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexSelector;

        #endregion

        #region Properties

        #region Graph

        /// <summary>
        /// The associated property graph.
        /// </summary>
        protected readonly IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph;

        /// <summary>
        /// The associated property graph.
        /// </summary>
        IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            Graph
            {
                get
                {
                    return Graph;
                }
            }

        #endregion

        #endregion

        #region Constructor(s)

        #region GenericPropertyHyperEdge(Graph, Id, Label, IdKey, RevIdKey, LabelKey, DatastructureInitializer, VerticesCollectionInitializer, HyperEdgeInitializer = null, Vertices = null, VertexSelector = null)

        /// <summary>
        /// Creates a new hyperedge.
        /// </summary>
        /// <param name="Graph">The associated property graph.</param>
        /// <param name="Id">The identification of this hyperedge.</param>
        /// <param name="Label">A label stored within this hyperedge.</param>
        /// <param name="IdKey">The key of the hyperedge identifier.</param>
        /// <param name="RevIdKey">The key of the hyperedge revision identifier.</param>
        /// <param name="LabelKey">The key to access the Label of this graph element.</param>
        /// <param name="DatastructureInitializer">A delegate to initialize the properties datastructure.</param>
        /// <param name="VerticesCollectionInitializer">A delegate to initialize the datastructure for storing the vertices.</param>
        /// <param name="VoteCreator">A delegate to create a new vote.</param>
        /// <param name="HyperEdgeInitializer">A delegate to initialize the newly created hyperedge.</param>
        /// <param name="VertexSelector">A delegate matching the vertices connected by this hyperedge.</param>
        /// <param name="Vertices">The vertices connected by this hyperedge.</param>
        public GenericPropertyHyperEdge(IReadOnlyGenericPropertyGraph<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex,
                                                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph,

                                        TIdHyperEdge     Id,
                                        THyperEdgeLabel  Label,
                                        TKeyHyperEdge    IdKey,
                                        TKeyHyperEdge    RevIdKey,
                                        TKeyHyperEdge    LabelKey,

                                        IDictionaryInitializer<TKeyHyperEdge, TValueHyperEdge> DatastructureInitializer,

                                        VertexCollectionInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VerticesCollectionInitializer,

                                        Func<IVote<Boolean>> VoteCreator = null,

                                        HyperEdgeInitializer<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeInitializer = null,

                                        VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexSelector = null,

                                        IEnumerable<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Vertices = null)

            : base(Id, Label, IdKey, RevIdKey, LabelKey, DatastructureInitializer, VoteCreator: VoteCreator)

        {

            #region Initial checks

            if (Graph == null)
                throw new ArgumentNullException("The given graph must not be null!");

            if (VerticesCollectionInitializer == null)
                throw new ArgumentNullException("The given VerticesCollectionInitializer must not be null!");

            #endregion

            this.Graph           = Graph;
            this._Vertices       = VerticesCollectionInitializer();
            this.VoteCreator     = (VoteCreator != null) ? VoteCreator : () => new VetoVote();

            this.VertexAddition  = new VotingNotificator<IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                         IReadOnlyGenericPropertyVertex   <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                         Boolean>(VoteCreator, true);

            this.VertexRemoval   = new VotingNotificator<IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                         IReadOnlyGenericPropertyVertex   <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                         Boolean>(VoteCreator, true);

            if (HyperEdgeInitializer != null)
                HyperEdgeInitializer(this);

            this.VertexSelector = (VertexSelector != null) ? VertexSelector : v => true;

            if (Vertices != null)
                Vertices.ForEach(Vertex => AddVertexToHyperEdge(Vertex));

        }

        #endregion

        #endregion


        #region Vertex methods

        #region OnVertexAddition

        private readonly IVotingNotificator<IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyVertex   <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> VertexAddition;

        /// <summary>
        /// Called whenever a vertex will be or was added to the hyperedge.
        /// </summary>
        IVotingNotification<IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            IReadOnlyGenericPropertyVertex   <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            Boolean>

            IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.OnVertexAddition

        {
            get
            {
                return VertexAddition;
            }
        }

        #endregion

        #region (private) AddVertexToHyperEdge(Vertex)

        /// <summary>
        /// Add the given vertex to this hyperedge.
        /// </summary>
        /// <param name="Vertex">A vertex.</param>
        /// <returns>The added vertex</returns>
        IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            AddVertexToHyperEdge(IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex)

        {

            if (VertexSelector(Vertex))
                if (VertexAddition.SendVoting(this, Vertex))
                {
                    if (_Vertices.TryAddValue(Vertex.Id, Vertex.AsMutable(), Vertex.Label))
                    {
                        VertexAddition.SendNotification(this, Vertex);
                        return Vertex;
                    }
                }

            return null;

        }

        #endregion

        #region AddVertex(Vertex)

        /// <summary>
        /// Adds the given vertex, and returns it again.
        /// An exception will be thrown if the vertex identifier is already being
        /// used by the hyperedge to reference another vertex.
        /// </summary>
        /// <param name="Vertex">A vertex.</param>
        /// <returns>The added vertex.</returns>
        IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IVertexMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                AddVertex(IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex)

        {

            #region Initial checks

            if (Vertex == null)
                throw new ArgumentNullException("The given vertex must not be null!");

            if (Vertex.Id == null)
                throw new ArgumentNullException("The unique identification of vertex must not be null!");

            if (_Vertices.ContainsKey(Vertex.Id))
                throw new DuplicateVertexIdException<TIdVertex>(Vertex.Id);

            #endregion

            return AddVertexToHyperEdge(Vertex);

        }

        #endregion

        #region AddVertexIfNotExists(Vertex, CheckExistanceDelegate = null)

        /// <summary>
        /// Adds the given vertex if the given check existance
        /// delegate returns true and the vertex identifier is not already
        /// being used by the graph to reference another vertex.
        /// </summary>
        /// <param name="Vertex">A Vertex.</param>
        /// <param name="CheckExistanceDelegate">A delegate the check the existance of the given vertex within the hyperedge.</param>
        /// <returns>The added vertex.</returns>
        IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            AddVertexIfNotExists(IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex,

                                 CheckVertexExistanceInHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> CheckExistanceDelegate)

        {

            #region Initial checks

            if (Vertex == null)
                throw new ArgumentNullException("The given vertex must not be null!");

            if (Vertex.Id == null || Vertex.Id.Equals(default(TIdVertex)))
                throw new ArgumentNullException("The unique identification of vertex must not be null!");

            if (_Vertices.ContainsKey(Vertex.Id))
                throw new DuplicateVertexIdException<TIdVertex>(Vertex.Id);

            #endregion

            if (CheckExistanceDelegate != null && !CheckExistanceDelegate(this, Vertex))
                throw new Exception("CheckExistanceDelegate constraint is violated!");

            return AddVertexToHyperEdge(Vertex);

        }

        #endregion


        #region VertexById(Id)

        /// <summary>
        /// Return the vertex referenced by the given vertex identifier.
        /// If no vertex is referenced by the identifier return null.
        /// </summary>
        /// <param name="Id">A vertex identifier.</param>
        IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

            IReadOnlyVertexMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                VertexById(TIdVertex Id)

        {

            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex = null;

            if (Id != null && _Vertices.TryGetByKey(Id, out Vertex))
                return Vertex;

            return null;

        }

        #endregion

        #region TryGetVertexById(Id)

        /// <summary>
        /// Try to return the vertex referenced by the given vertex identifier.
        /// </summary>
        /// <param name="Id">A vertex identifier.</param>
        /// <param name="Vertex">A vertex.</param>
        /// <returns>True when success; false otherwise.</returns>
        Boolean IReadOnlyVertexMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                TryGetVertexById(TIdVertex Id,
                                 out IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex)

        {

            if (Id != null && _Vertices.TryGetByKey(Id, out Vertex))
                return true;

            Vertex = null;
            return false;

        }

        #endregion

        #region HasVertexId(Id)

        /// <summary>
        /// Check if the given vertex identifier already exists within the graph.
        /// </summary>
        /// <param name="VertexId">A vertex identifier.</param>
        /// <returns>True when yes; false otherwise.</returns>
        Boolean IReadOnlyVertexMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            HasVertexId(TIdVertex Id)

        {

            if (Id == null)
                return false;

            return _Vertices.ContainsKey(Id);

        }

        #endregion

        #region VerticesById(params Ids)

        /// <summary>
        /// Return the vertices referenced by the given array of vertex identifiers.
        /// If no vertex is referenced by a given identifier this value will be null.
        /// </summary>
        /// <param name="Ids">An array of vertex identifiers.</param>
        IEnumerable<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IReadOnlyVertexMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                VerticesById(params TIdVertex[] Ids)

        {

            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex;

            if (Ids != null)
                foreach (var Id in Ids)
                {

                    if (Id != null && _Vertices.TryGetByKey(Id, out Vertex))
                        yield return Vertex;

                    else
                        yield return null;

                }

        }

        #endregion

        #region VerticesByLabel(params Labels)

        /// <summary>
        /// Return all vertices having one of the given vertex labels.
        /// </summary>
        /// <param name="Labels">An array of vertex labels.</param>
        IEnumerable<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IReadOnlyVertexMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                VerticesByLabel(params TVertexLabel[] Labels)

        {

            IEnumerable<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Vertices;

            if (Labels != null)
                foreach (var Label in Labels)
                    if (Label != null && _Vertices.TryGetByGroup(Label, out Vertices))
                        foreach (var Vertex in Vertices)
                            yield return Vertex;

        }

        #endregion

        #region Vertices(Include = null)

        /// <summary>
        /// Return all vertices or only the ones matching the given filter delegate.
        /// </summary>
        /// <param name="Include">An optional delegate to select the vertices to be returned.</param>
        IEnumerable<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            IReadOnlyVertexMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

                Vertices(VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Include)

        {

            return (Include == null) ? _Vertices
                                     : _Vertices.Where(v => Include(v));

        }

        #endregion

        #region NumberOfVertices(Include = null)

        /// <summary>
        /// Return the total number of vertices or only the number of vertices matching the given filter delegate.
        /// When the filter is null, this method should implement an optimized
        /// way to get the currenty number of vertices.
        /// </summary>
        /// <param name="Include">An optional delegate to select the vertices to be counted.</param>
        UInt64 IReadOnlyVertexMethods<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex,
                                      TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            NumberOfVertices(VertexFilter<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex,
                                          TIdEdge, TRevIdEdge, TEdgeLabel, TKeyEdge, TValueEdge,
                                          TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                          TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Include)
        {

            return (Include == null) ? _Vertices.Count()
                                     : Convert.ToUInt64(_Vertices.LongCount(v => Include(v)));

        }

        #endregion


        #region OnVertexRemoval

        private readonly IVotingNotificator<IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            IReadOnlyGenericPropertyVertex   <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                            Boolean> VertexRemoval;

        /// <summary>
        /// Called whenever a vertex will be or was removed from the hyperedge.
        /// </summary>
        IVotingNotification<IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            IReadOnlyGenericPropertyVertex   <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                            Boolean>

            IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.OnVertexRemoval

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
        void IVertexMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

            RemoveVerticesById(params TIdVertex[] VertexIds)

        {

            #region Initial checks

            if (VertexIds == null)
                return;

            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex;

            #endregion

            foreach (var VertexId in VertexIds)
            {
                if (_Vertices.TryGetByKey(VertexId, out Vertex))
                    this.Graph.AsMutable().RemoveVertices(Vertex);
                else
                    throw new ArgumentException("The given vertex identifier '" + VertexId.ToString() + "' is unknown!");
            }

        }

        #endregion

        #region RemoveVertices(params Vertices)

        /// <summary>
        /// Remove the given array of vertices from the graph.
        /// Upon removing a vertex, all the edges by which the vertex
        /// is connected will be removed as well.
        /// </summary>
        /// <param name="Vertices">An array of vertices to be removed from the graph.</param>
        void IVertexMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

             RemoveVertices(params IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] Vertices)

        {

            #region Initial checks

            if (Vertices == null)
                return;

            #endregion

            lock (this)
            {

                foreach (var Vertex in Vertices)
                    if (_Vertices.ContainsKey(Vertex.Id))
                    {

                        if (Vertex.OutDegree() > 0 || Vertex.InDegree() > 0)
                            throw new Exception("Vertex '" + Vertex.Id + "' could not be removed, as there are still edges attached!");

                        if (VertexRemoval.SendVoting(this, Vertex))
                        {

                            // Remove the vertex from any 'indexing' hyperedge...
                            Vertex.HyperEdges().ForEach(HyperEdge => HyperEdge.AsMutable().RemoveVertices(Vertex));

                            _Vertices.TryRemoveValue(Vertex.Id, Vertex.AsMutable(), Vertex.Label);
                            VertexRemoval.SendNotification(this, Vertex);

                        }

                    }

            }

        }

        #endregion

        #region RemoveVertices(VertexFilter = null)

        /// <summary>
        /// Remove each vertex matching the given filter.
        /// </summary>
        /// <param name="VertexFilter">A delegate for vertex filtering.</param>
        void IVertexMethods<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>.

             RemoveVertices(VertexFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexFilter)
        {

            lock (this)
            {

                if (VertexFilter == null)
                    _Vertices.ToArray().
                        ForEach(Vertex => this.Graph.AsMutable().
                            RemoveVertices(Vertex));

                else
                    _Vertices.
                        Where(Vertex => VertexFilter(Vertex)).ToArray().
                        ForEach(Vertex => this.Graph.AsMutable().
                            RemoveVertices(Vertex));

            }

        }

        #endregion

        #endregion


        #region Operator overloading

        #region Operator == (PropertyHyperEdge1, PropertyHyperEdge2)

        /// <summary>
        /// Compares two edges for equality.
        /// </summary>
        /// <param name="PropertyHyperEdge1">A Edge.</param>
        /// <param name="PropertyHyperEdge2">Another Edge.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public static Boolean operator == (GenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                                                    PropertyHyperEdge1,
                                           GenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                                                    PropertyHyperEdge2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(PropertyHyperEdge1, PropertyHyperEdge2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) PropertyHyperEdge1 == null) || ((Object) PropertyHyperEdge2 == null))
                return false;

            return PropertyHyperEdge1.Equals(PropertyHyperEdge2);

        }

        #endregion

        #region Operator != (PropertyHyperEdge1, PropertyHyperEdge2)

        /// <summary>
        /// Compares two edges for equality.
        /// </summary>
        /// <param name="PropertyHyperEdge1">A Edge.</param>
        /// <param name="PropertyHyperEdge2">Another Edge.</param>
        /// <returns>False if both match; True otherwise.</returns>
        public static Boolean operator != (GenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                                                    PropertyHyperEdge1,
                                           GenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                                                    PropertyHyperEdge2)
        {
            return !(PropertyHyperEdge1 == PropertyHyperEdge2);
        }

        #endregion

        #region Operator <  (PropertyHyperEdge1, PropertyHyperEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyHyperEdge1">A Edge.</param>
        /// <param name="PropertyHyperEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <  (GenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                                                    PropertyHyperEdge1,
                                           GenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                                                    PropertyHyperEdge2)
        {

            if ((Object) PropertyHyperEdge1 == null)
                throw new ArgumentNullException("The given PropertyHyperEdge1 must not be null!");

            if ((Object) PropertyHyperEdge2 == null)
                throw new ArgumentNullException("The given PropertyHyperEdge2 must not be null!");

            return PropertyHyperEdge1.CompareTo(PropertyHyperEdge2) < 0;

        }

        #endregion

        #region Operator <= (PropertyHyperEdge1, PropertyHyperEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyHyperEdge1">A Edge.</param>
        /// <param name="PropertyHyperEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (GenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                                                    PropertyHyperEdge1,
                                           GenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                                                    PropertyHyperEdge2)
        {
            return !(PropertyHyperEdge1 > PropertyHyperEdge2);
        }

        #endregion

        #region Operator >  (PropertyHyperEdge1, PropertyHyperEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyHyperEdge1">A Edge.</param>
        /// <param name="PropertyHyperEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >  (GenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                                                    PropertyHyperEdge1,
                                           GenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                                                    PropertyHyperEdge2)
        {

            if ((Object) PropertyHyperEdge1 == null)
                throw new ArgumentNullException("The given PropertyHyperEdge1 must not be null!");

            if ((Object) PropertyHyperEdge2 == null)
                throw new ArgumentNullException("The given PropertyHyperEdge2 must not be null!");

            return PropertyHyperEdge1.CompareTo(PropertyHyperEdge2) > 0;

        }

        #endregion

        #region Operator >= (PropertyHyperEdge1, PropertyHyperEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyHyperEdge1">A Edge.</param>
        /// <param name="PropertyHyperEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (GenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                                                    PropertyHyperEdge1,
                                           GenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                                                    PropertyHyperEdge2)
        {
            return !(PropertyHyperEdge1 < PropertyHyperEdge2);
        }

        #endregion

        #endregion

        #region IDynamicGraphObject<PropertyHyperEdge> Members

        #region GetMetaObject(Expression)

        /// <summary>
        /// Return the appropriate DynamicMetaObject.
        /// </summary>
        /// <param name="Expression">An Expression.</param>
        public virtual DynamicMetaObject GetMetaObject(Expression Expression)
        {
            return new DynamicGraphElement<GenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
                                                                    (Expression, this);
        }

        #endregion

        #endregion

        #region IComparable Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public override Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given Object must not be null!");

            // Check if the given object can be casted to a GenericPropertyHyperEdge<...>
            var GenericPropertyHyperEdge = Object as GenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;

            if ((Object) GenericPropertyHyperEdge == null)
                throw new ArgumentException("The given object is not a GenericPropertyHyperEdge<...>!");

            return CompareTo(GenericPropertyHyperEdge);

        }

        #endregion

        #region CompareTo(IGenericPropertyHyperEdge)

        /// <summary>
        /// Compares two generic property hyperedges.
        /// </summary>
        /// <param name="IGenericPropertyHyperEdge">A generic property hyperedge to compare with.</param>
        public Int32 CompareTo(IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyHyperEdge)
        {

            if ((Object) IGenericPropertyHyperEdge == null)
                throw new ArgumentNullException("IPropertyHyperEdge", "The given IPropertyHyperEdge must not be null!");

            return Id.CompareTo(IGenericPropertyHyperEdge.Id);

        }

        #endregion

        #region CompareTo(IReadOnlyGenericPropertyHyperEdge)

        /// <summary>
        /// Compares two generic property hyperedges.
        /// </summary>
        /// <param name="IReadOnlyGenericPropertyHyperEdge">A generic property hyperedge to compare with.</param>
        public Int32 CompareTo(IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IReadOnlyGenericPropertyHyperEdge)
        {

            if ((Object)IReadOnlyGenericPropertyHyperEdge == null)
                throw new ArgumentNullException("IReadOnlyGenericPropertyHyperEdge", "The given IReadOnlyGenericPropertyHyperEdge must not be null!");

            return Id.CompareTo(IReadOnlyGenericPropertyHyperEdge.Id);

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

            // Check if the given object can be casted to a PropertyHyperEdge
            var PropertyHyperEdge = Object as GenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;

            if ((Object) PropertyHyperEdge == null)
                return false;

            return this.Equals(PropertyHyperEdge);

        }

        #endregion

        #region Equals(IGenericPropertyHyperEdge)

        /// <summary>
        /// Compares two generic property hyperedges for equality.
        /// </summary>
        /// <param name="IGenericPropertyHyperEdge">A property edge to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IGenericPropertyHyperEdge)
        {

            if ((Object) IGenericPropertyHyperEdge == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return Id.Equals(IGenericPropertyHyperEdge.Id);

        }

        #endregion

        #region Equals(IReadOnlyGenericPropertyHyperEdge)

        /// <summary>
        /// Compares two generic property hyperedges for equality.
        /// </summary>
        /// <param name="IReadOnlyGenericPropertyHyperEdge">A property edge to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IReadOnlyGenericPropertyHyperEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IReadOnlyGenericPropertyHyperEdge)
        {

            if ((Object) IReadOnlyGenericPropertyHyperEdge == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return Id.Equals(IReadOnlyGenericPropertyHyperEdge.Id);

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
            return "GenericPropertyHyperEdge [Id: '" + Id.ToString() + "']";
        }

        #endregion

    }

}
