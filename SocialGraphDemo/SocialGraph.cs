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

#endregion

namespace SocialGraphDemo
{

    public class SocialGraph
    {

        
        
        public static void Main(String[] myArgs)
        {
            
            var _SocialGraph1 = new InMemoryGraph();

            var v1  = _SocialGraph1.AddVertex(new VertexId(1));
            var v2  = _SocialGraph1.AddVertex(new VertexId("55"));
            var v3  = _SocialGraph1.AddVertex(new VertexId("3"));
            var v4  = _SocialGraph1.AddVertex(new VertexId(40));
            var v5  = _SocialGraph1.AddVertex(new VertexId(42), (v) => v.SetProperty("name", "klaus"));

            var e1  = _SocialGraph1.AddEdge(v1, v2, new EdgeId("e1"), "edge 1->2");
            var e2  = _SocialGraph1.AddEdge(v1, v3, new EdgeId("e2"), "edge 1->3", e => e.SetProperty("ort", "norwegen"));

            var all = _SocialGraph1.GetVertices(v => v.Id > new VertexId(10));

        }

    }

}
