/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <achim@graph-database.org>
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
using System.Collections.Generic;

#endregion

namespace de.ahzf.Vanaheimr.Balder.UnitTests
{
    
    public static class BaseTest
    {

        #region GenerateUUIDs(myNumber)

        public static IEnumerable<String> GenerateUUIDs(UInt32 myNumber)
        {

            var _UUIDs = new List<String>();
            
            for (int i = 0; i < myNumber; i++)
                _UUIDs.Add(Guid.NewGuid().ToString());

            return _UUIDs;

        }

        #endregion

        #region GenerateUUIDs(myPrefix, myNumber)

        public static IEnumerable<String> GenerateUUIDs(String myPrefix, UInt32 myNumber)
        {

            var _UUIDs = new List<String>();

            for (int i = 0; i < myNumber; i++)
                _UUIDs.Add(myPrefix + Guid.NewGuid().ToString());

            return _UUIDs;

        }

        #endregion

        #region AsList<T>(myObject, myTimes)

        public static List<T> AsList<T>(T myObject, UInt64 myTimes)
        {

            var _List = new List<T>();

            for (var i = 0UL; i < myTimes; i++)
                _List.Add(myObject);

            return _List;

        }

        #endregion

    }

}
