/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
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

namespace de.ahzf.Styx
{

    /// <summary>
    /// The FileFilterPipe scans the given directories
    /// for files matching the given filters.
    /// </summary>
    public class FileFilterPipe : AbstractPipe<String, FileInfo>
    {

        #region Data

        private readonly String                _SearchPattern;
        private readonly SearchOption          _SearchOption;
        private readonly FileFilter            _FileFilter;
        private          IEnumerator<FileInfo> _TempIterator;

        #endregion

        #region Constructor(s)

        #region FileFilterPipe(SearchPattern = "*", SearchOption = TopDirectoryOnly, FileFilter = null)

        /// <summary>
        /// Scans the given directories for files matching the given filters.
        /// </summary>
        /// <param name="SearchPattern">A simple search pattern like "*.jpg".</param>
        /// <param name="SearchOption">Include or do not include subdirectories.</param>
        /// <param name="FileFilter">A delegate for filtering the found files.</param>
        /// <param name="IEnumerable">An optional enumation of directories as element source.</param>
        /// <param name="IEnumerator">An optional enumerator of directories as element source.</param>
        public FileFilterPipe(String              SearchPattern = "*",
                              SearchOption        SearchOption  = SearchOption.TopDirectoryOnly,
                              FileFilter          FileFilter    = null,
                              IEnumerable<String> IEnumerable   = null,
                              IEnumerator<String> IEnumerator   = null)

            : base(IEnumerable, IEnumerator)

        {
            _SearchPattern = SearchPattern;
            _SearchOption  = SearchOption;
            _FileFilter    = FileFilter;
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

            if (_InternalEnumerator == null)
                return false;

            while (true)
            {

                if (_TempIterator != null && _TempIterator.MoveNext())
                {

                    if (_TempIterator.Current != null)
                    {

                        // No advanced filter given or it matches...
                        if (_FileFilter == null ||
                           (_FileFilter != null && !_FileFilter(_TempIterator.Current)))
                        {

                            _CurrentElement = _TempIterator.Current;
                            return true;

                        }

                    }
                    
                }

                if (_InternalEnumerator.MoveNext())
                {

                    try
                    {

                        _TempIterator = new DirectoryInfo(_InternalEnumerator.Current).
                                                          EnumerateFiles(_SearchPattern, _SearchOption).
                                                          GetEnumerator();

                    }
                    catch (Exception)
                    {
                        return false;
                    }

                }

                else
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
            return base.ToString() + "<" + _InternalEnumerator.Current + ">";
        }

        #endregion

    }

}
