using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class TextFormattingTests : WikiToHtmlTest {
		[Test]
		public void Should_parse_strong() {
			Convert("__text__");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><strong>text</strong></p>");
		}

		[Test]
		public void Should_parse_emphasis() {
			Convert("''text''");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><em>text</em></p>");
		}

		[Test]
		public void Should_parse_underline() {
			Convert("--text--");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><ins>text</ins></p>");
		}

		[Test]
		public void Should_parse_strikethrough() {
			Convert("---text---");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><del>text</del></p>");
		}

		[Test]
		public void Should_parse_teletype() {
			Convert("==text==");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><tt>text</tt></p>");
		}

		[Test]
		public void Should_parse_formatted_string_within_formatted_string() {
			Convert("__text ''foo''__");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><strong>text <em>foo</em></strong></p>");
		}

		[Test]
		public void Should_parse_big_text() {
			Convert("(+text+)");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><big>text</big></p>");
		}

		[Test]
		public void Should_parse_nested_big_text() {
			Convert("(+text (+bigger+)+)");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><big>text <big>bigger</big></big></p>");
		}

		[Test]
		public void Should_parse_small_text() {
			Convert("(-text-)");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><small>text</small></p>");
		}

		[Test]
		public void Should_parse_nested_small_text() {
			Convert("(-text (-smaller-)-)");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><small>text <small>smaller</small></small></p>");
		}

		[Test]
		public void Can_have_big_closer_without_opener() {
			Convert("foo+)");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p>foo+)</p>");
		}

		[Test]
		public void Can_have_small_closer_without_opener() {
			Convert("foo-)");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p>foo-)</p>");
		}

		[Test]
		public void Should_properly_parse_strikethrough_followed_by_underline() {
			Convert("-----text-----");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><del><ins>text</ins></del></p>");
		}
	}
}