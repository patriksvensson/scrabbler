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
	public sealed class PrefixTreeNode
	{
		private readonly char _letter;		
		private readonly IList<PrefixTreeNode> _children;
		private readonly PrefixTreeNode _parent;
		private bool _isWord;

		internal PrefixTreeNode(PrefixTreeNode parent, char letter)
		{
			_parent = parent;
			_children = new List<PrefixTreeNode>();
			_isWord = false;
			_letter = letter;
		}

		public char Letter
		{
			get { return _letter; }
		}

		public bool IsWord
		{
			get { return this._isWord; }
			internal set { _isWord = value; }
		}

		public IList<PrefixTreeNode> Children
		{
			get { return _children; }
		}

		private PrefixTreeNode Parent
		{
			get { return _parent; }
		} 

		public PrefixTreeNode FindNode(char letter)
		{
			if (_children != null)
			{
				foreach (PrefixTreeNode child in _children)
				{
					if (child.Letter == letter)
					{
						return child;
					}
				}
			}
			return null;
		}

		public string GetWord()
		{
			List<char> sb = new List<char>();
			sb.Add(_letter);
			PrefixTreeNode parent = this.Parent;
			while (parent != null && parent.Letter != '\0')
			{
				sb.Add(parent.Letter);
				parent = parent.Parent;
			}
			return new string(sb.Reverse<char>().ToArray());
		}
	}
}
