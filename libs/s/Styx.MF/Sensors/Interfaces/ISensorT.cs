/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
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

#region Usings

using System;
using System.Collections.Generic;

using de.ahzf.Illias.Commons;

#endregion

namespace de.ahzf.Styx.Sensors
{

    #region ISensor<TId>

    /// <summary>
    /// The common generic ISensor interface.
    /// </summary>
    /// <typeparam name="TId">The type of the unique sensor identification.</typeparam>
    public interface ISensor<TId> : ISensor,
                                    IIdentifier<TId>

        where TId : IEquatable<TId>, IComparable<TId>, IComparable

    { }

    #endregion

    #region ISensor<TId, TValue>

    /// <summary>
    /// The generic ISensor interface with the type of the measureds values.
    /// </summary>
    /// <typeparam name="TId">The type of the unique sensor identification.</typeparam>
    /// <typeparam name="TValue">The type of the measured values.</typeparam>
    public interface ISensor<TId, TValue> : ISensor<TId>,
                                            IEnumerable<TValue>,
                                            IEnumerator<TValue>

        where TId : IEquatable<TId>, IComparable<TId>, IComparable

    {

        /// <summary>
        /// The current value of this sensor.
        /// </summary>
        TValue CurrentValue { get; }

        /// <summary>
        /// The current value of this sensor and its measurement timestamp.
        /// </summary>
        Measurement<TValue> TimestampedValue { get; }

    }

    #endregion

}
