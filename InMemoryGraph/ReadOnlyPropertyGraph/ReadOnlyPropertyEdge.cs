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
    public class ReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
        
                              : AReadOnlyGraphElement<TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge, TDatastructureEdge>,

                                IReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>,

                                IDynamicGraphElement<ReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                                          TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                          TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>


        where TIdVertex               : IEquatable<TIdVertex>,            IComparable<TIdVertex>,            IComparable, TValueVertex
        where TIdEdge                 : IEquatable<TIdEdge>,              IComparable<TIdEdge>,              IComparable, TValueEdge
        where TIdHyperEdge            : IEquatable<TIdHyperEdge>,         IComparable<TIdHyperEdge>,         IComparable, TValueHyperEdge

        where TRevisionIdVertex       : IEquatable<TRevisionIdVertex>,    IComparable<TRevisionIdVertex>,    IComparable, TValueVertex
        where TRevisionIdEdge         : IEquatable<TRevisionIdEdge>,      IComparable<TRevisionIdEdge>,      IComparable, TValueEdge
        where TRevisionIdHyperEdge    : IEquatable<TRevisionIdHyperEdge>, IComparable<TRevisionIdHyperEdge>, IComparable, TValueHyperEdge

        where TVertexType             : IEquatable<TVertexType>,          IComparable<TVertexType>,          IComparable
            where TEdgeLabel              : IEquatable<TEdgeLabel>,           IComparable<TEdgeLabel>,           IComparable
        where THyperEdgeLabel         : IEquatable<THyperEdgeLabel>,      IComparable<THyperEdgeLabel>,      IComparable

        where TKeyVertex              : IEquatable<TKeyVertex>,           IComparable<TKeyVertex>,           IComparable
        where TKeyEdge                : IEquatable<TKeyEdge>,             IComparable<TKeyEdge>,             IComparable
        where TKeyHyperEdge           : IEquatable<TKeyHyperEdge>,        IComparable<TKeyHyperEdge>,        IComparable

        where TDatastructureVertex    : IDictionary<TKeyVertex,    TValueVertex>
        where TDatastructureEdge      : IDictionary<TKeyEdge,      TValueEdge>
        where TDatastructureHyperEdge : IDictionary<TKeyHyperEdge, TValueHyperEdge>

    {

        #region Properties

        #region Graph

        /// <summary>
        /// The associated property graph.
        /// </summary>
        public IReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
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
        protected readonly IReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                                  _OutVertex;

        /// <summary>
        /// The vertex at the tail of this edge.
        /// </summary>
        public IReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                       OutVertex
        {
            get
            {
                return _OutVertex;
            }
        }

        #endregion

        #region InVertex

        /// <summary>
        /// The vertex at the head of this edge.
        /// </summary>
        protected readonly IReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                   TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                   TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                                   _InVertex;

        /// <summary>
        /// The vertex at the head of this edge.
        /// </summary>
        public IReadOnlyPropertyVertex<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>
                                       InVertex
        {
            get
            {
                return _InVertex;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region ReadOnlyPropertyEdge(Graph, Edge, InVertex, EdgeId, EdgeInitializer = null)

        /// <summary>
        /// Creates a new edge.
        /// </summary>
        /// <param name="Graph">The associated property graph.</param>
        /// <param name="Edge">A property edge to copy the data from.</param>
        /// <param name="DataInitializer">A func to initialize the properties datastructure.</param>
        public ReadOnlyPropertyEdge(IReadOnlyPropertyGraph<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                           TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                           TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Graph,
                                    IPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                  TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
                                                  TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge,

                            Func<TDatastructureEdge> DataInitializer)

            : base(Edge.Id, Edge.IdKey, Edge.RevIdKey, DataInitializer)

        {

            if (Graph == null)
                throw new ArgumentNullException("The given property graph must not be null!");

            if (Edge == null)
                throw new ArgumentNullException("The given property edge must not be null!");

            this.Graph      = Graph;
            this._OutVertex = Graph.VertexById(Edge.OutVertex.Id);
            this._InVertex  = Graph.VertexById(Edge.InVertex.Id);

            // Add the label
            this.Label      = Label;

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
        public static Boolean operator == (ReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                        PropertyEdge1,
                                           ReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
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
        public static Boolean operator != (ReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                        PropertyEdge1,
                                           ReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
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
        public static Boolean operator < (ReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                       PropertyEdge1,
                                          ReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
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
        public static Boolean operator <= (ReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                        PropertyEdge1,
                                           ReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
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
        public static Boolean operator > (ReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                       PropertyEdge1,
                                          ReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                       TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                       TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
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
        public static Boolean operator >= (ReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
                                                        PropertyEdge1,
                                           ReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                        TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                        TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>
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
            return new DynamicGraphElement<ReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                                TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                                TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>>
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

            if (Object == null)
                throw new ArgumentNullException("The given Object must not be null!");

            // Check if the given object can be casted to a PropertyEdge
            var PropertyEdge = Object as PropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>;
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

        #region CompareTo(IReadOnlyGraphElement)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="IGraphElement">An object to compare with.</param>
        public Int32 CompareTo(IReadOnlyGraphElement<TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge> IGraphElement)
        {

            if ((Object) IGraphElement == null)
                throw new ArgumentNullException("The given IGraphElement must not be null!");

            return Id.CompareTo(IGraphElement.PropertyData[IdKey]);

        }

        #endregion

        #region CompareTo(IReadOnlyPropertyEdge)

        /// <summary>
        /// Compares two property edges.
        /// </summary>
        /// <param name="IPropertyEdge">A property edge to compare with.</param>
        public Int32 CompareTo(IReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                     TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
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
            var PropertyEdge = Object as PropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,    TDatastructureVertex,
                                                      TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,      TDatastructureEdge,
                                                      TIdHyperEdge, TRevisionIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge, TDatastructureHyperEdge>;
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

        #region Equals(IReadOnlyGraphElement)

        /// <summary>
        /// Compares this property edge to another property element.
        /// </summary>
        /// <param name="IGraphElement">An object to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IReadOnlyGraphElement<TIdEdge, TRevisionIdEdge, TKeyEdge, TValueEdge> IGraphElement)
        {

            if ((Object) IGraphElement == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return Id.Equals(IGraphElement.PropertyData[IdKey]);

        }

        #endregion

        #region Equals(IReadOnlyPropertyEdge)

        /// <summary>
        /// Compares two property edges for equality.
        /// </summary>
        /// <param name="IPropertyEdge">A property edge to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IReadOnlyPropertyEdge<TIdVertex,    TRevisionIdVertex,    TVertexType,     TKeyVertex,    TValueVertex,
                                                    TIdEdge,      TRevisionIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
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
