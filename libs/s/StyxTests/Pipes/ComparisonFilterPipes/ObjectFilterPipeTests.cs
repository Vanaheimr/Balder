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
    public class ObjectFilterPipeTests
	{

		#region testNullObjects()

        [Test]
        public void testNullObjects()
        {	

			var _Starts = new List<String>() { "marko", "pavel", null };
	        var _Pipe 	= new ObjectFilterPipe<String>(null, ComparisonFilter.NOT_EQUAL);
	        _Pipe.SetSourceCollection(_Starts);
			
	        int _Counter = 0;
	        while (_Pipe.MoveNext())
			{
	            _Counter++;
	            Assert.IsNull(_Pipe.Current);
	        }
	        
			Assert.AreEqual(1, _Counter);
	
	        
			_Pipe = new ObjectFilterPipe<String>(null, ComparisonFilter.EQUAL);
	        _Pipe.SetSourceCollection(_Starts);
	        
			_Counter = 0;
	        while (_Pipe.MoveNext())
			{
	            _Counter++;
	            var _String = _Pipe.Current;
	            Assert.IsTrue(_String.Equals("marko") || _String.Equals("pavel"));
	        }
	        
			Assert.AreEqual(2, _Counter);
	
	        
			_Pipe = new ObjectFilterPipe<String>(null, ComparisonFilter.GREATER_THAN);
	        _Pipe.SetSourceCollection(_Starts);
	        
			_Counter = 0;
	        while (_Pipe.MoveNext())
			{
	            _Counter++;
	        }
	        
			Assert.AreEqual(0, _Counter);
	
		}

        #endregion

		#region testObjectFilter()

        [Test]
        public void testObjectFilter()
        {	

			var _Starts = new List<String>() { "marko", "josh", "peter" };
	        var _Pipe	= new ObjectFilterPipe<String>("marko", ComparisonFilter.EQUAL);
	        _Pipe.SetSource(_Starts.GetEnumerator());
	        
	        int _Counter = 0;
	        while (_Pipe.MoveNext())
			{
	            var next = _Pipe.Current;
	            Assert.IsTrue(next.Equals("josh") || next.Equals("peter"));
	            _Counter++;
	        }
			
	        Assert.AreEqual(2, _Counter);
	
			
	        _Pipe = new ObjectFilterPipe<String>("marko", ComparisonFilter.NOT_EQUAL);
	        _Pipe.SetSource(_Starts.GetEnumerator());

			_Counter = 0;
	        while (_Pipe.MoveNext())
			{
	            var next = _Pipe.Current;
	            Assert.IsTrue(next.Equals("marko"));
	            _Counter++;
	        }
	        
			Assert.AreEqual(1, _Counter);

		}

        #endregion

		#region testNumericComparisons()

        [Test]
        public void testNumericComparisons()
        {	

			var _Starts = new List<Int32>() { 32, 1, 7 };
	        var _Pipe 	= new ObjectFilterPipe<Int32>(6, ComparisonFilter.GREATER_THAN);
	        _Pipe.SetSource(_Starts.GetEnumerator());
			
	        int _Counter = 0;
	        while (_Pipe.MoveNext())
			{
	            var next = _Pipe.Current;
	            Assert.IsTrue(next.Equals(1));
	            _Counter++;
	        }

			Assert.AreEqual(1, _Counter);

			// -----------------------
			
			_Pipe = new ObjectFilterPipe<Int32>(8, ComparisonFilter.GREATER_THAN_EQUAL);
	        _Pipe.SetSource(_Starts.GetEnumerator());

			_Counter = 0;
	        while (_Pipe.MoveNext())
			{
	            var next = _Pipe.Current;
	            Assert.IsTrue(next.Equals(1) || next.Equals(7));
	            _Counter++;
	        }
	        
			Assert.AreEqual(2, _Counter);

			// -----------------------
			
			_Pipe = new ObjectFilterPipe<Int32>(8, ComparisonFilter.LESS_THAN);
	        _Pipe.SetSource(_Starts.GetEnumerator());

			_Counter = 0;
	        while (_Pipe.MoveNext())
			{
	            var next = _Pipe.Current;
	            Assert.IsTrue(next.Equals(32));
	            _Counter++;
	        }
	        
			Assert.AreEqual(1, _Counter);

			// -----------------------

			_Pipe = new ObjectFilterPipe<Int32>(6, ComparisonFilter.LESS_THAN_EQUAL);
	        _Pipe.SetSource(_Starts.GetEnumerator());
	        
			_Counter = 0;
	        while (_Pipe.MoveNext())
			{
	            var next = _Pipe.Current;
	            Assert.IsTrue(next.Equals(7) || next.Equals(32));
	            _Counter++;
	        }
			
	        Assert.AreEqual(2, _Counter);

		}

        #endregion
		
	}
	
}
