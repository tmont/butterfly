using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class IllegalScopingTests : WikiToHtmlTest {

		[Test]
		public void Should_not_create_paragraph_if_inline_scope_is_not_closed() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("__foo\n\n\nbar__"), "<p><strong>foobar</strong></p>");
		}

		[Test]
		[ExpectedException(typeof(ParseException), ExpectedMessage = "Scopes that need to be manually closed were not closed")]
		public void Should_throw_if_manually_closing_scope_is_not_closed() {
			Parser.Parse("__foo");
		}

		[Test]
		[ExpectedException(typeof(ParseException), ExpectedMessage = "Cannot close table row until all containing scopes are closed")]
		public void Should_throw_if_containing_scopes_in_table_row_are_not_closed() {
			Parser.Parse("|{ __foo }|");
		}

		[Test]
		[ExpectedException(typeof(ParseException), ExpectedMessage = "Cannot close table cell until all containing scopes are closed")]
		public void Should_throw_if_containg_scopes_in_table_cell_are_not_closed() {
			Parser.Parse("| __foo | bar |");
		}

	}
}