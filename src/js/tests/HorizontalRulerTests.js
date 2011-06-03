var hruleTests = function() {
	var parser;
	
	return {
		setup: function() {
			parser = createParser();
		},
		
		tearDown: function() {
			parser = null;
		},
		
		test: function Horizontal_ruler_tests() {
			return [
				function Should_parse_horizontal_ruler_with_eof() {
					Assert.that(trimLf(parser.parseAndReturn("----")), Is.equalTo("<hr />"));	
				},
				
				function Should_parse_horizontal_ruler_with_linebreak() {
					Assert.that(trimLf(parser.parseAndReturn("----\nlulz")), Is.equalTo("<hr /><p>lulz</p>"));	
				},
				
				function Should_not_allow_horizontal_ruler_inside_inline_scope() {
					Assert.that(trimLf(parser.parseAndReturn("__foo\n----\nbar---__")), Is.equalTo("<p><strong>foo<del>-bar</del></strong></p>"));	
				}
			];
		}
	};
}();

Jarvis.run(hruleTests);