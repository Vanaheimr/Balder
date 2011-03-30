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
using System.ComponentModel;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.Specialized;

#endregion

namespace de.ahzf.blueprints.InMemory.PropertyGraph.Generic
{

    public abstract class AElement<TId, TRevisionId, TKey, TValue, TDatastructure>
        
        where TKey           : IEquatable<TKey>,        IComparable<TKey>,        IComparable
        where TId            : IEquatable<TId>,         IComparable<TId>,         IComparable, TValue
        where TRevisionId    : IEquatable<TRevisionId>, IComparable<TRevisionId>, IComparable, TValue
        where TDatastructure : IDictionary<TKey, TValue>

    {

        #region Data

        protected readonly TKey _IdKey;
        protected readonly TKey _RevisonIdKey;

        #endregion

        #region Properties

        #region Id

        public TId Id
        {
            get
            {
                return (TId) Data.GetProperty(_IdKey);
            }
        }

        #endregion

        #region RevisionId

        public TRevisionId RevisionId
        {
            get
            {
                return (TRevisionId) Data.GetProperty(_RevisonIdKey);
            }
        }

        #endregion

        #region Data

        protected readonly IProperties<TKey, TValue, TDatastructure> _Data;

        public IProperties<TKey, TValue, TDatastructure> Data
        {
            get
            {
                return _Data;
            }
        }

        #endregion

        #endregion

        #region Events

        #region CollectionChanged/OnCollectionChanged(...)

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {

            add
            {
                Data.CollectionChanged += value;
            }

            remove
            {
                Data.CollectionChanged -= value;
            }

        }

        public void OnCollectionChanged(NotifyCollectionChangedEventArgs myNotifyCollectionChangedEventArgs)
        {
            Data.OnCollectionChanged(myNotifyCollectionChangedEventArgs);
        }

        #endregion

        #region PropertyChanging/OnPropertyChanging(...)

        public event PropertyChangingEventHandler PropertyChanging
        {

            add
            {
                Data.PropertyChanging += value;
            }

            remove
            {
                Data.PropertyChanging -= value;
            }

        }

        public void OnPropertyChanging(String myPropertyName)
        {
            Data.OnPropertyChanging(myPropertyName);
        }

        public void OnPropertyChanging<TResult>(Expression<Func<TResult>> myPropertyExpression)
        {
            Data.OnPropertyChanging(myPropertyExpression);
        }

        #endregion

        #region PropertyChanged/OnPropertyChanged(...)

        public event PropertyChangedEventHandler PropertyChanged
        {

            add
            {
                Data.PropertyChanged += value;
            }

            remove
            {
                Data.PropertyChanged -= value;
            }

        }

        public void OnPropertyChanged(String myPropertyName)
        {
            Data.OnPropertyChanged(myPropertyName);
        }

        public void OnPropertyChanged<TResult>(Expression<Func<TResult>> myPropertyExpression)
        {
            Data.OnPropertyChanged(myPropertyExpression);
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region AElement(myId, myEdgeInitializer = null)

        /// <summary>
        /// Creates a new AElement.
        /// </summary>
        internal protected AElement(TId                  myId,
                                    TKey                 myIdKey,
                                    TKey                 myRevisonIdKey,
                                    Func<TDatastructure> myDataInitializer,
                                    Action<IProperties<TKey, TValue, TDatastructure>> myElementInitializer = null)
        {

            _IdKey        = myIdKey;
            _RevisonIdKey = myRevisonIdKey;

            _Data         = new Properties<TId, TRevisionId, TKey, TValue, TDatastructure>
                                          (myId, myIdKey, myRevisonIdKey, myDataInitializer);

            // Add the label
            //_Properties.Add(__Label, myLabel);

            if (myElementInitializer != null)
                myElementInitializer(Data);

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
            return Id.GetHashCode();
        }

        #endregion

    }

}
