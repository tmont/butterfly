var textFormattingTests = function() {
	var parser;
	
	return {
		setup: function() {
			parser = createParser();
		},
		
		tearDown: function() {
			parser = null;
		},
		
		test: function Text_formatting_tests() {
			return [
				function Should_parse_strong() {
					Assert.that(trimLf(parser.parseAndReturn("__text__")), Is.equalTo("<p><strong>text</strong></p>"));
				},
				
				function Should_parse_emphasis() {
					Assert.that(trimLf(parser.parseAndReturn("''text''")), Is.equalTo("<p><em>text</em></p>"));
				}
			];
		}
	};
}();

Jarvis.run(textFormattingTests);