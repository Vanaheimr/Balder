﻿/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
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
        /// <param name="myPreferentialAttachment"></param>
        /// <param name="myBatchNumber"></param>
        /// <param name="myBatchAction"></param>
        /// <returns></returns>
        public static IEnumerable<HashSet<Int32>> Generate(Int32 myNumberOfVertices, Int32 myNumberOfEdges, Int32 myPreferentialAttachment = 3, Int32 myBatchNumber = 5000, Action<Int32> myBatchAction = null)
        {

            var _Vertices           = new HashSet<Int32>[myNumberOfVertices];
            var _EdgesPerVertex     = myNumberOfEdges / myNumberOfVertices;
            var _RandomGenerator    = new Random();
            var _RandomNumbers      = new Int32[myPreferentialAttachment];
            var _RandomNumber       = 0;

            for (var _ActualVertexID = 0; _ActualVertexID < myNumberOfVertices; _ActualVertexID++)
            {

                if (_Vertices[_ActualVertexID] == null)
                    _Vertices[_ActualVertexID] = new HashSet<Int32>();

                // Fill HashSet with random TargetVertices
                for (var j = 0; j < _EdgesPerVertex; j++)
                {

                    // Generate list of random numbers less than the actual vertex id
                    for (var r = 0; r < myPreferentialAttachment; r++)
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

                if (myBatchAction != null && _ActualVertexID % myBatchNumber == 0)
                    myBatchAction(_ActualVertexID);

            }

            if (myBatchAction != null)
                myBatchAction(myNumberOfVertices);

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
