using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class LinkTests : WikiToHtmlTest {
		[Test]
		public void Should_parse_wiki_link() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("[link]"), "<p><a href=\"/link\">link</a></p>");
		}

		[Test]
		public void Should_parse_wiki_link_with_link_text() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("[link|foo]"), "<p><a href=\"/link\">foo</a></p>");
		}

		[Test]
		public void Should_parse_external_link() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("[http://example.com/]"), "<p><a class=\"external\" href=\"http://example.com/\">http://example.com/</a></p>");
		}

		[Test]
		public void Should_parse_external_link_with_link_text() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("[http://example.com/|foo]"), "<p><a class=\"external\" href=\"http://example.com/\">foo</a></p>");
		}

		[Test]
		public void Should_allow_formatted_text_in_link_text() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("[foo|__text__]"), "<p><a href=\"/foo\"><strong>text</strong></a></p>");
		}

		[Test]
		public void Should_allow_formatted_text_and_string_literals_in_link_text() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("[foo|bar __text__ baz]"), "<p><a href=\"/foo\">bar <strong>text</strong> baz</a></p>");
		}

		[Test]
		public void Should_allow_open_bracket_inside_link() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("[foo|foo[]"), "<p><a href=\"/foo\">foo[</a></p>");
		}

		[Test]
		public void Should_allow_escaped_close_bracket_inside_link() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("[foo|foo]]"), "<p><a href=\"/foo\">foo]</a></p>");
		}
	}
}