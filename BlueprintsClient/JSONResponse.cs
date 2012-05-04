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
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

#endregion

namespace de.ahzf.Blueprints.HTTP
{

    // General JSON format:
    // {
    //   "requestId": 1
    //   "responseId": 1
    //   "duration": 0
    //   "warnings": [ ... ]
    //   "errors": [ ... ]
    //   "result": ...
    // }

    /// <summary>
    /// GraphServer JSON response object.
    /// </summary>
    public class JSONResponse : IResponseRO
    {

        #region Data

        private readonly JObject JSONData;

        #endregion

        #region Properties

        /// <summary>
        /// Every request needs to have a request Id, which will be
        /// copied to the result in order to match request and
        /// response.
        /// </summary>
        public UInt64  RequestId   { get; private set; }

        /// <summary>
        /// A request can lead to multiple independent respones.
        /// To keep strict ordering every response needs to have
        /// and response Id.
        /// </summary>
        public UInt64  ResponseId  { get; private set; }

        /// <summary>
        /// The processing time to complete the request.
        /// </summary>
        public UInt64  Duration    { get; private set; }


        /// <summary>
        /// The list of informational warnings.
        /// </summary>
        public IEnumerable<String> Warnings { get; private set; }

        /// <summary>
        /// The list of fatal errors.
        /// </summary>
        public IEnumerable<String> Errors { get; private set; }


        /// <summary>
        /// The list of results.
        /// </summary>
        public JObject Result      { get; private set; }

        #endregion

        #region Constructor(s)

        #region (private) JSONResponse(JSON)

        /// <summary>
        /// Create a new GraphServer response object.
        /// </summary>
        private JSONResponse(String JSON)
        {
            try
            {
                JSONData   = JObject.Parse(JSON);
                RequestId  = (UInt64) JSONData["requestId"];
                ResponseId = (UInt64) JSONData["responseId"];
                Duration   = (UInt64) JSONData["duration"];
                Warnings   = from p in JSONData["warnings"].Children() select (String) p;
                Errors     = from p in JSONData["errors"]  .Children() select (String) p;
                Result     = JSONData["result"] as JObject;
            }
            catch (Exception e)
            {
                Errors     = new List<String>() { "The received JSON string could not be parsed! [\"" + Environment.NewLine + JSON + "\"]" };
                Result     = new JObject();
            }
        }

        #endregion
        
        #endregion


        #region (static) ParseJSON(JSON)

        /// <summary>
        /// Generate a new response object from the given JSON string.
        /// </summary>
        /// <param name="JSON">A JSON string.</param>
        /// <returns>A new response object.</returns>
        public static JSONResponse ParseJSON(String JSON)
        {
            return new JSONResponse(JSON);
        }

        #endregion


        #region ToString()

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return JSONData.ToString();
        }

        #endregion

    }

}
