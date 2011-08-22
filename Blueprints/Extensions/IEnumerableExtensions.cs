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
using System.Collections.Generic;

#endregion

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// Extensions to the IEnumerable interface.
    /// </summary>
    public static class IEnumerableExtensions
    {

        #region StdDev(IEnumerable)

        /// <summary>
        /// Calculates the standard deviation of the given enumeration of doubles.
        /// </summary>
        /// <param name="IEnumerable">An enumeration of Doubles.</param>
        /// <returns>The stddev of the given enumeration of doubles.</returns>
        public static Double StdDev(this IEnumerable<Double> IEnumerable)
        {
            return IEnumerable.AverageAndStdDev().Item2;
        }

        #endregion

        #region AverageAndStdDev(IEnumerable)

        /// <summary>
        /// Calculates the standard deviation of the given enumeration of doubles.
        /// </summary>
        /// <param name="IEnumerable">An enumeration of Doubles.</param>
        /// <returns>The mean and stddev of the given enumeration of doubles.</returns>
        public static Tuple<Double, Double> AverageAndStdDev(this IEnumerable<Double> IEnumerable)
        {

            #region Initial Checks

            if (IEnumerable == null)
                throw new ArgumentNullException("The given enumeration of doubles must not be null!");

            var Count = IEnumerable.Count();

            if (Count == 0)
                throw new ArgumentNullException("The length of the given enumeration of doubles must not be zero!");

            if (Count == 1)
                return new Tuple<Double, Double>(IEnumerable.First(), 0);

            #endregion

            var average = IEnumerable.Average();
            var sum     = 0.0;

            foreach (var value in IEnumerable)
                sum += (value - average) * (value - average);

            return new Tuple<Double, Double>(average, Math.Sqrt(sum / (Count - 1)));

        }

        #endregion


        #region ForEach<T>(IEnumerable, Action)

        /// <summary>
        /// Call the given delegate for each element of the enumeration.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="IEnumerable">An enumertion of type T.</param>
        /// <param name="Action">An action to call for each element of the enumeration.</param>
        public static void ForEach<T>(this IEnumerable<T> IEnumerable, Action<T> Action)
        {

            #region Initial checks

            if (IEnumerable == null)
                throw new ArgumentNullException("The given IEnumerable must not be null!");

            if (Action == null)
                throw new ArgumentNullException("The given Action must not be null!");

            #endregion

            foreach (var Element in IEnumerable)
                Action(Element);

        }

        #endregion

    }

}
