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
using System.Collections.Generic;

#endregion

namespace de.ahzf.Blueprints
{

    #region ElementSelector<T>(Element)

    /// <summary>
    /// A delegate selecting which element to return.
    /// </summary>
    /// <typeparam name="T">The internal type of the element.</typeparam>
    /// <param name="Element">An element of type T.</param>
    /// <returns>True if the element is selected; False otherwise.</returns>
    public delegate Boolean ElementSelector<T>(T Element)
        where T : IEquatable<T>, IComparable<T>, IComparable;

    #endregion

    #region BintreeSplitEventHandler<T>(Bintree, Element)

    /// <summary>
    /// An event handler delegate definition whenever an
    /// bintree splits an internal line.
    /// </summary>
    /// <typeparam name="T">The type of the Bintree.</typeparam>
    /// <param name="Bintree">The sending bintree.</param>
    /// <param name="Element">The element causing the split.</param>
    public delegate void BintreeSplitEventHandler<T>(Bintree<T> Bintree, T Element)
        where T : IEquatable<T>, IComparable<T>, IComparable;

    #endregion

    #region Bintree<T>

    /// <summary>
    /// A bintree is an indexing structure for 1-dimensional spartial data.
    /// It stores the given maximum number of elements and forkes itself
    /// into two subtrees if this number becomes larger.
    /// Note: This datastructure is not self-balancing!
    /// </summary>
    /// <typeparam name="T">The internal datatype of the Bintree.</typeparam>
    public class Bintree<T> : Line<T>, IBintree<T>
        where T : IEquatable<T>, IComparable<T>, IComparable
    {

        #region Data

        private Bintree<T> Subtree1;
        private Bintree<T> Subtree2;

        private HashSet<T> EmbeddedData;

        #endregion

        #region Events

        #region OnTreeSplit

        /// <summary>
        /// An event to notify about a bintree split happening.
        /// </summary>
        public event BintreeSplitEventHandler<T> OnTreeSplit;

        #endregion

        #endregion

        #region Properties

        #region MaxNumberOfEmbeddedElements

        /// <summary>
        /// The maximum number of embedded elements before
        /// two subtrees will be created.
        /// </summary>
        public UInt32 MaxNumberOfEmbeddedElements { get; private set; }

        #endregion

        #region EmbeddedCount

        /// <summary>
        /// Return the number of embedded elements
        /// stored within this bintree(-node).
        /// </summary>
        public UInt64 EmbeddedCount
        {
            get
            {
                return (UInt64) EmbeddedData.Count;
            }
        }

        #endregion

        #region Count

        /// <summary>
        /// Return the number of elements stored
        /// within the entire bintree.
        /// </summary>
        public UInt64 Count
        {
            get
            {
                
                var _Count = (UInt64) EmbeddedData.Count;

                if (Subtree1 != null)
                    _Count += Subtree1.Count;

                if (Subtree2 != null)
                    _Count += Subtree2.Count;

                return _Count;

            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region Bintree(MaxNumberOfEmbeddedData = 256)

        /// <summary>
        /// Create a bintree of type T.
        /// </summary>
        /// <param name="Left">The left-coordinate of the line.</param>
        /// <param name="Right">The right-coordinate of the line.</param>
        /// <param name="MaxNumberOfEmbeddedElements">The maximum number of embedded elements before four child node will be created.</param>
        public Bintree(T Left, T Right, UInt32 MaxNumberOfEmbeddedElements = 256)
            : base(Left, Right)
        {
            this.MaxNumberOfEmbeddedElements = MaxNumberOfEmbeddedElements;
            this.EmbeddedData                = new HashSet<T>();
        }

        #endregion

        #endregion


        #region Add(Element)

        /// <summary>
        /// Add an element to the bintree.
        /// </summary>
        /// <param name="Element">A element of type T.</param>
        public void Add(T Element)
        {

            if (this.Contains(Element))
            {

                #region Check subtrees...

                if      (Subtree1 != null && Subtree1.Contains(Element))
                    Subtree1.Add(Element);

                else if (Subtree2 != null && Subtree2.Contains(Element))
                    Subtree2.Add(Element);

                #endregion

                #region ...or the embedded data.

                else if (EmbeddedData.Count < MaxNumberOfEmbeddedElements)
                    EmbeddedData.Add(Element);

                #endregion

                #region If necessary create subtrees...

                else
                {

                    #region Create Subtrees

                    if (Subtree1 == null)
                    {
                        Subtree1 = new Bintree<T>(Left,
                                                  Math.Add(Left, Math.Div2(Size)),
                                                  MaxNumberOfEmbeddedElements);
                        Subtree1.OnTreeSplit += OnTreeSplit;
                    }

                    if (Subtree2 == null)
                    {
                        Subtree2 = new Bintree<T>(Math.Add(Left, Math.Div2(Size)),
                                                  Right,
                                                  MaxNumberOfEmbeddedElements);
                        Subtree2.OnTreeSplit += OnTreeSplit;
                    }

                    #endregion

                    #region Fire BintreeSplit event

                    if (OnTreeSplit != null)
                        OnTreeSplit(this, Element);

                    #endregion

                    #region Move all embedded data into the subtrees

                    EmbeddedData.Add(Element);

                    foreach (var _Element in EmbeddedData)
                    {

                        if      (Subtree1.Contains(_Element))
                            Subtree1.Add(_Element);

                        else if (Subtree2.Contains(_Element))
                            Subtree2.Add(_Element);

                        else
                            throw new Exception("Mist!");

                    }

                    EmbeddedData.Clear();

                    #endregion

                }

                #endregion

            }

            else
                throw new BT_OutOfBoundsException<T>(this, Element);

        }

        #endregion

        #region Get(ElementSelector)

        /// <summary>
        /// Return all elements matching the given elementselector delegate.
        /// </summary>
        /// <param name="ElementSelector">A delegate selecting which elements to return.</param>
        public IEnumerable<T> Get(ElementSelector<T> ElementSelector)
        {

            #region Initial Checks

            if (ElementSelector == null)
                throw new ArgumentNullException("The given ElementSelector must not be null!");

            #endregion

            #region Check embedded element

            foreach (var _Element in EmbeddedData)
                if (ElementSelector(_Element))
                    yield return _Element;

            #endregion

            #region Check subtrees

            if (Subtree1 != null)
                foreach (var _Element in Subtree1.Get(ElementSelector))
                    yield return _Element;

            if (Subtree2 != null)
                foreach (var _Element in Subtree2.Get(ElementSelector))
                    yield return _Element;

            #endregion

        }

        #endregion

        #region Get(Line)

        /// <summary>
        /// Return all elements within the given line.
        /// </summary>
        /// <param name="Line">A line selecting which elements to return.</param>
        public IEnumerable<T> Get(ILine<T> Line)
        {

            #region Initial Checks

            if (Line == null)
                throw new ArgumentNullException("The given line must not be null!");

            #endregion

            #region Check embedded element

            foreach (var _Element in EmbeddedData)
                if (Line.Contains(_Element))
                    yield return _Element;

            #endregion

            #region Check subtrees

            if (Subtree1 != null)
                if (Subtree1.Overlaps(Line))
                    foreach (var _Element in Subtree1.Get(Line))
                        yield return _Element;

            if (Subtree2 != null)
                if (Subtree2.Overlaps(Line))
                    foreach (var _Element in Subtree2.Get(Line))
                        yield return _Element;

            #endregion

        }

        #endregion

        #region Remove(Element)

        /// <summary>
        /// Remove an element from the bintree.
        /// </summary>
        /// <param name="Element">A element of type T.</param>
        public void Remove(T Element)
        {

            #region Initial Checks

            if (Element == null)
                throw new ArgumentNullException("The given element must not be null!");

            #endregion

            #region Remove embedded element

            EmbeddedData.Remove(Element);

            #endregion

            #region Check subtrees

            if (Subtree1 != null)
                if (Subtree1.Contains(Element))
                    Subtree1.Remove(Element);

            if (Subtree2 != null)
                if (Subtree2.Contains(Element))
                    Subtree2.Remove(Element);

            #endregion

        }

        #endregion

        #region Remove(Line)

        /// <summary>
        /// Remove all elements located within the given line.
        /// </summary>
        /// <param name="Line">A line selecting which elements to remove.</param>
        public void Remove(ILine<T> Line)
        {

            #region Initial Checks

            if (Line == null)
                throw new ArgumentNullException("The given line must not be null!");

            #endregion

            #region Remove embedded data

            lock (this)
            {

                var _List = new List<T>();

                foreach (var _Element in EmbeddedData)
                    if (Line.Contains(_Element))
                        _List.Add(_Element);

                foreach (var _Element in _List)
                    EmbeddedData.Remove(_Element);

            }

            #endregion

            #region Check subtrees

            if (Subtree1 != null)
                if (Subtree1.Overlaps(Line))
                    Subtree1.Remove(Line);

            if (Subtree2 != null)
                if (Subtree2.Overlaps(Line))
                    Subtree2.Remove(Line);

            #endregion

        }

        #endregion


        #region IEnumerable Members

        /// <summary>
        /// Return an enumeration of all stored data.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {

            foreach (var _Element in EmbeddedData)
                yield return _Element;

            if (Subtree1 != null)
                foreach (var _Element in Subtree1)
                    yield return _Element;

            if (Subtree2 != null)
                foreach (var _Element in Subtree2)
                    yield return _Element;

        }

        /// <summary>
        /// Return an enumeration of all stored data.
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {

            foreach (var _Element in EmbeddedData)
                yield return _Element;

            if (Subtree1 != null)
                foreach (var _Element in Subtree1)
                    yield return _Element;

            if (Subtree2 != null)
                foreach (var _Element in Subtree2)
                    yield return _Element;

        }

        #endregion


        #region ToString()

        /// <summary>
        /// Return a string representation of this object.
        /// </summary>
        public override String ToString()
        {
            return String.Format("{0} Element(s); Bounds = [Left={1}, Right={2}]",
                                 EmbeddedData.Count,
                                 Left.  ToString(),
                                 Right. ToString());
        }

        #endregion

    }

    #endregion

}
