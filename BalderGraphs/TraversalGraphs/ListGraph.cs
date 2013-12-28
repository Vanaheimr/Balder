/*
 * Copyright (c) 2010-2014, Achim 'ahzf' Friedland <achim@graphdefined.org>
 * This file is part of Balder.NET <http://www.github.com/Vanaheimr/Balder.NET>
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

using eu.Vanaheimr.Illias.Commons;
using eu.Vanaheimr.Balder;

#endregion

namespace eu.Vanaheimr.Balder.TraversalGraphs
{

    public class ListGraph
    {

        #region Data

        private UInt64[][] _ListGraph;

        private IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                      UInt64, Int64, String, String, Object,
                                      UInt64, Int64, String, String, Object,
                                      UInt64, Int64, String, String, Object> Graph;

        #endregion

        #region ListGraph(Graph, TraversalGraphType)

        public ListGraph(IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object,
                                               UInt64, Int64, String, String, Object> Graph,
                         TraversalGraphType TraversalGraphType)
        {

            this.Graph = Graph;

            _ListGraph = new UInt64[Graph.NumberOfVertices()][];

            Graph.Vertices().ForEach(v => {
                _ListGraph[v.Id - 1] = (from e in v.OutEdges() select e.Id).ToArray();
            });

        }

        #endregion


        public void ForEachVertex(Action<UInt64[]> Action)
        {
            //_ListGraph.ForEach(vid => { });
            for (var i = 0UL; i < this.Graph.NumberOfVertices(); i++)
                Action(_ListGraph[i]);

        }

    }

}
