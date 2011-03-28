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

using de.ahzf.blueprints.Datastructures;

#endregion

namespace de.ahzf.blueprints.InMemory.PropertyGraph.Generic
{

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    public class PropertyVertex : PropertyVertex<VertexId,    RevisionId, String, Object, IDictionary<String, Object>,
                                                 EdgeId,      RevisionId, String, Object, IDictionary<String, Object>,
                                                 HyperEdgeId, RevisionId, String, Object, IDictionary<String, Object>,
                                                 ICollection<IGenericEdge<VertexId,    RevisionId, IProperties<String, Object, IDictionary<String, Object>>,
                                                                          EdgeId,      RevisionId, IProperties<String, Object, IDictionary<String, Object>>,
                                                                          HyperEdgeId, RevisionId, IProperties<String, Object, IDictionary<String, Object>>>>>

    {

        #region Constructor(s)

        #region PropertyVertex(myIGraph, myVertexId, myVertexInitializer = null)

        /// <summary>
        /// Creates a new property vertex.
        /// </summary>
        /// <param name="myIGraph">The associated graph.</param>
        /// <param name="myVertexId">The identification of this vertex.</param>
        /// <param name="myVertexInitializer">A delegate to initialize the newly created vertex.</param>
        public PropertyVertex(VertexId myVertexId,
                              Action<IPropertyVertex<VertexId,    RevisionId, String, Object, IDictionary<String, Object>,
                                                     EdgeId,      RevisionId, String, Object, IDictionary<String, Object>,
                                                     HyperEdgeId, RevisionId, String, Object, IDictionary<String, Object>>>
                                                     myVertexInitializer = null)
            : base(myVertexId,
                   "Id", "RevisionId",
                   () => new Dictionary<String, Object>(),
                   () => new HashSet<IGenericEdge<VertexId,    RevisionId, IProperties<String, Object, IDictionary<String, Object>>,
                                                  EdgeId,      RevisionId, IProperties<String, Object, IDictionary<String, Object>>,
                                                  HyperEdgeId, RevisionId, IProperties<String, Object, IDictionary<String, Object>>>>(),
                   myVertexInitializer)

        { }

        #endregion

        #endregion
    
    }

}
