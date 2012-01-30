///*
// * Copyright (c) 2010-2011, Achim 'ahzf' Friedland <code@ahzf.de>
// * This file is part of Blueprints.NET <http://www.github.com/ahzf/Blueprints.NET>
// *
// * Licensed under the Apache License, Version 2.0 (the "License");
// * you may not use this file except in compliance with the License.
// * You may obtain a copy of the License at
// *
// *     http://www.apache.org/licenses/LICENSE-2.0
// *
// * Unless required by applicable law or agreed to in writing, software
// * distributed under the License is distributed on an "AS IS" BASIS,
// * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// * See the License for the specific language governing permissions and
// * limitations under the License.
// */

//#region Usings

//using System;
//using System.IO;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Collections.Generic;
//using System.Diagnostics;

//#endregion

//namespace de.ahzf.Blueprints.Tools
//{

//    /// <summary>
//    /// Utilities to read and write CSV files.
//    /// </summary>
//    public static class CSV
//    {

//        #region GetLines(this myStreamReader)

//        /// <summary>
//        /// Yields one line from the given stream reader.
//        /// </summary>
//        /// <param name="myStreamReader">The stream to read the lines from.</param>
//        /// <returns>A single line.</returns>
//        public static IEnumerable<String> GetLines(this StreamReader myStreamReader)
//        {

//            if (myStreamReader == null)
//                throw new ArgumentNullException("myStreamReader must not be null!");

//            String _Line;

//            while ((_Line = myStreamReader.ReadLine()) != null)
//                yield return _Line;

//        }

//        #endregion

//        #region GetMultipleLines(this StreamReader, NumberOfLines)

//        /// <summary>
//        /// Yields multiple lines from the given stream reader.
//        /// </summary>
//        /// <param name="StreamReader">The stream to read the lines from.</param>
//        /// <param name="NumberOfLines">The number of lines to read at once.</param>
//        /// <returns>Multiple lines.</returns>
//        public static IEnumerable<IEnumerable<String>> GetMultipleLines(this StreamReader StreamReader, Int32 NumberOfLines)
//        {

//            if (StreamReader == null)
//                throw new ArgumentNullException("myStreamReader must not be null!");

//            var _Lines  = new String[NumberOfLines];
//            var _Number = 0;
//            var _Line   = "";

//            while ((_Line = StreamReader.ReadLine()) != null)
//            {

//                if (!_Line.StartsWith("#") && !_Line.StartsWith("//"))
//                {

//                    _Lines[_Number] = _Line;

//                    if (_Number == NumberOfLines - 1)
//                    {
//                        yield return _Lines;
//                        _Lines  = new String[NumberOfLines];
//                        _Number = -1;
//                    }

//                    _Number++;

//                }

//            }

//            Array.Resize(ref _Lines, _Number);
//            yield return _Lines;

//        }

//        #endregion


//        #region ParseFile(Filename, CSVAction, Seperators = null)

//        /// <summary>
//        /// Reads CSV data from the given file and processes them using multiple tasks.
//        /// </summary>
//        /// <param name="Filename">The path and name of the CSV file to read.</param>
//        /// <param name="CSVAction">Something to do with a CSVline seperated into an array of string values.</param>
//        /// <param name="Seperators">The seperators to use for splitting a CSVline into an array of string values.</param>
//        /// <returns>A task of tasks processing the CSV file concurrently.</returns>
//        public static Task ParseFile(String Filename, Action<String[]> CSVAction, Action FinishAction, String[] Seperators = null)
//        {

//            #region Data

//            if (Filename == null)
//                throw new ArgumentNullException("The given Filename must not be null!");

//            if (!File.Exists(Filename))
//                throw new ArgumentNullException("The given file could not be read!");

//            if (CSVAction == null)
//                throw new ArgumentNullException("The given CSVAction must not be null!");

//            var _TaskCancellation = new CancellationTokenSource();

//            if (Seperators == null)
//                Seperators = new String[] { ", ", ": " };

//            #endregion

//            return Task.Factory.StartNew(() =>
//            {

//                using (var _StreamReader = new StreamReader(Filename))
//                {

//                    foreach (var _Line in _StreamReader.GetLines())
//                        if (!_Line.StartsWith("#"))
//                            CSVAction(_Line.Split(Seperators, StringSplitOptions.None));

//                }

//                if (FinishAction != null)
//                    FinishAction();

//            });

//        }

//        #endregion

//        #region ParseFileParallel(Filename, CSVAction, Seperators = null, MainTaskName = "CSVImporter-", LinesPerTask = 20000)

//        /// <summary>
//        /// Reads CSV data from the given file and processes them using multiple tasks.
//        /// </summary>
//        /// <param name="Filename">The path and name of the CSV file to read.</param>
//        /// <param name="CSVAction">Something to do with a CSVline seperated into an array of string values.</param>
//        /// <param name="Seperators">The seperators to use for splitting a CSVline into an array of string values.</param>
//        /// <param name="MainTaskName">The name of the main processing task.</param>
//        /// <param name="LinesPerTask">THe number of CSVlines per task.</param>
//        /// <returns>A task of tasks processing the CSV file concurrently.</returns>
//        public static Task ParseFileParallel(String Filename, Action<String[]> CSVAction, String[] Seperators = null, String MainTaskName = "CSVImporter-", Int32 LinesPerTask = 20000)
//        {

//            #region Data

//            if (Filename == null)
//                throw new ArgumentNullException("The given Filename must not be null!");

//            if (!File.Exists(Filename))
//                throw new ArgumentNullException("The given file could not be read!");

//            if (CSVAction == null)
//                throw new ArgumentNullException("The given CSVAction must not be null!");

//            var _TaskCancellation = new CancellationTokenSource();

//            if (Seperators == null)
//                Seperators = new String[] { ", ", ": " };

//            #endregion

//            return Task.Factory.StartNew(() =>
//            {

//                using (var _StreamReader = new StreamReader(Filename))
//                {

//                    Console.WriteLine("Importing CSV file '{0}':", Filename);
//                    Thread.CurrentThread.Name = MainTaskName + "Task";

//                    Task[] _Tasks = (from _Lines in _StreamReader.GetMultipleLines(LinesPerTask)
//                                     select Task.Factory.StartNew((_Lines2) =>
//                                     {

//                                         //Thread.CurrentThread.Name = myMainTaskName + "ChildTask";
//                                         Debug.WriteLine("Task {0} started!", Thread.CurrentThread.ManagedThreadId);

//                                         foreach (var _Line in (IEnumerable<String>) _Lines2)
//                                             CSVAction(_Line.Split(Seperators, StringSplitOptions.RemoveEmptyEntries));

//                                         Debug.WriteLine("Task {0} finished!", Thread.CurrentThread.ManagedThreadId);

//                                     },
//                                         _Lines,
//                                         _TaskCancellation.Token,
//                                         TaskCreationOptions.AttachedToParent,
//                                         TaskScheduler.Current)).ToArray();


//                    Debug.WriteLine("Waiting for child tasks to complete!");

//                    Task.Factory.ContinueWhenAll(_Tasks, Tasks =>
//                    {
//                        foreach (var _Task in Tasks)
//                            _Task.Dispose();
//                    },

//                        _TaskCancellation.Token,
//                        TaskContinuationOptions.None,
//                        TaskScheduler.Current);

//                }

//            });

//        }

//        #endregion


//        #region WriteToFile(myData, myFileName, mySeperator = ", ")

//        /// <summary>
//        /// Writes the given CSV data into a file.
//        /// </summary>
//        /// <param name="myData">The CSV data.</param>
//        /// <param name="myFileName">The name of the output file.</param>
//        /// <param name="mySeperator">A string to seperate the individual CSV entries.</param>
//        public static void WriteToFile(IEnumerable<IEnumerable<Int32>> myData, String myFileName, String mySeperator = ", ")
//        {

//            var _VertexNumber = 0;
//            var _TextWriter = new StreamWriter(myFileName);

//            foreach (var _HashSet in myData)
//            {
//                _TextWriter.WriteLine(_HashSet.Aggregate(_VertexNumber.ToString(),
//                                      (sum, next) => sum.ToString() + mySeperator + next));
//                _VertexNumber++;
//            }

//            _TextWriter.Close();

//        }

//        #endregion

//    }

//}
