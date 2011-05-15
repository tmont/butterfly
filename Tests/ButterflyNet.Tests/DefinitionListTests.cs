using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class DefinitionListTests : WikiToHtmlTest {
		[Test]
		public void Should_parse_definition_list() {
			Parser.Parse(@";term
:definition");

			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<dl><dt>term</dt><dd>definition</dd></dl>");
		}

		[Test]
		public void Should_allow_formatting_in_definitions() {
			Parser.Parse(@";__te''rm''__
:definition");

			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<dl><dt><strong>te<em>rm</em></strong></dt><dd>definition</dd></dl>");
		}
	}
}