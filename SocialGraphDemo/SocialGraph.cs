/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using System;
using de.ahzf.blueprints.InMemoryGraph;
using de.ahzf.blueprints.Datastructures;
using de.ahzf.blueprints;

#endregion

namespace SocialGraphDemo
{

    public class SocialGraph
    {

        #region User

        public class User : Vertex
        {

            #region Properties

            #region Name

            public String Name
            {
                get
                {

                    Object _Object = null;

                    if (_Properties.TryGetValue("Name", out _Object))
                        return _Object as String;

                    return null;

                }
            }

            #endregion

            #endregion

            #region Constructor(s)

            #region User()

            public User()
            { }

            #endregion

            #region User(myIGraph, myId, myVertexInitializer = null)

            public User(IGraph myIGraph, VertexId myId, Action<IVertex> myVertexInitializer = null)
                : base (myIGraph, myId, myVertexInitializer)
            { }

            #endregion

            #endregion

        }

        #endregion

        #region Classmates

        public class Classmates : Edge
        {

            #region Properties

            #region School

            public String School
            {
                get
                {

                    Object _Object = null;

                    if (_Properties.TryGetValue("School", out _Object))
                        return _Object as String;

                    return null;

                }
            }

            #endregion

            #endregion

            #region Constructor(s)

            #region Classmates()

            public Classmates()
            { }

            #endregion

            #region Classmates(myIGraph, myEdgeId, myEdgeInitializer = null)

            public Classmates(IGraph myIGraph, IVertex myOutVertex, IVertex myInVertex, EdgeId myEdgeId, Action<IEdge> myEdgeInitializer = null)
                : base(myIGraph, myOutVertex, myInVertex, myEdgeId, myEdgeInitializer)
            { }

            #endregion

            #endregion

        }

        #endregion


        public static void Main(String[] myArgs)
        {
            
            var _SocialGraph1 = new InMemoryGraph();

            var v1  = _SocialGraph1.AddVertex(new VertexId(1));
            var v2  = _SocialGraph1.AddVertex(new VertexId("55"));
            var v3  = _SocialGraph1.AddVertex(new VertexId("3"));
            var v4  = _SocialGraph1.AddVertex<User>(new VertexId(40), v => v.SetProperty("Name", "klaus"));
            var v5  = _SocialGraph1.AddVertex(new VertexId(42), v => v.SetProperty("Name", "klaus"));

            var e1  = _SocialGraph1.AddEdge(v1, v2, new EdgeId("e1"), "edge 1->2");
            var e2  = _SocialGraph1.AddEdge(v1, v3, new EdgeId("e2"), "edge 1->3", e => e.SetProperty("ort", "norwegen"));
            var e3  = _SocialGraph1.AddEdge<Classmates>(v2, v3, new EdgeId("e3"), "edge 2->3", e => e.SetProperty("School", "New School"));

            var all = _SocialGraph1.GetVertices(v => v.Id > new VertexId(10));

        }

    }

}
