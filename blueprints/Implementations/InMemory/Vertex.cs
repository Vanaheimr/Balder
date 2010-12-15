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

        #region Protected Constructor(s)

        #region Vertex()

        protected Vertex()
            : base()
        {
            _OutEdges = new HashSet<IEdge>();
            _InEdges  = new HashSet<IEdge>();
        }

        #endregion

        #region Vertex(myId)

        protected Vertex(IComparable myId)
            : base(myId)
        {
            _OutEdges = new HashSet<IEdge>();
            _InEdges  = new HashSet<IEdge>();
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

        #region InEdges

        public IEnumerable<IEdge> InEdges
        {
            get
            {
                return _InEdges;
            }
        }

        #endregion

    }

}
