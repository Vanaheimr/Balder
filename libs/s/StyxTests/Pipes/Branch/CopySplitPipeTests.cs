/*
 * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Styx <http://www.github.com/Vanaheimr/Styx>
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region Usings

using System;
using System.Linq;

using NUnit.Framework;
using System.Collections.Generic;

#endregion

namespace de.ahzf.Vanaheimr.Styx.UnitTests.Pipes
{

    [TestFixture]
    public class CopySplitPipeTest
    {

        //#region testActionPipeNormal()

        //[Test]
        //public void testActionPipeNormal()
        //{

        //    var _Graph = TinkerGraphFactory.createTinkerGraph();
        //    Pipe pipe1 = new Pipeline(new OutEdgesPipe(), new InVertexPipe());
        //    Pipe pipe2 = new Pipeline(new OutEdgesPipe(), new InVertexPipe());
        //    Pipe pipe3 = new Pipeline(new OutEdgesPipe(), new InVertexPipe());


        //    CopySplitPipe<Vertex> copySplitPipe = new CopySplitPipe<Vertex>(Arrays.asList(pipe1, pipe2, pipe3));
        //    FairMergePipe<Vertex> fairMergePipe = new FairMergePipe<Vertex>(copySplitPipe.getPipes());
        //    copySplitPipe.setStarts(graph.getVertices());
        //    assertEquals(PipeHelper.counter(fairMergePipe), PipeHelper.counter(graph.getVertices().iterator()) * 3);
        //}

        //#endregion

        //public void testFairMerge2()
        //{

        //    Graph graph = TinkerGraphFactory.createTinkerGraph();
        //    Pipe pipe1 = new Pipeline(new OutEdgesPipe("knows"), new LabelPipe());
        //    Pipe pipe2 = new Pipeline(new OutEdgesPipe("created"), new LabelPipe());

        //    CopySplitPipe<Vertex> copySplitPipe = new CopySplitPipe<Vertex>(Arrays.asList(pipe1, pipe2));
        //    FairMergePipe<Vertex> exhaustiveMergePipe = new FairMergePipe<Vertex>(copySplitPipe.getPipes());
        //    copySplitPipe.setStarts(new SingleIterator<Vertex>(graph.getVertex(1)));
        //    List list = new ArrayList();
        //    PipeHelper.fillCollection(exhaustiveMergePipe.iterator(), list);
        //    assertEquals(list.get(0), "knows");
        //    assertEquals(list.get(1), "created");
        //    assertEquals(list.get(2), "knows");
        //    assertEquals(list.size(), 3);

        //}

        //public void testExhaustiveMerge()
        //{

        //    Graph graph = TinkerGraphFactory.createTinkerGraph();
        //    Pipe pipe1 = new Pipeline(new OutEdgesPipe("knows"), new LabelPipe());
        //    Pipe pipe2 = new Pipeline(new OutEdgesPipe("created"), new LabelPipe());

        //    CopySplitPipe<Vertex> copySplitPipe = new CopySplitPipe<Vertex>(Arrays.asList(pipe1, pipe2));
        //    ExhaustiveMergePipe<Vertex> exhaustiveMergePipe = new ExhaustiveMergePipe<Vertex>(copySplitPipe.getPipes());
        //    copySplitPipe.setStarts(new SingleIterator<Vertex>(graph.getVertex(1)));
        //    List list = new ArrayList();
        //    PipeHelper.fillCollection(exhaustiveMergePipe.iterator(), list);
        //    assertEquals(list.get(0), "knows");
        //    assertEquals(list.get(1), "knows");
        //    assertEquals(list.get(2), "created");
        //    assertEquals(list.size(), 3);

        //}

        //public void testBasicNext()
        //{

        //    Graph graph = TinkerGraphFactory.createTinkerGraph();
        //    Pipe pipe1 = new Pipeline(new OutEdgesPipe("knows"), new LabelPipe());
        //    Pipe pipe2 = new Pipeline(new OutEdgesPipe("created"), new LabelPipe());


        //    CopySplitPipe<Vertex> copySplitPipe = new CopySplitPipe<Vertex>(Arrays.asList(pipe1, pipe2));
        //    copySplitPipe.setStarts(graph.getVertices());
        //    int counter = 0;
        //    Set<Vertex> set = new HashSet<Vertex>();
        //    for (Vertex vertex : copySplitPipe) {
        //        counter++;
        //        set.add(vertex);
        //    }
        //    assertEquals(counter, 6);
        //    assertEquals(set.size(), 6);

        //}

    }

}
