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
using System.Collections.Concurrent;

#endregion

namespace de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable
{

    /// <summary>
    /// A class-based in-memory implementation of a semantic property graph.
    /// </summary>
    public class SemanticPropertyGraph

                     : GenericPropertyGraph<// Vertex definition
                                            VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object, IDictionary<SemanticProperty, Object>,

                                            // Edge definition
                                            EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object, IDictionary<SemanticProperty, Object>,
                                                ICollection<                  IPropertyEdge     <VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                                                 EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                                                 MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                                                 HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>>,

                                            // MultiEdge definition
                                            MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object, IDictionary<SemanticProperty, Object>,
                                                IDictionary<SemanticProperty, IPropertyMultiEdge<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                                                 EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                                                 MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                                                 HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>>,

                                            // Hyperedge definition
                                            HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object, IDictionary<SemanticProperty, Object>,
                                                IDictionary<SemanticProperty, IPropertyHyperEdge<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                                                 EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                                                 MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                                                 HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>>>,

                                            ISemanticPropertyGraph

    {

        /// <summary>
        /// Creates a new semantic property graph.
        /// </summary>
        /// <param name="GraphId">The identification of this graph.</param>
        /// <param name="GraphInitializer">A delegate to initialize the newly created graph.</param>
        public SemanticPropertyGraph(VertexId GraphId,
                                     GraphInitializer<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                      EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                      MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                      HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object> GraphInitializer = null)

            : base (GraphId,
                    GraphDBOntology.Id(),
                    GraphDBOntology.RevId(),
                    () => new Dictionary<SemanticProperty, Object>(),

                    // Create a new Vertex
                    (Graph) => VertexId.NewVertexId,
                    (Graph, myVertexId, myVertexPropertyInitializer) =>
                        new PropertyVertex<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object, IDictionary<SemanticProperty, Object>,
                                           EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object, IDictionary<SemanticProperty, Object>, ICollection<IPropertyEdge<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                                                                                                                                 EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                                                                                                                                 MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                                                                                                                                 HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>>,

                                           MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object, IDictionary<SemanticProperty, Object>, IDictionary<SemanticProperty, IPropertyMultiEdge<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                                                                                                                                                        EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                                                                                                                                                        MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                                                                                                                                                        HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>>,

                                           HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object, IDictionary<SemanticProperty, Object>, IDictionary<SemanticProperty, IPropertyHyperEdge<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                                                                                                                                                        EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                                                                                                                                                        MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                                                                                                                                                        HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>>>

                            (Graph, myVertexId, GraphDBOntology.Id(), GraphDBOntology.RevId(),
                             () => new Dictionary<SemanticProperty, Object>(),
                             () => new HashSet<IPropertyEdge<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                             EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                             MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                             HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>>(),
                             () => new Dictionary<SemanticProperty, IPropertyHyperEdge<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                                       EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                                       MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                                       HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>>(),
                             myVertexPropertyInitializer
                            ),

                   
                   // Create a new Edge
                   (Graph) => EdgeId.NewEdgeId,
                   (Graph, myOutVertex, myInVertex, myEdgeId, myLabel, myEdgePropertyInitializer) =>
                        new PropertyEdge<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object, IDictionary<SemanticProperty, Object>,
                                         EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object, IDictionary<SemanticProperty, Object>,
                                         MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object, IDictionary<SemanticProperty, Object>,
                                         HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object, IDictionary<SemanticProperty, Object>>

                            (Graph, myOutVertex, myInVertex, myEdgeId, myLabel, GraphDBOntology.Id(), GraphDBOntology.RevId(),
                             () => new Dictionary<SemanticProperty, Object>(),
                             myEdgePropertyInitializer
                            ),

                   // Create a new MultiEdge
                   (Graph) => MultiEdgeId.NewMultiEdgeId,
                   (Graph, myEdges, myMultiEdgeId, myLabel, myMultiEdgePropertyInitializer) =>
                       new PropertyMultiEdge<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object, IDictionary<SemanticProperty, Object>,
                                             EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object, IDictionary<SemanticProperty, Object>,
                                             MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object, IDictionary<SemanticProperty, Object>,
                                             HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object, IDictionary<SemanticProperty, Object>,

                                             ICollection<IPropertyVertex<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                         EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                         MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                         HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>>>

                            (Graph, myEdges, myMultiEdgeId, myLabel, GraphDBOntology.Id(), GraphDBOntology.RevId(),
                             () => new Dictionary<SemanticProperty, Object>(),
                             () => new HashSet<IPropertyVertex<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                               EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                               MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                               HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>>(),
                             myMultiEdgePropertyInitializer
                            ),

                   // Create a new HyperEdge
                   (Graph) => HyperEdgeId.NewHyperEdgeId,
                   (Graph, myEdges, myHyperEdgeId, myLabel, myHyperEdgePropertyInitializer) =>
                       new PropertyHyperEdge<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object, IDictionary<SemanticProperty, Object>,
                                             EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object, IDictionary<SemanticProperty, Object>,
                                             MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object, IDictionary<SemanticProperty, Object>,
                                             HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object, IDictionary<SemanticProperty, Object>,

                                             ICollection<IPropertyEdge<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                       EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                       MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                       HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>>>

                            (Graph, myEdges, myHyperEdgeId, myLabel, GraphDBOntology.Id(), GraphDBOntology.RevId(),
                             () => new Dictionary<SemanticProperty, Object>(),
                             () => new HashSet<IPropertyEdge<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                             EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                             MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                             HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>>(),
                             myHyperEdgePropertyInitializer
                            ),

#if SILVERLIGHT

                   // The vertices collection
                   new Dictionary<VertexId,    IPropertyVertex   <VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                  EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                  MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                  HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>>(),

                   // The edges collection
                   new Dictionary<EdgeId,      IPropertyEdge     <VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                  EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                  MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                  HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>>(),

                   // The multiedges collection
                   new Dictionary<MultiEdgeId, IPropertyMultiEdge<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                  EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                  MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                  HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>>(),

                   // The hyperedges collection
                   new Dictionary<HyperEdgeId, IPropertyHyperEdge<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                  EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                  MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                  HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>>(),

#else

                   // The vertices collection
                   new ConcurrentDictionary<VertexId,    IPropertyVertex   <VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                            EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                            MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                            HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>>(),

                   // The edges collection
                   new ConcurrentDictionary<EdgeId,      IPropertyEdge     <VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                            EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                            MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                            HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>>(),

                   // The multiedges collection
                   new ConcurrentDictionary<MultiEdgeId, IPropertyMultiEdge<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                            EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                            MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                            HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>>(),

                   // The hyperedges collection
                   new ConcurrentDictionary<HyperEdgeId, IPropertyHyperEdge<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                            EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                            MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,
                                                                            HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>>(),

#endif

                   GraphInitializer)


        { }

    }

}
