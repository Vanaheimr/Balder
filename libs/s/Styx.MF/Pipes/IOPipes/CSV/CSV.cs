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
    /// Utilities to read and write CSV files.
    /// </summary>
    public static class CSV
    {

        #region GetLines(this StreamReader)

        /// <summary>
        /// Yields one line from the given stream reader.
        /// </summary>
        /// <param name="StreamReader">The stream to read the lines from.</param>
        /// <returns>A single line.</returns>
        public static IEnumerable<String> GetLines(this StreamReader StreamReader)
        {

            if (StreamReader == null)
                throw new ArgumentNullException("StreamReader", "StreamReader must not be null!");

            String _Line;

            while ((_Line = StreamReader.ReadLine()) != null)
                yield return _Line;

        }

        #endregion

        #region GetMultipleLines(this StreamReader, NumberOfLines)

        /// <summary>
        /// Yields multiple lines from the given stream reader.
        /// </summary>
        /// <param name="StreamReader">The stream to read the lines from.</param>
        /// <param name="NumberOfLines">The number of lines to read at once.</param>
        /// <returns>Multiple lines.</returns>
        public static IEnumerable<IEnumerable<String>> GetMultipleLines(this StreamReader StreamReader, Int32 NumberOfLines)
        {

            if (StreamReader == null)
                throw new ArgumentNullException("myStreamReader must not be null!");

            var _Lines = new String[NumberOfLines];
            var _Number = 0;
            var _Line = "";

            while ((_Line = StreamReader.ReadLine()) != null)
            {

                if (!_Line.StartsWith("#") && !_Line.StartsWith("//"))
                {

                    _Lines[_Number] = _Line;

                    if (_Number == NumberOfLines - 1)
                    {
                        yield return _Lines;
                        _Lines = new String[NumberOfLines];
                        _Number = -1;
                    }

                    _Number++;

                }

            }

            Array.Resize(ref _Lines, _Number);
            yield return _Lines;

        }

        #endregion

    }

}
