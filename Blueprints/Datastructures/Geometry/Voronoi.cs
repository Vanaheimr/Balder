/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET <http://www.github.com/ahzf/Blueprints.NET>
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

using de.ahzf.Blueprints.Maths;

#endregion

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// A class for calculating a voronoi diagram.
    /// </summary>
    public static class Voronoi
    {

        public struct AdjacencyInfo<T>
            where T : IEquatable<T>, IComparable<T>, IComparable
        {

            public ITriangle<T> Neighbor1;
            public ITriangle<T> Neighbor2;
            public ITriangle<T> Neighbor3;

            public ILine2D<T> FreeEdge1;
            public ILine2D<T> FreeEdge2;
            public ILine2D<T> FreeEdge3;

            public AdjacencyInfo(ITriangle<T> Neighbor1, ITriangle<T> Neighbor2, ITriangle<T> Neighbor3, ILine2D<T> FreeEdge1, ILine2D<T> FreeEdge2, ILine2D<T> FreeEdge3)
            {
                this.Neighbor1 = Neighbor1;
                this.Neighbor2 = Neighbor2;
                this.Neighbor3 = Neighbor3;
                this.FreeEdge1 = FreeEdge1;
                this.FreeEdge2 = FreeEdge2;
                this.FreeEdge3 = FreeEdge3;
            }

        }


        public static IEnumerable<ITriangleValuePair<T, AdjacencyInfo<T>>> CalcAdjacentTriangles<T>(this IEnumerable<ITriangle<T>> Triangles)
            where T : IEquatable<T>, IComparable<T>, IComparable
        {

            foreach (var _Triangle in Triangles)
            {

                var _Borders   = _Triangle.Borders.ToList();
                var _Neighbors = new List<ITriangle<T>>();

                foreach (var _OtherTriangle in Triangles)
                {
                    if (!_Triangle.Equals(_OtherTriangle))
                    {
                        foreach (var _Border in _OtherTriangle.Borders)
                        {
                            if (_Borders.Contains(_Border))
                            {
                                _Neighbors.Add(_OtherTriangle);
                                _Borders.Remove(_Border);
                            }
                        }
                    }
                }

                var _Neighbors2 = _Neighbors.ToArray();
                var b = _Borders.ToArray();
                var AD = new AdjacencyInfo<T>();

                if (_Neighbors2.Length >= 1)
                    AD.Neighbor1 = _Neighbors2[0];

                if (_Neighbors2.Length >= 2)
                    AD.Neighbor2 = _Neighbors2[1];

                if (_Neighbors2.Length >= 3)
                    AD.Neighbor3 = _Neighbors2[2];

                if (b.Length >= 1)
                    AD.FreeEdge1 = b[0];

                if (b.Length >= 2)
                    AD.FreeEdge2 = b[1];

                if (b.Length >= 3)
                    AD.FreeEdge3 = b[2];

                yield return new TriangleValuePair<T, AdjacencyInfo<T>>(_Triangle.P1, _Triangle.P2, _Triangle.P3, AD);

            }

        }

    }

}
