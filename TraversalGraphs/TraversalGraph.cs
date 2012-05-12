using System;
using System.Linq;
using System.Collections.Generic;

using de.ahzf.Illias.Commons;
using de.ahzf.Blueprints.PropertyGraphs;

namespace de.ahzf.Vanaheimr.Blueprints.TraversalGraphs
{

    public enum TraversalGraphType
    {
        Adjacencylist,
        IncidenceList,
        InvertedIncidenceList
    }

    public class TraversalGraph
    {

        #region Data

        private UInt64[][] _ListGraph;

        private IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                      UInt64, Int64, String, String, Object,
                                      UInt64, Int64, String, String, Object,
                                      UInt64, Int64, String, String, Object> Graph;

        #endregion

        #region TraversalGraph(Graph, TraversalGraphType)

        public TraversalGraph(IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                    UInt64, Int64, String, String, Object,
                                                    UInt64, Int64, String, String, Object,
                                                    UInt64, Int64, String, String, Object> Graph,
                              TraversalGraphType TraversalGraphType)
        {

            this.Graph = Graph;

            _ListGraph = new UInt64[Graph.NumberOfVertices()][];

            Graph.Vertices().ForEach(v => {
                _ListGraph[v.Id - 1] = (from e in v.OutEdges() select e.Id).ToArray();
            });

        }

        #endregion


        public void ForEachVertex(Action<UInt64[]> Action)
        {
            //_ListGraph.ForEach(vid => { });
            for (var i = 0UL; i < this.Graph.NumberOfVertices(); i++)
                Action(_ListGraph[i]);

        }

    }

}
