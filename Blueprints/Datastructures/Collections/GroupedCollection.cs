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
using System.Linq;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Blueprints
{

    public class GroupedCollection<TGroup, TId, TValue> : IGroupedCollection<TGroup, TId, TValue>
    {

        private readonly IDictionary<TId,    TValue>             Ids;
        private readonly IDictionary<TGroup, LinkedList<TValue>> Groups;
        private readonly LinkedList<TValue>                      DefaultGroup;

        public GroupedCollection()
        {
            Ids          = new Dictionary<TId, TValue>();
            Groups       = new Dictionary<TGroup, LinkedList<TValue>>();
            DefaultGroup = new LinkedList<TValue>();
        }

        public Boolean TryAddValue(TGroup Group, TId Id, TValue Value)
        {

            Ids.Add(Id, Value);

            if (Group == null || Group.Equals(default(TGroup)))
                DefaultGroup.AddLast(Value);

            else
            {

                LinkedList<TValue> _Group = null;

                if (Groups.TryGetValue(Group, out _Group))
                {
                    try
                    {
                        _Group.AddLast(Value);
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }

            }

            return true;

        }

        public Boolean TryGetById(TId Id, out TValue Value)
        {
            return Ids.TryGetValue(Id, out Value);
        }


        public Boolean TryGetByGroup(TGroup Group, out IEnumerable<TValue> Entries)
        {

            if (Group == null || Group.Equals(default(TGroup)))
            {
                Entries = DefaultGroup;
                return true;
            }

            else
            {

                LinkedList<TValue> _Group = null;

                if (Groups.TryGetValue(Group, out _Group))
                {
                    Entries = _Group;
                    return true;
                }

            }

            Entries = new TValue[0];

            return false;

        }



        public IEnumerator<TValue> GetEnumerator()
        {

            foreach (var Value in DefaultGroup)
                yield return Value;

            foreach (var Group in Groups.Values)
                foreach (var Value in Group)
                    yield return Value;

        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {

            foreach (var Value in DefaultGroup)
                yield return Value;

            foreach (var Group in Groups.Values)
                foreach (var Value in Group)
                    yield return Value;

        }




        public Boolean TryRemoveValue(TGroup Group, TId Id, TValue Value)
        {
            throw new NotImplementedException();
        }





        public void Clear()
        {
            DefaultGroup.Clear();
            Groups.Clear();
        }


        public Boolean ContainsId(TId Id)
        {
            return Ids.ContainsKey(Id);
        }

    }

}
