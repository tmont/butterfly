var contextualTests = function() {
	var parser;
	
	return {
		setup: function() {
			parser = createParser();
		},
		
		tearDown: function() {
			parser = null;
		},
		
		test: function Contextual_tests() {
			return [
				function Should_close_list_when_followed_by_block_scope() {
					Assert.that(trimLf(parser.parseAndReturn("* lulz\n! oh hai!")), Is.equalTo("<ul><li>lulz</li></ul><h1>oh hai!</h1>"));
				},
				
				function Should_close_paragraph_before_opening_block_scope() {
					Assert.that(trimLf(parser.parseAndReturn("foo\n*lulz")), Is.equalTo("<p>foo</p><ul><li>lulz</li></ul>"));
				},
				
				function Should_close_list_before_opening_paragraph() {
					Assert.that(trimLf(parser.parseAndReturn("*lulz\nfoo")), Is.equalTo("<ul><li>lulz</li></ul><p>foo</p>"));
				},
				
				function Should_close_definition_list_before_opening_paragraph() {
					Assert.that(trimLf(parser.parseAndReturn(";term\n:definition\nfoo")), Is.equalTo("<dl><dt>term</dt><dd>definition</dd></dl><p>foo</p>"));
				},
				
				function Should_close_paragraph_before_opening_list() {
					Assert.that(trimLf(parser.parseAndReturn("foo\n* list")), Is.equalTo("<p>foo</p><ul><li>list</li></ul>"));
				},
				
				function Should_close_paragraph_before_opening_definition_list() {
					Assert.that(trimLf(parser.parseAndReturn("foo\n;list")), Is.equalTo("<p>foo</p><dl><dt>list</dt></dl>"));
				},
				
				function Should_handle_contextual_scope_followed_by_paragraph_with_inline_scope() {
					Assert.that(trimLf(parser.parseAndReturn("* lol\nfoo __bar__")), Is.equalTo("<ul><li>lol</li></ul><p>foo <strong>bar</strong></p>"));
				}
			];
		}
	};
}();

Jarvis.run(contextualTests);