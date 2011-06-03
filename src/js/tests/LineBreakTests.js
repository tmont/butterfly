var lineBreakTests = function() {
	var parser;
	
	return {
		setup: function() {
			parser = createParser();
		},
		
		tearDown: function() {
			parser = null;
		},
		
		test: function Line_break_tests() {
			return [
				function Should_add_line_break_inside_inline_scope() {
					Assert.that(trimLf(parser.parseAndReturn("__foo %%% bar__")), Is.equalTo("<p><strong>foo <br /> bar</strong></p>"));	
				},
				
				function Should_add_line_break_inside_text() {
					Assert.that(trimLf(parser.parseAndReturn("foo %%% bar")), Is.equalTo("<p>foo <br /> bar</p>"));	
				},
				
				function Should_add_line_break_inside_paragraph() {
					Assert.that(trimLf(parser.parseAndReturn("%%%foo")), Is.equalTo("<p><br />foo</p>"));	
				}
			];
		}
	};
}();

Jarvis.run(lineBreakTests);