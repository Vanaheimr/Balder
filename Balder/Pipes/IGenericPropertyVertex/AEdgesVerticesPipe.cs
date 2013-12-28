/*
 * Copyright (c) 2010-2014, Achim 'ahzf' Friedland <achim@graphdefined.org>
 * This file is part of Balder <http://www.github.com/Vanaheimr/Balder>
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
using System.Linq;
using System.Collections.Generic;

using eu.Vanaheimr.Balder;
using eu.Vanaheimr.Styx;

#endregion

namespace eu.Vanaheimr.Balder
{

    /// <summary>
    /// Emit the adjacent vertices of the given generic property vertices
    /// reachable via incoming and/or outgoing edges (OR-logic).
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TVertexLabel">The type of the vertex type.</typeparam>
    /// <typeparam name="TKeyVertex">The type of the vertex property keys.</typeparam>
    /// <typeparam name="TValueVertex">The type of the vertex property values.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
    /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
    /// 
    /// <typeparam name="TIdMultiEdge">The type of the multiedge identifiers.</typeparam>
    /// <typeparam name="TRevIdMultiEdge">The type of the multiedge revision identifiers.</typeparam>
    /// <typeparam name="TMultiEdgeLabel">The type of the multiedge label.</typeparam>
    /// <typeparam name="TKeyMultiEdge">The type of the multiedge property keys.</typeparam>
    /// <typeparam name="TValueMultiEdge">The type of the multiedge property values.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
    /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
    public abstract class AEdgesVerticesPipe<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                             TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

                                             : AbstractPipe<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                                            IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                           TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                           TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                           TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>


        where TIdVertex        : IEquatable<TIdVertex>,       IComparable<TIdVertex>,       IComparable, TValueVertex
        where TIdEdge          : IEquatable<TIdEdge>,         IComparable<TIdEdge>,         IComparable, TValueEdge
        where TIdMultiEdge     : IEquatable<TIdMultiEdge>,    IComparable<TIdMultiEdge>,    IComparable, TValueMultiEdge
        where TIdHyperEdge     : IEquatable<TIdHyperEdge>,    IComparable<TIdHyperEdge>,    IComparable, TValueHyperEdge

        where TRevIdVertex     : IEquatable<TRevIdVertex>,    IComparable<TRevIdVertex>,    IComparable, TValueVertex
        where TRevIdEdge       : IEquatable<TRevIdEdge>,      IComparable<TRevIdEdge>,      IComparable, TValueEdge
        where TRevIdMultiEdge  : IEquatable<TRevIdMultiEdge>, IComparable<TRevIdMultiEdge>, IComparable, TValueMultiEdge
        where TRevIdHyperEdge  : IEquatable<TRevIdHyperEdge>, IComparable<TRevIdHyperEdge>, IComparable, TValueHyperEdge

        where TVertexLabel     : IEquatable<TVertexLabel>,    IComparable<TVertexLabel>,    IComparable, TValueVertex
        where TEdgeLabel       : IEquatable<TEdgeLabel>,      IComparable<TEdgeLabel>,      IComparable, TValueEdge
        where TMultiEdgeLabel  : IEquatable<TMultiEdgeLabel>, IComparable<TMultiEdgeLabel>, IComparable, TValueMultiEdge
        where THyperEdgeLabel  : IEquatable<THyperEdgeLabel>, IComparable<THyperEdgeLabel>, IComparable, TValueHyperEdge

        where TKeyVertex       : IEquatable<TKeyVertex>,      IComparable<TKeyVertex>,      IComparable
        where TKeyEdge         : IEquatable<TKeyEdge>,        IComparable<TKeyEdge>,        IComparable
        where TKeyMultiEdge    : IEquatable<TKeyMultiEdge>,   IComparable<TKeyMultiEdge>,   IComparable
        where TKeyHyperEdge    : IEquatable<TKeyHyperEdge>,   IComparable<TKeyHyperEdge>,   IComparable

    {

        #region Data

        /// <summary>
        /// The labels of the edge(s) to follow (OR-logic).
        /// </summary>
        protected readonly TEdgeLabel[] EdgeLabels;

        /// <summary>
        /// A delegate for edge filtering.
        /// </summary>
        protected readonly EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                      TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                      TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter;

        private Boolean Switched;

        private readonly Func<Boolean> TraversalDelegate;

        /// <summary>
        /// Edges to be traversed next...
        /// </summary>
        private IEnumerator<IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                         TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                         TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                         TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> _NextEdges;

        /// <summary>
        /// The vertex of the edge to be returned.
        /// </summary>
        private Func<IReadOnlyGenericPropertyEdge  <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                     IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                    TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                    TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> Edge2VertexDelegate;

        #endregion

        #region Constructor(s)

        #region (internal) AEdgesVerticesPipe(TraversalDirection, IEnumerable, IEnumerator, params EdgeLabels)

        /// <summary>
        /// Emit the adjacent vertices of the given generic property vertices
        /// reachable via incoming and/or outgoing edges (OR-logic).
        /// </summary>
        /// <param name="TraversalDirection">The next edges to visit.</param>
        /// <param name="IEnumerable">An IEnumerable&lt;...&gt; as element source.</param>
        /// <param name="IEnumerator">An IEnumerator&lt;...&gt; as element source.</param>
        /// <param name="EdgeLabels">An optional array of edge labels to traverse (OR-logic).</param>
        internal AEdgesVerticesPipe(TraversalDirection TraversalDirection,

                                    IEnumerable<IReadOnlyGenericPropertyVertex<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex,
                                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> IEnumerable,

                                    IEnumerator<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> IEnumerator,

                                    params TEdgeLabel[] EdgeLabels)

            : base(IEnumerable, IEnumerator)

        {

            this.EdgeLabels = EdgeLabels.Any() ? EdgeLabels : null;

            switch (TraversalDirection)
            {

                case Balder.TraversalDirection.Out:  TraversalDelegate  = TraverseOneDirectionDelegate(vertex => vertex.OutEdges(EdgeLabels).GetEnumerator(),
                                                                                                       edge   => edge.InVertex);
                                                                          break;

                case Balder.TraversalDirection.In:   TraversalDelegate  = TraverseOneDirectionDelegate(vertex => vertex.InEdges(EdgeLabels).GetEnumerator(),
                                                                                                       edge   => edge.OutVertex);
                                                                          break;

                case Balder.TraversalDirection.Both: TraversalDelegate  = TraverseBothDirectionsDelegate;
                                                                          Switched = true;
                                                                          break;

            }

        }

        #endregion

        #region (internal) AEdgesVerticesPipe(TraversalDirection, EdgeFilter, Enumerable, IEnumerator)

        /// <summary>
        /// Emit the adjacent vertices of the given generic property vertices
        /// filtered by the given edge filter.
        /// </summary>
        /// <param name="TraversalDirection">The next edges to visit.</param>
        /// <param name="EdgeFilter">An optional delegate for edge filtering.</param>
        /// <param name="IEnumerable">An IEnumerable&lt;...&gt; as element source.</param>
        /// <param name="IEnumerator">An IEnumerator&lt;...&gt; as element source.</param>
        internal AEdgesVerticesPipe(TraversalDirection TraversalDirection,

                                    EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter,

                                    IEnumerable<IReadOnlyGenericPropertyVertex<TIdVertex, TRevIdVertex, TVertexLabel, TKeyVertex, TValueVertex,
                                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> IEnumerable,

                                    IEnumerator<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                               TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                               TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                               TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> IEnumerator)

            : base(IEnumerable, IEnumerator)

        {

            this.EdgeFilter = (EdgeFilter != null) ? EdgeFilter : e => true;

            switch (TraversalDirection)
            {

                case Balder.TraversalDirection.Out:  TraversalDelegate  = TraverseOneDirectionDelegate(vertex => vertex.OutEdges(EdgeFilter).GetEnumerator(),
                                                                                                       edge   => edge.InVertex);
                                                                          break;

                case Balder.TraversalDirection.In:   TraversalDelegate  = TraverseOneDirectionDelegate(vertex => vertex.InEdges(EdgeFilter).GetEnumerator(),
                                                                                                       edge   => edge.OutVertex);
                                                                          break;

                case Balder.TraversalDirection.Both: TraversalDelegate  = TraverseBothDirectionsDelegate;
                                                                          Switched = true;
                                                                          break;

            }

        }

        #endregion

        #endregion


        #region TraverseOneDirectionDelegate(..., ...)

        private Func<Boolean> TraverseOneDirectionDelegate(Func<IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                         IEnumerator<IReadOnlyGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                                  TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                                  TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                                  TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>> _Vertex2EdgesDelegate,

                                    Func<IReadOnlyGenericPropertyEdge  <TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>,

                                         IReadOnlyGenericPropertyVertex<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
                                                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>> _Edge2VertexDelegate)

        {

            return () => {

                while (true)
                {

                    if (_NextEdges != null && _NextEdges.MoveNext())
                    {
                        _CurrentElement = _Edge2VertexDelegate(_NextEdges.Current);
                        return true;
                    }

                    else if (_InputEnumerator.MoveNext())
                    {
                        _NextEdges = _Vertex2EdgesDelegate(_InputEnumerator.Current);
                    }

                    else
                        return false;

                }

            };

        }

        #endregion

        #region (private) TraverseBothDirectionsDelegate()

        private Boolean TraverseBothDirectionsDelegate()
        {

            while (true)
            {

                if (_NextEdges != null && _NextEdges.MoveNext())
                {
                    _CurrentElement      = Edge2VertexDelegate(_NextEdges.Current);
                    return true;
                }

                else if (!Switched)
                {
                    _NextEdges           = _InputEnumerator.Current.InEdges(EdgeFilter).GetEnumerator();
                    Edge2VertexDelegate  = edge => edge.OutVertex;
                    Switched             = true;
                }

                else if (_InputEnumerator.MoveNext())
                {
                    _NextEdges           = _InputEnumerator.Current.OutEdges(EdgeFilter).GetEnumerator();
                    Edge2VertexDelegate  = edge => edge.InVertex;
                    Switched             = false;
                }

                else
                    return false;

            }

        }

        #endregion


        #region MoveNext()

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// True if the enumerator was successfully advanced to the next
        /// element; false if the enumerator has passed the end of the
        /// collection.
        /// </returns>
        public override Boolean MoveNext()
        {

            if (_InputEnumerator == null)
                return false;

            return TraversalDelegate();

        }

        #endregion


        #region Reset()

        /// <summary>
        /// A pipe may maintain state. Reset is used to remove state.
        /// </summary>
        public override void Reset()
        {
            //_NextEdges = null;
            base.Reset();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// A string representation of this pipe.
        /// </summary>
        public override String ToString()
        {

            if (EdgeLabels != null)
                return base.ToString() + " (" + EdgeLabels.Aggregate("", (a, b) => a.ToString() + " " + b.ToString()) + ")";

            return base.ToString();

        }

        #endregion

    }

}
