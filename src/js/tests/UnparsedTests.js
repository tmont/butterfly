var unparsedTests = function() {
	var parser;
	
	return {
		setup: function() {
			parser = createParser();
		},
		
		tearDown: function() {
			parser = null;
		},
		
		test: function Unparsed_tests() {
			return [
				function Should_not_parse_inside_inline_scope() {
					Assert.that(
						trimLf(parser.parseAndReturn("__bold [!==not teletype==]__")), 
						Is.equalTo("<p><strong>bold ==not teletype==</strong></p>")
					);
				},
				
				function Should_allow_multiline_escaping() {
					Assert.that(
						trimLf(parser.parseAndReturn("foo [!oh hai!\n* not a list\n| not a table |\n]")), 
						Is.equalTo("<p>foo oh hai!* not a list| not a table |</p>")
					);	
				},
				
				function Should_break_at_first_closing_bracket() {
					Assert.that(
						trimLf(parser.parseAndReturn("[!oh hai!] __bold__]")), 
						Is.equalTo("<p>oh hai! <strong>bold</strong>]</p>")
					);	
				},
				
				function Should_escape_close_bracket() {
					Assert.that(trimLf(parser.parseAndReturn("[!foo]]]")), Is.equalTo("<p>foo]</p>"));	
				},
				
				function Should_throw_when_nowiki_never_closes() {
					Assert.willThrow(new ParseException("Unparsed scope never closes"));
					parser.parse("[!foo");
				}
			];
		}
	};
}();

Jarvis.run(unparsedTests);