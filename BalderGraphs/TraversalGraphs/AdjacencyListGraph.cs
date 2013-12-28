/*
 * Copyright (c) 2010-2014, Achim 'ahzf' Friedland <achim@graphdefined.org>
 * This file is part of Balder <http://www.github.com/Vanaheimr/Balder>
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

using eu.Vanaheimr.Styx;
using eu.Vanaheimr.Balder;

#endregion

namespace eu.Vanaheimr.Balder.DependentGraphs
{

    public class AdjacencyListGraph
    {

        private readonly HashSet<UInt64>[] AdjacencyList;

        public String Description { get; private set; }


        public AdjacencyListGraph(UInt64 NumberOfVertices, String Description = null)
        {
            this.AdjacencyList  = new HashSet<UInt64>[NumberOfVertices];
            this.Description    = Description;
        }


        public void AddVertex(UInt64 VertexId)
        {

            if (AdjacencyList[VertexId] != null)
                AdjacencyList[VertexId] = new HashSet<UInt64>();

        }

        public void AddEdge(UInt64 OutVertexId, UInt64 InVertexId)
        {

            if (AdjacencyList[OutVertexId] != null)
                AdjacencyList[OutVertexId].Add(InVertexId);
            else
                throw new Exception();

        }

    }


    public class AdjacencyListGraph<TVertexId>
        where TVertexId : IEquatable<TVertexId>, IComparable<TVertexId>, IComparable

    {

        private readonly Dictionary<TVertexId, HashSet<TVertexId>> AdjacencyList;


        public AdjacencyListGraph()
        {
            this.AdjacencyList = new Dictionary<TVertexId, HashSet<TVertexId>>();
        }


        public void AddVertex(TVertexId VertexId)
        {

            if (!AdjacencyList.ContainsKey(VertexId))
                AdjacencyList.Add(VertexId, new HashSet<TVertexId>());

        }

        public void AddEdge(TVertexId OutVertexId, TVertexId InVertexId)
        {

            HashSet<TVertexId> Set;

            if (AdjacencyList.TryGetValue(OutVertexId, out Set))
                Set.Add(InVertexId);
            else
                throw new Exception();

        }

    }

}
