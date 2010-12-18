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
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    public class Vertex : AElement, IVertex
    {

        #region Data

        protected readonly HashSet<IEdge> _OutEdges;
        protected readonly HashSet<IEdge> _InEdges;

        #endregion

        #region Properties

        #region Id

        /// <summary>
        /// An identifier that is unique to its inheriting class.
        /// All vertices of a graph must have unique identifiers.
        /// All edges of a graph must have unique identifiers.
        /// </summary>
        public VertexId Id
        {
            get
            {

                Object _Object = null;

                if (_Properties.TryGetValue(__Id, out _Object))
                    return _Object as VertexId;

                return null;

            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region Vertex()

        public Vertex()
        { }

        #endregion

        #region Vertex(myIGraph, myVertexId)

        internal protected Vertex(IGraph myIGraph, VertexId myVertexId, Action<IVertex> myVertexInitializer = null)
            : base(myIGraph, myVertexId)
        {
            
            _OutEdges = new HashSet<IEdge>();
            _InEdges  = new HashSet<IEdge>();

            if (myVertexInitializer != null)
                myVertexInitializer(this);

        }

        #endregion

        #endregion


        #region OutEdges

        public IEnumerable<IEdge> OutEdges
        {
            get
            {
                return _OutEdges;
            }
        }

        #endregion

        #region RemoveOutEdge(myIEdge)

        public void RemoveOutEdge(IEdge myIEdge)
        {
            lock (this)
            {
                _OutEdges.Remove(myIEdge);
            }
        }

        #endregion

        #region InEdges

        public IEnumerable<IEdge> InEdges
        {
            get
            {
                return _InEdges;
            }
        }

        #endregion

        #region RemoveInEdge(myIEdge)

        public void RemoveInEdge(IEdge myIEdge)
        {
            lock (this)
            {
                _InEdges.Remove(myIEdge);
            }
        }

        #endregion

    }

}
