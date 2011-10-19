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
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;

using de.ahzf.Blueprints.Indices;
using de.ahzf.Blueprints.PropertyGraphs.Indices;

#endregion

namespace de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable
{

    /// <summary>
    /// A class-based in-memory implementation of a generic property graph.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TVertexLabel">The type of the vertex type.</typeparam>
    /// <typeparam name="TKeyVertex">The type of the vertex property keys.</typeparam>
    /// <typeparam name="TValueVertex">The type of the vertex property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionVertex">A data structure to store the properties of the vertices.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
    /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionEdge">A data structure to store the properties of the edges.</typeparam>
    /// 
    /// <typeparam name="TIdMultiEdge">The type of the multiedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdMultiEdge">The type of the multiedge revision identifiers.</typeparam>
    /// <typeparam name="TMultiEdgeLabel">The type of the multiedge label.</typeparam>
    /// <typeparam name="TKeyMultiEdge">The type of the multiedge property keys.</typeparam>
    /// <typeparam name="TValueMultiEdge">The type of the multiedge property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionMultiEdge">A data structure to store the properties of the multiedges.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
    /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionHyperEdge">A data structure to store the properties of the hyperedges.</typeparam>
    public abstract class AGenericPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                     : PropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>


        where TIdVertex                      : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge                        : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdMultiEdge                   : IEquatable<TIdMultiEdge>,         IComparable<TIdMultiEdge>,         IComparable, TValueMultiEdge
        where TIdHyperEdge                   : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TRevisionIdVertex              : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge                : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdMultiEdge           : IEquatable<TRevisionIdMultiEdge>, IComparable<TRevisionIdMultiEdge>, IComparable, TValueMultiEdge
        where TRevisionIdHyperEdge           : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

        where TVertexLabel                   : IEquatable<TVertexLabel>,         IComparable<TVertexLabel>,         IComparable
        where TEdgeLabel                     : IEquatable<TEdgeLabel>,           IComparable<TEdgeLabel>,           IComparable
        where TMultiEdgeLabel                : IEquatable<TMultiEdgeLabel>,      IComparable<TMultiEdgeLabel>,      IComparable
        where THyperEdgeLabel                : IEquatable<THyperEdgeLabel>,      IComparable<THyperEdgeLabel>,      IComparable

        where TKeyVertex                     : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                       : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyMultiEdge                  : IEquatable<TKeyMultiEdge>,        IComparable<TKeyMultiEdge>,        IComparable
        where TKeyHyperEdge                  : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

    {

        #region Data

        private readonly IDictionary<String, IPropertyElementIndex<IPropertyVertex   <TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> _ManualVerticesIndices;
        private readonly IDictionary<String, IPropertyElementIndex<IPropertyVertex   <TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> _AutomaticVerticesIndices;

        private readonly IDictionary<String, IPropertyElementIndex<IPropertyEdge     <TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> _ManualEdgesIndices;
        private readonly IDictionary<String, IPropertyElementIndex<IPropertyEdge     <TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> _AutomaticEdgesIndices;

        private readonly IDictionary<String, IPropertyElementIndex<IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> _ManualHyperEdgesIndices;
        private readonly IDictionary<String, IPropertyElementIndex<IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                      TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> _AutomaticHyperEdgesIndices;

        #endregion

        #region Constructor(s)

        #region GenericPropertyGraph(VertexCreatorDelegate, EdgeCreatorDelegate, HyperEdgeCreatorDelegate, VerticesCollectionInitializer, EdgesCollectionInitializer, HyperEdgesCollectionInitializer)

        /// <summary>
        /// Created a new class-based in-memory implementation of a generic property graph.
        /// </summary>
        /// <param name="GraphId">The identification of this graph.</param>
        /// <param name="IdKey">The key to access the Id of this graph.</param>
        /// <param name="RevisonIdKey">The key to access the Id of this graph.</param>
        /// <param name="DataInitializer"></param>
        /// <param name="VertexIdCreatorDelegate">A delegate for creating a new VertexId (if no one was provided by the user).</param>
        /// <param name="VertexCreatorDelegate">A delegate for creating a new vertex.</param>
        /// <param name="EdgeIdCreatorDelegate">A delegate for creating a new EdgeId (if no one was provided by the user).</param>
        /// <param name="EdgeCreatorDelegate">A delegate for creating a new edge.</param>
        /// <param name="MultiEdgeIdCreatorDelegate">A delegate for creating a new MultiEdgeId (if no one was provided by the user).</param>
        /// <param name="MultiEdgeCreatorDelegate">A delegate for creating a new multiedge.</param>
        /// <param name="HyperEdgeIdCreatorDelegate">A delegate for creating a new HyperEdgeId (if no one was provided by the user).</param>
        /// <param name="HyperEdgeCreatorDelegate">A delegate for creating a new hyperedge.</param>
        /// <param name="VerticesCollectionInitializer">A delegate for initializing a new vertex with custom data.</param>
        /// <param name="EdgesCollectionInitializer">A delegate for initializing a new edge with custom data.</param>
        /// <param name="HyperEdgesCollectionInitializer">A delegate for initializing a new hyperedge with custom data.</param>
        /// <param name="GraphInitializer">A delegate to initialize the newly created graph.</param>
        public AGenericPropertyGraph(TIdVertex                          GraphId,
                                     TKeyVertex                         IdKey,
                                     TKeyVertex                         RevisonIdKey,
                                     Func<IDictionary<TKeyVertex, TValueVertex>> DataInitializer,

                                    // Create a new vertex
                                    VertexIdCreatorDelegate   <TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexIdCreatorDelegate,

                                    VertexCreatorDelegate     <TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> VertexCreatorDelegate,

                                    Func<IGroupedCollection<TVertexLabel, TIdVertex, IPropertyVertex<TIdVertex, TRevisionIdVertex, TVertexLabel, TKeyVertex, TValueVertex,
                                                                                                                    TIdEdge, TRevisionIdEdge, TEdgeLabel, TKeyEdge, TValueEdge,
                                                                                                                    TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> VerticesCollectionInitializer,


                                    // Create a new edge
                                    EdgeIdCreatorDelegate     <TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeIdCreatorDelegate,

                                    EdgeCreatorDelegate       <TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeCreatorDelegate,

                                    Func<IGroupedCollection<TEdgeLabel, TIdEdge, IPropertyEdge<TIdVertex, TRevisionIdVertex, TVertexLabel, TKeyVertex, TValueVertex,
                                                                                                                    TIdEdge, TRevisionIdEdge, TEdgeLabel, TKeyEdge, TValueEdge,
                                                                                                                    TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> EdgesCollectionInitializer,


                                    // Create a new multiedge
                                    MultiEdgeIdCreatorDelegate<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeIdCreatorDelegate,

                                    MultiEdgeCreatorDelegate  <TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> MultiEdgeCreatorDelegate,

                                    Func<IGroupedCollection<TMultiEdgeLabel, TIdMultiEdge, IPropertyMultiEdge<TIdVertex, TRevisionIdVertex, TVertexLabel, TKeyVertex, TValueVertex,
                                                                                                                    TIdEdge, TRevisionIdEdge, TEdgeLabel, TKeyEdge, TValueEdge,
                                                                                                                    TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> MultiEdgesCollectionInitializer,


                                    // Create a new hyperedge
                                    HyperEdgeIdCreatorDelegate<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeIdCreatorDelegate,

                                    HyperEdgeCreatorDelegate  <TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeCreatorDelegate,

                                    Func<IGroupedCollection<THyperEdgeLabel, TIdHyperEdge, IPropertyHyperEdge<TIdVertex, TRevisionIdVertex, TVertexLabel, TKeyVertex, TValueVertex,
                                                                                                                    TIdEdge, TRevisionIdEdge, TEdgeLabel, TKeyEdge, TValueEdge,
                                                                                                                    TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                                    TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> HyperEdgesCollectionInitializer,


                                    // Graph initializer
                                    GraphInitializer<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                     TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> GraphInitializer = null)


            : base(GraphId, IdKey, RevisonIdKey, DataInitializer,
                   VertexIdCreatorDelegate,    VertexCreatorDelegate,    VerticesCollectionInitializer,
                   EdgeIdCreatorDelegate,      EdgeCreatorDelegate,      EdgesCollectionInitializer,
                   MultiEdgeIdCreatorDelegate, MultiEdgeCreatorDelegate, MultiEdgesCollectionInitializer,
                   HyperEdgeIdCreatorDelegate, HyperEdgeCreatorDelegate, HyperEdgesCollectionInitializer)

        {

            _ManualVerticesIndices      = new Dictionary<String, IPropertyElementIndex<IPropertyVertex   <TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                          TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>();
            _AutomaticVerticesIndices   = new Dictionary<String, IPropertyElementIndex<IPropertyVertex   <TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                          TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>();

            _ManualEdgesIndices         = new Dictionary<String, IPropertyElementIndex<IPropertyEdge     <TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                          TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>();
            _AutomaticEdgesIndices      = new Dictionary<String, IPropertyElementIndex<IPropertyEdge     <TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                          TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>();

            _ManualHyperEdgesIndices    = new Dictionary<String, IPropertyElementIndex<IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                          TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>();
            _AutomaticHyperEdgesIndices = new Dictionary<String, IPropertyElementIndex<IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                                          TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>();

        }

        #endregion

        #endregion


        //#region GraphIndexing

        //#region (private) ActiveIndex(IndexClassName)

        //private IIndex<TIndexKey, TIndexValue>

        //        ActiveIndex<TIndexKey, TIndexValue>(String IndexClassName)

        //    where TIndexKey   : IEquatable<TIndexKey>,   IComparable<TIndexKey>,   IComparable
        //    where TIndexValue : IEquatable<TIndexValue>, IComparable<TIndexValue>, IComparable

        //{

        //    Type IndexType = null;
        //    //if (IndexDatastructure == "DictionaryIndex")
        //    //    IndexType = typeof(DictionaryIndex<,>);
        //    //var IndexType = IndexDatastructure.GetType().GetGenericTypeDefinition();

        //    var myAssembly = this.GetType().Assembly;

        //    if (IndexClassName.IndexOf('.') < 0)
        //        IndexType = myAssembly.GetType(this.GetType().Namespace + "." + IndexClassName + "`2");
        //    else
        //        IndexType = myAssembly.GetType(IndexClassName + "`2");


        //    // Check if the index type implements the ILookup interface
        //    if (!IndexType.FindInterfaces((type, o) => { if (type == typeof(IIndex)) return true; else return false; }, null).Any())
        //        throw new ArgumentException("The given class does not implement the ILookup interface!");

        //    var typeArgs = new Type[] { typeof(TIndexKey), typeof(TIndexValue) };
            
        //    Type constructed = IndexType.MakeGenericType(typeArgs);

        //    var Index = Activator.CreateInstance(constructed) as IIndex<TIndexKey, TIndexValue>;

        //    if (Index == null)
        //        throw new ArgumentException("The given class does not implement the ILookup<TIndexKey, TIndexValue> interface!");

        //    return Index;
        
        //}

        //#endregion


        //#region CreateVerticesIndex<TIndexKey>(Name, IndexClassName, Transformation, Selector = null, AutomaticIndex = false)

        ///// <summary>
        ///// Generate an index for vertex lookups.
        ///// </summary>
        ///// <typeparam name="TIndexKey">The type of the index keys.</typeparam>
        ///// <param name="Name">A human-friendly name for the index.</param>
        ///// <param name="IndexClassName">The class name of a datastructure maintaining the index.</param>
        ///// <param name="Transformation">A delegate for transforming a vertex into an index key.</param>
        ///// <param name="Selector">A delegate for deciding if a vertex should be indexed or not.</param>
        ///// <param name="IsAutomaticIndex">Should this index be maintained by the database or by the user?</param>
        ///// <returns>A new index data structure.</returns>
        //public IPropertyElementIndex<TIndexKey, IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                        TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
        //       CreateVerticesIndex<TIndexKey>(String Name,                                              
        //                                      String IndexClassName,
        //                                      IndexTransformation<TIndexKey,
        //                                                          IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                                          TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Transformation,
        //                                      IndexSelector      <TIndexKey,
        //                                                          IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                                          TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Selector = null,
        //                                      Boolean IsAutomaticIndex = false)

        //    where TIndexKey : IEquatable<TIndexKey>, IComparable<TIndexKey>, IComparable

        //{

        //    var IndexDatastructure = ActiveIndex<TIndexKey, IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                                    TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>(IndexClassName);

        //    var _NewIndex = new PropertyVertexIndex<TIndexKey,
        //                                            TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,  
        //                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,    
        //                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        //                                           (Name, IndexDatastructure, Transformation, Selector, IsAutomaticIndex);

        //    if (IsAutomaticIndex)
        //        _AutomaticVerticesIndices.Add(_NewIndex.Name, _NewIndex);
        //    else
        //        _ManualVerticesIndices.Add(_NewIndex.Name, _NewIndex);

        //    return _NewIndex;

        //}

        //#endregion

        //#region CreateEdgesIndex<TIndexKey>(Name, IndexClassName, Transformation, Selector = null, IsAutomaticIndex = false)

        ///// <summary>
        ///// Generate an index for edge lookups.
        ///// </summary>
        ///// <typeparam name="TIndexKey">The type of the index keys.</typeparam>
        ///// <param name="Name">A human-friendly name for the index.</param>
        ///// <param name="IndexClassName">The class name of a datastructure maintaining the index.</param>
        ///// <param name="Transformation">A delegate for transforming a vertex into an index key.</param>
        ///// <param name="Selector">A delegate for deciding if a vertex should be indexed or not.</param>
        ///// <param name="IsAutomaticIndex">Should this index be maintained by the database or by the user?</param>
        ///// <returns>A new index data structure.</returns>
        //public IPropertyElementIndex<TIndexKey, IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                      TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
        //       CreateEdgesIndex<TIndexKey>(String Name,                                              
        //                                   String IndexClassName,
        //                                   IndexTransformation<TIndexKey,
        //                                                       IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                                     TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Transformation,
        //                                   IndexSelector      <TIndexKey,
        //                                                       IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                                     TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Selector = null,
        //                                   Boolean IsAutomaticIndex = false)

        //    where TIndexKey : IEquatable<TIndexKey>, IComparable<TIndexKey>, IComparable

        //{

        //    var IndexDatastructure = ActiveIndex<TIndexKey, IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                                  TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>(IndexClassName);

        //    var _NewIndex = new PropertyEdgeIndex<TIndexKey,
        //                                          TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,  
        //                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,    
        //                                          TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        //                                         (Name, IndexDatastructure, Transformation, Selector, IsAutomaticIndex);

        //    if (IsAutomaticIndex)
        //        _AutomaticEdgesIndices.Add(_NewIndex.Name, _NewIndex);
        //    else
        //        _ManualEdgesIndices.Add(_NewIndex.Name, _NewIndex);

        //    return _NewIndex;

        //}

        //#endregion

        //#region CreateHyperEdgesIndex<TIndexKey>(Name, IndexClassName, Transformation, Selector = null, IsAutomaticIndex = false)

        ///// <summary>
        ///// Generate an index for hyperedge lookups.
        ///// </summary>
        ///// <typeparam name="TIndexKey">The type of the index keys.</typeparam>
        ///// <param name="Name">A human-friendly name for the index.</param>
        ///// <param name="IndexClassName">The class name of a datastructure maintaining the index.</param>
        ///// <param name="Transformation">A delegate for transforming a vertex into an index key.</param>
        ///// <param name="Selector">A delegate for deciding if a vertex should be indexed or not.</param>
        ///// <param name="IsAutomaticIndex">Should this index be maintained by the database or by the user?</param>
        ///// <returns>A new index data structure.</returns>
        //public IPropertyElementIndex<TIndexKey, IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                           TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>







        //       CreateHyperEdgesIndex<TIndexKey>(String Name,
        //                                        String IndexClassName,
        //                                        IndexTransformation   <TIndexKey,
        //                                                               IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                                                  TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Transformation,
        //                                        IndexSelector         <TIndexKey,
        //                                                               IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                                                  TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Selector = null,
        //                                        Boolean IsAutomaticIndex = false)

        //    where TIndexKey : IEquatable<TIndexKey>, IComparable<TIndexKey>, IComparable

        //{

        //    var IndexDatastructure = ActiveIndex<TIndexKey, IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                                       TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>(IndexClassName);

        //    var _NewIndex = new PropertyHyperEdgeIndex<TIndexKey,
        //                                               TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,  
        //                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,    
        //                                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
        //                                              (Name, IndexDatastructure, Transformation, Selector, IsAutomaticIndex);

        //    if (IsAutomaticIndex)
        //        _AutomaticHyperEdgesIndices.Add(_NewIndex.Name, _NewIndex);
        //    else
        //        _ManualHyperEdgesIndices.Add(_NewIndex.Name, _NewIndex);

        //    return _NewIndex;

        //}

        //#endregion


        //#region VerticesIndices(IndexFilter = null)

        ///// <summary>
        ///// Get all vertices indices maintained by the graph.
        ///// </summary>
        ///// <param name="IndexFilter">An optional index filter.</param>
        ///// <returns>The indices associated with the graph.</returns>
        //public IEnumerable<IPropertyElementIndex<IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                         TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                         TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>

        //       VerticesIndices(IndexFilter<IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                   TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> IndexFilter = null)

        //{
            
        //    if (IndexFilter == null)
        //    {
        //        foreach (var _Index in _AutomaticVerticesIndices.Values)
        //            yield return _Index;
        //        foreach (var _Index in _ManualVerticesIndices.Values)
        //            yield return _Index;
        //    }

        //    else
        //    {
        //        foreach (var _Index in _AutomaticVerticesIndices.Values)
        //            if (IndexFilter(_Index))
        //                yield return _Index;
        //        foreach (var _Index in _ManualVerticesIndices.Values)
        //            if (IndexFilter(_Index))
        //                yield return _Index;
        //    }

        //}

        //#endregion

        //#region VerticesIndices<T>(IndexFilter = null)

        ///// <summary>
        ///// Get all vertices indices maintained by the graph.
        ///// </summary>
        ///// <typeparam name="T">The type of the elements to be indexed.</typeparam>
        ///// <param name="IndexFilter">An optional index filter.</param>
        ///// <returns>The indices associated with the graph.</returns>
        //public IEnumerable<IPropertyElementIndex<T>> VerticesIndices<T>(IndexFilter<T> IndexFilter = null)
        //    where T : IEquatable<T>, IComparable<T>, IComparable
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion

        //#region EdgesIndices(IndexFilter = null)

        ///// <summary>
        ///// Get all edges indices maintained by the graph.
        ///// </summary>
        ///// <param name="IndexFilter">An optional index filter.</param>
        ///// <returns>The indices associated with the graph.</returns>
        //public IEnumerable<IPropertyElementIndex<IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                       TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>

        //       EdgesIndices(IndexFilter<IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> IndexFilter = null)

        //{

        //    if (IndexFilter == null)
        //    {
        //        foreach (var _Index in _AutomaticEdgesIndices.Values)
        //            yield return _Index;
        //        foreach (var _Index in _ManualEdgesIndices.Values)
        //            yield return _Index;
        //    }

        //    else
        //    {
        //        foreach (var _Index in _AutomaticEdgesIndices.Values)
        //            if (IndexFilter(_Index))
        //                yield return _Index;
        //        foreach (var _Index in _ManualEdgesIndices.Values)
        //            if (IndexFilter(_Index))
        //                yield return _Index;
        //    }

        //}

        //#endregion

        //#region EdgesIndices<T>(IndexFilter = null)

        ///// <summary>
        ///// Get all edges indices maintained by the graph.
        ///// </summary>
        ///// <typeparam name="T">The type of the elements to be indexed.</typeparam>
        ///// <param name="IndexFilter">An optional index filter.</param>
        ///// <returns>The indices associated with the graph.</returns>
        //public IEnumerable<IPropertyElementIndex<T>> EdgesIndices<T>(IndexFilter<T> IndexFilter = null)
        //    where T : IEquatable<T>, IComparable<T>, IComparable
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion

        //#region HyperEdgesIndices(IndexFilter = null)

        ///// <summary>
        ///// Get all hyperedges indices maintained by the graph.
        ///// </summary>
        ///// <param name="IndexFilter">An optional index filter.</param>
        ///// <returns>The indices associated with the graph.</returns>
        //public IEnumerable<IPropertyElementIndex<IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>>

        //    HyperEdgesIndices(IndexFilter<IPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                     TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                                     TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> IndexFilter = null)

        //{

        //    if (IndexFilter == null)
        //    {
        //        foreach (var _Index in _AutomaticHyperEdgesIndices.Values)
        //            yield return _Index;
        //        foreach (var _Index in _ManualHyperEdgesIndices.Values)
        //            yield return _Index;
        //    }

        //    else
        //    {
        //        foreach (var _Index in _AutomaticHyperEdgesIndices.Values)
        //            if (IndexFilter(_Index))
        //                yield return _Index;
        //        foreach (var _Index in _ManualHyperEdgesIndices.Values)
        //            if (IndexFilter(_Index))
        //                yield return _Index;
        //    }

        //}

        //#endregion

        //#region HyperEdgesIndices<T>(IndexFilter = null)

        ///// <summary>
        ///// Get all hyperedges indices maintained by the graph.
        ///// </summary>
        ///// <typeparam name="T">The type of the elements to be indexed.</typeparam>
        ///// <param name="IndexFilter">An optional index filter.</param>
        ///// <returns>The indices associated with the graph.</returns>
        //public IEnumerable<IPropertyElementIndex<T>> HyperEdgesIndices<T>(IndexFilter<T> IndexFilter = null)
        //    where T : IEquatable<T>, IComparable<T>, IComparable
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion


        //#region DropVerticesIndex(Name)

        ///// <summary>
        ///// Remove a vertices index associated with the graph.
        ///// </summary>
        ///// <param name="Name">The name of the index to drop.</param>
        //public void DropVerticesIndex(String Name)
        //{

        //    lock (_AutomaticVerticesIndices)
        //    {
        //        if (_AutomaticVerticesIndices.ContainsKey(Name))
        //            _AutomaticVerticesIndices.Remove(Name);
        //    }

        //    lock (_ManualVerticesIndices)
        //    {
        //        if (_ManualVerticesIndices.ContainsKey(Name))
        //            _ManualVerticesIndices.Remove(Name);
        //    }

        //}

        //#endregion

        //#region DropEdgesIndex(Name)

        ///// <summary>
        ///// Remove an edges index associated with the graph.
        ///// </summary>
        ///// <param name="Name">The name of the index to drop.</param>
        //public void DropEdgesIndex(String Name)
        //{

        //    lock (_AutomaticEdgesIndices)
        //    {
        //        if (_AutomaticEdgesIndices.ContainsKey(Name))
        //            _AutomaticEdgesIndices.Remove(Name);
        //    }

        //    lock (_ManualEdgesIndices)
        //    {
        //        if (_ManualEdgesIndices.ContainsKey(Name))
        //            _ManualEdgesIndices.Remove(Name);
        //    }

        //}


        //#endregion

        //#region DropHyperEdgesIndex(Name)

        ///// <summary>
        ///// Remove a hyperedges index associated with the graph.
        ///// </summary>
        ///// <param name="Name">The name of the index to drop.</param>
        //public void DropHyperEdgesIndex(String Name)
        //{

        //    lock (_AutomaticHyperEdgesIndices)
        //    {
        //        if (_AutomaticHyperEdgesIndices.ContainsKey(Name))
        //            _AutomaticHyperEdgesIndices.Remove(Name);
        //    }

        //    lock (_ManualHyperEdgesIndices)
        //    {
        //        if (_ManualHyperEdgesIndices.ContainsKey(Name))
        //            _ManualHyperEdgesIndices.Remove(Name);
        //    }

        //}

        //#endregion


        //#region DropVerticesIndices(IndexNameEvaluator = null)

        ///// <summary>
        ///// Remove vertices indices associated with the graph.
        ///// </summary>
        ///// <param name="IndexNameEvaluator">A delegate evaluating the indices to drop.</param>
        //public void DropVerticesIndex(IndexNameFilter IndexNameEvaluator = null)
        //{

        //    lock (_AutomaticVerticesIndices)
        //    {

        //        if (IndexNameEvaluator == null)
        //            _AutomaticEdgesIndices.Clear();

        //        else
        //        {
                    
        //            var _RemoveList = new List<String>();
                    
        //            foreach (var _IndexName in _AutomaticVerticesIndices.Keys)
        //                if (IndexNameEvaluator(_IndexName))
        //                    _RemoveList.Add(_IndexName);

        //            foreach (var _IndexName in _RemoveList)
        //                _AutomaticVerticesIndices.Remove(_IndexName);

        //        }

        //    }

        //    lock (_ManualVerticesIndices)
        //    {

        //        if (IndexNameEvaluator == null)
        //            _ManualVerticesIndices.Clear();

        //        else
        //        {

        //            var _RemoveList = new List<String>();

        //            foreach (var _IndexName in _ManualVerticesIndices.Keys)
        //                if (IndexNameEvaluator(_IndexName))
        //                    _RemoveList.Add(_IndexName);

        //            foreach (var _IndexName in _RemoveList)
        //                _ManualVerticesIndices.Remove(_IndexName);

        //        }

        //    }

        //}

        //#endregion

        //#region DropEdgesIndices(IndexNameEvaluator = null)

        ///// <summary>
        ///// Remove edges indices associated with the graph.
        ///// </summary>
        ///// <param name="IndexNameEvaluator">A delegate evaluating the indices to drop.</param>
        //public void DropEdgesIndex(IndexNameFilter IndexNameEvaluator = null)
        //{

        //    lock (_AutomaticEdgesIndices)
        //    {

        //        if (IndexNameEvaluator == null)
        //            _AutomaticEdgesIndices.Clear();

        //        else
        //        {

        //            var _RemoveList = new List<String>();

        //            foreach (var _IndexName in _AutomaticEdgesIndices.Keys)
        //                if (IndexNameEvaluator(_IndexName))
        //                    _RemoveList.Add(_IndexName);

        //            foreach (var _IndexName in _RemoveList)
        //                _AutomaticEdgesIndices.Remove(_IndexName);

        //        }

        //    }

        //    lock (_ManualEdgesIndices)
        //    {

        //        if (IndexNameEvaluator == null)
        //            _ManualEdgesIndices.Clear();

        //        else
        //        {

        //            var _RemoveList = new List<String>();

        //            foreach (var _IndexName in _ManualEdgesIndices.Keys)
        //                if (IndexNameEvaluator(_IndexName))
        //                    _RemoveList.Add(_IndexName);

        //            foreach (var _IndexName in _RemoveList)
        //                _ManualEdgesIndices.Remove(_IndexName);

        //        }

        //    }

        //}


        //#endregion

        //#region DropHyperEdgesIndices(IndexNameEvaluator = null)

        ///// <summary>
        ///// Remove hyperedge indices associated with the graph.
        ///// </summary>
        ///// <param name="IndexNameEvaluator">A delegate evaluating the indices to drop.</param>
        //public void DropHyperEdgesIndex(IndexNameFilter IndexNameEvaluator = null)
        //{

        //    lock (_AutomaticHyperEdgesIndices)
        //    {

        //        if (IndexNameEvaluator == null)
        //            _AutomaticHyperEdgesIndices.Clear();

        //        else
        //        {

        //            var _RemoveList = new List<String>();

        //            foreach (var _IndexName in _AutomaticHyperEdgesIndices.Keys)
        //                if (IndexNameEvaluator(_IndexName))
        //                    _RemoveList.Add(_IndexName);

        //            foreach (var _IndexName in _RemoveList)
        //                _AutomaticHyperEdgesIndices.Remove(_IndexName);

        //        }

        //    }

        //    lock (_ManualHyperEdgesIndices)
        //    {

        //        if (IndexNameEvaluator == null)
        //            _ManualHyperEdgesIndices.Clear();

        //        else
        //        {

        //            var _RemoveList = new List<String>();

        //            foreach (var _IndexName in _ManualHyperEdgesIndices.Keys)
        //                if (IndexNameEvaluator(_IndexName))
        //                    _RemoveList.Add(_IndexName);

        //            foreach (var _IndexName in _RemoveList)
        //                _ManualHyperEdgesIndices.Remove(_IndexName);

        //        }

        //    }

        //}

        //#endregion

        //#endregion





        //#region Operator overloading

        //#region Operator == (PropertyGraph1, PropertyGraph2)

        ///// <summary>
        ///// Compares two property graphs for equality.
        ///// </summary>
        ///// <param name="PropertyGraph1">A graph.</param>
        ///// <param name="PropertyGraph2">Another graph.</param>
        ///// <returns>True if both match; False otherwise.</returns>
        //public static Boolean operator == (GenericPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
        //                                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
        //                                                                TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
        //                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
        //                                                                PropertyGraph1,
        //                                   GenericPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
        //                                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
        //                                                                TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
        //                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
        //                                                                PropertyGraph2)
        //{

        //    // If both are null, or both are same instance, return true.
        //    if (Object.ReferenceEquals(PropertyGraph1, PropertyGraph2))
        //        return true;

        //    // If one is null, but not both, return false.
        //    if (((Object) PropertyGraph1 == null) || ((Object) PropertyGraph2 == null))
        //        return false;

        //    return PropertyGraph1.Equals(PropertyGraph2);

        //}

        //#endregion

        //#region Operator != (PropertyGraph1, PropertyGraph2)

        ///// <summary>
        ///// Compares two property graphs for inequality.
        ///// </summary>
        ///// <param name="PropertyGraph1">A graph.</param>
        ///// <param name="PropertyGraph2">Another graph.</param>
        ///// <returns>False if both match; True otherwise.</returns>
        //public static Boolean operator != (GenericPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
        //                                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
        //                                                                TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
        //                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
        //                                                                PropertyGraph1,
        //                                   GenericPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
        //                                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
        //                                                                TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
        //                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
        //                                                                PropertyGraph2)
        //{
        //    return !(PropertyGraph1 == PropertyGraph2);
        //}

        //#endregion

        //#region Operator <  (PropertyGraph1, PropertyGraph2)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="PropertyGraph1">A graph.</param>
        ///// <param name="PropertyGraph2">Another graph.</param>
        ///// <returns>true|false</returns>
        //public static Boolean operator < (GenericPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
        //                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
        //                                                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
        //                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
        //                                                               PropertyGraph1,
        //                                  GenericPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
        //                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
        //                                                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
        //                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
        //                                                               PropertyGraph2)
        //{

        //    if ((Object) PropertyGraph1 == null)
        //        throw new ArgumentNullException("The given PropertyGraph1 must not be null!");

        //    if ((Object) PropertyGraph2 == null)
        //        throw new ArgumentNullException("The given PropertyGraph2 must not be null!");

        //    return PropertyGraph1.CompareTo(PropertyGraph2) < 0;

        //}

        //#endregion

        //#region Operator <= (PropertyGraph1, PropertyGraph2)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="PropertyGraph1">A graph.</param>
        ///// <param name="PropertyGraph2">Another graph.</param>
        ///// <returns>true|false</returns>
        //public static Boolean operator <= (GenericPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
        //                                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
        //                                                                TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
        //                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
        //                                                                PropertyGraph1,
        //                                   GenericPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
        //                                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
        //                                                                TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
        //                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
        //                                                                PropertyGraph2)
        //{
        //    return !(PropertyGraph1 > PropertyGraph2);
        //}

        //#endregion

        //#region Operator >  (PropertyGraph1, PropertyGraph2)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="PropertyGraph1">A graph.</param>
        ///// <param name="PropertyGraph2">Another graph.</param>
        ///// <returns>true|false</returns>
        //public static Boolean operator > (GenericPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
        //                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
        //                                                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
        //                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
        //                                                               PropertyGraph1,
        //                                  GenericPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
        //                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
        //                                                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
        //                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
        //                                                               PropertyGraph2)
        //{

        //    if ((Object) PropertyGraph1 == null)
        //        throw new ArgumentNullException("The given PropertyGraph1 must not be null!");

        //    if ((Object) PropertyGraph2 == null)
        //        throw new ArgumentNullException("The given PropertyGraph2 must not be null!");

        //    return PropertyGraph1.CompareTo(PropertyGraph2) > 0;

        //}

        //#endregion

        //#region Operator >= (PropertyGraph1, PropertyGraph2)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="PropertyGraph1">A graph.</param>
        ///// <param name="PropertyGraph2">Another graph.</param>
        ///// <returns>true|false</returns>
        //public static Boolean operator >= (GenericPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
        //                                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
        //                                                                TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
        //                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
        //                                                                PropertyGraph1,
        //                                   GenericPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
        //                                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
        //                                                                TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
        //                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
        //                                                                PropertyGraph2)
        //{
        //    return !(PropertyGraph1 < PropertyGraph2);
        //}

        //#endregion

        //#endregion

        //#region IDynamicGraphObject<InMemoryGenericPropertyGraph> Members

        //#region GetMetaObject(myExpression)

        ///// <summary>
        ///// Return the appropriate DynamicMetaObject.
        ///// </summary>
        ///// <param name="myExpression">An Expression.</param>
        //public DynamicMetaObject GetMetaObject(Expression myExpression)
        //{
        //    return new DynamicGraphElement<GenericPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
        //                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,      TEdgeCollection,
        //                                                        TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge, TMultiEdgeCollection,
        //                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge, THyperEdgeCollection>>
        //                                                        (myExpression, this);
        //}

        //#endregion

        //#region GetDynamicMemberNames()

        ///// <summary>
        ///// Returns an enumeration of all property keys.
        ///// </summary>
        //public virtual IEnumerable<String> GetDynamicMemberNames()
        //{
        //    foreach (var _PropertyKey in PropertyData.Keys)
        //        yield return _PropertyKey.ToString();
        //}

        //#endregion


        //#region SetMember(myBinder, myObject)

        ///// <summary>
        ///// Sets a new property or overwrites an existing.
        ///// </summary>
        ///// <param name="myBinder">The property key</param>
        ///// <param name="myObject">The property value</param>
        //public virtual Object SetMember(String myBinder, Object myObject)
        //{
        //    return PropertyData.SetProperty((TKeyVertex) (Object) myBinder, (TValueVertex) myObject);
        //}

        //#endregion

        //#region GetMember(myBinder)

        ///// <summary>
        ///// Returns the value of the given property key.
        ///// </summary>
        ///// <param name="myBinder">The property key.</param>
        //public virtual Object GetMember(String myBinder)
        //{
        //    TValueVertex myObject;
        //    PropertyData.GetProperty((TKeyVertex) (Object) myBinder, out myObject);
        //    return myObject as Object;
        //}

        //#endregion

        //#region DeleteMember(myBinder)

        ///// <summary>
        ///// Tries to remove the property identified by the given property key.
        ///// </summary>
        ///// <param name="myBinder">The property key.</param>
        //public virtual Object DeleteMember(String myBinder)
        //{

        //    try
        //    {
        //        PropertyData.Remove((TKeyVertex) (Object) myBinder);
        //        return true;
        //    }
        //    catch
        //    { }

        //    return false;

        //}

        //#endregion

        //#endregion

        #region IComparable Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public Int32 CompareTo(Object Object)
        {

            if ((Object) Object == null)
                throw new ArgumentNullException("The given Object must not be null!");

            return CompareTo((TIdVertex) Object);

        }

        #endregion

        #region CompareTo(VertexId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VertexId">An object to compare with.</param>
        public Int32 CompareTo(TIdVertex VertexId)
        {

            if ((Object) VertexId == null)
                throw new ArgumentNullException("The given VertexId must not be null!");

            return Id.CompareTo(VertexId);

        }

        #endregion

        #region CompareTo(IGraphElement)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="IGraphElement">An object to compare with.</param>
        public Int32 CompareTo(IGraphElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex> IGraphElement)
        {

            if ((Object) IGraphElement == null)
                throw new ArgumentNullException("The given IGraphElement must not be null!");

            return Id.CompareTo(IGraphElement.PropertyData[IdKey]);

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

            // Check if the given object can be casted to a InMemoryGenericPropertyGraph
            var PropertyGraph = Object as AGenericPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>;
            if ((Object) PropertyGraph == null)
                return false;

            return this.Equals(PropertyGraph);

        }

        #endregion

        #region Equals(TIdVertex VertexId)

        /// <summary>
        /// Compares the identification of the property graph with another vertex id.
        /// </summary>
        /// <param name="VertexId">A vertex identification to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(TIdVertex VertexId)
        {

            if ((Object)VertexId == null)
                return false;

            return Id.Equals(VertexId);

        }

        #endregion

        #region Equals(IGraphElement)

        /// <summary>
        /// Compares this property graph to another graph element.
        /// </summary>
        /// <param name="IGraphElement">An object to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IGraphElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex> IGraphElement)
        {

            if ((Object) IGraphElement == null)
                return false;

            return Id.Equals(IGraphElement.PropertyData[IdKey]);

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
        /// Returns a string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return "PropertyGraph [Id: " + Id.ToString() + ", " + _Vertices.Count() + " Vertices, " + _ForeignEdges.Count() + " Edges]";
        }

        #endregion

    }

}
