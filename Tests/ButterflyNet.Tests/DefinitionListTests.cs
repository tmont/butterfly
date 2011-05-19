using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class DefinitionListTests : WikiToHtmlTest {
		[Test]
		public void Should_parse_definition_list() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn(";term\n:definition"), "<dl><dt>term</dt><dd>definition</dd></dl>");
		}

		[Test]
		public void Should_allow_formatting_in_definitions() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn(";__te''rm''__\n:definition"), "<dl><dt><strong>te<em>rm</em></strong></dt><dd>definition</dd></dl>");
		}

		[Test]
		public void Should_allow_multiple_paragraphs_inside_definition() {
			const string text = @";term
:{definition

that spans

multiple lines}:";

			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn(text), "<dl><dt>term</dt><dd><p>definition</p><p>that spans</p><p>multiple lines</p></dd></dl>");
		}

		[Test]
		public void Should_allow_formatting_in_multiline_definition() {
			const string text = @";term
:{__definition__}:";

			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn(text), "<dl><dt>term</dt><dd><p><strong>definition</strong></p></dd></dl>");
		}
	}
}