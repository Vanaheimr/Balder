/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <code@ahzf.de>
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

using de.ahzf.Blueprints.PropertyGraphs;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// A special implementation of a DynamicObject for property graphs.
    /// </summary>
    /// <typeparam name="CompileTimeType">The compile time type of this DynamicGraphObject.</typeparam>
    public sealed class DynamicGraphElement<CompileTimeType> : DynamicMetaObject
        where CompileTimeType : class, IDynamicGraphElement<CompileTimeType>
    {

        #region Data

        /// <summary>
        /// The runtime value as compile time type.
        /// </summary>
        private readonly CompileTimeType _RuntimeValue;

        #endregion

        #region Constructor(s)

        #region DynamicGraphObject(Expression, RuntimeValue)

        /// <summary>
        /// Creates a new DynamicGraphElement helper.
        /// </summary>
        /// <param name="Expression">The expression representing this System.Dynamic.DynamicMetaObject during the dynamic binding process.</param>
        /// <param name="RuntimeValue">The runtime value represented by the System.Dynamic.DynamicMetaObject.</param>
        public DynamicGraphElement(Expression Expression, CompileTimeType RuntimeValue)
            : this(Expression, BindingRestrictions.Empty, RuntimeValue)
        { }

        #endregion

        #region DynamicGraphObject(Expression, BindingRestrictions)

        /// <summary>
        /// Creates a new DynamicGraphElement helper.
        /// </summary>
        /// <param name="Expression">The expression representing this System.Dynamic.DynamicMetaObject during the dynamic binding process.</param>
        /// <param name="BindingRestrictions">The set of binding restrictions under which the binding is valid.</param>
        public DynamicGraphElement(Expression Expression, BindingRestrictions BindingRestrictions)
            : base(Expression, BindingRestrictions)
        { }

        #endregion

        #region DynamicGraphObject(Expression, BindingRestrictions, CompileTimeType)

        /// <summary>
        /// Creates a new DynamicGraphElement helper.
        /// </summary>
        /// <param name="Expression">The expression representing this System.Dynamic.DynamicMetaObject during the dynamic binding process.</param>
        /// <param name="BindingRestrictions">The set of binding restrictions under which the binding is valid.</param>
        /// <param name="RuntimeValue">The runtime value represented by the System.Dynamic.DynamicMetaObject.</param>
        public DynamicGraphElement(Expression Expression, BindingRestrictions BindingRestrictions, CompileTimeType RuntimeValue)
            : base(Expression, BindingRestrictions, RuntimeValue)
        {
            _RuntimeValue = Value as CompileTimeType;
        }

        #endregion

        #endregion


        #region GetDynamicMemberNames()

        /// <summary>
        /// Return all binder names.
        /// </summary>
        public override IEnumerable<String> GetDynamicMemberNames()
        {
            return _RuntimeValue.GetDynamicMemberNames();
        }

        #endregion

        #region BindSetMember(SetMemberBinder, DynamicMetaObject)

        /// <summary>
        /// Assign the given value to the given binder name.
        /// </summary>
        /// <param name="SetMemberBinder">A SetMemberBinder.</param>
        /// <param name="DynamicMetaObject">A DynamicMetaObject.</param>
        public override DynamicMetaObject BindSetMember(SetMemberBinder SetMemberBinder, DynamicMetaObject DynamicMetaObject)
        {

            _RuntimeValue.SetMember(SetMemberBinder.Name, DynamicMetaObject.Value);

            return new DynamicMetaObject(Expression.Constant(this),
                                         BindingRestrictions.GetTypeRestriction(
                                            this.Expression,
                                            typeof(CompileTimeType)
                                         )
                                        );

            //var self      = this.Expression;
            //var keyExpr   = Expression.Constant(mySetMemberBinder);
            //var valueExpr = Expression.Convert(myDynamicMetaObject.Expression, typeof(Object));
            
            //var target    = Expression.Call(Expression.Convert(self, typeof(T)),
            //                                typeof(T).GetMethod("SetMember"),
            //                                new Expression[] { keyExpr, valueExpr });

            //return new DynamicMetaObject(target, BindingRestrictions.GetTypeRestriction(self, typeof(T)));

        }

        #endregion

        #region BindGetMember(GetMemberBinder)

        /// <summary>
        /// Return the value of the given binder name.
        /// </summary>
        /// <param name="GetMemberBinder">The GetMemberBinder.</param>
        public override DynamicMetaObject BindGetMember(GetMemberBinder GetMemberBinder)
        {

            var _Result = _RuntimeValue.GetMember(GetMemberBinder.Name);

            return new DynamicMetaObject(Expression.Convert(
                                            Expression.Constant(_Result),
                                            GetMemberBinder.ReturnType
                                         ),
                                         BindingRestrictions.GetTypeRestriction(
                                            this.Expression,
                                            typeof(CompileTimeType)
                                         )
                                        );

            //var self = this.Expression;
            //var keyExpr = Expression.Constant(myGetMemberBinder);
            ////      var result    = Expression.Parameter(typeof(object).MakeByRefType(), "result");

            //var target = Expression.Call(Expression.Convert(self, typeof(T)),
            //                             typeof(T).GetMethod("GetMember"),
            //                             new Expression[] { keyExpr });

            //return new DynamicMetaObject(target, BindingRestrictions.GetTypeRestriction(self, typeof(T)));

        }

        #endregion

        #region BindInvokeMember(InvokeMemberBinder, Arguments)

        /// <summary>
        /// Invoke the given binder.
        /// </summary>
        /// <param name="InvokeMemberBinder">A binder to invoke.</param>
        /// <param name="Arguments">Arguments.</param>
        /// <returns>The result of the invocation.</returns>
        public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder InvokeMemberBinder, DynamicMetaObject[] Arguments)
        {

            var    _Delegate = _RuntimeValue.GetMember(InvokeMemberBinder.Name) as Delegate;
            Object _Result   = null;
            
            if (_Delegate != null)
                _Result = _Delegate.DynamicInvoke((from _DynObject in Arguments select _DynObject.Value as Object).ToArray());

            return new DynamicMetaObject(Expression.Convert(
                                            Expression.Constant(_Result),
                                            typeof(Object)
                                        ),
                                        BindingRestrictions.GetTypeRestriction(
                                            this.Expression,
                                            typeof(CompileTimeType)
                                        )
                                       );

        }

        #endregion

    }

}
