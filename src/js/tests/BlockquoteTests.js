var blockquoteTests = function() {
	var parser;
	
	return {
		setup: function() {
			parser = createParser();
		},
		
		tearDown: function() {
			parser = null;
		},
		
		test: function Blockquote_tests() {
			return [
				function Should_parse_blockquote_on_same_line() {
					Assert.that(trimLf(parser.parseAndReturn("<<text>>")), Is.equalTo("<blockquote><p>text</p></blockquote>"));	
				},
				
				function Should_parse_blockquote_on_different_line() {
					Assert.that(trimLf(parser.parseAndReturn("<<\ntext\n>>")), Is.equalTo("<blockquote><p>text</p></blockquote>"));	
				},
				
				function Should_allow_formatting_inside_blockquote() {
					Assert.that(trimLf(parser.parseAndReturn("<<oh __hai!__>>")), Is.equalTo("<blockquote><p>oh <strong>hai!</strong></p></blockquote>"));	
				},
				
				function Should_not_allow_blockquote_inside_inline_scope() {
					Assert.that(trimLf(parser.parseAndReturn("__foo\n<<not a blockquote\n__")), Is.equalTo("<p><strong>foo&lt;&lt;not a blockquote</strong></p>"));	
				}
				
			];
		}
	};
}();

Jarvis.run(blockquoteTests);