/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
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

using de.ahzf.Illias.Commons;
using de.ahzf.Illias.Commons.Collections;
using de.ahzf.Vanaheimr.Blueprints;
using de.ahzf.Vanaheimr.Blueprints.InMemory;

#endregion

namespace de.ahzf.Vanaheimr.Blueprints.Schema
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

        public const String AlternativeUsage = "AlternativeUsage";

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
        public static IGenericPropertyGraph<TVertexLabel,    TRevIdVertex,    VertexLabel,    TKeyVertex, Object,
                                            TEdgeLabel,      TRevIdEdge,      EdgeLabel,      String,     Object,
                                            TMultiEdgeLabel, TRevIdMultiEdge, MultiEdgeLabel, String,     Object,
                                            THyperEdgeLabel, TRevIdHyperEdge, HyperEdgeLabel, String,     Object>

                          StrictSchemaGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>(

                             this IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph,

                             TVertexLabel  SchemaGraphId,
                             String        SchemaGraphDescription  = null,
                             Boolean       ContinuousLearning      = true,
                             Boolean       EnforceSchema           = false,

                             IGenericPropertyGraph<TVertexLabel,    TRevIdVertex,    VertexLabel,    TKeyVertex, Object,
                                                   TEdgeLabel,      TRevIdEdge,      EdgeLabel,      String,     Object,
                                                   TMultiEdgeLabel, TRevIdMultiEdge, MultiEdgeLabel, String,     Object,
                                                   THyperEdgeLabel, TRevIdHyperEdge, HyperEdgeLabel, String,     Object> ExistingSchemaGraph = null)


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

                   new GenericPropertyVertex<TVertexLabel,    TRevIdVertex,    VertexLabel,    TKeyVertex, Object,
                                             TEdgeLabel,      TRevIdEdge,      EdgeLabel,      String,     Object,
                                             TMultiEdgeLabel, TRevIdMultiEdge, MultiEdgeLabel, String,     Object,
                                             THyperEdgeLabel, TRevIdHyperEdge, HyperEdgeLabel, String,     Object>(

                                                 SchemaGraphId,
                                                 SchemaGraphDescription,

                                                 // Vertices
                                                 Graph.IdKey,
                                                 Graph.RevIdKey,
                                                 Graph.LabelKey,
                                                 Graph.DescriptionKey,
                                                 (g) => { return default(TVertexLabel); },      // AutoIdGeneration currently turned off!
                                                 VertexLabel.DEFAULT,

                                                 // Edges
                                                 GraphDBOntology.Id().Suffix,
                                                 GraphDBOntology.RevId().Suffix,
                                                 GraphDBOntology.Label().Suffix,
                                                 GraphDBOntology.Description().Suffix,
                                                 (g) => { return default(TEdgeLabel); },        // AutoIdGeneration currently turned off!
                                                 EdgeLabel.DEFAULT,

                                                 // Multiedges
                                                 GraphDBOntology.Id().Suffix,
                                                 GraphDBOntology.RevId().Suffix,
                                                 GraphDBOntology.Label().Suffix,
                                                 GraphDBOntology.Description().Suffix,
                                                 (g) => { return default(TMultiEdgeLabel); },   // AutoIdGeneration currently turned off!
                                                 MultiEdgeLabel.DEFAULT,

                                                 // Hyperedges
                                                 GraphDBOntology.Id().Suffix,
                                                 GraphDBOntology.RevId().Suffix,
                                                 GraphDBOntology.Label().Suffix,
                                                 GraphDBOntology.Description().Suffix,
                                                 (g) => { return default(THyperEdgeLabel); },   // AutoIdGeneration currently turned off!
                                                 HyperEdgeLabel.DEFAULT)

                                                 { };

            #endregion

            #region If 'continuous learning', then wire graph 'update' events

            if (ContinuousLearning)
            {

                #region Register [Vertex|Edge]Added events: PropertyGraph -> SchemaGraph

                Graph.OnVertexAddition.OnNotification +=
                    (g, v) => SchemaGraph.AddVertexIfNotExists(Id:    v.Label,
                                                               Label: VertexLabel.Vertex);

                Graph.OnEdgeAddition.OnNotification   +=
                    (g, e) => SchemaGraph.AddEdgeIfNotExists(Id:              e.Label,
                                                             OutVertex:       SchemaGraph.VertexById(e.OutVertex.Label).AsMutable(),
                                                             Label:           EdgeLabel.IsConnectedWith,
                                                             InVertex:        SchemaGraph.VertexById(e.InVertex.Label).AsMutable(),
                                                             EdgeInitializer: Edge => Edge.ZSetAdd(GraphSchemaHandling.AlternativeUsage, e.OutVertex.Label.ToString() + " -> " + e.InVertex.Label.ToString()),
                                                             AnywayDo:        Edge => { throw new Exception("Strict schema graph violation!"); }
                                                            );

                #endregion

            }

            #endregion

            //ToDo: Attach a SchemaGraph to a PropertyGraph to _enforce a schema_...

            #region Add all current vertices and edges: PropertyGraph -> SchemaGraph

            Graph.Vertices().ForEach(v => SchemaGraph.AddVertexIfNotExists(Id:     v.Label,
                                                                           Label:  VertexLabel.Vertex));

            Graph.Edges().   ForEach(e => {

                IReadOnlyGenericPropertyEdge<TVertexLabel,    TRevIdVertex,    VertexLabel,    TKeyVertex, Object,
                                             TEdgeLabel,      TRevIdEdge,      EdgeLabel,      String,     Object,
                                             TMultiEdgeLabel, TRevIdMultiEdge, MultiEdgeLabel, String,     Object,
                                             THyperEdgeLabel, TRevIdHyperEdge, HyperEdgeLabel, String,     Object> __Edge;

                if (SchemaGraph.TryGetEdgeById(e.Label, out __Edge))
                {

                    if (__Edge.OutVertex.Id.Equals(e.OutVertex.Label) &&
                        __Edge.InVertex. Id.Equals(e.InVertex.Label))
                    {
                    }

                    else
                        throw new Exception("Strict schema graph violation! The edge label '" + e.Label + "' is used for '" +
                                                  e.OutVertex.Label.ToString() + " -> " +      e.InVertex.Label.ToString() + "' and '" +
                                             __Edge.OutVertex.Id.   ToString() + " -> " + __Edge.InVertex.Id.   ToString() + "'");

                }

                SchemaGraph.AddEdgeIfNotExists  (Id:              e.Label,
                                                 OutVertex:       SchemaGraph.VertexById(e.OutVertex.Label).AsMutable(),
                                                 Label:           EdgeLabel.IsConnectedWith,
                                                 InVertex:        SchemaGraph.VertexById(e.InVertex. Label).AsMutable(),
                                                 EdgeInitializer: Edge => Edge.ZSetAdd(GraphSchemaHandling.AlternativeUsage, e.OutVertex.Label.ToString() + " -> " + e.InVertex.Label.ToString()),
                                                 AnywayDo:        Edge => Edge.ZSetAdd(GraphSchemaHandling.AlternativeUsage, e.OutVertex.Label.ToString() + " -> " + e.InVertex.Label.ToString())
                                                );

            });

            //Graph.Edges().   ForEach(e => SchemaGraph.AddHyperEdge(//e.Label,
            //                                                       HyperEdgeLabel.DEFAULT,
            //                                                       null,
            //                                                       SchemaGraph.VertexById(e.OutVertex.Label).AsMutable(),
            //                                                       SchemaGraph.VertexById(e.InVertex.Label).AsMutable()));


            // It would also be a good idea to analyse the usage of the vertex/edge properties!

            #endregion

            return SchemaGraph;

        }

        #endregion

        #region SchemaGraph(this PropertyGraph, SchemaGraphId, SchemaGraphDescription = null, ContinuousLearning = true, EnforceSchema = false, ExistingSchemaGraph = null)

        /// <summary>
        /// Analyse the given property graph and return a schema graph for this graph.
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
        public static IGenericPropertyGraph<TVertexLabel,    TRevIdVertex,    VertexLabel,    TKeyVertex, Object,
                                            TEdgeLabel,      TRevIdEdge,      EdgeLabel,      String,     Object,
                                            TMultiEdgeLabel, TRevIdMultiEdge, MultiEdgeLabel, String,     Object,
                                            THyperEdgeLabel, TRevIdHyperEdge, HyperEdgeLabel, String,     Object>

                          SchemaGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>(

                             this IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph,

                             TVertexLabel  SchemaGraphId,
                             String        SchemaGraphDescription  = null,
                             Boolean       ContinuousLearning      = true,
                             Boolean       EnforceSchema           = false,

                             IGenericPropertyGraph<TVertexLabel,    TRevIdVertex,    VertexLabel,    TKeyVertex, Object,
                                                   TEdgeLabel,      TRevIdEdge,      EdgeLabel,      String,     Object,
                                                   TMultiEdgeLabel, TRevIdMultiEdge, MultiEdgeLabel, String,     Object,
                                                   THyperEdgeLabel, TRevIdHyperEdge, HyperEdgeLabel, String,     Object> ExistingSchemaGraph = null)


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

                   new GenericPropertyVertex<TVertexLabel,    TRevIdVertex,    VertexLabel,    TKeyVertex, Object,
                                             TEdgeLabel,      TRevIdEdge,      EdgeLabel,      String,     Object,
                                             TMultiEdgeLabel, TRevIdMultiEdge, MultiEdgeLabel, String,     Object,
                                             THyperEdgeLabel, TRevIdHyperEdge, HyperEdgeLabel, String,     Object>(

                                                 SchemaGraphId,
                                                 SchemaGraphDescription,

                                                 // Vertices
                                                 Graph.IdKey,
                                                 Graph.RevIdKey,
                                                 Graph.LabelKey,
                                                 Graph.DescriptionKey,
                                                 (g) => { return default(TVertexLabel); },      // AutoIdGeneration currently turned off!
                                                 VertexLabel.DEFAULT,

                                                 // Edges
                                                 GraphDBOntology.Id().Suffix,
                                                 GraphDBOntology.RevId().Suffix,
                                                 GraphDBOntology.Label().Suffix,
                                                 GraphDBOntology.Description().Suffix,
                                                 (g) => { return default(TEdgeLabel); },        // AutoIdGeneration currently turned off!
                                                 EdgeLabel.DEFAULT,

                                                 // Multiedges
                                                 GraphDBOntology.Id().Suffix,
                                                 GraphDBOntology.RevId().Suffix,
                                                 GraphDBOntology.Label().Suffix,
                                                 GraphDBOntology.Description().Suffix,
                                                 (g) => { return default(TMultiEdgeLabel); },   // AutoIdGeneration currently turned off!
                                                 MultiEdgeLabel.DEFAULT,

                                                 // Hyperedges
                                                 GraphDBOntology.Id().Suffix,
                                                 GraphDBOntology.RevId().Suffix,
                                                 GraphDBOntology.Label().Suffix,
                                                 GraphDBOntology.Description().Suffix,
                                                 (g) => { return default(THyperEdgeLabel); },   // AutoIdGeneration currently turned off!
                                                 HyperEdgeLabel.DEFAULT)

                                                 { };

            #endregion

            #region If 'continuous learning', then wire graph 'update' events

            if (ContinuousLearning)
            {

                #region Register [Vertex|Edge]Added events: PropertyGraph -> SchemaGraph

                Graph.OnVertexAddition.OnNotification +=
                    (g, v) => SchemaGraph.AddVertexIfNotExists(Id:    v.Label,
                                                               Label: VertexLabel.Vertex);

                Graph.OnEdgeAddition.OnNotification   +=
                    (g, e) => SchemaGraph.AddEdgeIfNotExists(Id:              e.Label,
                                                             OutVertex:       SchemaGraph.VertexById(e.OutVertex.Label).AsMutable(),
                                                             Label:           EdgeLabel.IsConnectedWith,
                                                             InVertex:        SchemaGraph.VertexById(e.InVertex.Label).AsMutable(),
                                                             EdgeInitializer: Edge => Edge.ZSetAdd(GraphSchemaHandling.AlternativeUsage, e.OutVertex.Label.ToString() + " -> " + e.InVertex.Label.ToString()),
                                                             AnywayDo:        Edge => Edge.ZSetAdd(GraphSchemaHandling.AlternativeUsage, e.OutVertex.Label.ToString() + " -> " + e.InVertex.Label.ToString()));

                #endregion

            }

            #endregion

            //ToDo: Attach a SchemaGraph to a PropertyGraph to _enforce a schema_...

            #region Add all current vertices and edges: PropertyGraph -> SchemaGraph

            Graph.Vertices().ForEach(v => SchemaGraph.AddVertexIfNotExists(Id:        v.Label,
                                                                           Label:     VertexLabel.Vertex));

            Graph.Edges().   ForEach(e => SchemaGraph.AddEdgeIfNotExists  (Id:              e.Label,
                                                                           OutVertex:       SchemaGraph.VertexById(e.OutVertex.Label).AsMutable(),
                                                                           Label:           EdgeLabel.IsConnectedWith,
                                                                           InVertex:        SchemaGraph.VertexById(e.InVertex. Label).AsMutable(),
                                                                           EdgeInitializer: Edge => Edge.ZSetAdd(GraphSchemaHandling.AlternativeUsage, e.OutVertex.Label.ToString() + " -> " + e.InVertex.Label.ToString()),
                                                                           AnywayDo:        Edge => Edge.ZSetAdd(GraphSchemaHandling.AlternativeUsage, e.OutVertex.Label.ToString() + " -> " + e.InVertex.Label.ToString())
                                                                          ));

            //Graph.Edges().   ForEach(e => SchemaGraph.AddHyperEdge(//e.Label,
            //                                                       HyperEdgeLabel.DEFAULT,
            //                                                       null,
            //                                                       SchemaGraph.VertexById(e.OutVertex.Label).AsMutable(),
            //                                                       SchemaGraph.VertexById(e.InVertex.Label).AsMutable()));


            // It would also be a good idea to analyse the usage of the vertex/edge properties!

            #endregion

            return SchemaGraph;

        }

        #endregion

    }

}
