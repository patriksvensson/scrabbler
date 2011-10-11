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
	public class PrefixTreeTests
	{
		[Test]
		public void PrefixTree_CountMatchesNumberOfWordsInTree()
		{
			PrefixTree tree = new PrefixTree();
			tree.Insert("CART");
			tree.Insert("DINO");
			tree.Insert("TAR");
			tree.Insert("PEN");

			Assert.AreEqual(4, tree.Count);
		}

		[Test]
		public void PrefixTree_WordsAreInsertedAsLowercase()
		{
			PrefixTree tree = new PrefixTree();
			tree.Insert("CART");
			tree.Insert("DINO");
			tree.Insert("TAR");
			tree.Insert("PEN");

			Assert.IsNotNull(tree.Root.FindNode('c'));
		}

		[Test]
		public void PrefixTree_ExactMatch()
		{
			PrefixTree tree = new PrefixTree();
			tree.Insert("PATRIK");

			Assert.IsFalse(tree.IsExactMatch("PAT"));
			Assert.IsTrue(tree.IsExactMatch("PATRIK"));
		}

		[Test]
		public void PrefixTree_ExactMatchWorksWithWrongCase()
		{
			PrefixTree tree = new PrefixTree();
			tree.Insert("PATRIK");

			Assert.IsFalse(tree.IsExactMatch("pat"));
			Assert.IsTrue(tree.IsExactMatch("patrik"));
		}

		[Test]
		public void PrefixTree_PartialMatch()
		{
			PrefixTree tree = new PrefixTree();
			tree.Insert("PATRIK");

			Assert.IsTrue(tree.IsPartialMatch("P"));
			Assert.IsTrue(tree.IsPartialMatch("PAT"));
			Assert.IsFalse(tree.IsPartialMatch("PAD"));
			Assert.IsFalse(tree.IsPartialMatch("T"));
			Assert.IsFalse(tree.IsPartialMatch(""));
		}

		[Test]
		public void PrefixTree_PartialMatchWorksWithWrongCase()
		{
			PrefixTree tree = new PrefixTree();
			tree.Insert("PATRIK");

			Assert.IsTrue(tree.IsPartialMatch("p"));
			Assert.IsTrue(tree.IsPartialMatch("pat"));
			Assert.IsFalse(tree.IsPartialMatch("pad"));
			Assert.IsFalse(tree.IsPartialMatch("t"));
		}
	}
}
