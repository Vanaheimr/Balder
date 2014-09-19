/*
 * Copyright (c) 2010-2014 Achim 'ahzf' Friedland <achim@graphdefined.org>
 * This file is part of Alviss <http://www.github.com/Vanaheimr/Alviss>
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

namespace org.GraphDefined.Vanaheimr.Alviss
{

    /// <summary>
    /// A delegate to convert a QuadId from the internal
    /// Int64 representation to the actual type T of a quad.
    /// </summary>
    /// <param name="QuadId">A QuadId.</param>
    /// <returns>A QuadId of type T.</returns>
    public delegate TQuadId QuadIdConverterDelegate<TQuadId>(Int64 QuadId);


    /// <summary>
    /// A delegate returning the default context of a quad
    /// if none was given.
    /// </summary>
    public delegate TSPOC DefaultContextDelegate<TSPOC>();


    /// <summary>
    /// A delegate for selecting the subject of a Quad.
    /// </summary>
    /// <typeparam name="TSPOC">The type of the subject.</typeparam>
    /// <param name="Subject">A subject.</param>
    /// <returns>TRUE if the subject had been selected, FALSE if not.</returns>
    public delegate Boolean SubjectSelector<in TSPOC>(TSPOC Subject);

    /// <summary>
    /// A delegate for selecting the predicate of a Quad.
    /// </summary>
    /// <typeparam name="TSPOC">The type of the predicate.</typeparam>
    /// <param name="Predicate">A predicate.</param>
    /// <returns>TRUE if the predicate had been selected, FALSE if not.</returns>
    public delegate Boolean PredicateSelector<in TSPOC>(TSPOC Predicate);

    /// <summary>
    /// A delegate for selecting the object of a Quad.
    /// </summary>
    /// <typeparam name="TSPOC">The type of the object.</typeparam>
    /// <param name="Object">An object.</param>
    /// <returns>TRUE if the object had been selected, FALSE if not.</returns>
    public delegate Boolean ObjectSelector<in TSPOC>(TSPOC Object);

    /// <summary>
    /// A delegate for selecting the context of a Quad.
    /// </summary>
    /// <typeparam name="TSPOC">The type of the context.</typeparam>
    /// <param name="Context">A context.</param>
    /// <returns>TRUE if the context had been selected, FALSE if not.</returns>
    public delegate Boolean ContextSelector<in TSPOC>(TSPOC Context);

}
