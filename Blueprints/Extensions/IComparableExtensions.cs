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
    /// Extensions to the IComparable interface.
    /// </summary>
    public static class IComparableExtensions
    {

        #region IsLessThan(Value1, Value2)

        /// <summary>
        /// Checks if the first value is less than the second value.
        /// </summary>
        /// <param name="Value1">A value of type T.</param>
        /// <param name="Value2">A value of type T.</param>
        /// <returns>True if the first value is less than the second value; False otherwise.</returns>
        public static Boolean IsLessThan<T>(this T Value1, T Value2)
            where T : IComparable<T>, IComparable
        {
            return Value1.CompareTo(Value2) < 0;
        }

        #endregion

        #region IsLessThanOrEquals(Value1, Value2)

        /// <summary>
        /// Checks if the first value is less than or equals the second value.
        /// </summary>
        /// <param name="Value1">A value of type T.</param>
        /// <param name="Value2">A value of type T.</param>
        /// <returns>True if the first value is less than or equals the second value; False otherwise.</returns>
        public static Boolean IsLessThanOrEquals<T>(this T Value1, T Value2)
            where T : IComparable<T>, IComparable
        {
            return Value1.CompareTo(Value2) <= 0;
        }

        #endregion

        #region IsLargerThan(Value1, Value2)

        /// <summary>
        /// Checks if the first value is larger than the second value.
        /// </summary>
        /// <param name="Value1">A value of type T.</param>
        /// <param name="Value2">A value of type T.</param>
        /// <returns>True if the first value is larger than the second value; False otherwise.</returns>
        public static Boolean IsLargerThan<T>(this T Value1, T Value2)
            where T : IComparable<T>, IComparable
        {
            return Value1.CompareTo(Value2) > 0;
        }

        #endregion

        #region IsLargerThanOrEquals(Value1, Value2)

        /// <summary>
        /// Checks if the first value is larger than or equals the second value.
        /// </summary>
        /// <param name="Value1">A value of type T.</param>
        /// <param name="Value2">A value of type T.</param>
        /// <returns>True if the first value is larger than or equals the second value; False otherwise.</returns>
        public static Boolean IsLargerThanOrEquals<T>(this T Value1, T Value2)
            where T : IComparable<T>, IComparable
        {
            return Value1.CompareTo(Value2) >= 0;
        }

        #endregion

    }

}
