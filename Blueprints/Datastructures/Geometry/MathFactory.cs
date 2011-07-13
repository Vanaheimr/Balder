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
    public static class MathFactory<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        /// <summary>
        /// A appropriate math object for datatype T.
        /// </summary>
        public static IMath<T> Instance
        {
            get
            {
                
                if (typeof(T) == typeof(Double))
                    return MathDouble.Instance as IMath<T>;

                else if (typeof(T) == typeof(Single))
                    return MathSingle.Instance as IMath<T>;

                else if (typeof(T) == typeof(Int32))
                    return MathInt32. Instance as IMath<T>;

                else
                    throw new Exception("No math class found for datatype '" + typeof(T).Name + "'!");

            }
        }

    }

}
