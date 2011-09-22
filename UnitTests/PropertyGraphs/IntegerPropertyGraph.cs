///*
// * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
// * This file is part of Blueprints.NET <http://www.github.com/ahzf/Blueprints.NET>
// *
// * Licensed under the Apache License, Version 2.0 (the "License");
// * you may not use this file except in compliance with the License.
// * You may obtain a copy of the License at
// *
// *     http://www.apache.org/licenses/LICENSE-2.0
// *
// * Unless required by applicable law or agreed to in writing, software
// * distributed under the License is distributed on an "AS IS" BASIS,
// * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// * See the License for the specific language governing permissions and
// * limitations under the License.
// */

//#region Usings

//using System;
//using System.Collections.Generic;

//using de.ahzf.Blueprints.PropertyGraphs;

//#endregion

//namespace de.ahzf.Blueprints.UnitTests.PropertyGraph
//{

//    /// <summary>
//    /// An in-memory implementation of a property graph.
//    /// </summary>
//    public class IntegerPropertyGraph : IPropertyGraph<// Vertex definition
//                                                      UInt64,
//                                                      UInt64,
//                                                      UInt16, UInt64,

//                                                      // Edge definition
//                                                      UInt64,
//                                                      UInt64,
//                                                      UInt16, UInt64,

//                                                      // Hyperedge definition
//                                                      UInt64,
//                                                      UInt64,
//                                                      UInt16, UInt64>

//    {

//        #region Constructor(s)

//        #region IntegerPropertyGraph()

//        /// <summary>
//        /// Created a new in-memory property graph.
//        /// </summary>
//        public IntegerPropertyGraph()
//        //    : base ((myVertexId, myVertexPropertyInitializer) =>
//        //                new PropertyVertex<VertexId,    RevisionId, String, Object, IDictionary<String, Object>,
//        //                                   EdgeId,      RevisionId, String, Object, IDictionary<String, Object>,
//        //                                   HyperEdgeId, RevisionId, String, Object, IDictionary<String, Object>,
//        //                                   ICollection<IGenericEdge<VertexId,    RevisionId, IProperties<String, Object>,
//        //                                                            EdgeId,      RevisionId, IProperties<String, Object>,
//        //                                                            HyperEdgeId, RevisionId, IProperties<String, Object>>>>    
//        //                    (myVertexId, "Id", "RevId",
//        //                     () => new Dictionary<String, Object>(),
//        //                     () => new HashSet<IGenericEdge<VertexId,    RevisionId, IProperties<String, Object>,
//        //                                                    EdgeId,      RevisionId, IProperties<String, Object>,
//        //                                                    HyperEdgeId, RevisionId, IProperties<String, Object>>>(),
//        //                     myVertexPropertyInitializer
//        //                    ),

                   
//        //           "Id", "RevId", () => new Dictionary<String, Object>(),
//        //           "Id", "RevId", () => new Dictionary<String, Object>())

//        { }

//        #endregion

//        #endregion


//        public IPropertyVertex<ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong> AddVertex(ulong VertexId = default(UInt64), Action<IProperties<ushort, ulong>> VertexInitializer = null)
//        {
//            throw new NotImplementedException();
//        }

//        public IPropertyVertex<ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong> AddVertex(IPropertyVertex<ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong> myVertexId)
//        {
//            throw new NotImplementedException();
//        }

//        public IPropertyVertex<ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong> GetVertex(ulong myVertexId)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<IPropertyVertex<ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong>> Vertices
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public IEnumerable<IPropertyVertex<ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong>> GetVertices(params ulong[] myVertexIds)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<IPropertyVertex<ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong>> GetVertices(Func<IPropertyVertex<ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong>, bool> myVertexFilter = null)
//        {
//            throw new NotImplementedException();
//        }

//        public void RemoveVertex(IPropertyVertex<ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong> myIVertex)
//        {
//            throw new NotImplementedException();
//        }

//        public IPropertyEdge<ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong> AddEdge(IPropertyVertex<ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong> myOutVertex, IPropertyVertex<ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong> myInVertex, ulong EdgeId = default(UInt64), string Label = null, Action<IProperties<ushort, ulong>> EdgeInitializer = null)
//        {
//            throw new NotImplementedException();
//        }

//        public IPropertyEdge<ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong> GetEdge(ulong myEdgeId)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<IPropertyEdge<ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong>> Edges
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public IEnumerable<IPropertyEdge<ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong>> GetEdges(params ulong[] myEdgeIds)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<IPropertyEdge<ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong>> GetEdges(Func<IPropertyEdge<ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong>, bool> myEdgeFilter = null)
//        {
//            throw new NotImplementedException();
//        }

//        public void RemoveEdge(IPropertyEdge<ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong, ulong, ulong, ushort, ulong> myIEdge)
//        {
//            throw new NotImplementedException();
//        }

//    }

//}
