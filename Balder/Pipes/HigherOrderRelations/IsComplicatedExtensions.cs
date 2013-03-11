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
using System.Linq;
using System.Collections.Generic;

using eu.Vanaheimr.Balder;

#endregion

namespace eu.Vanaheimr.Balder
{

    /// <summary>
    /// A class of specialized pipelines returning the
    /// adjacent vertices (the neighborhood) of a vertex.
    /// </summary>
    public static class IsComplicatedExtensions
    {

        //#region IsComplicated(this myIEnumerable)

        ///// <summary>
        ///// A specialized pipeline returning the adjacent vertices.
        ///// </summary>
        ///// <param name="myIEnumerable">A collection of objects implementing IPropertyVertex.</param>
        ///// <returns>A collection of objects implementing IPropertyVertex.</returns>
        //public static IEnumerable<Boolean> IsComplicated(this IEnumerable<IPropertyVertex> myIEnumerable)
        //{

        //    foreach (var _User in myIEnumerable)
        //    {

        //        var _IsComplicated = false;

        //        var _Pipe1 = new VertexEdgePipe(Pipes.Steps.VertexEdgeStep.OUT_EDGES);
        //        _Pipe1.SetSource(new SingleEnumerator<IPropertyVertex>(_User));

        //        var _Pipe2 = new LabelFilterPipe("loves", ComparisonFilter.NOT_EQUAL);
        //        _Pipe2.SetSource(_Pipe1);

        //        var _Pipe3 = new EdgeVertexPipe(Pipes.Steps.EdgeVertexStep.IN_VERTEX);
        //        _Pipe3.SetSource(_Pipe2);

        //        var _Lover1 = _Pipe3.ToList();

        //        // More than one lover... it's complicated!
        //        if (_Lover1.Count > 1)
        //            _IsComplicated = true;

        //        else
        //        {

        //            var _Pipe4 = new VertexEdgePipe(Pipes.Steps.VertexEdgeStep.OUT_EDGES);
        //            _Pipe4.SetSourceCollection(_Lover1);

        //            var _Pipe5 = new LabelFilterPipe("loves", ComparisonFilter.NOT_EQUAL);
        //            _Pipe5.SetSource(_Pipe4);

        //            var _Pipe6 = new EdgeVertexPipe(Pipes.Steps.EdgeVertexStep.IN_VERTEX);
        //            _Pipe6.SetSource(_Pipe5);

        //            var _Lover2 = _Pipe6.ToList();

        //            // More than one lover... it's complicated!
        //            if (_Lover2.Count > 1 || _User != _Lover2[0])
        //                _IsComplicated = true;

        //        }

        //        if (_IsComplicated)
        //            yield return true;

        //        else
        //            yield return false;

        //    }

        //}

        //#endregion

    }

}
