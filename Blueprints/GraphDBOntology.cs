/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
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

#endregion

namespace de.ahzf.Vanaheimr.Blueprints
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
        public static readonly Uri Prefix = new Uri(Eunomia.Semantics.GraphDBPrefix);

        #endregion


        #region Id()

        /// <summary>
        /// The Id of something.
        /// </summary>
        /// <returns>A semantic property key to be used within property graphs.</returns>
        public static SemanticProperty Id()
        {
            return new SemanticProperty(Prefix, Eunomia.Semantics.Id, Eunomia.Semantics.Id);
        }

        #endregion

        #region RevId()

        /// <summary>
        /// The RevId of something.
        /// </summary>
        /// <returns>A semantic property key to be used within property graphs.</returns>
        public static SemanticProperty RevId()
        {
            return new SemanticProperty(Prefix, Eunomia.Semantics.RevId, Eunomia.Semantics.RevId);
        }

        #endregion

        #region Label()

        /// <summary>
        /// The Label of something.
        /// </summary>
        /// <returns>A semantic property key to be used within property graphs.</returns>
        public static SemanticProperty Label()
        {
            return new SemanticProperty(Prefix, Eunomia.Semantics.Label, Eunomia.Semantics.Label);
        }

        #endregion

        #region Description()

        /// <summary>
        /// The description of something.
        /// </summary>
        /// <returns>A semantic property key to be used within property graphs.</returns>
        public static SemanticProperty Description()
        {
            return new SemanticProperty(Prefix, Eunomia.Semantics.Description, Eunomia.Semantics.Description);
        }

        #endregion


        #region DefaultVertexLabel()

        /// <summary>
        /// The default vertex label.
        /// </summary>
        /// <returns>A semantic property key to be used within property graphs.</returns>
        public static SemanticProperty DefaultVertexLabel()
        {
            return new SemanticProperty(Prefix, Eunomia.Semantics.DefaultVertexLabel, Eunomia.Semantics.DefaultVertexLabel);
        }

        #endregion

        #region DefaultEdgeLabel()

        /// <summary>
        /// The default edge label.
        /// </summary>
        /// <returns>A semantic property key to be used within property graphs.</returns>
        public static SemanticProperty DefaultEdgeLabel()
        {
            return new SemanticProperty(Prefix, Eunomia.Semantics.DefaultEdgeLabel, Eunomia.Semantics.DefaultEdgeLabel);
        }

        #endregion

        #region DefaultMultiEdgeLabel()

        /// <summary>
        /// The default multiedge label.
        /// </summary>
        /// <returns>A semantic property key to be used within property graphs.</returns>
        public static SemanticProperty DefaultMultiEdgeLabel()
        {
            return new SemanticProperty(Prefix, Eunomia.Semantics.DefaultMultiEdgeLabel, Eunomia.Semantics.DefaultMultiEdgeLabel);
        }

        #endregion

        #region DefaultHyperEdgeLabel()

        /// <summary>
        /// The default hyperedge label.
        /// </summary>
        /// <returns>A semantic property key to be used within property graphs.</returns>
        public static SemanticProperty DefaultHyperEdgeLabel()
        {
            return new SemanticProperty(Prefix, Eunomia.Semantics.DefaultHyperEdgeLabel, Eunomia.Semantics.DefaultHyperEdgeLabel);
        }

        #endregion

    }

}
