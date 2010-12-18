/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using System;
using de.ahzf.blueprints;
using de.ahzf.blueprints.InMemoryGraph;
using de.ahzf.blueprints.Datastructures;

#endregion

namespace SocialGraphDemo
{

    public class SocialGraph
    {

        #region User

        /// <summary>
        /// A class representing a user vertex
        /// </summary>
        public class User : Vertex
        {

            #region Properties

            #region Name

            /// <summary>
            /// The name of the user
            /// </summary>
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

            #region User(myIGraph, myId, myVertexInitializer = null)

            /// <summary>
            /// Creates a new user vertex
            /// </summary>
            /// <param name="myIGraph">The associated graph</param>
            /// <param name="myVertexId">The Id of the vertex</param>
            /// <param name="myVertexInitializer"></param>
            public User(IGraph myIGraph, VertexId myVertexId, Action<IVertex> myVertexInitializer = null)
                : base (myIGraph, myVertexId, myVertexInitializer)
            { }

            #endregion

            #endregion

        }

        #endregion

        #region Classmates

        /// <summary>
        /// A class representing a classmate edge
        /// </summary>
        public class Classmates : Edge
        {

            #region Properties

            #region School

            /// <summary>
            /// Defining a school
            /// </summary>
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

            #region Classmates(myIGraph, myOutVertex, myIGraph, myEdgeId, myEdgeInitializer = null)

            /// <summary>
            /// Creates a new classmate edge
            /// </summary>
            /// <param name="myIGraph"></param>
            /// <param name="myOutVertex"></param>
            /// <param name="myInVertex"></param>
            /// <param name="myEdgeId">A EdgeId. If none was given a new one will be generated.</param>
            /// <param name="myEdgeInitializer">A delegate to initialize the newly generated edge.</param>
            public Classmates(IGraph myIGraph, IVertex myOutVertex, IVertex myInVertex, EdgeId myEdgeId, Action<IEdge> myEdgeInitializer = null)
                : base (myIGraph, myOutVertex, myInVertex, myEdgeId, "classmates", myEdgeInitializer)
            { }

            #endregion

            #endregion

        }

        #endregion


        public static void Main(String[] myArgs)
        {
            
            var _SocialGraph1 = new InMemoryGraph();

            var v1      = _SocialGraph1.AddVertex(new VertexId(1));
            var v2      = _SocialGraph1.AddVertex(new VertexId("55"));
            var v3      = _SocialGraph1.AddVertex(new VertexId("3"));
            var v4      = _SocialGraph1.AddVertex<User>(new VertexId(40), v => v.SetProperty("Name", "Klaus"));
            var v5      = _SocialGraph1.AddVertex(new VertexId(42), v => v.SetProperty("Name", "Klaus"));

            var e1      = _SocialGraph1.AddEdge(v1, v2, new EdgeId("e1"), "edge 1->2");
            var e2      = _SocialGraph1.AddEdge(v1, v4, new EdgeId("e2"), "edge 1->4", e => e.SetProperty("Place", "Norway"));
            var e3      = _SocialGraph1.AddEdge<Classmates>(v2, v3, new EdgeId("e3"), "edge 2->3", e => e.SetProperty("School", "New School"));

            var _v4_1   = _SocialGraph1.GetVertex(v4.Id);
            var _v4_2   = _SocialGraph1.GetVertex<Vertex>(v4.Id);
            var _v4_3   = _SocialGraph1.GetVertex<User>(v4.Id);

            var _v4dyn  = _SocialGraph1.GetVertex(v4.Id).ToDynamic();
            var Name    = _v4dyn.Name;

            var all = _SocialGraph1.GetVertices(v => v.Id > new VertexId(10));

        }

    }

}
