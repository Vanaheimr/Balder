/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using System;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using de.ahzf.blueprints.datastructures;

#endregion

namespace de.ahzf.blueprints.InMemoryGraph
{

    /// <summary>
    /// An element is the base class for both vertices and edges.
    /// An element has an identifier that must be unique to its inheriting classes (vertex or edges).
    /// An element can maintain a collection of key/value properties.
    /// Keys are always strings and values can be any object.
    /// Particular implementations can reduce the space of objects that can be used as values.
    /// </summary>
    public abstract class AElement : DynamicObject, IElement
    {

        #region Data

        protected readonly IDictionary<String, Object> _Properties;

        /// <summary>
        /// The key of the Id property
        /// </summary>
        public const String __Id = "Id";

        #endregion

        #region Properties

        #region Properties

        #region Id

        /// <summary>
        /// An identifier that is unique to its inheriting class.
        /// All vertices of a graph must have unique identifiers.
        /// All edges of a graph must have unique identifiers.
        /// </summary>
        public ElementId Id
        {
            get
            {

                Object _Object = null;

                if (_Properties.TryGetValue(__Id, out _Object))
                    return _Object as ElementId;

                return null;

            }
        }

        #endregion

        #endregion

        //#region COMMENT

        //protected readonly String __COMMENT;

        //public String COMMENT
        //{

        //    get
        //    {

        //        Object _Object = null;

        //        if (_Properties.TryGetValue(__COMMENT, out _Object))
        //            return _Object as String;

        //        return null;

        //    }

        //    set
        //    {

        //        if (_Properties.ContainsKey(__COMMENT))
        //            _Properties[__COMMENT] = value;

        //        else
        //            _Properties.Add(__COMMENT, value);

        //    }

        //}

        //#endregion

        #endregion

        #region Protected Constructor(s)

        #region AElement(myId)

        protected AElement(ElementId myId)
        {

            // StringComparer.OrdinalIgnoreCase is often used to compare file names,
            // path names, network paths, and any other string whose value does not change
            // based on the locale of the user's computer.
            _Properties = new Dictionary<String, Object>(StringComparer.OrdinalIgnoreCase);
            _Properties.Add(__Id, myId);

        }

        #endregion

        #endregion


        #region Graph Properties

        #region SetProperty(myKey, myValue)

        /// <summary>
        /// Assign a key/value property to the element.
        /// If a value already exists for this key, then the previous key/value is overwritten.
        /// </summary>
        /// <param name="myKey">the string key of the property</param>
        /// <param name="myValue">the object value o the property</param>
        public virtual void SetProperty(String myKey, Object myValue)
        {

            if (myKey == __Id)
                throw new ArgumentException("Changing the Id property is not allowed!");

            //if (myKey == __REVISIONID && !(myObject is RevisionID))
            //    throw new ArgumentException();

            _Properties.Add(myKey, myValue);

        }

        #endregion

        #region GetProperty(myKey)

        public virtual Object GetProperty(String myKey)
        {

            Object _Object = null;

            _Properties.TryGetValue(myKey, out _Object);

            return _Object;

        }

        #endregion

        #region GetProperties(myPropertyFilter = null)

        public virtual IEnumerable<KeyValuePair<String, Object>> GetProperties(Func<String, Object, Boolean> myPropertyFilter = null)
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

        #region RemoveProperty(myKey)

        public virtual Object RemoveProperty(String myKey)
        {

            if (myKey == __Id)
                throw new ArgumentException("Removing the Id property is not allowed!");

            Object _Object = null;

            if (_Properties.TryGetValue(myKey, out _Object))
                _Properties.Remove(myKey);

            return _Object;

        }

        #endregion

        #endregion



        #region DynamicObject Members

        #region GetDynamicMemberNames()

        public override IEnumerable<String> GetDynamicMemberNames()
        {
            return _Properties.Keys;
        }

        #endregion

        #region TrySetMember(myBinder, myObject)

        public override Boolean TrySetMember(SetMemberBinder myBinder, Object myObject)
        {

            if (_Properties.ContainsKey(myBinder.Name))
                _Properties[myBinder.Name] = myObject;

            else
                _Properties.Add(myBinder.Name, myObject);

            return true;

        }

        #endregion

        #region TryGetMember(myBinder, out myObject)

        public override Boolean TryGetMember(GetMemberBinder myBinder, out Object myObject)
        {
            return _Properties.TryGetValue(myBinder.Name, out myObject);
        }

        #endregion

        #region TryInvokeMember(myBinder, out myObject)

        public override Boolean TryInvokeMember(InvokeMemberBinder myBinder, Object[] myArguments, out Object myObject)
        {

            var _Delegate = _Properties[myBinder.Name] as Delegate;
            if (_Delegate != null)
            {
                myObject = _Delegate.DynamicInvoke(myArguments);
                return true;
            }

            myObject = null;
            return false;

        }

        #endregion

        #region TryDeleteMember(myBinder)

        public override Boolean TryDeleteMember(DeleteMemberBinder myBinder)
        {
            return _Properties.Remove(myBinder.Name);
        }

        #endregion

        #endregion


        #region Operator overloading

        #region Operator == (myAElement1, myIElement2)

        public static Boolean operator == (AElement myAElement1, IElement myIElement2)
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

        public static Boolean operator != (AElement myAElement1, IElement myIElement2)
        {
            return !(myAElement1 == myIElement2);
        }

        #endregion

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Properties.GetEnumerator();
        }

        #endregion

        #region IEnumerable<KeyValuePair<String, Object>> Members

        public IEnumerator<KeyValuePair<String, Object>> GetEnumerator()
        {
            return _Properties.GetEnumerator();
        }

        #endregion

        #region IEquatable<IElement> Members

        #region Equals(myObject)

        public override Boolean Equals(Object myObject)
        {

            if (myObject == null)
                return false;

            var _Object = myObject as AElement;
            if (_Object == null)
                return Equals(_Object);

            return false;

        }

        #endregion

        #region Equals(myIElement)

        public Boolean Equals(IElement myIElement)
        {

            if ((Object) myIElement == null)
                return false;

            //TODO: Here it might be good to check all attributes of the UNIQUE constraint!
            return (this.Id == myIElement.Id);

        }

        #endregion

        #endregion

        #region GetHashCode()

        public override Int32 GetHashCode()
        {

            if (Id == null)
                return 0;

            return Id.GetHashCode();

        }

        #endregion

        #region ToString()

        public override String ToString()
        {

            var IdString = (Id == null) ? "<null>" : Id.ToString();

            return this.GetType().Name + "(Id = " + IdString + ")";

        }

        #endregion


    }

}
