﻿///*
// * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
// * This file is part of Balder <http://www.github.com/Vanaheimr/Balder>
// *
// * Licensed under the Apache License, Version 2.0 (the "License");
// * you may not use this file except in compliance with the License.
// * You may obtain a copy of the License at
// *
// *     http://www.apache.org/licenses/LICENSE-2.0
// *
// * Unless required by applicable law or agreed to in writing, software
// * distributed under the License is distributed on an "AS IS" BASIS,
// * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// * See the License for the specific language governing permissions and
// * limitations under the License.
// */

//#region Usings

//using System;
//using System.Linq;
//using System.IO;
//using System.Collections.Generic;
//using System.Text.RegularExpressions;
//using de.ahzf.Vanaheimr.Styx;

//#endregion

//namespace de.ahzf.Vanaheimr.Balder
//{

//    /// <summary>
//    /// Reads rdf tuples in notation 3 format from the given
//    /// enumeration of strings.
//    /// </summary>
//    public class N3ImportPipe : AbstractPipe<String, String>
//    {

//        #region Data

//        private readonly Regex               IgnoreLines;  
//        private readonly String[]            Seperators;
//        private readonly StringSplitOptions  StringSplitOptions;
//        private readonly UInt16?             ExpectedNumberOfColumns;
//        private readonly Boolean             FailOnWrongNumberOfColumns;
//        private readonly Boolean             TrimColumns;

//        private readonly Regex               EmptyColumRegex;

//        private          String              _CurrentLine;

//        #endregion

//        #region Constructor(s)

//        #region N3ImportPipe(IgnoreLines = null, Seperators = null)

//        /// <summary>
//        /// Reads rdf tuples in notation 3 format from the given
//        /// enumeration of strings.
//        /// </summary>
//        /// <param name="IgnoreLines">A regular expression indicating which input strings should be ignored. Default: All lines starting with a '#'.</param>
//        /// <param name="Seperators">An array of string used to split the input strings.</param>
//        /// <param name="IEnumerable">An optional enumeration of strings as element source.</param>
//        /// <param name="IEnumerator">An optional enumerator of strings as element source.</param>
//        public N3ImportPipe(Regex               IgnoreLines                = null,
//                            String[]            Seperators                 = null,
//                            IEnumerable<String> IEnumerable                = null,
//                            IEnumerator<String> IEnumerator                = null)

//            : base(IEnumerable, IEnumerator)

//        {

//            this.IgnoreLines                = (IgnoreLines == null) ? new Regex(@"^\#")        : IgnoreLines;
//            this.Seperators                 = (Seperators  == null) ? new String[] {" ", "\t"} : Seperators;
//            this.StringSplitOptions         = StringSplitOptions.RemoveEmptyEntries;

//        }

//        #endregion

//        #endregion


//        #region MoveNext()

//        /// <summary>
//        /// Advances the enumerator to the next element of the collection.
//        /// </summary>
//        /// <returns>
//        /// True if the enumerator was successfully advanced to the next
//        /// element; false if the enumerator has passed the end of the
//        /// collection.
//        /// </returns>
//        public override Boolean MoveNext()
//        {

//            if (_InternalEnumerator == null)
//                return false;

//            while (true)
//            {

//                if (_InternalEnumerator.MoveNext())
//                {

//                    // Remove leading and trailing whitespaces
//                    _CurrentLine = _InternalEnumerator.Current.Trim();

//                    // Ignore empty lines
//                    if (_CurrentLine == null | _CurrentLine == "")
//                        continue;

//                    // Ignore lines matching the IgnoreLines regular expression
//                    if (IgnoreLines.IsMatch(_CurrentLine))
//                        continue;

//                    // Remove patterns like ",  ,"
//                    if (StringSplitOptions == StringSplitOptions.RemoveEmptyEntries)
//                        _CurrentLine = EmptyColumRegex.Replace(_CurrentLine, Seperators[0]);
                    
//                    // Split the current line
//                    _CurrentElement = _CurrentLine.Split(Seperators, StringSplitOptions);

//                    // Remove empty arrays
//                    if (StringSplitOptions == StringSplitOptions.RemoveEmptyEntries &
//                        _CurrentElement.Length == 0)
//                        continue;

//                    // The found number of columns does not fit the expected number
//                    if (ExpectedNumberOfColumns != null &&
//                        ExpectedNumberOfColumns != _CurrentElement.Length)
//                        {
//                            if (FailOnWrongNumberOfColumns)
//                                throw new Exception("CVSPipe!");
//                            else
//                                continue;
//                        }

//                    //// Remove leading and trailing whitespaces from each column
//                    //if (TrimColumns)
//                    //    _CurrentElement = _CurrentElement.Select(s => s.Trim()).ToArray();

//                    return true;

//                }

//                return false;

//            }

//        }

//        #endregion


//        #region ToString()

//        /// <summary>
//        /// A string representation of this pipe.
//        /// </summary>
//        public override String ToString()
//        {
//            return base.ToString() + "<" + _InternalEnumerator.Current + ">";
//        }

//        #endregion

//    }

//}
