/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;

#endregion

namespace de.ahzf.blueprints
{

    /// <summary>
    /// A factory which uses reflection to generate a apropriate
    /// implementation of T for you.
    /// </summary>
    public class AutoDiscovery<T>
        // This is not perfect! But there is no 'where T : interface'!
        where T : class
    {

        #region Data

        private readonly ConcurrentDictionary<String, Type> _TypeDictionary;

        #endregion

        #region Properties

        #region SearchingFor

        /// <summary>
        /// Returns the Name of the interface T.
        /// </summary>
        public String SearchingFor
        {
            get
            {
                return typeof(T).Name;
            }
        }

        #endregion

        #region RegisteredNames

        /// <summary>
        /// Returns an enumeration of the names of all registered types of T.
        /// </summary>
        public IEnumerable<String> RegisteredNames
        {
            get
            {
                return from _StringTypePair in _TypeDictionary select _StringTypePair.Key;
            }
        }

        #endregion

        #region RegisteredTypes

        /// <summary>
        /// Returns an enumeration of activated instances of all registered types of T.
        /// </summary>
        public IEnumerable<T> RegisteredTypes
        {
            get
            {

                try
                {

                    return from _StringTypePair in _TypeDictionary select (T) Activator.CreateInstance(_StringTypePair.Value);

                }
                catch (Exception e)
                {
                    throw new AutoDiscoveryException("Could not create instance! " + e);
                }

            }
        }

        #endregion

        #region Count

        /// <summary>
        /// Returns the number of registered implementations of the interface T.
        /// </summary>
        public UInt64 Count
        {
            get
            {
                return (UInt64) _TypeDictionary.LongCount();
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region AutoDiscovery()

        /// <summary>
        /// Create a new AutoDiscovery instance and start the discovery.
        /// </summary>
        public AutoDiscovery()
            : this(true)
        { }

        #endregion

        #region AutoDiscovery(myAutostart)

        /// <summary>
        /// Create a new AutoDiscovery instance. An automatic discovery
        /// can be avoided.
        /// </summary>
        public AutoDiscovery(Boolean myAutostart)
        {

            _TypeDictionary = new ConcurrentDictionary<String, Type>();
            
            if (myAutostart)            
                FindAndRegister();

        }

        #endregion

        #endregion


        #region FindAndRegisterImplementations(myPaths, myIdentificator)

        /// <summary>
        /// Searches all matching files at the given paths for classes implementing the interface &lt;T&gt;.
        /// </summary>
        /// <param name="myClearTypeDictionary">Clears the TypeDictionary before adding new implementations.</param>
        /// <param name="myPaths">An enumeration of paths to search for implementations.</param>
        /// <param name="myFileExtensions">A enumeration of file extensions for filtering.</param>
        /// <param name="myIdentificator">A transformation of a type into its identification.</param>
        public void FindAndRegister(Boolean myClearTypeDictionary = true, IEnumerable<String> myPaths = null, IEnumerable<String> myFileExtensions = null, Func<Type, String> myIdentificator = null)
        {

            #region Get a list of interesting files

            var _ConcurrentBag = new ConcurrentBag<String>();

            if (myPaths == null)
                myPaths = new List<String> { "." };

            if (myFileExtensions == null)
                myFileExtensions = new List<String> { ".dll", ".exe" };

            foreach (var _Path in myPaths)
            {

                Parallel.ForEach(Directory.GetFiles(_Path), _ActualFile =>
                {

                    var _FileInfo = new FileInfo(_ActualFile);

                    if (myFileExtensions.Contains(_FileInfo.Extension))
                        _ConcurrentBag.Add(_FileInfo.FullName);

                });

            }

            if (myClearTypeDictionary)
                _TypeDictionary.Clear();

            #endregion

            #region Scan files of implementations of T

            Parallel.ForEach(_ConcurrentBag, _File =>
            {

                try
                {

                    foreach (var _ActualType in Assembly.LoadFrom(_File).GetTypes())
                    {

                        if (!_ActualType.IsAbstract &&
                             _ActualType.IsPublic   &&
                             _ActualType.IsVisible)
                        {

                            var _ActualTypeGetInterfaces = _ActualType.GetInterfaces();

                            if (_ActualTypeGetInterfaces != null)
                            {

                                foreach (var _Interface in _ActualTypeGetInterfaces)
                                {

                                    if (_Interface == typeof(T))
                                    {

                                        try
                                        {

                                            var __Id = _ActualType.Name;

                                            if (myIdentificator != null)
                                                __Id = myIdentificator(_ActualType);

                                            if (__Id != null && __Id != String.Empty)
                                                _TypeDictionary.TryAdd(__Id, _ActualType);

                                        }

                                        catch (Exception e)
                                        {
                                            throw new AutoDiscoveryException("Could not activate or register " + typeof(T).Name + "-instance '" + _ActualType.Name + "'!", e);
                                        }

                                    }

                                }

                            }

                        }

                    }

                }

                catch (Exception e)
                {
                    throw new AutoDiscoveryException("Autodiscovering implementations of interface '" + typeof(T).Name + "' within file '" + _File + "' failed!", e);
                }

            });

            #endregion

        }

        #endregion

        #region Activate(myImplementationID)

        /// <summary>
        /// Activates a new instance of an implementation based on its identification.
        /// </summary>
        /// <param name="myImplementationID">The identification of the implementation to activate.</param>
        /// <returns>An activated class implementing interface T.</returns>
        public T Activate(String myImplementationID)
        {

            lock (this)
            {

                try
                {

                    Type _Type;

                    if (_TypeDictionary.TryGetValue(myImplementationID, out _Type))
                        if (_Type != null)
                            return (T) Activator.CreateInstance(_Type);

                }
                catch (Exception e)
                {
                    throw new AutoDiscoveryException(typeof(T).Name + " implementation '" + myImplementationID + "' could not be activated!", e);
                }

                throw new AutoDiscoveryException(typeof(T).Name + " implementation '" + myImplementationID + "' could not be activated!");

            }

        }

        #endregion

        #region TryActivate(myImplementationID, out myInstance)

        /// <summary>
        /// Tries to activate a new instance of an implementation based on its identification.
        /// </summary>
        /// <param name="myImplementationID">The identification of the implementation to activate.</param>
        /// <param name="myInstance">The activated class implementing interface T.</param>
        /// <returns>true|false</returns>
        public Boolean TryActivate(String myImplementationID, out T myInstance)
        {

            lock (this)
            {

                try
                {

                    Type _Type;

                    if (_TypeDictionary.TryGetValue(myImplementationID, out _Type))
                        if (_Type != null)
                        {
                            myInstance = (T) Activator.CreateInstance(_Type);
                            return true;
                        }


                }
                catch (Exception)
                { }

                myInstance = default(T);

                return false;

            }

        }

        #endregion

    }

}
