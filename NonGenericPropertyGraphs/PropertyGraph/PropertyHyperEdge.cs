/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Blueprints.NET <http://www.github.com/Vanaheimr/Blueprints.NET>
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
using System.Dynamic;
using System.Linq.Expressions;
using System.Collections.Generic;

using de.ahzf.Illias.Commons;

#endregion

namespace de.ahzf.Vanaheimr.Blueprints.InMemory
{

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    /// <typeparam name="TIdVertex">The type of the vertex identifiers.</typeparam>
    /// <typeparam name="TRevIdVertex">The type of the vertex revision identifiers.</typeparam>
    /// <typeparam name="TVertexLabel">The type of the vertex type.</typeparam>
    /// <typeparam name="TKeyVertex">The type of the vertex property keys.</typeparam>
    /// <typeparam name="TValueVertex">The type of the vertex property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionVertex">A data structure to store the properties of the vertices.</typeparam>
    /// 
    /// <typeparam name="TIdEdge">The type of the edge identifiers.</typeparam>
    /// <typeparam name="TRevIdEdge">The type of the edge revision identifiers.</typeparam>
    /// <typeparam name="TEdgeLabel">The type of the edge label.</typeparam>
    /// <typeparam name="TKeyEdge">The type of the edge property keys.</typeparam>
    /// <typeparam name="TValueEdge">The type of the edge property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionEdge">A data structure to store the properties of the edges.</typeparam>
    /// 
    /// <typeparam name="TIdMultiEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevIdMultiEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="TMultiEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TKeyMultiEdge">The type of the hyperedge property keys.</typeparam>
    /// <typeparam name="TValueMultiEdge">The type of the hyperedge property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionMultiEdge">A data structure to store the properties of the hyperedges.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the hyperedge identifiers.</typeparam>
    /// <typeparam name="TRevIdHyperEdge">The type of the hyperedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the hyperedge label.</typeparam>
    /// <typeparam name="TKeyHyperEdge">The type of the hyperedge property keys.</typeparam>
    /// <typeparam name="TValueHyperEdge">The type of the hyperedge property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionHyperEdge">A data structure to store the properties of the hyperedges.</typeparam>
    /// 
    /// <typeparam name="TVerticesCollection">A data structure to store vertices.</typeparam>
    public class PropertyHyperEdge : GenericPropertyHyperEdge<UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object>,

                                     IPropertyHyperEdge,
                                     IDynamicGraphElement<PropertyHyperEdge>

    {

        #region Properties

        #region Graph

        /// <summary>
        /// The associated property graph.
        /// </summary>
        IPropertyGraph IPropertyHyperEdge.Graph
        {
            get
            {
                return Graph as IPropertyGraph;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region PropertyHyperEdge(Graph, OutVertex, InVertex, EdgeId, Label, IdKey, RevIdKey, DatastructureInitializer, EdgeInitializer = null)

        /// <summary>
        /// Creates a new hyperedge.
        /// </summary>
        /// <param name="Graph">The associated property graph.</param>
        /// <param name="Vertices">The vertices connected by this hyperedge.</param>
        /// <param name="MultiEdgeId">The identification of this hyperedge.</param>
        /// <param name="Label">A label stored within this hyperedge.</param>
        /// <param name="IdKey">The key of the hyperedge identifier.</param>
        /// <param name="RevIdKey">The key of the hyperedge revision identifier.</param>
        /// <param name="LabelKey">The key to access the Label of this graph element.</param>
        /// <param name="DatastructureInitializer">A delegate to initialize the properties datastructure.</param>
        /// <param name="VerticesCollectionInitializer">A delegate to initialize the datastructure for storing the vertices.</param>
        /// <param name="MultiEdgeInitializer">A delegate to initialize the newly created hyperedge.</param>
        public PropertyHyperEdge(IPropertyGraph               Graph,

                                 UInt64                       HyperEdgeId,
                                 String                       Label,
                                 String                       IdKey,
                                 String                       RevIdKey,
                                 String                       LabelKey,
                                 String                       DescriptionKey,

                                 IDictionaryInitializer<String, Object> DatastructureInitializer,

                                 VertexCollectionInitializer<UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object,
                                                             UInt64, Int64, String, String, Object> VerticesCollectionInitializer,

                                 HyperEdgeInitializer<UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object> HyperEdgeInitializer = null,

                                 VertexFilter<UInt64, Int64, String, String, Object,
                                              UInt64, Int64, String, String, Object,
                                              UInt64, Int64, String, String, Object,
                                              UInt64, Int64, String, String, Object> VertexSelector = null,

                                 IEnumerable<IReadOnlyGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                            UInt64, Int64, String, String, Object,
                                                                            UInt64, Int64, String, String, Object,
                                                                            UInt64, Int64, String, String, Object>> Vertices = null)

            : base(Graph, HyperEdgeId, Label, IdKey, RevIdKey, LabelKey, DescriptionKey, DatastructureInitializer, VerticesCollectionInitializer, HyperEdgeInitializer, VertexSelector, Vertices)

        { }

        #endregion

        #endregion



        #region Operator overloading

        #region Operator == (PropertyHyperEdge1, PropertyHyperEdge2)

        /// <summary>
        /// Compares two edges for equality.
        /// </summary>
        /// <param name="PropertyHyperEdge1">A Edge.</param>
        /// <param name="PropertyHyperEdge2">Another Edge.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public static Boolean operator == (PropertyHyperEdge PropertyHyperEdge1,
                                           PropertyHyperEdge PropertyHyperEdge2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(PropertyHyperEdge1, PropertyHyperEdge2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) PropertyHyperEdge1 == null) || ((Object) PropertyHyperEdge2 == null))
                return false;

            return PropertyHyperEdge1.Equals(PropertyHyperEdge2);

        }

        #endregion

        #region Operator != (PropertyHyperEdge1, PropertyHyperEdge2)

        /// <summary>
        /// Compares two edges for equality.
        /// </summary>
        /// <param name="PropertyHyperEdge1">A Edge.</param>
        /// <param name="PropertyHyperEdge2">Another Edge.</param>
        /// <returns>False if both match; True otherwise.</returns>
        public static Boolean operator != (PropertyHyperEdge PropertyHyperEdge1,
                                           PropertyHyperEdge PropertyHyperEdge2)
        {
            return !(PropertyHyperEdge1 == PropertyHyperEdge2);
        }

        #endregion

        #region Operator <  (PropertyHyperEdge1, PropertyHyperEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyHyperEdge1">A Edge.</param>
        /// <param name="PropertyHyperEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <  (PropertyHyperEdge PropertyHyperEdge1,
                                           PropertyHyperEdge PropertyHyperEdge2)
        {

            if ((Object) PropertyHyperEdge1 == null)
                throw new ArgumentNullException("The given PropertyHyperEdge1 must not be null!");

            if ((Object) PropertyHyperEdge2 == null)
                throw new ArgumentNullException("The given PropertyHyperEdge2 must not be null!");

            return PropertyHyperEdge1.CompareTo(PropertyHyperEdge2) < 0;

        }

        #endregion

        #region Operator <= (PropertyHyperEdge1, PropertyHyperEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyHyperEdge1">A Edge.</param>
        /// <param name="PropertyHyperEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (PropertyHyperEdge PropertyHyperEdge1,
                                           PropertyHyperEdge PropertyHyperEdge2)
        {
            return !(PropertyHyperEdge1 > PropertyHyperEdge2);
        }

        #endregion

        #region Operator >  (PropertyHyperEdge1, PropertyHyperEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyHyperEdge1">A Edge.</param>
        /// <param name="PropertyHyperEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >  (PropertyHyperEdge PropertyHyperEdge1,
                                           PropertyHyperEdge PropertyHyperEdge2)
        {

            if ((Object) PropertyHyperEdge1 == null)
                throw new ArgumentNullException("The given PropertyHyperEdge1 must not be null!");

            if ((Object) PropertyHyperEdge2 == null)
                throw new ArgumentNullException("The given PropertyHyperEdge2 must not be null!");

            return PropertyHyperEdge1.CompareTo(PropertyHyperEdge2) > 0;

        }

        #endregion

        #region Operator >= (PropertyHyperEdge1, PropertyHyperEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyHyperEdge1">A Edge.</param>
        /// <param name="PropertyHyperEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (PropertyHyperEdge PropertyHyperEdge1,
                                           PropertyHyperEdge PropertyHyperEdge2)
        {
            return !(PropertyHyperEdge1 < PropertyHyperEdge2);
        }

        #endregion

        #endregion

        #region IDynamicGraphObject<PropertyHyperEdge> Members

        #region GetMetaObject(Expression)

        /// <summary>
        /// Return the appropriate DynamicMetaObject.
        /// </summary>
        /// <param name="Expression">An Expression.</param>
        public override DynamicMetaObject GetMetaObject(Expression Expression)
        {
            return new DynamicGraphElement<PropertyHyperEdge>(Expression, this);
        }

        #endregion

        #endregion

        #region IComparable Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public override Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given Object must not be null!");

            // Check if the given object can be casted to a PropertyHyperEdge
            var PropertyHyperEdge = Object as PropertyHyperEdge;

            if ((Object) PropertyHyperEdge == null)
                throw new ArgumentException("The given object is not a PropertyHyperEdge!");

            return CompareTo(PropertyHyperEdge);

        }

        #endregion

        #region CompareTo(IPropertyHyperEdge)

        /// <summary>
        /// Compares two generic property hyperedges.
        /// </summary>
        /// <param name="IGenericPropertyHyperEdge">A generic property hyperedge to compare with.</param>
        public Int32 CompareTo(IPropertyHyperEdge IPropertyHyperEdge)
        {

            if ((Object)IPropertyHyperEdge == null)
                throw new ArgumentNullException("The given IPropertyHyperEdge must not be null!");

            return Id.CompareTo(IPropertyHyperEdge[IdKey]);

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

            // Check if the given object can be casted to a PropertyHyperEdge
            var PropertyHyperEdge = Object as PropertyHyperEdge;

            if ((Object) PropertyHyperEdge == null)
                return false;

            return this.Equals(PropertyHyperEdge);

        }

        #endregion

        #region Equals(IPropertyHyperEdge)

        /// <summary>
        /// Compares two generic property hyperedges for equality.
        /// </summary>
        /// <param name="IGenericPropertyHyperEdge">A property edge to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IPropertyHyperEdge IPropertyHyperEdge)
        {

            if ((Object) IPropertyHyperEdge == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return Id.Equals(IPropertyHyperEdge[IdKey]);

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
        /// Return a string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return "PropertyHyperEdge [Id: '" + Id.ToString() + "']";
        }

        #endregion


        public new IReadOnlyGenericPropertyGraph<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> Graph
        {
            get { throw new NotImplementedException(); }
        }

        public IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> VertexById(ulong VertexId)
        {
            throw new NotImplementedException();
        }

        public bool TryGetVertexById(ulong VertexId, out IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> Vertex)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> VerticesById(params ulong[] VertexIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> VerticesByLabel(params string[] VertexLabels)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> Vertices(VertexFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> VertexFilter = null)
        {
            throw new NotImplementedException();
        }


        public new System.Collections.IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> IReadOnlyVertexMethods<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.VertexById(ulong VertexId)
        {
            throw new NotImplementedException();
        }

        public bool TryGetVertexById(ulong VertexId, out IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> Vertex)
        {
            throw new NotImplementedException();
        }

        IEnumerable<IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> IReadOnlyVertexMethods<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>.VerticesById(params ulong[] VertexIds)
        {
            throw new NotImplementedException();
        }

        public ulong NumberOfVertices(VertexFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> VertexFilter = null)
        {
            throw new NotImplementedException();
        }

        public IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddVertex(IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> Vertex)
        {
            throw new NotImplementedException();
        }

        public IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddVertexIfNotExists(IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> Vertex, VertexFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> CheckExistanceDelegate = null)
        {
            throw new NotImplementedException();
        }

        public void RemoveVerticesById(params ulong[] VertexIds)
        {
            throw new NotImplementedException();
        }

        public void RemoveVertices(params IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>[] Vertices)
        {
            throw new NotImplementedException();
        }

        public void RemoveVertices(VertexFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> VertexFilter = null)
        {
            throw new NotImplementedException();
        }






        public IEnumerable<IReadOnlyGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> HyperEdges(params string[] HyperEdgeLabels)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IReadOnlyGenericPropertyHyperEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> HyperEdges(HyperEdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> HyperEdgeFilter)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddVertex(IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> Vertex)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> AddVertexIfNotExists(IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> Vertex, CheckVertexExistance<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> CheckExistanceDelegate = null)
        {
            throw new NotImplementedException();
        }

        public void RemoveVertices(params IReadOnlyGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>[] Vertices)
        {
            throw new NotImplementedException();
        }
    }

}
