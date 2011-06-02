var paragraphTests = function() {
	var parser;
	
	return {
		setup: function() {
			parser = createParser();
		},
		
		tearDown: function() {
			parser = null;
		},
		
		test: function Table_tests() {
			return [
				function Should_parse_tableheaders_and_tablerow_lines() {
					var markup = "|! foo |! bar |\n" +
						"| baz | bat |\n" +
						"| qux | meh |";
					
					var expected = "\
<table>\
<tr>\
<th>foo </th>\
<th>bar </th>\
</tr>\
<tr>\
<td>baz </td>\
<td>bat </td>\
</tr>\
<tr>\
<td>qux </td>\
<td>meh </td>\
</tr>\
</table>";
					
					Assert.that(trimLf(parser.parseAndReturn(markup)), Is.equalTo(expected));
				},
				
				function Should_parse_table_with_multiline_rows() {
					var markup = "|{! foo | bar\n" +
						"| bat \n" +
						"| boo |\n" +
						"bal" +
                        "\n" +
						"}|\n" +
						"| baz | bat |";
					
					var expected = "\
<table>\
<tr>\
<th>foo </th>\
<td>bar</td>\
<td>bat </td>\
<td>boo </td>\
<td>bal</td>\
</tr>\
<tr>\
<td>baz </td>\
<td>bat </td>\
</tr>\
</table>";
					
					Assert.that(trimLf(parser.parseAndReturn(markup)), Is.equalTo(expected));
				},
				
				function Should_not_create_table_cell_if_inline_scope_is_not_closed() {
					Assert.that(trimLf(parser.parseAndReturn("| __foo | bar |__")), Is.equalTo("<table><tr><td><strong>foo | bar |</strong></td></tr></table>"));
				}
				
			];
		}
	};
}();

Jarvis.run(paragraphTests);