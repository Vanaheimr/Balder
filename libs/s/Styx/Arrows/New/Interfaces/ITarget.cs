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

    public interface ITarget
    {
        void ProcessError    (dynamic Sender, Exception ExceptionMessage);
        void ProcessCompleted(dynamic Sender, String    Message);
    }

    public interface ITarget<in T> : ITarget
    {
        void ProcessNotification(T Message);
    }

    public interface ITarget<in T1, in T2> : ITarget
    {
        void ProcessNotification(T1 Message1, T2 Message2);
    }

    public interface ITarget<in T1, in T2, in T3> : ITarget
    {
        void ProcessNotification(T1 Message1, T2 Message2, T3 Message3);
    }

    public interface ITarget<in T1, in T2, in T3, in T4> : ITarget
    {
        void ProcessNotification(T1 Message1, T2 Message2, T3 Message3, T4 Message4);
    }

    public interface ITarget<in T1, in T2, in T3, in T4, in T5> : ITarget
    {
        void ProcessNotification(T1 Message1, T2 Message2, T3 Message3, T4 Message4, T5 Message5);
    }



    public interface IVotingTarget<in T, V> : ITarget
    {
        V ProcessVoting(T Message, IVote<V> Vote);
    }

    public interface IVotingTarget<in T1, in T2, V> : ITarget<T1, T2>
    {
        V ProcessVoting(T1 Message1, T2 Message2, IVote<V> Vote);
    }

    public interface IVotingTarget<in T1, in T2, in T3, V> : ITarget<T1, T2, T3>
    {
        V ProcessVoting(T1 Message1, T2 Message2, T3 Message3, IVote<V> Vote);
    }

    public interface IVotingTarget<in T1, in T2, in T3, in T4, V> : ITarget<T1, T2, T3, T4>
    {
        V ProcessVoting(T1 Message1, T2 Message2, T3 Message3, T4 Message4, IVote<V> Vote);
    }

    public interface IVotingTarget<in T1, in T2, in T3, in T4, in T5, V> : ITarget<T1, T2, T3, T4, T5>
    {
        V ProcessVoting(T1 Message1, T2 Message2, T3 Message3, T4 Message4, T5 Message5, IVote<V> Vote);
    }

}
