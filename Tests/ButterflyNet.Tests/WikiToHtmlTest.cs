﻿using System;
using System.IO;
using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public abstract class WikiToHtmlTest {
		protected StringWriter Writer { get; private set; }
		protected ButterflyParser Parser { get; private set; }

		protected static void AssertWithNoRegardForLineBreaks(string actual, string text) {
			Assert.That(actual.Replace("\n", ""), Is.EqualTo(text));
		}

		[SetUp]
		public virtual void SetUp() {
			Writer = new StringWriter();
			Parser = new ButterflyParser { ModuleFactory = new ActivatorFactory<IButterflyModule>(new NamedTypeRegistry<IButterflyModule>().LoadDefaults()) }
				.LoadDefaultStrategies(new DefaultParseStrategyFactory())
				.AddAnalyzer(new HtmlAnalyzer(Writer));
		}

		[Obsolete("Use Parser.Parse(wikitext) instead")]
		protected void Convert(string wikitext) {
			Parser.Parse(wikitext);
		}
	}
}
