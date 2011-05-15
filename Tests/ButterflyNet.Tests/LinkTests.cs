using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class LinkTests : WikiToHtmlTest {
		[Test]
		public void Should_parse_wiki_link() {
			Convert("[link]");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><a href=\"/link\">link</a></p>");
		}

		[Test]
		public void Should_parse_wiki_link_with_link_text() {
			Convert("[link|foo]");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><a href=\"/link\">foo</a></p>");
		}

		[Test]
		public void Should_parse_external_link() {
			Convert("[http://example.com/]");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><a class=\"external\" href=\"http://example.com/\">http://example.com/</a></p>");
		}

		[Test]
		public void Should_parse_external_link_with_link_text() {
			Convert("[http://example.com/|foo]");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><a class=\"external\" href=\"http://example.com/\">foo</a></p>");
		}

		[Test]
		public void Should_allow_formatted_text_in_link_text() {
			Convert("[foo|__text__]");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><a href=\"/foo\"><strong>text</strong></a></p>");
		}

		[Test]
		public void Should_allow_formatted_text_and_string_literals_in_link_text() {
			Convert("[foo|bar __text__ baz]");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><a href=\"/foo\">bar <strong>text</strong> baz</a></p>");
		}

		[Test]
		public void Should_allow_open_bracket_inside_link() {
			Convert("[foo|foo[]");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><a href=\"/foo\">foo[</a></p>");
		}

		[Test]
		public void Should_allow_escaped_close_bracket_inside_link() {
			Convert("[foo|foo]]]");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><a href=\"/foo\">foo]</a></p>");
		}
	}
}