/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using System.Collections.Generic;

#endregion

namespace de.ahzf.blueprints
{

    /// <summary>
    /// A vertex maintains pointers to both a set of incoming and outgoing edges.
    /// The outgoing edges are those edges for which the vertex is the tail.
    /// The incoming edges are those edges for which the vertex is the head.
    /// Diagrammatically, ---inEdges---> vertex ---outEdges--->.
    /// </summary>
    public interface IVertex : IElement
    {

        /// <summary>
        /// The edges emanating from, or leaving, the vertex.
        /// </summary>
        IEnumerable<IEdge> OutEdges { get; }


        /// <summary>
        /// The edges incoming to, or arriving at, the vertex.
        /// </summary>
        IEnumerable<IEdge> InEdges { get; }


    }

}
