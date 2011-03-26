/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET
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
using System.Collections;
using System.Collections.Generic;
using de.ahzf.blueprints.Datastructures;

#endregion

namespace de.ahzf.blueprints
{

    /// <summary>
    /// An element is the base class for both vertices and edges.
    /// An element has an identifier that must be unique to its inheriting classes (vertex or edges).
    /// An element can maintain a collection of key/value properties.
    /// Keys are always strings and values can be any object.
    /// Particular implementations can reduce the space of objects that can be used as values.
    /// </summary>
    public abstract class AStringProperties<TId> : AProperties<TId, String>, IDynamicMetaObjectProvider//, DynamicObject
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
    {

        #region Data

        /// <summary>
        /// The key of the well-known Id property.
        /// </summary>
        private const String ___Id = "Id";

        /// <summary>
        /// The key of the well-known RevisionId property.
        /// </summary>
        private const String ___RevisionId = "RevisionId";

        #endregion

        #region Protected Constructor(s)

        #region AStringProperties(myIGraph, myElementId)

        /// <summary>
        /// Creates a new AElement object
        /// </summary>
        /// <param name="myIGraph">The associated graph</param>
        /// <param name="myElementId">The Id of the new AElement</param>
        protected AStringProperties(IGraph myIGraph, ElementId myElementId)
            : base (myIGraph, myElementId, ___Id, ___RevisionId, () => new Dictionary<String, Object>(StringComparer.OrdinalIgnoreCase))
        {

            if (myIGraph == null)
                throw new ArgumentNullException("The graph reference must not be null!");

            if (myElementId == null)
                throw new ArgumentNullException("The ElementId must not be null!");

        }

        #endregion

        #endregion


        #region IDynamicMetaObjectProvider Members

        public DynamicMetaObject GetMetaObject(System.Linq.Expressions.Expression parameter)
        {
            throw new NotImplementedException();
        }

        //#region GetDynamicMemberNames()

        ///// <summary>
        ///// Returns an enumeration of all property keys.
        ///// </summary>
        //public IEnumerable<String> GetDynamicMemberNames()
        //{
        //    return _Properties.Keys;
        //}

        //#endregion

        

        //#region TrySetMember(myBinder, myObject)

        ///// <summary>
        ///// Sets a new property or overwrites an existing.
        ///// </summary>
        ///// <param name="myBinder">The property key</param>
        ///// <param name="myObject">The property value</param>
        ///// <returns>Always true</returns>
        //public override Boolean TrySetMember(SetMemberBinder myBinder, Object myObject)
        //{

        //    if (_Properties.ContainsKey(myBinder.Name))
        //        _Properties[myBinder.Name] = myObject;

        //    else
        //        _Properties.Add(myBinder.Name, myObject);

        //    return true;

        //}

        //#endregion

        //#region TryGetMember(myBinder, out myObject)

        ///// <summary>
        ///// Returns the value of a property.
        ///// </summary>
        ///// <param name="myBinder">The property key.</param>
        ///// <param name="myObject">The property value.</param>
        ///// <returns>Always true</returns>
        //public override Boolean TryGetMember(GetMemberBinder myBinder, out Object myObject)
        //{
        //    return _Properties.TryGetValue(myBinder.Name, out myObject);
        //}

        //#endregion

        //#region TryInvokeMember(myBinder, out myObject)

        ///// <summary>
        ///// Tries to invoke a property as long as it is a delegate.
        ///// </summary>
        ///// <param name="myBinder">The property key</param>
        ///// <param name="myArguments">The arguments for invoking the property</param>
        ///// <param name="myObject">The property value</param>
        ///// <returns>If the property could be invoked.</returns>
        //public override Boolean TryInvokeMember(InvokeMemberBinder myBinder, Object[] myArguments, out Object myObject)
        //{

        //    Object _Object = null;

        //    if (_Properties.TryGetValue(myBinder.Name, out _Object))
        //    {
                
        //        var _Delegate = _Object as Delegate;

        //        if (_Delegate != null)
        //        {
        //            myObject = _Delegate.DynamicInvoke(myArguments);
        //            return true;
        //        }

        //    }

        //    myObject = null;

        //    return false;

        //}

        //#endregion

        //#region TryDeleteMember(myBinder)

        ///// <summary>
        ///// Tries to remove the property identified by the given property key.
        ///// </summary>
        ///// <param name="myBinder">The property key</param>
        ///// <returns>True on success</returns>
        //public override Boolean TryDeleteMember(DeleteMemberBinder myBinder)
        //{

        //    try
        //    {
        //        return _Properties.Remove(myBinder.Name);
        //    }
        //    catch
        //    { }

        //    return false;

        //}

        //#endregion

        #endregion


    }

}
