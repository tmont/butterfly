using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class ParagraphTests : WikiToHtmlTest {
		[Test]
		public void Should_create_paragraph_after_double_linebreak() {
			Convert(@"lulz

lulz");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p>lulz</p><p>lulz</p>");
		}

		[Test]
		public void Should_not_create_paragraph_inside_preformatted() {
			Convert(@"{{{


}}}");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<pre></pre>");
		}

		[Test]
		public void Should_not_create_paragraph_on_double_linebreak_if_scopes_are_not_closed() {
			Convert(@"__bold


foo__");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p><strong>boldfoo</strong></p>");
		}
	}
}