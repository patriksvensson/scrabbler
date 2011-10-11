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

namespace Scrabbler
{
	public sealed class PrefixTree
	{
		private readonly PrefixTreeNode _root;
		private int _count;

		public PrefixTreeNode Root
		{
			get { return _root; }
		} 

		public int Count
		{
			get { return _count; }
		}

		public PrefixTree()
		{
			_root = new PrefixTreeNode(null, '\0');
		}

		public void Insert(string word)
		{
			if (word.Length == 0)
			{
				throw new ArgumentNullException("word");
			}

			_count++;
			char[] characters = word.ToLower().ToCharArray();
			PrefixTreeNode current = _root;
			for (int index = 0; index < word.Length; index++)
			{
				PrefixTreeNode child = current.FindNode(characters[index]);
				if (child != null)
				{
					current = child;
				}
				else
				{
					current.Children.Add(new PrefixTreeNode(current, characters[index]));
					current = current.FindNode(characters[index]);
				}

				if (index == characters.Length - 1)
				{
					current.IsWord = true;					
				}
			}
		}

		public PrefixTreeNode GetPartialMatch(string s)
		{
			return this.GetMatch(s, false /* Partial match */);
		}

		public PrefixTreeNode GetExactMatch(string s)
		{
			return this.GetMatch(s, true /* Exact match */);
		}

		private PrefixTreeNode GetMatch(string s, bool exact)
		{
			if (string.IsNullOrEmpty(s))
			{
				return null;
			}

			PrefixTreeNode current = _root;
			char[] word = s.ToLowerInvariant().ToCharArray();			
			while (current != null)
			{
				for (int index = 0; index < word.Length; index++)
				{
					if (current.FindNode(word[index]) == null)
					{
						return null;
					}
					current = current.FindNode(word[index]);
				}
				return exact ? (current.IsWord ? current : null) : current;
			}
			return null;
		}
	}
}
