using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class NoWikiTests : WikiToHtmlTest {

		[Test]
		public void Should_escape_wiki_syntax_inside_inline_scope() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("__bold [!==not teletype==]__"), "<p><strong>bold ==not teletype==</strong></p>");
		}

		[Test]
		public void Should_allow_multiline_escaping() {
			const string text = @"foo [!oh hai!
* not a list
| not a table |
]";
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn(text), "<p>foo oh hai!* not a list| not a table |</p>");
		}

		[Test]
		public void Should_escape_close_bracket() {
			const string text = @"[!foo]]]";
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn(text), "<p>foo]</p>");
		}

	}
}