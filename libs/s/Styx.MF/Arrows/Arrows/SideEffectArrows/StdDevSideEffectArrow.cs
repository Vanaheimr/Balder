/*
 * Copyright (c) 2011-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
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
using System.Threading;

#if SILVERLIGHT
using de.ahzf.Silverlight;
#endif

#endregion

namespace de.ahzf.Styx
{

    /// <summary>
    /// The StdDevSideEffectArrow produces a side effect that
    /// is the sliding standard deviation and the average of
    /// messages/objects that have passed through it.
    /// </summary>
    public class StdDevSideEffectArrow : AbstractSideEffectArrow<Double, Double, Double, Double>
    {

        #region Data

        private Int64  Counter;
        private Double Sum;
        private Double QuadratSum;

        #endregion

        #region Properties

        #region StdDev

        /// <summary>
        /// The standard deviation of the processed messages.
        /// </summary>
        public Double StdDev
        {
            get
            {
                return Math.Sqrt(SideEffect1);
            }
        }

        #endregion

        #region Variance

        /// <summary>
        /// The variance of the processed messages.
        /// </summary>
        public Double Variance
        {
            get
            {
                return SideEffect1;
            }
        }

        #endregion

        #region Average

        /// <summary>
        /// The average of the processed messages.
        /// </summary>
        public Double Average
        {
            get
            {
                return SideEffect2;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region StdDevSideEffectArrow(MessageRecipients.Recipient, params MessageRecipients.Recipients)

        /// <summary>
        /// The StdDevSideEffectArrow produces a side effect that
        /// is the sliding standard deviation and the average of
        /// messages/objects that have passed through it.
        /// </summary>
        public StdDevSideEffectArrow()
        { }

        #endregion

        #region StdDevSideEffectArrow(MessageRecipients.Recipient, params MessageRecipients.Recipients)

        /// <summary>
        /// The StdDevSideEffectArrow produces a side effect that
        /// is the sliding standard deviation and the average of
        /// messages/objects that have passed through it.
        /// </summary>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">The recipients of the processed messages.</param>
        public StdDevSideEffectArrow(MessageRecipient<Double> Recipient, params MessageRecipient<Double>[] Recipients)
            : base(Recipient, Recipients)
        { }

        #endregion

        #region StdDevSideEffectArrow(IArrowReceiver.Recipient, params IArrowReceiver.Recipients)

        /// <summary>
        /// The StdDevSideEffectArrow produces a side effect that
        /// is the sliding standard deviation and the average of
        /// messages/objects that have passed through it.
        /// </summary>
        /// <param name="Recipient">A recipient of the processed messages.</param>
        /// <param name="Recipients">The recipients of the processed messages.</param>
        public StdDevSideEffectArrow(IArrowReceiver<Double> Recipient, params IArrowReceiver<Double>[] Recipients)
            : base(Recipient, Recipients)
        { }

        #endregion

        #endregion


        #region (private) AddToSum(Summand)

        private Double AddToSum(Double Summand)
        {
            
            Double _InitialValue, _NewLocalSum;

            do
            {
                // Save the current Sum locally.
                _InitialValue = Sum;

                // Add the Summand to the Sum.
                _NewLocalSum  = _InitialValue + Summand;

            }
            // If Sum still equals _InitialValue, exchange it with _NewLocalSum
#if SILVERLIGHT
            while (_InitialValue != SilverlightTools.CompareExchange(ref Sum, _NewLocalSum, _InitialValue));
#else
            while (_InitialValue != Interlocked.CompareExchange(ref Sum, _NewLocalSum, _InitialValue));
#endif

            // Return _NewLocalSum as Sum may already be alternated
            // by a concurrent thread between the end of the loop
            // and the function returns.
            return _NewLocalSum;

        }

        #endregion

        #region (private) AddToQuadratSum(Summand)

        private Double AddToQuadratSum(Double Summand)
        {
            
            Double _InitialValue, _NewLocalSum;

            do
            {
                // Save the current Sum locally.
                _InitialValue = QuadratSum;

                // Add the Summand^2 to the Sum.
                _NewLocalSum  = _InitialValue + Summand * Summand;

            }
            // If Sum still equals _InitialValue, exchange it with _NewLocalSum
#if SILVERLIGHT
            while (_InitialValue != SilverlightTools.CompareExchange(ref QuadratSum, _NewLocalSum, _InitialValue));
#else
            while (_InitialValue != Interlocked.CompareExchange(ref QuadratSum, _NewLocalSum, _InitialValue));
#endif

            // Return _NewLocalSum as Sum may already be alternated
            // by a concurrent thread between the end of the loop
            // and the function returns.
            return _NewLocalSum;

        }

        #endregion


        #region ProcessMessage(MessageIn, out MessageOut)

        /// <summary>
        /// Process the incoming message and return an outgoing message.
        /// </summary>
        /// <param name="MessageIn">The incoming message.</param>
        /// <param name="MessageOut">The outgoing message.</param>
        protected override Boolean ProcessMessage(Double MessageIn, out Double MessageOut)
        {

            MessageOut = MessageIn;
            var _Counter = Interlocked.Increment(ref Counter);

            var _Sum     = AddToSum(MessageOut);
            _SideEffect1 = AddToQuadratSum(MessageOut) - (Math.Pow(_Sum, 2) / _Counter);
            _SideEffect2 = _Sum / _Counter;

            if (Counter > 1 && Counter < 30)
                _SideEffect1 = _SideEffect1 / (_Counter - 1);  // corr. Var.
            else
                _SideEffect1 = _SideEffect1 / _Counter;

            return true;

        }

        #endregion

        #region ToString()

        /// <summary>
        /// Returns a string representation of this Arrow.
        /// </summary>
        public override String ToString()
        {
            return base.ToString() + "<Counter: " + Counter + ">";
        }

        #endregion

    }

}
