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
	}
}