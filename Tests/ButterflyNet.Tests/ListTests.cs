using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class ListTests : WikiToHtmlTest {
		[Test]
		public void Should_parse_unordered_list() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("* lol"), "<ul><li>lol</li></ul>");
		}

		[Test]
		public void Should_parse_ordered_list() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("# lol"), "<ol><li>lol</li></ol>");
		}

		[Test]
		public void Should_parse_nested_list() {
			const string text = @"# lol
#* nested
#** nested again
#** lulz
#* back to depth=2";

			var expected = Regex.Replace(@"
<ol>
	<li>lol
		<ul>
			<li>nested
				<ul>
					<li>nested again</li>
					<li>lulz</li>
				</ul>
			</li>
			<li>back to depth=2</li>
		</ul>
	</li>
</ol>", @"^\s*", "", RegexOptions.Multiline).Replace(Environment.NewLine, "");

			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn(text), expected);
		}

		[Test]
		[ExpectedException(typeof(ParseException), ExpectedMessage = "Expected list of type OrderedList but got UnorderedList")]
		public void Should_not_allow_mixed_list_types_at_same_depth() {
			Parser.Parse(@"# lol
* lol");
		}

		[Test]
		[ExpectedException(typeof(ParseException), ExpectedMessage = "Expected list of type OrderedList but got UnorderedList")]
		public void Should_not_allow_mixed_list_types_at_previous_depth() {
			Parser.Parse(@"# lol
## lol
* lol");
		}

		[Test]
		[ExpectedException(typeof(ParseException), ExpectedMessage = "Nested lists cannot skip levels: expected a depth of less than or equal to 2 but got 3")]
		public void Should_catch_invalid_nesting_levels() {
			Parser.Parse(@"# lol
### lol");
		}

		[Test]
		public void Should_allow_formatting_in_list() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("* lol __bold and ''italic''__"), "<ul><li>lol <strong>bold and <em>italic</em></strong></li></ul>");
		}

		[Test]
		public void Should_parse_simple_list() {
			const string text = @"* foo
* bar
* baz";

			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn(text), "<ul><li>foo</li><li>bar</li><li>baz</li></ul>");
		}

		[Test]
		public void Should_parse_list_followed_by_paragraph() {
			const string text = @"* foo
* bar

oh hai";

			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn(text), "<ul><li>foo</li><li>bar</li></ul><p>oh hai</p>");
		}

		
	}
}