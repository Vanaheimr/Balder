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

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace org.GraphDefined.Vanaheimr.Balder.InMemory
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
    /// <typeparam name="TIdMultiEdge">The type of the multiedge identifiers.</typeparam>
    /// <typeparam name="TRevIdMultiEdge">The type of the multiedge revision identifiers.</typeparam>
    /// <typeparam name="TMultiEdgeLabel">The type of the multiedge label.</typeparam>
    /// <typeparam name="TKeyMultiEdge">The type of the multiedge property keys.</typeparam>
    /// <typeparam name="TValueMultiEdge">The type of the multiedge property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionMultiEdge">A data structure to store the properties of the multiedges.</typeparam>
    /// 
    /// <typeparam name="TIdHyperEdge">The type of the multiedge identifiers.</typeparam>
    /// <typeparam name="TRevIdHyperEdge">The type of the multiedge revision identifiers.</typeparam>
    /// <typeparam name="THyperEdgeLabel">The type of the multiedge label.</typeparam>
    /// <typeparam name="TKeyHyperEdge">The type of the multiedge property keys.</typeparam>
    /// <typeparam name="TValueHyperEdge">The type of the multiedge property values.</typeparam>
    /// <typeparam name="TPropertiesCollectionHyperEdge">A data structure to store the properties of the multiedges.</typeparam>
    /// 
    /// <typeparam name="TEdgesCollection">A data structure to store edges.</typeparam>
    public class PropertyMultiEdge : GenericPropertyMultiEdge<UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object,
                                                              UInt64, Int64, String, String, Object>,

                                     IPropertyMultiEdge,
                                     IDynamicGraphElement<PropertyMultiEdge>

    {

        #region Properties

        #region Graph

        /// <summary>
        /// The associated property graph.
        /// </summary>
        IPropertyGraph IPropertyMultiEdge.Graph
        {
            get
            {
                return Graph as IPropertyGraph;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region PropertyMultiEdge(Graph, Edges, MultiEdgeId, Label, IdKey, RevIdKey, DescriptionKey, DatastructureInitializer, EdgesCollectionInitializer, MultiEdgeInitializer = null)

        /// <summary>
        /// Creates a new edge.
        /// </summary>
        /// <param name="Graph">The associated property graph.</param>
        /// <param name="EdgeSelector">An enumeration of edges.</param>
        /// <param name="MultiEdgeId">The identification of this multiedge.</param>
        /// <param name="Label">A label stored within this multiedge.</param>
        /// <param name="IdKey">The key to access the Id of this multiedge.</param>
        /// <param name="RevIdKey">The key to access the RevId of this multiedge.</param>
        /// <param name="LabelKey">The key to access the Label of this graph element.</param>
        /// <param name="DatastructureInitializer">A delegate to initialize the properties datastructure.</param>
        /// <param name="EdgesCollectionInitializer">A delegate to initialize the datastructure for storing the edges.</param>
        /// <param name="MultiEdgeInitializer">A delegate to initialize the newly created multiedge.</param>
        public PropertyMultiEdge(IPropertyGraph Graph,

                                 MultiEdgeEdgeSelector<UInt64, Int64, String, String, Object,
                                                       UInt64, Int64, String, String, Object,
                                                       UInt64, Int64, String, String, Object,
                                                       UInt64, Int64, String, String, Object> EdgeSelector,

                                 UInt64 MultiEdgeId,
                                 String Label,
                                 String IdKey,
                                 String RevIdKey,
                                 String LabelKey,
                                 String DescriptionKey,

                                 IDictionaryInitializer<String, Object> DatastructureInitializer,

                                 Func<IGroupedCollection<String, UInt64, IReadOnlyGenericPropertyEdge<UInt64, Int64, String, String, Object,
                                                                                                      UInt64, Int64, String, String, Object,
                                                                                                      UInt64, Int64, String, String, Object,
                                                                                                      UInt64, Int64, String, String, Object>>> EdgesCollectionInitializer,

                                 MultiEdgeInitializer<UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object,
                                                      UInt64, Int64, String, String, Object> MultiEdgeInitializer = null)

            : base(Graph, EdgeSelector, MultiEdgeId, Label, IdKey, DescriptionKey, RevIdKey, LabelKey, DatastructureInitializer, EdgesCollectionInitializer, MultiEdgeInitializer)

        { }

        #endregion

        #endregion


        //#region Edges

        //#region EdgesByLabel(params EdgeLabels)

        ///// <summary>
        ///// The enumeration of all edges connected by this multiedge.
        ///// </summary>
        //public IEnumerable<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>

        //           EdgesByLabel(params TEdgeLabel[] EdgeLabels)

        //{
        //    throw new NotImplementedException();
        //}

        //#endregion

        //#region Edges(EdgeFilter = null)

        ///// <summary>
        ///// The enumeration of all edges connected by this multiedge.
        ///// An optional edge filter may be applied for filtering.
        ///// </summary>
        //public IEnumerable<IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                        TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                        TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                        TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>>
            
        //           Edges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                            TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                            TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                            TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> EdgeFilter = null)

        //{
        //    throw new NotImplementedException();
        //}

        //#endregion

        //#region NumberOfEdges(EdgeFilter = null)

        ///// <summary>
        ///// Return the current number of edges which match the given optional filter.
        ///// When the filter is null, this method should implement an optimized
        ///// way to get the currenty number of edges.
        ///// </summary>
        ///// <param name="EdgeFilter">A delegate for edge filtering.</param>
        //public UInt64 NumberOfEdges(EdgeFilter<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                       TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                       TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                       TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge>

        //           EdgeFilter = null)

        //{
        //    throw new NotImplementedException();
        //}

        //#endregion

        //#endregion



        //public Boolean CheckIfMatches(IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                   TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                   TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                                   TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)
        //{
        //    throw new NotImplementedException();
        //}

        //public Boolean AddIfMatches(IGenericPropertyEdge<TIdVertex,    TRevIdVertex,    TVertexLabel,    TKeyVertex,    TValueVertex,
        //                                                 TIdEdge,      TRevIdEdge,      TEdgeLabel,      TKeyEdge,      TValueEdge,
        //                                                 TIdMultiEdge, TRevIdMultiEdge, TMultiEdgeLabel, TKeyMultiEdge, TValueMultiEdge,
        //                                                 TIdHyperEdge, TRevIdHyperEdge, THyperEdgeLabel, TKeyHyperEdge, TValueHyperEdge> Edge)
        //{
        //    throw new NotImplementedException();
        //}


        #region Operator overloading

        #region Operator == (PropertyMultiEdge1, PropertyMultiEdge)

        /// <summary>
        /// Compares two multiedges for equality.
        /// </summary>
        /// <param name="PropertyMultiEdge1">A Edge.</param>
        /// <param name="PropertyMultiEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (PropertyMultiEdge PropertyMultiEdge1,
                                           PropertyMultiEdge PropertyMultiEdge2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(PropertyMultiEdge1, PropertyMultiEdge2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) PropertyMultiEdge1 == null) || ((Object) PropertyMultiEdge2 == null))
                return false;

            return PropertyMultiEdge1.Equals(PropertyMultiEdge2);

        }

        #endregion

        #region Operator != (PropertyMultiEdge1, PropertyMultiEdge)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyMultiEdge1">A Edge.</param>
        /// <param name="PropertyMultiEdge">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (PropertyMultiEdge PropertyMultiEdge1,
                                           PropertyMultiEdge PropertyMultiEdge2)
        {
            return !(PropertyMultiEdge1 == PropertyMultiEdge2);
        }

        #endregion

        #region Operator <  (PropertyMultiEdge1, PropertyMultiEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyMultiEdge1">A Edge.</param>
        /// <param name="PropertyMultiEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <  (PropertyMultiEdge PropertyMultiEdge1,
                                           PropertyMultiEdge PropertyMultiEdge2)
        {

            if ((Object) PropertyMultiEdge1 == null)
                throw new ArgumentNullException("The given PropertyMultiEdge1 must not be null!");

            if ((Object) PropertyMultiEdge2 == null)
                throw new ArgumentNullException("The given PropertyMultiEdge must not be null!");

            return PropertyMultiEdge1.CompareTo(PropertyMultiEdge2) < 0;

        }

        #endregion

        #region Operator <= (PropertyMultiEdge1, PropertyMultiEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyMultiEdge1">A Edge.</param>
        /// <param name="PropertyMultiEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (PropertyMultiEdge PropertyMultiEdge1,
                                           PropertyMultiEdge PropertyMultiEdge2)
        {
            return !(PropertyMultiEdge1 > PropertyMultiEdge2);
        }

        #endregion

        #region Operator >  (PropertyMultiEdge1, PropertyMultiEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyMultiEdge1">A Edge.</param>
        /// <param name="PropertyMultiEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >  (PropertyMultiEdge PropertyMultiEdge1,
                                           PropertyMultiEdge PropertyMultiEdge2)
        {

            if ((Object) PropertyMultiEdge1 == null)
                throw new ArgumentNullException("The given PropertyMultiEdge1 must not be null!");

            if ((Object) PropertyMultiEdge2 == null)
                throw new ArgumentNullException("The given PropertyMultiEdge2 must not be null!");

            return PropertyMultiEdge1.CompareTo(PropertyMultiEdge2) > 0;

        }

        #endregion

        #region Operator >= (PropertyMultiEdge1, PropertyMultiEdge2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PropertyMultiEdge1">A Edge.</param>
        /// <param name="PropertyMultiEdge2">Another Edge.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (PropertyMultiEdge PropertyMultiEdge1,
                                           PropertyMultiEdge PropertyMultiEdge2)
        {
            return !(PropertyMultiEdge1 < PropertyMultiEdge2);
        }

        #endregion

        #endregion

        #region IDynamicGraphObject<PropertyMultiEdge> Members

        #region GetMetaObject(Expression)

        /// <summary>
        /// Return the appropriate DynamicMetaObject.
        /// </summary>
        /// <param name="Expression">An Expression.</param>
        public override DynamicMetaObject GetMetaObject(Expression Expression)
        {
            return new DynamicGraphElement<PropertyMultiEdge>(Expression, this);
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

            // Check if the given object can be casted to a PropertyVertex
            var PropertyVertex = Object as PropertyVertex;

            if ((Object) PropertyVertex == null)
                throw new ArgumentException("The given object is not a PropertyVertex!");

            return CompareTo(PropertyVertex);

        }

        #endregion

        #region CompareTo(IPropertyMultiEdge)

        /// <summary>
        /// Compares two generic property multiedges.
        /// </summary>
        /// <param name="IGenericPropertyMultiEdge">A generic property multiedge to compare with.</param>
        public Int32 CompareTo(IPropertyMultiEdge IGenericPropertyMultiEdge)
        {

            if ((Object) IGenericPropertyMultiEdge == null)
                throw new ArgumentNullException("The given IGenericPropertyMultiEdge must not be null!");

            return Id.CompareTo(IGenericPropertyMultiEdge[IdKey]);

        }

        #endregion

        #endregion

        #region IEquatable Members

        #region Equals(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>true|false</returns>
        public override Boolean Equals(Object Object)
        {

            if (Object == null)
                return false;

            // Check if the given object can be casted to a PropertyMultiEdge
            var PropertyMultiEdge = Object as PropertyMultiEdge;
            if ((Object) PropertyMultiEdge == null)
                return false;

            return this.Equals(PropertyMultiEdge);

        }

        #endregion

        #region Equals(IPropertyMultiEdge)

        /// <summary>
        /// Compares two generic property multiedges for equality.
        /// </summary>
        /// <param name="IGenericPropertyMultiEdge">A generic property multiedge to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(IPropertyMultiEdge IGenericPropertyMultiEdge)
        {

            if ((Object) IGenericPropertyMultiEdge == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return Id.Equals(IGenericPropertyMultiEdge[IdKey]);

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
            return "PropertyMultiEdge [Id: " + Id.ToString() + "']";
        }

        #endregion


        public new IReadOnlyGenericPropertyGraph<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> Graph
        {
            get { throw new NotImplementedException(); }
        }

        public new System.Collections.Generic.IEnumerable<IReadOnlyGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> EdgesByLabel(params string[] EdgeLabels)
        {
            throw new NotImplementedException();
        }

        public new System.Collections.Generic.IEnumerable<IReadOnlyGenericPropertyEdge<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> Edges(EdgeFilter<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> EdgeFilter = null)
        {
            throw new NotImplementedException();
        }


    }

}


