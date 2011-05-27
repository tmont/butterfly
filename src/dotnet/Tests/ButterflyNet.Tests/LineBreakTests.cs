using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class LineBreakTests : WikiToHtmlTest {
		[Test]
		public void Should_add_line_break_inside_inline_scope() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("__foo %%% bar__"), "<p><strong>foo <br /> bar</strong></p>");
		}

		[Test]
		public void Should_add_line_break_inside_text() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("foo %%% bar"), "<p>foo <br /> bar</p>");
		}

		[Test]
		public void Should_add_line_break_inside_paragraph() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("%%%foo"), "<p><br />foo</p>");
		}
	}
}