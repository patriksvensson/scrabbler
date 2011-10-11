﻿//
// Copyright 2011 Patrik Svensson
//
// This file is part of Scrabbler.
//
// Scrabbler is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Scrabbler is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Lesser Public License for more details.
//
// You should have received a copy of the GNU Lesser Public License
// along with Scrabbler. If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace Scrabbler.Example
{
	public class Program
	{
		public static void Main(string[] args)
		{
			WordSolver solver = Program.CreateWordSolver(args);

			while (true)
			{
				char[] input = Program.ReadInput();
				if (input.Length == 0)
				{
					break;
				}

				// Solve for the words.
				DateTime solveStarted = DateTime.Now;
				var words = solver.Solve(input).OrderByDescending(x => x.Points).Take(5);
				TimeSpan searchTime = DateTime.Now - solveStarted;

				if (words.Any())
				{
					Program.WriteWordTable(words);

					Console.WriteLine();
					Console.WriteLine("Search took {0:f} seconds.", searchTime.TotalSeconds);
					Console.WriteLine();
				}
			}
		}

		private static WordSolver CreateWordSolver(string[] args)
		{
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.WriteLine("SCRABBLE HELPER");
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("\nReading word list and building tree...");

			// Create the solver.
			DateTime startIndex = DateTime.Now;
			string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			string defaultFilePath = Path.Combine(path, "dsso-1.46.txt");
			FileInfo file = new FileInfo(args.Length == 1 ? args[0] : defaultFilePath);
			WordSolver solver = new WordSolver(new SwedishWordList(file));
			TimeSpan indexTime = DateTime.Now - startIndex;

			Console.WriteLine("\n # Indexed {0} words in {1:f} seconds.", solver.IndexedWordCount, indexTime.TotalSeconds);
			Console.WriteLine(" # Limiting results to the 5 best matches.");
			Console.WriteLine(" # Exit the program by pressing ENTER.\n");
			return solver;
		}

		private static char[] ReadInput()
		{
			Console.Write("Enter letters -> ");
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.White;
			string input = Console.ReadLine();
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.Gray;
			return input.ToCharArray();
		}

		private static void WriteWordTable(IEnumerable<WordResult> words)
		{
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("\n-----------------------");
			Console.WriteLine("Word       |     Points");
			Console.WriteLine("-----------------------");
			foreach (var word in words)
			{
				Console.WriteLine("{0,-10} | {1,10}", word.Word, word.Points);
			}
			Console.WriteLine("-----------------------");
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.Gray;
		}
	}
}
