using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class HorizontalRulerTests : WikiToHtmlTest {
		[Test]
		public void Should_parse_horizontal_ruler_with_eof() {
			Convert("----");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<hr />");
		}

		[Test]
		public void Should_parse_horizontal_ruler_with_linebreak() {
			Convert(@"----
lulz");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<hr /><p>lulz</p>");
		}

	}
}