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
using System.Linq;
using System.Dynamic;
using System.Linq.Expressions;
using System.Collections.Generic;

using de.ahzf.Blueprints.PropertyGraph.ReadOnly;

#endregion

namespace de.ahzf.Blueprints.PropertyGraph.InMemory.ReadOnly
{

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    public class ReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                        TEdgeCollection, THyperEdgeCollection>

                                : AReadOnlyGraphElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex, TDatastructureVertex>,

                                  IReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>,

                                  IDynamicGraphElement<ReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                                              TEdgeCollection, THyperEdgeCollection>>

        where TIdVertex               : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge                 : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdHyperEdge            : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TRevisionIdVertex       : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge         : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdHyperEdge    : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

        where TEdgeLabel              : IEquatable<TEdgeLabel>,           IComparable<TEdgeLabel>,           IComparable
        where THyperEdgeLabel         : IEquatable<THyperEdgeLabel>,      IComparable<THyperEdgeLabel>,      IComparable

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

        where TDatastructureVertex    : IDictionary<TKeyVertex,    TValueVertex>
        where TDatastructureEdge      : IDictionary<TKeyEdge,      TValueEdge>
        where TDatastructureHyperEdge : IDictionary<TKeyHyperEdge, TValueHyperEdge>

        where TEdgeCollection         : ICollection<IReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

        where THyperEdgeCollection    : ICollection<IReadOnlyPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

    {

        #region Data

        /// <summary>
        /// The edges emanating from, or leaving, this vertex.
        /// </summary>
        protected readonly TEdgeCollection _OutEdges;

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex.
        /// </summary>
        protected readonly TEdgeCollection _InEdges;

        /// <summary>
        /// Cached number of OutEdges.
        /// </summary>
        protected UInt64 _NumberOfOutEdges;

        /// <summary>
        /// Cached number of InEdges.
        /// </summary>
        protected UInt64 _NumberOfInEdges;

        #endregion

        #region Properties

        #region ReadOnlyGraph

        /// <summary>
        /// The associated read-only property graph.
        /// </summary>
        public IReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,  
                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,    
                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> ReadOnlyGraph { get; private set; }

        #endregion

        #endregion

        #region OutEdge methods...

        #region OutEdges(params EdgeLabels)     // OutEdges()!

        /// <summary>
        /// The edges emanating from, or leaving, this vertex
        /// filtered by their label. If no label was given,
        /// all edges will be returned.
        /// </summary>
        public IEnumerable<IReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            OutEdges(params TEdgeLabel[] EdgeLabels)

        {

            if (EdgeLabels != null && EdgeLabels.Any())
            {
                foreach (var _Edge in _OutEdges)
                    foreach (var _Label in EdgeLabels)
                        if (_Edge.Label.Equals(_Label))
                            yield return _Edge;

            }

            else
                foreach (var _Edge in _OutEdges)
                    yield return _Edge;

        }

        #endregion

        #region OutEdges(EdgeFilter)

        /// <summary>
        /// The edges emanating from, or leaving, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        public IEnumerable<IReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            OutEdges(ReadOnlyEdgeFilter<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex,
                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)
        
        {
            return from _Edge in _OutEdges where EdgeFilter(_Edge) select _Edge;
        }

        #endregion

        #region OutDegree(params EdgeLabels)     // OutDegree()!

        /// <summary>
        /// The number of edges emanating from, or leaving, this vertex
        /// filtered by their label. If no label was given,
        /// all edges will be returned.
        /// </summary>
        public UInt64 OutDegree(params TEdgeLabel[] EdgeLabels)
        {
            
            if (EdgeLabels.Length == 0)
                return _NumberOfOutEdges;
            
            return (UInt64) OutEdges(EdgeLabels).Count();

        }

        #endregion

        #region OutDegree(EdgeFilter)

        /// <summary>
        /// The number of edges emanating from, or leaving, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        public UInt64 OutDegree(ReadOnlyEdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)
        {
            return (UInt64) OutEdges(EdgeFilter).Count();
        }

        #endregion

        #endregion

        #region InEdge methods...

        #region InEdges(params EdgeLabels)     // InEdges()!

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex
        /// filtered by their label. If no label was given,
        /// all edges will be returned.
        /// </summary>
        public IEnumerable<IReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            InEdges(params TEdgeLabel[] EdgeLabels)

        {

            if (EdgeLabels != null && EdgeLabels.Any())
                foreach (var _Edge in _InEdges)
                    yield return _Edge;

            else
                foreach (var _Edge in _InEdges)
                    foreach (var _Label in EdgeLabels)
                        if (_Edge.Label.Equals(_Label))
                            yield return _Edge;

        }

        #endregion

        #region InEdges(EdgeFilter)

        /// <summary>
        /// The edges incoming to, or arriving at, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        public IEnumerable<IReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
            InEdges(ReadOnlyEdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)
        
        {
            return from _Edge in _OutEdges where EdgeFilter(_Edge) select _Edge;
        }

        #endregion

        #region InDegree(params EdgeLabels)     // InDegree()!

        /// <summary>
        /// The number of edges incoming to, or arriving at, this vertex
        /// filtered by their label. If no label was given,
        /// all edges will be returned.
        /// </summary>
        public UInt64 InDegree(params TEdgeLabel[] EdgeLabels)
        {

            if (EdgeLabels.Length == 0)
                return _NumberOfInEdges;

            return (UInt64) InEdges(EdgeLabels).Count();

        }

        #endregion

        #region InDegree(EdgeFilter)

        /// <summary>
        /// The number of edges incoming to, or arriving at, this vertex
        /// filtered by the given edge filter delegate.
        /// </summary>
        public UInt64 InDegree(ReadOnlyEdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter)
        {
            return (UInt64) InEdges(EdgeFilter).Count();
        }

        #endregion

        #endregion

        #region HyperEdge methods...

        #region HyperEdges(params HyperEdgeLabels)      // HyperEdges()!

        /// <summary>
        /// The hyperedges emanating from, or leaving, this vertex
        /// filtered by their label. If no label was given,
        /// all hyperedges will be returned.
        /// </summary>
        public IEnumerable<IReadOnlyPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

            HyperEdges(params THyperEdgeLabel[] HyperEdgeLabels)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region HyperEdges(HyperEdgeFilter)

        /// <summary>
        /// The hyperedges emanating from, or leaving, this vertex
        /// filtered by the given hyperedge filter delegate.
        /// </summary>
        /// <param name="HyperEdgeFilter">A delegate for hyperedge filtering.</param>
        public IEnumerable<IReadOnlyPropertyHyperEdge<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
            HyperEdges(ReadOnlyHyperEdgeFilter<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> HyperEdgeFilter)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion


        #region Constructor(s)

        #region ReadOnlyPropertyVertex(ReadOnlyGraph, Vertex, DatastructureInitializer, EdgeCollectionInitializer)

        /// <summary>
        /// Creates a new vertex.
        /// </summary>
        /// <param name="Graph">The associated property graph.</param>
        /// <param name="Vertex">A property vertex to copy the data from.</param>
        /// <param name="DatastructureInitializer">A delegate to initialize the datastructure of this vertex.</param>
        /// <param name="EdgeCollectionInitializer">A delegate to initialize the datastructure for storing all edges.</param>
        public ReadOnlyPropertyVertex(IReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,  
                                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph,
                                      IPropertyVertex       <TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,  
                                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Vertex,
                                      Func<TDatastructureVertex>  DatastructureInitializer,
                                      Func<TEdgeCollection>       EdgeCollectionInitializer)

            : base(Vertex.Id, Vertex.IdKey, Vertex.RevIdKey, DatastructureInitializer)

        {

            if (Graph == null)
                throw new ArgumentNullException("The given property graph must not be null!");

            if (Vertex == null)
                throw new ArgumentNullException("The given property vertex must not be null!");

            this.ReadOnlyGraph = Graph;

            _OutEdges = EdgeCollectionInitializer();
            _InEdges  = EdgeCollectionInitializer();

        }

        #endregion

        #endregion


        #region Operator overloading

        #region Operator == (ReadOnlyPropertyVertex1, ReadOnlyPropertyVertex2)

        /// <summary>
        /// Compares two vertices for equality.
        /// </summary>
        /// <param name="ReadOnlyPropertyVertex1">A vertex.</param>
        /// <param name="ReadOnlyPropertyVertex2">Another vertex.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public static Boolean operator == (ReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                                  TEdgeCollection, THyperEdgeCollection> ReadOnlyPropertyVertex1,
                                           ReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                                  TEdgeCollection, THyperEdgeCollection> ReadOnlyPropertyVertex2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(ReadOnlyPropertyVertex1, ReadOnlyPropertyVertex2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) ReadOnlyPropertyVertex1 == null) || ((Object) ReadOnlyPropertyVertex2 == null))
                return false;

            return ReadOnlyPropertyVertex1.Equals(ReadOnlyPropertyVertex2);

        }

        #endregion

        #region Operator != (ReadOnlyPropertyVertex1, ReadOnlyPropertyVertex2)

        /// <summary>
        /// Compares two vertices for inequality.
        /// </summary>
        /// <param name="ReadOnlyPropertyVertex1">A vertex.</param>
        /// <param name="ReadOnlyPropertyVertex2">Another vertex.</param>
        /// <returns>False if both match; True otherwise.</returns>
        public static Boolean operator != (ReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                                  TEdgeCollection, THyperEdgeCollection> ReadOnlyPropertyVertex1,
                                           ReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                                  TEdgeCollection, THyperEdgeCollection> ReadOnlyPropertyVertex2)
        {
            return !(ReadOnlyPropertyVertex1 == ReadOnlyPropertyVertex2);
        }

        #endregion

        #region Operator <  (ReadOnlyPropertyVertex1, ReadOnlyPropertyVertex2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ReadOnlyPropertyVertex1">A Vertex.</param>
        /// <param name="ReadOnlyPropertyVertex2">Another Vertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (ReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                                 TEdgeCollection, THyperEdgeCollection> ReadOnlyPropertyVertex1,
                                          ReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                                 TEdgeCollection, THyperEdgeCollection> ReadOnlyPropertyVertex2)
        {

            if ((Object) ReadOnlyPropertyVertex1 == null)
                throw new ArgumentNullException("The given ReadOnlyPropertyVertex1 must not be null!");

            if ((Object) ReadOnlyPropertyVertex2 == null)
                throw new ArgumentNullException("The given ReadOnlyPropertyVertex2 must not be null!");

            return ReadOnlyPropertyVertex1.CompareTo(ReadOnlyPropertyVertex2) < 0;

        }

        #endregion

        #region Operator <= (ReadOnlyPropertyVertex1, ReadOnlyPropertyVertex2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ReadOnlyPropertyVertex1">A Vertex.</param>
        /// <param name="ReadOnlyPropertyVertex2">Another Vertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (ReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                                  TEdgeCollection, THyperEdgeCollection> ReadOnlyPropertyVertex1,
                                           ReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                                  TEdgeCollection, THyperEdgeCollection> ReadOnlyPropertyVertex2)
        {
            return !(ReadOnlyPropertyVertex1 > ReadOnlyPropertyVertex2);
        }

        #endregion

        #region Operator >  (ReadOnlyPropertyVertex1, ReadOnlyPropertyVertex2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ReadOnlyPropertyVertex1">A Vertex.</param>
        /// <param name="ReadOnlyPropertyVertex2">Another Vertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (ReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                                 TEdgeCollection, THyperEdgeCollection> ReadOnlyPropertyVertex1,
                                          ReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                                 TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                 TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                                 TEdgeCollection, THyperEdgeCollection> ReadOnlyPropertyVertex2)
        {

            if ((Object) ReadOnlyPropertyVertex1 == null)
                throw new ArgumentNullException("The given ReadOnlyPropertyVertex1 must not be null!");

            if ((Object) ReadOnlyPropertyVertex2 == null)
                throw new ArgumentNullException("The given ReadOnlyPropertyVertex2 must not be null!");

            return ReadOnlyPropertyVertex1.CompareTo(ReadOnlyPropertyVertex2) > 0;

        }

        #endregion

        #region Operator >= (ReadOnlyPropertyVertex1, ReadOnlyPropertyVertex2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ReadOnlyPropertyVertex1">A Vertex.</param>
        /// <param name="ReadOnlyPropertyVertex2">Another Vertex.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (ReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                                  TEdgeCollection, THyperEdgeCollection> ReadOnlyPropertyVertex1,
                                           ReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                                  TEdgeCollection, THyperEdgeCollection> ReadOnlyPropertyVertex2)
        {
            return !(ReadOnlyPropertyVertex1 < ReadOnlyPropertyVertex2);
        }

        #endregion

        #endregion

        #region IDynamicGraphObject<PropertyVertex> Members

        #region GetMetaObject(myExpression)

        /// <summary>
        /// Return the appropriate DynamicMetaObject.
        /// </summary>
        /// <param name="myExpression">An Expression.</param>
        public DynamicMetaObject GetMetaObject(Expression myExpression)
        {
            return new DynamicGraphElement<ReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                                  TEdgeCollection, THyperEdgeCollection>>(myExpression, this);
        }

        #endregion

        #region GetDynamicMemberNames()

        /// <summary>
        /// Returns an enumeration of all property keys.
        /// </summary>
        public virtual IEnumerable<String> GetDynamicMemberNames()
        {
            foreach (var _PropertyKey in PropertyData.Keys)
                yield return _PropertyKey.ToString();
        }

        #endregion


        #region SetMember(myBinder, myObject)

        /// <summary>
        /// Sets a new property or overwrites an existing.
        /// </summary>
        /// <param name="myBinder">The property key</param>
        /// <param name="myObject">The property value</param>
        public virtual Object SetMember(String myBinder, Object myObject)
        {
            throw new NotSupportedException("This data structure is read-only!");
        }

        #endregion

        #region GetMember(myBinder)

        /// <summary>
        /// Returns the value of the given property key.
        /// </summary>
        /// <param name="myBinder">The property key.</param>
        public virtual Object GetMember(String myBinder)
        {
            TValueVertex myObject;
            PropertyData.GetProperty((TKeyVertex) (Object) myBinder, out myObject);
            return myObject as Object;
        }

        #endregion

        #region DeleteMember(myBinder)

        /// <summary>
        /// Tries to remove the property identified by the given property key.
        /// </summary>
        /// <param name="myBinder">The property key.</param>
        public virtual Object DeleteMember(String myBinder)
        {
            throw new NotSupportedException("This data structure is read-only!");
        }

        #endregion

        #endregion

        #region IComparable Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public Int32 CompareTo(Object Object)
        {

            if ((Object) Object == null)
                throw new ArgumentNullException("The given Object must not be null!");

            return CompareTo((TIdVertex) Object);

        }

        #endregion

        #region CompareTo(VertexId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VertexId">An object to compare with.</param>
        public Int32 CompareTo(TIdVertex VertexId)
        {

            if ((Object) VertexId == null)
                throw new ArgumentNullException("The given VertexId must not be null!");

            return Id.CompareTo(VertexId);

        }

        #endregion

        #region CompareTo(IReadOnlyGraphElement)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="IReadOnlyGraphElement">An object to compare with.</param>
        public Int32 CompareTo(IReadOnlyGraphElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex> IReadOnlyGraphElement)
        {

            if ((Object) IReadOnlyGraphElement == null)
                throw new ArgumentNullException("The given IReadOnlyGraphElement must not be null!");

            return Id.CompareTo(IReadOnlyGraphElement.PropertyData[IdKey]);

        }

        #endregion

        #region CompareTo(IGraphElement)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="IPropertyElement">An object to compare with.</param>
        public Int32 CompareTo(IGraphElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex> IPropertyElement)
        {

            if ((Object) IPropertyElement == null)
                throw new ArgumentNullException("The given IPropertyElement must not be null!");

            return Id.CompareTo(IPropertyElement.PropertyData[IdKey]);

        }

        #endregion

        #region CompareTo(IReadOnlyPropertyVertex)

        /// <summary>
        /// Compares two read-only property vertices.
        /// </summary>
        /// <param name="IPropertyVertex">A property vertex to compare with.</param>
        public Int32 CompareTo(IReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IReadOnlyPropertyVertex)
        {
            
            if ((Object) IReadOnlyPropertyVertex == null)
                throw new ArgumentNullException("The given IReadOnlyPropertyVertex must not be null!");

            return Id.CompareTo(IReadOnlyPropertyVertex.PropertyData[IdKey]);

        }
        
        #endregion

        #region CompareTo(IPropertyVertex)

        /// <summary>
        /// Compares two property vertices.
        /// </summary>
        /// <param name="IPropertyVertex">A property vertex to compare with.</param>
        public Int32 CompareTo(IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IPropertyVertex)
        {
            
            if ((Object) IPropertyVertex == null)
                throw new ArgumentNullException("The given IPropertyVertex must not be null!");

            return Id.CompareTo(IPropertyVertex.PropertyData[IdKey]);

        }
        
        #endregion

        #endregion

        #region IEquatable Members

        #region Equals(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public override Boolean Equals(Object Object)
        {

            if (Object == null)
                return false;

            // Check if the given object can be casted to a PropertyVertex
            var PropertyVertex = Object as ReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge,
                                                                  TEdgeCollection, THyperEdgeCollection>;
            if ((Object) PropertyVertex == null)
                return false;

            return this.Equals(PropertyVertex);

        }

        #endregion

        #region Equals(VertexId)

        /// <summary>
        /// Compares this property vertex to a vertex identification.
        /// </summary>
        /// <param name="VertexId">A vertex identification to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(TIdVertex VertexId)
        {

            if ((Object) VertexId == null)
                return false;

            return Id.Equals(VertexId);

        }

        #endregion

        #region Equals(IReadOnlyGraphElement)

        /// <summary>
        /// Compares this property graph to another graph element.
        /// </summary>
        /// <param name="IReadOnlyGraphElement">An object to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IReadOnlyGraphElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex> IReadOnlyGraphElement)
        {

            if ((Object) IReadOnlyGraphElement == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return Id.Equals(IReadOnlyGraphElement.PropertyData[IdKey]);

        }

        #endregion

        #region Equals(IGraphElement)

        /// <summary>
        /// Compares this property graph to another property element.
        /// </summary>
        /// <param name="IPropertyElement">An object to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IGraphElement<TIdVertex, TRevisionIdVertex, TKeyVertex, TValueVertex> IPropertyElement)
        {

            if ((Object) IPropertyElement == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return Id.Equals(IPropertyElement.PropertyData[IdKey]);

        }

        #endregion

        #region Equals(IReadOnlyPropertyVertex)

        /// <summary>
        /// Compares two read-only property vertices for equality.
        /// </summary>
        /// <param name="IReadOnlyPropertyVertex">A read-only property vertex to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IReadOnlyPropertyVertex)
        {
            
            if ((Object) IReadOnlyPropertyVertex == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return Id.Equals(IReadOnlyPropertyVertex.PropertyData[IdKey]);

        }

        #endregion

        #region Equals(IPropertyVertex)

        /// <summary>
        /// Compares two property vertices for equality.
        /// </summary>
        /// <param name="IPropertyVertex">A property vertex to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IPropertyVertex<TIdVertex,    TRevisionIdVertex,                     TKeyVertex,    TValueVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IPropertyVertex)
        {
            
            if ((Object) IPropertyVertex == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return Id.Equals(IPropertyVertex.PropertyData[IdKey]);

        }

        #endregion

        #endregion

        #region GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override Int32 GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return "ReadOnlyPropertyVertex [Id: " + Id.ToString() + ", " + _OutEdges.Count + " OutEdges, " + _InEdges.Count + " InEdges]";
        }

        #endregion

    }

}
