using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class NoWikiTests : WikiToHtmlTest {

		[Test]
		public void Should_escape_wiki_syntax_inside_inline_scope() {
			Convert("__bold [!==not teletype==]__");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><strong>bold ==not teletype==</strong></p>");
		}

		[Test]
		public void Should_allow_multiline_escaping() {
			Convert(@"foo [!oh hai!
* not a list
| not a table |
]");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p>foo oh hai!* not a list| not a table |</p>");
		}

	}
}