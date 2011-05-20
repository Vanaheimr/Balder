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

#endregion

namespace SocialGraphDemo
{
    
    /// <summary>
    /// Utilities for generating social graphs
    /// </summary>
    public static class SocialGraphGenerator
    {

        #region Generate(myNumberOfVertices, myNumberOfEdges, myBatchNumber = 5000, myBatchAction = null)

        /// <summary>
        /// Generate a random growing network using preferential attachment.
        /// </summary>
        /// <param name="myNumberOfVertices"></param>
        /// <param name="myNumberOfEdges"></param>
        /// <param name="PreferentialAttachment"></param>
        /// <param name="BatchNumber"></param>
        /// <param name="BatchAction"></param>
        /// <returns></returns>
        public static IEnumerable<HashSet<Int32>> Generate(Int32 myNumberOfVertices, Int32 myNumberOfEdges, Int32 PreferentialAttachment = 3, Int32 BatchNumber = 5000, Action<Int32> BatchAction = null)
        {

            var _Vertices           = new HashSet<Int32>[myNumberOfVertices];
            var _EdgesPerVertex     = myNumberOfEdges / myNumberOfVertices;
            var _RandomGenerator    = new Random();
            var _RandomNumbers      = new Int32[PreferentialAttachment];
            var _RandomNumber       = 0;

            for (var _ActualVertexID = 0; _ActualVertexID < myNumberOfVertices; _ActualVertexID++)
            {

                if (_Vertices[_ActualVertexID] == null)
                    _Vertices[_ActualVertexID] = new HashSet<Int32>();

                // Fill HashSet with random TargetVertices
                for (var j = 0; j < _EdgesPerVertex; j++)
                {

                    // Generate list of random numbers less than the actual vertex id
                    for (var r = 0; r < PreferentialAttachment; r++)
                        _RandomNumbers[r] = _RandomGenerator.Next(_ActualVertexID);

                    // Pick the random number having the most edges (preferential attachment)
                    _RandomNumber = (from _Number in _RandomNumbers orderby _Vertices[_Number].Count select _Number).First();

                    if (_RandomNumber != _ActualVertexID)
                    {

                        if (_Vertices[_RandomNumber] == null)
                            _Vertices[_RandomNumber] = new HashSet<Int32>();

                        _Vertices[_RandomNumber].Add(_ActualVertexID);
                        _Vertices[_ActualVertexID].Add(_RandomNumber);

                    }

                }

                if (BatchAction != null && _ActualVertexID % BatchNumber == 0)
                    BatchAction(_ActualVertexID);

            }

            if (BatchAction != null)
                BatchAction(myNumberOfVertices);

            return _Vertices;

        }

        #endregion

        #region Histogram(mySocialGraph)

        /// <summary>
        /// Create a histogram of a Social Graph
        /// </summary>
        /// <param name="mySocialGraph">The Social Graph</param>
        /// <returns>The histogram</returns>
        public static IEnumerable<Int32> Histogram(IEnumerable<IEnumerable<Int32>> mySocialGraph)
        {

            var _Histogram = new Int32[mySocialGraph.Count()];
            var _Max       = 0;
            var _Count     = 0;

            foreach (var _HashSet in mySocialGraph)
            {
                _Count = _HashSet.Count();
                _Histogram[_Count]++;
                _Max = Math.Max(_Max, _Count);
            }

            Array.Resize(ref _Histogram, _Max + 1);

            return _Histogram;

        }

        #endregion

    }

}
