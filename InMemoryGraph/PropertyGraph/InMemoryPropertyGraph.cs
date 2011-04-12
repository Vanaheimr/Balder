/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET <http://www.github.com/ahzf/blueprints.NET>
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

using de.ahzf.blueprints.Datastructures;
using de.ahzf.blueprints.InMemory.PropertyGraph.Generic;

#endregion

namespace de.ahzf.blueprints.InMemory.PropertyGraph
{

    #region InMemoryPropertyGraph

    /// <summary>
    /// An in-memory implementation of a property graph.
    /// </summary>
    public class InMemoryPropertyGraph : InMemoryGenericPropertyGraph<// Vertex definition
                                                                      VertexId,    RevisionId, String, Object, IDictionary<String, Object>,
                                                                      ICollection<IPropertyEdge<VertexId,    RevisionId, String, Object,
                                                                                                EdgeId,      RevisionId, String, Object,
                                                                                                HyperEdgeId, RevisionId, String, Object>>,

                                                                      // Edge definition
                                                                      EdgeId,      RevisionId, String, Object, IDictionary<String, Object>,

                                                                      // Hyperedge definition
                                                                      HyperEdgeId, RevisionId, String, Object, IDictionary<String, Object>>,
                                        IPropertyGraph
    {

        #region Data

        private const String _VertexIdKey            = "Id";
        private const String _EdgeIdKey              = "Id";
        private const String _HyperEdgeIdKey         = "Id";

        private const String _VertexRevisionIdKey    = "RevId";
        private const String _EdgeRevisionIdKey      = "RevId";
        private const String _HyperEdgeRevisionIdKey = "RevId";

        #endregion

        #region Constructor(s)

        #region InMemoryPropertyGraph()

        /// <summary>
        /// Created a new in-memory property graph.
        /// </summary>
        public InMemoryPropertyGraph()
            : base (// Create a new Vertex
                    (myVertexId, myVertexPropertyInitializer) =>
                        new PropertyVertex<VertexId,    RevisionId, String, Object, IDictionary<String, Object>,
                                           EdgeId,      RevisionId, String, Object, IDictionary<String, Object>,
                                           HyperEdgeId, RevisionId, String, Object, IDictionary<String, Object>,
                                           ICollection<IPropertyEdge<VertexId,    RevisionId, String, Object,
                                                                     EdgeId,      RevisionId, String, Object,
                                                                     HyperEdgeId, RevisionId, String, Object>>>
                            (myVertexId, _VertexIdKey, _VertexRevisionIdKey,
                             () => new Dictionary<String, Object>(),
                             () => new HashSet<IPropertyEdge<VertexId,    RevisionId, String, Object,
                                                             EdgeId,      RevisionId, String, Object,
                                                             HyperEdgeId, RevisionId, String, Object>>(),
                             myVertexPropertyInitializer
                            ),

                   
                   // Create a new Edge
                   (myOutVertex, myInVertex, myEdgeId, myLabel, myEdgePropertyInitializer) =>
                        new PropertyEdge<VertexId,    RevisionId, String, Object, IDictionary<String, Object>,
                                         EdgeId,      RevisionId, String, Object, IDictionary<String, Object>,
                                         HyperEdgeId, RevisionId, String, Object, IDictionary<String, Object>>
                            (myOutVertex, myInVertex, myEdgeId, myLabel, _EdgeIdKey, _EdgeRevisionIdKey,
                             () => new Dictionary<String, Object>(),
                             myEdgePropertyInitializer
                            ),

                   // Create a new HyperEdge
                   (myOutVertex, myInVertices, myHyperEdgeId, myLabel, myHyperEdgePropertyInitializer) =>
                       new PropertyHyperEdge<VertexId,    RevisionId, String, Object, IDictionary<String, Object>,
                                             EdgeId,      RevisionId, String, Object, IDictionary<String, Object>,
                                             HyperEdgeId, RevisionId, String, Object, IDictionary<String, Object>,
                                             ICollection<IPropertyVertex<VertexId,    RevisionId, String, Object,
                                                                         EdgeId,      RevisionId, String, Object,
                                                                         HyperEdgeId, RevisionId, String, Object>>>
                            (myOutVertex, myInVertices, myHyperEdgeId, myLabel, _HyperEdgeIdKey, _HyperEdgeRevisionIdKey,
                             () => new Dictionary<String, Object>(),
                             () => new HashSet<IPropertyVertex<VertexId,    RevisionId, String, Object,
                                                               EdgeId,      RevisionId, String, Object,
                                                               HyperEdgeId, RevisionId, String, Object>>(),
                             myHyperEdgePropertyInitializer
                            ),

                   // The vertices collection
                   new ConcurrentDictionary<VertexId,    IPropertyVertex   <VertexId,    RevisionId, String, Object,
                                                                            EdgeId,      RevisionId, String, Object,
                                                                            HyperEdgeId, RevisionId, String, Object>>(),

                   // The edges collection
                   new ConcurrentDictionary<EdgeId,      IPropertyEdge     <VertexId,    RevisionId, String, Object,
                                                                            EdgeId,      RevisionId, String, Object,
                                                                            HyperEdgeId, RevisionId, String, Object>>(),

                   // The hyperedges collection
                   new ConcurrentDictionary<HyperEdgeId, IPropertyHyperEdge<VertexId,    RevisionId, String, Object,
                                                                            EdgeId,      RevisionId, String, Object,
                                                                            HyperEdgeId, RevisionId, String, Object>>()
              )

        { }

        #endregion

        #endregion

    }

    #endregion

}
