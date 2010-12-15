/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using System;
using System.Collections.Generic;

#endregion

namespace de.ahzf.blueprints.InMemory
{

    /// <summary>
    /// A graph is a container object for a collection of vertices and edges.
    /// </summary>
    public class InMemoryGraph : IGraph
    {

        public IVertex AddVertex(object myId)
        {
            throw new NotImplementedException();
        }

        public IVertex GetVertex(object myId)
        {
            throw new NotImplementedException();
        }

        public void RemoveVertex(IVertex myIVertex)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IVertex> Vertices
        {
            get { throw new NotImplementedException(); }
        }

        public IEdge AddEdge(object myId, IVertex myOutVertex, IVertex myInVertex, string myLabel)
        {
            throw new NotImplementedException();
        }

        public IEdge GetEdge(object myId)
        {
            throw new NotImplementedException();
        }

        public void RemoveEdge(IEdge myIEdge)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEdge> Edges
        {
            get { throw new NotImplementedException(); }
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }

    }

}
