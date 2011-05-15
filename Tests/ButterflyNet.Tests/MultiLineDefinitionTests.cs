using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class MultiLineDefinitionTests : WikiToHtmlTest {

		[Test]
		public void Should_allow_multiple_paragraphs_inside_definition() {
			const string text = @";term
:{definition

that spans

multiple lines}:";

			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn(text), "<dl><dt>term</dt><dd><p>definition</p><p>that spans</p><p>multiple lines</p></dd></dl>");
		}

		[Test]
		public void Should_allow_formatting() {
			const string text = @";term
:{__definition__}:";

			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn(text), "<dl><dt>term</dt><dd><p><strong>definition</strong></p></dd></dl>");
		}

	}
}
