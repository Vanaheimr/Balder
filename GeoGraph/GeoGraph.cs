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

using de.ahzf.blueprints.GenericGraph;

#endregion

namespace de.ahzf.blueprints.GeoGraph
{

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    public class GeoGraph : IGenericGraph<// Vertex definition
                                          VertexId, RevisionId,
                                          GeoCoordinate,
                                          IGeoVertex,
                                         
                                          // Edge definition
                                          EdgeId, RevisionId,
                                          Distance,
                                          IGeoEdge,
                                         
                                          // Hyperedge definition
                                          HyperEdgeId, RevisionId,
                                          Distance,
                                          IGenericHyperEdge<VertexId,    RevisionId, GeoCoordinate,
                                                            EdgeId,      RevisionId, Distance,
                                                            HyperEdgeId, RevisionId, Distance>>
    {


        public IGeoVertex AddVertex(VertexId VertexId = default(VertexId), Action<GeoCoordinate> VertexInitializer = null)
        {
            throw new NotImplementedException();
        }

        public IGeoVertex AddVertex(IGeoVertex myVertexId)
        {
            throw new NotImplementedException();
        }

        public IGeoVertex GetVertex(VertexId myVertexId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGeoVertex> Vertices
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IGeoVertex> GetVertices(params VertexId[] myVertexIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGeoVertex> GetVertices(Func<IGeoVertex, bool> myVertexFilter = null)
        {
            throw new NotImplementedException();
        }

        public void RemoveVertex(IGeoVertex myIVertex)
        {
            throw new NotImplementedException();
        }

        public IGeoEdge AddEdge(IGeoVertex myOutVertex, IGeoVertex myInVertex, EdgeId EdgeId = default(EdgeId), string Label = null, Action<Distance> EdgeInitializer = null)
        {
            throw new NotImplementedException();
        }

        public IGeoEdge GetEdge(EdgeId myEdgeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGeoEdge> Edges
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IGeoEdge> GetEdges(params EdgeId[] myEdgeIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGeoEdge> GetEdges(Func<IGeoEdge, bool> myEdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        public void RemoveEdge(IGeoEdge myIEdge)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }

    }

}
