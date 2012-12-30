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
using System.Linq;
using System.Threading;
using System.Collections.Generic;

using de.ahzf.Illias.Commons;

#endregion

namespace de.ahzf.Vanaheimr.Blueprints.InMemory
{

    #region GraphFactory

    /// <summary>
    /// Simplified creation of property graphs.
    /// </summary>
    public static class GraphFactory
    {

        #region Helpers

        #region (private) NewRevId

        /// <summary>
        /// Return a new random RevId.
        /// </summary>
        private static Int64 NewRevId
        {
            get
            {
                return (Int64) UniqueTimestamp.Ticks;
            }
        }

        #endregion

        #region IdCreatorDelegate<TId>()

        /// <summary>
        /// A delegate for creating a new TId
        /// (if no one was provided by the user).
        /// </summary>
        /// <returns>A valid TId.</returns>
        public delegate TId IdCreatorDelegate<TId>();

        #endregion

        #region RevIdCreatorDelegate<TRevId>()

        /// <summary>
        /// A delegate for creating a new RevisionId
        /// (if no one was provided by the user).
        /// </summary>
        /// <returns>A valid RevisionId.</returns>
        public delegate TRevId RevIdCreatorDelegate<TRevId>();

        #endregion

        #endregion


        #region CreatePropertyGraph(GraphId, Description = null, GraphInitializer = null)

        /// <summary>
        /// Create a new property graph.
        /// </summary>
        /// <param name="GraphId">The graph identification.</param>
        /// <param name="Description">The optional description of the graph.</param>
        /// <param name="GraphInitializer">The optional graph initializer.</param>
        public static IPropertyGraph CreatePropertyGraph(UInt64 GraphId,
                                                         String Description = null,
                                                         GraphInitializer<UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object,
                                                                          UInt64, Int64, String, String, Object> GraphInitializer = null)
        {

            return new PropertyGraph(GraphId, GraphInitializer) { Description = Description } as IPropertyGraph;

        }

        #endregion

        #region CreateGenericPropertyGraph(GraphId, Description = null, GraphInitializer = null)

        /// <summary>
        /// Create a new generic property graph.
        /// </summary>
        /// <param name="GraphId">The graph identification.</param>
        /// <param name="Description">The optional description of the graph.</param>
        /// <param name="GraphInitializer">The optional graph initializer.</param>
        public static IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object> CreateGenericPropertyGraph(
                          UInt64 GraphId,
                          String Description = null,
                          GraphInitializer<UInt64, Int64, String, String, Object,
                                           UInt64, Int64, String, String, Object,
                                           UInt64, Int64, String, String, Object,
                                           UInt64, Int64, String, String, Object> GraphInitializer = null)

        {

            return new GenericPropertyVertex<UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object>(GraphId,
                                                                                    Description,

                                                                                    // Vertices
                                                                                    GraphDBOntology.Id().Suffix,
                                                                                    GraphDBOntology.RevId().Suffix,
                                                                                    GraphDBOntology.Label().Suffix,
                                                                                    GraphDBOntology.Description().Suffix,
                                                                                    new IdGenerator_UInt64(),
                                                                                    GraphDBOntology.DefaultVertexLabel().Suffix,

                                                                                    // Edges
                                                                                    GraphDBOntology.Id().Suffix,
                                                                                    GraphDBOntology.RevId().Suffix,
                                                                                    GraphDBOntology.Label().Suffix,
                                                                                    GraphDBOntology.Description().Suffix,
                                                                                    new IdGenerator_UInt64(),
                                                                                    GraphDBOntology.DefaultEdgeLabel().Suffix,

                                                                                    // Multiedges
                                                                                    GraphDBOntology.Id().Suffix,
                                                                                    GraphDBOntology.RevId().Suffix,
                                                                                    GraphDBOntology.Label().Suffix,
                                                                                    GraphDBOntology.Description().Suffix,
                                                                                    new IdGenerator_UInt64(),
                                                                                    GraphDBOntology.DefaultMultiEdgeLabel().Suffix,

                                                                                    // Hyperedges
                                                                                    GraphDBOntology.Id().Suffix,
                                                                                    GraphDBOntology.RevId().Suffix,
                                                                                    GraphDBOntology.Label().Suffix,
                                                                                    GraphDBOntology.Description().Suffix,
                                                                                    new IdGenerator_UInt64(),
                                                                                    GraphDBOntology.DefaultHyperEdgeLabel().Suffix,

                                                                                    GraphInitializer)
                                                                                   
                                                                                    { Description = Description }
                                            
                                             as IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object>;

        }

        #endregion

        #region CreateGenericPropertyGraph2(GraphId, Description = null, GraphInitializer = null)

        /// <summary>
        /// Create a new generic property graph.
        /// </summary>
        /// <param name="GraphId">The graph identification.</param>
        /// <param name="Description">The optional description of the graph.</param>
        /// <param name="GraphInitializer">The optional graph initializer.</param>
        public static IGenericPropertyGraph<String, Int64, String, String, Object,
                                            String, Int64, String, String, Object,
                                            String, Int64, String, String, Object,
                                            String, Int64, String, String, Object> CreateGenericPropertyGraph2(
                          String GraphId,
                          String Description = null,
                          GraphInitializer<String, Int64, String, String, Object,
                                           String, Int64, String, String, Object,
                                           String, Int64, String, String, Object,
                                           String, Int64, String, String, Object> GraphInitializer = null)

        {

            return new GenericPropertyVertex<String, Int64, String, String, Object,
                                             String, Int64, String, String, Object,
                                             String, Int64, String, String, Object,
                                             String, Int64, String, String, Object>(GraphId,
                                                                                    Description,

                                                                                    // Vertices
                                                                                    GraphDBOntology.Id().Suffix,
                                                                                    GraphDBOntology.RevId().Suffix,
                                                                                    GraphDBOntology.Label().Suffix,
                                                                                    GraphDBOntology.Description().Suffix,
                                                                                    new IdGenerator_String(),
                                                                                    GraphDBOntology.DefaultVertexLabel().Suffix,

                                                                                    // Edges
                                                                                    GraphDBOntology.Id().Suffix,
                                                                                    GraphDBOntology.RevId().Suffix,
                                                                                    GraphDBOntology.Label().Suffix,
                                                                                    GraphDBOntology.Description().Suffix,
                                                                                    new IdGenerator_String(),
                                                                                    GraphDBOntology.DefaultEdgeLabel().Suffix,

                                                                                    // Multiedges
                                                                                    GraphDBOntology.Id().Suffix,
                                                                                    GraphDBOntology.RevId().Suffix,
                                                                                    GraphDBOntology.Label().Suffix,
                                                                                    GraphDBOntology.Description().Suffix,
                                                                                    new IdGenerator_String(),
                                                                                    GraphDBOntology.DefaultMultiEdgeLabel().Suffix,

                                                                                    // Hyperedges
                                                                                    GraphDBOntology.Id().Suffix,
                                                                                    GraphDBOntology.RevId().Suffix,
                                                                                    GraphDBOntology.Label().Suffix,
                                                                                    GraphDBOntology.Description().Suffix,
                                                                                    new IdGenerator_String(),
                                                                                    GraphDBOntology.DefaultHyperEdgeLabel().Suffix,

                                                                                    GraphInitializer) { Description = Description }

                                             as IGenericPropertyGraph<String, Int64, String, String, Object,
                                                                      String, Int64, String, String, Object,
                                                                      String, Int64, String, String, Object,
                                                                      String, Int64, String, String, Object>;

        }

        #endregion


        #region CreateCommonGenericGraph<TId, TRevId, TLabel, TKey, TValue>(GraphId, IdKey, RevIdKey, DescriptionKey, VertexIdCreatorDelegate, EdgeIdCreatorDelegate, MultiEdgeIdCreatorDelegate, HyperEdgeIdCreatorDelegate, RevIdCreatorDelegate, GraphInitializer = null)

        /// <summary>
        /// Create a new generic property graph with common datatypes for all graph elements (vertices, edges, multiedges and hyperedges).
        /// </summary>
        /// <typeparam name="TId">The type of all identificators.</typeparam>
        /// <typeparam name="TRevId">The type of all revision identificators.</typeparam>
        /// <typeparam name="TLabel">The type of all labels.</typeparam>
        /// <typeparam name="TKey">The type of all property keys.</typeparam>
        /// <typeparam name="TValue">The type of all property values.</typeparam>
        /// <param name="GraphId">The graph identification.</param></param>
        /// <param name="IdKey">The property key to access the Ids.</param>
        /// <param name="RevIdKey">The property key to access the RevisionIds.</param>
        /// <param name="LabelKey">The key to access the Label of this graph element.</param>
        /// <param name="DescriptionKey">The property key to access the descriptions.</param>
        /// <param name="VertexIdCreatorDelegate">A delegate to create a new vertex identification.</param>
        /// <param name="EdgeIdCreatorDelegate">A delegate to create a new edge identification.</param>
        /// <param name="MultiEdgeIdCreatorDelegate">A delegate to create a new multiedge identification.</param>
        /// <param name="HyperEdgeIdCreatorDelegate">A delegate to create a new hyperedge identification.</param>
        /// <param name="RevIdCreatorDelegate">A delegate to create a new revision identifications.</param>
        /// <param name="GraphInitializer">A delegate to initialize the new property graph.</param>
        public static IGenericPropertyGraph<TId, TRevId, TLabel, TKey, TValue,
                                            TId, TRevId, TLabel, TKey, TValue,
                                            TId, TRevId, TLabel, TKey, TValue,
                                            TId, TRevId, TLabel, TKey, TValue>
            
            CreateCommonGenericGraph<TId, TRevId, TLabel, TKey, TValue>(
                                     TId  GraphId,
                                     String Description,
                                     TKey IdKey,
                                     TKey RevIdKey,
                                     TKey LabelKey,
                                     TKey DescriptionKey,

                                     VertexIdCreatorDelegate   <TId, TRevId, TLabel, TKey, TValue,
                                                                TId, TRevId, TLabel, TKey, TValue,
                                                                TId, TRevId, TLabel, TKey, TValue,
                                                                TId, TRevId, TLabel, TKey, TValue> VertexIdCreatorDelegate,

                                     EdgeIdCreatorDelegate     <TId, TRevId, TLabel, TKey, TValue,
                                                                TId, TRevId, TLabel, TKey, TValue,
                                                                TId, TRevId, TLabel, TKey, TValue,
                                                                TId, TRevId, TLabel, TKey, TValue> EdgeIdCreatorDelegate,

                                     MultiEdgeIdCreatorDelegate<TId, TRevId, TLabel, TKey, TValue,
                                                                TId, TRevId, TLabel, TKey, TValue,
                                                                TId, TRevId, TLabel, TKey, TValue,
                                                                TId, TRevId, TLabel, TKey, TValue> MultiEdgeIdCreatorDelegate,

                                     HyperEdgeIdCreatorDelegate<TId, TRevId, TLabel, TKey, TValue,
                                                                TId, TRevId, TLabel, TKey, TValue,
                                                                TId, TRevId, TLabel, TKey, TValue,
                                                                TId, TRevId, TLabel, TKey, TValue> HyperEdgeIdCreatorDelegate,

                                     RevIdCreatorDelegate<TRevId>                                  RevIdCreatorDelegate,

                                     TLabel DefaultLabel,

                                     GraphInitializer          <TId, TRevId, TLabel, TKey, TValue,
                                                                TId, TRevId, TLabel, TKey, TValue,
                                                                TId, TRevId, TLabel, TKey, TValue,
                                                                TId, TRevId, TLabel, TKey, TValue> GraphInitializer = null)

            where TId     : IEquatable<TId>,    IComparable<TId>,    IComparable, TValue
            where TRevId  : IEquatable<TRevId>, IComparable<TRevId>, IComparable, TValue
            where TLabel  : IEquatable<TLabel>, IComparable<TLabel>, IComparable, TValue
            where TKey    : IEquatable<TKey>,   IComparable<TKey>,   IComparable

        {

            return new GenericPropertyVertex<TId, TRevId, TLabel, TKey, TValue,
                                             TId, TRevId, TLabel, TKey, TValue,
                                             TId, TRevId, TLabel, TKey, TValue,
                                             TId, TRevId, TLabel, TKey, TValue>(GraphId,
                                                                                Description,

                                                                                // Vertices
                                                                                IdKey,
                                                                                RevIdKey,
                                                                                LabelKey,
                                                                                DescriptionKey,
                                                                                VertexIdCreatorDelegate,
                                                                                DefaultLabel,

                                                                                // Edges
                                                                                IdKey,
                                                                                RevIdKey,
                                                                                LabelKey,
                                                                                DescriptionKey,
                                                                                EdgeIdCreatorDelegate,
                                                                                DefaultLabel,

                                                                                // Multiedges
                                                                                IdKey,
                                                                                RevIdKey,
                                                                                LabelKey,
                                                                                DescriptionKey,
                                                                                MultiEdgeIdCreatorDelegate,
                                                                                DefaultLabel,

                                                                                // Hyperedges
                                                                                IdKey,
                                                                                RevIdKey,
                                                                                LabelKey,
                                                                                DescriptionKey,
                                                                                HyperEdgeIdCreatorDelegate,
                                                                                DefaultLabel,
                                                                               
                                                                                GraphInitializer)

                                             as IGenericPropertyGraph<TId, TRevId, TLabel, TKey, TValue,
                                                                      TId, TRevId, TLabel, TKey, TValue,
                                                                      TId, TRevId, TLabel, TKey, TValue,
                                                                      TId, TRevId, TLabel, TKey, TValue>;

        }

        #endregion

        #region CreateCommonGenericGraph<TId, TRevId, TLabel, TKey, TValue>(GraphId, IdKey, RevIdKey, DescriptionKey, IdCreatorDelegate, RevIdCreatorDelegate, GraphInitializer = null)

        /// <summary>
        /// Create a new generic property graph with common datatypes for all graph elements (vertices, edges, multiedges and hyperedges).
        /// </summary>
        /// <typeparam name="TId">The type of all identificators.</typeparam>
        /// <typeparam name="TRevId">The type of all revision identificators.</typeparam>
        /// <typeparam name="TLabel">The type of all labels.</typeparam>
        /// <typeparam name="TKey">The type of all property keys.</typeparam>
        /// <typeparam name="TValue">The type of all property values.</typeparam>
        /// <param name="GraphId">The graph identification.</param></param>
        /// <param name="IdKey">The property key to access the Ids.</param>
        /// <param name="RevIdKey">The property key to access the RevisionIds.</param>
        /// <param name="DescriptionKey">The property key to access the descriptions.</param>
        /// <param name="IdCreatorDelegate">A delegate to create new graph element identifications.</param>
        /// <param name="RevIdCreatorDelegate">A delegate to create a new revision identifications.</param>
        /// <param name="GraphInitializer">A delegate to initialize the new property graph.</param>
        /// <returns></returns>
        public static IGenericPropertyGraph<TId, TRevId, TLabel, TKey, TValue,
                                            TId, TRevId, TLabel, TKey, TValue,
                                            TId, TRevId, TLabel, TKey, TValue,
                                            TId, TRevId, TLabel, TKey, TValue>
            
            CreateCommonGenericGraph<TId, TRevId, TLabel, TKey, TValue>(
                                     TId                          GraphId,
                                     String                       Description,
                                     TKey                         IdKey,
                                     TKey                         RevIdKey,
                                     TKey                         LabelKey,
                                     TKey                         DescriptionKey,                                     
                                     IdCreatorDelegate<TId>       IdCreatorDelegate,
                                     RevIdCreatorDelegate<TRevId> RevIdCreatorDelegate,
                                     TLabel                       DefaultLabel,
                                     GraphInitializer<TId, TRevId, TLabel, TKey, TValue,
                                                      TId, TRevId, TLabel, TKey, TValue,
                                                      TId, TRevId, TLabel, TKey, TValue,
                                                      TId, TRevId, TLabel, TKey, TValue> GraphInitializer = null)

            where TId     : IEquatable<TId>,    IComparable<TId>,    IComparable, TValue
            where TRevId  : IEquatable<TRevId>, IComparable<TRevId>, IComparable, TValue
            where TLabel  : IEquatable<TLabel>, IComparable<TLabel>, IComparable, TValue
            where TKey    : IEquatable<TKey>,   IComparable<TKey>,   IComparable

        {

            return CreateCommonGenericGraph<TId, TRevId, TLabel, TKey, TValue>(GraphId,
                                                                               Description,
                                                                               IdKey,
                                                                               RevIdKey,
                                                                               LabelKey,
                                                                               DescriptionKey,
                                                                               (graph) => IdCreatorDelegate(),
                                                                               (graph) => IdCreatorDelegate(),
                                                                               (graph) => IdCreatorDelegate(),
                                                                               (graph) => IdCreatorDelegate(),
                                                                               RevIdCreatorDelegate,
                                                                               DefaultLabel,
                                                                               GraphInitializer = null);

        }

        #endregion


        #region CreateLabeledPropertyGraph<TVertexLabel, TEdgeLabel, TMultiEdgeLabel, THyperEdgeLabel>(...)

        public static IGenericPropertyGraph<UInt64, Int64, TVertexLabel,    String, Object,
                                            UInt64, Int64, TEdgeLabel,      String, Object,
                                            UInt64, Int64, TMultiEdgeLabel, String, Object,
                                            UInt64, Int64, THyperEdgeLabel, String, Object>

            CreateLabeledPropertyGraph<TVertexLabel, TEdgeLabel, TMultiEdgeLabel, THyperEdgeLabel>(
                                       UInt64           GraphId,
                                       String           Description,

                                       TVertexLabel     DefaultVertexLabel,
                                       TEdgeLabel       DefaultEdgeLabel,
                                       TMultiEdgeLabel  DefaultMultiEdgeLabel,
                                       THyperEdgeLabel  DefaultHyperEdgeLabel,

                                       GraphInitializer<UInt64, Int64, TVertexLabel,    String, Object,
                                                        UInt64, Int64, TEdgeLabel,      String, Object,
                                                        UInt64, Int64, TMultiEdgeLabel, String, Object,
                                                        UInt64, Int64, THyperEdgeLabel, String, Object> GraphInitializer = null)

            where TVertexLabel     : IEquatable<TVertexLabel>,    IComparable<TVertexLabel>,    IComparable
            where TEdgeLabel       : IEquatable<TEdgeLabel>,      IComparable<TEdgeLabel>,      IComparable
            where TMultiEdgeLabel  : IEquatable<TMultiEdgeLabel>, IComparable<TMultiEdgeLabel>, IComparable
            where THyperEdgeLabel  : IEquatable<THyperEdgeLabel>, IComparable<THyperEdgeLabel>, IComparable

        {

            return new GenericPropertyVertex<UInt64, Int64, TVertexLabel,    String, Object,
                                             UInt64, Int64, TEdgeLabel,      String, Object,
                                             UInt64, Int64, TMultiEdgeLabel, String, Object,
                                             UInt64, Int64, THyperEdgeLabel, String, Object>(GraphId,
                                                                                             Description,

                                                                                             // Vertices
                                                                                             GraphDBOntology.Id().Suffix,
                                                                                             GraphDBOntology.RevId().Suffix,
                                                                                             GraphDBOntology.Label().Suffix,
                                                                                             GraphDBOntology.Description().Suffix,
                                                                                             new IdGenerator_UInt64(),
                                                                                             DefaultVertexLabel,

                                                                                             // Edges
                                                                                             GraphDBOntology.Id().Suffix,
                                                                                             GraphDBOntology.RevId().Suffix,
                                                                                             GraphDBOntology.Label().Suffix,
                                                                                             GraphDBOntology.Description().Suffix,
                                                                                             new IdGenerator_UInt64(),
                                                                                             DefaultEdgeLabel,

                                                                                             // Multiedges
                                                                                             GraphDBOntology.Id().Suffix,
                                                                                             GraphDBOntology.RevId().Suffix,
                                                                                             GraphDBOntology.Label().Suffix,
                                                                                             GraphDBOntology.Description().Suffix,
                                                                                             new IdGenerator_UInt64(),
                                                                                             DefaultMultiEdgeLabel,

                                                                                             // Hyperedges
                                                                                             GraphDBOntology.Id().Suffix,
                                                                                             GraphDBOntology.RevId().Suffix,
                                                                                             GraphDBOntology.Label().Suffix,
                                                                                             GraphDBOntology.Description().Suffix,
                                                                                             new IdGenerator_UInt64(),
                                                                                             DefaultHyperEdgeLabel,

                                                                                             GraphInitializer)

                                             as IGenericPropertyGraph<UInt64, Int64, TVertexLabel,    String, Object,
                                                                      UInt64, Int64, TEdgeLabel,      String, Object,
                                                                      UInt64, Int64, TMultiEdgeLabel, String, Object,
                                                                      UInt64, Int64, THyperEdgeLabel, String, Object>;

        }

        #endregion

        #region CreateLabeledPropertyGraph2<TVertexLabel, TEdgeLabel, TMultiEdgeLabel, THyperEdgeLabel>(...)

        public static IGenericPropertyGraph<String, Int64, TVertexLabel,    String, Object,
                                            String, Int64, TEdgeLabel,      String, Object,
                                            String, Int64, TMultiEdgeLabel, String, Object,
                                            String, Int64, THyperEdgeLabel, String, Object>

            CreateLabeledPropertyGraph2<TVertexLabel, TEdgeLabel, TMultiEdgeLabel, THyperEdgeLabel>(
                                        String           GraphId,
                                        String           Description,

                                        TVertexLabel     DefaultVertexLabel,
                                        TEdgeLabel       DefaultEdgeLabel,
                                        TMultiEdgeLabel  DefaultMultiEdgeLabel,
                                        THyperEdgeLabel  DefaultHyperEdgeLabel,

                                        GraphInitializer<String, Int64, TVertexLabel,    String, Object,
                                                         String, Int64, TEdgeLabel,      String, Object,
                                                         String, Int64, TMultiEdgeLabel, String, Object,
                                                         String, Int64, THyperEdgeLabel, String, Object> GraphInitializer = null)

            where TVertexLabel     : IEquatable<TVertexLabel>,    IComparable<TVertexLabel>,    IComparable
            where TEdgeLabel       : IEquatable<TEdgeLabel>,      IComparable<TEdgeLabel>,      IComparable
            where TMultiEdgeLabel  : IEquatable<TMultiEdgeLabel>, IComparable<TMultiEdgeLabel>, IComparable
            where THyperEdgeLabel  : IEquatable<THyperEdgeLabel>, IComparable<THyperEdgeLabel>, IComparable

        {

            return new GenericPropertyVertex<String, Int64, TVertexLabel,    String, Object,
                                             String, Int64, TEdgeLabel,      String, Object,
                                             String, Int64, TMultiEdgeLabel, String, Object,
                                             String, Int64, THyperEdgeLabel, String, Object>(GraphId,
                                                                                             Description,

                                                                                             // Vertices
                                                                                             GraphDBOntology.Id().Suffix,
                                                                                             GraphDBOntology.RevId().Suffix,
                                                                                             GraphDBOntology.Label().Suffix,
                                                                                             GraphDBOntology.Description().Suffix,
                                                                                             new IdGenerator_String(),
                                                                                             DefaultVertexLabel,

                                                                                             // Edges
                                                                                             GraphDBOntology.Id().Suffix,
                                                                                             GraphDBOntology.RevId().Suffix,
                                                                                             GraphDBOntology.Label().Suffix,
                                                                                             GraphDBOntology.Description().Suffix,
                                                                                             new IdGenerator_String(),
                                                                                             DefaultEdgeLabel,

                                                                                             // Multiedges
                                                                                             GraphDBOntology.Id().Suffix,
                                                                                             GraphDBOntology.RevId().Suffix,
                                                                                             GraphDBOntology.Label().Suffix,
                                                                                             GraphDBOntology.Description().Suffix,
                                                                                             new IdGenerator_String(),
                                                                                             DefaultMultiEdgeLabel,

                                                                                             // Hyperedges
                                                                                             GraphDBOntology.Id().Suffix,
                                                                                             GraphDBOntology.RevId().Suffix,
                                                                                             GraphDBOntology.Label().Suffix,
                                                                                             GraphDBOntology.Description().Suffix,
                                                                                             new IdGenerator_String(),
                                                                                             DefaultHyperEdgeLabel,

                                                                                             GraphInitializer)

                                             as IGenericPropertyGraph<String, Int64, TVertexLabel,    String, Object,
                                                                      String, Int64, TEdgeLabel,      String, Object,
                                                                      String, Int64, TMultiEdgeLabel, String, Object,
                                                                      String, Int64, THyperEdgeLabel, String, Object>;

        }

        #endregion


        #region CreateDistributedPropertyGraph(...)

        public static IGenericPropertyGraph<VertexId,    RevisionId, String, String, Object,
                                            EdgeId,      RevisionId, String, String, Object,
                                            MultiEdgeId, RevisionId, String, String, Object,
                                            HyperEdgeId, RevisionId, String, String, Object>

            CreateDistributedPropertyGraph(VertexId GraphId,
                                           String   Description,

                                           VertexIdCreatorDelegate   <VertexId,    RevisionId, String, String, Object,
                                                                      EdgeId,      RevisionId, String, String, Object,
                                                                      MultiEdgeId, RevisionId, String, String, Object,
                                                                      HyperEdgeId, RevisionId, String, String, Object> VertexIdCreatorDelegate,

                                           EdgeIdCreatorDelegate     <VertexId,    RevisionId, String, String, Object,
                                                                      EdgeId,      RevisionId, String, String, Object,
                                                                      MultiEdgeId, RevisionId, String, String, Object,
                                                                      HyperEdgeId, RevisionId, String, String, Object> EdgeIdCreatorDelegate,

                                           MultiEdgeIdCreatorDelegate<VertexId,    RevisionId, String, String, Object,
                                                                      EdgeId,      RevisionId, String, String, Object,
                                                                      MultiEdgeId, RevisionId, String, String, Object,
                                                                      HyperEdgeId, RevisionId, String, String, Object> MultiEdgeIdCreatorDelegate,

                                           HyperEdgeIdCreatorDelegate<VertexId,    RevisionId, String, String, Object,
                                                                      EdgeId,      RevisionId, String, String, Object,
                                                                      MultiEdgeId, RevisionId, String, String, Object,
                                                                      HyperEdgeId, RevisionId, String, String, Object> HyperEdgeIdCreatorDelegate,

                                           RevIdCreatorDelegate<Int64>                                                 RevIdCreatorDelegate,

                                           GraphInitializer          <VertexId,    RevisionId, String, String, Object,
                                                                      EdgeId,      RevisionId, String, String, Object,
                                                                      MultiEdgeId, RevisionId, String, String, Object,
                                                                      HyperEdgeId, RevisionId, String, String, Object> GraphInitializer = null)

        {

            return new GenericPropertyVertex<VertexId,    RevisionId, String, String, Object,
                                             EdgeId,      RevisionId, String, String, Object,
                                             MultiEdgeId, RevisionId, String, String, Object,
                                             HyperEdgeId, RevisionId, String, String, Object>(GraphId,
                                                                                              Description,

                                                                                              // Vertices
                                                                                              GraphDBOntology.Id().Suffix,
                                                                                              GraphDBOntology.RevId().Suffix,
                                                                                              GraphDBOntology.Label().Suffix,
                                                                                              GraphDBOntology.Description().Suffix,
                                                                                              VertexIdCreatorDelegate,
                                                                                              GraphDBOntology.DefaultVertexLabel().Suffix,

                                                                                              // Edhes
                                                                                              GraphDBOntology.Id().Suffix,
                                                                                              GraphDBOntology.RevId().Suffix,
                                                                                              GraphDBOntology.Label().Suffix,
                                                                                              GraphDBOntology.Description().Suffix,
                                                                                              EdgeIdCreatorDelegate,
                                                                                              GraphDBOntology.DefaultEdgeLabel().Suffix,

                                                                                              // MultiEdges
                                                                                              GraphDBOntology.Id().Suffix,
                                                                                              GraphDBOntology.RevId().Suffix,
                                                                                              GraphDBOntology.Label().Suffix,
                                                                                              GraphDBOntology.Description().Suffix,
                                                                                              MultiEdgeIdCreatorDelegate,
                                                                                              GraphDBOntology.DefaultMultiEdgeLabel().Suffix,

                                                                                              // HyperEdges
                                                                                              GraphDBOntology.Id().Suffix,
                                                                                              GraphDBOntology.RevId().Suffix,
                                                                                              GraphDBOntology.Label().Suffix,
                                                                                              GraphDBOntology.Description().Suffix,
                                                                                              HyperEdgeIdCreatorDelegate,
                                                                                              GraphDBOntology.DefaultHyperEdgeLabel().Suffix,
                                                                            
                                                                                              GraphInitializer)

                                             as IGenericPropertyGraph<VertexId,    RevisionId, String, String, Object,
                                                                      EdgeId,      RevisionId, String, String, Object,
                                                                      MultiEdgeId, RevisionId, String, String, Object,
                                                                      HyperEdgeId, RevisionId, String, String, Object>;

        }

        #endregion

        #region CreateSemanticPropertyGraph(...)

        /// <summary>
        /// Create a semantic property graph.
        /// </summary>
        public static IGenericPropertyGraph<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                            EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                            MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                            HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>

            CreateSemanticPropertyGraph(VertexId GraphId,
                                        String   Description,

                                        VertexIdCreatorDelegate   <VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                   EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                   MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                   HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object> VertexIdCreatorDelegate,

                                        EdgeIdCreatorDelegate     <VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                   EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                   MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                   HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object> EdgeIdCreatorDelegate,

                                        MultiEdgeIdCreatorDelegate<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                   EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                   MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                   HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object> MultiEdgeIdCreatorDelegate,

                                        HyperEdgeIdCreatorDelegate<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                   EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                   MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                   HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object> HyperEdgeIdCreatorDelegate,

                                        RevIdCreatorDelegate<Int64>                                                                     RevIdCreatorDelegate,

                                        GraphInitializer          <VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                   EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                   MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                   HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object> GraphInitializer = null)

        {

            return new GenericPropertyVertex<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                             EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                             MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                             HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>(GraphId,
                                                                                                                  Description,

                                                                                                                 // Vertices
                                                                                                                 GraphDBOntology.Id(),
                                                                                                                 GraphDBOntology.RevId(),
                                                                                                                 GraphDBOntology.Label(),
                                                                                                                 GraphDBOntology.Description(),
                                                                                                                 VertexIdCreatorDelegate,
                                                                                                                 GraphDBOntology.DefaultVertexLabel(),

                                                                                                                  // Edges
                                                                                                                  GraphDBOntology.Id(),
                                                                                                                  GraphDBOntology.RevId(),
                                                                                                                  GraphDBOntology.Label(),
                                                                                                                  GraphDBOntology.Description(),
                                                                                                                  EdgeIdCreatorDelegate,
                                                                                                                  GraphDBOntology.DefaultEdgeLabel(),

                                                                                                                  // Multiedges
                                                                                                                  GraphDBOntology.Id(),
                                                                                                                  GraphDBOntology.RevId(),
                                                                                                                  GraphDBOntology.Label(),
                                                                                                                  GraphDBOntology.Description(),
                                                                                                                  MultiEdgeIdCreatorDelegate,
                                                                                                                  GraphDBOntology.DefaultMultiEdgeLabel(),

                                                                                                                  // Hyperedges
                                                                                                                  GraphDBOntology.Id(),
                                                                                                                  GraphDBOntology.RevId(),
                                                                                                                  GraphDBOntology.Label(),
                                                                                                                  GraphDBOntology.Description(),
                                                                                                                  HyperEdgeIdCreatorDelegate,
                                                                                                                  GraphDBOntology.DefaultHyperEdgeLabel(),
                                                                            
                                                                                                                  GraphInitializer)

                                             as IGenericPropertyGraph<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                      EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                      MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                      HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>;

        }

        #endregion


        #region CreateSchemaGraph(GraphId, Description = null, GraphInitializer = null)

        /// <summary>
        /// Create a new schema graph.
        /// </summary>
        /// <param name="GraphId">The graph identification.</param>
        /// <param name="Description">The optional description of the graph.</param>
        /// <param name="GraphInitializer">The optional graph initializer.</param>
        public static IGenericPropertyGraph<String, Int64, String, String, Object,
                                            String, Int64, String, String, Object,
                                            String, Int64, String, String, Object,
                                            String, Int64, String, String, Object> CreateSchemaGraph(String GraphId,
                                                                                                     String Description = null,
                                                                                                     GraphInitializer<String, Int64, String, String, Object,
                                                                                                                      String, Int64, String, String, Object,
                                                                                                                      String, Int64, String, String, Object,
                                                                                                                      String, Int64, String, String, Object> GraphInitializer = null)
        {

            return new GenericPropertyVertex<String, Int64, String, String, Object,
                                             String, Int64, String, String, Object,
                                             String, Int64, String, String, Object,
                                             String, Int64, String, String, Object>(GraphId,
                                                                                    Description,

                                                                                    // Vertices
                                                                                    GraphDBOntology.Id().Suffix,
                                                                                    GraphDBOntology.RevId().Suffix,
                                                                                    GraphDBOntology.Label().Suffix,
                                                                                    GraphDBOntology.Description().Suffix,
                                                                                    new IdGenerator_String(),
                                                                                    GraphDBOntology.DefaultVertexLabel().Suffix,

                                                                                    // Edges
                                                                                    GraphDBOntology.Id().Suffix,
                                                                                    GraphDBOntology.RevId().Suffix,
                                                                                    GraphDBOntology.Label().Suffix,
                                                                                    GraphDBOntology.Description().Suffix,
                                                                                    new IdGenerator_String(),
                                                                                    GraphDBOntology.DefaultEdgeLabel().Suffix,

                                                                                    // Multiedges
                                                                                    GraphDBOntology.Id().Suffix,
                                                                                    GraphDBOntology.RevId().Suffix,
                                                                                    GraphDBOntology.Label().Suffix,
                                                                                    GraphDBOntology.Description().Suffix,
                                                                                    new IdGenerator_String(),
                                                                                    GraphDBOntology.DefaultMultiEdgeLabel().Suffix,

                                                                                    // Hyperedges
                                                                                    GraphDBOntology.Id().Suffix,
                                                                                    GraphDBOntology.RevId().Suffix,
                                                                                    GraphDBOntology.Label().Suffix,
                                                                                    GraphDBOntology.Description().Suffix,
                                                                                    new IdGenerator_String(),
                                                                                    GraphDBOntology.DefaultHyperEdgeLabel().Suffix,

                                                                                    GraphInitializer)
                                                                                   
                                                                                    { }

                                             as IGenericPropertyGraph<String, Int64, String, String, Object,
                                                                      String, Int64, String, String, Object,
                                                                      String, Int64, String, String, Object,
                                                                      String, Int64, String, String, Object>;

        }

        #endregion


        #region CreatePartitionGraph(GraphId, Description = null, WriteGraph = null, params ReadGraphs)

        /// <summary>
        /// A wrapper to combine multiple graphs to a single graph representation.
        /// The first graph may be used for writing, all others just for reading.
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
        /// <param name="GraphId">The unique identifiction of this graph.</param>
        /// <param name="Description">The description of this graph.</param>
        /// <param name="WriteGraph">An optional single graph for all write operations.</param>
        /// <param name="ReadGraphs">Multiple graphs for read-only operations.</param>
        public static GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
            CreatePartitionGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                                (TIdVertex GraphId,
                                 String Description = null,
                                 IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> WriteGraph = null,
             
                                 params IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                              TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                              TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                              TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] ReadGraphs)

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

            return new GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>(
                                                     GraphId,
                                                     Description,
                                                     WriteGraph,
                                                     ReadGraphs);

        }

        #endregion

        #region CreatePartitionGraph(this WriteGraph, GraphId, Description = null)

        /// <summary>
        /// A wrapper to combine multiple graphs to a single graph representation.
        /// The first graph may be used for writing, all others just for reading.
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
        /// <param name="WriteGraph">The graph for all write operations.</param>
        /// <param name="GraphId">The unique identifiction of this graph.</param>
        /// <param name="Description">The description of this graph.</param>
        /// <param name="ReadGraphs">Multiple graphs for read-only operations.</param>
        public static GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
            CreatePartitionGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                                 (this IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> WriteGraph,
                                  TIdVertex GraphId,
                                  String Description = null,
                                  params IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>[] ReadGraphs)

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

            #region Initial checks

            if (WriteGraph == null)
                throw new ArgumentException("The given WriteGraph must not be null!", "WriteGraph");

            #endregion

            return new GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>(
                                                     GraphId,
                                                     Description,
                                                     WriteGraph,
                                                     ReadGraphs);

        }

        #endregion

        #region CreatePartitionGraph(this WriteGraph, GraphId, Description = null)

        /// <summary>
        /// A wrapper to combine multiple graphs to a single graph representation.
        /// The first graph may be used for writing, all others just for reading.
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
        /// <param name="WriteGraph">The graph for all write operations.</param>
        /// <param name="GraphId">The unique identifiction of this graph.</param>
        /// <param name="Description">The description of this graph.</param>
        /// <param name="ReadGraphs">Multiple graphs for read-only operations.</param>
        public static GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
            
            CreatePartitionGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                                 (this IEnumerable<IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> ReadGraphs,
                                  TIdVertex GraphId,
                                  String Description = null,
                                  IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> WriteGraph = null)

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

            #region Initial checks

            if (ReadGraphs == null)
                throw new ArgumentException("The given ReadGraphs must not be null!", "ReadGraphs");

            #endregion

            return new GenericPartitionPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>(
                                                     GraphId,
                                                     Description,
                                                     WriteGraph,
                                                     ReadGraphs.ToArray());

        }

        #endregion

    }

    #endregion

}
