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

namespace de.ahzf.Blueprints.Tools
{

    /// <summary>
    /// Some type extensions to check for numeric types.
    /// </summary>
    public static class NumericTypeExtensions
    {

        #region IsArithmetic(this Type)

        /// <summary>
        /// Checks whether the given type is arithmetic or not.
        /// </summary>
        /// <param name="Type">A type.</param>
        /// <returns>True if the type is arithmetic; False otherwise.</returns>
        public static Boolean IsArithmetic(this Type Type)
        {

            switch (Type.GetTypeCode(Type))
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Double:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
            }

            return false;

        }

        #endregion

        #region IsInteger(this Type)

        /// <summary>
        /// Checks whether the given type is any kind of an integer or not.
        /// </summary>
        /// <param name="Type">A type.</param>
        /// <returns>True if the type is any kind of an integer; False otherwise.</returns>
        public static Boolean IsInteger(this Type Type)
        {

            switch (Type.GetTypeCode(Type))
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
            }

            return false;

        }

        #endregion

        #region IsFloatingPoint(this Type)

        /// <summary>
        /// Checks whether the given type is any kind of a floating point or not.
        /// </summary>
        /// <param name="Type">A type.</param>
        /// <returns>True if the type is any kind of a floating point; False otherwise.</returns>
        public static Boolean IsFloatingPoint(Type Type)
        {

            switch (Type.GetTypeCode(Type))
            {
                case TypeCode.Single:
                case TypeCode.Double:
                    return true;
            }

            return false;

        }

        #endregion

        #region IsNumeric(this Type)

        /// <summary>
        /// Checks whether the given type is any kind of a numeric or not.
        /// </summary>
        /// <param name="Type">A type.</param>
        /// <returns>True if the type is any kind of a numeric; False otherwise.</returns>
        public static Boolean IsNumeric(this Type Type)
        {

            switch (Type.GetTypeCode(Type))
            {
                case TypeCode.Char:
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Double:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
            }

            return false;

        }

        #endregion

    }

}
