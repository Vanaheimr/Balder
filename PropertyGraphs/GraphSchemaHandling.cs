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

using de.ahzf.Illias.Commons;
using de.ahzf.Blueprints.PropertyGraphs;

#endregion

namespace de.ahzf.Blueprints.Schema
{

    /// <summary>
    /// Graph schema handling
    /// </summary>
    public static class GraphSchemaHandling
    {

        public const String AlternativeUsage = "AlternativeUsage";

        #region GetSchemaGraph(this PropertyGraph, GraphId, Description = null, ContinuousLearning = true)

        /// <summary>
        /// Analyses the given property graph and returns a schema graph for the property graph.
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
        /// <param name="PropertyGraph">The property graph to extract the schema from.</param>
        /// <param name="GraphId">The schema graph identification.</param>
        /// <param name="Description">The optional description of the schema graph.</param>
        /// <param name="ContinuousLearning">If set to true, the schema graph will subsribe vertex/edge additions in order to continuously learn the graph schema.</param>
        public static IGenericPropertyGraph<String, Int64, String, String, Object,
                                            String, Int64, String, String, Object,
                                            String, Int64, String, String, Object,
                                            String, Int64, String, String, Object>

                          GetGraphSchema<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>(

                             this IGenericPropertyGraph<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> PropertyGraph,
                             String  GraphId,
                             String  Description        = null,
                             Boolean ContinuousLearning = true)

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

            var SchemaGraph = GraphFactory.CreateSchemaGraph(GraphId, Description);

            if (ContinuousLearning)
            {

                #region Register [Vertex|Edge]Added events: PropertyGraph -> SchemaGraph

                PropertyGraph.OnVertexAdded += (g, v) => SchemaGraph.AddVertexIfNotExists(v.Label.ToString(), "Vertex");

                PropertyGraph.OnEdgeAdded   += (g, e) => SchemaGraph.AddEdgeIfNotExists(e.Label.ToString(),
                                                                                        SchemaGraph.VertexById(e.OutVertex.Label.ToString()),
                                                                                        "Edge",
                                                                                        SchemaGraph.VertexById(e.InVertex.Label.ToString()),
                                                                                        Edge => Edge.SetAdd(GraphSchemaHandling.AlternativeUsage, e.OutVertex.Label.ToString() + " -> " + e.InVertex.Label.ToString()),
                                                                                        Edge => Edge.SetAdd(GraphSchemaHandling.AlternativeUsage, e.OutVertex.Label.ToString() + " -> " + e.InVertex.Label.ToString()));

                #endregion

            }

            #region Add all current vertices and edges: PropertyGraph -> SchemaGraph

            PropertyGraph.Vertices().ForEach(v => SchemaGraph.AddVertexIfNotExists(v.Label.ToString(), "Vertex"));

            PropertyGraph.Edges().   ForEach(e => SchemaGraph.AddEdgeIfNotExists(e.Label.ToString(),
                                                                                 SchemaGraph.VertexById(e.OutVertex.Label.ToString()),
                                                                                 "Edge", 
                                                                                 SchemaGraph.VertexById(e.InVertex. Label.ToString())));

            #endregion

            // It would also be a good idea to analyse the usage of the vertex/edge properties!

            return SchemaGraph;

        }

        #endregion

        //ToDo: Attach a SchemaGraph to a PropertyGraph to enforce a schema...

    }

}
