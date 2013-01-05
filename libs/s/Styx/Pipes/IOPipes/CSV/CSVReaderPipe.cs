/*
 * Copyright (c) 2010-2013, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Styx <http://www.github.com/Vanaheimr/Styx>
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
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    /// <summary>
    /// Splits a given strings into elements by a given sperator.
    /// </summary>
    public class CSVReaderPipe : AbstractPipe<String, String[]>
    {

        #region Data

        private readonly Regex               IgnoreLines;  
        private readonly String[]            Seperators;
        private readonly StringSplitOptions  StringSplitOptions;
        private readonly UInt16?             ExpectedNumberOfColumns;
        private readonly Boolean             FailOnWrongNumberOfColumns;
        private readonly Boolean             TrimColumns;

        private readonly Regex               EmptyColumRegex;

        private          String              _CurrentLine;

        #endregion

        #region Constructor(s)

        #region CSVReaderPipe(IgnoreLines = null, Seperators = null, StringSplitOptions = None, ExpectedNumberOfColumns = null, FailOnWrongNumberOfColumns = false)

        /// <summary>
        /// Splits a given strings into elements by a given sperator.
        /// </summary>
        /// <param name="IgnoreLines">A regular expression indicating which input strings should be ignored. Default: All lines starting with a '#'.</param>
        /// <param name="Seperators">An array of string used to split the input strings.</param>
        /// <param name="StringSplitOptions">Split options, e.g. remove empty entries.</param>
        /// <param name="ExpectedNumberOfColumns">If the CSV file had a schema, a specific number of columns can be expected. If instead it is a list of values no such value can be expected.</param>
        /// <param name="FailOnWrongNumberOfColumns">What to do when the current and expected number of columns do not match.</param>
        /// <param name="TrimColumns">Remove leading and trailing whitespaces.</param>
        /// <param name="IEnumerable">An optional enumeration of strings as element source.</param>
        /// <param name="IEnumerator">An optional enumerator of strings as element source.</param>
        public CSVReaderPipe(Regex               IgnoreLines                = null,
                             String[]            Seperators                 = null,
                             StringSplitOptions  StringSplitOptions         = StringSplitOptions.None,
                             UInt16?             ExpectedNumberOfColumns    = null,
                             Boolean             FailOnWrongNumberOfColumns = false,
                             Boolean             TrimColumns                = true,
                             IEnumerable<String> IEnumerable                = null,
                             IEnumerator<String> IEnumerator                = null)

            : base(IEnumerable, IEnumerator)

        {

            this.IgnoreLines                = (IgnoreLines == null) ? new Regex(@"^\#")  : IgnoreLines;
            this.Seperators                 = (Seperators  == null) ? new String[] {","} : Seperators;
            this.StringSplitOptions         = StringSplitOptions;
            this.ExpectedNumberOfColumns    = ExpectedNumberOfColumns;
            this.FailOnWrongNumberOfColumns = FailOnWrongNumberOfColumns;
            this.TrimColumns                = TrimColumns;

            this.EmptyColumRegex            = new Regex("\\" + this.Seperators[0] + "\\s\\" + this.Seperators[0]);
        
        }

        #endregion

        #endregion


        #region MoveNext()

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// True if the enumerator was successfully advanced to the next
        /// element; false if the enumerator has passed the end of the
        /// collection.
        /// </returns>
        public override Boolean MoveNext()
        {

            if (_InputEnumerator == null)
                return false;

            while (true)
            {

                if (_InputEnumerator.MoveNext())
                {

                    // Remove leading and trailing whitespaces
                    _CurrentLine = _InputEnumerator.Current.Trim();

                    // Ignore empty lines
                    if (_CurrentLine == null | _CurrentLine == "")
                        continue;

                    // Ignore lines matching the IgnoreLines regular expression
                    if (IgnoreLines.IsMatch(_CurrentLine))
                        continue;

                    // Remove patterns like ",  ,"
                    if (StringSplitOptions == StringSplitOptions.RemoveEmptyEntries)
                        _CurrentLine = EmptyColumRegex.Replace(_CurrentLine, Seperators[0]);
                    
                    // Split the current line
                    _CurrentElement = _CurrentLine.Split(Seperators, StringSplitOptions);

                    // Remove empty arrays
                    if (StringSplitOptions == StringSplitOptions.RemoveEmptyEntries &
                        _CurrentElement.Length == 0)
                        continue;

                    // The found number of columns does not fit the expected number
                    if (ExpectedNumberOfColumns != null &&
                        ExpectedNumberOfColumns != _CurrentElement.Length)
                        {
                            if (FailOnWrongNumberOfColumns)
                                throw new Exception("CVSPipe!");
                            else
                                continue;
                        }

                    // Remove leading and trailing whitespaces from each column
                    if (TrimColumns)
                        _CurrentElement = _CurrentElement.Select(s => s.Trim()).ToArray();

                    return true;

                }

                return false;

            }

        }

        #endregion


        #region ToString()

        /// <summary>
        /// A string representation of this pipe.
        /// </summary>
        public override String ToString()
        {
            return base.ToString() + "<" + _InputEnumerator.Current + ">";
        }

        #endregion

    }

}
