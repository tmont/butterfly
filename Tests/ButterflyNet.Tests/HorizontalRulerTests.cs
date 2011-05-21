using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class HorizontalRulerTests : WikiToHtmlTest {

		[Test]
		public void Should_parse_horizontal_ruler_with_eof() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("----"), "<hr />");
		}

		[Test]
		public void Should_parse_horizontal_ruler_with_linebreak() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("----\nlulz"), "<hr /><p>lulz</p>");
		}

		[Test]
		public void Should_not_allow_horizontal_ruler_inside_inline_scope() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("__foo\n----\nbar---__"), "<p><strong>foo<del>-bar</del></strong></p>");
		}
	}
}