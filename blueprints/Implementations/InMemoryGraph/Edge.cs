/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using System;
using System.Collections.Generic;
using de.ahzf.blueprints.datastructures;

#endregion

namespace de.ahzf.blueprints.InMemoryGraph
{

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    public class Edge : AElement, IEdge
    {

        #region Data

        protected readonly IVertex _OutVertex;
        protected readonly IVertex _InVertex;

        #endregion

        #region Properties

        #region Id

        /// <summary>
        /// An identifier that is unique to its inheriting class.
        /// All vertices of a graph must have unique identifiers.
        /// All edges of a graph must have unique identifiers.
        /// </summary>
        public EdgeId Id
        {
            get
            {

                Object _Object = null;

                if (_Properties.TryGetValue(__Id, out _Object))
                    return _Object as EdgeId;

                return null;

            }
        }

        #endregion

        #endregion

        #region Protected Constructor(s)

        #region Edge(myId, myEdgeInitializer = null)

        internal protected Edge(IVertex myOutVertex, IVertex myInVertex, EdgeId myId, Action<IEdge> myEdgeInitializer = null)
            : base(myId)
        {

            _OutVertex = myOutVertex;
            _InVertex  = myInVertex;

            if (myEdgeInitializer != null)
                myEdgeInitializer(this);

        }

        #endregion

        #endregion



        public IVertex OutVertex
        {
            get
            {
                return _OutVertex;
            }
        }

        public IVertex InVertex
        {
            get
            {
                return _InVertex;
            }
        }

        public string Label
        {
            get { throw new NotImplementedException(); }
        }

        public new System.Collections.IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }

}
