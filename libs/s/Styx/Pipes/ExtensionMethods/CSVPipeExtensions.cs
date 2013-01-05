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
using System.Collections.Generic;
using System.Text.RegularExpressions;

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    /// <summary>
    /// Extention methods for CSV pipes.
    /// </summary>
    public static class CSVPipeExtensions
    {

        #region CSVPipe(this IEnumerable, IgnoreLines = null, Seperators = null, StringSplitOptions = None, ExpectedNumberOfColumns = null, FailOnWrongNumberOfColumns = false, TrimColumns = true)

        /// <summary>
        /// Splits a given strings into elements by a given sperator.
        /// </summary>
        /// <param name="IEnumerable">An enumeration of strings.</param>
        /// <param name="IgnoreLines">A regular expression indicating which input strings should be ignored. Default: All lines starting with a '#'.</param>
        /// <param name="Seperators">An array of string used to split the input strings.</param>
        /// <param name="StringSplitOptions">Split options, e.g. remove empty entries.</param>
        /// <param name="ExpectedNumberOfColumns">If the CSV file had a schema, a specific number of columns can be expected. If instead it is a list of values no such value can be expected.</param>
        /// <param name="FailOnWrongNumberOfColumns">What to do when the current and expected number of columns do not match.</param>
        /// <param name="TrimColumns">Remove leading and trailing whitespaces.</param>
        /// <returns>An enumeration of string arrays.</returns>
        public static IEnumerable<String[]> CSVPipe(this IEnumerable<String> IEnumerable,
                                                         Regex               IgnoreLines                = null,
                                                         String[]            Seperators                 = null,
                                                         StringSplitOptions  StringSplitOptions         = StringSplitOptions.None,
                                                         UInt16?             ExpectedNumberOfColumns    = null,
                                                         Boolean             FailOnWrongNumberOfColumns = false,
                                                         Boolean             TrimColumns                = true)
        {
            return new CSVReaderPipe(IgnoreLines, Seperators, StringSplitOptions, ExpectedNumberOfColumns, FailOnWrongNumberOfColumns, TrimColumns, IEnumerable, null);
        }

        #endregion

        #region CSVPipe(this IEnumerator, IgnoreLines = null, Seperators = null, StringSplitOptions = None, ExpectedNumberOfColumns = null, FailOnWrongNumberOfColumns = false, TrimColumns = true)

        /// <summary>
        /// Splits a given strings into elements by a given sperator.
        /// </summary>
        /// <param name="IEnumerator">An enumerator of strings.</param>
        /// <param name="IgnoreLines">A regular expression indicating which input strings should be ignored. Default: All lines starting with a '#'.</param>
        /// <param name="Seperators">An array of string used to split the input strings.</param>
        /// <param name="StringSplitOptions">Split options, e.g. remove empty entries.</param>
        /// <param name="ExpectedNumberOfColumns">If the CSV file had a schema, a specific number of columns can be expected. If instead it is a list of values no such value can be expected.</param>
        /// <param name="FailOnWrongNumberOfColumns">What to do when the current and expected number of columns do not match.</param>
        /// <param name="TrimColumns">Remove leading and trailing whitespaces.</param>
        /// <returns>An enumeration of string arrays.</returns>
        public static IEnumerable<String[]> CSVPipe(this IEnumerator<String> IEnumerator,
                                                         Regex               IgnoreLines                = null,
                                                         String[]            Seperators                 = null,
                                                         StringSplitOptions  StringSplitOptions         = StringSplitOptions.None,
                                                         UInt16?             ExpectedNumberOfColumns    = null,
                                                         Boolean             FailOnWrongNumberOfColumns = false,
                                                         Boolean             TrimColumns                = true)
        {
            return new CSVReaderPipe(IgnoreLines, Seperators, StringSplitOptions, ExpectedNumberOfColumns, FailOnWrongNumberOfColumns, TrimColumns, null, IEnumerator);
        }

        #endregion


        #region FixLineBreaks(this IEnumerable, StartOfNewLineRegExpr, NewLineSeperator = "<br>", IEnumerable = null, IEnumerator = null)

        /// <summary>
        /// Sometimes there are unwanted new line characters within CSV files.
        /// This pipe tries to detect real new lines based on the given regular expression
        /// and concatenates the dangling lines using the given NewLineSeperator.
        /// </summary>
        /// <param name="IEnumerable">An enumeration of lines.</param>
        /// <param name="StartOfNewLineRegExpr">A regular expression detecting real new lines.</param>
        /// <param name="NewLineSeperator">The new line seperator between the prior dangling lines.</param>
        public static IEnumerable<String> FixLineBreaks(this IEnumerable<String> IEnumerable,
                                                        Regex                    StartOfNewLineRegExpr,
                                                        String                   NewLineSeperator = "<br>")
        {
            return new FixLineBreaksPipe(StartOfNewLineRegExpr, NewLineSeperator, IEnumerable);
        }

        #endregion

        #region FixLineBreaks(this IEnumerable, StartOfNewLineRegExprString, NewLineSeperator = "<br>", IEnumerable = null, IEnumerator = null)

        /// <summary>
        /// Sometimes there are unwanted new line characters within CSV files.
        /// This pipe tries to detect real new lines based on the given regular expression
        /// and concatenates the dangling lines using the given NewLineSeperator.
        /// </summary>
        /// <param name="IEnumerable">An enumeration of lines.</param>
        /// <param name="StartOfNewLineRegExprString">A regular expression detecting real new lines.</param>
        /// <param name="NewLineSeperator">The new line seperator between the prior dangling lines.</param>
        public static IEnumerable<String> FixLineBreaks(this IEnumerable<String> IEnumerable,
                                                        String                   StartOfNewLineRegExprString,
                                                        String                   NewLineSeperator = "<br>")
        {
            return new FixLineBreaksPipe(StartOfNewLineRegExprString, NewLineSeperator, IEnumerable);
        }

        #endregion

        #region FixLineBreaks(this IEnumerator, StartOfNewLineRegExpr, NewLineSeperator = "<br>", IEnumerable = null, IEnumerator = null)

        /// <summary>
        /// Sometimes there are unwanted new line characters within CSV files.
        /// This pipe tries to detect real new lines based on the given regular expression
        /// and concatenates the dangling lines using the given NewLineSeperator.
        /// </summary>
        /// <param name="IEnumerator">An enumerator of lines.</param>
        /// <param name="StartOfNewLineRegExpr">A regular expression detecting real new lines.</param>
        /// <param name="NewLineSeperator">The new line seperator between the prior dangling lines.</param>
        public static IEnumerable<String> FixLineBreaks(this IEnumerator<String> IEnumerator,
                                                        Regex                    StartOfNewLineRegExpr,
                                                        String                   NewLineSeperator = "<br>")
        {
            return new FixLineBreaksPipe(StartOfNewLineRegExpr, NewLineSeperator, null, IEnumerator);
        }

        #endregion

        #region FixLineBreaks(this IEnumerator, StartOfNewLineRegExprString, NewLineSeperator = "<br>", IEnumerable = null, IEnumerator = null)

        /// <summary>
        /// Sometimes there are unwanted new line characters within CSV files.
        /// This pipe tries to detect real new lines based on the given regular expression
        /// and concatenates the dangling lines using the given NewLineSeperator.
        /// </summary>
        /// <param name="IEnumerator">An enumerator of lines.</param>
        /// <param name="StartOfNewLineRegExprString">A regular expression detecting real new lines.</param>
        /// <param name="NewLineSeperator">The new line seperator between the prior dangling lines.</param>
        public static IEnumerable<String> FixLineBreaks(this IEnumerator<String> IEnumerator,
                                                        String                   StartOfNewLineRegExprString,
                                                        String                   NewLineSeperator = "<br>")
        {
            return new FixLineBreaksPipe(StartOfNewLineRegExprString, NewLineSeperator, null, IEnumerator);
        }

        #endregion

    }

}
