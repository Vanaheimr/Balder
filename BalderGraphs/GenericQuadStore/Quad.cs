/*
 * Copyright (c) 2010-2014 Achim 'ahzf' Friedland <achim@graphdefined.org>
 * This file is part of Alviss <http://www.github.com/Vanaheimr/Alviss>
 *
 * Licensed under the General Public License, Version 3.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.gnu.org/licenses/gpl.html
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

namespace eu.Vanaheimr.Alviss
{

    /// <summary>
    /// A Quad is a little fragment of a graph.
    /// QuadId: Subject -Predicate-> Object [Context/Graph] or
    /// VertexId: Vertex -Edge-> AnotherVertex [HyperEdge]
    /// </summary>
    /// <typeparam name="TSystemId">The type of the SystemId.</typeparam>
    /// <typeparam name="TQuadId">The type of the QuadId.</typeparam>
    /// <typeparam name="TTransactionId">The type of the transaction id.</typeparam>
    /// <typeparam name="TSPOC">The type of the subject, predicate, object and context.</typeparam>
    public class Quad<TSystemId, TQuadId, TTransactionId, TSPOC>
                     : IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>

        where TSystemId      : IEquatable<TSystemId>,      IComparable<TSystemId>,      IComparable
        where TQuadId        : IEquatable<TQuadId>,        IComparable<TQuadId>,        IComparable
        where TTransactionId : IEquatable<TTransactionId>, IComparable<TTransactionId>, IComparable
        where TSPOC          : IEquatable<TSPOC>,          IComparable<TSPOC>,          IComparable

    {

        #region Data

        #region QuadId, SystemId and TransactionId

        /// <summary>
        /// The Id of the quad.
        /// From another point of view this is an EdgeId.
        /// This Id is just local unique. To get a global unique
        /// Id add the SystemId of the QuadStore.
        /// </summary>
        public TQuadId QuadId                { get; private set; }

        /// <summary>
        /// The Id of the QuadStore which created this quad.
        /// </summary>
        public TSystemId SystemId            { get; private set; }

        /// <summary>
        /// The Id of the transaction this quad was build in.
        /// This Id is just local unique. To get a global unique
        /// Id add the SystemId of the QuadStore.
        /// </summary>
        public TTransactionId TransactionId  { get; private set; }

        #endregion

        #region Subject, Predicate, Object and Context

        /// <summary>
        /// The Subject of this quad.
        /// From another point of view this is an VertexId.
        /// </summary>
        public TSPOC Subject   { get; private set; }

        /// <summary>
        /// The Predicate of this quad.
        /// From another point of view this is a PropertyId.
        /// </summary>
        public TSPOC Predicate { get; private set; }

        /// <summary>
        /// The Object of this quad.
        /// </summary>
        public TSPOC Object    { get; private set; }

        /// <summary>
        /// The Context of this quad.
        /// From another point of view this is a HyperEdgeId.
        /// </summary>
        public TSPOC Context   { get; private set; }

        #endregion

        #region ObjectReference

        /// <summary>
        /// A hashset of references to quads having
        /// the Object of this quad as Subject.
        /// </summary>
        public HashSet<IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>> ObjectReference { get; set; }

        #endregion

        #region ObjectOnDisc

        /// <summary>
        /// The on disc position of this quad.
        /// </summary>
        public UInt64 ObjectOnDisc { get; private set; }

        #endregion

        #endregion

        #region Constructor(s)

        #region Quad(SystemId, TransactionId, QuadId, Subject, Predicate, Object, Context, ObjectOnDisc = default(T))

        /// <summary>
        /// A Quad is a little fragment of a graph.
        /// Subject -Predicate-> Object [Context]
        /// Vertex  -Edge->      Vertex [HyperEdge]
        /// </summary>
        /// <param name="SystemId">The Id of the QuadStore which created this quad.</param>
        /// <param name="TransactionId">The Id of the transaction this quad was build in.</param>
        /// <param name="QuadId">The Id of this quad.</param>
        /// <param name="Subject">The subject of this quad.</param>
        /// <param name="Predicate">The predicate of this quad.</param>
        /// <param name="Object">The object of this quad.</param>
        /// <param name="Context">The context of this quad.</param>
        /// <param name="ObjectOnDisc"></param>
        internal Quad(TSystemId      SystemId,
                    TTransactionId TransactionId,
                    TQuadId        QuadId,
                    TSPOC          Subject,
                    TSPOC          Predicate,
                    TSPOC          Object,
                    TSPOC          Context,
                    UInt64         ObjectOnDisc = default(UInt64))
        {

            #region Initial checks

            if (SystemId  == null || SystemId .Equals(default(TSystemId)))
                throw new ArgumentNullException("The SystemId must not be null or default(T)!");

            if (QuadId    == null || QuadId   .Equals(default(TQuadId)))
                throw new ArgumentNullException("The QuadId must not be null or default(T)!");

            if (Subject   == null || Subject  .Equals(default(TSPOC)))
                throw new ArgumentNullException("The Subject must not be null or default(T)!");

            if (Predicate == null || Predicate.Equals(default(TSPOC)))
                throw new ArgumentNullException("The Predicate must not be null or default(T)!");

            if (Object    == null || Object   .Equals(default(TSPOC)))
                throw new ArgumentNullException("The Object must not be null or default(T)!");

            if (Context   == null || Context.  Equals(default(TSPOC)))
                throw new ArgumentNullException("The Context must not be null or default(T)!");

            #endregion

            this.SystemId        = SystemId;
            this.TransactionId   = TransactionId;
            this.QuadId          = QuadId;
            this.Subject         = Subject;
            this.Predicate       = Predicate;
            this.Object          = Object;
            this.Context         = Context;
            this.ObjectOnDisc    = ObjectOnDisc;
            this.ObjectReference = null;

        }

        #endregion

        #endregion


        #region Operator overloading

        #region Operator == (Quad1, Quad2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Quad1">A Quad.</param>
        /// <param name="Quad2">Another Quad.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Quad<TSystemId, TQuadId, TTransactionId, TSPOC> Quad1,
                                           Quad<TSystemId, TQuadId, TTransactionId, TSPOC> Quad2)
        {

            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(Quad1, Quad2))
                return true;

            // If one is null, but not both, return false.
            if (((Object) Quad1 == null) || ((Object) Quad2 == null))
                return false;

            return Quad1.Equals(Quad2);

        }

        #endregion

        #region Operator != (Quad1, Quad2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Quad1">A Quad.</param>
        /// <param name="Quad2">Another Quad.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Quad<TSystemId, TQuadId, TTransactionId, TSPOC> Quad1,
                                           Quad<TSystemId, TQuadId, TTransactionId, TSPOC> Quad2)
        {
            return !(Quad1 == Quad2);
        }

        #endregion

        #endregion

        #region IEquatable<IQuad<T>> Members

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

            // Check if the given object is a IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>
            var AnotherQuad = Object as IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>;
            if ((Object) AnotherQuad == null)
                throw new ArgumentException("The given object is not a IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>!");

            return this.Equals(AnotherQuad);

        }

        #endregion

        #region Equals(AnotherQuad)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AnotherQuad">Another quad to compare with.</param>
        /// <returns>true|false</returns>
        public Boolean Equals(IQuad<TSystemId, TQuadId, TTransactionId, TSPOC> AnotherQuad)
        {

            if ((Object) AnotherQuad == null)
                throw new ArgumentNullException("Parameter AnotherQuad must not be null!");

            return QuadId.Equals(AnotherQuad.QuadId);

        }

        #endregion

        #endregion

        #region IComparable<IQuad<T>> Members

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

            // Check if the given object is a IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>
            var AnotherQuad = Object as IQuad<TSystemId, TQuadId, TTransactionId, TSPOC>;
            if ((Object) AnotherQuad == null)
                throw new ArgumentException("The given object is not a IQuad<T>!");

            return CompareTo(AnotherQuad);

        }

        #endregion

        #region CompareTo(AnotherQuad)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="AnotherQuad">Another quad to compare with.</param>
        /// <returns>true|false</returns>
        public Int32 CompareTo(IQuad<TSystemId, TQuadId, TTransactionId, TSPOC> AnotherQuad)
        {

            if ((Object) AnotherQuad == null)
                throw new ArgumentNullException("The given IQuad<TSystemId, TQuadId, TTransactionId, TSPOC> must not be null!");

            return QuadId.CompareTo(AnotherQuad.QuadId);

        }

        #endregion

        #endregion

        #region GetHashCode()

        /// <summary>
        /// Returns the HashCode of this object.
        /// </summary>
        public override Int32 GetHashCode()
        {
            return QuadId.GetHashCode();
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Shows information on this quad.
        /// </summary>
        public override String ToString()
        {
            return String.Format("Id {0}: {1} -{2}-> {3} [{4}]", QuadId.ToString(), Subject.ToString(), Predicate.ToString(), Object.ToString(), Context.ToString());
        }

        #endregion

    }

}
