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

    public static class INotificationExtentions
    {

        public static void SendTo<T>(this INotification<T> INotification, ITarget<T> Target)
        {
            INotification.OnNotification += Target.ProcessNotification;
            INotification.OnError        += Target.ProcessError;
            INotification.OnCompleted    += Target.ProcessCompleted;
        }

        public static void SendTo<T1, T2>(this INotification<T1, T2> INotification, ITarget<T1, T2> Target)
        {
            INotification.OnNotification += Target.ProcessNotification;
            INotification.OnError        += Target.ProcessError;
            INotification.OnCompleted    += Target.ProcessCompleted;
        }

        public static void SendTo<T1, T2, T3>(this INotification<T1, T2, T3> INotification, ITarget<T1, T2, T3> Target)
        {
            INotification.OnNotification += Target.ProcessNotification;
            INotification.OnError        += Target.ProcessError;
            INotification.OnCompleted    += Target.ProcessCompleted;
        }

        public static void SendTo<T1, T2, T3, T4>(this INotification<T1, T2, T3, T4> INotification, ITarget<T1, T2, T3, T4> Target)
        {
            INotification.OnNotification += Target.ProcessNotification;
            INotification.OnError        += Target.ProcessError;
            INotification.OnCompleted    += Target.ProcessCompleted;
        }

        public static void SendTo<T1, T2, T3, T4, T5>(this INotification<T1, T2, T3, T4, T5> INotification, ITarget<T1, T2, T3, T4, T5> Target)
        {
            INotification.OnNotification += Target.ProcessNotification;
            INotification.OnError        += Target.ProcessError;
            INotification.OnCompleted    += Target.ProcessCompleted;
        }

    }

    public delegate void NotificationEventHandler<T>                 (T  Message);
    public delegate void NotificationEventHandler<T1, T2>            (T1 Message1, T2 Message2);
    public delegate void NotificationEventHandler<T1, T2, T3>        (T1 Message1, T2 Message2, T3 Message3);
    public delegate void NotificationEventHandler<T1, T2, T3, T4>    (T1 Message1, T2 Message2, T3 Message3, T4 Message4);
    public delegate void NotificationEventHandler<T1, T2, T3, T4, T5>(T1 Message1, T2 Message2, T3 Message3, T4 Message4, T5 Message5);

    public delegate void VotingEventHandler<T, V>                 (T  Message, IVote<V> Vote);
    public delegate void VotingEventHandler<T1, T2, V>            (T1 Message1, T2 Message2, IVote<V> Vote);
    public delegate void VotingEventHandler<T1, T2, T3, V>        (T1 Message1, T2 Message2, T3 Message3, IVote<V> Vote);
    public delegate void VotingEventHandler<T1, T2, T3, T4, V>    (T1 Message1, T2 Message2, T3 Message3, T4 Message4, IVote<V> Vote);
    public delegate void VotingEventHandler<T1, T2, T3, T4, T5, V>(T1 Message1, T2 Message2, T3 Message3, T4 Message4, T5 Message5, IVote<V> Vote);

    public delegate void ExceptionEventHandler(dynamic Sender, Exception ExceptionMessage);
    public delegate void CompletedEventHandler(dynamic Sender, String Message);



    public interface INotification
    {
        event ExceptionEventHandler OnError;
        event CompletedEventHandler OnCompleted;
    }

    public interface INotification<T> : INotification
    {
        event NotificationEventHandler<T> OnNotification;
    }

    public interface INotification<T1, T2> : INotification
    {
        event NotificationEventHandler<T1, T2> OnNotification;
    }

    public interface INotification<T1, T2, T3> : INotification
    {
        event NotificationEventHandler<T1, T2, T3> OnNotification;
    }

    public interface INotification<T1, T2, T3, T4> : INotification
    {
        event NotificationEventHandler<T1, T2, T3, T4> OnNotification;
    }

    public interface INotification<T1, T2, T3, T4, T5> : INotification
    {
        event NotificationEventHandler<T1, T2, T3, T4, T5> OnNotification;
    }




    public interface IVotingNotification<T, V> : INotification<T>
    {
        event VotingEventHandler<T, V> OnVoting;
    }

    public interface IVotingNotification<T1, T2, V> : INotification<T1, T2>
    {
        event VotingEventHandler<T1, T2, V> OnVoting;
    }

    public interface IVotingNotification<T1, T2, T3, V> : INotification<T1, T2, T3>
    {
        event VotingEventHandler<T1, T2, T3, V> OnVoting;
    }

    public interface IVotingNotification<T1, T2, T3, T4, V> : INotification<T1, T2, T3, T4>
    {
        event VotingEventHandler<T1, T2, T3, T4, V> OnVoting;
    }

    public interface IVotingNotification<T1, T2, T3, T4, T5, V> : INotification<T1, T2, T3, T4, T5>
    {
        event VotingEventHandler<T1, T2, T3, T4, T5, V> OnVoting;
    }


}
