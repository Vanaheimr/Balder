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

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    public static class IOPipeExtensions
    {

        #region FileFilterPipe(this IEnumerable, SearchPattern = "*", SearchOption = TopDirectoryOnly, FileFilter = null)

        /// <summary>
        /// Scans the given directories for files matching the given filters.
        /// </summary>
        /// <param name="IEnumerable">An enumeration of directories.</param>
        /// <param name="SearchPattern">A simple search pattern like "*.jpg".</param>
        /// <param name="SearchOption">Include or do not include subdirectories.</param>
        /// <param name="FileFilter">A delegate for filtering the found files.</param>
        /// <returns>An enumeration of file infos.</returns>
        public static IEnumerable<FileInfo> FileFilterPipe(this IEnumerable<String> IEnumerable,
                                                                String              SearchPattern = "*",
                                                                SearchOption        SearchOption  = SearchOption.TopDirectoryOnly,
                                                                FileFilter          FileFilter    = null)
        {
            return new FileFilterPipe(SearchPattern, SearchOption, FileFilter, IEnumerable, null);
        }

        #endregion

        #region FileFilterPipe(this IEnumerator, SearchPattern = "*", SearchOption = TopDirectoryOnly, FileFilter = null)

        /// <summary>
        /// Scans the given directories for files matching the given filters.
        /// </summary>
        /// <param name="IEnumerator">An enumerator of directories.</param>
        /// <param name="SearchPattern">A simple search pattern like "*.jpg".</param>
        /// <param name="SearchOption">Include or do not include subdirectories.</param>
        /// <param name="FileFilter">A delegate for filtering the found files.</param>
        /// <returns>An enumeration of file infos.</returns>
        public static IEnumerable<FileInfo> FileFilterPipe(this IEnumerator<String> IEnumerator,
                                                                String              SearchPattern = "*",
                                                                SearchOption        SearchOption  = SearchOption.TopDirectoryOnly,
                                                                FileFilter          FileFilter    = null)
        {
            return new FileFilterPipe(SearchPattern, SearchOption, FileFilter, null, IEnumerator);
        }

        #endregion

    }

}
