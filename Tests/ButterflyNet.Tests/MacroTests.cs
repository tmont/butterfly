using System;
using System.IO;
using ButterflyNet.Parser.Macros;
using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class MacroTests : WikiToHtmlTest {
		[Test]
		public void Should_update_input_with_macro_value() {
			var parser = new ButterflyParser();
			parser.LoadDefaultStrategies(new DefaultParseStrategyFactory());
			parser.AddAnalyzer(new HtmlAnalyzer(new StringWriter()));
			parser.MacroFactory = new ActivatorFactory<IButterflyMacro>(new NamedTypeRegistry<IButterflyMacro>().RegisterType<FooMacro>("foo"));

			var result = parser.Parse("[::foo] foo [::foo]");
			var text = parser.FlushAndReturn();

			Assert.That(result.WikiText, Is.EqualTo("bar foo bar"));
			Assert.That(text, Is.EqualTo("<p>bar foo bar</p>"));
		}

		[Test]
		public void Should_parse_wiki_formatting_in_macros() {
			var parser = new ButterflyParser();
			parser.LoadDefaultStrategies(new DefaultParseStrategyFactory());
			parser.AddAnalyzer(new HtmlAnalyzer(new StringWriter()));
			parser.MacroFactory = new ActivatorFactory<IButterflyMacro>(new NamedTypeRegistry<IButterflyMacro>().RegisterType<BarMacro>("bar"));

			var result = parser.Parse("[::bar]");
			var text = parser.FlushAndReturn();

			Assert.That(result.WikiText, Is.EqualTo("__bold__"));
			Assert.That(text, Is.EqualTo("<p><strong>bold</strong></p>"));
		}

		[Test]
		public void Should_show_current_timestamp() {
			var parser = new ButterflyParser();
			parser.LoadDefaultStrategies(new DefaultParseStrategyFactory());
			parser.AddAnalyzer(new HtmlAnalyzer(new StringWriter()));
			parser.MacroFactory = new ActivatorFactory<IButterflyMacro>(new NamedTypeRegistry<IButterflyMacro>().RegisterType<TimestampMacro>("timestamp"));

			var text = parser.ParseAndReturn("[::timestamp]");
			Assert.That(text, Is.StringMatching(@"<p>\d{4}-\d\d-\d\d \d\d:\d\d:\d\d</p>"));
		}

		[Test]
		public void Should_show_current_timestamp_with_different_format() {
			var parser = new ButterflyParser();
			parser.LoadDefaultStrategies(new DefaultParseStrategyFactory());
			parser.AddAnalyzer(new HtmlAnalyzer(new StringWriter()));
			parser.MacroFactory = new ActivatorFactory<IButterflyMacro>(new NamedTypeRegistry<IButterflyMacro>().RegisterType<TimestampMacro>("timestamp"));

			var text = parser.ParseAndReturn("[::timestamp|format=yyyy]");
			Assert.That(text, Is.EqualTo(string.Format("<p>{0}</p>", DateTime.UtcNow.Year)));
		}

		public class FooMacro : IButterflyMacro {
			public string GetValue() {
				return "bar";
			}
		}

		public class BarMacro : IButterflyMacro {
			public string GetValue() {
				return "__bold__";
			}
		}
	}
}