/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
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
using System.Linq;
using System.Collections.Generic;

using de.ahzf.Illias.Commons;
using de.ahzf.Vanaheimr.Blueprints;

#endregion

namespace de.ahzf.Vanaheimr.Blueprints.TraversalGraphs
{

    #region MatrixArrayGraph

    public class MatrixArrayGraph
    {

        #region Data

        protected readonly UInt64[,] _MatrixGraph;

        protected readonly IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                 UInt64, Int64, String, String, Object,
                                                 UInt64, Int64, String, String, Object,
                                                 UInt64, Int64, String, String, Object> PropertyGraph;

        #endregion

        #region MatrixArrayGraph(PropertyGraph, X, Y)

        public MatrixArrayGraph(IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object> PropertyGraph,
                                UInt64 X, UInt64 Y)
        {

            this.PropertyGraph = PropertyGraph;
            this._MatrixGraph  = new UInt64[X, Y];

            PropertyGraph.Vertices().ForEach(v =>
            {
                _MatrixGraph[v.Id - 1, 0] = (from e in v.OutEdges() select e.Id).ToArray().First();
            });

        }

        #endregion


        //public void ForEachVertex(Action<UInt64[]> Action)
        //{
        //    //_ListGraph.ForEach(vid => { });
        //    for (var i = 0UL; i < this.Graph.NumberOfVertices(); i++)
        //        Action(_ListGraph[i]);

        //}

    }

    #endregion

    #region MatrixListGraph

    public class MatrixListGraph
    {

        #region Data

        protected readonly List<List<UInt64>> _MatrixListGraph;

        protected readonly IGenericPropertyGraph      <UInt64, Int64, String, String, Object,
                                                       UInt64, Int64, String, String, Object,
                                                       UInt64, Int64, String, String, Object,
                                                       UInt64, Int64, String, String, Object> PropertyGraph;

        protected readonly Func<IReadOnlyGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object,
                                                               UInt64, Int64, String, String, Object>, IEnumerable<UInt64>> Func;

        protected readonly VertexFilter               <UInt64, Int64, String, String, Object,
                                                       UInt64, Int64, String, String, Object,
                                                       UInt64, Int64, String, String, Object,
                                                       UInt64, Int64, String, String, Object> VertexFilter;

        protected readonly EdgeFilter                 <UInt64, Int64, String, String, Object,
                                                       UInt64, Int64, String, String, Object,
                                                       UInt64, Int64, String, String, Object,
                                                       UInt64, Int64, String, String, Object> EdgeFilter;

        #endregion

        #region MatrixListGraph(PropertyGraph, X, Y, Func)

        public MatrixListGraph(IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object> PropertyGraph,

                               VertexFilter         <UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object> VertexFilter = null,

                               EdgeFilter           <UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object,
                                                     UInt64, Int64, String, String, Object> EdgeFilter   = null,

                               Func<IReadOnlyGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                   UInt64, Int64, String, String, Object,
                                                                   UInt64, Int64, String, String, Object,
                                                                   UInt64, Int64, String, String, Object>, IEnumerable<UInt64>> Func = null)

        {

            this.PropertyGraph    = PropertyGraph;
            this.VertexFilter     = VertexFilter;
            this.EdgeFilter       = EdgeFilter;
            this.Func             = Func;
            this._MatrixListGraph = new List<List<UInt64>>();

            PropertyGraph.Vertices(VertexFilter).ForEach(v =>
            {

                var _List = new List<UInt64>();// { v.Id };
                _List.AddRange(Func(v));
                _MatrixListGraph.Add(_List);

            });

            PropertyGraph.OnVertexAddition.OnNotification += (g, v) => AddVertex(v);
            PropertyGraph.OnEdgeAddition.OnNotification   += (g, e) => AddEdge(e);

        }

        #endregion

        public virtual Boolean AddVertex(IReadOnlyGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object> Vertex)
        {

            if (VertexFilter == null || VertexFilter(Vertex))
            {
                _MatrixListGraph.Add(new List<UInt64>(Func(Vertex)));
                return true;
            }

            return false;

        }

        public virtual Boolean AddEdge(IReadOnlyGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                                    UInt64, Int64, String, String, Object,
                                                                    UInt64, Int64, String, String, Object,
                                                                    UInt64, Int64, String, String, Object> Edge)
        {
            return true;
        }


        //public void ForEachVertex(Action<UInt64[]> Action)
        //{
        //    //_ListGraph.ForEach(vid => { });
        //    for (var i = 0UL; i < this.Graph.NumberOfVertices(); i++)
        //        Action(_ListGraph[i]);

        //}

    }

    #endregion



    #region VertexVertex_MatrixListGraph

    public class VertexVertex_MatrixListGraph : MatrixListGraph
    {

        #region MatrixGraph(PropertyGraph, VertexFilter = null, EdgeFilter = null)

        public VertexVertex_MatrixListGraph(IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object> PropertyGraph,

                                            VertexFilter         <UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object> VertexFilter = null,

                                            EdgeFilter           <UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object> EdgeFilter   = null)

            : base(PropertyGraph,
                   VertexFilter,
                   EdgeFilter,
                   v => from v2 in (from e in v.OutEdges(EdgeFilter) select e.InVertex) select v2.Id)

        {
        }

        #endregion

        #region MatrixGraph(PropertyGraph, VertexLabel, EdgeLabel)

        public VertexVertex_MatrixListGraph(IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object,
                                                                  UInt64, Int64, String, String, Object> PropertyGraph,

                                           String VertexLabel,
                                           String EdgeLabel)

            : this(PropertyGraph,
                   v => (VertexLabel != null) ? v.Label == VertexLabel : true,
                   e => (EdgeLabel   != null) ? e.Label == EdgeLabel   : true)

        { }

        #endregion


        public override Boolean AddEdge(IReadOnlyGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                                     UInt64, Int64, String, String, Object,
                                                                     UInt64, Int64, String, String, Object,
                                                                     UInt64, Int64, String, String, Object> Edge)
        {

            if (EdgeFilter == null || EdgeFilter(Edge))
                _MatrixListGraph.ElementAt((int) Edge.OutVertex.Id).Add(Edge.InVertex.Id);

            return true;

        }


    }

    #endregion

    #region VertexVertex_MatrixListGraph

    public class VertexEdge_MatrixListGraph : MatrixListGraph
    {

        #region MatrixGraph(PropertyGraph)

        public VertexEdge_MatrixListGraph(IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object> PropertyGraph,

                                          VertexFilter         <UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object> VertexFilter = null,

                                          EdgeFilter           <UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object,
                                                                UInt64, Int64, String, String, Object> EdgeFilter = null)

            : base(PropertyGraph,
                   VertexFilter,
                   EdgeFilter,
                   v => from e in v.OutEdges(EdgeFilter) select e.Id)

        {
        }

        public override Boolean AddEdge(IReadOnlyGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                                     UInt64, Int64, String, String, Object,
                                                                     UInt64, Int64, String, String, Object,
                                                                     UInt64, Int64, String, String, Object> Edge)
        {
            
            _MatrixListGraph.ElementAt((int) Edge.OutVertex.Id).Add(Edge.Id);
            
            return true;

        }

        #endregion

    }

    #endregion

}
