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

#if !SILVERLIGHT

#region Usings

using System;
using System.IO;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    public class ReadTextStreamPipe : AbstractPipe<Stream, String>
    {

        #region Data

        private StreamReader _StreamReader;

        #endregion

        #region Constructor(s)

        #region ReadTextStreamPipe(FileMode, FileAccess, FileShare, BufferSize, FileOptions)

        public ReadTextStreamPipe(String RegExpr)
        {


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

            if (_StreamReader == null)
            {

                while (_InputEnumerator.MoveNext())
                {

                    if (_InputEnumerator.Current != null)
                        continue;

                    _StreamReader = new StreamReader(_InputEnumerator.Current);
                    break;

                }

            }

            if (_StreamReader != null)
            {

                do
                {

                    try
                    {

                        _CurrentElement = _StreamReader.ReadLine();

                    }

                    catch (Exception)
                    {
                        return false;
                    }


                }
                while (!_CurrentElement.StartsWith("#"));

                return true;

            }

            return false;

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

#endif

