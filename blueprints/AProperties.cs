/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET
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
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;

using de.ahzf.blueprints.Datastructures;

#endregion

namespace de.ahzf.blueprints
{

    /// <summary>
    /// An element is the base class for both vertices and edges.
    /// An element has an identifier that must be unique to its inheriting classes (vertex or edges).
    /// An element can maintain a collection of key/value properties.
    /// Keys are always strings and values can be any object.
    /// Particular implementations can reduce the space of objects that can be used as values.
    /// </summary>
    public abstract class AProperties<TId, TKey> : IProperties<TKey>
        where TId : IEquatable<TId>, IComparable<TId>, IComparable
        where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
    {


        #region Data

        /// <summary>
        /// The datastructure holding all graph properties.
        /// </summary>
        protected readonly IDictionary<TKey, Object> _Properties;

        /// <summary>
        /// The key of the Id property
        /// </summary>
        public readonly TKey __Id;

        /// <summary>
        /// The key of the RevisionId property
        /// </summary>
        public readonly TKey __RevisionId;

        #endregion

        #region Properties

        #region Graph

        /// <summary>
        /// The associated graph
        /// </summary>
        protected IGraph Graph { get; private set; }

        #endregion

        #region Id

        /// <summary>
        /// An identifier that is unique to its inheriting class.
        /// All vertices of a graph must have unique identifiers.
        /// All edges of a graph must have unique identifiers.
        /// </summary>
        public TId Id
        {
            get
            {

                Object _Object = null;

                if (_Properties.TryGetValue(__Id, out _Object))
                    return (TId) _Object;

                return default(TId);

            }
        }

        #endregion

        #region RevisionId

        /// <summary>
        /// The RevisionId extends the Id to identify multiple revisions of
        /// an element during the lifetime of a graph. A RevisionId should
        /// additionally be unique among all elements of a graph.
        /// </summary>
        public RevisionId RevisionId
        {
            get
            {

                Object _Object = null;

                if (_Properties.TryGetValue(__RevisionId, out _Object))
                    return _Object as RevisionId;

                return null;

            }
        }

        #endregion

        #endregion

        #region Protected Constructor(s)

        #region AElement(myIGraph, myElementId, myId, myRevisionId, myIDictionaryInitializer)

        /// <summary>
        /// Creates a new AElement object
        /// </summary>
        /// <param name="myIGraph">The associated graph</param>
        /// <param name="myElementId">The Id of the new AElement</param>
        protected AProperties(IGraph myIGraph, ElementId myElementId, TKey myId, TKey myRevisionId, Func<IDictionary<TKey, Object>> myIDictionaryInitializer)
        {

            if (myIGraph == null)
                throw new ArgumentNullException("The graph reference must not be null!");

            if (myElementId == null)
                throw new ArgumentNullException("The ElementId must not be null!");

            if (myId == null)
                throw new ArgumentNullException("The myId must not be null!");

            if (myRevisionId == null)
                throw new ArgumentNullException("The myRevisionId must not be null!");

            Graph = myIGraph;

            __Id         = myId;
            __RevisionId = myRevisionId;

            // StringComparer.OrdinalIgnoreCase is often used to compare file names,
            // path names, network paths, and any other string whose value does not change
            // based on the locale of the user's computer.
            _Properties = myIDictionaryInitializer();
            _Properties.Add(__Id, myElementId);

        }

        #endregion

        #endregion


        #region Graph Properties

        #region SetProperty(myPropertyKey, myPropertyValue)

        /// <summary>
        /// Assign a key/value property to the element.
        /// If a value already exists for this key, then the previous key/value is overwritten.
        /// </summary>
        /// <param name="myPropertyKey">The property key.</param>
        /// <param name="myPropertyValue">The property value.</param>
        public virtual IProperties<TKey> SetProperty(TKey myPropertyKey, Object myPropertyValue)
        {

            if (myPropertyKey.Equals(__Id))
                throw new ArgumentException("Changing the Id property is not allowed!");

            if (myPropertyKey.Equals(__RevisionId))
                throw new ArgumentException("Changing the RevisionId property is not allowed!");

            if (_Properties.ContainsKey(myPropertyKey))
                _Properties[myPropertyKey] = myPropertyValue;

            else
                _Properties.Add(myPropertyKey, myPropertyValue);

            return this;

        }

        #endregion

        #region GetProperty(myPropertyKey)

        /// <summary>
        /// Return the property value associated with the given property key.
        /// </summary>
        /// <param name="myPropertyKey">The key of the key/value property.</param>
        /// <returns>The property value related to the string key.</returns>
        public virtual Object GetProperty(TKey myPropertyKey)
        {

            Object _Object = null;

            _Properties.TryGetValue(myPropertyKey, out _Object);

            return _Object;

        }

        #endregion

        #region GetProperties(myPropertyFilter = null)

        /// <summary>
        /// Allows to return a filtered enumeration of all properties.
        /// </summary>
        /// <param name="myPropertyFilter">A function to filter a property based on its key and value.</param>
        /// <returns>A enumeration of all key/value pairs matching the given property filter.</returns>
        public virtual IEnumerable<KeyValuePair<TKey, Object>> GetProperties(Func<TKey, Object, Boolean> myPropertyFilter = null)
        {

            foreach (var _KeyValuePair in _Properties)
            {

                if (_KeyValuePair.Value != null)
                {

                    if (myPropertyFilter == null)
                        yield return _KeyValuePair;

                    else if (myPropertyFilter(_KeyValuePair.Key, _KeyValuePair.Value))
                        yield return _KeyValuePair;

                }

            }

        }

        #endregion

        #region RemoveProperty(myPropertyKey)

        /// <summary>
        /// Removes the property identified by the given property key.
        /// </summary>
        /// <param name="myPropertyKey">The key of the property to remove.</param>
        /// <returns>The property value associated with that key prior to removal.</returns>
        public virtual Object RemoveProperty(TKey myPropertyKey)
        {

            if (myPropertyKey.Equals(__Id))
                throw new ArgumentException("Removing the Id property is not allowed!");

            if (myPropertyKey.Equals(__RevisionId))
                throw new ArgumentException("Removing the RevisionId property is not allowed!");

            Object _Object = null;

            if (_Properties.TryGetValue(myPropertyKey, out _Object))
                _Properties.Remove(myPropertyKey);

            return _Object;

        }

        #endregion

        #region PropertyKeys

        /// <summary>
        /// Return all property keys.
        /// </summary>
        public IEnumerable<TKey> PropertyKeys
        {
            get
            {
                return _Properties.Keys;
            }
        }

        #endregion

        #endregion




        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Properties.GetEnumerator();
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey, Object>> Members

        /// <summary>
        /// Returns an enumeration of all properties within this element.
        /// </summary>
        /// <returns>An enumeration of all properties within this element.</returns>
        public IEnumerator<KeyValuePair<TKey, Object>> GetEnumerator()
        {
            return _Properties.GetEnumerator();
        }

        #endregion


        #region Operator overloading

        #region Operator == (myAElement1, myIElement2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myAElement1">A AElement.</param>
        /// <param name="myIElement2">A IElement.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (AProperties<TId, TKey> myAElement1, AProperties<TId, TKey> myIElement2)
        {

            // If both are null, or both are same instance, return true.
            if (Object.ReferenceEquals(myAElement1, myIElement2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) myAElement1 == null) || ((Object) myIElement2 == null))
                return false;

            return myAElement1.Equals(myIElement2);

        }

        #endregion

        #region Operator != (myAElement1, myIElement2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myAElement1">A AElement.</param>
        /// <param name="myIElement2">A IElement.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (AProperties<TId, TKey> myAElement1, AProperties<TId, TKey> myIElement2)
        {
            return !(myAElement1 == myIElement2);
        }

        #endregion

        #endregion

        #region IComparable<IElement> Members

        #region CompareTo(myObject)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myObject">An object to compare with.</param>
        /// <returns>true|false</returns>
        public abstract Int32 CompareTo(Object myObject);

        #endregion

        #region CompareTo(myObject)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myObject">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(TId myObject)
        {
            return 0;
            //return Id..CompareTo(myObject);
        }

        #endregion

        #region CompareTo(myIProperties)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myIProperties">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(IProperties<TKey> myIProperties)
        {

            // Check if myIElement is null
            if (myIProperties == null)
                throw new ArgumentNullException("myIElement must not be null!");

            return Id.CompareTo(myIProperties.GetProperty(__Id));

        }

        #endregion

        #endregion

        #region IEquatable<IElement> Members

        #region Equals(myObject)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myObject">An object to compare with.</param>
        /// <returns>true|false</returns>
        public override Boolean Equals(Object myObject)
        {

            if (myObject == null)
                return false;

            var _Object = myObject as IProperties<TKey>;
            if (_Object == null)
                return Equals(_Object);

            return false;

        }

        #endregion

        #region Equals(myObject)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="myObject">An object to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(TId myObject)
        {
            return Id.Equals(myObject);
        }

        #endregion


        #region Equals(myIProperties)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="myIProperties">An object to compare with.</param>
        ///// <returns>true|false</returns>
        //public Boolean Equals(IProperties<TKey> myIProperties)
        //{

        //    if ((Object) myIProperties == null)
        //        return false;

        //    //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
        //    return this.Count() == myIProperties.Count();

        //}

        #endregion

        #endregion

        #region GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        /// <returns>The HashCode of this object.</returns>
        public override Int32 GetHashCode()
        {

            if (Id == null)
                return 0;

            return Id.GetHashCode();

        }

        #endregion

        #region ToString()

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        public override String ToString()
        {

            var IdString = (Id == null) ? "<null>" : Id.ToString();

            return this.GetType().Name + "(Id = " + IdString + ")";

        }

        #endregion


        
        //bool IEquatable<IProperties<TKey>>.Equals(IProperties<TKey> other)
        //{
        //    throw new NotImplementedException();
        //}

        //int IComparable<IProperties<TKey>>.CompareTo(IProperties<TKey> other)
        //{
        //    throw new NotImplementedException();
        //}

        //int IComparable.CompareTo(object obj)
        //{
        //    throw new NotImplementedException();
        //}

    }

}
