/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
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

using de.ahzf.Illias.Commons;
using de.ahzf.Blueprints.PropertyGraphs;

#endregion

namespace de.ahzf.Blueprints
{

    /// <summary>
    /// PropertyExtensions
    /// </summary>
    public static class PropertyExtensions
    {

        #region ListAdd<TKey>(this IProperties, Key, FirstValue, params Values)

        public static IProperties<TKey, Object> ListAdd<TKey>(this IProperties<TKey, Object> IProperties, TKey Key, Object FirstValue, params Object[] Values)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException();

            if (Key == null)
                throw new ArgumentNullException();

            if (FirstValue == null)
                throw new ArgumentNullException();

            #endregion

            List<Object> _List = null;
            Object _Value = null;

            if (IProperties.TryGetProperty(Key, out _Value))
            {
                
                _List = _Value as List<Object>;

                if (_List == null)
                    throw new Exception("The value is not a list!");

                else
                {

                    _List.Add(FirstValue);

                    if (Values != null && Values.Any())
                        _List.AddRange(Values);

                    return IProperties;

                }

            }

            _List = new List<Object>() { FirstValue };

            if (Values != null && Values.Any())
                _List.AddRange(Values);

            IProperties.SetProperty(Key, _List);

            return IProperties;

        }

        #endregion

        #region ListGet<TKey>(this IProperties, Key)

        public static List<Object> ListGet<TKey>(this IProperties<TKey, Object> IProperties, TKey Key)
            
            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException();

            if (Key == null)
                throw new ArgumentNullException();

            #endregion

            Object _Value = null;

            if (IProperties.TryGetProperty(Key, out _Value))
                return _Value as List<Object>;

            return null;

        }

        #endregion

        #region ListTryGet<TKey>(this IProperties, Key, out List)

        public static Boolean ListTryGet<TKey>(this IProperties<TKey, Object> IProperties, TKey Key, out List<Object> List)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException();

            if (Key == null)
                throw new ArgumentNullException();

            #endregion

            Object _Value = null;

            if (IProperties.TryGetProperty(Key, out _Value))
            {
                
                List = _Value as List<Object>;
                
                if (List != null)
                    return true;

            }

            List = null;
            return false;

        }

        #endregion


        #region SetAdd<TKey>(this IProperties, Key, FirstValue, params Values)

        public static IProperties<TKey, Object> SetAdd<TKey>(this IProperties<TKey, Object> IProperties, TKey Key, Object FirstValue, params Object[] Values)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException();

            if (Key == null)
                throw new ArgumentNullException();

            if (FirstValue == null)
                throw new ArgumentNullException();

            #endregion

            HashSet<Object> _Set = null;
            Object _Value = null;

            if (IProperties.TryGetProperty(Key, out _Value))
            {

                _Set = _Value as HashSet<Object>;

                if (_Set == null)
                    throw new Exception("The value is not a list!");

                else
                {

                    _Set.Add(FirstValue);

                    if (Values != null && Values.Any())
                        Values.ForEach(value => _Set.Add(value));

                    return IProperties;

                }

            }

            _Set = new HashSet<Object>() { FirstValue };

            if (Values != null && Values.Any())
                Values.ForEach(value => _Set.Add(value));

            IProperties.SetProperty(Key, _Set);

            return IProperties;

        }

        #endregion

        #region SetGet<TKey>(this IProperties, Key)

        public static HashSet<Object> SetGet<TKey>(this IProperties<TKey, Object> IProperties, TKey Key)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException();

            if (Key == null)
                throw new ArgumentNullException();

            #endregion

            Object _Value = null;

            if (IProperties.TryGetProperty(Key, out _Value))
                return _Value as HashSet<Object>;

            return null;

        }

        #endregion

        #region SetTryGet<TKey>(this IProperties, Key, out Set)

        public static Boolean SetTryGet<TKey>(this IProperties<TKey, Object> IProperties, TKey Key, out HashSet<Object> Set)

            where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable

        {

            #region Initial checks

            if (IProperties == null)
                throw new ArgumentNullException();

            if (Key == null)
                throw new ArgumentNullException();

            #endregion

            Object _Value = null;

            if (IProperties.TryGetProperty(Key, out _Value))
            {

                Set = _Value as HashSet<Object>;

                if (Set != null)
                    return true;

            }

            Set = null;
            return false;

        }

        #endregion

    }

}
