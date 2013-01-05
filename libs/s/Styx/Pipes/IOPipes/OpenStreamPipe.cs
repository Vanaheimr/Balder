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

//#if !SILVERLIGHT

#region Usings

using System;
using System.IO;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    /// <summary>
    /// The OpenStreamPipe opens the given files
    /// and returns a stream of bytes.
    /// </summary>
    public class OpenStreamPipe : AbstractPipe<FileInfo, Stream>
    {

        #region Data

        private readonly FileMode              _FileMode;
        private readonly FileAccess            _FileAccess;
        private readonly FileShare             _FileShare;
        private readonly UInt32                _BufferSize;
#if !SILVERLIGHT
        private readonly FileOptions           _FileOptions;
#endif

        #endregion

        #region Constructor(s)

        #region OpenStreamPipe(FileMode, FileAccess, FileShare, BufferSize, FileOptions)

        /// <summary>
        /// Opens the given files and returns a stream of bytes.
        /// </summary>
        /// <param name="FileMode">A System.IO.FileMode constant that determines how to open or create the file.</param>
        /// <param name="FileAccess">A System.IO.FileAccess constant that determines how the file can be accessed by the FileStream object. This gets the System.IO.FileStream.CanRead and System.IO.FileStream.CanWrite properties of the FileStream object. System.IO.FileStream.CanSeek is true if path specifies a disk file.</param>
        /// <param name="FileShare">A System.IO.FileShare constant that determines how the file will be shared by processes.</param>
        /// <param name="BufferSize">A positive System.Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes.</param>
        /// <param name="FileOptions">A System.IO.FileOptions value that specifies additional file options.</param>
        /// <param name="IEnumerable">An optional enumation of directories as element source.</param>
        /// <param name="IEnumerator">An optional enumerator of directories as element source.</param>
        public OpenStreamPipe(FileMode              FileMode,
                              FileAccess            FileAccess,
                              FileShare             FileShare,
                              UInt32                BufferSize,
#if !SILVERLIGHT
                              FileOptions           FileOptions,
#endif
                              IEnumerable<FileInfo> IEnumerable = null,
                              IEnumerator<FileInfo> IEnumerator = null)

            : base(IEnumerable, IEnumerator)

        {

            if (_BufferSize > Int32.MaxValue)
                throw new ArgumentException("The BufferSize must not be larger than Int32.MaxValue!");

            _FileMode    = FileMode;
            _FileAccess  = FileAccess;
            _FileShare   = FileShare;
            _BufferSize  = BufferSize;
#if !SILVERLIGHT
            _FileOptions = FileOptions;
#endif

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

            while (_InputEnumerator.MoveNext())
            {

#if SILVERLIGHT
                _CurrentElement = new FileStream(_InternalEnumerator.Current.FullName, _FileMode, _FileAccess, _FileShare, (Int32) _BufferSize);
#else
                _CurrentElement = new FileStream(_InputEnumerator.Current.FullName, _FileMode, _FileAccess, _FileShare, (Int32) _BufferSize, _FileOptions);
#endif
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

//#endif

