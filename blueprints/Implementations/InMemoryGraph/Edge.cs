/*
 * Copyright (c) 2010, Achim 'ahzf' Friedland <code@ahzf.de>
 * 
 * This file is part of blueprints.NET and licensed
 * as free software under the New BSD License.
 */

#region Usings

using System;
using System.Collections;
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
    public class Edge : AElement, IEdge
    {

        #region Data

        protected readonly IVertex _OutVertex;
        protected readonly IVertex _InVertex;
        public    const    String  __Label = "Label";

        #endregion

        #region Properties

        // Edge properties

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

        #region Label

        public String Label
        {
            get
            {

                Object _Object = null;

                if (_Properties.TryGetValue(__Label, out _Object))
                    return _Object as String;

                return null;

            }
        }

        #endregion


        // Links to the associated vertices

        #region OutVertex

        public IVertex OutVertex
        {
            get
            {
                return _OutVertex;
            }
        }

        #endregion

        #region InVertex

        public IVertex InVertex
        {
            get
            {
                return _InVertex;
            }
        }

        #endregion

        #endregion

        #region Internal Protected Constructor(s)

        #region Edge(myOutVertex, myInVertex, myId, myEdgeInitializer = null)

        internal protected Edge(IVertex myOutVertex, IVertex myInVertex, EdgeId myId, Action<IEdge> myEdgeInitializer = null)
            : base(myId)
        {

            if (myOutVertex == null)
                throw new ArgumentNullException("The OutVertex must not be null!");

            if (myInVertex == null)
                throw new ArgumentNullException("The InVertex must not be null!");

            _OutVertex = myOutVertex;
            _InVertex  = myInVertex;

            if (myEdgeInitializer != null)
                myEdgeInitializer(this);

        }

        #endregion

        #endregion

    }

}
