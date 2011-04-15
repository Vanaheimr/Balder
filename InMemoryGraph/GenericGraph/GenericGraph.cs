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

#endregion

namespace de.ahzf.blueprints.GenericGraph.InMemory
{

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    public class GenericGraph<TIdVertex,    TRevisionIdVertex,    TDataVertex,    TVertex,    
                              TIdEdge,      TRevisionIdEdge,      TDataEdge,      TEdge,      
                              TIdHyperEdge, TRevisionIdHyperEdge, TDataHyperEdge, THyperEdge>

                              : IGenericGraph<TIdVertex,    TRevisionIdVertex,    TDataVertex,    TVertex,    
                                              TIdEdge,      TRevisionIdEdge,      TDataEdge,      TEdge,      
                                              TIdHyperEdge, TRevisionIdHyperEdge, TDataHyperEdge, THyperEdge>

        where TVertex              : IGenericVertex   <TIdVertex,    TRevisionIdVertex,    TDataVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TDataEdge, 
                                                       TIdHyperEdge, TRevisionIdHyperEdge, TDataHyperEdge>

        where TEdge                : IGenericEdge     <TIdVertex,    TRevisionIdVertex,    TDataVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TDataEdge, 
                                                       TIdHyperEdge, TRevisionIdHyperEdge, TDataHyperEdge>
        
        where THyperEdge           : IGenericHyperEdge<TIdVertex,    TRevisionIdVertex,    TDataVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TDataEdge, 
                                                       TIdHyperEdge, TRevisionIdHyperEdge, TDataHyperEdge>

        where TIdVertex            : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable
        where TIdEdge              : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable
        where TIdHyperEdge         : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable

        where TRevisionIdVertex    : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable
        where TRevisionIdEdge      : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable
        where TRevisionIdHyperEdge : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable

    {

        #region Data

        // Make it more generic??!
        private readonly IDictionary<TIdVertex,    IGenericVertex   <TIdVertex,    TRevisionIdVertex,    TDataVertex,
                                                                     TIdEdge,      TRevisionIdEdge,      TDataEdge,
                                                                     TIdHyperEdge, TRevisionIdHyperEdge, TDataHyperEdge>> _Vertices;

        private readonly IDictionary<TIdEdge,      IGenericEdge     <TIdVertex,    TRevisionIdVertex,    TDataVertex,
                                                                     TIdEdge,      TRevisionIdEdge,      TDataEdge,
                                                                     TIdHyperEdge, TRevisionIdHyperEdge, TDataHyperEdge>> _Edges;

        private readonly IDictionary<TIdHyperEdge, IGenericHyperEdge<TIdVertex,    TRevisionIdVertex,    TDataVertex,
                                                                     TIdEdge,      TRevisionIdEdge,      TDataEdge,
                                                                     TIdHyperEdge, TRevisionIdHyperEdge, TDataHyperEdge>> _HyperEdges;

        private readonly Func<TIdVertex,
                              Action<TDataVertex>,
                              IGenericVertex   <TIdVertex,    TRevisionIdVertex,    TDataVertex,
                                                TIdEdge,      TRevisionIdEdge,      TDataEdge,
                                                TIdHyperEdge, TRevisionIdHyperEdge, TDataHyperEdge>>

                         _VertexCreatorDelegate;

        private readonly Func<IGenericVertex   <TIdVertex,    TRevisionIdVertex,    TDataVertex,
                                                TIdEdge,      TRevisionIdEdge,      TDataEdge,
                                                TIdHyperEdge, TRevisionIdHyperEdge, TDataHyperEdge>,
                              IGenericVertex   <TIdVertex,    TRevisionIdVertex,    TDataVertex,
                                                TIdEdge,      TRevisionIdEdge,      TDataEdge,
                                                TIdHyperEdge, TRevisionIdHyperEdge, TDataHyperEdge>,
                              TIdEdge,
                              String,
                              Action<TDataEdge>,
                              IGenericEdge     <TIdVertex,    TRevisionIdVertex,    TDataVertex,
                                                TIdEdge,      TRevisionIdEdge,      TDataEdge,
                                                TIdHyperEdge, TRevisionIdHyperEdge, TDataHyperEdge>>

                         _EdgeCreatorDelegate;

        private readonly Func<IEnumerable<IGenericEdge<TIdVertex,    TRevisionIdVertex,    TDataVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TDataEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, TDataHyperEdge>>,
                              TIdHyperEdge,
                              String,
                              Action<TDataHyperEdge>,
                              IGenericHyperEdge<TIdVertex,    TRevisionIdVertex,    TDataVertex,
                                                TIdEdge,      TRevisionIdEdge,      TDataEdge,
                                                TIdHyperEdge, TRevisionIdHyperEdge, TDataHyperEdge>>

                         _HyperEdgeCreatorDelegate;

        //protected Map<String, TinkerIndex>          indices     = new HashMap<String, TinkerIndex>();
        //protected Map<String, TinkerAutomaticIndex> autoIndices = new HashMap<String, TinkerAutomaticIndex>();

        #endregion


        public TVertex AddVertex(TIdVertex VertexId = default(TIdVertex), Action<TDataVertex> VertexInitializer = null)
        {
            throw new NotImplementedException();
        }

        public TVertex AddVertex(TVertex myVertexId)
        {
            throw new NotImplementedException();
        }

        public TVertex GetVertex(TIdVertex myVertexId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TVertex> Vertices
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<TVertex> GetVertices(params TIdVertex[] myVertexIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TVertex> GetVertices(Func<TVertex, bool> myVertexFilter = null)
        {
            throw new NotImplementedException();
        }

        public void RemoveVertex(TVertex myIVertex)
        {
            throw new NotImplementedException();
        }






        public TEdge AddEdge(TVertex myOutVertex, TVertex myInVertex, TIdEdge EdgeId = default(TIdEdge), string Label = null, Action<TDataEdge> EdgeInitializer = null)
        {
            throw new NotImplementedException();
        }

        public TEdge GetEdge(TIdEdge myEdgeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEdge> Edges
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<TEdge> GetEdges(params TIdEdge[] myEdgeIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEdge> GetEdges(Func<TEdge, bool> myEdgeFilter = null)
        {
            throw new NotImplementedException();
        }

        public void RemoveEdge(TEdge myIEdge)
        {
            throw new NotImplementedException();
        }




        #region Clear()

        /// <summary>
        /// Remove all the edges and vertices from the graph.
        /// </summary>
        public void Clear()
        {
            _Vertices.Clear();
            _Edges.Clear();
            _HyperEdges.Clear();
        }

        #endregion

        #region Shutdown()

        /// <summary>
        /// Shutdown and close the graph.
        /// </summary>
        public void Shutdown()
        {
            Clear();
        }

        #endregion

    }

}
