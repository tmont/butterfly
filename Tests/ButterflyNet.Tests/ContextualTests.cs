using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class ContextualTests : WikiToHtmlTest {

		[Test]
		public void Should_close_list_when_followed_by_block_scope() {
			Convert(@"* lulz
! oh hai!");

			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<ul><li>lulz</li></ul><h1>oh hai!</h1>");
		}

		[Test]
		public void Should_close_paragraph_before_opening_block_scope() {
			Convert(@"foo
*lulz");

			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<p>foo</p><ul><li>lulz</li></ul>");
		}

		[Test]
		public void Should_close_list_before_opening_paragraph() {
			const string text = @"*lulz
foo";

			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn(text), "<ul><li>lulz</li></ul><p>foo</p>");
		}

		[Test]
		public void Should_close_definition_list_before_opening_paragraph() {
			const string text = @";term
:definition
foo
* lol";

			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn(text), "<dl><dt>term</dt><dd>definition</dd></dl><p>foo</p><ul><li>lol</li></ul>");
		}

		[Test]
		public void Should_handle_contextual_scope_followed_by_paragraph_with_inline_scope() {
			const string text = @"* lol
foo __bar__";

			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn(text), "<ul><li>lol</li></ul><p>foo <strong>bar</strong></p>");
		}

	}
}