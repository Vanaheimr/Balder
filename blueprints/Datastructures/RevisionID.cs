/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using System;
using System.Runtime.Serialization;

#endregion

namespace de.ahzf.blueprints.datastructures
{

    /// <summary>
    /// A RevisionID is an identificator for an object revision.
    /// </summary>    

    public class RevisionID : IComparable, IComparable<RevisionID>, IEquatable<RevisionID>
    {

        #region Properties

        #region Timestamp

        /// <summary>
        /// The timestamp of this revision.
        /// </summary>
        public UInt64 Timestamp { get; private set; }

        #endregion

        #region UUID

        /// <summary>
        /// A unique identification of the generation process of this revision.
        /// </summary>
        public VertexId UUID { get; private set; }

        #endregion

        #endregion

        #region Constructors

        #region RevisionID(myUUID)

        /// <summary>
        /// A constructor used for generating an RevisionID based on the actual
        /// DateTime and the given (system) UUID.
        /// </summary>
        /// <param name="myUUID">An unique identification for this generation process</param>
        public RevisionID(VertexId myUUID)
        {
            Timestamp = (UInt64)TimestampNonce.Ticks;
            UUID = myUUID;
        }

        #endregion

        #region RevisionID(myTimestamp, myUUID)

        /// <summary>
        /// A constructor used for generating an RevisionID based on the given
        /// timestamp and the given (system) UUID.
        /// </summary>
        /// <param name="myTimestamp">Any timestamp</param>
        /// <param name="myUUID">An unique identification for this generation process</param>
        public RevisionID(UInt64 myTimestamp, VertexId myUUID)
        {
            Timestamp = myTimestamp;
            UUID = myUUID;
        }

        #endregion

        #region RevisionID(myDateTime, myUUID)

        /// <summary>
        /// A constructor used for generating an RevisionID based on the given
        /// DateTime and the given (system) UUID.
        /// </summary>
        /// <param name="myDateTime">Any DateTime</param>
        /// <param name="myUUID">An unique identification for this generation process</param>
        public RevisionID(DateTime myDateTime, VertexId myUUID)
        {
            Timestamp = (UInt64)myDateTime.Ticks;
            UUID = myUUID;
        }

        #endregion

        #region RevisionID(myDateTime_String, myUUID)

        /// <summary>
        /// A constructor used for generating an RevisionID based on the given
        /// "yyyyddMM.HHmmss.fffffff"-formated DateTime and the given (system) UUID.
        /// </summary>
        /// <param name="myDateTime">A "yyyyddMM.HHmmss.fffffff"-formated DateTime string</param>
        /// <param name="myUUID">An unique identification for this generation process</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.FormatException"></exception>
        public RevisionID(String myDateTime_String, VertexId myUUID)
        {
            Timestamp = (UInt64)(DateTime.ParseExact(myDateTime_String, "yyyyddMM.HHmmss.fffffff", null)).Ticks;
            UUID = myUUID;
        }

        #endregion

        #region RevisionID(myRevisionString)

        /// <summary>
        /// A constructor used for generating an RevisionID based on the given
        /// "yyyyddMM.HHmmss.fffffff(UUID)"-formated DateTime with attached UUID.
        /// </summary>
        /// <param name="myRevisionIDString">A "yyyyddMM.HHmmss.fffffff(UUID)"-formated DateTime string with attached UUID</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.FormatException"></exception>
        public RevisionID(String myRevisionIDString)
        {
            var RevisionID_Timestamp = myRevisionIDString.Remove(myRevisionIDString.IndexOf("("));
            var RevisionID_UUID = myRevisionIDString.Substring(RevisionID_Timestamp.Length + 1, myRevisionIDString.Length - RevisionID_Timestamp.Length - 2);
            Timestamp = (UInt64)(DateTime.ParseExact(RevisionID_Timestamp, "yyyyddMM.HHmmss.fffffff", null)).Ticks;
            UUID = new VertexId(RevisionID_UUID);
        }

        #endregion

        #endregion


        #region Object-specific methods

        #region IncrementRevisionTimestamp()

        /// <summary>
        /// Increments the value of the RevisionTimestamp by one.
        /// This can be used to make the RevisionID unique in case of
        /// an imprecise resolution of your GetDateTime method.
        /// </summary>
        public void IncrementRevisionTimestamp()
        {
            Timestamp++;
        }

        #endregion

        #region Clone()

        public RevisionID Clone()
        {
            return new RevisionID(Timestamp, new VertexId(UUID));
        }

        #endregion

        #endregion


        #region Operator overloading

        #region Operator == (myRevisionID1, myRevisionID2)

        public static Boolean operator ==(RevisionID myRevisionID1, RevisionID myRevisionID2)
        {

            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(myRevisionID1, myRevisionID2))
                return true;

            // If one is null, but not both, return false.
            if (((Object)myRevisionID1 == null) || ((Object)myRevisionID2 == null))
                return false;

            return myRevisionID1.Equals(myRevisionID2);

        }

        #endregion

        #region Operator != (myRevisionID1, myRevisionID2)

        public static Boolean operator !=(RevisionID myRevisionID1, RevisionID myRevisionID2)
        {
            return !(myRevisionID1 == myRevisionID2);
        }

        #endregion

        #region Operator < (myRevisionID1, myRevisionID2)

        public static Boolean operator <(RevisionID myRevisionID1, RevisionID myRevisionID2)
        {

            if (myRevisionID1.Timestamp < myRevisionID2.Timestamp)
                return true;

            if (myRevisionID1.Timestamp > myRevisionID2.Timestamp)
                return false;

            // myRevisionID1.Timestamp == myRevisionID2.Timestamp
            if (myRevisionID1.UUID < myRevisionID2.UUID)
                return true;

            return false;

        }

        #endregion

        #region Operator > (myRevisionID1, myRevisionID2)

        public static Boolean operator >(RevisionID myRevisionID1, RevisionID myRevisionID2)
        {

            if (myRevisionID1.Timestamp > myRevisionID2.Timestamp)
                return true;

            if (myRevisionID1.Timestamp < myRevisionID2.Timestamp)
                return false;

            // myRevisionID1.Timestamp == myRevisionID2.Timestamp
            if (myRevisionID1.UUID > myRevisionID2.UUID)
                return true;

            return false;

        }

        #endregion

        #region Operator <= (myRevisionID1, myRevisionID2)

        public static Boolean operator <=(RevisionID myRevisionID1, RevisionID myRevisionID2)
        {
            return !(myRevisionID1 > myRevisionID2);
        }

        #endregion

        #region Operator >= (myRevisionID1, myRevisionID2)

        public static Boolean operator >=(RevisionID myRevisionID1, RevisionID myRevisionID2)
        {
            return !(myRevisionID1 < myRevisionID2);
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
            var _RevisionID = myObject as RevisionID;
            if ((Object)_RevisionID == null)
                throw new ArgumentException("myObject is not of type RevisionID!");

            if (this < _RevisionID) return -1;
            if (this > _RevisionID) return +1;

            return 0;

        }

        #endregion

        #region IComparable<RevisionID> Member

        public Int32 CompareTo(RevisionID myRevisionID)
        {

            // Check if myRevisionID is null
            if (myRevisionID == null)
                throw new ArgumentNullException();

            if (this < myRevisionID) return -1;
            if (this > myRevisionID) return +1;

            return 0;

        }

        #endregion

        #region IEquatable Members

        public override Boolean Equals(Object myObject)
        {

            // Check if myObject is null
            if (myObject == null)
                return false;

            // If parameter cannot be cast to RevisionID return false.
            var _RevisionID = myObject as RevisionID;
            if ((Object)_RevisionID == null)
                return false;

            return Equals(_RevisionID);

        }

        #endregion

        #region IEquatable<RevisionID> Members

        public Boolean Equals(RevisionID myRevisionID)
        {

            // If parameter is null return false:
            if ((Object)myRevisionID == null)
                return false;

            // Check if the inner fields have the same values
            if (Timestamp != myRevisionID.Timestamp)
                return false;

            if (UUID != myRevisionID.UUID)
                return false;

            return true;

        }

        #endregion

        #region GetHashCode()

        public override Int32 GetHashCode()
        {
            return Timestamp.GetHashCode() ^ UUID.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Returns a formated string representation of this revision
        /// </summary>
        /// <returns>A formated string representation of this revision</returns>
        public override String ToString()
        {
            return String.Format("{0:yyyyddMM.HHmmss.fffffff}({1})", new DateTime((Int64)Timestamp), UUID.ToString());
        }

        #endregion

    }

}
