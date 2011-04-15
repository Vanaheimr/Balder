///*
// * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
// * This file is part of Blueprints.NET <http://www.github.com/ahzf/blueprints.NET>
// *
// * Licensed under the Apache License, Version 2.0 (the "License");
// * you may not use this file except in compliance with the License.
// * You may obtain a copy of the License at
// *
// *     http://www.apache.org/licenses/LICENSE-2.0
// *
// * Unless required by applicable law or agreed to in writing, software
// * distributed under the License is distributed on an "AS IS" BASIS,
// * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// * See the License for the specific language governing permissions and
// * limitations under the License.
// */

//#region Usings

//using System;
//using System.Collections.Generic;

//#endregion

//namespace de.ahzf.blueprints.PropertyGraph
//{

//    /// <summary>
//    /// An indexable graph is a graph that supports the indexing of its elements.
//    /// An index is typically some sort of tree structure that allows for the fast lookup of elements by key/value pairs.
//    /// All indexable graphs are initially constructed with two automatic indices called "vertices" and "edges".
//    /// </summary>
//    public interface IIndexableGraph : IPropertyGraph
//    {


//        /// <summary>
//        /// Generate an index with a particular name, for a particular class, and of a particular type.
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="myIndexName">the name of the index</param>
//        /// <param name="myIndexType">whether the index is a manual or automatic index</param>
//        /// <returns>the index created</returns>
//        IIndex<T> CreateIndex<T>(String myIndexName, IndexType myIndexType)
//            where T : class;//, IElement;


//        /// <summary>
//        /// Get an index from the graph by its name and index class. An index is unique up to name.
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="myIndexName">the name of the index to retrieve</param>
//        /// <returns>the retrieved index</returns>
//        IIndex<T> GetIndex<T>(String myIndexName)
//            where T : class;//, IElement;


//        /// <summary>
//        /// Get all the indices maintained by the graph.
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <returns>the indices associated with the graph</returns>
//        IEnumerable<IIndex<T>> GetIndices<T>()
//            where T : class;//, IElement;


//        /// <summary>
//        /// Remove an index associated with the graph.
//        /// </summary>
//        /// <param name="myIndexName">the name of the index to drop</param>
//        void DropIndex(String myIndexName);


//    }

//}
