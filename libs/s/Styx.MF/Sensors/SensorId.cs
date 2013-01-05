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

#endregion

namespace de.ahzf.Styx.Sensors
{

    /// <summary>
    /// A SensorId is the unique identificator of any sensor.
    /// </summary>    
    public class SensorId : IEquatable<SensorId>, IComparable<SensorId>, IComparable
    {

        #region Data

        /// <summary>
        /// The internal identification.
        /// </summary>
        protected readonly String _Id;

        #endregion

        #region Properties

        #region Length

        /// <summary>
        /// Returns the length of the identificator.
        /// </summary>
        public UInt64 Length
        {
            get
            {
                return (UInt64) _Id.Length;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region SensorId()

        /// <summary>
        /// Generates a new SensorId based on a GUID.
        /// </summary>
        public SensorId()
        {
            _Id = Guid.NewGuid().ToString();
        }

        #endregion

        #region SensorId(Int32)

        /// <summary>
        /// Generates a SensorId based on the content of an Int32.
        /// </summary>
        public SensorId(Int32 Int32)
            : this(Math.Abs(Int32).ToString())
        { }

        #endregion

        #region SensorId(UInt32)

        /// <summary>
        /// Generates a SensorId based on the content of an UInt32.
        /// </summary>
        public SensorId(UInt32 UInt32)
            : this(UInt32.ToString())
        { }

        #endregion

        #region SensorId(Int64)

        /// <summary>
        /// Generates a SensorId based on the content of an Int64.
        /// </summary>
        public SensorId(Int64 Int64)
            : this(Int64.ToString())
        { }

        #endregion

        #region SensorId(UInt64)

        /// <summary>
        /// Generates a SensorId based on the content of an UInt64.
        /// </summary>
        public SensorId(UInt64 UInt64)
            : this(UInt64.ToString())
        { }

        #endregion

        #region SensorId(String)

        /// <summary>
        /// Generates a SensorId based on the content of String.
        /// </summary>
        public SensorId(String String)
        {
            _Id = String.Trim();
        }

        #endregion

        #region SensorId(Uri)

        /// <summary>
        /// Generates a SensorId based on the content of Uri.
        /// </summary>
        public SensorId(Uri Uri)
        {
            _Id = Uri.ToString();
        }

        #endregion

        #region SensorId(SensorId)

        /// <summary>
        /// Generates a SensorId based on the content of SensorId.
        /// </summary>
        /// <param name="SensorId">A SensorId</param>
        public SensorId(SensorId SensorId)
        {
            _Id = SensorId.ToString();
        }

        #endregion

        #endregion


        #region NewSensorId

        /// <summary>
        /// Generate a new SensorId.
        /// </summary>
        public static SensorId NewSensorId
        {
            get
            {
                return new SensorId(Guid.NewGuid().ToString());
            }
        }

        #endregion


        #region Operator overloading

        #region Operator == (SensorId1, SensorId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SensorId1">A SensorId.</param>
        /// <param name="SensorId2">Another SensorId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (SensorId SensorId1, SensorId SensorId2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(SensorId1, SensorId2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) SensorId1 == null) || ((Object) SensorId2 == null))
                return false;

            return SensorId1.Equals(SensorId2);

        }

        #endregion

        #region Operator != (SensorId1, SensorId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SensorId1">A SensorId.</param>
        /// <param name="SensorId2">Another SensorId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (SensorId SensorId1, SensorId SensorId2)
        {
            return !(SensorId1 == SensorId2);
        }

        #endregion

        #region Operator <  (SensorId1, SensorId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SensorId1">A SensorId.</param>
        /// <param name="SensorId2">Another SensorId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (SensorId SensorId1, SensorId SensorId2)
        {

            if ((Object) SensorId1 == null)
                throw new ArgumentNullException("The given SensorId1 must not be null!");

            return SensorId1.CompareTo(SensorId2) < 0;

        }

        #endregion

        #region Operator >  (SensorId1, SensorId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SensorId1">A SensorId.</param>
        /// <param name="SensorId2">Another SensorId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (SensorId SensorId1, SensorId SensorId2)
        {

            if ((Object) SensorId1 == null)
                throw new ArgumentNullException("The given SensorId1 must not be null!");

            return SensorId1.CompareTo(SensorId2) > 0;

        }

        #endregion

        #region Operator <= (SensorId1, SensorId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SensorId1">A SensorId.</param>
        /// <param name="SensorId2">Another SensorId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (SensorId SensorId1, SensorId SensorId2)
        {
            return !(SensorId1 > SensorId2);
        }

        #endregion

        #region Operator >= (SensorId1, SensorId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SensorId1">A SensorId.</param>
        /// <param name="SensorId2">Another SensorId.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (SensorId SensorId1, SensorId SensorId2)
        {
            return !(SensorId1 < SensorId2);
        }

        #endregion

        #endregion

        #region IComparable<SensorId> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given object must not be null!");

            // Check if the given object is an SensorId.
            var SensorId = Object as SensorId;
            if ((Object) SensorId == null)
                throw new ArgumentException("The given object is not a SensorId!");

            return CompareTo(SensorId);

        }

        #endregion

        #region CompareTo(SensorId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SensorId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(SensorId SensorId)
        {

            if ((Object) SensorId == null)
                throw new ArgumentNullException("The given SensorId must not be null!");

            // Compare the length of the SensorIds
            var _Result = this.Length.CompareTo(SensorId.Length);

            // If equal: Compare Ids
            if (_Result == 0)
                _Result = _Id.CompareTo(SensorId._Id);

            return _Result;

        }

        #endregion

        #endregion

        #region IEquatable<SensorId> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>true|false</returns>
        public override Boolean Equals(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given object must not be null!");

            // Check if the given object is an SensorId.
            var SensorId = Object as SensorId;
            if ((Object) SensorId == null)
                throw new ArgumentException("The given object is not a SensorId!");

            return this.Equals(SensorId);

        }

        #endregion

        #region Equals(SensorId)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="SensorId">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(SensorId SensorId)
        {

            if ((Object) SensorId == null)
                return false;

            return _Id.Equals(SensorId._Id);

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
            return _Id.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Return a string represtentation of this object.
        /// </summary>
        public override String ToString()
        {
            return _Id.ToString();
        }

        #endregion

    }

}
