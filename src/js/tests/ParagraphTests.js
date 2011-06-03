var paragraphTests = function() {
	var parser;
	
	return {
		setup: function() {
			parser = createParser();
		},
		
		tearDown: function() {
			parser = null;
		},
		
		test: function Paragraph_tests() {
			return [
				function Should_create_paragraph_after_double_linebreak() {
					Assert.that(trimLf(parser.parseAndReturn("lulz\n\nlulz")), Is.equalTo("<p>lulz</p><p>lulz</p>"));
				},
				
				function Should_not_create_paragraph_after_single_linebreak() {
					Assert.that(trimLf(parser.parseAndReturn("lulz\nlulz")), Is.equalTo("<p>lulzlulz</p>"));
				},
				
				function Should_not_create_paragraph_on_double_linebreak_if_scopes_are_not_closed() {
					Assert.that(trimLf(parser.parseAndReturn("__bold\n\n\nfoo__")), Is.equalTo("<p><strong>boldfoo</strong></p>"));
				}
			];
		}
	};
}();

Jarvis.run(paragraphTests);