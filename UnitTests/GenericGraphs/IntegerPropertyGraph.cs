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
using de.ahzf.blueprints.InMemory.PropertyGraph.Generic;

#endregion

namespace de.ahzf.blueprints.InMemory.PropertyGraph
{

    /// <summary>
    /// An in-memory implementation of a property graph.
    /// </summary>
    public class IntegerPropertyGraph : IGenericGraph<// Vertex definition
                                                      IGenericVertex<UInt64, UInt32, IProperties<UInt16, Int32>,
                                                                     UInt64, UInt32, IProperties<UInt16, Int32>,
                                                                     UInt64, UInt32, IProperties<UInt16, Int32>>,                                        
                                                      UInt64,
                                                      UInt32,
                                                      IProperties<UInt16, Int32>,
                                                      IGenericVertex<UInt64, UInt32, IProperties<UInt16, Int32>,
                                                                     UInt64, UInt32, IProperties<UInt16, Int32>,
                                                                     UInt64, UInt32, IProperties<UInt16, Int32>>,

                                                      // Edge definition
                                                      IGenericEdge<UInt64, UInt32, IProperties<UInt16, Int32>,
                                                                   UInt64, UInt32, IProperties<UInt16, Int32>,
                                                                   UInt64, UInt32, IProperties<UInt16, Int32>>,
                                                      UInt64,
                                                      UInt32,
                                                      IProperties<UInt16, Int32>,
                                                      IGenericEdge<UInt64, UInt32, IProperties<UInt16, Int32>,
                                                                     UInt64, UInt32, IProperties<UInt16, Int32>,
                                                                     UInt64, UInt32, IProperties<UInt16, Int32>>,

                                                      // Hyperedge definition
                                                      IGenericHyperEdge<UInt64, UInt32, IProperties<UInt16, Int32>,
                                                                        UInt64, UInt32, IProperties<UInt16, Int32>,
                                                                        UInt64, UInt32, IProperties<UInt16, Int32>>,
                                                      UInt64,
                                                      UInt32,
                                                      IProperties<UInt16, Int32>,
                                                      IGenericHyperEdge<UInt64, UInt32, IProperties<UInt16, Int32>,
                                                                     UInt64, UInt32, IProperties<UInt16, Int32>,
                                                                     UInt64, UInt32, IProperties<UInt16, Int32>>,

                                                      // Rest...
                                                      Object>

    {

        #region Constructor(s)

        #region IntegerPropertyGraph()

        /// <summary>
        /// Created a new in-memory property graph.
        /// </summary>
        public IntegerPropertyGraph()
        //    : base ((myVertexId, myVertexPropertyInitializer) =>
        //                new PropertyVertex<VertexId,    RevisionId, String, Object, IDictionary<String, Object>,
        //                                   EdgeId,      RevisionId, String, Object, IDictionary<String, Object>,
        //                                   HyperEdgeId, RevisionId, String, Object, IDictionary<String, Object>,
        //                                   ICollection<IGenericEdge<VertexId,    RevisionId, IProperties<String, Object>,
        //                                                            EdgeId,      RevisionId, IProperties<String, Object>,
        //                                                            HyperEdgeId, RevisionId, IProperties<String, Object>>>>    
        //                    (myVertexId, "Id", "RevId",
        //                     () => new Dictionary<String, Object>(),
        //                     () => new HashSet<IGenericEdge<VertexId,    RevisionId, IProperties<String, Object>,
        //                                                    EdgeId,      RevisionId, IProperties<String, Object>,
        //                                                    HyperEdgeId, RevisionId, IProperties<String, Object>>>(),
        //                     myVertexPropertyInitializer
        //                    ),

                   
        //           "Id", "RevId", () => new Dictionary<String, Object>(),
        //           "Id", "RevId", () => new Dictionary<String, Object>())

        { }

        #endregion

        #endregion




        public IGenericVertex<ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>> AddVertex(ulong VertexId = default(UInt64), Action<IProperties<ushort, int>> VertexInitializer = null)
        {
            throw new NotImplementedException();
        }

        public IGenericVertex<ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>> AddVertex(IGenericVertex<ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>> myVertexId)
        {
            throw new NotImplementedException();
        }

        public IGenericVertex<ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>> GetVertex(ulong myVertexId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGenericVertex<ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>>> GetVertices(params ulong[] myVertexIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGenericVertex<ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>>> GetVertices(Func<IGenericVertex<ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>>, bool> myVertexFilter = null)
        {
            throw new NotImplementedException();
        }

        public void RemoveVertex(IGenericVertex<ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>> myIVertex)
        {
            throw new NotImplementedException();
        }

        public IGenericEdge<ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>> AddEdge(IGenericVertex<ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>> myOutVertex, IGenericVertex<ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>> myInVertex, ulong EdgeId = default(UInt64), string Label = null, Action<IProperties<ushort, int>> EdgeInitializer = null)
        {
            throw new NotImplementedException();
        }

        public IGenericEdge<ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>> GetEdge(ulong myEdgeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGenericEdge<ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>>> GetEdges(params ulong[] myEdgeIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGenericEdge<ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>>> GetEdges(Func<IGenericEdge<ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>>, bool> myEdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        public void RemoveEdge(IGenericEdge<ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>, ulong, uint, IProperties<ushort, int>> myIEdge)
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
