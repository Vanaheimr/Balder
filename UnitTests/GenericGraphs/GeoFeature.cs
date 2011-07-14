/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
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

namespace de.ahzf.Blueprints.UnitTests.GeoGraph
{

    /// <summary>
    /// A simple geo feature struct.
    /// </summary>
    public struct GeoFeature
    {

        #region Data

        /// <summary>
        /// The name of the geo feature.
        /// </summary>
        public readonly String Name;

        /// <summary>
        /// The latitude of the geo feature.
        /// </summary>
        public readonly Double Latitude;

        /// <summary>
        /// The longitude of the geo feature.
        /// </summary>
        public readonly Double Longitude;

        /// <summary>
        /// The height of the geo feature.
        /// </summary>
        public readonly Double Height;

        #endregion

        #region Constructor(s)

        #region GeoFeature(Name, Latitude, Longitude, Height)

        /// <summary>
        /// A geo feature.
        /// </summary>
        /// <param name="Name">The name of the geo feature.</param>
        /// <param name="Latitude">The latitude of the geo feature.</param>
        /// <param name="Longitude">The longitude of the geo feature.</param>
        /// <param name="Height">The height of the geo feature.</param>
        public GeoFeature(String Name, Double Latitude, Double Longitude, Double Height)
        {
            this.Name        = Name;
            this.Latitude    = Latitude;
            this.Longitude   = Longitude;
            this.Height      = Height;
        }

        #endregion

        #endregion

    }

}
