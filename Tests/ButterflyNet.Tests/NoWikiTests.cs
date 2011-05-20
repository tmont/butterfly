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
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn(@"[!foo]]]"), "<p>foo]</p>");
		}

		[Test]
		[ExpectedException(typeof(ParseException), ExpectedMessage = "NoWiki scope never closes")]
		public void Should_throw_when_nowiki_never_closes() {
			Parser.ParseAndReturn(@"[!foo");
		}

	}
}