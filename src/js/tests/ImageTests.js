var iamgeTests = function() {
	var parser;
	
	return {
		setup: function() {
			parser = createParser();
		},
		
		tearDown: function() {
			parser = null;
		},
		
		test: function Image_tests() {
			return [
				function Should_parse_image() {
					Assert.that(
						trimLf(parser.parseAndReturn("[:image|url=foo.png]")), 
						Is.equalTo("<p><img src=\"/foo.png\" alt=\"foo.png\" title=\"foo.png\" /></p>")
					);
				},
				
				function Should_parse_external_image() {
					Assert.that(
						trimLf(parser.parseAndReturn("[:image|url=http://example.com/foo.png]")), 
						Is.equalTo("<p><img src=\"http://example.com/foo.png\" alt=\"http://example.com/foo.png\" title=\"http://example.com/foo.png\" /></p>")
					);
				},
				
				function Should_parse_image_with_width() {
					Assert.that(
						trimLf(parser.parseAndReturn("[:image|url=foo.png|width=100]")), 
						Is.equalTo("<p><img src=\"/foo.png\" alt=\"foo.png\" title=\"foo.png\" width=\"100\" /></p>")
					);
				},
				
				function Should_parse_image_with_height() {
					Assert.that(
						trimLf(parser.parseAndReturn("[:image|url=foo.png|height=150]")), 
						Is.equalTo("<p><img src=\"/foo.png\" alt=\"foo.png\" title=\"foo.png\" height=\"150\" /></p>")
					);
				},
				
				function Should_parse_image_with_custom_alt_text() {
					Assert.that(
						trimLf(parser.parseAndReturn("[:image|url=foo.png|alt=oh hai!]")), 
						Is.equalTo("<p><img src=\"/foo.png\" alt=\"oh hai!\" title=\"foo.png\" /></p>")
					);
				},
				
				function Should_parse_image_with_multiple_options() {
					Assert.that(
						trimLf(parser.parseAndReturn("[:image|url=foo.png|alt=oh hai!|width=100|height=234]")), 
						Is.equalTo("<p><img src=\"/foo.png\" alt=\"oh hai!\" title=\"foo.png\" width=\"100\" height=\"234\" /></p>")
					);
				},
				
				function Should_allow_open_bracket_inside_image() {
					Assert.that(
						trimLf(parser.parseAndReturn("[:image|url=foo.png|alt=foo[]")), 
						Is.equalTo("<p><img src=\"/foo.png\" alt=\"foo[\" title=\"foo.png\" /></p>")
					);
				},
				
				function Should_allow_escaped_close_bracket_inside_image() {
					Assert.that(
						trimLf(parser.parseAndReturn("[:image|url=foo.png|alt=foo]]]")), 
						Is.equalTo("<p><img src=\"/foo.png\" alt=\"foo]\" title=\"foo.png\" /></p>")
					);
				},
				
				function Should_parse_image_with_title() {
					Assert.that(
						trimLf(parser.parseAndReturn("[:image|url=foo.png|title=lulz]")), 
						Is.equalTo("<p><img src=\"/foo.png\" alt=\"foo.png\" title=\"lulz\" /></p>")
					);
				},
				
				function Should_require_url() {
					Assert.willThrow(new Butterfly.ModuleException("For images, the URL must be specified"));
					parser.parse("[:image]");
				},
				
				function Should_use_base_url() {
					parser.localImageBaseUrl = "/images/";
					Assert.that(
						trimLf(parser.parseAndReturn("[:image|url=foo.png]")), 
						Is.equalTo("<p><img src=\"/images/foo.png\" alt=\"foo.png\" title=\"foo.png\" /></p>")
					);
				}
			];
		}
	};
}();

Jarvis.run(iamgeTests);