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

    public class UndirectedAdjacencyMatrixGraph<TEdge>
    {

        private readonly UInt32     MaxNumberOfVertices;
        private readonly TEdge[][]  AdjacencyHalfMatrix;


        public UndirectedAdjacencyMatrixGraph(UInt32 MaxNumberOfVertices)
        {

            this.MaxNumberOfVertices = MaxNumberOfVertices;
            this.AdjacencyHalfMatrix = new TEdge[MaxNumberOfVertices][];

            for (var i=0; i<MaxNumberOfVertices; i++)
                AdjacencyHalfMatrix[i] = new TEdge[MaxNumberOfVertices - i];

        }


        public void AddEdge(UInt32 OutVertexId, UInt32 InVertexId, TEdge Value = default(TEdge))
        {

            if (OutVertexId >= MaxNumberOfVertices || InVertexId >= MaxNumberOfVertices)
                throw new Exception();

            if (InVertexId < OutVertexId)
            {
                var tmp      = InVertexId;
                InVertexId   = OutVertexId;
                OutVertexId  = tmp;
            }

            AdjacencyHalfMatrix[OutVertexId][InVertexId] = Value;

        }

        public IEnumerable<TEdge> Edges(UInt32 VertexId)
        {
            return AdjacencyHalfMatrix[VertexId].Where(t => t != null);
        }

        public IEnumerable<UInt32> Neighbors(UInt32 VertexId)
        {
            for (var i = 0U; i <= MaxNumberOfVertices - VertexId; i++)
                if (AdjacencyHalfMatrix[VertexId][i] != null)
                    yield return i;
        }

    }

}
