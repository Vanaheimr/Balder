/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
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

    public class WeightedAdjacencyListGraph<T>
    {

        public struct WeightedEdge<T> : IEquatable<WeightedEdge<T>>
        {

            public readonly T       Weight;
            public readonly UInt64  Vertex;

            public WeightedEdge(T Weight, UInt64  Vertex)
            {
                this.Weight = Weight;
                this.Vertex = Vertex;
            }


            public Boolean Equals(WeightedEdge<T> other)
            {
                return Vertex.Equals(other.Vertex);
            }

        }


        private readonly HashSet<WeightedEdge<T>>[] AdjacencyList;

        public String Description { get; private set; }


        public WeightedAdjacencyListGraph(UInt64 NumberOfVertices, String Description = null)
        {
            this.AdjacencyList  = new HashSet<WeightedEdge<T>>[NumberOfVertices];
            this.Description    = Description;
        }


        public void AddVertex(UInt64 VertexId)
        {

            if (AdjacencyList[VertexId] != null)
                AdjacencyList[VertexId] = new HashSet<WeightedEdge<T>>();

        }

        public void AddEdge(UInt64 OutVertexId, UInt64 InVertexId, T Weight)
        {

            if (AdjacencyList[OutVertexId] != null)
                AdjacencyList[OutVertexId].Add(new WeightedEdge<T>(Weight, InVertexId));
            else
                throw new Exception();

        }

    }

}
