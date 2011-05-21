using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class TextFormattingTests : WikiToHtmlTest {
		[Test]
		public void Should_parse_strong() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("__text__"), "<p><strong>text</strong></p>");
		}

		[Test]
		public void Should_parse_emphasis() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("''text''"), "<p><em>text</em></p>");
		}

		[Test]
		public void Should_parse_underline() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("--text--"), "<p><ins>text</ins></p>");
		}

		[Test]
		public void Should_parse_strikethrough() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("---text---"), "<p><del>text</del></p>");
		}

		[Test]
		public void Should_parse_teletype() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("==text=="), "<p><tt>text</tt></p>");
		}

		[Test]
		public void Should_parse_formatted_string_within_formatted_string() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("__text ''foo''__"), "<p><strong>text <em>foo</em></strong></p>");
		}

		[Test]
		public void Should_parse_big_text() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("(+text+)"), "<p><big>text</big></p>");
		}

		[Test]
		public void Should_parse_nested_big_text() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("(+text (+bigger+)+)"), "<p><big>text <big>bigger</big></big></p>");
		}

		[Test]
		public void Should_parse_small_text() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("(-text-)"), "<p><small>text</small></p>");
		}

		[Test]
		public void Should_parse_nested_small_text() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("(-text (-smaller-)-)"), "<p><small>text <small>smaller</small></small></p>");
		}

		[Test]
		public void Can_have_big_closer_without_opener() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("foo+)"), "<p>foo+)</p>");
		}

		[Test]
		public void Can_have_small_closer_without_opener() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("foo-)"), "<p>foo-)</p>");
		}

		[Test]
		public void Should_properly_parse_strikethrough_followed_by_underline() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("-----text-----"), "<p><del><ins>text</ins></del></p>");
		}
	}
}