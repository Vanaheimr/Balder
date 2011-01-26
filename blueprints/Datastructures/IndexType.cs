/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET
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

namespace de.ahzf.blueprints
{

    /// <summary>
    /// An Index is either manual or automatic.
    /// Automatic types must implement AutomaticIndex.
    /// </summary>
    public enum IndexType
    {
        
        /// <summary>
        /// Manual indexing mode
        /// </summary>
        MANUAL,

        /// <summary>
        /// Automatic indexing
        /// </summary>
        AUTOMATIC

    }

}
