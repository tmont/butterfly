using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class ParagraphTests : WikiToHtmlTest {
		[Test]
		public void Should_create_paragraph_after_double_linebreak() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("lulz\n\nlulz"), "<p>lulz</p><p>lulz</p>");
		}

		[Test]
		public void Should_not_create_paragraph_after_single_linebreak() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("lulz\nlulz"), "<p>lulzlulz</p>");
		}

		[Test]
		public void Should_not_create_paragraph_on_double_linebreak_if_scopes_are_not_closed() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("__bold\n\n\nfoo__"), "<p><strong>boldfoo</strong></p>");
		}
	}
}