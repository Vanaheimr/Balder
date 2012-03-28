﻿/*
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
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;

using de.ahzf.Blueprints.Indices;
using de.ahzf.Blueprints.PropertyGraphs.Indices;
using de.ahzf.Illias.Commons;
using System.Threading;

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
    public class PropertyGraph : PropertyVertex
    {

        #region Constructor(s)

        #region PropertyGraph()

        /// <summary>
        /// Creates a new class-based in-memory implementation of a property graph.
        /// (This constructor is needed for automatic activation!)
        /// </summary>
        public PropertyGraph()
            : this(PropertyGraph.NewVertexId)
        { }

        #endregion

        #region PropertyGraph(GraphInitializer)

        /// <summary>
        /// Creates a new class-based in-memory implementation of a property graph.
        /// </summary>
        /// <param name="GraphInitializer">A delegate to initialize the new property graph.</param>
        public PropertyGraph(GraphInitializer<UInt64, Int64, String, String, Object,
                                              UInt64, Int64, String, String, Object,
                                              UInt64, Int64, String, String, Object,
                                              UInt64, Int64, String, String, Object> GraphInitializer)
            : this(PropertyGraph.NewVertexId, GraphInitializer)
        { }

        #endregion

        #region PropertyGraph(GraphId, GraphInitializer = null)

        /// <summary>
        /// Creates a new class-based in-memory implementation of a property graph.
        /// </summary>
        /// <param name="GraphId">A unique identification for this graph (which is also a vertex!).</param>
        /// <param name="GraphInitializer">A delegate to initialize the new property graph.</param>
        public PropertyGraph(UInt64 GraphId,
                             GraphInitializer<UInt64, Int64, String, String, Object,
                                              UInt64, Int64, String, String, Object,
                                              UInt64, Int64, String, String, Object,
                                              UInt64, Int64, String, String, Object> GraphInitializer = null)

            : base(GraphId,

                    #region Vertices

                    // Property keys
                    GraphDBOntology.Id().Suffix,
                    GraphDBOntology.RevId().Suffix,
                    GraphDBOntology.Description().Suffix,
                    () => new Dictionary<String, Object>(),
                    
                    // Create a new vertex identification
                    (graph) => PropertyGraph.NewVertexId,

                    // Create a new vertex
                    (Graph, _VertexId, _VertexLabel, VertexInitializer) =>
                        new PropertyVertex
                            (Graph as IPropertyGraph,
                             _VertexId,
                             GraphDBOntology.Id().Suffix,
                             GraphDBOntology.RevId().Suffix,
                             GraphDBOntology.Description().Suffix,
                             () => new Dictionary<String, Object>(),

                             () => new GroupedCollection<UInt64, IGenericPropertyVertex   <UInt64, Int64, String, String, Object,
                                                                                           UInt64, Int64, String, String, Object,
                                                                                           UInt64, Int64, String, String, Object,
                                                                                           UInt64, Int64, String, String, Object>, String>(),

                             () => new GroupedCollection<UInt64, IGenericPropertyEdge     <UInt64, Int64, String, String, Object,
                                                                                           UInt64, Int64, String, String, Object,
                                                                                           UInt64, Int64, String, String, Object,
                                                                                           UInt64, Int64, String, String, Object>, String>(),

                             () => new GroupedCollection<UInt64, IGenericPropertyMultiEdge<UInt64, Int64, String, String, Object,
                                                                                           UInt64, Int64, String, String, Object,
                                                                                           UInt64, Int64, String, String, Object,
                                                                                           UInt64, Int64, String, String, Object>, String>(),

                             () => new GroupedCollection<UInt64, IGenericPropertyHyperEdge<UInt64, Int64, String, String, Object,
                                                                                           UInt64, Int64, String, String, Object,
                                                                                           UInt64, Int64, String, String, Object,
                                                                                           UInt64, Int64, String, String, Object>, String>(),
                             VertexInitializer),

                    // The vertices collection
                    () => new GroupedCollection< UInt64, IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                                UInt64, Int64, String, String, Object,
                                                                                UInt64, Int64, String, String, Object,
                                                                                UInt64, Int64, String, String, Object>, String>(),

                                #endregion

                    #region Edges

                    // Create a new edge identification
                    (graph) => PropertyGraph.NewEdgeId,

                    // Create a new edge
                    (Graph, OutVertex, InVertex, EdgeId, Label, EdgeInitializer) =>
                        new PropertyEdge
                            (Graph as IPropertyGraph,
                             OutVertex as IPropertyVertex,
                             InVertex as IPropertyVertex,
                             EdgeId,
                             Label,
                             GraphDBOntology.Id().Suffix,
                             GraphDBOntology.RevId().Suffix,
                             GraphDBOntology.Description().Suffix,
                             () => new Dictionary<String, Object>(),
                             EdgeInitializer),

                    // The edges collection
                    () => new GroupedCollection<UInt64, IGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                                             UInt64, Int64, String, String, Object,
                                                                             UInt64, Int64, String, String, Object,
                                                                             UInt64, Int64, String, String, Object>, String>(),

                    #endregion

                    #region MultiEdges

                    // Create a new multiedge identification
                    (graph) => PropertyGraph.NewMultiEdgeId,

                    // Create a new multiedge
                    (Graph, EdgeSelector, MultiEdgeId, Label, MultiEdgeInitializer) =>

                       new PropertyMultiEdge
                            (Graph as IPropertyGraph,
                             EdgeSelector,
                             MultiEdgeId,
                             Label,
                             GraphDBOntology.Id().Suffix,
                             GraphDBOntology.RevId().Suffix,
                             GraphDBOntology.Description().Suffix,

                             () => new Dictionary<String, Object>(),
                             () => new GroupedCollection<String, UInt64, IGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                                                              UInt64, Int64, String, String, Object,
                                                                                              UInt64, Int64, String, String, Object,
                                                                                              UInt64, Int64, String, String, Object>>(),

                             MultiEdgeInitializer),

                    // The multiedges collection
                    () => new GroupedCollection< UInt64, IGenericPropertyMultiEdge<UInt64, Int64, String, String, Object,
                                                                                   UInt64, Int64, String, String, Object,
                                                                                   UInt64, Int64, String, String, Object,
                                                                                   UInt64, Int64, String, String, Object>, String>(),

                    #endregion

                    #region HyperEdges

                    // Create a new hyperedge identification
                    (graph) => PropertyGraph.NewHyperEdgeId,

                    // Create a new hyperedge
                    (Graph, EdgeSelector, HyperEdgeId, Label, HyperEdgeInitializer) =>

                       new PropertyHyperEdge
                            (Graph as IPropertyGraph,
                             EdgeSelector,
                             HyperEdgeId,
                             Label,
                             GraphDBOntology.Id().Suffix,
                             GraphDBOntology.RevId().Suffix,
                             GraphDBOntology.Description().Suffix,

                             () => new Dictionary<String, Object>(),
                             () => new GroupedCollection<UInt64, IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                                        UInt64, Int64, String, String, Object,
                                                                                        UInt64, Int64, String, String, Object,
                                                                                        UInt64, Int64, String, String, Object>, String>(),

                             HyperEdgeInitializer),

                    // The hyperedges collection
                    () => new GroupedCollection<UInt64, IGenericPropertyHyperEdge<UInt64, Int64, String, String, Object,
                                                                                  UInt64, Int64, String, String, Object,
                                                                                  UInt64, Int64, String, String, Object,
                                                                                  UInt64, Int64, String, String, Object>, String>()

                    #endregion

                  )

        {

            _NewVertexId    = 0;
            _NewEdgeId      = 0;
            _NewMultiEdgeId = 0;
            _NewHyperEdgeId = 0;

            if (GraphInitializer != null)
                GraphInitializer(this);

        }

        #endregion

        #endregion


        #region (private) NewIds

        private static Int64 _NewVertexId;
        private static Int64 _NewEdgeId;
        private static Int64 _NewMultiEdgeId;
        private static Int64 _NewHyperEdgeId;

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
                return (Int64)UniqueTimestamp.Ticks;
            }
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return String.Concat("PropertyGraph [Id: ", Id.ToString(),
                                 ", vertices: ",   _VerticesWhenGraph.Count(),
                                 ", edges: ",      _EdgesWhenGraph.Count(),
                                 ", multiedges: ", _MultiEdgesWhenGraph.Count(),
                                 ", hyperedges: ", _HyperEdgesWhenGraph.Count(), "]");
        }

        #endregion

    }

}