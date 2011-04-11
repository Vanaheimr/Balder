/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
 * This file is part of Blueprints.NET <http://www.github.com/ahzf/blueprints.NET>
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
using System.Dynamic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using de.ahzf.blueprints.Datastructures;

#endregion

namespace de.ahzf.blueprints
{

    /// <summary>
    /// This generic class maintains a collection of key/value properties
    /// within the given datastructure.
    /// </summary>
    /// <typeparam name="TId">The type of the ids.</typeparam>
    /// <typeparam name="TRevisionId">The type of the revision identifiers.</typeparam>
    /// <typeparam name="TKey">The type of the property keys.</typeparam>
    /// <typeparam name="TValue">The type of the property values.</typeparam>
    /// <typeparam name="TDatastructure">The type of the datastructure to maintain the key/value pairs.</typeparam>
    public class Properties<TId, TRevisionId, TKey, TValue, TDatastructure>
                    : IProperties<TKey, TValue>,
                      IProperties<TKey, TValue, TDatastructure>

        where TId            : IEquatable<TId>,  IComparable<TId>,  IComparable, TValue
        where TKey           : IEquatable<TKey>, IComparable<TKey>, IComparable
        where TDatastructure : IDictionary<TKey , TValue>

    {


        #region Data

        /// <summary>
        /// The datastructure holding all graph properties.
        /// </summary>
        protected readonly TDatastructure _Properties;

        /// <summary>
        /// The key of the Id property
        /// </summary>
        public readonly TKey _IdKey;

        /// <summary>
        /// The key of the RevisionId property
        /// </summary>
        public readonly TKey _RevisionIdKey;

        #endregion

        #region Properties

        //#region Graph

        ///// <summary>
        ///// The associated graph
        ///// </summary>
        //protected IGenericGraph<

        //                                // Vertex definition
        //                                IPropertyVertex<String, Object, IDictionary<String, Object>,
        //                                  VertexId, IProperties<String, Object, IDictionary<String, Object>>,
        //                                  EdgeId, IProperties<String, Object, IDictionary<String, Object>>,
        //                                  HyperEdgeId, IProperties<String, Object, IDictionary<String, Object>>,
        //                                  RevisionId>,
        //                                VertexId,
        //                                IProperties<String, Object, IDictionary<String, Object>>,

        //                                // Edge definition
        //                                IPropertyEdge<String, Object, IDictionary<String, Object>,
        //                                  VertexId, IProperties<String, Object, IDictionary<String, Object>>,
        //                                  EdgeId, IProperties<String, Object, IDictionary<String, Object>>,
        //                                  HyperEdgeId, IProperties<String, Object, IDictionary<String, Object>>,
        //                                  RevisionId>,
        //                                VertexId,
        //                                IProperties<String, Object, IDictionary<String, Object>>,

        //                                // Hyperedge definition
        //                                IPropertyHyperEdge<String, Object, IDictionary<String, Object>,
        //                                  VertexId, IProperties<String, Object, IDictionary<String, Object>>,
        //                                  EdgeId, IProperties<String, Object, IDictionary<String, Object>>,
        //                                  HyperEdgeId, IProperties<String, Object, IDictionary<String, Object>>,
        //                                  RevisionId>,
        //                                VertexId,
        //                                IProperties<String, Object, IDictionary<String, Object>>,

        //                                // RevisionId definition
        //                                RevisionId, Object> Graph { get; private set; }

        //#endregion

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

                TValue _TValue;

                if (_Properties.TryGetValue(_IdKey, out _TValue))
                    return (TId) _TValue;

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
        public TRevisionId RevisionId
        {
            get
            {

                TValue _TValue;

                if (_Properties.TryGetValue(_RevisionIdKey, out _TValue))
                    return (TRevisionId) (Object) _TValue;

                return default(TRevisionId);

            }
        }

        #endregion

        #endregion

        #region Events

        #region CollectionChanged/OnCollectionChanged(...)

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void OnCollectionChanged(NotifyCollectionChangedEventArgs myNotifyCollectionChangedEventArgs)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, myNotifyCollectionChangedEventArgs);
        }

        #endregion

        #region PropertyChanging/OnPropertyChanging(...)

        public event PropertyChangingEventHandler PropertyChanging;

        public void OnPropertyChanging(String myPropertyName)
        {
            if (PropertyChanging != null)
                PropertyChanging(this, new PropertyChangingEventArgs(myPropertyName));
        }

        public void OnPropertyChanging<TResult>(Expression<Func<TResult>> myPropertyExpression)
        {
            OnPropertyChanged(((MemberExpression) myPropertyExpression.Body).Member.Name);
        }

        #endregion

        #region PropertyChanged/OnPropertyChanged(...)

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(String myPropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(myPropertyName));
        }

        public void OnPropertyChanged<TResult>(Expression<Func<TResult>> myPropertyExpression)
        {
            OnPropertyChanged(((MemberExpression)myPropertyExpression.Body).Member.Name);
        }

        #endregion

        #endregion

        #region Protected Constructor(s)

        #region (protected) AProperties(myIGraph, myId, myIdKey, myRevisionIdKey, myTDatastructureInitializer)

        /// <summary>
        /// Creates a new AElement object
        /// </summary>
        /// <param name="myIGraph">The associated graph.</param>
        /// <param name="myId">The Id of the new property.</param>
        public Properties(TId myId, TKey myIdKey, TKey myRevisionIdKey, Func<TDatastructure> myTDatastructureInitializer)
        {

            //if (myIGraph == null)
            //    throw new ArgumentNullException("The graph reference must not be null!");

            if (myId == null)
                throw new ArgumentNullException("The Id must not be null!");

            if (myIdKey == null)
                throw new ArgumentNullException("The IdKey must not be null!");

            if (myRevisionIdKey == null)
                throw new ArgumentNullException("The RevisionIdKey must not be null!");

           // Graph = myIGraph;

            _IdKey         = myIdKey;
            _RevisionIdKey = myRevisionIdKey;
            _Properties    = myTDatastructureInitializer();
            _Properties.Add(_IdKey, myId);

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
        public virtual IProperties<TKey, TValue> SetProperty(TKey myPropertyKey, TValue myPropertyValue)
        {

            if (myPropertyKey.Equals(_IdKey))
                throw new ArgumentException("Changing the Id property is not allowed!");

            if (myPropertyKey.Equals(_RevisionIdKey))
                throw new ArgumentException("Changing the RevisionId property is not allowed!");

            if (_Properties.ContainsKey(myPropertyKey))
            {
                OnPropertyChanging(myPropertyKey.ToString());
                _Properties[myPropertyKey] = myPropertyValue;
                OnPropertyChanged(myPropertyKey.ToString());
            }

            else
            {
                
                _Properties.Add(myPropertyKey, myPropertyValue);
                
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                                        NotifyCollectionChangedAction.Add,
                                        new Object[] { myPropertyKey, myPropertyValue })
                                   );

            }

            return this;

        }

        #endregion

        #region GetProperty(myPropertyKey)

        /// <summary>
        /// Return the property value associated with the given property key.
        /// </summary>
        /// <param name="myPropertyKey">The key of the key/value property.</param>
        /// <returns>The property value related to the string key.</returns>
        public virtual TValue GetProperty(TKey myPropertyKey)
        {

            TValue _Object;

            _Properties.TryGetValue(myPropertyKey, out _Object);

            return _Object;

        }

        #endregion

        #region TryGetProperty(myPropertyKey, out myPropertyValue)

        /// <summary>
        /// Try to return the property value associated with the given property key.
        /// </summary>
        /// <param name="myPropertyKey">The key of the key/value property.</param>
        /// <param name="myPropertyValue">The value of the key/value property.</param>
        /// <returns>True if the returned value is valid.</returns>
        public virtual Boolean TryGetProperty(TKey myPropertyKey, out TValue myPropertyValue)
        {
            return _Properties.TryGetValue(myPropertyKey, out myPropertyValue);
        }

        #endregion

        #region GetProperties(myPropertyFilter = null)

        /// <summary>
        /// Allows to return a filtered enumeration of all properties.
        /// </summary>
        /// <param name="myPropertyFilter">A function to filter a property based on its key and value.</param>
        /// <returns>A enumeration of all key/value pairs matching the given property filter.</returns>
        public virtual IEnumerable<KeyValuePair<TKey, TValue>> GetProperties(Func<TKey, TValue, Boolean> myPropertyFilter = null)
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
        public virtual TValue RemoveProperty(TKey myPropertyKey)
        {

            if (myPropertyKey.Equals(_IdKey))
                throw new ArgumentException("Removing the Id property is not allowed!");

            if (myPropertyKey.Equals(_RevisionIdKey))
                throw new ArgumentException("Removing the RevisionId property is not allowed!");

            TValue _Object;

            if (_Properties.TryGetValue(myPropertyKey, out _Object))
                _Properties.Remove(myPropertyKey);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                                        NotifyCollectionChangedAction.Remove,
                                        myPropertyKey)
                                   );

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
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _Properties.GetEnumerator();
        }

        #endregion


        //#region Operator overloading

        //#region Operator == (myAProperties1, myAProperties2)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="myAProperties1">A AElement.</param>
        ///// <param name="myAProperties2">A IElement.</param>
        ///// <returns>true|false</returns>
        //public static Boolean operator == (AProperties<TId, TRevisionId, TKey, TValue, TDatastructure> myAProperties1, AProperties<TId, TRevisionId, TKey, TValue, TDatastructure> myAProperties2)
        //{

        //    // If both are null, or both are same instance, return true.
        //    if (Object.ReferenceEquals(myAProperties1, myAProperties2))
        //        return true;

        //    // If one is null, but not both, return false.
        //    if (((Object) myAProperties1 == null) || ((Object) myAProperties2 == null))
        //        return false;

        //    return myAProperties1.Equals(myAProperties2);

        //}

        //#endregion

        //#region Operator != (myAProperties1, myAProperties2)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="myAProperties1">A AElement.</param>
        ///// <param name="myAProperties2">A IElement.</param>
        ///// <returns>true|false</returns>
        //public static Boolean operator != (AProperties<TId, TRevisionId, TKey, TValue, TDatastructure> myAProperties1, AProperties<TId, TRevisionId, TKey, TValue, TDatastructure> myAProperties2)
        //{
        //    return !(myAProperties1 == myAProperties2);
        //}

        //#endregion

        //#endregion

        //#region IComparable<IElement> Members

        //#region CompareTo(myObject)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="myObject">An object to compare with.</param>
        ///// <returns>true|false</returns>
        //public abstract Int32 CompareTo(Object myObject);

        //#endregion

        //#region CompareTo(myObject)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="myObject">An object to compare with.</param>
        ///// <returns>true|false</returns>
        //public Int32 CompareTo(TId myObject)
        //{
        //    return 0;
        //    //return Id..CompareTo(myObject);
        //}

        //#endregion

        //#region CompareTo(myIProperties)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="myIProperties">An object to compare with.</param>
        ///// <returns>true|false</returns>
        //public Int32 CompareTo(IProperties<TKey, TValue, TDatastructure> myIProperties)
        //{

        //    // Check if myIElement is null
        //    if (myIProperties == null)
        //        throw new ArgumentNullException("myIElement must not be null!");

        //    return Id.CompareTo(myIProperties.GetProperty(_IdKey));

        //}

        //#endregion

        //#endregion

        //#region IEquatable<IElement> Members

        //#region Equals(myObject)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="myObject">An object to compare with.</param>
        ///// <returns>true|false</returns>
        //public override Boolean Equals(Object myObject)
        //{

        //    if (myObject == null)
        //        return false;

        //    var _Object = myObject as IProperties<TKey, TValue, TDatastructure>;
        //    if (_Object == null)
        //        return Equals(_Object);

        //    return false;

        //}

        //#endregion

        //#region Equals(myObject)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="myObject">An object to compare with.</param>
        ///// <returns>true|false</returns>
        //public Boolean Equals(TId myObject)
        //{
        //    return Id.Equals(myObject);
        //}

        //#endregion


        //#region Equals(myIProperties)

        /////// <summary>
        /////// Compares two instances of this object.
        /////// </summary>
        /////// <param name="myIProperties">An object to compare with.</param>
        /////// <returns>true|false</returns>
        ////public Boolean Equals(IProperties<TKey> myIProperties)
        ////{

        ////    if ((Object) myIProperties == null)
        ////        return false;

        ////    //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
        ////    return this.Count() == myIProperties.Count();

        ////}

        //#endregion

        //#endregion

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

    }

}
