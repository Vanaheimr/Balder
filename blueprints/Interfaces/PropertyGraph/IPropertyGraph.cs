/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET <http://www.github.com/ahzf/blueprints.NET>
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
using de.ahzf.blueprints.Datastructures;

#endregion

namespace de.ahzf.blueprints
{

    #region IPropertyGraph

    /// <summary>
    /// A standardized Property Graph.
    /// </summary>
    public interface IPropertyGraph : IPropertyGraph<// Vertex definition
                                                     VertexId,    RevisionId, String, Object, IDictionary<String, Object>,
                                                     IPropertyVertex   <VertexId,    RevisionId, String, Object,
                                                                        EdgeId,      RevisionId, String, Object,
                                                                        HyperEdgeId, RevisionId, String, Object>,

                                                     // Edge definition
                                                     EdgeId,      RevisionId, String, Object, IDictionary<String, Object>,
                                                     IPropertyEdge     <VertexId,    RevisionId, String, Object,
                                                                        EdgeId,      RevisionId, String, Object,
                                                                        HyperEdgeId, RevisionId, String, Object>,

                                                     // HyperEdge definition
                                                     HyperEdgeId, RevisionId, String, Object, IDictionary<String, Object>,
                                                     IPropertyHyperEdge<VertexId, RevisionId, String, Object,
                                                                        EdgeId,      RevisionId, String, Object,
                                                                        HyperEdgeId, RevisionId, String, Object>>

    { }

    #endregion

    #region IPropertyGraph<...>

    /// <summary>
    /// A generic property graph.
    /// </summary>
    public interface IPropertyGraph<TIdVertex,    TRevisionIdVertex,    TKeyVertex,    TValueVertex,    TDatastructureVertex,    TVertex,
                                    TIdEdge,      TRevisionIdEdge,      TKeyEdge,      TValueEdge,      TDatastructureEdge,      TEdge,
                                    TIdHyperEdge, TRevisionIdHyperEdge, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge, THyperEdge>

                     : IGenericGraph<TIdVertex,    TRevisionIdVertex,    IProperties<TKeyVertex,    TValueVertex>,    TVertex,
                                     TIdEdge,      TRevisionIdEdge,      IProperties<TKeyEdge,      TValueEdge>,      TEdge,
                                     TIdHyperEdge, TRevisionIdHyperEdge, IProperties<TKeyHyperEdge, TValueHyperEdge>, THyperEdge>

        where TDatastructureVertex    : IDictionary<TKeyVertex,    TValueVertex>
        where TDatastructureEdge      : IDictionary<TKeyEdge,      TValueEdge>
        where TDatastructureHyperEdge : IDictionary<TKeyHyperEdge, TValueHyperEdge>

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable
                                                                                                            
        where TIdVertex               : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge                 : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdHyperEdge            : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TRevisionIdVertex       : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge         : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdHyperEdge    : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

        where TVertex                 : IGenericVertex   <TIdVertex,    TRevisionIdVertex,    IProperties<TKeyVertex,    TValueVertex>,
                                                          TIdEdge,      TRevisionIdEdge,      IProperties<TKeyEdge,      TValueEdge>,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, IProperties<TKeyHyperEdge, TValueHyperEdge>>

        where TEdge                   : IGenericEdge     <TIdVertex,    TRevisionIdVertex,    IProperties<TKeyVertex,    TValueVertex>,
                                                          TIdEdge,      TRevisionIdEdge,      IProperties<TKeyEdge,      TValueEdge>,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, IProperties<TKeyHyperEdge, TValueHyperEdge>>

        where THyperEdge              : IGenericHyperEdge<TIdVertex,    TRevisionIdVertex,    IProperties<TKeyVertex,    TValueVertex>,
                                                          TIdEdge,      TRevisionIdEdge,      IProperties<TKeyEdge,      TValueEdge>,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, IProperties<TKeyHyperEdge, TValueHyperEdge>>

    { }

    #endregion

}
