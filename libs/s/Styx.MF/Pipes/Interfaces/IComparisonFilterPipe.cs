/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Styx <http://www.github.com/Vanaheimr/Styx>
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

#endregion

namespace de.ahzf.Styx
{

    /// <summary>
    /// A ComparisonFilterPipe will allow or disallow objects that pass
    /// through it depending on some implemented comparison criteria.
    /// </summary>
    /// <typeparam name="S"></typeparam>
    /// <typeparam name="T"></typeparam>
    public interface IComparisonFilterPipe<S, T> : IFilterPipe<S>
        where S : IComparable
        where T : IComparable
    {

        /// <summary>
        /// Compares two objects.
        /// </summary>
        /// <param name="myLeftObject">The left object.</param>
        /// <param name="myRightObject">The right object.</param>
        /// <returns>A match based on the defined filter.</returns>
        Boolean CompareObjects(T myLeftObject, T myRightObject);

    }

}
