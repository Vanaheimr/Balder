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

using org.GraphDefined.Vanaheimr.Balder;

using Jurassic;

#endregion

namespace org.GraphDefined.Vanaheimr.Balder.JurassicGraph
{

    /// <summary>
    /// A wrapper to access a property vertex from JavaScript.
    /// </summary>
    public class JSPropertyVertex : JSGraphElement
    {

        #region Constructor(s)

        #region JSPropertyVertex(Vertex, JavaScriptEngine)

        /// <summary>
        /// Create a new property vertex wrapper for JavaScript.
        /// </summary>
        /// <param name="Vertex">The internal vertex.</param>
        /// <param name="JavaScriptEngine">An instance of a JavaScript engine.</param>
        public JSPropertyVertex(IGenericPropertyVertex<String, Int64, String, String, Object,
                                                       String, Int64, String, String, Object,
                                                       String, Int64, String, String, Object,
                                                       String, Int64, String, String, Object> Vertex,
                                ScriptEngine JavaScriptEngine)

            : base(Vertex, JavaScriptEngine)

        { }

        #endregion

        #endregion

    }

}
