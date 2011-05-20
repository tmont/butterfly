using NUnit.Framework;

namespace ButterflyNet.Parser.Tests {
	[TestFixture]
	public class TableTests : WikiToHtmlTest {
		[Test]
		public void Should_parse_tableheaders_and_tablerow_lines() {
			const string text = @"|! foo |! bar |
| baz | bat |
";

			const string expected = @"
<table>
<tr>
<th>foo </th>
<th>bar </th>
</tr>
<tr>
<td>baz </td>
<td>bat </td>
</tr>
</table>
";

			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn(text), expected.Replace("\r", "").Replace("\n", ""));
		}

		[Test]
		public void Should_parse_table_with_multiline_rows() {
			const string text = @"|{! foo | bar
| bat 
| boo |
bal

}|
| baz | bat |
";

			const string expected = @"
<table>
<tr>
<th>foo </th>
<td>bar</td>
<td>bat </td>
<td>boo </td>
<td>bal</td>
</tr>
<tr>
<td>baz </td>
<td>bat </td>
</tr>
</table>
";

			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn(text), expected.Replace("\r", "").Replace("\n", ""));
		}

		[Test]
		public void Should_not_create_table_cell_if_inline_scope_is_not_closed() {
			AssertWithNoRegardForLineBreaks(Parser.ParseAndReturn("| __foo | bar |__"), "<table><tr><td><strong>foo | bar |</strong></td></tr></table>");
		}
	}
}