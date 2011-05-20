using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class BlockquoteTests : WikiToHtmlTest {
		[Test]
		public void Should_parse_blockquote_on_same_line() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("<<text>>"), "<blockquote><p>text</p></blockquote>");
		}

		[Test]
		public void Should_parse_blockquote_on_different_line() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("<<\ntext\n>>"), "<blockquote><p>text</p></blockquote>");
		}

		[Test]
		public void Should_allow_formatting_inside_blockquote() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("<<oh __hai!__>>"), "<blockquote><p>oh <strong>hai!</strong></p></blockquote>");
		}

		[Test]
		public void Should_not_allow_blockquote_inside_inline_scope() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("__foo\n<<not a blockquote\n__"), "<p><strong>foo&lt;&lt;not a blockquote</strong></p>");
		}
	}
}