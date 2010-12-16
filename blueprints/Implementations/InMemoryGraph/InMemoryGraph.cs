/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using System;
using System.Collections.Generic;
using de.ahzf.blueprints.Datastructures;

#endregion

namespace de.ahzf.blueprints.InMemoryGraph
{

    /// <summary>
    /// A graph is a container object for a collection of vertices and edges.
    /// </summary>
    public class InMemoryGraph : IGraph
    {

        private readonly IDictionary<VertexId, IVertex> _Vertices;
        private readonly IDictionary<EdgeId,   IEdge>   _Edges;


        public InMemoryGraph()
        {
            _Vertices = new SortedDictionary<VertexId, IVertex>();
            _Edges    = new SortedDictionary<EdgeId,   IEdge>();
        }


        public IVertex AddVertex(VertexId myVertexId = null, Action<IVertex> myVertexInitializer = null)
        {

            if (myVertexId == null)
                myVertexId = new VertexId(Guid.NewGuid().ToString());

            IVertex _IVertex = null;

            if (_Vertices.TryGetValue(myVertexId, out _IVertex))
                throw new ArgumentException("Another vertex with id " + myVertexId + " already exists");

            _IVertex = new Vertex(myVertexId, myVertexInitializer);
            _Vertices.Add(myVertexId, _IVertex);

            return _IVertex;

        }

        public IVertex GetVertex(VertexId myId)
        {
            
            IVertex _IVertex = null;

            _Vertices.TryGetValue(myId, out _IVertex);

            return _IVertex;

        }

        public void RemoveVertex(IVertex myIVertex)
        {
            
            if (_Vertices.ContainsKey(myIVertex.Id))
                _Vertices.Remove(myIVertex.Id);

        }

        public IEnumerable<IVertex> GetVertices(Func<IVertex, Boolean> myVertexFilter = null)
        {

            if (myVertexFilter == null)
                foreach (var _IVertex in _Vertices.Values)
                    yield return _IVertex;

            foreach (var _IVertex in _Vertices.Values)
                if (myVertexFilter(_IVertex))
                    yield return _IVertex;

        }





        public IEdge AddEdge(IVertex myOutVertex, IVertex myInVertex, EdgeId myEdgeId = null, String myLabel = null, Action<IEdge> myEdgeInitializer = null)
        {

            if (myEdgeId == null)
                myEdgeId = new EdgeId(Guid.NewGuid().ToString());

            IEdge _IEdge = null;

            if (_Edges.TryGetValue(myEdgeId, out _IEdge))
                throw new ArgumentException("Another edge with id " + myEdgeId + " already exists");

            _IEdge = new Edge(myOutVertex, myInVertex, myEdgeId, myEdgeInitializer);
            _Edges.Add(myEdgeId, _IEdge);

            return _IEdge;

        }

        public IEdge GetEdge(EdgeId myId)
        {

            IEdge _IEdge = null;

            _Edges.TryGetValue(myId, out _IEdge);

            return _IEdge;

        }

        public void RemoveEdge(IEdge myIEdge)
        {

            if (_Edges.ContainsKey(myIEdge.Id))
                _Edges.Remove(myIEdge.Id);

        }

        public IEnumerable<IEdge> GetEdges(Func<IEdge, Boolean> myEdgeFilter = null)
        {

            if (myEdgeFilter == null)
                foreach (var _IEdge in _Edges.Values)
                    yield return _IEdge;

            foreach (var _IEdge in _Edges.Values)
                if (myEdgeFilter(_IEdge))
                    yield return _IEdge;

        }





        public void Clear()
        {
            _Vertices.Clear();
            _Edges.Clear();
        }

        public void Shutdown()
        {
            Clear();
        }

    }

}
