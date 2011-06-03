using System;
using ButterflyNet.Parser.Macros;
using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class MacroTests : WikiToHtmlTest {
		[Test]
		public void Should_update_input_with_macro_value() {
			var parser = new ButterflyParser();
			parser.LoadDefaultStrategies(new DefaultParseStrategyFactory());
			parser.MacroFactory = new ActivatorFactory<IButterflyMacro>(new NamedTypeRegistry<IButterflyMacro>().RegisterType<FooMacro>("foo"));

			var result = parser.Parse("[::foo] foo [::foo]");
			var text = parser.FlushAndReturn();

			Assert.That(result.Markup, Is.EqualTo("bar foo bar"));
			AssertWithNoRegardForLineBreaks(text, "<p>bar foo bar</p>");
		}

		[Test]
		public void Should_parse_markup_in_macros() {
			var parser = new ButterflyParser();
			parser.LoadDefaultStrategies(new DefaultParseStrategyFactory());
			parser.MacroFactory = new ActivatorFactory<IButterflyMacro>(new NamedTypeRegistry<IButterflyMacro>().RegisterType<BarMacro>("bar"));

			var result = parser.Parse("[::bar]");
			var text = parser.FlushAndReturn();

			Assert.That(result.Markup, Is.EqualTo("__bold__"));
			AssertWithNoRegardForLineBreaks(text, "<p><strong>bold</strong></p>");
		}

		[Test]
		public void Should_show_current_timestamp() {
			var parser = new ButterflyParser();
			parser.LoadDefaultStrategies(new DefaultParseStrategyFactory());
			parser.MacroFactory = new ActivatorFactory<IButterflyMacro>(new NamedTypeRegistry<IButterflyMacro>().RegisterType<TimestampMacro>("timestamp"));

			var text = parser.ParseAndReturn("[::timestamp]");
			Assert.That(text, Is.StringMatching(@"<p>\d{4}-\d\d-\d\d \d\d:\d\d:\d\dZ</p>"));
		}

		[Test]
		public void Should_show_current_timestamp_with_different_format() {
			var parser = new ButterflyParser();
			parser.LoadDefaultStrategies(new DefaultParseStrategyFactory());
			parser.MacroFactory = new ActivatorFactory<IButterflyMacro>(new NamedTypeRegistry<IButterflyMacro>().RegisterType<TimestampMacro>("timestamp"));

			var text = parser.ParseAndReturn("[::timestamp|format=yyyy]");
			AssertWithNoRegardForLineBreaks(text, string.Format("<p>{0}</p>", DateTime.UtcNow.Year));
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