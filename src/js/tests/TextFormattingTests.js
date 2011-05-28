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
				},
				
				function Should_parse_underline() {
					Assert.that(trimLf(parser.parseAndReturn("--text--")), Is.equalTo("<p><ins>text</ins></p>"));
				},
				
				function Should_parse_strike_through() {
					Assert.that(trimLf(parser.parseAndReturn("---text---")), Is.equalTo("<p><del>text</del></p>"));
				},
				
				function Should_parse_teletype() {
					Assert.that(trimLf(parser.parseAndReturn("==text==")), Is.equalTo("<p><tt>text</tt></p>"));
				},
				
				function Should_parse_formatted_string_within_formatted_string() {
					Assert.that(trimLf(parser.parseAndReturn("__text ''foo''__")), Is.equalTo("<p><strong>text <em>foo</em></strong></p>"));
				},
				
				function Should_parse_big_text() {
					Assert.that(trimLf(parser.parseAndReturn("(+text+)")), Is.equalTo("<p><big>text</big></p>"));
				},
				
				function Should_parse_nested_big_text() {
					Assert.that(trimLf(parser.parseAndReturn("(+text (+bigger+)+)")), Is.equalTo("<p><big>text <big>bigger</big></big></p>"));
				},
				
				function Should_parse_small_text() {
					Assert.that(trimLf(parser.parseAndReturn("(-text-)")), Is.equalTo("<p><small>text</small></p>"));
				},
				
				function Should_parse_nested_small_text() {
					Assert.that(trimLf(parser.parseAndReturn("(-text (-smaller-)-)")), Is.equalTo("<p><small>text <small>smaller</small></small></p>"));
				},
				
				function Can_have_big_closer_without_opener() {
					Assert.that(trimLf(parser.parseAndReturn("foo+)")), Is.equalTo("<p>foo+)</p>"));
				},
				
				function Can_have_small_closer_without_opener() {
					Assert.that(trimLf(parser.parseAndReturn("foo-)")), Is.equalTo("<p>foo-)</p>"));
				},
				
				function Should_properly_parse_strikethrough_followed_by_underline() {
					Assert.that(trimLf(parser.parseAndReturn("-----text-----")), Is.equalTo("<p><del><ins>text</ins></del></p>"));
				}
			];
		}
	};
}();

Jarvis.run(textFormattingTests);