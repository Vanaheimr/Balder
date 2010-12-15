/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using System;
using System.Collections.Generic;

#endregion

namespace de.ahzf.blueprints
{

    /// <summary>
    /// An indexable graph is a graph that supports the indexing of its elements.
    /// An index is typically some sort of tree structure that allows for the fast lookup of elements by key/value pairs.
    /// All indexable graphs are initially constructed with two automatic indices called "vertices" and "edges".
    /// </summary>
    public interface IIndexableGraph : IGraph
    {


        /// <summary>
        /// Generate an index with a particular name, for a particular class, and of a particular type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="myIndexName">the name of the index</param>
        /// <param name="myIndexType">whether the index is a manual or automatic index</param>
        /// <returns>the index created</returns>
        IIndex<T> CreateIndex<T>(String myIndexName, IndexType myIndexType)
            where T : class, IElement;


        /// <summary>
        /// Get an index from the graph by its name and index class. An index is unique up to name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="myIndexName">the name of the index to retrieve</param>
        /// <returns>the retrieved index</returns>
        IIndex<T> GetIndex<T>(String myIndexName)
            where T : class, IElement;


        /// <summary>
        /// Get all the indices maintained by the graph.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>the indices associated with the graph</returns>
        IEnumerable<IIndex<T>> GetIndices<T>()
            where T : class, IElement;


        /// <summary>
        /// Remove an index associated with the graph.
        /// </summary>
        /// <param name="myIndexName">the name of the index to drop</param>
        void DropIndex(String myIndexName);


    }

}
