/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Blueprints.NET <http://www.github.com/Vanaheimr/Balder>
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

using eu.Vanaheimr.Illias.Commons;

#endregion

namespace eu.Vanaheimr.Balder.InMemory
{

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    public class PropertyEdge : GenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                    UInt64, Int64, String, String, Object,
                                                    UInt64, Int64, String, String, Object,
                                                    UInt64, Int64, String, String, Object>,

                                IPropertyEdge,
                                IDynamicGraphElement<PropertyEdge>

    {

        #region Properties

        #region Graph

        /// <summary>
        /// The associated property graph.
        /// </summary>
        IPropertyGraph IPropertyEdge.Graph
        {
            get
            {
                return Graph as IPropertyGraph;
            }
        }

        #endregion

        #region OutVertex

        /// <summary>
        /// The vertex at the tail of this edge.
        /// </summary>
        IPropertyVertex IPropertyEdge.OutVertex
        {
            get
            {
                return OutVertex as IPropertyVertex;
            }
        }

        #endregion

        #region InVertex

        /// <summary>
        /// The vertex at the head of this edge.
        /// </summary>
        IPropertyVertex IPropertyEdge.InVertex
        {
            get
            {
                return InVertex as IPropertyVertex;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region PropertyEdge(Graph, OutVertex, InVertex, EdgeId, Label, IdKey, RevIdKey, DescriptionKey, DatastructureInitializer, EdgeInitializer = null)

        /// <summary>
        /// Creates a new generic property edge.
        /// </summary>
        /// <param name="Graph">The associated property graph.</param>
        /// <param name="OutVertex">The vertex at the tail of the edge.</param>
        /// <param name="InVertex">The vertex at the head of the edge.</param>
        /// <param name="EdgeId">The identification of this edge.</param>
        /// <param name="Label">A label stored within this edge.</param>
        /// <param name="IdKey">The key of the edge identifier.</param>
        /// <param name="RevIdKey">The key of the edge revision identifier.</param>
        /// <param name="LabelKey">The key to access the Label of this graph element.</param>
        /// <param name="DatastructureInitializer">A delegate to initialize the properties datastructure.</param>
        /// <param name="EdgeInitializer">A delegate to initialize the newly created edge.</param>
        public PropertyEdge(IPropertyGraph  Graph,
                            IPropertyVertex OutVertex,
                            IPropertyVertex InVertex,

                            UInt64          EdgeId,
                            String          Label,
                            String          IdKey,
                            String          RevIdKey,
                            String          LabelKey,
                            String          DescriptionKey,

                            IDictionaryInitializer<String, Object> DatastructureInitializer,

                            EdgeInitializer<UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object,
                                            UInt64, Int64, String, String, Object> EdgeInitializer = null)

            : base(Graph, OutVertex, InVertex, EdgeId, Label, IdKey, RevIdKey, LabelKey, DescriptionKey, DatastructureInitializer, EdgeInitializer)

        { }

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
        public static Boolean operator == (PropertyEdge PropertyEdge1,
                                           PropertyEdge PropertyEdge2)
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
        public static Boolean operator != (PropertyEdge PropertyEdge1,
                                           PropertyEdge PropertyEdge2)
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
        public static Boolean operator < (PropertyEdge PropertyEdge1,
                                          PropertyEdge PropertyEdge2)
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
        public static Boolean operator <= (PropertyEdge PropertyEdge1,
                                           PropertyEdge PropertyEdge2)
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
        public static Boolean operator > (PropertyEdge PropertyEdge1,
                                          PropertyEdge PropertyEdge2)
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
        public static Boolean operator >= (PropertyEdge PropertyEdge1,
                                           PropertyEdge PropertyEdge2)
        {
            return !(PropertyEdge1 < PropertyEdge2);
        }

        #endregion

        #endregion

        #region IDynamicGraphObject<PropertyEdge> Members

        #region GetMetaObject(Expression)

        /// <summary>
        /// Return the appropriate DynamicMetaObject.
        /// </summary>
        /// <param name="Expression">An Expression.</param>
        public override DynamicMetaObject GetMetaObject(Expression Expression)
        {
            return new DynamicGraphElement<PropertyEdge>(Expression, this);
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

            // Check if the given object can be casted to a PropertyEdge
            var PropertyEdge = Object as PropertyEdge;

            if ((Object) PropertyEdge == null)
                throw new ArgumentException("The given object is not a PropertyEdge!");

            return CompareTo(PropertyEdge);

        }

        #endregion

        #region CompareTo(IPropertyEdge)

        /// <summary>
        /// Compares two property edges.
        /// </summary>
        /// <param name="IPropertyEdge">A property edge to compare with.</param>
        public Int32 CompareTo(IPropertyEdge IPropertyEdge)
        {

            if ((Object) IPropertyEdge == null)
                throw new ArgumentNullException("The given IPropertyEdge must not be null!");

            return Id.CompareTo(IPropertyEdge[IdKey]);

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
            var PropertyEdge = Object as PropertyEdge;

            if ((Object) PropertyEdge == null)
                return false;

            return this.Equals(PropertyEdge);

        }

        #endregion

        #region Equals(IPropertyEdge)

        /// <summary>
        /// Compares two property edges for equality.
        /// </summary>
        /// <param name="IPropertyEdge">A property edge to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IPropertyEdge IPropertyEdge)
        {

            if ((Object) IPropertyEdge == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return Id.Equals(IPropertyEdge[IdKey]);

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
            return "PropertyEdge [Id: " + Id.ToString() + ", '" + OutVertex.Id + "' --" + Label + "-> '" + InVertex.Id + "']";
        }

        #endregion

    }

}
