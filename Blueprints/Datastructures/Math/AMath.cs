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

#endregion

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// An abstract math object for datatype T.
    /// </summary>
    /// <typeparam name="T">The internal type of the math object.</typeparam>
    public abstract class AMath<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Properties

        /// <summary>
        /// A appropriate math object for datatype T.
        /// </summary>
        public IMath<T> Math { get; private set; }

        #endregion

        #region Constructor(s)

        #region Math()

        /// <summary>
        /// Create a new abstract Math object for datatype T.
        /// </summary>
        public AMath()
        {
            
            if (typeof(T) == typeof(Double))
                this.Math = MathDouble.Instance as IMath<T>;

            else if (typeof(T) == typeof(Single))
                this.Math = MathSingle.Instance as IMath<T>;

            else if (typeof(T) == typeof(Int32))
                this.Math = MathInt32. Instance as IMath<T>;

            if (this.Math == null)
                throw new Exception("No math class found for datatype '" + typeof(T).Name + "'!");

        }

        #endregion

        #endregion

    }

}
