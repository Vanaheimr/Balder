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
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

#endregion

namespace de.ahzf.Blueprints.HTTP
{

    // {
    //   "requestId": 1
    //   "responseId": 1
    //   "duration": 0
    //   "warnings": [ ... ]
    //   "errors": [ ... ]
    //   "results": [ ... ]
    // }

    /// <summary>
    /// GraphServer response object interface.
    /// </summary>
    public interface IResponseRO
    {

        UInt64  RequestId  { get; }
        UInt64  ResponseId { get; }
        UInt64  Duration   { get; }
        JObject Result     { get; }

        IEnumerable<String> Warnings { get; }
        IEnumerable<String> Errors   { get; }

    }

}
