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

#endregion

namespace de.ahzf.Blueprints.QuadStore
{
    
    /// <summary>
    /// A delegate for selecting the subject of a Quad.
    /// </summary>
    /// <typeparam name="T">The type of the subject.</typeparam>
    /// <param name="Subject">A subject.</param>
    /// <returns>TRUE if the subject had been selected, FALSE if not.</returns>
    public delegate Boolean SubjectSelector       <in T>(T Subject);

    /// <summary>
    /// A delegate for selecting the predicate of a Quad.
    /// </summary>
    /// <typeparam name="T">The type of the predicate.</typeparam>
    /// <param name="Predicate">A predicate.</param>
    /// <returns>TRUE if the predicate had been selected, FALSE if not.</returns>
    public delegate Boolean PredicateSelector     <in T>(T Predicate);

    /// <summary>
    /// A delegate for selecting the object of a Quad.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="Object">An object.</param>
    /// <returns>TRUE if the object had been selected, FALSE if not.</returns>

    public delegate Boolean ObjectSelector        <in T>(T Object);

    /// <summary>
    /// A delegate for selecting the Context or Graph of a Quad.
    /// </summary>
    /// <typeparam name="T">The type of the context or graph.</typeparam>
    /// <param name="ContextOrGraph">A context or graph.</param>
    /// <returns>TRUE if the context or graph had been selected, FALSE if not.</returns>
    public delegate Boolean ContextOrGraphSelector<in T>(T ContextOrGraph);

}
