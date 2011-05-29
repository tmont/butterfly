var headerTests = function() {
	var parser;
	
	return {
		setup: function() {
			parser = createParser();
		},
		
		tearDown: function() {
			parser = null;
		},
		
		test: function Header_tests() {
			return [
				function Should_parse_headers_with_depth_one_through_six() {
					var markup = 
						"! one\n" +
						"!! two\n" +
						"!!! three\n" +
						"!!!! four\n" +
						"!!!!! five\n" +
						"!!!!!! six";
					
					Assert.that(trimLf(parser.parseAndReturn(markup)), Is.equalTo("<h1>one</h1><h2>two</h2><h3>three</h3><h4>four</h4><h5>five</h5><h6>six</h6>"));
				},
				
				function Should_treat_depth_greater_than_six_as_six() {
					Assert.that(trimLf(parser.parseAndReturn("!!!!!!! lol")), Is.equalTo("<h6>lol</h6>"));
					
				},
				
				function Should_allow_formatting_in_header() {
					Assert.that(trimLf(parser.parseAndReturn("! lol __bold and ''italic''__")), Is.equalTo("<h1>lol <strong>bold and <em>italic</em></strong></h1>"));
				},
				
				function Should_not_create_header_if_inline_scope_is_not_closed() {
					Assert.that(trimLf(parser.parseAndReturn("__foo\n!header__")), Is.equalTo("<p><strong>foo!header</strong></p>"));
				}
			];
		}
	};
}();

Jarvis.run(headerTests);