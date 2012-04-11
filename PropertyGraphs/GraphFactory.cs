/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <code@ahzf.de>
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
using System.Threading;

using de.ahzf.Illias.Commons;

using de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable;
using de.ahzf.Blueprints.PropertyGraphs;

#endregion

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// Simplified creation of property graphs.
    /// </summary>
    public static class GraphFactory
    {

        #region Helpers

        #region (private) NewIds

        private static Int64 _NewId;
        private static Int64 _NewVertexId;
        private static Int64 _NewEdgeId;
        private static Int64 _NewMultiEdgeId;
        private static Int64 _NewHyperEdgeId;

        /// <summary>
        /// Return a new Id.
        /// </summary>
        private static UInt64 NewId
        {
            get
            {
                var _NewLocalId = Interlocked.Increment(ref _NewId);
                return (UInt64) _NewLocalId;
            }
        }

        /// <summary>
        /// Return a new VertexId.
        /// </summary>
        private static UInt64 NewVertexId
        {
            get
            {
                var _NewLocalId = Interlocked.Increment(ref _NewVertexId);
                return (UInt64)_NewLocalId;
            }
        }

        /// <summary>
        /// Return a new NewEdgeId.
        /// </summary>
        private static UInt64 NewEdgeId
        {
            get
            {
                var _NewLocalId = Interlocked.Increment(ref _NewEdgeId);
                return (UInt64)_NewLocalId;
            }
        }

        /// <summary>
        /// Return a new NewMultiEdgeId.
        /// </summary>
        private static UInt64 NewMultiEdgeId
        {
            get
            {
                var _NewLocalId = Interlocked.Increment(ref _NewMultiEdgeId);
                return (UInt64)_NewLocalId;
            }
        }

        /// <summary>
        /// Return a new NewHyperEdgeId.
        /// </summary>
        private static UInt64 NewHyperEdgeId
        {
            get
            {
                var _NewLocalId = Interlocked.Increment(ref _NewHyperEdgeId);
                return (UInt64)_NewLocalId;
            }
        }

        #endregion

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
                                            UInt64, Int64, String, String, Object> CreateGenericPropertyGraph(UInt64 GraphId,
                                                                                                              String Description = null,
                                                                                                              GraphInitializer<UInt64, Int64, String, String, Object,
                                                                                                                               UInt64, Int64, String, String, Object,
                                                                                                                               UInt64, Int64, String, String, Object,
                                                                                                                               UInt64, Int64, String, String, Object> GraphInitializer = null)
        {

            return new GenericPropertyGraph<UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object>(GraphId,
                                                                                   GraphDBOntology.Id().Suffix,
                                                                                   GraphDBOntology.RevId().Suffix,
                                                                                   GraphDBOntology.Description().Suffix,
                                                                                   (graph) => GraphFactory.NewVertexId,
                                                                                   GraphDBOntology.DefaultVertexLabel().Suffix,

                                                                                   GraphDBOntology.Id().Suffix,
                                                                                   GraphDBOntology.RevId().Suffix,
                                                                                   GraphDBOntology.Description().Suffix,
                                                                                   (graph) => GraphFactory.NewEdgeId,
                                                                                   GraphDBOntology.DefaultEdgeLabel().Suffix,

                                                                                   GraphDBOntology.Id().Suffix,
                                                                                   GraphDBOntology.RevId().Suffix,
                                                                                   GraphDBOntology.Description().Suffix,
                                                                                   (graph) => GraphFactory.NewMultiEdgeId,
                                                                                   GraphDBOntology.DefaultMultiEdgeLabel().Suffix,

                                                                                   GraphDBOntology.Id().Suffix,
                                                                                   GraphDBOntology.RevId().Suffix,
                                                                                   GraphDBOntology.Description().Suffix,
                                                                                   (graph) => GraphFactory.NewHyperEdgeId,
                                                                                   GraphDBOntology.DefaultHyperEdgeLabel().Suffix,

                                                                                   GraphInitializer)
                                                                                   
                                                                                   { Description = Description }
                                            
                                            as IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                                     UInt64, Int64, String, String, Object,
                                                                     UInt64, Int64, String, String, Object,
                                                                     UInt64, Int64, String, String, Object>;

        }

        #endregion

        #region CreateGenericPropertyGraph(GraphId, Description = null, GraphInitializer = null)

        /// <summary>
        /// Create a new generic property graph.
        /// </summary>
        /// <param name="GraphId">The graph identification.</param>
        /// <param name="Description">The optional description of the graph.</param>
        /// <param name="GraphInitializer">The optional graph initializer.</param>
        public static IGenericPropertyGraph<String, Int64, String, String, Object,
                                            String, Int64, String, String, Object,
                                            String, Int64, String, String, Object,
                                            String, Int64, String, String, Object> CreateGenericPropertyGraph2(String GraphId,
                                                                                                               String Description = null,
                                                                                                               GraphInitializer<String, Int64, String, String, Object,
                                                                                                                                String, Int64, String, String, Object,
                                                                                                                                String, Int64, String, String, Object,
                                                                                                                                String, Int64, String, String, Object> GraphInitializer = null)
        {

            return new GenericPropertyGraph<String, Int64, String, String, Object,
                                            String, Int64, String, String, Object,
                                            String, Int64, String, String, Object,
                                            String, Int64, String, String, Object>(GraphId,
                                                                                   GraphDBOntology.Id().Suffix,
                                                                                   GraphDBOntology.RevId().Suffix,
                                                                                   GraphDBOntology.Description().Suffix,
                                                                                   (graph) => GraphFactory.NewVertexId.ToString(),
                                                                                   GraphDBOntology.DefaultVertexLabel().Suffix,

                                                                                   GraphDBOntology.Id().Suffix,
                                                                                   GraphDBOntology.RevId().Suffix,
                                                                                   GraphDBOntology.Description().Suffix,
                                                                                   (graph) => GraphFactory.NewEdgeId.ToString(),
                                                                                   GraphDBOntology.DefaultEdgeLabel().Suffix,

                                                                                   GraphDBOntology.Id().Suffix,
                                                                                   GraphDBOntology.RevId().Suffix,
                                                                                   GraphDBOntology.Description().Suffix,
                                                                                   (graph) => GraphFactory.NewMultiEdgeId.ToString(),
                                                                                   GraphDBOntology.DefaultMultiEdgeLabel().Suffix,

                                                                                   GraphDBOntology.Id().Suffix,
                                                                                   GraphDBOntology.RevId().Suffix,
                                                                                   GraphDBOntology.Description().Suffix,
                                                                                   (graph) => GraphFactory.NewHyperEdgeId.ToString(),
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
                                     TKey IdKey,
                                     TKey RevIdKey,
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
            where TKey    : IEquatable<TKey>,   IComparable<TKey>,   IComparable
            where TLabel  : IEquatable<TLabel>, IComparable<TLabel>, IComparable

        {

            return new GenericPropertyGraph<TId, TRevId, TLabel, TKey, TValue,
                                            TId, TRevId, TLabel, TKey, TValue,
                                            TId, TRevId, TLabel, TKey, TValue,
                                            TId, TRevId, TLabel, TKey, TValue>(GraphId,
                                                                               IdKey,
                                                                               RevIdKey,
                                                                               DescriptionKey,
                                                                               VertexIdCreatorDelegate,
                                                                               DefaultLabel,

                                                                               IdKey,
                                                                               RevIdKey,
                                                                               DescriptionKey,
                                                                               EdgeIdCreatorDelegate,
                                                                               DefaultLabel,

                                                                               IdKey,
                                                                               RevIdKey,
                                                                               DescriptionKey,
                                                                               MultiEdgeIdCreatorDelegate,
                                                                               DefaultLabel,

                                                                               IdKey,
                                                                               RevIdKey,
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
                                     TKey                         IdKey,
                                     TKey                         RevIdKey,
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
            where TKey    : IEquatable<TKey>,   IComparable<TKey>,   IComparable
            where TLabel  : IEquatable<TLabel>, IComparable<TLabel>, IComparable

        {

            return CreateCommonGenericGraph<TId, TRevId, TLabel, TKey, TValue>(GraphId,
                                                                               IdKey,
                                                                               RevIdKey,
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
                                     UInt64 GraphId,

                                     VertexIdCreatorDelegate   <UInt64, Int64, TVertexLabel,    String, Object,
                                                                UInt64, Int64, TEdgeLabel,      String, Object,
                                                                UInt64, Int64, TMultiEdgeLabel, String, Object,
                                                                UInt64, Int64, THyperEdgeLabel, String, Object> VertexIdCreatorDelegate,
                                     TVertexLabel DefaultVertexLabel,


                                     EdgeIdCreatorDelegate     <UInt64, Int64, TVertexLabel,    String, Object,
                                                                UInt64, Int64, TEdgeLabel,      String, Object,
                                                                UInt64, Int64, TMultiEdgeLabel, String, Object,
                                                                UInt64, Int64, THyperEdgeLabel, String, Object> EdgeIdCreatorDelegate,
                                     TEdgeLabel DefaultEdgeLabel,


                                     MultiEdgeIdCreatorDelegate<UInt64, Int64, TVertexLabel,    String, Object,
                                                                UInt64, Int64, TEdgeLabel,      String, Object,
                                                                UInt64, Int64, TMultiEdgeLabel, String, Object,
                                                                UInt64, Int64, THyperEdgeLabel, String, Object> MultiEdgeIdCreatorDelegate,
                                     TMultiEdgeLabel DefaultMultiEdgeLabel,


                                     HyperEdgeIdCreatorDelegate<UInt64, Int64, TVertexLabel,    String, Object,
                                                                UInt64, Int64, TEdgeLabel,      String, Object,
                                                                UInt64, Int64, TMultiEdgeLabel, String, Object,
                                                                UInt64, Int64, THyperEdgeLabel, String, Object> HyperEdgeIdCreatorDelegate,
                                     THyperEdgeLabel DefaultHyperEdgeLabel,


                                     RevIdCreatorDelegate<Int64>                                                RevIdCreatorDelegate,

                                     GraphInitializer          <UInt64, Int64, TVertexLabel,    String, Object,
                                                                UInt64, Int64, TEdgeLabel,      String, Object,
                                                                UInt64, Int64, TMultiEdgeLabel, String, Object,
                                                                UInt64, Int64, THyperEdgeLabel, String, Object> GraphInitializer = null)

            where TVertexLabel     : IEquatable<TVertexLabel>,    IComparable<TVertexLabel>,    IComparable
            where TEdgeLabel       : IEquatable<TEdgeLabel>,      IComparable<TEdgeLabel>,      IComparable
            where TMultiEdgeLabel  : IEquatable<TMultiEdgeLabel>, IComparable<TMultiEdgeLabel>, IComparable
            where THyperEdgeLabel  : IEquatable<THyperEdgeLabel>, IComparable<THyperEdgeLabel>, IComparable

        {

            return new GenericPropertyGraph<UInt64, Int64, TVertexLabel,    String, Object,
                                            UInt64, Int64, TEdgeLabel,      String, Object,
                                            UInt64, Int64, TMultiEdgeLabel, String, Object,
                                            UInt64, Int64, THyperEdgeLabel, String, Object>(GraphId,
                                                                                            GraphDBOntology.Id().Suffix,
                                                                                            GraphDBOntology.RevId().Suffix,
                                                                                            GraphDBOntology.Description().Suffix,
                                                                                            VertexIdCreatorDelegate,
                                                                                            DefaultVertexLabel,

                                                                                            GraphDBOntology.Id().Suffix,
                                                                                            GraphDBOntology.RevId().Suffix,
                                                                                            GraphDBOntology.Description().Suffix,
                                                                                            EdgeIdCreatorDelegate,
                                                                                            DefaultEdgeLabel,

                                                                                            GraphDBOntology.Id().Suffix,
                                                                                            GraphDBOntology.RevId().Suffix,
                                                                                            GraphDBOntology.Description().Suffix,
                                                                                            MultiEdgeIdCreatorDelegate,
                                                                                            DefaultMultiEdgeLabel,

                                                                                            GraphDBOntology.Id().Suffix,
                                                                                            GraphDBOntology.RevId().Suffix,
                                                                                            GraphDBOntology.Description().Suffix,
                                                                                            HyperEdgeIdCreatorDelegate,
                                                                                            DefaultHyperEdgeLabel,
                                                                            
                                                                                            GraphInitializer)

                                            as IGenericPropertyGraph<UInt64, Int64, TVertexLabel,    String, Object,
                                                                     UInt64, Int64, TEdgeLabel,      String, Object,
                                                                     UInt64, Int64, TMultiEdgeLabel, String, Object,
                                                                     UInt64, Int64, THyperEdgeLabel, String, Object>;

        }

        #endregion

        #region CreateDistributedPropertyGraph(...)

        public static IGenericPropertyGraph<VertexId,    RevisionId, String, String, Object,
                                            EdgeId,      RevisionId, String, String, Object,
                                            MultiEdgeId, RevisionId, String, String, Object,
                                            HyperEdgeId, RevisionId, String, String, Object>

            CreateDistributedPropertyGraph(
                                     VertexId GraphId,
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

            return new GenericPropertyGraph<VertexId,    RevisionId, String, String, Object,
                                            EdgeId,      RevisionId, String, String, Object,
                                            MultiEdgeId, RevisionId, String, String, Object,
                                            HyperEdgeId, RevisionId, String, String, Object>(GraphId,
                                                                                            GraphDBOntology.Id().Suffix,
                                                                                            GraphDBOntology.RevId().Suffix,
                                                                                            GraphDBOntology.Description().Suffix,
                                                                                            VertexIdCreatorDelegate,
                                                                                            GraphDBOntology.DefaultVertexLabel().Suffix,

                                                                                            GraphDBOntology.Id().Suffix,
                                                                                            GraphDBOntology.RevId().Suffix,
                                                                                            GraphDBOntology.Description().Suffix,
                                                                                            EdgeIdCreatorDelegate,
                                                                                            GraphDBOntology.DefaultEdgeLabel().Suffix,

                                                                                            GraphDBOntology.Id().Suffix,
                                                                                            GraphDBOntology.RevId().Suffix,
                                                                                            GraphDBOntology.Description().Suffix,
                                                                                            MultiEdgeIdCreatorDelegate,
                                                                                            GraphDBOntology.DefaultMultiEdgeLabel().Suffix,

                                                                                            GraphDBOntology.Id().Suffix,
                                                                                            GraphDBOntology.RevId().Suffix,
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

        public static IGenericPropertyGraph<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                            EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                            MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                            HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>

            CreateSemanticPropertyGraph(
                                     VertexId GraphId,
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

            return new GenericPropertyGraph<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                            EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                            MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                            HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>(GraphId,
                                                                                            GraphDBOntology.Id(),
                                                                                            GraphDBOntology.RevId(),
                                                                                            GraphDBOntology.Description(),
                                                                                            VertexIdCreatorDelegate,
                                                                                            GraphDBOntology.DefaultVertexLabel(),

                                                                                            GraphDBOntology.Id(),
                                                                                            GraphDBOntology.RevId(),
                                                                                            GraphDBOntology.Description(),
                                                                                            EdgeIdCreatorDelegate,
                                                                                            GraphDBOntology.DefaultEdgeLabel(),

                                                                                            GraphDBOntology.Id(),
                                                                                            GraphDBOntology.RevId(),
                                                                                            GraphDBOntology.Description(),
                                                                                            MultiEdgeIdCreatorDelegate,
                                                                                            GraphDBOntology.DefaultMultiEdgeLabel(),

                                                                                            GraphDBOntology.Id(),
                                                                                            GraphDBOntology.RevId(),
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

    }

}
