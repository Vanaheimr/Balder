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
using System.Collections;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.Specialized;

#endregion

namespace de.ahzf.Blueprints.PropertyGraph.InMemory
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

        public void OnPropertyChanging(String PropertyName)
        {
            if (PropertyChanging != null)
                PropertyChanging(this, new PropertyChangingEventArgs(PropertyName));
        }

        public void OnPropertyChanging<TResult>(Expression<Func<TResult>> myPropertyExpression)
        {
            OnPropertyChanged(((MemberExpression) myPropertyExpression.Body).Member.Name);
        }

        #endregion

        #region PropertyChanged/OnPropertyChanged(...)

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(String PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        public void OnPropertyChanged<TResult>(Expression<Func<TResult>> myPropertyExpression)
        {
            OnPropertyChanged(((MemberExpression)myPropertyExpression.Body).Member.Name);
        }

        #endregion

        #endregion

        #region Protected Constructor(s)

        #region (protected) AProperties(myId, myIdKey, myRevisionIdKey, myTDatastructureInitializer)

        /// <summary>
        /// Creates a new AElement object
        /// </summary>
        /// <param name="Id">The Id of the new property.</param>
        /// <param name="IdKey"></param>
        /// <param name="RevisionIdKey"></param>
        /// <param name="TDatastructureInitializer"></param>
        public Properties(TId Id, TKey IdKey, TKey RevisionIdKey, Func<TDatastructure> TDatastructureInitializer)
        {

            //if (IGraph == null)
            //    throw new ArgumentNullException("The graph reference must not be null!");

            if (Id == null)
                throw new ArgumentNullException("The Id must not be null!");

            if (IdKey == null)
                throw new ArgumentNullException("The IdKey must not be null!");

            if (RevisionIdKey == null)
                throw new ArgumentNullException("The RevisionIdKey must not be null!");

           // Graph = myIGraph;

            _IdKey         = IdKey;
            _RevisionIdKey = RevisionIdKey;
            _Properties    = TDatastructureInitializer();
            _Properties.Add(_IdKey, Id);

        }

        #endregion

        #endregion


        #region Graph Properties

        #region SetProperty(PropertyKey, PropertyValue)

        /// <summary>
        /// Assign a key/value property to the element.
        /// If a value already exists for this key, then the previous key/value is overwritten.
        /// </summary>
        /// <param name="PropertyKey">The property key.</param>
        /// <param name="PropertyValue">The property value.</param>
        public virtual IProperties<TKey, TValue> SetProperty(TKey PropertyKey, TValue PropertyValue)
        {

            if (PropertyKey.Equals(_IdKey))
                throw new ArgumentException("Changing the Id property is not allowed!");

            if (PropertyKey.Equals(_RevisionIdKey))
                throw new ArgumentException("Changing the RevisionId property is not allowed!");

            if (_Properties.ContainsKey(PropertyKey))
            {
                OnPropertyChanging(PropertyKey.ToString());
                _Properties[PropertyKey] = PropertyValue;
                OnPropertyChanged(PropertyKey.ToString());
            }

            else
            {
                
                _Properties.Add(PropertyKey, PropertyValue);
                
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                                        NotifyCollectionChangedAction.Add,
                                        new Object[] { PropertyKey, PropertyValue })
                                   );

            }

            return this;

        }

        #endregion

        #region Contains(Key)

        /// <summary>
        /// Checks if the given property key is assigned.
        /// </summary>
        /// <param name="Key">A property key.</param>
        public Boolean Contains(TKey Key)
        {
            return _Properties.ContainsKey(Key);
        }

        #endregion

        #region Contains(Key, Value)

        /// <summary>
        /// Checks if the given key/value pair is assigned.
        /// </summary>
        /// <param name="Key">A property key.</param>
        /// <param name="Value">A property value.</param>
        public Boolean Contains(TKey Key, TValue Value)
        {
            return _Properties.Contains(new KeyValuePair<TKey,TValue>(Key, Value));
        }

        #endregion

        #region Contains(KeyValuePair)

        /// <summary>
        /// Checks if the given key/value pair is assigned.
        /// </summary>
        /// <param name="KeyValuePair">A KeyValuePair.</param>
        public Boolean Contains(KeyValuePair<TKey, TValue> KeyValuePair)
        {
            return _Properties.Contains(KeyValuePair);
        }

        #endregion

        #region GetProperty(PropertyKey)

        /// <summary>
        /// Return the property value associated with the given property key.
        /// </summary>
        /// <param name="PropertyKey">The key of the key/value property.</param>
        /// <returns>The property value related to the string key.</returns>
        public virtual TValue GetProperty(TKey PropertyKey)
        {

            TValue _Object;

            _Properties.TryGetValue(PropertyKey, out _Object);

            return _Object;

        }

        #endregion

        #region TryGetProperty(PropertyKey, out PropertyValue)

        /// <summary>
        /// Try to return the property value associated with the given property key.
        /// </summary>
        /// <param name="PropertyKey">The key of the key/value property.</param>
        /// <param name="PropertyValue">The value of the key/value property.</param>
        /// <returns>True if the returned value is valid.</returns>
        public virtual Boolean TryGetProperty(TKey PropertyKey, out TValue PropertyValue)
        {
            return _Properties.TryGetValue(PropertyKey, out PropertyValue);
        }

        #endregion

        #region GetProperties(PropertyFilter = null)

        /// <summary>
        /// Allows to return a filtered enumeration of all properties.
        /// </summary>
        /// <param name="PropertyFilter">A function to filter a property based on its key and value.</param>
        /// <returns>A enumeration of all key/value pairs matching the given property filter.</returns>
        public virtual IEnumerable<KeyValuePair<TKey, TValue>> GetProperties(PropertyFilter<TKey, TValue> PropertyFilter = null)
        {

            foreach (var _KeyValuePair in _Properties)
            {

                if (_KeyValuePair.Value != null)
                {

                    if (PropertyFilter == null)
                        yield return _KeyValuePair;

                    else if (PropertyFilter(_KeyValuePair.Key, _KeyValuePair.Value))
                        yield return _KeyValuePair;

                }

            }

        }

        #endregion

        #region RemoveProperty(PropertyKey)

        /// <summary>
        /// Removes the property identified by the given property key.
        /// </summary>
        /// <param name="PropertyKey">The key of the property to remove.</param>
        /// <returns>The property value associated with that key prior to removal.</returns>
        public virtual TValue RemoveProperty(TKey PropertyKey)
        {

            if (PropertyKey.Equals(_IdKey))
                throw new ArgumentException("Removing the Id property is not allowed!");

            if (PropertyKey.Equals(_RevisionIdKey))
                throw new ArgumentException("Removing the RevisionId property is not allowed!");

            TValue _Object;

            if (_Properties.TryGetValue(PropertyKey, out _Object))
                _Properties.Remove(PropertyKey);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                                        NotifyCollectionChangedAction.Remove,
                                        PropertyKey)
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

        //#region Operator == (AProperties1, myAProperties2)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="AProperties1">A AElement.</param>
        ///// <param name="AProperties2">A IElement.</param>
        ///// <returns>true|false</returns>
        //public static Boolean operator == (AProperties<TId, TRevisionId, TKey, TValue, TDatastructure> myAProperties1, AProperties<TId, TRevisionId, TKey, TValue, TDatastructure> myAProperties2)
        //{

        //    // If both are null, or both are same instance, return true.
        //    if (Object.ReferenceEquals(AProperties1, myAProperties2))
        //        return true;

        //    // If one is null, but not both, return false.
        //    if (((Object) myAProperties1 == null) || ((Object) myAProperties2 == null))
        //        return false;

        //    return myAProperties1.Equals(AProperties2);

        //}

        //#endregion

        //#region Operator != (AProperties1, myAProperties2)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="AProperties1">A AElement.</param>
        ///// <param name="AProperties2">A IElement.</param>
        ///// <returns>true|false</returns>
        //public static Boolean operator != (AProperties<TId, TRevisionId, TKey, TValue, TDatastructure> myAProperties1, AProperties<TId, TRevisionId, TKey, TValue, TDatastructure> myAProperties2)
        //{
        //    return !(AProperties1 == myAProperties2);
        //}

        //#endregion

        //#endregion

        //#region IComparable<IElement> Members

        //#region CompareTo(Object)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="Object">An object to compare with.</param>
        ///// <returns>true|false</returns>
        //public abstract Int32 CompareTo(Object myObject);

        //#endregion

        //#region CompareTo(Object)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="Object">An object to compare with.</param>
        ///// <returns>true|false</returns>
        //public Int32 CompareTo(TId myObject)
        //{
        //    return 0;
        //    //return Id..CompareTo(Object);
        //}

        //#endregion

        //#region CompareTo(IProperties)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="IProperties">An object to compare with.</param>
        ///// <returns>true|false</returns>
        //public Int32 CompareTo(IProperties<TKey, TValue, TDatastructure> myIProperties)
        //{

        //    // Check if myIElement is null
        //    if (IProperties == null)
        //        throw new ArgumentNullException("myIElement must not be null!");

        //    return Id.CompareTo(IProperties.GetProperty(_IdKey));

        //}

        //#endregion

        //#endregion

        //#region IEquatable<IElement> Members

        //#region Equals(Object)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="Object">An object to compare with.</param>
        ///// <returns>true|false</returns>
        //public override Boolean Equals(Object myObject)
        //{

        //    if (Object == null)
        //        return false;

        //    var _Object = myObject as IProperties<TKey, TValue, TDatastructure>;
        //    if (_Object == null)
        //        return Equals(_Object);

        //    return false;

        //}

        //#endregion

        //#region Equals(Object)

        ///// <summary>
        ///// Compares two instances of this object.
        ///// </summary>
        ///// <param name="Object">An object to compare with.</param>
        ///// <returns>true|false</returns>
        //public Boolean Equals(TId myObject)
        //{
        //    return Id.Equals(Object);
        //}

        //#endregion


        //#region Equals(IProperties)

        /////// <summary>
        /////// Compares two instances of this object.
        /////// </summary>
        /////// <param name="IProperties">An object to compare with.</param>
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
