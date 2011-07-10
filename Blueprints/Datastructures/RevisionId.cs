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
    /// A RevisionId is an identificator for a specific IElement revision in
    /// a distributed system consisting of a timestamp and a SystemId.
    /// </summary>
    public class RevisionId : IComparable, IComparable<RevisionId>, IEquatable<RevisionId>
    {

        #region Properties

        #region Timestamp

        /// <summary>
        /// The timestamp of this revision.
        /// </summary>
        public UInt64 Timestamp { get; private set; }

        #endregion

        #region SystemId

        /// <summary>
        /// A unique identification of the generating system,
        /// process or thread of this revision.
        /// </summary>
        public SystemId SystemId { get; private set; }

        #endregion

        #endregion

        #region Constructors

        #region RevisionId(SystemId)

        /// <summary>
        /// Generates a RevisionId based on the actual timestamp and the given SystemId.
        /// </summary>
        /// <param name="SystemId">An unique identificator for the generating system, process or thread</param>
        public RevisionId(SystemId SystemId)
        {
            this.Timestamp = (UInt64) UniqueTimestamp.Ticks;
            this.SystemId  = SystemId;
        }

        #endregion

        #region RevisionId(Timestamp, SystemId)

        /// <summary>
        /// Generates a RevisionId based on the given UInt64 timestamp and the given SystemId.
        /// </summary>
        /// <param name="Timestamp">A timestamp</param>
        /// <param name="SystemId">An unique identificator for the generating system, process or thread</param>
        public RevisionId(UInt64 Timestamp, SystemId SystemId)
        {
            this.Timestamp = Timestamp;
            this.SystemId  = SystemId;
        }

        #endregion

        #region RevisionId(DateTime, SystemId)

        /// <summary>
        /// Generates a RevisionId based on the given DateTime object and the given SystemId.
        /// </summary>
        /// <param name="DateTime">A DateTime object</param>
        /// <param name="SystemId">An unique identificator for the generating system, process or thread</param>
        public RevisionId(DateTime DateTime, SystemId SystemId)
        {
            this.Timestamp = (UInt64)DateTime.Ticks;
            this.SystemId  = SystemId;
        }

        #endregion

        #region RevisionId(DateTimeString, SystemId)

        /// <summary>
        /// Generates a RevisionId based on the "yyyyddMM.HHmmss.fffffff" formated
        /// string representation of a DateTime object and the given SystemId.
        /// </summary>
        /// <param name="DateTimeString">A DateTime object as "yyyyddMM.HHmmss.fffffff"-formated string</param>
        /// <param name="SystemId">An unique identificator for the generating system, process or thread</param>
        /// <exception cref="System.FormatException"></exception>
        public RevisionId(String DateTimeString, SystemId SystemId)
        {
            try
            {
                this.Timestamp = (UInt64)(DateTime.ParseExact(DateTimeString, "yyyyddMM.HHmmss.fffffff", null)).Ticks;
                this.SystemId  = SystemId;
            }
            catch
            {
                throw new FormatException("The given string could not be parsed!");
            }
        }

        #endregion

        #region RevisionId(RevisionIdString)

        /// <summary>
        /// Generates a RevisionId based on the "yyyyddMM.HHmmss.fffffff(SystemId)"
        /// formated string representation of a RevisionId.
        /// </summary>
        /// <param name="RevisionIdString">A RevisionId object as "yyyyddMM.HHmmss.fffffff(SystemId)"-formated string</param>
        /// <exception cref="System.FormatException"></exception>
        public RevisionId(String RevisionIdString)
        {

            try
            {            
                var __Timestamp = RevisionIdString.Remove(RevisionIdString.IndexOf("("));
                var __SystemId  = RevisionIdString.Substring(__Timestamp.Length + 1, RevisionIdString.Length - __Timestamp.Length - 2);

                this.Timestamp  = (UInt64)(DateTime.ParseExact(__Timestamp, "yyyyddMM.HHmmss.fffffff", null)).Ticks;
                this.SystemId   = new SystemId(__SystemId);
            }
            catch
            {
                throw new FormatException("The given string could not be parsed!");
            }

        }

        #endregion

        #endregion


        #region Operator overloading

        #region Operator == (RevisionId1, RevisionId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="RevisionId1">A RevisionId.</param>
        /// <param name="RevisionId2">Another RevisionId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (RevisionId RevisionId1, RevisionId RevisionId2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(RevisionId1, RevisionId2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) RevisionId1 == null) || ((Object) RevisionId2 == null))
                return false;

            return RevisionId1.Equals(RevisionId2);

        }

        #endregion

        #region Operator != (RevisionId1, RevisionId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="RevisionId1">A RevisionId.</param>
        /// <param name="RevisionId2">Another RevisionId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (RevisionId RevisionId1, RevisionId RevisionId2)
        {
            return !(RevisionId1 == RevisionId2);
        }

        #endregion

        #region Operator <  (RevisionId1, RevisionId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="RevisionId1">A RevisionId.</param>
        /// <param name="RevisionId2">Another RevisionId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (RevisionId RevisionId1, RevisionId RevisionId2)
        {

            if (RevisionId1.Timestamp < RevisionId2.Timestamp)
                return true;

            if (RevisionId1.Timestamp > RevisionId2.Timestamp)
                return false;

            // RevisionId1.Timestamp == RevisionId2.Timestamp
            if (RevisionId1.SystemId < RevisionId2.SystemId)
                return true;

            return false;

        }

        #endregion

        #region Operator <= (RevisionId1, RevisionId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="RevisionId1">A RevisionId.</param>
        /// <param name="RevisionId2">Another RevisionId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (RevisionId RevisionId1, RevisionId RevisionId2)
        {
            return !(RevisionId1 > RevisionId2);
        }

        #endregion

        #region Operator >  (RevisionId1, RevisionId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="RevisionId1">A RevisionId.</param>
        /// <param name="RevisionId2">Another RevisionId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (RevisionId RevisionId1, RevisionId RevisionId2)
        {

            if (RevisionId1.Timestamp > RevisionId2.Timestamp)
                return true;

            if (RevisionId1.Timestamp < RevisionId2.Timestamp)
                return false;

            // RevisionId1.Timestamp == RevisionId2.Timestamp
            if (RevisionId1.SystemId > RevisionId2.SystemId)
                return true;

            return false;

        }

        #endregion

        #region Operator >= (RevisionId1, RevisionId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="RevisionId1">A RevisionId.</param>
        /// <param name="RevisionId2">Another RevisionId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (RevisionId RevisionId1, RevisionId RevisionId2)
        {
            return !(RevisionId1 < RevisionId2);
        }

        #endregion

        #endregion

        #region IComparable<RevisionId> Member

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given object must not be null!");

            // Check if the given object is a RevisionId.
            var _RevisionId = Object as RevisionId;
            if ((Object) _RevisionId == null)
                throw new ArgumentException("The given object is not a RevisionId!");

            if (this < _RevisionId) return -1;
            if (this > _RevisionId) return +1;

            return 0;

        }

        #endregion

        #region CompareTo(RevisionId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="RevisionId">An object to compare with.</param>
        public Int32 CompareTo(RevisionId RevisionId)
        {

            if ((Object) RevisionId == null)
                throw new ArgumentNullException();

            if (this < RevisionId) return -1;
            if (this > RevisionId) return +1;

            return 0;

        }

        #endregion

        #endregion

        #region IEquatable<RevisionId> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>true|false</returns>
        public override Boolean Equals(Object Object)
        {

            if (Object == null)
                return false;

            // Check if the given object is a RevisionId.
            var _RevisionId = Object as RevisionId;
            if ((Object) _RevisionId == null)
                return false;

            return Equals(_RevisionId);

        }

        #endregion

        #region Equals(RevisionId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="RevisionId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(RevisionId RevisionId)
        {

            if ((Object) RevisionId == null)
                return false;

            // Check if the inner fields have the same values
            if (Timestamp != RevisionId.Timestamp)
                return false;

            if (SystemId != RevisionId.SystemId)
                return false;

            return true;

        }

        #endregion

        #endregion

        #region GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override Int32 GetHashCode()
        {
            return Timestamp.GetHashCode() ^ SystemId.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Returns a formated string representation of this revision
        /// </summary>
        /// <returns>A formated string representation of this revision</returns>
        public override String ToString()
        {
            return String.Format("{0:yyyyddMM.HHmmss.fffffff}({1})", new DateTime((Int64) Timestamp), SystemId.ToString());
        }

        #endregion

    }

}
