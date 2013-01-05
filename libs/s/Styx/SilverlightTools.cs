/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Pipes.NET <http://www.github.com/ahzf/Pipes.NET>
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

#if SILVERLIGHT

#region Usings

using System;
using System.Threading;

#endregion

namespace de.ahzf.Silverlight
{

    public static class SilverlightTools
    {

        /// <summary>
        /// Silverlight is stupid... :(
        /// </summary>
        public static T CompareExchange<T>(ref T location1, T value, T comparand)
        {

            Object _location1 = location1;
            Object _value = value;
            Object _comparand = comparand;

            return (T) Interlocked.CompareExchange(ref _location1, _value, _comparand);

        }

    }

}

#endif
