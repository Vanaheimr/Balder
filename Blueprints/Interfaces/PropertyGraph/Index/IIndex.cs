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
using System.Collections.Generic;

#endregion

namespace de.ahzf.Blueprints.PropertyGraph
{

    /// <summary>
    /// An index maintains a mapping between some key/value pair and an element.
    /// A manual index requires that the developers code explicitly put elements of the graph into the index.
    /// A the key/value pair need not be specific to the element properties.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IIndex<T>
//        where T : IElement
    {

        
        ///// <summary>
        ///// For IndexableGraphs that support vertex indexing, an
        ///// AutomaticIndex must exist at construction named "vertices".
        ///// </summary>
        //static const String VERTICES = "vertices";

        ///// <summary>
        ///// For IndexableGraphs that support edge indexing, an
        ///// AutomaticIndex must exist at construction named "edges".
        ///// </summary>
        //static const String EDGES = "edges";



        /**
         * Get the name of the index.
         *
         * @return the name of the index
         */
        String getIndexName();

        /**
         * Get the class that this index is indexing.
         *
         * @return the class this index is indexing
         */
        //Class<T> getIndexClass();
        T getIndexClass();

        /**
         * Get the type of the index. This can be determined using instanceof on the interface names as well.
         *
         * @return the index type
         */
        Type getIndexType();

        /**
         * Index an element by a key and a value.
         *
         * @param key     the key to index the element by
         * @param value   the value to index the element by
         * @param element the element to index
         */
        void put(String key, Object value, T element);

        /**
         * Get all elements that are indexed by the provided key/value.
         *
         * @param key   the key of the indexed elements
         * @param value the value of the indexed elements
         * @return an iterable of elements that have a particular key/value in the index
         */
        IEnumerable<T> get(String key, Object value);

        /**
         * Remove an element indexed by a particular key/value.
         *
         * @param key     the key of the indexed element
         * @param value   the value of the indexed element
         * @param element the element to remove given the key/value pair
         */
        void remove(String key, Object value, T element);

    }

}
