using System;
using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class ListTests : WikiToHtmlTest {
		[Test]
		public void Should_parse_unordered_list() {
			Convert("* lol");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<ul><li>lol</li></ul>");
		}

		[Test]
		public void Should_parse_ordered_list() {
			Convert("# lol");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<ol><li>lol</li></ol>");
		}

		[Test]
		public void Should_parse_nested_list() {
			Convert(@"# lol
#* nested
#** nested again
#** lulz
#* back to depth=2");

			var expected = @"
<ol>
<li>lol</li>
<ul>
<li>nested</li>
<ul>
<li>nested again</li>
<li>lulz</li>
</ul>
<li>back to depth=2</li>
</ul>
</ol>".Replace(Environment.NewLine, "");

			AssertWithNoRegardForLineBreaks(Writer.ToString(), expected);
		}

		[Test]
		[ExpectedException(typeof(ParseException), ExpectedMessage = "Expected list of type OrderedList but got UnorderedList")]
		public void Should_not_allow_mixed_list_types_at_same_depth() {
			Convert(@"# lol
* lol");
		}

		[Test]
		[ExpectedException(typeof(ParseException), ExpectedMessage = "Expected list of type OrderedList but got UnorderedList")]
		public void Should_not_allow_mixed_list_types_at_previous_depth() {
			Convert(@"# lol
## lol
* lol");
		}

		[Test]
		[ExpectedException(typeof(ParseException), ExpectedMessage = "Nested lists cannot skip levels: expected a depth of less than or equal to 2 but got 3")]
		public void Should_catch_invalid_nesting_levels() {
			Convert(@"# lol
### lol");
		}

		[Test]
		public void Should_allow_formatting_in_list() {
			Convert("* lol __bold and ''italic''__");
			AssertWithNoRegardForLineBreaks(Writer.ToString(), "<ul><li>lol <strong>bold and <em>italic</em></strong></li></ul>");
		}

		[Test]
		public void Should_parse_consecutive_lists_as_one_list() {
			const string text = @"* lol


* lulz";

			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn(text), "<ul><li>lol</li><li>lulz</li></ul>");
		}

		
	}
}