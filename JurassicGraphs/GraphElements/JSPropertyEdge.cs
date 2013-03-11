/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Balder <http://www.github.com/Vanaheimr/Balder>
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

using eu.Vanaheimr.Balder;

using Jurassic;

#endregion

namespace eu.Vanaheimr.Balder.JurassicGraph
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
