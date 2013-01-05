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
using de.ahzf.Illias.Commons.Votes;

#endregion

namespace de.ahzf.Vanaheimr.Styx
{

    public interface INotificator<T> : INotification<T>
    {
        void SendNotification(T Message);
    }

    public interface INotificator<T1, T2> : INotification<T1, T2>
    {
        void SendNotification(T1 Message1, T2 Message2);
    }

    public interface INotificator<T1, T2, T3> : INotification<T1, T2, T3>
    {
        void SendNotification(T1 Message1, T2 Message2, T3 Message3);
    }

    public interface INotificator<T1, T2, T3, T4> : INotification<T1, T2, T3, T4>
    {
        void SendNotification(T1 Message1, T2 Message2, T3 Message3, T4 Message4);
    }

    public interface INotificator<T1, T2, T3, T4, T5> : INotification<T1, T2, T3, T4, T5>
    {
        void SendNotification(T1 Message1, T2 Message2, T3 Message3, T4 Message4, T5 Message5);
    }


    public interface IVotingNotificator<T, V> : INotificator<T>, IVotingNotification<T, V>
    {
        V SendVoting(T Message);
    }

    public interface IVotingNotificator<T1, T2, V> : INotificator<T1, T2>, IVotingNotification<T1, T2, V>
    {
        V SendVoting(T1 Message1, T2 Message2);
    }

    public interface IVotingNotificator<T1, T2, T3, V> : INotificator<T1, T2, T3>, IVotingNotification<T1, T2, T3, V>
    {
        V SendVoting(T1 Message1, T2 Message2, T3 Message3);
    }

    public interface IVotingNotificator<T1, T2, T3, T4, V> : INotificator<T1, T2, T3, T4>, IVotingNotification<T1, T2, T3, T4, V>
    {
        V SendVoting(T1 Message1, T2 Message2, T3 Message3, T4 Message4);
    }

    public interface IVotingNotificator<T1, T2, T3, T4, T5, V> : INotificator<T1, T2, T3, T4, T5>, IVotingNotification<T1, T2, T3, T4, T5, V>
    {
        V SendVoting(T1 Message1, T2 Message2, T3 Message3, T4 Message4, T5 Message5);
    }







    public abstract class ANotificator : INotification
    {

        public event ExceptionEventHandler OnError;

        public event CompletedEventHandler OnCompleted;

        public void SignalError(dynamic Sender, Exception Exception)
        {
            if (this.OnError != null)
                this.OnError(Sender, Exception);
        }

        public void SignalCompleted(dynamic Sender, String Message)
        {
            if (this.OnCompleted != null)
                this.OnCompleted(Sender, Message);
        }

    }


    public class Notificator<T> : ANotificator, INotificator<T>, INotification<T>
    {

        public event NotificationEventHandler<T> OnNotification;

        public void SendNotification(T Message)
        {
            if (this.OnNotification != null)
                this.OnNotification(Message);
        }

    }


    public class VotingNotificator<T, V> : Notificator<T>, IVotingNotificator<T, V>, IVotingNotification<T, V>
    {

        #region Data

        private readonly Func<IVote<V>> VoteCreator;
        private readonly V              DefaultValue;

        #endregion

        #region Events

        public event VotingEventHandler<T, V> OnVoting;

        #endregion


        public VotingNotificator(Func<IVote<V>> VoteCreator, V DefaultValue)
        {

            if (VoteCreator == null)
                throw new ArgumentNullException("VoteCreator", "The given VoteCreator delegate must not be null!");

            this.VoteCreator  = VoteCreator;
            this.DefaultValue = DefaultValue;

        }

        public V SendVoting(T Message)
        {

            if (this.OnVoting == null)
                return DefaultValue;

            var Vote = VoteCreator();
            this.OnVoting(Message, Vote);
            return Vote.Result;

        }

    }


    public class Notificator<T1, T2> : ANotificator, INotificator<T1, T2>, INotification<T1, T2>
    {

        public event NotificationEventHandler<T1, T2> OnNotification;

        public void SendNotification(T1 Message1, T2 Message2)
        {
            if (this.OnNotification != null)
                this.OnNotification(Message1, Message2);
        }

    }


    public class VotingNotificator<T1, T2, V> : Notificator<T1, T2>, IVotingNotificator<T1, T2, V>, IVotingNotification<T1, T2, V>
    {

        #region Data

        private readonly Func<IVote<V>> VoteCreator;
        private readonly V              DefaultValue;

        #endregion

        #region Events

        public event VotingEventHandler<T1, T2, V> OnVoting;

        #endregion


        public VotingNotificator(Func<IVote<V>> VoteCreator, V DefaultValue)
        {

            if (VoteCreator == null)
                throw new ArgumentNullException("VoteCreator", "The given VoteCreator delegate must not be null!");

            this.VoteCreator  = VoteCreator;
            this.DefaultValue = DefaultValue;

        }

        public V SendVoting(T1 Message1, T2 Message2)
        {

            if (this.OnVoting == null)
                return DefaultValue;

            var Vote = VoteCreator();
            this.OnVoting(Message1, Message2, Vote);
            return Vote.Result;

        }

    }

}
