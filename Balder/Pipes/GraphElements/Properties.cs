/*
 * Copyright (c) 2010-2015, Achim 'ahzf' Friedland <achim@graphdefined.org>
 * This file is part of Balder <http://www.github.com/Vanaheimr/Balder>
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
using System.Collections.Generic;

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Styx;
using org.GraphDefined.Vanaheimr.Illias.Collections;

#endregion

namespace org.GraphDefined.Vanaheimr.Balder
{

    #region Properties

    /// <summary>
    /// Extension methods for the Properties.
    /// </summary>
    public static class PropertiesExtensions
    {

        #region GetProperty<TKey, TValue>(this IReadOnlyProperties, Key)
        // Just an alternative syntax!

        /// <summary>
        /// Return the object value of type TValue associated with the provided property key.
        /// </summary>
        /// <typeparam name="TKey">The type of the property key.</typeparam>
        /// <typeparam name="TValue">The type of the property value.</typeparam>
        /// <param name="IReadOnlyProperties">An object implementing IReadOnlyProperties.</param>
        /// <param name="Key">The property key.</param>
        public static IEnumerable<TValue> P<TKey, TValue>(this IEnumerable<IReadOnlyProperties<TKey, TValue>> IReadOnlyProperties, TKey Key)
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
        {

            if (IReadOnlyProperties == null)
                yield break;

            TValue _Value;

            foreach (var p in IReadOnlyProperties)
            {

                if (p.TryGetProperty(Key, out _Value))
                    yield return _Value;

            }

        }

        #endregion

    }

    #endregion

}
