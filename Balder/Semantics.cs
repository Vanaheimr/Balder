/*
 * Copyright (c) 2011-2013, Achim 'ahzf' Friedland <achim@graphdefined.org>
 * This file is part of Eunomia <http://www.github.com/Vanaheimr/Eunomia>
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

namespace org.GraphDefined.Vanaheimr.Eunomia
{

    /// <summary>
    /// The graph-database ontology.
    /// </summary>
    public static class Semantics
    {


        /// <summary>
        /// The well-known prefix of the graph-database ontology.
        /// </summary>
        public static String GraphDBPrefix          = "http://graph-database.org/graphdb/0.1";



        /// <summary>
        /// The identification of any graph element.
        /// </summary>
        public static String Id                     = "Id";

        /// <summary>
        /// The revision identification of any graph element.
        /// </summary>
        public static String RevId                  = "RevId";

        /// <summary>
        /// The label of any graph element.
        /// </summary>
        public static String Label                  = "Label";

        /// <summary>
        /// The description of any graph element.
        /// </summary>
        public static String Description            = "Description";



        /// <summary>
        /// The default vertex label.
        /// </summary>
        public static String DefaultVertexLabel     = "default";

        /// <summary>
        /// The default edge label.
        /// </summary>
        public static String DefaultEdgeLabel       = "default";

        /// <summary>
        /// The default hyperedge label.
        /// </summary>
        public static String DefaultHyperEdgeLabel  = "default";

        /// <summary>
        /// The default multiedge label.
        /// </summary>
        public static String DefaultMultiEdgeLabel  = "default";


    }

}
