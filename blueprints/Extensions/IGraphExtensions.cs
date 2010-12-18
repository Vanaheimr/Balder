/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using de.ahzf.blueprints.Datastructures;

#endregion

namespace de.ahzf.blueprints
{

    /// <summary>
    /// Extensions to the IGraph interface
    /// </summary>
    public static class IGraphExtensions
    {

        #region ToDynamic(this myIGraph)

        /// <summary>
        /// Converts the given IGraph into a dynamic object
        /// </summary>
        /// <param name="myIGraph">An object implementing IGraph.</param>
        /// <returns>A dynamic object</returns>
        public static dynamic ToDynamic(this IGraph myIGraph)
        {
            return (dynamic) myIGraph;
        }

        #endregion
        

        #region GetVertex<TValue>(this myIGraph, myVertexId)

        /// <summary>
        /// Return the vertex referenced by the provided object identifier.
        /// If no vertex is referenced by that identifier, then return null.
        /// </summary>
        /// <typeparam name="TValue">The type of the vertex.</typeparam>
        /// <param name="myIGraph">An object implementing IGraph.</param>
        /// <param name="myVertexId">The identifier of the vertex to retrieved from the graph.</param>
        /// <returns>The vertex referenced by the provided identifier or null when no such vertex exists.</returns>
        public static TValue GetVertex<TValue>(this IGraph myIGraph, VertexId myVertexId)
            where TValue : class, IVertex
        {
            return myIGraph.GetVertex(myVertexId) as TValue;
        }

        #endregion


    }

}
