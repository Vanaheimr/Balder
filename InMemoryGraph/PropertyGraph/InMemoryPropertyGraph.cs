/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET
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
                                                                      //IPropertyVertex<VertexId,    RevisionId, String, Object, IDictionary<String, Object>,
                                                                      //                EdgeId,      RevisionId, String, Object, IDictionary<String, Object>,
                                                                      //                HyperEdgeId, RevisionId, String, Object, IDictionary<String, Object>>,
                                                                      IGenericVertex<VertexId,    RevisionId, IProperties<String, Object>,
                                                                                     EdgeId,      RevisionId, IProperties<String, Object>,
                                                                                     HyperEdgeId, RevisionId, IProperties<String, Object>>,
                                                                      ICollection<IGenericEdge<VertexId,    RevisionId, IProperties<String, Object>,
                                                                                               EdgeId,      RevisionId, IProperties<String, Object>,
                                                                                               HyperEdgeId, RevisionId, IProperties<String, Object>>>,

                                                                      // Edge definition
                                                                      EdgeId,      RevisionId, String, Object, IDictionary<String, Object>,
                                                                      IGenericEdge<VertexId,    RevisionId, IProperties<String, Object>,
                                                                                   EdgeId,      RevisionId, IProperties<String, Object>,
                                                                                   HyperEdgeId, RevisionId, IProperties<String, Object>>,

                                                                      // Hyperedge definition
                                                                      HyperEdgeId, RevisionId, String, Object, IDictionary<String, Object>,
                                                                      IGenericHyperEdge<VertexId,    RevisionId, IProperties<String, Object>,
                                                                                        EdgeId,      RevisionId, IProperties<String, Object>,
                                                                                        HyperEdgeId, RevisionId, IProperties<String, Object>>,

                                                                      Object>
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
                                           ICollection<IGenericEdge<VertexId,    RevisionId, IProperties<String, Object>,
                                                                    EdgeId,      RevisionId, IProperties<String, Object>,
                                                                    HyperEdgeId, RevisionId, IProperties<String, Object>>>>
                            (myVertexId, _VertexIdKey, _VertexRevisionIdKey,
                             () => new Dictionary<String, Object>(),
                             () => new HashSet<IGenericEdge<VertexId,    RevisionId, IProperties<String, Object>,
                                                            EdgeId,      RevisionId, IProperties<String, Object>,
                                                            HyperEdgeId, RevisionId, IProperties<String, Object>>>(),
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
                                             ICollection<IGenericVertex<VertexId,    RevisionId, IProperties<String, Object>,
                                                                        EdgeId,      RevisionId, IProperties<String, Object>,
                                                                        HyperEdgeId, RevisionId, IProperties<String, Object>>>>
                            (myOutVertex, myInVertices, myHyperEdgeId, myLabel, _HyperEdgeIdKey, _HyperEdgeRevisionIdKey,
                             () => new Dictionary<String, Object>(),
                             () => new HashSet<IGenericVertex<VertexId,    RevisionId, IProperties<String, Object>,
                                                              EdgeId,      RevisionId, IProperties<String, Object>,
                                                              HyperEdgeId, RevisionId, IProperties<String, Object>>>(),
                             myHyperEdgePropertyInitializer
                            ),

                   // The vertices collection
                   new ConcurrentDictionary<VertexId,    IGenericVertex   <VertexId,    RevisionId, IProperties<String, Object>,
                                                                           EdgeId,      RevisionId, IProperties<String, Object>,
                                                                           HyperEdgeId, RevisionId, IProperties<String, Object>>>(),

                   // The edges collection
                   new ConcurrentDictionary<EdgeId,      IGenericEdge     <VertexId,    RevisionId, IProperties<String, Object>,
                                                                           EdgeId,      RevisionId, IProperties<String, Object>,
                                                                           HyperEdgeId, RevisionId, IProperties<String, Object>>>(),

                   // The hyperedges collection
                   new ConcurrentDictionary<HyperEdgeId, IGenericHyperEdge<VertexId,    RevisionId, IProperties<String, Object>,
                                                                           EdgeId,      RevisionId, IProperties<String, Object>,
                                                                           HyperEdgeId, RevisionId, IProperties<String, Object>>>()

              )

        { }

        #endregion

        #endregion

    }

    #endregion

}
