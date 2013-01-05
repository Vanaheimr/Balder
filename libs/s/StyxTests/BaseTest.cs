/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <achim@graph-database.org>
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

#endregion

namespace de.ahzf.Vanaheimr.Styx.UnitTests
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

        #region PrintCollection<T>(myCollection)

        public static void PrintCollection<T>(ICollection<T> myCollection)
        {
            foreach (var _Object in myCollection)
                Console.WriteLine(_Object);
        }

        #endregion

        #region PrintIEnumerator<T>(myIEnumerator)

        public static void PrintIEnumerator<T>(IEnumerator<T> myIEnumerator)
        {
            while (myIEnumerator.MoveNext())
                Console.WriteLine(myIEnumerator.Current);
        }

        #endregion

        #region Count<T>(myIEnumerator)

        public static UInt64 Count<T>(IEnumerator<T> myIEnumerator)
        {

            UInt64 _Counter = 0;

            while (myIEnumerator.MoveNext())
                _Counter++;

            return _Counter;

        }

        #endregion

        #region Count<T>(myIEnumerable)

        public static UInt64 Count<T>(IEnumerable<T> myIEnumerable)
        {
            return Count(myIEnumerable.GetEnumerator());
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

        #region PrintPerformance(String myName, Nullable<Int32> myEvents, String myEventName, Double myTimeInMilliseconds)

        public static void PrintPerformance(String myName, Nullable<Int32> myEvents, String myEventName, Double myTimeInMilliseconds)
        {
            
            if (myEvents != null)
                Console.WriteLine("\t" + myName + ": " + myEvents + " " + myEventName + " in " + myTimeInMilliseconds + "ms");

            else
                Console.WriteLine("\t" + myName + ": " + myEventName + " in " + myTimeInMilliseconds + "ms");

        }

        #endregion

    }

}
