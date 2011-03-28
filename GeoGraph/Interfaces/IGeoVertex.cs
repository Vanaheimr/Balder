/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET
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

using de.ahzf.blueprints.Datastructures;
using System.Collections.Generic;

#endregion

namespace de.ahzf.blueprints.GeoGraph
{

    /// <summary>
    /// A simple geo vertex.
    /// </summary>
    public interface IGeoVertex : IGenericVertex<VertexId,    RevisionId, GeoCoordinate,
                                                 EdgeId,      RevisionId, Distance,
                                                 HyperEdgeId, RevisionId, Distance>
    {

        String              Type            { get; }
        String              Id              { get; }
        IEnumerable<String> Properties      { get; }

        GeoCoordinate       GeoCoordinate   { get; set; }

        Double              Latitude        { get; }
        Double              Longitude       { get; }
        Double              Height          { get; }

    }

}
