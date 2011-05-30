var preformattedTests = function() {
	var parser;
	
	return {
		setup: function() {
			parser = createParser();
		},
		
		tearDown: function() {
			parser = null;
		},
		
		test: function Preformatted_tests() {
			return [
				function Should_parse_language() {
					Assert.that(trimEnd(parser.parseAndReturn("{{{javascript\nlulz}}}")), Is.equalTo("<pre class=\"sunlight-highlight-javascript\">lulz</pre>"));
				},
				
				function Should_not_get_in_infinite_loop_while_parsing_language() {
					Assert.willThrow(new ParseException("No strategy found for <EOF> at index 3"));
					parser.parse("{{{");
				},
				
				function Should_keep_linebreaks_inside_preformatted() {
					Assert.that(trimEnd(parser.parseAndReturn("{{{\n\nlulz\n\n}}}")), Is.equalTo("<pre>\nlulz\n\n</pre>"));
				},
				
				function Should_allow_formatting_inside_preformatted() {
					Assert.that(trimLf(parser.parseAndReturn("{{{\n__bold__}}}")), Is.equalTo("<pre><strong>bold</strong></pre>"));
				},
				
				function Should_not_be_able_to_nest_preformatted_blocks() {
					Assert.that(trimLf(parser.parseAndReturn("{{{\nnot {{{nested}}}}}}")), Is.equalTo("<pre>not {{{nested</pre><p>}}}</p>"));
				},
				
				function Should_parse_preformatted_line() {
					Assert.that(trimLf(parser.parseAndReturn(" text")), Is.equalTo("<pre>text</pre>"));
				},
				
				function Should_parse_preformatted_line_and_close_on_line_break() {
					Assert.that(trimLf(parser.parseAndReturn(" text\nlol")), Is.equalTo("<pre>text</pre><p>lol</p>"));
				},
				
				function Should_ignore_formatting_inside_preformatted_code() {
					Assert.that(trimEnd(parser.parseAndReturn("{{{{\n__bold__\n}}}}")), Is.equalTo("<pre>__bold__\n</pre>"));
				},
				
				function Should_throw_when_preformatted_code_never_closes() {
					Assert.willThrow(new ParseException("Preformatted scope never closes"));
					parser.parse("{{{{oh hai!");
				},
				
			];
		}
	};
}();

Jarvis.run(preformattedTests);