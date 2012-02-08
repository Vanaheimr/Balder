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

using de.ahzf.Illias.Commons;

#endregion

namespace de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable
{

    /// <summary>
    /// A class-based in-memory implementation of a semantic property graph.
    /// </summary>
    public class SemanticPropertyGraph

                     : GenericPropertyGraph<VertexId,    RevisionId, SemanticProperty, SemanticProperty, Object,   // Vertex definition
                                             EdgeId,      RevisionId, SemanticProperty, SemanticProperty, Object,   // Edge definition
                                             MultiEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object,   // MultiEdge definition
                                             HyperEdgeId, RevisionId, SemanticProperty, SemanticProperty, Object>,  // Hyperedge definition
         
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

            : base (GraphDBOntology.Id(),
                    GraphDBOntology.RevId(),
                    GraphId,


                    // Create a new vertex identification
                    GraphDBOntology.Id(),
                    GraphDBOntology.RevId(),
                    (Graph) => VertexId.NewVertexId,

                    // Create a new Edge
                    GraphDBOntology.Id(),
                    GraphDBOntology.RevId(),
                    (Graph) => EdgeId.NewEdgeId,
                    
                    // Create a new MultiEdge
                    GraphDBOntology.Id(),
                    GraphDBOntology.RevId(),
                    (Graph) => MultiEdgeId.NewMultiEdgeId,

                    // Create a new HyperEdge
                    GraphDBOntology.Id(),
                    GraphDBOntology.RevId(),
                    (Graph) => HyperEdgeId.NewHyperEdgeId,

                   GraphInitializer)

        { }

    }

}
