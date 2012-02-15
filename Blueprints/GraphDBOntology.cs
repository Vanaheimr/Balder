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

#endregion

namespace de.ahzf.Blueprints.PropertyGraphs
{

    /// <summary>
    /// The graph-database ontology.
    /// </summary>
    public static class GraphDBOntology
    {

        #region Prefix

        /// <summary>
        /// The base Uri of the graph-database ontology.
        /// </summary>
        public static readonly Uri Prefix = new Uri("http://graph-database.org/gdb/0.1");

        #endregion


        #region Id()

        /// <summary>
        /// The Id of something.
        /// </summary>
        /// <returns>A semantic property key to be used within property graphs.</returns>
        public static SemanticProperty Id()
        {
            return new SemanticProperty(Prefix, "Id", "Id");
        }

        #endregion

        #region RevId()

        /// <summary>
        /// The RevId of something.
        /// </summary>
        /// <returns>A semantic property key to be used within property graphs.</returns>
        public static SemanticProperty RevId()
        {
            return new SemanticProperty(Prefix, "RevId", "RevId");
        }

        #endregion

        #region Description()

        /// <summary>
        /// The description of something.
        /// </summary>
        /// <returns>A semantic property key to be used within property graphs.</returns>
        public static SemanticProperty Description()
        {
            return new SemanticProperty(Prefix, "Description", "Description");
        }

        #endregion

    }

}
