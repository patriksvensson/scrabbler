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
using NUnit.Framework;
using Scrabbler;

namespace Scrabbler.Tests
{
	[TestFixture]
	public class WordSolverTests
	{
		#region Private Nested Classes
		private class Wordlist : IWordList
		{
			private List<string> _words;

			public Wordlist(IEnumerable<string> words)
			{
				_words = new List<string>(words);
			}

			public PrefixTree Build()
			{
				PrefixTree tree = new PrefixTree();
				foreach(string word in _words)
				{
					tree.Insert(word);
				}
				return tree;
			}

			public int Calculate(string word)
			{
				return 0;
			}
		}
		#endregion

		[Test]
		public void WordSolver_Solve()
		{
			Wordlist list = new Wordlist(new string[] { "car", "cart", "tar", "ape", "pen", "dino" });
			WordSolver solver = new WordSolver(list);
			var result = solver.Solve(new char[] { 'c', 'a', 'r', 't', 'p', 'e' });

			Assert.AreEqual(4, result.Length);
			Assert.IsTrue(result.Any(x => x.Word.Equals("car")));
			Assert.IsTrue(result.Any(x => x.Word.Equals("cart")));
			Assert.IsTrue(result.Any(x => x.Word.Equals("tar")));
			Assert.IsTrue(result.Any(x => x.Word.Equals("ape")));
		}
	}
}
