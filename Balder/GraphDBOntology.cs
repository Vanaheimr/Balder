/*
 * Copyright (c) 2010-2015, Achim 'ahzf' Friedland <achim@graphdefined.org>
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

using org.GraphDefined.Vanaheimr.Eunomia;

#endregion

namespace org.GraphDefined.Vanaheimr.Balder
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
        public static readonly Uri Prefix = new Uri(Semantics.GraphDBPrefix);

        #endregion


        #region Id

        /// <summary>
        /// The Id of something.
        /// </summary>
        /// <returns>A semantic property key to be used within property graphs.</returns>
        public static SemanticProperty Id
        {
            get
            {
                return new SemanticProperty(Prefix, Semantics.Id, Semantics.Id);
            }
        }

        #endregion

        #region RevId

        /// <summary>
        /// The RevId of something.
        /// </summary>
        /// <returns>A semantic property key to be used within property graphs.</returns>
        public static SemanticProperty RevId
        {
            get
            {
                return new SemanticProperty(Prefix, Semantics.RevId, Semantics.RevId);
            }
        }

        #endregion

        #region Label

        /// <summary>
        /// The Label of something.
        /// </summary>
        /// <returns>A semantic property key to be used within property graphs.</returns>
        public static SemanticProperty Label
        {
            get
            {
                return new SemanticProperty(Prefix, Semantics.Label, Semantics.Label);
            }
        }

        #endregion

        #region Description

        /// <summary>
        /// The description of something.
        /// </summary>
        /// <returns>A semantic property key to be used within property graphs.</returns>
        public static SemanticProperty Description
        {
            get
            {
                return new SemanticProperty(Prefix, Semantics.Description, Semantics.Description);
            }
        }

        #endregion


        #region DefaultVertexLabel

        /// <summary>
        /// The default vertex label.
        /// </summary>
        /// <returns>A semantic property key to be used within property graphs.</returns>
        public static SemanticProperty DefaultVertexLabel
        {
            get
            {
                return new SemanticProperty(Prefix, Semantics.DefaultVertexLabel, Semantics.DefaultVertexLabel);
            }
        }

        #endregion

        #region DefaultEdgeLabel

        /// <summary>
        /// The default edge label.
        /// </summary>
        /// <returns>A semantic property key to be used within property graphs.</returns>
        public static SemanticProperty DefaultEdgeLabel
        {
            get
            {
                return new SemanticProperty(Prefix, Semantics.DefaultEdgeLabel, Semantics.DefaultEdgeLabel);
            }
        }

        #endregion

        #region DefaultMultiEdgeLabel

        /// <summary>
        /// The default multiedge label.
        /// </summary>
        /// <returns>A semantic property key to be used within property graphs.</returns>
        public static SemanticProperty DefaultMultiEdgeLabel
        {
            get
            {
                return new SemanticProperty(Prefix, Semantics.DefaultMultiEdgeLabel, Semantics.DefaultMultiEdgeLabel);
            }
        }

        #endregion

        #region DefaultHyperEdgeLabel

        /// <summary>
        /// The default hyperedge label.
        /// </summary>
        /// <returns>A semantic property key to be used within property graphs.</returns>
        public static SemanticProperty DefaultHyperEdgeLabel
        {
            get
            {
                return new SemanticProperty(Prefix, Semantics.DefaultHyperEdgeLabel, Semantics.DefaultHyperEdgeLabel);
            }
        }

        #endregion

    }

}
