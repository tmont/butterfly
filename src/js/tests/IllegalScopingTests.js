var illegalScopingTests = function() {
	var parser;
	
	return {
		setup: function() {
			parser = createParser();
		},
		
		tearDown: function() {
			parser = null;
		},
		
		test: function Illegal_scoping_tests() {
			return [
				function Should_not_create_paragraph_if_inline_scope_is_not_closed() {
					Assert.that(trimLf(parser.parseAndReturn("__foo\n\n\nbar__")), Is.equalTo("<p><strong>foobar</strong></p>"));
				},
				
				function Should_throw_if_manually_closing_scope_is_not_closed() {
					Assert.willThrow(new Butterfly.ParseException("The following scope must be manually closed: strong"));
					parser.parse("__foo");
				},
				
				function Should_throw_if_containing_scopes_in_table_row_are_not_closed() {
					Assert.willThrow(new Butterfly.ParseException("Cannot close table row until all containing scopes are closed"));
					parser.parse("|{ __foo }|");
				}
			];
		}
	};
}();

Jarvis.run(illegalScopingTests);