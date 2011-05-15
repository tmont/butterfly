using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class BlockquoteTests : WikiToHtmlTest {
		[Test]
		public void Should_parse_blockquote_on_same_line() {
			Convert("<<text>>");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<blockquote><p>text</p></blockquote>");
		}

		[Test]
		public void Should_parse_blockquote_on_different_line() {
			Convert(@"<<
text
>>");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<blockquote><p>text</p></blockquote>");
		}

		[Test]
		public void Should_allow_formatting_inside_blockquote() {
			Convert("<<oh __hai!__>>");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<blockquote><p>oh <strong>hai!</strong></p></blockquote>");
		}
	}
}