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

namespace Scrabbler.Example
{
	public sealed class SwedishWordList : IWordList
	{
		private readonly Dictionary<char, int> _pointTable;
		private readonly FileInfo _file;

		public SwedishWordList(FileInfo file)
		{
			_file = file;
			_pointTable = new Dictionary<char, int>()
			{
				{ 'a', 1}, { 'b', 3}, { 'c', 8}, { 'd', 1}, { 'e', 1}, { 'f', 3}, { 'g', 2}, { 'h', 3}, 
				{ 'i', 1}, { 'j', 7}, { 'k', 3}, { 'l', 2}, { 'm', 3}, { 'n', 1}, { 'o', 2}, { 'p', 4}, 
				{ 'q', 0}, { 'r', 1}, { 's', 1}, { 't', 1}, { 'u', 4}, { 'v', 3}, { 'x', 8}, { 'y', 7}, 
				{ 'z', 8}, { 'å', 4}, { 'ä', 4}, { 'ö', 4}
			};
		}

		public int Calculate(string word)
		{
			int points = 0;
			foreach (char character in word)
			{
				if (_pointTable.ContainsKey(character))
				{
					points += _pointTable[character];
				}
			}
			return points;
		}

		public PrefixTree Build()
		{
			PrefixTree tree = new PrefixTree();
			string[] lines = File.ReadAllLines(_file.FullName);
			foreach (string line in lines)
			{
				if (line.StartsWith("CUSTOM") || line.StartsWith("BASEWORDS") || line.StartsWith("DEFINITION"))
				{
					continue;
				}
				else if (line.StartsWith("COMPOUND"))
				{
					string[] parts = line.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
					if (parts.Length == 2)
					{
						string[] words = parts[1].Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
						foreach (string word in words)
						{
							string temp = word.Trim();
							tree.Insert(temp);
						}
					}
				}
				else
				{
					string[] parts = line.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
					if (parts.Length == 2)
					{
						string[] words = parts[1].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
						foreach (string word in words)
						{
							tree.Insert(word);
						}
					}
				}
			}
			return tree;
		}
	}
}
