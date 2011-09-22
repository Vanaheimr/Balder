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
using System.Dynamic;
using System.Linq.Expressions;

#endregion

namespace de.ahzf.Blueprints.PropertyGraphs.InMemory.Mutable
{

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevisionIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TVertexType">The type of the vertex type.</typeparam>
    /// <typeparam name="TKeyVertex">The type of the vertex property keys.</typeparam>
    /// <typeparam name="TValueVertex">The type of the vertex property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionVertex">A data structure to store the properties of the vertices.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
    /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionEdge">A data structure to store the properties of the edges.</typeparam>
    /// 
    /// <typeparam name="TIdMultiEdge">The type of the multiedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdMultiEdge">The type of the multiedge revision identifiers.</typeparam>
    /// <typeparam name="TMultiEdgeLabel">The type of the multiedge label.</typeparam>
    /// <typeparam name="TKeyMultiEdge">The type of the multiedge property keys.</typeparam>
    /// <typeparam name="TValueMultiEdge">The type of the multiedge property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionMultiEdge">A data structure to store the properties of the multiedges.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevisionIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
    /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionHyperEdge">A data structure to store the properties of the hyperedges.</typeparam>
    public class PropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
        
                              : AGraphElement<TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge, TPropertiesCollectionEdge>,

                                IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
                                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
                                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
                                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>,

                                IDynamicGraphElement<PropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
                                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
                                                                  TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
                                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>>


        where TIdVertex               : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge                 : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdMultiEdge            : IEquatable<TIdMultiEdge>,         IComparable<TIdMultiEdge>,         IComparable, TValueMultiEdge
        where TIdHyperEdge            : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TRevisionIdVertex       : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge         : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdMultiEdge    : IEquatable<TRevisionIdMultiEdge>, IComparable<TRevisionIdMultiEdge>, IComparable, TValueMultiEdge
        where TRevisionIdHyperEdge    : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

        where TVertexType             : IEquatable<TVertexType>,          IComparable<TVertexType>,          IComparable
        where TEdgeLabel              : IEquatable<TEdgeLabel>,           IComparable<TEdgeLabel>,           IComparable
        where TMultiEdgeLabel         : IEquatable<TMultiEdgeLabel>,      IComparable<TMultiEdgeLabel>,      IComparable
        where THyperEdgeLabel         : IEquatable<THyperEdgeLabel>,      IComparable<THyperEdgeLabel>,      IComparable

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyMultiEdge           : IEquatable<TKeyMultiEdge>,        IComparable<TKeyMultiEdge>,        IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

        where TPropertiesCollectionVertex    : IDictionary<TKeyVertex,    TValueVertex>
        where TPropertiesCollectionEdge      : IDictionary<TKeyEdge,      TValueEdge>
        where TPropertiesCollectionMultiEdge : IDictionary<TKeyMultiEdge, TValueMultiEdge>
        where TPropertiesCollectionHyperEdge : IDictionary<TKeyHyperEdge, TValueHyperEdge>

    {

        #region Properties

        #region Graph

        /// <summary>
        /// The associated property graph.
        /// </summary>
        public IPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                              TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                              TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                              TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph { get; private set; }

        #endregion

        #region Label

        /// <summary>
        /// The label associated with this edge.
        /// </summary>
        public TEdgeLabel Label { get; private set; }

        #endregion

        #region OutVertex

        /// <summary>
        /// The vertex at the tail of this edge.
        /// </summary>
        public IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                               OutVertex { get; private set; }

        #endregion

        #region InVertex

        /// <summary>
        /// The vertex at the head of this edge.
        /// </summary>
        public IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                               TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                               TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                               TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                               InVertex { get; private set; }

        #endregion

        #endregion

        #region Constructor(s)

        #region PropertyEdge(Graph, OutVertex, InVertex, EdgeId, Label, IdKey, RevisionIdKey, PropertiesCollectionInitializer, EdgeInitializer = null)

        /// <summary>
        /// Creates a new edge.
        /// </summary>
        /// <param name="Graph">The associated property graph.</param>
        /// <param name="OutVertex">The vertex at the tail of the edge.</param>
        /// <param name="InVertex">The vertex at the head of the edge.</param>
        /// <param name="EdgeId">The identification of this edge.</param>
        /// <param name="Label">A label stored within this edge.</param>
        /// <param name="IdKey">The key of the edge identifier.</param>
        /// <param name="RevisonIdKey">The key of the edge revision identifier.</param>
        /// <param name="PropertiesCollectionInitializer">A delegate to initialize the properties datastructure.</param>
        /// <param name="EdgeInitializer">A delegate to initialize the newly created edge.</param>
        public PropertyEdge(IPropertyGraph <TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph,
                            IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                            OutVertex,

                            IPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                            InVertex,

                            TIdEdge                         EdgeId,
                            TEdgeLabel                      Label,
                            TKeyEdge                        IdKey,
                            TKeyEdge                        RevisonIdKey,
                            Func<TPropertiesCollectionEdge> PropertiesCollectionInitializer,

                            EdgeInitializer<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeInitializer = null)

            : base(EdgeId, IdKey, RevisonIdKey, PropertiesCollectionInitializer)

        {

            #region Initial checks

            if (Graph == null)
                throw new ArgumentNullException("The given graph must not be null!");

            if (EdgeId == null)
                throw new ArgumentNullException("The given EdgeId must not be null!");

            if (IdKey == null)
                throw new ArgumentNullException("The given IdKey must not be null!");

            if (RevisonIdKey == null)
                throw new ArgumentNullException("The given RevisonIdKey must not be null!");

            if (PropertiesCollectionInitializer == null)
                throw new ArgumentNullException("The given PropertiesCollectionInitializer must not be null!");

            if (OutVertex == null)
                throw new ArgumentNullException("The given OutVertex must not be null!");

            if (InVertex == null)
                throw new ArgumentNullException("The given InVertex must not be null!");

            #endregion

            this.Graph     = Graph;
            this.OutVertex = OutVertex;
            this.InVertex  = InVertex;
            this.Label     = Label;

            if (EdgeInitializer != null)
                EdgeInitializer(this);

        }

        #endregion

        #endregion


        #region Operator overloading

        #region Operator == (PropertyEdge1, PropertyEdge2)

        /// <summary>
        /// Compares two edges for equality.
        /// </summary>
        /// <param name="PropertyEdge1">A Edge.</param>
        /// <param name="PropertyEdge2">Another Edge.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public static Boolean operator == (PropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
                                                        TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
                                                        PropertyEdge1,
                                           PropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
                                                        TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
                                                        PropertyEdge2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(PropertyEdge1, PropertyEdge2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) PropertyEdge1 == null) || ((Object) PropertyEdge2 == null))
                return false;

            return PropertyEdge1.Equals(PropertyEdge2);

        }

        #endregion

        #region Operator != (PropertyEdge1, PropertyEdge2)

        /// <summary>
        /// Compares two edges for equality.
        /// </summary>
        /// <param name="PropertyEdge1">A Edge.</param>
        /// <param name="PropertyEdge2">Another Edge.</param>
        /// <returns>False if both match; True otherwise.</returns>
        public static Boolean operator != (PropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
                                                        TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
                                                        PropertyEdge1,
                                           PropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
                                                        TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
                                                        PropertyEdge2)
        {
            return !(PropertyEdge1 == PropertyEdge2);
        }

        #endregion

        #region Operator <  (PropertyEdge1, PropertyEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyEdge1">A Edge.</param>
        /// <param name="PropertyEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (PropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
                                                       TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
                                                       PropertyEdge1,
                                          PropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
                                                       TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
                                                       PropertyEdge2)
        {

            if ((Object) PropertyEdge1 == null)
                throw new ArgumentNullException("The given PropertyEdge1 must not be null!");

            if ((Object) PropertyEdge2 == null)
                throw new ArgumentNullException("The given PropertyEdge2 must not be null!");

            return PropertyEdge1.CompareTo(PropertyEdge2) < 0;

        }

        #endregion

        #region Operator <= (PropertyEdge1, PropertyEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyEdge1">A Edge.</param>
        /// <param name="PropertyEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (PropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
                                                        TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
                                                        PropertyEdge1,
                                           PropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
                                                        TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
                                                        PropertyEdge2)
        {
            return !(PropertyEdge1 > PropertyEdge2);
        }

        #endregion

        #region Operator >  (PropertyEdge1, PropertyEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyEdge1">A Edge.</param>
        /// <param name="PropertyEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (PropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
                                                       TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
                                                       PropertyEdge1,
                                          PropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
                                                       TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
                                                       PropertyEdge2)
        {

            if ((Object) PropertyEdge1 == null)
                throw new ArgumentNullException("The given PropertyEdge1 must not be null!");

            if ((Object) PropertyEdge2 == null)
                throw new ArgumentNullException("The given PropertyEdge2 must not be null!");

            return PropertyEdge1.CompareTo(PropertyEdge2) > 0;

        }

        #endregion

        #region Operator >= (PropertyEdge1, PropertyEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyEdge1">A Edge.</param>
        /// <param name="PropertyEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (PropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
                                                        TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
                                                        PropertyEdge1,
                                           PropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
                                                        TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>
                                                        PropertyEdge2)
        {
            return !(PropertyEdge1 < PropertyEdge2);
        }

        #endregion

        #endregion

        #region IDynamicGraphObject<PropertyEdge> Members

        #region GetMetaObject(myExpression)

        /// <summary>
        /// Return the appropriate DynamicMetaObject.
        /// </summary>
        /// <param name="myExpression">An Expression.</param>
        public DynamicMetaObject GetMetaObject(Expression myExpression)
        {
            return new DynamicGraphElement<PropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
                                                        TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>>
                                                        (myExpression, this);
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
            return PropertyData.SetProperty((TKeyEdge) (Object) myBinder, (TValueEdge) myObject);
        }

        #endregion

        #region GetMember(myBinder)

        /// <summary>
        /// Returns the value of the given property key.
        /// </summary>
        /// <param name="myBinder">The property key.</param>
        public virtual Object GetMember(String myBinder)
        {
            TValueEdge myObject;
            PropertyData.GetProperty((TKeyEdge) (Object) myBinder, out myObject);
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

            try
            {
                PropertyData.Remove((TKeyEdge) (Object) myBinder);
                return true;
            }
            catch
            { }

            return false;

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

            if (Object == null)
                throw new ArgumentNullException("The given Object must not be null!");

            // Check if the given object can be casted to a PropertyEdge
            var PropertyEdge = Object as PropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
                                                      TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>;

            if ((Object) PropertyEdge == null)
                throw new ArgumentException("The given object is not a PropertyEdge!");

            return CompareTo(PropertyEdge);

        }

        #endregion

        #region CompareTo(EdgeId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EdgeId">An object to compare with.</param>
        public Int32 CompareTo(TIdEdge EdgeId)
        {

            if ((Object) EdgeId == null)
                throw new ArgumentNullException("The given EdgeId must not be null!");

            return Id.CompareTo(EdgeId);

        }

        #endregion

        #region CompareTo(IGraphElement)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="IGraphElement">An object to compare with.</param>
        public Int32 CompareTo(IGraphElement<TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge> IGraphElement)
        {

            if ((Object) IGraphElement == null)
                throw new ArgumentNullException("The given IGraphElement must not be null!");

            return Id.CompareTo(IGraphElement.PropertyData[IdKey]);

        }

        #endregion

        #region CompareTo(IPropertyEdge)

        /// <summary>
        /// Compares two property edges.
        /// </summary>
        /// <param name="IPropertyEdge">A property edge to compare with.</param>
        public Int32 CompareTo(IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                             TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                             TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                             TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IPropertyEdge)
        {

            if ((Object) IPropertyEdge == null)
                throw new ArgumentNullException("The given IPropertyEdge must not be null!");

            return Id.CompareTo(IPropertyEdge.PropertyData[IdKey]);

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

            // Check if the given object can be casted to a PropertyEdge
            var PropertyEdge = Object as PropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TPropertiesCollectionVertex,
                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TPropertiesCollectionEdge,
                                                      TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge, TPropertiesCollectionMultiEdge,
                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TPropertiesCollectionHyperEdge>;

            if ((Object) PropertyEdge == null)
                return false;

            return this.Equals(PropertyEdge);

        }

        #endregion

        #region Equals(EdgeId)

        /// <summary>
        /// Compares this property edge to an edge identification.
        /// </summary>
        /// <param name="EdgeId">An edge identification to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(TIdEdge EdgeId)
        {

            if ((Object) EdgeId == null)
                return false;

            return Id.Equals(EdgeId);

        }

        #endregion

        #region Equals(IGraphElement)

        /// <summary>
        /// Compares this property edge to another property element.
        /// </summary>
        /// <param name="IGraphElement">An object to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IGraphElement<TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge> IGraphElement)
        {

            if ((Object) IGraphElement == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return Id.Equals(IGraphElement.PropertyData[IdKey]);

        }

        #endregion

        #region Equals(IPropertyEdge)

        /// <summary>
        /// Compares two property edges for equality.
        /// </summary>
        /// <param name="IPropertyEdge">A property edge to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                            TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                            TIdMultiEdge, TRevisionIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
                                            TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> IPropertyEdge)
        {

            if ((Object) IPropertyEdge == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return Id.Equals(IPropertyEdge.PropertyData[IdKey]);

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
            return "PropertyEdge [Id: " + Id.ToString() + ", '" + OutVertex.Id + "' -> '" + InVertex.Id + "']";
        }

        #endregion

    }

}
