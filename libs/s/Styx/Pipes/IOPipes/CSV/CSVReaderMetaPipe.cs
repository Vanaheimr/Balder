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
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    /// <summary>
    /// The CSVMetaPipe splits the lines of the found csv files into pieces.
    /// </summary>
    public class CSVReaderMetaPipe : AbstractMetaPipe<String, String[]>, IMetaPipe<String, String[]>
    {

        #region Constructor(s)

        #region CSVReaderMetaPipe(SearchPattern = "*", SearchOption = TopDirectoryOnly, FileFilter = null, IgnoreLines = null, Seperators = null, StringSplitOptions = None, ExpectedNumberOfColumns = null, FailOnWrongNumberOfColumns = false)

        /// <summary>
        /// The CSVMetaPipe splits the lines of the found csv files into pieces.
        /// </summary>
        /// <param name="SearchPattern">A simple search pattern like "*.jpg".</param>
        /// <param name="SearchOption">Include or do not include subdirectories.</param>
        /// <param name="FileFilter">A delegate for filtering the found files.</param>
        /// <param name="IgnoreLines">A regular expression indicating which input strings should be ignored. Default: All lines starting with a '#'.</param>
        /// <param name="Seperators">An array of string used to split the input strings.</param>
        /// <param name="StringSplitOptions">Split options, e.g. remove empty entries.</param>
        /// <param name="ExpectedNumberOfColumns">If the CSV file had a schema, a specific number of columns can be expected. If instead it is a list of values no such value can be expected.</param>
        /// <param name="FailOnWrongNumberOfColumns">What to do when the current and expected number of columns do not match.</param>
        /// <param name="IEnumerable">An optional IEnumerable&lt;S&gt; as element source.</param>
        /// <param name="IEnumerator">An optional IEnumerator&lt;S&gt; as element source.</param>
        public CSVReaderMetaPipe(// Parameters for the FileFilterPipe
                                 String              SearchPattern              = "*",
                                 SearchOption        SearchOption               = SearchOption.TopDirectoryOnly,
                                 FileFilter          FileFilter                 = null,
                                 // Parameters for the CSVPipe
                                 Regex               IgnoreLines                = null,
                                 String[]            Seperators                 = null,
                                 StringSplitOptions  StringSplitOptions         = StringSplitOptions.None,
                                 UInt16?             ExpectedNumberOfColumns    = null,
                                 Boolean             FailOnWrongNumberOfColumns = false,
                                 IEnumerable<String> IEnumerable                = null,
                                 IEnumerator<String> IEnumerator                = null)

            : base(new List<IPipe>() {

                       new FileFilterPipe(
                               SearchPattern: SearchPattern,
                               SearchOption:  SearchOption,
                               FileFilter:    FileFilter,
                               IEnumerable:   IEnumerable),

                       new OpenStreamPipe(
                               FileMode.Open,
                               FileAccess.Read,
                               FileShare.Read,
                               64000
#if SILVERLIGHT
                               ),
#else
                               , FileOptions.SequentialScan),
#endif

                       new FuncPipe<Stream, StreamReader>(
                               (stream) => new StreamReader(stream)),

                       new FuncPipe<StreamReader, IEnumerable<String>>(
                               (streamReader) => streamReader.GetLines()),

                       new UnrollPipe<String>(),

                       new CSVReaderPipe(
                               IgnoreLines:                IgnoreLines,
                               Seperators:                 Seperators,
                               StringSplitOptions:         StringSplitOptions,
                               ExpectedNumberOfColumns:    ExpectedNumberOfColumns,
                               FailOnWrongNumberOfColumns: FailOnWrongNumberOfColumns)

                   }.ToArray(),

                   IEnumerable,
                   IEnumerator)
                           
        { }

        #endregion

        #endregion

    }

}
