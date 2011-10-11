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
using System.IO;

namespace Scrabbler
{
	public sealed class WordSolver
	{
		private readonly PrefixTree _tree;
		private readonly IWordList _wordList;

		public int IndexedWordCount
		{
			get { return _tree.Count; }
		} 

		public WordSolver(IWordList wordList)
		{
			#region Sanity Check
			if (wordList == null)
			{
				throw new ArgumentNullException("wordList", "No word list provided.");
			}
			#endregion			

			_wordList = wordList;
			_tree = _wordList.Build();
		}

		public WordResult[] Solve(IEnumerable<char> characters)
		{
			// Sanitize input.
			List<WordResult> results = new List<WordResult>();
			string[] words = this.Solve(new WordRack(characters), _tree.Root);
			foreach (string word in words.Distinct())
			{
				results.Add(new WordResult(word, _wordList.Calculate(word)));
			}
			return results.OrderByDescending(x => x.Points).ToArray();
		}

		private string[] Solve(WordRack originalRack, PrefixTreeNode root)
		{
			List<string> words = new List<string>();
			foreach (char character in originalRack.AvailableLetters)
			{
				WordRack rack = originalRack.Clone();
				PrefixTreeNode node = root.Children.FirstOrDefault(x => x.Letter == character);
				if (node != null)
				{
					rack.Consume(character);
					if (node.IsWord)
					{
						words.Add(node.GetWord());
					}

					string[] result = this.Solve(rack, node);
					if (result != null && result.Length > 0)
					{
						words.AddRange(result);
					}
				}
			}
			return words.ToArray();
		}
	}
}
