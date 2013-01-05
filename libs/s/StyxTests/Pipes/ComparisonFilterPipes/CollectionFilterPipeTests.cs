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
using System.Collections.Generic;

using NUnit.Framework;

#endregion

namespace de.ahzf.Vanaheimr.Styx.UnitTests.FilterPipes
{

    [TestFixture]
    public class CollectionFilterPipeTests
	{
		
		#region testBasicCollectionFilterEquals()

        [Test]
        public void testBasicCollectionFilterEquals()
        {	

			var _Names 		= new List<String>() { "marko", "marko", "peter", "josh", "pavel", "marko" };
	        var _Collection = new HashSet<String>() { "marko", "pavel" };
	        var _Pipe1 		= new CollectionFilterPipe<String>(_Collection, ComparisonFilter.NOT_EQUAL);
	        _Pipe1.SetSourceCollection(_Names);
			
	        var _Counter = 0;
	        while (_Pipe1.MoveNext())
			{
	            _Counter++;
	            var _Name = _Pipe1.Current;
	            Assert.IsTrue(_Name.Equals("marko") || _Name.Equals("pavel"));
	        }
	        
			Assert.AreEqual(4, _Counter);

		}

        #endregion
		
		#region testBasicCollectionFilterNotEquals()

        [Test]
        public void testBasicCollectionFilterNotEquals()
        {	

			var _Names 		= new List<String>() { "marko", "marko", "peter", "josh", "pavel", "marko" };
	        var _Collection = new HashSet<String>() { "marko", "pavel" };
	        var _Pipe1 		= new CollectionFilterPipe<String>(_Collection, ComparisonFilter.EQUAL);
	        _Pipe1.SetSourceCollection(_Names);
			
	        var _Counter = 0;
	        while (_Pipe1.MoveNext())
			{
	            _Counter++;
	            var _Name = _Pipe1.Current;
	            Assert.IsTrue(_Name.Equals("peter") || _Name.Equals("josh"));
	        }
	        
			Assert.AreEqual(2, _Counter);

		}

        #endregion
		
	}
	
}
