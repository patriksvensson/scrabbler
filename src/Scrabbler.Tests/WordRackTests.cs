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

namespace Scrabbler.Tests
{
	[TestFixture]
	public class WordRackTests
	{
		[Test]
		public void WordRack_OnlyLettersProvidedInConstructorIsAddedToAvailableLetters()
		{
			WordRack rack = new WordRack(new char[] { 'a', 'b', '3', 'c', 'd', '#' });

			Assert.IsTrue(rack.AvailableLetters.Contains('a'));
			Assert.IsTrue(rack.AvailableLetters.Contains('b'));
			Assert.IsTrue(rack.AvailableLetters.Contains('c'));
			Assert.IsTrue(rack.AvailableLetters.Contains('d'));
			Assert.IsFalse(rack.AvailableLetters.Contains('3'));
			Assert.IsFalse(rack.AvailableLetters.Contains('#'));
		}
	}
}
