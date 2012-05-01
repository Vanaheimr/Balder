﻿/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <code@ahzf.de>
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

using de.ahzf.Blueprints.PropertyGraphs;

using Jurassic;

#endregion

namespace de.ahzf.Blueprints.JurassicGraph
{

    /// <summary>
    /// A wrapper to access a property edge from JavaScript.
    /// </summary>
    public class JSPropertyEdge : JSGraphElement
    {

        #region Constructor(s)

        #region JSPropertyEdge(PropertyEdge, JavaScriptEngine)

        /// <summary>
        /// Create a new property edge wrapper for JavaScript.
        /// </summary>
        /// <param name="PropertyEdge">The internal property edge.</param>
        /// <param name="JavaScriptEngine">An instance of a JavaScript engine.</param>
        public JSPropertyEdge(IGenericPropertyEdge<String, Int64, String, String, Object,
                                                   String, Int64, String, String, Object,
                                                   String, Int64, String, String, Object,
                                                   String, Int64, String, String, Object> PropertyEdge,
                              ScriptEngine JavaScriptEngine)

            : base(PropertyEdge, JavaScriptEngine)

        { }

        #endregion

        #endregion

    }

}