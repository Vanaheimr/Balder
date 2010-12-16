/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using System;

#endregion

namespace de.ahzf.blueprints.Datastructures
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

        #region RevisionId(mySystemId)

        /// <summary>
        /// Generates a RevisionId based on the actual timestamp and the given SystemId.
        /// </summary>
        /// <param name="mySystemId">An unique identificator for the generating system, process or thread</param>
        public RevisionId(SystemId mySystemId)
        {
            Timestamp = (UInt64) UniqueTimestamp.Ticks;
            SystemId  = mySystemId;
        }

        #endregion

        #region RevisionId(myTimestamp, mySystemId)

        /// <summary>
        /// Generates a RevisionId based on the given UInt64 timestamp and the given SystemId.
        /// </summary>
        /// <param name="myTimestamp">A timestamp</param>
        /// <param name="mySystemId">An unique identificator for the generating system, process or thread</param>
        public RevisionId(UInt64 myTimestamp, SystemId mySystemId)
        {
            Timestamp = myTimestamp;
            SystemId  = mySystemId;
        }

        #endregion

        #region RevisionId(myDateTime, mySystemId)

        /// <summary>
        /// Generates a RevisionId based on the given DateTime object and the given SystemId.
        /// </summary>
        /// <param name="myDateTime">A DateTime object</param>
        /// <param name="mySystemId">An unique identificator for the generating system, process or thread</param>
        public RevisionId(DateTime myDateTime, SystemId mySystemId)
        {
            Timestamp = (UInt64) myDateTime.Ticks;
            SystemId  = mySystemId;
        }

        #endregion

        #region RevisionId(myDateTimeString, mySystemId)

        /// <summary>
        /// Generates a RevisionId based on the "yyyyddMM.HHmmss.fffffff" formated
        /// string representation of a DateTime object and the given SystemId.
        /// </summary>
        /// <param name="myDateTimeString">A DateTime object as "yyyyddMM.HHmmss.fffffff"-formated string</param>
        /// <param name="mySystemId">An unique identificator for the generating system, process or thread</param>
        /// <exception cref="System.FormatException"></exception>
        public RevisionId(String myDateTimeString, SystemId mySystemId)
        {
            try
            {
                Timestamp = (UInt64) (DateTime.ParseExact(myDateTimeString, "yyyyddMM.HHmmss.fffffff", null)).Ticks;
                SystemId  = mySystemId;
            }
            catch
            {
                throw new FormatException("The given string could not be parsed!");
            }
        }

        #endregion

        #region RevisionId(myRevisionIdString)

        /// <summary>
        /// Generates a RevisionId based on the "yyyyddMM.HHmmss.fffffff(SystemId)"
        /// formated string representation of a RevisionId.
        /// </summary>
        /// <param name="myRevisionIdString">A RevisionId object as "yyyyddMM.HHmmss.fffffff(SystemId)"-formated string</param>
        /// <exception cref="System.FormatException"></exception>
        public RevisionId(String myRevisionIdString)
        {
            try
            {            
                var __Timestamp = myRevisionIdString.Remove(myRevisionIdString.IndexOf("("));
                var __SystemId  = myRevisionIdString.Substring(__Timestamp.Length + 1, myRevisionIdString.Length - __Timestamp.Length - 2);

                Timestamp       = (UInt64) (DateTime.ParseExact(__Timestamp, "yyyyddMM.HHmmss.fffffff", null)).Ticks;
                SystemId        = new SystemId(__SystemId);
            }
            catch
            {
                throw new FormatException("The given string could not be parsed!");
            }
        }

        #endregion

        #endregion


        #region Operator overloading

        #region Operator == (myRevisionId1, myRevisionId2)

        public static Boolean operator == (RevisionId myRevisionId1, RevisionId myRevisionId2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(myRevisionId1, myRevisionId2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) myRevisionId1 == null) || ((Object) myRevisionId2 == null))
                return false;

            return myRevisionId1.Equals(myRevisionId2);

        }

        #endregion

        #region Operator != (myRevisionId1, myRevisionId2)

        public static Boolean operator != (RevisionId myRevisionId1, RevisionId myRevisionId2)
        {
            return !(myRevisionId1 == myRevisionId2);
        }

        #endregion

        #region Operator <  (myRevisionId1, myRevisionId2)

        public static Boolean operator < (RevisionId myRevisionId1, RevisionId myRevisionId2)
        {

            if (myRevisionId1.Timestamp < myRevisionId2.Timestamp)
                return true;

            if (myRevisionId1.Timestamp > myRevisionId2.Timestamp)
                return false;

            // myRevisionId1.Timestamp == myRevisionId2.Timestamp
            if (myRevisionId1.SystemId < myRevisionId2.SystemId)
                return true;

            return false;

        }

        #endregion

        #region Operator >  (myRevisionId1, myRevisionId2)

        public static Boolean operator > (RevisionId myRevisionId1, RevisionId myRevisionId2)
        {

            if (myRevisionId1.Timestamp > myRevisionId2.Timestamp)
                return true;

            if (myRevisionId1.Timestamp < myRevisionId2.Timestamp)
                return false;

            // myRevisionId1.Timestamp == myRevisionId2.Timestamp
            if (myRevisionId1.SystemId > myRevisionId2.SystemId)
                return true;

            return false;

        }

        #endregion

        #region Operator <= (myRevisionId1, myRevisionId2)

        public static Boolean operator <= (RevisionId myRevisionId1, RevisionId myRevisionId2)
        {
            return !(myRevisionId1 > myRevisionId2);
        }

        #endregion

        #region Operator >= (myRevisionId1, myRevisionId2)

        public static Boolean operator >= (RevisionId myRevisionId1, RevisionId myRevisionId2)
        {
            return !(myRevisionId1 < myRevisionId2);
        }

        #endregion

        #endregion


        #region IComparable Member

        public Int32 CompareTo(Object myObject)
        {

            // Check if myObject is null
            if (myObject == null)
                throw new ArgumentNullException();

            // If parameter cannot be cast to Point return false.
            var _RevisionId = myObject as RevisionId;
            if ((Object) _RevisionId == null)
                throw new ArgumentException("myObject is not of type RevisionId!");

            if (this < _RevisionId) return -1;
            if (this > _RevisionId) return +1;

            return 0;

        }

        #endregion

        #region IComparable<RevisionId> Member

        public Int32 CompareTo(RevisionId myRevisionId)
        {

            // Check if myRevisionId is null
            if (myRevisionId == null)
                throw new ArgumentNullException();

            if (this < myRevisionId) return -1;
            if (this > myRevisionId) return +1;

            return 0;

        }

        #endregion

        #region IEquatable<RevisionId> Members

        #region Equals(myObject)

        public override Boolean Equals(Object myObject)
        {

            // Check if myObject is null
            if (myObject == null)
                return false;

            // If parameter cannot be cast to RevisionId return false.
            var _RevisionId = myObject as RevisionId;
            if ((Object) _RevisionId == null)
                return false;

            return Equals(_RevisionId);

        }

        #endregion

        #region Equals(myRevisionId)

        public Boolean Equals(RevisionId myRevisionId)
        {

            // If parameter is null return false:
            if ((Object) myRevisionId == null)
                return false;

            // Check if the inner fields have the same values
            if (Timestamp != myRevisionId.Timestamp)
                return false;

            if (SystemId != myRevisionId.SystemId)
                return false;

            return true;

        }

        #endregion

        #endregion

        #region GetHashCode()

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
