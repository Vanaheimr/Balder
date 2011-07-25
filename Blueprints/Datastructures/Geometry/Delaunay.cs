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

// http://paulbourke.net/papers/triangulate/morten.html
// http://www.codeguru.com/cpp/cpp/algorithms/general/article.php/c8901
// http://www.ics.uci.edu/~eppstein/gina/delaunay.html
 
#region Usings

using System;
using System.Linq;
using System.Collections.Generic;

using de.ahzf.Blueprints.Maths;

#endregion

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// A class for calculating a delaunay triangulation.
    /// </summary>
    public static class Delaunay_Triangulation
    {

        #region DelaunayTriangulation<T>(this Pixels)

        /// <summary>
        /// Calculate a delaunay triangulation for the given enumeration of pixels.
        /// </summary>
        /// <typeparam name="T">The type of the pixels.</typeparam>
        /// <param name="Pixels">An enumeration of pixels of type T.</param>
        /// <returns>An enumeration of triangles of type T.</returns>
        public static IEnumerable<ITriangle<T>> DelaunayTriangulation<T>(this IEnumerable<IPixel<T>> Pixels)
            where T : IEquatable<T>, IComparable<T>, IComparable
        {

            #region Initial Checks

            var Math            = MathsFactory<T>.Instance;
            var Points          = Pixels.ToList();
            var _NumberOfPixels = Pixels.Count();
            var _TriMax         = 4 * _NumberOfPixels;

            if (_NumberOfPixels < 3)
                throw new ArgumentException("Need at least three pixels for triangulation!");

            #endregion

            #region Set up the supertriangle

            #region Find the maximum and minimum vertex bounds.

            // This is to allow calculation of the bounding supertriangle

            var xmin = Points[0].X;
            var ymin = Points[0].Y;
            var xmax = xmin;
            var ymax = ymin;

            for (int i = 1; i < _NumberOfPixels; i++)
            {
                if (Points[i].X.IsLessThan(xmin))   xmin = Points[i].X;
                if (Points[i].X.IsLargerThan(xmax)) xmax = Points[i].X;
                if (Points[i].Y.IsLessThan(ymin))   ymin = Points[i].Y;
                if (Points[i].Y.IsLargerThan(ymax)) ymax = Points[i].Y;
            }

            var dx   = Math.Sub(xmax, xmin);
            var dy   = Math.Sub(ymax, ymin);
            var dmax = (dx.IsLargerThan(dy)) ? dx : dy;

            var xmid = Math.Div2(Math.Add(xmax, xmin));
            var ymid = Math.Div2(Math.Add(ymax, ymin));

            #endregion

            #region Set up the supertriangle

            // This is a triangle which encompasses all the sample points.
            // The supertriangle coordinates are added to the end of the
            // vertex list.
            Points.Add(new Pixel<T>(Math.Sub(xmid, Math.Mul2(dmax)), Math.Sub(ymid, dmax)));
            Points.Add(new Pixel<T>(xmid,                            Math.Add(ymid, Math.Mul2(dmax))));
            Points.Add(new Pixel<T>(Math.Add(xmid, Math.Mul2(dmax)), Math.Sub(ymid, dmax)));

            // The supertriangle is the first triangle in the triangle list.
            var Triangles = new List<ITriangle<T>>();

            // SuperTriangle placed at index 0
            Triangles.Add(new Triangle<T>(Points[_NumberOfPixels], Points[_NumberOfPixels + 1], Points[_NumberOfPixels + 2]));

            #endregion

            #endregion

            #region Include each point one at a time into the existing mesh

            for (int i = 0; i < _NumberOfPixels; i++)
            {
                
                var Edges = new List<ILine2D<T>>();

                // Set up the edge buffer.
                // If the Pixel(X, Y) lies inside the circumcircle then the
                // three edges of that triangle are added to the edge buffer
                // and the triangle is removed from list.
                for (int _CurrentTriangle = 0; _CurrentTriangle < Triangles.Count; _CurrentTriangle++)
                {

                    if (Circle<T>.IsInCircle(Points[i],
                                             Triangles[_CurrentTriangle].P1,
                                             Triangles[_CurrentTriangle].P2,
                                             Triangles[_CurrentTriangle].P3))
                    {
                        Edges.Add(new Line2D<T>(Triangles[_CurrentTriangle].P1, Triangles[_CurrentTriangle].P2));
                        Edges.Add(new Line2D<T>(Triangles[_CurrentTriangle].P2, Triangles[_CurrentTriangle].P3));
                        Edges.Add(new Line2D<T>(Triangles[_CurrentTriangle].P3, Triangles[_CurrentTriangle].P1));
                        Triangles.RemoveAt(_CurrentTriangle);
                        _CurrentTriangle--;
                    }

                }

                // In case we the last duplicate point we removed was the last in the array
                if (i >= _NumberOfPixels) continue;

                #region Remove duplicate edges

                // Note: if all triangles are specified anticlockwise then all
                // interior edges are opposite pointing in direction.
                for (int j = Edges.Count - 2; j >= 0; j--)
                {
                    for (int k = Edges.Count - 1; k >= j + 1; k--)
                    {
                        if (Edges[j].Equals(Edges[k]))
                        {
                            Edges.RemoveAt(k);
                            Edges.RemoveAt(j);
                            k--;
                            continue;
                        }
                    }
                }

                #endregion

                #region Form new triangles for the current point

                // Skipping over any tagged edges.
                // All edges are arranged in clockwise order.
                for (int j = 0; j < Edges.Count; j++)
                {

                    if (Triangles.Count >= _TriMax)
                        throw new ApplicationException("Exceeded maximum edges");

                    Triangles.Add(new Triangle<T>(Edges[j].Pixel1, Edges[j].Pixel2, Points[i]));

                }

                #endregion

            }

            #endregion

            #region Remove triangles with supertriangle vertices

            // These are triangles which have a vertex number greater than nv
            //for (int i = Triangles.Count - 1; i >= 0; i--)
            //{
            //    if (Triangles[i].p1 >= _NumberOfPixels || Triangles[i].p2 >= _NumberOfPixels || Triangles[i].p3 >= _NumberOfPixels)
            //        Triangles.RemoveAt(i);
            //}

            //T _NoP = (T) (Object) _NumberOfPixels;

            //for (int i = Triangle2.Count - 1; i >= 0; i--)
            //{

            //    if (Triangle2[i].P1.IsLargerThanOrEquals(_NoP) ||
            //        Triangle2[i].P2.IsLargerThanOrEquals(_NoP) ||
            //        Triangle2[i].P3.IsLargerThanOrEquals(_NoP))

            //        Triangle2.RemoveAt(i);

            //}

            #endregion

            return Triangles;

        }

        #endregion

    }

}
