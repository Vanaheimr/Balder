/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <code@ahzf.de>
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

namespace de.ahzf.Blueprints.PropertyGraphs
{

    /// <summary>
    /// A labeled property graph.
    /// </summary>
    public interface ILabeledPropertyGraph<TVertexLabel, TEdgeLabel, TMultiEdgeLabel, THyperEdgeLabel>

                         : IGenericPropertyGraph<UInt64, Int64, TVertexLabel,    String, Object,
                                                 UInt64, Int64, TEdgeLabel,      String, Object,
                                                 UInt64, Int64, TMultiEdgeLabel, String, Object,
                                                 UInt64, Int64, THyperEdgeLabel, String, Object>

        where TVertexLabel    : IEquatable<TVertexLabel>,    IComparable<TVertexLabel>,    IComparable
        where TEdgeLabel      : IEquatable<TEdgeLabel>,      IComparable<TEdgeLabel>,      IComparable
        where TMultiEdgeLabel : IEquatable<TMultiEdgeLabel>, IComparable<TMultiEdgeLabel>, IComparable
        where THyperEdgeLabel : IEquatable<THyperEdgeLabel>, IComparable<THyperEdgeLabel>, IComparable

    { }

}
