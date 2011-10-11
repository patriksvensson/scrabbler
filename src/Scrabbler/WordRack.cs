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
	internal sealed class WordRack
	{
		private readonly List<char> _originalCharacters;
		private readonly List<char> _availableCharacters;

		public List<char> AvailableLetters
		{
			get { return _availableCharacters; }
		}

		public WordRack(IEnumerable<char> characters)
		{
			_originalCharacters = new List<char>(this.Sanitize(characters));
			_availableCharacters = new List<char>(_originalCharacters);
		}

		private WordRack(IEnumerable<char> original, IEnumerable<char> available)
		{
			_originalCharacters = new List<char>(original);
			_availableCharacters = new List<char>(available);
		}

		private char[] Sanitize(IEnumerable<char> characters)
		{
			List<char> letters = new List<char>();
			foreach (char character in characters)
			{
				if (char.IsLetter(character))
				{
					char temp = character;
					if (!char.IsLower(temp))
					{
						temp = char.ToLower(temp);
					}
					letters.Add(temp);
				}
			}
			return letters.ToArray();
		}

		public void Consume(char character)
		{
			if (_availableCharacters.Contains(character))
			{
				_availableCharacters.Remove(character);
			}
		}

		public WordRack Clone()
		{
			return new WordRack(_originalCharacters, _availableCharacters);
		}

		public override string ToString()
		{
			return new string(_availableCharacters.ToArray());
		}
	}
}
