var linkTests = function() {
	var parser;
	
	return {
		setup: function() {
			parser = createParser();
		},
		
		tearDown: function() {
			parser = null;
		},
		
		test: function Link_tests() {
			return [
				function Should_parse_link() {
					Assert.that(trimLf(parser.parseAndReturn("[link]")), Is.equalTo("<p><a href=\"/link\">link</a></p>"));	
				},
				
				function Should_parse_link_with_link_text() {
					Assert.that(trimLf(parser.parseAndReturn("[link|foo]")), Is.equalTo("<p><a href=\"/link\">foo</a></p>"));
				},
				
				function Should_parse_external_link() {
					Assert.that(trimLf(parser.parseAndReturn("[http://example.com/]")), Is.equalTo("<p><a class=\"external\" href=\"http://example.com/\">http://example.com/</a></p>"));
				},
				
				function Should_parse_external_link_with_link_text() {
					Assert.that(trimLf(parser.parseAndReturn("[http://example.com/|foo]")), Is.equalTo("<p><a class=\"external\" href=\"http://example.com/\">foo</a></p>"));
				},
				
				function Should_allow_formatted_text_in_link_text() {
					Assert.that(trimLf(parser.parseAndReturn("[foo|__text__]")), Is.equalTo("<p><a href=\"/foo\"><strong>text</strong></a></p>"));
				},
				
				function Should_allow_formatted_text_and_string_literals_in_link_text() {
					Assert.that(trimLf(parser.parseAndReturn("[foo|foo __text__ bar]")), Is.equalTo("<p><a href=\"/foo\">foo <strong>text</strong> bar</a></p>"));
				},
				
				function Should_allow_open_bracket_inside_link() {
					Assert.that(trimLf(parser.parseAndReturn("[foo|foo[]")), Is.equalTo("<p><a href=\"/foo\">foo[</a></p>"));
				},
				
				function Should_allow_escaped_close_bracket_inside_link() {
					Assert.that(trimLf(parser.parseAndReturn("[foo|foo]]]")), Is.equalTo("<p><a href=\"/foo\">foo]</a></p>"));
				},
				
				function Should_use_base_url() {
					parser.localLinkBaseUrl = "/base/";
					Assert.that(trimLf(parser.parseAndReturn("[foo|bar]")), Is.equalTo("<p><a href=\"/base/foo\">bar</a></p>"));
				}
			];
		}
	};
}();

Jarvis.run(linkTests);