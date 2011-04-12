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
using System.Dynamic;
using System.Linq.Expressions;
using System.Collections.Generic;

using de.ahzf.blueprints.Datastructures;
using de.ahzf.blueprints.InMemory.PropertyGraph.Generic;

#endregion

namespace de.ahzf.blueprints.InMemory.PropertyGraph
{

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    public class PropertyVertex : PropertyVertex<VertexId,    RevisionId, String, Object, IDictionary<String, Object>,
                                                 EdgeId,      RevisionId, String, Object, IDictionary<String, Object>,
                                                 HyperEdgeId, RevisionId, String, Object, IDictionary<String, Object>,
                                                 ICollection<IPropertyEdge<VertexId,    RevisionId, String, Object,
                                                                           EdgeId,      RevisionId, String, Object,
                                                                           HyperEdgeId, RevisionId, String, Object>>>,
                                  IPropertyVertex,
                                  IDynamicGraphObject<PropertyVertex>

    {

        #region Constructor(s)

        #region PropertyVertex(myIGraph, myVertexId, myVertexInitializer = null)

        /// <summary>
        /// Creates a new property vertex.
        /// </summary>
        /// <param name="myIGraph">The associated graph.</param>
        /// <param name="myVertexId">The identification of this vertex.</param>
        /// <param name="myVertexInitializer">A delegate to initialize the newly created vertex.</param>
        public PropertyVertex(VertexId myVertexId,
                              Action<IProperties<String, Object>> myVertexInitializer = null)
            : base(myVertexId,
                   "Id", "RevisionId",
                   () => new Dictionary<String, Object>(),
                   () => new HashSet<IPropertyEdge<VertexId,    RevisionId, String, Object,
                                                   EdgeId,      RevisionId, String, Object,
                                                   HyperEdgeId, RevisionId, String, Object>>(),
                   myVertexInitializer)

        { }

        #endregion

        #endregion

        #region IDynamic<PropertyVertex> Members

        #region GetMetaObject(myExpression)

        /// <summary>
        /// Return the appropriate DynamicMetaObject.
        /// </summary>
        /// <param name="myExpression">An Expression.</param>
        public DynamicMetaObject GetMetaObject(Expression myExpression)
        {
            return new DynamicGraphObject<PropertyVertex>(myExpression, this);
        }

        #endregion

        #region GetDynamicMemberNames()

        /// <summary>
        /// Returns an enumeration of all property keys.
        /// </summary>
        public virtual IEnumerable<String> GetDynamicMemberNames()
        {
            return _Data.PropertyKeys;
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
            return _Data.SetProperty(myBinder, myObject);
            //if (_Properties.ContainsKey(myBinder.Name))
            //    _Properties[myBinder.Name] = myObject;
            //else
            //    _Properties.Add(myBinder.Name, myObject);
        }

        #endregion

        #region GetMember(myBinder)

        /// <summary>
        /// Returns the value of the given property key.
        /// </summary>
        /// <param name="myBinder">The property key.</param>
        public virtual Object GetMember(String myBinder)
        {
            Object myObject;
            _Data.TryGetProperty(myBinder, out myObject);
            return myObject;
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
                _Data.RemoveProperty(myBinder);
                return true;
            }
            catch
            { }

            return false;

        }

        #endregion

        #endregion

    }

}
