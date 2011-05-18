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

	}
}