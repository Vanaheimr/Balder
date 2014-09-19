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

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Illias.Collections;
using org.GraphDefined.Vanaheimr.Balder;
using org.GraphDefined.Vanaheimr.Balder.InMemory;
using org.GraphDefined.Vanaheimr.Illias.Votes;
using System.Collections.Generic;

#endregion

namespace org.GraphDefined.Vanaheimr.Balder.Schema
{

    /// <summary>
    /// Graph schema handling
    /// </summary>
    public static class GraphSchemaHandling
    {

        public class SchemaUsage<T>
        {
            public  T           DefaultValue;
            public  Type        DefaultType;
        }

        public const String AlternativeUsage        = "AlternativeUsage";
        public const String ContinuousLearningMode  = "Continuous Learning Mode";
        public const String EnforceSchemaMode       = "Enforce Schema Mode";

        #region StrictSchemaGraph(this PropertyGraph, SchemaGraphId, SchemaGraphDescription = null, ContinuousLearning = true, EnforceSchema = false, ExistingSchemaGraph = null)

        /// <summary>
        /// Analyse the given property graph and return a strict schema graph for this graph.
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
        /// <typeparam name="TIdHyperEdge">The type of the multiedge identifiers.</typeparam>
        /// <typeparam name="TRevIdHyperEdge">The type of the multiedge revision identifiers.</typeparam>
        /// <typeparam name="THyperEdgeLabel">The type of the multiedge label.</typeparam>
        /// <typeparam name="TKeyHyperEdge">The type of the multiedge property keys.</typeparam>
        /// <typeparam name="TValueHyperEdge">The type of the multiedge property values.</typeparam>
        /// <param name="Graph">The property graph to extract the schema from.</param>
        /// <param name="SchemaGraphId">The schema graph identification.</param>
        /// <param name="SchemaGraphDescription">An optional description of the schema graph.</param>
        /// <param name="ContinuousLearning">If set to true, the schema graph will subsribe vertex/edge additions in order to continuously learn the graph schema.</param>
        /// <param name="EnforceSchema">Disallow the 'continous learning' and any changes of the graph schema after setting up the schema graph. NOTE: Changing the schema graph is still allowed!</param>
        /// <param name="ExistingSchemaGraph">An existing schema graph.</param>
        public static IGenericPropertyGraph<TVertexLabel,    TRevIdVertex,    VertexLabel,    TKeyVertex,    Object,
                                            TEdgeLabel,      TRevIdEdge,      EdgeLabel,      TKeyEdge,      Object,
                                            TMultiEdgeLabel, TRevIdMultiEdge, MultiEdgeLabel, TKeyMultiEdge, Object,
                                            THyperEdgeLabel, TRevIdHyperEdge, HyperEdgeLabel, TKeyHyperEdge, Object>

                          StrictSchemaGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>(

                             this IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph,

                             TVertexLabel               SchemaGraphId,
                             String                     SchemaGraphDescription       = null,
                             Boolean                    ContinuousLearning           = true,
                             Boolean                    EnforceSchema                = false,
                             IEnumerable<TKeyVertex>    IgnoreVertexPropertyKeys     = null,
                             IEnumerable<TKeyEdge>      IgnoreEdgePropertyKeys       = null,
                             IEnumerable<TKeyMultiEdge> IgnoreMultiEdgePropertyKeys  = null,
                             IEnumerable<TKeyHyperEdge> IgnoreHyperEdgePropertyKeys  = null,

                             IGenericPropertyGraph<TVertexLabel,    TRevIdVertex,    VertexLabel,    TKeyVertex,    Object,
                                                   TEdgeLabel,      TRevIdEdge,      EdgeLabel,      TKeyEdge,      Object,
                                                   TMultiEdgeLabel, TRevIdMultiEdge, MultiEdgeLabel, TKeyMultiEdge, Object,
                                                   THyperEdgeLabel, TRevIdHyperEdge, HyperEdgeLabel, TKeyHyperEdge, Object> ExistingSchemaGraph = null)


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

            if (EnforceSchema)
                ContinuousLearning = false;

            #region Create a new or use the existing schema graph

            var SchemaGraph = (ExistingSchemaGraph != null) ?

                   ExistingSchemaGraph :

                   new GenericPropertyVertex<TVertexLabel,    TRevIdVertex,    VertexLabel,    TKeyVertex,    Object,
                                             TEdgeLabel,      TRevIdEdge,      EdgeLabel,      TKeyEdge,      Object,
                                             TMultiEdgeLabel, TRevIdMultiEdge, MultiEdgeLabel, TKeyMultiEdge, Object,
                                             THyperEdgeLabel, TRevIdHyperEdge, HyperEdgeLabel, TKeyHyperEdge, Object>(

                                                 SchemaGraphId,

                                                 // Vertices
                                                 Graph.IdKey,
                                                 Graph.RevIdKey,
                                                 Graph.LabelKey,
                                                 g => { return default(TVertexLabel); },      // AutoIdGeneration currently turned off!
                                                 VertexLabel.DEFAULT,

                                                 // Edges
                                                 Graph.EdgeIdKey,
                                                 Graph.EdgeRevIdKey,
                                                 Graph.EdgeLabelKey,
                                                 g => { return default(TEdgeLabel); },        // AutoIdGeneration currently turned off!
                                                 EdgeLabel.DEFAULT,

                                                 // Multiedges
                                                 Graph.MultiEdgeIdKey,
                                                 Graph.MultiEdgeRevIdKey,
                                                 Graph.MultiEdgeLabelKey,
                                                 g => { return default(TMultiEdgeLabel); },   // AutoIdGeneration currently turned off!
                                                 MultiEdgeLabel.DEFAULT,

                                                 // Hyperedges
                                                 Graph.HyperEdgeIdKey,
                                                 Graph.HyperEdgeRevIdKey,
                                                 Graph.HyperEdgeLabelKey,
                                                 g => { return default(THyperEdgeLabel); },   // AutoIdGeneration currently turned off!
                                                 HyperEdgeLabel.DEFAULT,

                                                 Graph.VoteCreator)

                                                 { };

            #endregion

            //SchemaGraph.SetProperty(GraphSchemaHandling.ContinuousLearningMode,  

            #region Setup some delegates to simplify vertex, edge, multiedge, hyperedge management

            #region Ignore the following properties

            var _IgnoreVertexPropertyKeys     = new org.GraphDefined.Vanaheimr.Illias.Collections.HashedSet<TKeyVertex>()    { Graph.IdKey,          Graph.RevIdKey,          Graph.LabelKey };
            var _IgnoreEdgePropertyKeys       = new org.GraphDefined.Vanaheimr.Illias.Collections.HashedSet<TKeyEdge>()      { Graph.EdgeIdKey,      Graph.EdgeRevIdKey,      Graph.EdgeLabelKey };
            var _IgnoreMultiEdgePropertyKeys  = new org.GraphDefined.Vanaheimr.Illias.Collections.HashedSet<TKeyMultiEdge>() { Graph.MultiEdgeIdKey, Graph.MultiEdgeRevIdKey, Graph.MultiEdgeLabelKey };
            var _IgnoreHyperEdgePropertyKeys  = new org.GraphDefined.Vanaheimr.Illias.Collections.HashedSet<TKeyHyperEdge>() { Graph.HyperEdgeIdKey, Graph.HyperEdgeRevIdKey, Graph.HyperEdgeLabelKey };

            if (IgnoreVertexPropertyKeys != null)
                IgnoreVertexPropertyKeys.   ForEach(PropertyKey => _IgnoreVertexPropertyKeys.   Add(PropertyKey));

            if (IgnoreEdgePropertyKeys != null)
                IgnoreEdgePropertyKeys.     ForEach(PropertyKey => _IgnoreEdgePropertyKeys.     Add(PropertyKey));

            if (IgnoreMultiEdgePropertyKeys != null)
                IgnoreMultiEdgePropertyKeys.ForEach(PropertyKey => _IgnoreMultiEdgePropertyKeys.Add(PropertyKey));

            if (IgnoreHyperEdgePropertyKeys != null)
                IgnoreHyperEdgePropertyKeys.ForEach(PropertyKey => _IgnoreHyperEdgePropertyKeys.Add(PropertyKey));

            #endregion

            #region AddVertex delegate...

            Action<IReadOnlyGenericPropertyGraph <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                   IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                AddVertexDelegate = (g, v) => SchemaGraph.AddVertex(VertexId:           v.Label,
                                                                    Label:              VertexLabel.Vertex,
                                                                    OnDuplicateVertex:  vertex => { },
                                                                    AnywayDo:           vertex => {
                                                                                            v.ForEach(kvp => {
                                                                                                if (!_IgnoreVertexPropertyKeys.Contains(kvp.Key))
                                                                                                    vertex.ZSetAdd(kvp.Key, kvp.Value.GetType());
                                                                                            });
                                                                                        });

            #endregion

            #region AddEdge delegate...

            Action<IReadOnlyGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                   IReadOnlyGenericPropertyEdge <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

                AddEdgeDelegate = (g, e) => {

                                                var _InVertex = SchemaGraph.VertexById(e.InVertex.Label).AsMutable();

                                                // Work-around for cases when edges are added via VertexInitializer and schema handling is active!
                                                // SchemaEdge will be added before the VertexInitializer finished and thus the schema vertex is not yet created!
                                                if (_InVertex == null)
                                                {
                                                    AddVertexDelegate(g, e.InVertex);
                                                    _InVertex = SchemaGraph.VertexById(e.InVertex.Label).AsMutable();
                                                }

                                                SchemaGraph.AddEdge(EdgeId:              e.Label,
                                                                               OutVertex:       SchemaGraph.VertexById(e.OutVertex.Label).AsMutable(),
                                                                               Label:           EdgeLabel.IsConnectedWith,
                                                                               InVertex:        _InVertex,
                                                                               OnDuplicateEdge: Edge => { throw new SchemaViolation("Strict schema violation! The edge label '" + e.Label + "' is already used for '" +
                                                                                                                                    Edge.OutVertex.Id.   ToString() + " -> " + Edge.InVertex.Id.   ToString() + "' relations and thus can not be used for '" +
                                                                                                                                       e.OutVertex.Label.ToString() + " -> " +    e.InVertex.Label.ToString() + "' relations."); },
                                                                               AnywayDo:        Edge => {
                                                                                   e.ForEach(kvp => {
                                                                                       if (!_IgnoreEdgePropertyKeys.Contains(kvp.Key))
                                                                                           Edge.ZSetAdd(kvp.Key, kvp.Value.GetType());
                                                                                   });
                                                                               }
                                                                              );

                };

            #endregion

            #endregion

            #region If 'continuous learning', then wire graph 'update' events

            if (ContinuousLearning)
            {

                // Register [Vertex|Edge]Added events: PropertyGraph -> SchemaGraph

                Graph.OnVertexAddition.OnNotification += (g, v) => AddVertexDelegate(g, v);
                Graph.OnEdgeAddition.  OnNotification += (g, e) => AddEdgeDelegate  (g, e);

            }

            #endregion

            //ToDo: Attach a SchemaGraph to a PropertyGraph to _enforce a schema_...

            #region Add all current vertices, edges, multiedges and hyperedges

            Graph.Vertices().ForEach(v => AddVertexDelegate(Graph, v));
            Graph.Edges().   ForEach(e => AddEdgeDelegate  (Graph, e));

            #endregion

            return SchemaGraph;

        }

        #endregion

    }

}
