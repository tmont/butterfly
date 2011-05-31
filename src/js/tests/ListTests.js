var listTests = function() {
	var parser;
	
	return {
		setup: function() {
			parser = createParser();
		},
		
		tearDown: function() {
			parser = null;
		},
		
		test: function List_tests() {
			return [
				function Should_parse_unordered_list() {
					Assert.that(trimLf(parser.parseAndReturn("* lol")), Is.equalTo("<ul><li>lol</li></ul>"));
				},
				
				function Should_parse_ordered_list() {
					Assert.that(trimLf(parser.parseAndReturn("# lol")), Is.equalTo("<ol><li>lol</li></ol>"));
				},
				
				function Should_parse_nested_list() {
					var markup = "# lol\n\
#* nested\n\
#** nested again\n\
#** lulz\n\
#*** one more\n\
#** back to depth=3\n\
# and all the way back";

					var expected = "\
<ol>\
	<li>lol\
		<ul>\
			<li>nested\
				<ul>\
					<li>nested again</li>\
					<li>lulz\
						<ul>\
							<li>one more</li>\
						</ul>\
					</li>\
					<li>back to depth=3</li>\
				</ul>\
			</li>\
		</ul>\
	</li>\
	<li>and all the way back</li>\
</ol>".replace(/[\t\n]/g, "");

					Assert.that(trimLf(parser.parseAndReturn(markup)), Is.equalTo(expected));
				},
				
				function Should_not_allow_mixed_list_types_at_same_depth() {
					Assert.willThrow(new ParseException("Expected list of type orderedList but got unorderedList"));
					parser.parse("# lol\n* lol");
				},
				
				function Should_not_allow_mixed_list_types_at_previous_depth() {
					Assert.willThrow(new ParseException("Expected list of type orderedList but got unorderedList"));
					parser.parse("# lol\n## lol\n* lol");
				},
				
				function Should_catch_invalid_nesting_levels() {
					Assert.willThrow(new ParseException("Nested lists cannot skip levels: expected a depth of less than or equal to 2 but got 3"));
					parser.parse("# lol\n### lol");
				},
				
				function Should_not_allow_starting_a_list_with_a_depth_greater_than_one() {
					Assert.willThrow(new ParseException("Cannot start a list with a depth greater than one"));
					parser.parse("** lol");
				},
				
				function Should_allow_formatting_in_list() {
					Assert.that(trimLf(parser.parseAndReturn("* lol __bold and ''italic''__")), Is.equalTo("<ul><li>lol <strong>bold and <em>italic</em></strong></li></ul>"));
				},
				
				function Should_parse_simple_list() {
					Assert.that(trimLf(parser.parseAndReturn("* foo\n* bar\n* baz")), Is.equalTo("<ul><li>foo</li><li>bar</li><li>baz</li></ul>"));
				},
				
				function Should_parse_list_followed_by_paragraph() {
					Assert.that(trimLf(parser.parseAndReturn("* foo\n* bar\n\noh hai")), Is.equalTo("<ul><li>foo</li><li>bar</li></ul><p>oh hai</p>"));
				},
				
				function Should_not_close_list_if_inline_scope_is_not_closed() {
					Assert.that(trimLf(parser.parseAndReturn("* __foo\n* bar__")), Is.equalTo("<ul><li><strong>foo* bar</strong></li></ul>"));
				}
			];
		}
	};
}();

Jarvis.run(listTests);