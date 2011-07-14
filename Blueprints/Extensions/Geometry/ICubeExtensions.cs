/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET <http://www.github.com/ahzf/Blueprints.NET>
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
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// Extensions to the ICube interface.
    /// </summary>
    public static class ICubeExtensions
    {

        #region Contains(this ICube, IVoxel)

        /// <summary>
        /// Checks if the given voxel is located
        /// within the given cube.
        /// </summary>
        /// <param name="ICube">A cube of type T.</param>
        /// <param name="IVoxel">A voxel of type T.</param>
        /// <returns>True if the voxel is located within the given cube; False otherwise.</returns>
        public static Boolean Contains<T>(this ICube<T> ICube, IVoxel<T> IVoxel)
            where T : IEquatable<T>, IComparable<T>, IComparable
        {
            return ICube.Contains(IVoxel.X, IVoxel.Y, IVoxel.Z);
        }

        #endregion

    }

}
