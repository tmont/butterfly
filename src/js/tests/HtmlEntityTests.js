var htmlEntityTests = function() {
	var parser;
	
	return {
		setup: function() {
			parser = createParser();
		},
		
		tearDown: function() {
			parser = null;
		},
		
		test: function HTML_entity_tests() {
			return [
				function Should_display_named_html_entity() {
					Assert.that(trimLf(parser.parseAndReturn("[:entity|value=hellip]")), Is.equalTo("<p>&hellip;</p>"));
				},
				
				function Should_display_numbered_html_entity() {
					Assert.that(trimLf(parser.parseAndReturn("[:entity|value=#8567]")), Is.equalTo("<p>&#8567;</p>"));
				},
				
				function Should_require_value_property() {
					Assert.willThrow(new Butterfly.ModuleException("The \"value\" property must be set to a valid HTML entity"));
					parser.parse("[:entity]");
				},
				
				function Should_not_allow_empty_entity_value() {
					Assert.willThrow(new Butterfly.ModuleException("The \"value\" property must be set to a valid HTML entity"));
					parser.parse("[:entity|value=]");
				},
				
				function Should_validate_entity_value() {
					Assert.willThrow(new Butterfly.ModuleException("\"<script>malicious code</script>\" is not a valid HTML entity"));
					parser.parse("[:entity|value=<script>malicious code</script>]");
				}
			];
		}
	};
}();

Jarvis.run(htmlEntityTests);