/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
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
