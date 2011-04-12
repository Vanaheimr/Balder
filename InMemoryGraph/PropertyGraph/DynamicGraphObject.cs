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
using System.Linq;
using System.Dynamic;
using System.Linq.Expressions;

#endregion

namespace de.ahzf.blueprints.InMemory.PropertyGraph
{

    /// <summary>
    /// A special implementation of a DynamicObject for property graphs.
    /// </summary>
    /// <typeparam name="CompileTimeType">The compile time type of this DynamicGraphObject.</typeparam>
    public sealed class DynamicGraphObject<CompileTimeType> : DynamicMetaObject
        where CompileTimeType : class, IDynamicGraphObject<CompileTimeType>
    {

        #region Data

        /// <summary>
        /// The runtime value as compile time type.
        /// </summary>
        protected readonly CompileTimeType _RuntimeValue;

        #endregion

        #region Constructor(s)

        #region (internal) DynamicGraphObject(myParameter, myValue)

        internal DynamicGraphObject(Expression myParameter, CompileTimeType myValue)
            : this(myParameter, BindingRestrictions.Empty, myValue)
        { }

        #endregion

        #region (internal) DynamicGraphObject(myParameter, myBindingRestrictions, myValue)

        internal DynamicGraphObject(Expression myParameter, BindingRestrictions myBindingRestrictions, CompileTimeType myValue)
            : base(myParameter, myBindingRestrictions, myValue)
        {
            _RuntimeValue = Value as CompileTimeType;
        }

        #endregion

        #endregion

        //public override System.Collections.Generic.IEnumerable<string> GetDynamicMemberNames()
        //{
        //    return ((T)Value).GetDynamicMemberNames();
        //}

        #region BindSetMember(mySetMemberBinder, myDynamicMetaObject)

        public override DynamicMetaObject BindSetMember(SetMemberBinder mySetMemberBinder, DynamicMetaObject myDynamicMetaObject)
        {

            _RuntimeValue.SetMember(mySetMemberBinder.Name, myDynamicMetaObject.Value);

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

        #region BindGetMember(myGetMemberBinder)

        public override DynamicMetaObject BindGetMember(GetMemberBinder myGetMemberBinder)
        {

            var _Result = _RuntimeValue.GetMember(myGetMemberBinder.Name);

            return new DynamicMetaObject(Expression.Convert(
                                            Expression.Constant(_Result),
                                            myGetMemberBinder.ReturnType
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

        #region BindInvokeMember(myInvokeMemberBinder, myArguments)

        public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder myInvokeMemberBinder, DynamicMetaObject[] myArguments)
        {

            var    _Delegate = _RuntimeValue.GetMember(myInvokeMemberBinder.Name) as Delegate;
            Object _Result   = null;
            
            if (_Delegate != null)
                _Result = _Delegate.DynamicInvoke((from _DynObject in myArguments select _DynObject.Value as Object).ToArray());

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
