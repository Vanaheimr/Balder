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
    /// GraphServer response object.
    /// </summary>
    public class JSONResponseBuilder : IResponse
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

        #region Warnings

        private List<String> _Warnings;

        /// <summary>
        /// The list of informational warnings.
        /// </summary>
        public IEnumerable<String> Warnings
        {
            get
            {
                return _Warnings;
            }
        }

        #endregion

        #region Errors

        private List<String> _Errors;

        /// <summary>
        /// The list of fatal errors.
        /// </summary>
        public IEnumerable<String> Errors
        {
            get
            {
                return _Errors;
            }
        }

        #endregion

        /// <summary>
        /// The list of results.
        /// </summary>
        public JObject Result      { get; private set; }

        #endregion

        #region Constructor(s)

        #region JSONResponseBuilder()

        /// <summary>
        /// Create a new GraphServer response object.
        /// </summary>
        public JSONResponseBuilder()
        {
            _Warnings = new List<String>();
            _Errors   = new List<String>();
        }

        #endregion
        
        #endregion


        public IResponse SetRequestId(UInt64 RequestId)
        {
            this.RequestId = RequestId;
            return this;
        }

        public IResponse SetResponseId(UInt64 ResponseId)
        {
            this.ResponseId = ResponseId;
            return this;
        }

        public IResponse SetDuration(UInt64 Duration)
        {
            this.Duration = Duration;
            return this;
        }

        public IResponse SetResult(JObject Result)
        {
            this.Result = Result;
            return this;
        }

        public IResponse AddWarning(String Warning)
        {
            this._Warnings.Add(Warning);
            return this;
        }

        public IResponse AddError(String Error)
        {
            this._Errors.Add(Error);
            return this;
        }


        #region ToString()

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return JObject.FromObject(new
            {
                requestId  = RequestId,
                responseId = ResponseId,
                duration   = Duration,
                warnings   = _Warnings,
                errors     = _Errors,
                result     = Result
            }).ToString();
        }

        #endregion

    }

}
